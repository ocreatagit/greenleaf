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

namespace Green_Leaf
{
    public partial class frm_laporanlayanan : Form
    {
        public frm_laporanlayanan()
        {
            InitializeComponent();
        }

        string lprnlayanan_tanggalcetakdari = "";
        string lprnlayanan_tanggalcetaksampai = "";
        DataSet lprnlayanan_DS = new DataSet();

        private void frm_laporanlayanan_Load(object sender, EventArgs e)
        {
            dgv_lprnlayanan_tabellayanan.DataSource = null;
            dgv_lprnlayanan_tabellayanan.Rows.Clear();
            dgv_lprnlayanan_tabellayanan.Columns.Clear();
            lprnlayanan_DS.Tables.Clear();

            btn_lprnlayanan_excel.Enabled = false;
        }

        private void dtp_lprnlayanan_tgldari_ValueChanged(object sender, EventArgs e)
        {
            string lprnlayanan_queryfinal = "";

            #region(Select)
            dgv_lprnlayanan_tabellayanan.DataSource = null;
            dgv_lprnlayanan_tabellayanan.Rows.Clear();
            dgv_lprnlayanan_tabellayanan.Columns.Clear();
            lprnlayanan_DS.Tables.Clear();
            string lprnlayanan_query;
            string lprnlayanan_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection lprnlayanan_conn = new MySqlConnection(lprnlayanan_connStr);
            try
            {
                // RRyner   
                lprnlayanan_conn.Open();
                lprnlayanan_query = "SELECT *, 'Normal', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
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

                lprnlayanan_queryfinal = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

                MySqlCommand lprnlayanan_cmd = new MySqlCommand(lprnlayanan_query, lprnlayanan_conn);
                MySqlDataReader lprnlayanan_readr = lprnlayanan_cmd.ExecuteReader();

                lprnlayanan_tanggalcetakdari = dtp_lprnlayanan_tgldari.Value.Year.ToString();
                lprnlayanan_tanggalcetakdari += "-" + dtp_lprnlayanan_tgldari.Value.Month.ToString();
                lprnlayanan_tanggalcetakdari += "-" + dtp_lprnlayanan_tgldari.Value.Day.ToString();

                lprnlayanan_tanggalcetaksampai = dtp_lprnlayanan_tglsampai.Value.Year.ToString();
                lprnlayanan_tanggalcetaksampai += "-" + dtp_lprnlayanan_tglsampai.Value.Month.ToString();
                lprnlayanan_tanggalcetaksampai += "-" + dtp_lprnlayanan_tglsampai.Value.Day.ToString();

                if (lprnlayanan_readr.HasRows)
                {
                    while (lprnlayanan_readr.Read())
                    {
                        string extra = "Dasar";
                        if (lprnlayanan_readr.GetString(8) != "Tidak")
                        {
                            extra = "Extra";
                        }

                        string temp = lprnlayanan_readr.GetString(4);
                        string harga = Convert.ToInt32(temp).ToString(String.Format("0,0",temp));

                        lprnlayanan_queryfinal += "(SELECT COUNT(n1.id_nota)" +
                                        " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + "\n\n{" + extra + "}\n\n[" + lprnlayanan_readr.GetString(1) + "]\n____________________\n" + lprnlayanan_readr.GetString(2) + "\n(" + harga + ")',";
                    }
                    //MessageBox.Show(readr.FieldCount + "");
                }
                lprnlayanan_queryfinal = lprnlayanan_queryfinal.Substring(0, lprnlayanan_queryfinal.Length - 1);
                lprnlayanan_queryfinal += " FROM terapis t";
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnlayanan_conn.Close();
            #endregion

            #region(Select)
            lprnlayanan_DS.Tables.Clear();
            string lprnlayanan_connStr3 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection lprnlayanan_conn3 = new MySqlConnection(lprnlayanan_connStr3);
            try
            {
                lprnlayanan_conn3.Open();

                MySqlDataAdapter lprnlayanan_mySqlDataAdapter3 = new MySqlDataAdapter(lprnlayanan_queryfinal, lprnlayanan_conn3);
                MySqlCommand lprnlayanan_cmd3 = new MySqlCommand(lprnlayanan_queryfinal, lprnlayanan_conn3);

                lprnlayanan_mySqlDataAdapter3.Fill(lprnlayanan_DS);

                dgv_lprnlayanan_tabellayanan.DataSource = lprnlayanan_DS.Tables[0];

                lprnlayanan_DS.Tables[0].Columns[0].ColumnName = "Nomor";
                lprnlayanan_DS.Tables[0].Columns[1].ColumnName = "Nama Terapis";
                lprnlayanan_DS.Tables[0].Columns[2].ColumnName = "Jabatan";

                dgv_lprnlayanan_tabellayanan.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnlayanan_tabellayanan.Columns[0].Width = 75;
                dgv_lprnlayanan_tabellayanan.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnlayanan_tabellayanan.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnlayanan_tabellayanan.Columns[2].Width = 75;
                dgv_lprnlayanan_tabellayanan.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnlayanan_tabellayanan.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
                for (int i = 3; i < dgv_lprnlayanan_tabellayanan.Columns.Count; i++)
                {
                    //dgv_lprnlayanan_tabellayanan.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgv_lprnlayanan_tabellayanan.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                for (int i = 0; i < dgv_lprnlayanan_tabellayanan.Columns.Count; i++)
                {
                    dgv_lprnlayanan_tabellayanan.Columns[i].ReadOnly = true;
                }
                List<string> lprnlayanan_listtotal = new List<string>();
                lprnlayanan_listtotal.Add("0");
                lprnlayanan_listtotal.Add("0");
                lprnlayanan_listtotal.Add("0");
                for (int i = 3; i < lprnlayanan_DS.Tables[0].Columns.Count; i++)
                {
                    int totalperkolom = 0;
                    for (int ii = 0; ii < lprnlayanan_DS.Tables[0].Rows.Count; ii++)
                    {

                        totalperkolom += Convert.ToInt32(lprnlayanan_DS.Tables[0].Rows[ii][i].ToString());
                        
                    }
                    lprnlayanan_listtotal.Add(totalperkolom.ToString());
                }
                DataRow row = lprnlayanan_DS.Tables[0].NewRow();
                row[0] = "0";
                row[1] = "Total";
                row[2] = "";
                for (int i = 3; i < lprnlayanan_listtotal.Count; i++)
                {
                    row[i] = lprnlayanan_listtotal[i];
                }
                lprnlayanan_DS.Tables[0].Rows.Add(row);
                dgv_lprnlayanan_tabellayanan.Rows[dgv_lprnlayanan_tabellayanan.Rows.Count - 1].DefaultCellStyle.Font = new Font(dgv_lprnlayanan_tabellayanan.Font, FontStyle.Bold);
                btn_lprnlayanan_excel.Enabled = true;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnlayanan_conn3.Close();
            #endregion

            
        }

        private void dtp_lprnlayanan_tglsampai_ValueChanged(object sender, EventArgs e)
        {
            string lprnlayanan_queryfinal = "";

            #region(Select)
            dgv_lprnlayanan_tabellayanan.DataSource = null;
            dgv_lprnlayanan_tabellayanan.Rows.Clear();
            dgv_lprnlayanan_tabellayanan.Columns.Clear();
            lprnlayanan_DS.Tables.Clear();
            string lprnlayanan_query;
            string lprnlayanan_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection lprnlayanan_conn = new MySqlConnection(lprnlayanan_connStr);
            try
            {
                // RRyner   
                lprnlayanan_conn.Open();
                lprnlayanan_query = "SELECT *, 'Normal', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
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

                lprnlayanan_queryfinal = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

                MySqlCommand lprnlayanan_cmd = new MySqlCommand(lprnlayanan_query, lprnlayanan_conn);
                MySqlDataReader lprnlayanan_readr = lprnlayanan_cmd.ExecuteReader();

                lprnlayanan_tanggalcetakdari = dtp_lprnlayanan_tgldari.Value.Year.ToString();
                lprnlayanan_tanggalcetakdari += "-" + dtp_lprnlayanan_tgldari.Value.Month.ToString();
                lprnlayanan_tanggalcetakdari += "-" + dtp_lprnlayanan_tgldari.Value.Day.ToString();

                lprnlayanan_tanggalcetaksampai = dtp_lprnlayanan_tglsampai.Value.Year.ToString();
                lprnlayanan_tanggalcetaksampai += "-" + dtp_lprnlayanan_tglsampai.Value.Month.ToString();
                lprnlayanan_tanggalcetaksampai += "-" + dtp_lprnlayanan_tglsampai.Value.Day.ToString();

                if (lprnlayanan_readr.HasRows)
                {
                    while (lprnlayanan_readr.Read())
                    {
                        string extra = "Dasar";
                        if (lprnlayanan_readr.GetString(8) != "Tidak")
                        {
                            extra = "Extra";
                        }

                        string temp = lprnlayanan_readr.GetString(4);
                        string harga = Convert.ToInt32(temp).ToString(String.Format("0,0", temp));

                        lprnlayanan_queryfinal += "(SELECT COUNT(n1.id_nota)" +
                                        " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE DATE(`tanggalcetak_nota`) >= '" + lprnlayanan_tanggalcetakdari + "' AND DATE(tanggalcetak_nota) <= '" + lprnlayanan_tanggalcetaksampai + "' AND n1.status_nota='-' AND n1.jamkerja_nota = '" + lprnlayanan_readr.GetString(7) + "' AND n1.extra_nota = '" + lprnlayanan_readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + lprnlayanan_readr.GetString(1) + "') AND n1.id_paket = " + lprnlayanan_readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + lprnlayanan_readr.GetString(7) + "\n\n{" + extra + "}\n\n[" + lprnlayanan_readr.GetString(1) + "]\n____________________\n" + lprnlayanan_readr.GetString(2) + "\n(" + harga + ")',";
                    }
                    //MessageBox.Show(readr.FieldCount + "");
                }
                lprnlayanan_queryfinal = lprnlayanan_queryfinal.Substring(0, lprnlayanan_queryfinal.Length - 1);
                lprnlayanan_queryfinal += " FROM terapis t";
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnlayanan_conn.Close();
            #endregion

            #region(Select)
            lprnlayanan_DS.Tables.Clear();
            string lprnlayanan_connStr3 = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection lprnlayanan_conn3 = new MySqlConnection(lprnlayanan_connStr3);
            try
            {
                lprnlayanan_conn3.Open();

                MySqlDataAdapter lprnlayanan_mySqlDataAdapter3 = new MySqlDataAdapter(lprnlayanan_queryfinal, lprnlayanan_conn3);
                MySqlCommand lprnlayanan_cmd3 = new MySqlCommand(lprnlayanan_queryfinal, lprnlayanan_conn3);

                lprnlayanan_mySqlDataAdapter3.Fill(lprnlayanan_DS);

                dgv_lprnlayanan_tabellayanan.DataSource = lprnlayanan_DS.Tables[0];

                lprnlayanan_DS.Tables[0].Columns[0].ColumnName = "Nomor";
                lprnlayanan_DS.Tables[0].Columns[1].ColumnName = "Nama Terapis";
                lprnlayanan_DS.Tables[0].Columns[2].ColumnName = "Jabatan";

                dgv_lprnlayanan_tabellayanan.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnlayanan_tabellayanan.Columns[0].Width = 75;
                dgv_lprnlayanan_tabellayanan.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnlayanan_tabellayanan.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnlayanan_tabellayanan.Columns[2].Width = 75;
                dgv_lprnlayanan_tabellayanan.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv_lprnlayanan_tabellayanan.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                for (int i = 3; i < dgv_lprnlayanan_tabellayanan.Columns.Count; i++)
                {
                    dgv_lprnlayanan_tabellayanan.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgv_lprnlayanan_tabellayanan.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                for (int i = 0; i < dgv_lprnlayanan_tabellayanan.Columns.Count; i++)
                {
                    dgv_lprnlayanan_tabellayanan.Columns[i].ReadOnly = true;
                }

                btn_lprnlayanan_excel.Enabled = true;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnlayanan_conn3.Close();
            #endregion
        }

        private void btn_lprnlayanan_excel_Click(object sender, EventArgs e)
        {
            string folderPath = "C:\\Excel\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(lprnlayanan_DS.Tables[0], "Customers");
                wb.SaveAs(folderPath + "Laporan Layanan.xlsx");
                MessageBox.Show("File Excel telah disimpan");
            }
        }

        private void btn_lprnlayanan_batal_Click(object sender, EventArgs e)
        {

        }
    }
}
