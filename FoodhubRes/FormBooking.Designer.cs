using System;
using System.Data;
using System.Drawing; 
using System.Windows.Forms;
using Npgsql;

namespace FoodhubRes
{
    public partial class FormBooking : Form
    {
        // เปลี่ยน Password และ Database ให้ตรงกับของคุณ
        private string connString = "Host=localhost;Username=foodhub_admin;Password=AdminPass123;Database=FoodHubDB";

        // ตัวแปรสำหรับจำ ID ของรายการจองที่ถูกคลิก
        private string selectedBookingId = "";

        // 1. ต้องมี Constructor เพื่อเรียกให้หน้าต่างทำงาน
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            btnBack = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            btnEnter = new Button();
            btnCheckIn = new Button();
            btnNoshow = new Button();
            btnReject = new Button();
            btnNewrequest= new Button();
            dataGridView1 = new DataGridView();
            btnNewrequest = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Location = new Point(657, 29);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(127, 48);
            btnBack.TabIndex = 0;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(44, 74);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(357, 27);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 29);
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
            btnCheckIn.Location = new Point(44, 140);
            btnCheckIn.Name = "btnCheckIn";
            btnCheckIn.Size = new Size(120, 29);
            btnCheckIn.TabIndex = 4;
            btnCheckIn.Text = "Check In";
            btnCheckIn.UseVisualStyleBackColor = true;
            btnCheckIn.Click += btnCheckIn_Click;
            // 
            // btnNoshow
            // 
            btnNoshow.Location = new Point(214, 140);
            btnNoshow.Name = "btnNoshow";
            btnNoshow.Size = new Size(120, 29);
            btnNoshow.TabIndex = 5;
            btnNoshow.Text = "No Show";
            btnNoshow.UseVisualStyleBackColor = true;
            btnNoshow.Click += btnNoshow_Click;
            // 
            // btnReject
            // 
            btnReject.Location = new Point(596, 262);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(120, 29);
            btnReject.TabIndex = 6;
            btnReject.Text = "Reject";
            btnReject.UseVisualStyleBackColor = true;
            btnReject.Click += btnReject_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(44, 201);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(502, 206);
            dataGridView1.TabIndex = 7;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // btnNewrequest
            // 
            btnNewrequest.Location = new Point(433, 140);
            btnNewrequest.Name = "btnNewrequest";
            btnNewrequest.Size = new Size(120, 29);
            btnNewrequest.TabIndex = 8;
            btnNewrequest.Text = "New Request";
            btnNewrequest.UseVisualStyleBackColor = true;
            btnNewrequest.Click += btnNewrequest_Click;
            // 
            // FormBooking
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnNewrequest);
            Controls.Add(dataGridView1);
            Controls.Add(btnReject);
            Controls.Add(btnNoshow);
            Controls.Add(btnCheckIn);
            Controls.Add(btnEnter);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(btnBack);
            Name = "FormBooking";
            Text = "FormBooking";
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
        private Button btnReject;
        private DataGridView dataGridView1;
        #endregion

        // ---------------------------------------------------------
        // ส่วนของฟังก์ชันการทำงาน (Database และ Event ปุ่ม)
        // ---------------------------------------------------------

        private void FormBooking_Load(object sender, EventArgs e)
        {
            LoadBookings("");
        }

        private void LoadBookings(string resId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string sql = @"SELECT bookingid AS ""Booking ID"", 
                                          customerid AS ""Customer ID"", 
                                          restaurantid AS ""Restaurant ID"", 
                                          bookingdate AS ""เวลาที่จอง"", 
                                          status AS ""สถานะ""
                                   FROM bookings";

                    if (!string.IsNullOrEmpty(resId))
                    {
                        sql += $" WHERE restaurantid = {resId}";
                    }

                    sql += " ORDER BY bookingdate DESC";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dataGridView1.DataSource = dt;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ในการดึงข้อมูล: " + ex.Message);
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
                selectedBookingId = row.Cells["Booking ID"].Value.ToString();
            }
        }

        private void UpdateBookingStatus(string newStatus)
        {
            if (string.IsNullOrEmpty(selectedBookingId))
            {
                MessageBox.Show("กรุณาคลิกเลือกรายการจองในตารางก่อนครับ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. ดึงข้อมูลจากแถวที่คุณเลือกในตารางมาเตรียมไว้
            string customerId = "ไม่พบข้อมูล";
            string bookingDate = "ไม่พบข้อมูล";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Booking ID"].Value?.ToString() == selectedBookingId)
                {
                    customerId = row.Cells["Customer ID"].Value.ToString();
                    bookingDate = row.Cells["เวลาที่จอง"].Value.ToString();
                    break;
                }
            }

            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string sql = "UPDATE bookings SET status = @status WHERE bookingid = @id";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("status", newStatus);
                        cmd.Parameters.AddWithValue("id", int.Parse(selectedBookingId));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // 2. สร้างข้อความแจ้งเตือน (Popup) นำข้อมูลมาแสดง
                            string message = $"อัปเดตสถานะสำเร็จ!\n\n" +
                                             $"🔹 ไอดีลูกค้า: {customerId}\n" +
                                             $"🔹 เวลาที่จอง: {bookingDate}\n" +
                                             $"🔹 สถานะล่าสุด: {newStatus}";

                            MessageBox.Show(message, "ข้อมูลการจอง", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // โหลดตารางใหม่และล้างค่าที่จำไว้
                            LoadBookings(textBox1.Text);
                            selectedBookingId = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ในการอัปเดตสถานะ: " + ex.Message);
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

        private void btnNewrequest_Click(object sender, EventArgs e)
        {
            UpdateBookingStatus("NewRequest");
        }
        private void btnReject_Click(object sender, EventArgs e)
        {
            UpdateBookingStatus("Closed");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private Button btnNewrequest;
    }
}