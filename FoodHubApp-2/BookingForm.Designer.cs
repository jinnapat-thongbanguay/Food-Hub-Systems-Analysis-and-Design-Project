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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
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
            txtPerson = new TextBox();
            dgvPromotions = new DataGridView();
            btnSave = new Button();
            btnClearPromo = new Button();
            lblRestaurantName = new Label();
            dtpDate = new DateTimePicker();
            dtpTime = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)dgvPromotions).BeginInit();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new System.Drawing.Font("Times New Roman", 10.2F, FontStyle.Bold);
            btnBack.ForeColor = Color.FromArgb(90, 60, 70);
            btnBack.Location = new Point(897, 15);
            btnBack.Margin = new Padding(3, 4, 3, 4);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(97, 29);
            btnBack.TabIndex = 14;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new System.Drawing.Font("Times New Roman", 10.2F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(90, 60, 70);
            lblName.Location = new Point(17, 144);
            lblName.Name = "lblName";
            lblName.Size = new Size(51, 19);
            lblName.TabIndex = 12;
            lblName.Text = "Name";
            // 
            // lblMail
            // 
            lblMail.AutoSize = true;
            lblMail.Font = new System.Drawing.Font("Times New Roman", 10.2F, FontStyle.Bold);
            lblMail.ForeColor = Color.FromArgb(90, 60, 70);
            lblMail.Location = new Point(17, 182);
            lblMail.Name = "lblMail";
            lblMail.Size = new Size(45, 19);
            lblMail.TabIndex = 11;
            lblMail.Text = "Mail";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new System.Drawing.Font("Times New Roman", 10.2F, FontStyle.Bold);
            lblPhone.ForeColor = Color.FromArgb(90, 60, 70);
            lblPhone.Location = new Point(17, 220);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(55, 19);
            lblPhone.TabIndex = 10;
            lblPhone.Text = "Phone";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new System.Drawing.Font("Times New Roman", 10.2F, FontStyle.Bold);
            lblDate.ForeColor = Color.FromArgb(90, 60, 70);
            lblDate.Location = new Point(17, 258);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(43, 19);
            lblDate.TabIndex = 9;
            lblDate.Text = "Date";
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Font = new System.Drawing.Font("Times New Roman", 10.2F, FontStyle.Bold);
            lblTime.ForeColor = Color.FromArgb(90, 60, 70);
            lblTime.Location = new Point(17, 296);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(47, 19);
            lblTime.TabIndex = 8;
            lblTime.Text = "Time";
            // 
            // lblPerson
            // 
            lblPerson.AutoSize = true;
            lblPerson.Font = new System.Drawing.Font("Times New Roman", 10.2F, FontStyle.Bold);
            lblPerson.ForeColor = Color.FromArgb(90, 60, 70);
            lblPerson.Location = new Point(17, 334);
            lblPerson.Name = "lblPerson";
            lblPerson.Size = new Size(61, 19);
            lblPerson.TabIndex = 7;
            lblPerson.Text = "Person";
            // 
            // lblPromotion
            // 
            lblPromotion.AutoSize = true;
            lblPromotion.ForeColor = Color.FromArgb(90, 60, 70);
            lblPromotion.Location = new Point(17, 573);
            lblPromotion.Name = "lblPromotion";
            lblPromotion.Size = new Size(88, 19);
            lblPromotion.TabIndex = 3;
            lblPromotion.Text = "Promotion:";
            // 
            // lblPromoHeader
            // 
            lblPromoHeader.AutoSize = true;
            lblPromoHeader.ForeColor = Color.FromArgb(90, 60, 70);
            lblPromoHeader.Location = new Point(17, 390);
            lblPromoHeader.Name = "lblPromoHeader";
            lblPromoHeader.Size = new Size(267, 19);
            lblPromoHeader.TabIndex = 6;
            lblPromoHeader.Text = "── Promotion ของร้านนี้ (คลิกเพื่อเลือก) ──";
            // 
            // lblNoPromo
            // 
            lblNoPromo.AutoSize = true;
            lblNoPromo.ForeColor = Color.Gray;
            lblNoPromo.Location = new Point(17, 548);
            lblNoPromo.Name = "lblNoPromo";
            lblNoPromo.Size = new Size(172, 19);
            lblNoPromo.TabIndex = 4;
            lblNoPromo.Text = "ไม่มี Promotion สำหรับร้านนี้";
            lblNoPromo.Visible = false;
            // 
            // lblSelectedPromo
            // 
            lblSelectedPromo.ForeColor = Color.Gray;
            lblSelectedPromo.Location = new Point(117, 573);
            lblSelectedPromo.Name = "lblSelectedPromo";
            lblSelectedPromo.Size = new Size(642, 26);
            lblSelectedPromo.TabIndex = 2;
            lblSelectedPromo.Text = "ไม่ได้เลือก";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new System.Drawing.Font("Times New Roman", 10.8F);
            txtName.Location = new Point(117, 141);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(876, 28);
            txtName.TabIndex = 1;
            // 
            // txtMail
            // 
            txtMail.BorderStyle = BorderStyle.FixedSingle;
            txtMail.Font = new System.Drawing.Font("Times New Roman", 10.8F);
            txtMail.Location = new Point(117, 179);
            txtMail.Margin = new Padding(3, 4, 3, 4);
            txtMail.Name = "txtMail";
            txtMail.Size = new Size(876, 28);
            txtMail.TabIndex = 2;
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Font = new System.Drawing.Font("Times New Roman", 10.8F);
            txtPhone.Location = new Point(117, 217);
            txtPhone.Margin = new Padding(3, 4, 3, 4);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(876, 28);
            txtPhone.TabIndex = 3;
            // 
            // txtPerson
            // 
            txtPerson.BorderStyle = BorderStyle.FixedSingle;
            txtPerson.Font = new System.Drawing.Font("Times New Roman", 10.8F);
            txtPerson.Location = new Point(117, 331);
            txtPerson.Margin = new Padding(3, 4, 3, 4);
            txtPerson.Name = "txtPerson";
            txtPerson.Size = new Size(876, 28);
            txtPerson.TabIndex = 6;
            // 
            // dgvPromotions
            // 
            dgvPromotions.BackgroundColor = Color.White;
            dgvPromotions.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Pink;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(90, 60, 70);
            dataGridViewCellStyle1.SelectionBackColor = Color.HotPink;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvPromotions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvPromotions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPromotions.EnableHeadersVisualStyles = false;
            dgvPromotions.GridColor = Color.FromArgb(255, 210, 220);
            dgvPromotions.Location = new Point(17, 415);
            dgvPromotions.Margin = new Padding(3, 4, 3, 4);
            dgvPromotions.Name = "dgvPromotions";
            dgvPromotions.RowHeadersWidth = 51;
            dgvPromotions.Size = new Size(978, 126);
            dgvPromotions.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new System.Drawing.Font("Times New Roman", 10.2F, FontStyle.Bold);
            btnSave.ForeColor = Color.FromArgb(90, 60, 70);
            btnSave.Location = new Point(898, 568);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(97, 29);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnClearPromo
            // 
            btnClearPromo.BackColor = Color.White;
            btnClearPromo.FlatStyle = FlatStyle.Flat;
            btnClearPromo.Font = new System.Drawing.Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClearPromo.ForeColor = Color.FromArgb(210, 105, 130);
            btnClearPromo.Location = new Point(773, 568);
            btnClearPromo.Margin = new Padding(3, 4, 3, 4);
            btnClearPromo.Name = "btnClearPromo";
            btnClearPromo.Size = new Size(97, 29);
            btnClearPromo.TabIndex = 1;
            btnClearPromo.Text = "Cancel";
            btnClearPromo.UseVisualStyleBackColor = false;
            btnClearPromo.Click += btnClearPromo_Click;
            // 
            // lblRestaurantName
            // 
            lblRestaurantName.AutoSize = true;
            lblRestaurantName.Font = new System.Drawing.Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRestaurantName.ForeColor = Color.FromArgb(210, 105, 130);
            lblRestaurantName.Location = new Point(14, 74);
            lblRestaurantName.Name = "lblRestaurantName";
            lblRestaurantName.Size = new Size(133, 25);
            lblRestaurantName.TabIndex = 15;
            lblRestaurantName.Text = "Restuarant:";
            // 
            // dtpDate
            // 
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Location = new Point(117, 258);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(876, 27);
            dtpDate.TabIndex = 16;
            // 
            // dtpTime
            // 
            dtpTime.Format = DateTimePickerFormat.Time;
            dtpTime.Location = new Point(117, 297);
            dtpTime.Name = "dtpTime";
            dtpTime.ShowUpDown = true;
            dtpTime.Size = new Size(876, 27);
            dtpTime.TabIndex = 17;
            // 
            // BookingForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 230, 235);
            ClientSize = new Size(1028, 710);
            Controls.Add(dtpTime);
            Controls.Add(dtpDate);
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
            Controls.Add(lblTime);
            Controls.Add(lblDate);
            Controls.Add(txtPhone);
            Controls.Add(lblPhone);
            Controls.Add(txtMail);
            Controls.Add(lblMail);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(btnBack);
            Font = new System.Drawing.Font("Times New Roman", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
        private TextBox txtPerson;
        private DataGridView dgvPromotions;
        private Button btnSave;
        private Button btnClearPromo;
        private Label lblRestaurantName;
        private DateTimePicker dtpDate;
        private DateTimePicker dtpTime;
    }
}