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
    public partial class frm_laporanlayanan : Form
    {
        public frm_laporanlayanan()
        {
            InitializeComponent();
        }

        DataSet lprnpnjln_DS = new DataSet();

        private void frm_laporanlayanan_Load(object sender, EventArgs e)
        {
            #region(Select)
            dgv_lprnlayanan.DataSource = null;
            dgv_lprnlayanan.Rows.Clear();
            dgv_lprnlayanan.Columns.Clear();
            //dgv_lprnpnjln_tabellaporan.Refresh();
            lprnpnjln_DS.Tables.Clear();
            string lprnpnjln_query;
            string lprnpnjln_connStr = "server=localhost;user=root;database=greenleaf;port=3306;password=;";
            MySqlConnection lprnpnjln_conn = new MySqlConnection(lprnpnjln_connStr);
            try
            {
                // RRyner   
                lprnpnjln_conn.Open();
                //lprnpnjln_query = "SELECT * FROM `nota` WHERE DATE(`tanggalcetak_nota`) >= '" + lprnpnjln_tanggalcetakdari + "' and DATE(tanggalcetak_nota) <= '" + lprnpnjln_tanggalcetaksampai + "'";
                lprnpnjln_query = "SELECT *, 'Normal', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Normal', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION  " +
                                    "SELECT *, 'Midnight', 'Tidak' FROM paket WHERE jenis_paket = 'Deluxe' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'VIP' " +
                                    "UNION " +
                                    "SELECT *, 'Midnight', 'Ya' FROM `paket` WHERE jenis_paket = 'Deluxe';";

                string sql_laporan = "SELECT t.kode_terapis, t.nama_terapis, 'THERAPIST', ";

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(lprnpnjln_query, lprnpnjln_conn);
                MySqlCommand ctknota_cmd = new MySqlCommand(lprnpnjln_query, lprnpnjln_conn);
                MySqlDataReader readr = ctknota_cmd.ExecuteReader();

                if (readr.HasRows)
                {
                    while (readr.Read())
                    {
                        string extra = "Dasar";
                        if (readr.GetString(8) != "Tidak")
                        {
                            extra = "Extra Session";
                        }

                        sql_laporan += "(SELECT COUNT(n1.id_nota)" +
                                        " FROM nota n1 INNER JOIN terapis t1 ON t1.kode_terapis = n1.kodeterapis_nota " +
                                        "INNER JOIN paket p1 ON p1.id_paket = n1.id_paket " +
                                        "WHERE n1.jamkerja_nota = '" + readr.GetString(7) + "' AND n1.extra_nota = '" + readr.GetString(8) + "' " +
                                        "AND p1.jenis_paket = TRIM('" + readr.GetString(1) + "') AND n1.id_paket = " + readr.GetInt32(0) + " AND t.id_terapis = t1.id_terapis) as ' " + readr.GetString(7) + " -> " + extra + " -> " + readr.GetString(1) + " -> " + readr.GetString(2) + "',";
                    }
                    MessageBox.Show(readr.FieldCount + "");
                }
                sql_laporan = sql_laporan.Substring(0, sql_laporan.Length - 1);
                sql_laporan += " FROM terapis t";

                // RRyner  

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
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                MessageBox.Show("Error Occurred");
            }
            lprnpnjln_conn.Close();
            #endregion
        }
    }
}
