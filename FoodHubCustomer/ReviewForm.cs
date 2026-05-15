using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace FoodHubCustomer
{
    public partial class ReviewForm : Form
    {
        private string connString = "Host=localhost;Username=foodhub_customer;Password=CustomerPass123;Database=FoodHubDB";

        // ประกาศ Control (เพิ่ม btnSearch เข้ามา)
        private TextBox textBox1 = null!;
        private Button btnSearch = null!; // 🌟 ปุ่มค้นหาที่เพิ่มมาใหม่
        private Button btnEnter = null!;
        private DataGridView dataGridView1 = null!;
        private TextBox textBox2 = null!;
        private Label label1 = null!;
        private Label label2 = null!;
        private Button btnBack = null!;

        // ประกาศตัวแปรเก็บ ID ลูกค้าที่ระดับ Class
        private int selectedCustomerId = 0;

        public ReviewForm()
        {
            InitializeComponent();

            // --- ตั้งค่าเริ่มต้นและการผูก Event ---
            btnEnter.Click += btnEnter_Click;
            btnBack.Click += btnBack_Click;
            btnSearch.Click += btnSearch_Click; // 🌟 ผูก Event ให้ปุ่ม Search

            // ตั้งค่า DataGrid
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false; // ป้องกันแถวว่างส่วนเกิน
        }

        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            btnSearch = new Button(); // 🌟 สร้างปุ่ม
            btnEnter = new Button();
            dataGridView1 = new DataGridView();
            textBox2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();

            // 
            // textBox1
            // 
            textBox1.Location = new Point(62, 80);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(390, 27); // 🌟 ปรับให้สั้นลงเพื่อให้วางปุ่ม Search ได้
            textBox1.TabIndex = 6;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(460, 78);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(95, 31);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Search";
            // 
            // btnEnter
            // 
            btnEnter.Location = new Point(635, 382);
            btnEnter.Name = "btnEnter";
            btnEnter.Size = new Size(159, 41);
            btnEnter.TabIndex = 5;
            btnEnter.Text = "Enter Review";
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView1.ColumnHeadersHeight = 29;
            dataGridView1.Location = new Point(62, 134);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(497, 177);
            dataGridView1.TabIndex = 4;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(62, 389);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(494, 27);
            textBox2.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(65, 37);
            label1.Name = "label1";
            label1.Size = new Size(91, 20);
            label1.TabIndex = 2;
            label1.Text = "Customer ID";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(62, 350);
            label2.Name = "label2";
            label2.Size = new Size(56, 20);
            label2.TabIndex = 1;
            label2.Text = "Review Text";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(768, 27);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(95, 45);
            btnBack.TabIndex = 0;
            btnBack.Text = "Back";
            // 
            // ReviewForm
            // 
            ClientSize = new Size(949, 470);
            Controls.Add(btnBack);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(dataGridView1);
            Controls.Add(btnEnter);
            Controls.Add(btnSearch); 
            Controls.Add(textBox1);
            Name = "ReviewForm";
            Text = "Customer Reviews";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        // 🌟 1. ฟังก์ชันเมื่อกดปุ่ม Search
        private void btnSearch_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("กรุณากรอก Customer ID ก่อนกดค้นหาครับ");
                return;
            }

            // เรียกใช้ฟังก์ชันโหลดข้อมูล
            LoadCustomerBookings(textBox1.Text);
        }

        // 2. ฟังก์ชันโหลดรายการจองจาก Database
        private void LoadCustomerBookings(string cid)
        {
            if (!int.TryParse(cid, out selectedCustomerId))
            {
                MessageBox.Show("Customer ID ต้องเป็นตัวเลขเท่านั้นครับ");
                return;
            }

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    string query = @"
                    SELECT b.bookingid, r.name AS restaurantname, b.restaurantid, rev.comment, rev.rating
                    FROM bookings b
                    JOIN restaurants r ON b.restaurantid = r.restaurantid
                    LEFT JOIN reviews rev ON b.customerid = rev.customerid AND b.restaurantid = rev.restaurantid
                     WHERE b.customerid = @cid";

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@cid", selectedCustomerId);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    if (dataGridView1.Columns.Contains("restaurantid"))
                        dataGridView1.Columns["restaurantid"]!.Visible = false;

                    if (dt.Rows.Count == 0)
                        MessageBox.Show("ไม่พบประวัติการจองของลูกค้ารหัสนี้ครับ");
                }
            }
            catch (Exception ex) { MessageBox.Show("Database Error: " + ex.Message); }
        }

        // 3. บันทึกรีวิวเมื่อกดปุ่ม Enter Review
        private void btnEnter_Click(object? sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("กรุณาคลิกเลือกแถวของร้านที่ต้องการรีวิวในตารางก่อนครับ");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("กรุณาพิมพ์รีวิวลงในช่อง Review ก่อนบันทึก");
                return;
            }

            try
            {
                int restId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["restaurantid"].Value);

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string insertQuery = @"
                        INSERT INTO reviews (customerid, restaurantid, rating, comment, createdat) 
                        VALUES (@cid, @rid, @rate, @comment, CURRENT_TIMESTAMP)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@cid", selectedCustomerId);
                        cmd.Parameters.AddWithValue("@rid", restId);
                        cmd.Parameters.AddWithValue("@rate", 5); // Default rating
                        cmd.Parameters.AddWithValue("@comment", textBox2.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("บันทึกรีวิวสำเร็จแล้ว!");
                    textBox2.Clear();

                    // รีเฟรชตาราง
                    LoadCustomerBookings(selectedCustomerId.ToString());
                }
            }
            catch (Exception ex) { MessageBox.Show("Insert Error: " + ex.Message); }
        }

        private void btnBack_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}