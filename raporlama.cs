using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CAGNET
{
    public partial class raporlama : Form
    {
        public int _customerID { get; set; }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
        public raporlama()
        {
            InitializeComponent();
        }
        public raporlama(int customerID)
        {
            InitializeComponent();
            _customerID = customerID;

        }
        private void listele() {

            OleDbCommand cmd1 = new OleDbCommand("SELECT tblMusteri.adi, tblMusteri.Soyad,tblOdemeler.Tutari,tblOdemeler.OdemeTuru,tblOdemeler.Aciklama FROM tblMusteri INNER JOIN tblOdemeler ON tblOdemeler.MusteriId =tblMusteri.ID where tblOdemeler.Tarih= @p4", baglan);
            cmd1.Parameters.AddWithValue("@Tarih", dateTimePicker1.Text);
            DataTable dt1 = new DataTable();
            OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
            baglan.Open();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            baglan.Close();/*
            OleDbCommand cmd = new OleDbCommand("Select * from tblOdemeler Where Tarih = @Tarih", baglan);
            cmd.Parameters.AddWithValue("@Tarih", dateTimePicker1.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            baglan.Open();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Ödeme Tutarı";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[2].HeaderText = "Müşteri ID";
            dataGridView1.Columns[3].Visible = false;*/

        
        
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            listele();

        }

        private void raporlama_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Visible = true; 

            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);

            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            int StartCol = 1;

            int StartRow = 1; 

            for (int j = 5; j < dataGridView1.Columns.Count; j++)
            {

                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];

                myRange.Value2 = dataGridView1.Columns[j].HeaderText;

            }

            StartRow++;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {

                    try
                    {

                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];

                        myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;

                    }

                    catch
                    {

                        ;

                    }

                } 

            }
        }
    }
}
