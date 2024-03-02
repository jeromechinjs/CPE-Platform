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
using System.Web.UI.HtmlControls;
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
				((StaffMaster)this.Master).ToggleCPECardVisibility(true);
				((StaffMaster)this.Master).ToggleCPEDetailsVisibility(false);
				//GetCPEName();
				//lstStudent.Items.Insert(0, " Select Students");
				BindCPEData();
			}
		}
		private void BindCPEData()
		{
			// Retrieve data from the database
			DataTable CPEData = GetDataFromDatabase();

			// Bind the data to the Repeater control
			rptrCPE.DataSource = CPEData;
			rptrCPE.DataBind();
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			// Move selected items from AvailableStudents to SelectedStudents
			MoveItems(lstStudent, lstSelectedStudents);
		}
		protected void btnRemove_Click(object sender, EventArgs e)
		{
			// Move selected items from SelectedStudents to AvailableStudents
			MoveItems(lstSelectedStudents, lstStudent);
		}
		private void MoveItems(ListBox source, ListBox destination)
		{
			// Move selected items from source to destination
			for (int i = source.Items.Count - 1; i >= 0; i--)
			{
				if (source.Items[i].Selected)
				{
					ListItem item = source.Items[i];
					destination.Items.Add(item);
					source.Items.Remove(item);
				}
			}

		}
		protected void CPESelected(object sender, RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "Select")
			{
				// Retrieve the ID of the selected card
				string selectedCPECourse = e.CommandArgument.ToString();
				((StaffMaster)this.Master).ToggleCPECardVisibility(false);
				((StaffMaster)this.Master).ToggleCPEDetailsVisibility(true);
				string getRewards;
				lblCPECourse.Text = selectedCPECourse;
				string[] separatedCPE = selectedCPECourse.Trim().Split(new char[] { ' ' }, 2);
				if (separatedCPE.Length == 2)
				{
					string CPECode = separatedCPE[0]; // First part is CPECode
					string CPEDesc = separatedCPE[1]; // Second part is CPEDesc

					// Implement your logic to retrieve details of the selected card from the database
					SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
					query = "SELECT DISTINCT CONCAT(S.StudentID, ' ', S.StudentName) AS Student, S.StudentID, Rewards FROM CPE_Registration R, Student S, CPE_Course C WHERE C.CPECode ='" + CPECode.Trim().ToString() + "' AND S.StudentID = R.StudentID AND C.CPECode =R.CPECode";
					using (SqlCommand cmd = new SqlCommand(query, con))
					{
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
							txtRewards.Text = getRewards + " Points";
						}
						else  // execute only when the course selected doesn't have student registered
						{
							txtRewards.Text = string.Empty;
							lstStudent.Items.Clear();
							lstStudent.Items.Insert(0, " There are no student completed " + selectedCPECourse.ToString());
						}
					}
				}
			}
		}
		//private DataTable GetCPEDetailsFromDatabase(string CPECourse)
		//{
		//	string getRewards;

		//	string[] separatedCPE = CPECourse.Trim().Split(new char[] { ' ' }, 2);
		//	if (separatedCPE.Length == 2)
		//	{
		//		string CPECode = separatedCPE[0]; // First part is CPECode
		//		string CPEDesc = separatedCPE[1]; // Second part is CPEDesc

		//		// Implement your logic to retrieve details of the selected card from the database
		//		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
		//		query = "SELECT DISTINCT CONCAT(S.StudentID, ' ', S.StudentName) AS Student, S.StudentID, Rewards FROM CPE_Registration R, Student S, CPE_Course C WHERE C.CPECode ='" + CPECode.ToString() + "' AND S.StudentID = R.StudentID AND C.CPECode =R.CPECode";
		//		using (SqlCommand cmd = new SqlCommand(query, con))
		//		{
		//			con.Open();
		//			SqlDataReader reader = cmd.ExecuteReader();
		//			DataTable CPEDetails = new DataTable();
		//			CPEDetails.Load(reader);
		//			if (CPEDetails.Rows.Count > 0)
		//			{
		//				lstStudent.DataSource = ds;
		//				lstStudent.DataTextField = "Student";
		//				lstStudent.DataValueField = "StudentID";
		//				lstStudent.DataBind();
		//				lstStudent.SelectedIndex = 0;

		//				getRewards = ds.Tables[0].Rows[0]["Rewards"].ToString();
		//				txtRewards.Text = getRewards;
		//			}
		//			else  // execute only when the course selected doesn't have student registered
		//			{
		//				txtRewards.Text = string.Empty;
		//				lstStudent.Items.Clear();
		//				lstStudent.Items.Insert(0, " There are no student completed " + txtCPECode.ToString());
		//			}
		//			// Populate CPEDetails with data from the database based on the provided cardID
		//			return CPEDetails;
		//		}
		//	}
		//	//	// dataAdapter
		//	//	dataAdapter = new SqlDataAdapter(query, con);
		//	//dataAdapter.Fill(ds);
		//	//// check if there is a row in the dataset (ds)
		//	//if (ds.Tables[0].Rows.Count > 0)
		//	//{
		//	//	lstStudent.DataSource = ds;
		//	//	lstStudent.DataTextField = "Student";
		//	//	lstStudent.DataValueField = "StudentID";
		//	//	lstStudent.DataBind();
		//	//	lstStudent.SelectedIndex = 0;

		//	//	getRewards = ds.Tables[0].Rows[0]["Rewards"].ToString();
		//	//	txtRewards.Text = getRewards;
		//	//}
		//	//else  // execute only when the course selected doesn't have student registered
		//	//{
		//	//	txtRewards.Text = string.Empty;
		//	//	lstStudent.Items.Clear();
		//	//	lstStudent.Items.Insert(0, " There are no student completed " + txtCPECode.ToString());
		//	//}
		//	//// Populate CPEDetails with data from the database based on the provided cardID
		//	//return CPEDetails;
		//}
		private DataTable GetDataFromDatabase()
		{
			// Implement your logic to fetch card data from the database
			// Example:
			SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
			query = "SELECT CONCAT(CPECode, ' ', CPEDesc) AS CPECourse, CPEStartDate, CPEEndDate FROM CPE_Course";
			using (SqlCommand cmd = new SqlCommand(query, con))
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				DataTable CPEData = new DataTable();
				CPEData.Load(reader);
				if (CPEData.Rows.Count > 0)
				{
					rptrCPE.DataBind();
					//close reader
					reader.Close();
					con.Close();
				}
				// Populate cardData with data from the database
				return CPEData;
			}
		}
		//private void GetCPEName()
		//{
		//	SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
		//	query = "SELECT CONCAT(CPECode, ' ', CPEDesc) AS CPECourse, CPECode, Rewards FROM CPE_Course";
		//	dataAdapter = new SqlDataAdapter(query, con);

		//	dataAdapter.Fill(ds);

		//	if (ds.Tables[0].Rows.Count > 0)
		//	{
		//		CPECourse_DropDown.DataSource = ds;
		//		CPECourse_DropDown.DataTextField = "CPECourse";
		//		CPECourse_DropDown.DataValueField = "CPECode";
		//		CPECourse_DropDown.DataBind();
		//		CPECourse_DropDown.Items.Insert(0, new ListItem("Any Course", "0"));
		//	}

		//}
		protected void AssignRewards(object sender, EventArgs e)  // function for btn assign rewards
		{
			string selectedWrongMsg = "Please select at least one student before assigning rewards.";
			string errorMsg = "Please Select Valid Student Name";
			SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
			DateTime currentDateTime = DateTime.Now;
			
			//to check if the user click the students in available student listbox
			foreach (ListItem item in lstStudent.Items)
			{
				if (item.Selected)
				{
					string script = "alert('Please select the student in the selected list');";
					ScriptManager.RegisterStartupScript(this, GetType(), "NoStudentSelected", script, true);
				}
			}

			// check whether the listed item is selected correctly
			foreach (ListItem item in lstSelectedStudents.Items)
			{
				//if (item.Text == " Select Students" || item.Text == " There are no student completed " + CPECourse_DropDown.SelectedItem.Text) // selected value 0 (by default)
				if (item.Text == " Select Students" || item.Text == " There are no student completed " + lblCPECourse.Text) // selected value 0 (by default)
				{
					ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + errorMsg + "');", true);
				}
				else
				{
					if (item.Selected) // the student selected will execute this condition
					{
						SqlCommand cmd = new SqlCommand("INSERT INTO Rewards_Assign (RewardAwarded, rewardsDate, Progress, StudentID, CPECode) VALUES (@RewardAwarded, @rewardsDate, @Progress, @StudentID, @CPECode)", con);
						//cmd.Parameters.AddWithValue("@RewardAwarded", txtRewards.Text);
						string rewardsText = txtRewards.Text;
						int pointsIndex = rewardsText.IndexOf("Points");

						if (pointsIndex != -1)
						{
							string rewardsValue = rewardsText.Substring(0, pointsIndex).Trim();

							// Add the extracted value to the SQL parameter
							cmd.Parameters.AddWithValue("@RewardAwarded", rewardsValue);
						}
						cmd.Parameters.AddWithValue("@rewardsDate", currentDateTime);
						cmd.Parameters.AddWithValue("@Progress", "Completed");

						// separate student id and name 
						string[] separatedStudent = item.Value.Split(',');
						string studentID = separatedStudent[0].Trim();
						cmd.Parameters.AddWithValue("@StudentID", studentID);

						// separate CPE code and description
						// string[] separatedCPECourse = CPECourse_DropDown.SelectedValue.ToString().Split(',');
						//string[] separatedCPECourse = lblCPECourse.Text.Trim().Split(',');
						string selectedCPECourse = lblCPECourse.Text;
						string[] separatedCPE = selectedCPECourse.Trim().Split(new char[] { ' ' }, 2);
						if (separatedCPE.Length == 2)
						{
							string CPECode = separatedCPE[0]; // First part is CPECode
															  //string CPECode = separatedCPECourse[0].Trim();
							cmd.Parameters.AddWithValue("@CPECode", CPECode.ToString());

							con.Open();
							cmd.ExecuteNonQuery();
							con.Close();

							// Delete data from CPE_Registration
							SqlCommand cmd2 = new SqlCommand("DELETE FROM CPE_Registration WHERE StudentID =@StudentID AND CPECode =@CPECode", con);
							cmd2.Parameters.AddWithValue("@StudentID", studentID);
							cmd2.Parameters.AddWithValue("@CPECode", CPECode);

							con.Open();
							cmd2.ExecuteNonQuery();
							con.Close();
						}
						// execute this message when click the assign button when fulfill the condition
						string script = "alert('Successfully Assigned Rewards to the Students');";
						ScriptManager.RegisterStartupScript(this, GetType(), "Alert", script, true);

						string redirectScript = "setTimeout(function() { window.location.href = '../Private/StaffRewardsAllocation.aspx'; }, 100);"; // Redirect after 0.01 seconds (10 milliseconds)
						ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", redirectScript, true);

					}
				}

			}
			// reset the value after the button is clicked
			//CPECourse_DropDown.SelectedValue = "0";
			//txtRewards.Text = string.Empty;
			//lstStudent.Items.Clear();
			//lstStudent.Items.Insert(0, " Select Students");
		}

		protected void btnBack_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Private/StaffRewardsAllocation.aspx");
		}
		//protected void CPECourse_DropDown_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	string get_CPECode, get_CPEName, getRewards;

		//	get_CPECode = CPECourse_DropDown.SelectedValue.ToString();
		//	get_CPEName = CPECourse_DropDown.SelectedItem.Text;

		//	SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
		//	// to fill up the drop down based on the db data
		//	if (get_CPECode != "0")
		//	{
		//		query = "SELECT DISTINCT CONCAT(S.StudentID, ' ', S.StudentName) AS Student, S.StudentID, Rewards FROM CPE_Registration R, Student S, CPE_Course C WHERE C.CPECode ='" + get_CPECode.ToString() + "' AND S.StudentID = R.StudentID AND C.CPECode =R.CPECode";

		//		// dataAdapter
		//		dataAdapter = new SqlDataAdapter(query, con);
		//		dataAdapter.Fill(ds);
		//		// check if there is a row in the dataset (ds)
		//		if (ds.Tables[0].Rows.Count > 0)
		//		{
		//			lstStudent.DataSource = ds;
		//			lstStudent.DataTextField = "Student";
		//			lstStudent.DataValueField = "StudentID";
		//			lstStudent.DataBind();
		//			lstStudent.SelectedIndex = 0;

		//			getRewards = ds.Tables[0].Rows[0]["Rewards"].ToString();
		//			txtRewards.Text = getRewards;
		//		}
		//		else  // execute only when the course selected doesn't have student registered
		//		{
		//			txtRewards.Text = string.Empty;
		//			lstStudent.Items.Clear();
		//			lstStudent.Items.Insert(0, " There are no student completed " + get_CPEName.ToString());
		//		}

		//	}
		//	else  // execute only when selected any course option in dropdown
		//	{
		//		txtRewards.Text = string.Empty;
		//		lstStudent.Items.Clear();
		//		lstStudent.Items.Insert(0, " Select Students");

		//	}
		//}
	}
}