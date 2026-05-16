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
            dgvData = new DataGridView();
            btnSearch = new Button();
            txtSearch = new TextBox();
            btnManageBooking = new Button();
            btnOpenReview = new Button();
            btnViewReview = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();
            // 
            // dgvData
            // 
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Columns.AddRange(new DataGridViewColumn[] { btnViewReview });
            dgvData.Location = new Point(164, 159);
            dgvData.Name = "dgvData";
            dgvData.RowHeadersWidth = 51;
            dgvData.Size = new Size(498, 247);
            dgvData.TabIndex = 0;
            dgvData.CellContentClick += dgvData_CellContentClick;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(525, 62);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(137, 27);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(164, 62);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(344, 27);
            txtSearch.TabIndex = 2;
            // 
            // btnManageBooking
            // 
            btnManageBooking.Location = new Point(527, 25);
            btnManageBooking.Name = "btnManageBooking";
            btnManageBooking.Size = new Size(135, 26);
            btnManageBooking.TabIndex = 3;
            btnManageBooking.Text = "History";
            btnManageBooking.UseVisualStyleBackColor = true;
            btnManageBooking.Click += btnManageBooking_Click;
            // 
            // btnOpenReview
            // 
            btnOpenReview.Location = new Point(525, 109);
            btnOpenReview.Name = "btnOpenReview";
            btnOpenReview.Size = new Size(137, 25);
            btnOpenReview.TabIndex = 4;
            btnOpenReview.Text = "Write Review";
            btnOpenReview.UseVisualStyleBackColor = true;
            btnOpenReview.Click += btnOpenReview_Click;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnOpenReview);
            Controls.Add(btnManageBooking);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(dgvData);
            Name = "Form1";
            Text = "Form1";
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
