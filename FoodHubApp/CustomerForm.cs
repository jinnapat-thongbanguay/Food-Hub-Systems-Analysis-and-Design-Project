using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FoodHubApp
{
    public partial class CustomerForm : Form
    {
        // ตัวแปรเก็บข้อมูลที่ส่งมาจากหน้า BookingForm
        private int _resId, _promoId, _people;
        private DateTime _date;
        private BookingService _service = new BookingService();

        public CustomerForm(int resId, int promoId, DateTime date, int people)
        {
            InitializeComponent();
            _resId = resId; _promoId = promoId; _date = date; _people = people;
        }

        private void btnConfirmFinal_Click(object sender, EventArgs e)
        {
            // 1. ตรวจสอบว่ากรอกข้อมูลครบไหม
            if (string.IsNullOrEmpty(txtFullName.Text) || string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("กรุณากรอกชื่อและเบอร์โทรศัพท์");
                return;
            }

            // 2. เรียก Service เพื่อบันทึกทั้งลูกค้าและการจอง
            bool success = _service.CreateCustomerAndBooking(
                txtFullName.Text, txtEmail.Text, txtPhone.Text,
                _resId, _promoId, _date, _people
            );

            if (success)
            {
                MessageBox.Show("จองสำเร็จ! บันทึกข้อมูลลงฐานข้อมูลเรียบร้อย");
                this.Tag = "Success"; // บอกหน้าแรกว่าสำเร็จแล้ว
                this.Close();
            }
        }
        private void btnBackToBooking_Click(object sender, EventArgs e)
        {
            // ปิดหน้านี้เพื่อกลับไปยังหน้า BookingForm
            this.Close();
        }
    }
}
