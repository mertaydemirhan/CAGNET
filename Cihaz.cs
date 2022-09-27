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
    public partial class Cihaz : Form
    {
        public Giris Giris;
        public Cihaz()
        {
            InitializeComponent();
        }
        private void listele()
        {

            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
            DataTable tablo = new DataTable();
            tablo.Clear();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from tblAnten", baglan);
            adapter.Fill(tablo);
            dataGridView1.DataSource = tablo;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Adi";
        }

        private void kulSil_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" ) { MessageBox.Show(" Alanlar Boş geçilemez."); return; }
                OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
                OleDbCommand kmt = new OleDbCommand("INSERT INTO tblAnten(Adi) Values(@p1)", baglan);
                kmt.Parameters.Add(new OleDbParameter("@p1", textBox1.Text.Trim()));
                baglan.Open();
                kmt.ExecuteNonQuery();
                baglan.Close();
                DialogResult uyari;
                uyari = MessageBox.Show("Ekleme Başarılı", "Uyarı", MessageBoxButtons.OK);
                listele();
                textBox1.Clear();
            }
            catch (OleDbException)
            {
                MessageBox.Show("Hata ile Karşılaşıldı !!");
                textBox1.Clear();
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult uyari;
                uyari = MessageBox.Show("Silmek istediğinize Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (uyari == DialogResult.Yes)
                {
                    OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
                    OleDbCommand kmt = new OleDbCommand("DELETE FROM tblAnten Where ID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "", baglan);
                    baglan.Open();
                    kmt.ExecuteNonQuery();
                    baglan.Close();
                    listele();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Silerken Hata ile Karşılaşıldı.!");
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
