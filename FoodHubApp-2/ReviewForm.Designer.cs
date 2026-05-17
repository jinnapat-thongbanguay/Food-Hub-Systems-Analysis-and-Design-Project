namespace FoodHubApp
{
    partial class ReviewForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            lblRestaurantName = new Label();
            dgvReviews = new DataGridView();
            lblShowResName = new Label();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvReviews).BeginInit();
            SuspendLayout();
            // 
            // lblRestaurantName
            // 
            lblRestaurantName.AutoSize = true;
            lblRestaurantName.Location = new Point(270, 36);
            lblRestaurantName.Name = "lblRestaurantName";
            lblRestaurantName.Size = new Size(0, 19);
            lblRestaurantName.TabIndex = 0;
            // 
            // dgvReviews
            // 
            dgvReviews.BackgroundColor = Color.White;
            dgvReviews.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Pink;
            dataGridViewCellStyle1.Font = new Font("Times New Roman", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(90, 60, 70);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvReviews.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvReviews.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReviews.EnableHeadersVisualStyles = false;
            dgvReviews.GridColor = Color.FromArgb(255, 210, 220);
            dgvReviews.Location = new Point(38, 58);
            dgvReviews.Name = "dgvReviews";
            dgvReviews.RowHeadersWidth = 51;
            dgvReviews.Size = new Size(813, 286);
            dgvReviews.TabIndex = 1;
            // 
            // lblShowResName
            // 
            lblShowResName.AutoSize = true;
            lblShowResName.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblShowResName.ForeColor = Color.FromArgb(210, 105, 130);
            lblShowResName.Location = new Point(314, 18);
            lblShowResName.Name = "lblShowResName";
            lblShowResName.Size = new Size(84, 25);
            lblShowResName.TabIndex = 2;
            lblShowResName.Text = "Review";
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.LightPink;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClose.ForeColor = Color.FromArgb(90, 60, 70);
            btnClose.Location = new Point(409, 362);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(106, 36);
            btnClose.TabIndex = 3;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // ReviewForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 230, 235);
            ClientSize = new Size(900, 428);
            Controls.Add(btnClose);
            Controls.Add(lblShowResName);
            Controls.Add(dgvReviews);
            Controls.Add(lblRestaurantName);
            Font = new Font("Times New Roman", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "ReviewForm";
            Text = "ReviewForm";
            ((System.ComponentModel.ISupportInitialize)dgvReviews).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblRestaurantName;
        private DataGridView dgvReviews;
        private Label lblShowResName;
        private Button btnClose;
    }
}