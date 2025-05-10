using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Coursework
{
    public partial class Login : Form
    {
        DBLibrary dbLibrary = new DBLibrary();

        public Login()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '•';
            pictureBox3.Visible = false;
            textBox1.MaxLength = 20;
            textBox2.MaxLength = 20;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var login = textBox1.Text;
            var password = textBox2.Text;
            if (login != "" && password !="")
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                string str = $" select Id_user, Login_user, Password_user from Register where Login_user = '{login}' and Password_user = '{password}'";

                SqlCommand command = new SqlCommand(str, dbLibrary.getConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count == 1)
                {
                    FrontMain frontMain = new FrontMain();
                    this.Hide();
                    frontMain.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Помилка !! Такого користувача не існує");
                }
            }else
            {
                MessageBox.Show("Помилка !! Введіть всі поля");
            }


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
            pictureBox3.Visible = false;
            pictureBox2.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            pictureBox3.Visible = true;
            pictureBox2.Visible = false;
        }
    }
}