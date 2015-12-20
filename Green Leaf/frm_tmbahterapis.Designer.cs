namespace Green_Leaf
{
    partial class frm_tmbahterapis
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
            this.panel_isi = new System.Windows.Forms.Panel();
            this.btn_batal = new System.Windows.Forms.Button();
            this.btn_tambah = new System.Windows.Forms.Button();
            this.rdo_statustdkaktif = new System.Windows.Forms.RadioButton();
            this.rdo_statusaktif = new System.Windows.Forms.RadioButton();
            this.btn_browsefoto = new System.Windows.Forms.Button();
            this.txt_namaterapis = new System.Windows.Forms.TextBox();
            this.txt_kodeterapis = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_status = new System.Windows.Forms.Label();
            this.label_upload = new System.Windows.Forms.Label();
            this.label_namaterapis = new System.Windows.Forms.Label();
            this.label_kodeterapis = new System.Windows.Forms.Label();
            this.panel_logo = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.pict_fotoKTP = new System.Windows.Forms.PictureBox();
            this.panel_isi.SuspendLayout();
            this.panel_logo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pict_fotoKTP)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_isi
            // 
            this.panel_isi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_isi.Controls.Add(this.pict_fotoKTP);
            this.panel_isi.Controls.Add(this.btn_batal);
            this.panel_isi.Controls.Add(this.btn_tambah);
            this.panel_isi.Controls.Add(this.rdo_statustdkaktif);
            this.panel_isi.Controls.Add(this.rdo_statusaktif);
            this.panel_isi.Controls.Add(this.btn_browsefoto);
            this.panel_isi.Controls.Add(this.txt_namaterapis);
            this.panel_isi.Controls.Add(this.txt_kodeterapis);
            this.panel_isi.Controls.Add(this.label5);
            this.panel_isi.Controls.Add(this.label4);
            this.panel_isi.Controls.Add(this.label3);
            this.panel_isi.Controls.Add(this.label1);
            this.panel_isi.Controls.Add(this.label_status);
            this.panel_isi.Controls.Add(this.label_upload);
            this.panel_isi.Controls.Add(this.label_namaterapis);
            this.panel_isi.Controls.Add(this.label_kodeterapis);
            this.panel_isi.Location = new System.Drawing.Point(12, 112);
            this.panel_isi.Name = "panel_isi";
            this.panel_isi.Size = new System.Drawing.Size(888, 387);
            this.panel_isi.TabIndex = 5;
            // 
            // btn_batal
            // 
            this.btn_batal.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_batal.Location = new System.Drawing.Point(466, 306);
            this.btn_batal.Name = "btn_batal";
            this.btn_batal.Size = new System.Drawing.Size(155, 67);
            this.btn_batal.TabIndex = 14;
            this.btn_batal.Text = "Batal";
            this.btn_batal.UseVisualStyleBackColor = true;
            this.btn_batal.Click += new System.EventHandler(this.btn_batal_Click);
            // 
            // btn_tambah
            // 
            this.btn_tambah.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tambah.Location = new System.Drawing.Point(266, 306);
            this.btn_tambah.Name = "btn_tambah";
            this.btn_tambah.Size = new System.Drawing.Size(155, 67);
            this.btn_tambah.TabIndex = 13;
            this.btn_tambah.Text = "Tambah";
            this.btn_tambah.UseVisualStyleBackColor = true;
            this.btn_tambah.Click += new System.EventHandler(this.btn_tambah_Click);
            // 
            // rdo_statustdkaktif
            // 
            this.rdo_statustdkaktif.AutoSize = true;
            this.rdo_statustdkaktif.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdo_statustdkaktif.Location = new System.Drawing.Point(484, 257);
            this.rdo_statustdkaktif.Name = "rdo_statustdkaktif";
            this.rdo_statustdkaktif.Size = new System.Drawing.Size(198, 43);
            this.rdo_statustdkaktif.TabIndex = 12;
            this.rdo_statustdkaktif.TabStop = true;
            this.rdo_statustdkaktif.Text = "Tidak Aktif";
            this.rdo_statustdkaktif.UseVisualStyleBackColor = true;
            // 
            // rdo_statusaktif
            // 
            this.rdo_statusaktif.AutoSize = true;
            this.rdo_statusaktif.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rdo_statusaktif.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rdo_statusaktif.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdo_statusaktif.Location = new System.Drawing.Point(332, 257);
            this.rdo_statusaktif.Name = "rdo_statusaktif";
            this.rdo_statusaktif.Size = new System.Drawing.Size(104, 43);
            this.rdo_statusaktif.TabIndex = 11;
            this.rdo_statusaktif.TabStop = true;
            this.rdo_statusaktif.Text = "Aktif";
            this.rdo_statusaktif.UseVisualStyleBackColor = true;
            // 
            // btn_browsefoto
            // 
            this.btn_browsefoto.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_browsefoto.Location = new System.Drawing.Point(332, 175);
            this.btn_browsefoto.Name = "btn_browsefoto";
            this.btn_browsefoto.Size = new System.Drawing.Size(155, 50);
            this.btn_browsefoto.TabIndex = 10;
            this.btn_browsefoto.Text = "Pilih";
            this.btn_browsefoto.UseVisualStyleBackColor = true;
            this.btn_browsefoto.Click += new System.EventHandler(this.btn_browsefoto_Click);
            // 
            // txt_namaterapis
            // 
            this.txt_namaterapis.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_namaterapis.Location = new System.Drawing.Point(332, 100);
            this.txt_namaterapis.MaxLength = 35;
            this.txt_namaterapis.Name = "txt_namaterapis";
            this.txt_namaterapis.Size = new System.Drawing.Size(530, 47);
            this.txt_namaterapis.TabIndex = 9;
            // 
            // txt_kodeterapis
            // 
            this.txt_kodeterapis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_kodeterapis.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_kodeterapis.Location = new System.Drawing.Point(332, 22);
            this.txt_kodeterapis.MaxLength = 4;
            this.txt_kodeterapis.Name = "txt_kodeterapis";
            this.txt_kodeterapis.Size = new System.Drawing.Size(92, 47);
            this.txt_kodeterapis.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(299, 259);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 39);
            this.label5.TabIndex = 7;
            this.label5.Text = ":";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(299, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 39);
            this.label4.TabIndex = 6;
            this.label4.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(299, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 39);
            this.label3.TabIndex = 5;
            this.label3.Text = ":";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(299, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 39);
            this.label1.TabIndex = 4;
            this.label1.Text = ":";
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_status.Location = new System.Drawing.Point(22, 259);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(116, 39);
            this.label_status.TabIndex = 3;
            this.label_status.Text = "Status";
            // 
            // label_upload
            // 
            this.label_upload.AutoSize = true;
            this.label_upload.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_upload.Location = new System.Drawing.Point(22, 181);
            this.label_upload.Name = "label_upload";
            this.label_upload.Size = new System.Drawing.Size(280, 39);
            this.label_upload.TabIndex = 2;
            this.label_upload.Text = "Upload Foto KTP";
            // 
            // label_namaterapis
            // 
            this.label_namaterapis.AutoSize = true;
            this.label_namaterapis.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_namaterapis.Location = new System.Drawing.Point(22, 103);
            this.label_namaterapis.Name = "label_namaterapis";
            this.label_namaterapis.Size = new System.Drawing.Size(235, 39);
            this.label_namaterapis.TabIndex = 1;
            this.label_namaterapis.Text = "Nama Terapis";
            // 
            // label_kodeterapis
            // 
            this.label_kodeterapis.AutoSize = true;
            this.label_kodeterapis.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_kodeterapis.Location = new System.Drawing.Point(22, 25);
            this.label_kodeterapis.Name = "label_kodeterapis";
            this.label_kodeterapis.Size = new System.Drawing.Size(222, 39);
            this.label_kodeterapis.TabIndex = 0;
            this.label_kodeterapis.Text = "Kode Terapis";
            // 
            // panel_logo
            // 
            this.panel_logo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_logo.Controls.Add(this.label6);
            this.panel_logo.Location = new System.Drawing.Point(12, 12);
            this.panel_logo.Name = "panel_logo";
            this.panel_logo.Size = new System.Drawing.Size(888, 101);
            this.panel_logo.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(397, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 39);
            this.label6.TabIndex = 8;
            this.label6.Text = "Logo";
            // 
            // pict_fotoKTP
            // 
            this.pict_fotoKTP.Location = new System.Drawing.Point(688, 167);
            this.pict_fotoKTP.Name = "pict_fotoKTP";
            this.pict_fotoKTP.Size = new System.Drawing.Size(184, 206);
            this.pict_fotoKTP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_fotoKTP.TabIndex = 15;
            this.pict_fotoKTP.TabStop = false;
            // 
            // frm_tmbahterapis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(915, 509);
            this.ControlBox = false;
            this.Controls.Add(this.panel_isi);
            this.Controls.Add(this.panel_logo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_tmbahterapis";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form Tambah Terapis";
            this.Load += new System.EventHandler(this.frm_tmbahterapis_Load);
            this.panel_isi.ResumeLayout(false);
            this.panel_isi.PerformLayout();
            this.panel_logo.ResumeLayout(false);
            this.panel_logo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pict_fotoKTP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_isi;
        private System.Windows.Forms.Button btn_batal;
        private System.Windows.Forms.Button btn_tambah;
        private System.Windows.Forms.RadioButton rdo_statustdkaktif;
        private System.Windows.Forms.RadioButton rdo_statusaktif;
        private System.Windows.Forms.Button btn_browsefoto;
        private System.Windows.Forms.TextBox txt_namaterapis;
        private System.Windows.Forms.TextBox txt_kodeterapis;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Label label_upload;
        private System.Windows.Forms.Label label_namaterapis;
        private System.Windows.Forms.Label label_kodeterapis;
        private System.Windows.Forms.Panel panel_logo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pict_fotoKTP;
    }
}