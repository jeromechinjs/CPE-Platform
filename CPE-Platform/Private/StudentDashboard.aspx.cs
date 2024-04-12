using PayPalCheckoutSdk.Orders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPE_Platform.Private
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlCommand cmd;
            SqlDataAdapter dataAdapter;
            DataSet ds = new DataSet();

            if (!Page.IsPostBack)
            {
                getDashboardFigures(); // get the points collected, discounts, and courses taking
                getCoursesCompleted();
            }
        }
        protected void getDashboardFigures()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                if (Session["StudentID"] != null)
                {
                    String studentID = Session["StudentID"].ToString();

                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Student WHERE StudentID = @StudentID", con);
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    SqlDataReader studentInfo = cmd.ExecuteReader();
                    if (studentInfo.Read()) // returns true if have more rows to read, else false
                    {
                        pts_collected.Text = studentInfo["RewardsAmount"].ToString();
                        //discounts_collected.Text = (studentInfo["RewardsAmount"].GetInt32(0) / 200).ToString();
                        num_active_courses.Text = studentInfo["RewardsAmount"].ToString();
                        // need get rows of active courses (COUNT inside CPE_Registration table)
                    }
                    studentInfo.Close();
                }
            }
        }

        protected void getCoursesCompleted()
        {
            if (Session["StudentID"] != null)
            {
                //lblStudentID.Text = Session["StudentID"].ToString();
                string studentID = Session["StudentID"].ToString();
                //PopulateGridView();


                // studentID = Session["StudentID"].ToString();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                string query = "SELECT R.CPECode, C.CPEName, R.Progress, R.RewardAwarded FROM CPE_Course C, Rewards_Assign R WHERE R.StudentID =@StudentID AND R.CPECode =C.CPECode";


                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    // bind data to grid view
                    DataTable table = new DataTable();
                    table.Load(reader);
                    gvRewardsView.DataSource = table;
                    if (table.Rows.Count > 0)
                    {
                        gvRewardsView.DataBind();
                        //close reader
                        reader.Close();
                        con.Close();
                    }
                    else
                    {
                        table.Rows.Add(table.NewRow());

                        gvRewardsView.DataSource = table;
                        gvRewardsView.DataBind();
                        gvRewardsView.Rows[0].Cells.Clear();
                        gvRewardsView.Rows[0].Cells.Add(new TableCell());
                        gvRewardsView.Rows[0].Cells[0].ColumnSpan = table.Columns.Count;
                        gvRewardsView.Rows[0].Cells[0].Text = "No Record is found!";
                        gvRewardsView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    }

                }

            }

        }

    }

}