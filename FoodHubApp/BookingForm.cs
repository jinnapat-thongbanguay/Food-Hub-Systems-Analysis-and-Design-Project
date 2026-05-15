using FoodHubApp;
using System;
using System.Data;
using System.Windows.Forms; // ต้องมีบรรทัดนี้ด้วย

namespace FoodHubApp // เพิ่มบรรทัดนี้ครอบไว้
{
    public partial class BookingForm : Form // ตรวจสอบว่ามี : Form
    {
        private int _restaurantId;
        private BookingService _service = new BookingService();

        public BookingForm(int restaurantId, string restaurantName)
        {
            InitializeComponent();
            _restaurantId = restaurantId;
            lblResName.Text = "ร้าน: " + restaurantName;
            LoadPromotions();
        }

        private void LoadPromotions()
        {
            DataTable dt = _service.GetPromotionsByRestaurant(_restaurantId);
            cmbPromo.DataSource = dt;
            cmbPromo.DisplayMember = "full_display";
            cmbPromo.ValueMember = "promotionid";

            // สั่งให้มันไปรันโค้ดล็อควันทันทีหลังจากโหลดข้อมูลเสร็จ
            if (dt.Rows.Count > 0)
            {
                cmbPromo_SelectedIndexChanged(null, null);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            bool success = _service.ConfirmBooking(
                _restaurantId,
                (int)cmbPromo.SelectedValue,
                dtpBooking.Value,
                (int)numPeople.Value
            );

            if (success)
            {
                MessageBox.Show("จองสำเร็จ! ข้อมูลถูกส่งไปที่ pgAdmin แล้ว");
                this.Close();
            }
        }
        // ในไฟล์ BookingForm.cs

        private void cmbPromo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ตรวจสอบว่ามีแถวข้อมูลถูกเลือกจริง
            if (cmbPromo.SelectedItem is DataRowView row)
            {
                DateTime start = Convert.ToDateTime(row["startdate"]);
                DateTime end = Convert.ToDateTime(row["enddate"]);

                // 1. คลายล็อคเก่าออกก่อน (ป้องกัน Error กรณี Min ใหม่ > Max เก่า)
                dtpBooking.MinDate = new DateTime(1753, 1, 1);
                dtpBooking.MaxDate = new DateTime(9998, 12, 31);

                // 2. คำนวณวันเริ่ม (ถ้าโปรเริ่มไปแล้ว ให้ยึดวันนี้เป็นขั้นต่ำ)
                DateTime actualMin = start < DateTime.Today ? DateTime.Today : start;

                // 3. ตั้งค่า .Value ให้อยู่ในช่วงก่อนล็อค (สำคัญมาก!)
                dtpBooking.Value = actualMin;

                // 4. สั่งล็อคจริง
                dtpBooking.MinDate = actualMin;
                dtpBooking.MaxDate = end;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // ปิดหน้าจอนี้เพื่อกลับไปยังหน้าแรก (Form1)
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // 1. เก็บข้อมูลที่เลือกไว้ในตัวแปร
            int promoId = (int)cmbPromo.SelectedValue;
            DateTime bookingDate = dtpBooking.Value;
            int people = (int)numPeople.Value;

            // 2. เปิดหน้ากรอกข้อมูลลูกค้า (CustomerForm) โดยส่งข้อมูลการจองไปด้วย
            CustomerForm custForm = new CustomerForm(_restaurantId, promoId, bookingDate, people);
            custForm.ShowDialog();

            // ถ้าหน้าข้อมูลลูกค้าจองสำเร็จ ให้ปิดหน้าจอนี้ตามไป
            if (custForm.Tag != null && custForm.Tag.ToString() == "Success")
            {
                this.Close();
            }
        }
    }
} // ปิดปีกกาของ namespace