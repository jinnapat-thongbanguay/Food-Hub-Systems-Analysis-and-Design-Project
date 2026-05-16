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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
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
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Times New Roman", 10.8F, FontStyle.Bold);
            textBox1.Location = new Point(62, 81);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(390, 21);
            textBox1.TabIndex = 6;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.LightPink;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.ForeColor = Color.FromArgb(90, 60, 70);
            btnSearch.Location = new Point(490, 75);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(95, 33);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnEnter
            // 
            btnEnter.BackColor = Color.FromArgb(255, 150, 170);
            btnEnter.FlatStyle = FlatStyle.Flat;
            btnEnter.ForeColor = Color.White;
            btnEnter.Location = new Point(722, 416);
            btnEnter.Name = "btnEnter";
            btnEnter.Size = new Size(141, 31);
            btnEnter.TabIndex = 5;
            btnEnter.Text = "Enter Review";
            btnEnter.UseVisualStyleBackColor = false;
            btnEnter.Click += btnEnter_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Pink;
            dataGridViewCellStyle1.Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(90, 60, 70);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeight = 29;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.FromArgb(255, 210, 220);
            dataGridView1.Location = new Point(62, 134);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(801, 177);
            dataGridView1.TabIndex = 4;
            // 
            // textBox2
            // 
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Times New Roman", 10.8F, FontStyle.Bold);
            textBox2.Location = new Point(62, 421);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(494, 21);
            textBox2.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(90, 60, 70);
            label1.Location = new Point(62, 40);
            label1.Name = "label1";
            label1.Size = new Size(257, 19);
            label1.TabIndex = 2;
            label1.Text = "Please insert your Phone Number";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(90, 60, 70);
            label2.Location = new Point(62, 382);
            label2.Name = "label2";
            label2.Size = new Size(98, 19);
            label2.TabIndex = 1;
            label2.Text = "Review Text";
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.LightPink;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.ForeColor = Color.FromArgb(90, 60, 70);
            btnBack.Location = new Point(800, 27);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(63, 32);
            btnBack.TabIndex = 0;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // Rating
            // 
            Rating.AutoSize = true;
            Rating.ForeColor = Color.FromArgb(90, 60, 70);
            Rating.Location = new Point(65, 340);
            Rating.Name = "Rating";
            Rating.Size = new Size(59, 19);
            Rating.TabIndex = 8;
            Rating.Text = "Rating";
            // 
            // cboRating
            // 
            cboRating.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRating.ForeColor = Color.FromArgb(90, 60, 70);
            cboRating.FormattingEnabled = true;
            cboRating.Items.AddRange(new object[] { "5", "4", "3", "2", "1" });
            cboRating.Location = new Point(156, 337);
            cboRating.Name = "cboRating";
            cboRating.Size = new Size(111, 27);
            cboRating.TabIndex = 9;
            // 
            // AddReviewForm
            // 
            BackColor = Color.FromArgb(255, 230, 235);
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
            Font = new Font("Times New Roman", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
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