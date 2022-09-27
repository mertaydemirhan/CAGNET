using System;
using System.Windows.Forms;
using CAGNET.Entities;

namespace CAGNET
{
    public partial class MainWindow : Form
    {
        public UserRecords _user { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(UserRecords User)
        {
            InitializeComponent();
            _user = User;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Anasayfa mi = new Anasayfa(_user) { MdiParent = this };
            mi.Show();
        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
