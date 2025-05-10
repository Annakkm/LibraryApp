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
    public partial class Form1 : Form
    {
        DBLibrary dBLibrary = new DBLibrary();

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '•';
            pictureBox3.Visible = false;
            textBox1.MaxLength = 20;
            textBox2.MaxLength = 20;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
            pictureBox3.Visible = false;
            pictureBox2.Visible = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Помилка !! Заповніть всі поля");
            }
            else
            {
                var login = textBox1.Text;
                var password = textBox2.Text;

                if (check())
                {
                    return;
                }

                string str = $"insert into Register (Login_user, Password_user) values ('{login}','{password}')";
                SqlCommand command = new SqlCommand(str, dBLibrary.getConnection());
                dBLibrary.OpenConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    FrontMain frontMain = new FrontMain();
                    this.Hide();
                    frontMain.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Помилка !! Акаунт не створений");
                }
                dBLibrary.CloseConnection();
            }
        }

        private Boolean check()
        {
            var _login = textBox1.Text;
            var _password = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table1 = new DataTable();

            string str = $"select Id_user, Login_user, Password_user from Register where Login_user = '{_login}' and Password_user = '{_password}'";
            SqlCommand command = new SqlCommand(str, dBLibrary.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table1);

            if (table1.Rows.Count > 0)
            {
                MessageBox.Show("Користувач з таким логіном вже зареєстрований");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            pictureBox3.Visible = true;
            pictureBox2.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
