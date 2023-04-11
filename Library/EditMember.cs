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
    public partial class EditMember : Form
    {
        public EditMember()
        {
            InitializeComponent();
        }

        SqlConnection connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");

        void ListMembers()
        {

            connect.Open();
            SqlDataAdapter db; db = new SqlDataAdapter("SELECT * FROM MEMBER", connect);
            DataTable table = new DataTable();
            db.Fill(table);
            dataGridView1.DataSource = table;
            connect.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be Deleted.", "INFO", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

                SqlCommand command = new SqlCommand();
                command.Connection = connect;

                string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                string Delete = "DELETE FROM MEMBER WHERE ID = '" + id + "'";

                command.CommandText = Delete;

                SqlDataAdapter dA = new SqlDataAdapter(command);
                DataSet DS = new DataSet();
                dA.Fill(DS);

            }

            MessageBox.Show("Delete Successful", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListMembers();
            
        }

        private void EditMember_Load(object sender, EventArgs e)
        {
            ListMembers();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            txtName.Text = row.Cells[1].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(row.Cells[6].Value);
            txtJob.Text = row.Cells[7].Value.ToString();
            txtID.Text = row.Cells[2].Value.ToString();
            txtAdress.Text = row.Cells[4].Value.ToString();
            maskedTextBoxPhone.Text = row.Cells[3].Value.ToString();

            if (row.Cells[5].Value.ToString() == "Male")
                radioButtonMale.Checked = true;
            else
                radioButtonFemale.Checked = true;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be Update?", "INFO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connect;

                command.Parameters.AddWithValue("@name", txtName.Text);
                command.Parameters.AddWithValue("@identity", txtID.Text);
                command.Parameters.AddWithValue("@phone", maskedTextBoxPhone.Text);
                command.Parameters.AddWithValue("@adress", txtAdress.Text);
                command.Parameters.AddWithValue("@birhdate", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@job", txtJob.Text);
                


                if (radioButtonMale.Checked)
                    command.Parameters.AddWithValue("@gender", "Male");
                else
                    command.Parameters.AddWithValue("@gender", "Female");


                string update = "UPDATE MEMBER SET NAME = @name," +
                                              "IDENTITY_NO = @identity," +
                                               "PHONE_NUM = @phone," +
                                                "ADRESS = @adress, " +
                                                "GENDER = @gender, "+
                                                "BIRTHDATE = @birhdate, " +
                                                "JOB = @job "+
                                                "WHERE IDENTITY_NO = @identity";


                command.CommandText = update;

                SqlDataAdapter dA = new SqlDataAdapter(command);
                DataSet DS = new DataSet();
                dA.Fill(DS);
            }
            MessageBox.Show("Update Successful", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListMembers();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
            connect.Open();
            SqlDataAdapter search = new SqlDataAdapter("SELECT * FROM MEMBER " +
                                                        "WHERE IDENTITY_NO LIKE '" + txtSearch.Text + "%'", connect);
            DataTable dt = new DataTable();
            search.Fill(dt);
            dataGridView1.DataSource = dt;
            connect.Close();
        }

        private void btnSAVE_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {

                SqlConnection connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");

                string query = "INSERT INTO MEMBER (NAME, IDENTITY_NO, PHONE_NUM, ADRESS, GENDER, BIRTHDATE, JOB) " +
                                              "VALUES (@name, @identity, @phoneNo, @adress, @gender, @birhdate, @job) ";


                SqlCommand command = new SqlCommand(query, connect);

                command.Parameters.AddWithValue("@name", txtName.Text);
                command.Parameters.AddWithValue("@phoneNo", maskedTextBoxPhone.Text);
                command.Parameters.AddWithValue("@adress", txtAdress.Text);
                command.Parameters.AddWithValue("@identity", txtID.Text);
                command.Parameters.AddWithValue("@birhdate", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@job", txtJob.Text);

                if (radioButtonMale.Checked)
                    command.Parameters.AddWithValue("@gender", "Male");
                else
                    command.Parameters.AddWithValue("@gender", "Female");


                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();

                ListMembers();
            }
        }
    }
}
