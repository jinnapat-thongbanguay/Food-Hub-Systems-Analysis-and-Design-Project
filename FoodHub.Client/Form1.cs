using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* namespace FoodHub.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    }
} */
namespace FoodHub.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // อันนี้คือส่วนที่ต้องใส่ (ชื่อฟังก์ชันจะตามชื่อปุ่มที่คุณตั้ง)
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseManager db = new DatabaseManager();

                // Fix User ID 1 ตามโจทย์อาจารย์
                // ข้อมูล: CustomerID=1, RestaurantID=101, PromotionID=5, People=2
                db.CreateBooking(1, 101, 5, 2);

                MessageBox.Show("จองสำเร็จ! ข้อมูลบันทึกลง pgAdmin แล้ว");
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
            }
        }
    }
}