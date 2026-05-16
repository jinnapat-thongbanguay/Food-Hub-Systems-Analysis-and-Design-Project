using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FoodHubApp
{
    public partial class ManageBookingForm : Form
    {
        public ManageBookingForm()
        {
            InitializeComponent();
        }

        private BookingService _service = new BookingService();
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string phone = txtPhoneSearch.Text.Trim();
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์เพื่อค้นหา");
                return;
            }

            DataTable dt = _service.GetBookingsByPhone(phone);
            dgvMyBookings.DataSource = dt;

            // ตกแต่งตารางพื้นฐาน
            dgvMyBookings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // เพิ่มโค้ดส่วนนี้: สร้างปุ่มกดยกเลิกต่อท้ายแถวอัตโนมัติ
            // ตรวจสอบก่อนว่าเคยสร้างปุ่มไปแล้วหรือยัง เพื่อไม่ให้ปุ่มมันงอกซ้ำซ้อนเวลาสั่ง Search ใหม่
            if (!dgvMyBookings.Columns.Contains("btnCancel"))
            {
                DataGridViewButtonColumn cancelBtn = new DataGridViewButtonColumn();
                cancelBtn.Name = "btnCancel";
                cancelBtn.HeaderText = "Action";
                cancelBtn.Text = "Cancel";
                cancelBtn.UseColumnTextForButtonValue = true; // บังคับให้ทุกปุ่มโชว์คำว่า Cancel
                dgvMyBookings.Columns.Add(cancelBtn);
            }

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("ไม่พบประวัติการจองสำหรับเบอร์โทรนี้");
            }
        }

        private void dgvMyBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. ป้องกัน Error เวลาคลิกโดนแถวหัวตาราง (Header)
            if (e.RowIndex < 0) return;

            // 2. ตรวจสอบว่าผู้ใช้งานคลิกโดนคอลัมน์ปุ่มยกเลิกที่เราสร้างไว้ชื่อ "btnCancel" หรือไม่
            if (dgvMyBookings.Columns[e.ColumnIndex].Name == "btnCancel")
            {
                // 3. ดึงเลข bookingid จากแถวที่ผู้ใช้คลิกขึ้นมา
                int bookingId = Convert.ToInt32(dgvMyBookings.Rows[e.RowIndex].Cells["bookingid"].Value);
                string resName = dgvMyBookings.Rows[e.RowIndex].Cells["restaurant_name"].Value?.ToString() ?? "";

                // 4. แสดงกล่องข้อความถามย้ำเพื่อความแน่ใจ (UX ที่ดี)
                DialogResult result = MessageBox.Show(
                    $"คุณต้องการยกเลิกการจองของร้าน '{resName}' ใช่หรือไม่?",
                    "ยืนยันการยกเลิก",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // 5. ส่ง ID ไปอัปเดตที่ตารางใน Database ผ่าน Service
                    if (_service.CancelBooking(bookingId))
                    {
                        MessageBox.Show("ยกเลิกการจองเรียบร้อยแล้ว");

                        // 6. โดโหลดข้อมูลในตารางใหม่ทันที เพื่อให้รายการที่ยกเลิกหายไปจากหน้าจอ
                        btnSearch_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("เกิดข้อผิดพลาดทางเทคนิค ไม่สามารถยกเลิกการจองได้");
                    }
                }
            }
        }

        private void btnFormClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
