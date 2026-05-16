using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace FoodHubCustomer
{
    public partial class AddReviewForm : Form
    {
        private string connString = "Host=localhost;Username=foodhub_customer;Password=CustomerPass123;Database=FoodHubDB";

        // เปลี่ยนมาเก็บ ID ลูกค้า และ เบอร์โทรศัพท์ ที่ค้นหาเจอ
        private int selectedCustomerId = 0;
        private string searchedPhone = "";

        public AddReviewForm()
        {
            InitializeComponent();
        }

        // 1. ฟังก์ชันเมื่อกดปุ่ม Search (ค้นหาด้วยเบอร์โทร)
        private void btnSearch_Click(object? sender, EventArgs e)
        {
            string phone = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์ก่อนกดค้นหา", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            searchedPhone = phone;
            // เรียกใช้ฟังก์ชันโหลดข้อมูลการจองที่ทานเสร็จแล้ว
            LoadCompletedBookingsByPhone(phone);
        }

        // 2. ฟังก์ชันโหลดรายการจองที่มีสถานะ 'Completed' เท่านั้น
        private void LoadCompletedBookingsByPhone(string phone)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    // SQL Query ตัวใหม่: ค้นหาด้วยเบอร์โทรศัพท์ และดักเฉพาะสถานะ Completed (มาทานแล้วจริง)
                    string query = @"
                        SELECT b.bookingid, c.customerid, r.name AS restaurantname, b.restaurantid, b.bookingdate, rev.comment, rev.rating
                        FROM bookings b
                        JOIN customers c ON b.customerid = c.customerid
                        JOIN restaurants r ON b.restaurantid = r.restaurantid
                        LEFT JOIN reviews rev ON b.customerid = rev.customerid AND b.restaurantid = rev.restaurantid
                        WHERE c.phone = @phone AND b.status = 'Completed'
                        ORDER BY b.bookingdate DESC";

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);

                    // ป้องกันสัญกรณ์ประเภทข้อมูลเพี้ยนด้วย Varchar
                    var param = new NpgsqlParameter("phone", NpgsqlDbType.Varchar);
                    param.Value = phone;
                    da.SelectCommand.Parameters.Add(param);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    // ซ่อนคอลัมน์ที่ไม่จำเป็นต้องแสดงบนหน้าจอ
                    if (dataGridView1.Columns.Contains("restaurantid"))
                        dataGridView1.Columns["restaurantid"]!.Visible = false;
                    if (dataGridView1.Columns.Contains("customerid"))
                        dataGridView1.Columns["customerid"]!.Visible = false;

                    // จัดการขนาดตารางให้สวยงาม
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    if (dt.Rows.Count > 0)
                    {
                        // เก็บรหัสลูกค้าของแถวแรกไว้ใช้งานตอน Insert รีวิว
                        selectedCustomerId = Convert.ToInt32(dt.Rows[0]["customerid"]);
                    }
                    else
                    {
                        selectedCustomerId = 0;
                        MessageBox.Show("ไม่พบประวัติการจองที่ทานเสร็จเรียบร้อยแล้ว (Completed) สำหรับเบอร์โทรนี้\n\n*หมายเหตุ: ต้องเดินทางไปทานที่ร้านจริงก่อนจึงจะสามารถรีวิวได้*",
                                        "ไม่สามารถรีวิวได้", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. บันทึกรีวิวเมื่อกดปุ่ม Enter Review
        private void btnEnter_Click(object? sender, EventArgs e)
        {
            if (selectedCustomerId == 0 || dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("กรุณาค้นหาเบอร์โทรศัพท์ที่มีประวัติการเข้าทานอาหารก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("กรุณาคลิกเลือกแถวของร้านอาหารที่ต้องการรีวิวในตารางก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("กรุณาพิมพ์ข้อความรีวิวลงในช่อง Review Text ก่อนบันทึก", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. ดึง ID ของร้าน และ ID ของการจองจากตาราง
                int restId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["restaurantid"].Value);
                int bookingId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["bookingid"].Value);

                // 2. ⚡ ดึงค่าคะแนนที่ลูกค้าเลือกจาก ComboBox (แปลงเป็นตัวเลข int)
                // ถ้าผู้ใช้ไม่ได้เลือก ให้กำหนดค่า Default เป็น 5 ดาวป้องกันเออเร่อ
                int userRating = cboRating.SelectedItem != null ? Convert.ToInt32(cboRating.SelectedItem) : 5;

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string insertQuery = @"
            INSERT INTO reviews (customerid, restaurantid, bookingid, rating, comment, reviewdate) 
            VALUES (@cid, @rid, @bid, @rate, @comment, CURRENT_TIMESTAMP)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@cid", selectedCustomerId);
                        cmd.Parameters.AddWithValue("@rid", restId);
                        cmd.Parameters.AddWithValue("@bid", bookingId);
                        cmd.Parameters.AddWithValue("@rate", userRating); // ⚡ เปลี่ยนจากเลข 5 ล็อกตายตัว มาใช้ค่า userRating จาก ComboBox ครับ!
                        cmd.Parameters.AddWithValue("@comment", textBox2.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("บันทึกข้อมูลการรีวิวและให้คะแนนสำเร็จเรียบร้อยแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Clear();
                    cboRating.SelectedIndex = 0; // รีเซ็ตกลับไปเลือก 5 ดาวเหมือนเดิมหลังจากบันทึกเสร็จ

                    // รีเฟรชตัวตารางรีวิวเพื่ออัปเดตข้อความล่าสุด
                    LoadCompletedBookingsByPhone(searchedPhone);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}