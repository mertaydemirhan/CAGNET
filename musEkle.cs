using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CAGNET
{
    public partial class musEkle : Form
    {
        public Giris Giris;
        public musEkle()
        {
            InitializeComponent();
        }

        private void kulEkle_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

        }
            /* Cihaz Adı*//*
            ds.Clear();
            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
            baglan.Open();
            string sorgu = "Select Marka from tblAnten";
            OleDbDataAdapter da = new OleDbDataAdapter(sorgu, baglan);
            da.Fill(ds, "tblAnten");
            foreach (DataRow Satir in ds.Tables["tblAnten"].Rows)
            {
                string deger = Satir[0].ToString();
                bool durum = cmbCihaz.Items.Contains(deger);
                if (!durum)
                {
                    cmbCihaz.Items.Add(deger).ToString();
                }
                baglan.Close();
            }
            /* Paket*//*
            ds.Clear();
            baglan.Open();
            sorgu = "Select ID, Adi from tblPaket";
            OleDbDataAdapter df = new OleDbDataAdapter(sorgu, baglan);
            df.Fill(ds1, "tblPaket");
            baglan.Close();
            //foreach (DataRow Satir in ds1.Tables["tblPaket"].Rows)
            //{
            //    string deger = Satir[0].ToString();
            //    bool durum = comboBox1.Items.Contains(deger);
            //    if (!durum)
            //    {
            //        comboBox1.Items.Add(deger).ToString();
            //    }
            //    baglan.Close();
            //}
            cmbPaket.DataSource = ds1.Tables["tblPaket"];
            /* Cihaz modeli*//*
            ds.Clear();
            baglan.Open();
            string sorgu1 = "Select Model from tblAnten";
            OleDbDataAdapter dta = new OleDbDataAdapter(sorgu1, baglan);
            dta.Fill(ds2, "tblAnten");
            foreach (DataRow Satir in ds2.Tables["tblAnten"].Rows)
            {
                string deger = Satir[0].ToString();
                bool durum = comboBox3.Items.Contains(deger);
                if (!durum)
                {
                    comboBox3.Items.Add(deger).ToString();
                }
                baglan.Close();
            }
        }

        private void CihazYukle()
        {
            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
            DataTable dt = new DataTable();
            OleDbDataAdapter dta = new OleDbDataAdapter("Select ID, Marka from tblAnten", baglan);
            baglan.Open();
            dta.Fill(dt);
            baglan.Close();
            cmbCihaz.DataSource = dt;
            cmbCihaz.Items.Insert(0, "<Seçiniz>");
        }

        private void PaketYukle()
        {
            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
            DataTable dt = new DataTable();
            OleDbDataAdapter dta = new OleDbDataAdapter("Select ID, Marka from tblAnten", baglan);
            baglan.Open();
            dta.Fill(dt);
            baglan.Close();
            cmbCihaz.DataSource = dt;
            cmbCihaz.Items.Insert(0, "<Seçiniz>");
        }
       */
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string kira = "Evet";
                if (checkBox1.Checked == false) kira = "Hayır";
                OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
                OleDbCommand kmt = new OleDbCommand
                ("INSERT INTO tblMusteri(Adi,Soyad,Adres,KimlikNo,Ceptel1,Ceptel2,Evtel,AdslNo,KayitTarihi,KullaniciAdi,Sifre,Ip,Notlar,Kira,SozlesmeBasT,SozlesmeBitT) Values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@15,@16)", baglan);
                kmt.Parameters.Add(new OleDbParameter("@p1", textBox1.Text.Trim()));
                kmt.Parameters.Add(new OleDbParameter("@p2", textBox2.Text.Trim()));
                kmt.Parameters.Add(new OleDbParameter("@p3", richTextBox1.Text.Trim()));
                kmt.Parameters.Add(new OleDbParameter("@p4", textBox3.Text.Trim()));
                kmt.Parameters.Add(new OleDbParameter("@p5", textBox4.Text.Trim()));
                kmt.Parameters.Add(new OleDbParameter("@p6", textBox5.Text.Trim()));
                kmt.Parameters.Add(new OleDbParameter("@p7", textBox6.Text.Trim()));
                kmt.Parameters.Add(new OleDbParameter("@p8", textBox7.Text.Trim()));
                kmt.Parameters.Add(new OleDbParameter("@p9", dateTimePicker1.Text));
                kmt.Parameters.Add(new OleDbParameter("@p10", textBox8.Text.Trim()));
                kmt.Parameters.Add(new OleDbParameter("@p11", textBox9.Text.Trim()));
                kmt.Parameters.Add(new OleDbParameter("@p12", textBox10.Text.Trim()));
                kmt.Parameters.Add(new OleDbParameter("@p13", richTextBox2.Text));
                kmt.Parameters.Add(new OleDbParameter("@p14", kira));
                kmt.Parameters.Add(new OleDbParameter("@p15", dateTimePicker2.Text));
                kmt.Parameters.Add(new OleDbParameter("@p16", dateTimePicker3.Text));
                //kmt.Parameters.Add(new OleDbParameter("@p3", (comboBox1.SelectedItem == "Kullanıcı") ? 0 : 1));
                baglan.Open();//bağlantı açıldı
                kmt.ExecuteNonQuery();//sorguyu çalıştır
                baglan.Close();//bağlantı kapandı
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
                richTextBox1.Clear();
                richTextBox2.Clear();
                textBox1.Clear();
                textBox1.Clear();
                textBox1.Clear();

                DialogResult uyari;
                uyari = MessageBox.Show("Ekleme Başarılı", "Uyarı", MessageBoxButtons.OK);
                if (uyari == DialogResult.OK)
                {
                    this.Hide();
                    Anasayfa Anasayfa = new Anasayfa();
                    this.Close();
                }
            }
            catch (OleDbException) {

                MessageBox.Show("Hata Oluştu ! Tekrar Dene");
                return;
            }
            }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        }

    }

