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
            txtPromoStatus = new TextBox();
            btnUpdatePromo = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(40, 117);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(585, 111);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(40, 71);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(287, 27);
            btnSearch.TabIndex = 1;
            // 
            // btnEnter
            // 
            btnEnter.Font = new Font("Book Antiqua", 9F);
            btnEnter.Location = new Point(358, 71);
            btnEnter.Name = "btnEnter";
            btnEnter.Size = new Size(86, 26);
            btnEnter.TabIndex = 3;
            btnEnter.Text = "Enter";
            btnEnter.UseVisualStyleBackColor = true;
            btnEnter.TextChanged += FormAdmin_Load;
            btnEnter.Click += button1_Click;
            // 
            // btnBack
            // 
            btnBack.Font = new Font("Book Antiqua", 9F);
            btnBack.Location = new Point(697, 15);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(92, 45);
            btnBack.TabIndex = 4;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Book Antiqua", 9F);
            label1.Location = new Point(40, 27);
            label1.Name = "label1";
            label1.Size = new Size(79, 20);
            label1.TabIndex = 5;
            label1.Text = "Promotion";
            // 
            // txtPromoId
            // 
            txtPromoId.Location = new Point(220, 257);
            txtPromoId.Name = "txtPromoId";
            txtPromoId.Size = new Size(174, 27);
            txtPromoId.TabIndex = 6;
            // 
            // txtPromoName
            // 
            txtPromoName.Location = new Point(220, 300);
            txtPromoName.Name = "txtPromoName";
            txtPromoName.Size = new Size(174, 27);
            txtPromoName.TabIndex = 7;
            // 
            // txtDiscount
            // 
            txtDiscount.Location = new Point(220, 342);
            txtDiscount.Name = "txtDiscount";
            txtDiscount.Size = new Size(174, 27);
            txtDiscount.TabIndex = 8;
            // 
            // txtPromoStatus
            // 
            txtPromoStatus.Location = new Point(220, 389);
            txtPromoStatus.Name = "txtPromoStatus";
            txtPromoStatus.Size = new Size(174, 27);
            txtPromoStatus.TabIndex = 9;
            // 
            // btnUpdatePromo
            // 
            btnUpdatePromo.Font = new Font("Book Antiqua", 9F);
            btnUpdatePromo.Location = new Point(554, 389);
            btnUpdatePromo.Name = "btnUpdatePromo";
            btnUpdatePromo.Size = new Size(86, 26);
            btnUpdatePromo.TabIndex = 10;
            btnUpdatePromo.Text = "Save";
            btnUpdatePromo.UseVisualStyleBackColor = true;
            btnUpdatePromo.Click += btnUpdate_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Book Antiqua", 9F);
            label2.Location = new Point(64, 257);
            label2.Name = "label2";
            label2.Size = new Size(100, 20);
            label2.TabIndex = 11;
            label2.Text = "Promotion ID";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Book Antiqua", 9F);
            label3.Location = new Point(64, 303);
            label3.Name = "label3";
            label3.Size = new Size(123, 20);
            label3.TabIndex = 12;
            label3.Text = "Promotion Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Book Antiqua", 9F);
            label4.Location = new Point(64, 349);
            label4.Name = "label4";
            label4.Size = new Size(69, 20);
            label4.TabIndex = 13;
            label4.Text = "Discount";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Book Antiqua", 9F);
            label5.Location = new Point(64, 392);
            label5.Name = "label5";
            label5.Size = new Size(124, 20);
            label5.TabIndex = 14;
            label5.Text = "Promotion Status";
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Book Antiqua", 9F);
            btnDelete.Location = new Point(660, 202);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(86, 26);
            btnDelete.TabIndex = 15;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // FormAdmin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources._1;
            ClientSize = new Size(800, 450);
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
        private TextBox txtPromoStatus;
        private Button btnUpdatePromo;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnDelete;
    }
}