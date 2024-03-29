﻿using System;
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
using System.Net.Configuration;

namespace CPE_Platform
{
	public partial class StudentForgetPassword : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			lblErrorColor.Visible = false;
		}


		protected void btnConfirmStudentForgetPassword_Click(object sender, EventArgs e)
		{
			if (Page.IsValid)
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
							string script = "alert('An email with instructions to reset your password is sent to your registered email');";
							ScriptManager.RegisterStartupScript(this, GetType(), "Alert", script, true);
							lblErrorMsg.ForeColor = System.Drawing.Color.White;
							lblErrorMsg.Text = "An email with instructions to reset your password is sent to your registered email";
						}
						else
						{
							lblErrorMsg.ForeColor = System.Drawing.Color.Red;
							lblErrorColor.Visible = true;
							lblErrorMsg.Text = "IC Number not found!";
						}
					}
				}
			}

		}

		SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
		private void SendPasswordResetEmail(string ToEmail, string studentName, string UniqueId)
		{
			// MailMessage class is present is System.Net.Mail namespace
			MailMessage mailMessage = new MailMessage("fypproject811@gmail.com", ToEmail);
			// StringBuilder class is present in System.Text namespace
			StringBuilder sbEmailBody = new StringBuilder();
			sbEmailBody.Append("Dear " + studentName + ",<br/><br/>");
			sbEmailBody.Append("Please click on the following link to reset your password");
			sbEmailBody.Append("<br/>"); sbEmailBody.Append("http://localhost:62893/Public/StudentResetPassword?uid=" + UniqueId);
			sbEmailBody.Append("<br/><br/>");
			sbEmailBody.Append("<b>Tunku Abdul Rahman University of Management and Technology (TAR UMT) </b>");

			mailMessage.IsBodyHtml = true;

			SmtpClient smtp = new SmtpClient();
			smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
			smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
			NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
			//---Your Email and password
			smtp.UseDefaultCredentials = true;
			smtp.Credentials = NetworkCred;
			smtp.Port = 587; //---- SMTP Server port number. This varies from host to host. 

			mailMessage.Body = sbEmailBody.ToString();
			mailMessage.Subject = "Reset Your Password";

			smtp.Send(mailMessage);
		}
	}
}