namespace Green_Leaf
{
    partial class frm_laporanlayanan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl_lprnlayanan_isi = new System.Windows.Forms.Panel();
            this.dgv_lprnlayanan_tabellayanan = new System.Windows.Forms.DataGridView();
            this.btn_lprnlayanan_batal = new System.Windows.Forms.Button();
            this.btn_lprnlayanan_excel = new System.Windows.Forms.Button();
            this.pnl_lprnlayanan_isi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_lprnlayanan_tabellayanan)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_lprnlayanan_isi
            // 
            this.pnl_lprnlayanan_isi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_lprnlayanan_isi.Controls.Add(this.btn_lprnlayanan_excel);
            this.pnl_lprnlayanan_isi.Controls.Add(this.dgv_lprnlayanan_tabellayanan);
            this.pnl_lprnlayanan_isi.Controls.Add(this.btn_lprnlayanan_batal);
            this.pnl_lprnlayanan_isi.Location = new System.Drawing.Point(7, 57);
            this.pnl_lprnlayanan_isi.Name = "pnl_lprnlayanan_isi";
            this.pnl_lprnlayanan_isi.Size = new System.Drawing.Size(1250, 528);
            this.pnl_lprnlayanan_isi.TabIndex = 50;
            // 
            // dgv_lprnlayanan_tabellayanan
            // 
            this.dgv_lprnlayanan_tabellayanan.AllowUserToAddRows = false;
            this.dgv_lprnlayanan_tabellayanan.AllowUserToDeleteRows = false;
            this.dgv_lprnlayanan_tabellayanan.AllowUserToResizeColumns = false;
            this.dgv_lprnlayanan_tabellayanan.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_lprnlayanan_tabellayanan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_lprnlayanan_tabellayanan.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_lprnlayanan_tabellayanan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_lprnlayanan_tabellayanan.ColumnHeadersHeight = 200;
            this.dgv_lprnlayanan_tabellayanan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_lprnlayanan_tabellayanan.Location = new System.Drawing.Point(13, 17);
            this.dgv_lprnlayanan_tabellayanan.Name = "dgv_lprnlayanan_tabellayanan";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_lprnlayanan_tabellayanan.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_lprnlayanan_tabellayanan.RowHeadersVisible = false;
            this.dgv_lprnlayanan_tabellayanan.RowHeadersWidth = 20;
            this.dgv_lprnlayanan_tabellayanan.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_lprnlayanan_tabellayanan.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_lprnlayanan_tabellayanan.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_lprnlayanan_tabellayanan.RowTemplate.DefaultCellStyle.NullValue = null;
            this.dgv_lprnlayanan_tabellayanan.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_lprnlayanan_tabellayanan.RowTemplate.Height = 20;
            this.dgv_lprnlayanan_tabellayanan.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_lprnlayanan_tabellayanan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_lprnlayanan_tabellayanan.Size = new System.Drawing.Size(1221, 432);
            this.dgv_lprnlayanan_tabellayanan.TabIndex = 120;
            // 
            // btn_lprnlayanan_batal
            // 
            this.btn_lprnlayanan_batal.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_lprnlayanan_batal.Location = new System.Drawing.Point(1079, 455);
            this.btn_lprnlayanan_batal.Name = "btn_lprnlayanan_batal";
            this.btn_lprnlayanan_batal.Size = new System.Drawing.Size(155, 60);
            this.btn_lprnlayanan_batal.TabIndex = 15;
            this.btn_lprnlayanan_batal.Text = "Batal";
            this.btn_lprnlayanan_batal.UseVisualStyleBackColor = true;
            // 
            // btn_lprnlayanan_excel
            // 
            this.btn_lprnlayanan_excel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_lprnlayanan_excel.Location = new System.Drawing.Point(13, 455);
            this.btn_lprnlayanan_excel.Name = "btn_lprnlayanan_excel";
            this.btn_lprnlayanan_excel.Size = new System.Drawing.Size(250, 60);
            this.btn_lprnlayanan_excel.TabIndex = 121;
            this.btn_lprnlayanan_excel.Text = "Convert Excel";
            this.btn_lprnlayanan_excel.UseVisualStyleBackColor = true;
            this.btn_lprnlayanan_excel.Click += new System.EventHandler(this.btn_lprnlayanan_excel_Click);
            // 
            // frm_laporanlayanan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 642);
            this.Controls.Add(this.pnl_lprnlayanan_isi);
            this.Name = "frm_laporanlayanan";
            this.Text = "frm_laporanlayanan";
            this.Load += new System.EventHandler(this.frm_laporanlayanan_Load);
            this.pnl_lprnlayanan_isi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_lprnlayanan_tabellayanan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_lprnlayanan_isi;
        private System.Windows.Forms.DataGridView dgv_lprnlayanan_tabellayanan;
        private System.Windows.Forms.Button btn_lprnlayanan_batal;
        private System.Windows.Forms.Button btn_lprnlayanan_excel;
    }
}