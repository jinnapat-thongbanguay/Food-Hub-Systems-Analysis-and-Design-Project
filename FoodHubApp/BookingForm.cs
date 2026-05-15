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
            if (dt.Rows.Count > 0)
            {
                cmbPromo.DataSource = dt;
                // เปลี่ยนมาใช้คอลัมน์ที่เราเชื่อมข้อความไว้แล้ว
                cmbPromo.DisplayMember = "full_display";
                cmbPromo.ValueMember = "promotionid";
            }
            else
            {
                cmbPromo.Text = "ไม่มีโปรโมชันในช่วงนี้";
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
            if (cmbPromo.SelectedValue != null && cmbPromo.SelectedItem is DataRowView row)
            {
                try
                {
                    DateTime startDate = Convert.ToDateTime(row["startdate"]);
                    DateTime endDate = Convert.ToDateTime(row["enddate"]);

                    // --- ขั้นตอนแก้ปัญหา "ล็อคไม่อยู่" ---

                    // 1. คลายล็อคเดิมออกให้กว้างที่สุดก่อน เพื่อป้องกัน Error ตอนสลับค่า
                    dtpBooking.MinDate = new DateTime(1753, 1, 1);
                    dtpBooking.MaxDate = new DateTime(9998, 12, 31);

                    // 2. คำนวณวันเริ่มที่เหมาะสม (ถ้าโปรเริ่มไปแล้วให้ยึดวันนี้เป็นขั้นต่ำ)
                    DateTime actualMin = startDate < DateTime.Today ? DateTime.Today : startDate;

                    // 3. สำคัญมาก: ต้องตั้งค่า .Value ให้อยู่ในช่วงใหม่ "ก่อน" จะสั่งล็อค Min/Max
                    dtpBooking.Value = actualMin;

                    // 4. สั่งล็อคขอบเขตจริง
                    dtpBooking.MinDate = actualMin;
                    dtpBooking.MaxDate = endDate;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("พบข้อผิดพลาดในการตั้งค่าวันที่: " + ex.Message);
                }
            }
        }

    }
} // ปิดปีกกาของ namespace