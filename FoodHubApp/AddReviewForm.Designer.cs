namespace FoodHubCustomer
{
    partial class AddReviewForm
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
            textBox1 = new TextBox();
            btnSearch = new Button();
            btnEnter = new Button();
            dataGridView1 = new DataGridView();
            textBox2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            btnBack = new Button();
            Rating = new Label();
            cboRating = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(62, 80);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(390, 27);
            textBox1.TabIndex = 6;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(460, 78);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(95, 31);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnEnter
            // 
            btnEnter.Location = new Point(704, 407);
            btnEnter.Name = "btnEnter";
            btnEnter.Size = new Size(159, 41);
            btnEnter.TabIndex = 5;
            btnEnter.Text = "Enter Review";
            btnEnter.UseVisualStyleBackColor = true;
            btnEnter.Click += btnEnter_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView1.ColumnHeadersHeight = 29;
            dataGridView1.Location = new Point(62, 134);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(801, 177);
            dataGridView1.TabIndex = 4;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(62, 421);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(494, 27);
            textBox2.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(65, 37);
            label1.Name = "label1";
            label1.Size = new Size(152, 20);
            label1.TabIndex = 2;
            label1.Text = "กรุณากรอกเบอร์โทรศัพท์";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(62, 382);
            label2.Name = "label2";
            label2.Size = new Size(87, 20);
            label2.TabIndex = 1;
            label2.Text = "Review Text";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(768, 27);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(95, 45);
            btnBack.TabIndex = 0;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // Rating
            // 
            Rating.AutoSize = true;
            Rating.Location = new Point(65, 340);
            Rating.Name = "Rating";
            Rating.Size = new Size(142, 20);
            Rating.TabIndex = 8;
            Rating.Text = "ให้คะแนนร้าน (Rating)";
            // 
            // cboRating
            // 
            cboRating.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRating.FormattingEnabled = true;
            cboRating.Items.AddRange(new object[] { "5", "4", "3", "2", "1" });
            cboRating.Location = new Point(262, 340);
            cboRating.Name = "cboRating";
            cboRating.Size = new Size(111, 28);
            cboRating.TabIndex = 9;
            cboRating.SelectedIndex = 0; // เลือกบรรทัดแรกสุด (เลข 5) ไว้รอเลย
            // 
            // AddReviewForm
            // 
            ClientSize = new Size(949, 470);
            Controls.Add(cboRating);
            Controls.Add(Rating);
            Controls.Add(btnBack);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(dataGridView1);
            Controls.Add(btnEnter);
            Controls.Add(btnSearch);
            Controls.Add(textBox1);
            Name = "AddReviewForm";
            Text = "Customer Reviews";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBack;
        private Label Rating;
        private ComboBox cboRating;
        // ลบ textBox3 ที่ไม่ได้ใช้ออกไป เพื่อป้องกัน Error
    }
}