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
using System.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace CAGNET
{
    public partial class Giris : Form
    {
        MainWindow Main = new MainWindow();

        public Anasayfa Anasayfa;
        object girilen;
        int yetki = 0;
        public Giris()
        {
            InitializeComponent();
            Anasayfa = new Anasayfa();
            Anasayfa.Giris = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool sonuc = false;
            Timer bw = new Timer();
            bw.Interval = 86400000;
            bw.Tick += (g, t) =>
            {
                var a = ConfigurationManager.AppSettings["ApplicationCode"];

                string val = Decrypt(a);

                sonuc = DateTime.Now <= DateTime.Parse(val);
                if (!sonuc)
                {
                    Application.Exit();
                    Application.ExitThread();
                    Dispose();
                }
                
            };
            bw.Start();

            

            this.WindowState = FormWindowState.Maximized;
            TextBox2.PasswordChar = '*';
            comboBox1.Items.Add("Yönetici");
            comboBox1.Items.Add("Kullanıcı");
            comboBox1.SelectedIndex = 0;
        }

        static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");

        public static string Encrypt(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException
                       ("The string which needs to be encrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        public static string Decrypt(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException
                   ("The string which needs to be decrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream
                    (Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            int secilen = (comboBox1.SelectedIndex == 1) ? 0 : 1;

            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
            OleDbCommand kmt = new OleDbCommand("select * from tblKullanici where KullaniciAdi='" + TextBox1.Text + "' AND Sifre='" + TextBox2.Text + "' AND Yetki='" + secilen + "'", baglan);
            if (baglan.State != ConnectionState.Closed)
                baglan.Close();

            baglan.Open();
            OleDbDataReader okuyucu = kmt.ExecuteReader();
            if (okuyucu.Read())
            {
                user.UserType = secilen;
                this.Hide();
                MainWindow mw = new MainWindow(user);
                mw.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı veya Parola Yanlış");
            }
            baglan.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        UserRecords user = new UserRecords();

        private void Button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int secilen = (comboBox1.SelectedIndex == 1) ? 0 : 1;

                OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
                OleDbCommand kmt = new OleDbCommand("select * from tblKullanici where KullaniciAdi='" + TextBox1.Text + "' AND Sifre='" + TextBox2.Text + "' AND Yetki='" + secilen + "'", baglan);
                if (baglan.State != ConnectionState.Closed)
                    baglan.Close();

                baglan.Open();
                OleDbDataReader okuyucu = kmt.ExecuteReader();
                if (okuyucu.Read())
                {
                    user.UserType = secilen;
                    this.Hide();
                    MainWindow mw = new MainWindow(user);
                    mw.Show();


                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Parola Yanlış");
                }
                baglan.Close();
            }
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int secilen = (comboBox1.SelectedIndex == 1) ? 0 : 1;
                OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
                OleDbCommand kmt = new OleDbCommand("select * from tblKullanici where KullaniciAdi='" + TextBox1.Text + "' AND Sifre='" + TextBox2.Text + "' AND Yetki='" + secilen + "'", baglan);
                if (baglan.State != ConnectionState.Closed)
                    baglan.Close();

                baglan.Open();
                OleDbDataReader okuyucu = kmt.ExecuteReader();
                if (okuyucu.Read())
                {
                    user.UserType = secilen;
                    this.Hide();
                    MainWindow mw = new MainWindow(user);
                    mw.Show();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Parola Yanlış");
                }
                baglan.Close();
            }

        }

        private void Giris_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int secilen = (comboBox1.SelectedIndex == 1) ? 0 : 1;

                OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
                OleDbCommand kmt = new OleDbCommand("select * from tblKullanici where KullaniciAdi='" + TextBox1.Text + "' AND Sifre='" + TextBox2.Text + "' AND Yetki='" + secilen + "'", baglan);
                if (baglan.State != ConnectionState.Closed)
                    baglan.Close();

                baglan.Open();
                OleDbDataReader okuyucu = kmt.ExecuteReader();
                if (okuyucu.Read())
                {
                    user.UserType = secilen;
                    this.Hide();
                    MainWindow mw = new MainWindow(user);
                    mw.Show();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Parola Yanlış");
                }
                baglan.Close();
            }
        }
    }
}
