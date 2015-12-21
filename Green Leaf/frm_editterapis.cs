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
        string edttrps_lokasi_gambar;
        string edttrps_idTerapis;
        string edttrps_kodeTerakhir;

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
            OpenFileDialog edttrps_open = new OpenFileDialog();
            // image filters
            edttrps_open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (edttrps_open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box

                // image file path
                edttrps_lokasi_gambar = edttrps_open.FileName.Replace(@"\", @"\\");
                pcb_edttrps_fotoKTP.Image = new Bitmap(edttrps_lokasi_gambar);
            }
        }

        private void frm_editterapis_Load(object sender, EventArgs e)
        {
            btn_edttrp_simpan.Enabled = false;
            txt_edttrps_kodeterapis.Enabled = false;
            txt_edttrps_namaterapis.Enabled = false;
            btn_edttrps_browsefoto.Enabled = false;
            rdo_edttrps_statusaktif.Enabled = false;
            rdo_edttrps_statustdkaktif.Enabled = false;

            string edttrps_query;

            #region(Select)
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

        private void button1_Click(object sender, EventArgs e)
        {
            btn_edttrp_simpan.Enabled = true;
            txt_edttrps_kodeterapis.Enabled = true;
            txt_edttrps_namaterapis.Enabled = true;
            btn_edttrps_browsefoto.Enabled = true;
            rdo_edttrps_statusaktif.Enabled = true;
            rdo_edttrps_statustdkaktif.Enabled = true;
            if (lsb_edttrps_kodeterapis.SelectedItem != null)
            {
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
                        edttrps_idTerapis = edttrps_rdr.GetString(0);
                        txt_edttrps_kodeterapis.Text = edttrps_rdr.GetString(1);
                        edttrps_kodeTerakhir = txt_edttrps_kodeterapis.Text;
                        txt_edttrps_namaterapis.Text = edttrps_rdr.GetString(2);
                        pcb_edttrps_fotoKTP.Image = new Bitmap(edttrps_rdr.GetString(3));
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

        private void btn_simpan_Click(object sender, EventArgs e)
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

                if (txt_edttrps_kodeterapis.Text == edttrps_kodeTerakhir)
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
                            pcb_edttrps_fotoKTP.Image = null;
                            txt_edttrps_kodeterapis.Focus();
                            btn_edttrp_simpan.Enabled = false;
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
                            pcb_edttrps_fotoKTP.Image = null;
                            txt_edttrps_kodeterapis.Focus();
                            btn_edttrp_simpan.Enabled = false;
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
                                pcb_edttrps_fotoKTP.Image = null;
                                txt_edttrps_kodeterapis.Focus();
                                btn_edttrp_simpan.Enabled = false;
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
                                pcb_edttrps_fotoKTP.Image = null;
                                txt_edttrps_kodeterapis.Focus();
                                btn_edttrp_simpan.Enabled = false;
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

        private void btn_batal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
