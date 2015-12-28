﻿using System;
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
    public partial class frm_laporanpenjualan : Form
    {
        public frm_laporanpenjualan()
        {
            InitializeComponent();
        }

        

        private void dtp_lprnpnjln_tglsampai_ValueChanged(object sender, EventArgs e)
        {
            #region(Select)
            DataSet lprnpnjln_DS = new DataSet();
            string lprnpnjln_query;
            string lprnpnjln_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection lprnpnjln_conn = new MySqlConnection(lprnpnjln_connStr);
            try
            {
                lprnpnjln_conn.Open();
                lprnpnjln_query = "SELECT * FROM `nota` WHERE `tanggalcetak_nota` BETWEEN '"+dtp_lprnpnjln_tgldari.Value.ToShortDateString()
                                    +"' and '"+dtp_lprnpnjln_tglsampai.Value.ToShortDateString()+"'";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(lprnpnjln_query, lprnpnjln_conn);
                MySqlCommand ctknota_cmd = new MySqlCommand(lprnpnjln_query, lprnpnjln_conn);

                mySqlDataAdapter.Fill(lprnpnjln_DS);
                //lprnpnjln_DS.Tables[0].Columns.Remove("id_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("jenis_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                //lprnpnjln_DS.Tables[0].Columns["nama_paket"].ColumnName = "Nama Paket";
                //lprnpnjln_DS.Tables[0].Columns["harga_paket"].ColumnName = "Harga Paket";
                //lprnpnjln_DS.Tables[0].Columns["durasi_paket"].ColumnName = "Durasi Paket";

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Tamu Hotel", typeof(int)));
                //for (int i = 0; i < lprnpnjln_DS.Tables[0].Rows.Count; i++)
                //{
                //    lprnpnjln_DS.Tables[0].Rows[i]["Tamu Hotel"] = ctknota_tamuhotel;
                //}

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Extra", typeof(bool)));
                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Pilih", typeof(bool)));
                //lprnpnjln_DS.Tables[0].Columns["Pilih"].SetOrdinal(0);

                //lprnpnjln_DS.Tables[0].Columns.Add(new DataColumn("Nominal Extra", typeof(int)));
                //for (int ii = 0; ii < lprnpnjln_DS.Tables[0].Rows.Count; ii++)
                //{
                //    lprnpnjln_DS.Tables[0].Rows[ii]["Nominal Extra"] = ctknota_extra;
                //}

                dgv_lprnpnjln_tabellaporan.DataSource = lprnpnjln_DS.Tables[0];

                //dgv_ctknota_tabelhrgpkt.Columns["Extra"].ReadOnly = true;
                //dgv_ctknota_tabelhrgpkt.Columns["Extra"].Width = 70;
                //dgv_ctknota_tabelhrgpkt.Columns["Pilih"].Width = 70;
                //dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].ReadOnly = true;
                //dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].Width = 300;
                //dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].ReadOnly = true;
                //dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].Width = 175;
                //dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].ReadOnly = true;
                //dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].Width = 200;
                //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].ReadOnly = true;
                //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].Width = 175;
                //dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].ReadOnly = true;
                //dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Width = 175;
                //dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].Visible = false;

                //dgv_ctknota_tabelhrgpkt.Columns["Nama Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                //dgv_ctknota_tabelhrgpkt.Columns["Harga Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                //dgv_ctknota_tabelhrgpkt.Columns["Durasi Paket"].SortMode = DataGridViewColumnSortMode.NotSortable;
                //dgv_ctknota_tabelhrgpkt.Columns["Tamu Hotel"].SortMode = DataGridViewColumnSortMode.NotSortable;
                //dgv_ctknota_tabelhrgpkt.Columns["Nominal Extra"].SortMode = DataGridViewColumnSortMode.NotSortable;

                //List<string> lstExtra = new List<string>();
                //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                //{
                //    //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                //    //extra = Convert.ToString());
                //    double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                //    int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                //    double hasil = nominal * Convert.ToDouble(harga);
                //    lstExtra.Add(hasil.ToString());
                //    //MessageBox.Show(hasilfinal.ToString());
                //    //foreach (char c in extra)
                //    //{
                //    //    if (char.IsDigit(c))
                //    //    {
                //    //        countdigitextra++;
                //    //    }
                //    //}
                //    //int digit = countdigitextra;
                //    //while (digit > 3)
                //    //{
                //    //    digit -= 3;
                //    //    extra = extra.Insert(digit, ".");
                //    //    dsCloned.Tables[0].Rows[i]["Nominal Extra"] = extra;
                //    //}

                //}

                //DataSet dsCloned = lprnpnjln_DS.Clone();

                //dsCloned.Tables[0].Columns["Harga Paket"].DataType = typeof(string);
                //dsCloned.Tables[0].Columns["Tamu Hotel"].DataType = typeof(string);
                //dsCloned.Tables[0].Columns["Nominal Extra"].DataType = typeof(string);
                //foreach (DataRow row in lprnpnjln_DS.Tables[0].Rows)
                //{
                //    dsCloned.Tables[0].ImportRow(row);
                //}

                //dgv_ctknota_tabelhrgpkt.DataSource = dsCloned.Tables[0];

                //int countdigitharga = 0;
                //string hargapaket;
                //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                //{
                //    countdigitharga = 0;
                //    hargapaket = dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString();
                //    foreach (char c in hargapaket)
                //    {
                //        if (char.IsDigit(c))
                //        {
                //            countdigitharga++;
                //        }
                //    }
                //    int digit = countdigitharga;
                //    while (digit > 3)
                //    {
                //        digit -= 3;
                //        hargapaket = hargapaket.Insert(digit, ".");
                //        dsCloned.Tables[0].Rows[i]["Harga Paket"] = hargapaket;
                //    }

                //}

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

                //int countdigitextra = 0;
                //for (int i = 0; i < dgv_ctknota_tabelhrgpkt.Rows.Count; i++)
                //{
                //    countdigitextra = 0;
                //    //extra = (int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100) * int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                //    //extra = Convert.ToString());
                //    //double nominal = double.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Nominal Extra"].Value.ToString()) / 100;
                //    //int harga = int.Parse(dgv_ctknota_tabelhrgpkt.Rows[i].Cells["Harga Paket"].Value.ToString());
                //    //double hasil = nominal * Convert.ToDouble(harga);
                //    //extra = hasil.ToString();
                //    //MessageBox.Show(hasilfinal.ToString());
                //    foreach (char c in lstExtra[i])
                //    {
                //        if (char.IsDigit(c))
                //        {
                //            countdigitextra++;
                //        }
                //    }
                //    int digit = countdigitextra;
                //    while (digit > 3)
                //    {
                //        digit -= 3;
                //        lstExtra[i] = lstExtra[i].Insert(digit, ".");
                //        dsCloned.Tables[0].Rows[i]["Nominal Extra"] = lstExtra[i];
                //    }

                //}


            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnpnjln_conn.Close();
            #endregion
        }



        private void frm_laporanpenjualan_Load(object sender, EventArgs e)
        {
            dtp_lprnpnjln_tgldari.Format = DateTimePickerFormat.Custom;
            dtp_lprnpnjln_tgldari.CustomFormat = "dddd, dd MMMM yyyy";

            dtp_lprnpnjln_tglsampai.Format = DateTimePickerFormat.Custom;
            dtp_lprnpnjln_tglsampai.CustomFormat = "dddd, dd MMMM yyyy";
        }
    }
}