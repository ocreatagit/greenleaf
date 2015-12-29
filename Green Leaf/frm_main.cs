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

        #region(Panel Login)
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

        private void txt_login_username_KeyDown(object sender, KeyEventArgs e)
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
        #endregion

        #region(Panel Menu)
        private void btn_menu_tbhtrps_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
            pnl_tbhtrps_isi.Visible = true;
        }

        private void btn_menu_edttrps_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
            pnl_edttrps_isi.Visible = true;
            btn_edttrps_simpan.Enabled = false;
            txt_edttrps_kodeterapis.Enabled = false;
            txt_edttrps_namaterapis.Enabled = false;
            btn_edttrps_browsefoto.Enabled = false;
            rdo_edttrps_statusaktif.Enabled = false;
            rdo_edttrps_statustdkaktif.Enabled = false;



            #region(Select)
            string edttrps_query;
            string edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection edttrps_conn = new MySqlConnection(edttrps_connStr);
            List<string> edttrps_lstKode = new List<string>();
            try
            {
                edttrps_conn.Open();

                edttrps_query = "SELECT * FROM `terapis` order by id_terapis DESC";
                MySqlCommand edttrps_cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                MySqlDataReader edttrps_rdr = edttrps_cmd.ExecuteReader();

                while (edttrps_rdr.Read())
                {
                    //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                    lsb_edttrps_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                }
                edttrps_rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            edttrps_conn.Close();
            #endregion
        }

        private void btn_menu_tbhpkt_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
            pnl_tbhpkt_isi.Visible = true;
        }

        private void btn_menu_edtpkt_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
            pnl_edtpkt_isi.Visible = true;

            edtpkt_lstnamapaket[0] = new List<string>();
            edtpkt_lstnamapaket[1] = new List<string>();
            cbo_edtpkt_jenispaket.Enabled = false;
            txt_edtpkt_namapaket.Enabled = false;
            txt_tbhpkt_komisipaketnormal.Enabled = false;
            txt_tbhpkt_komisipaketmidnight.Enabled = false;
            txt_edtpkt_durasipaketjam.Enabled = false;
            txt_edtpkt_durasipaketmenit.Enabled = false;
            txt_edtpkt_hargapaket.Enabled = false;
            btn_edtpkt_simpan.Enabled = false;
            lsb_edtpkt_jenisnamapkt.Items.Clear();

            edtpkt_lstidpktTerpilih.Clear();

            #region(Isi listbox dengan Jenis dan Nama Paket per baris)
            string edtpkt_jenisnamapkt;
            string edtpkt_query;
            string edtpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection edtpkt_conn = new MySqlConnection(edtpkt_connStr);
            try
            {
                edtpkt_conn.Open();

                edtpkt_query = "SELECT * FROM `paket` ORDER BY `id_paket` DESC";
                MySqlCommand edtpkt_cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                MySqlDataReader edtpkt_rdr = edtpkt_cmd.ExecuteReader();

                while (edtpkt_rdr.Read())
                {
                    //cbo_kodeterapis.Items.Add(edtpkt_rdr.GetString(1));
                    edtpkt_lstidpktTerpilih.Add(edtpkt_rdr.GetInt16(0));
                    edtpkt_jenisnamapkt = edtpkt_rdr.GetString(1);
                    edtpkt_jenisnamapkt += " - " + edtpkt_rdr.GetString(2);
                    lsb_edtpkt_jenisnamapkt.Items.Add(edtpkt_jenisnamapkt);
                    edtpkt_lstnamapaket[0].Add(edtpkt_rdr.GetString(1));
                    edtpkt_lstnamapaket[1].Add(edtpkt_rdr.GetString(2));
                }
                edtpkt_rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Error Occured");
            }
            edtpkt_conn.Close();
            #endregion
        }

        private void btn_menu_ctknota_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
            pnl_ctknota_isi.Visible = true;

            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = false;
            rdo_ctknota_midnight.Enabled = false;
            rdo_ctknota_hotel.Enabled = false;
            rdo_ctknota_biasa.Enabled = false;
            cbo_ctknota_jenispaket.Enabled = false;
            dgv_ctknota_tabelhrgpkt.Enabled = false;
            rdo_ctknota_cash.Enabled = false;
            rdo_ctknota_credit.Enabled = false;
            cbo_ctknota_kodeterapis.Enabled = false;
            txt_ctknota_namaterapis.Enabled = false;
            txt_ctknota_diskon.Enabled = false;
            txt_ctknota_ket.Enabled = false;
            txt_ctknota_fee.Enabled = false;

            btn_ctknota_cetak.Enabled = false;
            btn_ctknota_batal.Enabled = true;



            #region(Ambil data Potongan dan Extra dari database)
            string ctknota_query2;
            string ctknota_connStr2 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection ctknota_conn2 = new MySqlConnection(ctknota_connStr2);
            try
            {
                ctknota_conn2.Open();

                ctknota_query2 = "SELECT * FROM `variabel`";
                MySqlCommand ctknota_cmd2 = new MySqlCommand(ctknota_query2, ctknota_conn2);
                MySqlDataReader ctknota_rdr2 = ctknota_cmd2.ExecuteReader();

                while (ctknota_rdr2.Read())
                {
                    //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                    //cbo_ctknota_jenispaket.Items.Add(ctknota_rdr.GetString(0));
                    ctknota_extra = ctknota_rdr2.GetInt32(1);
                    ctknota_tamuhotel = ctknota_rdr2.GetInt32(2);
                }
                ctknota_rdr2.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            ctknota_conn2.Close();
            #endregion

            cbo_ctknota_kodeterapis.Items.Clear();
            #region(Select)
            string ctknota_query3;
            string ctknota_connStr3 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection ctknota_conn3 = new MySqlConnection(ctknota_connStr3);
            try
            {
                ctknota_conn3.Open();

                ctknota_query3 = "SELECT * FROM `terapis` WHERE `status_terapis` = 'Aktif'";
                MySqlCommand ctknota_cmd3 = new MySqlCommand(ctknota_query3, ctknota_conn3);
                MySqlDataReader ctknota_rdr3 = ctknota_cmd3.ExecuteReader();

                while (ctknota_rdr3.Read())
                {
                    //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                    cbo_ctknota_kodeterapis.Items.Add(ctknota_rdr3.GetString(1));
                }
                ctknota_rdr3.Close();
            }
            catch (Exception ex)
            {
                string Hasilex = ex.ToString();
                MessageBox.Show("Error Occured");
            }
            ctknota_conn3.Close();
            #endregion
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
            pnl_variabel_isi.Visible = true;

            #region(Isi textbox)
            string edtpkt_query;
            string edtpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection edtpkt_conn = new MySqlConnection(edtpkt_connStr);
            try
            {
                edtpkt_conn.Open();

                edtpkt_query = "SELECT * FROM `variabel`";
                MySqlCommand edtpkt_cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                MySqlDataReader edtpkt_rdr = edtpkt_cmd.ExecuteReader();

                while (edtpkt_rdr.Read())
                {
                    txt_variabel_extra.Text = edtpkt_rdr.GetString(1);
                    txt_variabel_potonganhotel.Text = edtpkt_rdr.GetString(2);
                }
                edtpkt_rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Error Occured");
            }
            edtpkt_conn.Close();
            #endregion
        }
        #endregion

        #region(Panel Tambah Terapis)
        string tbhtrps_lokasi_gambar = "";
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
        #endregion

        #region(Panel Edit Terapis)
        string edttrps_lokasi_gambar;
        int edttrps_idTerapis;
        int edttrps_kodeTerakhir;
        private void btn_edttrps_browsefoto_Click(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog edttrps_open = new OpenFileDialog();
            // image filters
            edttrps_open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (edttrps_open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box

                // image file path
                edttrps_lokasi_gambar = edttrps_open.FileName.Replace(@"\", @"\\");
                pict_edttrps_fotoKTP.Image = new Bitmap(edttrps_lokasi_gambar);
            }
        }

        private void btn_edttrps_ok_Click(object sender, EventArgs e)
        {

            if (lsb_edttrps_kodeterapis.SelectedItem != null)
            {
                btn_edttrps_simpan.Enabled = true;
                txt_edttrps_kodeterapis.Enabled = true;
                txt_edttrps_namaterapis.Enabled = true;
                btn_edttrps_browsefoto.Enabled = true;
                rdo_edttrps_statusaktif.Enabled = true;
                rdo_edttrps_statustdkaktif.Enabled = true;
                string edttrps_kodeTerpilih = lsb_edttrps_kodeterapis.SelectedItem.ToString();

                string edttrps_query;

                #region(Select)
                string edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                MySqlConnection edttrps_conn = new MySqlConnection(edttrps_connStr);
                List<string> edttrps_lstKode = new List<string>();
                try
                {
                    edttrps_conn.Open();

                    edttrps_query = "SELECT * FROM `terapis` WHERE `kode_terapis` LIKE '" + edttrps_kodeTerpilih + "'";
                    MySqlCommand edttrps_cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                    MySqlDataReader edttrps_rdr = edttrps_cmd.ExecuteReader();

                    while (edttrps_rdr.Read())
                    {
                        edttrps_idTerapis = edttrps_rdr.GetInt32(0);
                        txt_edttrps_kodeterapis.Text = edttrps_rdr.GetInt32(1).ToString();
                        edttrps_kodeTerakhir = int.Parse(txt_edttrps_kodeterapis.Text);
                        txt_edttrps_namaterapis.Text = edttrps_rdr.GetString(2);
                        pict_edttrps_fotoKTP.Image = new Bitmap(edttrps_rdr.GetString(3));
                        edttrps_lokasi_gambar = edttrps_rdr.GetString(3);
                        if (edttrps_rdr.GetString(4) == "Aktif")
                        {
                            rdo_edttrps_statusaktif.Checked = true;
                        }
                        else if (edttrps_rdr.GetString(4) == "Tidak Aktif")
                        {
                            rdo_edttrps_statustdkaktif.Checked = true;
                        }
                    }
                    edttrps_rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                edttrps_conn.Close();
                #endregion
            }
            else
            {
                MessageBox.Show("Mohon pilih Kode Terapis terlebih dahulu!");
            }
        }

        private void btn_edttrps_simpan_Click(object sender, EventArgs e)
        {
            if (txt_edttrps_kodeterapis.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Kode Terapis terlebih dahulu!");
            }
            else if (txt_edttrps_namaterapis.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Nama Terapis terlebih dahulu!");
            }
            else if (edttrps_lokasi_gambar == "")
            {
                MessageBox.Show("Mohon pilih foto KTP terlebih dahulu!");
            }
            else
            {
                string edttrps_connStr;
                MySqlConnection edttrps_conn;
                DBConnect edttrps_sql = new DBConnect();
                string edttrps_query;
                bool edttrps_kodeSama = false;
                edttrps_lokasi_gambar = edttrps_lokasi_gambar.Replace(@"\", @"\\");

                if (txt_edttrps_kodeterapis.Text == edttrps_kodeTerakhir.ToString())
                {
                    if (rdo_edttrps_statusaktif.Checked)
                    {
                        #region(Update)
                        edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                        edttrps_conn = new MySqlConnection(edttrps_connStr);
                        try
                        {
                            edttrps_conn.Open();

                            edttrps_query = "UPDATE `terapis` SET `kode_terapis` = '" + edttrps_kodeTerakhir + "', `nama_terapis` = '" + txt_edttrps_namaterapis.Text + "', `lokasi_gambar` = '" + edttrps_lokasi_gambar + "', `status_terapis` = 'Aktif' WHERE `terapis`.`id_terapis` = " + edttrps_idTerapis + "";
                            MySqlCommand cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        edttrps_conn.Close();
                        #endregion
                        MessageBox.Show("Data terapis telah berhasil disimpan");
                        txt_edttrps_kodeterapis.Clear();
                        txt_edttrps_namaterapis.Clear();
                        edttrps_lokasi_gambar = "";
                        rdo_edttrps_statusaktif.Checked = false;
                        rdo_edttrps_statustdkaktif.Checked = false;
                        pict_edttrps_fotoKTP.Image = null;
                        txt_edttrps_kodeterapis.Focus();
                        btn_edttrps_simpan.Enabled = false;
                        txt_edttrps_kodeterapis.Enabled = false;
                        txt_edttrps_namaterapis.Enabled = false;
                        btn_edttrps_browsefoto.Enabled = false;
                        rdo_edttrps_statusaktif.Enabled = false;
                        rdo_edttrps_statustdkaktif.Enabled = false;
                    }
                    else if (rdo_edttrps_statustdkaktif.Checked)
                    {
                        #region(Update)
                        edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                        edttrps_conn = new MySqlConnection(edttrps_connStr);
                        try
                        {
                            edttrps_conn.Open();

                            edttrps_query = "UPDATE `terapis` SET `kode_terapis` = '" + edttrps_kodeTerakhir + "', `nama_terapis` = '" + txt_edttrps_namaterapis.Text + "', `lokasi_gambar` = '" + edttrps_lokasi_gambar + "', `status_terapis` = 'Tidak Aktif' WHERE `terapis`.`id_terapis` = " + edttrps_idTerapis + "";
                            MySqlCommand edttrps_cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                            edttrps_cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        edttrps_conn.Close();
                        #endregion
                        MessageBox.Show("Data terapis telah berhasil disimpan");
                        txt_edttrps_kodeterapis.Clear();
                        txt_edttrps_namaterapis.Clear();
                        edttrps_lokasi_gambar = "";
                        rdo_edttrps_statusaktif.Checked = false;
                        rdo_edttrps_statustdkaktif.Checked = false;
                        pict_edttrps_fotoKTP.Image = null;
                        txt_edttrps_kodeterapis.Focus();
                        btn_edttrps_simpan.Enabled = false;
                        txt_edttrps_kodeterapis.Enabled = false;
                        txt_edttrps_namaterapis.Enabled = false;
                        btn_edttrps_browsefoto.Enabled = false;
                        rdo_edttrps_statusaktif.Enabled = false;
                        rdo_edttrps_statustdkaktif.Enabled = false;
                    }
                }
                else
                {
                    #region(Select)
                    edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    edttrps_conn = new MySqlConnection(edttrps_connStr);
                    List<string> edttrps_lstKode = new List<string>();
                    try
                    {
                        edttrps_conn.Open();

                        edttrps_query = "SELECT kode_terapis FROM `terapis`";
                        MySqlCommand edttrps_cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                        MySqlDataReader edttrps_rdr = edttrps_cmd.ExecuteReader();

                        while (edttrps_rdr.Read())
                        {
                            edttrps_lstKode.Add(edttrps_rdr[0].ToString());
                        }
                        edttrps_rdr.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    edttrps_conn.Close();
                    #endregion

                    foreach (string edttrps_kode in edttrps_lstKode)
                    {
                        if (edttrps_kode == txt_edttrps_kodeterapis.Text)
                        {
                            MessageBox.Show("Maaf, Kode Terapis yang anda masukkan sudah terdaftar, silahkan ganti dengan Kode Terapis yang berbeda!");
                            edttrps_kodeSama = true;
                            break;
                        }
                        else
                        {
                            edttrps_kodeSama = false;
                        }
                    }

                    if (edttrps_kodeSama == false)
                    {
                        if (rdo_edttrps_statusaktif.Checked)
                        {
                            #region(Update)
                            edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                            edttrps_conn = new MySqlConnection(edttrps_connStr);
                            try
                            {
                                edttrps_conn.Open();

                                edttrps_query = "UPDATE `terapis` SET `kode_terapis` = '" + txt_edttrps_kodeterapis.Text + "', `nama_terapis` = '" + txt_edttrps_namaterapis.Text + "', `lokasi_gambar` = '" + edttrps_lokasi_gambar + "', `status_terapis` = 'Aktif' WHERE `terapis`.`id_terapis` = " + edttrps_idTerapis + "";
                                MySqlCommand cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                            edttrps_conn.Close();
                            #endregion
                            MessageBox.Show("Data terapis telah berhasil disimpan");
                            txt_edttrps_kodeterapis.Clear();
                            txt_edttrps_namaterapis.Clear();
                            edttrps_lokasi_gambar = "";
                            rdo_edttrps_statusaktif.Checked = false;
                            rdo_edttrps_statustdkaktif.Checked = false;
                            pict_edttrps_fotoKTP.Image = null;
                            txt_edttrps_kodeterapis.Focus();
                            btn_edttrps_simpan.Enabled = false;
                            txt_edttrps_kodeterapis.Enabled = false;
                            txt_edttrps_namaterapis.Enabled = false;
                            btn_edttrps_browsefoto.Enabled = false;
                            rdo_edttrps_statusaktif.Enabled = false;
                            rdo_edttrps_statustdkaktif.Enabled = false;
                        }
                        else if (rdo_edttrps_statustdkaktif.Checked)
                        {
                            #region(Update)
                            edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                            edttrps_conn = new MySqlConnection(edttrps_connStr);
                            try
                            {
                                edttrps_conn.Open();

                                edttrps_query = "UPDATE `terapis` SET `kode_terapis` = '" + txt_edttrps_kodeterapis.Text + "', `nama_terapis` = '" + txt_edttrps_namaterapis.Text + "', `lokasi_gambar` = '" + edttrps_lokasi_gambar + "', `status_terapis` = 'Tidak Aktif' WHERE `terapis`.`id_terapis` = " + edttrps_idTerapis + "";
                                MySqlCommand cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                            edttrps_conn.Close();
                            #endregion
                            MessageBox.Show("Data terapis telah berhasil disimpan");
                            txt_edttrps_kodeterapis.Clear();
                            txt_edttrps_namaterapis.Clear();
                            edttrps_lokasi_gambar = "";
                            rdo_edttrps_statusaktif.Checked = false;
                            rdo_edttrps_statustdkaktif.Checked = false;
                            pict_edttrps_fotoKTP.Image = null;
                            txt_edttrps_kodeterapis.Focus();
                            btn_edttrps_simpan.Enabled = false;
                            txt_edttrps_kodeterapis.Enabled = false;
                            txt_edttrps_namaterapis.Enabled = false;
                            btn_edttrps_browsefoto.Enabled = false;
                            rdo_edttrps_statusaktif.Enabled = false;
                            rdo_edttrps_statustdkaktif.Enabled = false;
                        }
                    }
                }
                //cbo_kodeterapis.Items.Clear();
                lsb_edttrps_kodeterapis.Items.Clear();
                #region(Select)
                edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                edttrps_conn = new MySqlConnection(edttrps_connStr);
                try
                {
                    edttrps_conn.Open();

                    edttrps_query = "SELECT * FROM `terapis` order by id_terapis DESC";
                    MySqlCommand edttrps_cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                    MySqlDataReader edttrps_rdr = edttrps_cmd.ExecuteReader();

                    while (edttrps_rdr.Read())
                    {
                        //cbo_kodeterapis.Items.Add(rdr.GetString(1));
                        lsb_edttrps_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                    }
                    edttrps_rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                edttrps_conn.Close();
                #endregion
            }


        }

        private void btn_edttrps_batal_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = true;
            pnl_edttrps_isi.Visible = false;
        }

        private void txt_edttrps_kodeterapis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region(Panel Tambah Paket)
        private void btn_tbhpkt_tambah_Click(object sender, EventArgs e)
        {
            #region(Cek inputan kosong)
            if (cbo_tbhpkt_jenispaket.SelectedItem == null)
            {
                MessageBox.Show("Mohon pilih Jenis Paket terlebih dahulu");
            }
            else if (txt_tbhpkt_namapaket.Text == "")
            {
                MessageBox.Show("Mohon isi kolom Nama Paket terlebih dahulu");
            }
            else if (txt_tbhpkt_durasipaketjam.Text == "")
            {
                MessageBox.Show("Mohon lengkapi kolom Durasi Paket terlebih dahulu");
            }
            else if (txt_tbhpkt_durasipaketmenit.Text == "")
            {
                MessageBox.Show("Mohon lengkapi kolom Durasi Paket terlebih dahulu");
            }
            else if (txt_tbhpkt_hargapaket.Text == "")
            {
                MessageBox.Show("Mohon isi kolom Harga Paket terlebih dahulu");
            }
            else if (txt_tbhpkt_komisipaketnormal.Text == "")
            {
                MessageBox.Show("Mohon lengkapi kolom Komisi Paket terlebih dahulu");
            }
            else if (txt_tbhpkt_komisipaketmidnight.Text == "")
            {
                MessageBox.Show("Mohon lengkapi kolom Komisi Paket terlebih dahulu");
            }

            else
            {
            #endregion
                #region(Cek Nama Paket yang sama berdasarkan Jenis Paket)
                string tbhpkt_query;
                string tbhpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                MySqlConnection tbhpkt_conn = new MySqlConnection(tbhpkt_connStr);
                List<string> tbhpkt_lstHasil = new List<string>();
                try
                {
                    tbhpkt_conn.Open();

                    tbhpkt_query = "SELECT * FROM `paket` WHERE `jenis_paket` = '" + cbo_tbhpkt_jenispaket.SelectedItem + "' AND `nama_paket` = '" + txt_tbhpkt_namapaket.Text + "'";
                    MySqlCommand tbhpkt_cmd = new MySqlCommand(tbhpkt_query, tbhpkt_conn);
                    MySqlDataReader tbhpkt_rdr = tbhpkt_cmd.ExecuteReader();

                    while (tbhpkt_rdr.Read())
                    {
                        tbhpkt_lstHasil.Add(tbhpkt_rdr.GetString(1));
                        tbhpkt_lstHasil.Add(tbhpkt_rdr.GetString(2));
                    }
                    tbhpkt_rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                tbhpkt_conn.Close();
                if (tbhpkt_lstHasil.Count != 0)
                {
                    MessageBox.Show("Maaf, Nama Paket: " + txt_tbhpkt_namapaket.Text + ", dengan Jenis Paket: " + cbo_tbhpkt_jenispaket.SelectedItem + ", sudah ada di dalam database");
                }
                #endregion
                #region(Insert paket ke databse)
                else
                {
                    //#region(Buat huruf besar untuk Jam dan Menit)
                    //string tbhpkt_durasiPaket = txt_tbhpkt_durasipaketjam.Text;
                    //var regex = new Regex(@"\b[A-Z]", RegexOptions.IgnoreCase);
                    //tbhpkt_durasiPaket = regex.Replace(tbhpkt_durasiPaket, m => m.ToString().ToUpper());
                    //#endregion
                    string durasi = txt_tbhpkt_durasipaketjam.Text + " Jam " + txt_tbhpkt_durasipaketmenit.Text + " Menit";

                    DBConnect tbhpkt_sql = new DBConnect();

                    //tbhpkt_query = "INSERT INTO `paket` (`id_paket`, `jenis_paket`, `nama_paket`, `durasi_paket`, `harga_paket`, `jam_kerja`, `komisi_per_paket`) "
                    //        + "VALUES (NULL, '" +  + "', '" +  + "', '" +  + "', '" +  + "', 'Normal', '');";

                    tbhpkt_query = "INSERT INTO `paket` (`id_paket`, `jenis_paket`, `nama_paket`, `durasi_paket`, `harga_paket`, "
                        + "`komisi_normal_paket`, `komisi_midnight_paket`) VALUES (NULL, '" + cbo_tbhpkt_jenispaket.SelectedItem + "', '" + txt_tbhpkt_namapaket.Text + "', '" +
                        durasi + "', '" + txt_tbhpkt_hargapaket.Text + "', '" + txt_tbhpkt_komisipaketnormal.Text + "', '" + txt_tbhpkt_komisipaketmidnight.Text + "');";
                    tbhpkt_sql.Insert(tbhpkt_query);

                    MessageBox.Show("Paket telah berhasil ditambahkan");
                    cbo_tbhpkt_jenispaket.SelectedItem = null;
                    txt_tbhpkt_namapaket.Clear();
                    txt_tbhpkt_durasipaketjam.Clear();
                    txt_tbhpkt_durasipaketmenit.Clear();
                    txt_tbhpkt_hargapaket.Clear();
                    txt_tbhpkt_komisipaketnormal.Clear();
                    txt_tbhpkt_komisipaketmidnight.Clear();
                }

            }
                #endregion
        }

        private void btn_tbhpkt_batal_Click(object sender, EventArgs e)
        {
            pnl_tbhpkt_isi.Visible = false;
            pnl_menu_isi.Visible = true;
        }

        private void txt_tbhpkt_durasipaketjam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_tbhpkt_durasipaketmenit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_tbhpkt_hargapaket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_tbhpkt_komisipaketnormal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_tbhpkt_komisipaketmidnight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region(Panel Edit Paket)
        List<string>[] edtpkt_lstnamapaket = new List<string>[2];
        List<int> edtpkt_lstidpktTerpilih = new List<int>();
        int edtpkt_idTerpilih = new int();
        string namapakettersimpan;
        private void btn_edtpkt_ok_Click(object sender, EventArgs e)
        {
            if (lsb_edtpkt_jenisnamapkt.SelectedItem != null)
            {
                string edtpkt_query;
                string edtpkt_jam;
                string edtpkt_menit;
                edtpkt_idTerpilih = edtpkt_lstidpktTerpilih[lsb_edtpkt_jenisnamapkt.SelectedIndex];
                namapakettersimpan = lsb_edtpkt_jenisnamapkt.SelectedItem.ToString();
                #region(Select)
                string edtpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                MySqlConnection edtpkt_conn = new MySqlConnection(edtpkt_connStr);
                List<string> edtpkt_lstKode = new List<string>();
                try
                {
                    edtpkt_conn.Open();

                    edtpkt_query = "SELECT * FROM `paket` WHERE `id_paket` = " + edtpkt_lstidpktTerpilih[lsb_edtpkt_jenisnamapkt.SelectedIndex] + "";
                    MySqlCommand edtpkt_cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                    MySqlDataReader edtpkt_rdr = edtpkt_cmd.ExecuteReader();

                    while (edtpkt_rdr.Read())
                    {

                        if (Char.IsDigit(edtpkt_rdr.GetString(3), 1) == true)
                        {
                            edtpkt_jam = edtpkt_rdr.GetString(3).Substring(0, 2);
                            if (Char.IsDigit(edtpkt_rdr.GetString(3), 8) == false)
                            {
                                edtpkt_menit = edtpkt_rdr.GetString(3).Substring(7, 1);
                            }
                            else
                            {
                                edtpkt_menit = edtpkt_rdr.GetString(3).Substring(7, 2);
                            }
                        }
                        else
                        {
                            edtpkt_jam = edtpkt_rdr.GetString(3).Substring(0, 1);
                            if (Char.IsDigit(edtpkt_rdr.GetString(3), 7) == false)
                            {
                                edtpkt_menit = edtpkt_rdr.GetString(3).Substring(6, 1);
                            }
                            else
                            {
                                edtpkt_menit = edtpkt_rdr.GetString(3).Substring(6, 2);
                            }
                        }

                        cbo_edtpkt_jenispaket.SelectedItem = edtpkt_rdr.GetString(1);
                        txt_edtpkt_namapaket.Text = edtpkt_rdr.GetString(2);
                        txt_edtpkt_komisipaketnormal.Text = edtpkt_rdr.GetString(5);
                        txt_edtpkt_komisipaketmidnight.Text = edtpkt_rdr.GetString(6);
                        txt_edtpkt_hargapaket.Text = edtpkt_rdr.GetString(4);
                        txt_edtpkt_durasipaketjam.Text = edtpkt_jam;
                        txt_edtpkt_durasipaketmenit.Text = edtpkt_menit;
                    }
                    edtpkt_rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                edtpkt_conn.Close();

                cbo_edtpkt_jenispaket.Enabled = true;
                txt_edtpkt_namapaket.Enabled = true;
                txt_edtpkt_komisipaketnormal.Enabled = true;
                txt_edtpkt_komisipaketmidnight.Enabled = true;
                txt_edtpkt_durasipaketjam.Enabled = true;
                txt_edtpkt_durasipaketmenit.Enabled = true;
                txt_edtpkt_hargapaket.Enabled = true;
                btn_edtpkt_simpan.Enabled = true;
                #endregion
            }
            else
            {
                MessageBox.Show("Mohon pilih Jenis dan Nama Paket terlebih dahulu!");
            }
        }

        private void btn_edtpkt_simpan_Click(object sender, EventArgs e)
        {
            string edtpkt_connStr;
            MySqlConnection edtpkt_conn;
            DBConnect edtpkt_sql = new DBConnect();
            string edtpkt_query;

            #region(Update ke database)
            if (txt_edtpkt_namapaket.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Nama Paket terlebih dahulu!");
            }
            else if (txt_edtpkt_komisipaketnormal.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Komisi Paket terlebih dahulu!");
            }
            else if (txt_edtpkt_komisipaketmidnight.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Komisi Paket terlebih dahulu!");
            }
            else if (txt_edtpkt_hargapaket.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Harga Paket terlebih dahulu!");
            }
            else if (txt_edtpkt_durasipaketjam.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Durasi Paket terlebih dahulu!");
            }
            else if (txt_edtpkt_durasipaketmenit.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Durasi Paket terlebih dahulu!");
            }
            else
            {
                bool namapaketsamadatabase = false;
                bool namapaketsamatersimpan = false;
                for (int i = 0; i < edtpkt_lstnamapaket[0].Count; i++)
                {
                    //if (edtpkt_lstnamapaket[0][i] != cbo_edtpkt_jenispaket.SelectedItem.ToString())
                    //{
                    //    edtpkt_lstnamapaket[0][i] = "";
                    //    edtpkt_lstnamapaket[1][i] = "";
                    //}
                    if (edtpkt_lstnamapaket[0][i] == cbo_edtpkt_jenispaket.SelectedItem.ToString())
                    {
                        if (txt_edtpkt_namapaket.Text.Replace(" ", String.Empty).ToUpper() == edtpkt_lstnamapaket[1][i].Trim().Replace(" ", String.Empty).ToUpper())
                        {
                            namapaketsamadatabase = true;
                            break;
                        }
                        else
                        {
                            namapaketsamadatabase = false;
                        }
                    }
                }
                if (namapakettersimpan.Replace(" ", String.Empty).ToUpper() == cbo_edtpkt_jenispaket.SelectedItem.ToString().ToUpper() + "-" + txt_edtpkt_namapaket.Text.Replace(" ", String.Empty).ToUpper())
                {
                    namapaketsamatersimpan = true;
                }
                else
                {
                    namapaketsamatersimpan = false;
                }
                //for (int i = 0; i < edtpkt_lstnamapaket[1].Count; i++)
                //{
                //    if (txt_edtpkt_namapaket.Text.Replace(" ", String.Empty).ToUpper() == edtpkt_lstnamapaket[1][i].Trim().Replace(" ", String.Empty).ToUpper())
                //    {
                //        namapaketsama = true;
                //        break;
                //    }
                //    else
                //    {
                //        namapaketsama = false;
                //    }
                //}
                if (namapaketsamatersimpan == true)
                {

                    #region(Update)
                    edtpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    edtpkt_conn = new MySqlConnection(edtpkt_connStr);
                    try
                    {
                        edtpkt_conn.Open();

                        edtpkt_query = "UPDATE `paket` SET `jenis_paket` = '" + cbo_edtpkt_jenispaket.SelectedItem + "', `nama_paket` = '" + txt_edtpkt_namapaket.Text + "', "
                                          + "`durasi_paket` = '" + txt_edtpkt_durasipaketjam.Text + " Jam " + txt_edtpkt_durasipaketmenit.Text + " Menit', `harga_paket` = '" + txt_edtpkt_hargapaket.Text + "', "
                                            + "`komisi_normal_paket` = '" + int.Parse(txt_edtpkt_komisipaketnormal.Text) + "', `komisi_midnight_paket` = '" + int.Parse(txt_edtpkt_komisipaketmidnight.Text) +
                                                "' WHERE `paket`.`id_paket` = " + edtpkt_idTerpilih + " ;";
                        MySqlCommand cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    edtpkt_conn.Close();
                    #endregion

                    cbo_edtpkt_jenispaket.Enabled = false;
                    txt_edtpkt_namapaket.Enabled = false;
                    txt_edtpkt_komisipaketnormal.Enabled = false;
                    txt_edtpkt_komisipaketmidnight.Enabled = false;
                    txt_edtpkt_durasipaketjam.Enabled = false;
                    txt_edtpkt_durasipaketmenit.Enabled = false;
                    txt_edtpkt_hargapaket.Enabled = false;
                    btn_edtpkt_simpan.Enabled = false;
                    lsb_edtpkt_jenisnamapkt.Items.Clear();

                    cbo_edtpkt_jenispaket.SelectedItem = null;
                    txt_edtpkt_namapaket.Clear();
                    txt_edtpkt_komisipaketnormal.Clear();
                    txt_edtpkt_komisipaketmidnight.Clear();
                    txt_edtpkt_durasipaketjam.Clear();
                    txt_edtpkt_durasipaketmenit.Clear();
                    txt_edtpkt_hargapaket.Clear();
                    btn_edtpkt_simpan.Enabled = false;
                    lsb_edtpkt_jenisnamapkt.Items.Clear();

                    edtpkt_lstidpktTerpilih.Clear();
                    #region(Isi listbox dengan Jenis dan Nama Paket per baris)
                    string edtpkt_jenisnamapkt;
                    string edtpkt_query2;
                    string edtpkt_connStr2 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    MySqlConnection edtpkt_conn2 = new MySqlConnection(edtpkt_connStr2);
                    try
                    {
                        edtpkt_conn2.Open();

                        edtpkt_query2 = "SELECT * FROM `paket` ORDER BY `id_paket` DESC";
                        MySqlCommand edtpkt_cmd2 = new MySqlCommand(edtpkt_query2, edtpkt_conn2);
                        MySqlDataReader edtpkt_rdr2 = edtpkt_cmd2.ExecuteReader();

                        while (edtpkt_rdr2.Read())
                        {
                            //cbo_kodeterapis.Items.Add(edtpkt_rdr.GetString(1));
                            edtpkt_lstidpktTerpilih.Add(edtpkt_rdr2.GetInt16(0));
                            edtpkt_jenisnamapkt = edtpkt_rdr2.GetString(1);
                            edtpkt_jenisnamapkt += " - " + edtpkt_rdr2.GetString(2);
                            lsb_edtpkt_jenisnamapkt.Items.Add(edtpkt_jenisnamapkt);
                            edtpkt_lstnamapaket[0].Add(edtpkt_rdr2.GetString(1));
                            edtpkt_lstnamapaket[1].Add(edtpkt_rdr2.GetString(2));
                        }
                        edtpkt_rdr2.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        MessageBox.Show("Error Occured");
                    }
                    edtpkt_conn2.Close();
                    #endregion



                    MessageBox.Show("Data Paket telah berhasil disimpan");
                }
                else
                {
                    if (namapaketsamadatabase == true)
                    {
                        MessageBox.Show("Maaf Nama Paket: " + txt_edtpkt_namapaket.Text + ", sudah ada di dalam daftar Paket: " + cbo_edtpkt_jenispaket.SelectedItem.ToString() + ", silahkan ganti nama paket");
                    }
                    else
                    {
                        #region(Update)
                        edtpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                        edtpkt_conn = new MySqlConnection(edtpkt_connStr);
                        try
                        {
                            edtpkt_conn.Open();

                            edtpkt_query = "UPDATE `paket` SET `jenis_paket` = '" + cbo_edtpkt_jenispaket.SelectedItem + "', `nama_paket` = '" + txt_edtpkt_namapaket.Text + "', "
                                              + "`durasi_paket` = '" + txt_edtpkt_durasipaketjam.Text + " Jam " + txt_edtpkt_durasipaketmenit.Text + " Menit', `harga_paket` = '" + txt_edtpkt_hargapaket.Text + "', "
                                                + "`komisi_normal_paket` = '" + int.Parse(txt_edtpkt_komisipaketnormal.Text) + "', `komisi_midnight_paket` = '" + int.Parse(txt_edtpkt_komisipaketmidnight.Text) +
                                                    "' WHERE `paket`.`id_paket` = " + edtpkt_idTerpilih + " ;";
                            MySqlCommand cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        edtpkt_conn.Close();
                        #endregion

                        cbo_edtpkt_jenispaket.Enabled = false;
                        txt_edtpkt_namapaket.Enabled = false;
                        txt_edtpkt_komisipaketnormal.Enabled = false;
                        txt_edtpkt_komisipaketmidnight.Enabled = false;
                        txt_edtpkt_durasipaketjam.Enabled = false;
                        txt_edtpkt_durasipaketmenit.Enabled = false;
                        txt_edtpkt_hargapaket.Enabled = false;
                        btn_edtpkt_simpan.Enabled = false;
                        lsb_edtpkt_jenisnamapkt.Items.Clear();

                        cbo_edtpkt_jenispaket.SelectedItem = null;
                        txt_edtpkt_namapaket.Clear();
                        txt_edtpkt_komisipaketnormal.Clear();
                        txt_edtpkt_komisipaketmidnight.Clear();
                        txt_edtpkt_durasipaketjam.Clear();
                        txt_edtpkt_durasipaketmenit.Clear();
                        txt_edtpkt_hargapaket.Clear();
                        btn_edtpkt_simpan.Enabled = false;
                        lsb_edtpkt_jenisnamapkt.Items.Clear();

                        edtpkt_lstidpktTerpilih.Clear();
                        #region(Isi listbox dengan Jenis dan Nama Paket per baris)
                        string edtpkt_jenisnamapkt;
                        string edtpkt_query2;
                        string edtpkt_connStr2 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                        MySqlConnection edtpkt_conn2 = new MySqlConnection(edtpkt_connStr2);
                        try
                        {
                            edtpkt_conn2.Open();

                            edtpkt_query2 = "SELECT * FROM `paket` ORDER BY `id_paket` DESC";
                            MySqlCommand edtpkt_cmd2 = new MySqlCommand(edtpkt_query2, edtpkt_conn2);
                            MySqlDataReader edtpkt_rdr2 = edtpkt_cmd2.ExecuteReader();

                            while (edtpkt_rdr2.Read())
                            {
                                //cbo_kodeterapis.Items.Add(edtpkt_rdr.GetString(1));
                                edtpkt_lstidpktTerpilih.Add(edtpkt_rdr2.GetInt16(0));
                                edtpkt_jenisnamapkt = edtpkt_rdr2.GetString(1);
                                edtpkt_jenisnamapkt += " - " + edtpkt_rdr2.GetString(2);
                                lsb_edtpkt_jenisnamapkt.Items.Add(edtpkt_jenisnamapkt);
                                edtpkt_lstnamapaket[0].Add(edtpkt_rdr2.GetString(1));
                                edtpkt_lstnamapaket[1].Add(edtpkt_rdr2.GetString(2));
                            }
                            edtpkt_rdr2.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            MessageBox.Show("Error Occured");
                        }
                        edtpkt_conn2.Close();
                        #endregion



                        MessageBox.Show("Data Paket telah berhasil disimpan");
                    }
                }
                //#region(Buat huruf besar untuk Jam dan Menit)
                //string edtpkt_durasiPaket = txt_edtpkt_durasipaket.Text;
                //var regex = new Regex(@"\b[A-Z]", RegexOptions.IgnoreCase);
                //edtpkt_durasiPaket = regex.Replace(edtpkt_durasiPaket, m => m.ToString().ToUpper());
                //#endregion


            }
            #endregion
        }

        private void txt_edtpkt_komisipaketnormal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_edtpkt_komisipaketmidnight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_edtpkt_hargapaket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_edtpkt_durasipaketjam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_edtpkt_durasipaketmenit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_edtpkt_batal_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = true;
            pnl_edtpkt_isi.Visible = false;
        }
        #endregion

        #region(Panel Cetak Nota)
        DataSet ctknota_DS = new DataSet();
        int ctknota_tamuhotel;
        int ctknota_extra;
        int ctknota_countExtraColumn;
        private void rdo_ctknota_biasa_CheckedChanged(object sender, EventArgs e)
        {
            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = false;

            cbo_ctknota_jenispaket.Items.Clear();
            #region(Select)
            string ctknota_query;
            string ctknota_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection ctknota_conn = new MySqlConnection(ctknota_connStr);
            List<string> ctknota_lstKode = new List<string>();
            try
            {
                ctknota_conn.Open();

                ctknota_query = "SELECT DISTINCT `jenis_paket` FROM `paket`";
                MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                MySqlDataReader ctknota_rdr = ctknota_cmd.ExecuteReader();

                while (ctknota_rdr.Read())
                {
                    //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                    cbo_ctknota_jenispaket.Items.Add(ctknota_rdr.GetString(0));
                }
                ctknota_rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            ctknota_conn.Close();
            #endregion
            //dtEmp.Columns.Add("Nama Paket", typeof(string));
            //dtEmp.Columns.Add("Durasi Paket", typeof(string));
            //dtEmp.Columns.Add("Harga Paket", typeof(string));
            //dtEmp.Columns.Add("Extra", typeof(bool));
            //dtEmp.Columns.Add("Pilih", typeof(bool));

            //dtEmp.Rows.Add("Traditional Massage", "1 Jam 30 Menit" , "Rp. 500.000,-", false,false);
            //dtEmp.Rows.Add("Traditional Massage", "1 Jam 30 Menit", "Rp. 500.000,-", false, false);
            //dgv_ctknota_tabelhrgpkt.DataSource = dtEmp; 
        }

        private void rdo_ctknota_hotel_CheckedChanged(object sender, EventArgs e)
        {
            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = false;


            cbo_ctknota_jenispaket.Items.Clear();
            #region(Select)
            string ctknota_query;
            string ctknota_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection ctknota_conn = new MySqlConnection(ctknota_connStr);
            List<string> ctknota_lstKode = new List<string>();
            try
            {
                ctknota_conn.Open();

                ctknota_query = "SELECT DISTINCT `jenis_paket` FROM `paket` WHERE `jenis_paket` NOT IN (SELECT DISTINCT `jenis_paket` FROM `paket` WHERE `jenis_paket`='Deluxe')";
                MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                MySqlDataReader ctknota_rdr = ctknota_cmd.ExecuteReader();

                while (ctknota_rdr.Read())
                {
                    //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                    cbo_ctknota_jenispaket.Items.Add(ctknota_rdr.GetString(0));
                }
                ctknota_rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            ctknota_conn.Close();
            #endregion
            //dtEmp.Columns.Add("Tamu Hotel", typeof(string));
        }

        private void rdo_ctknota_normal_CheckedChanged(object sender, EventArgs e)
        {
            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = false;
        }

        private void rdo_ctknota_midnight_CheckedChanged(object sender, EventArgs e)
        {
            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = false;
        }

        private void cbo_ctknota_jenispaket_SelectedIndexChanged(object sender, EventArgs e)
        {

            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = true;
            rdo_ctknota_cash.Enabled = true;
            rdo_ctknota_credit.Enabled = true;

            //dtEmp.Columns.Add("Nama Paket", typeof(string));
            //dtEmp.Columns.Add("Durasi Paket", typeof(string));
            //dtEmp.Columns.Add("Harga Paket", typeof(string));
            //dtEmp.Columns.Add("Extra", typeof(bool));
            //dtEmp.Columns.Add("Pilih", typeof(bool));

            dgv_ctknota_tabelhrgpkt.DataSource = null;
            dgv_ctknota_tabelhrgpkt.Rows.Clear();
            dgv_ctknota_tabelhrgpkt.Refresh();
            ctknota_DS.Tables.Clear();
            ctknota_countExtraColumn = 0;

            if (rdo_ctknota_normal.Checked)
            {
                if (rdo_ctknota_hotel.Checked)
                {
                    #region(Select)
                    string ctknota_query;
                    string ctknota_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    MySqlConnection ctknota_conn = new MySqlConnection(ctknota_connStr);
                    List<string> ctknota_lstKode = new List<string>();
                    try
                    {
                        ctknota_conn.Open();
                        ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = '" + cbo_ctknota_jenispaket.SelectedItem + "'";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(ctknota_query, ctknota_conn);
                        MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);

                        mySqlDataAdapter.Fill(ctknota_DS);
                        ctknota_DS.Tables[0].Columns.Remove("id_paket");
                        ctknota_DS.Tables[0].Columns.Remove("jenis_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        ctknota_DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        ctknota_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        ctknota_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                        for (int i = 0; i < ctknota_DS.Tables[0].Rows.Count; i++)
                        {
                            ctknota_DS.Tables[0].Rows[i]["Tamu Hotel"] = ctknota_tamuhotel;
                        }

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                        ctknota_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                        for (int ii = 0; ii < ctknota_DS.Tables[0].Rows.Count; ii++)
                        {
                            ctknota_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                        }

                        dgv_ctknota_tabelhrgpkt.DataSource = ctknota_DS.Tables[0];

                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;
                        dgv_ctknota_tabelhrgpkt.Columns["Pilih"].Width = 70;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].Width = 300;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].Width = 200;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;

                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].SortMode = DataGridViewColumnSortMode.NotSortable;

                        List<string> lstExtra = new List<string>();
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            double hasil = nominal * Convert.ToDouble(harga);
                            lstExtra.Add(hasil.ToString());
                            //MessageBox.Show(hasilfinal.ToString());
                            //foreach (char c in extra)
                            //{
                            //    if (char.IsDigit(c))
                            //    {
                            //        countdigitextra++;
                            //    }
                            //}
                            //int digit = countdigitextra;
                            //while (digit > 3)
                            //{
                            //    digit -= 3;
                            //    extra = extra.Insert(digit, ".");
                            //    dsCloned.Tables[0].Rows[i]["Nominal Extra"] = extra;
                            //}

                        }

                        DataSet dsCloned = ctknota_DS.Clone();

                        dsCloned.Tables[0].Columns["Harga Paket"].DataType = typeof(string);
                        dsCloned.Tables[0].Columns["Tamu Hotel"].DataType = typeof(string);
                        dsCloned.Tables[0].Columns["Nominal Extra"].DataType = typeof(string);
                        foreach (DataRow row in ctknota_DS.Tables[0].Rows)
                        {
                            dsCloned.Tables[0].ImportRow(row);
                        }

                        dgv_ctknota_tabelhrgpkt.DataSource = dsCloned.Tables[0];

                        int countdigitharga = 0;
                        string hargapaket;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitharga = 0;
                            hargapaket = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString();
                            foreach (char c in hargapaket)
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitharga++;
                                }
                            }
                            int digit = countdigitharga;
                            while (digit > 3)
                            {
                                digit -= 3;
                                hargapaket = hargapaket.Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Harga Paket"] = hargapaket;
                            }

                        }

                        int countdigittamu = 0;
                        string tamuhotel;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigittamu = 0;
                            tamuhotel = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString();
                            foreach (char c in tamuhotel)
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigittamu++;
                                }
                            }
                            int digit = countdigittamu;
                            while (digit > 3)
                            {
                                digit -= 3;
                                tamuhotel = tamuhotel.Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                            }

                        }

                        int countdigitextra = 0;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitextra = 0;
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            //double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            //int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //double hasil = nominal * Convert.ToDouble(harga);
                            //extra = hasil.ToString();
                            //MessageBox.Show(hasilfinal.ToString());
                            foreach (char c in lstExtra[i])
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitextra++;
                                }
                            }
                            int digit = countdigitextra;
                            while (digit > 3)
                            {
                                digit -= 3;
                                lstExtra[i] = lstExtra[i].Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Nominal Extra"] = lstExtra[i];
                            }

                        }


                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                        MessageBox.Show("Error Occurred");
                    }
                    ctknota_conn.Close();
                    #endregion
                }
                else if (rdo_ctknota_biasa.Checked)
                {
                    #region(Select)
                    string ctknota_query;
                    string ctknota_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    MySqlConnection ctknota_conn = new MySqlConnection(ctknota_connStr);
                    List<string> ctknota_lstKode = new List<string>();
                    try
                    {
                        ctknota_conn.Open();
                        ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = '" + cbo_ctknota_jenispaket.SelectedItem + "'";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(ctknota_query, ctknota_conn);
                        MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                        mySqlDataAdapter.Fill(ctknota_DS);
                        ctknota_DS.Tables[0].Columns.Remove("id_paket");
                        ctknota_DS.Tables[0].Columns.Remove("jenis_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        ctknota_DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        ctknota_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        ctknota_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        //DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                        //for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        //{
                        //    DS.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                        //}

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                        ctknota_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                        dgv_ctknota_tabelhrgpkt.DataSource = ctknota_DS.Tables[0];
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Pilih"].Width = 70;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].Width = 300;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].Width = 200;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Paket"].ReadOnly = true;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].Width = 200;
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;



                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                        for (int ii = 0; ii < ctknota_DS.Tables[0].Rows.Count; ii++)
                        {
                            ctknota_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                        }
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;

                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].SortMode = DataGridViewColumnSortMode.NotSortable;

                        List<string> lstExtra = new List<string>();
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            double hasil = nominal * Convert.ToDouble(harga);
                            lstExtra.Add(hasil.ToString());
                            //MessageBox.Show(hasilfinal.ToString());
                            //foreach (char c in extra)
                            //{
                            //    if (char.IsDigit(c))
                            //    {
                            //        countdigitextra++;
                            //    }
                            //}
                            //int digit = countdigitextra;
                            //while (digit > 3)
                            //{
                            //    digit -= 3;
                            //    extra = extra.Insert(digit, ".");
                            //    dsCloned.Tables[0].Rows[i]["Nominal Extra"] = extra;
                            //}

                        }

                        DataSet dsCloned = ctknota_DS.Clone();

                        dsCloned.Tables[0].Columns["Harga Paket"].DataType = typeof(string);
                        //dsCloned.Tables[0].Columns["Tamu Hotel"].DataType = typeof(string);
                        dsCloned.Tables[0].Columns["Nominal Extra"].DataType = typeof(string);
                        foreach (DataRow row in ctknota_DS.Tables[0].Rows)
                        {
                            dsCloned.Tables[0].ImportRow(row);
                        }

                        dgv_ctknota_tabelhrgpkt.DataSource = dsCloned.Tables[0];

                        int countdigitharga = 0;
                        string hargapaket;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitharga = 0;
                            hargapaket = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString();
                            foreach (char c in hargapaket)
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitharga++;
                                }
                            }
                            int digit = countdigitharga;
                            while (digit > 3)
                            {
                                digit -= 3;
                                hargapaket = hargapaket.Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Harga Paket"] = hargapaket;
                            }

                        }

                        //int countdigittamu = 0;
                        //string tamuhotel;
                        //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        //{
                        //    countdigittamu = 0;
                        //    tamuhotel = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString();
                        //    foreach (char c in tamuhotel)
                        //    {
                        //        if (char.IsDigit(c))
                        //        {
                        //            countdigittamu++;
                        //        }
                        //    }
                        //    int digit = countdigittamu;
                        //    while (digit > 3)
                        //    {
                        //        digit -= 3;
                        //        tamuhotel = tamuhotel.Insert(digit, ".");
                        //        dsCloned.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                        //    }

                        //}

                        int countdigitextra = 0;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitextra = 0;
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            //double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            //int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //double hasil = nominal * Convert.ToDouble(harga);
                            //extra = hasil.ToString();
                            //MessageBox.Show(hasilfinal.ToString());
                            foreach (char c in lstExtra[i])
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitextra++;
                                }
                            }
                            int digit = countdigitextra;
                            while (digit > 3)
                            {
                                digit -= 3;
                                lstExtra[i] = lstExtra[i].Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Nominal Extra"] = lstExtra[i];
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                        MessageBox.Show("Error Occurred");
                    }
                    ctknota_conn.Close();
                    #endregion
                }
            }
            else if (rdo_ctknota_midnight.Checked)
            {
                if (rdo_ctknota_hotel.Checked)
                {
                    #region(Select)
                    string ctknota_query;
                    string ctknota_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    MySqlConnection ctknota_conn = new MySqlConnection(ctknota_connStr);
                    List<string> ctknota_lstKode = new List<string>();
                    try
                    {
                        ctknota_conn.Open();

                        ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = '" + cbo_ctknota_jenispaket.SelectedItem + "'";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(ctknota_query, ctknota_conn);
                        MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                        mySqlDataAdapter.Fill(ctknota_DS);
                        ctknota_DS.Tables[0].Columns.Remove("id_paket");
                        ctknota_DS.Tables[0].Columns.Remove("jenis_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        ctknota_DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        ctknota_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        ctknota_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                        //int i = 0;
                        //int[] listI = new int[1];
                        //listI[0] = 100000;
                        //foreach (int somevalue in listI)
                        //{
                        //    DS.Tables[0].Rows[i++][1] = somevalue;  //Add value to second column of each row
                        //}
                        for (int i = 0; i < ctknota_DS.Tables[0].Rows.Count; i++)
                        {
                            ctknota_DS.Tables[0].Rows[i]["Tamu Hotel"] = ctknota_tamuhotel;
                        }

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                        ctknota_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                        dgv_ctknota_tabelhrgpkt.DataSource = ctknota_DS.Tables[0];
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Pilih"].Width = 70;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].Width = 300;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].Width = 200;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;



                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                        for (int ii = 0; ii < ctknota_DS.Tables[0].Rows.Count; ii++)
                        {
                            ctknota_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                        }
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;

                        List<string> lstExtra = new List<string>();
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            double hasil = nominal * Convert.ToDouble(harga);
                            lstExtra.Add(hasil.ToString());
                            //MessageBox.Show(hasilfinal.ToString());
                            //foreach (char c in extra)
                            //{
                            //    if (char.IsDigit(c))
                            //    {
                            //        countdigitextra++;
                            //    }
                            //}
                            //int digit = countdigitextra;
                            //while (digit > 3)
                            //{
                            //    digit -= 3;
                            //    extra = extra.Insert(digit, ".");
                            //    dsCloned.Tables[0].Rows[i]["Nominal Extra"] = extra;
                            //}

                        }

                        DataSet dsCloned = ctknota_DS.Clone();

                        dsCloned.Tables[0].Columns["Harga Paket"].DataType = typeof(string);
                        dsCloned.Tables[0].Columns["Tamu Hotel"].DataType = typeof(string);
                        dsCloned.Tables[0].Columns["Nominal Extra"].DataType = typeof(string);

                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        foreach (DataRow row in ctknota_DS.Tables[0].Rows)
                        {
                            dsCloned.Tables[0].ImportRow(row);
                        }

                        dgv_ctknota_tabelhrgpkt.DataSource = dsCloned.Tables[0];

                        int countdigitharga = 0;
                        string hargapaket;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitharga = 0;
                            hargapaket = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString();
                            foreach (char c in hargapaket)
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitharga++;
                                }
                            }
                            int digit = countdigitharga;
                            while (digit > 3)
                            {
                                digit -= 3;
                                hargapaket = hargapaket.Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Harga Paket"] = hargapaket;
                            }

                        }

                        int countdigittamu = 0;
                        string tamuhotel;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigittamu = 0;
                            tamuhotel = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString();
                            foreach (char c in tamuhotel)
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigittamu++;
                                }
                            }
                            int digit = countdigittamu;
                            while (digit > 3)
                            {
                                digit -= 3;
                                tamuhotel = tamuhotel.Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                            }

                        }

                        int countdigitextra = 0;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitextra = 0;
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            //double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            //int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //double hasil = nominal * Convert.ToDouble(harga);
                            //extra = hasil.ToString();
                            //MessageBox.Show(hasilfinal.ToString());
                            foreach (char c in lstExtra[i])
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitextra++;
                                }
                            }
                            int digit = countdigitextra;
                            while (digit > 3)
                            {
                                digit -= 3;
                                lstExtra[i] = lstExtra[i].Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Nominal Extra"] = lstExtra[i];
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                        MessageBox.Show("Error Occurred");
                    }
                    ctknota_conn.Close();
                    #endregion
                }
                else if (rdo_ctknota_biasa.Checked)
                {
                    #region(Select)
                    string ctknota_query;
                    string ctknota_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    MySqlConnection ctknota_conn = new MySqlConnection(ctknota_connStr);
                    List<string> ctknota_lstKode = new List<string>();
                    try
                    {
                        ctknota_conn.Open();

                        ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = '" + cbo_ctknota_jenispaket.SelectedItem + "'";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(ctknota_query, ctknota_conn);
                        MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                        mySqlDataAdapter.Fill(ctknota_DS);
                        ctknota_DS.Tables[0].Columns.Remove("id_paket");
                        ctknota_DS.Tables[0].Columns.Remove("jenis_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        ctknota_DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        ctknota_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        ctknota_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        //DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                        //for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        //{
                        //    DS.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                        //}

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                        ctknota_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                        dgv_ctknota_tabelhrgpkt.DataSource = ctknota_DS.Tables[0];
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Pilih"].Width = 70;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].Width = 300;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].Width = 200;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Paket"].ReadOnly = true;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].Width = 200;
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;



                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                        for (int ii = 0; ii < ctknota_DS.Tables[0].Rows.Count; ii++)
                        {
                            ctknota_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                        }
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].SortMode = DataGridViewColumnSortMode.NotSortable;

                        List<string> lstExtra = new List<string>();
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            double hasil = nominal * Convert.ToDouble(harga);
                            lstExtra.Add(hasil.ToString());
                            //MessageBox.Show(hasilfinal.ToString());
                            //foreach (char c in extra)
                            //{
                            //    if (char.IsDigit(c))
                            //    {
                            //        countdigitextra++;
                            //    }
                            //}
                            //int digit = countdigitextra;
                            //while (digit > 3)
                            //{
                            //    digit -= 3;
                            //    extra = extra.Insert(digit, ".");
                            //    dsCloned.Tables[0].Rows[i]["Nominal Extra"] = extra;
                            //}

                        }

                        DataSet dsCloned = ctknota_DS.Clone();

                        dsCloned.Tables[0].Columns["Harga Paket"].DataType = typeof(string);
                        //dsCloned.Tables[0].Columns["Tamu Hotel"].DataType = typeof(string);
                        dsCloned.Tables[0].Columns["Nominal Extra"].DataType = typeof(string);
                        foreach (DataRow row in ctknota_DS.Tables[0].Rows)
                        {
                            dsCloned.Tables[0].ImportRow(row);
                        }

                        dgv_ctknota_tabelhrgpkt.DataSource = dsCloned.Tables[0];

                        int countdigitharga = 0;
                        string hargapaket;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitharga = 0;
                            hargapaket = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString();
                            foreach (char c in hargapaket)
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitharga++;
                                }
                            }
                            int digit = countdigitharga;
                            while (digit > 3)
                            {
                                digit -= 3;
                                hargapaket = hargapaket.Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Harga Paket"] = hargapaket;
                            }

                        }

                        //int countdigittamu = 0;
                        //string tamuhotel;
                        //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        //{
                        //    countdigittamu = 0;
                        //    tamuhotel = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString();
                        //    foreach (char c in tamuhotel)
                        //    {
                        //        if (char.IsDigit(c))
                        //        {
                        //            countdigittamu++;
                        //        }
                        //    }
                        //    int digit = countdigittamu;
                        //    while (digit > 3)
                        //    {
                        //        digit -= 3;
                        //        tamuhotel = tamuhotel.Insert(digit, ".");
                        //        dsCloned.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                        //    }

                        //}

                        int countdigitextra = 0;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitextra = 0;
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            //double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            //int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //double hasil = nominal * Convert.ToDouble(harga);
                            //extra = hasil.ToString();
                            //MessageBox.Show(hasilfinal.ToString());
                            foreach (char c in lstExtra[i])
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitextra++;
                                }
                            }
                            int digit = countdigitextra;
                            while (digit > 3)
                            {
                                digit -= 3;
                                lstExtra[i] = lstExtra[i].Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Nominal Extra"] = lstExtra[i];
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                        MessageBox.Show("Error Occurred");
                    }
                    ctknota_conn.Close();
                    #endregion
                }
            }


        }

        private void rdo_ctknota_cash_CheckedChanged(object sender, EventArgs e)
        {
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = true;
            rdo_ctknota_cash.Enabled = true;
            rdo_ctknota_credit.Enabled = true;
            cbo_ctknota_kodeterapis.Enabled = true;
            txt_ctknota_namaterapis.Enabled = true;
            txt_ctknota_diskon.Enabled = true;
            txt_ctknota_ket.Enabled = true;
            txt_ctknota_fee.Enabled = true;
            txt_ctknota_nomorruangan.Enabled = true;

            btn_ctknota_cetak.Enabled = true;
            btn_ctknota_batal.Enabled = true;
        }

        private void rdo_ctknota_credit_CheckedChanged(object sender, EventArgs e)
        {
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = true;
            rdo_ctknota_cash.Enabled = true;
            rdo_ctknota_credit.Enabled = true;
            cbo_ctknota_kodeterapis.Enabled = true;
            txt_ctknota_namaterapis.Enabled = true;
            txt_ctknota_diskon.Enabled = true;
            txt_ctknota_ket.Enabled = true;
            txt_ctknota_fee.Enabled = true;
            txt_ctknota_nomorruangan.Enabled = true;

            btn_ctknota_cetak.Enabled = true;
            btn_ctknota_batal.Enabled = true;
        }

        private void cbo_ctknota_kodeterapis_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = true;
            rdo_ctknota_cash.Enabled = true;
            rdo_ctknota_credit.Enabled = true;
            cbo_ctknota_kodeterapis.Enabled = true;
            txt_ctknota_namaterapis.Enabled = true;
            txt_ctknota_diskon.Enabled = true;
            txt_ctknota_ket.Enabled = true;
            txt_ctknota_fee.Enabled = true;
            txt_ctknota_nomorruangan.Enabled = true;
            btn_ctknota_batal.Enabled = true;

            #region(Select)
            string ctknota_query3;
            string ctknota_connStr3 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection ctknota_conn3 = new MySqlConnection(ctknota_connStr3);
            try
            {
                ctknota_conn3.Open();

                ctknota_query3 = "SELECT * FROM `terapis` WHERE `kode_terapis` = '" + cbo_ctknota_kodeterapis.SelectedItem + "'";
                MySqlCommand ctknota_cmd3 = new MySqlCommand(ctknota_query3, ctknota_conn3);
                MySqlDataReader ctknota_rdr3 = ctknota_cmd3.ExecuteReader();

                while (ctknota_rdr3.Read())
                {
                    //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                    txt_ctknota_namaterapis.Text = ctknota_rdr3.GetString(2);
                }
                ctknota_rdr3.Close();
            }
            catch (Exception ex)
            {
                string Hasilex = ex.ToString();
                MessageBox.Show("Error Occured");
            }
            ctknota_conn3.Close();
            #endregion
        }

        private void txt_ctknota_diskon_TextChanged(object sender, EventArgs e)
        {
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = true;
            rdo_ctknota_cash.Enabled = true;
            rdo_ctknota_credit.Enabled = true;
            cbo_ctknota_kodeterapis.Enabled = true;
            txt_ctknota_namaterapis.Enabled = true;
            txt_ctknota_diskon.Enabled = true;
            txt_ctknota_ket.Enabled = true;
            txt_ctknota_fee.Enabled = true;
            txt_ctknota_nomorruangan.Enabled = true;
            btn_ctknota_batal.Enabled = true;
        }

        private void txt_ctknota_diskon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        private void btn_ctknota_cetak_Click(object sender, EventArgs e)
        {
            if (cbo_ctknota_kodeterapis.SelectedItem == null)
            {
                MessageBox.Show("Mohon pilih kode terapis terlebih dahulu terlebih dahulu");
            }
            else if (txt_ctknota_nomorruangan.Text == "")
            {
                MessageBox.Show("Mohon lengkapi kolom nomor ruangan terlebih dahulu");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Apakah anda sudah yakin?", "Alert", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    bool cekdgvcheckbox = true;
                    for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                    {
                        if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value.ToString() == "")
                        {
                            cekdgvcheckbox = false;
                        }
                        else
                        {
                            cekdgvcheckbox = true;
                            break;
                        }
                    }
                    if (cekdgvcheckbox == true)
                    {
                        if (txt_ctknota_diskon.Text == "")
                        {
                            #region(cek)
                            if (txt_ctknota_fee.Text == "")
                            {
                                #region(Insert Nota)
                                string tanggalcetak = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                                string jamkerja = "";
                                string tamuhotel = "";
                                int potonganhotel = 0; ;
                                int nomorruangan = 0;
                                string namapaket = "";
                                int hargapaket = 0;
                                string extra = "";
                                int nominalextra = 0;
                                int kodeterapis = 0;
                                string namaterapis = "";
                                int diskon = 0;
                                string ket = "";
                                int fee = 0;
                                int totalbayar = 0;
                                string jenisbayar = "";
                                //header.Trim(new Char[] { ' ', '*', '.' });
                                for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                                {
                                    if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value.ToString() == "True")
                                    {
                                        if (rdo_ctknota_normal.Checked)
                                        {
                                            jamkerja = "Normal";
                                        }
                                        else if (rdo_ctknota_midnight.Checked)
                                        {
                                            jamkerja = "Midnight";
                                        }
                                        if (rdo_ctknota_biasa.Checked)
                                        {
                                            tamuhotel = "Tidak";
                                            potonganhotel = 0;
                                        }
                                        else if (rdo_ctknota_hotel.Checked)
                                        {
                                            tamuhotel = "Ya";
                                            potonganhotel = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString().Replace(".", string.Empty));
                                        }
                                        nomorruangan = int.Parse(txt_ctknota_nomorruangan.Text);
                                        namapaket = cbo_ctknota_jenispaket.SelectedItem + " - " + dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nama Paket"].Value.ToString();
                                        hargapaket = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString().Replace(".", string.Empty));
                                        if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "True")
                                        {
                                            extra = "Ya";
                                            nominalextra = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString().Replace(".", string.Empty));
                                        }
                                        else if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "False")
                                        {
                                            extra = "Tidak";
                                            nominalextra = 0;
                                        }
                                        kodeterapis = int.Parse(cbo_ctknota_kodeterapis.SelectedItem.ToString());
                                        namaterapis = txt_ctknota_namaterapis.Text;
                                        diskon = 0;
                                        ket = "";
                                        fee = 0;
                                        totalbayar = hargapaket - potonganhotel + nominalextra;
                                        if (rdo_ctknota_cash.Checked)
                                        {
                                            jenisbayar = "Cash";
                                        }
                                        else if (rdo_ctknota_credit.Checked)
                                        {
                                            jenisbayar = "Credit";
                                        }
                                    }
                                }

                                DBConnect ctknota_sql = new DBConnect();
                                string ctknota_query = "INSERT INTO `nota` (`id_nota`, `tanggalcetak_nota`, `nomorruangan_nota`, `jamkerja_nota`, `tamuhotel_nota`, `potonganhotel_nota`, "
                                                            + "`namapaket_nota`, `hargapaket_nota`, `extra_nota`, `nominalextra_nota`, `kodeterapis_nota`, `namaterapis_nota`,"
                                                                + " `diskon_nota`, `keterangan_nota`, `totalbayar_nota`, `feeterapis_nota`, `jenisbayar_nota`, `status_nota`) "
                                                                    + "VALUES (NULL, '" + tanggalcetak + "', '" + nomorruangan + "', '" + jamkerja + "', '" + tamuhotel + "', '" + potonganhotel + "',"
                                                                        + " '" + namapaket + "', '" + hargapaket + "', '" + extra + "', '" + nominalextra + "', '" + kodeterapis + "', '" + namaterapis + "', "
                                                                            + "'" + diskon + "', '" + ket + "', '" + totalbayar + "', '" + fee + "', '" + jenisbayar + "', '-');";
                                ctknota_sql.Insert(ctknota_query);


                                #endregion
                                string totalbayarFinal = "";
                                int countdigittotal = 0;
                                foreach (char c in totalbayar.ToString())
                                {
                                    if (char.IsDigit(c))
                                    {
                                        countdigittotal++;
                                    }
                                }
                                int digit = countdigittotal;
                                int countdigitend = 0;
                                while (digit > 3)
                                {
                                    countdigitend++;
                                    digit -= 3;
                                    totalbayarFinal = totalbayar.ToString().Insert(digit, ".");
                                }
                                totalbayarFinal = totalbayarFinal.Insert(countdigittotal + countdigitend, ",-");
                                lbl_ctknota_totalbyr.Text = totalbayarFinal;
                                MessageBox.Show("Nota telah berhasil ditambahkan");
                                //MessageBox.Show("diskon kosong, fee kosong");
                            }
                            else
                            {
                                #region(Insert Nota)
                                string tanggalcetak = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                                string jamkerja = "";
                                string tamuhotel = "";
                                int potonganhotel = 0; ;
                                int nomorruangan = 0;
                                string namapaket = "";
                                int hargapaket = 0;
                                string extra = "";
                                int nominalextra = 0;
                                int kodeterapis = 0;
                                string namaterapis = "";
                                int diskon = 0;
                                string ket = "";
                                int fee = 0;
                                int totalbayar = 0;
                                string jenisbayar = "";
                                //header.Trim(new Char[] { ' ', '*', '.' });
                                for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                                {
                                    if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value.ToString() == "True")
                                    {
                                        if (rdo_ctknota_normal.Checked)
                                        {
                                            jamkerja = "Normal";
                                        }
                                        else if (rdo_ctknota_midnight.Checked)
                                        {
                                            jamkerja = "Midnight";
                                        }
                                        if (rdo_ctknota_biasa.Checked)
                                        {
                                            tamuhotel = "Tidak";
                                            potonganhotel = 0;
                                        }
                                        else if (rdo_ctknota_hotel.Checked)
                                        {
                                            tamuhotel = "Ya";
                                            potonganhotel = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString().Replace(".", string.Empty));
                                        }
                                        nomorruangan = int.Parse(txt_ctknota_nomorruangan.Text);
                                        namapaket = cbo_ctknota_jenispaket.SelectedItem + " - " + dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nama Paket"].Value.ToString();
                                        hargapaket = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString().Replace(".", string.Empty));
                                        if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "True")
                                        {
                                            extra = "Ya";
                                            nominalextra = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString().Replace(".", string.Empty));
                                        }
                                        else if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "False")
                                        {
                                            extra = "Tidak";
                                            nominalextra = 0;
                                        }
                                        kodeterapis = int.Parse(cbo_ctknota_kodeterapis.SelectedItem.ToString());
                                        namaterapis = txt_ctknota_namaterapis.Text;
                                        diskon = 0;
                                        ket = "";
                                        fee = int.Parse(txt_ctknota_fee.Text);
                                        totalbayar = hargapaket - potonganhotel + nominalextra;
                                        if (rdo_ctknota_cash.Checked)
                                        {
                                            jenisbayar = "Cash";
                                        }
                                        else if (rdo_ctknota_credit.Checked)
                                        {
                                            jenisbayar = "Credit";
                                        }
                                    }
                                }

                                DBConnect ctknota_sql = new DBConnect();
                                string ctknota_query = "INSERT INTO `nota` (`id_nota`, `tanggalcetak_nota`, `nomorruangan_nota`, `jamkerja_nota`, `tamuhotel_nota`, `potonganhotel_nota`, "
                                                            + "`namapaket_nota`, `hargapaket_nota`, `extra_nota`, `nominalextra_nota`, `kodeterapis_nota`, `namaterapis_nota`,"
                                                                + " `diskon_nota`, `keterangan_nota`, `totalbayar_nota`, `feeterapis_nota`, `jenisbayar_nota`, `status_nota`) "
                                                                    + "VALUES (NULL, '" + tanggalcetak + "', '" + nomorruangan + "', '" + jamkerja + "', '" + tamuhotel + "', '" + potonganhotel + "',"
                                                                        + " '" + namapaket + "', '" + hargapaket + "', '" + extra + "', '" + nominalextra + "', '" + kodeterapis + "', '" + namaterapis + "', "
                                                                            + "'" + diskon + "', '" + ket + "', '" + totalbayar + "', '" + fee + "', '" + jenisbayar + "', '-');";
                                ctknota_sql.Insert(ctknota_query);


                                #endregion
                                string totalbayarFinal = "";
                                int countdigittotal = 0;
                                foreach (char c in totalbayar.ToString())
                                {
                                    if (char.IsDigit(c))
                                    {
                                        countdigittotal++;
                                    }
                                }
                                int digit = countdigittotal;
                                int countdigitend = 0;
                                while (digit > 3)
                                {
                                    countdigitend++;
                                    digit -= 3;
                                    totalbayarFinal = totalbayar.ToString().Insert(digit, ".");
                                }
                                totalbayarFinal = totalbayarFinal.Insert(countdigittotal + countdigitend, ",-");
                                lbl_ctknota_totalbyr.Text = totalbayarFinal;
                                MessageBox.Show("Nota telah berhasil ditambahkan");
                                //MessageBox.Show("diskon kosong, fee ada");
                            }
                        }
                        else
                        {
                            if (txt_ctknota_ket.Text == "")
                            {
                                MessageBox.Show("Mohon lengkapi keterangan diskon terlebih dahulu");
                            }
                            else
                            {
                                if (txt_ctknota_fee.Text == "")
                                {
                                    #region(Insert Nota)
                                    string tanggalcetak = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                                    string jamkerja = "";
                                    string tamuhotel = "";
                                    int potonganhotel = 0; ;
                                    int nomorruangan = 0;
                                    string namapaket = "";
                                    int hargapaket = 0;
                                    string extra = "";
                                    int nominalextra = 0;
                                    int kodeterapis = 0;
                                    string namaterapis = "";
                                    int diskon = 0;
                                    string ket = "";
                                    int fee = 0;
                                    int totalbayar = 0;
                                    string jenisbayar = "";
                                    //header.Trim(new Char[] { ' ', '*', '.' });
                                    for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                                    {
                                        if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value.ToString() == "True")
                                        {
                                            if (rdo_ctknota_normal.Checked)
                                            {
                                                jamkerja = "Normal";
                                            }
                                            else if (rdo_ctknota_midnight.Checked)
                                            {
                                                jamkerja = "Midnight";
                                            }
                                            if (rdo_ctknota_biasa.Checked)
                                            {
                                                tamuhotel = "Tidak";
                                                potonganhotel = 0;
                                            }
                                            else if (rdo_ctknota_hotel.Checked)
                                            {
                                                tamuhotel = "Ya";
                                                potonganhotel = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString().Replace(".", string.Empty));
                                            }
                                            nomorruangan = int.Parse(txt_ctknota_nomorruangan.Text);
                                            namapaket = cbo_ctknota_jenispaket.SelectedItem + " - " + dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nama Paket"].Value.ToString();
                                            hargapaket = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString().Replace(".", string.Empty));
                                            if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "True")
                                            {
                                                extra = "Ya";
                                                nominalextra = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString().Replace(".", string.Empty));
                                            }
                                            else if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "False")
                                            {
                                                extra = "Tidak";
                                                nominalextra = 0;
                                            }
                                            kodeterapis = int.Parse(cbo_ctknota_kodeterapis.SelectedItem.ToString());
                                            namaterapis = txt_ctknota_namaterapis.Text;
                                            diskon = int.Parse(txt_ctknota_diskon.Text);
                                            ket = txt_ctknota_ket.Text;
                                            fee = 0;
                                            totalbayar = hargapaket - potonganhotel + nominalextra;
                                            if (rdo_ctknota_cash.Checked)
                                            {
                                                jenisbayar = "Cash";
                                            }
                                            else if (rdo_ctknota_credit.Checked)
                                            {
                                                jenisbayar = "Credit";
                                            }
                                        }
                                    }

                                    DBConnect ctknota_sql = new DBConnect();
                                    string ctknota_query = "INSERT INTO `nota` (`id_nota`, `tanggalcetak_nota`, `nomorruangan_nota`, `jamkerja_nota`, `tamuhotel_nota`, `potonganhotel_nota`, "
                                                                + "`namapaket_nota`, `hargapaket_nota`, `extra_nota`, `nominalextra_nota`, `kodeterapis_nota`, `namaterapis_nota`,"
                                                                    + " `diskon_nota`, `keterangan_nota`, `totalbayar_nota`, `feeterapis_nota`, `jenisbayar_nota`, `status_nota`) "
                                                                        + "VALUES (NULL, '" + tanggalcetak + "', '" + nomorruangan + "', '" + jamkerja + "', '" + tamuhotel + "', '" + potonganhotel + "',"
                                                                            + " '" + namapaket + "', '" + hargapaket + "', '" + extra + "', '" + nominalextra + "', '" + kodeterapis + "', '" + namaterapis + "', "
                                                                                + "'" + diskon + "', '" + ket + "', '" + totalbayar + "', '" + fee + "', '" + jenisbayar + "', '-');";
                                    ctknota_sql.Insert(ctknota_query);


                                    #endregion
                                    string totalbayarFinal = "";
                                    int countdigittotal = 0;
                                    foreach (char c in totalbayar.ToString())
                                    {
                                        if (char.IsDigit(c))
                                        {
                                            countdigittotal++;
                                        }
                                    }
                                    int digit = countdigittotal;
                                    int countdigitend = 0;
                                    while (digit > 3)
                                    {
                                        countdigitend++;
                                        digit -= 3;
                                        totalbayarFinal = totalbayar.ToString().Insert(digit, ".");
                                    }
                                    totalbayarFinal = totalbayarFinal.Insert(countdigittotal + countdigitend, ",-");
                                    lbl_ctknota_totalbyr.Text = totalbayarFinal;
                                    MessageBox.Show("Nota telah berhasil ditambahkan");
                                    //MessageBox.Show("diskon ada, fee kosong");
                                }
                                else
                                {
                                    #region(Insert Nota)
                                    string tanggalcetak = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                                    string jamkerja = "";
                                    string tamuhotel = "";
                                    int potonganhotel = 0; ;
                                    int nomorruangan = 0;
                                    string namapaket = "";
                                    int hargapaket = 0;
                                    string extra = "";
                                    int nominalextra = 0;
                                    int kodeterapis = 0;
                                    string namaterapis = "";
                                    int diskon = 0;
                                    string ket = "";
                                    int fee = 0;
                                    int totalbayar = 0;
                                    string jenisbayar = "";
                                    //header.Trim(new Char[] { ' ', '*', '.' });
                                    for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                                    {
                                        if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value.ToString() == "True")
                                        {
                                            if (rdo_ctknota_normal.Checked)
                                            {
                                                jamkerja = "Normal";
                                            }
                                            else if (rdo_ctknota_midnight.Checked)
                                            {
                                                jamkerja = "Midnight";
                                            }
                                            if (rdo_ctknota_biasa.Checked)
                                            {
                                                tamuhotel = "Tidak";
                                                potonganhotel = 0;
                                            }
                                            else if (rdo_ctknota_hotel.Checked)
                                            {
                                                tamuhotel = "Ya";
                                                potonganhotel = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString().Replace(".", string.Empty));
                                            }
                                            nomorruangan = int.Parse(txt_ctknota_nomorruangan.Text);
                                            namapaket = cbo_ctknota_jenispaket.SelectedItem + " - " + dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nama Paket"].Value.ToString();
                                            hargapaket = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString().Replace(".", string.Empty));
                                            if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "True")
                                            {
                                                extra = "Ya";
                                                nominalextra = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString().Replace(".", string.Empty));
                                            }
                                            else if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "False")
                                            {
                                                extra = "Tidak";
                                                nominalextra = 0;
                                            }
                                            kodeterapis = int.Parse(cbo_ctknota_kodeterapis.SelectedItem.ToString());
                                            namaterapis = txt_ctknota_namaterapis.Text;
                                            diskon = int.Parse(txt_ctknota_diskon.Text);
                                            ket = txt_ctknota_ket.Text;
                                            fee = int.Parse(txt_ctknota_fee.Text);
                                            totalbayar = hargapaket - potonganhotel + nominalextra;
                                            if (rdo_ctknota_cash.Checked)
                                            {
                                                jenisbayar = "Cash";
                                            }
                                            else if (rdo_ctknota_credit.Checked)
                                            {
                                                jenisbayar = "Credit";
                                            }
                                        }
                                    }

                                    DBConnect ctknota_sql = new DBConnect();
                                    string ctknota_query = "INSERT INTO `nota` (`id_nota`, `tanggalcetak_nota`, `nomorruangan_nota`, `jamkerja_nota`, `tamuhotel_nota`, `potonganhotel_nota`, "
                                                                + "`namapaket_nota`, `hargapaket_nota`, `extra_nota`, `nominalextra_nota`, `kodeterapis_nota`, `namaterapis_nota`,"
                                                                    + " `diskon_nota`, `keterangan_nota`, `totalbayar_nota`, `feeterapis_nota`, `jenisbayar_nota`, `status_nota`) "
                                                                        + "VALUES (NULL, '" + tanggalcetak + "', '" + nomorruangan + "', '" + jamkerja + "', '" + tamuhotel + "', '" + potonganhotel + "',"
                                                                            + " '" + namapaket + "', '" + hargapaket + "', '" + extra + "', '" + nominalextra + "', '" + kodeterapis + "', '" + namaterapis + "', "
                                                                                + "'" + diskon + "', '" + ket + "', '" + totalbayar + "', '" + fee + "', '" + jenisbayar + "', '-');";
                                    ctknota_sql.Insert(ctknota_query);


                                    #endregion
                                    string totalbayarFinal = "";
                                    int countdigittotal = 0;
                                    foreach (char c in totalbayar.ToString())
                                    {
                                        if (char.IsDigit(c))
                                        {
                                            countdigittotal++;
                                        }
                                    }
                                    int digit = countdigittotal;
                                    int countdigitend = 0;
                                    while (digit > 3)
                                    {
                                        countdigitend++;
                                        digit -= 3;
                                        totalbayarFinal = totalbayar.ToString().Insert(digit, ".");
                                    }
                                    totalbayarFinal = totalbayarFinal.Insert(countdigittotal + countdigitend, ",-");
                                    lbl_ctknota_totalbyr.Text = totalbayarFinal;
                                    MessageBox.Show("Nota telah berhasil ditambahkan");
                                    //MessageBox.Show("diskon ada, fee ada");
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mohon pilih paket terlebih dahulu");
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
        }

        private void txt_ctknota_nomorruangan_TextChanged(object sender, EventArgs e)
        {
            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
        }

        private void dgv_ctknota_tabelhrgpkt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
                e.RowIndex >= 0)
            {
                if (dgv_ctknota_tabelhrgpkt.CurrentCell.OwningColumn.Name == "Pilih")
                {
                    ctknota_countExtraColumn = 0;
                    dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;
                    dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly = true;
                    for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                    {
                        dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value = false;

                    }
                    for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                    {
                        if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Selected)
                        {
                            dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value = true;
                            dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].ReadOnly = true;
                            dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].ReadOnly = false;
                        }
                        else
                            dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value = false;

                    }
                }
                else if (dgv_ctknota_tabelhrgpkt.CurrentCell.OwningColumn.Name == "Extra")
                {
                    for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                    {
                        if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value.ToString() == "True")
                        {
                            if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Selected)
                            {
                                if (ctknota_countExtraColumn % 2 == 0)
                                {
                                    dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = true;
                                }
                                else
                                {
                                    dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;
                                }
                                ctknota_countExtraColumn++;
                            }
                        }
                    }
                }
            }
            
        }

        private void txt_ctknota_nomorruangan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_ctknota_fee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_ctknota_batal_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = true;
            pnl_ctknota_isi.Visible = false;
        }
        #endregion

        #region(Panel Ganti Variabel)
        private void btn_variabel_simpan_Click(object sender, EventArgs e)
        {
            if (txt_variabel_extra.Text == "")
            {
                MessageBox.Show("Mohon lengkapi kolom Extra terlebih dahulu");
            }
            else if (txt_variabel_potonganhotel.Text == "")
            {
                MessageBox.Show("Mohon lengkapi kolom Potongan Hotel terlebih dahulu");
            }
            else
            {
                #region(Update)
                string edtpkt_connStr;
                MySqlConnection edtpkt_conn;
                DBConnect edtpkt_sql = new DBConnect();
                string edtpkt_query;
                edtpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                edtpkt_conn = new MySqlConnection(edtpkt_connStr);
                try
                {
                    edtpkt_conn.Open();

                    edtpkt_query = "UPDATE `variabel` SET `extra_variabel` = '" + int.Parse(txt_variabel_extra.Text) + "', "
                                    + "`potonganhotel_variabel` = '" + int.Parse(txt_variabel_potonganhotel.Text) + "' "
                                        + "WHERE `variabel`.`id_variabel` = 1;";
                    MySqlCommand cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data variabel telah tersimpan");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                edtpkt_conn.Close();
                #endregion
            }
        }

        private void txt_variabel_extra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_variabel_potonganhotel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_variabel_batal_Click(object sender, EventArgs e)
        {
            pnl_variabel_isi.Visible = false;
            pnl_menu_isi.Visible = true;
        }
        #endregion









    }
}