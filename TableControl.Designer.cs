namespace TienSuToCoffee
{
    partial class TableControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableControl));
            this.btnAddTable = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnViewTable = new System.Windows.Forms.Button();
            this.btnEditTable = new System.Windows.Forms.Button();
            this.btnDeleteTable = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtIDTable = new System.Windows.Forms.TextBox();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvTableFood = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableFood)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddTable
            // 
            this.btnAddTable.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnAddTable.Location = new System.Drawing.Point(298, 79);
            this.btnAddTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddTable.Name = "btnAddTable";
            this.btnAddTable.Size = new System.Drawing.Size(133, 93);
            this.btnAddTable.TabIndex = 0;
            this.btnAddTable.Text = "ADD";
            this.btnAddTable.UseVisualStyleBackColor = true;
            this.btnAddTable.UseWaitCursor = true;
            this.btnAddTable.Click += new System.EventHandler(this.btnAddTable_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F);
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(1, 517);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 91);
            this.label1.TabIndex = 2;
            this.label1.Text = "   ";
            this.label1.UseWaitCursor = true;
            // 
            // btnViewTable
            // 
            this.btnViewTable.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnViewTable.Location = new System.Drawing.Point(437, 176);
            this.btnViewTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewTable.Name = "btnViewTable";
            this.btnViewTable.Size = new System.Drawing.Size(113, 97);
            this.btnViewTable.TabIndex = 0;
            this.btnViewTable.Text = "VIEW";
            this.btnViewTable.UseVisualStyleBackColor = true;
            this.btnViewTable.UseWaitCursor = true;
            // 
            // btnEditTable
            // 
            this.btnEditTable.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnEditTable.Location = new System.Drawing.Point(437, 80);
            this.btnEditTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEditTable.Name = "btnEditTable";
            this.btnEditTable.Size = new System.Drawing.Size(113, 92);
            this.btnEditTable.TabIndex = 0;
            this.btnEditTable.Text = "EDIT";
            this.btnEditTable.UseVisualStyleBackColor = true;
            this.btnEditTable.UseWaitCursor = true;
            this.btnEditTable.Click += new System.EventHandler(this.btnEditTable_Click);
            // 
            // btnDeleteTable
            // 
            this.btnDeleteTable.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnDeleteTable.Location = new System.Drawing.Point(298, 176);
            this.btnDeleteTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDeleteTable.Name = "btnDeleteTable";
            this.btnDeleteTable.Size = new System.Drawing.Size(133, 96);
            this.btnDeleteTable.TabIndex = 0;
            this.btnDeleteTable.Text = "DELETE";
            this.btnDeleteTable.UseVisualStyleBackColor = true;
            this.btnDeleteTable.UseWaitCursor = true;
            this.btnDeleteTable.Click += new System.EventHandler(this.btnDeleteTable_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.Color.PaleVioletRed;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Algerian", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(8, 33);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(213, 38);
            this.cmbStatus.TabIndex = 3;
            this.cmbStatus.UseWaitCursor = true;
            // 
            // txtIDTable
            // 
            this.txtIDTable.BackColor = System.Drawing.Color.GhostWhite;
            this.txtIDTable.Font = new System.Drawing.Font("Algerian", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIDTable.Location = new System.Drawing.Point(8, 200);
            this.txtIDTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtIDTable.Name = "txtIDTable";
            this.txtIDTable.Size = new System.Drawing.Size(213, 43);
            this.txtIDTable.TabIndex = 4;
            this.txtIDTable.UseWaitCursor = true;
            // 
            // txtTableName
            // 
            this.txtTableName.BackColor = System.Drawing.Color.Azure;
            this.txtTableName.Font = new System.Drawing.Font("Algerian", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTableName.Location = new System.Drawing.Point(8, 124);
            this.txtTableName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(213, 43);
            this.txtTableName.TabIndex = 4;
            this.txtTableName.UseWaitCursor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(3, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 27);
            this.label4.TabIndex = 5;
            this.label4.Text = "ID";
            this.label4.UseWaitCursor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label3.Location = new System.Drawing.Point(1, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 27);
            this.label3.TabIndex = 5;
            this.label3.Text = "Table Name";
            this.label3.UseWaitCursor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(3, -2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 27);
            this.label5.TabIndex = 5;
            this.label5.Text = "Status";
            this.label5.UseWaitCursor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MediumVioletRed;
            this.panel1.Controls.Add(this.btnAddTable);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnDeleteTable);
            this.panel1.Controls.Add(this.btnEditTable);
            this.panel1.Controls.Add(this.btnViewTable);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Location = new System.Drawing.Point(555, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(603, 633);
            this.panel1.TabIndex = 6;
            this.panel1.UseWaitCursor = true;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.HotPink;
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cmbStatus);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtIDTable);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtTableName);
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(556, 633);
            this.panel2.TabIndex = 9;
            this.panel2.UseWaitCursor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F);
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.Location = new System.Drawing.Point(313, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 91);
            this.label6.TabIndex = 6;
            this.label6.Text = "   ";
            this.label6.UseWaitCursor = true;
            // 
            // dgvTableFood
            // 
            this.dgvTableFood.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTableFood.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvTableFood.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableFood.Location = new System.Drawing.Point(272, 78);
            this.dgvTableFood.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvTableFood.Name = "dgvTableFood";
            this.dgvTableFood.RowHeadersWidth = 51;
            this.dgvTableFood.RowTemplate.Height = 24;
            this.dgvTableFood.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTableFood.Size = new System.Drawing.Size(548, 516);
            this.dgvTableFood.TabIndex = 12;
            this.dgvTableFood.UseWaitCursor = true;
            this.dgvTableFood.SelectionChanged += new System.EventHandler(this.dgvTableFood_SelectionChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 190.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Image = ((System.Drawing.Image)(resources.GetObject("label10.Image")));
            this.label10.Location = new System.Drawing.Point(180, 299);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(402, 359);
            this.label10.TabIndex = 2;
            this.label10.Text = "   ";
            this.label10.UseWaitCursor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 157.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Image = ((System.Drawing.Image)(resources.GetObject("label11.Image")));
            this.label11.Location = new System.Drawing.Point(3, 245);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(335, 297);
            this.label11.TabIndex = 13;
            this.label11.Text = "   ";
            this.label11.UseWaitCursor = true;
            // 
            // TableControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvTableFood);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "TableControl";
            this.Size = new System.Drawing.Size(1157, 633);
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.TableControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableFood)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnViewTable;
        private System.Windows.Forms.Button btnEditTable;
        private System.Windows.Forms.Button btnDeleteTable;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtIDTable;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvTableFood;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}
