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

namespace FoodHubApp
{
    public partial class ReviewForm : Form
    {
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7139/") };

        private int _restaurantId;

        public ReviewForm(int resId, string resName)
        {
            InitializeComponent();

            _restaurantId = resId;
            lblShowResName.Text = "Review for: " + resName; // โชว์ชื่อร้านจริงให้ผู้ใช้เห็น

            // จัดการโครงสร้างตารางแสดงผลเบื้องต้นให้อ่านง่าย
            dgvReviews.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReviews.ReadOnly = true;

            // สั่งรันฟังก์ชันโหลดข้อมูลรีวิวทันทีที่เปิดหน้าจอ
            LoadReviews(_restaurantId);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // คำสั่งสำหรับปิดหน้าต่างปัจจุบัน
            this.Close();
        }

        // ปรับปรุงเป็น async void เพื่อดึงข้อมูลประวัติรีวิวคอมเมนต์แยกรายร้านผ่าน REST API หลังบ้าน
        private async void LoadReviews(int resId)
        {
            try
            {
                // ส่งคำสั่ง GET สื่อสารกับเซิร์ฟเวอร์ Backend ตามเส้นทางพิกัด api/reviews/{resId}
                HttpResponseMessage response = await client.GetAsync($"api/reviews/{resId}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();

                    // แกะโครงสร้าง JSON Array คลี่ออกเป็นแนวแถวข้อมูลลง DataTable เพื่อถมเข้า DataGridView
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

                        // ถมข้อมูลประวัติรีวิวลงตารางบนหน้าจอพาสเทล
                        dgvReviews.DataSource = dt;

    
                        // if (dgvReviews.Columns["reviewername"] != null) dgvReviews.Columns["reviewername"].HeaderText = "ผู้รีวิว";
                        if (dgvReviews.Columns["comment"] != null) dgvReviews.Columns["comment"].HeaderText = "ความคิดเห็น";
                        if (dgvReviews.Columns["rating"] != null) dgvReviews.Columns["rating"].HeaderText = "คะแนน";

                        // สั่งซ่อนระบบรหัสไอดีต่าง ๆ ไม่ให้โชว์เกะกะรกสายตาผู้ใช้งาน
                        if (dgvReviews.Columns["reviewid"] != null) dgvReviews.Columns["reviewid"].Visible = false;
                        if (dgvReviews.Columns["restaurantid"] != null) dgvReviews.Columns["restaurantid"].Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("เซิร์ฟเวอร์ API ปฏิเสธการเรียกดูข้อมูลรีวิว");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("โหลดรีวิวไม่สำเร็จ: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}