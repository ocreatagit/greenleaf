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
        string lokasi_gambar = "";

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
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                
                // image file path
                lokasi_gambar = open.FileName.Replace(@"\", @"\\");
                pict_fotoKTP.Image = new Bitmap(lokasi_gambar);
            }
        }

        private void btn_tambah_Click(object sender, EventArgs e)
        {
            DBConnect sql = new DBConnect();
            string query;
            bool kodeSama=false;

            #region(Select khusus kode terapis, disimpan ke dalam List lstkode)
            string connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            List<string> lstKode = new List<string>();
            try
            {
                conn.Open();

                query = "SELECT kode_terapis FROM `terapis`";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    lstKode.Add(rdr[0].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            #endregion

            foreach (string kode in lstKode)
            {
                if (kode==txt_kodeterapis.Text)
                {
                    MessageBox.Show("Maaf, Kode Terapis yang anda masukkan sudah terdaftar, silahkan ganti dengan Kode Terapis yang berbeda!");
                    kodeSama = true;
                    break;
                }
                else
                {
                    kodeSama = false;
                }
            }

            if (kodeSama == false)
            {
                if (txt_kodeterapis.Text == "")
                {
                    MessageBox.Show("Mohon lengkapi data Kode Terapis terlebih dahulu!");
                }
                else if (txt_namaterapis.Text == "")
                {
                    MessageBox.Show("Mohon lengkapi data Nama Terapis terlebih dahulu!");
                }
                else if (lokasi_gambar == "")
                {
                    MessageBox.Show("Mohon pilih foto KTP terlebih dahulu!");
                }
                else
                {
                    if (rdo_statusaktif.Checked == false && rdo_statustdkaktif.Checked == false)
                    {
                        MessageBox.Show("Mohon pilih Status terlebih dahulu!");
                    }
                    else if (rdo_statusaktif.Checked)
                    {
                        query = "INSERT INTO `terapis` (`kode_terapis`, `nama_terapis`, `lokasi_gambar`, `status_terapis`) VALUES ('" + txt_kodeterapis.Text + "', '" + txt_namaterapis.Text + "', '" + lokasi_gambar + "', 'Aktif');";
                        sql.Insert(query);
                        MessageBox.Show("Terapis telah berhasil ditambahkan");
                        txt_kodeterapis.Clear();
                        txt_namaterapis.Clear();
                        lokasi_gambar = "";
                        rdo_statusaktif.Checked = false;
                        rdo_statustdkaktif.Checked = false;
                        pict_fotoKTP.Image = null;
                        txt_kodeterapis.Focus();
                    }
                    else if (rdo_statustdkaktif.Checked)
                    {
                        query = "INSERT INTO `terapis` (`kode_terapis`, `nama_terapis`, `lokasi_gambar`, `status_terapis`) VALUES ('" + txt_kodeterapis.Text + "', '" + txt_namaterapis.Text + "', '" + lokasi_gambar + "', 'Tidak Aktif');";
                        sql.Insert(query);
                        MessageBox.Show("Terapis telah berhasil ditambahkan");
                        txt_kodeterapis.Clear();
                        txt_namaterapis.Clear();
                        lokasi_gambar = "";
                        rdo_statusaktif.Checked = false;
                        rdo_statustdkaktif.Checked = false;
                        pict_fotoKTP.Image = null;
                        txt_kodeterapis.Focus();
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
