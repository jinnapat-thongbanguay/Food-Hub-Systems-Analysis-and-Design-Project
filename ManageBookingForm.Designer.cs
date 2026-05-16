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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            txtPhoneSearch = new TextBox();
            btnSearch = new Button();
            dgvMyBookings = new DataGridView();
            btnFormClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMyBookings).BeginInit();
            SuspendLayout();
            // 
            // txtPhoneSearch
            // 
            txtPhoneSearch.BackColor = SystemColors.Window;
            txtPhoneSearch.BorderStyle = BorderStyle.FixedSingle;
            txtPhoneSearch.Font = new Font("Times New Roman", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPhoneSearch.ForeColor = Color.FromArgb(90, 60, 70);
            txtPhoneSearch.Location = new Point(232, 33);
            txtPhoneSearch.Name = "txtPhoneSearch";
            txtPhoneSearch.Size = new Size(277, 28);
            txtPhoneSearch.TabIndex = 0;
            txtPhoneSearch.Text = "Please insert your Phone Number";
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.LightPink;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.ForeColor = Color.FromArgb(90, 60, 70);
            btnSearch.Location = new Point(557, 33);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(109, 28);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "❤︎ Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // dgvMyBookings
            // 
            dgvMyBookings.BackgroundColor = Color.White;
            dgvMyBookings.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.Pink;
            dataGridViewCellStyle2.Font = new Font("Times New Roman", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(90, 60, 70);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvMyBookings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvMyBookings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMyBookings.EnableHeadersVisualStyles = false;
            dgvMyBookings.GridColor = Color.FromArgb(255, 210, 220);
            dgvMyBookings.Location = new Point(30, 77);
            dgvMyBookings.Name = "dgvMyBookings";
            dgvMyBookings.RowHeadersWidth = 51;
            dgvMyBookings.Size = new Size(839, 273);
            dgvMyBookings.TabIndex = 2;
            dgvMyBookings.CellContentClick += dgvMyBookings_CellContentClick;
            // 
            // btnFormClose
            // 
            btnFormClose.BackColor = Color.FromArgb(255, 150, 170);
            btnFormClose.FlatStyle = FlatStyle.Flat;
            btnFormClose.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFormClose.ForeColor = Color.White;
            btnFormClose.Location = new Point(399, 365);
            btnFormClose.Name = "btnFormClose";
            btnFormClose.Size = new Size(110, 32);
            btnFormClose.TabIndex = 3;
            btnFormClose.Text = "Close";
            btnFormClose.UseVisualStyleBackColor = false;
            btnFormClose.Click += btnFormClose_Click;
            // 
            // ManageBookingForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 230, 235);
            ClientSize = new Size(900, 428);
            Controls.Add(btnFormClose);
            Controls.Add(dgvMyBookings);
            Controls.Add(btnSearch);
            Controls.Add(txtPhoneSearch);
            Font = new Font("Times New Roman", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
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