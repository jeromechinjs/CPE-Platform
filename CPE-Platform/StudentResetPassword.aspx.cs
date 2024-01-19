using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPE_Platform
{
	public partial class StudentResetPassword : System.Web.UI.Page
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


		protected void btnRetrieveStudentLogin_Click(object sender, EventArgs e)
		{
			Response.Redirect("StudentLogin.aspx");
		}

		

	}
}