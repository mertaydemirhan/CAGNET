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
    public partial class musIslem : Form
    {
        public Giris Giris;
        public int _customerID { get; set; }
        public UserRecords _user { get; set; }
        public object girilen { get; set; }
        object ba;
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OleDB.12.0;Data Source=" + Application.StartupPath + "\\db.accdb");

        public musIslem()
        {
            InitializeComponent();
        }

        public musIslem(int customerID)
        {
            InitializeComponent();
            _customerID = customerID;
        }
        public musIslem(int CustomerID, UserRecords User)
        {
            InitializeComponent();
            _user = User;
            _customerID = CustomerID;
        }
        public musIslem(object girilen)
        {
            InitializeComponent();
            girilen=girilen;
        }
            private void listele()
            {
            OleDbCommand cmd = new OleDbCommand("Select * from tblMusteri Where ID = @id", baglan);
            cmd.Parameters.AddWithValue("@id", _customerID);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            baglan.Open();
            da.Fill(dt);
            baglan.Close();
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dt.Rows[0]["Adi"].ToString();
                textBox2.Text = dt.Rows[0]["Soyad"].ToString();
                richTextBox1.Text = dt.Rows[0]["Adres"].ToString();
                textBox3.Text = dt.Rows[0]["KimlikNo"].ToString();
                textBox4.Text = dt.Rows[0]["Ceptel1"].ToString();
                textBox5.Text = dt.Rows[0]["Ceptel2"].ToString();
                textBox6.Text = dt.Rows[0]["Evtel"].ToString();
                textBox7.Text = dt.Rows[0]["AdslNo"].ToString();
                dateTimePicker1.Text = dt.Rows[0]["KayitTarihi"].ToString();
                textBox8.Text = dt.Rows[0]["KullaniciAdi"].ToString();
                textBox9.Text = dt.Rows[0]["Sifre"].ToString();
                textBox10.Text = dt.Rows[0]["Ip"].ToString();
                richTextBox2.Text = dt.Rows[0]["Notlar"].ToString();
                dateTimePicker2.Text = dt.Rows[0]["SozlesmeBasT"].ToString();
                dateTimePicker3.Text = dt.Rows[0]["SozlesmeBitT"].ToString();
                if (dt.Rows[0]["Kira"].ToString() == "Evet") { checkBox1.Checked = true; }
                if (dt.Rows[0]["Kira"].ToString() == "Hayır") { checkBox1.Checked = false; }
                checkBox2.Checked = Convert.ToBoolean(dt.Rows[0]["Durumu"].ToString());
            }


        }
        private void musIslem_Load(object sender, EventArgs e)
        {
            listele();
            textBox9.Visible = label14.Visible = textBox8.Visible = label13.Visible = textBox10.Visible = label15.Visible = _user.UserType == 1 ? true : false;
            this.WindowState = FormWindowState.Maximized;
            groupBox2.Visible = false;
            comboBox2.Items.Add("Mikrotik SXD");
            comboBox2.Items.Add("Mikrotik 411");
            comboBox2.Items.Add("Mikrotik BUDLET");
            comboBox2.Items.Add("UBNT NANOBRİGE N5");
            comboBox2.Items.Add("UBNT AIRGRID 23DBI");
            comboBox2.Items.Add("UBNT AIRGRID 27DBI");
            comboBox2.Items.Add("UBNT NANOSTATION M5");
            comboBox2.Items.Add("UBNT NANOSTATION M5 LOCO");
            comboBox2.Items.Add("APARTMAN SİSTEMİ");

        }
        private void LoadInformation(int CustomerID)
        {

        }
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        { }
        private void button1_Click(object sender, EventArgs e)
        {
            string kira = "Evet";
            if (checkBox1.Checked == false) kira = "Hayır";
            bool Chk1 = false;
            if (checkBox2.Checked == true) Chk1 = true;
            OleDbCommand asd = new OleDbCommand
            ("UPDATE tblMusteri SET Adi=@p1,Soyad=@p2,Adres=@p3,KimlikNo=@p4,Ceptel1=@p5,Ceptel2=@p6,Evtel=@p7,AdslNo=@p8,KayitTarihi=@p9,KullaniciAdi=@p10,Sifre=@p11,Ip=@p12,Notlar=@p13,Kira=@p14,Durumu=@p15,SozlesmeBasT=@p16,SozlesmeBitT=@17 where ID=@p18", baglan);
             asd.Parameters.Add(new OleDbParameter("@p1",textBox1.Text));
             asd.Parameters.Add(new OleDbParameter("@p2",textBox2.Text));
             asd.Parameters.Add(new OleDbParameter("@p3",richTextBox1.Text));
             asd.Parameters.Add(new OleDbParameter("@p4",textBox3.Text));
             asd.Parameters.Add(new OleDbParameter("@p5",textBox4.Text));
             asd.Parameters.Add(new OleDbParameter("@p6",textBox5.Text));
             asd.Parameters.Add(new OleDbParameter("@p7",textBox6.Text));
             asd.Parameters.Add(new OleDbParameter("@p8",textBox7.Text));
             asd.Parameters.Add(new OleDbParameter("@p9",dateTimePicker1.Text));
             asd.Parameters.Add(new OleDbParameter("@p10",textBox8.Text));
             asd.Parameters.Add(new OleDbParameter("@p11",textBox9.Text));
             asd.Parameters.Add(new OleDbParameter("@p12",textBox10.Text));
             asd.Parameters.Add(new OleDbParameter("@p13",richTextBox2.Text));
             asd.Parameters.Add(new OleDbParameter("@p14",kira));
             asd.Parameters.Add(new OleDbParameter("@p15",Chk1));
             asd.Parameters.Add(new OleDbParameter("@p16",dateTimePicker2.Text));
             asd.Parameters.Add(new OleDbParameter("@p17", dateTimePicker3.Text));
             asd.Parameters.Add(new OleDbParameter("@p18", _customerID));
            baglan.Open();
            asd.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("Düzenleme İşlemi Başarılı Oldu");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MusteriPaket musPkt = new MusteriPaket(_customerID, _user,girilen);
            musPkt.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        { }
        private void button4_Click_1(object sender, EventArgs e)
        {
            CihazEkrani ChzEkrn = new CihazEkrani(_customerID, textBox8.Text, textBox9.Text, textBox10.Text);
            ChzEkrn.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            musOdeme musOdeme = new musOdeme(_customerID, _user);
            musOdeme.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            try
            {
                OleDbCommand kmt = new OleDbCommand("SELECT AntenId FROM tblMusteriAnten Where MusteriId=@p1", baglan);
                kmt.Parameters.Add(new OleDbParameter("@p1", _customerID));
                OleDbCommand kmt1 = new OleDbCommand("SELECT Cihaz FROM tblMusteriAnten Where MusteriId=@p1", baglan);
                kmt1.Parameters.Add(new OleDbParameter("@p1", _customerID));
                ds1.Clear();
                baglan.Open();
                string sorgu = "Select ID, Adi from tblAnten";
                OleDbDataAdapter df = new OleDbDataAdapter(sorgu, baglan);
                df.Fill(ds1, "tblAnten");
                baglan.Close();
                foreach (DataRow Satir in ds1.Tables["tblAnten"].Rows)
                {
                    string deger = Satir[0].ToString();
                    bool durum = comboBox1.Items.Contains(deger);
                    if (!durum)
                    {
                        comboBox1.Items.Add(deger).ToString();
                    }
                    baglan.Close();
                }
                comboBox1.DataSource = ds1.Tables["tblAnten"];
                baglan.Open();
                ba = kmt.ExecuteScalar();
                comboBox1.SelectedValue = Convert.ToInt32(ba) ;
                ba = kmt1.ExecuteScalar();
                comboBox2.SelectedIndex = Convert.ToInt32(ba) - 1;
                baglan.Close();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Hata İle Karşılaşıldı");
                return;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void button7_Click(object sender, EventArgs e)
        { }
        private void button7_Click_1(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand kmt = new OleDbCommand("INSERT INTO tblMusteriAnten(MusteriId,AntenId,Cihaz) Values (@p1, @p2, @p3)", baglan);
                kmt.Parameters.AddWithValue("@p1", _customerID);
                kmt.Parameters.AddWithValue("@p2", (comboBox1.SelectedIndex) + 1);
                kmt.Parameters.AddWithValue("@p3", (comboBox2.SelectedIndex) + 1);
                baglan.Open();
                kmt.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Başarıyla Verici ve Cihaz Kaydedildi.");
            }
            catch (OleDbException)
            {
                baglan.Close();
                OleDbCommand fsd = new OleDbCommand("UPDATE tblMusteriAnten SET AntenId=@p2,Cihaz=@p3  where MusteriId=@p1", baglan);
                fsd.Parameters.Add(new OleDbParameter("@p2", (comboBox1.SelectedValue)));
                fsd.Parameters.Add(new OleDbParameter("@p3", (comboBox2.SelectedIndex) + 1));
                fsd.Parameters.Add(new OleDbParameter("@p1", _customerID));
                baglan.Open();
                fsd.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Başarıyla Verici ve Cihaz Değiştirildi.");
            }
        }
    }
}
