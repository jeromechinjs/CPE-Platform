using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPE_Platform
{
	public partial class StudentProfileManagement : System.Web.UI.Page
	{
		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
		SqlCommand cmd;
		SqlDataAdapter dataAdapter;
		DataSet ds = new DataSet();
		string query;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				PopulateGridView();
			}
		}

		void PopulateGridView()
        {
            con.Open();
            DataTable table = new DataTable();

            dataAdapter = new SqlDataAdapter("SELECT * FROM CPE_Course, Rewards_Assign", con);
          

            dataAdapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                if (Session["StudentID"] != null)
                { 
                    string studentID = Session["StudentID"].ToString().Trim();
                    lblStudentID.Text = studentID;
                    lblStudentID.Text = Session["staffID"].ToString().Trim();
             
					gvRewardsView.DataSource = table;

                    string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                    string query = "SELECT C.CPECode, C.CPEDesc, R.RewardAwarded, R.StudentID FROM C CPE_Course, S Student, R Rewards_Assign WHERE R.StudentID = @StudentID AND R.CPECode = C.CPECode";

                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            con.Open();
							//cmd.Parameters.AddWithValue("@R." + Session["StudentID"] + "", Session["StudentID"].ToString());
							cmd.Parameters.AddWithValue("@StudentID", studentID);

							gvRewardsView.DataBind();
                            con.Close();
                        }
                    }
                    
                }
                else
                {
                    Response.Redirect("~/StudentLogin.aspx");
                }
            }

            //cmd.Parameters.AddWithValue("@P." + Session["PatientEmailAddress"] + "", !string.IsNullOrEmpty(this.Page.User.Identity.Name) ? this.Page.User.Identity.Name : (object)DBNull.Value);
            //using (SqlConnection con = new SqlConnection(conString))
            //{
            //    using (SqlDataAdapter sda = new SqlDataAdapter())
            //    {
            //        cmd.Connection = con;
            //        sda.SelectCommand = cmd;
            //        using (DataTable dt = new DataTable())
            //        {
            //            sda.Fill(dt);

            //        }
            //    }
            //}



            else
            {
                table.Rows.Add(table.NewRow());

				gvRewardsView.DataSource = table;
				gvRewardsView.DataBind();
				gvRewardsView.Rows[0].Cells.Clear();
				gvRewardsView.Rows[0].Cells.Add(new TableCell());
				gvRewardsView.Rows[0].Cells[0].ColumnSpan = table.Columns.Count;
				gvRewardsView.Rows[0].Cells[0].Text = "No Record is found !!";
				gvRewardsView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            con.Close();


        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
			FormsAuthentication.SignOut();
			Response.Redirect("~/loginSelection.aspx");
		}
    }
}