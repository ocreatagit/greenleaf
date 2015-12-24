using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Web;

namespace Green_Leaf
{
    public partial class frm_cetaknota : Form
    {
        public frm_cetaknota()
        {
            InitializeComponent();
        }

        //DataTable dtEmp = new DataTable();

        private void frm_cetaknota_Load(object sender, EventArgs e)
        {
            //rdo_ctknota_normal.Enabled = true;
            //rdo_ctknota_midnight.Enabled = true;
            //rdo_ctknota_hotel.Enabled = false;
            //rdo_ctknota_biasa.Enabled = false;
            //cbo_ctknota_jenispaket.Enabled = false;
            //dgv_ctknota_tabelhrgpkt.Enabled = false;
            //rdo_ctknota_cash.Enabled = false;
            //rdo_ctknota_credit.Enabled = false;
            //cbo_ctknota_kodeterapis.Enabled = false;
            //txt_ctknota_namaterapis.Enabled = false;
            //txt_ctknota_diskon.Enabled = false;
            //txt_ctknota_ket.Enabled = false;
            //txt_ctknota_fee.Enabled = false;
            //txt_ctknota_nomorruangan.Enabled = false;
            //lbl_ctknota_totalbyr.Enabled = false;
            //btn_ctknota_cetak.Enabled = false;
            //btn_ctknota_batal.Enabled = true;

            cbo_ctknota_jenispaket.Items.Clear();
            #region(Select)
            string ctknota_query;
            string ctknota_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection ctknota_conn = new MySqlConnection(ctknota_connStr);
            List<string> ctknota_lstKode = new List<string>();
            try
            {
                ctknota_conn.Open();

                ctknota_query = "SELECT DISTINCT `jenis_paket` FROM `paket`";
                MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                MySqlDataReader ctknota_rdr = ctknota_cmd.ExecuteReader();

                while (ctknota_rdr.Read())
                {
                    //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                    cbo_ctknota_jenispaket.Items.Add(ctknota_rdr.GetString(0));
                }
                ctknota_rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            ctknota_conn.Close();
            #endregion
        }

        private void rdo_ctknota_biasa_CheckedChanged(object sender, EventArgs e)
        {
            //rdo_ctknota_normal.Enabled = true;
            //rdo_ctknota_midnight.Enabled = true;
            //rdo_ctknota_hotel.Enabled = true;
            //rdo_ctknota_biasa.Enabled = true;
            //cbo_ctknota_jenispaket.Enabled = true;
            //dgv_ctknota_tabelhrgpkt.Enabled = false;
            //rdo_ctknota_cash.Enabled = false;
            //rdo_ctknota_credit.Enabled = false;
            //cbo_ctknota_kodeterapis.Enabled = false;
            //txt_ctknota_namaterapis.Enabled = false;
            //txt_ctknota_diskon.Enabled = false;
            //txt_ctknota_ket.Enabled = false;
            //txt_ctknota_fee.Enabled = false;
            //txt_ctknota_nomorruangan.Enabled = false;
            //lbl_ctknota_totalbyr.Enabled = false;
            //btn_ctknota_cetak.Enabled = false;
            //btn_ctknota_batal.Enabled = true;

            //dtEmp.Columns.Add("Nama Paket", typeof(string));
            //dtEmp.Columns.Add("Durasi Paket", typeof(string));
            //dtEmp.Columns.Add("Harga Paket", typeof(string));
            //dtEmp.Columns.Add("Extra", typeof(bool));
            //dtEmp.Columns.Add("Pilih", typeof(bool));

            //dtEmp.Rows.Add("Traditional Massage", "1 Jam 30 Menit" , "Rp. 500.000,-", false,false);
            //dtEmp.Rows.Add("Traditional Massage", "1 Jam 30 Menit", "Rp. 500.000,-", false, false);
            //dgv_ctknota_tabelhrgpkt.DataSource = dtEmp; 
        }

        private void rdo_ctknota_hotel_CheckedChanged(object sender, EventArgs e)
        {
            //rdo_ctknota_normal.Enabled = true;
            //rdo_ctknota_midnight.Enabled = true;
            //rdo_ctknota_hotel.Enabled = true;
            //rdo_ctknota_biasa.Enabled = true;
            //cbo_ctknota_jenispaket.Enabled = true;
            //dgv_ctknota_tabelhrgpkt.Enabled = false;
            //rdo_ctknota_cash.Enabled = false;
            //rdo_ctknota_credit.Enabled = false;
            //cbo_ctknota_kodeterapis.Enabled = false;
            //txt_ctknota_namaterapis.Enabled = false;
            //txt_ctknota_diskon.Enabled = false;
            //txt_ctknota_ket.Enabled = false;
            //txt_ctknota_fee.Enabled = false;
            //txt_ctknota_nomorruangan.Enabled = false;
            //lbl_ctknota_totalbyr.Enabled = false;
            //btn_ctknota_cetak.Enabled = false;
            //btn_ctknota_batal.Enabled = true;

            //dtEmp.Columns.Add("Tamu Hotel", typeof(string));
        }

        private void rdo_ctknota_normal_CheckedChanged(object sender, EventArgs e)
        {
            //rdo_ctknota_normal.Enabled = true;
            //rdo_ctknota_midnight.Enabled = true;
            //rdo_ctknota_hotel.Enabled = true;
            //rdo_ctknota_biasa.Enabled = true;
            //cbo_ctknota_jenispaket.Enabled = false;
            //dgv_ctknota_tabelhrgpkt.Enabled = false;
            //rdo_ctknota_cash.Enabled = false;
            //rdo_ctknota_credit.Enabled = false;
            //cbo_ctknota_kodeterapis.Enabled = false;
            //txt_ctknota_namaterapis.Enabled = false;
            //txt_ctknota_diskon.Enabled = false;
            //txt_ctknota_ket.Enabled = false;
            //txt_ctknota_fee.Enabled = false;
            //txt_ctknota_nomorruangan.Enabled = false;
            //lbl_ctknota_totalbyr.Enabled = false;
            //btn_ctknota_cetak.Enabled = false;
            //btn_ctknota_batal.Enabled = true;
        }

        private void rdo_ctknota_midnight_CheckedChanged(object sender, EventArgs e)
        {
            //rdo_ctknota_normal.Enabled = true;
            //rdo_ctknota_midnight.Enabled = true;
            //rdo_ctknota_hotel.Enabled = true;
            //rdo_ctknota_biasa.Enabled = true;
            //cbo_ctknota_jenispaket.Enabled = false;
            //dgv_ctknota_tabelhrgpkt.Enabled = false;
            //rdo_ctknota_cash.Enabled = false;
            //rdo_ctknota_credit.Enabled = false;
            //cbo_ctknota_kodeterapis.Enabled = false;
            //txt_ctknota_namaterapis.Enabled = false;
            //txt_ctknota_diskon.Enabled = false;
            //txt_ctknota_ket.Enabled = false;
            //txt_ctknota_fee.Enabled = false;
            //txt_ctknota_nomorruangan.Enabled = false;
            //lbl_ctknota_totalbyr.Enabled = false;
            //btn_ctknota_cetak.Enabled = false;
            //btn_ctknota_batal.Enabled = true;
        }

        private void cbo_ctknota_jenispaket_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rdo_ctknota_normal.Enabled = true;
            //rdo_ctknota_midnight.Enabled = true;
            //rdo_ctknota_hotel.Enabled = true;
            //rdo_ctknota_biasa.Enabled = true;
            //cbo_ctknota_jenispaket.Enabled = true;
            //dgv_ctknota_tabelhrgpkt.Enabled = true;
            //rdo_ctknota_cash.Enabled = true;
            //rdo_ctknota_credit.Enabled = true;
            //cbo_ctknota_kodeterapis.Enabled = false;
            //txt_ctknota_namaterapis.Enabled = false;
            //txt_ctknota_diskon.Enabled = false;
            //txt_ctknota_ket.Enabled = false;
            //txt_ctknota_fee.Enabled = false;
            //txt_ctknota_nomorruangan.Enabled = false;
            //lbl_ctknota_totalbyr.Enabled = false;
            //btn_ctknota_cetak.Enabled = false;
            //btn_ctknota_batal.Enabled = true;

            //dtEmp.Columns.Add("Nama Paket", typeof(string));
            //dtEmp.Columns.Add("Durasi Paket", typeof(string));
            //dtEmp.Columns.Add("Harga Paket", typeof(string));
            //dtEmp.Columns.Add("Extra", typeof(bool));
            //dtEmp.Columns.Add("Pilih", typeof(bool));

            dgv_ctknota_tabelhrgpkt.DataSource = null;
            dgv_ctknota_tabelhrgpkt.Rows.Clear();
            dgv_ctknota_tabelhrgpkt.Refresh();

            if (rdo_ctknota_normal.Checked)
            {
                if (rdo_ctknota_hotel.Checked)
                {
                    #region(Select)
                    string ctknota_query;
                    string ctknota_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    MySqlConnection ctknota_conn = new MySqlConnection(ctknota_connStr);
                    List<string> ctknota_lstKode = new List<string>();
                    try
                    {
                        ctknota_conn.Open();

                        ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = '"+cbo_ctknota_jenispaket.SelectedItem+"'";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(ctknota_query, ctknota_conn);
                        MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                        DataSet DS = new DataSet();
                        mySqlDataAdapter.Fill(DS);
                        DS.Tables[0].Columns.Remove("id_paket");
                        DS.Tables[0].Columns.Remove("jenis_paket");
                        DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                        DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                        dgv_ctknota_tabelhrgpkt.DataSource = DS.Tables[0];
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    ctknota_conn.Close();
                    #endregion
                }
                else if (rdo_ctknota_biasa.Checked)
                {
                    #region(Select)
            string ctknota_query;
            string ctknota_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection ctknota_conn = new MySqlConnection(ctknota_connStr);
            List<string> ctknota_lstKode = new List<string>();
            try
            {
                ctknota_conn.Open();

                ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = 'VIP' AND `jam_kerja` = 'Normal'";
                MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                MySqlDataReader ctknota_rdr = ctknota_cmd.ExecuteReader();

                while (ctknota_rdr.Read())
                {
                    //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                    //lsb_edttrps_kodeterapis.Items.Add(ctknota_rdr.GetString(1));
                }
                ctknota_rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            ctknota_conn.Close();
            #endregion
                }
            }
            else if (rdo_ctknota_midnight.Checked)
            {
                if (rdo_ctknota_hotel.Checked)
                {
                    #region(Select)
                    string ctknota_query;
                    string ctknota_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    MySqlConnection ctknota_conn = new MySqlConnection(ctknota_connStr);
                    List<string> ctknota_lstKode = new List<string>();
                    try
                    {
                        ctknota_conn.Open();

                        ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = 'VIP' AND `jam_kerja` = 'Normal'";
                        MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                        MySqlDataReader ctknota_rdr = ctknota_cmd.ExecuteReader();

                        while (ctknota_rdr.Read())
                        {
                            //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                            //lsb_edttrps_kodeterapis.Items.Add(ctknota_rdr.GetString(1));
                        }
                        ctknota_rdr.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    ctknota_conn.Close();
                    #endregion
                }
                else if (rdo_ctknota_biasa.Checked)
                {
                    #region(Select)
                    string ctknota_query;
                    string ctknota_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
                    MySqlConnection ctknota_conn = new MySqlConnection(ctknota_connStr);
                    List<string> ctknota_lstKode = new List<string>();
                    try
                    {
                        ctknota_conn.Open();

                        ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = 'VIP' AND `jam_kerja` = 'Normal'";
                        MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                        MySqlDataReader ctknota_rdr = ctknota_cmd.ExecuteReader();

                        while (ctknota_rdr.Read())
                        {
                            //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                            //lsb_edttrps_kodeterapis.Items.Add(ctknota_rdr.GetString(1));
                        }
                        ctknota_rdr.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    ctknota_conn.Close();
                    #endregion
                }
            }

            
        }

        private void rdo_ctknota_cash_CheckedChanged(object sender, EventArgs e)
        {
            //rdo_ctknota_normal.Enabled = true;
            //rdo_ctknota_midnight.Enabled = true;
            //rdo_ctknota_hotel.Enabled = true;
            //rdo_ctknota_biasa.Enabled = true;
            //cbo_ctknota_jenispaket.Enabled = true;
            //dgv_ctknota_tabelhrgpkt.Enabled = true;
            //rdo_ctknota_cash.Enabled = true;
            //rdo_ctknota_credit.Enabled = true;
            //cbo_ctknota_kodeterapis.Enabled = true;
            //txt_ctknota_namaterapis.Enabled = true;
            //txt_ctknota_diskon.Enabled = false;
            //txt_ctknota_ket.Enabled = false;
            //txt_ctknota_fee.Enabled = false;
            //txt_ctknota_nomorruangan.Enabled = false;
            //lbl_ctknota_totalbyr.Enabled = false;
            //btn_ctknota_cetak.Enabled = false;
            //btn_ctknota_batal.Enabled = true;
        }

        private void rdo_ctknota_credit_CheckedChanged(object sender, EventArgs e)
        {
            //rdo_ctknota_normal.Enabled = true;
            //rdo_ctknota_midnight.Enabled = true;
            //rdo_ctknota_hotel.Enabled = true;
            //rdo_ctknota_biasa.Enabled = true;
            //cbo_ctknota_jenispaket.Enabled = true;
            //dgv_ctknota_tabelhrgpkt.Enabled = true;
            //rdo_ctknota_cash.Enabled = true;
            //rdo_ctknota_credit.Enabled = true;
            //cbo_ctknota_kodeterapis.Enabled = true;
            //txt_ctknota_namaterapis.Enabled = true;
            //txt_ctknota_diskon.Enabled = false;
            //txt_ctknota_ket.Enabled = false;
            //txt_ctknota_fee.Enabled = false;
            //txt_ctknota_nomorruangan.Enabled = false;
            //lbl_ctknota_totalbyr.Enabled = false;
            //btn_ctknota_cetak.Enabled = false;
            //btn_ctknota_batal.Enabled = true;
        }

        private void cbo_ctknota_kodeterapis_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rdo_ctknota_normal.Enabled = true;
            //rdo_ctknota_midnight.Enabled = true;
            //rdo_ctknota_hotel.Enabled = true;
            //rdo_ctknota_biasa.Enabled = true;
            //cbo_ctknota_jenispaket.Enabled = true;
            //dgv_ctknota_tabelhrgpkt.Enabled = true;
            //rdo_ctknota_cash.Enabled = true;
            //rdo_ctknota_credit.Enabled = true;
            //cbo_ctknota_kodeterapis.Enabled = true;
            //txt_ctknota_namaterapis.Enabled = true;
            //txt_ctknota_diskon.Enabled = true;
            //txt_ctknota_ket.Enabled = false;
            //txt_ctknota_fee.Enabled = false;
            //txt_ctknota_nomorruangan.Enabled = false;
            //lbl_ctknota_totalbyr.Enabled = false;
            //btn_ctknota_cetak.Enabled = false;
            //btn_ctknota_batal.Enabled = true;
        }

        private void txt_ctknota_diskon_TextChanged(object sender, EventArgs e)
        {
            //rdo_ctknota_normal.Enabled = true;
            //rdo_ctknota_midnight.Enabled = true;
            //rdo_ctknota_hotel.Enabled = true;
            //rdo_ctknota_biasa.Enabled = true;
            //cbo_ctknota_jenispaket.Enabled = true;
            //dgv_ctknota_tabelhrgpkt.Enabled = true;
            //rdo_ctknota_cash.Enabled = true;
            //rdo_ctknota_credit.Enabled = true;
            //cbo_ctknota_kodeterapis.Enabled = true;
            //txt_ctknota_namaterapis.Enabled = true;
            //txt_ctknota_diskon.Enabled = true;
            //txt_ctknota_ket.Enabled = true;
            //txt_ctknota_fee.Enabled = true;
            //txt_ctknota_nomorruangan.Enabled = true;
            //lbl_ctknota_totalbyr.Enabled = true;
            //btn_ctknota_cetak.Enabled = true;
            //btn_ctknota_batal.Enabled = true;
        }

        private void txt_ctknota_diskon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        private void btn_ctknota_cetak_Click(object sender, EventArgs e)
        {

        }

        private void txt_ctknota_nomorruangan_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgv_ctknota_tabelhrgpkt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgv_ctknota_tabelhrgpkt.CurrentCell.OwningColumn.Name == "Extra")
            //{
            //    for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
            //    {
            //        if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Selected)
            //        {
            //            dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value = true;
            //            dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].ReadOnly = true;
            //        }
            //        else
            //            dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value = false;

            //    }
            //}
            if (dgv_ctknota_tabelhrgpkt.CurrentCell.OwningColumn.Name == "Pilih")
            {
                dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly = true;
                for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                {
                    dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value = false;

                }
                for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                {
                    if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Selected)
                    {
                        dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value = true;
                        dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].ReadOnly = false;
                    }
                    else
                        dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value = false;

                }
            }
            //else if (dgv_ctknota_tabelhrgpkt.CurrentCell.OwningColumn.Name == "Extra")
            //{
            //    for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
            //    {
            //        if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Selected)
            //        {
            //            dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value = true;
            //        }
            //        else
            //        dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value = false;
            //        dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].ReadOnly = true;
            //    }

            //}
            
        }
    }
}
