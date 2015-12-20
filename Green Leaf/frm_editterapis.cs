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


namespace Green_Leaf
{
    public partial class frm_editterapis : Form
    {
        string lokasi_gambar;
        string idTerapis;
        string kodeTerakhir;

        public frm_editterapis()
        {
            InitializeComponent();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void frm_editterapis_Load(object sender, EventArgs e)
        {
            string query;

            #region(Select)
            string connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            List<string> lstKode = new List<string>();
            try
            {
                conn.Open();

                query = "SELECT * FROM `terapis` order by id_terapis DESC";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cbo_kodeterapis.Items.Add(rdr.GetString(1));
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kodeTerpilih = cbo_kodeterapis.SelectedItem.ToString();

            string query;

            #region(Select)
            string connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            List<string> lstKode = new List<string>();
            try
            {
                conn.Open();

                query = "SELECT * FROM `terapis` WHERE `kode_terapis` LIKE '"+kodeTerpilih+"'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    idTerapis = rdr.GetString(0);
                    txt_kodeterapis.Text = rdr.GetString(1);
                    kodeTerakhir = txt_kodeterapis.Text;
                    txt_namaterapis.Text = rdr.GetString(2);
                    pict_fotoKTP.Image = new Bitmap(rdr.GetString(3));
                    lokasi_gambar = rdr.GetString(3);
                    if (rdr.GetString(4)=="Aktif")
                    {
                        rdo_statusaktif.Checked = true;
                    }
                    else if (rdr.GetString(4) == "Tidak Aktif")
                    {
                        rdo_statustdkaktif.Checked = true;
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            #endregion
        }

        private void btn_simpan_Click(object sender, EventArgs e)
        {
            string connStr;
            MySqlConnection conn;
            DBConnect sql = new DBConnect();
            string query;
            bool kodeSama = false;
            lokasi_gambar = lokasi_gambar.Replace(@"\", @"\\");

            if (txt_kodeterapis.Text == kodeTerakhir)
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
                        #region(Update)
                        connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                        conn = new MySqlConnection(connStr);
                        try
                        {
                            conn.Open();

                            query = "UPDATE `terapis` SET `kode_terapis` = '" + kodeTerakhir + "', `nama_terapis` = '" + txt_namaterapis.Text + "', `lokasi_gambar` = '" + lokasi_gambar + "', `status_terapis` = 'Aktif' WHERE `terapis`.`id_terapis` = " + idTerapis + "";
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        conn.Close();
                        #endregion
                        MessageBox.Show("Data terapis telah berhasil disimpan");
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
                        #region(Update)
                        connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                        conn = new MySqlConnection(connStr);
                        try
                        {
                            conn.Open();

                            query = "UPDATE `terapis` SET `kode_terapis` = '" + kodeTerakhir + "', `nama_terapis` = '" + txt_namaterapis.Text + "', `lokasi_gambar` = '" + lokasi_gambar + "', `status_terapis` = 'Tidak Aktif' WHERE `terapis`.`id_terapis` = " + idTerapis + "";
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        conn.Close();
                        #endregion
                        MessageBox.Show("Data terapis telah berhasil disimpan");
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
            else
            {
                #region(Select)
                connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                conn = new MySqlConnection(connStr);
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
                    if (kode == txt_kodeterapis.Text)
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
                            #region(Update)
                            connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                            conn = new MySqlConnection(connStr);
                            try
                            {
                                conn.Open();

                                query = "UPDATE `terapis` SET `kode_terapis` = '" + txt_kodeterapis.Text + "', `nama_terapis` = '" + txt_namaterapis.Text + "', `lokasi_gambar` = '" + lokasi_gambar + "', `status_terapis` = 'Aktif' WHERE `terapis`.`id_terapis` = " + idTerapis + "";
                                MySqlCommand cmd = new MySqlCommand(query, conn);
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                            conn.Close();
                            #endregion
                            MessageBox.Show("Data terapis telah berhasil disimpan");
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
                            #region(Update)
                            connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                            conn = new MySqlConnection(connStr);
                            try
                            {
                                conn.Open();

                                query = "UPDATE `terapis` SET `kode_terapis` = '" + txt_kodeterapis.Text + "', `nama_terapis` = '" + txt_namaterapis.Text + "', `lokasi_gambar` = '" + lokasi_gambar + "', `status_terapis` = 'Tidak Aktif' WHERE `terapis`.`id_terapis` = " + idTerapis + "";
                                MySqlCommand cmd = new MySqlCommand(query, conn);
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                            conn.Close();
                            #endregion
                            MessageBox.Show("Data terapis telah berhasil disimpan");
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
            cbo_kodeterapis.Items.Clear();
            #region(Select)
            connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();

                query = "SELECT * FROM `terapis` order by id_terapis DESC";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cbo_kodeterapis.Items.Add(rdr.GetString(1));
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            #endregion
            
        }

        

        private void btn_batal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
