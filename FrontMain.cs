using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework
{
    public partial class FrontMain : Form
    {
        public FrontMain()
        {
            InitializeComponent();
            this.MaximizeBox = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Hide();
            main.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Readers readers = new Readers();
            this.Hide();
            readers.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Rent rent = new Rent();
            this.Hide();
            rent.ShowDialog();
            this.Show();
        }

        private void FrontMain_Load(object sender, EventArgs e)
        {

        }
    }
}
