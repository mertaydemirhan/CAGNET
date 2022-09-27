using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using CAGNET.Entities;

namespace CAGNET
{

    public partial class Anasayfa : Form
    {
        int a = 0;
        public Giris Giris;

        public UserRecords _user { get; set; }

        public Anasayfa()
        {
            InitializeComponent();
        }

        public Anasayfa(UserRecords User)
        {
            InitializeComponent();
            _user = User;
        }

        public void listele()
        {
            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
            DataTable tablo = new DataTable();
            tablo.Clear();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from tblMusteri", baglan);
            adapter.Fill(tablo);
            dataGridView1.DataSource = tablo;
            dataGridView1.Columns[0].HeaderText = "İşlem";
            dataGridView1.Columns[1].Visible = false; //Durumu
            dataGridView1.Columns[2].Visible = false; //Adi
            dataGridView1.Columns[3].HeaderText = "Adı"; //Soyad
            dataGridView1.Columns[4].HeaderText = "Soyadı"; //Adres
            dataGridView1.Columns[5].Visible = false; //KimlikNo
            dataGridView1.Columns[6].Visible = false; //Ceptel1
            dataGridView1.Columns[7].Visible = false; //Ceptel2
            dataGridView1.Columns[8].Visible = false;//Evtel
            dataGridView1.Columns[9].Visible = false; //AdslNo
            dataGridView1.Columns[10].Visible = false; //KayitTarihi
            dataGridView1.Columns[11].Visible = false; //Paket
            dataGridView1.Columns[12].Visible = false; //Cihaz
            dataGridView1.Columns[13].Visible = false; //CihazModeli
            dataGridView1.Columns[14].Visible = false;//KullaniciAdi
            dataGridView1.Columns[15].Visible = false; //Sifre
        }
        public void yukle()
        {
            webBrowser1.Visible = false;
            this.WindowState = FormWindowState.Maximized;
            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
            DataTable tablo = new DataTable();
            tablo.Clear();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from tblMusteri", baglan);
            adapter.Fill(tablo);
            DataGridViewButtonColumn btcol2 = new DataGridViewButtonColumn();
            btcol2.HeaderText = "işlem";
            btcol2.Text = "işlem Yap";
            btcol2.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(btcol2);
            dataGridView1.DataSource = tablo;
            dataGridView1.Columns[0].HeaderText = "İşlem";
            dataGridView1.Columns[1].Visible = false; //ID
            dataGridView1.Columns[2].Visible = false; //Durum
            dataGridView1.Columns[3].HeaderText = "Adı"; //Adı
            dataGridView1.Columns[4].HeaderText = "Soyadı"; //Soyadı
            dataGridView1.Columns[5].Visible = false; //Adres
            dataGridView1.Columns[6].Visible = false; //Kimlikno
            dataGridView1.Columns[7].Visible = false; //Ceptel1
            dataGridView1.Columns[8].Visible = false;//Ceptel2
            dataGridView1.Columns[9].Visible = false; //Evtel
            dataGridView1.Columns[10].Visible = false; //AdslNo
            dataGridView1.Columns[11].Visible = false; //KayitTarihi
            dataGridView1.Columns[12].Visible = false; //KullaniciAdi
            dataGridView1.Columns[13].Visible = false; //Sifre
            dataGridView1.Columns[14].Visible = false;//IP
            dataGridView1.Columns[15].Visible = false; //Notlar
            dataGridView1.Columns[16].Visible = false; //Kira
            dataGridView1.Columns[17].Visible = false; //Kira
            dataGridView1.Columns[18].Visible = false; //Kira

        }
        private void Anasayfa_Load(object sender, EventArgs e)
        {

            webBrowser1.Visible = false;
            groupBox2.Visible = false;
            yukle();
            //if (Giris.comboBox1.SelectedIndex == 0)
            //{
            //    label1.Visible = true;
            //    groupBox1.Visible = true;
            //}
            //if (Giris.comboBox1.SelectedIndex == 1)
            //{
            //    label1.Text = "Hoşgeldin Kullanıcı";
            //    groupBox1.Enabled = false;
            //    groupBox3.Enabled= false;
            //}
            //groupBox2.Visible = false;
            try
            {
               button6.Enabled = gbUserPanel.Visible = gbAdminPanel.Visible = _user.UserType == 1 ? true : false;
                if (_user.UserType == 0) {label1.Text = "Hoşgeldin Kullanıcı"; a=0;}
                if (_user.UserType == 1) { label1.Text = "Hoşgeldin Admin"; a = 1; }
            }
            catch (Exception)
            {
                 gbUserPanel.Visible = gbAdminPanel.Visible = a == 1 ? true:false;
                // if (a == 0)  label1.Text = "Hoşgeldin " + _user.UserName; 
                // if (a == 1)  label1.Text = "Hoşgeldin " + _user.UserName;
               
            
            }
        }
        public void ara(string ara, string yer)
        {
            if (yer.Trim() == "")
            {
                listele();
            }
            else
            {
                OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
                DataTable tablo = new DataTable();
                tablo.Clear();
                OleDbDataAdapter adapter = new OleDbDataAdapter("select * From tblMusteri where " + ara + " like '%" + yer + "%'", baglan);
                adapter.Fill(tablo);
                dataGridView1.DataSource = tablo;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            musEkle musEkle = new musEkle();
            musEkle.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cihaz Cihaz = new Cihaz();
            Cihaz.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kulGuncelle kulGuncelle = new kulGuncelle();
            kulGuncelle.Show();

        }
        /*
        private void textBox1_TextChanged(object sender, EventArgs e)
        {}
        private void textBox2_TextChanged(object sender, EventArgs e)
        {}
        private void textBox3_TextChanged(object sender, EventArgs e)
        {}
        private void textBox4_TextChanged(object sender, EventArgs e)
        {}
        private void textBox5_TextChanged(object sender, EventArgs e)
        {}
        private void textBox6_TextChanged(object sender, EventArgs e)
        {}*/
        private void button4_Click(object sender, EventArgs e)
        {
            button4.Visible = false;
            groupBox2.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // this.Hide();
            // Giris.musEkle.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            button4.Visible = false;
            groupBox2.Visible = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            musEkle musEkle = new musEkle();
            musEkle.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            kulGuncelle kulGuncelle = new kulGuncelle();
            kulGuncelle.Show();

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Cihaz Cihaz = new Cihaz();
            Cihaz.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            kulEkle kulEkle = new kulEkle();
            kulEkle.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult uyari;
            uyari = MessageBox.Show("Silmek istediğinize Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (uyari == DialogResult.Yes)
            {
                OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
                OleDbCommand kmt = new OleDbCommand("DELETE FROM tblMusteri Where ID=" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "", baglan);
                baglan.Open();
                kmt.ExecuteNonQuery();
                baglan.Close();
                listele();
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells[1].Value != null)
            {
                musIslem mi = new musIslem((int)dataGridView1.SelectedCells[1].Value,_user);
                mi.Show();

            }
        }

        private Form Main()
        {
            throw new NotImplementedException();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool s = false;
            if (checkBox1.Checked == true) s = true;
            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
            DataTable tablo = new DataTable();
            tablo.Clear();
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * From tblMusteri where Durumu=" + s + "", baglan);
            adapter.Fill(tablo);
            dataGridView1.DataSource = tablo;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Paket Paket = new Paket();
            Paket.Show();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string al = textBox1.Text;
            ara("Adi", al);

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            string al = textBox2.Text;
            ara("Soyad", al);
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            string al = textBox3.Text;
            ara("Ceptel1", al);
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            string al = textBox4.Text;
            ara("Ceptel2", al);
        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            string al = textBox5.Text;
            ara("AdslNo", al);

        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {
            string al = textBox6.Text;
            ara("KimlikNo", al);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button10.Visible = true;
            webBrowser1.Visible = true;
            gbUserPanel.Visible = false;
            groupBox2.Visible = false;
            gbAdminPanel.Visible = false;
            dataGridView1.Visible = false;
            button1.Visible = false;
            button4.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button9.Visible = false;
            try
            {
                webBrowser1.Navigate(dataGridView1.CurrentRow.Cells[14].Value.ToString());
                while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                {
                    // Burada webbrowser document complete olmasını bekliyoruz. 
                    Application.DoEvents(); //Bu kod ile programın kitlemesini önlüyor.
                }
                HtmlElement user = webBrowser1.Document.GetElementById("username");
                user.SetAttribute("value", "" + dataGridView1.CurrentRow.Cells[12].Value.ToString() + "");
                HtmlElement pass = webBrowser1.Document.GetElementById("password");
                pass.SetAttribute("value", "" + dataGridView1.CurrentRow.Cells[13].Value.ToString() + "");
                HtmlElement button = webBrowser1.Document.GetElementById("submit");
                button.InvokeMember("click");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşterinin Kullanıcı Adı veya Şifresi veya IP'si Eksiktir Veya IP geçersizdir. Lütfen Bu alanları Doldurunuz,Düzeltiniz. /n ");

            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            webBrowser1.Visible = false;
            gbAdminPanel.Visible = a == 1 ? true : false;
            groupBox2.Visible = false;
            gbUserPanel.Visible = a == 1 ? true : false;
            dataGridView1.Visible = true;
            button1.Visible = true;
            button4.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            button9.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            raporlama raporlama = new raporlama();
            raporlama.Show();
        }
    }
}
