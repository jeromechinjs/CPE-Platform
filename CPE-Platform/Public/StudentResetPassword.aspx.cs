﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
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
			if (!IsPostBack)
			{
				if (!IsPasswordResetLinkValid())
				{
					Response.Write("<script>alert('Password Reset link has expired or is invalid');</script>");
					Response.Redirect("~/StudentLogin.aspx");
					//lblErrorMsg.ForeColor = System.Drawing.Color.Red;
					//lblErrorMsg.Text = "Password Reset link has expired or is invalid";
				}
			}
		}

		private bool IsPasswordResetLinkValid()
		{
			List<SqlParameter> paramList = new List<SqlParameter>()
			{
				new SqlParameter()
				{
					ParameterName = "@GUID",
					Value = Request.QueryString["uid"]
				}
			};

			return ExecuteSP("spIsPasswordResetLinkValid", paramList);
		}

		private bool ChangeUserPassword()
		{
			List<SqlParameter> paramList = new List<SqlParameter>()
			{
				new SqlParameter()
				{
					ParameterName = "@GUID",
					Value = Request.QueryString["uid"]
				},
				new SqlParameter()
				{
					ParameterName = "@Password",
					Value = EncryptData(txtConfirmPassword.Text.Trim())
					//Value = FormsAuthentication.HashPasswordForStoringInConfigFile(EncryptData(txtConfirmPassword.Text.Trim()), "SHA1")

				}
			};

			return ExecuteSP("spChangePassword", paramList);
		}
		private bool ExecuteSP(string SPName, List<SqlParameter> SPParameters)
		{
			string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			using (SqlConnection con = new SqlConnection(conStr))
			{
				SqlCommand cmd = new SqlCommand(SPName, con);
				cmd.CommandType = CommandType.StoredProcedure;

				foreach (SqlParameter parameter in SPParameters)
				{
					cmd.Parameters.Add(parameter);
				}

				con.Open();
				return Convert.ToBoolean(cmd.ExecuteScalar()); // use execute scalar because the output is 1 or 0
			}
		}

		protected void btnConfirmStudentForgetPassword_Click(object sender, EventArgs e)
		{
			if (Page.IsValid)
			{

				if (ChangeUserPassword())
				{
					Response.Write("<script>alert('Password Changed Successfully!')</script>");
					//lblErrorMsg.Text = "Password Changed Successfully!";
					Response.Redirect("~/StudentLogin.aspx");
				}
				else
				{
					lblErrorMsg.ForeColor = System.Drawing.Color.Red;
					lblErrorMsg.Text = "Password Reset link has expired or is invalid";
				}

			}
			else
			{
				lblErrorMsg.Text = "Invalid Password.";
			}

		}
	}
}