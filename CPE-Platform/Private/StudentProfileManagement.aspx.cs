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
				if (Session["StudentID"] != null)
				{
					lblStudentID.Text = Session["StudentID"].ToString();
					PopulateGridView();
				}
					
			}
		}

		void PopulateGridView()
		{
			string studentID = Session["StudentID"].ToString();

			//con.Open();
			DataTable table = new DataTable();

			dataAdapter = new SqlDataAdapter("SELECT * FROM Rewards_Assign", con);


			dataAdapter.Fill(table);

			if (table.Rows.Count > 0)
			{

				//gvRewardsView.DataSource = table;

				string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
				SqlCommand cmd = new SqlCommand("SELECT * FROM Rewards_Assign StudentID =@StudentID", con);
				//string query = "SELECT R.CPECode, C.CPEDesc, R.RewardAwarded, R.StudentID FROM CPE_Course C, Rewards_Assign R WHERE R.StudentID =@StudentID AND R.CPECode =C.CPECode";
				//string query = "SELECT CPECode, RewardAwarded, StudentID FROM Rewards_Assign StudentID =@StudentID";
				
				con.Open();
						//cmd.Parameters.AddWithValue("@R." + Session["StudentID"] + "", Session["StudentID"].ToString());
				cmd.Parameters.AddWithValue("@StudentID", studentID);
						//cmd.Parameters.AddWithValue("@R." + Session["StudentID"] + "", Session["StudentID"].ToString());
				gvRewardsView.DataSource= table;
				gvRewardsView.DataBind();
				con.Close();



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