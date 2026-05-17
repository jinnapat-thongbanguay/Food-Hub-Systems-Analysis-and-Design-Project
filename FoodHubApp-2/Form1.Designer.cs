namespace FoodHubApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dgvData = new DataGridView();
            btnViewReview = new DataGridViewButtonColumn();
            btnSearch = new Button();
            txtSearch = new TextBox();
            btnManageBooking = new Button();
            btnOpenReview = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();
            // 
            // dgvData
            // 
            dgvData.BackgroundColor = Color.White;
            dgvData.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Pink;
            dataGridViewCellStyle1.Font = new Font("Times New Roman", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(90, 60, 70);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Columns.AddRange(new DataGridViewColumn[] { btnViewReview });
            dgvData.EnableHeadersVisualStyles = false;
            dgvData.GridColor = Color.FromArgb(255, 210, 220);
            dgvData.Location = new Point(184, 151);
            dgvData.Name = "dgvData";
            dgvData.RowHeadersWidth = 51;
            dgvData.Size = new Size(560, 265);
            dgvData.TabIndex = 0;
            dgvData.CellContentClick += dgvData_CellContentClick;
            // 
            // btnViewReview
            // 
            btnViewReview.HeaderText = "Review";
            btnViewReview.MinimumWidth = 6;
            btnViewReview.Name = "btnViewReview";
            btnViewReview.Text = "Review";
            btnViewReview.UseColumnTextForButtonValue = true;
            btnViewReview.Width = 125;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.LightPink;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.ForeColor = Color.FromArgb(90, 60, 70);
            btnSearch.Location = new Point(593, 55);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(154, 34);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "❤︎  Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Times New Roman", 12F);
            txtSearch.Location = new Point(184, 59);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(386, 30);
            txtSearch.TabIndex = 2;
            // 
            // btnManageBooking
            // 
            btnManageBooking.BackColor = Color.LightPink;
            btnManageBooking.FlatStyle = FlatStyle.Flat;
            btnManageBooking.ForeColor = Color.FromArgb(90, 60, 70);
            btnManageBooking.Location = new Point(593, 12);
            btnManageBooking.Name = "btnManageBooking";
            btnManageBooking.Size = new Size(152, 37);
            btnManageBooking.TabIndex = 3;
            btnManageBooking.Text = "☘︎  History";
            btnManageBooking.UseVisualStyleBackColor = false;
            btnManageBooking.Click += btnManageBooking_Click;
            // 
            // btnOpenReview
            // 
            btnOpenReview.BackColor = Color.LightPink;
            btnOpenReview.FlatStyle = FlatStyle.Flat;
            btnOpenReview.ForeColor = Color.FromArgb(90, 60, 70);
            btnOpenReview.Location = new Point(593, 95);
            btnOpenReview.Name = "btnOpenReview";
            btnOpenReview.Size = new Size(154, 34);
            btnOpenReview.TabIndex = 4;
            btnOpenReview.Text = "𝜗ৎ  Write Review";
            btnOpenReview.UseVisualStyleBackColor = false;
            btnOpenReview.Click += btnOpenReview_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 230, 235);
            ClientSize = new Size(900, 428);
            Controls.Add(btnOpenReview);
            Controls.Add(btnManageBooking);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(dgvData);
            Font = new Font("Times New Roman", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "Form1";
            Text = "Food Hub App (form1)";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvData;
        private Button btnSearch;
        private TextBox txtSearch;
        private Button btnManageBooking;
        private Button btnOpenReview;
        private DataGridViewButtonColumn btnViewReview;
    }
}
