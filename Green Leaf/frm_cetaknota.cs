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
        DataSet ctknota_DS = new DataSet();
        int ctknota_tamuhotel;
        int ctknota_extra;
        int ctknota_countExtraColumn;

        #region(Buat Windows Form tidak bisa dirubah posisinya)
        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }
        #endregion

        private void frm_cetaknota_Load(object sender, EventArgs e)
        {
            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = false;
            rdo_ctknota_midnight.Enabled = false;
            rdo_ctknota_hotel.Enabled = false;
            rdo_ctknota_biasa.Enabled = false;
            cbo_ctknota_jenispaket.Enabled = false;
            dgv_ctknota_tabelhrgpkt.Enabled = false;
            rdo_ctknota_cash.Enabled = false;
            rdo_ctknota_credit.Enabled = false;
            cbo_ctknota_kodeterapis.Enabled = false;
            txt_ctknota_namaterapis.Enabled = false;
            txt_ctknota_diskon.Enabled = false;
            txt_ctknota_ket.Enabled = false;
            txt_ctknota_fee.Enabled = false;
            
            btn_ctknota_cetak.Enabled = false;
            btn_ctknota_batal.Enabled = true;

            

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
                    ctknota_extra = ctknota_rdr2.GetInt32(1);
                    ctknota_tamuhotel = ctknota_rdr2.GetInt32(2);
                }
                ctknota_rdr2.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            ctknota_conn2.Close();
            #endregion

            cbo_ctknota_kodeterapis.Items.Clear();
            #region(Select)
            string ctknota_query3;
            string ctknota_connStr3 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection ctknota_conn3 = new MySqlConnection(ctknota_connStr3);
            try
            {
                ctknota_conn3.Open();

                ctknota_query3 = "SELECT * FROM `terapis` WHERE `status_terapis` = 'Aktif'";
                MySqlCommand ctknota_cmd3 = new MySqlCommand(ctknota_query3, ctknota_conn3);
                MySqlDataReader ctknota_rdr3 = ctknota_cmd3.ExecuteReader();

                while (ctknota_rdr3.Read())
                {
                    //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                    cbo_ctknota_kodeterapis.Items.Add(ctknota_rdr3.GetString(1));
                }
                ctknota_rdr3.Close();
            }
            catch (Exception ex)
            {
                string Hasilex = ex.ToString();
                MessageBox.Show("Error Occured");
            }
            ctknota_conn3.Close();
            #endregion
        }

        private void rdo_ctknota_biasa_CheckedChanged(object sender, EventArgs e)
        {
            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = false;
            rdo_ctknota_cash.Enabled = false;
            rdo_ctknota_credit.Enabled = false;
            cbo_ctknota_kodeterapis.Enabled = false;
            txt_ctknota_namaterapis.Enabled = false;
            txt_ctknota_diskon.Enabled = false;
            txt_ctknota_ket.Enabled = false;
            txt_ctknota_fee.Enabled = false;
            btn_ctknota_batal.Enabled = false;

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
            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = false;
            rdo_ctknota_cash.Enabled = false;
            rdo_ctknota_credit.Enabled = false;
            cbo_ctknota_kodeterapis.Enabled = false;
            txt_ctknota_namaterapis.Enabled = false;
            txt_ctknota_diskon.Enabled = false;
            txt_ctknota_ket.Enabled = false;
            txt_ctknota_fee.Enabled = false;
            btn_ctknota_batal.Enabled = false;
            

            cbo_ctknota_jenispaket.Items.Clear();
            #region(Select)
            string ctknota_query;
            string ctknota_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection ctknota_conn = new MySqlConnection(ctknota_connStr);
            List<string> ctknota_lstKode = new List<string>();
            try
            {
                ctknota_conn.Open();

                ctknota_query = "SELECT DISTINCT `jenis_paket` FROM `paket` WHERE `jenis_paket` NOT IN (SELECT DISTINCT `jenis_paket` FROM `paket` WHERE `jenis_paket`='Deluxe')";
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
            //dtEmp.Columns.Add("Tamu Hotel", typeof(string));
        }

        private void rdo_ctknota_normal_CheckedChanged(object sender, EventArgs e)
        {
            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = false;
            dgv_ctknota_tabelhrgpkt.Enabled = false;
            rdo_ctknota_cash.Enabled = false;
            rdo_ctknota_credit.Enabled = false;
            cbo_ctknota_kodeterapis.Enabled = false;
            txt_ctknota_namaterapis.Enabled = false;
            txt_ctknota_diskon.Enabled = false;
            txt_ctknota_ket.Enabled = false;
            txt_ctknota_fee.Enabled = false;
            btn_ctknota_batal.Enabled = false;
        }

        private void rdo_ctknota_midnight_CheckedChanged(object sender, EventArgs e)
        {
            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = false;
            dgv_ctknota_tabelhrgpkt.Enabled = false;
            rdo_ctknota_cash.Enabled = false;
            rdo_ctknota_credit.Enabled = false;
            cbo_ctknota_kodeterapis.Enabled = false;
            txt_ctknota_namaterapis.Enabled = false;
            txt_ctknota_diskon.Enabled = false;
            txt_ctknota_ket.Enabled = false;
            txt_ctknota_fee.Enabled = false;
            btn_ctknota_batal.Enabled = false;
        }

        private void cbo_ctknota_jenispaket_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_ctknota_fee.Focus();

            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = true;
            rdo_ctknota_cash.Enabled = true;
            rdo_ctknota_credit.Enabled = true;
            cbo_ctknota_kodeterapis.Enabled = false;
            txt_ctknota_namaterapis.Enabled = false;
            txt_ctknota_diskon.Enabled = false;
            txt_ctknota_ket.Enabled = false;
            txt_ctknota_fee.Enabled = false;
            btn_ctknota_batal.Enabled = false;

            //dtEmp.Columns.Add("Nama Paket", typeof(string));
            //dtEmp.Columns.Add("Durasi Paket", typeof(string));
            //dtEmp.Columns.Add("Harga Paket", typeof(string));
            //dtEmp.Columns.Add("Extra", typeof(bool));
            //dtEmp.Columns.Add("Pilih", typeof(bool));

            dgv_ctknota_tabelhrgpkt.DataSource = null;
            dgv_ctknota_tabelhrgpkt.Rows.Clear();
            dgv_ctknota_tabelhrgpkt.Refresh();
            ctknota_DS.Tables.Clear();
            ctknota_countExtraColumn = 0;

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
                        
                        mySqlDataAdapter.Fill(ctknota_DS);
                        ctknota_DS.Tables[0].Columns.Remove("id_paket");
                        ctknota_DS.Tables[0].Columns.Remove("jenis_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        ctknota_DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        ctknota_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        ctknota_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                        for (int i = 0; i < ctknota_DS.Tables[0].Rows.Count; i++)
                        {
                            ctknota_DS.Tables[0].Rows[i]["Tamu Hotel"] = ctknota_tamuhotel;
                        }

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                        ctknota_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                        for (int ii = 0; ii < ctknota_DS.Tables[0].Rows.Count; ii++)
                        {
                            ctknota_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                        }

                        dgv_ctknota_tabelhrgpkt.DataSource = ctknota_DS.Tables[0];

                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;
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
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;

                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        
                        List<string> lstExtra = new List<string>();
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString())/100;
                            int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            double hasil = nominal * Convert.ToDouble(harga);
                            lstExtra.Add(hasil.ToString());
                            //MessageBox.Show(hasilfinal.ToString());
                            //foreach (char c in extra)
                            //{
                            //    if (char.IsDigit(c))
                            //    {
                            //        countdigitextra++;
                            //    }
                            //}
                            //int digit = countdigitextra;
                            //while (digit > 3)
                            //{
                            //    digit -= 3;
                            //    extra = extra.Insert(digit, ".");
                            //    dsCloned.Tables[0].Rows[i]["Nominal Extra"] = extra;
                            //}

                        }

                        DataSet dsCloned = ctknota_DS.Clone();

                        dsCloned.Tables[0].Columns["Harga Paket"].DataType = typeof(string);
                        dsCloned.Tables[0].Columns["Tamu Hotel"].DataType = typeof(string);
                        dsCloned.Tables[0].Columns["Nominal Extra"].DataType = typeof(string);
                        foreach (DataRow row in ctknota_DS.Tables[0].Rows)
                        {
                            dsCloned.Tables[0].ImportRow(row);
                        }

                        dgv_ctknota_tabelhrgpkt.DataSource = dsCloned.Tables[0];

                        int countdigitharga = 0;
                        string hargapaket;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitharga = 0;
                            hargapaket = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString();
                            foreach (char c in hargapaket)
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitharga++;
                                }
                            }
                            int digit = countdigitharga;
                            while (digit>3)
	                        {
                                digit -= 3;
	                            hargapaket = hargapaket.Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Harga Paket"] = hargapaket;
	                        }

                        }

                        int countdigittamu = 0;
                        string tamuhotel;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigittamu = 0;
                            tamuhotel = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString();
                            foreach (char c in tamuhotel)
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigittamu++;
                                }
                            }
                            int digit = countdigittamu;
                            while (digit > 3)
                            {
                                digit -= 3;
                                tamuhotel = tamuhotel.Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                            }

                        }

                        int countdigitextra = 0;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitextra = 0;
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            //double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            //int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //double hasil = nominal * Convert.ToDouble(harga);
                            //extra = hasil.ToString();
                            //MessageBox.Show(hasilfinal.ToString());
                            foreach (char c in lstExtra[i])
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitextra++;
                                }
                            }
                            int digit = countdigitextra;
                            while (digit > 3)
                            {
                                digit -= 3;
                                lstExtra[i] = lstExtra[i].Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Nominal Extra"] = lstExtra[i];
                            }

                        }

                        
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
                        mySqlDataAdapter.Fill(ctknota_DS);
                        ctknota_DS.Tables[0].Columns.Remove("id_paket");
                        ctknota_DS.Tables[0].Columns.Remove("jenis_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        ctknota_DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        ctknota_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        ctknota_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        //DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                        //for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        //{
                        //    DS.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                        //}

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                        ctknota_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                        dgv_ctknota_tabelhrgpkt.DataSource = ctknota_DS.Tables[0];
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

                        

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                        for (int ii = 0; ii < ctknota_DS.Tables[0].Rows.Count; ii++)
                        {
                            ctknota_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                        }
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;

                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].SortMode = DataGridViewColumnSortMode.NotSortable;

                        List<string> lstExtra = new List<string>();
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            double hasil = nominal * Convert.ToDouble(harga);
                            lstExtra.Add(hasil.ToString());
                            //MessageBox.Show(hasilfinal.ToString());
                            //foreach (char c in extra)
                            //{
                            //    if (char.IsDigit(c))
                            //    {
                            //        countdigitextra++;
                            //    }
                            //}
                            //int digit = countdigitextra;
                            //while (digit > 3)
                            //{
                            //    digit -= 3;
                            //    extra = extra.Insert(digit, ".");
                            //    dsCloned.Tables[0].Rows[i]["Nominal Extra"] = extra;
                            //}

                        }

                        DataSet dsCloned = ctknota_DS.Clone();

                        dsCloned.Tables[0].Columns["Harga Paket"].DataType = typeof(string);
                        //dsCloned.Tables[0].Columns["Tamu Hotel"].DataType = typeof(string);
                        dsCloned.Tables[0].Columns["Nominal Extra"].DataType = typeof(string);
                        foreach (DataRow row in ctknota_DS.Tables[0].Rows)
                        {
                            dsCloned.Tables[0].ImportRow(row);
                        }

                        dgv_ctknota_tabelhrgpkt.DataSource = dsCloned.Tables[0];

                        int countdigitharga = 0;
                        string hargapaket;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitharga = 0;
                            hargapaket = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString();
                            foreach (char c in hargapaket)
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitharga++;
                                }
                            }
                            int digit = countdigitharga;
                            while (digit > 3)
                            {
                                digit -= 3;
                                hargapaket = hargapaket.Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Harga Paket"] = hargapaket;
                            }

                        }

                        //int countdigittamu = 0;
                        //string tamuhotel;
                        //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        //{
                        //    countdigittamu = 0;
                        //    tamuhotel = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString();
                        //    foreach (char c in tamuhotel)
                        //    {
                        //        if (char.IsDigit(c))
                        //        {
                        //            countdigittamu++;
                        //        }
                        //    }
                        //    int digit = countdigittamu;
                        //    while (digit > 3)
                        //    {
                        //        digit -= 3;
                        //        tamuhotel = tamuhotel.Insert(digit, ".");
                        //        dsCloned.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                        //    }

                        //}

                        int countdigitextra = 0;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitextra = 0;
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            //double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            //int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //double hasil = nominal * Convert.ToDouble(harga);
                            //extra = hasil.ToString();
                            //MessageBox.Show(hasilfinal.ToString());
                            foreach (char c in lstExtra[i])
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitextra++;
                                }
                            }
                            int digit = countdigitextra;
                            while (digit > 3)
                            {
                                digit -= 3;
                                lstExtra[i] = lstExtra[i].Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Nominal Extra"] = lstExtra[i];
                            }

                        }
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
                        mySqlDataAdapter.Fill(ctknota_DS);
                        ctknota_DS.Tables[0].Columns.Remove("id_paket");
                        ctknota_DS.Tables[0].Columns.Remove("jenis_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        ctknota_DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        ctknota_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        ctknota_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                        //int i = 0;
                        //int[] listI = new int[1];
                        //listI[0] = 100000;
                        //foreach (int somevalue in listI)
                        //{
                        //    DS.Tables[0].Rows[i++][1] = somevalue;  //Add value to second column of each row
                        //}
                        for (int i = 0; i < ctknota_DS.Tables[0].Rows.Count; i++)
                        {
                            ctknota_DS.Tables[0].Rows[i]["Tamu Hotel"] = ctknota_tamuhotel;
                        }

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                        ctknota_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                        dgv_ctknota_tabelhrgpkt.DataSource = ctknota_DS.Tables[0];
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

                        

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                        for (int ii = 0; ii < ctknota_DS.Tables[0].Rows.Count; ii++)
                        {
                            ctknota_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                        }
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;

                        List<string> lstExtra = new List<string>();
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            double hasil = nominal * Convert.ToDouble(harga);
                            lstExtra.Add(hasil.ToString());
                            //MessageBox.Show(hasilfinal.ToString());
                            //foreach (char c in extra)
                            //{
                            //    if (char.IsDigit(c))
                            //    {
                            //        countdigitextra++;
                            //    }
                            //}
                            //int digit = countdigitextra;
                            //while (digit > 3)
                            //{
                            //    digit -= 3;
                            //    extra = extra.Insert(digit, ".");
                            //    dsCloned.Tables[0].Rows[i]["Nominal Extra"] = extra;
                            //}

                        }

                        DataSet dsCloned = ctknota_DS.Clone();

                        dsCloned.Tables[0].Columns["Harga Paket"].DataType = typeof(string);
                        dsCloned.Tables[0].Columns["Tamu Hotel"].DataType = typeof(string);
                        dsCloned.Tables[0].Columns["Nominal Extra"].DataType = typeof(string);

                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        foreach (DataRow row in ctknota_DS.Tables[0].Rows)
                        {
                            dsCloned.Tables[0].ImportRow(row);
                        }

                        dgv_ctknota_tabelhrgpkt.DataSource = dsCloned.Tables[0];

                        int countdigitharga = 0;
                        string hargapaket;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitharga = 0;
                            hargapaket = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString();
                            foreach (char c in hargapaket)
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitharga++;
                                }
                            }
                            int digit = countdigitharga;
                            while (digit > 3)
                            {
                                digit -= 3;
                                hargapaket = hargapaket.Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Harga Paket"] = hargapaket;
                            }

                        }

                        int countdigittamu = 0;
                        string tamuhotel;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigittamu = 0;
                            tamuhotel = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString();
                            foreach (char c in tamuhotel)
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigittamu++;
                                }
                            }
                            int digit = countdigittamu;
                            while (digit > 3)
                            {
                                digit -= 3;
                                tamuhotel = tamuhotel.Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                            }

                        }

                        int countdigitextra = 0;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitextra = 0;
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            //double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            //int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //double hasil = nominal * Convert.ToDouble(harga);
                            //extra = hasil.ToString();
                            //MessageBox.Show(hasilfinal.ToString());
                            foreach (char c in lstExtra[i])
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitextra++;
                                }
                            }
                            int digit = countdigitextra;
                            while (digit > 3)
                            {
                                digit -= 3;
                                lstExtra[i] = lstExtra[i].Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Nominal Extra"] = lstExtra[i];
                            }

                        }
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
                        mySqlDataAdapter.Fill(ctknota_DS);
                        ctknota_DS.Tables[0].Columns.Remove("id_paket");
                        ctknota_DS.Tables[0].Columns.Remove("jenis_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                        ctknota_DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                        ctknota_DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                        ctknota_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                        ctknota_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                        //DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                        //for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        //{
                        //    DS.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                        //}

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                        ctknota_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                        dgv_ctknota_tabelhrgpkt.DataSource = ctknota_DS.Tables[0];
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

                        

                        ctknota_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                        for (int ii = 0; ii < ctknota_DS.Tables[0].Rows.Count; ii++)
                        {
                            ctknota_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                        }
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;
                        dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].SortMode = DataGridViewColumnSortMode.NotSortable;

                        List<string> lstExtra = new List<string>();
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            double hasil = nominal * Convert.ToDouble(harga);
                            lstExtra.Add(hasil.ToString());
                            //MessageBox.Show(hasilfinal.ToString());
                            //foreach (char c in extra)
                            //{
                            //    if (char.IsDigit(c))
                            //    {
                            //        countdigitextra++;
                            //    }
                            //}
                            //int digit = countdigitextra;
                            //while (digit > 3)
                            //{
                            //    digit -= 3;
                            //    extra = extra.Insert(digit, ".");
                            //    dsCloned.Tables[0].Rows[i]["Nominal Extra"] = extra;
                            //}

                        }

                        DataSet dsCloned = ctknota_DS.Clone();

                        dsCloned.Tables[0].Columns["Harga Paket"].DataType = typeof(string);
                        //dsCloned.Tables[0].Columns["Tamu Hotel"].DataType = typeof(string);
                        dsCloned.Tables[0].Columns["Nominal Extra"].DataType = typeof(string);
                        foreach (DataRow row in ctknota_DS.Tables[0].Rows)
                        {
                            dsCloned.Tables[0].ImportRow(row);
                        }

                        dgv_ctknota_tabelhrgpkt.DataSource = dsCloned.Tables[0];

                        int countdigitharga = 0;
                        string hargapaket;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitharga = 0;
                            hargapaket = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString();
                            foreach (char c in hargapaket)
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitharga++;
                                }
                            }
                            int digit = countdigitharga;
                            while (digit > 3)
                            {
                                digit -= 3;
                                hargapaket = hargapaket.Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Harga Paket"] = hargapaket;
                            }

                        }

                        //int countdigittamu = 0;
                        //string tamuhotel;
                        //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        //{
                        //    countdigittamu = 0;
                        //    tamuhotel = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString();
                        //    foreach (char c in tamuhotel)
                        //    {
                        //        if (char.IsDigit(c))
                        //        {
                        //            countdigittamu++;
                        //        }
                        //    }
                        //    int digit = countdigittamu;
                        //    while (digit > 3)
                        //    {
                        //        digit -= 3;
                        //        tamuhotel = tamuhotel.Insert(digit, ".");
                        //        dsCloned.Tables[0].Rows[i]["Tamu Hotel"] = tamuhotel;
                        //    }

                        //}

                        int countdigitextra = 0;
                        for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                        {
                            countdigitextra = 0;
                            //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //extra = Convert.ToString());
                            //double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                            //int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                            //double hasil = nominal * Convert.ToDouble(harga);
                            //extra = hasil.ToString();
                            //MessageBox.Show(hasilfinal.ToString());
                            foreach (char c in lstExtra[i])
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigitextra++;
                                }
                            }
                            int digit = countdigitextra;
                            while (digit > 3)
                            {
                                digit -= 3;
                                lstExtra[i] = lstExtra[i].Insert(digit, ".");
                                dsCloned.Tables[0].Rows[i]["Nominal Extra"] = lstExtra[i];
                            }

                        }
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
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = true;
            rdo_ctknota_cash.Enabled = true;
            rdo_ctknota_credit.Enabled = true;
            cbo_ctknota_kodeterapis.Enabled = true;
            txt_ctknota_namaterapis.Enabled = true;
            txt_ctknota_diskon.Enabled = true;
            txt_ctknota_ket.Enabled = true;
            txt_ctknota_fee.Enabled = true;
            txt_ctknota_nomorruangan.Enabled = true;
             
            btn_ctknota_cetak.Enabled = true;
            btn_ctknota_batal.Enabled = true;
        }

        private void rdo_ctknota_credit_CheckedChanged(object sender, EventArgs e)
        {
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = true;
            rdo_ctknota_cash.Enabled = true;
            rdo_ctknota_credit.Enabled = true;
            cbo_ctknota_kodeterapis.Enabled = true;
            txt_ctknota_namaterapis.Enabled = true;
            txt_ctknota_diskon.Enabled = true;
            txt_ctknota_ket.Enabled = true;
            txt_ctknota_fee.Enabled = true;
            txt_ctknota_nomorruangan.Enabled = true;
             
            btn_ctknota_cetak.Enabled = true;
            btn_ctknota_batal.Enabled = true;
        }

        private void cbo_ctknota_kodeterapis_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = true;
            rdo_ctknota_cash.Enabled = true;
            rdo_ctknota_credit.Enabled = true;
            cbo_ctknota_kodeterapis.Enabled = true;
            txt_ctknota_namaterapis.Enabled = true;
            txt_ctknota_diskon.Enabled = true;
            txt_ctknota_ket.Enabled = true;
            txt_ctknota_fee.Enabled = true;
            txt_ctknota_nomorruangan.Enabled = true;
            btn_ctknota_batal.Enabled = true;

            #region(Select)
            string ctknota_query3;
            string ctknota_connStr3 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection ctknota_conn3 = new MySqlConnection(ctknota_connStr3);
            try
            {
                ctknota_conn3.Open();

                ctknota_query3 = "SELECT * FROM `terapis` WHERE `kode_terapis` = '"+cbo_ctknota_kodeterapis.SelectedItem+"'";
                MySqlCommand ctknota_cmd3 = new MySqlCommand(ctknota_query3, ctknota_conn3);
                MySqlDataReader ctknota_rdr3 = ctknota_cmd3.ExecuteReader();

                while (ctknota_rdr3.Read())
                {
                    //cbo_kodeterapis.Items.Add(edttrps_rdr.GetString(1));
                    txt_ctknota_namaterapis.Text = ctknota_rdr3.GetString(2);
                }
                ctknota_rdr3.Close();
            }
            catch (Exception ex)
            {
                string Hasilex = ex.ToString();
                MessageBox.Show("Error Occured");
            }
            ctknota_conn3.Close();
            #endregion
        }

        private void txt_ctknota_diskon_TextChanged(object sender, EventArgs e)
        {
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = true;
            rdo_ctknota_biasa.Enabled = true;
            cbo_ctknota_jenispaket.Enabled = true;
            dgv_ctknota_tabelhrgpkt.Enabled = true;
            rdo_ctknota_cash.Enabled = true;
            rdo_ctknota_credit.Enabled = true;
            cbo_ctknota_kodeterapis.Enabled = true;
            txt_ctknota_namaterapis.Enabled = true;
            txt_ctknota_diskon.Enabled = true;
            txt_ctknota_ket.Enabled = true;
            txt_ctknota_fee.Enabled = true;
            txt_ctknota_nomorruangan.Enabled = true;
            btn_ctknota_batal.Enabled = true;
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
            if (cbo_ctknota_kodeterapis.SelectedItem == null)
            {
                MessageBox.Show("Mohon pilih kode terapis terlebih dahulu terlebih dahulu");
            }
            else if (txt_ctknota_nomorruangan.Text == "")
            {
                MessageBox.Show("Mohon lengkapi kolom nomor ruangan terlebih dahulu");
            }
            else
            {
                if (txt_ctknota_diskon.Text == "")
                    {
                        if (txt_ctknota_fee.Text == "")
                        {
                            #region(Insert Nota)
                            string tanggalcetak = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() +" "+ DateTime.Now.Hour.ToString() +":"+ DateTime.Now.Minute.ToString() +":"+ DateTime.Now.Second.ToString();
                            string jamkerja = "";
                            string tamuhotel = "";
                            int potonganhotel = 0; ;
                            int nomorruangan =0;
                            string namapaket="";
                            int hargapaket =0;
                            string extra = "";
                            int nominalextra = 0;
                            int kodeterapis =0;
                            string namaterapis ="";
                            int diskon = 0;
                            string ket = "";
                            int fee = 0;
                            int totalbayar=0;
                            string jenisbayar = "";
                            //header.Trim(new Char[] { ' ', '*', '.' });
                            for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                            {
                                if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value.ToString() == "True")
                                {
                                    if (rdo_ctknota_normal.Checked)
                                    {
                                        jamkerja = "Normal";
                                    }
                                    else if (rdo_ctknota_midnight.Checked)
                                    {
                                        jamkerja = "Midnight";
                                    }
                                    if (rdo_ctknota_biasa.Checked)
                                    {
                                        tamuhotel = "Tidak";
                                        potonganhotel = 0;
                                    }
                                    else if (rdo_ctknota_hotel.Checked)
                                    {
                                        tamuhotel = "Ya";
                                        potonganhotel = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString().Replace(".", string.Empty));
                                    }
                                    nomorruangan = int.Parse(txt_ctknota_nomorruangan.Text);
                                    namapaket = cbo_ctknota_jenispaket.SelectedItem+" - "+dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nama Paket"].Value.ToString();
                                    hargapaket = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString().Replace(".", string.Empty));
                                    if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString()=="True")
                                    {
                                        extra = "Ya";
                                        nominalextra = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString().Replace(".", string.Empty));
                                    }
                                    else if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString()=="False")
                                    {
                                        extra = "Tidak";
                                        nominalextra = 0;
                                    }
                                    kodeterapis = int.Parse(cbo_ctknota_kodeterapis.SelectedItem.ToString());
                                    namaterapis = txt_ctknota_namaterapis.Text;
                                    diskon = 0;
                                    ket = "";
                                    fee = 0;
                                    totalbayar = hargapaket - potonganhotel + nominalextra;
                                    if (rdo_ctknota_cash.Checked)
                                    {
                                        jenisbayar = "Cash";
                                    }
                                    else if (rdo_ctknota_credit.Checked)
                                    {
                                        jenisbayar = "Credit";
                                    }
                                }
                            }

                            DBConnect ctknota_sql = new DBConnect();
                            string ctknota_query = "INSERT INTO `nota` (`id_nota`, `tanggalcetak_nota`, `nomorruangan_nota`, `jamkerja_nota`, `tamuhotel_nota`, `potonganhotel_nota`, "
                                                        +"`namapaket_nota`, `hargapaket_nota`, `extra_nota`, `nominalextra_nota`, `kodeterapis_nota`, `namaterapis_nota`,"
                                                            +" `diskon_nota`, `keterangan_nota`, `totalbayar_nota`, `feeterapis_nota`, `jenisbayar_nota`, `status_nota`) "
                                                                +"VALUES (NULL, '"+tanggalcetak+"', '"+nomorruangan+"', '"+jamkerja+"', '"+tamuhotel+"', '"+potonganhotel+"',"
                                                                    +" '"+namapaket+"', '"+hargapaket+"', '"+extra+"', '"+nominalextra+"', '"+kodeterapis+"', '"+namaterapis+"', "
                                                                        +"'"+diskon+"', '"+ket+"', '"+totalbayar+"', '"+fee+"', '"+jenisbayar+"', '-');";
                            ctknota_sql.Insert(ctknota_query);

                            MessageBox.Show("Nota telah berhasil ditambahkan");
                            #endregion
                            string totalbayarFinal = "";
                            int countdigittotal = 0;
                            foreach (char c in totalbayar.ToString())
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigittotal++;
                                }
                            }
                            int digit = countdigittotal;
                            int countdigitend = 0;
                            while (digit > 3)
                            {
                                countdigitend++;
                                digit -= 3;
                                totalbayarFinal = totalbayar.ToString().Insert(digit,".");
                            }
                            totalbayarFinal = totalbayarFinal.Insert(countdigittotal + countdigitend, ",-");
                            lbl_ctknota_totalbyr.Text = totalbayarFinal;
                            //MessageBox.Show("diskon kosong, fee kosong");
                        }
                        else
                        {
                            #region(Insert Nota)
                            string tanggalcetak = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                            string jamkerja = "";
                            string tamuhotel = "";
                            int potonganhotel = 0; ;
                            int nomorruangan = 0;
                            string namapaket = "";
                            int hargapaket = 0;
                            string extra = "";
                            int nominalextra = 0;
                            int kodeterapis = 0;
                            string namaterapis = "";
                            int diskon = 0;
                            string ket = "";
                            int fee = 0;
                            int totalbayar = 0;
                            string jenisbayar = "";
                            //header.Trim(new Char[] { ' ', '*', '.' });
                            for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                            {
                                if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value.ToString() == "True")
                                {
                                    if (rdo_ctknota_normal.Checked)
                                    {
                                        jamkerja = "Normal";
                                    }
                                    else if (rdo_ctknota_midnight.Checked)
                                    {
                                        jamkerja = "Midnight";
                                    }
                                    if (rdo_ctknota_biasa.Checked)
                                    {
                                        tamuhotel = "Tidak";
                                        potonganhotel = 0;
                                    }
                                    else if (rdo_ctknota_hotel.Checked)
                                    {
                                        tamuhotel = "Ya";
                                        potonganhotel = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString().Replace(".", string.Empty));
                                    }
                                    nomorruangan = int.Parse(txt_ctknota_nomorruangan.Text);
                                    namapaket = cbo_ctknota_jenispaket.SelectedItem + " - " + dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nama Paket"].Value.ToString();
                                    hargapaket = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString().Replace(".", string.Empty));
                                    if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "True")
                                    {
                                        extra = "Ya";
                                        nominalextra = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString().Replace(".", string.Empty));
                                    }
                                    else if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "False")
                                    {
                                        extra = "Tidak";
                                        nominalextra = 0;
                                    }
                                    kodeterapis = int.Parse(cbo_ctknota_kodeterapis.SelectedItem.ToString());
                                    namaterapis = txt_ctknota_namaterapis.Text;
                                    diskon = 0;
                                    ket = "";
                                    fee = int.Parse(txt_ctknota_fee.Text);
                                    totalbayar = hargapaket - potonganhotel + nominalextra;
                                    if (rdo_ctknota_cash.Checked)
                                    {
                                        jenisbayar = "Cash";
                                    }
                                    else if (rdo_ctknota_credit.Checked)
                                    {
                                        jenisbayar = "Credit";
                                    }
                                }
                            }

                            DBConnect ctknota_sql = new DBConnect();
                            string ctknota_query = "INSERT INTO `nota` (`id_nota`, `tanggalcetak_nota`, `nomorruangan_nota`, `jamkerja_nota`, `tamuhotel_nota`, `potonganhotel_nota`, "
                                                        + "`namapaket_nota`, `hargapaket_nota`, `extra_nota`, `nominalextra_nota`, `kodeterapis_nota`, `namaterapis_nota`,"
                                                            + " `diskon_nota`, `keterangan_nota`, `totalbayar_nota`, `feeterapis_nota`, `jenisbayar_nota`, `status_nota`) "
                                                                + "VALUES (NULL, '" + tanggalcetak + "', '" + nomorruangan + "', '" + jamkerja + "', '" + tamuhotel + "', '" + potonganhotel + "',"
                                                                    + " '" + namapaket + "', '" + hargapaket + "', '" + extra + "', '" + nominalextra + "', '" + kodeterapis + "', '" + namaterapis + "', "
                                                                        + "'" + diskon + "', '" + ket + "', '" + totalbayar + "', '" + fee + "', '" + jenisbayar + "', '-');";
                            ctknota_sql.Insert(ctknota_query);

                            MessageBox.Show("Nota telah berhasil ditambahkan");
                            #endregion
                            string totalbayarFinal = "";
                            int countdigittotal = 0;
                            foreach (char c in totalbayar.ToString())
                            {
                                if (char.IsDigit(c))
                                {
                                    countdigittotal++;
                                }
                            }
                            int digit = countdigittotal;
                            int countdigitend = 0;
                            while (digit > 3)
                            {
                                countdigitend++;
                                digit -= 3;
                                totalbayarFinal = totalbayar.ToString().Insert(digit, ".");
                            }
                            totalbayarFinal = totalbayarFinal.Insert(countdigittotal + countdigitend, ",-");
                            lbl_ctknota_totalbyr.Text = totalbayarFinal;
                            //MessageBox.Show("diskon kosong, fee ada");
                        }
                    }
                    else
                    {
                        if (txt_ctknota_ket.Text == "")
                        {
                            MessageBox.Show("Mohon lengkapi keterangan diskon terlebih dahulu");
                        }
                        else
                        {
                            if (txt_ctknota_fee.Text == "")
                            {
                                #region(Insert Nota)
                                string tanggalcetak = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                                string jamkerja = "";
                                string tamuhotel = "";
                                int potonganhotel = 0; ;
                                int nomorruangan = 0;
                                string namapaket = "";
                                int hargapaket = 0;
                                string extra = "";
                                int nominalextra = 0;
                                int kodeterapis = 0;
                                string namaterapis = "";
                                int diskon = 0;
                                string ket = "";
                                int fee = 0;
                                int totalbayar = 0;
                                string jenisbayar = "";
                                //header.Trim(new Char[] { ' ', '*', '.' });
                                for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                                {
                                    if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value.ToString() == "True")
                                    {
                                        if (rdo_ctknota_normal.Checked)
                                        {
                                            jamkerja = "Normal";
                                        }
                                        else if (rdo_ctknota_midnight.Checked)
                                        {
                                            jamkerja = "Midnight";
                                        }
                                        if (rdo_ctknota_biasa.Checked)
                                        {
                                            tamuhotel = "Tidak";
                                            potonganhotel = 0;
                                        }
                                        else if (rdo_ctknota_hotel.Checked)
                                        {
                                            tamuhotel = "Ya";
                                            potonganhotel = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString().Replace(".", string.Empty));
                                        }
                                        nomorruangan = int.Parse(txt_ctknota_nomorruangan.Text);
                                        namapaket = cbo_ctknota_jenispaket.SelectedItem + " - " + dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nama Paket"].Value.ToString();
                                        hargapaket = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString().Replace(".", string.Empty));
                                        if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "True")
                                        {
                                            extra = "Ya";
                                            nominalextra = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString().Replace(".", string.Empty));
                                        }
                                        else if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "False")
                                        {
                                            extra = "Tidak";
                                            nominalextra = 0;
                                        }
                                        kodeterapis = int.Parse(cbo_ctknota_kodeterapis.SelectedItem.ToString());
                                        namaterapis = txt_ctknota_namaterapis.Text;
                                        diskon = int.Parse(txt_ctknota_diskon.Text);
                                        ket = txt_ctknota_ket.Text; 
                                        fee = 0;
                                        totalbayar = hargapaket - potonganhotel + nominalextra;
                                        if (rdo_ctknota_cash.Checked)
                                        {
                                            jenisbayar = "Cash";
                                        }
                                        else if (rdo_ctknota_credit.Checked)
                                        {
                                            jenisbayar = "Credit";
                                        }
                                    }
                                }

                                DBConnect ctknota_sql = new DBConnect();
                                string ctknota_query = "INSERT INTO `nota` (`id_nota`, `tanggalcetak_nota`, `nomorruangan_nota`, `jamkerja_nota`, `tamuhotel_nota`, `potonganhotel_nota`, "
                                                            + "`namapaket_nota`, `hargapaket_nota`, `extra_nota`, `nominalextra_nota`, `kodeterapis_nota`, `namaterapis_nota`,"
                                                                + " `diskon_nota`, `keterangan_nota`, `totalbayar_nota`, `feeterapis_nota`, `jenisbayar_nota`, `status_nota`) "
                                                                    + "VALUES (NULL, '" + tanggalcetak + "', '" + nomorruangan + "', '" + jamkerja + "', '" + tamuhotel + "', '" + potonganhotel + "',"
                                                                        + " '" + namapaket + "', '" + hargapaket + "', '" + extra + "', '" + nominalextra + "', '" + kodeterapis + "', '" + namaterapis + "', "
                                                                            + "'" + diskon + "', '" + ket + "', '" + totalbayar + "', '" + fee + "', '" + jenisbayar + "', '-');";
                                ctknota_sql.Insert(ctknota_query);

                                MessageBox.Show("Nota telah berhasil ditambahkan");
                                #endregion
                                string totalbayarFinal = "";
                                int countdigittotal = 0;
                                foreach (char c in totalbayar.ToString())
                                {
                                    if (char.IsDigit(c))
                                    {
                                        countdigittotal++;
                                    }
                                }
                                int digit = countdigittotal;
                                int countdigitend = 0;
                                while (digit > 3)
                                {
                                    countdigitend++;
                                    digit -= 3;
                                    totalbayarFinal = totalbayar.ToString().Insert(digit, ".");
                                }
                                totalbayarFinal = totalbayarFinal.Insert(countdigittotal + countdigitend, ",-");
                                lbl_ctknota_totalbyr.Text = totalbayarFinal;
                                //MessageBox.Show("diskon ada, fee kosong");
                            }
                            else
                            {
                                #region(Insert Nota)
                                string tanggalcetak = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                                string jamkerja = "";
                                string tamuhotel = "";
                                int potonganhotel = 0; ;
                                int nomorruangan = 0;
                                string namapaket = "";
                                int hargapaket = 0;
                                string extra = "";
                                int nominalextra = 0;
                                int kodeterapis = 0;
                                string namaterapis = "";
                                int diskon = 0;
                                string ket = "";
                                int fee = 0;
                                int totalbayar = 0;
                                string jenisbayar = "";
                                //header.Trim(new Char[] { ' ', '*', '.' });
                                for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                                {
                                    if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value.ToString() == "True")
                                    {
                                        if (rdo_ctknota_normal.Checked)
                                        {
                                            jamkerja = "Normal";
                                        }
                                        else if (rdo_ctknota_midnight.Checked)
                                        {
                                            jamkerja = "Midnight";
                                        }
                                        if (rdo_ctknota_biasa.Checked)
                                        {
                                            tamuhotel = "Tidak";
                                            potonganhotel = 0;
                                        }
                                        else if (rdo_ctknota_hotel.Checked)
                                        {
                                            tamuhotel = "Ya";
                                            potonganhotel = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Tamu Hotel"].Value.ToString().Replace(".", string.Empty));
                                        }
                                        nomorruangan = int.Parse(txt_ctknota_nomorruangan.Text);
                                        namapaket = cbo_ctknota_jenispaket.SelectedItem + " - " + dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nama Paket"].Value.ToString();
                                        hargapaket = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString().Replace(".", string.Empty));
                                        if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "True")
                                        {
                                            extra = "Ya";
                                            nominalextra = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString().Replace(".", string.Empty));
                                        }
                                        else if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Value.ToString() == "False")
                                        {
                                            extra = "Tidak";
                                            nominalextra = 0;
                                        }
                                        kodeterapis = int.Parse(cbo_ctknota_kodeterapis.SelectedItem.ToString());
                                        namaterapis = txt_ctknota_namaterapis.Text;
                                        diskon = int.Parse(txt_ctknota_diskon.Text) ;
                                        ket = txt_ctknota_ket.Text;
                                        fee = int.Parse(txt_ctknota_fee.Text);
                                        totalbayar = hargapaket - potonganhotel + nominalextra;
                                        if (rdo_ctknota_cash.Checked)
                                        {
                                            jenisbayar = "Cash";
                                        }
                                        else if (rdo_ctknota_credit.Checked)
                                        {
                                            jenisbayar = "Credit";
                                        }
                                    }
                                }

                                DBConnect ctknota_sql = new DBConnect();
                                string ctknota_query = "INSERT INTO `nota` (`id_nota`, `tanggalcetak_nota`, `nomorruangan_nota`, `jamkerja_nota`, `tamuhotel_nota`, `potonganhotel_nota`, "
                                                            + "`namapaket_nota`, `hargapaket_nota`, `extra_nota`, `nominalextra_nota`, `kodeterapis_nota`, `namaterapis_nota`,"
                                                                + " `diskon_nota`, `keterangan_nota`, `totalbayar_nota`, `feeterapis_nota`, `jenisbayar_nota`, `status_nota`) "
                                                                    + "VALUES (NULL, '" + tanggalcetak + "', '" + nomorruangan + "', '" + jamkerja + "', '" + tamuhotel + "', '" + potonganhotel + "',"
                                                                        + " '" + namapaket + "', '" + hargapaket + "', '" + extra + "', '" + nominalextra + "', '" + kodeterapis + "', '" + namaterapis + "', "
                                                                            + "'" + diskon + "', '" + ket + "', '" + totalbayar + "', '" + fee + "', '" + jenisbayar + "', '-');";
                                ctknota_sql.Insert(ctknota_query);

                                MessageBox.Show("Nota telah berhasil ditambahkan");
                                #endregion
                                string totalbayarFinal = "";
                                int countdigittotal = 0;
                                foreach (char c in totalbayar.ToString())
                                {
                                    if (char.IsDigit(c))
                                    {
                                        countdigittotal++;
                                    }
                                }
                                int digit = countdigittotal;
                                int countdigitend = 0;
                                while (digit > 3)
                                {
                                    countdigitend++;
                                    digit -= 3;
                                    totalbayarFinal = totalbayar.ToString().Insert(digit, ".");
                                }
                                totalbayarFinal = totalbayarFinal.Insert(countdigittotal + countdigitend, ",-");
                                lbl_ctknota_totalbyr.Text = totalbayarFinal;
                                //MessageBox.Show("diskon ada, fee ada");
                            }
                        }
                    }
                
            }
        }

        private void txt_ctknota_nomorruangan_TextChanged(object sender, EventArgs e)
        {
            txt_ctknota_nomorruangan.Enabled = true;
            rdo_ctknota_normal.Enabled = true;
            rdo_ctknota_midnight.Enabled = true;
            rdo_ctknota_hotel.Enabled = false;
            rdo_ctknota_biasa.Enabled = false;
            cbo_ctknota_jenispaket.Enabled = false;
            dgv_ctknota_tabelhrgpkt.Enabled = false;
            rdo_ctknota_cash.Enabled = false;
            rdo_ctknota_credit.Enabled = false;
            cbo_ctknota_kodeterapis.Enabled = false;
            txt_ctknota_namaterapis.Enabled = false;
            txt_ctknota_diskon.Enabled = false;
            txt_ctknota_ket.Enabled = false;
            txt_ctknota_fee.Enabled = false;
            btn_ctknota_batal.Enabled = false;
        }

        private void dgv_ctknota_tabelhrgpkt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_ctknota_tabelhrgpkt.CurrentCell.OwningColumn.Name == "Pilih")
            {
                ctknota_countExtraColumn = 0;
                dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;
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
            else if (dgv_ctknota_tabelhrgpkt.CurrentCell.OwningColumn.Name == "Extra")
            {
                for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                {
                    if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Pilih"].Value.ToString() == "True")
                    {
                        if (dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Extra"].Selected)
                        {
                            if (ctknota_countExtraColumn % 2 == 0)
                            {
                                dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = true;
                            }
                            else
                            {
                                dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;
                            }
                            ctknota_countExtraColumn++;
                        }
                    }
                }
            }
        }

        private void txt_ctknota_nomorruangan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void txt_ctknota_fee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        
    }
}
