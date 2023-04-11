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
    public partial class SignUp : Form
    {

        SqlConnection connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
        SqlDataAdapter db = new SqlDataAdapter();
        DataTable table = new DataTable();


        public SignUp()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if(textBoxName.Text != "" && textBoxSurname.Text !="" )
            {
                string query = "INSERT INTO PERSONEL (NAME , SURNAME , GENDER, DEPARTMENT, PHONE_NUM, ADRESS, BIRTH_DATE)" +
                                       "VALUES (@name, @surname, @gender, @department, @phoneNo, @adress, @birthDate)";

                string returnID = "Select @@Identity"; // return last ID value


                SqlCommand command = new SqlCommand(query, connect);

                command.Parameters.AddWithValue("@name", textBoxName.Text);
                command.Parameters.AddWithValue("@surname", textBoxSurname.Text);
                command.Parameters.AddWithValue("@phoneNo", maskedTextBoxPhone.Text);
                command.Parameters.AddWithValue("@adress", txtAdress.Text);
                command.Parameters.AddWithValue("@birthDate", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@department", comboBox1.GetItemText(comboBox1.SelectedItem));

                if (radioButtonMale.Checked)
                    command.Parameters.AddWithValue("@gender", "Male");
                else
                    command.Parameters.AddWithValue("@gender", "Female");

                connect.Open();
                command.ExecuteNonQuery();

                int personalId = 0;
                command.CommandText = returnID;
                personalId = (int)(decimal)command.ExecuteScalar();

                connect.Close();

                MessageBox.Show("Registration Successful", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if(textBoxUsername.Text != "" || textBoxPassword.Text != "")
                {
                    string query2 = "INSERT INTO dbo.[USER](USERNAME, PASSWORD, PERSONAL_ID)" +
                                                    "VALUES(@username, @password, @personalId)";

                   
                    SqlCommand command2 = new SqlCommand(query2, connect);

                    command2.Parameters.AddWithValue("@username", textBoxUsername.Text);
                    command2.Parameters.AddWithValue("@password", textBoxPassword.Text);
                    command2.Parameters.AddWithValue("@personalId", personalId);

                    connect.Open();
                    command2.ExecuteNonQuery();
                    connect.Close();

                }

            }

            
           
        }

        private void lblAdress_Click(object sender, EventArgs e)
        {

        }

        private void lblBirdtDate_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
