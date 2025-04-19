using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zoo
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //this.Hide();
            //Auth auth = new Auth();
            //auth.ShowDialog();

            AuthClass.Fio = "Пупкин И.И.";
            toolStripStatusLabel2.Text = AuthClass.Fio;
        }
               

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sprav.Employs employs = new Sprav.Employs();
            employs.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
