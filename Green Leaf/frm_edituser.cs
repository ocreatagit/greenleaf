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
    public partial class frm_edituser : Form
    {
        public frm_edituser()
        {
            InitializeComponent();
        }

        private void frm_edituser_Load(object sender, EventArgs e)
        {
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

                string edttrps_query;

                #region(Select)
                string edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                MySqlConnection edttrps_conn = new MySqlConnection(edttrps_connStr);
                List<string> edttrps_lstKode = new List<string>();
                try
                {
                    edttrps_conn.Open();

                    edttrps_query = "SELECT * FROM `pengguna` WHERE `nama_pengguna` LIKE '" + edtuser_userterpilih + "'";
                    MySqlCommand edttrps_cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                    MySqlDataReader edttrps_rdr = edttrps_cmd.ExecuteReader();

                    while (edttrps_rdr.Read())
                    {
                        edtuser_iduser = edttrps_rdr.GetInt32(0);
                        txt_edtuser_user.Text = edttrps_rdr.GetString(1);
                        edtuser_user = edttrps_rdr.GetString(1);
                        txt_edtuser_pass.Text = edttrps_rdr.GetString(2);
                        txt_edtuser_nama.Text = edttrps_rdr.GetString(4);
                        cbo_edtuser_jenisuser.SelectedItem = edttrps_rdr.GetString(3);
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

        private void btn_edtuser_hapus_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Apakah anda sudah yakin?", "Alert", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                #region(Update)
                MySqlConnection edttrps_conn;
                DBConnect edttrps_sql = new DBConnect();
                string edttrps_query;
                edttrps_conn = new MySqlConnection("server=localhost;user=root;database=greenleaf;port=3306;password=;");
                try
                {
                    edttrps_conn.Open();

                    edttrps_query = "DELETE FROM `pengguna` WHERE `pengguna`.`nama_pengguna` = '" + edtuser_user +"'";
                    MySqlCommand cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                    cmd.ExecuteNonQuery();

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
                    string edttrps_query2;
                    string edttrps_connStr2 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    MySqlConnection edttrps_conn2 = new MySqlConnection(edttrps_connStr2);
                    List<string> edttrps_lstKode = new List<string>();
                    try
                    {
                        edttrps_conn2.Open();

                        edttrps_query2 = "SELECT * FROM `pengguna` order by id_pengguna DESC";
                        MySqlCommand edttrps_cmd2 = new MySqlCommand(edttrps_query2, edttrps_conn2);
                        MySqlDataReader edttrps_rdr2 = edttrps_cmd2.ExecuteReader();

                        while (edttrps_rdr2.Read())
                        {
                            //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                            lsb_edtuser_user.Items.Add(edttrps_rdr2.GetString(1));
                        }
                        edttrps_rdr2.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
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

        private void btn_edtuser_simpan_Click(object sender, EventArgs e)
        {
            if (txt_edtuser_nama.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Kode Terapis terlebih dahulu!");
            }
            else if (txt_edtuser_pass.Text == "")
            {
                MessageBox.Show("Mohon lengkapi data Nama Terapis terlebih dahulu!");
            }
            else if (txt_edtuser_user.Text == "")
            {
                MessageBox.Show("Mohon pilih foto KTP terlebih dahulu!");
            }
            else if (cbo_edtuser_jenisuser.SelectedItem == null)
            {
                MessageBox.Show("Mohon pilih foto KTP terlebih dahulu!");
            }
            else
            {
                string edttrps_connStr;
                MySqlConnection edttrps_conn;
                DBConnect edttrps_sql = new DBConnect();
                string edttrps_query;
                bool edtuser_namasama = false;
                //edttrps_lokasi_gambar = edttrps_lokasi_gambar.Replace(@"\", @"\\");

                #region(Select)
                edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                edttrps_conn = new MySqlConnection(edttrps_connStr);
                List<string> edtuser_lstpengguna = new List<string>();
                try
                {
                    edttrps_conn.Open();

                    edttrps_query = "SELECT nama_pengguna FROM `pengguna`";
                    MySqlCommand edttrps_cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                    MySqlDataReader edttrps_rdr = edttrps_cmd.ExecuteReader();

                    while (edttrps_rdr.Read())
                    {
                        edtuser_lstpengguna.Add(edttrps_rdr[0].ToString());
                    }
                    edttrps_rdr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                edttrps_conn.Close();
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
                    edttrps_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    edttrps_conn = new MySqlConnection(edttrps_connStr);
                    try
                    {
                        edttrps_conn.Open();

                        edttrps_query = "UPDATE `pengguna` SET `nama_pengguna` = '" + txt_edtuser_user.Text + "', `kata_kunci` = '" + txt_edtuser_pass.Text + 
                                            "', `namaasli_pengguna` = '" + txt_edtuser_nama.Text + "', `jenis_pengguna` = '"+cbo_edtuser_jenisuser.SelectedItem.ToString()+
                                              "' WHERE `pengguna`.`id_pengguna` = " + edtuser_iduser + "";
                        MySqlCommand cmd = new MySqlCommand(edttrps_query, edttrps_conn);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    edttrps_conn.Close();
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
                    string edttrps_query2;
                    string edttrps_connStr2 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    MySqlConnection edttrps_conn2 = new MySqlConnection(edttrps_connStr2);
                    List<string> edttrps_lstKode = new List<string>();
                    try
                    {
                        edttrps_conn2.Open();

                        edttrps_query2 = "SELECT * FROM `pengguna` order by id_pengguna DESC";
                        MySqlCommand edttrps_cmd2 = new MySqlCommand(edttrps_query2, edttrps_conn2);
                        MySqlDataReader edttrps_rdr2 = edttrps_cmd2.ExecuteReader();

                        while (edttrps_rdr2.Read())
                        {
                            //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                            lsb_edtuser_user.Items.Add(edttrps_rdr2.GetString(1));
                        }
                        edttrps_rdr2.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    edttrps_conn2.Close();
                    #endregion
                }
                
            }
        }
    }
}
