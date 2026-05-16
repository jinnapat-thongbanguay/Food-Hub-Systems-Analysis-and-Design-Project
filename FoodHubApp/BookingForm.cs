using FoodHubApp;
using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace FoodHubCustomer
{
    public partial class BookingForm : Form
    {
        private string _connString = "Host=localhost;Port=5432;Username=foodhub_customer;Password=CustomerPass123;Database=FoodHubDB";
        private int _selectedRestaurantId = -1;
        private int _selectedPromotionId = -1;
        private DateTime _promoStartDate = DateTime.MinValue;
        private DateTime _promoEndDate = DateTime.MinValue;

        // ⚡ 1. ปรับปรุง Constructor รับค่าข้ามหน้าให้สมบูรณ์
        public BookingForm(int restaurantId, string restaurantName)
        {
            InitializeComponent();

            // เรียกฟังก์ชันตั้งค่าพวกคลิกวันที่/เวลา และ GridView หลังบ้าน
            SetupForm();

            // นำรหัสและชื่อร้านที่ส่งมาจาก Form1 มาล็อกใส่กล่องข้อความทันที
            _selectedRestaurantId = restaurantId;
            lblRestaurantName.Text = "ร้านอาหารที่เลือก: " + restaurantName;

            // สั่งโหลดโปรโมชันของร้านนี้ขึ้นมารอให้เลือก
            LoadPromotions(_selectedRestaurantId);
        }

        private void SetupForm()
        {
            // ตั้งค่า dgvPromotions
            dgvPromotions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPromotions.ReadOnly = true;
            dgvPromotions.AllowUserToAddRows = false;
            dgvPromotions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPromotions.BackgroundColor = System.Drawing.Color.White;
            dgvPromotions.CellClick += dgvPromotions_CellClick;

            // ตั้งค่า Date และ Time
            txtDate.ReadOnly = true;
            txtDate.Cursor = Cursors.Hand;
            txtDate.Click += txtDate_Click;

            txtTime.ReadOnly = true;
            txtTime.Cursor = Cursors.Hand;
            txtTime.Click += txtTime_Click;

            // เพราะเราลบตารางเลือกร้านอันบนทิ้งไปแล้วตามหลัก UX ใหม่
        }

        // โหลด Promotion ของร้านที่เลือก
        private void LoadPromotions(int restaurantId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            PromotionID,
                            Name            AS ""โปรโมชั่น"",
                            Description     AS ""รายละเอียด"",
                            DiscountAmount  AS ""ส่วนลด"",
                            StartDate       AS ""เริ่มต้น"",
                            EndDate         AS ""สิ้นสุด""
                        FROM Promotions
                        WHERE RestaurantID = @restaurantId
                        AND Status = 'Active'
                        ORDER BY StartDate DESC";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("restaurantId", restaurantId);
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
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดโปรโมชั่น: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ปุ่ม Back
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
                this.Close();
            }
            else
            {
                this.Close(); // เปิดแบบ ShowDialog สั่ง Close ได้เลยครับ หน้าเก่าจะไม่หาย
            }
        }

        // คลิกเลือก Promotion ใน dgvPromotions
        private void dgvPromotions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPromotions.Rows[e.RowIndex];

                if (row.Cells["promotionid"]?.Value != null)
                {
                    _selectedPromotionId = Convert.ToInt32(row.Cells["promotionid"].Value);

                    string promoName = row.Cells["โปรโมชั่น"]?.Value?.ToString();
                    string discount = row.Cells["ส่วนลด"]?.Value?.ToString();

                    // เก็บวันที่ Start และ End
                    _promoStartDate = Convert.ToDateTime(row.Cells["เริ่มต้น"]?.Value);
                    _promoEndDate = Convert.ToDateTime(row.Cells["สิ้นสุด"]?.Value);

                    // แสดง Promotion ที่เลือก
                    lblSelectedPromo.Text =
                        $"{promoName} (ลด {discount}) " +
                        $"[{_promoStartDate:dd/MM/yyyy} - {_promoEndDate:dd/MM/yyyy}]";
                    lblSelectedPromo.ForeColor = System.Drawing.Color.Green;

                    // Reset วันที่เพราะเปลี่ยน Promotion
                    txtDate.Text = string.Empty;

                    MessageBox.Show(
                        $"เลือก Promotion สำเร็จ!\n\n" +
                        $"กรุณาเลือกวันที่จองในช่วง:\n" +
                        $"{_promoStartDate:dd/MM/yyyy} - {_promoEndDate:dd/MM/yyyy}",
                        "แจ้งเตือน",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        // ปุ่ม ยกเลิก Promotion
        private void btnClearPromo_Click(object sender, EventArgs e)
        {
            ResetPromotion();
            MessageBox.Show("ยกเลิก Promotion แล้ว",
                "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Reset Promotion
        private void ResetPromotion()
        {
            _selectedPromotionId = -1;
            _promoStartDate = DateTime.MinValue;
            _promoEndDate = DateTime.MinValue;
            txtDate.Text = string.Empty;
            lblSelectedPromo.Text = "ไม่ได้เลือก";
            lblSelectedPromo.ForeColor = System.Drawing.Color.Gray;
        }

        // คลิก txtDate - เปิดเลือกวันที่
        private void txtDate_Click(object sender, EventArgs e)
        {
            using (Form dateForm = new Form())
            {
                dateForm.StartPosition = FormStartPosition.CenterParent;
                dateForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                dateForm.MaximizeBox = false;
                dateForm.MinimizeBox = false;
                dateForm.Size = new System.Drawing.Size(300, 150);

                DateTimePicker dtp = new DateTimePicker();
                dtp.Format = DateTimePickerFormat.Short;
                dtp.Location = new System.Drawing.Point(20, 20);
                dtp.Size = new System.Drawing.Size(240, 23);

                if (_selectedPromotionId != -1 &&
                    _promoStartDate != DateTime.MinValue &&
                    _promoEndDate != DateTime.MinValue)
                {
                    // Lock ตามช่วง Promotion
                    dtp.MinDate = _promoStartDate.Date > DateTime.Today
                        ? _promoStartDate.Date
                        : DateTime.Today;
                    dtp.MaxDate = _promoEndDate.Date;
                    dtp.Value = dtp.MinDate;

                    dateForm.Text =
                        $"เลือกวันที่ ({_promoStartDate:dd/MM/yyyy}" +
                        $" - {_promoEndDate:dd/MM/yyyy})";
                }
                else
                {
                    // ไม่มี Promotion เลือกได้ตั้งแต่วันนี้
                    dtp.MinDate = DateTime.Today;
                    dtp.MaxDate = DateTime.MaxValue;
                    dtp.Value = DateTime.Today;
                    dateForm.Text = "เลือกวันที่";
                }

                Button btnOk = new Button();
                btnOk.Text = "ตกลง";
                btnOk.Location = new System.Drawing.Point(100, 60);
                btnOk.Size = new System.Drawing.Size(75, 25);
                btnOk.DialogResult = DialogResult.OK;

                dateForm.Controls.Add(dtp);
                dateForm.Controls.Add(btnOk);
                dateForm.AcceptButton = btnOk;

                if (dateForm.ShowDialog() == DialogResult.OK)
                    txtDate.Text = dtp.Value.ToString("dd/MM/yyyy");
            }
        }

        // คลิก txtTime - เปิดเลือกเวลา
        private void txtTime_Click(object sender, EventArgs e)
        {
            using (Form timeForm = new Form())
            {
                timeForm.Text = "เลือกเวลา";
                timeForm.Size = new System.Drawing.Size(300, 160);
                timeForm.StartPosition = FormStartPosition.CenterParent;
                timeForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                timeForm.MaximizeBox = false;
                timeForm.MinimizeBox = false;

                Label lbl = new Label();
                lbl.Text = "เลือกเวลา:";
                lbl.Location = new System.Drawing.Point(20, 20);

                DateTimePicker dtpTime = new DateTimePicker();
                dtpTime.Format = DateTimePickerFormat.Time;
                dtpTime.ShowUpDown = true;
                dtpTime.Location = new System.Drawing.Point(20, 45);
                dtpTime.Size = new System.Drawing.Size(240, 23);

                Button btnOk = new Button();
                btnOk.Text = "ตกลง";
                btnOk.Location = new System.Drawing.Point(100, 80);
                btnOk.Size = new System.Drawing.Size(75, 25);
                btnOk.DialogResult = DialogResult.OK;

                timeForm.Controls.Add(lbl);
                timeForm.Controls.Add(dtpTime);
                timeForm.Controls.Add(btnOk);
                timeForm.AcceptButton = btnOk;

                if (timeForm.ShowDialog() == DialogResult.OK)
                    txtTime.Text = dtpTime.Value.ToString("HH:mm");
            }
        }

        // ปุ่ม Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                using (var conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    int customerId = GetOrInsertCustomer(conn);
                    InsertBooking(conn, customerId);

                    string promoMsg = _selectedPromotionId != -1
                        ? $"\nPromotion: {lblSelectedPromo.Text}"
                        : "\nไม่ได้ใช้ Promotion";

                    MessageBox.Show(
                        $"จองสำเร็จ\n" +
                        $"ชื่อ: {txtName.Text}\n" +
                        $"ร้าน: {lblRestaurantName.Text.Replace("ร้านอาหารที่เลือก: ", "")}\n" +
                        $"วันที่: {txtDate.Text} เวลา: {txtTime.Text}\n" +
                        $"จำนวนคน: {txtPerson.Text} คน" +
                        promoMsg,
                        "สำเร็จ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    ClearForm(); // ล้างฟอร์มกรอกข้อมูลเพื่อเตรียมให้คนต่อไปจองต่อได้
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึก: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Insert หรือ Get Customer
        private int GetOrInsertCustomer(NpgsqlConnection conn)
        {
            string checkQuery = "SELECT CustomerID FROM Customers WHERE Email = @email";
            using (var checkCmd = new NpgsqlCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("email", txtMail.Text.Trim());
                var existing = checkCmd.ExecuteScalar();
                if (existing != null)
                    return Convert.ToInt32(existing);
            }

            string insertQuery = @"
                INSERT INTO Customers (FullName, Email, Phone)
                VALUES (@fullname, @email, @phone)
                RETURNING CustomerID";

            using (var cmd = new NpgsqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("fullname", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("email", txtMail.Text.Trim());
                cmd.Parameters.AddWithValue("phone", txtPhone.Text.Trim());
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Insert Booking
        private void InsertBooking(NpgsqlConnection conn, int customerId)
        {
            DateTime bookingDate = DateTime.ParseExact(
                $"{txtDate.Text} {txtTime.Text}",
                "dd/MM/yyyy HH:mm",
                System.Globalization.CultureInfo.InvariantCulture);

            int numberOfPeople = int.Parse(txtPerson.Text.Trim());

            object promotionId = _selectedPromotionId != -1
                ? (object)_selectedPromotionId
                : DBNull.Value;

            string query = @"
                INSERT INTO Bookings 
                    (CustomerID, RestaurantID, BookingDate, Status, NumberOfPeople, PromotionID)
                VALUES 
                    (@customerId, @restaurantId, @bookingDate, 'NewRequest', @numberOfPeople, @promotionId)";

            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("customerId", customerId);
                cmd.Parameters.AddWithValue("restaurantId", _selectedRestaurantId);
                cmd.Parameters.AddWithValue("bookingDate", bookingDate);
                cmd.Parameters.AddWithValue("numberOfPeople", numberOfPeople);
                cmd.Parameters.AddWithValue("promotionId", promotionId);
                cmd.ExecuteNonQuery();
            }
        }

        // Validate Input
        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("กรุณากรอกชื่อ", "แจ้งเตือน",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus(); return false;
            }
            if (string.IsNullOrEmpty(txtMail.Text.Trim()))
            {
                MessageBox.Show("กรุณากรอก Email", "แจ้งเตือน",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMail.Focus(); return false;
            }
            if (string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                MessageBox.Show("กรุณากรอกเบอร์โทร", "แจ้งเตือน",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus(); return false;
            }
            if (_selectedRestaurantId == -1)
            {
                MessageBox.Show("ระบบไม่พบรหัสร้านอาหาร กรุณากลับไปเลือกจากหน้าหลักใหม่", "แจ้งเตือน",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                MessageBox.Show("กรุณาเลือกวันที่", "แจ้งเตือน",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!DateTime.TryParseExact(
                txtDate.Text,
                "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime bookingDate))
            {
                MessageBox.Show("รูปแบบวันที่ไม่ถูกต้อง", "แจ้งเตือน",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDate.Text = string.Empty;
                return false;
            }

            if (bookingDate.Date < DateTime.Today)
            {
                MessageBox.Show(
                    $"ไม่สามารถจองวันที่ผ่านมาแล้วได้!\n\n" +
                    $"วันที่จอง: {bookingDate:dd/MM/yyyy}\n" +
                    $"วันที่ปัจจุบัน: {DateTime.Today:dd/MM/yyyy}\n\n" +
                    $"กรุณาเลือกวันที่ใหม่",
                    "วันที่ไม่ถูกต้อง",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtDate.Text = string.Empty;
                return false;
            }

            if (string.IsNullOrEmpty(txtTime.Text))
            {
                MessageBox.Show("กรุณาเลือกเวลา", "แจ้งเตือน",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txtPerson.Text.Trim()))
            {
                MessageBox.Show("กรุณากรอกจำนวนคน", "แจ้งเตือน",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPerson.Focus(); return false;
            }
            if (!int.TryParse(txtPerson.Text.Trim(), out int person) || person <= 0)
            {
                MessageBox.Show("กรุณากรอกจำนวนคนเป็นตัวเลขที่มากกว่า 0", "แจ้งเตือน",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPerson.Focus(); return false;
            }

            if (_selectedPromotionId != -1)
            {
                if (bookingDate.Date < _promoStartDate.Date ||
                    bookingDate.Date > _promoEndDate.Date)
                {
                    MessageBox.Show(
                        $"วันที่จองไม่อยู่ในช่วง Promotion!\n\n" +
                        $"วันที่จอง: {bookingDate:dd/MM/yyyy}\n" +
                        $"ช่วง Promotion: {_promoStartDate:dd/MM/yyyy}" +
                        $" - {_promoEndDate:dd/MM/yyyy}\n\n" +
                        $"กรุณาเลือกวันที่ใหม่",
                        "วันที่ไม่ถูกต้อง",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtDate.Text = string.Empty;
                    return false;
                }
            }

            return true;
        }

        // ล้างข้อมูลแบบคงค่าร้านอาหารเดิมไว้
        private void ClearForm()
        {
            txtName.Text = string.Empty;
            txtMail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtDate.Text = string.Empty;
            txtTime.Text = string.Empty;
            txtPerson.Text = string.Empty;

            // ไม่ล้างคำในช่อง txtSearch และไม่ ResetPromotion เพื่อให้จองร้านเดิมซ้ำหรือเลือกโปรเดิมได้สะดวกขึ้น
        }
    }
}