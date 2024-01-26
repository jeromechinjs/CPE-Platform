using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.Web.Security;

namespace CPE_Platform
{
	public partial class StaffLogin : System.Web.UI.Page
	{
		public static string EncryptData(string SimpleString)
		{
			MD5 md5 = new MD5CryptoServiceProvider();

			byte[] passwordHash = Encoding.UTF8.GetBytes(SimpleString);
			return Encoding.UTF8.GetString(md5.ComputeHash(passwordHash));
		}
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnStaffLogin_Click(object sender, EventArgs e)
		{
			if (Page.IsValid)
			{
				string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
				SqlConnection con = new SqlConnection(strCon);
				SqlCommand cmd = new SqlCommand("Select * from Staff where StaffID = @StaffID and StaffPassword = @StaffPassword", con);
				cmd.Parameters.AddWithValue("@StaffID", txtStaffID.Text);
				cmd.Parameters.AddWithValue("@StaffPassword", EncryptData(txtStaffPassword.Text.Trim()));
				SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				dataAdapter.Fill(ds, "Staff");
				if (ds.Tables["Staff"].Rows.Count == 0)
				{
					lblErrorMsg.ForeColor = System.Drawing.Color.Red;
					String errorMsg = "Invalid Staff ID or Password. Please Try again";
					lblErrorMsg.Text = errorMsg;
					//Response.Write("<script>alert('Invalid Staff ID or Password. Please Try again')</script>");
				}
				else
				{
					bool isValidUser = true;

					if (isValidUser)
					{
						Session["StaffID"] = txtStaffID.Text;
						FormsAuthentication.RedirectFromLoginPage(txtStaffID.Text, false);
						Response.Redirect("~/Private/StudentProfileManagement.aspx");  // will redirect to home page once home page is created
					}
					else
					{
						lblErrorMsg.Text = "Invalid Staff ID or Password.";
					}
				}
			}
		}
	}
}