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
    public partial class SignUpMember : Form
    {
        public SignUpMember()
        {
            InitializeComponent();
        }

        SqlConnection connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");

      
        
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtName.Text != "" && txtID.Text != "")
            {
                string query = "INSERT INTO MEMBER (NAME , IDENTITY_NO , GENDER, JOB, PHONE_NUM, ADRESS, BIRTHDATE)" +
                                       "VALUES (@name, @identity, @gender, @job, @phoneNo, @adress, @birthDate)";

                


                SqlCommand command = new SqlCommand(query, connect);

                command.Parameters.AddWithValue("@name", txtName.Text);
                command.Parameters.AddWithValue("@identity", txtID.Text);
                command.Parameters.AddWithValue("@phoneNo", maskedTextBoxPhone.Text);
                command.Parameters.AddWithValue("@adress", txtAdress.Text);
                command.Parameters.AddWithValue("@birthDate", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@job", txtJob.Text);

                if (radioButtonMale.Checked)
                    command.Parameters.AddWithValue("@gender", "Male");
                else
                    command.Parameters.AddWithValue("@gender", "Female");

                connect.Open();
                command.ExecuteNonQuery();


                connect.Close();

                MessageBox.Show("Registration Successful", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

           

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

            LoginMember login = new LoginMember();
            login.Show();
        }
    }
}
