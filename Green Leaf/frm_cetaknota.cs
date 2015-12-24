﻿using System;
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
        DataSet DS = new DataSet();
        int tamuhotel;
        int extra;

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

            #region(Ambil data Potongan dan Extra dari database)
            string ctknota_query2;
            string ctknota_connStr2 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection ctknota_conn2 = new MySqlConnection(ctknota_connStr2);
            try
            {
                ctknota_conn2.Open();

                ctknota_query2 = "SELECT * FROM `variabel`";
                MySqlCommand ctknota_cmd2 = new MySqlCommand(ctknota_query2, ctknota_conn2);
                MySqlDataReader ctknota_rdr2 = ctknota_cmd2.ExecuteReader();

                while (ctknota_rdr2.Read())
                {
                    //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                    //cbo_ctknota_jenispaket.Items.Add(ctknota_rdr.GetString(0));
                    extra = ctknota_rdr2.GetInt32(1);
                    tamuhotel = ctknota_rdr2.GetInt32(2);
                }
                ctknota_rdr2.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            ctknota_conn2.Close();
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
            DS.Tables.Clear();

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
                        
                        mySqlDataAdapter.Fill(DS);
                        DS.Tables[0].Columns.Remove("id_paket");
                        DS.Tables[0].Columns.Remove("jenis_paket");
                        DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                        //int i = 0;
                        //int[] listI = new int[1];
                        //listI[0] = 100000;
                        //foreach (int somevalue in listI)
                        //{
                        //    DS.Tables[0].Rows[i++][1] = somevalue;  //Add value to second column of each row
                        //}
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            DS.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                        }

                        DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                        DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                        DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                        dgv_ctknota_tabelhrgpkt.DataSource = DS.Tables[0];
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Pilih"].Width = 70;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].Width = 300;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].Width = 200;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                        MessageBox.Show("Error Occurred");
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
                        ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = '" + cbo_ctknota_jenispaket.SelectedItem + "'";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(ctknota_query, ctknota_conn);
                        MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                        mySqlDataAdapter.Fill(DS);
                        DS.Tables[0].Columns.Remove("id_paket");
                        DS.Tables[0].Columns.Remove("jenis_paket");
                        DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        //DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                        //for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        //{
                        //    DS.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                        //}

                        DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                        DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                        DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                        dgv_ctknota_tabelhrgpkt.DataSource = DS.Tables[0];
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Pilih"].Width = 70;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].Width = 300;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].Width = 200;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Paket"].ReadOnly = true;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].Width = 200;
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                        MessageBox.Show("Error Occurred");
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

                        ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = '" + cbo_ctknota_jenispaket.SelectedItem + "'";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(ctknota_query, ctknota_conn);
                        MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                        mySqlDataAdapter.Fill(DS);
                        DS.Tables[0].Columns.Remove("id_paket");
                        DS.Tables[0].Columns.Remove("jenis_paket");
                        DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                        //int i = 0;
                        //int[] listI = new int[1];
                        //listI[0] = 100000;
                        //foreach (int somevalue in listI)
                        //{
                        //    DS.Tables[0].Rows[i++][1] = somevalue;  //Add value to second column of each row
                        //}
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            DS.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                        }

                        DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                        DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                        DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                        dgv_ctknota_tabelhrgpkt.DataSource = DS.Tables[0];
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Pilih"].Width = 70;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].Width = 300;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].Width = 200;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                        MessageBox.Show("Error Occurred");
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

                        ctknota_query = "SELECT * FROM `paket` WHERE `jenis_paket` = '" + cbo_ctknota_jenispaket.SelectedItem + "'";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(ctknota_query, ctknota_conn);
                        MySqlCommand ctknota_cmd = new MySqlCommand(ctknota_query, ctknota_conn);
                        mySqlDataAdapter.Fill(DS);
                        DS.Tables[0].Columns.Remove("id_paket");
                        DS.Tables[0].Columns.Remove("jenis_paket");
                        DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        //DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                        //for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        //{
                        //    DS.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                        //}

                        DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                        DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                        DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                        dgv_ctknota_tabelhrgpkt.DataSource = DS.Tables[0];
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Pilih"].Width = 70;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].Width = 300;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].Width = 200;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Paket"].ReadOnly = true;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].Width = 200;
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                        MessageBox.Show("Error Occurred");
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
                if (DS.Tables[0].Columns.Count == 7)
                {
                    DS.Tables[0].Columns.Remove("Nominal Extra");
                }
                else if (DS.Tables[0].Columns.Count == 7)
                {
                    DS.Tables[0].Columns.Remove("Nominal Extra");
                }
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
            //    if (dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly == false)
            //    {
            //        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
            //        {
            //            if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].ReadOnly == false)
            //            {
            //                MessageBox.Show("Test");
            //            }

            //        }
            //    }
                
            //}
            ////else if (dgv_ctknota_tabelhrgpkt.CurrentCell.OwningColumn.Name == "Extra")
            ////{
            ////    for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
            ////    {
            ////        if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Selected)
            ////        {
            ////            //if (DS.Tables[0].Columns.Count == 7)
            ////            //{
            ////            //    DS.Tables[0].Columns.Remove("Nominal Extra");
            ////            //}
            ////            //else if (DS.Tables[0].Columns.Count == 7)
            ////            //{
            ////            //    DS.Tables[0].Columns.Remove("Nominal Extra");
            ////            //}
            ////            //else
            ////            //{
            ////            //    if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "True")
            ////            //    {
            ////            //        dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value = false;
            ////            //    }
            ////            //    else if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "False")
            ////            //    {
            ////            //        dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value = true;
            ////            //    }
            ////            //    DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
            ////            //    for (int ii = 0; ii < DS.Tables[0].Rows.Count; ii++)
            ////            //    {
            ////            //        DS.Tables[0].Rows[ii]["Nominal Extra"] = extra;
            ////            //    }
            ////            //    if (rdo_ctknota_hotel.Checked)
            ////            //    {
            ////            //        DS.Tables[0].Columns["Nominal Extra"].SetOrdinal(6);
            ////            //    }
            ////            //    else if (rdo_ctknota_biasa.Checked)
            ////            //    {
            ////            //        DS.Tables[0].Columns["Nominal Extra"].SetOrdinal(5);
            ////            //    }


            ////            //    dgv_ctknota_tabelhrgpkt.DataSource = DS.Tables[0];
            ////            //    dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
            ////            //    dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ////            //    dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
            ////        }
            ////        else
            ////        {
            ////            dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].ReadOnly = true;
            ////        }
            //        //if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value.ToString() == "True")
            //        //{
            //            //if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].ReadOnly == false && dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Selected)
            //            //{
            //                //if (DS.Tables[0].Columns.Count == 7)
            //                //{
            //                //    DS.Tables[0].Columns.Remove("Nominal Extra");
            //                //}
            //                //else if (DS.Tables[0].Columns.Count == 7)
            //                //{
            //                //    DS.Tables[0].Columns.Remove("Nominal Extra");
            //                //}
            //                //else
            //                //{
            //                //    if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "True")
            //                //    {
            //                //        dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value = false;
            //                //    }
            //                //    else if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "False")
            //                //    {
            //                //        dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value = true;
            //                //    }
            //                //    DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
            //                //    for (int ii = 0; ii < DS.Tables[0].Rows.Count; ii++)
            //                //    {
            //                //        DS.Tables[0].Rows[ii]["Nominal Extra"] = extra;
            //                //    }
            //                //    if (rdo_ctknota_hotel.Checked)
            //                //    {
            //                //        DS.Tables[0].Columns["Nominal Extra"].SetOrdinal(6);
            //                //    }
            //                //    else if (rdo_ctknota_biasa.Checked)
            //                //    {
            //                //        DS.Tables[0].Columns["Nominal Extra"].SetOrdinal(5);
            //                //    }


            //                //    dgv_ctknota_tabelhrgpkt.DataSource = DS.Tables[0];
            //                //    dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
            //                //    dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //                //    dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
            //                //}
            //            //}
            //        //}
            //    }
            //}
            
            
            

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
