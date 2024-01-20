using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace CPE_Platform
{
	public partial class StudentForgetPassword : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnRetrieveStudentLogin_Click(object sender, EventArgs e)
		{
			Response.Redirect("StudentLogin.aspx");
		}


		protected void btnConfirmStudentForgetPassword_Click(object sender, EventArgs e)
		{
			string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			using (SqlConnection con = new SqlConnection(conStr))
			{
				SqlCommand cmd = new SqlCommand("spResetPassword", con);
				cmd.CommandType = CommandType.StoredProcedure;

				//SqlParameter paramStudentID = new SqlParameter("@StudentId", txtStudentID.Text);
				SqlParameter paramStudentIC = new SqlParameter("@StudentIc", txtStudentIC.Text);

				//cmd.Parameters.Add(paramStudentID);
				cmd.Parameters.Add(paramStudentIC);

				con.Open();
				SqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					if (Convert.ToBoolean(rdr["ReturnCode"]) == true)
					{
						SendPasswordResetEmail(rdr["Email"].ToString(), rdr["StuName"].ToString(), rdr["UniqueId"].ToString());
						lblErrorMsg.Text = "An email with instructions to reset your password is sent to your registered email";
					}
					else
					{
						lblErrorMsg.ForeColor = System.Drawing.Color.Red;
						lblErrorMsg.Text = "Username not found!";
					}
				}
			}
		}

		private void SendPasswordResetEmail(string ToEmail, string studentName, string UniqueId)
		{
			// MailMessage class is present is System.Net.Mail namespace
			MailMessage mailMessage = new MailMessage("chungyc-wp20@student.tarc.edu.my", ToEmail);
			// StringBuilder class is present in System.Text namespace
			StringBuilder sbEmailBody = new StringBuilder();
			sbEmailBody.Append("Dear " + studentName + ",<br/><br/>");
			sbEmailBody.Append("Please click on the following link to reset your password");
			sbEmailBody.Append("<br/>"); sbEmailBody.Append("http://localhost:62893/StudentResetPassword?uid=" + UniqueId);
			sbEmailBody.Append("<br/><br/>");
			sbEmailBody.Append("<b>Pragim Technologies</b>");

			mailMessage.IsBodyHtml = true;

			mailMessage.Body = sbEmailBody.ToString();
			mailMessage.Subject = "Reset Your Password";
			SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

			smtpClient.Credentials = new System.Net.NetworkCredential()
			{
				UserName = "your email address",
				Password = "google account password"
			};

			smtpClient.EnableSsl = true;
			smtpClient.Send(mailMessage);
		}
	}
}