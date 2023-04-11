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
    public partial class Menu : Form
    {
        void ListBooks()
        {
            SqlConnection connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
            connect.Open();
            SqlDataAdapter db = new SqlDataAdapter("SELECT b.NAME, a.NAME as AUTHOR_NAME, p.NAME as PUBLISHER_NAME, b.ISBN_NO, b.CATEGORY FROM BOOK b " +
                                                    "INNER JOIN  AUTHOR a " +
                                                    "ON b.AUTHOR_ID =  a.ID " +
                                                    "INNER JOIN PUBLISHER p " +
                                                    "ON b.PUBLISHER_ID = p.ID", connect);
            DataTable table = new DataTable();
            db.Fill(table);
            dataGridView1.DataSource = table;
            connect.Close();
        }

        
        
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            ListBooks();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBook_Click(object sender, EventArgs e)
        {

            Book book = new Book();
            book.Show();
        }

       

        private void btnAuthor_Click(object sender, EventArgs e)
        {
          
            Form1 author = new Form1();
            author.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
         //   panel2.Controls.Clear();
          //  ListBooks();
        }

        private void btnPublisher_Click(object sender, EventArgs e)
        {
            Publisher publisher = new Publisher();
            publisher.Show();
        }

        private void btnPersonel_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
        
            SqlConnection connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
            connect.Open();
          
            SqlDataAdapter search = new SqlDataAdapter("SELECT b.NAME, a.NAME as AUTHOR_NAME, p.NAME as PUBLISHER_NAME, b.ISBN_NO, b.CATEGORY FROM BOOK b " +
                                                       "INNER JOIN  AUTHOR a " +
                                                       "ON b.AUTHOR_ID =  a.ID " +
                                                       "INNER JOIN PUBLISHER p " +
                                                       "ON b.PUBLISHER_ID = p.ID " +
                                                       "WHERE b.NAME LIKE '" + txtSearch.Text + "%'" +
                                                       "OR p.Name LIKE '" + txtSearch.Text + "%'" +
                                                       "OR a.NAME LIKE '" + txtSearch.Text + "%'" , connect);
                                                 



            DataTable dt = new DataTable();
            search.Fill(dt);
            dataGridView1.DataSource = dt;
            connect.Close();
           
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnMember_Click(object sender, EventArgs e)
        {
            EditMember editMember = new EditMember();
            editMember.Show();
        }
    }
}
