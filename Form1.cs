using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace dvdcollection
{
    
    public partial class Form1 : Form
    {
        public static string sUniqMovieID = "1";
        public static string connectionString = "server=(local)\\SQLExpress;database=dvdlib;integrated Security=SSPI;";
        public static string sEditType = "E"; //A-Add, E-Edit

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbSearch.SelectedIndex = 0;
            DataTable dt = new DataTable();
            SqlConnection sql1 = new SqlConnection(connectionString);
            sql1.Open();
            SqlCommand sqlcmd = new SqlCommand("Select * from movies where UniqueMovie != -1 order by Title", sql1);
            //sqlcmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sql1.Close();
            da.Dispose();

            dgvList.DataSource = dt;
            dgvList.Columns[0].HeaderText = "ID";
            dgvList.Columns[0].Width = 50;
            dgvList.Columns[1].Width = 200;
            dgvList.Columns[3].HeaderText = "Studio";
            dgvList.Columns[3].Width = 200;
            dgvList.Columns[4].Width = 50;
            dgvList.Refresh();
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchResults();
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // sUniqMovieID = dgvList.SelectedRows[e.RowIndex].Cells[0].Value.ToString();
            //MessageBox.Show(e.RowIndex.ToString());
            sUniqMovieID = dgvList.Rows[e.RowIndex].Cells[0].Value.ToString();
            sEditType = "E";

            MovieInfo mi = new MovieInfo();
            mi.Show();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SearchResults();
            }
        }

        private void SearchResults()
        {
            if (txtSearch.Text != "")
            {
                string sQuery = "";

                if (cmbSearch.GetItemText(cmbSearch.SelectedItem) == "Title")
                {
                    sQuery = "select * from Movies where UniqueMovie != -1 AND title like '%" + txtSearch.Text.Trim() + "%'";
                }
                else if (cmbSearch.GetItemText(cmbSearch.SelectedItem) == "Genre")
                {
                    sQuery = "select * from Movies where UniqueMovie != -1 AND genre like '%" + txtSearch.Text.Trim() + "%'";
                }
                else if (cmbSearch.GetItemText(cmbSearch.SelectedItem) == "Studio")
                {
                    sQuery = "select * from Movies where UniqueMovie != -1 AND leadstudio like '%" + txtSearch.Text.Trim() + "%'";
                }
                else if (cmbSearch.GetItemText(cmbSearch.SelectedItem) == "Year")
                {
                    sQuery = "select * from Movies where UniqueMovie != -1 AND year like '%" + txtSearch.Text.Trim() + "%'";
                }

                DataTable dt = new DataTable();
                SqlConnection sql1 = new SqlConnection(connectionString);
                sql1.Open();
                SqlCommand sqlcmd = new SqlCommand(sQuery, sql1);
                //sqlcmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sql1.Close();
                da.Dispose();

                dgvList.DataSource = dt;
                dgvList.Columns[0].HeaderText = "ID";
                dgvList.Columns[0].Width = 50;
                dgvList.Columns[1].Width = 200;
                dgvList.Columns[3].HeaderText = "Studio";
                dgvList.Columns[3].Width = 200;
                dgvList.Columns[4].Width = 50;
                dgvList.Refresh();
            }
            else
            {
                MessageBox.Show("Please enter some search criteria.");
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            sUniqMovieID = "-1";
            sEditType = "A";

            MovieInfo mi = new MovieInfo();
            mi.Show();
        }
    }
}
