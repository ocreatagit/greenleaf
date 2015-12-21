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

        List<int> edtpkt_lstidpktTerpilih = new List<int>();
        int edtpkt_idTerpilih = new int();

        private void frm_editpaket_Load(object sender, EventArgs e)
        {
            cbo_edtpkt_jenispaket.Enabled = false;
            txt_edtpkt_namapaket.Enabled = false;
            txt_edtpkt_durasipaket.Enabled = false;
            txt_edtpkt_hargapaket.Enabled = false;
            rdo_edtpkt_jamnormal.Enabled = false;
            rdo_edtpkt_jammidnight.Enabled = false;
            txt_edtpkt_komisipaket.Enabled = false;
            btn_edtpkt_simpan.Enabled = false;
            lsb_edtpkt_jenisnamapkt.Items.Clear();

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
                }
                edtpkt_rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            edtpkt_conn.Close();
            #endregion
        }

        private void btn_edtpkt_ok_Click(object sender, EventArgs e)
        {
            

            if (lsb_edtpkt_jenisnamapkt.SelectedItem != null)
            {
                string edtpkt_query;
                edtpkt_idTerpilih = edtpkt_lstidpktTerpilih[lsb_edtpkt_jenisnamapkt.SelectedIndex];
                #region(Select)
                string edtpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                MySqlConnection edtpkt_conn = new MySqlConnection(edtpkt_connStr);
                List<string> edtpkt_lstKode = new List<string>();
                try
                {
                    edtpkt_conn.Open();

                    edtpkt_query = "SELECT * FROM `paket` WHERE `id_paket` = "+edtpkt_lstidpktTerpilih[lsb_edtpkt_jenisnamapkt.SelectedIndex]+"";
                    MySqlCommand edtpkt_cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                    MySqlDataReader edtpkt_rdr = edtpkt_cmd.ExecuteReader();

                    while (edtpkt_rdr.Read())
                    {
                        cbo_edtpkt_jenispaket.SelectedItem = edtpkt_rdr.GetString(1);
                        txt_edtpkt_namapaket.Text = edtpkt_rdr.GetString(2);
                        txt_edtpkt_durasipaket.Text = edtpkt_rdr.GetString(3);
                        txt_edtpkt_hargapaket.Text = edtpkt_rdr.GetString(4);
                        txt_edtpkt_komisipaket.Text = edtpkt_rdr.GetString(6);
                        if (edtpkt_rdr.GetString(5) == "Normal")
                        {
                            rdo_edtpkt_jamnormal.Checked = true;
                        }
                        else if (edtpkt_rdr.GetString(5) == "Midnight")
                        {
                            rdo_edtpkt_jammidnight.Checked = true;
                        }
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
                txt_edtpkt_durasipaket.Enabled = true;
                txt_edtpkt_hargapaket.Enabled = true;
                rdo_edtpkt_jamnormal.Enabled = true;
                rdo_edtpkt_jammidnight.Enabled = true;
                txt_edtpkt_komisipaket.Enabled = true;
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
            else if (txt_edtpkt_durasipaket.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Durasi Paket terlebih dahulu!");
            }
            else if (txt_edtpkt_hargapaket.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Harga Paket terlebih dahulu!");
            }
            else if (txt_edtpkt_komisipaket.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Komisi Paket terlebih dahulu!");
            }
            else
            {
                #region(Buat huruf besar untuk Jam dan Menit)
                string edtpkt_durasiPaket = txt_edtpkt_durasipaket.Text;
                var regex = new Regex(@"\b[A-Z]", RegexOptions.IgnoreCase);
                edtpkt_durasiPaket = regex.Replace(edtpkt_durasiPaket, m => m.ToString().ToUpper());
                #endregion
                if (rdo_edtpkt_jamnormal.Checked)
                {
                    #region(Update)
                    edtpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    edtpkt_conn = new MySqlConnection(edtpkt_connStr);
                    try
                    {
                        edtpkt_conn.Open();

                        edtpkt_query = "UPDATE `paket` SET `jenis_paket` = '"+cbo_edtpkt_jenispaket.SelectedItem+"', `nama_paket` = '"+txt_edtpkt_namapaket.Text+"', "
                                          +"`durasi_paket` = '"+edtpkt_durasiPaket+"', `harga_paket` = '"+txt_edtpkt_hargapaket.Text+"', "
                                            + "`jam_kerja` = 'Normal', `komisi_per_paket` = '" + txt_edtpkt_komisipaket.Text + "' WHERE `paket`.`id_paket` = " + edtpkt_idTerpilih + " ;";
                        MySqlCommand cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    edtpkt_conn.Close();
                    #endregion
                    MessageBox.Show("Data Paket telah berhasil diperbarui");
                    cbo_edtpkt_jenispaket.Enabled = false;
                    cbo_edtpkt_jenispaket.SelectedItem = null;
                    txt_edtpkt_namapaket.Enabled = false;
                    txt_edtpkt_namapaket.Clear();
                    txt_edtpkt_durasipaket.Enabled = false;
                    txt_edtpkt_durasipaket.Clear();
                    txt_edtpkt_hargapaket.Enabled = false;
                    txt_edtpkt_hargapaket.Clear();
                    rdo_edtpkt_jamnormal.Enabled = false;
                    rdo_edtpkt_jamnormal.Checked = false;
                    rdo_edtpkt_jammidnight.Enabled = false;
                    rdo_edtpkt_jammidnight.Checked = false;
                    txt_edtpkt_komisipaket.Enabled = false;
                    txt_edtpkt_komisipaket.Clear();
                    btn_edtpkt_simpan.Enabled = false;
                    lsb_edtpkt_jenisnamapkt.Items.Clear();
                }
                else if (rdo_edtpkt_jammidnight.Checked)
                {
                    #region(Update)
                    edtpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    edtpkt_conn = new MySqlConnection(edtpkt_connStr);
                    try
                    {
                        edtpkt_conn.Open();

                        edtpkt_query = "UPDATE `paket` SET `jenis_paket` = '" + cbo_edtpkt_jenispaket.SelectedItem + "', `nama_paket` = '" + txt_edtpkt_namapaket.Text + "', "
                                          + "`durasi_paket` = '" + edtpkt_durasiPaket + "', `harga_paket` = '" + txt_edtpkt_hargapaket.Text + "', "
                                            + "`jam_kerja` = 'Midnight', `komisi_per_paket` = '" + txt_edtpkt_komisipaket.Text + "' WHERE `paket`.`id_paket` = " + edtpkt_idTerpilih + " ;";
                        MySqlCommand edtpkt_cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                        edtpkt_cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    edtpkt_conn.Close();
                    #endregion
                    MessageBox.Show("Data Paket telah berhasil diperbarui");
                    cbo_edtpkt_jenispaket.Enabled = false;
                    cbo_edtpkt_jenispaket.SelectedItem = null;
                    txt_edtpkt_namapaket.Enabled = false;
                    txt_edtpkt_namapaket.Clear();
                    txt_edtpkt_durasipaket.Enabled = false;
                    txt_edtpkt_durasipaket.Clear();
                    txt_edtpkt_hargapaket.Enabled = false;
                    txt_edtpkt_hargapaket.Clear();
                    rdo_edtpkt_jamnormal.Enabled = false;
                    rdo_edtpkt_jamnormal.Checked = false;
                    rdo_edtpkt_jammidnight.Enabled = false;
                    rdo_edtpkt_jammidnight.Checked = false;
                    txt_edtpkt_komisipaket.Enabled = false;
                    txt_edtpkt_komisipaket.Clear();
                    btn_edtpkt_simpan.Enabled = false;
                    lsb_edtpkt_jenisnamapkt.Items.Clear();
                }
                    
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
            }
                #endregion

            #region(Isi listbox dengan Jenis dan Nama Paket per baris)
            string edtpkt_jenisnamapkt;
            edtpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            edtpkt_conn = new MySqlConnection(edtpkt_connStr);
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
                }
                edtpkt_rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            edtpkt_conn.Close();
            #endregion
        }
    }
}
