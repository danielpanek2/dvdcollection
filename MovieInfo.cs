using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
//using System.Drawing;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace dvdcollection
{
    public partial class MovieInfo : Form
    {
        public MovieInfo()
        {
            InitializeComponent();
        }

        private void MovieInfo_Load(object sender, EventArgs e)
        {
            txtUniqueMovie.Text = Form1.sUniqMovieID;
            if (Form1.sEditType == "E")
            {
                btnAdd.Visible = false;
                btnDelete.Visible = true;
                txtUniqueMovie.Show();
            }
            else
            {
                btnAdd.Visible = true;
                btnDelete.Visible = false;
                txtUniqueMovie.Hide();
            }

            string sQuery = "select * from Movies where UniqueMovie = " + Form1.sUniqMovieID;

            DataTable dt = new DataTable();
            SqlConnection sql1 = new SqlConnection(Form1.connectionString);
            sql1.Open();
            SqlCommand sqlcmd = new SqlCommand(sQuery, sql1);
            //sqlcmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sql1.Close();
            da.Dispose();

            txtTitle.Text = dt.Rows[0].ItemArray[1].ToString();
            txtGenre.Text = dt.Rows[0].ItemArray[2].ToString();
            txtStudio.Text = dt.Rows[0].ItemArray[3].ToString();
            txtYear.Text = dt.Rows[0].ItemArray[4].ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string sQuery = "UPDATE movies " +
                            "SET Title = '" + txtTitle.Text.Trim().Replace("'", "''") + "', " +
                            "Genre = '" + txtGenre.Text.Trim().Replace("'", "''") + "', " +
                            "LeadStudio = '" + txtStudio.Text.Trim().Replace("'", "''") + "', " +
                            "[Year] = '" + txtYear.Text.Trim().Replace("'", "''") + "' " +
                            "WHERE UniqueMovie = " + Form1.sUniqMovieID;

            SqlConnection sql1 = new SqlConnection(Form1.connectionString);
            sql1.Open();
            SqlCommand sqlcmd = new SqlCommand(sQuery, sql1);
            //MessageBox.Show(sQuery);
            sqlcmd.ExecuteNonQuery();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sQuery = "DELETE FROM movies where UniqueMovie = " + Form1.sUniqMovieID;

            SqlConnection sql1 = new SqlConnection(Form1.connectionString);
            sql1.Open();
            SqlCommand sqlcmd = new SqlCommand(sQuery, sql1);
            //MessageBox.Show(sQuery);
            sqlcmd.ExecuteNonQuery();
            MessageBox.Show("Movie #" + Form1.sUniqMovieID + " deleted.");
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string sQuery = "insert into movies (Title, Genre, LeadStudio, [Year]) " +
                            "values('" + txtTitle.Text.Trim().Replace("'", "''") + "', " +
                            "'" + txtGenre.Text.Trim().Replace("'", "''") + "', " +
                            "'" + txtStudio.Text.Trim().Replace("'", "''") + "', " +
                            "'" + ValidatedYear(txtYear.Text) + "')";

            SqlConnection sql1 = new SqlConnection(Form1.connectionString);
            sql1.Open();
            SqlCommand sqlcmd = new SqlCommand(sQuery, sql1);
            //MessageBox.Show(sQuery);
            sqlcmd.ExecuteNonQuery();
            MessageBox.Show("Movie " + txtTitle.Text.Trim() + " added. Closing now.");
            this.Close();
        }

        private string ValidatedYear(string inputyr)
        {
            if (string.IsNullOrWhiteSpace(inputyr))
            {
                return "0";
            }
            else if (inputyr.Trim().Length != 2 && inputyr.Trim().Length != 4)
            {
                return "0";
            }
            else if (!inputyr.Trim().All(char.IsDigit))
            {
                return "0";
            }
            else
            {
                return inputyr.Trim();
            }

        }

    }
}
