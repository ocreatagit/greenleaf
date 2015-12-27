namespace Green_Leaf
{
    partial class frm_gantivariabel
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
            this.pnl_variabel_isi = new System.Windows.Forms.Panel();
            this.btn_variabel_simpan = new System.Windows.Forms.Button();
            this.btn_variabel_batal = new System.Windows.Forms.Button();
            this.txt_variabel_extra = new System.Windows.Forms.TextBox();
            this.lbl_variabel_1 = new System.Windows.Forms.Label();
            this.lbl_variabel_extra = new System.Windows.Forms.Label();
            this.lbl_variabel_2 = new System.Windows.Forms.Label();
            this.lbl_variabel_potonganhotel = new System.Windows.Forms.Label();
            this.lbl_variabel_persen = new System.Windows.Forms.Label();
            this.txt_variabel_potonganhotel = new System.Windows.Forms.TextBox();
            this.lbl_variabel_rp1 = new System.Windows.Forms.Label();
            this.pnl_variabel_isi.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_variabel_isi
            // 
            this.pnl_variabel_isi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_variabel_isi.Controls.Add(this.txt_variabel_potonganhotel);
            this.pnl_variabel_isi.Controls.Add(this.lbl_variabel_rp1);
            this.pnl_variabel_isi.Controls.Add(this.lbl_variabel_persen);
            this.pnl_variabel_isi.Controls.Add(this.lbl_variabel_2);
            this.pnl_variabel_isi.Controls.Add(this.lbl_variabel_potonganhotel);
            this.pnl_variabel_isi.Controls.Add(this.lbl_variabel_1);
            this.pnl_variabel_isi.Controls.Add(this.lbl_variabel_extra);
            this.pnl_variabel_isi.Controls.Add(this.txt_variabel_extra);
            this.pnl_variabel_isi.Controls.Add(this.btn_variabel_batal);
            this.pnl_variabel_isi.Controls.Add(this.btn_variabel_simpan);
            this.pnl_variabel_isi.Location = new System.Drawing.Point(12, 102);
            this.pnl_variabel_isi.Name = "pnl_variabel_isi";
            this.pnl_variabel_isi.Size = new System.Drawing.Size(1250, 528);
            this.pnl_variabel_isi.TabIndex = 0;
            // 
            // btn_variabel_simpan
            // 
            this.btn_variabel_simpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_variabel_simpan.Location = new System.Drawing.Point(442, 296);
            this.btn_variabel_simpan.Name = "btn_variabel_simpan";
            this.btn_variabel_simpan.Size = new System.Drawing.Size(155, 98);
            this.btn_variabel_simpan.TabIndex = 15;
            this.btn_variabel_simpan.Text = "Simpan";
            this.btn_variabel_simpan.UseVisualStyleBackColor = true;
            this.btn_variabel_simpan.Click += new System.EventHandler(this.btn_variabel_simpan_Click);
            // 
            // btn_variabel_batal
            // 
            this.btn_variabel_batal.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_variabel_batal.Location = new System.Drawing.Point(669, 296);
            this.btn_variabel_batal.Name = "btn_variabel_batal";
            this.btn_variabel_batal.Size = new System.Drawing.Size(155, 98);
            this.btn_variabel_batal.TabIndex = 16;
            this.btn_variabel_batal.Text = "Batal";
            this.btn_variabel_batal.UseVisualStyleBackColor = true;
            // 
            // txt_variabel_extra
            // 
            this.txt_variabel_extra.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_variabel_extra.Location = new System.Drawing.Point(690, 135);
            this.txt_variabel_extra.MaxLength = 3;
            this.txt_variabel_extra.Name = "txt_variabel_extra";
            this.txt_variabel_extra.Size = new System.Drawing.Size(55, 40);
            this.txt_variabel_extra.TabIndex = 1;
            this.txt_variabel_extra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_variabel_extra_KeyPress);
            // 
            // lbl_variabel_1
            // 
            this.lbl_variabel_1.AutoSize = true;
            this.lbl_variabel_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_variabel_1.Location = new System.Drawing.Point(661, 138);
            this.lbl_variabel_1.Name = "lbl_variabel_1";
            this.lbl_variabel_1.Size = new System.Drawing.Size(23, 33);
            this.lbl_variabel_1.TabIndex = 80;
            this.lbl_variabel_1.Text = ":";
            // 
            // lbl_variabel_extra
            // 
            this.lbl_variabel_extra.AutoSize = true;
            this.lbl_variabel_extra.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_variabel_extra.Location = new System.Drawing.Point(427, 138);
            this.lbl_variabel_extra.Name = "lbl_variabel_extra";
            this.lbl_variabel_extra.Size = new System.Drawing.Size(83, 33);
            this.lbl_variabel_extra.TabIndex = 79;
            this.lbl_variabel_extra.Text = "Extra";
            // 
            // lbl_variabel_2
            // 
            this.lbl_variabel_2.AutoSize = true;
            this.lbl_variabel_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_variabel_2.Location = new System.Drawing.Point(661, 195);
            this.lbl_variabel_2.Name = "lbl_variabel_2";
            this.lbl_variabel_2.Size = new System.Drawing.Size(23, 33);
            this.lbl_variabel_2.TabIndex = 82;
            this.lbl_variabel_2.Text = ":";
            // 
            // lbl_variabel_potonganhotel
            // 
            this.lbl_variabel_potonganhotel.AutoSize = true;
            this.lbl_variabel_potonganhotel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_variabel_potonganhotel.Location = new System.Drawing.Point(427, 195);
            this.lbl_variabel_potonganhotel.Name = "lbl_variabel_potonganhotel";
            this.lbl_variabel_potonganhotel.Size = new System.Drawing.Size(214, 33);
            this.lbl_variabel_potonganhotel.TabIndex = 81;
            this.lbl_variabel_potonganhotel.Text = "Potongan Hotel";
            // 
            // lbl_variabel_persen
            // 
            this.lbl_variabel_persen.AutoSize = true;
            this.lbl_variabel_persen.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_variabel_persen.Location = new System.Drawing.Point(747, 138);
            this.lbl_variabel_persen.Name = "lbl_variabel_persen";
            this.lbl_variabel_persen.Size = new System.Drawing.Size(41, 33);
            this.lbl_variabel_persen.TabIndex = 83;
            this.lbl_variabel_persen.Text = "%";
            // 
            // txt_variabel_potonganhotel
            // 
            this.txt_variabel_potonganhotel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_variabel_potonganhotel.Location = new System.Drawing.Point(743, 192);
            this.txt_variabel_potonganhotel.MaxLength = 35;
            this.txt_variabel_potonganhotel.Name = "txt_variabel_potonganhotel";
            this.txt_variabel_potonganhotel.Size = new System.Drawing.Size(116, 40);
            this.txt_variabel_potonganhotel.TabIndex = 2;
            this.txt_variabel_potonganhotel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_variabel_potonganhotel_KeyPress);
            // 
            // lbl_variabel_rp1
            // 
            this.lbl_variabel_rp1.AutoSize = true;
            this.lbl_variabel_rp1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_variabel_rp1.Location = new System.Drawing.Point(684, 195);
            this.lbl_variabel_rp1.Name = "lbl_variabel_rp1";
            this.lbl_variabel_rp1.Size = new System.Drawing.Size(60, 33);
            this.lbl_variabel_rp1.TabIndex = 86;
            this.lbl_variabel_rp1.Text = "Rp.";
            // 
            // frm_gantivariabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 642);
            this.Controls.Add(this.pnl_variabel_isi);
            this.Name = "frm_gantivariabel";
            this.Text = "frm_gantivariabel";
            this.Load += new System.EventHandler(this.frm_gantivariabel_Load);
            this.pnl_variabel_isi.ResumeLayout(false);
            this.pnl_variabel_isi.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_variabel_isi;
        private System.Windows.Forms.Button btn_variabel_batal;
        private System.Windows.Forms.Button btn_variabel_simpan;
        private System.Windows.Forms.TextBox txt_variabel_extra;
        private System.Windows.Forms.Label lbl_variabel_persen;
        private System.Windows.Forms.Label lbl_variabel_2;
        private System.Windows.Forms.Label lbl_variabel_potonganhotel;
        private System.Windows.Forms.Label lbl_variabel_1;
        private System.Windows.Forms.Label lbl_variabel_extra;
        private System.Windows.Forms.TextBox txt_variabel_potonganhotel;
        private System.Windows.Forms.Label lbl_variabel_rp1;
    }
}