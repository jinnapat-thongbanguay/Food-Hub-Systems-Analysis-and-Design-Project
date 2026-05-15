using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace FoodhubRes
{
    public partial class FormAdmin : Form
    {
        // ใช้สิทธิ์ Admin ตามที่ตั้งไว้
        private string connString = "Host=localhost;Username=foodhub_admin;Password=AdminPass123;Database=FoodHubDB";

        public FormAdmin()
        {
            InitializeComponent();
        }

        // --- 1. โหลดโปรโมชั่นทั้งหมด (รวมชื่อร้านจากตาราง Restaurants) ---
        private void LoadAllPromotions()
        {
            string sql = @"SELECT p.promotionid AS ""ID"", r.name AS ""ชื่อร้าน"", 
                           p.name AS ""ชื่อโปรโมชั่น"", p.discountamount AS ""ส่วนลด"", p.status AS ""สถานะ""
                           FROM Promotions p
                           JOIN Restaurants r ON p.restaurantid = r.restaurantid
                           ORDER BY p.promotionid ASC";
            ExecuteQuery(sql);
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
            string resId = btnSearch.Text;

            if (string.IsNullOrEmpty(resId))
            {
                LoadAllPromotions();
                return;
            }

            // ค้นหาโดยจอยตารางเพื่อให้ได้ Format หัวตารางเหมือนกัน
            string sql = $@"SELECT p.promotionid AS ""ID"", r.name AS ""ชื่อร้าน"", 
                           p.name AS ""ชื่อโปรโมชั่น"", p.discountamount AS ""ส่วนลด"", p.status AS ""สถานะ""
                           FROM Promotions p
                           JOIN Restaurants r ON p.restaurantid = r.restaurantid
                           WHERE p.restaurantid = {resId}";
            ExecuteQuery(sql);
        }

        // --- 4. คลิกแถวในตาราง แล้วข้อมูลเด้งเข้า TextBox ---
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ตรวจสอบว่าไม่ได้คลิกโดนหัวตาราง
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // ดึงข้อมูลจาก Column ตามชื่อที่เราตั้งไว้ใน SQL (AS "...")
                txtPromoId.Text = row.Cells["ID"].Value.ToString();
                txtPromoName.Text = row.Cells["ชื่อโปรโมชั่น"].Value.ToString();
                txtDiscount.Text = row.Cells["ส่วนลด"].Value.ToString();
                txtPromoStatus.Text = row.Cells["สถานะ"].Value.ToString();
            }
        }

        // --- 5. ปุ่มสำหรับบันทึกการแก้ไข (Update) ---
        // (คุณต้องสร้างปุ่มใหม่ชื่อ btnUpdate หรือดับเบิลคลิกที่ปุ่ม Enter เดิมถ้าจะใช้ร่วมกัน)
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPromoId.Text))
            {
                MessageBox.Show("กรุณาเลือกโปรโมชั่นที่ต้องการแก้ไขจากตารางก่อนครับ");
                return;
            }

            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string sql = @"UPDATE Promotions 
                                   SET name = @name, 
                                       discountamount = @discount, 
                                       status = @status 
                                   WHERE promotionid = @id";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("name", txtPromoName.Text);
                        cmd.Parameters.AddWithValue("discount", decimal.Parse(txtDiscount.Text));
                        cmd.Parameters.AddWithValue("status", txtPromoStatus.Text);
                        cmd.Parameters.AddWithValue("id", int.Parse(txtPromoId.Text));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("แก้ไขโปรโมชั่นสำเร็จ! ✨");
                            LoadAllPromotions(); // รีเฟรชตาราง
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ในการแก้ไข: " + ex.Message);
            }
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
                            MessageBox.Show("ลบสำเร็จ!");
                            LoadAllPromotions();
                            // เคลียร์ค่าในช่องกรอก
                            txtPromoId.Clear(); txtPromoName.Clear(); txtDiscount.Clear(); txtPromoStatus.Clear();
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
            LoadAllPromotions();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}