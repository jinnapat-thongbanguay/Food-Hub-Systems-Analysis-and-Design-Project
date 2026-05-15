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
            txtSearch = new TextBox();
            btnEnter = new Button();
            btnBack = new Button();
            dgvRestaurants = new DataGridView();
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
            ((System.ComponentModel.ISupportInitialize)dgvRestaurants).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPromotions).BeginInit();
            SuspendLayout();

            // txtSearch
            txtSearch.Location = new Point(12, 12);
            txtSearch.Size = new Size(580, 23);
            txtSearch.PlaceholderText = "ค้นหาชื่อร้านอาหาร...";
            txtSearch.TabIndex = 0;

            // btnEnter
            btnEnter.Location = new Point(600, 12);
            btnEnter.Size = new Size(75, 23);
            btnEnter.Text = "Enter";
            btnEnter.UseVisualStyleBackColor = true;
            btnEnter.Click += btnEnter_Click;

            // btnBack
            btnBack.Location = new Point(697, 12);
            btnBack.Size = new Size(75, 23);
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;

            // dgvRestaurants
            dgvRestaurants.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRestaurants.Location = new Point(12, 45);
            dgvRestaurants.Size = new Size(760, 130);

            // lblName
            lblName.AutoSize = true;
            lblName.Location = new Point(12, 190);
            lblName.Text = "Name";

            // txtName
            txtName.Location = new Point(90, 187);
            txtName.Size = new Size(682, 23);
            txtName.TabIndex = 1;

            // lblMail
            lblMail.AutoSize = true;
            lblMail.Location = new Point(12, 220);
            lblMail.Text = "Mail";

            // txtMail
            txtMail.Location = new Point(90, 217);
            txtMail.Size = new Size(682, 23);
            txtMail.TabIndex = 2;

            // lblPhone
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(12, 250);
            lblPhone.Text = "Phone";

            // txtPhone
            txtPhone.Location = new Point(90, 247);
            txtPhone.Size = new Size(682, 23);
            txtPhone.TabIndex = 3;

            // lblDate
            lblDate.AutoSize = true;
            lblDate.Location = new Point(12, 280);
            lblDate.Text = "Date";

            // txtDate
            txtDate.Location = new Point(90, 277);
            txtDate.Size = new Size(682, 23);
            txtDate.TabIndex = 4;

            // lblTime
            lblTime.AutoSize = true;
            lblTime.Location = new Point(12, 310);
            lblTime.Text = "Time";

            // txtTime
            txtTime.Location = new Point(90, 307);
            txtTime.Size = new Size(682, 23);
            txtTime.TabIndex = 5;

            // lblPerson
            lblPerson.AutoSize = true;
            lblPerson.Location = new Point(12, 340);
            lblPerson.Text = "Person";

            // txtPerson
            txtPerson.Location = new Point(90, 337);
            txtPerson.Size = new Size(682, 23);
            txtPerson.TabIndex = 6;

            // lblPromoHeader
            lblPromoHeader.AutoSize = true;
            lblPromoHeader.Location = new Point(12, 375);
            lblPromoHeader.Text = "── Promotion ของร้านนี้ (คลิกเพื่อเลือก) ──";
            lblPromoHeader.ForeColor = System.Drawing.Color.DarkBlue;

            // dgvPromotions
            dgvPromotions.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPromotions.Location = new Point(12, 395);
            dgvPromotions.Size = new Size(760, 100);

            // lblNoPromo
            lblNoPromo.AutoSize = true;
            lblNoPromo.Location = new Point(12, 500);
            lblNoPromo.Text = "ไม่มี Promotion สำหรับร้านนี้";
            lblNoPromo.ForeColor = System.Drawing.Color.Gray;
            lblNoPromo.Visible = false;

            // lblPromotion
            lblPromotion.AutoSize = true;
            lblPromotion.Location = new Point(12, 520);
            lblPromotion.Text = "Promotion:";

            // lblSelectedPromo
            lblSelectedPromo.Location = new Point(90, 520);
            lblSelectedPromo.Size = new Size(500, 20);
            lblSelectedPromo.Text = "ไม่ได้เลือก";
            lblSelectedPromo.ForeColor = System.Drawing.Color.Gray;

            // btnClearPromo
            btnClearPromo.Location = new Point(600, 516);
            btnClearPromo.Size = new Size(75, 23);
            btnClearPromo.Text = "ยกเลิก";
            btnClearPromo.ForeColor = System.Drawing.Color.Red;
            btnClearPromo.UseVisualStyleBackColor = true;
            btnClearPromo.Click += btnClearPromo_Click;

            // btnSave
            btnSave.Location = new Point(697, 516);
            btnSave.Size = new Size(75, 23);
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;

            // BookingForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 560);
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
            Controls.Add(dgvRestaurants);
            Controls.Add(btnBack);
            Controls.Add(btnEnter);
            Controls.Add(txtSearch);
            Name = "BookingForm";
            Text = "BookingForm";
            ((System.ComponentModel.ISupportInitialize)dgvRestaurants).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPromotions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private TextBox txtSearch;
        private Button btnEnter;
        private Button btnBack;
        private DataGridView dgvRestaurants;
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
    }
}
