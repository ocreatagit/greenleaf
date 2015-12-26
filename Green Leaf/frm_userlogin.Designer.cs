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
            this.pnl_login_isi = new System.Windows.Forms.Panel();
            this.lbl_login_silahkan = new System.Windows.Forms.Label();
            this.btn_login_batal = new System.Windows.Forms.Button();
            this.btn_login_masuk = new System.Windows.Forms.Button();
            this.txt_login_pass = new System.Windows.Forms.TextBox();
            this.txt_login_username = new System.Windows.Forms.TextBox();
            this.lbl_login_2 = new System.Windows.Forms.Label();
            this.lbl_login_1 = new System.Windows.Forms.Label();
            this.lbl_login_pass = new System.Windows.Forms.Label();
            this.lbl_login_user = new System.Windows.Forms.Label();
            this.panel_logo = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnl_login_isi.SuspendLayout();
            this.panel_logo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_login_isi
            // 
            this.pnl_login_isi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_login_isi.Controls.Add(this.lbl_login_silahkan);
            this.pnl_login_isi.Controls.Add(this.btn_login_batal);
            this.pnl_login_isi.Controls.Add(this.btn_login_masuk);
            this.pnl_login_isi.Controls.Add(this.txt_login_pass);
            this.pnl_login_isi.Controls.Add(this.txt_login_username);
            this.pnl_login_isi.Controls.Add(this.lbl_login_2);
            this.pnl_login_isi.Controls.Add(this.lbl_login_1);
            this.pnl_login_isi.Controls.Add(this.lbl_login_pass);
            this.pnl_login_isi.Controls.Add(this.lbl_login_user);
            this.pnl_login_isi.Location = new System.Drawing.Point(12, 101);
            this.pnl_login_isi.Name = "pnl_login_isi";
            this.pnl_login_isi.Size = new System.Drawing.Size(1250, 528);
            this.pnl_login_isi.TabIndex = 7;
            // 
            // lbl_login_silahkan
            // 
            this.lbl_login_silahkan.AutoSize = true;
            this.lbl_login_silahkan.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_login_silahkan.Location = new System.Drawing.Point(152, 14);
            this.lbl_login_silahkan.Name = "lbl_login_silahkan";
            this.lbl_login_silahkan.Size = new System.Drawing.Size(467, 39);
            this.lbl_login_silahkan.TabIndex = 15;
            this.lbl_login_silahkan.Text = "Silahkan login terlebih dahulu";
            // 
            // btn_login_batal
            // 
            this.btn_login_batal.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_login_batal.Location = new System.Drawing.Point(399, 251);
            this.btn_login_batal.Name = "btn_login_batal";
            this.btn_login_batal.Size = new System.Drawing.Size(155, 67);
            this.btn_login_batal.TabIndex = 14;
            this.btn_login_batal.Text = "Batal";
            this.btn_login_batal.UseVisualStyleBackColor = true;
            this.btn_login_batal.Click += new System.EventHandler(this.btn_batal_Click);
            // 
            // btn_login_masuk
            // 
            this.btn_login_masuk.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_login_masuk.Location = new System.Drawing.Point(217, 251);
            this.btn_login_masuk.Name = "btn_login_masuk";
            this.btn_login_masuk.Size = new System.Drawing.Size(155, 67);
            this.btn_login_masuk.TabIndex = 13;
            this.btn_login_masuk.Text = "Masuk";
            this.btn_login_masuk.UseVisualStyleBackColor = true;
            this.btn_login_masuk.Click += new System.EventHandler(this.btn_masuk_Click);
            // 
            // txt_login_pass
            // 
            this.txt_login_pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_login_pass.Location = new System.Drawing.Point(330, 178);
            this.txt_login_pass.MaxLength = 30;
            this.txt_login_pass.Name = "txt_login_pass";
            this.txt_login_pass.PasswordChar = '*';
            this.txt_login_pass.Size = new System.Drawing.Size(337, 47);
            this.txt_login_pass.TabIndex = 9;
            this.txt_login_pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_login_pass_KeyDown);
            // 
            // txt_login_username
            // 
            this.txt_login_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_login_username.Location = new System.Drawing.Point(330, 100);
            this.txt_login_username.MaxLength = 30;
            this.txt_login_username.Name = "txt_login_username";
            this.txt_login_username.Size = new System.Drawing.Size(337, 47);
            this.txt_login_username.TabIndex = 8;
            // 
            // lbl_login_2
            // 
            this.lbl_login_2.AutoSize = true;
            this.lbl_login_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_login_2.Location = new System.Drawing.Point(297, 181);
            this.lbl_login_2.Name = "lbl_login_2";
            this.lbl_login_2.Size = new System.Drawing.Size(27, 39);
            this.lbl_login_2.TabIndex = 5;
            this.lbl_login_2.Text = ":";
            // 
            // lbl_login_1
            // 
            this.lbl_login_1.AutoSize = true;
            this.lbl_login_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_login_1.Location = new System.Drawing.Point(297, 103);
            this.lbl_login_1.Name = "lbl_login_1";
            this.lbl_login_1.Size = new System.Drawing.Size(27, 39);
            this.lbl_login_1.TabIndex = 4;
            this.lbl_login_1.Text = ":";
            // 
            // lbl_login_pass
            // 
            this.lbl_login_pass.AutoSize = true;
            this.lbl_login_pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_login_pass.Location = new System.Drawing.Point(103, 181);
            this.lbl_login_pass.Name = "lbl_login_pass";
            this.lbl_login_pass.Size = new System.Drawing.Size(170, 39);
            this.lbl_login_pass.TabIndex = 1;
            this.lbl_login_pass.Text = "Password";
            // 
            // lbl_login_user
            // 
            this.lbl_login_user.AutoSize = true;
            this.lbl_login_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_login_user.Location = new System.Drawing.Point(103, 103);
            this.lbl_login_user.Name = "lbl_login_user";
            this.lbl_login_user.Size = new System.Drawing.Size(91, 39);
            this.lbl_login_user.TabIndex = 0;
            this.lbl_login_user.Text = "User";
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
            // frm_userlogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1274, 652);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_login_isi);
            this.Controls.Add(this.panel_logo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_userlogin";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Login";
            this.pnl_login_isi.ResumeLayout(false);
            this.pnl_login_isi.PerformLayout();
            this.panel_logo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_login_isi;
        private System.Windows.Forms.Label lbl_login_silahkan;
        private System.Windows.Forms.Button btn_login_batal;
        private System.Windows.Forms.Button btn_login_masuk;
        private System.Windows.Forms.TextBox txt_login_pass;
        private System.Windows.Forms.TextBox txt_login_username;
        private System.Windows.Forms.Label lbl_login_2;
        private System.Windows.Forms.Label lbl_login_1;
        private System.Windows.Forms.Label lbl_login_pass;
        private System.Windows.Forms.Label lbl_login_user;
        private System.Windows.Forms.Panel panel_logo;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}