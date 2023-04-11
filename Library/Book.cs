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
    public partial class Book : Form
    {
        
        public Book()
        {
            InitializeComponent();
        }

        void ListBook()
        {
            connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
            connect.Open();
            String sorgu = "SELECT ISBN_NO, b.NAME, p.NAME as PublisherName, a.NAME as AuthorNAME, LANGUAGE, CATEGORY, PAGE_NUM  FROM ((BOOK b " +
                             "INNER JOIN PUBLISHER p " +
                             "ON b.PUBLISHER_ID = p.ID) " +
                             "INNER JOIN AUTHOR a " +
                             "ON b.AUTHOR_ID = a.ID)";

            SqlDataAdapter da = new SqlDataAdapter(sorgu, connect);
            DataTable tableBook = new DataTable();
            da.Fill(tableBook);
            dataGridView1.DataSource = tableBook;
            connect.Close();
        }

        void SearchBook()
        {
            connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
            connect.Open();
            SqlDataAdapter search = new SqlDataAdapter("SELECT * from BOOK " +
                                                        "where NAME like '" + txtSearch.Text + "%'",connect);
            DataTable dt = new DataTable();
            search.Fill(dt);
            dataGridView1.DataSource = dt;
            connect.Close();
        }

#pragma warning disable IDE1006 // Adlandırma Stilleri
        private void lblName_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Adlandırma Stilleri
        {

        }


        SqlConnection connect = new SqlConnection();
        SqlConnection connect2 = new SqlConnection();
        SqlConnection connect3 = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
        SqlDataAdapter db;
        SqlDataAdapter db2;


        void LoadAuthor()
        {
            //  BOOK TABLOSUNDA AUTHOR COMBOBOX'A  LOAD EDİLEN KISIM
            connect = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
            connect.Open();

            db = new SqlDataAdapter("SELECT NAME, ID FROM AUTHOR ORDER BY NAME ASC", connect);

            DataTable table = new DataTable();
            DataTable table3 = new DataTable();
            db.Fill(table);
            comboBoxAuthor.DataSource = table;

            comboBoxAuthor.DisplayMember = "NAME";
            comboBoxAuthor.ValueMember = "ID";
            connect.Close();
        }
        
        void LoadPublisher()
        {
            connect2 = new SqlConnection("server=.;Initial Catalog=LibraryDB; integrated Security=SSPI");
            connect2.Open();
            db2 = new SqlDataAdapter("SELECT ID, NAME FROM PUBLISHER ORDER BY NAME ASC", connect2);
            DataTable table2 = new DataTable();
            db2.Fill(table2);
            comboBoxPublisher.DataSource = table2;
            
           
            comboBoxPublisher.DisplayMember = "NAME";
            comboBoxPublisher.ValueMember = "ID";
            connect2.Close();

        }

        private void Book_Load(object sender, EventArgs e)
        {
            LoadAuthor();
            LoadPublisher();
            ListBook();
        }

        private void comboBoxAuthor_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void lblAuthorAdd_Click(object sender, EventArgs e)
        {
            Form1 author = new Form1();
            author.Show();
       
        }
       
        private void btnSave_Click(object sender, EventArgs e)


        {
            if (txtISBN.Text !="" && txtPageNum.Text != "" && txtName.Text != "")
            {                
                string query = "INSERT INTO BOOK (ISBN_NO, NAME, PAGE_NUM, LANGUAGE, AUTHOR_ID, PUBLISHER_ID, CATEGORY ) " +
                                          "VALUES (@isbn, @name, @pageNum, @language, @author, @publisher, @category) ";
                                         
                
                SqlCommand command2 = new SqlCommand(query, connect3);

                int AuthorID = int.Parse(comboBoxAuthor.SelectedValue.ToString());
                int PublisherID = int.Parse(comboBoxPublisher.SelectedValue.ToString());

                command2.Parameters.AddWithValue("@isbn", txtISBN.Text);
                command2.Parameters.AddWithValue("@name", txtName.Text);
                command2.Parameters.AddWithValue("@pageNum", txtPageNum.Text);
                command2.Parameters.AddWithValue("@category", comboBoxCategory.GetItemText(comboBoxCategory.SelectedItem));
                command2.Parameters.AddWithValue("@language", comboBoxLanguage.GetItemText(comboBoxLanguage.SelectedItem));
                command2.Parameters.AddWithValue("@author", AuthorID);
                command2.Parameters.AddWithValue("@publisher", PublisherID);

                connect3.Open();
                command2.ExecuteNonQuery();
                connect3.Close();

                MessageBox.Show("Registration Successful", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblPublisherAdd_Click(object sender, EventArgs e)
        {
            Publisher publisher = new Publisher();
            publisher.Show();
        }

        private void lblReflesh_Click(object sender, EventArgs e)
        {
            LoadAuthor();
            LoadPublisher();
            ListBook();

        }


        //INSERT DATA FROM DATAGRIDVIEW
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            txtISBN.Text = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            comboBoxPublisher.Text = row.Cells[2].Value.ToString();
            comboBoxAuthor.Text = row.Cells[3].Value.ToString();
            comboBoxLanguage.Text = row.Cells[4].Value.ToString();
            comboBoxCategory.Text = row.Cells[5].Value.ToString();
            txtPageNum.Text = row.Cells[6].Value.ToString();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchBook();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be Update?", "INFO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string name = txtName.Text;
                string isbn = txtISBN.Text;
                string language = comboBoxLanguage.Text;
                string category = comboBoxCategory.Text;
                string author = comboBoxAuthor.Text;
                string publisher = comboBoxPublisher.Text;
                Int64 pageNum = Int64.Parse(txtPageNum.Text);

                SqlCommand command = new SqlCommand();
                command.Connection = connect;

                string sorgu = "update book set ISBN_NO ='" + isbn + "', NAME = '" + name + "' , " +
                    "PAGE_NUM ='" + pageNum + "', CATEGORY ='" + category + "', LANGUAGE = '" + language + "' ," +
                    " AUTHOR_ID = ( select ID from AUTHOR where NAME ='" + author + "'), " +
                    "PUBLISHER_ID = ( select ID from PUBLISHER where NAME ='" + publisher + "')   from BOOK b " +
                    "Inner join AUTHOR a on b.AUTHOR_ID = a.ID " +
                    "Inner Join PUBLISHER p on b.PUBLISHER_ID =p.ID where b.NAME = '" + name + "'or b.ISBN_NO ='" + isbn + "'";

                command.CommandText = sorgu;

                SqlDataAdapter dA = new SqlDataAdapter(command);
                DataSet DS = new DataSet();
                dA.Fill(DS);
            }
            MessageBox.Show("Update Successful", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListBook();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if(MessageBox.Show("Data will be Deleted.", "INFO", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
               
                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                string isbn = txtISBN.Text;
                string Delete = "DELETE FROM BOOK WHERE ISBN_NO = '"+isbn+"'";
                
                command.CommandText = Delete;

                SqlDataAdapter dA = new SqlDataAdapter(command);
                DataSet DS = new DataSet();
                dA.Fill(DS);


            }

            MessageBox.Show("Delete Successful", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListBook();
        }

    
    }
}
