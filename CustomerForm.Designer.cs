namespace FoodHubApp
{
    partial class CustomerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblName = new Label(); // เปลี่ยนชื่อจาก Name เป็น lblName
            this.lblEmail = new Label();
            this.lblPhone = new Label();
            this.txtFullName = new TextBox();
            this.txtEmail = new TextBox();
            this.txtPhone = new TextBox();
            this.btnConfirmFinal = new Button();
            this.btnBackToBooking = new Button();
            this.SuspendLayout();

            // lblName
            this.lblName.AutoSize = true;
            this.lblName.Location = new Point(276, 142);
            this.lblName.Text = "Name";

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new Point(275, 189);
            this.lblEmail.Text = "Email";

            // lblPhone
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new Point(217, 230);
            this.lblPhone.Text = "Phone Number";

            // txtFullName, txtEmail, txtPhone (ตั้งค่า Location/Size ตามที่คุณทำไว้)
            this.txtFullName.Location = new Point(385, 135);
            this.txtEmail.Location = new Point(385, 182);
            this.txtPhone.Location = new Point(385, 223);

            // btnConfirmFinal
            this.btnConfirmFinal.Location = new Point(348, 305);
            this.btnConfirmFinal.Text = "Confirm Booking";
            this.btnConfirmFinal.Click += new EventHandler(btnConfirmFinal_Click);

            // btnBackToBooking
            this.btnBackToBooking.Location = new Point(217, 52);
            this.btnBackToBooking.Text = "Back";
            this.btnBackToBooking.Click += new EventHandler(btnBackToBooking_Click);

            // CustomerForm
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(btnBackToBooking);
            this.Controls.Add(btnConfirmFinal);
            this.Controls.Add(txtPhone);
            this.Controls.Add(txtEmail);
            this.Controls.Add(txtFullName);
            this.Controls.Add(lblPhone);
            this.Controls.Add(lblEmail);
            this.Controls.Add(lblName);
            this.Name = "CustomerForm"; // บรรทัดนี้ห้ามลบ แต่ห้ามซ้ำกับชื่อ Label
            this.Text = "Customer Registration";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblName;
        private Label lblEmail;
        private Label lblPhone;
        private TextBox txtFullName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private Button btnConfirmFinal;
        private Button btnBackToBooking;
    }
}