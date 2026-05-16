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
        // URL ของ API (เปลี่ยนพอร์ตให้ตรงกับเครื่องคุณ เช่น 7001)
        private readonly string apiUrl = "https://localhost:7001/api/promotions";

        public FormAdmin()
        {
            InitializeComponent();
        }

        // =======================================================
        // ⚠️ โค้ดส่วน InitializeComponent() และตัวแปรปุ่มต่างๆ ของคุณจะอยู่ตรงนี้ 
        // ไม่ต้องไปลบมันนะครับ ปล่อยไว้เหมือนเดิม
        // =======================================================

        // --- 1. โหลดโปรโมชั่นทั้งหมด (ผ่าน API) ---
        private void FormAdmin_Load(object sender, EventArgs e)
        {
            LoadPromotions("");
        }

        private async void LoadPromotions(string resId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
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

        // --- 2. ปุ่ม ENTER (ค้นหาโปรโมชั่นตาม ID ร้านอาหาร) ---
        private void button1_Click(object sender, EventArgs e)
        {
            // ใช้ btnSearch.Text ค้นหาตามโค้ดเดิมของคุณ
            LoadPromotions(btnSearch.Text);
        }

        // --- 3. คลิกแถวในตาราง แล้วข้อมูลเด้งเข้า TextBox ---
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtPromoId.Text = row.Cells["ID"].Value?.ToString();
                txtPromoName.Text = row.Cells["ชื่อโปรโมชั่น"].Value?.ToString();
                txtDiscount.Text = row.Cells["ส่วนลด"].Value?.ToString();
                txtPromoStatus.Text = row.Cells["สถานะ"].Value?.ToString();
            }
        }

        // --- 4. ปุ่มสำหรับบันทึกการแก้ไข (Update ผ่าน PUT) ---
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPromoId.Text))
            {
                MessageBox.Show("กรุณาเลือกโปรโมชั่นที่ต้องการแก้ไขจากตารางก่อนครับ");
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var updateData = new
                    {
                        Name = txtPromoName.Text,
                        DiscountAmount = decimal.Parse(txtDiscount.Text),
                        Status = txtPromoStatus.Text
                    };

                    var response = await client.PutAsJsonAsync($"{apiUrl}/{txtPromoId.Text}", updateData);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("แก้ไขโปรโมชั่นสำเร็จ! ✨");
                        LoadPromotions(""); // รีเฟรชตาราง
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถแก้ไขได้ Status Code: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ในการแก้ไขผ่าน API: " + ex.Message);
            }
        }

        // --- 5. ปุ่ม DELETE (ลบโปรโมชั่นผ่าน DELETE) ---
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
                            LoadPromotions(""); // รีเฟรชตาราง

                            // เคลียร์ค่าในช่องกรอก
                            txtPromoId.Clear();
                            txtPromoName.Clear();
                            txtDiscount.Clear();
                            txtPromoStatus.Clear();
                        }
                        else
                        {
                            MessageBox.Show("ไม่สามารถลบได้ Status Code: " + response.StatusCode);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error ในการลบผ่าน API: " + ex.Message);
                }
            }
        }

        // --- 6. ปุ่ม BACK ---
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
    }
}