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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        int count = 0;

        private void lblCreateAccount_Click(object sender, EventArgs e)
        {
            SignUp form3 = new SignUp();
            form3.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM [LibraryDB].[dbo].[USER] where USERNAME = @user AND PASSWORD = @password";
            SqlConnection con = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user", txtUsername.Text);
            cmd.Parameters.AddWithValue("@password", txtPassword.Text);
            con.Open();

            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                MessageBox.Show("Successful", "İnformation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Menu menu = new Menu();
                menu.Show();
                //Book book = new Book();
                //book.Show();
            }
            else
            {
                count++;

                if(count <3)
                    MessageBox.Show("Check username or password"," Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                else
                {
                    count = 0;
                    DialogResult dialogResult = MessageBox.Show("Create Account", "Warning" , MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialogResult == DialogResult.Yes)
                    {
                        SignUp signUp = new SignUp();
                        signUp.Show();
                    }
                    
                }


            }
            con.Close();
        }

       

        private void lblClick_Click(object sender, EventArgs e)
        {
            this.Hide();

            LoginMember loginMember = new LoginMember();
            loginMember.Show();
        }
    }
}
