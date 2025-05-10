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
  
    public partial class Readers : Form
    {
        DBLibrary DBLibrary2 = new DBLibrary();
        int selectedRows;
        enum RowState2
        {
            Existed, Modified, ModifiedNew, Deleted
        }
        public Readers()
        {
            InitializeComponent();
            this.MaximizeBox = false;
           
        }

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchstr = $"select * from Readers where concat (id, last_name, first_name, middle_name, phone, Address, email) like '%" + textBox1.Text + "%'";
            SqlCommand comm = new SqlCommand(searchstr, DBLibrary2.getConnection());

            DBLibrary2.OpenConnection();

            SqlDataReader read = comm.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }

            read.Close();
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("last_name", "Прізвище");
            dataGridView1.Columns.Add("first_name", "Ім'я");
            dataGridView1.Columns.Add("middle_name", "По батькові");
            dataGridView1.Columns.Add("phone", "Телефон");
            dataGridView1.Columns.Add("Address", "Адреса");
            dataGridView1.Columns.Add("email", "Email");
            dataGridView1.Columns.Add("IsNew1", String.Empty);

        }

        
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3),
                record.GetString(4), record.GetString(5), record.GetString(6), RowState2.ModifiedNew);

        }


        private void RefreshDataGridView(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string str = $"select * from Readers";
            SqlCommand command = new SqlCommand(str, DBLibrary2.getConnection());

            DBLibrary2.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }

            reader.Close();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Add_readers add_readers = new Add_readers();
            this.Hide();
            add_readers.ShowDialog();
            this.Show();
        }

        private void Update()
        {
            DBLibrary2.OpenConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState2)dataGridView1.Rows[index].Cells[7].Value;

                if (rowState == RowState2.Existed)
                {
                    continue;
                }

                if (rowState == RowState2.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deletestr = $"delete from Readers where id = {id}";

                    var command = new SqlCommand(deletestr, DBLibrary2.getConnection());

                    command.ExecuteNonQuery();
                }

                if (rowState == RowState2.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var last_name = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var first_name = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var middle_name = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var phone = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var address = dataGridView1.Rows[index].Cells[5].Value.ToString();
                    var email = dataGridView1.Rows[index].Cells[6].Value.ToString();

                    var query = $"update Readers set last_name = '{last_name}', first_name = '{first_name}', middle_name = '{middle_name}', phone = '{phone}', Address = '{address}', email = '{email}' where id = '{id}'";
                    var command = new SqlCommand(query, DBLibrary2.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            DBLibrary2.CloseConnection();
        }
        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id = textBox2.Text;
            var last_name = textBox3.Text;
            var first_name = textBox4.Text;
            var middle_name = textBox5.Text;
            var phone = textBox6.Text;
            var Address = textBox7.Text;
            var email = textBox8.Text;

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(id, last_name, first_name, middle_name, phone, Address, email);
                dataGridView1.Rows[selectedRowIndex].Cells[7].Value = RowState2.Modified;

            }

        }
        private void DeleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[7].Value = RowState2.Deleted;
                return;
            }

            dataGridView1.Rows[index].Cells[7].Value = RowState2.Deleted;
        }
        private void Readers_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGridView(dataGridView1);
            dataGridView1.Columns[7].Visible = false;

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            RefreshDataGridView(dataGridView1);
        }



        private void button2_Click(object sender, EventArgs e)
        {
            Change();
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

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRows = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRows];

                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox4.Text = row.Cells[2].Value.ToString();
                textBox5.Text = row.Cells[3].Value.ToString();
                textBox6.Text = row.Cells[4].Value.ToString();
                textBox7.Text = row.Cells[5].Value.ToString();
                textBox8.Text = row.Cells[6].Value.ToString();
            }
        }
    }
}
