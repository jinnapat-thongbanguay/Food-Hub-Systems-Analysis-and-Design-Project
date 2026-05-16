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
            btnManagePromotion = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvRestaurants).BeginInit();
            SuspendLayout();
            // 
            // dgvRestaurants
            // 
            dgvRestaurants.BackgroundColor = SystemColors.ControlLightLight;
            dgvRestaurants.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRestaurants.Location = new Point(174, 26);
            dgvRestaurants.Margin = new Padding(3, 2, 3, 2);
            dgvRestaurants.Name = "dgvRestaurants";
            dgvRestaurants.RowHeadersWidth = 51;
            dgvRestaurants.Size = new Size(505, 221);
            dgvRestaurants.TabIndex = 0;
            // 
            // btnLoad
            // 
            btnLoad.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLoad.Location = new Point(12, 26);
            btnLoad.Margin = new Padding(3, 2, 3, 2);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(138, 44);
            btnLoad.TabIndex = 1;
            btnLoad.Text = "restaurant";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnPromoActive
            // 
            btnPromoActive.Font = new Font("Microsoft Sans Serif", 9F);
            btnPromoActive.Location = new Point(12, 113);
            btnPromoActive.Margin = new Padding(3, 2, 3, 2);
            btnPromoActive.Name = "btnPromoActive";
            btnPromoActive.Size = new Size(138, 45);
            btnPromoActive.TabIndex = 2;
            btnPromoActive.Text = "Promotion";
            btnPromoActive.UseVisualStyleBackColor = true;
            btnPromoActive.Click += btnPromoActive_Click;
            // 
            // btnManagePromotion
            // 
            btnManagePromotion.Font = new Font("Microsoft Sans Serif", 9F);
            btnManagePromotion.Location = new Point(550, 270);
            btnManagePromotion.Margin = new Padding(3, 2, 3, 2);
            btnManagePromotion.Name = "btnManagePromotion";
            btnManagePromotion.Size = new Size(138, 45);
            btnManagePromotion.TabIndex = 4;
            btnManagePromotion.Text = "Manage Promotion";
            btnManagePromotion.UseVisualStyleBackColor = true;
            btnManagePromotion.Click += btnManagePromotion_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft Sans Serif", 9F);
            button1.Location = new Point(174, 270);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(138, 45);
            button1.TabIndex = 5;
            button1.Text = "Booking";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnBooking_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources._1;
            ClientSize = new Size(700, 338);
            Controls.Add(button1);
            Controls.Add(btnManagePromotion);
            Controls.Add(btnPromoActive);
            Controls.Add(btnLoad);
            Controls.Add(dgvRestaurants);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Restaurant";
            ((System.ComponentModel.ISupportInitialize)dgvRestaurants).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvRestaurants;
        private Button btnLoad;
        private Button btnPromoActive;
        private Button btnManagePromotion;
        private Button button1;
    }
}
