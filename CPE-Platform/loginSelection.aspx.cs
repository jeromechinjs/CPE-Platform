using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPE_Platform
{
	public partial class loginSelection : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void btnStaffLogin_Click(object sender, EventArgs e)
        {
			Response.Redirect("~/StaffLogin.aspx");  // will redirect to home page once home page is created
		}

		protected void btnStudentLogin_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/StudentLogin.aspx");  // will redirect to home page once home page is created
		}
	}
}