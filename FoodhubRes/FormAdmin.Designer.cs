namespace FoodhubRes
{
    partial class FormAdmin
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
            dataGridView1 = new DataGridView();
            btnSearch = new TextBox();
            btnEnter = new Button();
            btnBack = new Button();
            label1 = new Label();
            txtPromoId = new TextBox();
            txtPromoName = new TextBox();
            txtDiscount = new TextBox();
            txtPromoStatus = new ComboBox();
            btnUpdatePromo = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnDelete = new Button();
            txtResId = new TextBox();
            label6 = new Label();
            btnInsert = new Button();
            txtDescription = new TextBox();
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(34, 80);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(785, 117);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(118, 44);
            btnSearch.Margin = new Padding(3, 2, 3, 2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(442, 23);
            btnSearch.TabIndex = 1;
            btnSearch.TextChanged += btnSearch_TextChanged;
            // 
            // btnEnter
            // 
            btnEnter.Font = new Font("Microsoft Sans Serif", 9F);
            btnEnter.Location = new Point(566, 44);
            btnEnter.Margin = new Padding(3, 2, 3, 2);
            btnEnter.Name = "btnEnter";
            btnEnter.Size = new Size(80, 23);
            btnEnter.TabIndex = 3;
            btnEnter.Text = "Enter";
            btnEnter.UseVisualStyleBackColor = true;
            btnEnter.TextChanged += FormAdmin_Load;
            btnEnter.Click += button1_Click;
            // 
            // btnBack
            // 
            btnBack.Font = new Font("Microsoft Sans Serif", 9F);
            btnBack.Location = new Point(739, 11);
            btnBack.Margin = new Padding(3, 2, 3, 2);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(80, 34);
            btnBack.TabIndex = 4;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9F);
            label1.Location = new Point(35, 20);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 5;
            label1.Text = "Promotion";
            // 
            // txtPromoId
            // 
            txtPromoId.Location = new Point(170, 219);
            txtPromoId.Margin = new Padding(3, 2, 3, 2);
            txtPromoId.Name = "txtPromoId";
            txtPromoId.Size = new Size(153, 23);
            txtPromoId.TabIndex = 6;
            // 
            // txtPromoName
            // 
            txtPromoName.Location = new Point(170, 251);
            txtPromoName.Margin = new Padding(3, 2, 3, 2);
            txtPromoName.Name = "txtPromoName";
            txtPromoName.Size = new Size(153, 23);
            txtPromoName.TabIndex = 7;
            // 
            // txtDiscount
            // 
            txtDiscount.Location = new Point(170, 282);
            txtDiscount.Margin = new Padding(3, 2, 3, 2);
            txtDiscount.Name = "txtDiscount";
            txtDiscount.Size = new Size(153, 23);
            txtDiscount.TabIndex = 8;
            // 
            // txtPromoStatus
            // 
            txtPromoStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            txtPromoStatus.Items.AddRange(new object[] { "Active", "Paused", "Expired" });
            txtPromoStatus.Location = new Point(170, 318);
            txtPromoStatus.Name = "txtPromoStatus";
            txtPromoStatus.Size = new Size(153, 23);
            txtPromoStatus.TabIndex = 0;
            // 
            // btnUpdatePromo
            // 
            btnUpdatePromo.Font = new Font("Microsoft Sans Serif", 9F);
            btnUpdatePromo.Location = new Point(566, 425);
            btnUpdatePromo.Margin = new Padding(3, 2, 3, 2);
            btnUpdatePromo.Name = "btnUpdatePromo";
            btnUpdatePromo.Size = new Size(90, 39);
            btnUpdatePromo.TabIndex = 10;
            btnUpdatePromo.Text = "Save";
            btnUpdatePromo.UseVisualStyleBackColor = true;
            btnUpdatePromo.Click += btnUpdate_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9F);
            label2.Location = new Point(34, 219);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 11;
            label2.Text = "Promotion ID";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9F);
            label3.Location = new Point(34, 253);
            label3.Name = "label3";
            label3.Size = new Size(101, 15);
            label3.TabIndex = 12;
            label3.Text = "Promotion Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 9F);
            label4.Location = new Point(34, 288);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 13;
            label4.Text = "Discount";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 9F);
            label5.Location = new Point(34, 320);
            label5.Name = "label5";
            label5.Size = new Size(101, 15);
            label5.TabIndex = 14;
            label5.Text = "Promotion Status";
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Microsoft Sans Serif", 9F);
            btnDelete.Location = new Point(465, 425);
            btnDelete.Margin = new Padding(3, 2, 3, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(95, 39);
            btnDelete.TabIndex = 15;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // txtResId
            // 
            txtResId.Location = new Point(170, 351);
            txtResId.Name = "txtResId";
            txtResId.Size = new Size(153, 23);
            txtResId.TabIndex = 16;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 9F);
            label6.Location = new Point(34, 353);
            label6.Name = "label6";
            label6.Size = new Size(82, 15);
            label6.TabIndex = 17;
            label6.Text = "Restaurant ID";
            // 
            // btnInsert
            // 
            btnInsert.Font = new Font("Microsoft Sans Serif", 9F);
            btnInsert.Location = new Point(662, 425);
            btnInsert.Name = "btnInsert";
            btnInsert.Size = new Size(101, 39);
            btnInsert.TabIndex = 18;
            btnInsert.Text = "Add New";
            btnInsert.UseVisualStyleBackColor = true;
            btnInsert.Click += btnInsert_Click;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(170, 381);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(153, 23);
            txtDescription.TabIndex = 19;
            // 
            // dtpStart
            // 
            dtpStart.Format = DateTimePickerFormat.Short;
            dtpStart.Location = new Point(170, 411);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(153, 23);
            dtpStart.TabIndex = 20;
            // 
            // dtpEnd
            // 
            dtpEnd.Format = DateTimePickerFormat.Short;
            dtpEnd.Location = new Point(170, 441);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(153, 23);
            dtpEnd.TabIndex = 21;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(34, 384);
            label7.Name = "label7";
            label7.Size = new Size(67, 15);
            label7.TabIndex = 22;
            label7.Text = "Description";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(34, 414);
            label8.Name = "label8";
            label8.Size = new Size(58, 15);
            label8.TabIndex = 23;
            label8.Text = "Start Date";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(34, 444);
            label9.Name = "label9";
            label9.Size = new Size(54, 15);
            label9.TabIndex = 24;
            label9.Text = "End Date";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(35, 47);
            label10.Name = "label10";
            label10.Size = new Size(77, 15);
            label10.TabIndex = 25;
            label10.Text = "Restaurant ID";
            label10.Click += label10_Click;
            // 
            // FormAdmin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources._1;
            ClientSize = new Size(831, 480);
            Controls.Add(label10);
            Controls.Add(btnDelete);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnUpdatePromo);
            Controls.Add(txtPromoStatus);
            Controls.Add(txtDiscount);
            Controls.Add(txtPromoName);
            Controls.Add(txtPromoId);
            Controls.Add(label1);
            Controls.Add(btnBack);
            Controls.Add(btnEnter);
            Controls.Add(btnSearch);
            Controls.Add(dataGridView1);
            Controls.Add(txtResId);
            Controls.Add(label6);
            Controls.Add(btnInsert);
            Controls.Add(txtDescription);
            Controls.Add(dtpStart);
            Controls.Add(dtpEnd);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(label9);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormAdmin";
            Text = "FormAdmin";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox btnSearch;
        private Button btnEnter;
        private Button btnBack;
        private Label label1;
        private TextBox txtPromoId;
        private TextBox txtPromoName;
        private TextBox txtDiscount;
        private Button btnUpdatePromo;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnDelete;
        private TextBox txtResId;
        private Label label6;
        private Button btnInsert;
        private TextBox txtDescription;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private System.Windows.Forms.ComboBox txtPromoStatus;
    }
}