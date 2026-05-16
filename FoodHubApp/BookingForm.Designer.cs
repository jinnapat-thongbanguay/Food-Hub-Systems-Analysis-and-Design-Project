using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace FoodHubCustomer
{
    partial class BookingForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            btnBack = new Button();
            lblName = new Label();
            lblMail = new Label();
            lblPhone = new Label();
            lblDate = new Label();
            lblTime = new Label();
            lblPerson = new Label();
            lblPromotion = new Label();
            lblPromoHeader = new Label();
            lblNoPromo = new Label();
            lblSelectedPromo = new Label();
            txtName = new TextBox();
            txtMail = new TextBox();
            txtPhone = new TextBox();
            txtDate = new TextBox();
            txtTime = new TextBox();
            txtPerson = new TextBox();
            dgvPromotions = new DataGridView();
            btnSave = new Button();
            btnClearPromo = new Button();
            lblRestaurantName = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPromotions).BeginInit();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Location = new Point(797, 16);
            btnBack.Margin = new Padding(3, 4, 3, 4);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(86, 31);
            btnBack.TabIndex = 14;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(15, 152);
            lblName.Name = "lblName";
            lblName.Size = new Size(49, 20);
            lblName.TabIndex = 12;
            lblName.Text = "Name";
            // 
            // lblMail
            // 
            lblMail.AutoSize = true;
            lblMail.Location = new Point(15, 192);
            lblMail.Name = "lblMail";
            lblMail.Size = new Size(38, 20);
            lblMail.TabIndex = 11;
            lblMail.Text = "Mail";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(15, 232);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(50, 20);
            lblPhone.TabIndex = 10;
            lblPhone.Text = "Phone";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(15, 272);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(41, 20);
            lblDate.TabIndex = 9;
            lblDate.Text = "Date";
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(15, 312);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(42, 20);
            lblTime.TabIndex = 8;
            lblTime.Text = "Time";
            // 
            // lblPerson
            // 
            lblPerson.AutoSize = true;
            lblPerson.Location = new Point(15, 352);
            lblPerson.Name = "lblPerson";
            lblPerson.Size = new Size(52, 20);
            lblPerson.TabIndex = 7;
            lblPerson.Text = "Person";
            // 
            // lblPromotion
            // 
            lblPromotion.AutoSize = true;
            lblPromotion.Location = new Point(15, 603);
            lblPromotion.Name = "lblPromotion";
            lblPromotion.Size = new Size(82, 20);
            lblPromotion.TabIndex = 3;
            lblPromotion.Text = "Promotion:";
            // 
            // lblPromoHeader
            // 
            lblPromoHeader.AutoSize = true;
            lblPromoHeader.ForeColor = Color.DarkBlue;
            lblPromoHeader.Location = new Point(15, 410);
            lblPromoHeader.Name = "lblPromoHeader";
            lblPromoHeader.Size = new Size(268, 20);
            lblPromoHeader.TabIndex = 6;
            lblPromoHeader.Text = "── Promotion ของร้านนี้ (คลิกเพื่อเลือก) ──";
            // 
            // lblNoPromo
            // 
            lblNoPromo.AutoSize = true;
            lblNoPromo.ForeColor = Color.Gray;
            lblNoPromo.Location = new Point(15, 577);
            lblNoPromo.Name = "lblNoPromo";
            lblNoPromo.Size = new Size(179, 20);
            lblNoPromo.TabIndex = 4;
            lblNoPromo.Text = "ไม่มี Promotion สำหรับร้านนี้";
            lblNoPromo.Visible = false;
            // 
            // lblSelectedPromo
            // 
            lblSelectedPromo.ForeColor = Color.Gray;
            lblSelectedPromo.Location = new Point(104, 603);
            lblSelectedPromo.Name = "lblSelectedPromo";
            lblSelectedPromo.Size = new Size(571, 27);
            lblSelectedPromo.TabIndex = 2;
            lblSelectedPromo.Text = "ไม่ได้เลือก";
            // 
            // txtName
            // 
            txtName.Location = new Point(104, 148);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(779, 27);
            txtName.TabIndex = 1;
            // 
            // txtMail
            // 
            txtMail.Location = new Point(104, 188);
            txtMail.Margin = new Padding(3, 4, 3, 4);
            txtMail.Name = "txtMail";
            txtMail.Size = new Size(779, 27);
            txtMail.TabIndex = 2;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(104, 228);
            txtPhone.Margin = new Padding(3, 4, 3, 4);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(779, 27);
            txtPhone.TabIndex = 3;
            // 
            // txtDate
            // 
            txtDate.Location = new Point(104, 268);
            txtDate.Margin = new Padding(3, 4, 3, 4);
            txtDate.Name = "txtDate";
            txtDate.Size = new Size(779, 27);
            txtDate.TabIndex = 4;
            // 
            // txtTime
            // 
            txtTime.Location = new Point(104, 308);
            txtTime.Margin = new Padding(3, 4, 3, 4);
            txtTime.Name = "txtTime";
            txtTime.Size = new Size(779, 27);
            txtTime.TabIndex = 5;
            // 
            // txtPerson
            // 
            txtPerson.Location = new Point(104, 348);
            txtPerson.Margin = new Padding(3, 4, 3, 4);
            txtPerson.Name = "txtPerson";
            txtPerson.Size = new Size(779, 27);
            txtPerson.TabIndex = 6;
            // 
            // dgvPromotions
            // 
            dgvPromotions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPromotions.Location = new Point(15, 437);
            dgvPromotions.Margin = new Padding(3, 4, 3, 4);
            dgvPromotions.Name = "dgvPromotions";
            dgvPromotions.RowHeadersWidth = 51;
            dgvPromotions.Size = new Size(869, 133);
            dgvPromotions.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(798, 598);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(86, 31);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnClearPromo
            // 
            btnClearPromo.ForeColor = Color.Red;
            btnClearPromo.Location = new Point(687, 598);
            btnClearPromo.Margin = new Padding(3, 4, 3, 4);
            btnClearPromo.Name = "btnClearPromo";
            btnClearPromo.Size = new Size(86, 31);
            btnClearPromo.TabIndex = 1;
            btnClearPromo.Text = "ยกเลิก";
            btnClearPromo.UseVisualStyleBackColor = true;
            btnClearPromo.Click += btnClearPromo_Click;
            // 
            // lblRestaurantName
            // 
            lblRestaurantName.AutoSize = true;
            lblRestaurantName.Font = new System.Drawing.Font("Segoe UI", 14F);
            lblRestaurantName.Location = new Point(12, 78);
            lblRestaurantName.Name = "lblRestaurantName";
            lblRestaurantName.Size = new Size(173, 32);
            lblRestaurantName.TabIndex = 15;
            lblRestaurantName.Text = "ร้านอาหารที่เลือก:";
            // 
            // BookingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 747);
            Controls.Add(lblRestaurantName);
            Controls.Add(btnSave);
            Controls.Add(btnClearPromo);
            Controls.Add(lblSelectedPromo);
            Controls.Add(lblPromotion);
            Controls.Add(lblNoPromo);
            Controls.Add(dgvPromotions);
            Controls.Add(lblPromoHeader);
            Controls.Add(txtPerson);
            Controls.Add(lblPerson);
            Controls.Add(txtTime);
            Controls.Add(lblTime);
            Controls.Add(txtDate);
            Controls.Add(lblDate);
            Controls.Add(txtPhone);
            Controls.Add(lblPhone);
            Controls.Add(txtMail);
            Controls.Add(lblMail);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(btnBack);
            Margin = new Padding(3, 4, 3, 4);
            Name = "BookingForm";
            Text = "BookingForm";
            ((System.ComponentModel.ISupportInitialize)dgvPromotions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnBack;
        private Label lblName;
        private Label lblMail;
        private Label lblPhone;
        private Label lblDate;
        private Label lblTime;
        private Label lblPerson;
        private Label lblPromotion;
        private Label lblPromoHeader;
        private Label lblNoPromo;
        private Label lblSelectedPromo;
        private TextBox txtName;
        private TextBox txtMail;
        private TextBox txtPhone;
        private TextBox txtDate;
        private TextBox txtTime;
        private TextBox txtPerson;
        private DataGridView dgvPromotions;
        private Button btnSave;
        private Button btnClearPromo;
        private Label lblRestaurantName;
    }
}