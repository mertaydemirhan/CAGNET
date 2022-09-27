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
    public partial class Paket : Form
    {
        public Giris Giris;
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
        public Paket()
        {
            InitializeComponent();
        }
        private void listele()
        {
            DataTable tablo = new DataTable();
            tablo.Clear();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from tblPaket", baglan);
            adapter.Fill(tablo);
            dataGridView1.DataSource = tablo;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Paket Adı";
            dataGridView1.Columns[2].HeaderText = "Fiyatı";
        }
        private void Paket_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
                OleDbCommand kmt = new OleDbCommand("INSERT INTO tblPaket(Adi,Fiyat) Values(@p1,@p2)", baglan);
                kmt.Parameters.Add(new OleDbParameter("@p1", textBox1.Text.Trim()));
                kmt.Parameters.Add(new OleDbParameter("@p2", textBox2.Text.Trim()));
                baglan.Open();
                kmt.ExecuteNonQuery();
                baglan.Close();
                DialogResult uyari;
                uyari = MessageBox.Show("Ekleme Başarılı", "Uyarı", MessageBoxButtons.OK);
                listele();
                textBox1.Clear();
                textBox2.Clear();
            }
            catch (OleDbException)
            {
                MessageBox.Show("Hata ile Karşılaşıldı !! \n - Fiyat değeri Sayı Olmalıdır \n -Alanlar Boş geçilmemelidir.");
                textBox2.Clear();
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult uyari;
            uyari = MessageBox.Show("Silmek istediğinize Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (uyari == DialogResult.Yes)
            {
                OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
                OleDbCommand kmt = new OleDbCommand("DELETE FROM tblPaket Where ID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "", baglan);
                baglan.Open();
                kmt.ExecuteNonQuery();
                baglan.Close();
                listele();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            OleDbCommand asd = new OleDbCommand
            ("UPDATE tblPaket SET Adi=@p1,Fiyat=@p2 where ID=@p18", baglan);
            asd.Parameters.Add(new OleDbParameter("@p1", textBox1.Text));
            asd.Parameters.Add(new OleDbParameter("@p2", textBox2.Text));
            asd.Parameters.Add(new OleDbParameter("@p18", dataGridView1.CurrentRow.Cells[0].Value.ToString()));
            baglan.Open();
            asd.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("Düzenleme İşlemi Başarılı Oldu");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu");
                return;

            }
        }
    }
}
