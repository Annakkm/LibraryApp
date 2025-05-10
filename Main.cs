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
using System.Reflection;


namespace Coursework
{
    enum RowState
    {
        Existed, Modified, ModifiedNew, Deleted
    }
    public partial class Main : Form
    {
        DBLibrary DBLibrary = new DBLibrary();
        int selectedRow;
        public Main()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("name_book", "Назва книги");
            dataGridView1.Columns.Add("year_of_publication", "Рік видання");
            dataGridView1.Columns.Add("author", "Автор");
            dataGridView1.Columns.Add("publishing", "Видавництво");
            dataGridView1.Columns.Add("number_of_pages", "Кількість сторінок");
            dataGridView1.Columns.Add("genre", "Жанр");
            dataGridView1.Columns.Add("IsNew", String.Empty);

        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3),
                record.GetString(4), record.GetInt32(5), record.GetString(6), RowState.ModifiedNew);
            
        }

        private void RefreshDataGridView(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string str = $"select * from Books";
            SqlCommand command = new SqlCommand(str, DBLibrary.getConnection());

            DBLibrary.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }

            reader.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGridView(dataGridView1);
            dataGridView1.Columns[7].Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add addform = new Add();
            this.Hide();
            addform.ShowDialog();

            this.Show();
        }

        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            
            var id = textBox2.Text;
            var name_book =textBox3.Text;
            var year_of_publication = textBox4.Text;
            var author = textBox5.Text; 
            var publishing = textBox6.Text;
            var number_of_pages =textBox7.Text;
            var genre = textBox8.Text;

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(id, name_book, year_of_publication, author, publishing, number_of_pages, genre);
                dataGridView1.Rows[selectedRowIndex].Cells[7].Value = RowState.Modified;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Change();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox4.Text = row.Cells[2].Value.ToString();
                textBox5.Text = row.Cells[3].Value.ToString();
                textBox6.Text = row.Cells[4].Value.ToString();
                textBox7.Text = row.Cells[5].Value.ToString();
                textBox8.Text = row.Cells[6].Value.ToString();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            RefreshDataGridView(dataGridView1);
        }

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchstr = $"select * from Books where concat (id, name_book, year_of_publication, author, publishing, number_of_pages, genre) like '%" + textBox1.Text + "%'";
            SqlCommand comm = new SqlCommand(searchstr, DBLibrary.getConnection());

            DBLibrary.OpenConnection();

            SqlDataReader read = comm.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }

            read.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
        }


        private void DeleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[7].Value = RowState.Deleted;
                return;
            }

            dataGridView1.Rows[index].Cells[7].Value = RowState.Deleted;
        }


        private void Update()
        {
            DBLibrary.OpenConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[7].Value;

                if (rowState ==RowState.Existed)
                {
                    continue;
                }

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deletestr = $"delete from Books where id = {id}";

                    var command = new SqlCommand(deletestr, DBLibrary.getConnection());

                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var name_book = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var year_of_publication = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var author = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var publishing = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var number_of_pages = dataGridView1.Rows[index].Cells[5].Value.ToString();
                    var genre = dataGridView1.Rows[index].Cells[6].Value.ToString();
                    var readers_id = dataGridView1.Rows[index].Cells[7].Value.ToString();

                    var query = $"update Books set name_book = '{name_book}', year_of_publication = '{year_of_publication}', author = '{author}', publishing = '{publishing}', number_of_pages = '{number_of_pages}', genre = '{genre}' where id = '{id}'";
                    var command = new SqlCommand(query, DBLibrary.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            DBLibrary.CloseConnection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}

    

