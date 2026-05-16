using System;
using System.Data;
using System.Windows.Forms;

namespace FoodHubApp // ตรวจสอบชื่อ Namespace ให้ตรงกับโปรเจกต์ของคุณ
{
    public partial class Form1 : Form
    {
        // เรียกใช้ BookingService (Logic Tier) ตามหลัก 3-Tier
        BookingService _service = new BookingService();

        public Form1()
        {
            InitializeComponent();
        }

        // 1. เมื่อเปิดหน้าจอมา ให้โหลดร้านอาหารทั้งหมดทันที
        private void Form1_Load(object sender, EventArgs e)
        {
            // เมื่อเปิดแอป ให้ดึงร้านอาหารมาโชว์ทันที (ส่งค่าว่างไปเพื่อให้ SQL ดึงทุกร้าน)
            dgvData.DataSource = _service.SearchRestaurants("");

            // แถม: ตั้งค่าให้คอลัมน์ขยายเต็มพื้นที่ตาราง จะได้ดูสวยงาม
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // แถม: ซ่อนคอลัมน์ ID ไม่ให้ User เห็น (แต่เรายังใช้ ID ในการทำงานเบื้องหลังได้)
            if (dgvData.Columns.Contains("restaurantid"))
            {
                dgvData.Columns["restaurantid"].Visible = false;
            }
        }

        // 1. สร้างฟังก์ชันสำหรับโหลดข้อมูล
        private void LoadAllRestaurants(string keyword = "")
        {
            try
            {
                // เรียกใช้ Logic Tier เพื่อดึงข้อมูล
                dgvData.DataSource = _service.SearchRestaurants(keyword);

                // ตกแต่งตารางให้ดูโปรขึ้น (ถ้าต้องการ)
                dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถโหลดข้อมูลร้านอาหารได้: " + ex.Message);
            }
        }

        // 2. เมื่อกดปุ่มค้นหา (Search)
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAllRestaurants(txtSearch.Text); // ดึงข้อมูลตามคำที่พิมพ์
        }

        // 3. เมื่อคลิกเลือกแถวในตาราง (เพื่อไปหน้าจอง)
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // ตรวจสอบว่าไม่ได้คลิกที่หัวตาราง
            if (e.RowIndex >= 0)
            {
                // ดึงค่า ID และชื่อร้านจากแถวที่ถูกคลิก
                int resId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["restaurantid"].Value);
                string resName = dgvData.Rows[e.RowIndex].Cells["name"].Value.ToString();

                // เปิดหน้าจอ BookingForm (หน้า 2) พร้อมส่งข้อมูลร้านไป
                BookingForm bookingPage = new BookingForm(resId, resName);
                bookingPage.ShowDialog(); // แสดงหน้าจอจองแบบ Pop-up
            }
        }

        private void dgvData_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // ตรวจสอบว่าคลิกโดนแถวที่มีข้อมูล (ไม่ใช่หัวตาราง)
            if (e.RowIndex >= 0)
            {
                // 1. ดึง ID และชื่อร้านจากแถวที่เราคลิก
                int resId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["restaurantid"].Value);
                string resName = dgvData.Rows[e.RowIndex].Cells["name"].Value.ToString();

                // 2. สร้างคำสั่งเปิดหน้า BookingForm (หน้า 2) พร้อมส่งข้อมูลร้านไปให้
                BookingForm bookingPage = new BookingForm(resId, resName);

                // 3. สั่งให้หน้าจอจองเด้งขึ้นมาแบบ Pop-up
                bookingPage.ShowDialog();
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // ทุกครั้งที่พิมพ์หรือลบตัวอักษร ให้ไปค้นหาใน DB ทันที
            dgvData.DataSource = _service.SearchRestaurants(txtSearch.Text);
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            // ทุกครั้งที่พิมพ์หรือลบตัวอักษร ข้อมูลจะเปลี่ยนตามทันที
            dgvData.DataSource = _service.SearchRestaurants(txtSearch.Text);
        }
    }
}