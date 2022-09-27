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
    public partial class kulGuncelle : Form
    {
        public Giris Giris;
        public kulGuncelle()
        {
            InitializeComponent();
        }
        public void listele()
        {
            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
            DataTable tablo = new DataTable();
            tablo.Clear();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from tblKullanici", baglan);
            adapter.Fill(tablo);
            dataGridView1.DataSource = tablo;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Kullanıcı Adı";
            dataGridView1.Columns[2].HeaderText = "Şifre";
            dataGridView1.Columns[3].Visible = false;
        }
        private void kulGuncelle_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            listele();
            comboBox1.Items.Add("Kullanıcı");
            comboBox1.Items.Add("Yönetici");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Anasayfa Anasayfa = new Anasayfa();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = 0;
        
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                comboBox1.SelectedIndex = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            }
            catch (Exception) {
                MessageBox.Show("Hata Oluştu");
                return;
            
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var secilen = comboBox1.SelectedItem;
            if (secilen == null)
                return;

            if (textBox1 == null || textBox2 == null)
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre boş Olmamalıdır");
            }
            else if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "")
            {
                int yetki = 1;
                var id = dataGridView1.CurrentRow.Cells[0].Value;
                if (comboBox1.SelectedIndex == 0) yetki = 0;
                OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
                OleDbCommand asd = new OleDbCommand
                ("UPDATE tblKullanici SET KullaniciAdi=@p1,Sifre=@p2,Yetki=@p3 where ID=@p4", baglan);
                asd.Parameters.Add(new OleDbParameter("@p1", textBox1.Text.Trim()));
                asd.Parameters.Add(new OleDbParameter("@p2", textBox2.Text.Trim()));
                asd.Parameters.Add(new OleDbParameter("@p3", yetki));
                asd.Parameters.Add(new OleDbParameter("@p4", Convert.ToInt32(id.ToString())));
                baglan.Open();
                asd.ExecuteNonQuery();
                baglan.Close();
                listele();
                MessageBox.Show("Düzenleme İşlemi Başarılı Oldu");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult uyari;
            uyari = MessageBox.Show("Silmek istediğinize Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (uyari == DialogResult.Yes)
            {
                OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
                OleDbCommand kmt = new OleDbCommand("DELETE FROM tblKullanici Where ID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "", baglan);
                baglan.Open();
                kmt.ExecuteNonQuery();
                baglan.Close();
                listele();
            }
        }
    }
}
