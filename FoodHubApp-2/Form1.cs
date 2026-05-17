using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Http;       // ใช้เชื่อมต่อ REST API
using System.Text.Json;     // ใช้จัดการแปลงข้อมูล JSON
using System.Threading.Tasks;
using FoodHubCustomer;       // เชื่อมต่อกับ BookingForm ตัวใหม่

namespace FoodHubApp
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7139/") };

        public Form1()
        {
            InitializeComponent();
        }

        // เมื่อเปิดหน้าจอมา ให้โหลดร้านอาหารทั้งหมดทันที
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAllRestaurants("");
        }

        // ปรับปรุงเป็น async void เพื่อยิงดึงข้อมูลร้านอาหารผ่านช่องทาง HTTP GET ของ API
        private async void LoadAllRestaurants(string keyword = "")
        {
            try
            {
                // ยิงไปที่ URL: api/restaurants?keyword=...
                HttpResponseMessage response = await client.GetAsync($"api/restaurants?keyword={Uri.EscapeDataString(keyword)}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();

                    // ใช้ระบบแกะโครงสร้าง JSON ออกมาถมลงตาราง dgvData โดยตรงแบบปลอดภัย ไม่ติดขัดภาษาไทย
                    using (JsonDocument doc = JsonDocument.Parse(jsonResult))
                    {
                        DataTable dt = new DataTable();
                        bool columnsCreated = false;

                        foreach (JsonElement element in doc.RootElement.EnumerateArray())
                        {
                            if (!columnsCreated)
                            {
                                foreach (JsonProperty prop in element.EnumerateObject())
                                {
                                    dt.Columns.Add(prop.Name);
                                }
                                columnsCreated = true;
                            }

                            DataRow row = dt.NewRow();
                            foreach (JsonProperty prop in element.EnumerateObject())
                            {
                                row[prop.Name] = prop.Value.ToString();
                            }
                            dt.Rows.Add(row);
                        }

                        dgvData.DataSource = dt;
                        dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        // ปรับหัวตารางให้เป็นภาษาไทยสวยงามที่ฝั่งนี้ได้เลยครับลูก
                        if (dgvData.Columns["name"] != null) dgvData.Columns["name"].HeaderText = "ชื่อร้านอาหาร";
                        if (dgvData.Columns["address"] != null) dgvData.Columns["address"].HeaderText = "ที่อยู่";
                        if (dgvData.Columns["phone"] != null) dgvData.Columns["phone"].HeaderText = "เบอร์โทรศัพท์";
                        if (dgvData.Columns["restaurantid"] != null) dgvData.Columns["restaurantid"].Visible = false; // ซ่อน ID ระบบ
                    }
                }
                else
                {
                    MessageBox.Show("เซิร์ฟเวอร์ API ปฏิเสธคำขอโหลดข้อมูลร้านอาหาร");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถโหลดข้อมูลร้านอาหารได้: " + ex.Message);
            }
        }

        // เมื่อกดปุ่มค้นหา (Search)
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAllRestaurants(txtSearch.Text);
        }

        // ค้นหาอัตโนมัติเมื่อมีการพิมพ์ในช่องค้นหา
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadAllRestaurants(txtSearch.Text);
        }

        // ฟังก์ชันควบคุมการคลิกบนตารางเมื่อผู้ใช้งานเลือกแถวร้านอาหาร
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string colName = dgvData.Columns[e.ColumnIndex].Name;

            // ดักจับชื่อคอลัมน์ให้เป็นตัวพิมพ์เล็กตามโครงสร้าง JSON สากลที่ส่งมาจาก API
            int resId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["restaurantid"].Value);
            string resName = dgvData.Rows[e.RowIndex].Cells["name"].Value?.ToString() ?? "";

            if (colName == "btnViewReview")
            {
                ReviewForm reviewPage = new ReviewForm(resId, resName);
                reviewPage.ShowDialog();
            }
            else
            {
                BookingForm bookingPage = new BookingForm(resId, resName);
                bookingPage.ShowDialog();
            }
        }

        private void btnManageBooking_Click(object sender, EventArgs e)
        {
            ManageBookingForm f = new ManageBookingForm();
            f.ShowDialog();
        }

        private void btnOpenReview_Click(object sender, EventArgs e)
        {
            AddReviewForm reviewPage = new AddReviewForm();
            reviewPage.ShowDialog();
        }
    }
}