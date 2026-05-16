namespace FoodHubApp
{
    partial class ManageBookingForm
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
            txtPhoneSearch = new TextBox();
            btnSearch = new Button();
            dgvMyBookings = new DataGridView();
            btnFormClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMyBookings).BeginInit();
            SuspendLayout();
            // 
            // txtPhoneSearch
            // 
            txtPhoneSearch.Location = new Point(197, 30);
            txtPhoneSearch.Name = "txtPhoneSearch";
            txtPhoneSearch.Size = new Size(247, 27);
            txtPhoneSearch.TabIndex = 0;
            txtPhoneSearch.Text = "กรุณากรอกเบอร์โทรศัพท์";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(494, 29);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(97, 29);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // dgvMyBookings
            // 
            dgvMyBookings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMyBookings.Location = new Point(27, 81);
            dgvMyBookings.Name = "dgvMyBookings";
            dgvMyBookings.RowHeadersWidth = 51;
            dgvMyBookings.Size = new Size(746, 287);
            dgvMyBookings.TabIndex = 2;
            dgvMyBookings.CellContentClick += dgvMyBookings_CellContentClick;
            // 
            // btnFormClose
            // 
            btnFormClose.Location = new Point(346, 385);
            btnFormClose.Name = "btnFormClose";
            btnFormClose.Size = new Size(98, 34);
            btnFormClose.TabIndex = 3;
            btnFormClose.Text = "Close";
            btnFormClose.UseVisualStyleBackColor = true;
            btnFormClose.Click += btnFormClose_Click;
            // 
            // ManageBookingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnFormClose);
            Controls.Add(dgvMyBookings);
            Controls.Add(btnSearch);
            Controls.Add(txtPhoneSearch);
            Name = "ManageBookingForm";
            Text = "ManageBookingForm";
            ((System.ComponentModel.ISupportInitialize)dgvMyBookings).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPhoneSearch;
        private Button btnSearch;
        private DataGridView dgvMyBookings;
        private Button btnFormClose;
    }
}