using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace FoodhubRes
{
    public partial class Form1 : Form
    {
        private string connString = "Host=localhost;Username=foodhub_app;Password=AppPass123;Database=FoodHubDB";

        public Form1()
        {
            InitializeComponent();
        }

        // ฟังก์ชันกลางสำหรับรัน SQL และแสดงผลในตาราง
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
                            dgvRestaurants.DataSource = dt;
                            dgvRestaurants.AutoSizeColumnsMode =
                                DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 1. ปุ่มแสดงรายชื่อร้านอาหารทั้งหมด
        private void btnLoad_Click(object sender, EventArgs e)
        {
            string sql = "SELECT restaurantid, name, address, phone FROM restaurants";
            ExecuteQuery(sql);
        }

        // 2. ปุ่ม PromoActive: ดูโปรโมชั่นที่ยังใช้งานได้ (Status = 'Active')
        private void btnPromoActive_Click(object sender, EventArgs e)
        {
            // ดึงข้อมูลโปรโมชั่น พร้อมชื่อร้านอาหารมาแสดงคู่กัน
            string sql = @"
                SELECT 
                    p.name           AS ""Promotion"", 
                    r.name           AS ""Restaurant"",
                    p.discountamount AS ""Discount"", 
                    p.enddate        AS ""Expiry date""
                FROM Promotions p
                JOIN Restaurants r ON p.restaurantid = r.restaurantid
                WHERE p.status = 'Active'";
            ExecuteQuery(sql);
        }


        // ปุ่มไปหน้า Admin
        private void btnManagePromotion_Click(object sender, EventArgs e)
        {
            // สร้าง Object ของหน้า Admin
            FormAdmin adminPage = new FormAdmin();

            // แสดงหน้า Admin
            adminPage.Show();

            // ซ่อนหน้าปัจจุบัน
            this.Hide();

            // เมื่อปิดหน้า Admin ให้หน้าหลักกลับมาแสดงใหม่
            adminPage.FormClosed += (s, args) => this.Show();
        }

        // ปุ่มไปหน้า Booking
        private void btnBooking_Click(object sender, EventArgs e)
        {
            // สร้าง Object ของหน้า Booking
            FormBooking bookingPage = new FormBooking();

            // แสดงหน้า Booking
            bookingPage.Show();

            // ซ่อนหน้าหลัก
            this.Hide();

            // เมื่อปิดหน้า Booking ให้หน้าหลักกลับมาโชว์
            bookingPage.FormClosed += (s, args) => this.Show();
        }

        // โหลดรายชื่อร้านขึ้นมาโชว์ตอนเปิดแอปทันที
        private void Form1_Load(object sender, EventArgs e)
        {
            btnLoad_Click(sender, e);
        }
    }
}