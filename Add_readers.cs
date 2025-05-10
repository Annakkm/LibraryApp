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
    public partial class Add_readers : Form
    {
        DBLibrary DBLibrary = new DBLibrary();

        public Add_readers()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBLibrary.OpenConnection();

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Помилка !! Заповніть всі поля");

            }
            else
            {
                var last_name = textBox1.Text;
                var first_name = textBox2.Text;
                var middle_name = textBox3.Text;
                var phone = textBox4.Text;
                var Address = textBox5.Text;
                var email = textBox6.Text;

                var query = $"insert into Readers (last_name, first_name, middle_name, phone, Address, email) values ('{last_name}', '{first_name}', '{middle_name}', '{phone}', '{Address}', '{email}')";
                var command = new SqlCommand(query, DBLibrary.getConnection());
                command.ExecuteNonQuery();


                MessageBox.Show("Запис створений! ", "Успіх ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "")
            {
                MessageBox.Show("Помилка !! Поля пусті ");

            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Readers readers2 = new Readers();
            readers2.Show();
        }
    }
}

