using System;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json; // ต้องใช้ตัวนี้ช่วยรับส่ง JSON
using System.Windows.Forms;

namespace FoodhubRes
{
    public partial class FormBooking : Form
    {
        // URL ที่เปิดพอร์ตเชื่อมต่อกับเซิร์ฟเวอร์หลังบ้าน
        private readonly string apiUrl = "https://localhost:7242/api/bookings";

        // ตัวแปรจำ ID ของรายการจองที่ถูกคลิกเลือกในตาราง
        private string selectedBookingId = "";

        public FormBooking()
        {
            InitializeComponent();
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            btnBack = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            btnEnter = new Button();
            btnCheckIn = new Button();
            btnNoshow = new Button();
            btnCancelled = new Button();
            btnConfirmed = new Button();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Location = new Point(433, 19);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(115, 48);
            btnBack.TabIndex = 0;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(43, 75);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(357, 27);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 29);
            label1.Name = "label1";
            label1.Size = new Size(98, 20);
            label1.TabIndex = 2;
            label1.Text = "Restaurant ID";
            // 
            // btnEnter
            // 
            btnEnter.Location = new Point(433, 72);
            btnEnter.Name = "btnEnter";
            btnEnter.Size = new Size(115, 31);
            btnEnter.TabIndex = 3;
            btnEnter.Text = "Enter";
            btnEnter.UseVisualStyleBackColor = true;
            btnEnter.Click += btnEnter_Click;
            // 
            // btnCheckIn
            // 
            btnCheckIn.Location = new Point(43, 140);
            btnCheckIn.Name = "btnCheckIn";
            btnCheckIn.Size = new Size(120, 29);
            btnCheckIn.TabIndex = 4;
            btnCheckIn.Text = "Check In";
            btnCheckIn.UseVisualStyleBackColor = true;
            btnCheckIn.Click += btnCheckIn_Click;
            // 
            // btnNoshow
            // 
            btnNoshow.Location = new Point(170, 140);
            btnNoshow.Name = "btnNoshow";
            btnNoshow.Size = new Size(120, 29);
            btnNoshow.TabIndex = 5;
            btnNoshow.Text = "No Show";
            btnNoshow.UseVisualStyleBackColor = true;
            btnNoshow.Click += btnNoshow_Click;
            // 
            // btnCancelled
            // 
            btnCancelled.Location = new Point(429, 140);
            btnCancelled.Name = "btnCancelled";
            btnCancelled.Size = new Size(120, 29);
            btnCancelled.TabIndex = 6;
            btnCancelled.Text = "Cancelled";
            btnCancelled.UseVisualStyleBackColor = true;
            btnCancelled.Click += btnCancelled_Click;
            // 
            // btnConfirmed
            // 
            btnConfirmed.Location = new Point(297, 140);
            btnConfirmed.Name = "btnConfirmed";
            btnConfirmed.Size = new Size(120, 29);
            btnConfirmed.TabIndex = 8;
            btnConfirmed.Text = "Confirmed";
            btnConfirmed.UseVisualStyleBackColor = true;
            btnConfirmed.Click += btnConfirmed_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(43, 201);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(502, 205);
            dataGridView1.TabIndex = 7;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // FormBooking
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 192, 192);
            ClientSize = new Size(800, 451);
            Controls.Add(btnConfirmed);
            Controls.Add(dataGridView1);
            Controls.Add(btnCancelled);
            Controls.Add(btnNoshow);
            Controls.Add(btnCheckIn);
            Controls.Add(btnEnter);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(btnBack);
            Name = "FormBooking";
            Text = "FormBooking";
            Load += FormBooking_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btnBack;
        private TextBox textBox1;
        private Label label1;
        private Button btnEnter;
        private Button btnCheckIn;
        private Button btnNoshow;
        private Button btnCancelled;
        private DataGridView dataGridView1;
        private Button btnConfirmed;
        #endregion

        // ---------------------------------------------------------
        // ส่วนของฟังก์ชันการทำงาน (ดึงข้อมูลและอัปเดตผ่าน API)
        // ---------------------------------------------------------

        private void FormBooking_Load(object sender, EventArgs e)
        {
            LoadBookings("");
        }

        private async void LoadBookings(string resId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // ส่ง Query Parameter ไปกรองข้อมูลร้านอาหาร (ถ้ามีค่าส่งมา)
                    string url = string.IsNullOrEmpty(resId) ? apiUrl : $"{apiUrl}?resId={resId}";

                    var data = await client.GetFromJsonAsync<object>(url);
                    dataGridView1.DataSource = data;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ในการดึงข้อมูลผ่าน API: " + ex.Message, "ข้อผิดพลาด");
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            LoadBookings(textBox1.Text);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // หมายเหตุ: ชื่อ Column ขึ้นอยู่กับโครงสร้าง JSON ที่ API ส่งกลับมา
                // หากพังตรงนี้ ให้เช็คชื่อ Key จาก JSON แล้วเปลี่ยนชื่อตรงในวงเล็บให้ตรงกัน
                selectedBookingId = row.Cells["bookingId"].Value?.ToString() ?? row.Cells["Booking ID"].Value?.ToString();
            }
        }

        private async void UpdateBookingStatus(string newStatus)
        {
            if (string.IsNullOrEmpty(selectedBookingId))
            {
                MessageBox.Show("กรุณาคลิกเลือกรายการจองในตารางก่อนครับ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // หาข้อมูลมาทำ Alert แบบในโค้ดเดิม (ชื่อคอลัมน์ต้องตรงกับ JSON ที่รับมา หรือตั้งค่า DataGridView ไว้)
            string customerId = "ไม่พบข้อมูล";
            string bookingDate = "ไม่พบข้อมูล";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string rowId = row.Cells["bookingId"]?.Value?.ToString() ?? row.Cells["Booking ID"]?.Value?.ToString();

                if (rowId == selectedBookingId)
                {
                    customerId = row.Cells["customerId"]?.Value?.ToString() ?? row.Cells["Customer ID"]?.Value?.ToString() ?? "ไม่พบข้อมูล";
                    bookingDate = row.Cells["bookingDate"]?.Value?.ToString() ?? row.Cells["เวลาที่จอง"]?.Value?.ToString() ?? "ไม่พบข้อมูล";
                    break;
                }
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var statusData = new { Status = newStatus };
                    var response = await client.PutAsJsonAsync($"{apiUrl}/{selectedBookingId}/status", statusData);

                    if (response.IsSuccessStatusCode)
                    {
                        string message = $"อัปเดตสถานะสำเร็จ! ✨\n\n" +
                                         $"🔹 ไอดีลูกค้า: {customerId}\n" +
                                         $"🔹 เวลาที่จอง: {bookingDate}\n" +
                                         $"🔹 สถานะล่าสุด: {newStatus}";

                        MessageBox.Show(message, "ข้อมูลการจอง", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // โหลดตารางใหม่และล้างค่าที่จำไว้
                        LoadBookings(textBox1.Text);
                        selectedBookingId = "";
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถอัปเดตสถานะได้ผ่าน API รหัส: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ในการอัปเดตสถานะผ่าน API: " + ex.Message, "ข้อผิดพลาด");
            }
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            UpdateBookingStatus("CheckedIn");
        }

        private void btnNoshow_Click(object sender, EventArgs e)
        {
            UpdateBookingStatus("NoShow");
        }

        private void btnConfirmed_Click(object sender, EventArgs e)
        {
            UpdateBookingStatus("Confirmed");
        }

        private void btnCancelled_Click(object sender, EventArgs e)
        {
            // 1. เช็คก่อนว่าผู้ใช้ได้คลิกเลือกรายการจองจากตาราง (DataGridView) หรือยัง
            if (string.IsNullOrEmpty(selectedBookingId))
            {
                MessageBox.Show("กรุณาคลิกเลือกรายการจองในตารางก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. ขึ้นกล่องข้อความให้กดยืนยันเพื่อความชัวร์
            var confirm = MessageBox.Show($"คุณต้องการยกเลิกรายการจองรหัส: {selectedBookingId} ใช่หรือไม่?", "ยืนยันการยกเลิก", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                UpdateBookingStatus("Cancelled");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}