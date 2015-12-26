using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Green_Leaf
{
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
        }

        string tbhtrps_lokasi_gambar = "";

        #region(Buat Windows Form tidak bisa dirubah posisinya)
        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }
        #endregion

        private void frm_main_Load(object sender, EventArgs e)
        {

        }

        private void btn_login_masuk_Click(object sender, EventArgs e)
        {
            bool login_userAda = false;
            bool login_passSama = false;

            #region(Select Username dan Password)
            string query;
            string connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            List<string> lstUsers = new List<string>();
            List<string> lstPass = new List<string>();
            try
            {
                conn.Open();

                query = "SELECT `id_pengguna`, `nama_pengguna`, `kata_kunci`, `jenis_pengguna` FROM `pengguna`";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    lstUsers.Add(rdr[1].ToString());
                    lstPass.Add(rdr[2].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            #endregion

            if (txt_login_username.Text != "")
            {
                foreach (string user in lstUsers)
                {
                    if (user == txt_login_username.Text)
                    {
                        login_userAda = true;
                        break;
                    }
                    else
                    {
                        login_userAda = false;
                    }
                }

                if (login_userAda == true)
                {
                    for (int i = 0; i < lstUsers.Count; i++)
                    {
                        if (txt_login_pass.Text == lstPass[i])
                        {
                            login_passSama = true;
                            //MessageBox.Show("Login berhasil");
                            break;
                        }
                        else
                        {
                            login_passSama = false;
                            //MessageBox.Show("Login gagal, Password yang anda masukan salah");
                        }

                    }
                    if (login_passSama == true)
                    {
                        MessageBox.Show("Login berhasil");
                        if (txt_login_username.Text == "superadmin")
                        {
                            pnl_login_isi.Visible = false;
                            pnl_menu_isi.Visible = true;
                        }
                        else if (txt_login_username.Text == "sales")
                        {
                            pnl_login_isi.Visible = false;
                            pnl_menu_isi.Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Login gagal, Password yang anda masukan salah");
                    }
                }
                else
                {
                    MessageBox.Show("Login gagal, Username yang anda masukan tidak terdaftar");
                }
            }
            else
            {
                MessageBox.Show("Mohon isi kolom Username terlebih dahulu");
            }

        }

        private void btn_batal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_login_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool login_userAda = false;
                bool login_passSama = false;

                #region(Select Username dan Password)
                string query;
                string connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                MySqlConnection conn = new MySqlConnection(connStr);
                List<string> lstUsers = new List<string>();
                List<string> lstPass = new List<string>();
                try
                {
                    conn.Open();

                    query = "SELECT `id_pengguna`, `nama_pengguna`, `kata_kunci`, `jenis_pengguna` FROM `pengguna`";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        lstUsers.Add(rdr[1].ToString());
                        lstPass.Add(rdr[2].ToString());
                    }
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
                #endregion

                if (txt_login_username.Text != "")
                {
                    foreach (string user in lstUsers)
                    {
                        if (user == txt_login_username.Text)
                        {
                            login_userAda = true;
                            break;
                        }
                        else
                        {
                            login_userAda = false;
                        }
                    }

                    if (login_userAda == true)
                    {
                        for (int i = 0; i < lstUsers.Count; i++)
                        {
                            if (txt_login_pass.Text == lstPass[i])
                            {
                                login_passSama = true;
                                //MessageBox.Show("Login berhasil");
                                break;
                            }
                            else
                            {
                                login_passSama = false;
                                //MessageBox.Show("Login gagal, Password yang anda masukan salah");
                            }

                        }
                        if (login_passSama == true)
                        {
                            MessageBox.Show("Login berhasil");
                            if (txt_login_username.Text == "superadmin")
                            {
                                pnl_login_isi.Visible = false;
                                pnl_menu_isi.Visible = true;
                            }
                            else if (txt_login_username.Text == "sales")
                            {
                                pnl_login_isi.Visible = false;
                                pnl_menu_isi.Visible = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Login gagal, Password yang anda masukan salah");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Login gagal, Username yang anda masukan tidak terdaftar");
                    }
                }
                else
                {
                    MessageBox.Show("Mohon isi kolom Username terlebih dahulu");
                }
            }
        }

        private void btn_menu_tbhtrps_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
            pnl_tbhtrps_isi.Visible = true;
        }

        private void btn_menu_edttrps_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
        }

        private void btn_menu_tbhpkt_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
        }

        private void btn_menu_edtpkt_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
        }

        private void btn_menu_ctknota_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
        }

        private void btn_menu_laporanpenjualan_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
        }

        private void btn_menu_laporangajiterapis_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
        }

        private void btn_menu_laporangajiexcel_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
        }

        private void btn_menu_variabel_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
        }

        private void btn_tbhtrps_browsefoto_Click(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog tbhtrps_open = new OpenFileDialog();
            // image filters
            tbhtrps_open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (tbhtrps_open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box

                // image file path
                tbhtrps_lokasi_gambar = tbhtrps_open.FileName.Replace(@"\", @"\\");
                pict_tbhtrps_fotoKTP.Image = new Bitmap(tbhtrps_lokasi_gambar);
            }
        }

        private void btn_tbhtrps_tambah_Click(object sender, EventArgs e)
        {
            DBConnect tbhtrps_sql = new DBConnect();

            bool tbhtrps_kodeSama = false;

            #region(Select khusus kode terapis, disimpan ke dalam List lstkode)
            string tbhtrps_query;
            string tbhtrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection tbhtrps_conn = new MySqlConnection(tbhtrps_connStr);
            List<int> tbhtrps_lstKode = new List<int>();
            try
            {
                tbhtrps_conn.Open();

                tbhtrps_query = "SELECT kode_terapis FROM `terapis`";
                MySqlCommand tbhtrps_cmd = new MySqlCommand(tbhtrps_query, tbhtrps_conn);
                MySqlDataReader tbhtrps_rdr = tbhtrps_cmd.ExecuteReader();

                while (tbhtrps_rdr.Read())
                {
                    tbhtrps_lstKode.Add(tbhtrps_rdr.GetInt32(0));
                }
                tbhtrps_rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            tbhtrps_conn.Close();
            #endregion

            foreach (int tbhtrps_kode in tbhtrps_lstKode)
            {
                if (tbhtrps_kode.ToString() == txt_tbhtrps_kodeterapis.Text)
                {
                    MessageBox.Show("Maaf, Kode Terapis yang anda masukkan sudah terdaftar, silahkan ganti dengan Kode Terapis yang berbeda!");
                    tbhtrps_kodeSama = true;
                    break;
                }
                else
                {
                    tbhtrps_kodeSama = false;
                }
            }

            if (tbhtrps_kodeSama == false)
            {
                if (txt_tbhtrps_kodeterapis.Text == "")
                {
                    MessageBox.Show("Mohon lengkapi data Kode Terapis terlebih dahulu!");
                }
                else if (txt_tbhtrps_namaterapis.Text == "")
                {
                    MessageBox.Show("Mohon lengkapi data Nama Terapis terlebih dahulu!");
                }
                else if (tbhtrps_lokasi_gambar == "")
                {
                    MessageBox.Show("Mohon pilih foto KTP terlebih dahulu!");
                }
                else
                {
                    if (rdo_tbhtrps_statusaktif.Checked == false && rdo_tbhtrps_statustdkaktif.Checked == false)
                    {
                        MessageBox.Show("Mohon pilih Status terlebih dahulu!");
                    }
                    else if (rdo_tbhtrps_statusaktif.Checked)
                    {
                        tbhtrps_query = "INSERT INTO `terapis` (`kode_terapis`, `nama_terapis`, `lokasi_gambar`, `status_terapis`) VALUES ('" + txt_tbhtrps_kodeterapis.Text + "', '" + txt_tbhtrps_namaterapis.Text + "', '" + tbhtrps_lokasi_gambar + "', 'Aktif');";
                        tbhtrps_sql.Insert(tbhtrps_query);
                        MessageBox.Show("Terapis telah berhasil ditambahkan");
                        txt_tbhtrps_kodeterapis.Clear();
                        txt_tbhtrps_namaterapis.Clear();
                        tbhtrps_lokasi_gambar = "";
                        rdo_tbhtrps_statusaktif.Checked = false;
                        rdo_tbhtrps_statustdkaktif.Checked = false;
                        pict_tbhtrps_fotoKTP.Image = null;
                        txt_tbhtrps_kodeterapis.Focus();
                    }
                    else if (rdo_tbhtrps_statustdkaktif.Checked)
                    {
                        tbhtrps_query = "INSERT INTO `terapis` (`kode_terapis`, `nama_terapis`, `lokasi_gambar`, `status_terapis`) VALUES ('" + txt_tbhtrps_kodeterapis.Text + "', '" + txt_tbhtrps_namaterapis.Text + "', '" + tbhtrps_lokasi_gambar + "', 'Tidak Aktif');";
                        tbhtrps_sql.Insert(tbhtrps_query);
                        MessageBox.Show("Terapis telah berhasil ditambahkan");
                        txt_tbhtrps_kodeterapis.Clear();
                        txt_tbhtrps_namaterapis.Clear();
                        tbhtrps_lokasi_gambar = "";
                        rdo_tbhtrps_statusaktif.Checked = false;
                        rdo_tbhtrps_statustdkaktif.Checked = false;
                        pict_tbhtrps_fotoKTP.Image = null;
                        txt_tbhtrps_kodeterapis.Focus();
                    }
                }
            }
        }

        private void btn_tbhtrps_batal_Click(object sender, EventArgs e)
        {
            pnl_tbhtrps_isi.Visible = false;
            pnl_menu_isi.Visible = true;
        }

        private void txt_tbhtrps_kodeterapis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        

    }
}
