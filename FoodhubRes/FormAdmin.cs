using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace FoodhubRes
{
    public partial class FormAdmin : Form
    {
        // ใช้สิทธิ์ Admin ตามที่ตั้งไว้
        private string connString = "Host=localhost;Port=5432;Username=foodhub_admin;Password=AdminPass123;Database=FoodHubDB";

        public FormAdmin()
        {
            InitializeComponent();
        }



        // --- 1. โหลดโปรโมชั่นทั้งหมด (รวมชื่อร้านจากตาราง Restaurants) ---
        // ปรับให้ฟังก์ชันนี้รับค่า resId เข้ามาดักเสมอ
        private void LoadAllPromotions(string resId)
        {
            // ถ้าไม่มีการส่ง ID มา หรือเป็นค่าว่าง ไม่ต้องดึงอะไรมาโชว์เลยเพื่อความปลอดภัย
            if (string.IsNullOrEmpty(resId))
            {
                dataGridView1.DataSource = null;
                return;
            }

            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    // จุดตาย: ต้องมี WHERE p.restaurantid = @resId ตรงนี้ด้วยครับ!
                    string sql = @"SELECT p.promotionid AS ""Promotion ID"", r.name AS ""Restaurant"", 
                           p.name AS ""Promotion"", p.description AS ""Description"", 
                           p.discountamount AS ""Discount"", p.status AS ""Status"",
                           p.startdate AS ""Start Date"", p.enddate AS ""End Date""
                           FROM promotions p
                           JOIN restaurants r ON p.restaurantid = r.restaurantid
                           WHERE p.restaurantid = @resId
                           ORDER BY p.promotionid ASC";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("resId", int.Parse(resId));
                        DataTable dt = new DataTable();
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                            dataGridView1.DataSource = dt;
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error โหลดข้อมูล: " + ex.Message); }
        }

        // --- 2. ฟังก์ชันกลางสำหรับรัน SELECT และแสดงผลใน DataGridView ---
        private void ExecuteQuery(string sql)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            dataGridView1.DataSource = dt;
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // --- 3. ปุ่ม ENTER (ค้นหาโปรโมชั่นตาม ID ร้านอาหาร) ---
        private void button1_Click(object sender, EventArgs e)
        {
            // 1. ดึงค่าจากช่องบนสุดที่เราเพิ่งตั้งชื่อว่า txtSearchId
            string resId = btnSearch.Text.Trim();

            // ตรวจสอบค่าว่างเหมือนเดิม
            if (string.IsNullOrEmpty(resId))
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("กรุณากรอก Restaurant ID ของร้านคุณก่อนทำการค้นหาหรือจัดการข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. ส่งค่าเลข ID จากช่องบน ลงไปแปะที่ช่อง txtResId ด้านล่างด้วย 
            // เพื่อเวลาคุณกดปุ่ม Save หรือ Add New ด้านล่าง ระบบจะได้รู้ว่านี่คือของร้านอาหาร ID นี้นะ
            txtResId.Text = resId;

            // --- โค้ดส่วนดึงข้อมูลเข้า DataGridView ด้านล่าง (ใช้ resId วิ่งไปหาใน DB เหมือนเดิม) ---
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string sql = @"SELECT p.promotionid AS ""Promotion ID"", r.name AS ""Restaurant"", 
                           p.name AS ""Promotion"", p.description AS ""Description"", 
                           p.discountamount AS ""Discount"", p.status AS ""Status"",
                           p.startdate AS ""Start Date"", p.enddate AS ""End Date""
                           FROM promotions p
                           JOIN restaurants r ON p.restaurantid = r.restaurantid
                           WHERE p.restaurantid = @resId
                           ORDER BY p.promotionid ASC";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("resId", int.Parse(resId));
                        DataTable dt = new DataTable();
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                            dataGridView1.DataSource = dt;
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error ค้นหา: " + ex.Message); }
        }

        // --- 4. คลิกแถวในตาราง แล้วข้อมูลเด้งเข้า TextBox ---
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtPromoId.Text = row.Cells["Promotion ID"].Value.ToString();
                txtPromoName.Text = row.Cells["Promotion"].Value.ToString();
                txtDiscount.Text = row.Cells["Discount"].Value.ToString();
                txtPromoStatus.SelectedItem = row.Cells["Status"].Value.ToString();
                // --- ใส่เพิ่ม 3 บรรทัดนี้ (ต้องอัปเดตฟังก์ชัน LoadAllPromotions ให้ดึงคอลัมน์พวกนี้ขึ้นมา) ---
                if (row.Cells["Description"].Value != null)
                    txtDescription.Text = row.Cells["Description"].Value.ToString();

                if (row.Cells["Start Date"].Value != DBNull.Value && row.Cells["Start Date"].Value != null)
                    dtpStart.Value = Convert.ToDateTime(row.Cells["Start Date"].Value);

                if (row.Cells["End Date"].Value != DBNull.Value && row.Cells["End Date"].Value != null)
                    dtpEnd.Value = Convert.ToDateTime(row.Cells["End Date"].Value);

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPromoId.Text))
            {
                MessageBox.Show("กรุณาเลือกโปรโมชั่นที่ต้องการแก้ไขจากตารางก่อน");
                return;
            }
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string sql = @"UPDATE Promotions 
                           SET name = @name, 
                               description = @desc,
                               discountamount = @discount, 
                               status = @status,
                               startdate = @start,
                               enddate = @end
                           WHERE promotionid = @id";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("name", txtPromoName.Text);
                        cmd.Parameters.AddWithValue("desc", txtDescription.Text);
                        cmd.Parameters.AddWithValue("discount", decimal.Parse(txtDiscount.Text));
                        cmd.Parameters.AddWithValue("start", dtpStart.Value);
                        cmd.Parameters.AddWithValue("end", dtpEnd.Value);
                        cmd.Parameters.AddWithValue("id", int.Parse(txtPromoId.Text));
                        cmd.Parameters.AddWithValue("status", txtPromoStatus.SelectedItem?.ToString() ?? "Active");

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("แก้ไขโปรโมชั่นสำเร็จ");
                            LoadAllPromotions(btnSearch.Text.Trim());
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error Update: " + ex.Message); }
        }

        // --- 6. ปุ่ม DELETE (ลบโปรโมชั่น) ---
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPromoId.Text))
            {
                MessageBox.Show("กรุณาเลือกโปรโมชั่นจากตารางก่อนลบ");
                return;
            }

            var confirm = MessageBox.Show($"ต้องการลบโปรโมชั่น ID: {txtPromoId.Text} ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        string sql = "DELETE FROM Promotions WHERE promotionid = @id";
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("id", int.Parse(txtPromoId.Text));
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("ลบสำเร็จ");
                            LoadAllPromotions(btnSearch.Text.Trim());
                            // เคลียร์ค่าในช่องกรอก
                            txtPromoStatus.SelectedIndex = -1; // ล้างไม่ให้มีตัวเลือกใดถูกเลือกอยู่
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        // --- 7. ปุ่ม BACK ---
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPromoName.Text) || string.IsNullOrEmpty(txtDiscount.Text) || string.IsNullOrEmpty(txtResId.Text))
            {
                MessageBox.Show("กรุณากรอก ชื่อโปรโมชั่น, ส่วนลด และ Restaurant ID ให้ครบถ้วน");
                return;
            }

            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    // เพิ่มคอลัมน์ description, startdate, enddate เข้าไปใน SQL
                    string sql = @"INSERT INTO Promotions (restaurantid, name, description, discountamount, status, startdate, enddate) 
                           VALUES (@resId, @name, @desc, @discount, @status, @start, @end)";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("resId", int.Parse(txtResId.Text));
                        cmd.Parameters.AddWithValue("name", txtPromoName.Text);
                        cmd.Parameters.AddWithValue("desc", txtDescription.Text); // ดึงค่า description
                        cmd.Parameters.AddWithValue("discount", decimal.Parse(txtDiscount.Text));
                        // ตัดเศษเวลาออก ให้เริ่มที่ 00:00:00 ของวันนั้นพอดี
                        cmd.Parameters.AddWithValue("start", dtpStart.Value.Date);
                        // สิ้นสุดตอนเที่ยงคืนตรง (00:00:00) ของวันถัดไป 
                        cmd.Parameters.AddWithValue("end", dtpEnd.Value.Date.AddDays(1));
                        cmd.Parameters.AddWithValue("status", txtPromoStatus.SelectedItem == null ? "Active" : txtPromoStatus.SelectedItem.ToString());

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("สร้างโปรโมชั่นใหม่สำเร็จแล้ว");
                            LoadAllPromotions(btnSearch.Text.Trim());

                            // เคลียร์ฟอร์ม
                            txtPromoId.Clear(); txtPromoName.Clear(); txtDescription.Clear();
                            txtPromoStatus.SelectedIndex = -1; // ล้างไม่ให้มีตัวเลือกใดถูกเลือกอยู่
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error Insert: " + ex.Message); }
        }

        private void btnPromotion_Click(object sender, EventArgs e)
        {
            FormAdmin adminForm = new FormAdmin();
            adminForm.Show(); // หรือใช้ adminForm.ShowDialog(); เพื่อบังคับให้จัดการหน้านี้ให้เสร็จก่อนก็ได้
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}