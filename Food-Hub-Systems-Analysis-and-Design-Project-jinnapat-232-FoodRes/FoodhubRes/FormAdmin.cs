using System;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json; // สำหรับรับส่ง JSON
using System.Windows.Forms;

namespace FoodhubRes
{
    public partial class FormAdmin : Form
    {
        // URL ของ API สำหรับโปรโมชั่น (เช็คพอร์ตของคุณให้ตรงด้วยนะครับ)
        private readonly string apiUrl = "https://localhost:7001/api/promotions";

        public FormAdmin()
        {
            InitializeComponent();
        }

        // =======================================================
        // โค้ดส่วน InitializeComponent() และตัวแปรปุ่มต่างๆ ของคุณจะซ่อนอยู่ในไฟล์ .Designer.cs 
        // ไม่ต้องไปลบมันนะครับ ปล่อยไว้เหมือนเดิม
        // =======================================================

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            // ปล่อยว่างไว้ หรือจะให้โหลดค่าเริ่มต้นก็ได้
        }

        // --- 1. ฟังก์ชันโหลดข้อมูลผ่าน API โดยต้องส่ง resId มาด้วย ---
        private async void LoadPromotions(string resId)
        {
            // ถ้าไม่มีการส่ง ID มา หรือเป็นค่าว่าง ไม่ต้องดึงอะไรมาโชว์เลยเพื่อความปลอดภัย
            if (string.IsNullOrEmpty(resId))
            {
                dataGridView1.DataSource = null;
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // ส่ง query parameter ไปหา API
                    string url = $"{apiUrl}?resId={resId}";
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

        // --- 2. ปุ่ม ENTER (ค้นหาโปรโมชั่นตาม ID ร้านอาหาร) ---
        private void button1_Click(object sender, EventArgs e)
        {
            // ดึงค่าจากช่องบนสุด
            string resId = btnSearch.Text.Trim();

            // ตรวจสอบค่าว่าง
            if (string.IsNullOrEmpty(resId))
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("กรุณากรอก Restaurant ID ของร้านคุณก่อนทำการค้นหาหรือจัดการข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ส่งค่าเลข ID จากช่องบน ลงไปแปะที่ช่อง txtResId ด้านล่างด้วย
            txtResId.Text = resId;

            // โหลดข้อมูลเข้าตาราง
            LoadPromotions(resId);
        }

        // --- 3. คลิกแถวในตาราง แล้วข้อมูลเด้งเข้า TextBox และ DatePicker ---
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // หมายเหตุ: ชื่อคอลัมน์ในวงเล็บอิงตามชื่อ Property JSON ที่ได้จาก API
                // เช็คให้ตรงกับ Model API ของคุณ (อาจจะเป็นตัวเล็กหรือตัวใหญ่)
                txtPromoId.Text = row.Cells["promotionId"]?.Value?.ToString() ?? row.Cells["Promotion ID"]?.Value?.ToString();
                txtPromoName.Text = row.Cells["name"]?.Value?.ToString() ?? row.Cells["Promotion"]?.Value?.ToString();
                txtDiscount.Text = row.Cells["discountAmount"]?.Value?.ToString() ?? row.Cells["Discount"]?.Value?.ToString();
                txtDescription.Text = row.Cells["description"]?.Value?.ToString() ?? row.Cells["Description"]?.Value?.ToString();

                // ดึงสถานะ
                string status = row.Cells["status"]?.Value?.ToString() ?? row.Cells["Status"]?.Value?.ToString();
                if (txtPromoStatus.Items.Contains(status))
                    txtPromoStatus.SelectedItem = status;
                else
                    txtPromoStatus.Text = status;

                // ดึงวันที่
                var startDateObj = row.Cells["startDate"]?.Value ?? row.Cells["Start Date"]?.Value;
                if (startDateObj != null && DateTime.TryParse(startDateObj.ToString(), out DateTime start))
                {
                    dtpStart.Value = start;
                }

                var endDateObj = row.Cells["endDate"]?.Value ?? row.Cells["End Date"]?.Value;
                if (endDateObj != null && DateTime.TryParse(endDateObj.ToString(), out DateTime end))
                {
                    dtpEnd.Value = end;
                }
            }
        }

        // --- 4. ปุ่ม INSERT (สร้างโปรโมชั่นใหม่ผ่าน POST) ---
        private async void btnInsert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPromoName.Text) || string.IsNullOrEmpty(txtDiscount.Text) || string.IsNullOrEmpty(txtResId.Text))
            {
                MessageBox.Show("กรุณากรอก ชื่อโปรโมชั่น, ส่วนลด และ Restaurant ID ให้ครบถ้วน");
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // เตรียมข้อมูลเป็น Object เพื่อแปลงเป็น JSON ส่งไป API
                    var newPromo = new
                    {
                        RestaurantId = int.Parse(txtResId.Text),
                        Name = txtPromoName.Text,
                        Description = txtDescription.Text,
                        DiscountAmount = decimal.Parse(txtDiscount.Text),
                        StartDate = dtpStart.Value.Date,
                        EndDate = dtpEnd.Value.Date.AddDays(1), // สิ้นสุดเวลาเที่ยงคืนของวันถัดไปตามลอจิกเดิม
                        Status = txtPromoStatus.SelectedItem == null ? "Active" : txtPromoStatus.SelectedItem.ToString()
                    };

                    var response = await client.PostAsJsonAsync(apiUrl, newPromo);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("สร้างโปรโมชั่นใหม่สำเร็จแล้ว");
                        LoadPromotions(btnSearch.Text.Trim());

                        // เคลียร์ฟอร์ม
                        txtPromoId.Clear();
                        txtPromoName.Clear();
                        txtDescription.Clear();
                        txtPromoStatus.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถสร้างได้ Status Code: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error Insert API: " + ex.Message); }
        }

        // --- 5. ปุ่ม UPDATE (แก้ไขโปรโมชั่นผ่าน PUT) ---
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPromoId.Text))
            {
                MessageBox.Show("กรุณาเลือกโปรโมชั่นที่ต้องการแก้ไขจากตารางก่อน");
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var updateData = new
                    {
                        Name = txtPromoName.Text,
                        Description = txtDescription.Text,
                        DiscountAmount = decimal.Parse(txtDiscount.Text),
                        StartDate = dtpStart.Value,
                        EndDate = dtpEnd.Value,
                        Status = txtPromoStatus.SelectedItem?.ToString() ?? "Active"
                    };

                    // ส่ง PUT ไปที่ /api/promotions/{id}
                    var response = await client.PutAsJsonAsync($"{apiUrl}/{txtPromoId.Text}", updateData);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("แก้ไขโปรโมชั่นสำเร็จ");
                        LoadPromotions(btnSearch.Text.Trim());
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถแก้ไขได้ Status Code: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error Update API: " + ex.Message); }
        }

        // --- 6. ปุ่ม DELETE (ลบโปรโมชั่นผ่าน DELETE) ---
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPromoId.Text))
            {
                MessageBox.Show("กรุณาเลือกโปรโมชั่นจากตารางก่อนลบ");
                return;
            }

            var confirm = MessageBox.Show($"ต้องการลบโปรโมชั่น ID: {txtPromoId.Text} ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var response = await client.DeleteAsync($"{apiUrl}/{txtPromoId.Text}");

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("ลบสำเร็จ!");
                            LoadPromotions(btnSearch.Text.Trim()); // โหลดตารางใหม่

                            // เคลียร์ค่าในช่องกรอก
                            txtPromoId.Clear();
                            txtPromoName.Clear();
                            txtDescription.Clear();
                            txtDiscount.Clear();
                            txtPromoStatus.SelectedIndex = -1;
                        }
                        else
                        {
                            MessageBox.Show("ไม่สามารถลบได้ Status Code: " + response.StatusCode);
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error Delete API: " + ex.Message); }
            }
        }

        // --- 7. ปุ่มและ Event อื่นๆ ตามโค้ดดั้งเดิม ---
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPromotion_Click(object sender, EventArgs e)
        {
            FormAdmin adminForm = new FormAdmin();
            adminForm.Show();
        }

        // ปล่อย Event ว่างเหล่านี้ไว้ เพื่อป้องกัน Error หน้า Designer จากการที่ผูกโค้ดไว้แต่ไม่มีฟังก์ชันรองรับ
        private void label2_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void btnSearch_TextChanged(object sender, EventArgs e) { }
    }
}