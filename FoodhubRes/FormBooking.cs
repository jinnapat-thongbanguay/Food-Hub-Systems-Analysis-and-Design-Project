using System;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json; // ต้องใช้ตัวนี้ช่วยรับส่ง JSON
using System.Windows.Forms;
namespace FoodhubRes
{
    public partial class FormBooking : Form
    {
        // URL ที่เปิดพอร์ตเชื่อมต่อกับเซิร์ฟเวอร์หลังบ้าน (เช็คพอร์ตของคุณด้วยนะว่าใช่ 7001 ไหม)
        private readonly string apiUrl = "https://localhost:7001/api/bookings";

        // ตัวแปรจำ ID ของรายการจองที่ถูกคลิกเลือกในตาราง
        private string selectedBookingId = "";

        // 📌 Constructor  เพื่อให้ตอนเปิดโปรแกรมมันวาดหน้าต่างขึ้นมาได้
        public FormBooking()
        {
            InitializeComponent();
        }

        // 📌  ส่วนของฟังก์ชันการทำงาน (ดึงข้อมูลและอัปเดตผ่าน API)
        // ---------------------------------------------------------

        private void FormBooking_Load(object sender, EventArgs e)
        {
            LoadBookings("");
        }

private async void LoadBookings(string resId)
{
    try
    {
        using (HttpClient client = new HttpClient())
        {
            // ส่ง Query Parameter ไปกรองข้อมูลร้านอาหาร (ถ้ามีค่าส่งมา)
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

private void btnEnter_Click(object sender, EventArgs e)
{
    LoadBookings(textBox1.Text);
}

private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
{
    if (e.RowIndex >= 0)
    {
        DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
        selectedBookingId = row.Cells["Booking ID"].Value?.ToString();
    }
}

private async void UpdateBookingStatus(string newStatus)
{
    if (string.IsNullOrEmpty(selectedBookingId))
    {
        MessageBox.Show("กรุณาคลิกเลือกรายการจองในตารางก่อนครับ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    string customerId = "ไม่พบข้อมูล";
    string bookingDate = "ไม่พบข้อมูล";

    foreach (DataGridViewRow row in dataGridView1.Rows)
    {
        if (row.Cells["Booking ID"].Value?.ToString() == selectedBookingId)
        {
            customerId = row.Cells["Customer ID"].Value?.ToString();
            bookingDate = row.Cells["เวลาที่จอง"].Value?.ToString();
            break;
        }
    }

    try
    {
        using (HttpClient client = new HttpClient())
        {
            var statusData = new { Status = newStatus };
            var response = await client.PutAsJsonAsync($"{apiUrl}/{selectedBookingId}/status", statusData);

            if (response.IsSuccessStatusCode)
            {
                string message = $"อัปเดตสถานะสำเร็จ! ✨\n\n" +
                                 $"🔹 ไอดีลูกค้า: {customerId}\n" +
                                 $"🔹 เวลาที่จอง: {bookingDate}\n" +
                                 $"🔹 สถานะล่าสุด: {newStatus}";

                MessageBox.Show(message, "ข้อมูลการจอง", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // โหลดตารางใหม่และล้างค่าที่จำไว้
                LoadBookings(textBox1.Text);
                selectedBookingId = "";
            }
            else
            {
                MessageBox.Show("ไม่สามารถอัปเดตสถานะได้ผ่าน API รหัส: " + response.StatusCode);
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error ในการอัปเดตสถานะผ่าน API: " + ex.Message, "ข้อผิดพลาด");
    }
}

private void btnCheckIn_Click(object sender, EventArgs e)
{
    UpdateBookingStatus("CheckedIn");
}

private void btnNoshow_Click(object sender, EventArgs e)
{
    UpdateBookingStatus("NoShow");
}

private void btnNewrequest_Click(object sender, EventArgs e)
{
    UpdateBookingStatus("NewRequest");
}

private void btnReject_Click(object sender, EventArgs e)
{
    UpdateBookingStatus("Closed");
}

private void btnBack_Click(object sender, EventArgs e)
{
    this.Close();
}
    }
}