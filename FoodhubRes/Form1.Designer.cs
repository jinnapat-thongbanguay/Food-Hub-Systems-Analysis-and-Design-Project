namespace FoodhubRes
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
            dgvRestaurants = new DataGridView();
            btnLoad = new Button();
            btnPromoActive = new Button();
            btnCustomerCheckIn = new Button();
            btnAdmin = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvRestaurants).BeginInit();
            SuspendLayout();
            // 
            // dgvRestaurants
            // 
            dgvRestaurants.BackgroundColor = SystemColors.ControlLightLight;
            dgvRestaurants.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRestaurants.Location = new Point(310, 35);
            dgvRestaurants.Name = "dgvRestaurants";
            dgvRestaurants.RowHeadersWidth = 51;
            dgvRestaurants.Size = new Size(466, 295);
            dgvRestaurants.TabIndex = 0;
            // 
            // btnLoad
            // 
            btnLoad.Font = new Font("Book Antiqua", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLoad.Location = new Point(48, 35);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(158, 59);
            btnLoad.TabIndex = 1;
            btnLoad.Text = "restaurant";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnPromoActive
            // 
            btnPromoActive.Font = new Font("Book Antiqua", 9F);
            btnPromoActive.Location = new Point(48, 154);
            btnPromoActive.Name = "btnPromoActive";
            btnPromoActive.Size = new Size(158, 60);
            btnPromoActive.TabIndex = 2;
            btnPromoActive.Text = "Promotion";
            btnPromoActive.UseVisualStyleBackColor = true;
            btnPromoActive.Click += btnPromoActive_Click;
            // 
            // btnCustomerCheckIn
            // 
            btnCustomerCheckIn.Font = new Font("Book Antiqua", 9F);
            btnCustomerCheckIn.Location = new Point(48, 270);
            btnCustomerCheckIn.Name = "btnCustomerCheckIn";
            btnCustomerCheckIn.Size = new Size(158, 60);
            btnCustomerCheckIn.TabIndex = 3;
            btnCustomerCheckIn.Text = "Customer";
            btnCustomerCheckIn.UseVisualStyleBackColor = true;
            btnCustomerCheckIn.Click += btnCustomerCheckIn_Click;
            // 
            // btnAdmin
            // 
            btnAdmin.Font = new Font("Book Antiqua", 9F);
            btnAdmin.Location = new Point(618, 360);
            btnAdmin.Name = "btnAdmin";
            btnAdmin.Size = new Size(158, 60);
            btnAdmin.TabIndex = 4;
            btnAdmin.Text = "Admin  mode";
            btnAdmin.UseVisualStyleBackColor = true;
            btnAdmin.Click += btnAdmin_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Book Antiqua", 9F);
            button1.Location = new Point(310, 360);
            button1.Name = "button1";
            button1.Size = new Size(158, 60);
            button1.TabIndex = 5;
            button1.Text = "Booking";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnBooking_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources._1;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(btnAdmin);
            Controls.Add(btnCustomerCheckIn);
            Controls.Add(btnPromoActive);
            Controls.Add(btnLoad);
            Controls.Add(dgvRestaurants);
            Name = "Form1";
            Text = "Restaurant";
            ((System.ComponentModel.ISupportInitialize)dgvRestaurants).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvRestaurants;
        private Button btnLoad;
        private Button btnPromoActive;
        private Button btnCustomerCheckIn;
        private Button btnAdmin;
        private Button button1;
    }
}
