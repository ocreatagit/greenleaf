using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Green_Leaf
{
    public partial class frm_cetaknota : Form
    {
        public frm_cetaknota()
        {
            InitializeComponent();
        }

        DataTable dtEmp = new DataTable();

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void frm_cetaknota_Load(object sender, EventArgs e)
        {
            

            
        }

        private void btn_tbhtrps_tambah_Click(object sender, EventArgs e)
        {

        }

        private void rdo_ctknota_biasa_CheckedChanged(object sender, EventArgs e)
        {
            dtEmp.Columns.Add("Nama Paket", typeof(string));
            dtEmp.Columns.Add("Durasi Paket", typeof(string));
            dtEmp.Columns.Add("Harga Paket", typeof(string));
            dtEmp.Columns.Add("Extra", typeof(bool));
            dtEmp.Columns.Add("Pilih", typeof(bool));

            dtEmp.Rows.Add("Traditional Massage", "1 Jam 30 Menit" , "Rp. 500.000,-", false,false);
            dtEmp.Rows.Add("Traditional Massage", "1 Jam 30 Menit", "Rp. 500.000,-", false, false);
            dgv_ctknota_tblhrgpkt.DataSource = dtEmp; 
        }

        private void rdo_ctknota_hotel_CheckedChanged(object sender, EventArgs e)
        {
            dtEmp.Columns.Add("Tamu Hotel", typeof(string));
        }
    }
}
