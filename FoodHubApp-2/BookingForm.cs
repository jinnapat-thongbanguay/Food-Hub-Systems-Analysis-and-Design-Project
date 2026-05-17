using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodHubCustomer
{
    public partial class BookingForm : Form
    {
        // เช็คเลขพอร์ต (7139) ให้ตรงกับหน้าเว็บ Swagger 
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7139/") };

        private int _selectedRestaurantId = -1;
        private int _selectedPromotionId = -1;
        private DateTime _promoStartDate = DateTime.MinValue;
        private DateTime _promoEndDate = DateTime.MinValue;

        public BookingForm(int restaurantId, string restaurantName)
        {
            InitializeComponent();
            SetupForm();

            _selectedRestaurantId = restaurantId;
            lblRestaurantName.Text = "ร้านอาหารที่เลือก: " + restaurantName;

            // สั่งโหลดโปรโมชันผ่าน API
            LoadPromotionsAsync(_selectedRestaurantId);
        }

        private void SetupForm()
        {
            dgvPromotions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPromotions.ReadOnly = true;
            dgvPromotions.AllowUserToAddRows = false;
            dgvPromotions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPromotions.BackgroundColor = System.Drawing.Color.White;

            dgvPromotions.CellClick -= dgvPromotions_CellClick;
            dgvPromotions.CellClick += dgvPromotions_CellClick;

            // ตั้งค่าเริ่มต้นปฏิทิน: ห้ามจองย้อนหลัง
            dtpDate.MinDate = DateTime.Today;
        }

        private async void LoadPromotionsAsync(int restaurantId)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/promotions/restaurant/{restaurantId}");

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
                                if (prop.Name == "startdate" || prop.Name == "enddate")
                                {
                                    row[prop.Name] = Convert.ToDateTime(prop.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                {
                                    row[prop.Name] = prop.Value.ToString();
                                }
                            }
                            dt.Rows.Add(row);
                        }

                        if (dt.Columns.Contains("name")) dt.Columns["name"].ColumnName = "โปรโมชั่น";
                        if (dt.Columns.Contains("description")) dt.Columns["description"].ColumnName = "ส่วนลด";
                        if (dt.Columns.Contains("startdate")) dt.Columns["startdate"].ColumnName = "เริ่มต้น";
                        if (dt.Columns.Contains("enddate")) dt.Columns["enddate"].ColumnName = "สิ้นสุด";

                        dgvPromotions.DataSource = dt;

                        if (dgvPromotions.Columns["promotionid"] != null)
                            dgvPromotions.Columns["promotionid"].Visible = false;

                        lblNoPromo.Visible = dt.Rows.Count == 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดโปรโมชั่น: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void dgvPromotions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvPromotions.Rows[e.RowIndex];

                    if (row.Cells["promotionid"]?.Value != DBNull.Value && row.Cells["promotionid"]?.Value != null)
                    {
                        _selectedPromotionId = Convert.ToInt32(row.Cells["promotionid"].Value);

                        string promoName = row.Cells["โปรโมชั่น"]?.Value?.ToString();
                        string discount = row.Cells["ส่วนลด"]?.Value?.ToString();

                        _promoStartDate = Convert.ToDateTime(row.Cells["เริ่มต้น"]?.Value);
                        _promoEndDate = Convert.ToDateTime(row.Cells["สิ้นสุด"]?.Value);

                        lblSelectedPromo.Text = $"{promoName} (ลด {discount}) [{_promoStartDate:dd/MM/yyyy} - {_promoEndDate:dd/MM/yyyy}]";
                        lblSelectedPromo.ForeColor = System.Drawing.Color.Green;

                        // ล็อกปฏิทินตามช่วงเวลาโปรโมชัน
                        ApplyDateRestrictions(_promoStartDate, _promoEndDate);

                        MessageBox.Show($"เลือก Promotion สำเร็จ!\n\nปฏิทินถูกล็อกให้เลือกได้เฉพาะช่วง:\n{_promoStartDate:dd/MM/yyyy} - {_promoEndDate:dd/MM/yyyy}",
                            "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการเลือกโปรโมชั่น: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearPromo_Click(object sender, EventArgs e)
        {
            ResetPromotion();
            MessageBox.Show("ยกเลิก Promotion แล้ว ปฏิทินกลับสู่โหมดปกติ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        // ฟังก์ชันตัวช่วยสำหรับล็อกปฏิทินให้ปลอดภัย ป้องกัน Error ขัดแย้งเรื่อง Min/Max
        // 1. ฟังก์ชันตัวช่วยสำหรับล็อกปฏิทิน
        private void ApplyDateRestrictions(DateTime start, DateTime end)
        {
            // เปลี่ยนมาใช้ DateTimePicker.MinimumDateTime แทนเพื่อป้องกัน Error ปี 1753
            dtpDate.MinDate = DateTimePicker.MinimumDateTime;
            dtpDate.MaxDate = DateTimePicker.MaximumDateTime;

            DateTime newMin = start.Date > DateTime.Today ? start.Date : DateTime.Today;
            DateTime newMax = end.Date;

            dtpDate.Value = newMin;
            dtpDate.MinDate = newMin;
            dtpDate.MaxDate = newMax;
        }

        // 2. ฟังก์ชันยกเลิกโปรโมชัน
        private void ResetPromotion()
        {
            _selectedPromotionId = -1;
            _promoStartDate = DateTime.MinValue;
            _promoEndDate = DateTime.MinValue;

            lblSelectedPromo.Text = "ไม่ได้เลือก";
            lblSelectedPromo.ForeColor = System.Drawing.Color.Gray;

            // ปลดล็อกปฏิทินกลับเป็นปกติ แบบปลอดภัย
            dtpDate.MinDate = DateTimePicker.MinimumDateTime;
            dtpDate.MaxDate = DateTimePicker.MaximumDateTime;
            dtpDate.MinDate = DateTime.Today;
            dtpDate.Value = DateTime.Today;
        }

        // 3. ฟังก์ชันเคลียร์หน้าจอทั้งหมด
        private void ClearForm()
        {
            txtName.Text = string.Empty;
            txtMail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtPerson.Text = string.Empty;

            // รีเซ็ตปฏิทินแบบปลอดภัย
            dtpDate.MinDate = DateTimePicker.MinimumDateTime;
            dtpDate.MaxDate = DateTimePicker.MaximumDateTime;
            dtpDate.MinDate = DateTime.Today;
            dtpDate.Value = DateTime.Today;
            dtpTime.Value = DateTime.Now;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            ResetPromotion();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                // รวมร่างวันที่และเวลาจาก DateTimePicker ทั้ง 2 ตัว
                DateTime selectedDateTime = dtpDate.Value.Date.Add(dtpTime.Value.TimeOfDay);

                var bookingData = new
                {
                    fullName = txtName.Text.Trim(),
                    email = txtMail.Text.Trim(),
                    phone = txtPhone.Text.Trim(),
                    restaurantId = _selectedRestaurantId,
                    bookingDateString = selectedDateTime.ToString("dd/MM/yyyy HH:mm"), // ส่งฟอร์แมตเป๊ะๆ ให้ API
                    numberOfPeople = int.Parse(txtPerson.Text.Trim()),
                    promotionId = _selectedPromotionId != -1 ? (int?)_selectedPromotionId : null
                };

                string jsonPayload = JsonSerializer.Serialize(bookingData);
                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("api/bookings", content);

                if (response.IsSuccessStatusCode)
                {
                    string promoMsg = _selectedPromotionId != -1 ? $"\nPromotion: {lblSelectedPromo.Text}" : "\nไม่ได้ใช้ Promotion";
                    MessageBox.Show($"จองสำเร็จเรียบร้อยแล้ว!\nร้าน: {lblRestaurantName.Text.Replace("ร้านอาหารที่เลือก: ", "")}" + promoMsg,
                        "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();
                    ResetPromotion();
                }
                else
                {
                    string errorMsg = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("เซิร์ฟเวอร์ปฏิเสธการบันทึก: " + errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เชื่อมต่อ API ไม่สำเร็จ: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim())) { MessageBox.Show("กรุณากรอกชื่อ"); txtName.Focus(); return false; }
            if (string.IsNullOrEmpty(txtMail.Text.Trim())) { MessageBox.Show("กรุณากรอก Email"); txtMail.Focus(); return false; }
            if (string.IsNullOrEmpty(txtPhone.Text.Trim())) { MessageBox.Show("กรุณากรอกเบอร์โทร"); txtPhone.Focus(); return false; }
            if (_selectedRestaurantId == -1) { MessageBox.Show("ระบบไม่พบรหัสร้านอาหาร"); return false; }
            if (string.IsNullOrEmpty(txtPerson.Text.Trim()) || !int.TryParse(txtPerson.Text.Trim(), out int person) || person <= 0)
            {
                MessageBox.Show("กรุณากรอกจำนวนคนให้ถูกต้อง"); txtPerson.Focus(); return false;
            }
            return true;
        }


        private void txtTime_TextChanged(object sender, EventArgs e)
        {

        }
    }
}