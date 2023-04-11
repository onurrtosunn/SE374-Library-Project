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
    public partial class LoginMember : Form
    {
        public LoginMember()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
        public static string SetValueForText1 = "";
        private void lblCreateAccount_Click(object sender, EventArgs e)
        {
            SignUpMember signUpMember = new SignUpMember();
            signUpMember.Show();

            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM MEMBER WHERE IDENTITY_NO = @id";
            SqlConnection con = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", txtID.Text);
            string id = txtID.Text;
            
            con.Open();

            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                MessageBox.Show("Successful", "İnformation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MemberMenu menuMember = new MemberMenu();
                menuMember.Show();

                SetValueForText1 = txtID.Text;

                 String x = txtID.Text;
            }

            else
            {
                MessageBox.Show("Check ID", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          
    }

    
    }
}
