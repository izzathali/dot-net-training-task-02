namespace ABC_Drive.UserControlls
{
    partial class longhire
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
            this.btnAddLongTour = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvLongTour = new System.Windows.Forms.DataGridView();
            this.btnEditLongTour = new System.Windows.Forms.Button();
            this.btnDeleteLongTour = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLongTour)).BeginInit();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(716, 473);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.Controls.Add(this.btnDeleteLongTour);
            this.panel1.Controls.Add(this.btnEditLongTour);
            this.panel1.Controls.Add(this.btnAddLongTour);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(710, 39);
            this.panel1.TabIndex = 0;
            // 
            // btnAddLongTour
            // 
            this.btnAddLongTour.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddLongTour.Location = new System.Drawing.Point(6, 4);
            this.btnAddLongTour.Name = "btnAddLongTour";
            this.btnAddLongTour.Size = new System.Drawing.Size(126, 31);
            this.btnAddLongTour.TabIndex = 0;
            this.btnAddLongTour.Text = "Add Long Tour";
            this.btnAddLongTour.UseVisualStyleBackColor = true;
            this.btnAddLongTour.Click += new System.EventHandler(this.btnAddLongTour_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvLongTour);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(710, 422);
            this.panel2.TabIndex = 1;
            // 
            // dgvLongTour
            // 
            this.dgvLongTour.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLongTour.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLongTour.Location = new System.Drawing.Point(0, 0);
            this.dgvLongTour.Name = "dgvLongTour";
            this.dgvLongTour.Size = new System.Drawing.Size(710, 422);
            this.dgvLongTour.TabIndex = 0;
            // 
            // btnEditLongTour
            // 
            this.btnEditLongTour.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditLongTour.Location = new System.Drawing.Point(138, 4);
            this.btnEditLongTour.Name = "btnEditLongTour";
            this.btnEditLongTour.Size = new System.Drawing.Size(126, 31);
            this.btnEditLongTour.TabIndex = 1;
            this.btnEditLongTour.Text = "Edit Long Tour";
            this.btnEditLongTour.UseVisualStyleBackColor = true;
            this.btnEditLongTour.Click += new System.EventHandler(this.btnEditLongTour_Click);
            // 
            // btnDeleteLongTour
            // 
            this.btnDeleteLongTour.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteLongTour.Location = new System.Drawing.Point(270, 4);
            this.btnDeleteLongTour.Name = "btnDeleteLongTour";
            this.btnDeleteLongTour.Size = new System.Drawing.Size(144, 31);
            this.btnDeleteLongTour.TabIndex = 2;
            this.btnDeleteLongTour.Text = "Delete Long Tour";
            this.btnDeleteLongTour.UseVisualStyleBackColor = true;
            this.btnDeleteLongTour.Click += new System.EventHandler(this.btnDeleteLongTour_Click);
            // 
            // longhire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "longhire";
            this.Size = new System.Drawing.Size(716, 473);
            this.Load += new System.EventHandler(this.longhire_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLongTour)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddLongTour;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvLongTour;
        private System.Windows.Forms.Button btnDeleteLongTour;
        private System.Windows.Forms.Button btnEditLongTour;
    }
}
