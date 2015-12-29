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
    public partial class frm_laporanpenjualan : Form
    {
        public frm_laporanpenjualan()
        {
            InitializeComponent();
        }

        int lprnpnjln_potonganhotel = 0;
        int lprnpnjln_hargapaket = 0;
        int lprnpnjln_extra = 0;
        int lprnpnjln_diskon = 0;
        int lprnpnjln_totalbayar = 0;
        int lprnpnjln_totalbayarcash = 0;
        int lprnpnjln_totalbayarcredit = 0;
        int lprnpnjln_feeterapis = 0;
        DataGridViewButtonColumn lprnpnjln_hapusNota = new DataGridViewButtonColumn();
        DataSet lprnpnjln_DS = new DataSet();

        private void dtp_lprnpnjln_tgldari_ValueChanged(object sender, EventArgs e)
        {
            lprnpnjln_potonganhotel = 0;
            lprnpnjln_hargapaket = 0;
            lprnpnjln_extra = 0;
            lprnpnjln_diskon = 0;
            lprnpnjln_totalbayar = 0;
            lprnpnjln_totalbayarcash = 0;
            lprnpnjln_totalbayarcredit = 0;
            lprnpnjln_feeterapis = 0;

            #region(Select)
            dgv_lprnpnjln_tabellaporan.DataSource = null;
            dgv_lprnpnjln_tabellaporan.Rows.Clear();
            dgv_lprnpnjln_tabellaporan.Columns.Clear();
            dgv_lprnpnjln_tabellaporan.Refresh();
            lprnpnjln_DS.Tables.Clear();
            string lprnpnjln_query;
            string lprnpnjln_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection lprnpnjln_conn = new MySqlConnection(lprnpnjln_connStr);
            try
            {
                lprnpnjln_conn.Open();
                lprnpnjln_query = "SELECT * FROM `nota` WHERE `tanggalcetak_nota` BETWEEN '" + dtp_lprnpnjln_tgldari.Value.ToShortDateString()
                                    + "' and '" + dtp_lprnpnjln_tglsampai.Value.ToShortDateString() + "'";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(lprnpnjln_query, lprnpnjln_conn);
                MySqlCommand ctknota_cmd = new MySqlCommand(lprnpnjln_query, lprnpnjln_conn);

                mySqlDataAdapter.Fill(lprnpnjln_DS);
                //lprnpnjln_DS.Tables[0].Columns.Remove("id_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("jenis_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_normal_paket");
                //lprnpnjln_DS.Tables[0].Columns.Remove("komisi_midnight_paket");
                lprnpnjln_DS.Tables[0].Columns["id_nota"].ColumnName = "Nomor Nota";
                lprnpnjln_DS.Tables[0].Columns["tanggalcetak_nota"].ColumnName = "Tanggal Cetak Nota";
                lprnpnjln_DS.Tables[0].Columns["nomorruangan_nota"].ColumnName = "Nomor Ruangan";
                lprnpnjln_DS.Tables[0].Columns["jamkerja_nota"].ColumnName = "Jam Kerja";
                lprnpnjln_DS.Tables[0].Columns["tamuhotel_nota"].ColumnName = "Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["potonganhotel_nota"].ColumnName = "Potongan Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["namapaket_nota"].ColumnName = "Nama Paket";
                lprnpnjln_DS.Tables[0].Columns["hargapaket_nota"].ColumnName = "Harga Paket";
                lprnpnjln_DS.Tables[0].Columns["extra_nota"].ColumnName = "Extra";
                lprnpnjln_DS.Tables[0].Columns["nominalextra_nota"].ColumnName = "Nominal Extra";
                lprnpnjln_DS.Tables[0].Columns["kodeterapis_nota"].ColumnName = "Kode Terapis";
                lprnpnjln_DS.Tables[0].Columns["namaterapis_nota"].ColumnName = "Nama Terapis";
                lprnpnjln_DS.Tables[0].Columns["diskon_nota"].ColumnName = "Diskon";
                lprnpnjln_DS.Tables[0].Columns["keterangan_nota"].ColumnName = "Keterangan Diskon";
                lprnpnjln_DS.Tables[0].Columns["totalbayar_nota"].ColumnName = "Subtotal";
                lprnpnjln_DS.Tables[0].Columns["feeterapis_nota"].ColumnName = "Fee Terapis";
                lprnpnjln_DS.Tables[0].Columns["jenisbayar_nota"].ColumnName = "Jenis Bayar";
                lprnpnjln_DS.Tables[0].Columns["status_nota"].ColumnName = "Status";
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
                dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_hapusNota);
                dgv_lprnpnjln_tabellaporan.Columns["Hapus Nota"].DisplayIndex = 18;

                //dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].ReadOnly = true;
                //dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].Width = 55;
                //dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].ReadOnly = true;
                //dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                //dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].Width = 115;
                //dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].ReadOnly = true;
                //dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].Width = 70;
                //dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].ReadOnly = true;
                //dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].Width = 50;
                //dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].ReadOnly = true;
                //dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].Width = 45;
                //dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].ReadOnly = true;
                //dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].Width = 85;
                //dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                //dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].ReadOnly = true;
                //dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                //dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].Width = 150;
                //dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].ReadOnly = true;
                //dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].Width = 60;
                //dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                //dgv_lprnpnjln_tabellaporan.Columns["Extra"].ReadOnly = true;
                //dgv_lprnpnjln_tabellaporan.Columns["Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv_lprnpnjln_tabellaporan.Columns["Extra"].Width = 85;
                //dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].ReadOnly = true;
                //dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].Width = 85;
                //dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";


                //MessageBox.Show(dgv_lprnpnjln_tabellaporan.Columns.Count.ToString());
                //for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Columns.Count; i++)
                //{
                //    dgv_lprnpnjln_tabellaporan.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //    dgv_lprnpnjln_tabellaporan.Columns[i].ReadOnly = true;
                //}

                dgv_lprnpnjln_tabellaporan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgv_lprnpnjln_tabellaporan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv_lprnpnjln_tabellaporan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.ReadOnly = true;
                dgv_lprnpnjln_tabellaporan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_lprnpnjln_tabellaporan.Columns["Keterangan Diskon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Rows.Count; i++)
                {
                    lprnpnjln_potonganhotel += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Potongan Tamu Hotel"].Value.ToString());
                    lprnpnjln_hargapaket += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Harga Paket"].Value.ToString());
                    lprnpnjln_extra += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Nominal Extra"].Value.ToString());
                    lprnpnjln_diskon += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Diskon"].Value.ToString());
                    lprnpnjln_totalbayar += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Subtotal"].Value.ToString());
                    lprnpnjln_feeterapis += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Fee Terapis"].Value.ToString());
                    if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString()=="Cash")
                    {
                        lprnpnjln_totalbayarcash += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Subtotal"].Value.ToString());
                    }
                    else if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString()=="Credit")
                    {
                        lprnpnjln_totalbayarcredit += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Subtotal"].Value.ToString());
                    }
                }
                lbl_lprnpnjln_sumtotalhotel.Text = lprnpnjln_potonganhotel.ToString("#,##0");
                lbl_lprnpnjln_sumtotalhargapaket.Text = lprnpnjln_hargapaket.ToString("#,##0");
                lbl_lprnpnjln_sumtotalextra.Text = lprnpnjln_extra.ToString("#,##0");
                lbl_lprnpnjln_sumtotaldiskon.Text = lprnpnjln_diskon.ToString("#,##0");
                lbl_lprnpnjln_sumtotalsubtotal.Text = lprnpnjln_totalbayar.ToString("#,##0");
                lbl_lprnpnjln_sumtotalcash.Text = lprnpnjln_totalbayarcash.ToString("#,##0");
                lbl_lprnpnjln_sumtotalcredit.Text = lprnpnjln_totalbayarcredit.ToString("#,##0");
                lbl_lprnpnjln_sumfeeterapis.Text = lprnpnjln_feeterapis.ToString("#,##0");
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

        private void dtp_lprnpnjln_tglsampai_ValueChanged(object sender, EventArgs e)
        {
            #region(Select)
            dgv_lprnpnjln_tabellaporan.DataSource = null;
            dgv_lprnpnjln_tabellaporan.Rows.Clear();
            dgv_lprnpnjln_tabellaporan.Columns.Clear();
            dgv_lprnpnjln_tabellaporan.Refresh();
            lprnpnjln_DS.Tables.Clear();
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
                lprnpnjln_DS.Tables[0].Columns["id_nota"].ColumnName = "Nomor Nota";
                lprnpnjln_DS.Tables[0].Columns["tanggalcetak_nota"].ColumnName = "Tanggal Cetak Nota";
                lprnpnjln_DS.Tables[0].Columns["nomorruangan_nota"].ColumnName = "Nomor Ruangan";
                lprnpnjln_DS.Tables[0].Columns["jamkerja_nota"].ColumnName = "Jam Kerja";
                lprnpnjln_DS.Tables[0].Columns["tamuhotel_nota"].ColumnName = "Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["potonganhotel_nota"].ColumnName = "Potongan Tamu Hotel";
                lprnpnjln_DS.Tables[0].Columns["namapaket_nota"].ColumnName = "Nama Paket";
                lprnpnjln_DS.Tables[0].Columns["hargapaket_nota"].ColumnName = "Harga Paket";
                lprnpnjln_DS.Tables[0].Columns["extra_nota"].ColumnName = "Extra";
                lprnpnjln_DS.Tables[0].Columns["nominalextra_nota"].ColumnName = "Nominal Extra";
                lprnpnjln_DS.Tables[0].Columns["kodeterapis_nota"].ColumnName = "Kode Terapis";
                lprnpnjln_DS.Tables[0].Columns["namaterapis_nota"].ColumnName = "Nama Terapis";
                lprnpnjln_DS.Tables[0].Columns["diskon_nota"].ColumnName = "Diskon";
                lprnpnjln_DS.Tables[0].Columns["keterangan_nota"].ColumnName = "Keterangan Diskon";
                lprnpnjln_DS.Tables[0].Columns["totalbayar_nota"].ColumnName = "Subtotal";
                lprnpnjln_DS.Tables[0].Columns["feeterapis_nota"].ColumnName = "Fee Terapis";
                lprnpnjln_DS.Tables[0].Columns["jenisbayar_nota"].ColumnName = "Jenis Bayar";
                lprnpnjln_DS.Tables[0].Columns["status_nota"].ColumnName = "Status";
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




                dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].ReadOnly = true;
                dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.Columns["Nomor Nota"].Width = 55;
                dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].ReadOnly = true;
                dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_lprnpnjln_tabellaporan.Columns["Tanggal Cetak Nota"].Width = 115;
                dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].ReadOnly = true;
                dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.Columns["Nomor Ruangan"].Width = 70;
                dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].ReadOnly = true;
                dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.Columns["Jam Kerja"].Width = 50;
                dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].ReadOnly = true;
                dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.Columns["Tamu Hotel"].Width = 45;
                dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].ReadOnly = true;
                dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].Width = 85;
                dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].ReadOnly = true;
                dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].Width = 150;
                dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].ReadOnly = true;
                dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].Width = 60;
                dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                dgv_lprnpnjln_tabellaporan.Columns["Extra"].ReadOnly = true;
                dgv_lprnpnjln_tabellaporan.Columns["Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.Columns["Extra"].Width = 85;
                dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].ReadOnly = true;
                dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].Width = 85;
                dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";

                
                //MessageBox.Show(dgv_lprnpnjln_tabellaporan.Columns.Count.ToString());
                //for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Columns.Count; i++)
                //{
                //    dgv_lprnpnjln_tabellaporan.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //    dgv_lprnpnjln_tabellaporan.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //    dgv_lprnpnjln_tabellaporan.Columns[i].ReadOnly = true;
                //}
                dgv_lprnpnjln_tabellaporan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv_lprnpnjln_tabellaporan.Columns["Keterangan Diskon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_hapusNota);
                dgv_lprnpnjln_tabellaporan.Columns["Hapus Nota"].DisplayIndex = 18;
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

            lprnpnjln_hapusNota.Name = "Hapus Nota";
            lprnpnjln_hapusNota.Text = "Hapus";
            lprnpnjln_hapusNota.UseColumnTextForButtonValue = true;
        }

        private void dgv_lprnpnjln_tabellaporan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                if (dgv_lprnpnjln_tabellaporan.CurrentCell.OwningColumn.Name == "Hapus Nota")
                {
                    for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Rows.Count; i++)
                    {
                        if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Hapus Nota"].Selected)
                        {
                            MessageBox.Show(i.ToString());
                        }

                    }
                }
            }
        }

        
    }
}
