namespace FoodhubRes
{
    public partial class FormBooking : Form
    {
    
        // 📌 1. ต้องมี Constructor ตรงนี้ เพื่อให้ตอนเปิดโปรแกรมมันวาดหน้าต่างขึ้นมาได้
        public FormBooking()
        {
            InitializeComponent();
        }

        // 📌 2. นี่คือส่วน InitializeComponent (หน้าตา UI) 
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            btnBack = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            btnEnter = new Button();
            btnCheckIn = new Button();
            btnNoshow = new Button();
            btnReject = new Button();
            btnNewrequest = new Button();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Location = new Point(657, 29);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(127, 48);
            btnBack.TabIndex = 0;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(44, 74);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(357, 27);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 29);
            label1.Name = "label1";
            label1.Size = new Size(98, 20);
            label1.TabIndex = 2;
            label1.Text = "Restaurant ID";
            // 
            // btnEnter
            // 
            btnEnter.Location = new Point(433, 72);
            btnEnter.Name = "btnEnter";
            btnEnter.Size = new Size(115, 31);
            btnEnter.TabIndex = 3;
            btnEnter.Text = "Enter";
            btnEnter.UseVisualStyleBackColor = true;
            btnEnter.Click += btnEnter_Click;
            // 
            // btnCheckIn
            // 
            btnCheckIn.Location = new Point(44, 140);
            btnCheckIn.Name = "btnCheckIn";
            btnCheckIn.Size = new Size(120, 29);
            btnCheckIn.TabIndex = 4;
            btnCheckIn.Text = "Check In";
            btnCheckIn.UseVisualStyleBackColor = true;
            btnCheckIn.Click += btnCheckIn_Click;
            // 
            // btnNoshow
            // 
            btnNoshow.Location = new Point(214, 140);
            btnNoshow.Name = "btnNoshow";
            btnNoshow.Size = new Size(120, 29);
            btnNoshow.TabIndex = 5;
            btnNoshow.Text = "No Show";
            btnNoshow.UseVisualStyleBackColor = true;
            btnNoshow.Click += btnNoshow_Click;
            // 
            // btnReject
            // 
            btnReject.Location = new Point(596, 262);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(120, 29);
            btnReject.TabIndex = 6;
            btnReject.Text = "Reject";
            btnReject.UseVisualStyleBackColor = true;
            btnReject.Click += btnReject_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(44, 201);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(502, 206);
            dataGridView1.TabIndex = 7;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // btnNewrequest
            // 
            btnNewrequest.Location = new Point(433, 140);
            btnNewrequest.Name = "btnNewrequest";
            btnNewrequest.Size = new Size(120, 29);
            btnNewrequest.TabIndex = 8;
            btnNewrequest.Text = "New Request";
            btnNewrequest.UseVisualStyleBackColor = true;
            btnNewrequest.Click += btnNewrequest_Click;
            // 
            // FormBooking
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnNewrequest);
            Controls.Add(dataGridView1);
            Controls.Add(btnReject);
            Controls.Add(btnNoshow);
            Controls.Add(btnCheckIn);
            Controls.Add(btnEnter);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(btnBack);
            Name = "FormBooking";
            Text = "FormBooking";
            Load += FormBooking_Load; // 📌 ผูก Event Load ให้ทำงานตอนเปิดฟอร์ม
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btnBack;
        private TextBox textBox1;
        private Label label1;
        private Button btnEnter;
        private Button btnCheckIn;
        private Button btnNoshow;
        private Button btnReject;
        private DataGridView dataGridView1;
        private Button btnNewrequest;
        #endregion
    }
}