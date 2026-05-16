using System;
using System.Data;
using System.Windows.Forms;
using FoodHubCustomer; // เชื่อมต่อกับ BookingForm ตัวใหม่

namespace FoodHubApp
{
    public partial class Form1 : Form
    {
        // เรียกใช้ BookingService (Logic Tier) ตามหลัก 3-Tier
        BookingService _service = new BookingService();

        public Form1()
        {
            InitializeComponent();
        }

        // เมื่อเปิดหน้าจอมา ให้โหลดร้านอาหารทั้งหมดทันที
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAllRestaurants("");
        }

        // ฟังก์ชันส่วนกลางสำหรับโหลดข้อมูลร้านอาหาร
        private void LoadAllRestaurants(string keyword = "")
        {
            try
            {
                dgvData.DataSource = _service.SearchRestaurants(keyword);
                dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถโหลดข้อมูลร้านอาหารได้: " + ex.Message);
            }
        }

        // เมื่อกดปุ่มค้นหา (Search)
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAllRestaurants(txtSearch.Text);
        }

        // ค้นหาอัตโนมัติเมื่อมีการพิมพ์ในช่องค้นหา (เหลือไว้ตัวเดียว ไม่ซ้ำซ้อน)
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadAllRestaurants(txtSearch.Text);
        }

        // ฟังก์ชันควบคุมการคลิกบนตาราง (จัดการแยกปุ่มรีวิว และ หน้าจอง อย่างเด็ดขาด)
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ป้องกัน Error เวลาคลิกโดนหัวตาราง
            if (e.RowIndex < 0) return;

            // ดึงชื่อคอลัมน์ที่ถูกคลิก
            string colName = dgvData.Columns[e.ColumnIndex].Name;

            // ดึงค่า ID และชื่อร้านของแถวที่เลือกมาเตรียมไว้
            int resId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["restaurantid"].Value);
            string resName = dgvData.Rows[e.RowIndex].Cells["name"].Value?.ToString() ?? "";

            // ⚡ จุดสำคัญ: เช็คให้ตรงกับชื่อปุ่ม (btnViewReview) ที่เราเห็นในหน้า Design เมื่อกี้
            if (colName == "btnViewReview")
            {
                // 1. ถ้าคลิกปุ่มรีวิว -> ให้เปิดหน้าอ่านรีวิว (สมมติหน้าชื่อ ReviewForm หรือ ViewReviewForm ของหนูนะ)
                // อย่าลืมส่งรหัสร้านและชื่อร้านเข้าไปโชว์ด้วยครับ
                ReviewForm reviewPage = new ReviewForm(resId, resName);
                reviewPage.ShowDialog();
            }
            else
            {
                // 2. ถ้าคลิกส่วนอื่น ๆ บนแถวนั้นที่ไม่ใช่ปุ่มรีวิว -> ให้เปิดหน้า BookingForm (หน้าจอง) แทนครับ
                BookingForm bookingPage = new BookingForm(resId, resName);
                bookingPage.ShowDialog();
            }
        }

        private void btnManageBooking_Click(object sender, EventArgs e)
        {
            ManageBookingForm f = new ManageBookingForm();
            f.ShowDialog(); // เปิดหน้าต่างจัดการการจองขึ้นมาครอบไว้
        }

        private void btnOpenReview_Click(object sender, EventArgs e)
        {
            // เปลี่ยนจาก ReviewForm เป็น AddReviewForm ให้ตรงกับชื่อไฟล์จริงของหนู
            AddReviewForm reviewPage = new AddReviewForm();

            // สั่งให้หน้าต่างรีวิวเด้งขึ้นมาซ้อนทับหน้าแรกแบบ Pop-up
            reviewPage.ShowDialog();
        }
    } // ปิด Class
} // ปิด Namespace