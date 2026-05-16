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

            dgvMyBookings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;



            if (!dgvMyBookings.Columns.Contains("btnCancel"))

            {

                DataGridViewButtonColumn cancelBtn = new DataGridViewButtonColumn();

                cancelBtn.Name = "btnCancel";

                cancelBtn.HeaderText = "Action";

                cancelBtn.Text = "Cancel";

                cancelBtn.UseColumnTextForButtonValue = false; // ⚡ เปลี่ยนเป็น false เพื่อให้เราเขียนทับตัวหนังสือบนบางปุ่มได้ครับ

                dgvMyBookings.Columns.Add(cancelBtn);

            }



            // เพิ่มโค้ดชุดนี้เข้าไปท้ายฟังก์ชันเพื่อวนลูปปิดปุ่มแถวที่กดยกเลิกไม่ได้

            foreach (DataGridViewRow row in dgvMyBookings.Rows)

            {

                if (row.IsNewRow) continue;



                string status = row.Cells["status"].Value?.ToString() ?? "";



                // ถ้าผ่านเงื่อนไขจองที่ปกติ ยัดคำว่า Cancel ลงปุ่มปกติ

                if (status == "NewRequest" || status == "Confirmed")

                {

                    row.Cells["btnCancel"].Value = "Cancel";

                }

                else

                {

                    // ถ้าสถานะเป็น Cancelled, Completed, CheckedIn หรือ NoShow

                    // ให้ล้างตัวหนังสือออก และเปลี่ยนสไตล์เป็นกล่องธรรมดาเพื่อให้กดคลิกไม่ได้

                    row.Cells["btnCancel"].Value = "—";

                    DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)row.Cells["btnCancel"];

                    buttonCell.FlatStyle = FlatStyle.Flat; // เปลี่ยนให้เรียบแบนกลืนไปกับตาราง

                }

            }

        }



        private void dgvMyBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)

        {

            if (e.RowIndex < 0) return;



            if (dgvMyBookings.Columns[e.ColumnIndex].Name == "btnCancel")

            {

                // 1. ดึงสถานะ (status) ของแถวที่ถูกคลิกขึ้นมาเช็คก่อน

                // หมายเหตุ: ช่องเครื่องหมายคำพูด ต้องสะกดให้ตรงกับชื่อคอลัมน์สถานะในตาราง DataGridView (เช่น "status")

                string currentStatus = dgvMyBookings.Rows[e.RowIndex].Cells["status"].Value?.ToString() ?? "";



                // 2. ดักจับเงื่อนไข: ถ้าสถานะไม่ใช่ NewRequest และไม่ใช่ Confirmed จะไม่ยอมให้ยกเลิก

                if (currentStatus != "NewRequest" && currentStatus != "Confirmed")

                {

                    MessageBox.Show($"ไม่สามารถยกเลิกการจองได้ เนื่องจากรายการนี้มีสถานะเป็น '{currentStatus}'",

                    "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return; // สั่งเด้งออกทันที ไม่ให้ทำโค้ดยกเลิกด้านล่าง

                }



                // --- โค้ดเดิมของหนูด้านล่างรันต่อตามปกติ ถ้าผ่านเงื่อนไขด้านบนมาได้ ---

                int bookingId = Convert.ToInt32(dgvMyBookings.Rows[e.RowIndex].Cells["bookingid"].Value);

                string resName = dgvMyBookings.Rows[e.RowIndex].Cells["restaurant_name"].Value?.ToString() ?? "";



                DialogResult result = MessageBox.Show(

                $"คุณต้องการยกเลิกการจองของร้าน '{resName}' ใช่หรือไม่?",

                "ยืนยันการยกเลิก",

                MessageBoxButtons.YesNo,

                MessageBoxIcon.Question

                );



                if (result == DialogResult.Yes)

                {

                    if (_service.CancelBooking(bookingId))

                    {

                        MessageBox.Show("ยกเลิกการจองเรียบร้อยแล้ว");

                        btnSearch_Click(sender, e); // ปรับจาก null, null เป็น sender, e ป้องกันการไล่ตัวแปรพัง

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