using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
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
			if(!IsPostBack)
			{
				GetCPEName();
				lstStudent.Items.Insert(0, " Select Students");
			}
		}
		private void GetCPEName()
		{
			SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
			query = "Select * from CPE_Course";
			dataAdapter = new SqlDataAdapter(query, con);

			dataAdapter.Fill(ds);

			if (ds.Tables[0].Rows.Count > 0)
			{
				CPECourse_DropDown.DataSource = ds;
				CPECourse_DropDown.DataTextField = "CPEDesc";
				CPECourse_DropDown.DataValueField = "CPECode";
				CPECourse_DropDown.DataBind();
				CPECourse_DropDown.Items.Insert(0, new ListItem("Any Course", "0"));
			}
		}
		protected void Submit(object sender, EventArgs e)
		{
			string message = "";
			foreach (ListItem item in lstStudent.Items)
			{
				if (item.Selected)
				{
					message += item.Text + " " + item.Value + "\\n";
				}
			}
			ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
		}

		

		protected void CPECourse_DropDown_SelectedIndexChanged(object sender, EventArgs e)
		{
			string get_CPECode, get_CPEName;

			get_CPECode = CPECourse_DropDown.SelectedValue.ToString();
			get_CPEName = CPECourse_DropDown.SelectedItem.Text;

			SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
			// to fill up the drop down based on the db data
			if (get_CPECode != "0")
			{
				query = "SELECT R.StudentID, S.StudentName from CPE_Registration R, Student S WHERE CPECode ='" + get_CPECode.ToString() + "' AND R.StudentID = S.StudentID";
				dataAdapter = new SqlDataAdapter(query, con);
				dataAdapter.Fill(ds);
				if (ds.Tables[0].Rows.Count > 0)
				{
					lstStudent.DataSource = ds;
					lstStudent.DataTextField = "StudentName";
					lstStudent.DataValueField = "StudentID";
					lstStudent.DataBind();
					//lstStudent.Items.Insert(0, new ListItem("Choose Students from" + get_CPEName.ToString()));
					lstStudent.SelectedIndex = 0;
				}
				else
				{
					lstStudent.Items.Insert(0, " There are no student completed " + get_CPEName.ToString());
				}

			}
			else
			{
				lstStudent.Items.Clear();
				lstStudent.Items.Insert(0, " Select Students");

			}
		}
	}
}