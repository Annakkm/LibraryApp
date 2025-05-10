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
    public partial class Add : Form
    {

        DBLibrary DBLibrary = new DBLibrary();
        public Add()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBLibrary.OpenConnection();

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text =="" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Помилка !! Заповніть всі поля");

            }else 
            {
                var name_book = textBox1.Text;
                var year_of_publication = textBox2.Text;
                var author = textBox3.Text;
                var publishing = textBox4.Text;
                var number_of_pages = textBox5.Text;
                var genre = textBox6.Text;

                var query = $"insert into Books (name_book, year_of_publication, author, publishing, number_of_pages, genre) values ('{name_book}', '{year_of_publication}', '{author}', '{publishing}', '{number_of_pages}', '{genre}')";
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Add_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main main6 = new Main();
            main6.Show();
        }
    }
}
