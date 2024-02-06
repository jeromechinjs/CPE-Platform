using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPE_Platform.Private
{
	public partial class StaffRewardsAllocation : System.Web.UI.Page
	{
		SqlCommand cmd;
		SqlDataAdapter dataAdapter;
		DataSet ds = new DataSet();
		string query;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				GetCPEName();
				lstStudent.Items.Insert(0, " Select Students");
			}
		}
		private void GetCPEName()
		{
			SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
			query = "SELECT CONCAT(CPECode, ' ', CPEDesc) AS CPECourse, CPECode, Rewards FROM CPE_Course";
			dataAdapter = new SqlDataAdapter(query, con);

			dataAdapter.Fill(ds);

			if (ds.Tables[0].Rows.Count > 0)
			{
				CPECourse_DropDown.DataSource = ds;
				CPECourse_DropDown.DataTextField = "CPECourse";
				CPECourse_DropDown.DataValueField = "CPECode";
				CPECourse_DropDown.DataBind();
				CPECourse_DropDown.Items.Insert(0, new ListItem("Any Course", "0"));


			}

		}
		protected void AssignRewards(object sender, EventArgs e)  // function for btn assign rewards
		{
			string message = "";
			string errorMsg = "Please Select Valid Student Name";
			SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
			DateTime currentDateTime = DateTime.Now;
			
			// for loop to check whether the listed item is selected correctly
			foreach (ListItem item in lstStudent.Items)
			{
				if (item.Text == " Select Students") // selected value 0 (by default)
				{
					ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + errorMsg + "');", true);
				}
				else
				{
					if (item.Selected)  // will change once bootstrap design template is found
					{
						SqlCommand cmd = new SqlCommand("INSERT INTO Rewards_Assign (RewardAwarded, rewardsDate, Progress, StudentID, CPECode) VALUES (@RewardAwarded, @rewardsDate, @Progress, @StudentID, @CPECode)", con);
						cmd.Parameters.AddWithValue("@RewardAwarded", txtRewards.Text);
						cmd.Parameters.AddWithValue("@rewardsDate", currentDateTime);
						cmd.Parameters.AddWithValue("@Progress", "Completed");

						// separate student id and name 
						string[] separatedStudent = item.Value.Split(',');
						string studentID = separatedStudent[0].Trim();
						//txtStudentID.Text = studentID;
						cmd.Parameters.AddWithValue("@StudentID", studentID);

						// separate CPE code and description
						string[] separatedCPECourse = CPECourse_DropDown.SelectedValue.ToString().Split(',');
						string CPECode = separatedCPECourse[0].Trim();
						//txtCPECode.Text = CPECode; 
						cmd.Parameters.AddWithValue("@CPECode", CPECode.ToString());
						con.Open();
						cmd.ExecuteNonQuery();
						con.Close();

						// to be continued to delete data from CPE_Registration
						


					}
				}

			}
			
			message = "Successfully Assigned Rewards to the Students";
			ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
		}



		protected void CPECourse_DropDown_SelectedIndexChanged(object sender, EventArgs e)
		{
			string get_CPECode, get_CPEName, getRewards;

			get_CPECode = CPECourse_DropDown.SelectedValue.ToString();
			get_CPEName = CPECourse_DropDown.SelectedItem.Text;

			SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
			// to fill up the drop down based on the db data
			if (get_CPECode != "0")
			{
				query = "SELECT DISTINCT CONCAT(S.StudentID, ' ', S.StudentName) AS Student, S.StudentID, Rewards FROM CPE_Registration R, Student S, CPE_Course C WHERE C.CPECode ='" + get_CPECode.ToString() + "' AND S.StudentID = R.StudentID AND C.CPECode =R.CPECode";

				// dataAdapter
				dataAdapter = new SqlDataAdapter(query, con);
				dataAdapter.Fill(ds);
				// check if there is a row in the dataset (ds)
				if (ds.Tables[0].Rows.Count > 0)
				{
					lstStudent.DataSource = ds;
					lstStudent.DataTextField = "Student";
					lstStudent.DataValueField = "StudentID";
					lstStudent.DataBind();
					lstStudent.SelectedIndex = 0;

					getRewards = ds.Tables[0].Rows[0]["Rewards"].ToString();
					txtRewards.Text = getRewards;
				}
				else  // execute only when the course selected doesn't have student registered
				{
					lstStudent.Items.Insert(0, " There are no student completed " + get_CPEName.ToString());
				}

			}
			else  // execute only when selected any course option in dropdown
			{
				txtRewards.Text = string.Empty;
				lstStudent.Items.Clear();
				lstStudent.Items.Insert(0, " Select Students");

			}
		}
	}
}