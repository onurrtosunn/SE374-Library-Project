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
    public partial class MemberMenu : Form
    {
        public MemberMenu()
        {
            InitializeComponent();
           
        }
        SqlConnection connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
                
        void ListBook()
        {
           
            connect.Open();
            
            String sorgu = "SELECT ISBN_NO, b.NAME, p.NAME as PUBLISHER_NAME, a.NAME as AUTHOR_NAME, LANGUAGE, CATEGORY, PAGE_NUM ,b.ID FROM ((BOOK b " +
                             "INNER JOIN PUBLISHER p " +
                             "ON b.PUBLISHER_ID = p.ID) " +
                             "INNER JOIN AUTHOR a " +
                             "ON b.AUTHOR_ID = a.ID)" +
                             "WHERE b.FLAG = 0";

           

            SqlDataAdapter da = new SqlDataAdapter(sorgu, connect);
            DataTable tableBook = new DataTable();
            da.Fill(tableBook);
            dataGridView1.DataSource = tableBook;
            connect.Close();
            
        }

        void listBorrowed()
        {
            lblIdentity.Text = LoginMember.SetValueForText1;

            SqlConnection connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
            connect.Open();
           
            String sorgu = "SELECT ISBN_NO, b.NAME, p.NAME as PUBLISHER_NAME, a.NAME as AUTHOR_NAME, bb.BORROW_DATE, bb.DEADLINE_DATE, bb.DELIVERY_DATE as PAY ,b.ID FROM ((BOOK b " +
                             "INNER JOIN PUBLISHER p " +
                             "ON b.PUBLISHER_ID = p.ID) " +
                             "INNER JOIN AUTHOR a " +
                             "ON b.AUTHOR_ID = a.ID)" +
                             "INNER JOIN BORROW bb " +
                             "ON bb.BOOK_ID = b.ID " +
					         " WHERE b.FLAG = 1 " +
					         " and bb.MEMBER_ID = 1" ;     

            SqlDataAdapter da = new SqlDataAdapter(sorgu, connect);
            DataTable tableBook = new DataTable();
            da.Fill(tableBook);
            dataGridView2.DataSource = tableBook;
            connect.Close();

        }


        private void MemberMenu_Load(object sender, EventArgs e)
        {
            ListBook();
            listBorrowed();
            lblIdentity.Text = LoginMember.SetValueForText1;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            lblIdentity.Text = LoginMember.SetValueForText1;
           
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            lblIdentity.Text = LoginMember.SetValueForText1;

            string insert = "INSERT INTO BORROW (BORROW_DATE, DEADLINE_DATE, BOOK_ID, MEMBER_ID ) " +
                                      "VALUES (GetDate(), DATEADD(DAY, 30, GetDate()), @bookID, ( SELECT ID FROM MEMBER" +
                                                                                                           " WHERE IDENTITY_NO = " + lblIdentity.Text + " )) ";

            SqlCommand command = new SqlCommand(insert, connect);

            command.Parameters.AddWithValue("@bookID", dataGridView1.CurrentRow.Cells[7].Value.ToString());

            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();

            MessageBox.Show("Borrow Successful", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListBook();
            listBorrowed();
        }


        private void btnReturn_Click(object sender, EventArgs e)
        {

            string insert = "UPDATE BORROW SET DELIVERY_DATE = GETDATE() " +
                             "WHERE @BOOK = BOOK_ID ";
                             
            SqlCommand command = new SqlCommand(insert, connect);

            command.Parameters.AddWithValue("@BOOK", dataGridView2.CurrentRow.Cells[7].Value.ToString());

            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();

            MessageBox.Show("Return Successful", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListBook();
            listBorrowed();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
         
                connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
                connect.Open();
                SqlDataAdapter search = new SqlDataAdapter("SELECT * from BOOK " +
                                                            "where NAME like '" + txtSearch.Text + "%'", connect);
                DataTable dt = new DataTable();
                search.Fill(dt);
                dataGridView1.DataSource = dt;
                connect.Close();
           
        }
    }
}
