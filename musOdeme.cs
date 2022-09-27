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
    public partial class musOdeme : Form
    {
        public int _customerID { get; set; }
        public UserRecords _user { get; set; }

        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");
        public Giris Giris;
        object ba, bt;
        public musOdeme()
        {
            InitializeComponent();
        }
        public musOdeme(int CustomerID, UserRecords User)
        {
            InitializeComponent();
            _customerID = CustomerID;
            _user = User;
        }
        
        public void listele()
        {
            DataTable tablo = new DataTable();
            tablo.Clear();
            StringBuilder sb = new StringBuilder("SELECT * FROM tblOdemeler ");
            sb.Append("WHERE tblOdemeler.MusteriId = @p1");
            OleDbCommand cmd = new OleDbCommand(sb.ToString(), baglan);
            cmd.Parameters.AddWithValue("@p1", _customerID);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            baglan.Open();
            adapter.Fill(tablo);
            baglan.Close();

            try
            {
                dataGridView1.DataSource = tablo;
                cmd = new OleDbCommand("Select  SUM(Tutari) from tblOdemeler where MusteriId = @p2", baglan);
                cmd.Parameters.AddWithValue("@p2", _customerID);
                baglan.Open();
                ba = cmd.ExecuteScalar();
                baglan.Close();
                label8.Text = ba != DBNull.Value ? ba.ToString() : "0";

                cmd = new OleDbCommand("SELECT SUM(tblMusteriPaket.Fiyat) FROM tblMusteriPaket where MusteriId=@p2", baglan);
                cmd.Parameters.AddWithValue("@p2", _customerID);
                baglan.Open();
                bt = cmd.ExecuteScalar();
                baglan.Close();
                label7.Text = bt != DBNull.Value ? bt.ToString() : "0";

                int toplam = Convert.ToInt32(label7.Text) - Convert.ToInt32(label8.Text);
                lbBakiye.Text = toplam.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Müşterinin herhangi bir Paketi veya Ödemesi bulunmamaktadır.");
                label8.Text = ba != DBNull.Value ? ba.ToString() : "0";
                label7.Text = bt != DBNull.Value ? bt.ToString() : "0";
                int toplam = Convert.ToInt32(label7.Text) - Convert.ToInt32(label8.Text);
                lbBakiye.Text = toplam.ToString();
                return;
            }
            finally
            {
                baglan.Close();
            }
        }
        private void musOdeme_Load(object sender, EventArgs e)
        {
            label8.Text = "0";
            label7.Text = "0";
            lbBakiye.Text = "0";
            this.WindowState = FormWindowState.Maximized;
            listele();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Ödeme Tutarı";//Durumu
            dataGridView1.Columns[2].Visible = false; ;//Adi
            dataGridView1.Columns[3].HeaderText = "Tarih"; //Soyad
            dataGridView1.Columns[4].HeaderText = "Ödeme Türü";
            dataGridView1.Columns[5].HeaderText = "Açıklama";
            button2.Enabled = button4.Enabled = _user.UserType == 1 ? true : false;
            comboBox1.Items.Add("Kredi Kartı");
            comboBox1.Items.Add("Nakit");
            comboBox1.Items.Add("Çek ile Ödeme");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listele();
            this.Hide();
            //Giris.musIslem.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand kmt = new OleDbCommand
                ("INSERT INTO tblOdemeler(Tutari,MusteriId,Tarih,OdemeTuru,Aciklama) Values(@p1,@p2,@p3,@p4,@p5)", baglan);
                kmt.Parameters.Add(new OleDbParameter("@p1", Convert.ToInt32(textBox1.Text)));
                kmt.Parameters.Add(new OleDbParameter("@p2", _customerID));
                kmt.Parameters.Add(new OleDbParameter("@p3", dateTimePicker1.Text));
                kmt.Parameters.Add(new OleDbParameter("@p4", comboBox1.SelectedItem.ToString()));
                kmt.Parameters.Add(new OleDbParameter("@p5", richTextBox1.Text));
                //kmt.Parameters.Add(new OleDbParameter("@p3", (comboBox1.SelectedItem == "Kullanıcı") ? 0 : 1));
                //bağlantı açıldı
                baglan.Open();
                kmt.ExecuteNonQuery();//sorguyu çalıştır
                baglan.Close();//bağlantı kapandı


                DialogResult uyari;
                uyari = MessageBox.Show("Ekleme Başarılı", "Uyarı", MessageBoxButtons.OK);
                listele();
            }
            catch (Exception)
            {
                MessageBox.Show("Boş Kayıt Eklenmemelidir!");
                return;

            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult uyari;
            uyari = MessageBox.Show("Silmek istediğinize Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (uyari == DialogResult.Yes)
            {
                OleDbCommand kmt = new OleDbCommand("DELETE FROM tblOdemeler Where ID=" + dataGridView1.SelectedCells[0].Value.ToString() + "", baglan);
                baglan.Close();
                baglan.Open();
                kmt.ExecuteNonQuery();
                baglan.Close();
                listele();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int cmbTur = 0;
            if (dataGridView1.SelectedCells[3].Value.ToString() == "Nakit") cmbTur = 1;
            textBox1.Text = dataGridView1.SelectedCells[1].Value.ToString();
            dateTimePicker1.Text = dataGridView1.SelectedCells[3].Value.ToString();
            comboBox1.SelectedIndex = cmbTur;
            richTextBox1.Text = dataGridView1.SelectedCells[5].Value.ToString();
        }

        private void musOdeme_Leave(object sender, EventArgs e)
        {
            listele();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
