using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Reflection;
using ClosedXML.Excel;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace Green_Leaf
{
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
        }

        string all_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";

        #region(Buat Windows Form tidak bisa dirubah posisinya)
        //protected override void WndProc(ref Message message)
        //{
        //    const int WM_SYSCOMMAND = 0x0112;
        //    const int SC_MOVE = 0xF010;

        //    switch (message.Msg)
        //    {
        //        case WM_SYSCOMMAND:
        //            int command = message.WParam.ToInt32() & 0xfff0;
        //            if (command == SC_MOVE)
        //                return;
        //            break;
        //    }

        //    base.WndProc(ref message);
        //}
        #endregion

        private void frm_main_Load(object sender, EventArgs e)
        {
            txt_ctknota_diskon.MaxLength = 9;
            txt_ctknota_fee.MaxLength = 9;
            txt_edtpkt_hargapaket.MaxLength = 9;
            txt_tbhpkt_hargapaket.MaxLength = 9;
            txt_variabel_potonganhotel.MaxLength = 9;
            txt_edtpkt_durasipaketjam.ShortcutsEnabled = false;
            txt_edtpkt_durasipaketmenit.ShortcutsEnabled = false;
            txt_tbhpkt_durasipaketjam.ShortcutsEnabled = false;
            txt_tbhpkt_durasipaketmenit.ShortcutsEnabled = false;
            txt_ctknota_diskon.ShortcutsEnabled = false;
            txt_ctknota_fee.ShortcutsEnabled = false;
            txt_edtpkt_hargapaket.ShortcutsEnabled = false;
            txt_edtpkt_komisipaketmidnight.ShortcutsEnabled = false;
            txt_edtpkt_komisipaketnormal.ShortcutsEnabled = false;
            txt_tbhpkt_hargapaket.ShortcutsEnabled = false;
            txt_tbhpkt_komisipaketmidnight.ShortcutsEnabled = false;
            txt_tbhpkt_komisipaketnormal.ShortcutsEnabled = false;
            txt_variabel_potonganhotel.ShortcutsEnabled = false;
            txt_ctknota_nomorruangan.ShortcutsEnabled = false;
            txt_variabel_extra.ShortcutsEnabled = false;
            txt_tbhpkt_namapaket.ShortcutsEnabled = false;
            txt_edtpkt_namapaket.ShortcutsEnabled = false;
            txt_tbhtrps_namaterapis.ShortcutsEnabled = false;
            txt_edttrps_namaterapis.ShortcutsEnabled = false;

            pnl_login_isi.Enabled = true;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = false;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;

            #region(Isi textbox)
            string edtpkt_query;
            MySqlConnection edtpkt_conn = new MySqlConnection(all_connStr);
            try
            {
                edtpkt_conn.Open();

                edtpkt_query = "SELECT * FROM `variabel`";
                MySqlCommand edtpkt_cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                MySqlDataReader edtpkt_rdr = edtpkt_cmd.ExecuteReader();

                while (edtpkt_rdr.Read())
                {
                    edtvariabel_extra = edtpkt_rdr.GetInt32(1);
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

        private void frm_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        #region(Method Buatan Sendiri)
        int privatemethod_countkoma = 0;
        private void cekinputankomisi(TextBox txt ,KeyPressEventArgs e)
        {
            if (!txt.Text.Contains(','))
            {
                privatemethod_countkoma = 0;
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
            else
           {
                if (e.KeyChar == ',' && privatemethod_countkoma > 0)
                {
                    e.Handled = true;
                }
                else
                {
                    privatemethod_countkoma++;
                }
            }
        }
        #endregion

        #region(Panel Login)
        string login_jenisuser;
        string login_namauser;

        private void btn_login_masuk_Click(object sender, EventArgs e)
        {
            bool login_userAda = false;
            bool login_passSama = false;

            #region(Select Username dan Password)
            string query;
            
            MySqlConnection conn = new MySqlConnection(all_connStr);
            List<string>[] lstUsers = new List<string>[50];
            //List<string> lstPass = new List<string>();
            try
            {
                conn.Open();

                query = "SELECT `namaasli_pengguna`, `nama_pengguna`, `kata_kunci`, `jenis_pengguna` FROM `pengguna`";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                lstUsers[0] = new List<string>();
                lstUsers[1] = new List<string>();
                lstUsers[2] = new List<string>();
                lstUsers[3] = new List<string>();
                while (rdr.Read())
                {
                    lstUsers[0].Add(rdr[1].ToString());
                    lstUsers[1].Add(rdr[2].ToString());
                    lstUsers[2].Add(rdr[3].ToString());
                    lstUsers[3].Add(rdr[0].ToString());
                    //lstPass.Add(rdr[2].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Error Occured");
            }
            conn.Close();
            #endregion

            if (txt_login_username.Text != "")
            {
                for (int i = 0; i < lstUsers[0].Count; i++)
			    {
			        if (lstUsers[0][i] == txt_login_username.Text)
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
                    for (int i = 0; i < lstUsers[1].Count; i++)
                    {
                        if (txt_login_pass.Text == lstUsers[1][i])
                        {
                            login_passSama = true;
                            login_jenisuser = lstUsers[2][i];
                            login_namauser = lstUsers[3][i];
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
                        pnl_login_isi.Visible = false;
                        pnl_menu_isi.Visible = true;
                        lbl_menu_user.Text = txt_login_username.Text + ",";
                        pnl_login_isi.Enabled = false;
                        pnl_edttrps_isi.Enabled = false;
                        pnl_edtpkt_isi.Enabled = false;
                        pnl_ctknota_isi.Enabled = false;
                        pnl_lprnlayanan_isi.Enabled = false;
                        pnl_lprnpnjln_isi.Enabled = false;
                        pnl_menu_isi.Enabled = true;
                        pnl_tbhpkt_isi.Enabled = false;
                        pnl_tbhtrps_isi.Enabled = false;
                        pnl_variabel_isi.Enabled = false;
                        if (login_jenisuser == "Superadmin")
                        {
                            pnl_menu_laporan.Location = new Point(791, 47);
                            pnl_menu_others.Location = new Point(30,26);
                            pnl_menu_user.Location = new Point(1049, 124);
                            pnl_menu_user.Visible = true;
                            pnl_menu_user.Enabled = true;
                        }
                        else if (login_jenisuser == "Sales")
                        {
                            pnl_menu_laporan.Location = new Point(873, 47);
                            pnl_menu_others.Location = new Point(30, 26);
                            pnl_menu_user.Location = new Point(1049, 124);
                            pnl_menu_user.Visible = false;
                            pnl_menu_user.Enabled = false;
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

        private void btn_login_batal_Click(object sender, EventArgs e)
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

                MySqlConnection conn = new MySqlConnection(all_connStr);
                List<string>[] lstUsers = new List<string>[50];
                //List<string> lstPass = new List<string>();
                try
                {
                    conn.Open();

                    query = "SELECT `namaasli_pengguna`, `nama_pengguna`, `kata_kunci`, `jenis_pengguna` FROM `pengguna`";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    lstUsers[0] = new List<string>();
                    lstUsers[1] = new List<string>();
                    lstUsers[2] = new List<string>();
                    lstUsers[3] = new List<string>();
                    while (rdr.Read())
                    {
                        lstUsers[0].Add(rdr[1].ToString());
                        lstUsers[1].Add(rdr[2].ToString());
                        lstUsers[2].Add(rdr[3].ToString());
                        lstUsers[3].Add(rdr[0].ToString());
                        //lstPass.Add(rdr[2].ToString());
                    }
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Error Occured");
                }
                conn.Close();
                #endregion

                if (txt_login_username.Text != "")
                {
                    for (int i = 0; i < lstUsers[0].Count; i++)
                    {
                        if (lstUsers[0][i] == txt_login_username.Text)
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
                        for (int i = 0; i < lstUsers[1].Count; i++)
                        {
                            if (txt_login_pass.Text == lstUsers[1][i])
                            {
                                login_passSama = true;
                                login_jenisuser = lstUsers[2][i];
                                login_namauser = lstUsers[3][i];
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
                            pnl_login_isi.Visible = false;
                            pnl_menu_isi.Visible = true;
                            lbl_menu_user.Text = txt_login_username.Text + ",";
                            pnl_login_isi.Enabled = false;
                            pnl_edttrps_isi.Enabled = false;
                            pnl_edtpkt_isi.Enabled = false;
                            pnl_ctknota_isi.Enabled = false;
                            pnl_lprnlayanan_isi.Enabled = false;
                            pnl_lprnpnjln_isi.Enabled = false;
                            pnl_menu_isi.Enabled = true;
                            pnl_tbhpkt_isi.Enabled = false;
                            pnl_tbhtrps_isi.Enabled = false;
                            pnl_variabel_isi.Enabled = false;
                            if (login_jenisuser == "Superadmin")
                            {
                                pnl_menu_laporan.Location = new Point(791, 47);
                                pnl_menu_others.Location = new Point(30, 26);
                                pnl_menu_user.Location = new Point(1049, 124);
                                pnl_menu_user.Visible = true;
                                pnl_menu_user.Enabled = true;
                            }
                            else if (login_jenisuser == "Sales")
                            {
                                pnl_menu_laporan.Location = new Point(873, 47);
                                pnl_menu_others.Location = new Point(30, 26);
                                pnl_menu_user.Location = new Point(1049, 124);
                                pnl_menu_user.Visible = false;
                                pnl_menu_user.Enabled = false;
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

                MySqlConnection conn = new MySqlConnection(all_connStr);
                List<string>[] lstUsers = new List<string>[50];
                //List<string> lstPass = new List<string>();
                try
                {
                    conn.Open();

                    query = "SELECT `namaasli_pengguna`, `nama_pengguna`, `kata_kunci`, `jenis_pengguna` FROM `pengguna`";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    lstUsers[0] = new List<string>();
                    lstUsers[1] = new List<string>();
                    lstUsers[2] = new List<string>();
                    lstUsers[3] = new List<string>();
                    while (rdr.Read())
                    {
                        lstUsers[0].Add(rdr[1].ToString());
                        lstUsers[1].Add(rdr[2].ToString());
                        lstUsers[2].Add(rdr[3].ToString());
                        lstUsers[3].Add(rdr[0].ToString());
                        //lstPass.Add(rdr[2].ToString());
                    }
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Error Occured");
                }
                conn.Close();
                #endregion

                if (txt_login_username.Text != "")
                {
                    for (int i = 0; i < lstUsers[0].Count; i++)
                    {
                        if (lstUsers[0][i] == txt_login_username.Text)
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
                        for (int i = 0; i < lstUsers[1].Count; i++)
                        {
                            if (txt_login_pass.Text == lstUsers[1][i])
                            {
                                login_passSama = true;
                                login_jenisuser = lstUsers[2][i];
                                login_namauser = lstUsers[3][i];
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
                            pnl_login_isi.Visible = false;
                            pnl_menu_isi.Visible = true;
                            lbl_menu_user.Text = txt_login_username.Text + ",";
                            pnl_login_isi.Enabled = false;
                            pnl_edttrps_isi.Enabled = false;
                            pnl_edtpkt_isi.Enabled = false;
                            pnl_ctknota_isi.Enabled = false;
                            pnl_lprnlayanan_isi.Enabled = false;
                            pnl_lprnpnjln_isi.Enabled = false;
                            pnl_menu_isi.Enabled = true;
                            pnl_tbhpkt_isi.Enabled = false;
                            pnl_tbhtrps_isi.Enabled = false;
                            pnl_variabel_isi.Enabled = false;
                            if (login_jenisuser == "Superadmin")
                            {
                                pnl_menu_laporan.Location = new Point(791, 47);
                                pnl_menu_others.Location = new Point(30, 26);
                                pnl_menu_user.Location = new Point(1049, 124);
                                pnl_menu_user.Visible = true;
                                pnl_menu_user.Enabled = true;
                            }
                            else if (login_jenisuser == "Sales")
                            {
                                pnl_menu_laporan.Location = new Point(873, 47);
                                pnl_menu_others.Location = new Point(30, 26);
                                pnl_menu_user.Location = new Point(1049, 124);
                                pnl_menu_user.Visible = false;
                                pnl_menu_user.Enabled = false;
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

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = false;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = true;
            pnl_variabel_isi.Enabled = false;
            pnl_edtuser_isi.Enabled = false;
            pnl_tbhuser_isi.Enabled = false;
        }

        private void btn_menu_edttrps_Click(object sender, EventArgs e)
        {
            pnl_edttrps_isi.Visible = true;
            pnl_menu_isi.Visible = false;
            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = true;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = false;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;
            pnl_edtuser_isi.Enabled = false;
            pnl_tbhuser_isi.Enabled = false;

            if (login_jenisuser == "Superadmin")
            {
                btn_edttrps_hapus.Visible = true;
                btn_edttrps_simpan.Location = new Point(550, 399);
                btn_edttrps_batal.Location = new Point(735, 399);
            }
            else if (login_jenisuser == "Sales")
            {
                btn_edttrps_hapus.Visible = false;
                btn_edttrps_simpan.Location = new Point(454, 399);
                btn_edttrps_batal.Location = new Point(639, 399);
            }

            rdo_edttrps_statusaktif.Checked = false;
            rdo_edttrps_statustdkaktif.Checked = false;
            btn_edttrps_simpan.Enabled = false;
            txt_edttrps_kodeterapis.Enabled = false;
            txt_edttrps_namaterapis.Enabled = false;
            btn_edttrps_browsefoto.Enabled = false;
            rdo_edttrps_statusaktif.Enabled = false;
            rdo_edttrps_statustdkaktif.Enabled = false;
            btn_edttrps_hapus.Enabled = false;

            lsb_edttrps_kodeterapis.Items.Clear();
            #region(Select)
            MySqlConnection edttrps_conn = new MySqlConnection();
            string edttrps_query = "";
            edttrps_conn = new MySqlConnection(all_connStr);
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
                MessageBox.Show("Error Occured");
            }
            edttrps_conn.Close();
            #endregion
        }

        private void btn_menu_tbhpkt_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
            pnl_tbhpkt_isi.Visible = true;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = false;
            pnl_tbhpkt_isi.Enabled = true;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;
            pnl_edtuser_isi.Enabled = false;
            pnl_tbhuser_isi.Enabled = false;

            txt_tbhpkt_komisipaketmidnight.Enabled = true;
            txt_tbhpkt_komisipaketnormal.Enabled = true;
        }

        private void btn_menu_edtpkt_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
            pnl_edtpkt_isi.Visible = true;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = true;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = false;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;
            pnl_edtuser_isi.Enabled = false;
            pnl_tbhuser_isi.Enabled = false;

            if (login_jenisuser == "Superadmin")
            {
                btn_edtpkt_hapus.Visible = true;
                btn_edtpkt_simpan.Location = new Point(314, 439);
                btn_edtpkt_batal.Location = new Point(514, 439);
            }
            else if(login_jenisuser == "Sales")
            {
                btn_edtpkt_hapus.Visible = false;
                btn_edtpkt_simpan.Location = new Point(447, 439);
                btn_edtpkt_batal.Location = new Point(647, 439);
            }
            edtpkt_lstnamapaket[0] = new List<string>();
            edtpkt_lstnamapaket[1] = new List<string>();
            cbo_edtpkt_jenispaket.Enabled = false;
            txt_edtpkt_namapaket.Enabled = false;
            txt_edtpkt_komisipaketnormal.Enabled = false;
            txt_edtpkt_komisipaketmidnight.Enabled = false;
            txt_edtpkt_durasipaketjam.Enabled = false;
            txt_edtpkt_durasipaketmenit.Enabled = false;
            txt_edtpkt_hargapaket.Enabled = false;
            btn_edtpkt_simpan.Enabled = false;
            lsb_edtpkt_jenisnamapkt.Items.Clear();
            btn_edtpkt_hapus.Enabled = false;

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
            MySqlConnection edtpkt_conn2 = new MySqlConnection(all_connStr);
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
        }

        private void btn_menu_ctknota_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
            pnl_ctknota_isi.Visible = true;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = true;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = false;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;
            pnl_edtuser_isi.Enabled = false;
            pnl_tbhuser_isi.Enabled = false;

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

            dgv_ctknota_tabelhrgpkt.DataSource = null;
            dgv_ctknota_tabelhrgpkt.Rows.Clear();
            dgv_ctknota_tabelhrgpkt.Refresh();
            ctknota_DS.Tables.Clear();
            ctknota_countExtraColumn = 0;

            rdo_ctknota_biasa.Checked = false;
            rdo_ctknota_cash.Checked = false;
            rdo_ctknota_credit.Checked = false;
            rdo_ctknota_hotel.Checked = false;
            rdo_ctknota_midnight.Checked = false;
            rdo_ctknota_normal.Checked = false;
            cbo_ctknota_jenispaket.Items.Clear();
            txt_ctknota_namaterapis.Clear();
            txt_ctknota_nomorruangan.Clear();
            lbl_ctknota_totalbyr.Text = "";
            txt_ctknota_fee.Clear();
            txt_ctknota_diskon.Clear();
            txt_ctknota_ket.Clear();

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
            MySqlConnection ctknota_conn2 = new MySqlConnection(all_connStr);
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
                MessageBox.Show("Error Occured");
            }
            ctknota_conn2.Close();
            #endregion

            cbo_ctknota_kodeterapis.Items.Clear();
            #region(Select)
            string ctknota_query3;
            MySqlConnection ctknota_conn3 = new MySqlConnection(all_connStr);
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
            pnl_lprnpnjln_isi.Visible = true;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = true;
            pnl_menu_isi.Enabled = false;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;
            pnl_edtuser_isi.Enabled = false;
            pnl_tbhuser_isi.Enabled = false;

            dtp_lprnpnjln_tgldari.ResetText();
            dtp_lprnpnjln_tglsampai.ResetText();

            lprnpnjln_tanggalcetakdari = dtp_lprnpnjln_tgldari.Value.Year.ToString();
            lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Month.ToString();
            lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Day.ToString();
            //tanggalcetakdari += " " + dtp_lprnpnjln_tgldari.Value.TimeOfDay.ToString();

            lprnpnjln_tanggalcetaksampai = dtp_lprnpnjln_tglsampai.Value.Year.ToString();
            lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Month.ToString();
            lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Day.ToString();
            //tanggalcetaksampai += " " + dtp_lprnpnjln_tglsampai.Value.TimeOfDay.ToString();

            //rdo_lprnpnjln_all.Enabled = false;
            //rdo_lprnpnjln_cash.Enabled = false;
            //rdo_lprnpnjln_credit.Enabled = false;
            
            rdo_lprnpnjln_cash.Enabled = true;
            rdo_lprnpnjln_credit.Enabled = true;
            rdo_lprnpnjln_all.Enabled = true;
            btn_lprnpnjln_excel.Enabled = true;
            

            dtp_lprnpnjln_tgldari.Format = DateTimePickerFormat.Custom;
            dtp_lprnpnjln_tgldari.CustomFormat = "dddd, dd MMMM yyyy";

            dtp_lprnpnjln_tglsampai.Format = DateTimePickerFormat.Custom;
            dtp_lprnpnjln_tglsampai.CustomFormat = "dddd, dd MMMM yyyy";

            lprnpnjln_hapusNota.Name = "Hapus Nota";
            lprnpnjln_hapusNota.Text = "Hapus";
            lprnpnjln_hapusNota.UseColumnTextForButtonValue = true;

            lprnpnjln_batalhapusNota.Name = "Batal Hapus";
            lprnpnjln_batalhapusNota.Text = "Batal";
            lprnpnjln_batalhapusNota.UseColumnTextForButtonValue = true;

            dgv_lprnpnjln_tabellaporan.RowTemplate.Height = 22;

            dgv_lprnpnjln_tabellaporan.DataSource = null;
            dgv_lprnpnjln_tabellaporan.Rows.Clear();
            dgv_lprnpnjln_tabellaporan.Columns.Clear();
            dgv_lprnpnjln_tabellaporan.Refresh();
            lprnpnjln_DS.Tables.Clear();
            lbl_lprnpnjln_sumfeeterapis.Text = "";
            lbl_lprnpnjln_sumtotalcash.Text = "";
            lbl_lprnpnjln_sumtotalcredit.Text = "";
            lbl_lprnpnjln_sumtotaldiskon.Text = "";
            lbl_lprnpnjln_sumtotalextra.Text = "";
            lbl_lprnpnjln_sumtotalgrandtotal.Text = "";
            lbl_lprnpnjln_sumtotalhotel.Text = "";
            lbl_lprnpnjln_sumtotalsubtotal.Text = "";
            //dtp_lprnpnjln_tgldari.ResetText();
            //dtp_lprnpnjln_tglsampai.ResetText();
            //rdo_lprnpnjln_all.Checked = false;
            //rdo_lprnpnjln_cash.Checked = false;
            //rdo_lprnpnjln_credit.Checked = false;

            #region(Select)
            dgv_lprnpnjln_tabellaporan.DataSource = null;
            dgv_lprnpnjln_tabellaporan.Rows.Clear();
            dgv_lprnpnjln_tabellaporan.Columns.Clear();
            //dgv_lprnpnjln_tabellaporan.Refresh();
            lprnpnjln_DS.Tables.Clear();
            string lprnpnjln_query;
            MySqlConnection lprnpnjln_conn = new MySqlConnection(all_connStr);
            try
            {
                lprnpnjln_tanggalcetakdari = dtp_lprnpnjln_tgldari.Value.Year.ToString();
                lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Month.ToString();
                lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Day.ToString();

                lprnpnjln_tanggalcetaksampai = dtp_lprnpnjln_tglsampai.Value.Year.ToString();
                lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Month.ToString();
                lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Day.ToString();

                lprnpnjln_conn.Open();
                lprnpnjln_query = "SELECT * FROM `nota` WHERE DATE(`tanggalcetak_nota`) >= '" + lprnpnjln_tanggalcetakdari + "' and DATE(tanggalcetak_nota) <= '" + lprnpnjln_tanggalcetaksampai + "'";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(lprnpnjln_query, lprnpnjln_conn);
                MySqlCommand ctknota_cmd = new MySqlCommand(lprnpnjln_query, lprnpnjln_conn);

                mySqlDataAdapter.Fill(lprnpnjln_DS);
                lprnpnjln_DS.Tables[0].Columns.Remove("id_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("jenis_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_midnight_paket");

                lprnpnjln_DS.Tables[0].Columns["id_nota"].ColumnName = "Nomor Nota";
                lprnpnjln_DS.Tables[0].Columns["tanggalcetak_nota"].ColumnName = "Tanggal Cetak Nota";
                lprnpnjln_DS.Tables[0].Columns["nomorruangan_nota"].ColumnName = "Nomor Ruangan";
                lprnpnjln_DS.Tables[0].Columns["jamkerja_nota"].ColumnName = "Jam Kerja";
                lprnpnjln_DS.Tables[0].Columns["tamuhotel_nota"].ColumnName = "Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["potonganhotel_nota"].ColumnName = "Potongan Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["namapaket_nota"].ColumnName = "Nama Paket";
                lprnpnjln_DS.Tables[0].Columns["hargapaket_nota"].ColumnName = "Harga Paket";
                lprnpnjln_DS.Tables[0].Columns["extra_nota"].ColumnName = "Extra";
                lprnpnjln_DS.Tables[0].Columns["nominalextra_nota"].ColumnName = "Nominal Extra";
                lprnpnjln_DS.Tables[0].Columns["kodeterapis_nota"].ColumnName = "Kode Terapis";
                lprnpnjln_DS.Tables[0].Columns["namaterapis_nota"].ColumnName = "Nama Terapis";
                lprnpnjln_DS.Tables[0].Columns["diskon_nota"].ColumnName = "Diskon";
                lprnpnjln_DS.Tables[0].Columns["keterangan_nota"].ColumnName = "Keterangan Diskon";
                lprnpnjln_DS.Tables[0].Columns["totalbayar_nota"].ColumnName = "Subtotal";
                lprnpnjln_DS.Tables[0].Columns["feeterapis_nota"].ColumnName = "Fee Terapis";
                lprnpnjln_DS.Tables[0].Columns["jenisbayar_nota"].ColumnName = "Jenis Bayar";
                lprnpnjln_DS.Tables[0].Columns["status_nota"].ColumnName = "Status";
                lprnpnjln_DS.Tables[0].Columns["grandtotal_nota"].ColumnName = "Grand Total";
                //lprnpnjln_DS.Tables[0].Columns["grandtotal_nota"].SetOrdinal(16);
                //lprnpnjln_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                //lprnpnjln_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                //for (int i = 0; i < lprnpnjln_DS.Tables[0].Rows.Count; i++)
                //{
                //    lprnpnjln_DS.Tables[0].Rows[i]["Tamu Hotel"] = ctknota_tamuhotel;
                //}

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                //lprnpnjln_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                //for (int ii = 0; ii < lprnpnjln_DS.Tables[0].Rows.Count; ii++)
                //{
                //    lprnpnjln_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                //}

                dgv_lprnpnjln_tabellaporan.DataSource = lprnpnjln_DS.Tables[0];

                if (login_jenisuser == "Superadmin")
                {
                    dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_hapusNota);
                    dgv_lprnpnjln_tabellaporan.Columns["Hapus Nota"].DisplayIndex = 19;
                    dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_batalhapusNota);
                    dgv_lprnpnjln_tabellaporan.Columns["Batal Hapus"].DisplayIndex = 20;
                }


                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].Width = 55;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].Width = 115;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].Width = 70;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].Width = 50;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].Width = 45;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].Width = 150;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].Width = 60;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";


                ////MessageBox.Show(dgv_lprnpnjln_tabellaporan.Columns.Count.ToString());
                ////for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Columns.Count; i++)
                ////{
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].ReadOnly = true;
                ////}
                dgv_lprnpnjln_tabellaporan.Columns["Grand Total"].DisplayIndex = 16;
                dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DisplayIndex = 14;

                dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Diskon"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Subtotal"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Grand Total"].DefaultCellStyle.Format = "N0";

                dgv_lprnpnjln_tabellaporan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgv_lprnpnjln_tabellaporan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv_lprnpnjln_tabellaporan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.ReadOnly = true;

                dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_lprnpnjln_tabellaporan.Columns["Keterangan Diskon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                #region(SUM Kolom berisi nominal harga)
                lprnpnjln_potonganhotel = 0;
                lprnpnjln_hargapaket = 0;
                lprnpnjln_extra = 0;
                lprnpnjln_diskon = 0;
                lprnpnjln_totalbayar = 0;
                lprnpnjln_totalbayarcash = 0;
                lprnpnjln_totalbayarcredit = 0;
                lprnpnjln_feeterapis = 0;
                lprnpnjln_grandtotal = 0;
                for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Rows.Count; i++)
                {
                    if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Status"].Value.ToString() != "Terhapus")
                    {
                        lprnpnjln_potonganhotel += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Potongan Tamu Hotel"].Value.ToString());
                        lprnpnjln_hargapaket += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Harga Paket"].Value.ToString());
                        lprnpnjln_extra += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Nominal Extra"].Value.ToString());
                        lprnpnjln_diskon += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Diskon"].Value.ToString());
                        lprnpnjln_totalbayar += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Subtotal"].Value.ToString());
                        lprnpnjln_feeterapis += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Fee Terapis"].Value.ToString());
                        lprnpnjln_grandtotal += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Cash")
                        {
                            lprnpnjln_totalbayarcash += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        }
                        else if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Credit")
                        {
                            lprnpnjln_totalbayarcredit += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        }
                    }
                }
                lbl_lprnpnjln_sumtotalhotel.Text = lprnpnjln_potonganhotel.ToString("#,##0");
                lbl_lprnpnjln_sumtotalgrandtotal.Text = lprnpnjln_grandtotal.ToString("#,##0");
                lbl_lprnpnjln_sumtotalextra.Text = lprnpnjln_extra.ToString("#,##0");
                lbl_lprnpnjln_sumtotaldiskon.Text = lprnpnjln_diskon.ToString("#,##0");
                lbl_lprnpnjln_sumtotalsubtotal.Text = lprnpnjln_totalbayar.ToString("#,##0");
                lbl_lprnpnjln_sumtotalcash.Text = lprnpnjln_totalbayarcash.ToString("#,##0");
                lbl_lprnpnjln_sumtotalcredit.Text = lprnpnjln_totalbayarcredit.ToString("#,##0");
                lbl_lprnpnjln_sumfeeterapis.Text = lprnpnjln_feeterapis.ToString("#,##0");

                //DataRow row = lprnpnjln_DS.Tables[0].NewRow();
                //row[0] = "0";
                //row[1] = "2016-01-08 15:42:16";
                //row[2] = "0";
                //row[3] = "";
                //row[4] = "";
                //row[5] = lbl_lprnpnjln_sumtotalhotel.Text;
                //row[6] = "";
                //row[7] = lprnpnjln_hargapaket.ToString("#,##0"); ;
                //row[8] = "";
                //row[9] = lbl_lprnpnjln_sumtotalextra.Text;
                //row[10] = "0";
                //row[11] = "";
                //row[12] = lbl_lprnpnjln_sumtotaldiskon.Text;
                //row[13] = "";
                //row[14] = lbl_lprnpnjln_sumtotalsubtotal.Text;
                //row[15] = lbl_lprnpnjln_sumfeeterapis.Text;
                //row[16] = "";
                //row[17] = "";
                //lprnpnjln_DS.Tables[0].Rows.Add(row);
                #endregion


            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnpnjln_conn.Close();
            #endregion

            rdo_lprnpnjln_all.Checked = true;
        }

        private void btn_menu_laporanlayanan_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
            pnl_lprnlayanan_isi.Visible = true;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = true;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = false;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;
            pnl_edtuser_isi.Enabled = false;
            pnl_tbhuser_isi.Enabled = false;

            dgv_lprnlayanan_tabellayanan.DataSource = null;
            dgv_lprnlayanan_tabellayanan.Rows.Clear();
            dgv_lprnlayanan_tabellayanan.Columns.Clear();
            lprnlayanan_DS.Tables.Clear();

            dtp_lprnlayanan_tgldari.ResetText();
            dtp_lprnlayanan_tglsampai.ResetText();

            //btn_lprnlayanan_excel.Enabled = false;
        }

        private void btn_menu_laporangajiexcel_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
            pnl_lprngaji_isi.Visible = true;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = false;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;
            pnl_edtuser_isi.Enabled = false;
            pnl_tbhuser_isi.Enabled = false;
            pnl_lprngaji_isi.Enabled = true;

            dtp_lprngaji_tgldari.ResetText();
            dtp_lprngaji_tglsampai.ResetText();

            //lprngaji_tanggalcetakdari = dtp_lprngaji_tgldari.Value.Year.ToString();
            //lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Month.ToString();
            //lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Day.ToString();
            ////tanggalcetakdari += " " + dtp_lprnpnjln_tgldari.Value.TimeOfDay.ToString();

            //lprngaji_tanggalcetaksampai = dtp_lprngaji_tglsampai.Value.Year.ToString();
            //lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Month.ToString();
            //lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Day.ToString();
            ////tanggalcetaksampai += " " + dtp_lprnpnjln_tglsampai.Value.TimeOfDay.ToString();

            ////rdo_lprnpnjln_all.Enabled = false;
            ////rdo_lprnpnjln_cash.Enabled = false;
            ////rdo_lprnpnjln_credit.Enabled = false;

            ////rdo_lprnpnjln_cash.Enabled = true;
            ////rdo_lprnpnjln_credit.Enabled = true;
            ////rdo_lprnpnjln_all.Enabled = true;
            ////btn_lprnpnjln_excel.Enabled = true;


            //dtp_lprngaji_tgldari.Format = DateTimePickerFormat.Custom;
            //dtp_lprngaji_tgldari.CustomFormat = "dddd, dd MMMM yyyy";

            //dtp_lprngaji_tglsampai.Format = DateTimePickerFormat.Custom;
            //dtp_lprngaji_tglsampai.CustomFormat = "dddd, dd MMMM yyyy";

            //btn_lprngaji_excel.Enabled = false;
            btn_lprngaji_batal.Enabled = true;
        }

        private void btn_menu_variabel_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
            pnl_variabel_isi.Visible = true;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = false;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = true;
            pnl_edtuser_isi.Enabled = false;
            pnl_tbhuser_isi.Enabled = false;

            #region(Isi textbox)
            string edtpkt_query;
            MySqlConnection edtpkt_conn = new MySqlConnection(all_connStr);
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

        private void btn_menu_tbhuser_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
            pnl_tbhuser_isi.Visible = true;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = false;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;
            pnl_edtuser_isi.Enabled = false;
            pnl_tbhuser_isi.Enabled = true;
        }

        private void btn_menu_edituser_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = false;
            pnl_edtuser_isi.Visible = true;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = false;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;
            pnl_edtuser_isi.Enabled = true;
            pnl_tbhuser_isi.Enabled = false;

            txt_edtuser_nama.Clear();
            txt_edtuser_pass.Clear();
            txt_edtuser_user.Clear();
            cbo_edtuser_jenisuser.SelectedItem = null;
            btn_edtuser_simpan.Enabled = false;
            txt_edtuser_nama.Enabled = false;
            txt_edtuser_pass.Enabled = false;
            txt_edtuser_user.Enabled = false;
            cbo_edtuser_jenisuser.Enabled = false;
            btn_edtuser_hapus.Enabled = false;
            lsb_edtuser_user.Items.Clear();
            #region(Select)
            string edttrps_query;
            string edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection edttrps_conn = new MySqlConnection(edttrps_connStr);
            List<string> edttrps_lstKode = new List<string>();
            try
            {
                edttrps_conn.Open();

                edttrps_query = "SELECT * FROM `pengguna` order by id_pengguna DESC";
                MySqlCommand edttrps_cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                MySqlDataReader edttrps_rdr = edttrps_cmd.ExecuteReader();

                while (edttrps_rdr.Read())
                {
                    //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                    lsb_edtuser_user.Items.Add(edttrps_rdr.GetString(1));
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

        private void lbllink_menu_logout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah anda ingin Logout?", "Alert", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                pnl_menu_isi.Visible = false;
                pnl_login_isi.Visible = true;
                txt_login_username.Clear();
                txt_login_pass.Clear();
                txt_login_username.Focus();

                pnl_login_isi.Enabled = true;
                pnl_edttrps_isi.Enabled = false;
                pnl_edtpkt_isi.Enabled = false;
                pnl_ctknota_isi.Enabled = false;
                pnl_lprnlayanan_isi.Enabled = false;
                pnl_lprnpnjln_isi.Enabled = false;
                pnl_menu_isi.Enabled = false;
                pnl_tbhpkt_isi.Enabled = false;
                pnl_tbhtrps_isi.Enabled = false;
                pnl_variabel_isi.Enabled = false;
            }
        }
        #endregion

        #region(Panel Tambah Terapis)
        string tbhtrps_lokasi_gambar = "";
        OpenFileDialog tbhtrps_open = new OpenFileDialog();
        private void btn_tbhtrps_browsefoto_Click(object sender, EventArgs e)
        {
            // open file dialog 
            
            // image filters
            tbhtrps_open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (tbhtrps_open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                
                // image file path
                
                
                pict_tbhtrps_fotoKTP.Image = new Bitmap(tbhtrps_open.FileName);
            }
        }

        private void btn_tbhtrps_tambah_Click(object sender, EventArgs e)
        {
            DBConnect tbhtrps_sql = new DBConnect();

            bool tbhtrps_kodeSama = false;

            #region(Select khusus kode terapis, disimpan ke dalam List lstkode)
            string tbhtrps_query;
            MySqlConnection tbhtrps_conn = new MySqlConnection(all_connStr);
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
                MessageBox.Show("Error Occured");
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
                else if (pict_tbhtrps_fotoKTP.Image == null)
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
                        File.Copy(tbhtrps_open.FileName.ToString(), Application.StartupPath.ToString() + "\\img\\" + txt_tbhtrps_namaterapis.Text + ".jpg", true);
                        tbhtrps_lokasi_gambar = Application.StartupPath.ToString() + "\\img\\" + txt_tbhtrps_namaterapis.Text + ".jpg";
                        tbhtrps_lokasi_gambar = tbhtrps_lokasi_gambar.Replace("\\", "\\\\");

                        for (int i = 2; i < 7; i++)
                        {
                            tbhtrps_query = "INSERT INTO `terapis` (`kode_terapis`, `nama_terapis`, `lokasi_gambar`, `status_terapis`) VALUES ('" + i + "', '" + txt_tbhtrps_namaterapis.Text + "', '" + tbhtrps_lokasi_gambar + "', 'Aktif');";
                            tbhtrps_sql.Insert(tbhtrps_query); 
                        }
                        
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
                        File.Copy(tbhtrps_open.FileName.ToString(), Application.StartupPath.ToString() + "\\img\\" + txt_tbhtrps_namaterapis.Text + ".jpg", true);
                        tbhtrps_lokasi_gambar = Application.StartupPath.ToString() + "\\img\\" + txt_tbhtrps_namaterapis.Text + ".jpg";
                        tbhtrps_lokasi_gambar = tbhtrps_lokasi_gambar.Replace("\\", "\\\\");

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

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = true;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;

            txt_tbhtrps_kodeterapis.Clear();
            txt_tbhtrps_namaterapis.Clear();
            tbhtrps_lokasi_gambar = "";
            rdo_tbhtrps_statusaktif.Checked = false;
            rdo_tbhtrps_statustdkaktif.Checked = false;
            pict_tbhtrps_fotoKTP.Image = null;
            txt_tbhtrps_kodeterapis.Focus();
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
        string edttrps_namalama;
        int edttrps_idTerapis;
        int edttrps_kodeTerakhir;
        OpenFileDialog edttrps_open = new OpenFileDialog();
        private void btn_edttrps_browsefoto_Click(object sender, EventArgs e)
        {
            // open file dialog 
            
            // image filters
            edttrps_open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (edttrps_open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box

                // image file path
                pict_edttrps_fotoKTP.Image = Image.FromStream(new MemoryStream(File.ReadAllBytes(edttrps_open.FileName)));
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
                btn_edttrps_hapus.Enabled = true;
                string edttrps_kodeTerpilih = lsb_edttrps_kodeterapis.SelectedItem.ToString();

                string edttrps_query;

                #region(Select)
                MySqlConnection edttrps_conn = new MySqlConnection(all_connStr);
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
                        edttrps_namalama = edttrps_rdr.GetString(2);
                        pict_edttrps_fotoKTP.Image = Image.FromStream(new MemoryStream(File.ReadAllBytes(edttrps_rdr.GetString(3))));
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
                    MessageBox.Show("Error Occured");
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
            else if (pict_edttrps_fotoKTP.ImageLocation == "")
            {
                MessageBox.Show("Mohon pilih foto KTP terlebih dahulu!");
            }
            else
            {
                MySqlConnection edttrps_conn;
                DBConnect edttrps_sql = new DBConnect();
                string edttrps_query;
                bool edttrps_kodeSama = false;
                edttrps_lokasi_gambar = edttrps_lokasi_gambar.Replace(@"\", @"\\");

                if (txt_edttrps_kodeterapis.Text == edttrps_kodeTerakhir.ToString())
                {
                    if (rdo_edttrps_statusaktif.Checked)
                    {
                        if (edttrps_open.FileName != "")
                        {
                            File.Copy(edttrps_open.FileName.ToString(), Application.StartupPath.ToString() + "\\img\\" + txt_edttrps_namaterapis.Text + ".jpg", true);
                            edttrps_lokasi_gambar = Application.StartupPath.ToString() + "\\img\\" + txt_edttrps_namaterapis.Text + ".jpg";
                            edttrps_lokasi_gambar = edttrps_lokasi_gambar.Replace("\\", "\\\\");
                            if (txt_edttrps_namaterapis.Text != edttrps_namalama)
                            {
                                File.Delete(Application.StartupPath.ToString() + "\\img\\" + edttrps_namalama + ".jpg");
                            }
                        }
                        #region(Update)
                        edttrps_conn = new MySqlConnection(all_connStr);
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
                            MessageBox.Show("Error Occured");
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
                        btn_edttrps_hapus.Enabled = false;
                    }
                    else if (rdo_edttrps_statustdkaktif.Checked)
                    {
                        if (edttrps_open.FileName != "")
                        {
                            File.Copy(edttrps_open.FileName.ToString(), Application.StartupPath.ToString() + "\\img\\" + txt_edttrps_namaterapis.Text + ".jpg", true);
                            edttrps_lokasi_gambar = Application.StartupPath.ToString() + "\\img\\" + txt_edttrps_namaterapis.Text + ".jpg";
                            edttrps_lokasi_gambar = edttrps_lokasi_gambar.Replace("\\", "\\\\");
                            if (txt_edttrps_namaterapis.Text != edttrps_namalama)
                            {
                                File.Delete(Application.StartupPath.ToString() + "\\img\\" + edttrps_namalama + ".jpg");
                            }
                        }
                        #region(Update)
                        edttrps_conn = new MySqlConnection(all_connStr);
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
                            MessageBox.Show("Error Occured");
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
                        btn_edttrps_hapus.Enabled = false;
                    }
                }
                else
                {
                    #region(Select)
                    edttrps_conn = new MySqlConnection(all_connStr);
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
                        MessageBox.Show("Error Occured");
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
                            if (edttrps_open.FileName != "")
                            {
                                File.Copy(edttrps_open.FileName.ToString(), Application.StartupPath.ToString() + "\\img\\" + txt_edttrps_namaterapis.Text + ".jpg", true);
                                edttrps_lokasi_gambar = Application.StartupPath.ToString() + "\\img\\" + txt_edttrps_namaterapis.Text + ".jpg";
                                edttrps_lokasi_gambar = edttrps_lokasi_gambar.Replace("\\", "\\\\");
                                if (txt_edttrps_namaterapis.Text != edttrps_namalama)
                                {
                                    File.Delete(Application.StartupPath.ToString() + "\\img\\" + edttrps_namalama + ".jpg");
                                }
                            }
                            #region(Update)
                            edttrps_conn = new MySqlConnection(all_connStr);
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
                                MessageBox.Show("Error Occured");
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
                            btn_edttrps_hapus.Enabled = false;
                        }
                        else if (rdo_edttrps_statustdkaktif.Checked)
                        {
                            if (edttrps_open.FileName != "")
                            {
                                File.Copy(edttrps_open.FileName.ToString(), Application.StartupPath.ToString() + "\\img\\" + txt_edttrps_namaterapis.Text + ".jpg", true);
                                edttrps_lokasi_gambar = Application.StartupPath.ToString() + "\\img\\" + txt_edttrps_namaterapis.Text + ".jpg";
                                edttrps_lokasi_gambar = edttrps_lokasi_gambar.Replace("\\", "\\\\");
                                if (txt_edttrps_namaterapis.Text != edttrps_namalama)
                                {
                                    File.Delete(Application.StartupPath.ToString() + "\\img\\" + edttrps_namalama + ".jpg");
                                }
                            }
                            #region(Update)
                            edttrps_conn = new MySqlConnection(all_connStr);
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
                                MessageBox.Show("Error Occured");
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
                            btn_edttrps_hapus.Enabled = false;
                        }
                    }
                }
                //cbo_kodeterapis.Items.Clear();
                lsb_edttrps_kodeterapis.Items.Clear();
                #region(Select)
                MySqlConnection edttrps_conn2 = new MySqlConnection();
                string edttrps_query2 = "";
                edttrps_conn2 = new MySqlConnection(all_connStr);
                try
                {
                    edttrps_conn2.Open();

                    edttrps_query2 = "SELECT * FROM `terapis` order by id_terapis DESC";
                    MySqlCommand edttrps_cmd2 = new MySqlCommand(edttrps_query2, edttrps_conn2);
                    MySqlDataReader edttrps_rdr2 = edttrps_cmd2.ExecuteReader();

                    while (edttrps_rdr2.Read())
                    {
                        //cbo_kodeterapis.Items.Add(rdr.GetString(1));
                        lsb_edttrps_kodeterapis.Items.Add(edttrps_rdr2.GetString(1));
                    }
                    edttrps_rdr2.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Error Occured");
                }
                edttrps_conn2.Close();
                #endregion
            }
            

        }

        private void btn_edttrps_batal_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = true;
            pnl_edttrps_isi.Visible = false;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = true;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;

            txt_edttrps_kodeterapis.Clear();
            txt_edttrps_namaterapis.Clear();
            edttrps_lokasi_gambar = "";
            pict_edttrps_fotoKTP.Image = null;
            txt_edttrps_kodeterapis.Focus();
            rdo_edttrps_statusaktif.Checked = false;
            rdo_edttrps_statustdkaktif.Checked = false;
            btn_edttrps_simpan.Enabled = false;
            txt_edttrps_kodeterapis.Enabled = false;
            txt_edttrps_namaterapis.Enabled = false;
            btn_edttrps_browsefoto.Enabled = false;
            rdo_edttrps_statusaktif.Enabled = false;
            rdo_edttrps_statustdkaktif.Enabled = false;
            btn_edttrps_hapus.Enabled = false;

        }

        private void btn_edttrps_hapus_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah anda ingin menghapus data terapis ini?", "Alert", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                #region(Update)
                MySqlConnection edttrps_conn;
                DBConnect edttrps_sql = new DBConnect();
                string edttrps_query;
                edttrps_conn = new MySqlConnection(all_connStr);
                try
                {
                    edttrps_conn.Open();

                    edttrps_query = "DELETE FROM `terapis` WHERE `terapis`.`id_terapis` = '" + edttrps_idTerapis + "'";
                    MySqlCommand cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                    cmd.ExecuteNonQuery();
                    File.Delete(Application.StartupPath.ToString() + "\\img\\" + edttrps_namalama + ".jpg");
                    MessageBox.Show("Data terapis telah berhasil dihapus");
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
                    btn_edttrps_hapus.Enabled = false;

                    lsb_edttrps_kodeterapis.Items.Clear();
                    #region(Select)
                    MySqlConnection edttrps_conn2 = new MySqlConnection();
                    string edttrps_query2 = "";
                    edttrps_conn2 = new MySqlConnection(all_connStr);
                    try
                    {
                        edttrps_conn2.Open();

                        edttrps_query2 = "SELECT * FROM `terapis` order by id_terapis DESC";
                        MySqlCommand edttrps_cmd2 = new MySqlCommand(edttrps_query2, edttrps_conn2);
                        MySqlDataReader edttrps_rdr2 = edttrps_cmd2.ExecuteReader();

                        while (edttrps_rdr2.Read())
                        {
                            //cbo_kodeterapis.Items.Add(rdr.GetString(1));
                            lsb_edttrps_kodeterapis.Items.Add(edttrps_rdr2.GetString(1));
                        }
                        edttrps_rdr2.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        MessageBox.Show("Error Occured");
                    }
                    edttrps_conn2.Close();
                    #endregion
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Error Occured");
                }
                edttrps_conn.Close();
                #endregion
            }
            
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
            else if (double.Parse(txt_tbhpkt_komisipaketnormal.Text) > 100)
            {
                MessageBox.Show("Inputan Tidak Valid, Maksimal Komisi adalah 100%");
            }
            else if (double.Parse(txt_tbhpkt_komisipaketmidnight.Text) > 100)
            {
                MessageBox.Show("Inputan Tidak Valid, Maksimal Komisi adalah 100%");
            }

            else
            {
            #endregion
                #region(Cek Nama Paket yang sama berdasarkan Jenis Paket)
                string tbhpkt_query;
                MySqlConnection tbhpkt_conn = new MySqlConnection(all_connStr);
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
                    MessageBox.Show("Error Occured");
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

                    for (int i = 0; i < 5; i++)
                    {
                        tbhpkt_query = "INSERT INTO `paket` (`id_paket`, `jenis_paket`, `nama_paket`, `durasi_paket`, `harga_paket`, "
                        + "`komisi_normal_paket`, `komisi_midnight_paket`) VALUES (NULL, '" + cbo_tbhpkt_jenispaket.SelectedItem + "', '" + txt_tbhpkt_namapaket.Text + "', '" +
                        durasi + "', '" + txt_tbhpkt_hargapaket.Text + "', '" + txt_tbhpkt_komisipaketnormal.Text.Replace(',', '.') + "', '" + txt_tbhpkt_komisipaketmidnight.Text.Replace(',', '.') + "');";
                        tbhpkt_sql.Insert(tbhpkt_query);
                    }
                    

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

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = true;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;

            cbo_tbhpkt_jenispaket.SelectedItem = null;
            txt_tbhpkt_namapaket.Clear();
            txt_tbhpkt_durasipaketjam.Clear();
            txt_tbhpkt_durasipaketmenit.Clear();
            txt_tbhpkt_hargapaket.Clear();
            txt_tbhpkt_komisipaketnormal.Clear();
            txt_tbhpkt_komisipaketmidnight.Clear();
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
            cekinputankomisi(txt_tbhpkt_komisipaketnormal, e);
        }

        private void txt_tbhpkt_komisipaketmidnight_KeyPress(object sender, KeyPressEventArgs e)
        {
            cekinputankomisi(txt_tbhpkt_komisipaketmidnight,e);
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
                MySqlConnection edtpkt_conn = new MySqlConnection(all_connStr);
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
                        txt_edtpkt_komisipaketnormal.Text = edtpkt_rdr.GetDouble(5).ToString();
                        txt_edtpkt_komisipaketmidnight.Text = edtpkt_rdr.GetDouble(6).ToString();
                        txt_edtpkt_hargapaket.Text = edtpkt_rdr.GetString(4);
                        txt_edtpkt_durasipaketjam.Text = edtpkt_jam;
                        txt_edtpkt_durasipaketmenit.Text = edtpkt_menit;
                    }
                    edtpkt_rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Error Occured");
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
                btn_edtpkt_hapus.Enabled = true;
                #endregion
            }
            else
            {
                MessageBox.Show("Mohon pilih Jenis dan Nama Paket terlebih dahulu!");
            }
        }

        private void btn_edtpkt_simpan_Click(object sender, EventArgs e)
        {
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
            else if (double.Parse(txt_edtpkt_komisipaketnormal.Text) > 100)
            {
                MessageBox.Show("Inputan Tidak Valid, Maksimal Komisi adalah 100%");
            }
            else if (double.Parse(txt_edtpkt_komisipaketmidnight.Text) > 100)
            {
                MessageBox.Show("Inputan Tidak Valid, Maksimal Komisi adalah 100%");
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
                    edtpkt_conn = new MySqlConnection(all_connStr);
                    try
                    {
                        edtpkt_conn.Open();

                        edtpkt_query = "UPDATE `paket` SET `jenis_paket` = '" + cbo_edtpkt_jenispaket.SelectedItem + "', `nama_paket` = '" + txt_edtpkt_namapaket.Text + "', "
                                          + "`durasi_paket` = '" + txt_edtpkt_durasipaketjam.Text + " Jam " + txt_edtpkt_durasipaketmenit.Text + " Menit', `harga_paket` = '" + txt_edtpkt_hargapaket.Text + "', "
                                            + "`komisi_normal_paket` = '" + txt_edtpkt_komisipaketnormal.Text.Replace(',','.') +
                                                "', `komisi_midnight_paket` = '" + txt_edtpkt_komisipaketmidnight.Text.Replace(',', '.') +
                                                    "' WHERE `paket`.`id_paket` = " + edtpkt_idTerpilih + " ;";
                        MySqlCommand cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        MessageBox.Show("Error Occured");
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
                    btn_edtpkt_hapus.Enabled = false;

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
                    MySqlConnection edtpkt_conn2 = new MySqlConnection(all_connStr);
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
                        edtpkt_conn = new MySqlConnection(all_connStr);
                        try
                        {
                            edtpkt_conn.Open();

                            edtpkt_query = "UPDATE `paket` SET `jenis_paket` = '" + cbo_edtpkt_jenispaket.SelectedItem + "', `nama_paket` = '" + txt_edtpkt_namapaket.Text + "', "
                                              + "`durasi_paket` = '" + txt_edtpkt_durasipaketjam.Text + " Jam " + txt_edtpkt_durasipaketmenit.Text + " Menit', `harga_paket` = '" + txt_edtpkt_hargapaket.Text + "', "
                                                + "`komisi_normal_paket` = '" + txt_edtpkt_komisipaketnormal.Text.Replace(',', '.') +
                                                    "', `komisi_midnight_paket` = '" + txt_edtpkt_komisipaketmidnight.Text.Replace(',','.') +
                                                        "' WHERE `paket`.`id_paket` = " + edtpkt_idTerpilih + " ;";
                            MySqlCommand cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            MessageBox.Show("Error Occured");
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
                        btn_edtpkt_hapus.Enabled = false;

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
                        MySqlConnection edtpkt_conn2 = new MySqlConnection(all_connStr);
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
            cekinputankomisi(txt_edtpkt_komisipaketnormal,e);
        }

        private void txt_edtpkt_komisipaketmidnight_KeyPress(object sender, KeyPressEventArgs e)
        {
            cekinputankomisi(txt_edtpkt_komisipaketmidnight,e);
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

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = true;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;

            cbo_edtpkt_jenispaket.Enabled = false;
            txt_edtpkt_namapaket.Enabled = false;
            txt_edtpkt_komisipaketnormal.Enabled = false;
            txt_edtpkt_komisipaketmidnight.Enabled = false;
            txt_edtpkt_durasipaketjam.Enabled = false;
            txt_edtpkt_durasipaketmenit.Enabled = false;
            txt_edtpkt_hargapaket.Enabled = false;
            btn_edtpkt_simpan.Enabled = false;
            lsb_edtpkt_jenisnamapkt.Items.Clear();
            btn_edtpkt_hapus.Enabled = false;

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
        }

        private void btn_edtpkt_hapus_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah anda ingin menghapus data paket ini?", "Alert", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                #region(Update)
                MySqlConnection edttrps_conn;
                DBConnect edttrps_sql = new DBConnect();
                string edttrps_query;
                edttrps_conn = new MySqlConnection(all_connStr);
                try
                {
                    edttrps_conn.Open();

                    edttrps_query = "DELETE FROM `paket` WHERE `paket`.`nama_paket` = '" + txt_edtpkt_namapaket.Text + "' AND `paket`.`jenis_paket` = '" + cbo_edtpkt_jenispaket.SelectedItem.ToString() + "'";
                    MySqlCommand cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Error Occured");
                }
                edttrps_conn.Close();
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
                btn_edtpkt_hapus.Enabled = false;

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
                MySqlConnection edtpkt_conn2 = new MySqlConnection(all_connStr);
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



                MessageBox.Show("Data Paket telah berhasil dihapus");
            }

        }
        #endregion

        #region(Panel Tambah User)
        private void btn_tbhuser_simpan_Click(object sender, EventArgs e)
        {
            #region(Cek inputan kosong)
            if (txt_tbhuser_nama.Text == "")
            {
                MessageBox.Show("Mohon isi kolom Nama terlebih dahulu");
            }
            else if (txt_tbhuser_user.Text == "")
            {
                MessageBox.Show("Mohon isi kolom Username terlebih dahulu");
            }
            else if (txt_tbhuser_user.Text == "")
            {
                MessageBox.Show("Mohon isi kolom Password terlebih dahulu");
            }
            else if (cbo_tbhuser_jenisuser.SelectedItem == null)
            {
                MessageBox.Show("Mohon pilih Jenis User terlebih dahulu");
            }

            else
            {
            #endregion
                #region(Cek Nama Paket yang sama berdasarkan Jenis Paket)
                string tbhuser_query;
                //string tbhpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                MySqlConnection tbhuser_conn = new MySqlConnection(all_connStr);
                int countuser = 0;
                try
                {
                    tbhuser_conn.Open();

                    tbhuser_query = "SELECT * FROM `pengguna` WHERE `nama_pengguna` = '" + txt_tbhuser_user.Text + "'";
                    MySqlCommand tbhuser_cmd = new MySqlCommand(tbhuser_query, tbhuser_conn);
                    MySqlDataReader tbhuser_rdr = tbhuser_cmd.ExecuteReader();

                    while (tbhuser_rdr.Read())
                    {
                        countuser++;
                    }
                    tbhuser_rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                tbhuser_conn.Close();
                if (countuser != 0)
                {
                    MessageBox.Show("Maaf, Nama User: " + txt_tbhuser_user.Text + ", sudah terdaftar");
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

                    DBConnect tbhuser_sql = new DBConnect();

                    //tbhpkt_query = "INSERT INTO `paket` (`id_paket`, `jenis_paket`, `nama_paket`, `durasi_paket`, `harga_paket`, `jam_kerja`, `komisi_per_paket`) "
                    //        + "VALUES (NULL, '" +  + "', '" +  + "', '" +  + "', '" +  + "', 'Normal', '');";

                    tbhuser_query = "INSERT INTO `pengguna` (`id_pengguna`, `namaasli_pengguna` ,`nama_pengguna`, `kata_kunci`, `jenis_pengguna`)"
                        + "VALUES (NULL, '" + txt_tbhuser_nama.Text + "', '" + txt_tbhuser_user.Text + "', '" + txt_tbhuser_pass.Text + "', '" + cbo_tbhuser_jenisuser.SelectedItem + "');";
                    tbhuser_sql.Insert(tbhuser_query);

                    MessageBox.Show("User telah berhasil ditambahkan");
                    cbo_tbhuser_jenisuser.SelectedItem = null;
                    txt_tbhuser_pass.Clear();
                    txt_tbhuser_user.Clear();
                    txt_tbhuser_nama.Clear();
                }

            }
                #endregion
        }

        private void btn_tbhuser_batal_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = true;
            pnl_tbhuser_isi.Visible = false;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = true;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;
            pnl_edtuser_isi.Enabled = false;
            pnl_tbhuser_isi.Enabled = false;

            cbo_tbhuser_jenisuser.SelectedItem = null;
            txt_tbhuser_pass.Clear();
            txt_tbhuser_user.Clear();
            txt_tbhuser_nama.Clear();
        }
        #endregion

        #region(Panel Edit User)
        int edtuser_iduser;
        string edtuser_user;
        private void btn_edtuser_ok_Click(object sender, EventArgs e)
        {
            if (lsb_edtuser_user.SelectedItem != null)
            {
                btn_edtuser_simpan.Enabled = true;
                btn_edtuser_hapus.Enabled = true;
                txt_edtuser_user.Enabled = true;
                txt_edtuser_nama.Enabled = true;
                txt_edtuser_pass.Enabled = true;
                txt_edtuser_user.Enabled = true;
                cbo_edtuser_jenisuser.Enabled = true;
                string edtuser_userterpilih = lsb_edtuser_user.SelectedItem.ToString();

                string edtuser_query;

                #region(Select)
                //string edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                MySqlConnection edtuser_conn = new MySqlConnection(all_connStr);
                //List<string> edttrps_lstKode = new List<string>();
                try
                {
                    edtuser_conn.Open();

                    edtuser_query = "SELECT * FROM `pengguna` WHERE `nama_pengguna` LIKE '" + edtuser_userterpilih + "'";
                    MySqlCommand edtuser_cmd = new MySqlCommand(edtuser_query, edtuser_conn);
                    MySqlDataReader edtuser_rdr = edtuser_cmd.ExecuteReader();

                    while (edtuser_rdr.Read())
                    {
                        edtuser_iduser = edtuser_rdr.GetInt32(0);
                        txt_edtuser_user.Text = edtuser_rdr.GetString(1);
                        edtuser_user = edtuser_rdr.GetString(1);
                        txt_edtuser_pass.Text = edtuser_rdr.GetString(2);
                        txt_edtuser_nama.Text = edtuser_rdr.GetString(4);
                        cbo_edtuser_jenisuser.SelectedItem = edtuser_rdr.GetString(3);
                    }
                    edtuser_rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                edtuser_conn.Close();
                #endregion
            }
            else
            {
                MessageBox.Show("Mohon pilih User terlebih dahulu!");
            }
        }

        private void btn_edtuser_hapus_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah anda ingin menghapus data pengguna ini?", "Alert", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                #region(Update)
                MySqlConnection edtuser_conn;
                DBConnect edtuser_sql = new DBConnect();
                string edtuser_query;
                edtuser_conn = new MySqlConnection(all_connStr);
                try
                {
                    edtuser_conn.Open();

                    edtuser_query = "DELETE FROM `pengguna` WHERE `pengguna`.`nama_pengguna` = '" + edtuser_user + "'";
                    MySqlCommand edtuser_cmd = new MySqlCommand(edtuser_query, edtuser_conn);
                    edtuser_cmd.ExecuteNonQuery();

                    txt_edtuser_nama.Clear();
                    txt_edtuser_pass.Clear();
                    txt_edtuser_user.Clear();
                    cbo_edtuser_jenisuser.SelectedItem = null;
                    btn_edtuser_simpan.Enabled = false;
                    txt_edtuser_nama.Enabled = false;
                    txt_edtuser_pass.Enabled = false;
                    txt_edtuser_user.Enabled = false;
                    cbo_edtuser_jenisuser.Enabled = false;
                    btn_edtuser_hapus.Enabled = false;
                    lsb_edtuser_user.Items.Clear();
                    #region(Select)
                    string edtuser_query2;
                    //string edttrps_connStr2 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    MySqlConnection edtuser_conn2 = new MySqlConnection(all_connStr);
                    //List<string> edttrps_lstKode = new List<string>();
                    try
                    {
                        edtuser_conn2.Open();

                        edtuser_query2 = "SELECT * FROM `pengguna` order by id_pengguna DESC";
                        MySqlCommand edtuser_cmd2 = new MySqlCommand(edtuser_query2, edtuser_conn2);
                        MySqlDataReader edtuser_rdr2 = edtuser_cmd2.ExecuteReader();

                        while (edtuser_rdr2.Read())
                        {
                            //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                            lsb_edtuser_user.Items.Add(edtuser_rdr2.GetString(1));
                        }
                        edtuser_rdr2.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    edtuser_conn2.Close();
                    #endregion
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Error Occured");
                }
                edtuser_conn.Close();
                #endregion
            }
        }

        private void btn_edtuser_simpan_Click(object sender, EventArgs e)
        {
            if (txt_edtuser_nama.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Nama terlebih dahulu!");
            }
            else if (txt_edtuser_pass.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Password terlebih dahulu!");
            }
            else if (txt_edtuser_user.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Username terlebih dahulu!");
            }
            else if (cbo_edtuser_jenisuser.SelectedItem == null)
            {
                MessageBox.Show("Mohon pilih Jenis User terlebih dahulu!");
            }
            else
            {
                //string edttrps_connStr;
                MySqlConnection edtuser_conn;
                DBConnect edtuser_sql = new DBConnect();
                string edtuser_query;
                bool edtuser_namasama = false;
                //edttrps_lokasi_gambar = edttrps_lokasi_gambar.Replace(@"\", @"\\");

                #region(Select)
                //edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                edtuser_conn = new MySqlConnection(all_connStr);
                List<string> edtuser_lstpengguna = new List<string>();
                try
                {
                    edtuser_conn.Open();

                    edtuser_query = "SELECT nama_pengguna FROM `pengguna`";
                    MySqlCommand edtuser_cmd = new MySqlCommand(edtuser_query, edtuser_conn);
                    MySqlDataReader edtuser_rdr = edtuser_cmd.ExecuteReader();

                    while (edtuser_rdr.Read())
                    {
                        edtuser_lstpengguna.Add(edtuser_rdr[0].ToString());
                    }
                    edtuser_rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                edtuser_conn.Close();
                #endregion

                foreach (string edtuser_pengguna in edtuser_lstpengguna)
                {
                    if (edtuser_user != txt_edtuser_user.Text)
                    {
                        if (edtuser_pengguna == txt_edtuser_user.Text)
                        {
                            MessageBox.Show("Maaf, Nama Pengguna yang anda masukkan sudah terdaftar, silahkan ganti dengan Nama Pengguna yang berbeda!");
                            edtuser_namasama = true;
                            break;
                        }
                        else
                        {
                            edtuser_namasama = false;
                        }
                    }

                }
                if (edtuser_namasama == false)
                {
                    #region(Update)
                    //edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    edtuser_conn = new MySqlConnection(all_connStr);
                    try
                    {
                        edtuser_conn.Open();

                        edtuser_query = "UPDATE `pengguna` SET `nama_pengguna` = '" + txt_edtuser_user.Text + "', `kata_kunci` = '" + txt_edtuser_pass.Text +
                                            "', `namaasli_pengguna` = '" + txt_edtuser_nama.Text + "', `jenis_pengguna` = '" + cbo_edtuser_jenisuser.SelectedItem.ToString() +
                                              "' WHERE `pengguna`.`id_pengguna` = " + edtuser_iduser + "";
                        MySqlCommand cmd = new MySqlCommand(edtuser_query, edtuser_conn);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    edtuser_conn.Close();
                    #endregion
                    MessageBox.Show("Data user telah berhasil disimpan");
                    txt_edtuser_nama.Clear();
                    txt_edtuser_pass.Clear();
                    txt_edtuser_user.Clear();
                    cbo_edtuser_jenisuser.SelectedItem = null;
                    btn_edtuser_simpan.Enabled = false;
                    txt_edtuser_nama.Enabled = false;
                    txt_edtuser_pass.Enabled = false;
                    txt_edtuser_user.Enabled = false;
                    cbo_edtuser_jenisuser.Enabled = false;
                    btn_edtuser_hapus.Enabled = false;


                    lsb_edtuser_user.Items.Clear();
                    #region(Select)
                    string edtuser_query2;
                    //string edttrps_connStr2 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    MySqlConnection edtuser_conn2 = new MySqlConnection(all_connStr);
                    //List<string> edttrps_lstKode = new List<string>();
                    try
                    {
                        edtuser_conn2.Open();

                        edtuser_query2 = "SELECT * FROM `pengguna` order by id_pengguna DESC";
                        MySqlCommand edtuser_cmd2 = new MySqlCommand(edtuser_query2, edtuser_conn2);
                        MySqlDataReader edtuser_rdr2 = edtuser_cmd2.ExecuteReader();

                        while (edtuser_rdr2.Read())
                        {
                            //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                            lsb_edtuser_user.Items.Add(edtuser_rdr2.GetString(1));
                        }
                        edtuser_rdr2.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    edtuser_conn2.Close();
                    #endregion
                }

            }
        }

        private void btn_edtuser_batal_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = true;
            pnl_edtuser_isi.Visible = false;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = true;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;
            pnl_edtuser_isi.Enabled = false;
            pnl_tbhuser_isi.Enabled = false;

            txt_edtuser_nama.Clear();
            txt_edtuser_pass.Clear();
            txt_edtuser_user.Clear();
            cbo_edtuser_jenisuser.SelectedItem = null;
            btn_edtuser_simpan.Enabled = false;
            txt_edtuser_nama.Enabled = false;
            txt_edtuser_pass.Enabled = false;
            txt_edtuser_user.Enabled = false;
            cbo_edtuser_jenisuser.Enabled = false;
            btn_edtuser_hapus.Enabled = false;


            lsb_edtuser_user.Items.Clear();
        }
        #endregion

        #region(Panel Cetak Nota)
        DataSet ctknota_DS = new DataSet();
        int ctknota_tamuhotel;
        int ctknota_extra;
        int ctknota_countExtraColumn;
        List<int> ctknota_listidpaket = new List<int>();
        Image ctknota_logo;
        List<string> ctknota_lstcetak = new List<string>();

        private void rdo_ctknota_biasa_CheckedChanged(object sender, EventArgs e)
        {
            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = false;

            dgv_ctknota_tabelhrgpkt.DataSource = null;
            dgv_ctknota_tabelhrgpkt.Rows.Clear();
            dgv_ctknota_tabelhrgpkt.Refresh();

            cbo_ctknota_jenispaket.Items.Clear();
            #region(Select)
            string ctknota_query;
            MySqlConnection ctknota_conn = new MySqlConnection(all_connStr);
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
                MessageBox.Show("Error Occured");
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

            dgv_ctknota_tabelhrgpkt.DataSource = null;
            dgv_ctknota_tabelhrgpkt.Rows.Clear();
            dgv_ctknota_tabelhrgpkt.Refresh();

            cbo_ctknota_jenispaket.Items.Clear();
            #region(Select)
            string ctknota_query;
            MySqlConnection ctknota_conn = new MySqlConnection(all_connStr);
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
                MessageBox.Show("Error Occured");
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

            dgv_ctknota_tabelhrgpkt.DataSource = null;
            dgv_ctknota_tabelhrgpkt.Rows.Clear();
            dgv_ctknota_tabelhrgpkt.Refresh();
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

            dgv_ctknota_tabelhrgpkt.DataSource = null;
            dgv_ctknota_tabelhrgpkt.Rows.Clear();
            dgv_ctknota_tabelhrgpkt.Refresh();
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
            ctknota_listidpaket.Clear();

            if (rdo_ctknota_normal.Checked)
            {
                if (rdo_ctknota_hotel.Checked)
                {
                    #region(Select)
                    string ctknota_query;
                    MySqlConnection ctknota_conn = new MySqlConnection(all_connStr);
                    List<string> ctknota_lstKode = new List<string>();
                    
                    try
                    {
                        ctknota_conn.Open();

                        ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = '" + cbo_ctknota_jenispaket.SelectedItem + "'";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(ctknota_query, ctknota_conn);
                        MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                        mySqlDataAdapter.Fill(ctknota_DS);
                        for (int i = 0; i < ctknota_DS.Tables[0].Rows.Count; i++)
                        {
                            ctknota_listidpaket.Add(int.Parse(ctknota_DS.Tables[0].Rows[i]["id_paket"].ToString()));
                        }
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
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].Width = 200;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Format = "N0";
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(double)));
                        for (int ii = 0; ii < ctknota_DS.Tables[0].Rows.Count; ii++)
                        {
                            ctknota_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                        }
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";

                        //List<string> lstExtra = new List<string>();
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            double hasil = nominal * Convert.ToDouble(harga);
                            hasil = Math.Ceiling(hasil);
                            //lstExtra.Add(hasil.ToString());
                            ctknota_DS.Tables[0].Rows[i]["Nominal Extra"] = hasil.ToString();
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

                        //DataSet dsCloned = ctknota_DS.Clone();

                        //dsCloned.Tables[0].Columns["Harga Paket"].DataType = typeof(string);
                        //dsCloned.Tables[0].Columns["Tamu Hotel"].DataType = typeof(string);
                        //dsCloned.Tables[0].Columns["Nominal Extra"].DataType = typeof(string);

                        //dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //foreach (DataRow row in ctknota_DS.Tables[0].Rows)
                        //{
                        //    dsCloned.Tables[0].ImportRow(row);
                        //}

                        //dgv_ctknota_tabelhrgpkt.DataSource = dsCloned.Tables[0];

                        //int countdigitharga = 0;
                        //string hargapaket;
                        //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        //{
                        //    countdigitharga = 0;
                        //    hargapaket = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString();
                        //    foreach (char c in hargapaket)
                        //    {
                        //        if (char.IsDigit(c))
                        //        {
                        //            countdigitharga++;
                        //        }
                        //    }
                        //    int digit = countdigitharga;
                        //    while (digit > 3)
                        //    {
                        //        digit -= 3;
                        //        hargapaket = hargapaket.Insert(digit, ".");
                        //        dsCloned.Tables[0].Rows[i]["Harga Paket"] = hargapaket;
                        //    }

                        //}

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

                        //int countdigitextra = 0;
                        //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        //{
                        //    countdigitextra = 0;
                        //    //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                        //    //extra = Convert.ToString());
                        //    //double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                        //    //int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                        //    //double hasil = nominal * Convert.ToDouble(harga);
                        //    //extra = hasil.ToString();
                        //    //MessageBox.Show(hasilfinal.ToString());
                        //    foreach (char c in lstExtra[i])
                        //    {
                        //        if (char.IsDigit(c))
                        //        {
                        //            countdigitextra++;
                        //        }
                        //    }
                        //    int digit = countdigitextra;
                        //    while (digit > 3)
                        //    {
                        //        digit -= 3;
                        //        lstExtra[i] = lstExtra[i].Insert(digit, ".");
                        //        dsCloned.Tables[0].Rows[i]["Nominal Extra"] = lstExtra[i];
                        //    }

                        //}
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
                    MySqlConnection ctknota_conn = new MySqlConnection(all_connStr);
                    List<string> ctknota_lstKode = new List<string>();
                    try
                    {
                        ctknota_conn.Open();

                        ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = '" + cbo_ctknota_jenispaket.SelectedItem + "'";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(ctknota_query, ctknota_conn);
                        MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                        mySqlDataAdapter.Fill(ctknota_DS);
                        for (int i = 0; i < ctknota_DS.Tables[0].Rows.Count; i++)
                        {
                            ctknota_listidpaket.Add(int.Parse(ctknota_DS.Tables[0].Rows[i]["id_paket"].ToString()));
                        }
                        ctknota_DS.Tables[0].Columns.Remove("id_paket");
                        ctknota_DS.Tables[0].Columns.Remove("jenis_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        ctknota_DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        ctknota_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        ctknota_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        //ctknota_DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                        //int i = 0;
                        //int[] listI = new int[1];
                        //listI[0] = 100000;
                        //foreach (int somevalue in listI)
                        //{
                        //    DS.Tables[0].Rows[i++][1] = somevalue;  //Add value to second column of each row
                        //}
                        //for (int i = 0; i < ctknota_DS.Tables[0].Rows.Count; i++)
                        //{
                        //    ctknota_DS.Tables[0].Rows[i]["Tamu Hotel"] = ctknota_tamuhotel;
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
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].Width = 200;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].ReadOnly = true;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].Width = 175;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Format = "N0";
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(double)));
                        for (int ii = 0; ii < ctknota_DS.Tables[0].Rows.Count; ii++)
                        {
                            ctknota_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                        }
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";

                        //List<string> lstExtra = new List<string>();
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;

                            int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            double hasil = nominal * Convert.ToDouble(harga);
                            hasil = Math.Ceiling(hasil);
                            //lstExtra.Add(hasil.ToString());
                            ctknota_DS.Tables[0].Rows[i]["Nominal Extra"] = hasil.ToString();
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

                        //DataSet dsCloned = ctknota_DS.Clone();

                        //dsCloned.Tables[0].Columns["Harga Paket"].DataType = typeof(string);
                        //dsCloned.Tables[0].Columns["Tamu Hotel"].DataType = typeof(string);
                        //dsCloned.Tables[0].Columns["Nominal Extra"].DataType = typeof(string);

                        //dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //foreach (DataRow row in ctknota_DS.Tables[0].Rows)
                        //{
                        //    dsCloned.Tables[0].ImportRow(row);
                        //}

                        //dgv_ctknota_tabelhrgpkt.DataSource = dsCloned.Tables[0];

                        //int countdigitharga = 0;
                        //string hargapaket;
                        //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        //{
                        //    countdigitharga = 0;
                        //    hargapaket = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString();
                        //    foreach (char c in hargapaket)
                        //    {
                        //        if (char.IsDigit(c))
                        //        {
                        //            countdigitharga++;
                        //        }
                        //    }
                        //    int digit = countdigitharga;
                        //    while (digit > 3)
                        //    {
                        //        digit -= 3;
                        //        hargapaket = hargapaket.Insert(digit, ".");
                        //        dsCloned.Tables[0].Rows[i]["Harga Paket"] = hargapaket;
                        //    }

                        //}

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

                        //int countdigitextra = 0;
                        //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        //{
                        //    countdigitextra = 0;
                        //    //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                        //    //extra = Convert.ToString());
                        //    //double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                        //    //int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                        //    //double hasil = nominal * Convert.ToDouble(harga);
                        //    //extra = hasil.ToString();
                        //    //MessageBox.Show(hasilfinal.ToString());
                        //    foreach (char c in lstExtra[i])
                        //    {
                        //        if (char.IsDigit(c))
                        //        {
                        //            countdigitextra++;
                        //        }
                        //    }
                        //    int digit = countdigitextra;
                        //    while (digit > 3)
                        //    {
                        //        digit -= 3;
                        //        lstExtra[i] = lstExtra[i].Insert(digit, ".");
                        //        dsCloned.Tables[0].Rows[i]["Nominal Extra"] = lstExtra[i];
                        //    }

                        //}
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
                    MySqlConnection ctknota_conn = new MySqlConnection(all_connStr);
                    List<string> ctknota_lstKode = new List<string>();
                    try
                    {
                        ctknota_conn.Open();

                        ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = '" + cbo_ctknota_jenispaket.SelectedItem + "'";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(ctknota_query, ctknota_conn);
                        MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                        mySqlDataAdapter.Fill(ctknota_DS);
                        for (int i = 0; i < ctknota_DS.Tables[0].Rows.Count; i++)
                        {
                            ctknota_listidpaket.Add(int.Parse(ctknota_DS.Tables[0].Rows[i]["id_paket"].ToString()));
                        }
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
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].Width = 200;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Format = "N0";
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(double)));
                        for (int ii = 0; ii < ctknota_DS.Tables[0].Rows.Count; ii++)
                        {
                            ctknota_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                        }
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";

                        //List<string> lstExtra = new List<string>();
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            double hasil = nominal * Convert.ToDouble(harga);
                            hasil = Math.Ceiling(hasil);
                            //lstExtra.Add(hasil.ToString());
                            ctknota_DS.Tables[0].Rows[i]["Nominal Extra"] = hasil.ToString();
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

                        //DataSet dsCloned = ctknota_DS.Clone();

                        //dsCloned.Tables[0].Columns["Harga Paket"].DataType = typeof(string);
                        //dsCloned.Tables[0].Columns["Tamu Hotel"].DataType = typeof(string);
                        //dsCloned.Tables[0].Columns["Nominal Extra"].DataType = typeof(string);

                        //dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //foreach (DataRow row in ctknota_DS.Tables[0].Rows)
                        //{
                        //    dsCloned.Tables[0].ImportRow(row);
                        //}

                        //dgv_ctknota_tabelhrgpkt.DataSource = dsCloned.Tables[0];

                        //int countdigitharga = 0;
                        //string hargapaket;
                        //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        //{
                        //    countdigitharga = 0;
                        //    hargapaket = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString();
                        //    foreach (char c in hargapaket)
                        //    {
                        //        if (char.IsDigit(c))
                        //        {
                        //            countdigitharga++;
                        //        }
                        //    }
                        //    int digit = countdigitharga;
                        //    while (digit > 3)
                        //    {
                        //        digit -= 3;
                        //        hargapaket = hargapaket.Insert(digit, ".");
                        //        dsCloned.Tables[0].Rows[i]["Harga Paket"] = hargapaket;
                        //    }

                        //}

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

                        //int countdigitextra = 0;
                        //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        //{
                        //    countdigitextra = 0;
                        //    //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                        //    //extra = Convert.ToString());
                        //    //double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                        //    //int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                        //    //double hasil = nominal * Convert.ToDouble(harga);
                        //    //extra = hasil.ToString();
                        //    //MessageBox.Show(hasilfinal.ToString());
                        //    foreach (char c in lstExtra[i])
                        //    {
                        //        if (char.IsDigit(c))
                        //        {
                        //            countdigitextra++;
                        //        }
                        //    }
                        //    int digit = countdigitextra;
                        //    while (digit > 3)
                        //    {
                        //        digit -= 3;
                        //        lstExtra[i] = lstExtra[i].Insert(digit, ".");
                        //        dsCloned.Tables[0].Rows[i]["Nominal Extra"] = lstExtra[i];
                        //    }

                        //}
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
                    MySqlConnection ctknota_conn = new MySqlConnection(all_connStr);
                    List<string> ctknota_lstKode = new List<string>();
                    try
                    {
                        ctknota_conn.Open();

                        ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = '" + cbo_ctknota_jenispaket.SelectedItem + "'";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(ctknota_query, ctknota_conn);
                        MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                        mySqlDataAdapter.Fill(ctknota_DS);
                        for (int i = 0; i < ctknota_DS.Tables[0].Rows.Count; i++)
                        {
                            ctknota_listidpaket.Add(int.Parse(ctknota_DS.Tables[0].Rows[i]["id_paket"].ToString()));
                        }
                        ctknota_DS.Tables[0].Columns.Remove("id_paket");
                        ctknota_DS.Tables[0].Columns.Remove("jenis_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        ctknota_DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        ctknota_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        ctknota_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        //ctknota_DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                        ////int i = 0;
                        ////int[] listI = new int[1];
                        ////listI[0] = 100000;
                        ////foreach (int somevalue in listI)
                        ////{
                        ////    DS.Tables[0].Rows[i++][1] = somevalue;  //Add value to second column of each row
                        ////}
                        //for (int i = 0; i < ctknota_DS.Tables[0].Rows.Count; i++)
                        //{
                        //    ctknota_DS.Tables[0].Rows[i]["Tamu Hotel"] = ctknota_tamuhotel;
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
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].Width = 200;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].ReadOnly = true;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].Width = 175;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Format = "N0";
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(double)));
                        for (int ii = 0; ii < ctknota_DS.Tables[0].Rows.Count; ii++)
                        {
                            ctknota_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                        }
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";

                        //List<string> lstExtra = new List<string>();
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            double hasil = nominal * Convert.ToDouble(harga);
                            hasil = Math.Ceiling(hasil);
                            //lstExtra.Add(hasil.ToString());
                            ctknota_DS.Tables[0].Rows[i]["Nominal Extra"] = hasil.ToString();
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

                        //DataSet dsCloned = ctknota_DS.Clone();

                        //dsCloned.Tables[0].Columns["Harga Paket"].DataType = typeof(string);
                        //dsCloned.Tables[0].Columns["Tamu Hotel"].DataType = typeof(string);
                        //dsCloned.Tables[0].Columns["Nominal Extra"].DataType = typeof(string);

                        //dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //foreach (DataRow row in ctknota_DS.Tables[0].Rows)
                        //{
                        //    dsCloned.Tables[0].ImportRow(row);
                        //}

                        //dgv_ctknota_tabelhrgpkt.DataSource = dsCloned.Tables[0];

                        //int countdigitharga = 0;
                        //string hargapaket;
                        //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        //{
                        //    countdigitharga = 0;
                        //    hargapaket = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString();
                        //    foreach (char c in hargapaket)
                        //    {
                        //        if (char.IsDigit(c))
                        //        {
                        //            countdigitharga++;
                        //        }
                        //    }
                        //    int digit = countdigitharga;
                        //    while (digit > 3)
                        //    {
                        //        digit -= 3;
                        //        hargapaket = hargapaket.Insert(digit, ".");
                        //        dsCloned.Tables[0].Rows[i]["Harga Paket"] = hargapaket;
                        //    }

                        //}

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

                        //int countdigitextra = 0;
                        //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        //{
                        //    countdigitextra = 0;
                        //    //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                        //    //extra = Convert.ToString());
                        //    //double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                        //    //int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                        //    //double hasil = nominal * Convert.ToDouble(harga);
                        //    //extra = hasil.ToString();
                        //    //MessageBox.Show(hasilfinal.ToString());
                        //    foreach (char c in lstExtra[i])
                        //    {
                        //        if (char.IsDigit(c))
                        //        {
                        //            countdigitextra++;
                        //        }
                        //    }
                        //    int digit = countdigitextra;
                        //    while (digit > 3)
                        //    {
                        //        digit -= 3;
                        //        lstExtra[i] = lstExtra[i].Insert(digit, ".");
                        //        dsCloned.Tables[0].Rows[i]["Nominal Extra"] = lstExtra[i];
                        //    }

                        //}
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
            txt_ctknota_fee.Enabled = false;
            txt_ctknota_fee.Text = "";
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
            txt_ctknota_fee.Text = "";
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
            //txt_ctknota_fee.Enabled = true;
            txt_ctknota_nomorruangan.Enabled = true;
            btn_ctknota_batal.Enabled = true;

            #region(Select)
            string ctknota_query3;
            MySqlConnection ctknota_conn3 = new MySqlConnection(all_connStr);
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
            //txt_ctknota_fee.Enabled = true;
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

        private void ctknota_insertnota(string kasus)
        {
            ctknota_lstcetak.Clear();
            int ctknota_idnota = 0;
            #region(Select)
                    string ctknota_query2;
                    MySqlConnection ctknota_conn2 = new MySqlConnection(all_connStr);
                    
                    try
                    {
                        ctknota_conn2.Open();

                        ctknota_query2 = "SELECT `id_nota` FROM `nota` ORDER BY `nota`.`id_nota` DESC LIMIT 1";
                        //MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(ctknota_query, ctknota_conn);
                        MySqlCommand ctknota_cmd2 = new MySqlCommand(ctknota_query2, ctknota_conn2);
                        MySqlDataReader ctknota_rdr2 = ctknota_cmd2.ExecuteReader();
                        while (ctknota_rdr2.Read())
	                    {
	                        ctknota_idnota = ctknota_rdr2.GetInt32(0);
	                    }
                        ctknota_conn2.Close();
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                        MessageBox.Show("Error Occurred");
                    }
                    #endregion
            if (kasus == "diskon kosong, fee kosong")
            {
                #region(Insert Nota)
                            //string tanggalcetak = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                            string jamkerja = "";
                            string tamuhotel = "";
                            int potonganhotel = 0; ;
                            string nomorruangan = "";
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
                            int grandtotal = 0;
                            int idpaket = 0;
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
                                    idpaket = ctknota_listidpaket[i];
                                    nomorruangan = txt_ctknota_nomorruangan.Text;
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
                                    totalbayar = hargapaket  + nominalextra + fee;
                                    grandtotal = totalbayar - potonganhotel - diskon;
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
                                                            + " `diskon_nota`, `keterangan_nota`, `totalbayar_nota`, `feeterapis_nota`, `jenisbayar_nota`, `status_nota`, `id_paket`, `grandtotal_nota`) "
                                                                + "VALUES (NULL, (now()), '" + nomorruangan + "', '" + jamkerja + "', '" + tamuhotel + "', '" + potonganhotel + "',"
                                                                    + " '" + namapaket + "', '" + hargapaket + "', '" + extra + "', '" + nominalextra + "', '" + kodeterapis + "', '" + namaterapis + "', "
                                                                        + "'" + diskon + "', '" + ket + "', '" + totalbayar + "', '" + fee + "', '" + jenisbayar + "', '-', '"+idpaket+"', '"+grandtotal+"');";
                            ctknota_sql.Insert(ctknota_query);

                            #region(Simpan Data Buat Cetak)
                            string value = namapaket;
                            Char splitter = '-';
                            string[] namajenispaket = value.Split(splitter);

                            ctknota_lstcetak.Add(ctknota_idnota.ToString());
                            if (login_namauser.Length > 12)
                            {
                                ctknota_lstcetak.Add(login_namauser.ToUpper().Substring(0, 12));
                            }
                            else
                            {
                                ctknota_lstcetak.Add(login_namauser.ToUpper());
                            }
                            ctknota_lstcetak.Add(nomorruangan.ToString());
                            ctknota_lstcetak.Add(kodeterapis.ToString());
                            ctknota_lstcetak.Add(namajenispaket[0].Substring(0, namajenispaket[0].Length-1));
                            ctknota_lstcetak.Add(jenisbayar);
                            ctknota_lstcetak.Add(tamuhotel);
                            ctknota_lstcetak.Add(namajenispaket[1].Substring(1));
                            ctknota_lstcetak.Add(hargapaket.ToString());
                            ctknota_lstcetak.Add(extra);
                            ctknota_lstcetak.Add(nominalextra.ToString());
                            ctknota_lstcetak.Add(fee.ToString());
                            ctknota_lstcetak.Add((hargapaket+nominalextra+fee).ToString());
                            ctknota_lstcetak.Add(potonganhotel.ToString());
                            ctknota_lstcetak.Add(diskon.ToString());
                            ctknota_lstcetak.Add(grandtotal.ToString());
                            #endregion
                #endregion


                            string totalbayarFinal = grandtotal.ToString(String.Format("0,0", totalbayar));
                                   
                            lbl_ctknota_totalbyr.Text = totalbayarFinal;
                            MessageBox.Show("Nota telah berhasil dibuat");
                            ctknota_logo = Image.FromFile("C:\\Users\\William\\Documents\\Visual Studio 2010\\Projects\\Green Leaf\\Green Leaf\\bin\\Debug\\img\\logo_small.png");
                            PrintDialog printDialog = new PrintDialog();

                            PrintDocument printDocument = new PrintDocument();

                            printDialog.Document = printDocument; //add the document to the dialog box...        

                            printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(CreateReceipt); //add an event handler that will do the printing

                            //on a till you will not want to ask the user where to print but this is fine for the test envoironment.

                            //DialogResult result = printDialog.ShowDialog();

                            //if (result == DialogResult.OK)
                            //{
                            printDocument.Print();
                            //}
            }
            else if (kasus == "diskon kosong, fee ada")
            {
                #region(Insert Nota)
                //string tanggalcetak = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                string jamkerja = "";
                string tamuhotel = "";
                int potonganhotel = 0; ;
                string nomorruangan = "";
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
                int grandtotal = 0;
                int idpaket = 0;
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
                        nomorruangan = txt_ctknota_nomorruangan.Text;
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
                        idpaket = ctknota_listidpaket[i];
                        kodeterapis = int.Parse(cbo_ctknota_kodeterapis.SelectedItem.ToString());
                        namaterapis = txt_ctknota_namaterapis.Text;
                        diskon = 0;
                        ket = "";
                        fee = int.Parse(txt_ctknota_fee.Text);
                        totalbayar = hargapaket + nominalextra + fee;
                        grandtotal = totalbayar - potonganhotel - diskon;
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
                                                + " `diskon_nota`, `keterangan_nota`, `totalbayar_nota`, `feeterapis_nota`, `jenisbayar_nota`, `status_nota`, `id_paket` , `grandtotal_nota`) "
                                                    + "VALUES (NULL, (now()), '" + nomorruangan + "', '" + jamkerja + "', '" + tamuhotel + "', '" + potonganhotel + "',"
                                                        + " '" + namapaket + "', '" + hargapaket + "', '" + extra + "', '" + nominalextra + "', '" + kodeterapis + "', '" + namaterapis + "', "
                                                            + "'" + diskon + "', '" + ket + "', '" + totalbayar + "', '" + fee + "', '" + jenisbayar + "', '-', '" + idpaket + "', '" + grandtotal + "');";
                ctknota_sql.Insert(ctknota_query);

                #region(Simpan Data Buat Cetak)
                string value = namapaket;
                Char splitter = '-';
                string[] namajenispaket = value.Split(splitter);

                ctknota_lstcetak.Add(ctknota_idnota.ToString());
                if (login_namauser.Length > 12)
                {
                    ctknota_lstcetak.Add(login_namauser.ToUpper().Substring(0, 12));
                }
                else
                {
                    ctknota_lstcetak.Add(login_namauser.ToUpper());
                }
                ctknota_lstcetak.Add(nomorruangan.ToString());
                ctknota_lstcetak.Add(kodeterapis.ToString());
                ctknota_lstcetak.Add(namajenispaket[0].Substring(0, namajenispaket[0].Length - 1));
                ctknota_lstcetak.Add(jenisbayar);
                ctknota_lstcetak.Add(tamuhotel);
                ctknota_lstcetak.Add(namajenispaket[1].Substring(1));
                ctknota_lstcetak.Add(hargapaket.ToString());
                ctknota_lstcetak.Add(extra);
                ctknota_lstcetak.Add(nominalextra.ToString());
                ctknota_lstcetak.Add(fee.ToString());
                ctknota_lstcetak.Add((hargapaket + nominalextra + fee).ToString());
                ctknota_lstcetak.Add(potonganhotel.ToString());
                ctknota_lstcetak.Add(diskon.ToString());
                ctknota_lstcetak.Add(grandtotal.ToString());
                #endregion

                #endregion
                string totalbayarFinal = grandtotal.ToString(String.Format("0,0", totalbayar));
                lbl_ctknota_totalbyr.Text = totalbayarFinal;
                MessageBox.Show("Nota telah berhasil ditambahkan");
                ctknota_logo = Image.FromFile("C:\\Users\\William\\Documents\\Visual Studio 2010\\Projects\\Green Leaf\\Green Leaf\\bin\\Debug\\img\\logo_small.png");
                PrintDialog printDialog = new PrintDialog();

                PrintDocument printDocument = new PrintDocument();

                printDialog.Document = printDocument; //add the document to the dialog box...        

                printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(CreateReceipt); //add an event handler that will do the printing

                //on a till you will not want to ask the user where to print but this is fine for the test envoironment.

                //DialogResult result = printDialog.ShowDialog();

                //if (result == DialogResult.OK)
                //{
                printDocument.Print();
                //}
            }
            else if (kasus == "diskon ada, fee kosong")
            {
                #region(Insert Nota)
                                //string tanggalcetak = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                                string jamkerja = "";
                                string tamuhotel = "";
                                int potonganhotel = 0; ;
                                string nomorruangan = "";
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
                                int grandtotal = 0;
                                int idpaket = 0;
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
                                        nomorruangan = txt_ctknota_nomorruangan.Text;
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
                                        idpaket = ctknota_listidpaket[i];
                                        kodeterapis = int.Parse(cbo_ctknota_kodeterapis.SelectedItem.ToString());
                                        namaterapis = txt_ctknota_namaterapis.Text;
                                        diskon = int.Parse(txt_ctknota_diskon.Text);
                                        ket = txt_ctknota_ket.Text;
                                        fee = 0;
                                        totalbayar = hargapaket + nominalextra + fee;
                                        grandtotal = totalbayar - potonganhotel - diskon;
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
                                                                + " `diskon_nota`, `keterangan_nota`, `totalbayar_nota`, `feeterapis_nota`, `jenisbayar_nota`, `status_nota`, `id_paket`, `grandtotal_nota`) "
                                                                    + "VALUES (NULL, (now()), '" + nomorruangan + "', '" + jamkerja + "', '" + tamuhotel + "', '" + potonganhotel + "',"
                                                                        + " '" + namapaket + "', '" + hargapaket + "', '" + extra + "', '" + nominalextra + "', '" + kodeterapis + "', '" + namaterapis + "', "
                                                                            + "'" + diskon + "', '" + ket + "', '" + totalbayar + "', '" + fee + "', '" + jenisbayar + "', '-', '"+idpaket+"', '"+grandtotal+"');";
                                ctknota_sql.Insert(ctknota_query);

                                #region(Simpan Data Buat Cetak)
                                string value = namapaket;
                                Char splitter = '-';
                                string[] namajenispaket = value.Split(splitter);

                                ctknota_lstcetak.Add(ctknota_idnota.ToString());
                                if (login_namauser.Length > 12)
                                {
                                    ctknota_lstcetak.Add(login_namauser.ToUpper().Substring(0, 12));
                                }
                                else
                                {
                                    ctknota_lstcetak.Add(login_namauser.ToUpper());
                                }
                                ctknota_lstcetak.Add(nomorruangan.ToString());
                                ctknota_lstcetak.Add(kodeterapis.ToString());
                                ctknota_lstcetak.Add(namajenispaket[0].Substring(0, namajenispaket[0].Length - 1));
                                ctknota_lstcetak.Add(jenisbayar);
                                ctknota_lstcetak.Add(tamuhotel);
                                ctknota_lstcetak.Add(namajenispaket[1].Substring(1));
                                ctknota_lstcetak.Add(hargapaket.ToString());
                                ctknota_lstcetak.Add(extra);
                                ctknota_lstcetak.Add(nominalextra.ToString());
                                ctknota_lstcetak.Add(fee.ToString());
                                ctknota_lstcetak.Add((hargapaket + nominalextra + fee).ToString());
                                ctknota_lstcetak.Add(potonganhotel.ToString());
                                ctknota_lstcetak.Add(diskon.ToString());
                                ctknota_lstcetak.Add(grandtotal.ToString());
                                #endregion


                                #endregion
                                string totalbayarFinal = grandtotal.ToString(String.Format("0,0", totalbayar));
                                //int countdigittotal = 0;
                                //foreach (char c in totalbayar.ToString())
                                //{
                                //    if (char.IsDigit(c))
                                //    {
                                //        countdigittotal++;
                                //    }
                                //}
                                //int digit = countdigittotal;
                                //int countdigitend = 0;
                                //while (digit > 3)
                                //{
                                //    countdigitend++;
                                //    digit -= 3;
                                //    totalbayarFinal = totalbayar.ToString().Insert(digit, ".");
                                //}
                                //totalbayarFinal = totalbayarFinal.Insert(countdigittotal + countdigitend, ",-");
                                lbl_ctknota_totalbyr.Text = totalbayarFinal;
                                MessageBox.Show("Nota telah berhasil ditambahkan");
                                ctknota_logo = Image.FromFile("C:\\Users\\William\\Documents\\Visual Studio 2010\\Projects\\Green Leaf\\Green Leaf\\bin\\Debug\\img\\logo_small.png");
                                PrintDialog printDialog = new PrintDialog();

                                PrintDocument printDocument = new PrintDocument();

                                printDialog.Document = printDocument; //add the document to the dialog box...        

                                printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(CreateReceipt); //add an event handler that will do the printing

                                //on a till you will not want to ask the user where to print but this is fine for the test envoironment.

                                //DialogResult result = printDialog.ShowDialog();

                                //if (result == DialogResult.OK)
                                //{
                                printDocument.Print();
                //}
            }
            else if (kasus == "diskon ada, fee ada")
            {
                #region(Insert Nota)
                                //string tanggalcetak = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                                string jamkerja = "";
                                string tamuhotel = "";
                                int potonganhotel = 0; ;
                                string nomorruangan = "";
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
                                int grandtotal = 0;
                                int idpaket = 0;
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
                                        idpaket = ctknota_listidpaket[i];
                                        nomorruangan = txt_ctknota_nomorruangan.Text;
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
                                        totalbayar = hargapaket + nominalextra + fee;
                                        grandtotal = totalbayar - potonganhotel - diskon;
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
                                                                + " `diskon_nota`, `keterangan_nota`, `totalbayar_nota`, `feeterapis_nota`, `jenisbayar_nota`, `status_nota`, `id_paket`, `grandtotal_nota`) "
                                                                    + "VALUES (NULL, (now()), '" + nomorruangan + "', '" + jamkerja + "', '" + tamuhotel + "', '" + potonganhotel + "',"
                                                                        + " '" + namapaket + "', '" + hargapaket + "', '" + extra + "', '" + nominalextra + "', '" + kodeterapis + "', '" + namaterapis + "', "
                                                                            + "'" + diskon + "', '" + ket + "', '" + totalbayar + "', '" + fee + "', '" + jenisbayar + "', '-', '"+idpaket+"', '"+grandtotal+"');";
                                ctknota_sql.Insert(ctknota_query);

                                #region(Simpan Data Buat Cetak)
                                string value = namapaket;
                                Char splitter = '-';
                                string[] namajenispaket = value.Split(splitter);

                                ctknota_lstcetak.Add(ctknota_idnota.ToString());
                                if (login_namauser.Length > 12)
                                {
                                    ctknota_lstcetak.Add(login_namauser.ToUpper().Substring(0, 12));
                                }
                                else
                                {
                                    ctknota_lstcetak.Add(login_namauser.ToUpper());
                                }
                                ctknota_lstcetak.Add(nomorruangan.ToString());
                                ctknota_lstcetak.Add(kodeterapis.ToString());
                                ctknota_lstcetak.Add(namajenispaket[0].Substring(0, namajenispaket[0].Length - 1));
                                ctknota_lstcetak.Add(jenisbayar);
                                ctknota_lstcetak.Add(tamuhotel);
                                ctknota_lstcetak.Add(namajenispaket[1].Substring(1));
                                ctknota_lstcetak.Add(hargapaket.ToString());
                                ctknota_lstcetak.Add(extra);
                                ctknota_lstcetak.Add(nominalextra.ToString());
                                ctknota_lstcetak.Add(fee.ToString());
                                ctknota_lstcetak.Add((hargapaket + nominalextra + fee).ToString());
                                ctknota_lstcetak.Add(potonganhotel.ToString());
                                ctknota_lstcetak.Add(diskon.ToString());
                                ctknota_lstcetak.Add(grandtotal.ToString());
                                #endregion


                                #endregion
                                string totalbayarFinal = grandtotal.ToString(String.Format("0,0", totalbayar));
                                //int countdigittotal = 0;
                                //foreach (char c in totalbayar.ToString())
                                //{
                                //    if (char.IsDigit(c))
                                //    {
                                //        countdigittotal++;
                                //    }
                                //}
                                //int digit = countdigittotal;
                                //int countdigitend = 0;
                                //while (digit > 3)
                                //{
                                //    countdigitend++;
                                //    digit -= 3;
                                //    totalbayarFinal = totalbayar.ToString().Insert(digit, ".");
                                //}
                                //totalbayarFinal = totalbayarFinal.Insert(countdigittotal + countdigitend, ",-");
                                lbl_ctknota_totalbyr.Text = totalbayarFinal;
                                MessageBox.Show("Nota telah berhasil ditambahkan");
                                ctknota_logo = Image.FromFile("C:\\Users\\William\\Documents\\Visual Studio 2010\\Projects\\Green Leaf\\Green Leaf\\bin\\Debug\\img\\logo_small.png");
                                PrintDialog printDialog = new PrintDialog();

                                PrintDocument printDocument = new PrintDocument();

                                printDialog.Document = printDocument; //add the document to the dialog box...        

                                printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(CreateReceipt); //add an event handler that will do the printing

                                //on a till you will not want to ask the user where to print but this is fine for the test envoironment.

                                //DialogResult result = printDialog.ShowDialog();

                                //if (result == DialogResult.OK)
                                //{
                                printDocument.Print();
                //}
            }
            
        }

        private void ctknota_clearform()
        {
            #region(Bersihkan Form)
            dgv_ctknota_tabelhrgpkt.DataSource = null;
            dgv_ctknota_tabelhrgpkt.Rows.Clear();
            dgv_ctknota_tabelhrgpkt.Refresh();
            ctknota_DS.Tables.Clear();
            ctknota_countExtraColumn = 0;

            rdo_ctknota_biasa.Checked = false;
            rdo_ctknota_cash.Checked = false;
            rdo_ctknota_credit.Checked = false;
            rdo_ctknota_hotel.Checked = false;
            rdo_ctknota_midnight.Checked = false;
            rdo_ctknota_normal.Checked = false;
            cbo_ctknota_jenispaket.Items.Clear();
            txt_ctknota_namaterapis.Clear();
            txt_ctknota_nomorruangan.Clear();
            lbl_ctknota_totalbyr.Text = "";
            txt_ctknota_fee.Clear();
            txt_ctknota_diskon.Clear();
            txt_ctknota_ket.Clear();

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

            //#region(Ambil data Potongan dan Extra dari database)
            //string ctknota_query2;
            //string ctknota_connStr2 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            //MySqlConnection ctknota_conn2 = new MySqlConnection(ctknota_connStr2);
            //try
            //{
            //    ctknota_conn2.Open();

            //    ctknota_query2 = "SELECT * FROM `variabel`";
            //    MySqlCommand ctknota_cmd2 = new MySqlCommand(ctknota_query2, ctknota_conn2);
            //    MySqlDataReader ctknota_rdr2 = ctknota_cmd2.ExecuteReader();

            //    while (ctknota_rdr2.Read())
            //    {
            //        //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
            //        //cbo_ctknota_jenispaket.Items.Add(ctknota_rdr.GetString(0));
            //        ctknota_extra = ctknota_rdr2.GetInt32(1);
            //        ctknota_tamuhotel = ctknota_rdr2.GetInt32(2);
            //    }
            //    ctknota_rdr2.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
            //ctknota_conn2.Close();
            //#endregion

            cbo_ctknota_kodeterapis.Items.Clear();
            #region(Select)
            string ctknota_query3;
            MySqlConnection ctknota_conn3 = new MySqlConnection(all_connStr);
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
            #endregion
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
                    #region(cek)
                    if (txt_ctknota_diskon.Text == "")
                    {
                        
                        if (txt_ctknota_fee.Text == "")
                        {
                            DialogResult dialogResult = MessageBox.Show("Apakah anda sudah yakin?", "Alert", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                #region(tes print)
                                //photo = Image.FromFile("C:\\Users\\William\\Documents\\Visual Studio 2010\\Projects\\Green Leaf\\Green Leaf\\bin\\Debug\\img\\logo_small.jpg");
                                //PrintDocument printDoc = new PrintDocument();

                                //Margins margins = new Margins(1, 10, 10, 10);
                                //printdoc_ctknota_printdokumen.DefaultPageSettings.Margins = margins;
                                //printDoc.PrintPage += new PrintPageEventHandler(printdoc_ctknota_printdokumen_PrintPage);
                                //printDoc.Print();
                                //PrintPreviewDialog dlg = new PrintPreviewDialog();
                                //dlg.Document = printDoc;
                                //dlg.ShowDialog();


                                //string s = "Ini Budi\n"+
                                //                "Ini Bapak Budi";

                                //PrintDocument p = new PrintDocument();
                                //p.PrintPage += delegate(object sender1, PrintPageEventArgs e1)
                                //{
                                //    e1.Graphics.DrawString(s, new Font("Monospaced Sans Serif", 10), new SolidBrush(Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));

                                //};
                                //try
                                //{
                                //    p.Print();
                                //}
                                //catch (Exception ex)
                                //{
                                //    throw new Exception("Exception Occured While Printing", ex);
                                //}

                                //PrintDialog printDialog = new PrintDialog();

                                //PrintDocument printDocument = new PrintDocument();

                                //printDialog.Document = printDocument; //add the document to the dialog box...        

                                //printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(CreateReceipt); //add an event handler that will do the printing

                                ////on a till you will not want to ask the user where to print but this is fine for the test envoironment.

                                //DialogResult result = printDialog.ShowDialog();

                                //if (result == DialogResult.OK)
                                //{
                                //    printDocument.Print();

                                //}
                                #endregion
                                ctknota_insertnota("diskon kosong, fee kosong");
                                ctknota_clearform();
                                //MessageBox.Show("diskon kosong, fee kosong");
                            }
                        }
                        else
                        {
                            DialogResult dialogResult = MessageBox.Show("Apakah anda sudah yakin?", "Alert", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                ctknota_insertnota("diskon kosong, fee ada");
                                ctknota_clearform();
                                //MessageBox.Show("diskon kosong, fee ada");
                            }
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
                                DialogResult dialogResult = MessageBox.Show("Apakah anda sudah yakin?", "Alert", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    ctknota_insertnota("diskon ada, fee kosong");
                                    ctknota_clearform();
                                    //MessageBox.Show("diskon ada, fee kosong");
                                }
                            }
                            else
                            {
                                DialogResult dialogResult = MessageBox.Show("Apakah anda sudah yakin?", "Alert", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    ctknota_insertnota("diskon ada, fee ada");
                                    ctknota_clearform();
                                    //MessageBox.Show("diskon ada, fee ada");
                                }
                            }
                        }

                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("Mohon pilih paket terlebih dahulu");
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

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = true;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;

            ctknota_clearform();
        }
        #endregion

        #region(Panel Ganti Variabel)
        double edtvariabel_extra;
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
                MySqlConnection edtpkt_conn;
                DBConnect edtpkt_sql = new DBConnect();
                string edtpkt_query;
                edtpkt_conn = new MySqlConnection(all_connStr);
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
                    MessageBox.Show("Error Occured");
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

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = true;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;
        }
        #endregion

        #region(Panel Laporan Penjualan)
        string lprnpnjln_tanggalcetakdari = "";
        string lprnpnjln_tanggalcetaksampai = "";
        Int64 lprnpnjln_potonganhotel = 0;
        Int64 lprnpnjln_hargapaket = 0;
        Int64 lprnpnjln_extra = 0;
        Int64 lprnpnjln_diskon = 0;
        Int64 lprnpnjln_totalbayar = 0;
        Int64 lprnpnjln_totalbayarcash = 0;
        Int64 lprnpnjln_totalbayarcredit = 0;
        Int64 lprnpnjln_feeterapis = 0;
        Int64 lprnpnjln_grandtotal = 0;
        DataGridViewButtonColumn lprnpnjln_hapusNota = new DataGridViewButtonColumn();
        DataGridViewButtonColumn lprnpnjln_batalhapusNota = new DataGridViewButtonColumn();
        DataSet lprnpnjln_DS = new DataSet();

        private void dtp_lprnpnjln_tgldari_ValueChanged(object sender, EventArgs e)
        {

            //rdo_lprnpnjln_all.Enabled = true;
            //rdo_lprnpnjln_cash.Enabled = true;
            //rdo_lprnpnjln_credit.Enabled = true;

            btn_lprnpnjln_excel.Enabled = true;
            rdo_lprnpnjln_all.Checked = true;
            rdo_lprnpnjln_cash.Enabled = true;
            rdo_lprnpnjln_credit.Enabled = true;
            rdo_lprnpnjln_all.Enabled = true;
            #region(Select)
            dgv_lprnpnjln_tabellaporan.DataSource = null;
            dgv_lprnpnjln_tabellaporan.Rows.Clear();
            dgv_lprnpnjln_tabellaporan.Columns.Clear();
            //dgv_lprnpnjln_tabellaporan.Refresh();
            lprnpnjln_DS.Tables.Clear();
            string lprnpnjln_query;
            MySqlConnection lprnpnjln_conn = new MySqlConnection(all_connStr);
            try
            {
                lprnpnjln_tanggalcetakdari = dtp_lprnpnjln_tgldari.Value.Year.ToString();
                lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Month.ToString();
                lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Day.ToString();

                lprnpnjln_tanggalcetaksampai = dtp_lprnpnjln_tglsampai.Value.Year.ToString();
                lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Month.ToString();
                lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Day.ToString();

                lprnpnjln_conn.Open();
                lprnpnjln_query = "SELECT * FROM `nota` WHERE DATE(`tanggalcetak_nota`) >= '" + lprnpnjln_tanggalcetakdari + "' and DATE(tanggalcetak_nota) <= '" + lprnpnjln_tanggalcetaksampai + "'";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(lprnpnjln_query, lprnpnjln_conn);
                MySqlCommand ctknota_cmd = new MySqlCommand(lprnpnjln_query, lprnpnjln_conn);

                mySqlDataAdapter.Fill(lprnpnjln_DS);
                lprnpnjln_DS.Tables[0].Columns.Remove("id_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("jenis_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_midnight_paket");

                lprnpnjln_DS.Tables[0].Columns["id_nota"].ColumnName = "Nomor Nota";
                lprnpnjln_DS.Tables[0].Columns["tanggalcetak_nota"].ColumnName = "Tanggal Cetak Nota";
                lprnpnjln_DS.Tables[0].Columns["nomorruangan_nota"].ColumnName = "Nomor Ruangan";
                lprnpnjln_DS.Tables[0].Columns["jamkerja_nota"].ColumnName = "Jam Kerja";
                lprnpnjln_DS.Tables[0].Columns["tamuhotel_nota"].ColumnName = "Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["potonganhotel_nota"].ColumnName = "Potongan Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["namapaket_nota"].ColumnName = "Nama Paket";
                lprnpnjln_DS.Tables[0].Columns["hargapaket_nota"].ColumnName = "Harga Paket";
                lprnpnjln_DS.Tables[0].Columns["extra_nota"].ColumnName = "Extra";
                lprnpnjln_DS.Tables[0].Columns["nominalextra_nota"].ColumnName = "Nominal Extra";
                lprnpnjln_DS.Tables[0].Columns["kodeterapis_nota"].ColumnName = "Kode Terapis";
                lprnpnjln_DS.Tables[0].Columns["namaterapis_nota"].ColumnName = "Nama Terapis";
                lprnpnjln_DS.Tables[0].Columns["diskon_nota"].ColumnName = "Diskon";
                lprnpnjln_DS.Tables[0].Columns["keterangan_nota"].ColumnName = "Keterangan Diskon";
                lprnpnjln_DS.Tables[0].Columns["totalbayar_nota"].ColumnName = "Subtotal";
                lprnpnjln_DS.Tables[0].Columns["feeterapis_nota"].ColumnName = "Fee Terapis";
                lprnpnjln_DS.Tables[0].Columns["jenisbayar_nota"].ColumnName = "Jenis Bayar";
                lprnpnjln_DS.Tables[0].Columns["status_nota"].ColumnName = "Status";
                lprnpnjln_DS.Tables[0].Columns["grandtotal_nota"].ColumnName = "Grand Total";
                //lprnpnjln_DS.Tables[0].Columns["grandtotal_nota"].SetOrdinal(16);
                //lprnpnjln_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                //lprnpnjln_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                //for (int i = 0; i < lprnpnjln_DS.Tables[0].Rows.Count; i++)
                //{
                //    lprnpnjln_DS.Tables[0].Rows[i]["Tamu Hotel"] = ctknota_tamuhotel;
                //}

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                //lprnpnjln_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                //for (int ii = 0; ii < lprnpnjln_DS.Tables[0].Rows.Count; ii++)
                //{
                //    lprnpnjln_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                //}

                dgv_lprnpnjln_tabellaporan.DataSource = lprnpnjln_DS.Tables[0];

                if (login_jenisuser == "Superadmin")
                {
                    dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_hapusNota);
                    dgv_lprnpnjln_tabellaporan.Columns["Hapus Nota"].DisplayIndex = 19;
                    dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_batalhapusNota);
                    dgv_lprnpnjln_tabellaporan.Columns["Batal Hapus"].DisplayIndex = 20;
                }


                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].Width = 55;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].Width = 115;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].Width = 70;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].Width = 50;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].Width = 45;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].Width = 150;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].Width = 60;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";


                ////MessageBox.Show(dgv_lprnpnjln_tabellaporan.Columns.Count.ToString());
                ////for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Columns.Count; i++)
                ////{
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].ReadOnly = true;
                ////}
                dgv_lprnpnjln_tabellaporan.Columns["Grand Total"].DisplayIndex = 16;
                dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DisplayIndex = 14;

                dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Diskon"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Subtotal"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Grand Total"].DefaultCellStyle.Format = "N0";

                dgv_lprnpnjln_tabellaporan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgv_lprnpnjln_tabellaporan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv_lprnpnjln_tabellaporan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.ReadOnly = true;

                dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_lprnpnjln_tabellaporan.Columns["Keterangan Diskon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                #region(SUM Kolom berisi nominal harga)
                lprnpnjln_potonganhotel = 0;
                lprnpnjln_hargapaket = 0;
                lprnpnjln_extra = 0;
                lprnpnjln_diskon = 0;
                lprnpnjln_totalbayar = 0;
                lprnpnjln_totalbayarcash = 0;
                lprnpnjln_totalbayarcredit = 0;
                lprnpnjln_feeterapis = 0;
                lprnpnjln_grandtotal = 0;
                for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Rows.Count; i++)
                {
                    if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Status"].Value.ToString() != "Terhapus")
                    {
                        lprnpnjln_potonganhotel += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Potongan Tamu Hotel"].Value.ToString());
                        lprnpnjln_hargapaket += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Harga Paket"].Value.ToString());
                        lprnpnjln_extra += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Nominal Extra"].Value.ToString());
                        lprnpnjln_diskon += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Diskon"].Value.ToString());
                        lprnpnjln_totalbayar += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Subtotal"].Value.ToString());
                        lprnpnjln_feeterapis += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Fee Terapis"].Value.ToString());
                        lprnpnjln_grandtotal += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Cash")
                        {
                            lprnpnjln_totalbayarcash += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        }
                        else if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Credit")
                        {
                            lprnpnjln_totalbayarcredit += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        }
                    }
                }
                lbl_lprnpnjln_sumtotalhotel.Text = lprnpnjln_potonganhotel.ToString("#,##0");
                lbl_lprnpnjln_sumtotalgrandtotal.Text = lprnpnjln_grandtotal.ToString("#,##0");
                lbl_lprnpnjln_sumtotalextra.Text = lprnpnjln_extra.ToString("#,##0");
                lbl_lprnpnjln_sumtotaldiskon.Text = lprnpnjln_diskon.ToString("#,##0");
                lbl_lprnpnjln_sumtotalsubtotal.Text = lprnpnjln_totalbayar.ToString("#,##0");
                lbl_lprnpnjln_sumtotalcash.Text = lprnpnjln_totalbayarcash.ToString("#,##0");
                lbl_lprnpnjln_sumtotalcredit.Text = lprnpnjln_totalbayarcredit.ToString("#,##0");
                lbl_lprnpnjln_sumfeeterapis.Text = lprnpnjln_feeterapis.ToString("#,##0");

                //DataRow row = lprnpnjln_DS.Tables[0].NewRow();
                //row[0] = "0";
                //row[1] = "2016-01-08 15:42:16";
                //row[2] = "0";
                //row[3] = "";
                //row[4] = "";
                //row[5] = lbl_lprnpnjln_sumtotalhotel.Text;
                //row[6] = "";
                //row[7] = lprnpnjln_hargapaket.ToString("#,##0"); ;
                //row[8] = "";
                //row[9] = lbl_lprnpnjln_sumtotalextra.Text;
                //row[10] = "0";
                //row[11] = "";
                //row[12] = lbl_lprnpnjln_sumtotaldiskon.Text;
                //row[13] = "";
                //row[14] = lbl_lprnpnjln_sumtotalsubtotal.Text;
                //row[15] = lbl_lprnpnjln_sumfeeterapis.Text;
                //row[16] = "";
                //row[17] = "";
                //lprnpnjln_DS.Tables[0].Rows.Add(row);
                #endregion


            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnpnjln_conn.Close();
            #endregion
        }

        private void dtp_lprnpnjln_tglsampai_ValueChanged(object sender, EventArgs e)
        {
            btn_lprnpnjln_excel.Enabled = true;
            rdo_lprnpnjln_all.Checked = true;
            rdo_lprnpnjln_cash.Enabled = true;
            rdo_lprnpnjln_credit.Enabled = true;
            rdo_lprnpnjln_all.Enabled = true;
            #region(Select)
            dgv_lprnpnjln_tabellaporan.DataSource = null;
            dgv_lprnpnjln_tabellaporan.Rows.Clear();
            dgv_lprnpnjln_tabellaporan.Columns.Clear();
            //dgv_lprnpnjln_tabellaporan.Refresh();
            lprnpnjln_DS.Tables.Clear();
            string lprnpnjln_query;
            MySqlConnection lprnpnjln_conn = new MySqlConnection(all_connStr);
            try
            {
                lprnpnjln_tanggalcetakdari = dtp_lprnpnjln_tgldari.Value.Year.ToString();
                lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Month.ToString();
                lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Day.ToString();

                lprnpnjln_tanggalcetaksampai = dtp_lprnpnjln_tglsampai.Value.Year.ToString();
                lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Month.ToString();
                lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Day.ToString();

                lprnpnjln_conn.Open();
                lprnpnjln_query = "SELECT * FROM `nota` WHERE DATE(`tanggalcetak_nota`) >= '" + lprnpnjln_tanggalcetakdari + "' and DATE(tanggalcetak_nota) <= '" + lprnpnjln_tanggalcetaksampai + "'";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(lprnpnjln_query, lprnpnjln_conn);
                MySqlCommand ctknota_cmd = new MySqlCommand(lprnpnjln_query, lprnpnjln_conn);

                mySqlDataAdapter.Fill(lprnpnjln_DS);
                lprnpnjln_DS.Tables[0].Columns.Remove("id_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("jenis_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_midnight_paket");

                lprnpnjln_DS.Tables[0].Columns["id_nota"].ColumnName = "Nomor Nota";
                lprnpnjln_DS.Tables[0].Columns["tanggalcetak_nota"].ColumnName = "Tanggal Cetak Nota";
                lprnpnjln_DS.Tables[0].Columns["nomorruangan_nota"].ColumnName = "Nomor Ruangan";
                lprnpnjln_DS.Tables[0].Columns["jamkerja_nota"].ColumnName = "Jam Kerja";
                lprnpnjln_DS.Tables[0].Columns["tamuhotel_nota"].ColumnName = "Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["potonganhotel_nota"].ColumnName = "Potongan Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["namapaket_nota"].ColumnName = "Nama Paket";
                lprnpnjln_DS.Tables[0].Columns["hargapaket_nota"].ColumnName = "Harga Paket";
                lprnpnjln_DS.Tables[0].Columns["extra_nota"].ColumnName = "Extra";
                lprnpnjln_DS.Tables[0].Columns["nominalextra_nota"].ColumnName = "Nominal Extra";
                lprnpnjln_DS.Tables[0].Columns["kodeterapis_nota"].ColumnName = "Kode Terapis";
                lprnpnjln_DS.Tables[0].Columns["namaterapis_nota"].ColumnName = "Nama Terapis";
                lprnpnjln_DS.Tables[0].Columns["diskon_nota"].ColumnName = "Diskon";
                lprnpnjln_DS.Tables[0].Columns["keterangan_nota"].ColumnName = "Keterangan Diskon";
                lprnpnjln_DS.Tables[0].Columns["totalbayar_nota"].ColumnName = "Subtotal";
                lprnpnjln_DS.Tables[0].Columns["feeterapis_nota"].ColumnName = "Fee Terapis";
                lprnpnjln_DS.Tables[0].Columns["jenisbayar_nota"].ColumnName = "Jenis Bayar";
                lprnpnjln_DS.Tables[0].Columns["status_nota"].ColumnName = "Status";
                lprnpnjln_DS.Tables[0].Columns["grandtotal_nota"].ColumnName = "Grand Total";
                //lprnpnjln_DS.Tables[0].Columns["grandtotal_nota"].SetOrdinal(16);
                //lprnpnjln_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                //lprnpnjln_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                //for (int i = 0; i < lprnpnjln_DS.Tables[0].Rows.Count; i++)
                //{
                //    lprnpnjln_DS.Tables[0].Rows[i]["Tamu Hotel"] = ctknota_tamuhotel;
                //}

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                //lprnpnjln_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                //for (int ii = 0; ii < lprnpnjln_DS.Tables[0].Rows.Count; ii++)
                //{
                //    lprnpnjln_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                //}

                dgv_lprnpnjln_tabellaporan.DataSource = lprnpnjln_DS.Tables[0];

                if (login_jenisuser == "Superadmin")
                {
                    dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_hapusNota);
                    dgv_lprnpnjln_tabellaporan.Columns["Hapus Nota"].DisplayIndex = 19;
                    dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_batalhapusNota);
                    dgv_lprnpnjln_tabellaporan.Columns["Batal Hapus"].DisplayIndex = 20;
                }


                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].Width = 55;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].Width = 115;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].Width = 70;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].Width = 50;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].Width = 45;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].Width = 150;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].Width = 60;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";


                ////MessageBox.Show(dgv_lprnpnjln_tabellaporan.Columns.Count.ToString());
                ////for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Columns.Count; i++)
                ////{
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].ReadOnly = true;
                ////}
                dgv_lprnpnjln_tabellaporan.Columns["Grand Total"].DisplayIndex = 16;
                dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DisplayIndex = 14;

                dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Diskon"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Subtotal"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Grand Total"].DefaultCellStyle.Format = "N0";

                dgv_lprnpnjln_tabellaporan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgv_lprnpnjln_tabellaporan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv_lprnpnjln_tabellaporan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.ReadOnly = true;

                dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_lprnpnjln_tabellaporan.Columns["Keterangan Diskon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                #region(SUM Kolom berisi nominal harga)
                lprnpnjln_potonganhotel = 0;
                lprnpnjln_hargapaket = 0;
                lprnpnjln_extra = 0;
                lprnpnjln_diskon = 0;
                lprnpnjln_totalbayar = 0;
                lprnpnjln_totalbayarcash = 0;
                lprnpnjln_totalbayarcredit = 0;
                lprnpnjln_feeterapis = 0;
                lprnpnjln_grandtotal = 0;
                for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Rows.Count; i++)
                {
                    if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Status"].Value.ToString() != "Terhapus")
                    {
                        lprnpnjln_potonganhotel += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Potongan Tamu Hotel"].Value.ToString());
                        lprnpnjln_hargapaket += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Harga Paket"].Value.ToString());
                        lprnpnjln_extra += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Nominal Extra"].Value.ToString());
                        lprnpnjln_diskon += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Diskon"].Value.ToString());
                        lprnpnjln_totalbayar += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Subtotal"].Value.ToString());
                        lprnpnjln_feeterapis += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Fee Terapis"].Value.ToString());
                        lprnpnjln_grandtotal += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Cash")
                        {
                            lprnpnjln_totalbayarcash += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        }
                        else if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Credit")
                        {
                            lprnpnjln_totalbayarcredit += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        }
                    }
                }
                lbl_lprnpnjln_sumtotalhotel.Text = lprnpnjln_potonganhotel.ToString("#,##0");
                lbl_lprnpnjln_sumtotalgrandtotal.Text = lprnpnjln_grandtotal.ToString("#,##0");
                lbl_lprnpnjln_sumtotalextra.Text = lprnpnjln_extra.ToString("#,##0");
                lbl_lprnpnjln_sumtotaldiskon.Text = lprnpnjln_diskon.ToString("#,##0");
                lbl_lprnpnjln_sumtotalsubtotal.Text = lprnpnjln_totalbayar.ToString("#,##0");
                lbl_lprnpnjln_sumtotalcash.Text = lprnpnjln_totalbayarcash.ToString("#,##0");
                lbl_lprnpnjln_sumtotalcredit.Text = lprnpnjln_totalbayarcredit.ToString("#,##0");
                lbl_lprnpnjln_sumfeeterapis.Text = lprnpnjln_feeterapis.ToString("#,##0");

                //DataRow row = lprnpnjln_DS.Tables[0].NewRow();
                //row[0] = "0";
                //row[1] = "2016-01-08 15:42:16";
                //row[2] = "0";
                //row[3] = "";
                //row[4] = "";
                //row[5] = lbl_lprnpnjln_sumtotalhotel.Text;
                //row[6] = "";
                //row[7] = lprnpnjln_hargapaket.ToString("#,##0"); ;
                //row[8] = "";
                //row[9] = lbl_lprnpnjln_sumtotalextra.Text;
                //row[10] = "0";
                //row[11] = "";
                //row[12] = lbl_lprnpnjln_sumtotaldiskon.Text;
                //row[13] = "";
                //row[14] = lbl_lprnpnjln_sumtotalsubtotal.Text;
                //row[15] = lbl_lprnpnjln_sumfeeterapis.Text;
                //row[16] = "";
                //row[17] = "";
                //lprnpnjln_DS.Tables[0].Rows.Add(row);
                #endregion


            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnpnjln_conn.Close();
            #endregion
        }

        private void dgv_lprnpnjln_tabellaporan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            int idnota = 0;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                if (dgv_lprnpnjln_tabellaporan.CurrentCell.OwningColumn.Name == "Hapus Nota")
                {
                    for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Rows.Count; i++)
                    {
                        if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Hapus Nota"].Selected)
                        {
                            DialogResult dialogResult = MessageBox.Show("Apakah anda ingin menghapus data nota ini?", "Alert", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                idnota = int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Nomor Nota"].Value.ToString());
                                //UPDATE `nota` SET `status_nota` = 'Terhapus' WHERE `nota`.`id_nota` = 1;
                                #region(Update)
                                MySqlConnection edtpkt_conn = new MySqlConnection(all_connStr);
                                try
                                {
                                    edtpkt_conn.Open();

                                    string edtpkt_query = "UPDATE `nota` SET `status_nota` = 'Terhapus' WHERE `nota`.`id_nota` = '" + idnota + "'";
                                    MySqlCommand cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                                    cmd.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                    MessageBox.Show("Error Occured");
                                }
                                edtpkt_conn.Close();
                                #endregion

                                lprnpnjln_DS.Tables[0].Rows[i]["Status"] = "Terhapus";

                                #region(SUM Kolom berisi nominal harga)
                                lprnpnjln_potonganhotel = 0;
                                lprnpnjln_hargapaket = 0;
                                lprnpnjln_extra = 0;
                                lprnpnjln_diskon = 0;
                                lprnpnjln_totalbayar = 0;
                                lprnpnjln_totalbayarcash = 0;
                                lprnpnjln_totalbayarcredit = 0;
                                lprnpnjln_feeterapis = 0;
                                lprnpnjln_grandtotal = 0;
                                for (int ii = 0; ii < dgv_lprnpnjln_tabellaporan.Rows.Count; ii++)
                                {
                                    if (dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Status"].Value.ToString() != "Terhapus")
                                    {
                                        lprnpnjln_potonganhotel += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Potongan Tamu Hotel"].Value.ToString());
                                        lprnpnjln_hargapaket += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Harga Paket"].Value.ToString());
                                        lprnpnjln_extra += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Nominal Extra"].Value.ToString());
                                        lprnpnjln_diskon += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Diskon"].Value.ToString());
                                        lprnpnjln_totalbayar += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Subtotal"].Value.ToString());
                                        lprnpnjln_feeterapis += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Fee Terapis"].Value.ToString());
                                        lprnpnjln_grandtotal += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Grand Total"].Value.ToString());
                                        if (dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Jenis Bayar"].Value.ToString() == "Cash")
                                        {
                                            lprnpnjln_totalbayarcash += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Grand Total"].Value.ToString());
                                        }
                                        else if (dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Jenis Bayar"].Value.ToString() == "Credit")
                                        {
                                            lprnpnjln_totalbayarcredit += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Grand Total"].Value.ToString());
                                        }
                                    }
                                }
                                lbl_lprnpnjln_sumtotalhotel.Text = lprnpnjln_potonganhotel.ToString("#,##0");
                                lbl_lprnpnjln_sumtotalgrandtotal.Text = lprnpnjln_grandtotal.ToString("#,##0");
                                lbl_lprnpnjln_sumtotalextra.Text = lprnpnjln_extra.ToString("#,##0");
                                lbl_lprnpnjln_sumtotaldiskon.Text = lprnpnjln_diskon.ToString("#,##0");
                                lbl_lprnpnjln_sumtotalsubtotal.Text = lprnpnjln_totalbayar.ToString("#,##0");
                                lbl_lprnpnjln_sumtotalcash.Text = lprnpnjln_totalbayarcash.ToString("#,##0");
                                lbl_lprnpnjln_sumtotalcredit.Text = lprnpnjln_totalbayarcredit.ToString("#,##0");
                                lbl_lprnpnjln_sumfeeterapis.Text = lprnpnjln_feeterapis.ToString("#,##0");

                                //DataRow row = lprnpnjln_DS.Tables[0].NewRow();
                                //row[0] = "0";
                                //row[1] = "2016-01-08 15:42:16";
                                //row[2] = "0";
                                //row[3] = "";
                                //row[4] = "";
                                //row[5] = lbl_lprnpnjln_sumtotalhotel.Text;
                                //row[6] = "";
                                //row[7] = lprnpnjln_hargapaket.ToString("#,##0"); ;
                                //row[8] = "";
                                //row[9] = lbl_lprnpnjln_sumtotalextra.Text;
                                //row[10] = "0";
                                //row[11] = "";
                                //row[12] = lbl_lprnpnjln_sumtotaldiskon.Text;
                                //row[13] = "";
                                //row[14] = lbl_lprnpnjln_sumtotalsubtotal.Text;
                                //row[15] = lbl_lprnpnjln_sumfeeterapis.Text;
                                //row[16] = "";
                                //row[17] = "";
                                //lprnpnjln_DS.Tables[0].Rows.Add(row);
                                #endregion
                            }
                            //MessageBox.Show(i.ToString());

                        }
                    }

                }
                else if (dgv_lprnpnjln_tabellaporan.CurrentCell.OwningColumn.Name == "Batal Hapus")
                {
                    for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Rows.Count; i++)
                    {
                        if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Batal Hapus"].Selected)
                        {
                            DialogResult dialogResult = MessageBox.Show("Apakah anda ingin menghapus data nota ini?", "Alert", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                idnota = int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Nomor Nota"].Value.ToString());
                                //UPDATE `nota` SET `status_nota` = 'Terhapus' WHERE `nota`.`id_nota` = 1;
                                #region(Update)
                                MySqlConnection edtpkt_conn = new MySqlConnection(all_connStr);
                                try
                                {
                                    edtpkt_conn.Open();

                                    string edtpkt_query = "UPDATE `nota` SET `status_nota` = '-' WHERE `nota`.`id_nota` = '" + idnota + "'";
                                    MySqlCommand cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                                    cmd.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                    MessageBox.Show("Error Occured");
                                }
                                edtpkt_conn.Close();
                                #endregion

                                lprnpnjln_DS.Tables[0].Rows[i]["Status"] = "-";

                                #region(SUM Kolom berisi nominal harga)
                                lprnpnjln_potonganhotel = 0;
                                lprnpnjln_hargapaket = 0;
                                lprnpnjln_extra = 0;
                                lprnpnjln_diskon = 0;
                                lprnpnjln_totalbayar = 0;
                                lprnpnjln_totalbayarcash = 0;
                                lprnpnjln_totalbayarcredit = 0;
                                lprnpnjln_feeterapis = 0;
                                lprnpnjln_grandtotal = 0;
                                for (int ii = 0; ii < dgv_lprnpnjln_tabellaporan.Rows.Count; ii++)
                                {
                                    if (dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Status"].Value.ToString() != "Terhapus")
                                    {
                                        lprnpnjln_potonganhotel += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Potongan Tamu Hotel"].Value.ToString());
                                        lprnpnjln_hargapaket += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Harga Paket"].Value.ToString());
                                        lprnpnjln_extra += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Nominal Extra"].Value.ToString());
                                        lprnpnjln_diskon += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Diskon"].Value.ToString());
                                        lprnpnjln_totalbayar += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Subtotal"].Value.ToString());
                                        lprnpnjln_feeterapis += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Fee Terapis"].Value.ToString());
                                        lprnpnjln_grandtotal += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Grand Total"].Value.ToString());
                                        if (dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Jenis Bayar"].Value.ToString() == "Cash")
                                        {
                                            lprnpnjln_totalbayarcash += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Grand Total"].Value.ToString());
                                        }
                                        else if (dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Jenis Bayar"].Value.ToString() == "Credit")
                                        {
                                            lprnpnjln_totalbayarcredit += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[ii].Cells["Grand Total"].Value.ToString());
                                        }
                                    }
                                }
                                lbl_lprnpnjln_sumtotalhotel.Text = lprnpnjln_potonganhotel.ToString("#,##0");
                                lbl_lprnpnjln_sumtotalgrandtotal.Text = lprnpnjln_grandtotal.ToString("#,##0");
                                lbl_lprnpnjln_sumtotalextra.Text = lprnpnjln_extra.ToString("#,##0");
                                lbl_lprnpnjln_sumtotaldiskon.Text = lprnpnjln_diskon.ToString("#,##0");
                                lbl_lprnpnjln_sumtotalsubtotal.Text = lprnpnjln_totalbayar.ToString("#,##0");
                                lbl_lprnpnjln_sumtotalcash.Text = lprnpnjln_totalbayarcash.ToString("#,##0");
                                lbl_lprnpnjln_sumtotalcredit.Text = lprnpnjln_totalbayarcredit.ToString("#,##0");
                                lbl_lprnpnjln_sumfeeterapis.Text = lprnpnjln_feeterapis.ToString("#,##0");

                                //DataRow row = lprnpnjln_DS.Tables[0].NewRow();
                                //row[0] = "0";
                                //row[1] = "2016-01-08 15:42:16";
                                //row[2] = "0";
                                //row[3] = "";
                                //row[4] = "";
                                //row[5] = lbl_lprnpnjln_sumtotalhotel.Text;
                                //row[6] = "";
                                //row[7] = lprnpnjln_hargapaket.ToString("#,##0"); ;
                                //row[8] = "";
                                //row[9] = lbl_lprnpnjln_sumtotalextra.Text;
                                //row[10] = "0";
                                //row[11] = "";
                                //row[12] = lbl_lprnpnjln_sumtotaldiskon.Text;
                                //row[13] = "";
                                //row[14] = lbl_lprnpnjln_sumtotalsubtotal.Text;
                                //row[15] = lbl_lprnpnjln_sumfeeterapis.Text;
                                //row[16] = "";
                                //row[17] = "";
                                //lprnpnjln_DS.Tables[0].Rows.Add(row);
                                #endregion
                            }
                            //MessageBox.Show(i.ToString());

                        }
                    }

                }
            }
        }

        private void rdo_lprnpnjln_all_CheckedChanged(object sender, EventArgs e)
        {
            #region(Select)
            dgv_lprnpnjln_tabellaporan.DataSource = null;
            dgv_lprnpnjln_tabellaporan.Rows.Clear();
            dgv_lprnpnjln_tabellaporan.Columns.Clear();
            //dgv_lprnpnjln_tabellaporan.Refresh();
            lprnpnjln_DS.Tables.Clear();
            string lprnpnjln_query;
            MySqlConnection lprnpnjln_conn = new MySqlConnection(all_connStr);
            try
            {
                lprnpnjln_tanggalcetakdari = dtp_lprnpnjln_tgldari.Value.Year.ToString();
                lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Month.ToString();
                lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Day.ToString();

                lprnpnjln_tanggalcetaksampai = dtp_lprnpnjln_tglsampai.Value.Year.ToString();
                lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Month.ToString();
                lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Day.ToString();

                lprnpnjln_conn.Open();
                lprnpnjln_query = "SELECT * FROM `nota` WHERE DATE(`tanggalcetak_nota`) >= '" + lprnpnjln_tanggalcetakdari + "' and DATE(tanggalcetak_nota) <= '" + lprnpnjln_tanggalcetaksampai + "'";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(lprnpnjln_query, lprnpnjln_conn);
                MySqlCommand ctknota_cmd = new MySqlCommand(lprnpnjln_query, lprnpnjln_conn);

                mySqlDataAdapter.Fill(lprnpnjln_DS);
                lprnpnjln_DS.Tables[0].Columns.Remove("id_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("jenis_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_midnight_paket");

                lprnpnjln_DS.Tables[0].Columns["id_nota"].ColumnName = "Nomor Nota";
                lprnpnjln_DS.Tables[0].Columns["tanggalcetak_nota"].ColumnName = "Tanggal Cetak Nota";
                lprnpnjln_DS.Tables[0].Columns["nomorruangan_nota"].ColumnName = "Nomor Ruangan";
                lprnpnjln_DS.Tables[0].Columns["jamkerja_nota"].ColumnName = "Jam Kerja";
                lprnpnjln_DS.Tables[0].Columns["tamuhotel_nota"].ColumnName = "Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["potonganhotel_nota"].ColumnName = "Potongan Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["namapaket_nota"].ColumnName = "Nama Paket";
                lprnpnjln_DS.Tables[0].Columns["hargapaket_nota"].ColumnName = "Harga Paket";
                lprnpnjln_DS.Tables[0].Columns["extra_nota"].ColumnName = "Extra";
                lprnpnjln_DS.Tables[0].Columns["nominalextra_nota"].ColumnName = "Nominal Extra";
                lprnpnjln_DS.Tables[0].Columns["kodeterapis_nota"].ColumnName = "Kode Terapis";
                lprnpnjln_DS.Tables[0].Columns["namaterapis_nota"].ColumnName = "Nama Terapis";
                lprnpnjln_DS.Tables[0].Columns["diskon_nota"].ColumnName = "Diskon";
                lprnpnjln_DS.Tables[0].Columns["keterangan_nota"].ColumnName = "Keterangan Diskon";
                lprnpnjln_DS.Tables[0].Columns["totalbayar_nota"].ColumnName = "Subtotal";
                lprnpnjln_DS.Tables[0].Columns["feeterapis_nota"].ColumnName = "Fee Terapis";
                lprnpnjln_DS.Tables[0].Columns["jenisbayar_nota"].ColumnName = "Jenis Bayar";
                lprnpnjln_DS.Tables[0].Columns["status_nota"].ColumnName = "Status";
                lprnpnjln_DS.Tables[0].Columns["grandtotal_nota"].ColumnName = "Grand Total";
                //lprnpnjln_DS.Tables[0].Columns["grandtotal_nota"].SetOrdinal(16);
                //lprnpnjln_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                //lprnpnjln_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                //for (int i = 0; i < lprnpnjln_DS.Tables[0].Rows.Count; i++)
                //{
                //    lprnpnjln_DS.Tables[0].Rows[i]["Tamu Hotel"] = ctknota_tamuhotel;
                //}

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                //lprnpnjln_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                //for (int ii = 0; ii < lprnpnjln_DS.Tables[0].Rows.Count; ii++)
                //{
                //    lprnpnjln_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                //}

                dgv_lprnpnjln_tabellaporan.DataSource = lprnpnjln_DS.Tables[0];

                if (login_jenisuser == "Superadmin")
                {
                    dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_hapusNota);
                    dgv_lprnpnjln_tabellaporan.Columns["Hapus Nota"].DisplayIndex = 19;
                    dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_batalhapusNota);
                    dgv_lprnpnjln_tabellaporan.Columns["Batal Hapus"].DisplayIndex = 20;
                }


                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].Width = 55;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].Width = 115;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].Width = 70;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].Width = 50;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].Width = 45;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].Width = 150;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].Width = 60;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";


                ////MessageBox.Show(dgv_lprnpnjln_tabellaporan.Columns.Count.ToString());
                ////for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Columns.Count; i++)
                ////{
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].ReadOnly = true;
                ////}
                dgv_lprnpnjln_tabellaporan.Columns["Grand Total"].DisplayIndex = 16;
                dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DisplayIndex = 14;

                dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Diskon"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Subtotal"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Grand Total"].DefaultCellStyle.Format = "N0";

                dgv_lprnpnjln_tabellaporan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgv_lprnpnjln_tabellaporan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv_lprnpnjln_tabellaporan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.ReadOnly = true;

                dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_lprnpnjln_tabellaporan.Columns["Keterangan Diskon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                #region(SUM Kolom berisi nominal harga)
                lprnpnjln_potonganhotel = 0;
                lprnpnjln_hargapaket = 0;
                lprnpnjln_extra = 0;
                lprnpnjln_diskon = 0;
                lprnpnjln_totalbayar = 0;
                lprnpnjln_totalbayarcash = 0;
                lprnpnjln_totalbayarcredit = 0;
                lprnpnjln_feeterapis = 0;
                lprnpnjln_grandtotal = 0;
                for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Rows.Count; i++)
                {
                    if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Status"].Value.ToString() != "Terhapus")
                    {
                        lprnpnjln_potonganhotel += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Potongan Tamu Hotel"].Value.ToString());
                        lprnpnjln_hargapaket += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Harga Paket"].Value.ToString());
                        lprnpnjln_extra += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Nominal Extra"].Value.ToString());
                        lprnpnjln_diskon += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Diskon"].Value.ToString());
                        lprnpnjln_totalbayar += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Subtotal"].Value.ToString());
                        lprnpnjln_feeterapis += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Fee Terapis"].Value.ToString());
                        lprnpnjln_grandtotal += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Cash")
                        {
                            lprnpnjln_totalbayarcash += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        }
                        else if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Credit")
                        {
                            lprnpnjln_totalbayarcredit += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        }
                    }
                }
                lbl_lprnpnjln_sumtotalhotel.Text = lprnpnjln_potonganhotel.ToString("#,##0");
                lbl_lprnpnjln_sumtotalgrandtotal.Text = lprnpnjln_grandtotal.ToString("#,##0");
                lbl_lprnpnjln_sumtotalextra.Text = lprnpnjln_extra.ToString("#,##0");
                lbl_lprnpnjln_sumtotaldiskon.Text = lprnpnjln_diskon.ToString("#,##0");
                lbl_lprnpnjln_sumtotalsubtotal.Text = lprnpnjln_totalbayar.ToString("#,##0");
                lbl_lprnpnjln_sumtotalcash.Text = lprnpnjln_totalbayarcash.ToString("#,##0");
                lbl_lprnpnjln_sumtotalcredit.Text = lprnpnjln_totalbayarcredit.ToString("#,##0");
                lbl_lprnpnjln_sumfeeterapis.Text = lprnpnjln_feeterapis.ToString("#,##0");

                //DataRow row = lprnpnjln_DS.Tables[0].NewRow();
                //row[0] = "0";
                //row[1] = "2016-01-08 15:42:16";
                //row[2] = "0";
                //row[3] = "";
                //row[4] = "";
                //row[5] = lbl_lprnpnjln_sumtotalhotel.Text;
                //row[6] = "";
                //row[7] = lprnpnjln_hargapaket.ToString("#,##0"); ;
                //row[8] = "";
                //row[9] = lbl_lprnpnjln_sumtotalextra.Text;
                //row[10] = "0";
                //row[11] = "";
                //row[12] = lbl_lprnpnjln_sumtotaldiskon.Text;
                //row[13] = "";
                //row[14] = lbl_lprnpnjln_sumtotalsubtotal.Text;
                //row[15] = lbl_lprnpnjln_sumfeeterapis.Text;
                //row[16] = "";
                //row[17] = "";
                //lprnpnjln_DS.Tables[0].Rows.Add(row);
                #endregion


            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnpnjln_conn.Close();
            #endregion
        }

        private void rdo_lprnpnjln_cash_CheckedChanged(object sender, EventArgs e)
        {
            #region(Select)
            dgv_lprnpnjln_tabellaporan.DataSource = null;
            dgv_lprnpnjln_tabellaporan.Rows.Clear();
            dgv_lprnpnjln_tabellaporan.Columns.Clear();
            //dgv_lprnpnjln_tabellaporan.Refresh();
            lprnpnjln_DS.Tables.Clear();
            string lprnpnjln_query;
            MySqlConnection lprnpnjln_conn = new MySqlConnection(all_connStr);
            try
            {
                lprnpnjln_tanggalcetakdari = dtp_lprnpnjln_tgldari.Value.Year.ToString();
                lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Month.ToString();
                lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Day.ToString();

                lprnpnjln_tanggalcetaksampai = dtp_lprnpnjln_tglsampai.Value.Year.ToString();
                lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Month.ToString();
                lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Day.ToString();

                lprnpnjln_conn.Open();
                lprnpnjln_query = "SELECT * FROM `nota` WHERE DATE(`tanggalcetak_nota`) >= '" + lprnpnjln_tanggalcetakdari + "' and DATE(tanggalcetak_nota) <= '" + lprnpnjln_tanggalcetaksampai + "' and `jenisbayar_nota` = 'Cash'";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(lprnpnjln_query, lprnpnjln_conn);
                MySqlCommand ctknota_cmd = new MySqlCommand(lprnpnjln_query, lprnpnjln_conn);

                mySqlDataAdapter.Fill(lprnpnjln_DS);
                lprnpnjln_DS.Tables[0].Columns.Remove("id_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("jenis_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_midnight_paket");

                lprnpnjln_DS.Tables[0].Columns["id_nota"].ColumnName = "Nomor Nota";
                lprnpnjln_DS.Tables[0].Columns["tanggalcetak_nota"].ColumnName = "Tanggal Cetak Nota";
                lprnpnjln_DS.Tables[0].Columns["nomorruangan_nota"].ColumnName = "Nomor Ruangan";
                lprnpnjln_DS.Tables[0].Columns["jamkerja_nota"].ColumnName = "Jam Kerja";
                lprnpnjln_DS.Tables[0].Columns["tamuhotel_nota"].ColumnName = "Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["potonganhotel_nota"].ColumnName = "Potongan Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["namapaket_nota"].ColumnName = "Nama Paket";
                lprnpnjln_DS.Tables[0].Columns["hargapaket_nota"].ColumnName = "Harga Paket";
                lprnpnjln_DS.Tables[0].Columns["extra_nota"].ColumnName = "Extra";
                lprnpnjln_DS.Tables[0].Columns["nominalextra_nota"].ColumnName = "Nominal Extra";
                lprnpnjln_DS.Tables[0].Columns["kodeterapis_nota"].ColumnName = "Kode Terapis";
                lprnpnjln_DS.Tables[0].Columns["namaterapis_nota"].ColumnName = "Nama Terapis";
                lprnpnjln_DS.Tables[0].Columns["diskon_nota"].ColumnName = "Diskon";
                lprnpnjln_DS.Tables[0].Columns["keterangan_nota"].ColumnName = "Keterangan Diskon";
                lprnpnjln_DS.Tables[0].Columns["totalbayar_nota"].ColumnName = "Subtotal";
                lprnpnjln_DS.Tables[0].Columns["feeterapis_nota"].ColumnName = "Fee Terapis";
                lprnpnjln_DS.Tables[0].Columns["jenisbayar_nota"].ColumnName = "Jenis Bayar";
                lprnpnjln_DS.Tables[0].Columns["status_nota"].ColumnName = "Status";
                lprnpnjln_DS.Tables[0].Columns["grandtotal_nota"].ColumnName = "Grand Total";
                //lprnpnjln_DS.Tables[0].Columns["grandtotal_nota"].SetOrdinal(16);
                //lprnpnjln_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                //lprnpnjln_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                //for (int i = 0; i < lprnpnjln_DS.Tables[0].Rows.Count; i++)
                //{
                //    lprnpnjln_DS.Tables[0].Rows[i]["Tamu Hotel"] = ctknota_tamuhotel;
                //}

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                //lprnpnjln_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                //for (int ii = 0; ii < lprnpnjln_DS.Tables[0].Rows.Count; ii++)
                //{
                //    lprnpnjln_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                //}

                dgv_lprnpnjln_tabellaporan.DataSource = lprnpnjln_DS.Tables[0];

                if (login_jenisuser == "Superadmin")
                {
                    dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_hapusNota);
                    dgv_lprnpnjln_tabellaporan.Columns["Hapus Nota"].DisplayIndex = 19;
                    dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_batalhapusNota);
                    dgv_lprnpnjln_tabellaporan.Columns["Batal Hapus"].DisplayIndex = 20;
                }


                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].Width = 55;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].Width = 115;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].Width = 70;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].Width = 50;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].Width = 45;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].Width = 150;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].Width = 60;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";


                ////MessageBox.Show(dgv_lprnpnjln_tabellaporan.Columns.Count.ToString());
                ////for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Columns.Count; i++)
                ////{
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].ReadOnly = true;
                ////}
                dgv_lprnpnjln_tabellaporan.Columns["Grand Total"].DisplayIndex = 16;
                dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DisplayIndex = 14;

                dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Diskon"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Subtotal"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Grand Total"].DefaultCellStyle.Format = "N0";

                dgv_lprnpnjln_tabellaporan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgv_lprnpnjln_tabellaporan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv_lprnpnjln_tabellaporan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.ReadOnly = true;

                dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_lprnpnjln_tabellaporan.Columns["Keterangan Diskon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                #region(SUM Kolom berisi nominal harga)
                lprnpnjln_potonganhotel = 0;
                lprnpnjln_hargapaket = 0;
                lprnpnjln_extra = 0;
                lprnpnjln_diskon = 0;
                lprnpnjln_totalbayar = 0;
                lprnpnjln_totalbayarcash = 0;
                lprnpnjln_totalbayarcredit = 0;
                lprnpnjln_feeterapis = 0;
                lprnpnjln_grandtotal = 0;
                for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Rows.Count; i++)
                {
                    if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Status"].Value.ToString() != "Terhapus")
                    {
                        lprnpnjln_potonganhotel += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Potongan Tamu Hotel"].Value.ToString());
                        lprnpnjln_hargapaket += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Harga Paket"].Value.ToString());
                        lprnpnjln_extra += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Nominal Extra"].Value.ToString());
                        lprnpnjln_diskon += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Diskon"].Value.ToString());
                        lprnpnjln_totalbayar += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Subtotal"].Value.ToString());
                        lprnpnjln_feeterapis += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Fee Terapis"].Value.ToString());
                        lprnpnjln_grandtotal += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Cash")
                        {
                            lprnpnjln_totalbayarcash += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        }
                        else if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Credit")
                        {
                            lprnpnjln_totalbayarcredit += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        }
                    }
                }
                lbl_lprnpnjln_sumtotalhotel.Text = lprnpnjln_potonganhotel.ToString("#,##0");
                lbl_lprnpnjln_sumtotalgrandtotal.Text = lprnpnjln_grandtotal.ToString("#,##0");
                lbl_lprnpnjln_sumtotalextra.Text = lprnpnjln_extra.ToString("#,##0");
                lbl_lprnpnjln_sumtotaldiskon.Text = lprnpnjln_diskon.ToString("#,##0");
                lbl_lprnpnjln_sumtotalsubtotal.Text = lprnpnjln_totalbayar.ToString("#,##0");
                lbl_lprnpnjln_sumtotalcash.Text = lprnpnjln_totalbayarcash.ToString("#,##0");
                lbl_lprnpnjln_sumtotalcredit.Text = lprnpnjln_totalbayarcredit.ToString("#,##0");
                lbl_lprnpnjln_sumfeeterapis.Text = lprnpnjln_feeterapis.ToString("#,##0");

                //DataRow row = lprnpnjln_DS.Tables[0].NewRow();
                //row[0] = "0";
                //row[1] = "2016-01-08 15:42:16";
                //row[2] = "0";
                //row[3] = "";
                //row[4] = "";
                //row[5] = lbl_lprnpnjln_sumtotalhotel.Text;
                //row[6] = "";
                //row[7] = lprnpnjln_hargapaket.ToString("#,##0"); ;
                //row[8] = "";
                //row[9] = lbl_lprnpnjln_sumtotalextra.Text;
                //row[10] = "0";
                //row[11] = "";
                //row[12] = lbl_lprnpnjln_sumtotaldiskon.Text;
                //row[13] = "";
                //row[14] = lbl_lprnpnjln_sumtotalsubtotal.Text;
                //row[15] = lbl_lprnpnjln_sumfeeterapis.Text;
                //row[16] = "";
                //row[17] = "";
                //lprnpnjln_DS.Tables[0].Rows.Add(row);
                #endregion


            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnpnjln_conn.Close();
            #endregion
        }

        private void rdo_lprnpnjln_credit_CheckedChanged(object sender, EventArgs e)
        {
            #region(Select)
            dgv_lprnpnjln_tabellaporan.DataSource = null;
            dgv_lprnpnjln_tabellaporan.Rows.Clear();
            dgv_lprnpnjln_tabellaporan.Columns.Clear();
            //dgv_lprnpnjln_tabellaporan.Refresh();
            lprnpnjln_DS.Tables.Clear();
            string lprnpnjln_query;
            MySqlConnection lprnpnjln_conn = new MySqlConnection(all_connStr);
            try
            {
                lprnpnjln_tanggalcetakdari = dtp_lprnpnjln_tgldari.Value.Year.ToString();
                lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Month.ToString();
                lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Day.ToString();

                lprnpnjln_tanggalcetaksampai = dtp_lprnpnjln_tglsampai.Value.Year.ToString();
                lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Month.ToString();
                lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Day.ToString();

                lprnpnjln_conn.Open();
                lprnpnjln_query = "SELECT * FROM `nota` WHERE DATE(`tanggalcetak_nota`) >= '" + lprnpnjln_tanggalcetakdari + "' and DATE(tanggalcetak_nota) <= '" + lprnpnjln_tanggalcetaksampai + "' and `jenisbayar_nota` = 'Credit'";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(lprnpnjln_query, lprnpnjln_conn);
                MySqlCommand ctknota_cmd = new MySqlCommand(lprnpnjln_query, lprnpnjln_conn);

                mySqlDataAdapter.Fill(lprnpnjln_DS);
                lprnpnjln_DS.Tables[0].Columns.Remove("id_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("jenis_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_midnight_paket");

                lprnpnjln_DS.Tables[0].Columns["id_nota"].ColumnName = "Nomor Nota";
                lprnpnjln_DS.Tables[0].Columns["tanggalcetak_nota"].ColumnName = "Tanggal Cetak Nota";
                lprnpnjln_DS.Tables[0].Columns["nomorruangan_nota"].ColumnName = "Nomor Ruangan";
                lprnpnjln_DS.Tables[0].Columns["jamkerja_nota"].ColumnName = "Jam Kerja";
                lprnpnjln_DS.Tables[0].Columns["tamuhotel_nota"].ColumnName = "Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["potonganhotel_nota"].ColumnName = "Potongan Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["namapaket_nota"].ColumnName = "Nama Paket";
                lprnpnjln_DS.Tables[0].Columns["hargapaket_nota"].ColumnName = "Harga Paket";
                lprnpnjln_DS.Tables[0].Columns["extra_nota"].ColumnName = "Extra";
                lprnpnjln_DS.Tables[0].Columns["nominalextra_nota"].ColumnName = "Nominal Extra";
                lprnpnjln_DS.Tables[0].Columns["kodeterapis_nota"].ColumnName = "Kode Terapis";
                lprnpnjln_DS.Tables[0].Columns["namaterapis_nota"].ColumnName = "Nama Terapis";
                lprnpnjln_DS.Tables[0].Columns["diskon_nota"].ColumnName = "Diskon";
                lprnpnjln_DS.Tables[0].Columns["keterangan_nota"].ColumnName = "Keterangan Diskon";
                lprnpnjln_DS.Tables[0].Columns["totalbayar_nota"].ColumnName = "Subtotal";
                lprnpnjln_DS.Tables[0].Columns["feeterapis_nota"].ColumnName = "Fee Terapis";
                lprnpnjln_DS.Tables[0].Columns["jenisbayar_nota"].ColumnName = "Jenis Bayar";
                lprnpnjln_DS.Tables[0].Columns["status_nota"].ColumnName = "Status";
                lprnpnjln_DS.Tables[0].Columns["grandtotal_nota"].ColumnName = "Grand Total";
                //lprnpnjln_DS.Tables[0].Columns["grandtotal_nota"].SetOrdinal(16);
                //lprnpnjln_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                //lprnpnjln_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                //for (int i = 0; i < lprnpnjln_DS.Tables[0].Rows.Count; i++)
                //{
                //    lprnpnjln_DS.Tables[0].Rows[i]["Tamu Hotel"] = ctknota_tamuhotel;
                //}

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                //lprnpnjln_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                //for (int ii = 0; ii < lprnpnjln_DS.Tables[0].Rows.Count; ii++)
                //{
                //    lprnpnjln_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                //}

                dgv_lprnpnjln_tabellaporan.DataSource = lprnpnjln_DS.Tables[0];

                if (login_jenisuser == "Superadmin")
                {
                    dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_hapusNota);
                    dgv_lprnpnjln_tabellaporan.Columns["Hapus Nota"].DisplayIndex = 19;
                    dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_batalhapusNota);
                    dgv_lprnpnjln_tabellaporan.Columns["Batal Hapus"].DisplayIndex = 20;
                }


                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].Width = 55;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].Width = 115;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].Width = 70;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].Width = 50;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].Width = 45;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].Width = 150;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].Width = 60;
                ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].ReadOnly = true;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].Width = 85;
                ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";


                ////MessageBox.Show(dgv_lprnpnjln_tabellaporan.Columns.Count.ToString());
                ////for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Columns.Count; i++)
                ////{
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                ////    dgv_lprnpnjln_tabellaporan.Columns[i].ReadOnly = true;
                ////}
                dgv_lprnpnjln_tabellaporan.Columns["Grand Total"].DisplayIndex = 16;
                dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DisplayIndex = 14;

                dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Diskon"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Subtotal"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Grand Total"].DefaultCellStyle.Format = "N0";

                dgv_lprnpnjln_tabellaporan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgv_lprnpnjln_tabellaporan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv_lprnpnjln_tabellaporan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.ReadOnly = true;

                dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_lprnpnjln_tabellaporan.Columns["Keterangan Diskon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                #region(SUM Kolom berisi nominal harga)
                lprnpnjln_potonganhotel = 0;
                lprnpnjln_hargapaket = 0;
                lprnpnjln_extra = 0;
                lprnpnjln_diskon = 0;
                lprnpnjln_totalbayar = 0;
                lprnpnjln_totalbayarcash = 0;
                lprnpnjln_totalbayarcredit = 0;
                lprnpnjln_feeterapis = 0;
                lprnpnjln_grandtotal = 0;
                for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Rows.Count; i++)
                {
                    if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Status"].Value.ToString() != "Terhapus")
                    {
                        lprnpnjln_potonganhotel += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Potongan Tamu Hotel"].Value.ToString());
                        lprnpnjln_hargapaket += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Harga Paket"].Value.ToString());
                        lprnpnjln_extra += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Nominal Extra"].Value.ToString());
                        lprnpnjln_diskon += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Diskon"].Value.ToString());
                        lprnpnjln_totalbayar += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Subtotal"].Value.ToString());
                        lprnpnjln_feeterapis += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Fee Terapis"].Value.ToString());
                        lprnpnjln_grandtotal += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Cash")
                        {
                            lprnpnjln_totalbayarcash += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        }
                        else if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Credit")
                        {
                            lprnpnjln_totalbayarcredit += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                        }
                    }
                }
                lbl_lprnpnjln_sumtotalhotel.Text = lprnpnjln_potonganhotel.ToString("#,##0");
                lbl_lprnpnjln_sumtotalgrandtotal.Text = lprnpnjln_grandtotal.ToString("#,##0");
                lbl_lprnpnjln_sumtotalextra.Text = lprnpnjln_extra.ToString("#,##0");
                lbl_lprnpnjln_sumtotaldiskon.Text = lprnpnjln_diskon.ToString("#,##0");
                lbl_lprnpnjln_sumtotalsubtotal.Text = lprnpnjln_totalbayar.ToString("#,##0");
                lbl_lprnpnjln_sumtotalcash.Text = lprnpnjln_totalbayarcash.ToString("#,##0");
                lbl_lprnpnjln_sumtotalcredit.Text = lprnpnjln_totalbayarcredit.ToString("#,##0");
                lbl_lprnpnjln_sumfeeterapis.Text = lprnpnjln_feeterapis.ToString("#,##0");

                //DataRow row = lprnpnjln_DS.Tables[0].NewRow();
                //row[0] = "0";
                //row[1] = "2016-01-08 15:42:16";
                //row[2] = "0";
                //row[3] = "";
                //row[4] = "";
                //row[5] = lbl_lprnpnjln_sumtotalhotel.Text;
                //row[6] = "";
                //row[7] = lprnpnjln_hargapaket.ToString("#,##0"); ;
                //row[8] = "";
                //row[9] = lbl_lprnpnjln_sumtotalextra.Text;
                //row[10] = "0";
                //row[11] = "";
                //row[12] = lbl_lprnpnjln_sumtotaldiskon.Text;
                //row[13] = "";
                //row[14] = lbl_lprnpnjln_sumtotalsubtotal.Text;
                //row[15] = lbl_lprnpnjln_sumfeeterapis.Text;
                //row[16] = "";
                //row[17] = "";
                //lprnpnjln_DS.Tables[0].Rows.Add(row);
                #endregion


            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnpnjln_conn.Close();
            #endregion
        }

        private void btn_lprnpnjln_excel_Click(object sender, EventArgs e)
        {
            string folderPath = "C:\\Excel\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                //wb.Cell().DataType = XLCellValues.Number;
                wb.Worksheets.Add(lprnpnjln_DS.Tables[0], "Laporan Penjualan");
                wb.SaveAs(folderPath + "Laporan Penjualan (" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour + ";" + DateTime.Now.Minute.ToString() + ";" + DateTime.Now.Second.ToString() + ").xlsx");
                MessageBox.Show("File Excel telah disimpan");
            }

            DialogResult dialogResult = MessageBox.Show("Apakah anda ingin menghapus Data Laporan Penjualan?", "Alert", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                #region(Update)
                MySqlConnection lprnpnjln_conn = new MySqlConnection(all_connStr);
                try
                {
                    lprnpnjln_conn.Open();

                    string lprnpnjln_query = "DELETE FROM `nota` WHERE DATE(`tanggalcetak_nota`) >= '" + lprnpnjln_tanggalcetakdari + "' and DATE(tanggalcetak_nota) <= '" + lprnpnjln_tanggalcetaksampai + "'";
                    MySqlCommand lprnpnjln_cmd = new MySqlCommand(lprnpnjln_query, lprnpnjln_conn);
                    lprnpnjln_cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Error Occured");
                }
                lprnpnjln_conn.Close();
                #endregion

                #region(Select)
                dgv_lprnpnjln_tabellaporan.DataSource = null;
                dgv_lprnpnjln_tabellaporan.Rows.Clear();
                dgv_lprnpnjln_tabellaporan.Columns.Clear();
                //dgv_lprnpnjln_tabellaporan.Refresh();
                lprnpnjln_DS.Tables.Clear();
                string lprnpnjln_query2;
                MySqlConnection lprnpnjln_conn2 = new MySqlConnection(all_connStr);
                try
                {
                    lprnpnjln_tanggalcetakdari = dtp_lprnpnjln_tgldari.Value.Year.ToString();
                    lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Month.ToString();
                    lprnpnjln_tanggalcetakdari += "-" + dtp_lprnpnjln_tgldari.Value.Day.ToString();

                    lprnpnjln_tanggalcetaksampai = dtp_lprnpnjln_tglsampai.Value.Year.ToString();
                    lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Month.ToString();
                    lprnpnjln_tanggalcetaksampai += "-" + dtp_lprnpnjln_tglsampai.Value.Day.ToString();

                    lprnpnjln_conn2.Open();
                    lprnpnjln_query2 = "SELECT * FROM `nota` WHERE DATE(`tanggalcetak_nota`) >= '" + lprnpnjln_tanggalcetakdari + "' and DATE(tanggalcetak_nota) <= '" + lprnpnjln_tanggalcetaksampai + "'";
                    MySqlDataAdapter mySqlDataAdapter2 = new MySqlDataAdapter(lprnpnjln_query2, lprnpnjln_conn2);
                    MySqlCommand ctknota_cmd2 = new MySqlCommand(lprnpnjln_query2, lprnpnjln_conn2);

                    mySqlDataAdapter2.Fill(lprnpnjln_DS);
                    lprnpnjln_DS.Tables[0].Columns.Remove("id_paket");
                    //lprnpnjln_DS.Tables[0].Columns.Remove("jenis_paket");
                    //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                    //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_midnight_paket");

                    lprnpnjln_DS.Tables[0].Columns["id_nota"].ColumnName = "Nomor Nota";
                    lprnpnjln_DS.Tables[0].Columns["tanggalcetak_nota"].ColumnName = "Tanggal Cetak Nota";
                    lprnpnjln_DS.Tables[0].Columns["nomorruangan_nota"].ColumnName = "Nomor Ruangan";
                    lprnpnjln_DS.Tables[0].Columns["jamkerja_nota"].ColumnName = "Jam Kerja";
                    lprnpnjln_DS.Tables[0].Columns["tamuhotel_nota"].ColumnName = "Tamu Hotel";
                    lprnpnjln_DS.Tables[0].Columns["potonganhotel_nota"].ColumnName = "Potongan Tamu Hotel";
                    lprnpnjln_DS.Tables[0].Columns["namapaket_nota"].ColumnName = "Nama Paket";
                    lprnpnjln_DS.Tables[0].Columns["hargapaket_nota"].ColumnName = "Harga Paket";
                    lprnpnjln_DS.Tables[0].Columns["extra_nota"].ColumnName = "Extra";
                    lprnpnjln_DS.Tables[0].Columns["nominalextra_nota"].ColumnName = "Nominal Extra";
                    lprnpnjln_DS.Tables[0].Columns["kodeterapis_nota"].ColumnName = "Kode Terapis";
                    lprnpnjln_DS.Tables[0].Columns["namaterapis_nota"].ColumnName = "Nama Terapis";
                    lprnpnjln_DS.Tables[0].Columns["diskon_nota"].ColumnName = "Diskon";
                    lprnpnjln_DS.Tables[0].Columns["keterangan_nota"].ColumnName = "Keterangan Diskon";
                    lprnpnjln_DS.Tables[0].Columns["totalbayar_nota"].ColumnName = "Subtotal";
                    lprnpnjln_DS.Tables[0].Columns["feeterapis_nota"].ColumnName = "Fee Terapis";
                    lprnpnjln_DS.Tables[0].Columns["jenisbayar_nota"].ColumnName = "Jenis Bayar";
                    lprnpnjln_DS.Tables[0].Columns["status_nota"].ColumnName = "Status";
                    //lprnpnjln_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                    //lprnpnjln_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                    //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                    //for (int i = 0; i < lprnpnjln_DS.Tables[0].Rows.Count; i++)
                    //{
                    //    lprnpnjln_DS.Tables[0].Rows[i]["Tamu Hotel"] = ctknota_tamuhotel;
                    //}

                    //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                    //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                    //lprnpnjln_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                    //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                    //for (int ii = 0; ii < lprnpnjln_DS.Tables[0].Rows.Count; ii++)
                    //{
                    //    lprnpnjln_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                    //}

                    dgv_lprnpnjln_tabellaporan.DataSource = lprnpnjln_DS.Tables[0];
                    if (login_jenisuser == "Superadmin")
                    {
                        dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_hapusNota);
                        dgv_lprnpnjln_tabellaporan.Columns["Hapus Nota"].DisplayIndex = 18;
                        dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_batalhapusNota);
                        dgv_lprnpnjln_tabellaporan.Columns["Batal Hapus"].DisplayIndex = 19;
                    }


                    ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].ReadOnly = true;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].Width = 55;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].ReadOnly = true;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].Width = 115;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].ReadOnly = true;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].Width = 70;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].ReadOnly = true;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].Width = 50;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].ReadOnly = true;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].Width = 45;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].ReadOnly = true;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].Width = 85;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                    ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].ReadOnly = true;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].Width = 150;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].ReadOnly = true;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].Width = 60;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                    ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].ReadOnly = true;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Extra"].Width = 85;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].ReadOnly = true;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].Width = 85;
                    ////dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";


                    ////MessageBox.Show(dgv_lprnpnjln_tabellaporan.Columns.Count.ToString());
                    ////for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Columns.Count; i++)
                    ////{
                    ////    dgv_lprnpnjln_tabellaporan.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    ////    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    ////    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ////    dgv_lprnpnjln_tabellaporan.Columns[i].ReadOnly = true;
                    ////}

                    dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                    dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                    dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";
                    dgv_lprnpnjln_tabellaporan.Columns["Diskon"].DefaultCellStyle.Format = "N0";
                    dgv_lprnpnjln_tabellaporan.Columns["Subtotal"].DefaultCellStyle.Format = "N0";
                    dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DefaultCellStyle.Format = "N0";

                    dgv_lprnpnjln_tabellaporan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dgv_lprnpnjln_tabellaporan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dgv_lprnpnjln_tabellaporan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv_lprnpnjln_tabellaporan.ReadOnly = true;

                    dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv_lprnpnjln_tabellaporan.Columns["Keterangan Diskon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                    #region(SUM Kolom berisi nominal harga)
                    lprnpnjln_potonganhotel = 0;
                    lprnpnjln_hargapaket = 0;
                    lprnpnjln_extra = 0;
                    lprnpnjln_diskon = 0;
                    lprnpnjln_totalbayar = 0;
                    lprnpnjln_totalbayarcash = 0;
                    lprnpnjln_totalbayarcredit = 0;
                    lprnpnjln_feeterapis = 0;
                    for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Rows.Count; i++)
                    {
                        if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Status"].Value.ToString() != "Terhapus")
                        {
                            lprnpnjln_potonganhotel += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Potongan Tamu Hotel"].Value.ToString());
                            lprnpnjln_hargapaket += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Harga Paket"].Value.ToString());
                            lprnpnjln_extra += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Nominal Extra"].Value.ToString());
                            lprnpnjln_diskon += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Diskon"].Value.ToString());
                            lprnpnjln_totalbayar += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Subtotal"].Value.ToString());
                            lprnpnjln_feeterapis += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Fee Terapis"].Value.ToString());
                            if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Cash")
                            {
                                lprnpnjln_totalbayarcash += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Subtotal"].Value.ToString());
                            }
                            else if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Credit")
                            {
                                lprnpnjln_totalbayarcredit += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Subtotal"].Value.ToString());
                            }
                        }
                    }
                    lbl_lprnpnjln_sumtotalhotel.Text = lprnpnjln_potonganhotel.ToString("#,##0");
                    lbl_lprnpnjln_sumtotalgrandtotal.Text = lprnpnjln_hargapaket.ToString("#,##0");
                    lbl_lprnpnjln_sumtotalextra.Text = lprnpnjln_extra.ToString("#,##0");
                    lbl_lprnpnjln_sumtotaldiskon.Text = lprnpnjln_diskon.ToString("#,##0");
                    lbl_lprnpnjln_sumtotalsubtotal.Text = lprnpnjln_totalbayar.ToString("#,##0");
                    lbl_lprnpnjln_sumtotalcash.Text = lprnpnjln_totalbayarcash.ToString("#,##0");
                    lbl_lprnpnjln_sumtotalcredit.Text = lprnpnjln_totalbayarcredit.ToString("#,##0");
                    lbl_lprnpnjln_sumfeeterapis.Text = lprnpnjln_feeterapis.ToString("#,##0");
                    #endregion
                    //dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly = true;
                    //dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;
                    //dgv_ctknota_tabelhrgpkt.Columns["Pilih"].Width = 70;
                    //dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].ReadOnly = true;
                    //dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].Width = 300;
                    //dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].ReadOnly = true;
                    //dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].Width = 175;
                    //dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].ReadOnly = true;
                    //dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].Width = 200;
                    //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].ReadOnly = true;
                    //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].Width = 175;
                    //dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
                    //dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
                    //dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;

                    //dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                    //dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                    //dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                    //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].SortMode = DataGridViewColumnSortMode.NotSortable;
                    //dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].SortMode = DataGridViewColumnSortMode.NotSortable;

                    //List<string> lstExtra = new List<string>();
                    //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                    //{
                    //    //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                    //    //extra = Convert.ToString());
                    //    double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                    //    int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                    //    double hasil = nominal * Convert.ToDouble(harga);
                    //    lstExtra.Add(hasil.ToString());
                    //    //MessageBox.Show(hasilfinal.ToString());
                    //    //foreach (char c in extra)
                    //    //{
                    //    //    if (char.IsDigit(c))
                    //    //    {
                    //    //        countdigitextra++;
                    //    //    }
                    //    //}
                    //    //int digit = countdigitextra;
                    //    //while (digit > 3)
                    //    //{
                    //    //    digit -= 3;
                    //    //    extra = extra.Insert(digit, ".");
                    //    //    dsCloned.Tables[0].Rows[i]["Nominal Extra"] = extra;
                    //    //}

                    //}

                    //DataSet dsCloned = lprnpnjln_DS.Clone();

                    //dsCloned.Tables[0].Columns["Harga Paket"].DataType = typeof(string);
                    //dsCloned.Tables[0].Columns["Tamu Hotel"].DataType = typeof(string);
                    //dsCloned.Tables[0].Columns["Nominal Extra"].DataType = typeof(string);
                    //foreach (DataRow row in lprnpnjln_DS.Tables[0].Rows)
                    //{
                    //    dsCloned.Tables[0].ImportRow(row);
                    //}

                    //dgv_ctknota_tabelhrgpkt.DataSource = dsCloned.Tables[0];

                    //int countdigitharga = 0;
                    //string hargapaket;
                    //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                    //{
                    //    countdigitharga = 0;
                    //    hargapaket = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString();
                    //    foreach (char c in hargapaket)
                    //    {
                    //        if (char.IsDigit(c))
                    //        {
                    //            countdigitharga++;
                    //        }
                    //    }
                    //    int digit = countdigitharga;
                    //    while (digit > 3)
                    //    {
                    //        digit -= 3;
                    //        hargapaket = hargapaket.Insert(digit, ".");
                    //        dsCloned.Tables[0].Rows[i]["Harga Paket"] = hargapaket;
                    //    }

                    //}

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

                    //int countdigitextra = 0;
                    //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                    //{
                    //    countdigitextra = 0;
                    //    //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                    //    //extra = Convert.ToString());
                    //    //double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                    //    //int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                    //    //double hasil = nominal * Convert.ToDouble(harga);
                    //    //extra = hasil.ToString();
                    //    //MessageBox.Show(hasilfinal.ToString());
                    //    foreach (char c in lstExtra[i])
                    //    {
                    //        if (char.IsDigit(c))
                    //        {
                    //            countdigitextra++;
                    //        }
                    //    }
                    //    int digit = countdigitextra;
                    //    while (digit > 3)
                    //    {
                    //        digit -= 3;
                    //        lstExtra[i] = lstExtra[i].Insert(digit, ".");
                    //        dsCloned.Tables[0].Rows[i]["Nominal Extra"] = lstExtra[i];
                    //    }

                    //}


                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    MessageBox.Show("Error Occurred");
                }
                lprnpnjln_conn2.Close();
                #endregion
            }
            
        }

        private void btn_lprnpnjln_batal_Click(object sender, EventArgs e)
        {
            pnl_lprnpnjln_isi.Visible = false;
            pnl_menu_isi.Visible = true;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = true;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;
        }
        #endregion

        #region(Panel Laporan Layanan)
        string lprnlayanan_tanggalcetakdari = "";
        string lprnlayanan_tanggalcetaksampai = "";
        DataSet lprnlayanan_DS = new DataSet();
        DataSet lprnlayanan_DSolahankomisi = new DataSet();
        //XLWorkbook lprnlayanan_wb;
        private void dtp_lprnlayanan_tgldari_ValueChanged(object sender, EventArgs e)
        {
            dgv_lprnlayanan_tabellayanan.DataSource = null;
            dgv_lprnlayanan_tabellayanan.Rows.Clear();
            dgv_lprnlayanan_tabellayanan.Columns.Clear();
            lprnlayanan_DS.Tables.Clear();
            #region(Select)
            string lprnlayanan_queryfinalcount = "";
            //string lprnlayanan_queryfinalkomisi = "";


            //#region(Select Query Komisi)
            //dgv_lprnlayanan_tabellayanan.DataSource = null;
            //dgv_lprnlayanan_tabellayanan.Rows.Clear();
            //dgv_lprnlayanan_tabellayanan.Columns.Clear();
            //lprnlayanan_DS.Tables.Clear();
            //lprnlayanan_DSolahankomisi.Tables.Clear();
            //string lprnlayanan_query;
            //MySqlConnection lprnlayanan_conn = new MySqlConnection(all_connStr);
            //try
            //{
            //    // RRyner   
            //    lprnlayanan_conn.Open();
            //    lprnlayanan_query = "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
            //                        "UNION " +
            //                        "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'Deluxe' " +
            //                        "UNION " +
            //                        "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'VVIP' " +
            //                        "UNION " +
            //                        "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
            //                        "UNION " +
            //                        "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'Deluxe' " +
            //                        "UNION " +
            //                        "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VVIP' " +
            //                        "UNION " +
            //                        "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
            //                        "UNION  " +
            //                        "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'Deluxe' " +
            //                        "UNION " +
            //                        "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'VVIP' " +
            //                        "UNION " +
            //                        "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
            //                        "UNION " +
            //                        "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'Deluxe'" +
            //                        "UNION " +
            //                        "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VVIP';";

            //    lprnlayanan_queryfinalkomisi = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

            //    MySqlCommand lprnlayanan_cmd = new MySqlCommand(lprnlayanan_query, lprnlayanan_conn);
            //    MySqlDataReader lprnlayanan_readr = lprnlayanan_cmd.ExecuteReader();

            //    lprnlayanan_tanggalcetakdari = dtp_lprnlayanan_tgldari.Value.Year.ToString();
            //    lprnlayanan_tanggalcetakdari += "-" + dtp_lprnlayanan_tgldari.Value.Month.ToString();
            //    lprnlayanan_tanggalcetakdari += "-" + dtp_lprnlayanan_tgldari.Value.Day.ToString();

            //    lprnlayanan_tanggalcetaksampai = dtp_lprnlayanan_tglsampai.Value.Year.ToString();
            //    lprnlayanan_tanggalcetaksampai += "-" + dtp_lprnlayanan_tglsampai.Value.Month.ToString();
            //    lprnlayanan_tanggalcetaksampai += "-" + dtp_lprnlayanan_tglsampai.Value.Day.ToString();

            //    if (lprnlayanan_readr.HasRows)
            //    {
            //        while (lprnlayanan_readr.Read())
            //        {
            //            string extra = "Dasar";
            //            if (lprnlayanan_readr.GetString(8) != "Tidak")
            //            {
            //                extra = "Extra";
            //            }

            //            int temp = lprnlayanan_readr.GetInt32(4);
            //            string harganormal = temp.ToString("0,0");
            //            string hargaextra = (temp + (temp * (edtvariabel_extra / 100))).ToString("0,0");

            //            //lprnlayanan_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)" +
            //            //                " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
            //            //                "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
            //            //                "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
            //            //                "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + " {" + extra + "} [" + lprnlayanan_readr.GetString(1) + "] " + lprnlayanan_readr.GetString(2) + " (" + harga + ")',";

            //            //lprnlayanan_queryfinal += "(SELECT COUNT(n1.id_nota)" +
            //            //                " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
            //            //                "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
            //            //                "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
            //            //                "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7).Substring(0, 1) + "\n\n{" + extra.Substring(0, 1) + "}\n\n[" + lprnlayanan_readr.GetString(1).Substring(0, 1) + "]\n\n" + lprnlayanan_readr.GetString(2) + "\n(" + harga + ")',";

            //            if (lprnlayanan_readr.GetString(7) == "Normal")
            //            {
            //                if (lprnlayanan_readr.GetString(8) == "Ya")
            //                {
            //                    lprnlayanan_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprnlayanan_readr.GetInt32(4) + (lprnlayanan_readr.GetInt32(4) * (lprnlayanan_readr.GetDouble(9) / 100))) * (lprnlayanan_readr.GetDouble(5) / 100)).ToString().Replace(',', '.') +
            //                            ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
            //                            "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
            //                            "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
            //                            "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + " {" + extra + "} [" + lprnlayanan_readr.GetString(1) + "] " + lprnlayanan_readr.GetString(2) + " (" + hargaextra + ")',";
            //                }
            //                else if (lprnlayanan_readr.GetString(8) == "Tidak")
            //                {
            //                    lprnlayanan_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprnlayanan_readr.GetInt32(4)) * (lprnlayanan_readr.GetDouble(5) / 100)).ToString().Replace(',', '.') +
            //                            ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
            //                            "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
            //                            "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
            //                            "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + " {" + extra + "} [" + lprnlayanan_readr.GetString(1) + "] " + lprnlayanan_readr.GetString(2) + " (" + harganormal + ")',";
            //                }

            //            }
            //            else if (lprnlayanan_readr.GetString(7) == "Midnight")
            //            {
            //                if (lprnlayanan_readr.GetString(8) == "Ya")
            //                {
            //                    lprnlayanan_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprnlayanan_readr.GetInt32(4) + (lprnlayanan_readr.GetInt32(4) * (lprnlayanan_readr.GetDouble(9) / 100))) * (lprnlayanan_readr.GetDouble(6) / 100)).ToString().Replace(',', '.') +
            //                            ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
            //                            "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
            //                            "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
            //                            "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + " {" + extra + "} [" + lprnlayanan_readr.GetString(1) + "] " + lprnlayanan_readr.GetString(2) + " (" + hargaextra + ")',";
            //                }
            //                else if (lprnlayanan_readr.GetString(8) == "Tidak")
            //                {
            //                    lprnlayanan_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprnlayanan_readr.GetInt32(4)) * (lprnlayanan_readr.GetDouble(6) / 100)).ToString().Replace(',', '.') +
            //                            ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
            //                            "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
            //                            "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
            //                            "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + " {" + extra + "} [" + lprnlayanan_readr.GetString(1) + "] " + lprnlayanan_readr.GetString(2) + " (" + harganormal + ")',";
            //                }
            //            }

            //        }
            //        //MessageBox.Show(readr.FieldCount + "");
            //    }
            //    lprnlayanan_queryfinalkomisi = lprnlayanan_queryfinalkomisi.Substring(0, lprnlayanan_queryfinalkomisi.Length - 1);
            //    lprnlayanan_queryfinalkomisi += " FROM terapis t";
            //}
            //catch (Exception ex)
            //{
            //    string error = ex.ToString();
            //    MessageBox.Show("Error Occurred");
            //}
            //lprnlayanan_conn.Close();
            //#endregion

            #region(Select Query Count)
            //dgv_lprnlayanan_tabellayanan.DataSource = null;
            //dgv_lprnlayanan_tabellayanan.Rows.Clear();
            //dgv_lprnlayanan_tabellayanan.Columns.Clear();
            //lprnlayanan_DS.Tables.Clear();
            string lprnlayanan_query2;
            MySqlConnection lprnlayanan_conn2 = new MySqlConnection(all_connStr);
            try
            {
                // RRyner   
                lprnlayanan_conn2.Open();
                lprnlayanan_query2 = "SELECT *, 'Normal', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION  " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe'" +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VVIP';";

                lprnlayanan_queryfinalcount = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

                MySqlCommand lprnlayanan_cmd2 = new MySqlCommand(lprnlayanan_query2, lprnlayanan_conn2);
                MySqlDataReader lprnlayanan_readr2 = lprnlayanan_cmd2.ExecuteReader();

                lprnlayanan_tanggalcetakdari = dtp_lprnlayanan_tgldari.Value.Year.ToString();
                lprnlayanan_tanggalcetakdari += "-" + dtp_lprnlayanan_tgldari.Value.Month.ToString();
                lprnlayanan_tanggalcetakdari += "-" + dtp_lprnlayanan_tgldari.Value.Day.ToString();

                lprnlayanan_tanggalcetaksampai = dtp_lprnlayanan_tglsampai.Value.Year.ToString();
                lprnlayanan_tanggalcetaksampai += "-" + dtp_lprnlayanan_tglsampai.Value.Month.ToString();
                lprnlayanan_tanggalcetaksampai += "-" + dtp_lprnlayanan_tglsampai.Value.Day.ToString();

                if (lprnlayanan_readr2.HasRows)
                {
                    while (lprnlayanan_readr2.Read())
                    {
                        string extra = "Dasar";
                        if (lprnlayanan_readr2.GetString(8) != "Tidak")
                        {
                            extra = "Extra";
                        }

                        string temp2 = lprnlayanan_readr2.GetString(4);
                        string harga2 = Convert.ToInt32(temp2).ToString(String.Format("0,0", temp2));

                        lprnlayanan_queryfinalcount += "(SELECT COUNT(n1.id_nota)" +
                                        " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr2.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr2.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr2.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr2.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr2.GetString(7) + " {" + extra + "} [" + lprnlayanan_readr2.GetString(1) + "] " + lprnlayanan_readr2.GetString(2) + " (" + harga2 + ")',";

                        //lprnlayanan_queryfinal += "(SELECT COUNT(n1.id_nota)" +
                        //                " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                        //                "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                        //                "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
                        //                "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7).Substring(0, 1) + "\n\n{" + extra.Substring(0, 1) + "}\n\n[" + lprnlayanan_readr.GetString(1).Substring(0, 1) + "]\n\n" + lprnlayanan_readr.GetString(2) + "\n(" + harga + ")',";
                        /*
                        if (lprnlayanan_readr.GetString(7)== "Normal")
                        {
                            lprnlayanan_queryfinal += "(SELECT COUNT(n1.id_nota)*" + lprnlayanan_readr.GetInt32(5) +
                                        " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + "\n\n{" + extra + "}\n\n[" + lprnlayanan_readr.GetString(1) + "]\n____________________\n" + lprnlayanan_readr.GetString(2) + "\n(" + harga + ")',";
                    
                        }
                        else if (lprnlayanan_readr.GetString(7) == "Midnight")
                        {
                            lprnlayanan_queryfinal += "(SELECT COUNT(n1.id_nota)*" + lprnlayanan_readr.GetInt32(6) +
                                        " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + "\n\n{" + extra + "}\n\n[" + lprnlayanan_readr.GetString(1) + "]\n____________________\n" + lprnlayanan_readr.GetString(2) + "\n(" + harga + ")',";
                    
                        }
                         * */
                    }
                    //MessageBox.Show(readr.FieldCount + "");
                }
                lprnlayanan_queryfinalcount = lprnlayanan_queryfinalcount.Substring(0, lprnlayanan_queryfinalcount.Length - 1);
                lprnlayanan_queryfinalcount += " FROM terapis t";
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnlayanan_conn2.Close();
            #endregion

            if (lprnlayanan_queryfinalcount != "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', FROM terapis t")
            {
                //MySqlConnection lprnlayanan_conn4 = new MySqlConnection(all_connStr);
                //try
                //{
                //    lprnlayanan_conn4.Open();

                //    MySqlDataAdapter lprnlayanan_mySqlDataAdapter4 = new MySqlDataAdapter(lprnlayanan_queryfinalkomisi, lprnlayanan_conn4);
                //    MySqlCommand lprnlayanan_cmd4 = new MySqlCommand(lprnlayanan_queryfinalcount, lprnlayanan_conn4);

                //    lprnlayanan_mySqlDataAdapter4.Fill(lprnlayanan_DSolahankomisi);
                //}
                //catch (Exception ex)
                //{
                //    string error = ex.ToString();
                //    MessageBox.Show("Error Occurred");
                //}
                //lprnlayanan_conn4.Close();

                #region(Isi Data)
                //lprnlayanan_DS.Tables.Clear();
                MySqlConnection lprnlayanan_conn3 = new MySqlConnection(all_connStr);
                try
                {
                    lprnlayanan_conn3.Open();

                    MySqlDataAdapter lprnlayanan_mySqlDataAdapter3 = new MySqlDataAdapter(lprnlayanan_queryfinalcount, lprnlayanan_conn3);
                    MySqlCommand lprnlayanan_cmd3 = new MySqlCommand(lprnlayanan_queryfinalcount, lprnlayanan_conn3);

                    lprnlayanan_mySqlDataAdapter3.Fill(lprnlayanan_DS);

                    dgv_lprnlayanan_tabellayanan.DataSource = lprnlayanan_DS.Tables[0];
                    btn_lprnlayanan_excel.Enabled = true;
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    MessageBox.Show("Error Occurred");
                }
                lprnlayanan_conn3.Close();
                #endregion
            }
            #endregion
        }

        private void dtp_lprnlayanan_tglsampai_ValueChanged(object sender, EventArgs e)
        {
            dgv_lprnlayanan_tabellayanan.DataSource = null;
            dgv_lprnlayanan_tabellayanan.Rows.Clear();
            dgv_lprnlayanan_tabellayanan.Columns.Clear();
            lprnlayanan_DS.Tables.Clear();
            #region(Select)
            string lprnlayanan_queryfinalcount = "";
            //string lprnlayanan_queryfinalkomisi = "";


            //#region(Select Query Komisi)
            //dgv_lprnlayanan_tabellayanan.DataSource = null;
            //dgv_lprnlayanan_tabellayanan.Rows.Clear();
            //dgv_lprnlayanan_tabellayanan.Columns.Clear();
            //lprnlayanan_DS.Tables.Clear();
            //lprnlayanan_DSolahankomisi.Tables.Clear();
            //string lprnlayanan_query;
            //MySqlConnection lprnlayanan_conn = new MySqlConnection(all_connStr);
            //try
            //{
            //    // RRyner   
            //    lprnlayanan_conn.Open();
            //    lprnlayanan_query = "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
            //                        "UNION " +
            //                        "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'Deluxe' " +
            //                        "UNION " +
            //                        "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'VVIP' " +
            //                        "UNION " +
            //                        "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
            //                        "UNION " +
            //                        "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'Deluxe' " +
            //                        "UNION " +
            //                        "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VVIP' " +
            //                        "UNION " +
            //                        "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
            //                        "UNION  " +
            //                        "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'Deluxe' " +
            //                        "UNION " +
            //                        "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'VVIP' " +
            //                        "UNION " +
            //                        "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
            //                        "UNION " +
            //                        "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'Deluxe'" +
            //                        "UNION " +
            //                        "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VVIP';";

            //    lprnlayanan_queryfinalkomisi = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

            //    MySqlCommand lprnlayanan_cmd = new MySqlCommand(lprnlayanan_query, lprnlayanan_conn);
            //    MySqlDataReader lprnlayanan_readr = lprnlayanan_cmd.ExecuteReader();

            //    lprnlayanan_tanggalcetakdari = dtp_lprnlayanan_tgldari.Value.Year.ToString();
            //    lprnlayanan_tanggalcetakdari += "-" + dtp_lprnlayanan_tgldari.Value.Month.ToString();
            //    lprnlayanan_tanggalcetakdari += "-" + dtp_lprnlayanan_tgldari.Value.Day.ToString();

            //    lprnlayanan_tanggalcetaksampai = dtp_lprnlayanan_tglsampai.Value.Year.ToString();
            //    lprnlayanan_tanggalcetaksampai += "-" + dtp_lprnlayanan_tglsampai.Value.Month.ToString();
            //    lprnlayanan_tanggalcetaksampai += "-" + dtp_lprnlayanan_tglsampai.Value.Day.ToString();

            //    if (lprnlayanan_readr.HasRows)
            //    {
            //        while (lprnlayanan_readr.Read())
            //        {
            //            string extra = "Dasar";
            //            if (lprnlayanan_readr.GetString(8) != "Tidak")
            //            {
            //                extra = "Extra";
            //            }

            //            int temp = lprnlayanan_readr.GetInt32(4);
            //            string harganormal = temp.ToString("0,0");
            //            string hargaextra = (temp + (temp * (edtvariabel_extra / 100))).ToString("0,0");

            //            //lprnlayanan_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)" +
            //            //                " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
            //            //                "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
            //            //                "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
            //            //                "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + " {" + extra + "} [" + lprnlayanan_readr.GetString(1) + "] " + lprnlayanan_readr.GetString(2) + " (" + harga + ")',";

            //            //lprnlayanan_queryfinal += "(SELECT COUNT(n1.id_nota)" +
            //            //                " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
            //            //                "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
            //            //                "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
            //            //                "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7).Substring(0, 1) + "\n\n{" + extra.Substring(0, 1) + "}\n\n[" + lprnlayanan_readr.GetString(1).Substring(0, 1) + "]\n\n" + lprnlayanan_readr.GetString(2) + "\n(" + harga + ")',";

            //            if (lprnlayanan_readr.GetString(7) == "Normal")
            //            {
            //                if (lprnlayanan_readr.GetString(8) == "Ya")
            //                {
            //                    lprnlayanan_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprnlayanan_readr.GetInt32(4) + (lprnlayanan_readr.GetInt32(4) * (lprnlayanan_readr.GetDouble(9) / 100))) * (lprnlayanan_readr.GetDouble(5) / 100)).ToString().Replace(',', '.') +
            //                            ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
            //                            "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
            //                            "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
            //                            "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + " {" + extra + "} [" + lprnlayanan_readr.GetString(1) + "] " + lprnlayanan_readr.GetString(2) + " (" + hargaextra + ")',";
            //                }
            //                else if (lprnlayanan_readr.GetString(8) == "Tidak")
            //                {
            //                    lprnlayanan_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprnlayanan_readr.GetInt32(4)) * (lprnlayanan_readr.GetDouble(5) / 100)).ToString().Replace(',', '.') +
            //                            ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
            //                            "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
            //                            "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
            //                            "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + " {" + extra + "} [" + lprnlayanan_readr.GetString(1) + "] " + lprnlayanan_readr.GetString(2) + " (" + harganormal + ")',";
            //                }

            //            }
            //            else if (lprnlayanan_readr.GetString(7) == "Midnight")
            //            {
            //                if (lprnlayanan_readr.GetString(8) == "Ya")
            //                {
            //                    lprnlayanan_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprnlayanan_readr.GetInt32(4) + (lprnlayanan_readr.GetInt32(4) * (lprnlayanan_readr.GetDouble(9) / 100))) * (lprnlayanan_readr.GetDouble(6) / 100)).ToString().Replace(',', '.') +
            //                            ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
            //                            "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
            //                            "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
            //                            "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + " {" + extra + "} [" + lprnlayanan_readr.GetString(1) + "] " + lprnlayanan_readr.GetString(2) + " (" + hargaextra + ")',";
            //                }
            //                else if (lprnlayanan_readr.GetString(8) == "Tidak")
            //                {
            //                    lprnlayanan_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprnlayanan_readr.GetInt32(4)) * (lprnlayanan_readr.GetDouble(6) / 100)).ToString().Replace(',', '.') +
            //                            ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
            //                            "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
            //                            "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
            //                            "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + " {" + extra + "} [" + lprnlayanan_readr.GetString(1) + "] " + lprnlayanan_readr.GetString(2) + " (" + harganormal + ")',";
            //                }
            //            }

            //        }
            //        //MessageBox.Show(readr.FieldCount + "");
            //    }
            //    lprnlayanan_queryfinalkomisi = lprnlayanan_queryfinalkomisi.Substring(0, lprnlayanan_queryfinalkomisi.Length - 1);
            //    lprnlayanan_queryfinalkomisi += " FROM terapis t";
            //}
            //catch (Exception ex)
            //{
            //    string error = ex.ToString();
            //    MessageBox.Show("Error Occurred");
            //}
            //lprnlayanan_conn.Close();
            //#endregion

            #region(Select Query Count)
            //dgv_lprnlayanan_tabellayanan.DataSource = null;
            //dgv_lprnlayanan_tabellayanan.Rows.Clear();
            //dgv_lprnlayanan_tabellayanan.Columns.Clear();
            //lprnlayanan_DS.Tables.Clear();
            string lprnlayanan_query2;
            MySqlConnection lprnlayanan_conn2 = new MySqlConnection(all_connStr);
            try
            {
                // RRyner   
                lprnlayanan_conn2.Open();
                lprnlayanan_query2 = "SELECT *, 'Normal', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION  " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe'" +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VVIP';";

                lprnlayanan_queryfinalcount = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

                MySqlCommand lprnlayanan_cmd2 = new MySqlCommand(lprnlayanan_query2, lprnlayanan_conn2);
                MySqlDataReader lprnlayanan_readr2 = lprnlayanan_cmd2.ExecuteReader();

                lprnlayanan_tanggalcetakdari = dtp_lprnlayanan_tgldari.Value.Year.ToString();
                lprnlayanan_tanggalcetakdari += "-" + dtp_lprnlayanan_tgldari.Value.Month.ToString();
                lprnlayanan_tanggalcetakdari += "-" + dtp_lprnlayanan_tgldari.Value.Day.ToString();

                lprnlayanan_tanggalcetaksampai = dtp_lprnlayanan_tglsampai.Value.Year.ToString();
                lprnlayanan_tanggalcetaksampai += "-" + dtp_lprnlayanan_tglsampai.Value.Month.ToString();
                lprnlayanan_tanggalcetaksampai += "-" + dtp_lprnlayanan_tglsampai.Value.Day.ToString();

                if (lprnlayanan_readr2.HasRows)
                {
                    while (lprnlayanan_readr2.Read())
                    {
                        string extra = "Dasar";
                        if (lprnlayanan_readr2.GetString(8) != "Tidak")
                        {
                            extra = "Extra";
                        }

                        string temp2 = lprnlayanan_readr2.GetString(4);
                        string harga2 = Convert.ToInt32(temp2).ToString(String.Format("0,0", temp2));

                        lprnlayanan_queryfinalcount += "(SELECT COUNT(n1.id_nota)" +
                                        " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr2.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr2.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr2.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr2.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr2.GetString(7) + " {" + extra + "} [" + lprnlayanan_readr2.GetString(1) + "] " + lprnlayanan_readr2.GetString(2) + " (" + harga2 + ")',";

                        //lprnlayanan_queryfinal += "(SELECT COUNT(n1.id_nota)" +
                        //                " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                        //                "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                        //                "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
                        //                "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7).Substring(0, 1) + "\n\n{" + extra.Substring(0, 1) + "}\n\n[" + lprnlayanan_readr.GetString(1).Substring(0, 1) + "]\n\n" + lprnlayanan_readr.GetString(2) + "\n(" + harga + ")',";
                        /*
                        if (lprnlayanan_readr.GetString(7)== "Normal")
                        {
                            lprnlayanan_queryfinal += "(SELECT COUNT(n1.id_nota)*" + lprnlayanan_readr.GetInt32(5) +
                                        " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + "\n\n{" + extra + "}\n\n[" + lprnlayanan_readr.GetString(1) + "]\n____________________\n" + lprnlayanan_readr.GetString(2) + "\n(" + harga + ")',";
                    
                        }
                        else if (lprnlayanan_readr.GetString(7) == "Midnight")
                        {
                            lprnlayanan_queryfinal += "(SELECT COUNT(n1.id_nota)*" + lprnlayanan_readr.GetInt32(6) +
                                        " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + "\n\n{" + extra + "}\n\n[" + lprnlayanan_readr.GetString(1) + "]\n____________________\n" + lprnlayanan_readr.GetString(2) + "\n(" + harga + ")',";
                    
                        }
                         * */
                    }
                    //MessageBox.Show(readr.FieldCount + "");
                }
                lprnlayanan_queryfinalcount = lprnlayanan_queryfinalcount.Substring(0, lprnlayanan_queryfinalcount.Length - 1);
                lprnlayanan_queryfinalcount += " FROM terapis t";
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnlayanan_conn2.Close();
            #endregion

            if (lprnlayanan_queryfinalcount != "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', FROM terapis t")
            {
                //MySqlConnection lprnlayanan_conn4 = new MySqlConnection(all_connStr);
                //try
                //{
                //    lprnlayanan_conn4.Open();

                //    MySqlDataAdapter lprnlayanan_mySqlDataAdapter4 = new MySqlDataAdapter(lprnlayanan_queryfinalkomisi, lprnlayanan_conn4);
                //    MySqlCommand lprnlayanan_cmd4 = new MySqlCommand(lprnlayanan_queryfinalcount, lprnlayanan_conn4);

                //    lprnlayanan_mySqlDataAdapter4.Fill(lprnlayanan_DSolahankomisi);
                //}
                //catch (Exception ex)
                //{
                //    string error = ex.ToString();
                //    MessageBox.Show("Error Occurred");
                //}
                //lprnlayanan_conn4.Close();

                #region(Isi Data)
                //lprnlayanan_DS.Tables.Clear();
                MySqlConnection lprnlayanan_conn3 = new MySqlConnection(all_connStr);
                try
                {
                    lprnlayanan_conn3.Open();

                    MySqlDataAdapter lprnlayanan_mySqlDataAdapter3 = new MySqlDataAdapter(lprnlayanan_queryfinalcount, lprnlayanan_conn3);
                    MySqlCommand lprnlayanan_cmd3 = new MySqlCommand(lprnlayanan_queryfinalcount, lprnlayanan_conn3);

                    lprnlayanan_mySqlDataAdapter3.Fill(lprnlayanan_DS);

                    dgv_lprnlayanan_tabellayanan.DataSource = lprnlayanan_DS.Tables[0];
                    btn_lprnlayanan_excel.Enabled = true;
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    MessageBox.Show("Error Occurred");
                }
                lprnlayanan_conn3.Close();
                #endregion
            }
            #endregion
        }

        private void btn_lprnlayanan_excel_Click(object sender, EventArgs e)
        {
            string folderPath = "C:\\Excel\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                //wb.Cell().DataType = XLCellValues.Number;
                wb.Worksheets.Add(lprnlayanan_DS.Tables[0], "Laporan Layanan");
                wb.SaveAs(folderPath + "Laporan Layanan (" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour + ";" + DateTime.Now.Minute.ToString() + ";" + DateTime.Now.Second.ToString() + ").xlsx");
                MessageBox.Show("File Excel telah disimpan");
            }
            #region(Slip Gaji)
            //lprnlayanan_wb = new XLWorkbook();
            //lprnlayanan_wb.Worksheets.Add("Slip Gaji");

            //string folderPath = "C:\\Excel\\";
            //if (!Directory.Exists(folderPath))
            //{
            //    Directory.CreateDirectory(folderPath);
            //}
            //List<string>[] listrowcetak_jenispaket = new List<string>[1];
            //listrowcetak_jenispaket[0] = new List<string>();
            //Array.Resize(ref listrowcetak_jenispaket, 2);
            //List<Int32>[] listrowcetak_counterjasa = new List<Int32>[lprnlayanan_DSolahankomisi.Tables[0].Rows.Count];
            //List<Double>[] listrowcetak_komisi = new List<Double>[lprnlayanan_DSolahankomisi.Tables[0].Rows.Count];
            //for (int i = 0; i < listrowcetak_counterjasa.Length; i++)
            //{
            //    listrowcetak_counterjasa[i] = new List<Int32>();
            //    listrowcetak_komisi[i] = new List<Double>();
            //}
            //for (int i = 3; i < lprnlayanan_DSolahankomisi.Tables[0].Columns.Count; i++)
            //{
            //    if (lprnlayanan_DSolahankomisi.Tables[0].Columns[i].ColumnName.Substring(0, 6) == "Normal")
            //    {
            //        if (i == 3)
            //        {
            //            listrowcetak_jenispaket[0].Add("Normal");
            //            listrowcetak_jenispaket[0].Add(lprnlayanan_DSolahankomisi.Tables[0].Columns[i].ColumnName);
            //        }
            //        else if (lprnlayanan_DSolahankomisi.Tables[0].Columns[i + 1].ColumnName.Substring(0, 8) == "Midnight")
            //        {
            //            listrowcetak_jenispaket[0].Add(lprnlayanan_DSolahankomisi.Tables[0].Columns[i].ColumnName);
            //            listrowcetak_jenispaket[0].Add("Midnight");
            //        }
            //        else
            //        {
            //            listrowcetak_jenispaket[0].Add(lprnlayanan_DSolahankomisi.Tables[0].Columns[i].ColumnName);
            //        }
            //    }
            //    else if (lprnlayanan_DSolahankomisi.Tables[0].Columns[i].ColumnName.Substring(0, 8) == "Midnight")
            //    {
            //        listrowcetak_jenispaket[0].Add(lprnlayanan_DSolahankomisi.Tables[0].Columns[i].ColumnName);
            //    }
            //}

            //for (int i = 0; i < listrowcetak_counterjasa.Length; i++)
            //{
            //    for (int ii = 3; ii < lprnlayanan_DS.Tables[0].Columns.Count; ii++)
            //    {
            //        listrowcetak_counterjasa[i].Add(int.Parse(lprnlayanan_DS.Tables[0].Rows[i][ii].ToString()));
            //    }
            //}
            //for (int i = 0; i < listrowcetak_counterjasa.Length; i++)
            //{
            //    for (int ii = 0; ii < listrowcetak_jenispaket[0].Count; ii++)
            //    {
            //        if (listrowcetak_jenispaket[0][ii] == "Normal")
            //        {
            //            listrowcetak_counterjasa[i].Insert(ii, 0);
            //        }
            //        else if (listrowcetak_jenispaket[0][ii] == "Midnight")
            //        {
            //            listrowcetak_counterjasa[i].Insert(ii, 0);
            //        }
            //    }
            //}

            //for (int i = 0; i < listrowcetak_komisi.Length; i++)
            //{
            //    for (int ii = 3; ii < lprnlayanan_DS.Tables[0].Columns.Count; ii++)
            //    {
            //        listrowcetak_komisi[i].Add(lprnlayanan_DSolahankomisi.Tables[0].Rows[i][ii].CastTo<Double>());
            //    }
            //}
            //for (int i = 0; i < listrowcetak_komisi.Length; i++)
            //{
            //    for (int ii = 0; ii < listrowcetak_jenispaket[0].Count; ii++)
            //    {
            //        if (listrowcetak_jenispaket[0][ii] == "Normal")
            //        {
            //            listrowcetak_komisi[i].Insert(ii, 0);
            //        }
            //        else if (listrowcetak_jenispaket[0][ii] == "Midnight")
            //        {
            //            listrowcetak_komisi[i].Insert(ii, 0);
            //        }
            //    }
            //}

            //lprnlayanan_wb.Worksheet("Slip Gaji").PageSetup.Margins.Top = 1;
            //lprnlayanan_wb.Worksheet("Slip Gaji").PageSetup.Margins.Bottom = 1;
            //lprnlayanan_wb.Worksheet("Slip Gaji").PageSetup.Margins.Left = 0.6;
            //lprnlayanan_wb.Worksheet("Slip Gaji").PageSetup.Margins.Right = 0.6;
            //lprnlayanan_wb.Worksheet("Slip Gaji").PageSetup.Margins.Footer = 0.8;
            //lprnlayanan_wb.Worksheet("Slip Gaji").PageSetup.Margins.Header = 0.8;
            //lprnlayanan_wb.Worksheet("Slip Gaji").PageSetup.CenterHorizontally = true;
            //int pageoffset = 0;
            //int pagecount = 1;
            //pagecount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(listrowcetak_jenispaket[0].Count) / 30));
            //if (listrowcetak_jenispaket[0].Count - (30 * (pagecount - 1)) > 22)
            //{
            //    pagecount++;
            //}
            //if (pagecount == 1)
            //{
            //    for (int i = 0; i < listrowcetak_komisi.Length; i++)
            //    {
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Value = "SLIP GAJI";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Style.Font.FontSize = 24;
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Range(1 + pageoffset, 1, 1 + pageoffset, 14).Merge();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 2).Value = "BULAN";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 2).Style.Font.SetBold();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 3).Value = dtp_lprnlayanan_tgldari.Value.ToLongDateString() + " - " + dtp_lprnlayanan_tglsampai.Value.ToLongDateString();

            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 2).Value = "NO ID";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 2).Style.Font.SetBold();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 3).Value = lprnlayanan_DS.Tables[0].Rows[i][0];
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 2).Value = "NAMA";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 2).Style.Font.SetBold();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 3).Value = lprnlayanan_DS.Tables[0].Rows[i][1];
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 2).Value = "POSISI";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 2).Style.Font.SetBold();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 3).Value = lprnlayanan_DS.Tables[0].Rows[i][2];

            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Value = "Jenis Paket";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Style.Font.SetBold();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, 9 + pageoffset, 11).Merge();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Value = "Jumlah";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Style.Font.SetBold();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Value = "Komisi";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Style.Font.SetBold();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 2).InsertData(listrowcetak_jenispaket[0]);
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 12).InsertData(listrowcetak_counterjasa[i]);
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 13).InsertData(listrowcetak_komisi[i]);
            //        //lprnlayanan_wb.Worksheet("Slip Gaji").Column(2).AdjustToContents();
            //        for (int ii = 0; ii < listrowcetak_jenispaket[0].Count; ii++)
            //        {
            //            if (listrowcetak_jenispaket[0][ii] == "Normal")
            //            {
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Value = "";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 12).Value = "";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 2).Style.Font.SetBold();
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
            //            }
            //            else if (listrowcetak_jenispaket[0][ii] == "Midnight")
            //            {
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Value = "";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 12).Value = "";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 2).Style.Font.SetBold();
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
            //            }
            //            else
            //            {
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
            //            }
            //        }
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 2).Value = "Total";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 2).Style.Font.SetBold();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 12).Merge();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).FormulaA1 = "SUM(M" + (10 + pageoffset).ToString() + ":M" + (listrowcetak_jenispaket[0].Count + 9 + pageoffset).ToString() + ")";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorderColor(XLColor.Black);
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 11).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 11).Style.Border.SetOutsideBorderColor(XLColor.Black);
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 12).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 12).Style.Border.SetOutsideBorderColor(XLColor.Black);
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorderColor(XLColor.Black);

            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 2).Value = "+ Bonus Target";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 5).Value = ":";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 2).Value = "+ Bonus Tahunan";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 5).Value = ":";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 2).Value = "+ Bonus Lain-lain";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 5).Value = ":";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 15 + pageoffset, 2).Value = "- Potongan Biaya Masuk";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 15 + pageoffset, 5).Value = ":";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 15 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 16 + pageoffset, 2).Value = "- Potongan Absensi";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 16 + pageoffset, 5).Value = ":";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 16 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 17 + pageoffset, 2).Value = "- Potong Tabungan";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 17 + pageoffset, 5).Value = ":";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 17 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 18 + pageoffset, 2).Value = "- Potong Hutang";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 18 + pageoffset, 5).Value = ":";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 18 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 2).Value = "Gaji Total";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 2).Style.Font.SetBold();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 5).Value = ":";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 5).Style.Font.SetBold();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 6).FormulaA1 = "M" + (listrowcetak_jenispaket[0].Count + 10 + pageoffset).ToString() + "+SUM(F" + (listrowcetak_jenispaket[0].Count + 12 + pageoffset).ToString() + ":F" + (listrowcetak_jenispaket[0].Count + 14 + pageoffset).ToString() +
            //                                                                                                    ")-SUM(F" + (listrowcetak_jenispaket[0].Count + 15 + pageoffset).ToString() + ":F" + (listrowcetak_jenispaket[0].Count + 18 + pageoffset).ToString() + ")";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 6).Style.Font.SetBold();

            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 8).Value = "Sisa Hutang";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 11).Value = ":";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 8).Value = "Total Tabungan";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 11).Value = ":";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 8).Value = "Sisa Biaya Masuk Kembali";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 11).Value = ":";
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";

            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(43 + pageoffset, 13).Value = "Hal. " + 1.ToString();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Range(43 + pageoffset, 13, 43 + pageoffset, 14).Merge();
            //        lprnlayanan_wb.Worksheet("Slip Gaji").Cell(43 + pageoffset, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            //        pageoffset += 43;
            //    }
            //}
            //else if (pagecount > 1)
            //{
            //    Array.Resize(ref listrowcetak_jenispaket, pagecount);
            //    for (int i = 1; i < pagecount; i++)
            //    {
            //        listrowcetak_jenispaket[i] = new List<string>();
            //    }
            //    List<int>[,] listrowcetak_counterjasahal2dst = new List<int>[lprnlayanan_DSolahankomisi.Tables[0].Rows.Count, pagecount];
            //    List<double>[,] listrowcetak_komisihal2dst = new List<double>[lprnlayanan_DSolahankomisi.Tables[0].Rows.Count, pagecount];
            //    for (int x = 0; x < lprnlayanan_DSolahankomisi.Tables[0].Rows.Count; x++)
            //    {
            //        for (int y = 0; y < pagecount; y++)
            //        {
            //            listrowcetak_counterjasahal2dst[x, y] = new List<int>();
            //            listrowcetak_komisihal2dst[x, y] = new List<double>();
            //        }
            //    }
            //    for (int x = 0; x < lprnlayanan_DSolahankomisi.Tables[0].Rows.Count; x++)
            //    {
            //        listrowcetak_counterjasahal2dst[x, 0] = listrowcetak_counterjasa[x];
            //        listrowcetak_komisihal2dst[x, 0] = listrowcetak_komisi[x];
            //    }
            //    int normalextracont = 0;
            //    int midnightextracount = 0;
            //    int countfortakeitem = 0;
            //    for (int row = 0; row < listrowcetak_jenispaket.Length; row++)
            //    {
            //        midnightextracount = 0;
            //        normalextracont = 0;
            //        if (listrowcetak_jenispaket[row].Count > 30)
            //        {
            //            countfortakeitem = (listrowcetak_jenispaket[row].Count - 2) / 4 - 7;
            //            for (int i = 0; i < listrowcetak_jenispaket[row].Count; i++)
            //            {
            //                if (row + 1 <= listrowcetak_jenispaket.Length)
            //                {
            //                    if (listrowcetak_jenispaket[row][i] == "Normal")
            //                    {
            //                        listrowcetak_jenispaket[row + 1].Add("Normal");
            //                        for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
            //                        {
            //                            listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(0);
            //                            listrowcetak_komisihal2dst[idxterapis, row + 1].Add(0);
            //                        }
            //                    }
            //                    else if (i > 1)
            //                    {
            //                        if (listrowcetak_jenispaket[row][i].Contains("Normal {Extra}") && normalextracont < 1)
            //                        {
            //                            for (int idx = 1; idx <= countfortakeitem; idx++)
            //                            {
            //                                listrowcetak_jenispaket[row + 1].Add(listrowcetak_jenispaket[row][i - idx]);
            //                                listrowcetak_jenispaket[row].RemoveAt(i - idx);
            //                            }
            //                            for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
            //                            {
            //                                for (int idx = 1; idx <= countfortakeitem; idx++)
            //                                {
            //                                    listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(listrowcetak_counterjasahal2dst[idxterapis, row][i - idx]);
            //                                    listrowcetak_counterjasahal2dst[idxterapis, row].RemoveAt(i - idx);
            //                                    listrowcetak_komisihal2dst[idxterapis, row + 1].Add(listrowcetak_komisihal2dst[idxterapis, row][i - idx]);
            //                                    listrowcetak_komisihal2dst[idxterapis, row].RemoveAt(i - idx);
            //                                }
            //                            }
            //                            normalextracont++;
            //                        }
            //                        else if (listrowcetak_jenispaket[row][i] == "Midnight")
            //                        {
            //                            for (int idx = 1; idx <= countfortakeitem; idx++)
            //                            {
            //                                listrowcetak_jenispaket[row + 1].Add(listrowcetak_jenispaket[row][i - idx]);
            //                                listrowcetak_jenispaket[row].RemoveAt(i - idx);
            //                            }
            //                            for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
            //                            {
            //                                for (int idx = 1; idx <= countfortakeitem; idx++)
            //                                {
            //                                    listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(listrowcetak_counterjasahal2dst[idxterapis, row][i - idx]);
            //                                    listrowcetak_counterjasahal2dst[idxterapis, row].RemoveAt(i - idx);
            //                                    listrowcetak_komisihal2dst[idxterapis, row + 1].Add(listrowcetak_komisihal2dst[idxterapis, row][i - idx]);
            //                                    listrowcetak_komisihal2dst[idxterapis, row].RemoveAt(i - idx);
            //                                }
            //                            }
            //                            listrowcetak_jenispaket[row + 1].Add("Midnight");
            //                            for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
            //                            {
            //                                listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(0);
            //                                listrowcetak_komisihal2dst[idxterapis, row + 1].Add(0);
            //                            }
            //                        }
            //                        else if (listrowcetak_jenispaket[row][i].Contains("Midnight {Extra}") && midnightextracount < 1)
            //                        {
            //                            for (int idx = 1; idx <= countfortakeitem; idx++)
            //                            {
            //                                listrowcetak_jenispaket[row + 1].Add(listrowcetak_jenispaket[row][i - idx]);
            //                                listrowcetak_jenispaket[row].RemoveAt(i - idx);
            //                            }
            //                            for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
            //                            {
            //                                for (int idx = 1; idx <= countfortakeitem; idx++)
            //                                {
            //                                    listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(listrowcetak_counterjasahal2dst[idxterapis, row][i - idx]);
            //                                    listrowcetak_counterjasahal2dst[idxterapis, row].RemoveAt(i - idx);
            //                                    listrowcetak_komisihal2dst[idxterapis, row + 1].Add(listrowcetak_komisihal2dst[idxterapis, row][i - idx]);
            //                                    listrowcetak_komisihal2dst[idxterapis, row].RemoveAt(i - idx);
            //                                }
            //                            }
            //                            midnightextracount++;
            //                        }
            //                        else if (i == listrowcetak_jenispaket[row].Count - 1)
            //                        {
            //                            for (int idx = 0; idx < countfortakeitem; idx++)
            //                            {
            //                                listrowcetak_jenispaket[row + 1].Add(listrowcetak_jenispaket[row][i - idx]);
            //                                listrowcetak_jenispaket[row].RemoveAt(i - idx);
            //                            }
            //                            for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
            //                            {
            //                                for (int idx = 1; idx <= countfortakeitem; idx++)
            //                                {
            //                                    listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(listrowcetak_counterjasahal2dst[idxterapis, row][i - idx]);
            //                                    listrowcetak_counterjasahal2dst[idxterapis, row].RemoveAt(i - idx);
            //                                    listrowcetak_komisihal2dst[idxterapis, row + 1].Add(listrowcetak_komisihal2dst[idxterapis, row][i - idx]);
            //                                    listrowcetak_komisihal2dst[idxterapis, row].RemoveAt(i - idx);
            //                                }
            //                            }
            //                        }
            //                    }
            //                }

            //            }
            //        }
            //    }
            //    for (int i = 0; i < listrowcetak_komisi.Length; i++)
            //    {
            //        string formulatotalkomisi = "";
            //        for (int p = 1; p <= pagecount; p++)
            //        {
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Value = "SLIP GAJI";
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Style.Font.FontSize = 24;
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Range(1 + pageoffset, 1, 1 + pageoffset, 14).Merge();
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 2).Value = "BULAN";
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 2).Style.Font.SetBold();
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 3).Value = dtp_lprnlayanan_tgldari.Value.ToLongDateString() + " - " + dtp_lprnlayanan_tglsampai.Value.ToLongDateString();

            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 2).Value = "NO ID";
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 2).Style.Font.SetBold();
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 3).Value = lprnlayanan_DS.Tables[0].Rows[i][0];
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 2).Value = "NAMA";
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 2).Style.Font.SetBold();
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 3).Value = lprnlayanan_DS.Tables[0].Rows[i][1];
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 2).Value = "POSISI";
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 2).Style.Font.SetBold();
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 3).Value = lprnlayanan_DS.Tables[0].Rows[i][2];

            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Value = "Jenis Paket";
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Style.Font.SetBold();
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, 9 + pageoffset, 11).Merge();
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Value = "Jumlah";
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Style.Font.SetBold();
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Value = "Komisi";
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Style.Font.SetBold();
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 2).InsertData(listrowcetak_jenispaket[p - 1]);
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 12).InsertData(listrowcetak_counterjasahal2dst[i, p - 1]);
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 13).InsertData(listrowcetak_komisihal2dst[i, p - 1]);
            //            //lprnlayanan_wb.Worksheet("Slip Gaji").Column(2).AdjustToContents();
            //            for (int ii = 0; ii < listrowcetak_jenispaket[p - 1].Count; ii++)
            //            {
            //                if (listrowcetak_jenispaket[p - 1][ii] == "Normal")
            //                {
            //                    lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Value = "";
            //                    lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 12).Value = "";
            //                    lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 2).Style.Font.SetBold();
            //                    lprnlayanan_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
            //                }
            //                else if (listrowcetak_jenispaket[p - 1][ii] == "Midnight")
            //                {
            //                    lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Value = "";
            //                    lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 12).Value = "";
            //                    lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 2).Style.Font.SetBold();
            //                    lprnlayanan_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
            //                }
            //                else
            //                {
            //                    lprnlayanan_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
            //                    lprnlayanan_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
            //                }
            //            }
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 13).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 13).Style.Border.SetOutsideBorderColor(XLColor.Black);
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 11).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 11).Style.Border.SetOutsideBorderColor(XLColor.Black);
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 12).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 12).Style.Border.SetOutsideBorderColor(XLColor.Black);
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 13).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 13).Style.Border.SetOutsideBorderColor(XLColor.Black);

            //            if (listrowcetak_jenispaket[p - 1].Count <= 0)
            //            {
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Value = "Total";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Style.Font.SetBold();
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 12).Merge();
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).FormulaA1 = "0";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
            //            }
            //            else if (listrowcetak_jenispaket[p - 1].Count > 0)
            //            {
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Value = "Total";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Style.Font.SetBold();
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 12).Merge();
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).FormulaA1 = "SUM(M" + (10 + pageoffset).ToString() + ":M" + (listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset).ToString() + ")";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);

            //            }
            //            formulatotalkomisi += "M" + (listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset).ToString() + "+";

            //            if (listrowcetak_jenispaket[p - 1].Count <= 22)
            //            {
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 2).Value = "+ Bonus Target";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 5).Value = ":";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 2).Value = "+ Bonus Tahunan";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 5).Value = ":";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 2).Value = "+ Bonus Lain-lain";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 5).Value = ":";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 15 + pageoffset, 2).Value = "- Potongan Biaya Masuk";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 15 + pageoffset, 5).Value = ":";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 15 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 16 + pageoffset, 2).Value = "- Potongan Absensi";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 16 + pageoffset, 5).Value = ":";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 16 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 17 + pageoffset, 2).Value = "- Potong Tabungan";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 17 + pageoffset, 5).Value = ":";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 17 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 18 + pageoffset, 2).Value = "- Potong Hutang";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 18 + pageoffset, 5).Value = ":";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 18 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 2).Value = "Gaji Total";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 2).Style.Font.SetBold();
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 5).Value = ":";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 5).Style.Font.SetBold();
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 6).FormulaA1 = formulatotalkomisi + "SUM(F" + (listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset).ToString() + ":F" + (listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset).ToString() +
            //                                                                                                            ")-SUM(F" + (listrowcetak_jenispaket[p - 1].Count + 15 + pageoffset).ToString() + ":F" + (listrowcetak_jenispaket[p - 1].Count + 18 + pageoffset).ToString() + ")";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 6).Style.Font.SetBold();

            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 8).Value = "Sisa Hutang";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 11).Value = ":";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 8).Value = "Total Tabungan";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 11).Value = ":";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 8).Value = "Sisa Biaya Masuk Kembali";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 11).Value = ":";
            //                lprnlayanan_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";

            //            }

            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(43 + pageoffset, 13).Value = "Hal. " + p.ToString();
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Range(43 + pageoffset, 13, 43 + pageoffset, 14).Merge();
            //            lprnlayanan_wb.Worksheet("Slip Gaji").Cell(43 + pageoffset, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            //            pageoffset += 43;

            //        }
            //    }
            //}



            //lprnlayanan_wb.Worksheet("Slip Gaji").Column("A").Width = 2.29;
            //lprnlayanan_wb.Worksheet("Slip Gaji").Column("C").Width = 4.43;
            //lprnlayanan_wb.Worksheet("Slip Gaji").Column("E").Width = 0.58;
            //lprnlayanan_wb.Worksheet("Slip Gaji").Column("F").Width = 10.43;
            //lprnlayanan_wb.Worksheet("Slip Gaji").Column("G").Width = 2.71;
            //lprnlayanan_wb.Worksheet("Slip Gaji").Column("I").Width = 5.29;
            //lprnlayanan_wb.Worksheet("Slip Gaji").Column("K").Width = 0.58;
            //lprnlayanan_wb.Worksheet("Slip Gaji").Column("L").Width = 10.43;
            //lprnlayanan_wb.Worksheet("Slip Gaji").Column("M").Width = 10.43;
            //lprnlayanan_wb.Worksheet("Slip Gaji").Column("N").Width = 0.67;


            //lprnlayanan_wb.SaveAs(folderPath + "Laporan Layanan (" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour + ";" + DateTime.Now.Minute.ToString() + ";" + DateTime.Now.Second.ToString() + ").xlsx");
            //MessageBox.Show("File Excel telah disimpan");
            //btn_lprnlayanan_excel.Enabled = false;
            //}
            #endregion
        }

        private void btn_lprnlayanan_batal_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = true;
            pnl_lprnlayanan_isi.Visible = false;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = true;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;

            dgv_lprnlayanan_tabellayanan.DataSource = null;
            dgv_lprnlayanan_tabellayanan.Rows.Clear();
            dgv_lprnlayanan_tabellayanan.Columns.Clear();
            lprnlayanan_DS.Tables.Clear();
        }
        #endregion

        #region (Laporan Gaji Excel)
        string lprngaji_tanggalcetakdari = "";
        string lprngaji_tanggalcetaksampai = "";
        DataSet lprngaji_DS = new DataSet();
        DataSet lprngaji_DSolahankomisi = new DataSet();
        XLWorkbook lprngaji_wb;
        private void dtp_lprngaji_tgldari_ValueChanged(object sender, EventArgs e)
        {
            #region(Select)
            string lprngaji_queryfinalcount = "";
            string lprngaji_queryfinalkomisi = "";


            #region(Select Query Komisi)
            //dgv_lprngaji_tabellayanan.DataSource = null;
            //dgv_lprngaji_tabellayanan.Rows.Clear();
            //dgv_lprngaji_tabellayanan.Columns.Clear();
            lprngaji_DS.Tables.Clear();
            lprngaji_DSolahankomisi.Tables.Clear();
            string lprngaji_query;
            MySqlConnection lprngaji_conn = new MySqlConnection(all_connStr);
            try
            {
                // RRyner   
                lprngaji_conn.Open();
                lprngaji_query = "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION  " +
                                    "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'Deluxe'" +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VVIP';";

                lprngaji_queryfinalkomisi = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

                MySqlCommand lprngaji_cmd = new MySqlCommand(lprngaji_query, lprngaji_conn);
                MySqlDataReader lprngaji_readr = lprngaji_cmd.ExecuteReader();

                lprngaji_tanggalcetakdari = dtp_lprngaji_tgldari.Value.Year.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Month.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Day.ToString();

                lprngaji_tanggalcetaksampai = dtp_lprngaji_tglsampai.Value.Year.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Month.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Day.ToString();

                if (lprngaji_readr.HasRows)
                {
                    while (lprngaji_readr.Read())
                    {
                        string extra = "Dasar";
                        if (lprngaji_readr.GetString(8) != "Tidak")
                        {
                            extra = "Extra";
                        }

                        int temp = lprngaji_readr.GetInt32(4);
                        string harganormal = temp.ToString("0,0");
                        string hargaextra = (temp + (temp * (edtvariabel_extra / 100))).ToString("0,0");

                        //lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)" +
                        //                " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                        //                "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                        //                "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                        //                "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + harga + ")',";

                        //lprngaji_queryfinal += "(SELECT COUNT(n1.id_nota)" +
                        //                " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                        //                "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                        //                "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                        //                "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7).Substring(0, 1) + "\n\n{" + extra.Substring(0, 1) + "}\n\n[" + lprngaji_readr.GetString(1).Substring(0, 1) + "]\n\n" + lprngaji_readr.GetString(2) + "\n(" + harga + ")',";

                        if (lprngaji_readr.GetString(7) == "Normal")
                        {
                            if (lprngaji_readr.GetString(8) == "Ya")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4) + (lprngaji_readr.GetInt32(4) * (lprngaji_readr.GetDouble(9) / 100))) * (lprngaji_readr.GetDouble(5) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + hargaextra + ")',";
                            }
                            else if (lprngaji_readr.GetString(8) == "Tidak")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4)) * (lprngaji_readr.GetDouble(5) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + harganormal + ")',";
                            }

                        }
                        else if (lprngaji_readr.GetString(7) == "Midnight")
                        {
                            if (lprngaji_readr.GetString(8) == "Ya")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4) + (lprngaji_readr.GetInt32(4) * (lprngaji_readr.GetDouble(9) / 100))) * (lprngaji_readr.GetDouble(6) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + hargaextra + ")',";
                            }
                            else if (lprngaji_readr.GetString(8) == "Tidak")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4)) * (lprngaji_readr.GetDouble(6) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + harganormal + ")',";
                            }
                        }

                    }
                    //MessageBox.Show(readr.FieldCount + "");
                }
                lprngaji_queryfinalkomisi = lprngaji_queryfinalkomisi.Substring(0, lprngaji_queryfinalkomisi.Length - 1);
                lprngaji_queryfinalkomisi += " FROM terapis t";
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprngaji_conn.Close();
            #endregion

            #region(Select Query Count)
            //dgv_lprngaji_tabellayanan.DataSource = null;
            //dgv_lprngaji_tabellayanan.Rows.Clear();
            //dgv_lprngaji_tabellayanan.Columns.Clear();
            //lprngaji_DS.Tables.Clear();
            string lprngaji_query2;
            MySqlConnection lprngaji_conn2 = new MySqlConnection(all_connStr);
            try
            {
                // RRyner   
                lprngaji_conn2.Open();
                lprngaji_query2 = "SELECT *, 'Normal', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION  " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe'" +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VVIP';";

                lprngaji_queryfinalcount = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

                MySqlCommand lprngaji_cmd2 = new MySqlCommand(lprngaji_query2, lprngaji_conn2);
                MySqlDataReader lprngaji_readr2 = lprngaji_cmd2.ExecuteReader();

                lprngaji_tanggalcetakdari = dtp_lprngaji_tgldari.Value.Year.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Month.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Day.ToString();

                lprngaji_tanggalcetaksampai = dtp_lprngaji_tglsampai.Value.Year.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Month.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Day.ToString();

                if (lprngaji_readr2.HasRows)
                {
                    while (lprngaji_readr2.Read())
                    {
                        string extra = "Dasar";
                        if (lprngaji_readr2.GetString(8) != "Tidak")
                        {
                            extra = "Extra";
                        }

                        string temp2 = lprngaji_readr2.GetString(4);
                        string harga2 = Convert.ToInt32(temp2).ToString(String.Format("0,0", temp2));

                        lprngaji_queryfinalcount += "(SELECT COUNT(n1.id_nota)" +
                                        " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr2.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr2.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr2.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr2.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr2.GetString(7) + " {" + extra + "} [" + lprngaji_readr2.GetString(1) + "] " + lprngaji_readr2.GetString(2) + " (" + harga2 + ")',";

                    }
                }
                lprngaji_queryfinalcount = lprngaji_queryfinalcount.Substring(0, lprngaji_queryfinalcount.Length - 1);
                lprngaji_queryfinalcount += " FROM terapis t";
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprngaji_conn2.Close();
            #endregion

            if (lprngaji_queryfinalcount != "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', FROM terapis t")
            {
                MySqlConnection lprngaji_conn4 = new MySqlConnection(all_connStr);
                try
                {
                    lprngaji_conn4.Open();

                    MySqlDataAdapter lprngaji_mySqlDataAdapter4 = new MySqlDataAdapter(lprngaji_queryfinalkomisi, lprngaji_conn4);
                    MySqlCommand lprngaji_cmd4 = new MySqlCommand(lprngaji_queryfinalcount, lprngaji_conn4);

                    lprngaji_mySqlDataAdapter4.Fill(lprngaji_DSolahankomisi);
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    MessageBox.Show("Error Occurred");
                }
                lprngaji_conn4.Close();

                #region(Isi Data)
                //lprngaji_DS.Tables.Clear();
                MySqlConnection lprngaji_conn3 = new MySqlConnection(all_connStr);
                try
                {
                    lprngaji_conn3.Open();

                    MySqlDataAdapter lprngaji_mySqlDataAdapter3 = new MySqlDataAdapter(lprngaji_queryfinalcount, lprngaji_conn3);
                    MySqlCommand lprngaji_cmd3 = new MySqlCommand(lprngaji_queryfinalcount, lprngaji_conn3);

                    lprngaji_mySqlDataAdapter3.Fill(lprngaji_DS);

                    //dgv_lprngaji_tabellayanan.DataSource = lprngaji_DS.Tables[0];
                    btn_lprngaji_excel.Enabled = true;
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    MessageBox.Show("Error Occurred");
                }
                lprngaji_conn3.Close();
                #endregion
            }
            #endregion
        }

        private void dtp_lprngaji_tglsampai_ValueChanged(object sender, EventArgs e)
        {
            #region(Select)
            string lprngaji_queryfinalcount = "";
            string lprngaji_queryfinalkomisi = "";


            #region(Select Query Komisi)
            //dgv_lprngaji_tabellayanan.DataSource = null;
            //dgv_lprngaji_tabellayanan.Rows.Clear();
            //dgv_lprngaji_tabellayanan.Columns.Clear();
            lprngaji_DS.Tables.Clear();
            lprngaji_DSolahankomisi.Tables.Clear();
            string lprngaji_query;
            MySqlConnection lprngaji_conn = new MySqlConnection(all_connStr);
            try
            {
                // RRyner   
                lprngaji_conn.Open();
                lprngaji_query = "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION  " +
                                    "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'Deluxe'" +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VVIP';";

                lprngaji_queryfinalkomisi = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

                MySqlCommand lprngaji_cmd = new MySqlCommand(lprngaji_query, lprngaji_conn);
                MySqlDataReader lprngaji_readr = lprngaji_cmd.ExecuteReader();

                lprngaji_tanggalcetakdari = dtp_lprngaji_tgldari.Value.Year.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Month.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Day.ToString();

                lprngaji_tanggalcetaksampai = dtp_lprngaji_tglsampai.Value.Year.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Month.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Day.ToString();

                if (lprngaji_readr.HasRows)
                {
                    while (lprngaji_readr.Read())
                    {
                        string extra = "Dasar";
                        if (lprngaji_readr.GetString(8) != "Tidak")
                        {
                            extra = "Extra";
                        }

                        int temp = lprngaji_readr.GetInt32(4);
                        string harganormal = temp.ToString("0,0");
                        string hargaextra = (temp + (temp * (edtvariabel_extra / 100))).ToString("0,0");

                        //lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)" +
                        //                " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                        //                "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                        //                "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                        //                "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + harga + ")',";

                        //lprngaji_queryfinal += "(SELECT COUNT(n1.id_nota)" +
                        //                " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                        //                "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                        //                "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                        //                "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7).Substring(0, 1) + "\n\n{" + extra.Substring(0, 1) + "}\n\n[" + lprngaji_readr.GetString(1).Substring(0, 1) + "]\n\n" + lprngaji_readr.GetString(2) + "\n(" + harga + ")',";

                        if (lprngaji_readr.GetString(7) == "Normal")
                        {
                            if (lprngaji_readr.GetString(8) == "Ya")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4) + (lprngaji_readr.GetInt32(4) * (lprngaji_readr.GetDouble(9) / 100))) * (lprngaji_readr.GetDouble(5) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + hargaextra + ")',";
                            }
                            else if (lprngaji_readr.GetString(8) == "Tidak")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4)) * (lprngaji_readr.GetDouble(5) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + harganormal + ")',";
                            }

                        }
                        else if (lprngaji_readr.GetString(7) == "Midnight")
                        {
                            if (lprngaji_readr.GetString(8) == "Ya")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4) + (lprngaji_readr.GetInt32(4) * (lprngaji_readr.GetDouble(9) / 100))) * (lprngaji_readr.GetDouble(6) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + hargaextra + ")',";
                            }
                            else if (lprngaji_readr.GetString(8) == "Tidak")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4)) * (lprngaji_readr.GetDouble(6) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + harganormal + ")',";
                            }
                        }

                    }
                    //MessageBox.Show(readr.FieldCount + "");
                }
                lprngaji_queryfinalkomisi = lprngaji_queryfinalkomisi.Substring(0, lprngaji_queryfinalkomisi.Length - 1);
                lprngaji_queryfinalkomisi += " FROM terapis t";
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprngaji_conn.Close();
            #endregion

            #region(Select Query Count)
            //dgv_lprngaji_tabellayanan.DataSource = null;
            //dgv_lprngaji_tabellayanan.Rows.Clear();
            //dgv_lprngaji_tabellayanan.Columns.Clear();
            //lprngaji_DS.Tables.Clear();
            string lprngaji_query2;
            MySqlConnection lprngaji_conn2 = new MySqlConnection(all_connStr);
            try
            {
                // RRyner   
                lprngaji_conn2.Open();
                lprngaji_query2 = "SELECT *, 'Normal', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION  " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe'" +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VVIP';";

                lprngaji_queryfinalcount = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

                MySqlCommand lprngaji_cmd2 = new MySqlCommand(lprngaji_query2, lprngaji_conn2);
                MySqlDataReader lprngaji_readr2 = lprngaji_cmd2.ExecuteReader();

                lprngaji_tanggalcetakdari = dtp_lprngaji_tgldari.Value.Year.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Month.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Day.ToString();

                lprngaji_tanggalcetaksampai = dtp_lprngaji_tglsampai.Value.Year.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Month.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Day.ToString();

                if (lprngaji_readr2.HasRows)
                {
                    while (lprngaji_readr2.Read())
                    {
                        string extra = "Dasar";
                        if (lprngaji_readr2.GetString(8) != "Tidak")
                        {
                            extra = "Extra";
                        }

                        string temp2 = lprngaji_readr2.GetString(4);
                        string harga2 = Convert.ToInt32(temp2).ToString(String.Format("0,0", temp2));

                        lprngaji_queryfinalcount += "(SELECT COUNT(n1.id_nota)" +
                                        " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr2.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr2.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr2.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr2.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr2.GetString(7) + " {" + extra + "} [" + lprngaji_readr2.GetString(1) + "] " + lprngaji_readr2.GetString(2) + " (" + harga2 + ")',";

                    }
                }
                lprngaji_queryfinalcount = lprngaji_queryfinalcount.Substring(0, lprngaji_queryfinalcount.Length - 1);
                lprngaji_queryfinalcount += " FROM terapis t";
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprngaji_conn2.Close();
            #endregion

            if (lprngaji_queryfinalcount != "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', FROM terapis t")
            {
                MySqlConnection lprngaji_conn4 = new MySqlConnection(all_connStr);
                try
                {
                    lprngaji_conn4.Open();

                    MySqlDataAdapter lprngaji_mySqlDataAdapter4 = new MySqlDataAdapter(lprngaji_queryfinalkomisi, lprngaji_conn4);
                    MySqlCommand lprngaji_cmd4 = new MySqlCommand(lprngaji_queryfinalcount, lprngaji_conn4);

                    lprngaji_mySqlDataAdapter4.Fill(lprngaji_DSolahankomisi);
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    MessageBox.Show("Error Occurred");
                }
                lprngaji_conn4.Close();

                #region(Isi Data)
                //lprngaji_DS.Tables.Clear();
                MySqlConnection lprngaji_conn3 = new MySqlConnection(all_connStr);
                try
                {
                    lprngaji_conn3.Open();

                    MySqlDataAdapter lprngaji_mySqlDataAdapter3 = new MySqlDataAdapter(lprngaji_queryfinalcount, lprngaji_conn3);
                    MySqlCommand lprngaji_cmd3 = new MySqlCommand(lprngaji_queryfinalcount, lprngaji_conn3);

                    lprngaji_mySqlDataAdapter3.Fill(lprngaji_DS);

                    //dgv_lprngaji_tabellayanan.DataSource = lprngaji_DS.Tables[0];
                    btn_lprngaji_excel.Enabled = true;
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    MessageBox.Show("Error Occurred");
                }
                lprngaji_conn3.Close();
                #endregion
            }
            #endregion
        }

        private void btn_lprngaji_excel_Click(object sender, EventArgs e)
        {
            #region(Slip Gaji)
            lprngaji_wb = new XLWorkbook();
            lprngaji_wb.Worksheets.Add("Slip Gaji");

            string folderPath = "C:\\Excel\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            List<string>[] listrowcetak_jenispaket = new List<string>[1];
            listrowcetak_jenispaket[0] = new List<string>();
            Array.Resize(ref listrowcetak_jenispaket, 2);
            List<Int32>[] listrowcetak_counterjasa = new List<Int32>[lprngaji_DSolahankomisi.Tables[0].Rows.Count];
            List<Double>[] listrowcetak_komisi = new List<Double>[lprngaji_DSolahankomisi.Tables[0].Rows.Count];
            for (int i = 0; i < listrowcetak_counterjasa.Length; i++)
            {
                listrowcetak_counterjasa[i] = new List<Int32>();
                listrowcetak_komisi[i] = new List<Double>();
            }
            for (int i = 3; i < lprngaji_DSolahankomisi.Tables[0].Columns.Count; i++)
            {
                if (lprngaji_DSolahankomisi.Tables[0].Columns[i].ColumnName.Substring(0, 6) == "Normal")
                {
                    if (i == 3)
                    {
                        listrowcetak_jenispaket[0].Add("Normal");
                        listrowcetak_jenispaket[0].Add(lprngaji_DSolahankomisi.Tables[0].Columns[i].ColumnName);
                    }
                    else if (lprngaji_DSolahankomisi.Tables[0].Columns[i + 1].ColumnName.Substring(0, 8) == "Midnight")
                    {
                        listrowcetak_jenispaket[0].Add(lprngaji_DSolahankomisi.Tables[0].Columns[i].ColumnName);
                        listrowcetak_jenispaket[0].Add("Midnight");
                    }
                    else
                    {
                        listrowcetak_jenispaket[0].Add(lprngaji_DSolahankomisi.Tables[0].Columns[i].ColumnName);
                    }
                }
                else if (lprngaji_DSolahankomisi.Tables[0].Columns[i].ColumnName.Substring(0, 8) == "Midnight")
                {
                    listrowcetak_jenispaket[0].Add(lprngaji_DSolahankomisi.Tables[0].Columns[i].ColumnName);
                }
            }

            for (int i = 0; i < listrowcetak_counterjasa.Length; i++)
            {
                for (int ii = 3; ii < lprngaji_DS.Tables[0].Columns.Count; ii++)
                {
                    listrowcetak_counterjasa[i].Add(int.Parse(lprngaji_DS.Tables[0].Rows[i][ii].ToString()));
                }
            }
            for (int i = 0; i < listrowcetak_counterjasa.Length; i++)
            {
                for (int ii = 0; ii < listrowcetak_jenispaket[0].Count; ii++)
                {
                    if (listrowcetak_jenispaket[0][ii] == "Normal")
                    {
                        listrowcetak_counterjasa[i].Insert(ii, 0);
                    }
                    else if (listrowcetak_jenispaket[0][ii] == "Midnight")
                    {
                        listrowcetak_counterjasa[i].Insert(ii, 0);
                    }
                }
            }

            for (int i = 0; i < listrowcetak_komisi.Length; i++)
            {
                for (int ii = 3; ii < lprngaji_DS.Tables[0].Columns.Count; ii++)
                {
                    listrowcetak_komisi[i].Add(lprngaji_DSolahankomisi.Tables[0].Rows[i][ii].CastTo<Double>());
                }
            }
            for (int i = 0; i < listrowcetak_komisi.Length; i++)
            {
                for (int ii = 0; ii < listrowcetak_jenispaket[0].Count; ii++)
                {
                    if (listrowcetak_jenispaket[0][ii] == "Normal")
                    {
                        listrowcetak_komisi[i].Insert(ii, 0);
                    }
                    else if (listrowcetak_jenispaket[0][ii] == "Midnight")
                    {
                        listrowcetak_komisi[i].Insert(ii, 0);
                    }
                }
            }

            lprngaji_wb.Worksheet("Slip Gaji").PageSetup.Margins.Top = 1;
            lprngaji_wb.Worksheet("Slip Gaji").PageSetup.Margins.Bottom = 1;
            lprngaji_wb.Worksheet("Slip Gaji").PageSetup.Margins.Left = 0.6;
            lprngaji_wb.Worksheet("Slip Gaji").PageSetup.Margins.Right = 0.6;
            lprngaji_wb.Worksheet("Slip Gaji").PageSetup.Margins.Footer = 0.8;
            lprngaji_wb.Worksheet("Slip Gaji").PageSetup.Margins.Header = 0.8;
            lprngaji_wb.Worksheet("Slip Gaji").PageSetup.CenterHorizontally = true;
            int pageoffset = 0;
            int pagecount = 1;
            pagecount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(listrowcetak_jenispaket[0].Count) / 30));
            if (listrowcetak_jenispaket[0].Count - (30 * (pagecount - 1)) > 22)
            {
                pagecount++;
            }
            if (pagecount == 1)
            {
                for (int i = 0; i < listrowcetak_komisi.Length; i++)
                {
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Value = "SLIP GAJI";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Style.Font.FontSize = 24;
                    lprngaji_wb.Worksheet("Slip Gaji").Range(1 + pageoffset, 1, 1 + pageoffset, 14).Merge();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    lprngaji_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 2).Value = "BULAN";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 2).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 3).Value = dtp_lprngaji_tgldari.Value.ToLongDateString() + " - " + dtp_lprngaji_tglsampai.Value.ToLongDateString();

                    lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 2).Value = "NO ID";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 2).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 3).Value = lprngaji_DS.Tables[0].Rows[i][0];
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 2).Value = "NAMA";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 2).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 3).Value = lprngaji_DS.Tables[0].Rows[i][1];
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 2).Value = "POSISI";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 2).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 3).Value = lprngaji_DS.Tables[0].Rows[i][2];

                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Value = "Jenis Paket";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, 9 + pageoffset, 11).Merge();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Value = "Jumlah";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Value = "Komisi";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 2).InsertData(listrowcetak_jenispaket[0]);
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 12).InsertData(listrowcetak_counterjasa[i]);
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 13).InsertData(listrowcetak_komisi[i]);
                    //lprngaji_wb.Worksheet("Slip Gaji").Column(2).AdjustToContents();
                    for (int ii = 0; ii < listrowcetak_jenispaket[0].Count; ii++)
                    {
                        if (listrowcetak_jenispaket[0][ii] == "Normal")
                        {
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Value = "";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 12).Value = "";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 2).Style.Font.SetBold();
                            lprngaji_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
                        }
                        else if (listrowcetak_jenispaket[0][ii] == "Midnight")
                        {
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Value = "";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 12).Value = "";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 2).Style.Font.SetBold();
                            lprngaji_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
                        }
                        else
                        {
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
                        }
                    }
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 2).Value = "Total";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 2).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 12).Merge();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).FormulaA1 = "SUM(M" + (10 + pageoffset).ToString() + ":M" + (listrowcetak_jenispaket[0].Count + 9 + pageoffset).ToString() + ")";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorderColor(XLColor.Black);
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 11).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 11).Style.Border.SetOutsideBorderColor(XLColor.Black);
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 12).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 12).Style.Border.SetOutsideBorderColor(XLColor.Black);
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorderColor(XLColor.Black);

                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 2).Value = "+ Bonus Target";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 2).Value = "+ Bonus Tahunan";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 2).Value = "+ Bonus Lain-lain";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 15 + pageoffset, 2).Value = "- Potongan Biaya Masuk";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 15 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 15 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 16 + pageoffset, 2).Value = "- Potongan Absensi";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 16 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 16 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 17 + pageoffset, 2).Value = "- Potong Tabungan";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 17 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 17 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 18 + pageoffset, 2).Value = "- Potong Hutang";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 18 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 18 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 2).Value = "Gaji Total";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 2).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 5).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 6).FormulaA1 = "M" + (listrowcetak_jenispaket[0].Count + 10 + pageoffset).ToString() + "+SUM(F" + (listrowcetak_jenispaket[0].Count + 12 + pageoffset).ToString() + ":F" + (listrowcetak_jenispaket[0].Count + 14 + pageoffset).ToString() +
                                                                                                                ")-SUM(F" + (listrowcetak_jenispaket[0].Count + 15 + pageoffset).ToString() + ":F" + (listrowcetak_jenispaket[0].Count + 18 + pageoffset).ToString() + ")";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 6).Style.Font.SetBold();

                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 8).Value = "Sisa Hutang";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 11).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 8).Value = "Total Tabungan";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 11).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 8).Value = "Sisa Biaya Masuk Kembali";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 11).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";

                    lprngaji_wb.Worksheet("Slip Gaji").Cell(43 + pageoffset, 13).Value = "Hal. " + 1.ToString();
                    lprngaji_wb.Worksheet("Slip Gaji").Range(43 + pageoffset, 13, 43 + pageoffset, 14).Merge();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(43 + pageoffset, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    pageoffset += 43;
                }
            }
            else if (pagecount > 1)
            {
                Array.Resize(ref listrowcetak_jenispaket, pagecount);
                for (int i = 1; i < pagecount; i++)
                {
                    listrowcetak_jenispaket[i] = new List<string>();
                }
                List<int>[,] listrowcetak_counterjasahal2dst = new List<int>[lprngaji_DSolahankomisi.Tables[0].Rows.Count, pagecount];
                List<double>[,] listrowcetak_komisihal2dst = new List<double>[lprngaji_DSolahankomisi.Tables[0].Rows.Count, pagecount];
                for (int x = 0; x < lprngaji_DSolahankomisi.Tables[0].Rows.Count; x++)
                {
                    for (int y = 0; y < pagecount; y++)
                    {
                        listrowcetak_counterjasahal2dst[x, y] = new List<int>();
                        listrowcetak_komisihal2dst[x, y] = new List<double>();
                    }
                }
                for (int x = 0; x < lprngaji_DSolahankomisi.Tables[0].Rows.Count; x++)
                {
                    listrowcetak_counterjasahal2dst[x, 0] = listrowcetak_counterjasa[x];
                    listrowcetak_komisihal2dst[x, 0] = listrowcetak_komisi[x];
                }
                int normalextracont = 0;
                int midnightextracount = 0;
                int countfortakeitem = 0;
                for (int row = 0; row < listrowcetak_jenispaket.Length; row++)
                {
                    midnightextracount = 0;
                    normalextracont = 0;
                    if (listrowcetak_jenispaket[row].Count > 30)
                    {
                        countfortakeitem = (listrowcetak_jenispaket[row].Count - 2) / 4 - 7;
                        for (int i = 0; i < listrowcetak_jenispaket[row].Count; i++)
                        {
                            if (row + 1 <= listrowcetak_jenispaket.Length)
                            {
                                if (listrowcetak_jenispaket[row][i] == "Normal")
                                {
                                    listrowcetak_jenispaket[row + 1].Add("Normal");
                                    for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
                                    {
                                        listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(0);
                                        listrowcetak_komisihal2dst[idxterapis, row + 1].Add(0);
                                    }
                                }
                                else if (i > 1)
                                {
                                    if (listrowcetak_jenispaket[row][i].Contains("Normal {Extra}") && normalextracont < 1)
                                    {
                                        for (int idx = 1; idx <= countfortakeitem; idx++)
                                        {
                                            listrowcetak_jenispaket[row + 1].Add(listrowcetak_jenispaket[row][i - idx]);
                                            listrowcetak_jenispaket[row].RemoveAt(i - idx);
                                        }
                                        for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
                                        {
                                            for (int idx = 1; idx <= countfortakeitem; idx++)
                                            {
                                                listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(listrowcetak_counterjasahal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_counterjasahal2dst[idxterapis, row].RemoveAt(i - idx);
                                                listrowcetak_komisihal2dst[idxterapis, row + 1].Add(listrowcetak_komisihal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_komisihal2dst[idxterapis, row].RemoveAt(i - idx);
                                            }
                                        }
                                        normalextracont++;
                                    }
                                    else if (listrowcetak_jenispaket[row][i] == "Midnight")
                                    {
                                        for (int idx = 1; idx <= countfortakeitem; idx++)
                                        {
                                            listrowcetak_jenispaket[row + 1].Add(listrowcetak_jenispaket[row][i - idx]);
                                            listrowcetak_jenispaket[row].RemoveAt(i - idx);
                                        }
                                        for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
                                        {
                                            for (int idx = 1; idx <= countfortakeitem; idx++)
                                            {
                                                listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(listrowcetak_counterjasahal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_counterjasahal2dst[idxterapis, row].RemoveAt(i - idx);
                                                listrowcetak_komisihal2dst[idxterapis, row + 1].Add(listrowcetak_komisihal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_komisihal2dst[idxterapis, row].RemoveAt(i - idx);
                                            }
                                        }
                                        listrowcetak_jenispaket[row + 1].Add("Midnight");
                                        for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
                                        {
                                            listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(0);
                                            listrowcetak_komisihal2dst[idxterapis, row + 1].Add(0);
                                        }
                                    }
                                    else if (listrowcetak_jenispaket[row][i].Contains("Midnight {Extra}") && midnightextracount < 1)
                                    {
                                        for (int idx = 1; idx <= countfortakeitem; idx++)
                                        {
                                            listrowcetak_jenispaket[row + 1].Add(listrowcetak_jenispaket[row][i - idx]);
                                            listrowcetak_jenispaket[row].RemoveAt(i - idx);
                                        }
                                        for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
                                        {
                                            for (int idx = 1; idx <= countfortakeitem; idx++)
                                            {
                                                listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(listrowcetak_counterjasahal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_counterjasahal2dst[idxterapis, row].RemoveAt(i - idx);
                                                listrowcetak_komisihal2dst[idxterapis, row + 1].Add(listrowcetak_komisihal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_komisihal2dst[idxterapis, row].RemoveAt(i - idx);
                                            }
                                        }
                                        midnightextracount++;
                                    }
                                    else if (i == listrowcetak_jenispaket[row].Count - 1)
                                    {
                                        for (int idx = 0; idx < countfortakeitem; idx++)
                                        {
                                            listrowcetak_jenispaket[row + 1].Add(listrowcetak_jenispaket[row][i - idx]);
                                            listrowcetak_jenispaket[row].RemoveAt(i - idx);
                                        }
                                        for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
                                        {
                                            for (int idx = 1; idx <= countfortakeitem; idx++)
                                            {
                                                listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(listrowcetak_counterjasahal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_counterjasahal2dst[idxterapis, row].RemoveAt(i - idx);
                                                listrowcetak_komisihal2dst[idxterapis, row + 1].Add(listrowcetak_komisihal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_komisihal2dst[idxterapis, row].RemoveAt(i - idx);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                for (int i = 0; i < listrowcetak_komisi.Length; i++)
                {
                    string formulatotalkomisi = "";
                    for (int p = 1; p <= pagecount; p++)
                    {
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Value = "SLIP GAJI";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Style.Font.FontSize = 24;
                        lprngaji_wb.Worksheet("Slip Gaji").Range(1 + pageoffset, 1, 1 + pageoffset, 14).Merge();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        lprngaji_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 2).Value = "BULAN";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 2).Style.Font.SetBold();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 3).Value = dtp_lprngaji_tgldari.Value.ToLongDateString() + " - " + dtp_lprngaji_tglsampai.Value.ToLongDateString();

                        lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 2).Value = "NO ID";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 2).Style.Font.SetBold();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 3).Value = lprngaji_DS.Tables[0].Rows[i][0];
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 2).Value = "NAMA";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 2).Style.Font.SetBold();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 3).Value = lprngaji_DS.Tables[0].Rows[i][1];
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 2).Value = "POSISI";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 2).Style.Font.SetBold();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 3).Value = lprngaji_DS.Tables[0].Rows[i][2];

                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Value = "Jenis Paket";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Style.Font.SetBold();
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, 9 + pageoffset, 11).Merge();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Value = "Jumlah";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Style.Font.SetBold();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Value = "Komisi";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Style.Font.SetBold();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                        lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 2).InsertData(listrowcetak_jenispaket[p - 1]);
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 12).InsertData(listrowcetak_counterjasahal2dst[i, p - 1]);
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 13).InsertData(listrowcetak_komisihal2dst[i, p - 1]);
                        //lprngaji_wb.Worksheet("Slip Gaji").Column(2).AdjustToContents();
                        for (int ii = 0; ii < listrowcetak_jenispaket[p - 1].Count; ii++)
                        {
                            if (listrowcetak_jenispaket[p - 1][ii] == "Normal")
                            {
                                lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Value = "";
                                lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 12).Value = "";
                                lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 2).Style.Font.SetBold();
                                lprngaji_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
                            }
                            else if (listrowcetak_jenispaket[p - 1][ii] == "Midnight")
                            {
                                lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Value = "";
                                lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 12).Value = "";
                                lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 2).Style.Font.SetBold();
                                lprngaji_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
                            }
                            else
                            {
                                lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
                                lprngaji_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
                            }
                        }
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 13).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 13).Style.Border.SetOutsideBorderColor(XLColor.Black);
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 11).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 11).Style.Border.SetOutsideBorderColor(XLColor.Black);
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 12).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 12).Style.Border.SetOutsideBorderColor(XLColor.Black);
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 13).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 13).Style.Border.SetOutsideBorderColor(XLColor.Black);

                        if (listrowcetak_jenispaket[p - 1].Count <= 0)
                        {
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Value = "Total";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Style.Font.SetBold();
                            lprngaji_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 12).Merge();
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).FormulaA1 = "0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                            lprngaji_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                        }
                        else if (listrowcetak_jenispaket[p - 1].Count > 0)
                        {
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Value = "Total";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Style.Font.SetBold();
                            lprngaji_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 12).Merge();
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).FormulaA1 = "SUM(M" + (10 + pageoffset).ToString() + ":M" + (listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset).ToString() + ")";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                            lprngaji_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);

                        }
                        formulatotalkomisi += "M" + (listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset).ToString() + "+";

                        if (listrowcetak_jenispaket[p - 1].Count <= 22)
                        {
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 2).Value = "+ Bonus Target";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 2).Value = "+ Bonus Tahunan";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 2).Value = "+ Bonus Lain-lain";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 15 + pageoffset, 2).Value = "- Potongan Biaya Masuk";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 15 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 15 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 16 + pageoffset, 2).Value = "- Potongan Absensi";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 16 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 16 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 17 + pageoffset, 2).Value = "- Potong Tabungan";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 17 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 17 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 18 + pageoffset, 2).Value = "- Potong Hutang";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 18 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 18 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 2).Value = "Gaji Total";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 2).Style.Font.SetBold();
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 5).Style.Font.SetBold();
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 6).FormulaA1 = formulatotalkomisi + "SUM(F" + (listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset).ToString() + ":F" + (listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset).ToString() +
                                                                                                                        ")-SUM(F" + (listrowcetak_jenispaket[p - 1].Count + 15 + pageoffset).ToString() + ":F" + (listrowcetak_jenispaket[p - 1].Count + 18 + pageoffset).ToString() + ")";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 6).Style.Font.SetBold();

                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 8).Value = "Sisa Hutang";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 11).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 8).Value = "Total Tabungan";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 11).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 8).Value = "Sisa Biaya Masuk Kembali";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 11).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";

                        }

                        lprngaji_wb.Worksheet("Slip Gaji").Cell(43 + pageoffset, 13).Value = "Hal. " + p.ToString();
                        lprngaji_wb.Worksheet("Slip Gaji").Range(43 + pageoffset, 13, 43 + pageoffset, 14).Merge();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(43 + pageoffset, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        pageoffset += 43;

                    }
                }
            }



            lprngaji_wb.Worksheet("Slip Gaji").Column("A").Width = 2.29;
            lprngaji_wb.Worksheet("Slip Gaji").Column("C").Width = 4.43;
            lprngaji_wb.Worksheet("Slip Gaji").Column("E").Width = 0.58;
            lprngaji_wb.Worksheet("Slip Gaji").Column("F").Width = 10.43;
            lprngaji_wb.Worksheet("Slip Gaji").Column("G").Width = 2.71;
            lprngaji_wb.Worksheet("Slip Gaji").Column("I").Width = 5.29;
            lprngaji_wb.Worksheet("Slip Gaji").Column("K").Width = 0.58;
            lprngaji_wb.Worksheet("Slip Gaji").Column("L").Width = 10.43;
            lprngaji_wb.Worksheet("Slip Gaji").Column("M").Width = 10.43;
            lprngaji_wb.Worksheet("Slip Gaji").Column("N").Width = 0.67;


            lprngaji_wb.SaveAs(folderPath + "Slip Gaji (" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour + ";" + DateTime.Now.Minute.ToString() + ";" + DateTime.Now.Second.ToString() + ").xlsx");
            MessageBox.Show("File Excel telah disimpan");
            btn_lprngaji_excel.Enabled = false;
            //}
            #endregion
        }

        private void btn_lprngaji_batal_Click(object sender, EventArgs e)
        {
            pnl_menu_isi.Visible = true;
            pnl_lprngaji_isi.Visible = false;

            pnl_login_isi.Enabled = false;
            pnl_edttrps_isi.Enabled = false;
            pnl_edtpkt_isi.Enabled = false;
            pnl_ctknota_isi.Enabled = false;
            pnl_lprnlayanan_isi.Enabled = false;
            pnl_lprnpnjln_isi.Enabled = false;
            pnl_menu_isi.Enabled = true;
            pnl_tbhpkt_isi.Enabled = false;
            pnl_tbhtrps_isi.Enabled = false;
            pnl_variabel_isi.Enabled = false;
            pnl_lprngaji_isi.Enabled = false;

        }
        #endregion

        public void CreateReceipt(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            printdoc_ctknota_printdokumen.OriginAtMargins = true;
            
            Graphics graphic = e.Graphics;

            Font font = new Font("Consolas", 7); //must use a mono spaced font as the spaces need to line up

            float fontHeight = font.GetHeight();

            int startX = 0;
            int startY = 10;
            int offset = 90;

            Point ulCorner = new Point(45, 0);
            graphic.DrawImage(ctknota_logo, ulCorner);
            
            graphic.DrawString("---------------------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight; //make the spacing consistent
            
            string top = (DateTime.Now.Day.ToString("00") + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Year.ToString() + " "
                            + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00")).PadRight(16) + ("" + ctknota_lstcetak[0] + "/" + ctknota_lstcetak[1] + "/" + ctknota_lstcetak[2] + "/" + ctknota_lstcetak[3] + "").PadLeft(29);
            graphic.DrawString(top, font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight; //make the spacing consistent

            graphic.DrawString("---------------------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight; //make the spacing consistent

            if (ctknota_lstcetak[6] == "Ya")
            {
                if (ctknota_lstcetak[5] == "Credit")
                {
                    graphic.DrawString(("ROOM : " + ctknota_lstcetak[4] + "").PadRight(15) + ("   " + ctknota_lstcetak[5].ToUpper() + "").PadRight(15) + ("Tamu Hotel: " + ctknota_lstcetak[6] + "").PadLeft(15), font, new SolidBrush(Color.Black), startX, startY + offset);
                    offset = offset + (int)fontHeight + 10; //make the spacing consistent 
                }
                else if (ctknota_lstcetak[5] == "Cash")
                {
                    graphic.DrawString(("ROOM : " + ctknota_lstcetak[4] + "").PadRight(15) + ("    " + ctknota_lstcetak[5].ToUpper() + "").PadRight(15) + ("Tamu Hotel: " + ctknota_lstcetak[6] + "").PadLeft(15), font, new SolidBrush(Color.Black), startX, startY + offset);
                    offset = offset + (int)fontHeight + 10; //make the spacing consistent
                }
                
            }
            else
            {
                graphic.DrawString(("ROOM : " + ctknota_lstcetak[4] + "").PadRight(15) + ("     " + ctknota_lstcetak[5].ToUpper() + "").PadRight(15) + ("Tamu Hotel: -").PadLeft(15), font, new SolidBrush(Color.Black), startX, startY + offset);
                offset = offset + (int)fontHeight + 10; //make the spacing consistent
            }

            string hargapaket = String.Format("{0:n0}", int.Parse(ctknota_lstcetak[8]));
            graphic.DrawString(("1 " + ctknota_lstcetak[7].ToUpper() + "").PadRight(27) + hargapaket.PadLeft(18), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight+5; //make the spacing consistent

            string nominalextra = String.Format("{0:n0}", int.Parse(ctknota_lstcetak[10]));
            string tips = String.Format("{0:n0}", int.Parse(ctknota_lstcetak[11]));
            if (ctknota_lstcetak[9] == "Ya")
            {
                graphic.DrawString("  - EXTRA SESSION".PadRight(27) + nominalextra.PadLeft(18), font, new SolidBrush(Color.Black), startX, startY + offset);
                offset = offset + (int)fontHeight; //make the spacing consistent
            }
            if (ctknota_lstcetak[5] == "Credit")
            {
                graphic.DrawString("  - TIPS".PadRight(27) + tips.PadLeft(18), font, new SolidBrush(Color.Black), startX, startY + offset);
                offset = offset + (int)fontHeight + 5; //make the spacing consistent
            }

            graphic.DrawString("---------------".PadLeft(45), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight+5; //make the spacing consistent

            string subtotal = String.Format("{0:n0}", int.Parse(ctknota_lstcetak[12]));
            string potonganhotel = String.Format("{0:n0}", int.Parse(ctknota_lstcetak[13]));
            string diskon = String.Format("{0:n0}", int.Parse(ctknota_lstcetak[14]));
            graphic.DrawString("                   Sub Total :".PadRight(30) + subtotal.PadLeft(15), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight; //make the spacing consistent
            graphic.DrawString("              Potongan Hotel :".PadRight(30) + potonganhotel.PadLeft(15), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight; //make the spacing consistent
            graphic.DrawString("                      Diskon :".PadRight(30) + diskon.PadLeft(15), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight+10; //make the spacing consistent

            string grandtotal = String.Format("{0:c}", int.Parse(ctknota_lstcetak[15]));
            grandtotal =  grandtotal.Insert(2, ". ");
            grandtotal =  grandtotal.Insert(grandtotal.Length, ",-");
            graphic.DrawString("Grand Total ".PadRight(12, '#') + grandtotal.PadLeft(17), new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + 30; //make some room so that the total stands out.

            graphic.DrawString("                TERIMA KASIH", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + 15;

            graphic.DrawString("           SELAMAT DATANG KEMBALI", font, new SolidBrush(Color.Black), startX, startY + offset);

            #region(Comment)
            //int total = 0;
            //float cash = 0 ;
            //float change = 0.00f;

            //this prints the reciept

            //graphic.DrawString("  Green Leaf Spa", new Font("Courier New", 15), new SolidBrush(Color.Black), startX, startY);
            //Graha Simatupang
            //Tower 2 Unit C, 7th - 11th Floor
            //Jalan Letjen T.B. Simatupang
            //Jakarta - 12540

            //string nama = ;

            //graphic.DrawString("No. Ruangan : 1".PadRight(25)+"No. Terapis : 50".PadLeft(20), font, new SolidBrush(Color.Black), startX, startY + offset);
            //offset = offset + (int)fontHeight; //make the spacing consistent

            //graphic.DrawString("TAMU HOTEL", font, new SolidBrush(Color.Black), startX, startY + offset);
            //offset = offset + (int)fontHeight; //make the spacing consistent

            //float totalprice = 0.00f;

            //graphic.DrawString("1 TRADITIONAL MASSAGE".PadRight(27) + String.Format("{0:c}", 500000).PadLeft(18), font, new SolidBrush(Color.Black), startX, startY + offset);
            //offset = offset + (int)fontHeight; //make the spacing consistent

            //graphic.DrawString("1 FULL BODY MASSAGE".PadRight(27) + String.Format("{0:c}", 500000).PadLeft(18), font, new SolidBrush(Color.Black), startX, startY + offset);
            //offset = offset + (int)fontHeight; //make the spacing consistent

            //graphic.DrawString(productLine, new Font("Courier New", 12, FontStyle.Italic), new SolidBrush(Color.Red), startX, startY + offset);

            //offset = offset + (int)fontHeight + 5; //make the spacing consistent
            //foreach (string item in listBox1.Items)
            //{
            //    //create the string to print on the reciept
            //    string productDescription = item;
            //    string productTotal = item.Substring(item.Length - 6, 6);
            //    float productPrice = float.Parse(item.Substring(item.Length - 5, 5));

            //    //MessageBox.Show(item.Substring(item.Length - 5, 5) + "PROD TOTAL: " + productTotal);


            //    totalprice += productPrice;

            //    if (productDescription.Contains("  -"))
            //    {
            //        string productLine = productDescription.Substring(0, 24);

            //        graphic.DrawString(productLine, new Font("Courier New", 12, FontStyle.Italic), new SolidBrush(Color.Red), startX, startY + offset);

            //        offset = offset + (int)fontHeight + 5; //make the spacing consistent
            //    }
            //    else
            //    {
            //        string productLine = productDescription;

            //        graphic.DrawString(productLine, font, new SolidBrush(Color.Black), startX, startY + offset);

            //        offset = offset + (int)fontHeight + 5; //make the spacing consistent
            //    }

            //}

            //change = (cash - totalprice);

            //when we have drawn all of the items add the total

            //offset = offset; //make some room so that the total stands out.
            //graphic.DrawString("          Jenis Pembayaran :".PadRight(28) + "Cash".PadLeft(17), font, new SolidBrush(Color.Black), startX, startY + offset);
            //offset = offset + (int)fontHeight; //make the spacing consistent

            //graphic.DrawString("CASH ".PadRight(30) + String.Format("{0:c}", cash), font, new SolidBrush(Color.Black), startX, startY + offset);
            //offset = offset + 15;
            //graphic.DrawString("CHANGE ".PadRight(30) + String.Format("{0:c}", change), font, new SolidBrush(Color.Black), startX, startY + offset);
            //offset = offset + 30; //make some room so that the total stands out.
            #endregion
        }

        #region(Validasi input)
        private void txt_ctknota_nomorruangan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_tbhpkt_namapaket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_edtpkt_namapaket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_tbhtrps_namaterapis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_tbhuser_nama_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_edttrps_namaterapis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        



        















    }
}