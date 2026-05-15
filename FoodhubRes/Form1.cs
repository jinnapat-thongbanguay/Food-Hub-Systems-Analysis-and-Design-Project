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

        // --- ฟังก์ชันกลางสำหรับรัน SQL และแสดงผลในตาราง ---
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
                            dgvRestaurants.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string sql = @"SELECT p.name AS ""ชื่อโปรโมชั่น"", r.name AS ""ร้านอาหาร"", 
                           p.discountamount AS ""ส่วนลด"", p.enddate AS ""วันหมดเขต""
                           FROM Promotions p
                           JOIN Restaurants r ON p.restaurantid = r.restaurantid
                           WHERE p.status = 'Active'";
            ExecuteQuery(sql);
        }

        // 3. ปุ่ม CustomerCheckIn: ดูรายชื่อลูกค้าที่มาเช็คอินแล้ว (Status = 'CheckedIn')
        private void btnCustomerCheckIn_Click(object sender, EventArgs e)
        {
            // ดึงข้อมูลการจองที่สถานะเป็น CheckedIn พร้อมชื่อลูกค้าและร้านอาหาร
            string sql = @"SELECT c.fullname AS ""ชื่อลูกค้า"", r.name AS ""ร้านอาหาร"", 
                           b.bookingdate AS ""เวลาจอง"", b.status AS ""สถานะ""
                           FROM Bookings b
                           JOIN Customers c ON b.customerid = c.customerid
                           JOIN Restaurants r ON b.restaurantid = r.restaurantid
                           WHERE b.status = 'CheckedIn'";
            ExecuteQuery(sql);
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            // 1. สร้าง Object ของหน้า Admin
            FormAdmin adminPage = new FormAdmin();

            // 2. แสดงหน้า Admin
            adminPage.Show();

            // 3. ซ่อนหน้าปัจจุบัน (หน้าที่มีปุ่ม btnAdmin)
            this.Hide();

            // แถม: เมื่อปิดหน้า Admin ให้หน้าหลัก (หน้านี้) กลับมาแสดงใหม่
            adminPage.FormClosed += (s, args) => this.Show();
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            // 1. สร้าง Object ของหน้า Booking 
            FormBooking bookingPage = new FormBooking();

            // 2. แสดงหน้า Booking
            bookingPage.Show();

            // 3. ซ่อนหน้าหลัก
            this.Hide();

            // 4. เมื่อปิดหน้า Booking ให้หน้าหลักกลับมาโชว์
            bookingPage.FormClosed += (s, args) => this.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // โหลดรายชื่อร้านขึ้นมาโชว์ตอนเปิดแอปทันที
            btnLoad_Click(sender, e);
        }
    }
}