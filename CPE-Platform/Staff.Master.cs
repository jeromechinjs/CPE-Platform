using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPE_Platform
{
    public partial class StaffMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			string staffID;
			if (Session["StaffID"] == null)
			{
			// Redirect the user to the login page
			Response.Redirect("~/LoginSelection.aspx", true);
			}
			else
			{
				//staffID = Session["StaffID"].ToString();
				if (!IsPostBack)
				{
					staffID = Session["StaffID"].ToString();

					SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\DatabaseCPE.mdf\";Integrated Security=True");
					SqlCommand cmd = new SqlCommand("Select StaffName from Staff where StaffID = @StaffID", con);

					cmd.Parameters.AddWithValue("@StaffID", staffID);

					con.Open();
					cmd.ExecuteNonQuery();
					con.Close();

					SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
					DataSet ds = new DataSet();
					dataAdapter.Fill(ds, "Staff");


					name.Text = "" + ds.Tables["Staff"].Rows[0][0].ToString().ToUpper(); // retrieve logged in user's name (then UPPERCASE)

				}
			}
			


		}
		public void ToggleCPECardVisibility(bool isVisible)
		{
			CPECard.Visible = isVisible;
		}
		public void ToggleCPEDetailsVisibility(bool isVisible)
		{
			DetailsContent.Visible = isVisible;
		}

		protected void Logout(object sender, EventArgs e)
		{
			FormsAuthentication.SignOut();

			// Drop all the information held in the session
			Session.Clear();
			Session.Abandon();

			// clear authentication cookie
			HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
			cookie1.Expires = DateTime.Now.AddYears(-1);
			Response.Cookies.Add(cookie1);

			// clear session cookie
			HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
			cookie2.Expires = DateTime.Now.AddYears(-1);
			Response.Cookies.Add(cookie2);

			// Redirect the user to the login page
			Response.Redirect("~/LoginSelection.aspx", true);
		}

        protected void goDashboard(object sender, EventArgs e)
        {
            // Redirect the user to the dashboard page
            Response.Redirect("~/Private/StaffDashboard.aspx", true);
        }

        protected void cpeMangement(object sender, EventArgs e)
        {
            // Redirect the user to the courses page
            Response.Redirect("~/Private/StaffCPEManagement.aspx", true);
        }
    }
}