namespace ABC_Drive.UserControlls
{
    partial class daytour
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeleteDayTour = new System.Windows.Forms.Button();
            this.btnEditDayTour = new System.Windows.Forms.Button();
            this.btnAddDayTour = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvDayTour = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDayTour)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(739, 431);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.Controls.Add(this.btnDeleteDayTour);
            this.panel1.Controls.Add(this.btnEditDayTour);
            this.panel1.Controls.Add(this.btnAddDayTour);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 39);
            this.panel1.TabIndex = 0;
            // 
            // btnDeleteDayTour
            // 
            this.btnDeleteDayTour.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteDayTour.Location = new System.Drawing.Point(268, 4);
            this.btnDeleteDayTour.Name = "btnDeleteDayTour";
            this.btnDeleteDayTour.Size = new System.Drawing.Size(135, 31);
            this.btnDeleteDayTour.TabIndex = 2;
            this.btnDeleteDayTour.Text = "Delete Day Tour";
            this.btnDeleteDayTour.UseVisualStyleBackColor = true;
            this.btnDeleteDayTour.Click += new System.EventHandler(this.btnDeleteDayTour_Click);
            // 
            // btnEditDayTour
            // 
            this.btnEditDayTour.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditDayTour.Location = new System.Drawing.Point(136, 4);
            this.btnEditDayTour.Name = "btnEditDayTour";
            this.btnEditDayTour.Size = new System.Drawing.Size(126, 31);
            this.btnEditDayTour.TabIndex = 1;
            this.btnEditDayTour.Text = "Edit Day Tour";
            this.btnEditDayTour.UseVisualStyleBackColor = true;
            this.btnEditDayTour.Click += new System.EventHandler(this.btnEditDayTour_Click);
            // 
            // btnAddDayTour
            // 
            this.btnAddDayTour.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDayTour.Location = new System.Drawing.Point(6, 4);
            this.btnAddDayTour.Name = "btnAddDayTour";
            this.btnAddDayTour.Size = new System.Drawing.Size(126, 31);
            this.btnAddDayTour.TabIndex = 0;
            this.btnAddDayTour.Text = "Add Day Tour";
            this.btnAddDayTour.UseVisualStyleBackColor = true;
            this.btnAddDayTour.Click += new System.EventHandler(this.btnAddDayTour_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvDayTour);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(733, 380);
            this.panel2.TabIndex = 1;
            // 
            // dgvDayTour
            // 
            this.dgvDayTour.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDayTour.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDayTour.Location = new System.Drawing.Point(0, 0);
            this.dgvDayTour.Name = "dgvDayTour";
            this.dgvDayTour.Size = new System.Drawing.Size(733, 380);
            this.dgvDayTour.TabIndex = 0;
            // 
            // daytour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "daytour";
            this.Size = new System.Drawing.Size(739, 431);
            this.Load += new System.EventHandler(this.daytour_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDayTour)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddDayTour;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvDayTour;
        private System.Windows.Forms.Button btnEditDayTour;
        private System.Windows.Forms.Button btnDeleteDayTour;
    }
}
