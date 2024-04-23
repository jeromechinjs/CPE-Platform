using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace CPE_Platform
{
    public partial class StudentMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StudentID"] != null)
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\DatabaseCPE.mdf\";Integrated Security=True");
                SqlCommand cmd = new SqlCommand("Select StudentName from Student where StudentID = @StudentID", con);

                cmd.Parameters.AddWithValue("@StudentID", Session["StudentID"].ToString());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Student");


                name.Text = "" + ds.Tables["Student"].Rows[0][0].ToString().ToUpper(); // retrieve logged in user's name (then UPPERCASE)
            }
            else
            {
				FormsAuthentication.SignOut();
				Response.Redirect("~/SessionEnd.aspx", true);
			}

            // cart items number badge (this section important, when switching to other pages, cart number will change value)
            int numOfItems = Convert.ToInt32(Session["numOfItems"]);
            if (numOfItems > 0)
            {
                cartBadge.Text = "" + numOfItems;
            }
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
        protected void goCart(object sender, EventArgs e)
        {
            // Redirect the user to the dashboard page
            Response.Redirect("~/Private/Cart.aspx", true);
        }

		protected void goPaymentHistory(object sender, EventArgs e)
		{
			// Redirect the user to the dashboard page
			Response.Redirect("~/Private/PaymentHistory.aspx", true);
		}


		protected void goDashboard(object sender, EventArgs e)
        {
            // Redirect the user to the dashboard page
            Response.Redirect("~/Private/StudentDashboard.aspx", true);
        }

        protected void goCourses(object sender, EventArgs e)
        {
            // Redirect the user to the courses page
            Response.Redirect("~/Private/Courses.aspx", true);
        }

        protected void goViewNotices(object sender, EventArgs e)
        {
            // Redirect the user to the dashboard page
            Response.Redirect("~/Private/ViewNotices.aspx", true);
        }
    }
}