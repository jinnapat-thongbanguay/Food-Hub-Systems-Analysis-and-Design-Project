using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Http;       // ✅ 
using System.Text.Json;     // ✅ 
using System.Text;
using System.Threading.Tasks;

namespace FoodHubCustomer
{
    public partial class AddReviewForm : Form
    {
        // ⚠️ เช็คเลขพอร์ต (7139) ให้ตรงกับหน้าเว็บ Swagger ของเครื่องหนูด้วยน้า
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7139/") };

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
            LoadCompletedBookingsByPhone(phone);
        }

        // 2. ⚡ ยิง GET ไปดึงข้อมูลประวัติการจองผ่าน API
        private async void LoadCompletedBookingsByPhone(string phone)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/AddReviews/{phone}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
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

                        dataGridView1.DataSource = dt;

                        // ซ่อนคอลัมน์ที่ไม่จำเป็นต้องแสดง
                        if (dataGridView1.Columns.Contains("restaurantid")) dataGridView1.Columns["restaurantid"]!.Visible = false;
                        if (dataGridView1.Columns.Contains("customerid")) dataGridView1.Columns["customerid"]!.Visible = false;
                        if (dataGridView1.Columns.Contains("bookingid")) dataGridView1.Columns["bookingid"]!.Visible = false;

                        // ปรับหัวคอลัมน์ให้อ่านง่าย
                        if (dataGridView1.Columns.Contains("restaurantname")) dataGridView1.Columns["restaurantname"].HeaderText = "ชื่อร้านอาหาร";
                        if (dataGridView1.Columns.Contains("bookingdate")) dataGridView1.Columns["bookingdate"].HeaderText = "วันที่เข้าทาน";
                        if (dataGridView1.Columns.Contains("comment")) dataGridView1.Columns["comment"].HeaderText = "คอมเมนต์เดิม";
                        if (dataGridView1.Columns.Contains("rating")) dataGridView1.Columns["rating"].HeaderText = "คะแนนเดิม";

                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        if (dt.Rows.Count > 0)
                        {
                            selectedCustomerId = Convert.ToInt32(dt.Rows[0]["customerid"]);
                        }
                    }
                }
                else
                {
                    selectedCustomerId = 0;
                    dataGridView1.DataSource = null;
                    MessageBox.Show("ไม่พบประวัติการจองที่ทานเสร็จเรียบร้อยแล้ว (Completed) สำหรับเบอร์โทรนี้\n\n*หมายเหตุ: ต้องเดินทางไปทานที่ร้านจริงก่อนจึงจะสามารถรีวิวได้*",
                                    "ไม่สามารถรีวิวได้", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ติดต่อเซิร์ฟเวอร์ไม่ได้: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. ⚡ ยิง POST ส่งข้อมูลรีวิวขึ้นไปบันทึก
        private async void btnEnter_Click(object? sender, EventArgs e)
        {
            if (selectedCustomerId == 0 || dataGridView1.Rows.Count == 0 || dataGridView1.DataSource == null)
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
                int restId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["restaurantid"].Value);
                int bookingId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["bookingid"].Value);
                int userRating = cboRating.SelectedItem != null ? Convert.ToInt32(cboRating.SelectedItem) : 5;

                // ประกอบข้อมูล
                var reviewData = new
                {
                    customerId = selectedCustomerId,
                    restaurantId = restId,
                    bookingId = bookingId,
                    rating = userRating,
                    comment = textBox2.Text.Trim()
                };

                // แปลงเป็น JSON
                string jsonPayload = JsonSerializer.Serialize(reviewData);
                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // ส่งข้อมูล
                HttpResponseMessage response = await client.PostAsync("api/AddReviews", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("บันทึกข้อมูลการรีวิวและให้คะแนนสำเร็จเรียบร้อยแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Clear();

                    if (cboRating.Items.Count > 0)
                        cboRating.SelectedIndex = 0;

                    // โหลดตารางใหม่เพื่อแสดงผลรีวิวที่เพิ่งพิมพ์ไป
                    LoadCompletedBookingsByPhone(searchedPhone);
                }
                else
                {
                    MessageBox.Show("เกิดข้อผิดพลาด เซิร์ฟเวอร์ปฏิเสธการบันทึกรีวิว", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการเชื่อมต่อ API: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}