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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Coursework
{
    
    public partial class Rent : Form
    {
        DBLibrary dBLibrary3 = new DBLibrary();
        int selectedRows;
        enum RowState3
        {
            Existed, Modified, ModifiedNew, Deleted
        }
        public Rent()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
        }

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchstr = $"select * from Rent where concat (rentId, bookId, readerId, BorrowDate, ReturnDate, check_return) like '%" + textBox2.Text + "%'";
            SqlCommand comm = new SqlCommand(searchstr, dBLibrary3.getConnection());

            dBLibrary3.OpenConnection();

            SqlDataReader read = comm.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }

            read.Close();
        }
        
        private void CreateColumns()
        {
            dataGridView3.Columns.Add("rentId","id");
            dataGridView3.Columns.Add("bookId", "id - книги");
            dataGridView3.Columns.Add("readerId", "id - читача");
            dataGridView3.Columns.Add("BorrowDate", "Дата орендування");
            dataGridView3.Columns.Add("ReturnDate", "Дата повернення");
            dataGridView3.Columns.Add("check_return", "Здача книг");
            dataGridView3.Columns.Add("IsNew3", String.Empty);

        }
  
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetDateTime(3).ToString(), record.GetDateTime(4).ToString(), record.GetString(5) ,RowState3.ModifiedNew);

        }

        private void RefreshDataGridView(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string str = $"select * from Rent";
            SqlCommand command = new SqlCommand(str, dBLibrary3.getConnection());

            dBLibrary3.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }

            reader.Close();
        }

        private void Rent_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGridView(dataGridView3);
            dataGridView3.Columns[6].Visible = false;
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dBLibrary3.OpenConnection();
            selectedRows = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[selectedRows];

                textBox14.Text = row.Cells[0].Value.ToString();
                textBox13.Text = row.Cells[1].Value.ToString();
                textBox12.Text = row.Cells[2].Value.ToString();
                textBox11.Text = row.Cells[3].Value.ToString();
                textBox10.Text = row.Cells[4].Value.ToString();
                textBox1.Text = row.Cells[5].Value.ToString();
            }
            dBLibrary3.CloseConnection();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Rent add_Rent = new Add_Rent();
            this.Hide();
            add_Rent.ShowDialog();
            this.Show();

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            RefreshDataGridView(dataGridView3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox14.Text = "";
            textBox13.Text = "";
            textBox12.Text = "";
            textBox11.Text = "";
            textBox10.Text = "";
            textBox1.Text = "";
         
        }

        private void DeleteRow()
        {
            int index = dataGridView3.CurrentCell.RowIndex;
            dataGridView3.Rows[index].Visible = false;

            if (dataGridView3.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView3.Rows[index].Cells[6].Value = RowState3.Deleted;
                return;
            }
            dataGridView3.Rows[index].Cells[6].Value = RowState3.Deleted;
        }
        private void Change()
        {
            var selectedRowIndex = dataGridView3.CurrentCell.RowIndex;

            var rentId =textBox14.Text;
            var bookId = textBox13.Text;
            var readerId = textBox12.Text;
            var BorrowDate = textBox11.Text;
            var ReturnDate = textBox10.Text;
            var check_return = textBox1.Text;

            if (dataGridView3.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView3.Rows[selectedRowIndex].SetValues(rentId, bookId, readerId, BorrowDate, ReturnDate, check_return);
                dataGridView3.Rows[selectedRowIndex].Cells[6].Value = RowState3.Modified;

            }

        }
        private void Update()
        {
            for (int index = 0; index < dataGridView3.Rows.Count; index++)
            {
                dBLibrary3.OpenConnection();

                var rowstate3 = (RowState3)dataGridView3.Rows[index].Cells[6].Value;

                if (rowstate3 == RowState3.Existed) 
                {
                    continue; 
                }

                if (rowstate3 == RowState3.Deleted) {
                    var id = Convert.ToInt32(dataGridView3.Rows[index].Cells[0].Value);
                    var querystr = $"delete from Rent where rentId = {id}";
                    var command= new SqlCommand(querystr, dBLibrary3.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowstate3 == RowState3.Modified)
                {
                    var rentId = dataGridView3.Rows[index].Cells[0].Value.ToString();
                    var bookId = dataGridView3.Rows[index].Cells[1].Value.ToString();
                    var readerId = dataGridView3.Rows[index].Cells[2].Value.ToString();
                    var BorrowDate = dataGridView3.Rows[index].Cells[3].Value.ToString();
                    var ReturnDate = dataGridView3.Rows[index].Cells[4].Value.ToString();
                    var check_return = dataGridView3.Rows[index].Cells[5].Value.ToString();


                    var query = "UPDATE Rent SET bookId = @bookId, readerId = @readerId, BorrowDate = @BorrowDate, ReturnDate = @ReturnDate, check_return = @check_return WHERE rentId = @rentId";

                    var command = new SqlCommand(query, dBLibrary3.getConnection());

                    command.Parameters.AddWithValue("@bookId", bookId);
                    command.Parameters.AddWithValue("@readerId", readerId);
                    command.Parameters.AddWithValue("@BorrowDate", Convert.ToDateTime(BorrowDate));
                    command.Parameters.AddWithValue("@ReturnDate", Convert.ToDateTime(ReturnDate));
                    command.Parameters.AddWithValue("@check_return", check_return);
                    command.Parameters.AddWithValue("@rentId", rentId);
                    command.ExecuteNonQuery();

                }
            }
            dBLibrary3.CloseConnection();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Change();
        }
    }
}
