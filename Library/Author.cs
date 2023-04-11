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

namespace Library
{
    public partial class Form1 : Form
    {

        SqlConnection connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
        SqlDataAdapter db;
        public Form1()
        {
            InitializeComponent();
        }

        void SearchAuthor()
        {
            connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
            connect.Open();
            SqlDataAdapter search = new SqlDataAdapter("SELECT * from AUTHOR " +
                                                        "where NAME like '" + txtSearch.Text + "%'", connect);
            DataTable dt = new DataTable();
            search.Fill(dt);
            dataGridView1.DataSource = dt;
            connect.Close();
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ListAuthor()
        {
            connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
            connect.Open();
            db = new SqlDataAdapter("SELECT * FROM AUTHOR", connect);
            DataTable table = new DataTable();
            db.Fill(table);
            dataGridView1.DataSource = table;
            connect.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListAuthor();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            string insert = "INSERT INTO AUTHOR (NAME, NATIONALITY, BIRTDATE, GENDER) " +
                                        "VALUES (@name, @nationality, @birtdate, @gender)";

            SqlCommand command = new SqlCommand(insert, connect);

            command.Parameters.AddWithValue("@name", txtAuthorName.Text);
            command.Parameters.AddWithValue("@nationality", textBoxNationality.Text);
            command.Parameters.AddWithValue("@birtdate", dateTimePicker1.Value);
            
            if (rBtnMale.Checked)
                command.Parameters.AddWithValue("@gender", "Male");
            else
                command.Parameters.AddWithValue("@gender", "Female");
            
            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();

            ListAuthor();
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchAuthor();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Data will be Update?", "INFO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connect;

                command.Parameters.AddWithValue("@birtdatee", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@name", txtAuthorName.Text);
                command.Parameters.AddWithValue("@nationality", textBoxNationality.Text);
                command.Parameters.AddWithValue("@id", txtID.Text);


                if (rBtnMale.Checked)
                    command.Parameters.AddWithValue("@gender", "Male");
                else
                    command.Parameters.AddWithValue("@gender", "Female");


                string update = "UPDATE AUTHOR SET NAME = @name," +
                                           "NATIONALITY = @nationality,  " +
                                              "BIRTDATE = @birtdatee," +
                                                "GENDER = @gender "+
                                            "WHERE ID = @id";


                command.CommandText = update;

                SqlDataAdapter dA = new SqlDataAdapter(command);
                DataSet DS = new DataSet();
                dA.Fill(DS);
            }
            MessageBox.Show("Update Successful", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListAuthor();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be Deleted.", "INFO", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                string name = txtAuthorName.Text;
                string Delete = "DELETE FROM AUTHOR WHERE NAME = '" + name + "'";

                command.CommandText = Delete;

                SqlDataAdapter dA = new SqlDataAdapter(command);
                DataSet DS = new DataSet();
                dA.Fill(DS);


            }

            MessageBox.Show("Delete Successful", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListAuthor();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            txtAuthorName.Text = row.Cells[1].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(row.Cells[3].Value);
            textBoxNationality.Text = row.Cells[2].Value.ToString();
            txtID.Text = row.Cells[0].Value.ToString();


            if (row.Cells[4].Value.ToString() == "Male")
                rBtnMale.Checked = true;
            else
                rBtnFemale.Checked = true;


        }

        private void lblReflesh_Click(object sender, EventArgs e)
        {
           
            ListAuthor();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
