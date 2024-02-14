using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace CPE_Platform
{
    public partial class StudentLogin : System.Web.UI.Page
    {

		public static string EncryptData(string SimpleString)
		{
			MD5 md5 = new MD5CryptoServiceProvider();

			byte[] passwordHash = Encoding.UTF8.GetBytes(SimpleString);
			return Encoding.UTF8.GetString(md5.ComputeHash(passwordHash));
		}

		protected void Page_Load(object sender, EventArgs e)
        {
			//FormsAuthentication.SignOut();
        }

		protected void btnStudentLogin_Click(object sender, EventArgs e)
		{
			if (Page.IsValid)
			{
				SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\DatabaseCPE.mdf\";Integrated Security=True");
				SqlCommand cmd = new SqlCommand("Select * from Student where StudentID = @StudentID and StudentPassword = @StudentPassword", con);
				cmd.Parameters.AddWithValue("@StudentID", txtStudentID.Text);
				cmd.Parameters.AddWithValue("@StudentPassword", EncryptData(txtStudentPassword.Text.Trim()));
				SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				dataAdapter.Fill(ds, "Student");
				if (ds.Tables["Student"].Rows.Count == 0)
				{
					lblErrorMsg.ForeColor = System.Drawing.Color.Red;
					String errorMsg = "Invalid Student ID or Password. Please Try again";
					lblErrorMsg.Text = errorMsg;
					//Response.Write("<script>alert('Invalid Student ID or Password. Please Try again')</script>");
				}
				else
				{
					bool isValidUser = true;

					if(isValidUser)
					{
						Session["StudentID"] = txtStudentID.Text;
                        FormsAuthentication.RedirectFromLoginPage(txtStudentID.Text, false);
						Response.Redirect("~/Private/StudentDashboard.aspx");  // will redirect to home page once home page is created
					}
					else
					{
						lblErrorMsg.Text = "Invalid Student ID or Password.";
					}

					

					 //paste this into homepage.aspx.cs pageload function once created

					//if (Session["StudentID"] != null)
					//{
					//	lblStudentID.Text = Session["StudentID"].ToString();
					//}

				}
			}
			else
			{
				lblErrorMsg.Text = "Invalid Student ID or Password.";
			}
            
        }

	}
}