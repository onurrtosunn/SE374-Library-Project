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
    public partial class Publisher : Form
    {
        public Publisher()
        {
            InitializeComponent();
        }


        SqlConnection connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");


        void searchPublisher()
        {
            connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
            connect.Open();
            SqlDataAdapter search = new SqlDataAdapter("SELECT * FROM PUBLISHER " +
                                                        "WHERE NAME LIKE '" + txtSearch.Text + "%'", connect);
            DataTable dt = new DataTable();
            search.Fill(dt);
            dataGridView1.DataSource = dt;
            connect.Close();
        }


        void ListPublisher()
        {
           
            connect.Open();
            SqlDataAdapter db; db = new SqlDataAdapter("SELECT * FROM PUBLISHER", connect);
            DataTable table = new DataTable();
            db.Fill(table);
            dataGridView1.DataSource = table;
            connect.Close();
        }

        private void btnSAVE_Click(object sender, EventArgs e)
        {
            if(txtName.Text != "" )
            {

                SqlConnection connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");

                string query = "INSERT INTO PUBLISHER (NAME, PHONE_NUM, ADRESS) " +
                                              "VALUES (@name, @phoneNo, @adress) ";


                SqlCommand command = new SqlCommand(query, connect);

                command.Parameters.AddWithValue("@name", txtName.Text);
                command.Parameters.AddWithValue("@phoneNo", maskedTextBox1.Text);
                command.Parameters.AddWithValue("@adress", richTextBox1.Text);

                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();

                ListPublisher();
            }
        }

        private void Publisher_Load(object sender, EventArgs e)
        {
            ListPublisher();
          
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchPublisher();
        }

        private void lblReflesh_Click(object sender, EventArgs e)
        {
            ListPublisher();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Data will be Update?", "INFO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connect;

                command.Parameters.AddWithValue("@phone", maskedTextBox1.Text);
                command.Parameters.AddWithValue("@name", txtName.Text);
                command.Parameters.AddWithValue("@adress", richTextBox1.Text);
                command.Parameters.AddWithValue("@id", txtID.Text);

                string update = "UPDATE PUBLISHER SET NAME = @name," +
                                                   "ADRESS = @adress,  " +
                                                   "PHONE_NUM = @phone " +
                                                   "WHERE ID = @id";


                command.CommandText = update;

                SqlDataAdapter dA = new SqlDataAdapter(command);
                DataSet DS = new DataSet();
                dA.Fill(DS);
            }
            MessageBox.Show("Update Successful", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListPublisher();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            txtName.Text = row.Cells[1].Value.ToString();
            maskedTextBox1.Text = row.Cells[2].Value.ToString();
            richTextBox1.Text = row.Cells[3].Value.ToString();
            txtID.Text = row.Cells[0].Value.ToString();


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be Deleted.", "INFO", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                string name = txtName.Text;
                string Delete = "DELETE FROM PUBLISHER WHERE NAME = '" + name + "'";

                command.CommandText = Delete;

                SqlDataAdapter dA = new SqlDataAdapter(command);
                DataSet DS = new DataSet();
                dA.Fill(DS);


            }

            MessageBox.Show("Delete Successful", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListPublisher();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
