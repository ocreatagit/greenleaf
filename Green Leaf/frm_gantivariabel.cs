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
    public partial class frm_gantivariabel : Form
    {
        public frm_gantivariabel()
        {
            InitializeComponent();
        }

        private void btn_variabel_simpan_Click(object sender, EventArgs e)
        {
            if (txt_variabel_extra.Text == "")
            {
                MessageBox.Show("Mohon lengkapi kolom Extra terlebih dahulu");
            }
            else if (txt_variabel_potonganhotel.Text == "")
            {
                MessageBox.Show("Mohon lengkapi kolom Potongan Hotel terlebih dahulu");
            }
            else
            {
                #region(Update)
                string edtpkt_connStr;
                MySqlConnection edtpkt_conn;
                DBConnect edtpkt_sql = new DBConnect();
                string edtpkt_query;
                edtpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                edtpkt_conn = new MySqlConnection(edtpkt_connStr);
                try
                {
                    edtpkt_conn.Open();

                    edtpkt_query = "UPDATE `variabel` SET `extra_variabel` = '"+int.Parse(txt_variabel_extra.Text)+"', "
                                    + "`potonganhotel_variabel` = '" + int.Parse(txt_variabel_potonganhotel.Text) + "' "
                                        +"WHERE `variabel`.`id_variabel` = 1;";
                    MySqlCommand cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data variabel telah tersimpan");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                edtpkt_conn.Close();
                #endregion
            }
        }

        private void txt_variabel_extra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_variabel_potonganhotel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void frm_gantivariabel_Load(object sender, EventArgs e)
        {
            #region(Isi listbox dengan Jenis dan Nama Paket per baris)
            string edtpkt_query;
            string edtpkt_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection edtpkt_conn = new MySqlConnection(edtpkt_connStr);
            try
            {
                edtpkt_conn.Open();

                edtpkt_query = "SELECT * FROM `variabel`";
                MySqlCommand edtpkt_cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                MySqlDataReader edtpkt_rdr = edtpkt_cmd.ExecuteReader();

                while (edtpkt_rdr.Read())
                {
                    txt_variabel_extra.Text = edtpkt_rdr.GetString(1);
                    txt_variabel_potonganhotel.Text = edtpkt_rdr.GetString(2);
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
    }
}
