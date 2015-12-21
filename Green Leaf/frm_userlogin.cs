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
    public partial class frm_userlogin : Form
    {
        public frm_userlogin()
        {
            InitializeComponent();
        }

        private void btn_masuk_Click(object sender, EventArgs e)
        {
            bool userAda = false;
            bool passSama = false;

            #region(Select Username dan Password)
            string query;
            string connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            List<string> lstUsers = new List<string>();
            List<string> lstPass = new List<string>();
            try
            {
                conn.Open();

                query = "SELECT `id_pengguna`, `nama_pengguna`, `kata_kunci`, `jenis_pengguna` FROM `pengguna`";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    lstUsers.Add(rdr[1].ToString());
                    lstPass.Add(rdr[2].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            #endregion

            if (txt_username.Text != "")
            {
                foreach (string user in lstUsers)
                {
                    if (user == txt_username.Text)
                    {
                        userAda = true;
                        break;
                    }
                    else
                    {
                        userAda = false;
                    }
                }

                if (userAda == true)
                {
                    for (int i = 0; i < lstUsers.Count; i++)
                    {
                        if (txt_pass.Text == lstPass[i])
                        {
                            passSama = true;
                            //MessageBox.Show("Login berhasil");
                            break;
                        }
                        else
                        {
                            passSama = false;
                            //MessageBox.Show("Login gagal, Password yang anda masukan salah");
                        }

                    }
                    if (passSama == true)
                    {
                        MessageBox.Show("Login berhasil");
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

        private void btn_batal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
