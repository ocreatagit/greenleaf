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
    public partial class frm_tambahpaket : Form
    {
        public frm_tambahpaket()
        {
            InitializeComponent();
        }

        private void btn_tbhpkt_tambah_Click(object sender, EventArgs e)
        {
            #region(Cek inputan kosong)
            if (cbo_tbhpkt_jenispaket.SelectedItem == null)
            {
                MessageBox.Show("Mohon pilih Jenis Paket terlebih dahulu");
            }
            else if (txt_tbhpkt_namapaket.Text=="")
            {
                MessageBox.Show("Mohon isi kolom Nama Paket terlebih dahulu");
            }
            else if (txt_tbhpkt_durasipaket.Text=="")
            {
                MessageBox.Show("Mohon isi kolom Durasi Paket terlebih dahulu");
            }
            else if (txt_tbhpkt_hargapaket.Text=="")
            {
                MessageBox.Show("Mohon isi kolom Harga Paket terlebih dahulu");
            }
            else if (rdo_tbhpkt_jamnormal.Checked==false&&rdo_tbhpkt_jammidnight.Checked==false)
            {
                MessageBox.Show("Mohon pilih Jam Kerja terlebih dahulu");
            }
            else if (txt_tbhpkt_komisipaket.Text=="")
            {
                MessageBox.Show("Mohon isi kolom Komisi Paket terlebih dahulu");
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

                    tbhpkt_query = "SELECT * FROM `paket` WHERE `jenis_paket` = '" + cbo_tbhpkt_jenispaket.SelectedItem + "' AND `nama_paket` = '"+txt_tbhpkt_namapaket.Text+"'";
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
                if (tbhpkt_lstHasil.Count!=0)
                {
                    MessageBox.Show("Maaf, Nama Paket: "+txt_tbhpkt_namapaket.Text+", dengan Jenis Paket: "+cbo_tbhpkt_jenispaket.SelectedItem+", sudah ada di dalam database");
                }
                #endregion
            #region(Insert paket ke databse)
                else
                {
                    #region(Buat huruf besar untuk Jam dan Menit)
                    string tbhpkt_durasiPaket = txt_tbhpkt_durasipaket.Text;
                    var regex = new Regex(@"\b[A-Z]", RegexOptions.IgnoreCase);
                    tbhpkt_durasiPaket = regex.Replace(tbhpkt_durasiPaket, m => m.ToString().ToUpper());
                    #endregion
                    DBConnect tbhpkt_sql = new DBConnect();

                    if (rdo_tbhpkt_jamnormal.Checked)
                    {
                        tbhpkt_query = "INSERT INTO `paket` (`id_paket`, `jenis_paket`, `nama_paket`, `durasi_paket`, `harga_paket`, `jam_kerja`, `komisi_per_paket`) "
                                + "VALUES (NULL, '" + cbo_tbhpkt_jenispaket.SelectedItem + "', '" + txt_tbhpkt_namapaket.Text + "', '" + tbhpkt_durasiPaket + "', '" + txt_tbhpkt_hargapaket.Text + "', 'Normal', '"+txt_tbhpkt_komisipaket.Text+"');";
                        tbhpkt_sql.Insert(tbhpkt_query);
                    }
                    else if (rdo_tbhpkt_jammidnight.Checked)
                    {
                        tbhpkt_query = "INSERT INTO `paket` (`id_paket`, `jenis_paket`, `nama_paket`, `durasi_paket`, `harga_paket`, `jam_kerja`, `komisi_per_paket`) "
                                + "VALUES (NULL, '" + cbo_tbhpkt_jenispaket.SelectedItem + "', '" + txt_tbhpkt_namapaket.Text + "', '" + tbhpkt_durasiPaket + "', '" + txt_tbhpkt_hargapaket.Text + "', 'Midnight', '" + txt_tbhpkt_komisipaket.Text + "');";
                        tbhpkt_sql.Insert(tbhpkt_query);
                    }
                    MessageBox.Show("Paket telah berhasil ditambahkan");
                }

            }
            #endregion
        }

        private void btn_tbhpkt_batal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_tambahpaket_Load(object sender, EventArgs e)
        {

        }
    }
}
