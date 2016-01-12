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
    public partial class frm_tambahuser : Form
    {
        public frm_tambahuser()
        {
            InitializeComponent();
        }

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
                string tbhpkt_query;
                string tbhpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                MySqlConnection tbhpkt_conn = new MySqlConnection(tbhpkt_connStr);
                int countuser = 0;
                try
                {
                    tbhpkt_conn.Open();

                    tbhpkt_query = "SELECT * FROM `pengguna` WHERE `nama_pengguna` = '" + txt_tbhuser_user.Text + "'";
                    MySqlCommand tbhpkt_cmd = new MySqlCommand(tbhpkt_query, tbhpkt_conn);
                    MySqlDataReader tbhpkt_rdr = tbhpkt_cmd.ExecuteReader();

                    while (tbhpkt_rdr.Read())
                    {
                        countuser++;
                    }
                    tbhpkt_rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                tbhpkt_conn.Close();
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

                    DBConnect tbhpkt_sql = new DBConnect();

                    //tbhpkt_query = "INSERT INTO `paket` (`id_paket`, `jenis_paket`, `nama_paket`, `durasi_paket`, `harga_paket`, `jam_kerja`, `komisi_per_paket`) "
                    //        + "VALUES (NULL, '" +  + "', '" +  + "', '" +  + "', '" +  + "', 'Normal', '');";

                    tbhpkt_query = "INSERT INTO `pengguna` (`id_pengguna`, `namaasli_pengguna` ,`nama_pengguna`, `kata_kunci`, `jenis_pengguna`)"
                        + "VALUES (NULL, '"+txt_tbhuser_nama.Text+"', '" + txt_tbhuser_user.Text + "', '" + txt_tbhuser_pass.Text + "', '" + cbo_tbhuser_jenisuser.SelectedItem + "');";
                    tbhpkt_sql.Insert(tbhpkt_query);

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

        }
    }
}
