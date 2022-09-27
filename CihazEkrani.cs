using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace CAGNET
{
    public partial class CihazEkrani : Form
    {
        public int _customerID { get; set; }
        public string _kulAdi { get; set; }
        public string _kulSifre { get; set; }
        public string _Ip { get; set; }
        public Giris Giris;
        public CihazEkrani()
        {
            InitializeComponent();
        }
        public CihazEkrani(int CustomerID, string kulAdi, string kulSifre,string Ip)
        {
            InitializeComponent();
            _customerID = CustomerID;
            _kulAdi = kulAdi;
            _kulSifre = kulSifre;
            _Ip = Ip;
        }
        
        private void CihazEkrani_Load(object sender, EventArgs e)
        {
            try
            {
                webBrowser1.Navigate(_Ip);
                while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                {
                    // Burada webbrowser document complete olmasını bekliyoruz. 
                    Application.DoEvents(); //Bu kod ile programın kitlemesini önlüyor.
                }
                HtmlElement user = webBrowser1.Document.GetElementById("username");
                user.SetAttribute("value", "" + _kulAdi.ToString() + "");
                HtmlElement pass = webBrowser1.Document.GetElementById("password");
                pass.SetAttribute("value", "" + _kulSifre.ToString() + "");
                HtmlElement button = webBrowser1.Document.GetElementById("submit");
                button.InvokeMember("click");
            }
            catch (NullReferenceException) {
                MessageBox.Show("Müşterinin Kullanıcı Adı veya Şifresi veya IP'si Eksiktir Veya IP geçersizdir.");
                return;
            
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }

        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }

        private void bilgileriGirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
