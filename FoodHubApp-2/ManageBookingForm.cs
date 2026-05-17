using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Http;     
using System.Text.Json;     
using System.Threading.Tasks;

namespace FoodHubApp
{
    public partial class ManageBookingForm : Form
    {
        // ล็อกเป้าหมายไปที่พอร์ตหลังบ้าน (เช็คเลข 7139 ให้ตรงกับ Swagger ของหนูด้วยน้า)
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7139/") };

        public ManageBookingForm()
        {
            InitializeComponent();
        }

        // ฟังก์ชันค้นหาเบอร์โทร
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string phone = txtPhoneSearch.Text.Trim();
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์เพื่อค้นหา");
                return;
            }

            try
            {
                // ยิงคำขอผ่าน URL
                HttpResponseMessage response = await client.GetAsync($"api/ManageBookings/{phone}");

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

                        dgvMyBookings.DataSource = dt;
                        dgvMyBookings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        // ปรับแต่งหัวตารางภาษาไทย
                        if (dgvMyBookings.Columns["bookingid"] != null) dgvMyBookings.Columns["bookingid"].Visible = false;
                        if (dgvMyBookings.Columns["restaurant_name"] != null) dgvMyBookings.Columns["restaurant_name"].HeaderText = "ร้านอาหาร";
                        if (dgvMyBookings.Columns["bookingdate"] != null) dgvMyBookings.Columns["bookingdate"].HeaderText = "วัน-เวลาที่จอง";
                        if (dgvMyBookings.Columns["numberofpeople"] != null) dgvMyBookings.Columns["numberofpeople"].HeaderText = "จำนวนคน";
                        if (dgvMyBookings.Columns["status"] != null) dgvMyBookings.Columns["status"].HeaderText = "สถานะ";

                        if (!dgvMyBookings.Columns.Contains("btnCancel"))
                        {
                            DataGridViewButtonColumn cancelBtn = new DataGridViewButtonColumn();
                            cancelBtn.Name = "btnCancel";
                            cancelBtn.HeaderText = "Action";
                            cancelBtn.Text = "Cancel";
                            cancelBtn.UseColumnTextForButtonValue = false;
                            dgvMyBookings.Columns.Add(cancelBtn);
                        }

                        foreach (DataGridViewRow row in dgvMyBookings.Rows)
                        {
                            if (row.IsNewRow) continue;

                            string status = row.Cells["status"].Value?.ToString() ?? "";

                            if (status == "NewRequest" || status == "Confirmed")
                            {
                                row.Cells["btnCancel"].Value = "Cancel";
                            }
                            else
                            {
                                row.Cells["btnCancel"].Value = "—";
                                DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)row.Cells["btnCancel"];
                                buttonCell.FlatStyle = FlatStyle.Flat;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูลการจอง หรือเซิร์ฟเวอร์ปฏิเสธคำขอ");
                    dgvMyBookings.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เชื่อมต่อ API ไม่สำเร็จ: " + ex.Message);
            }
        }

        // ฟังก์ชันกดยกเลิก
        private async void dgvMyBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvMyBookings.Columns[e.ColumnIndex].Name == "btnCancel")
            {
                string currentStatus = dgvMyBookings.Rows[e.RowIndex].Cells["status"].Value?.ToString() ?? "";

                if (currentStatus != "NewRequest" && currentStatus != "Confirmed")
                {
                    MessageBox.Show($"ไม่สามารถยกเลิกการจองได้ เนื่องจากรายการนี้มีสถานะเป็น '{currentStatus}'",
                    "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int bookingId = Convert.ToInt32(dgvMyBookings.Rows[e.RowIndex].Cells["bookingid"].Value);
                string resName = dgvMyBookings.Rows[e.RowIndex].Cells["restaurant_name"].Value?.ToString() ?? "";

                DialogResult result = MessageBox.Show(
                    $"คุณต้องการยกเลิกการจองของร้าน '{resName}' ใช่หรือไม่?",
                    "ยืนยันการยกเลิก",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // ยิงคำสั่ง PUT ไปที่ API เพื่อเปลี่ยนสถานะ
                        // พารามิเตอร์ที่ 2 เป็น null เพราะเราส่งเลข id ไปทาง URL แล้ว ไม่ต้องส่ง body เข้าไป
                        HttpResponseMessage response = await client.PutAsync($"api/ManageBookings/cancel/{bookingId}", null);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("ยกเลิกการจองเรียบร้อยแล้ว");
                            btnSearch_Click(sender, e); // สั่งรีเฟรชตารางใหม่
                        }
                        else
                        {
                            MessageBox.Show("เกิดข้อผิดพลาดทางเทคนิค ไม่สามารถยกเลิกการจองได้");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ติดต่อ API ไม่สำเร็จ: " + ex.Message);
                    }
                }
            }
        }

        private void btnFormClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}