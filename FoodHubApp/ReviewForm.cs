using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FoodHubApp
{
    public partial class ReviewForm : Form
    {
        // ต้องระบุ (int resId, string resName) ให้ตรงกับตอนส่งมา
        public ReviewForm(int resId, string resName)
        {
            InitializeComponent();
            lblShowResName.Text = "Review for: " + resName; // โชว์ชื่อร้านจริง

            // โหลดรีวิวมาใส่ตาราง
            BookingService service = new BookingService();
            dgvReviews.DataSource = service.GetReviewsByRestaurant(resId);

            // ปรับตารางให้อ่านง่าย
            dgvReviews.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReviews.ReadOnly = true;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            // คำสั่งสำหรับปิดหน้าต่างปัจจุบัน
            this.Close();
        }
        private void LoadReviews(int resId)
        {
            BookingService service = new BookingService();
            dgvReviews.DataSource = service.GetReviewsByRestaurant(resId);
        }
    }
}
