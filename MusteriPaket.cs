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
    public partial class MusteriPaket : Form
    {
        musOdeme musOdeme = new musOdeme();
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
        public Giris Giris;
        public UserRecords _user { get; set; }
        public int _customerID { get; set; }
        object borc, bt, ba,odenen,fiyat;
        public MusteriPaket()
        {
            InitializeComponent();
        }
        public MusteriPaket(int CustomerID, UserRecords User,object girilen)
        {
            InitializeComponent();
            _customerID = CustomerID;
            _user = User;
        }
      
        public void listele()
        {
            DataTable tablo = new DataTable();
            tablo.Clear();
            StringBuilder sb = new StringBuilder("SELECT tblMusteriPaket.ID,tblPaket.ID,tblPaket.Adi,tblMusteriPaket.Fiyat,tblMusteriPaket.BaslangicT,tblMusteriPaket.BitisT FROM tblPaket INNER JOIN tblMusteriPaket ON tblPaket.ID = tblMusteriPaket.PaketId ");
            sb.Append("WHERE tblMusteriPaket.MusteriId = @p1");
            OleDbCommand cmd = new OleDbCommand(sb.ToString(), baglan);
            cmd.Parameters.AddWithValue("@p1", _customerID);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            try
            {
                baglan.Open();
                adapter.Fill(tablo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                baglan.Close();
            }

            dataGridView1.DataSource = tablo;
           OleDbCommand cmd1 = new OleDbCommand("Select  SUM(Tutari) from tblOdemeler where MusteriId = @p2", baglan);
            cmd1.Parameters.AddWithValue("@p2", _customerID);
            baglan.Open();
            ba = cmd1.ExecuteScalar();
            baglan.Close();
            odenen = ba;
            cmd1 = new OleDbCommand("SELECT SUM(tblMusteriPaket.Fiyat) FROM tblMusteriPaket where MusteriId=@p2", baglan);
            cmd1.Parameters.AddWithValue("@p2", _customerID);
            baglan.Open();
            bt = cmd1.ExecuteScalar();
            baglan.Close();
            borc = bt;

            int toplam = (bt== DBNull.Value ? 0 : Convert.ToInt32(bt)) - (ba == DBNull.Value ? 0: Convert.ToInt32(ba));
            label5.Text = toplam.ToString();
        }
        private void MusteriPaket_Load(object sender, EventArgs e)
        {
            
            this.WindowState = FormWindowState.Maximized;
            listele();
            string sorgu = "Select ID, Adi from tblPaket";
            OleDbDataAdapter df = new OleDbDataAdapter(sorgu, baglan);
            baglan.Open();
            df.Fill(ds1, "tblPaket");
            baglan.Close();

            if (baglan.State != ConnectionState.Open)
                baglan.Open();
            else if (baglan.State == ConnectionState.Open)
                baglan.Close();

            foreach (DataRow Satir in ds1.Tables["tblPaket"].Rows)
            {
                string deger = Satir[0].ToString();
                bool durum = cmbPaket.Items.Contains(deger);
                if (!durum)
                {
                    cmbPaket.Items.Add(deger).ToString();
                }
                baglan.Close();
                
            }
            cmbPaket.DataSource = ds1.Tables["tblPaket"];
            
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "Paket Adı";//Durumu
            dataGridView1.Columns[3].HeaderText = "Fiyat";//Durumu
            dataGridView1.Columns[4].HeaderText = "Başlangıç Tarihi"; //Adi
            dataGridView1.Columns[5].HeaderText = "Bitiş Tarihi"; //Soyad

            if (_user.UserType == 1)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false; 
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand kmt = new OleDbCommand
            ("select tblPaket.Fiyat from tblPaket where ID=@p1", baglan);
            kmt.Parameters.Add(new OleDbParameter("@p1", cmbPaket.SelectedValue));
            baglan.Open();//bağlantı açıldı
            fiyat = kmt.ExecuteScalar();
            baglan.Close();//bağlantı kapandı
             kmt = new OleDbCommand
            ("INSERT INTO tblMusteriPaket(MusteriId,PaketId,Fiyat,BaslangicT,BitisT) Values(@p1,@p2,@p3,@p4,p5)", baglan);
            kmt.Parameters.Add(new OleDbParameter("@p1", _customerID));
            kmt.Parameters.Add(new OleDbParameter("@p2", cmbPaket.SelectedValue));
            kmt.Parameters.Add(new OleDbParameter("@p3", fiyat));
            kmt.Parameters.Add(new OleDbParameter("@p4", dateTimePicker1.Text));
            kmt.Parameters.Add(new OleDbParameter("@p5", dateTimePicker2.Text));
            //kmt.Parameters.Add(new OleDbParameter("@p3", (comboBox1.SelectedItem == "Kullanıcı") ? 0 : 1));
            baglan.Open();//bağlantı açıldı
            kmt.ExecuteNonQuery();//sorguyu çalıştır
            baglan.Close();//bağlantı kapandı
            DialogResult uyari;
            uyari = MessageBox.Show("Ekleme Başarılı", "Uyarı", MessageBoxButtons.OK);
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult uyari;
            uyari = MessageBox.Show("Silmek istediğinize Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (uyari == DialogResult.Yes)
            {
                OleDbCommand kmt = new OleDbCommand("DELETE FROM tblMusteriPaket Where ID=" + dataGridView1.SelectedCells[0].Value.ToString() + "", baglan);
                baglan.Open();
                kmt.ExecuteNonQuery();
                baglan.Close();
                listele();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
           // Giris.musIslem.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
