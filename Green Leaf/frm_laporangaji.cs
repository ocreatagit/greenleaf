using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Reflection;
using ClosedXML.Excel;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace Green_Leaf
{
    public partial class frm_laporangaji : Form
    {
        public frm_laporangaji()
        {
            InitializeComponent();
        }

        string all_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
        int edtvariabel_extra;

        #region Laporan Penjualan
        string lprnpnjln_tanggalcetakdari = "";
        string lprnpnjln_tanggalcetaksampai = "";
        DataSet lprnpnjln_DS = new DataSet();
        #endregion

        #region Laporan Layanan
        string lprnlayanan_tanggalcetakdari = "";
        string lprnlayanan_tanggalcetaksampai = "";
        DataSet lprnlayanan_DS = new DataSet();
        DataSet lprnlayanan_DSolahankomisi = new DataSet();
        #endregion

        #region (Laporan Gaji Excel)
        string lprngaji_tanggalcetakdari = "";
        string lprngaji_tanggalcetaksampai = "";
        DataSet lprngaji_DS = new DataSet();
        DataSet lprngaji_DSolahankomisi = new DataSet();
        XLWorkbook lprngaji_wb;
        private void dtp_lprngaji_tgldari_ValueChanged(object sender, EventArgs e)
        {
            #region(Select)
            string lprngaji_queryfinalcount = "";
            string lprngaji_queryfinalkomisi = "";


            #region(Select Query Komisi)
            //dgv_lprngaji_tabellayanan.DataSource = null;
            //dgv_lprngaji_tabellayanan.Rows.Clear();
            //dgv_lprngaji_tabellayanan.Columns.Clear();
            lprngaji_DS.Tables.Clear();
            lprngaji_DSolahankomisi.Tables.Clear();
            string lprngaji_query;
            MySqlConnection lprngaji_conn = new MySqlConnection(all_connStr);
            try
            {
                // RRyner   
                lprngaji_conn.Open();
                lprngaji_query = "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION  " +
                                    "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'Deluxe'" +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VVIP';";

                lprngaji_queryfinalkomisi = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

                MySqlCommand lprngaji_cmd = new MySqlCommand(lprngaji_query, lprngaji_conn);
                MySqlDataReader lprngaji_readr = lprngaji_cmd.ExecuteReader();

                lprngaji_tanggalcetakdari = dtp_lprngaji_tgldari.Value.Year.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Month.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Day.ToString();

                lprngaji_tanggalcetaksampai = dtp_lprngaji_tglsampai.Value.Year.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Month.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Day.ToString();

                if (lprngaji_readr.HasRows)
                {
                    while (lprngaji_readr.Read())
                    {
                        string extra = "Dasar";
                        if (lprngaji_readr.GetString(8) != "Tidak")
                        {
                            extra = "Extra";
                        }

                        int temp = lprngaji_readr.GetInt32(4);
                        string harganormal = temp.ToString("0,0");
                        string hargaextra = (temp + (temp * (edtvariabel_extra / 100))).ToString("0,0");

                        //lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)" +
                        //                " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                        //                "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                        //                "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                        //                "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + harga + ")',";

                        //lprngaji_queryfinal += "(SELECT COUNT(n1.id_nota)" +
                        //                " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                        //                "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                        //                "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                        //                "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7).Substring(0, 1) + "\n\n{" + extra.Substring(0, 1) + "}\n\n[" + lprngaji_readr.GetString(1).Substring(0, 1) + "]\n\n" + lprngaji_readr.GetString(2) + "\n(" + harga + ")',";

                        if (lprngaji_readr.GetString(7) == "Normal")
                        {
                            if (lprngaji_readr.GetString(8) == "Ya")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4) + (lprngaji_readr.GetInt32(4) * (lprngaji_readr.GetDouble(9) / 100))) * (lprngaji_readr.GetDouble(5) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + hargaextra + ")',";
                            }
                            else if (lprngaji_readr.GetString(8) == "Tidak")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4)) * (lprngaji_readr.GetDouble(5) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + harganormal + ")',";
                            }

                        }
                        else if (lprngaji_readr.GetString(7) == "Midnight")
                        {
                            if (lprngaji_readr.GetString(8) == "Ya")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4) + (lprngaji_readr.GetInt32(4) * (lprngaji_readr.GetDouble(9) / 100))) * (lprngaji_readr.GetDouble(6) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + hargaextra + ")',";
                            }
                            else if (lprngaji_readr.GetString(8) == "Tidak")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4)) * (lprngaji_readr.GetDouble(6) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + harganormal + ")',";
                            }
                        }

                    }
                    //MessageBox.Show(readr.FieldCount + "");
                }
                lprngaji_queryfinalkomisi = lprngaji_queryfinalkomisi.Substring(0, lprngaji_queryfinalkomisi.Length - 1);
                lprngaji_queryfinalkomisi += " FROM terapis t WHERE t.status_terapis = 'Aktif'";
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprngaji_conn.Close();
            #endregion

            #region(Select Query Count)
            //dgv_lprngaji_tabellayanan.DataSource = null;
            //dgv_lprngaji_tabellayanan.Rows.Clear();
            //dgv_lprngaji_tabellayanan.Columns.Clear();
            //lprngaji_DS.Tables.Clear();
            string lprngaji_query2;
            MySqlConnection lprngaji_conn2 = new MySqlConnection(all_connStr);
            try
            {
                // RRyner   
                lprngaji_conn2.Open();
                lprngaji_query2 = "SELECT *, 'Normal', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION  " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe'" +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VVIP';";

                lprngaji_queryfinalcount = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

                MySqlCommand lprngaji_cmd2 = new MySqlCommand(lprngaji_query2, lprngaji_conn2);
                MySqlDataReader lprngaji_readr2 = lprngaji_cmd2.ExecuteReader();

                lprngaji_tanggalcetakdari = dtp_lprngaji_tgldari.Value.Year.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Month.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Day.ToString();

                lprngaji_tanggalcetaksampai = dtp_lprngaji_tglsampai.Value.Year.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Month.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Day.ToString();

                if (lprngaji_readr2.HasRows)
                {
                    while (lprngaji_readr2.Read())
                    {
                        string extra = "Dasar";
                        if (lprngaji_readr2.GetString(8) != "Tidak")
                        {
                            extra = "Extra";
                        }

                        string temp2 = lprngaji_readr2.GetString(4);
                        string harga2 = Convert.ToInt32(temp2).ToString(String.Format("0,0", temp2));

                        lprngaji_queryfinalcount += "(SELECT COUNT(n1.id_nota)" +
                                        " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr2.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr2.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr2.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr2.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr2.GetString(7) + " {" + extra + "} [" + lprngaji_readr2.GetString(1) + "] " + lprngaji_readr2.GetString(2) + " (" + harga2 + ")',";

                    }
                }
                lprngaji_queryfinalcount = lprngaji_queryfinalcount.Substring(0, lprngaji_queryfinalcount.Length - 1);
                lprngaji_queryfinalcount += " FROM terapis t";
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprngaji_conn2.Close();
            #endregion

            if (lprngaji_queryfinalcount != "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', FROM terapis t")
            {
                MySqlConnection lprngaji_conn4 = new MySqlConnection(all_connStr);
                try
                {
                    lprngaji_conn4.Open();

                    MySqlDataAdapter lprngaji_mySqlDataAdapter4 = new MySqlDataAdapter(lprngaji_queryfinalkomisi, lprngaji_conn4);
                    MySqlCommand lprngaji_cmd4 = new MySqlCommand(lprngaji_queryfinalcount, lprngaji_conn4);

                    lprngaji_mySqlDataAdapter4.Fill(lprngaji_DSolahankomisi);
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    MessageBox.Show("Error Occurred");
                }
                lprngaji_conn4.Close();

                #region(Isi Data)
                //lprngaji_DS.Tables.Clear();
                MySqlConnection lprngaji_conn3 = new MySqlConnection(all_connStr);
                try
                {
                    lprngaji_conn3.Open();

                    MySqlDataAdapter lprngaji_mySqlDataAdapter3 = new MySqlDataAdapter(lprngaji_queryfinalcount, lprngaji_conn3);
                    MySqlCommand lprngaji_cmd3 = new MySqlCommand(lprngaji_queryfinalcount, lprngaji_conn3);

                    lprngaji_mySqlDataAdapter3.Fill(lprngaji_DS);

                    //dgv_lprngaji_tabellayanan.DataSource = lprngaji_DS.Tables[0];
                    btn_lprngaji_excel.Enabled = true;
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    MessageBox.Show("Error Occurred");
                }
                lprngaji_conn3.Close();
                #endregion
            }
            #endregion
        }

        private void dtp_lprngaji_tglsampai_ValueChanged(object sender, EventArgs e)
        {
            #region(Select)
            string lprngaji_queryfinalcount = "";
            string lprngaji_queryfinalkomisi = "";


            #region(Select Query Komisi)
            //dgv_lprngaji_tabellayanan.DataSource = null;
            //dgv_lprngaji_tabellayanan.Rows.Clear();
            //dgv_lprngaji_tabellayanan.Columns.Clear();
            lprngaji_DS.Tables.Clear();
            lprngaji_DSolahankomisi.Tables.Clear();
            string lprngaji_query;
            MySqlConnection lprngaji_conn = new MySqlConnection(all_connStr);
            try
            {
                // RRyner   
                lprngaji_conn.Open();
                lprngaji_query = "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION  " +
                                    "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak', " + edtvariabel_extra + " AS EXTRA FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'Deluxe'" +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya', " + edtvariabel_extra + " AS EXTRA FROM `paket` WHERE jenis_paket = 'VVIP';";

                lprngaji_queryfinalkomisi = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

                MySqlCommand lprngaji_cmd = new MySqlCommand(lprngaji_query, lprngaji_conn);
                MySqlDataReader lprngaji_readr = lprngaji_cmd.ExecuteReader();

                lprngaji_tanggalcetakdari = dtp_lprngaji_tgldari.Value.Year.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Month.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Day.ToString();

                lprngaji_tanggalcetaksampai = dtp_lprngaji_tglsampai.Value.Year.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Month.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Day.ToString();

                if (lprngaji_readr.HasRows)
                {
                    while (lprngaji_readr.Read())
                    {
                        string extra = "Dasar";
                        if (lprngaji_readr.GetString(8) != "Tidak")
                        {
                            extra = "Extra";
                        }

                        int temp = lprngaji_readr.GetInt32(4);
                        string harganormal = temp.ToString("0,0");
                        string hargaextra = (temp + (temp * (edtvariabel_extra / 100))).ToString("0,0");

                        //lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)" +
                        //                " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                        //                "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                        //                "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                        //                "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + harga + ")',";

                        //lprngaji_queryfinal += "(SELECT COUNT(n1.id_nota)" +
                        //                " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                        //                "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                        //                "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                        //                "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7).Substring(0, 1) + "\n\n{" + extra.Substring(0, 1) + "}\n\n[" + lprngaji_readr.GetString(1).Substring(0, 1) + "]\n\n" + lprngaji_readr.GetString(2) + "\n(" + harga + ")',";

                        if (lprngaji_readr.GetString(7) == "Normal")
                        {
                            if (lprngaji_readr.GetString(8) == "Ya")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4) + (lprngaji_readr.GetInt32(4) * (lprngaji_readr.GetDouble(9) / 100))) * (lprngaji_readr.GetDouble(5) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + hargaextra + ")',";
                            }
                            else if (lprngaji_readr.GetString(8) == "Tidak")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4)) * (lprngaji_readr.GetDouble(5) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + harganormal + ")',";
                            }

                        }
                        else if (lprngaji_readr.GetString(7) == "Midnight")
                        {
                            if (lprngaji_readr.GetString(8) == "Ya")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4) + (lprngaji_readr.GetInt32(4) * (lprngaji_readr.GetDouble(9) / 100))) * (lprngaji_readr.GetDouble(6) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + hargaextra + ")',";
                            }
                            else if (lprngaji_readr.GetString(8) == "Tidak")
                            {
                                lprngaji_queryfinalkomisi += "(SELECT COUNT(n1.id_nota)*(" + ((lprngaji_readr.GetInt32(4)) * (lprngaji_readr.GetDouble(6) / 100)).ToString().Replace(',', '.') +
                                        ") FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr.GetString(7) + " {" + extra + "} [" + lprngaji_readr.GetString(1) + "] " + lprngaji_readr.GetString(2) + " (" + harganormal + ")',";
                            }
                        }

                    }
                    //MessageBox.Show(readr.FieldCount + "");
                }
                lprngaji_queryfinalkomisi = lprngaji_queryfinalkomisi.Substring(0, lprngaji_queryfinalkomisi.Length - 1);
                lprngaji_queryfinalkomisi += " FROM terapis t WHERE t.status_terapis = 'Aktif'";
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprngaji_conn.Close();
            #endregion

            #region(Select Query Count)
            //dgv_lprngaji_tabellayanan.DataSource = null;
            //dgv_lprngaji_tabellayanan.Rows.Clear();
            //dgv_lprngaji_tabellayanan.Columns.Clear();
            //lprngaji_DS.Tables.Clear();
            string lprngaji_query2;
            MySqlConnection lprngaji_conn2 = new MySqlConnection(all_connStr);
            try
            {
                // RRyner   
                lprngaji_conn2.Open();
                lprngaji_query2 = "SELECT *, 'Normal', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION  " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe'" +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VVIP';";

                lprngaji_queryfinalcount = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

                MySqlCommand lprngaji_cmd2 = new MySqlCommand(lprngaji_query2, lprngaji_conn2);
                MySqlDataReader lprngaji_readr2 = lprngaji_cmd2.ExecuteReader();

                lprngaji_tanggalcetakdari = dtp_lprngaji_tgldari.Value.Year.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Month.ToString();
                lprngaji_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Day.ToString();

                lprngaji_tanggalcetaksampai = dtp_lprngaji_tglsampai.Value.Year.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Month.ToString();
                lprngaji_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Day.ToString();

                if (lprngaji_readr2.HasRows)
                {
                    while (lprngaji_readr2.Read())
                    {
                        string extra = "Dasar";
                        if (lprngaji_readr2.GetString(8) != "Tidak")
                        {
                            extra = "Extra";
                        }

                        string temp2 = lprngaji_readr2.GetString(4);
                        string harga2 = Convert.ToInt32(temp2).ToString(String.Format("0,0", temp2));

                        lprngaji_queryfinalcount += "(SELECT COUNT(n1.id_nota)" +
                                        " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprngaji_readr2.GetString(7) + "' AND n1.extra_nota = '" + lprngaji_readr2.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprngaji_readr2.GetString(1) + "') AND n1.id_paket = " + lprngaji_readr2.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprngaji_readr2.GetString(7) + " {" + extra + "} [" + lprngaji_readr2.GetString(1) + "] " + lprngaji_readr2.GetString(2) + " (" + harga2 + ")',";

                    }
                }
                lprngaji_queryfinalcount = lprngaji_queryfinalcount.Substring(0, lprngaji_queryfinalcount.Length - 1);
                lprngaji_queryfinalcount += " FROM terapis t WHERE t.status_terapis = 'Aktif'";
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprngaji_conn2.Close();
            #endregion

            if (lprngaji_queryfinalcount != "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', FROM terapis t")
            {
                MySqlConnection lprngaji_conn4 = new MySqlConnection(all_connStr);
                try
                {
                    lprngaji_conn4.Open();

                    MySqlDataAdapter lprngaji_mySqlDataAdapter4 = new MySqlDataAdapter(lprngaji_queryfinalkomisi, lprngaji_conn4);
                    MySqlCommand lprngaji_cmd4 = new MySqlCommand(lprngaji_queryfinalcount, lprngaji_conn4);

                    lprngaji_mySqlDataAdapter4.Fill(lprngaji_DSolahankomisi);
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    MessageBox.Show("Error Occurred");
                }
                lprngaji_conn4.Close();

                #region(Isi Data)
                //lprngaji_DS.Tables.Clear();
                MySqlConnection lprngaji_conn3 = new MySqlConnection(all_connStr);
                try
                {
                    lprngaji_conn3.Open();

                    MySqlDataAdapter lprngaji_mySqlDataAdapter3 = new MySqlDataAdapter(lprngaji_queryfinalcount, lprngaji_conn3);
                    MySqlCommand lprngaji_cmd3 = new MySqlCommand(lprngaji_queryfinalcount, lprngaji_conn3);

                    lprngaji_mySqlDataAdapter3.Fill(lprngaji_DS);

                    //dgv_lprngaji_tabellayanan.DataSource = lprngaji_DS.Tables[0];
                    btn_lprngaji_excel.Enabled = true;
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    MessageBox.Show("Error Occurred");
                }
                lprngaji_conn3.Close();
                #endregion
            }
            #endregion
        }

        private void btn_lprngaji_excel_Click(object sender, EventArgs e)
        {
            #region(Slip Gaji)
            lprngaji_wb = new XLWorkbook();
            lprngaji_wb.Worksheets.Add("Slip Gaji");

            string folderPath = "C:\\Excel\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            List<string>[] listrowcetak_jenispaket = new List<string>[1];
            listrowcetak_jenispaket[0] = new List<string>();
            Array.Resize(ref listrowcetak_jenispaket, 2);
            List<Int32>[] listrowcetak_counterjasa = new List<Int32>[lprngaji_DSolahankomisi.Tables[0].Rows.Count];
            List<Double>[] listrowcetak_komisi = new List<Double>[lprngaji_DSolahankomisi.Tables[0].Rows.Count];
            for (int i = 0; i < listrowcetak_counterjasa.Length; i++)
            {
                listrowcetak_counterjasa[i] = new List<Int32>();
                listrowcetak_komisi[i] = new List<Double>();
            }
            for (int i = 3; i < lprngaji_DSolahankomisi.Tables[0].Columns.Count; i++)
            {
                if (lprngaji_DSolahankomisi.Tables[0].Columns[i].ColumnName.Substring(0, 6) == "Normal")
                {
                    if (i == 3)
                    {
                        listrowcetak_jenispaket[0].Add("Normal");
                        listrowcetak_jenispaket[0].Add(lprngaji_DSolahankomisi.Tables[0].Columns[i].ColumnName);
                    }
                    else if (lprngaji_DSolahankomisi.Tables[0].Columns[i + 1].ColumnName.Substring(0, 8) == "Midnight")
                    {
                        listrowcetak_jenispaket[0].Add(lprngaji_DSolahankomisi.Tables[0].Columns[i].ColumnName);
                        listrowcetak_jenispaket[0].Add("Midnight");
                    }
                    else
                    {
                        listrowcetak_jenispaket[0].Add(lprngaji_DSolahankomisi.Tables[0].Columns[i].ColumnName);
                    }
                }
                else if (lprngaji_DSolahankomisi.Tables[0].Columns[i].ColumnName.Substring(0, 8) == "Midnight")
                {
                    listrowcetak_jenispaket[0].Add(lprngaji_DSolahankomisi.Tables[0].Columns[i].ColumnName);
                }
            }

            for (int i = 0; i < listrowcetak_counterjasa.Length; i++)
            {
                for (int ii = 3; ii < lprngaji_DS.Tables[0].Columns.Count; ii++)
                {
                    listrowcetak_counterjasa[i].Add(int.Parse(lprngaji_DS.Tables[0].Rows[i][ii].ToString()));
                }
            }
            for (int i = 0; i < listrowcetak_counterjasa.Length; i++)
            {
                for (int ii = 0; ii < listrowcetak_jenispaket[0].Count; ii++)
                {
                    if (listrowcetak_jenispaket[0][ii] == "Normal")
                    {
                        listrowcetak_counterjasa[i].Insert(ii, 0);
                    }
                    else if (listrowcetak_jenispaket[0][ii] == "Midnight")
                    {
                        listrowcetak_counterjasa[i].Insert(ii, 0);
                    }
                }
            }

            for (int i = 0; i < listrowcetak_komisi.Length; i++)
            {
                for (int ii = 3; ii < lprngaji_DS.Tables[0].Columns.Count; ii++)
                {
                    listrowcetak_komisi[i].Add(lprngaji_DSolahankomisi.Tables[0].Rows[i][ii].CastTo<Double>());
                }
            }
            for (int i = 0; i < listrowcetak_komisi.Length; i++)
            {
                for (int ii = 0; ii < listrowcetak_jenispaket[0].Count; ii++)
                {
                    if (listrowcetak_jenispaket[0][ii] == "Normal")
                    {
                        listrowcetak_komisi[i].Insert(ii, 0);
                    }
                    else if (listrowcetak_jenispaket[0][ii] == "Midnight")
                    {
                        listrowcetak_komisi[i].Insert(ii, 0);
                    }
                }
            }

            lprngaji_wb.Worksheet("Slip Gaji").PageSetup.Margins.Top = 1;
            lprngaji_wb.Worksheet("Slip Gaji").PageSetup.Margins.Bottom = 1;
            lprngaji_wb.Worksheet("Slip Gaji").PageSetup.Margins.Left = 0.6;
            lprngaji_wb.Worksheet("Slip Gaji").PageSetup.Margins.Right = 0.6;
            lprngaji_wb.Worksheet("Slip Gaji").PageSetup.Margins.Footer = 0.8;
            lprngaji_wb.Worksheet("Slip Gaji").PageSetup.Margins.Header = 0.8;
            lprngaji_wb.Worksheet("Slip Gaji").PageSetup.CenterHorizontally = true;
            int pageoffset = 0;
            int pagecount = 1;
            pagecount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(listrowcetak_jenispaket[0].Count) / 30));
            if (listrowcetak_jenispaket[0].Count - (30 * (pagecount - 1)) >= 22)
            {
                pagecount++;
            }
            if (pagecount == 1)
            {
                for (int i = 0; i < listrowcetak_komisi.Length; i++)
                {
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Value = "SLIP GAJI";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Style.Font.FontSize = 24;
                    lprngaji_wb.Worksheet("Slip Gaji").Range(1 + pageoffset, 1, 1 + pageoffset, 14).Merge();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    lprngaji_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 2).Value = "BULAN";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 2).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 3).Value = dtp_lprngaji_tgldari.Value.ToLongDateString() + " - " + dtp_lprngaji_tglsampai.Value.ToLongDateString();

                    lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 2).Value = "NO ID";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 2).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 3).Value = lprngaji_DS.Tables[0].Rows[i][0];
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 2).Value = "NAMA";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 2).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 3).Value = lprngaji_DS.Tables[0].Rows[i][1];
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 2).Value = "POSISI";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 2).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 3).Value = lprngaji_DS.Tables[0].Rows[i][2];

                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Value = "Jenis Paket";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, 9 + pageoffset, 11).Merge();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Value = "Jumlah";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Value = "Komisi";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 2).InsertData(listrowcetak_jenispaket[0]);
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 12).InsertData(listrowcetak_counterjasa[i]);
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 13).InsertData(listrowcetak_komisi[i]);
                    //lprngaji_wb.Worksheet("Slip Gaji").Column(2).AdjustToContents();
                    for (int ii = 0; ii < listrowcetak_jenispaket[0].Count; ii++)
                    {
                        if (listrowcetak_jenispaket[0][ii] == "Normal")
                        {
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Value = "";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 12).Value = "";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 2).Style.Font.SetBold();
                            lprngaji_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
                        }
                        else if (listrowcetak_jenispaket[0][ii] == "Midnight")
                        {
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Value = "";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 12).Value = "";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 2).Style.Font.SetBold();
                            lprngaji_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
                        }
                        else
                        {
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
                        }
                    }
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 2).Value = "Total";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 2).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 11).Merge();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 12).FormulaA1 = "SUM(L" + (10 + pageoffset).ToString() + ":L" + (listrowcetak_jenispaket[0].Count + 9 + pageoffset).ToString() + ")";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).FormulaA1 = "SUM(M" + (10 + pageoffset).ToString() + ":M" + (listrowcetak_jenispaket[0].Count + 9 + pageoffset).ToString() + ")";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorderColor(XLColor.Black);
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 11).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 11).Style.Border.SetOutsideBorderColor(XLColor.Black);
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 12).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 12).Style.Border.SetOutsideBorderColor(XLColor.Black);
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                    lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[0].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorderColor(XLColor.Black);

                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 2).Value = "+ Bonus Target";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 2).Value = "+ Bonus Tahunan";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 2).Value = "+ Bonus Lain-lain";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 15 + pageoffset, 2).Value = "- Potongan Biaya Masuk";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 15 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 15 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 16 + pageoffset, 2).Value = "- Potongan Absensi";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 16 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 16 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 17 + pageoffset, 2).Value = "- Potong Tabungan";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 17 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 17 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 18 + pageoffset, 2).Value = "- Potong Hutang";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 18 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 18 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 2).Value = "Gaji Total";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 2).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 5).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 5).Style.Font.SetBold();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 6).FormulaA1 = "M" + (listrowcetak_jenispaket[0].Count + 10 + pageoffset).ToString() + "+SUM(F" + (listrowcetak_jenispaket[0].Count + 12 + pageoffset).ToString() + ":F" + (listrowcetak_jenispaket[0].Count + 14 + pageoffset).ToString() +
                                                                                                                ")-SUM(F" + (listrowcetak_jenispaket[0].Count + 15 + pageoffset).ToString() + ":F" + (listrowcetak_jenispaket[0].Count + 18 + pageoffset).ToString() + ")";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 19 + pageoffset, 6).Style.Font.SetBold();

                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 8).Value = "Sisa Hutang";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 11).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 12 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 8).Value = "Total Tabungan";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 11).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 13 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 8).Value = "Sisa Biaya Masuk Kembali";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 11).Value = ":";
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[0].Count + 14 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";

                    lprngaji_wb.Worksheet("Slip Gaji").Cell(43 + pageoffset, 13).Value = "Hal. " + 1.ToString();
                    lprngaji_wb.Worksheet("Slip Gaji").Range(43 + pageoffset, 13, 43 + pageoffset, 14).Merge();
                    lprngaji_wb.Worksheet("Slip Gaji").Cell(43 + pageoffset, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    pageoffset += 43;
                }
            }
            else if (pagecount > 1)
            {
                Array.Resize(ref listrowcetak_jenispaket, pagecount);
                for (int i = 1; i < pagecount; i++)
                {
                    listrowcetak_jenispaket[i] = new List<string>();
                }
                List<int>[,] listrowcetak_counterjasahal2dst = new List<int>[lprngaji_DSolahankomisi.Tables[0].Rows.Count, pagecount];
                List<double>[,] listrowcetak_komisihal2dst = new List<double>[lprngaji_DSolahankomisi.Tables[0].Rows.Count, pagecount];
                for (int x = 0; x < lprngaji_DSolahankomisi.Tables[0].Rows.Count; x++)
                {
                    for (int y = 0; y < pagecount; y++)
                    {
                        listrowcetak_counterjasahal2dst[x, y] = new List<int>();
                        listrowcetak_komisihal2dst[x, y] = new List<double>();
                    }
                }
                for (int x = 0; x < lprngaji_DSolahankomisi.Tables[0].Rows.Count; x++)
                {
                    listrowcetak_counterjasahal2dst[x, 0] = listrowcetak_counterjasa[x];
                    listrowcetak_komisihal2dst[x, 0] = listrowcetak_komisi[x];
                }
                int normalextracont = 0;
                int midnightextracount = 0;
                int countfortakeitem = 0;
                for (int row = 0; row < listrowcetak_jenispaket.Length; row++)
                {
                    midnightextracount = 0;
                    normalextracont = 0;
                    if (listrowcetak_jenispaket[row].Count > 30)
                    {
                        countfortakeitem = (listrowcetak_jenispaket[row].Count - 2) / 4 - 7;
                        for (int i = 0; i < listrowcetak_jenispaket[row].Count; i++)
                        {
                            if (row + 1 <= listrowcetak_jenispaket.Length)
                            {
                                if (listrowcetak_jenispaket[row][i] == "Normal")
                                {
                                    listrowcetak_jenispaket[row + 1].Add("Normal");
                                    for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
                                    {
                                        listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(0);
                                        listrowcetak_komisihal2dst[idxterapis, row + 1].Add(0);
                                    }
                                }
                                else if (i > 1)
                                {
                                    if (listrowcetak_jenispaket[row][i].Contains("Normal {Extra}") && normalextracont < 1)
                                    {
                                        for (int idx = 1; idx <= countfortakeitem; idx++)
                                        {
                                            listrowcetak_jenispaket[row + 1].Add(listrowcetak_jenispaket[row][i - idx]);
                                            listrowcetak_jenispaket[row].RemoveAt(i - idx);
                                        }
                                        for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
                                        {
                                            for (int idx = 1; idx <= countfortakeitem; idx++)
                                            {
                                                listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(listrowcetak_counterjasahal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_counterjasahal2dst[idxterapis, row].RemoveAt(i - idx);
                                                listrowcetak_komisihal2dst[idxterapis, row + 1].Add(listrowcetak_komisihal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_komisihal2dst[idxterapis, row].RemoveAt(i - idx);
                                            }
                                        }
                                        normalextracont++;
                                    }
                                    else if (listrowcetak_jenispaket[row][i] == "Midnight")
                                    {
                                        for (int idx = 1; idx <= countfortakeitem; idx++)
                                        {
                                            listrowcetak_jenispaket[row + 1].Add(listrowcetak_jenispaket[row][i - idx]);
                                            listrowcetak_jenispaket[row].RemoveAt(i - idx);
                                        }
                                        for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
                                        {
                                            for (int idx = 1; idx <= countfortakeitem; idx++)
                                            {
                                                listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(listrowcetak_counterjasahal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_counterjasahal2dst[idxterapis, row].RemoveAt(i - idx);
                                                listrowcetak_komisihal2dst[idxterapis, row + 1].Add(listrowcetak_komisihal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_komisihal2dst[idxterapis, row].RemoveAt(i - idx);
                                            }
                                        }
                                        listrowcetak_jenispaket[row + 1].Add("Midnight");
                                        for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
                                        {
                                            listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(0);
                                            listrowcetak_komisihal2dst[idxterapis, row + 1].Add(0);
                                        }
                                    }
                                    else if (listrowcetak_jenispaket[row][i].Contains("Midnight {Extra}") && midnightextracount < 1)
                                    {
                                        for (int idx = 1; idx <= countfortakeitem; idx++)
                                        {
                                            listrowcetak_jenispaket[row + 1].Add(listrowcetak_jenispaket[row][i - idx]);
                                            listrowcetak_jenispaket[row].RemoveAt(i - idx);
                                        }
                                        for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
                                        {
                                            for (int idx = 1; idx <= countfortakeitem; idx++)
                                            {
                                                listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(listrowcetak_counterjasahal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_counterjasahal2dst[idxterapis, row].RemoveAt(i - idx);
                                                listrowcetak_komisihal2dst[idxterapis, row + 1].Add(listrowcetak_komisihal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_komisihal2dst[idxterapis, row].RemoveAt(i - idx);
                                            }
                                        }
                                        midnightextracount++;
                                    }
                                    else if (i == listrowcetak_jenispaket[row].Count - 1)
                                    {
                                        for (int idx = 0; idx < countfortakeitem; idx++)
                                        {
                                            listrowcetak_jenispaket[row + 1].Add(listrowcetak_jenispaket[row][i - idx]);
                                            listrowcetak_jenispaket[row].RemoveAt(i - idx);
                                        }
                                        for (int idxterapis = 0; idxterapis < listrowcetak_komisihal2dst.GetLength(0); idxterapis++)
                                        {
                                            for (int idx = 1; idx <= countfortakeitem; idx++)
                                            {
                                                listrowcetak_counterjasahal2dst[idxterapis, row + 1].Add(listrowcetak_counterjasahal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_counterjasahal2dst[idxterapis, row].RemoveAt(i - idx);
                                                listrowcetak_komisihal2dst[idxterapis, row + 1].Add(listrowcetak_komisihal2dst[idxterapis, row][i - idx]);
                                                listrowcetak_komisihal2dst[idxterapis, row].RemoveAt(i - idx);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                for (int i = 0; i < listrowcetak_komisi.Length; i++)
                {
                    string formulatotalkomisi = "";
                    for (int p = 1; p <= pagecount; p++)
                    {
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Value = "SLIP GAJI";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Style.Font.FontSize = 24;
                        lprngaji_wb.Worksheet("Slip Gaji").Range(1 + pageoffset, 1, 1 + pageoffset, 14).Merge();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(1 + pageoffset, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        lprngaji_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 2).Value = "BULAN";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 2).Style.Font.SetBold();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(3 + pageoffset, 3).Value = dtp_lprngaji_tgldari.Value.ToLongDateString() + " - " + dtp_lprngaji_tglsampai.Value.ToLongDateString();

                        lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 2).Value = "NO ID";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 2).Style.Font.SetBold();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 3).Value = lprngaji_DS.Tables[0].Rows[i][0];
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(5 + pageoffset, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 2).Value = "NAMA";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 2).Style.Font.SetBold();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(6 + pageoffset, 3).Value = lprngaji_DS.Tables[0].Rows[i][1];
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 2).Value = "POSISI";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 2).Style.Font.SetBold();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(7 + pageoffset, 3).Value = lprngaji_DS.Tables[0].Rows[i][2];

                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Value = "Jenis Paket";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Style.Font.SetBold();
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, 9 + pageoffset, 11).Merge();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Value = "Jumlah";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Style.Font.SetBold();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Value = "Komisi";
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Style.Font.SetBold();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(9 + pageoffset, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                        lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 2).InsertData(listrowcetak_jenispaket[p - 1]);
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 12).InsertData(listrowcetak_counterjasahal2dst[i, p - 1]);
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + pageoffset, 13).InsertData(listrowcetak_komisihal2dst[i, p - 1]);
                        //lprngaji_wb.Worksheet("Slip Gaji").Column(2).AdjustToContents();
                        for (int ii = 0; ii < listrowcetak_jenispaket[p - 1].Count; ii++)
                        {
                            if (listrowcetak_jenispaket[p - 1][ii] == "Normal")
                            {
                                lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Value = "";
                                lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 12).Value = "";
                                lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 2).Style.Font.SetBold();
                                lprngaji_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
                            }
                            else if (listrowcetak_jenispaket[p - 1][ii] == "Midnight")
                            {
                                lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Value = "";
                                lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 12).Value = "";
                                lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 2).Style.Font.SetBold();
                                lprngaji_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
                            }
                            else
                            {
                                lprngaji_wb.Worksheet("Slip Gaji").Cell(10 + ii + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
                                lprngaji_wb.Worksheet("Slip Gaji").Range(10 + ii + pageoffset, 2, 10 + ii + pageoffset, 11).Merge();
                            }
                        }
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 13).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 13).Style.Border.SetOutsideBorderColor(XLColor.Black);
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 11).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 11).Style.Border.SetOutsideBorderColor(XLColor.Black);
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 12).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 12).Style.Border.SetOutsideBorderColor(XLColor.Black);
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 13).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                        lprngaji_wb.Worksheet("Slip Gaji").Range(9 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset, 13).Style.Border.SetOutsideBorderColor(XLColor.Black);

                        if (listrowcetak_jenispaket[p - 1].Count <= 0)
                        {
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Value = "Total";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Style.Font.SetBold();
                            lprngaji_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 11).Merge();
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 12).FormulaA1 = "0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).FormulaA1 = "0";
                            //lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                            lprngaji_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                        }
                        else if (listrowcetak_jenispaket[p - 1].Count > 0)
                        {
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Value = "Total";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Style.Font.SetBold();
                            lprngaji_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 11).Merge();
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 12).FormulaA1 = "SUM(L" + (10 + pageoffset).ToString() + ":L" + (listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset).ToString() + ")";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).FormulaA1 = "SUM(M" + (10 + pageoffset).ToString() + ":M" + (listrowcetak_jenispaket[p - 1].Count + 9 + pageoffset).ToString() + ")";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                            lprngaji_wb.Worksheet("Slip Gaji").Range(listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 2, listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset, 13).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);

                        }
                        formulatotalkomisi += "M" + (listrowcetak_jenispaket[p - 1].Count + 10 + pageoffset).ToString() + "+";

                        if (listrowcetak_jenispaket[p - 1].Count <= 22)
                        {
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 2).Value = "+ Bonus Target";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 2).Value = "+ Bonus Tahunan";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 2).Value = "+ Bonus Lain-lain";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 15 + pageoffset, 2).Value = "- Potongan Biaya Masuk";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 15 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 15 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 16 + pageoffset, 2).Value = "- Potongan Absensi";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 16 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 16 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 17 + pageoffset, 2).Value = "- Potong Tabungan";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 17 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 17 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 18 + pageoffset, 2).Value = "- Potong Hutang";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 18 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 18 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 2).Value = "Gaji Total";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 2).Style.Font.SetBold();
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 5).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 5).Style.Font.SetBold();
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 6).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 6).FormulaA1 = formulatotalkomisi + "SUM(F" + (listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset).ToString() + ":F" + (listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset).ToString() +
                                                                                                                        ")-SUM(F" + (listrowcetak_jenispaket[p - 1].Count + 15 + pageoffset).ToString() + ":F" + (listrowcetak_jenispaket[p - 1].Count + 18 + pageoffset).ToString() + ")";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 19 + pageoffset, 6).Style.Font.SetBold();

                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 8).Value = "Sisa Hutang";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 11).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 12 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 8).Value = "Total Tabungan";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 11).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 13 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 8).Value = "Sisa Biaya Masuk Kembali";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 11).Value = ":";
                            lprngaji_wb.Worksheet("Slip Gaji").Cell(listrowcetak_jenispaket[p - 1].Count + 14 + pageoffset, 12).Style.NumberFormat.Format = "#,##0";

                        }

                        lprngaji_wb.Worksheet("Slip Gaji").Cell(43 + pageoffset, 13).Value = "Hal. " + p.ToString();
                        lprngaji_wb.Worksheet("Slip Gaji").Range(43 + pageoffset, 13, 43 + pageoffset, 14).Merge();
                        lprngaji_wb.Worksheet("Slip Gaji").Cell(43 + pageoffset, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        pageoffset += 43;

                    }
                }
            }



            lprngaji_wb.Worksheet("Slip Gaji").Column("A").Width = 2.29;
            lprngaji_wb.Worksheet("Slip Gaji").Column("C").Width = 4.43;
            lprngaji_wb.Worksheet("Slip Gaji").Column("E").Width = 0.58;
            lprngaji_wb.Worksheet("Slip Gaji").Column("F").Width = 10.43;
            lprngaji_wb.Worksheet("Slip Gaji").Column("G").Width = 2.71;
            lprngaji_wb.Worksheet("Slip Gaji").Column("I").Width = 5.29;
            lprngaji_wb.Worksheet("Slip Gaji").Column("K").Width = 0.58;
            lprngaji_wb.Worksheet("Slip Gaji").Column("L").Width = 10.43;
            lprngaji_wb.Worksheet("Slip Gaji").Column("M").Width = 10.43;
            lprngaji_wb.Worksheet("Slip Gaji").Column("N").Width = 0.67;


            lprngaji_wb.SaveAs(folderPath + "Slip Gaji (" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour + ";" + DateTime.Now.Minute.ToString() + ";" + DateTime.Now.Second.ToString() + ").xlsx");
            btn_lprngaji_excel.Enabled = false;
            //lprngaji_wb.Dispose();
            //lprngaji_wb = new XLWorkbook();
            //lprngaji_wb.Worksheets.Add("Slip Gaji");

            //}
            #endregion

            #region Laporan Penjualan
            #region(Select)
            //dgv_lprnpnjln_tabellaporan.Refresh();
            lprnpnjln_DS.Tables.Clear();
            string lprnpnjln_query;
            MySqlConnection lprnpnjln_conn = new MySqlConnection(all_connStr);
            try
            {
                lprnpnjln_tanggalcetakdari = dtp_lprngaji_tgldari.Value.Year.ToString();
                lprnpnjln_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Month.ToString();
                lprnpnjln_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Day.ToString();

                lprnpnjln_tanggalcetaksampai = dtp_lprngaji_tglsampai.Value.Year.ToString();
                lprnpnjln_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Month.ToString();
                lprnpnjln_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Day.ToString();

                lprnpnjln_conn.Open();
                lprnpnjln_query = "SELECT * FROM `nota` WHERE DATE(`tanggalcetak_nota`) >= '" + lprnpnjln_tanggalcetakdari + "' and DATE(tanggalcetak_nota) <= '" + lprnpnjln_tanggalcetaksampai + "'";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(lprnpnjln_query, lprnpnjln_conn);
                MySqlCommand ctknota_cmd = new MySqlCommand(lprnpnjln_query, lprnpnjln_conn);

                mySqlDataAdapter.Fill(lprnpnjln_DS);
                lprnpnjln_DS.Tables[0].Columns.Remove("id_paket");
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
                lprnpnjln_DS.Tables[0].Columns["grandtotal_nota"].ColumnName = "Grand Total";
                //dgv_lprnpnjln_tabellaporan.DataSource = lprnpnjln_DS.Tables[0];

                //if (login_jenisuser == "Superadmin")
                //{
                //    dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_hapusNota);
                //    dgv_lprnpnjln_tabellaporan.Columns["Hapus Nota"].DisplayIndex = 19;
                //    dgv_lprnpnjln_tabellaporan.Columns.Add(lprnpnjln_batalhapusNota);
                //    dgv_lprnpnjln_tabellaporan.Columns["Batal Hapus"].DisplayIndex = 20;
                //}



                //dgv_lprnpnjln_tabellaporan.Columns["Grand Total"].DisplayIndex = 16;
                //dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DisplayIndex = 14;

                //dgv_lprnpnjln_tabellaporan.Columns["Potongan Tamu Hotel"].DefaultCellStyle.Format = "N0";
                //dgv_lprnpnjln_tabellaporan.Columns["Harga Paket"].DefaultCellStyle.Format = "N0";
                //dgv_lprnpnjln_tabellaporan.Columns["Nominal Extra"].DefaultCellStyle.Format = "N0";
                //dgv_lprnpnjln_tabellaporan.Columns["Diskon"].DefaultCellStyle.Format = "N0";
                //dgv_lprnpnjln_tabellaporan.Columns["Subtotal"].DefaultCellStyle.Format = "N0";
                //dgv_lprnpnjln_tabellaporan.Columns["Fee Terapis"].DefaultCellStyle.Format = "N0";
                //dgv_lprnpnjln_tabellaporan.Columns["Grand Total"].DefaultCellStyle.Format = "N0";

                //dgv_lprnpnjln_tabellaporan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                //dgv_lprnpnjln_tabellaporan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //dgv_lprnpnjln_tabellaporan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv_lprnpnjln_tabellaporan.ReadOnly = true;

                //dgv_lprnpnjln_tabellaporan.Columns["Nama Paket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                //dgv_lprnpnjln_tabellaporan.Columns["Keterangan Diskon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                //#region(SUM Kolom berisi nominal harga)
                //lprnpnjln_potonganhotel = 0;
                //lprnpnjln_hargapaket = 0;
                //lprnpnjln_extra = 0;
                //lprnpnjln_diskon = 0;
                //lprnpnjln_totalbayar = 0;
                //lprnpnjln_totalbayarcash = 0;
                //lprnpnjln_totalbayarcredit = 0;
                //lprnpnjln_feeterapis = 0;
                //lprnpnjln_grandtotal = 0;
                //for (int i = 0; i < dgv_lprnpnjln_tabellaporan.Rows.Count; i++)
                //{
                //    if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Status"].Value.ToString() != "Terhapus")
                //    {
                //        lprnpnjln_potonganhotel += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Potongan Tamu Hotel"].Value.ToString());
                //        lprnpnjln_hargapaket += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Harga Paket"].Value.ToString());
                //        lprnpnjln_extra += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Nominal Extra"].Value.ToString());
                //        lprnpnjln_diskon += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Diskon"].Value.ToString());
                //        lprnpnjln_totalbayar += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Subtotal"].Value.ToString());
                //        lprnpnjln_feeterapis += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Fee Terapis"].Value.ToString());
                //        lprnpnjln_grandtotal += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                //        if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Cash")
                //        {
                //            lprnpnjln_totalbayarcash += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                //        }
                //        else if (dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Jenis Bayar"].Value.ToString() == "Credit")
                //        {
                //            lprnpnjln_totalbayarcredit += int.Parse(dgv_lprnpnjln_tabellaporan.Rows[i].Cells["Grand Total"].Value.ToString());
                //        }
                //    }
                //}
                //lbl_lprnpnjln_sumtotalhotel.Text = lprnpnjln_potonganhotel.ToString("#,##0");
                //lbl_lprnpnjln_sumtotalgrandtotal.Text = lprnpnjln_grandtotal.ToString("#,##0");
                //lbl_lprnpnjln_sumtotalextra.Text = lprnpnjln_extra.ToString("#,##0");
                //lbl_lprnpnjln_sumtotaldiskon.Text = lprnpnjln_diskon.ToString("#,##0");
                //lbl_lprnpnjln_sumtotalsubtotal.Text = lprnpnjln_totalbayar.ToString("#,##0");
                //lbl_lprnpnjln_sumtotalcash.Text = lprnpnjln_totalbayarcash.ToString("#,##0");
                //lbl_lprnpnjln_sumtotalcredit.Text = lprnpnjln_totalbayarcredit.ToString("#,##0");
                //lbl_lprnpnjln_sumfeeterapis.Text = lprnpnjln_feeterapis.ToString("#,##0");
                //#endregion


            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnpnjln_conn.Close();
            #endregion

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                //wb.Cell().DataType = XLCellValues.Number;
                wb.Worksheets.Add(lprnpnjln_DS.Tables[0], "Laporan Penjualan");
                wb.SaveAs(folderPath + "Laporan Penjualan (" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour + ";" + DateTime.Now.Minute.ToString() + ";" + DateTime.Now.Second.ToString() + ").xlsx");

            }
            #endregion

            #region Laporan Layanan
            #region(Select)
            string lprnlayanan_queryfinalcount = "";

            #region(Select Query Count)
            string lprnlayanan_query2;
            MySqlConnection lprnlayanan_conn2 = new MySqlConnection(all_connStr);
            try
            {
                // RRyner   
                lprnlayanan_conn2.Open();
                lprnlayanan_query2 = "SELECT *, 'Normal', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION  " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'VVIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe'" +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VVIP';";

                lprnlayanan_queryfinalcount = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

                MySqlCommand lprnlayanan_cmd2 = new MySqlCommand(lprnlayanan_query2, lprnlayanan_conn2);
                MySqlDataReader lprnlayanan_readr2 = lprnlayanan_cmd2.ExecuteReader();

                lprnlayanan_tanggalcetakdari = dtp_lprngaji_tgldari.Value.Year.ToString();
                lprnlayanan_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Month.ToString();
                lprnlayanan_tanggalcetakdari += "-" + dtp_lprngaji_tgldari.Value.Day.ToString();

                lprnlayanan_tanggalcetaksampai = dtp_lprngaji_tglsampai.Value.Year.ToString();
                lprnlayanan_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Month.ToString();
                lprnlayanan_tanggalcetaksampai += "-" + dtp_lprngaji_tglsampai.Value.Day.ToString();

                if (lprnlayanan_readr2.HasRows)
                {
                    while (lprnlayanan_readr2.Read())
                    {
                        string extra = "Dasar";
                        if (lprnlayanan_readr2.GetString(8) != "Tidak")
                        {
                            extra = "Extra";
                        }

                        string temp2 = lprnlayanan_readr2.GetString(4);
                        string harga2 = Convert.ToInt32(temp2).ToString(String.Format("0,0", temp2));

                        lprnlayanan_queryfinalcount += "(SELECT COUNT(n1.id_nota)" +
                                        " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr2.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr2.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr2.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr2.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr2.GetString(7) + " {" + extra + "} [" + lprnlayanan_readr2.GetString(1) + "] " + lprnlayanan_readr2.GetString(2) + " (" + harga2 + ")',";

                    }
                }
                lprnlayanan_queryfinalcount = lprnlayanan_queryfinalcount.Substring(0, lprnlayanan_queryfinalcount.Length - 1);
                lprnlayanan_queryfinalcount += " FROM terapis t WHERE t.status_terapis = 'Aktif'";
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnlayanan_conn2.Close();
            #endregion

            if (lprnlayanan_queryfinalcount != "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', FROM terapis t")
            {

                #region(Isi Data)
                MySqlConnection lprnlayanan_conn3 = new MySqlConnection(all_connStr);
                try
                {
                    lprnlayanan_conn3.Open();

                    MySqlDataAdapter lprnlayanan_mySqlDataAdapter3 = new MySqlDataAdapter(lprnlayanan_queryfinalcount, lprnlayanan_conn3);
                    MySqlCommand lprnlayanan_cmd3 = new MySqlCommand(lprnlayanan_queryfinalcount, lprnlayanan_conn3);

                    lprnlayanan_mySqlDataAdapter3.Fill(lprnlayanan_DS);
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    MessageBox.Show("Error Occurred");
                }
                lprnlayanan_conn3.Close();
                #endregion
            }
            #endregion

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                //wb.Cell().DataType = XLCellValues.Number;
                wb.Worksheets.Add(lprnlayanan_DS.Tables[0], "Laporan Layanan");
                wb.SaveAs(folderPath + "Laporan Layanan (" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour + ";" + DateTime.Now.Minute.ToString() + ";" + DateTime.Now.Second.ToString() + ").xlsx");
                MessageBox.Show("File Excel telah disimpan", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion

            #region(Hapus Data Laporan Penjualan)
            DialogResult dialogResult = MessageBox.Show("Apakah anda ingin menghapus Data Laporan Penjualan?", "Alert", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                #region(Update)
                MySqlConnection lprnpnjln_conn4 = new MySqlConnection(all_connStr);
                try
                {
                    lprnpnjln_conn4.Open();

                    string lprnpnjln_query4 = "DELETE FROM `nota` WHERE DATE(`tanggalcetak_nota`) >= '" + lprngaji_tanggalcetakdari + "' and DATE(tanggalcetak_nota) <= '" + lprngaji_tanggalcetaksampai + "'";
                    MySqlCommand lprnpnjln_cmd4 = new MySqlCommand(lprnpnjln_query4, lprnpnjln_conn4);
                    lprnpnjln_cmd4.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("Error Occured");
                }
                lprnpnjln_conn4.Close();
                #endregion
            }
            #endregion

            lprngaji_DS = new DataSet();
            lprngaji_DSolahankomisi = new DataSet();
            lprngaji_wb = new XLWorkbook();
            GC.Collect();
        }

        private void btn_lprngaji_batal_Click(object sender, EventArgs e)
        {

            //pnl_menu_isi.Visible = true;
            //pnl_lprngaji_isi.Visible = false;

            //pnl_login_isi.Enabled = false;
            //pnl_edttrps_isi.Enabled = false;
            //pnl_edtpkt_isi.Enabled = false;
            //pnl_ctknota_isi.Enabled = false;
            //pnl_lprnlayanan_isi.Enabled = false;
            //pnl_lprnpnjln_isi.Enabled = false;
            //pnl_menu_isi.Enabled = true;
            //pnl_tbhpkt_isi.Enabled = false;
            //pnl_tbhtrps_isi.Enabled = false;
            //pnl_variabel_isi.Enabled = false;
            //pnl_lprngaji_isi.Enabled = false;

            //lprngaji_DS.Clear();
            //lprngaji_DSolahankomisi.Clear();
            //lprngaji_wb.Dispose();
            this.Close();
            GC.Collect();
        }
        #endregion

        private void frm_laporangaji_Load(object sender, EventArgs e)
        {
            #region(Isi textbox)
            string edtpkt_query;
            MySqlConnection edtpkt_conn = new MySqlConnection(all_connStr);
            try
            {
                edtpkt_conn.Open();

                edtpkt_query = "SELECT * FROM `variabel`";
                MySqlCommand edtpkt_cmd = new MySqlCommand(edtpkt_query, edtpkt_conn);
                MySqlDataReader edtpkt_rdr = edtpkt_cmd.ExecuteReader();

                while (edtpkt_rdr.Read())
                {
                    edtvariabel_extra = edtpkt_rdr.GetInt32(1);
                    //txt_variabel_potonganhotel.Text = edtpkt_rdr.GetString(2);
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
