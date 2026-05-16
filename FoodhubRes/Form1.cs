using System;
using System.Net.Http;
using System.Net.Http.Json; // ต้องใช้ตัวนี้เพื่อช่วยแปลง JSON เป็น Object ง่ายๆ
using System.Windows.Forms;

namespace FoodhubRes
{
    public partial class Form1 : Form
    {
        // URL ของเซิร์ฟเวอร์หลังบ้าน 
        private readonly string apiUrl = "https://localhost:7001/api/foodhubapi/";

        public Form1()
        {
            InitializeComponent();
        }

        // --- เปลี่ยนจากรัน SQL เป็น ดึงข้อมูลจาก API แทน ---
        private async void LoadDataFromApi(string endpoint)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // ยิง GET Request ไปที่ API หลังบ้านแบบ Async หน้าจอจะไม่ค้าง
                    var response = await client.GetAsync(apiUrl + endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        // อ่านค่า JSON แล้วโยนเข้า DataGridView ได้เลย
                        var data = await response.Content.ReadFromJsonAsync<object>();
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
            // เรียกผ่านพาร์ทหลังบ้าน
            LoadDataFromApi("restaurants");
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
