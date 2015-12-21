namespace Green_Leaf
{
    partial class frm_userlogin
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
            this.btn_masuk = new System.Windows.Forms.Button();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_namaterapis = new System.Windows.Forms.Label();
            this.label_kodeterapis = new System.Windows.Forms.Label();
            this.panel_logo = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_isi.SuspendLayout();
            this.panel_logo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_isi
            // 
            this.panel_isi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_isi.Controls.Add(this.label2);
            this.panel_isi.Controls.Add(this.btn_batal);
            this.panel_isi.Controls.Add(this.btn_masuk);
            this.panel_isi.Controls.Add(this.txt_pass);
            this.panel_isi.Controls.Add(this.txt_username);
            this.panel_isi.Controls.Add(this.label3);
            this.panel_isi.Controls.Add(this.label1);
            this.panel_isi.Controls.Add(this.label_namaterapis);
            this.panel_isi.Controls.Add(this.label_kodeterapis);
            this.panel_isi.Location = new System.Drawing.Point(12, 101);
            this.panel_isi.Name = "panel_isi";
            this.panel_isi.Size = new System.Drawing.Size(772, 407);
            this.panel_isi.TabIndex = 7;
            // 
            // btn_batal
            // 
            this.btn_batal.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_batal.Location = new System.Drawing.Point(399, 251);
            this.btn_batal.Name = "btn_batal";
            this.btn_batal.Size = new System.Drawing.Size(155, 67);
            this.btn_batal.TabIndex = 14;
            this.btn_batal.Text = "Batal";
            this.btn_batal.UseVisualStyleBackColor = true;
            this.btn_batal.Click += new System.EventHandler(this.btn_batal_Click);
            // 
            // btn_masuk
            // 
            this.btn_masuk.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_masuk.Location = new System.Drawing.Point(217, 251);
            this.btn_masuk.Name = "btn_masuk";
            this.btn_masuk.Size = new System.Drawing.Size(155, 67);
            this.btn_masuk.TabIndex = 13;
            this.btn_masuk.Text = "Masuk";
            this.btn_masuk.UseVisualStyleBackColor = true;
            this.btn_masuk.Click += new System.EventHandler(this.btn_masuk_Click);
            // 
            // txt_pass
            // 
            this.txt_pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pass.Location = new System.Drawing.Point(330, 178);
            this.txt_pass.MaxLength = 30;
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.PasswordChar = '*';
            this.txt_pass.Size = new System.Drawing.Size(337, 47);
            this.txt_pass.TabIndex = 9;
            // 
            // txt_username
            // 
            this.txt_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_username.Location = new System.Drawing.Point(330, 100);
            this.txt_username.MaxLength = 30;
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(337, 47);
            this.txt_username.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(297, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 39);
            this.label3.TabIndex = 5;
            this.label3.Text = ":";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(297, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 39);
            this.label1.TabIndex = 4;
            this.label1.Text = ":";
            // 
            // label_namaterapis
            // 
            this.label_namaterapis.AutoSize = true;
            this.label_namaterapis.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_namaterapis.Location = new System.Drawing.Point(103, 181);
            this.label_namaterapis.Name = "label_namaterapis";
            this.label_namaterapis.Size = new System.Drawing.Size(170, 39);
            this.label_namaterapis.TabIndex = 1;
            this.label_namaterapis.Text = "Password";
            // 
            // label_kodeterapis
            // 
            this.label_kodeterapis.AutoSize = true;
            this.label_kodeterapis.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_kodeterapis.Location = new System.Drawing.Point(103, 103);
            this.label_kodeterapis.Name = "label_kodeterapis";
            this.label_kodeterapis.Size = new System.Drawing.Size(91, 39);
            this.label_kodeterapis.TabIndex = 0;
            this.label_kodeterapis.Text = "User";
            // 
            // panel_logo
            // 
            this.panel_logo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_logo.Controls.Add(this.pictureBox1);
            this.panel_logo.Location = new System.Drawing.Point(12, 12);
            this.panel_logo.Name = "panel_logo";
            this.panel_logo.Size = new System.Drawing.Size(772, 91);
            this.panel_logo.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Green_Leaf.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(121, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(529, 101);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(152, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(467, 39);
            this.label2.TabIndex = 15;
            this.label2.Text = "Silahkan login terlebih dahulu";
            // 
            // frm_userlogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(796, 520);
            this.ControlBox = false;
            this.Controls.Add(this.panel_isi);
            this.Controls.Add(this.panel_logo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_userlogin";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Login";
            this.panel_isi.ResumeLayout(false);
            this.panel_isi.PerformLayout();
            this.panel_logo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_isi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_batal;
        private System.Windows.Forms.Button btn_masuk;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_namaterapis;
        private System.Windows.Forms.Label label_kodeterapis;
        private System.Windows.Forms.Panel panel_logo;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}