using Npgsql; // 🌟 กลับมาใช้ตัวเชื่อมต่อ PostgreSQL โดยตรง
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FoodHubCustomer
{
    public partial class BookingForm : Form
    {
        private readonly string _connString = "Server=localhost;Port=5432;Database=FoodHubDB;User Id=postgres;Password=112547;";

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

            // สั่งโหลดโปรโมชันจากดาต้าเบสตรงๆ
            LoadPromotionsDirect(_selectedRestaurantId);
        }

        private void SetupForm()
        {
            dgvPromotions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPromotions.ReadOnly = true;
            dgvPromotions.AllowUserToAddRows = false;
            dgvPromotions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPromotions.BackgroundColor = System.Drawing.Color.White;
        }

        private void LoadPromotionsDirect(int restaurantId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    string query = @"
                        SELECT promotionid, name AS ""โปรโมชั่น"", description AS ""ส่วนลด"", startdate AS ""เริ่มต้น"", enddate AS ""สิ้นสุด""
                        FROM public.promotions 
                        WHERE restaurantid = @resId AND enddate >= CURRENT_DATE";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("resId", restaurantId);
                        var adapter = new NpgsqlDataAdapter(cmd);
                        var dt = new DataTable();
                        adapter.Fill(dt);

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

                    txtDate.Text = string.Empty;

                    MessageBox.Show($"เลือก Promotion สำเร็จ!\n\nกรุณาเลือกวันที่จองในช่วง:\n{_promoStartDate:dd/MM/yyyy} - {_promoEndDate:dd/MM/yyyy}",
                        "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnClearPromo_Click(object sender, EventArgs e)
        {
            ResetPromotion();
            MessageBox.Show("ยกเลิก Promotion แล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ResetPromotion()
        {
            _selectedPromotionId = -1;
            _promoStartDate = DateTime.MinValue;
            _promoEndDate = DateTime.MinValue;
            txtDate.Text = string.Empty;
            lblSelectedPromo.Text = "ไม่ได้เลือก";
            lblSelectedPromo.ForeColor = System.Drawing.Color.Gray;
        }

        private void txtDate_Click(object sender, EventArgs e)
        {
            using (Form dateForm = new Form())
            {
                dateForm.StartPosition = FormStartPosition.CenterParent;
                dateForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                dateForm.MaximizeBox = false; dateForm.MinimizeBox = false;
                dateForm.Size = new System.Drawing.Size(300, 150);

                DateTimePicker dtp = new DateTimePicker();
                dtp.Format = DateTimePickerFormat.Short;
                dtp.Location = new System.Drawing.Point(20, 20);
                dtp.Size = new System.Drawing.Size(240, 23);

                if (_selectedPromotionId != -1 && _promoStartDate != DateTime.MinValue && _promoEndDate != DateTime.MinValue)
                {
                    dtp.MinDate = _promoStartDate.Date > DateTime.Today ? _promoStartDate.Date : DateTime.Today;
                    dtp.MaxDate = _promoEndDate.Date;
                    dtp.Value = dtp.MinDate;
                    dateForm.Text = $"เลือกวันที่ ({_promoStartDate:dd/MM/yyyy} - {_promoEndDate:dd/MM/yyyy})";
                }
                else
                {
                    dtp.MinDate = DateTime.Today;
                    dtp.MaxDate = DateTime.MaxValue;
                    dtp.Value = DateTime.Today;
                    dateForm.Text = "เลือกวันที่";
                }

                Button btnOk = new Button { Text = "ตกลง", Location = new System.Drawing.Point(100, 60), Size = new System.Drawing.Size(75, 25), DialogResult = DialogResult.OK };
                dateForm.Controls.Add(dtp);
                dateForm.Controls.Add(btnOk);
                dateForm.AcceptButton = btnOk;

                if (dateForm.ShowDialog() == DialogResult.OK)
                    txtDate.Text = dtp.Value.ToString("dd/MM/yyyy");
            }
        }

        private void txtTime_Click(object sender, EventArgs e)
        {
            using (Form timeForm = new Form())
            {
                timeForm.Text = "เลือกเวลา"; timeForm.Size = new System.Drawing.Size(300, 160);
                timeForm.StartPosition = FormStartPosition.CenterParent;
                timeForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                timeForm.MaximizeBox = false; timeForm.MinimizeBox = false;

                Label lbl = new Label { Text = "เลือกเวลา:", Location = new System.Drawing.Point(20, 20) };
                DateTimePicker dtpTime = new DateTimePicker { Format = DateTimePickerFormat.Time, ShowUpDown = true, Location = new System.Drawing.Point(20, 45), Size = new System.Drawing.Size(240, 23) };
                Button btnOk = new Button { Text = "ตกลง", Location = new System.Drawing.Point(100, 80), Size = new System.Drawing.Size(75, 25), DialogResult = DialogResult.OK };

                timeForm.Controls.Add(lbl); timeForm.Controls.Add(dtpTime); timeForm.Controls.Add(btnOk);
                timeForm.AcceptButton = btnOk;

                if (timeForm.ShowDialog() == DialogResult.OK)
                    txtTime.Text = dtpTime.Value.ToString("HH:mm");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        // 🟢 ปุ่ม Save บันทึกตรงเข้าตาราง customers ใน pgAdmin เวย์ดั้งเดิม
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                string bookingDateString = $"{txtDate.Text} {txtTime.Text}";
                DateTime finalBookingDate = DateTime.ParseExact(bookingDateString, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                using (var conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();

                    // ยิงตรงเข้าตาราง public.customers ของหนูเป๊ะๆ ทุกคอลัมน์!
                    string query = @"
                        INSERT INTO public.customers (fullname, email, phone, restaurantid, bookingdate, status, createat, numberofpeople, promotionid) 
                        VALUES (@fullName, @email, @phone, @restaurantId, @bookingDate, @status, @createAt, @numberOfPeople, @promotionId)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("fullName", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("email", txtMail.Text.Trim());
                        cmd.Parameters.AddWithValue("phone", txtPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("restaurantId", _selectedRestaurantId);
                        cmd.Parameters.AddWithValue("bookingDate", finalBookingDate);
                        cmd.Parameters.AddWithValue("status", "NewRequest");
                        cmd.Parameters.AddWithValue("createAt", DateTime.Now);
                        cmd.Parameters.AddWithValue("numberOfPeople", int.Parse(txtPerson.Text.Trim()));
                        cmd.Parameters.AddWithValue("promotionId", _selectedPromotionId != -1 ? (object)_selectedPromotionId : DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }

                string promoMsg = _selectedPromotionId != -1 ? $"\nPromotion: {lblSelectedPromo.Text}" : "\nไม่ได้ใช้ Promotion";
                MessageBox.Show($"จองสำเร็จเรียบร้อยแล้ว!\nร้าน: {lblRestaurantName.Text.Replace("ร้านอาหารที่เลือก: ", "")}" + promoMsg,
                    "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                ResetPromotion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกข้อมูล: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim())) { MessageBox.Show("กรุณากรอกชื่อ"); txtName.Focus(); return false; }
            if (string.IsNullOrEmpty(txtMail.Text.Trim())) { MessageBox.Show("กรุณากรอก Email"); txtMail.Focus(); return false; }
            if (string.IsNullOrEmpty(txtPhone.Text.Trim())) { MessageBox.Show("กรุณากรอกเบอร์โทร"); txtPhone.Focus(); return false; }
            if (_selectedRestaurantId == -1) { MessageBox.Show("ระบบไม่พบรหัสร้านอาหาร"); return false; }
            if (string.IsNullOrEmpty(txtDate.Text)) { MessageBox.Show("กรุณาเลือกวันที่"); return false; }
            if (string.IsNullOrEmpty(txtTime.Text)) { MessageBox.Show("กรุณาเลือกเวลา"); return false; }
            if (string.IsNullOrEmpty(txtPerson.Text.Trim()) || !int.TryParse(txtPerson.Text.Trim(), out int person) || person <= 0)
            {
                MessageBox.Show("กรุณากรอกจำนวนคนให้ถูกต้อง"); txtPerson.Focus(); return false;
            }
            return true;
        }

        private void ClearForm()
        {
            txtName.Text = string.Empty; txtMail.Text = string.Empty; txtPhone.Text = string.Empty;
            txtDate.Text = string.Empty; txtTime.Text = string.Empty; txtPerson.Text = string.Empty;
        }
    }
}