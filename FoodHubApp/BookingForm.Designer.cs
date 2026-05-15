namespace FoodHubApp
{
    partial class BookingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnNext = new Button();
            numPeople = new NumericUpDown();
            dtpBooking = new DateTimePicker();
            cmbPromo = new ComboBox();
            lblResName = new Label();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)numPeople).BeginInit();
            SuspendLayout();
            // 
            // btnNext
            // 
            btnNext.Location = new Point(336, 318);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(119, 31);
            btnNext.TabIndex = 9;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // numPeople
            // 
            numPeople.Location = new Point(259, 256);
            numPeople.Name = "numPeople";
            numPeople.Size = new Size(283, 27);
            numPeople.TabIndex = 8;
            // 
            // dtpBooking
            // 
            dtpBooking.Location = new Point(259, 199);
            dtpBooking.Name = "dtpBooking";
            dtpBooking.Size = new Size(283, 27);
            dtpBooking.TabIndex = 7;
            // 
            // cmbPromo
            // 
            cmbPromo.FormattingEnabled = true;
            cmbPromo.Location = new Point(259, 143);
            cmbPromo.Name = "cmbPromo";
            cmbPromo.Size = new Size(283, 28);
            cmbPromo.TabIndex = 6;
            cmbPromo.SelectedIndexChanged += cmbPromo_SelectedIndexChanged;
            // 
            // lblResName
            // 
            lblResName.AutoSize = true;
            lblResName.Location = new Point(259, 102);
            lblResName.Name = "lblResName";
            lblResName.Size = new Size(72, 20);
            lblResName.TabIndex = 5;
            lblResName.Text = "ResName";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(259, 63);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(60, 25);
            btnBack.TabIndex = 10;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // BookingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBack);
            Controls.Add(btnNext);
            Controls.Add(numPeople);
            Controls.Add(dtpBooking);
            Controls.Add(cmbPromo);
            Controls.Add(lblResName);
            Name = "BookingForm";
            Text = "BookingForm";
            ((System.ComponentModel.ISupportInitialize)numPeople).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnNext;
        private NumericUpDown numPeople;
        private DateTimePicker dtpBooking;
        private ComboBox cmbPromo;
        private Label lblResName;
        private Button btnBack;
    }
}