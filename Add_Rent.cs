using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Coursework
{
    
    public partial class Add_Rent : Form
    {
        DBLibrary dbLibrary4 = new DBLibrary();
        public Add_Rent()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main main5  = new Main();
            main5.StartPosition = FormStartPosition.Manual;

            int screenWidth = Screen.PrimaryScreen.Bounds.Width; 
            int screenHeight = Screen.PrimaryScreen.Bounds.Height; 

            int formWidth = main5.Width; 
            int formHeight = main5.Height; 

            int x = (screenWidth - formWidth) / 2 + (screenWidth - formWidth) / 2; 
            int y = (screenHeight - formHeight) / 2; 

            main5.Location = new Point(x, y); 
            main5.Show(); 

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Readers readers5 = new Readers();
            readers5.StartPosition = FormStartPosition.Manual;

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            int formWidth = readers5.Width;
            int formHeight = readers5.Height;

            int x = (screenWidth - formWidth) / 2 + (screenWidth - formWidth) / 2;
            int y = (screenHeight - formHeight) / 2;

            readers5.Location = new Point(x, y);
            readers5.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dbLibrary4.OpenConnection();

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" ||textBox5.Text =="")
            {
                MessageBox.Show("Помилка !! Заповніть всі поля");

            }
            else
            {
                var bookId = textBox1.Text;
                var readerId = textBox2.Text;
                var BorrowDate = textBox3.Text;
                var ReturnDate = textBox4.Text;
                var check_return = textBox5.Text;

                var query = $"insert into Rent (bookId, readerId, BorrowDate, ReturnDate, check_return) values ('{bookId}', '{readerId}', '{BorrowDate}', '{ReturnDate}', '{check_return}')";
                var command = new SqlCommand(query, dbLibrary4.getConnection());
                command.ExecuteNonQuery();


                MessageBox.Show("Запис створений! ", "Успіх ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
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
            }
        }
    }
}
