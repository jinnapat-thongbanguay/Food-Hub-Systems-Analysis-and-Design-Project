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
            lblRestaurantName.Location = new Point(240, 38);
            lblRestaurantName.Name = "lblRestaurantName";
            lblRestaurantName.Size = new Size(0, 20);
            lblRestaurantName.TabIndex = 0;
            // 
            // dgvReviews
            // 
            dgvReviews.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReviews.Location = new Point(204, 61);
            dgvReviews.Name = "dgvReviews";
            dgvReviews.RowHeadersWidth = 51;
            dgvReviews.Size = new Size(423, 301);
            dgvReviews.TabIndex = 1;
            // 
            // lblShowResName
            // 
            lblShowResName.AutoSize = true;
            lblShowResName.Location = new Point(204, 27);
            lblShowResName.Name = "lblShowResName";
            lblShowResName.Size = new Size(50, 20);
            lblShowResName.TabIndex = 2;
            lblShowResName.Text = "label1";
            // 
            // btnClose
            // 
            btnClose.Location = new Point(332, 381);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(169, 38);
            btnClose.TabIndex = 3;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // ReviewForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnClose);
            Controls.Add(lblShowResName);
            Controls.Add(dgvReviews);
            Controls.Add(lblRestaurantName);
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