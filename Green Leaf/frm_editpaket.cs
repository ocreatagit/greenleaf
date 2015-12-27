using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Green_Leaf
{
    public partial class frm_editpaket : Form
    {
        public frm_editpaket()
        {
            InitializeComponent();
        }

        List<string>[] edtpkt_lstnamapaket = new List<string>[2];
        List<int> edtpkt_lstidpktTerpilih = new List<int>();
        int edtpkt_idTerpilih = new int();
        string namapakettersimpan;

        private void frm_editpaket_Load(object sender, EventArgs e)
        {
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
                        
                        if (Char.IsDigit(edtpkt_rdr.GetString(3),1)==true)
                        {
                            edtpkt_jam = edtpkt_rdr.GetString(3).Substring(0, 2);
                            if (Char.IsDigit(edtpkt_rdr.GetString(3),8)==false)
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
                if (namapakettersimpan.Replace(" ", String.Empty).ToUpper() == cbo_edtpkt_jenispaket.SelectedItem.ToString().ToUpper()+"-"+txt_edtpkt_namapaket.Text.Replace(" ", String.Empty).ToUpper())
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

            //cbo_edtpkt_jenispaket.Enabled = false;
            //    cbo_edtpkt_jenispaket.SelectedItem = null;
            //    txt_edtpkt_namapaket.Enabled = false;
            //    txt_edtpkt_namapaket.Clear();
            //    //txt_edtpkt_durasipaket.Enabled = false;
            //    //txt_edtpkt_durasipaket.Clear();
            //    txt_edtpkt_hargapaket.Enabled = false;
            //    txt_edtpkt_hargapaket.Clear();
            //    //rdo_edtpkt_jamnormal.Enabled = false;
            //    //rdo_edtpkt_jamnormal.Checked = false;
            //    //rdo_edtpkt_jammidnight.Enabled = false;
            //    //rdo_edtpkt_jammidnight.Checked = false;
            //    //txt_edtpkt_komisipaket.Enabled = false;
            //    //txt_edtpkt_komisipaket.Clear();
            //    btn_edtpkt_simpan.Enabled = false;
            //    lsb_edtpkt_jenisnamapkt.Items.Clear();

            //cbo_kodeterapis.Items.Clear();
            //lsb_edtpkt_kodeterapis.Items.Clear();
            //#region(Select)
            //edtpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            //edtpkt_conn = new MySqlConnection(edtpkt_connStr);
            //try
            //{
            //    edtpkt_conn.Open();

            //    edtpkt_query = "SELECT * FROM `terapis` order by id_terapis DESC";
            //    MySqlCommand edtpkt_cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
            //    MySqlDataReader edtpkt_rdr = edtpkt_cmd.ExecuteReader();

            //    while (edtpkt_rdr.Read())
            //    {
            //        //cbo_kodeterapis.Items.Add(rdr.GetString(1));
            //        lsb_edtpkt_kodeterapis.Items.Add(edtpkt_rdr.GetString(1));
            //    }
            //    edtpkt_rdr.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
            //edtpkt_conn.Close();
            //#endregion

            //#region(Isi listbox dengan Jenis dan Nama Paket per baris)
            //string edtpkt_jenisnamapkt;
            //edtpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            //edtpkt_conn = new MySqlConnection(edtpkt_connStr);
            //try
            //{
            //    edtpkt_conn.Open();

            //    edtpkt_query = "SELECT * FROM `paket` ORDER BY `id_paket` DESC";
            //    MySqlCommand edtpkt_cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
            //    MySqlDataReader edtpkt_rdr = edtpkt_cmd.ExecuteReader();

            //    while (edtpkt_rdr.Read())
            //    {
            //        //cbo_kodeterapis.Items.Add(edtpkt_rdr.GetString(1));
            //        edtpkt_lstidpktTerpilih.Add(edtpkt_rdr.GetInt16(0));
            //        edtpkt_jenisnamapkt = edtpkt_rdr.GetString(1);
            //        edtpkt_jenisnamapkt += " - " + edtpkt_rdr.GetString(2);
            //        lsb_edtpkt_jenisnamapkt.Items.Add(edtpkt_jenisnamapkt);
            //    }
            //    edtpkt_rdr.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
            //edtpkt_conn.Close();
            //#endregion
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
    }
}
