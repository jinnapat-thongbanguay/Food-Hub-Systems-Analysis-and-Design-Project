using System;
using System.Net.Http;
using System.Windows.Forms;
using System.Data; // เพิ่มเข้ามาสำหรับ DataTable
using Newtonsoft.Json; // เพิ่มเข้ามาสำหรับแปลง JSON (ต้องติดตั้ง NuGet Package Newtonsoft.Json ก่อนนะ)

namespace FoodhubRes
{
    public partial class Form1 : Form
    {
        // URL ของเซิร์ฟเวอร์หลังบ้าน 
        private readonly string apiUrl = "https://localhost:7242/api/foodhubapi/";

        public Form1()
        {
            InitializeComponent();
        }

        // --- เปลี่ยนเป็นใช้ Newtonsoft.Json แปลงเป็น DataTable ---
        private async void LoadDataFromApi(string endpoint)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // ยิง GET Request ไปที่ API หลังบ้านแบบ Async
                    var response = await client.GetAsync(apiUrl + endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        // 1. อ่านข้อมูล JSON กลับมาเป็น String ก่อน
                        string jsonString = await response.Content.ReadAsStringAsync();

                        // 2. แปลง String JSON ให้กลายเป็น DataTable เพื่อให้ DataGridView เข้าใจ
                        DataTable data = JsonConvert.DeserializeObject<DataTable>(jsonString);

                        // 3. โยนเข้าตาราง
                        dgvRestaurants.DataSource = data;
                        dgvRestaurants.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    else
                    {
                        MessageBox.Show("เซิร์ฟเวอร์ตอบกลับด้วยข้อผิดพลาด: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถเชื่อมต่อกับ API ได้: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 1. ปุ่มแสดงรายชื่อร้านอาหารทั้งหมด
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadDataFromApi("restaurants");
        }

        // 2. ปุ่ม PromoActive
        private void btnPromoActive_Click(object sender, EventArgs e)
        {
            LoadDataFromApi("promotions/Active");
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            FormAdmin adminPage = new FormAdmin();
            adminPage.Show();
            this.Hide();
            adminPage.FormClosed += (s, args) => this.Show();
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            FormBooking bookingPage = new FormBooking();
            bookingPage.Show();
            this.Hide();
            bookingPage.FormClosed += (s, args) => this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnLoad_Click(sender, e);
        }
    }
}