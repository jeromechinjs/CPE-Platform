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
		protected void AssignRewards(object sender, EventArgs e)
		{
			string message = "";
			string errorMsg = "Please Select Valid Student Name";
			foreach (ListItem item in lstStudent.Items)
			{
				if (item.Text == " Select Students")
				{
					ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + errorMsg + "');", true);
				}
				else
				{
					if (item.Selected)  // will change once bootstrap design template is found
					{
						message += item.Text + " " + "\\n";
					}
				}

			}
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
				query = "SELECT DISTINCT CONCAT(S.StudentID, ' ', S.StudentName) AS Student, StudentName, Rewards FROM CPE_Registration R, Student S, CPE_Course C WHERE C.CPECode ='" + get_CPECode.ToString() + "' AND S.StudentID = R.StudentID AND C.CPECode =R.CPECode";

				// dataAdapter
				dataAdapter = new SqlDataAdapter(query, con);
				dataAdapter.Fill(ds);
				// check if there is a row in the dataset (ds)
				if (ds.Tables[0].Rows.Count > 0)
				{
					lstStudent.DataSource = ds;
					lstStudent.DataTextField = "Student";
					lstStudent.DataValueField = "StudentName";
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