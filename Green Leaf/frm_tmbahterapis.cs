using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//Add MySql Library
using MySql.Data.MySqlClient;
using System.IO;

namespace Green_Leaf
{
    public partial class frm_tmbahterapis : Form
    {
        string tbhtrps_lokasi_gambar = "";

        public frm_tmbahterapis()
        {
            InitializeComponent();
        }

        private void frm_tmbahterapis_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_browsefoto_Click(object sender, EventArgs e)
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

        private void btn_tambah_Click(object sender, EventArgs e)
        {
            DBConnect tbhtrps_sql = new DBConnect();
            
            bool tbhtrps_kodeSama=false;

            #region(Select khusus kode terapis, disimpan ke dalam List lstkode)
            string tbhtrps_query;
            string tbhtrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection tbhtrps_conn = new MySqlConnection(tbhtrps_connStr);
            List<string> tbhtrps_lstKode = new List<string>();
            try
            {
                tbhtrps_conn.Open();

                tbhtrps_query = "SELECT kode_terapis FROM `terapis`";
                MySqlCommand tbhtrps_cmd = new MySqlCommand(tbhtrps_query, tbhtrps_conn);
                MySqlDataReader tbhtrps_rdr = tbhtrps_cmd.ExecuteReader();

                while (tbhtrps_rdr.Read())
                {
                    tbhtrps_lstKode.Add(tbhtrps_rdr[0].ToString());
                }
                tbhtrps_rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            tbhtrps_conn.Close();
            #endregion

            foreach (string tbhtrps_kode in tbhtrps_lstKode)
            {
                if (tbhtrps_kode==txt_tbhtrps_kodeterapis.Text)
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

        private void btn_batal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
