namespace ABC_Drive.Dashboard
{
    partial class frmDashboard
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnLongTour = new System.Windows.Forms.Button();
            this.btnDayTour = new System.Windows.Forms.Button();
            this.btnRent = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.longhire1 = new ABC_Drive.UserControlls.longhire();
            this.daytour1 = new ABC_Drive.UserControlls.daytour();
            this.rent1 = new ABC_Drive.UserControlls.Rent();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(993, 608);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnLongTour, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnDayTour, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnRent, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnHome, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 45);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(194, 270);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnLongTour
            // 
            this.btnLongTour.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLongTour.Location = new System.Drawing.Point(3, 192);
            this.btnLongTour.Name = "btnLongTour";
            this.btnLongTour.Size = new System.Drawing.Size(186, 48);
            this.btnLongTour.TabIndex = 3;
            this.btnLongTour.Text = "Long Tour";
            this.btnLongTour.UseVisualStyleBackColor = true;
            this.btnLongTour.Click += new System.EventHandler(this.btnLongTour_Click);
            // 
            // btnDayTour
            // 
            this.btnDayTour.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDayTour.Location = new System.Drawing.Point(3, 138);
            this.btnDayTour.Name = "btnDayTour";
            this.btnDayTour.Size = new System.Drawing.Size(186, 48);
            this.btnDayTour.TabIndex = 2;
            this.btnDayTour.Text = "Day Tour";
            this.btnDayTour.UseVisualStyleBackColor = true;
            this.btnDayTour.Click += new System.EventHandler(this.btnDayTour_Click);
            // 
            // btnRent
            // 
            this.btnRent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRent.Location = new System.Drawing.Point(3, 84);
            this.btnRent.Name = "btnRent";
            this.btnRent.Size = new System.Drawing.Size(186, 48);
            this.btnRent.TabIndex = 1;
            this.btnRent.Text = "Rent";
            this.btnRent.UseVisualStyleBackColor = true;
            this.btnRent.Click += new System.EventHandler(this.btnRent_Click);
            // 
            // btnHome
            // 
            this.btnHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHome.Location = new System.Drawing.Point(3, 30);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(186, 48);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.longhire1);
            this.panel1.Controls.Add(this.daytour1);
            this.panel1.Controls.Add(this.rent1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(203, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 547);
            this.panel1.TabIndex = 1;
            // 
            // longhire1
            // 
            this.longhire1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.longhire1.Location = new System.Drawing.Point(0, 0);
            this.longhire1.Name = "longhire1";
            this.longhire1.Size = new System.Drawing.Size(787, 547);
            this.longhire1.TabIndex = 2;
            this.longhire1.Visible = false;
            // 
            // daytour1
            // 
            this.daytour1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.daytour1.Location = new System.Drawing.Point(0, 0);
            this.daytour1.Name = "daytour1";
            this.daytour1.Size = new System.Drawing.Size(787, 547);
            this.daytour1.TabIndex = 1;
            this.daytour1.Visible = false;
            // 
            // rent1
            // 
            this.rent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rent1.Location = new System.Drawing.Point(0, 0);
            this.rent1.Name = "rent1";
            this.rent1.Size = new System.Drawing.Size(787, 547);
            this.rent1.TabIndex = 0;
            this.rent1.Visible = false;
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 608);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.frmDashboard_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnRent;
        private System.Windows.Forms.Button btnDayTour;
        private System.Windows.Forms.Button btnLongTour;
        private System.Windows.Forms.Panel panel1;
        private UserControlls.Rent rent1;
        private UserControlls.daytour daytour1;
        private UserControlls.longhire longhire1;
    }
}