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
    public partial class kulEkle : Form
    {
        public Giris Giris;
        public kulEkle()
        {
            InitializeComponent();
        }

        private void musEkle_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            comboBox1.Items.Add("<Seçiniz>");
            comboBox1.Items.Add("Kullanıcı");
            comboBox1.Items.Add("Yönetici");
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var secilen = comboBox1.SelectedItem;
            if (secilen == null)
                return;

            if (secilen != null)
            {
                if (comboBox1.SelectedIndex == 0) // eğer <seçiniz> seçiliyse seçmemiştir
                {
                    MessageBox.Show("Lütfen tip seç");
                    return;
                }
            }

            if (textBox1 == null || textBox2 == null)
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre boş Olmamalıdır");
            }
            else if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "")
            {
                OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
                //OleDbCommand kmt = new OleDbCommand("INSERT INTO tblKullanici(KullaniciAdi,Sifre,Yetki) Values ('" + textBox1.Text + "','" 
                //    + textBox2.Text + "'" 
                //    + comboBox1.Items.IndexOf(comboBox1.SelectedItem)
                //    + ")", baglan);

                OleDbCommand kmt = new OleDbCommand("INSERT INTO tblKullanici(KullaniciAdi,Sifre,Yetki) Values (@p1, @p2, @p3)", baglan);
                kmt.Parameters.Add(new OleDbParameter("@p1", textBox1.Text.Trim()));
                kmt.Parameters.Add(new OleDbParameter("@p2", textBox2.Text.Trim()));
                kmt.Parameters.Add(new OleDbParameter("@p3", (comboBox1.SelectedItem == "Kullanıcı") ? 0 : 1));
                baglan.Open();
                kmt.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Ekleme İşlemi Başarılı Oldu");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                MessageBox.Show("Aynı isimde Kullanıcı var !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Anasayfa Anasayfa = new Anasayfa();
            this.Close();
        }
    }
}
