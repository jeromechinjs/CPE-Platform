using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;

namespace CPE_Platform
{
	public partial class RegisterStudentInfo : System.Web.UI.Page
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

		protected void btnStudentRegister_Click(object sender, EventArgs e)
		{
			SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\FYP Project 2\\CPE-Platform\\App_Data\\DatabaseCPE.mdf\";Integrated Security=True");
			SqlCommand cmd = new SqlCommand("Insert into Student (StudentID, StudentName, StudentPhoneNum, StudentPassword, StudentEmail, StudentFaculty) Values (@StudentID, @StudentName, @StudentPhoneNum, @StudentPassword, @StudentEmail, @StudentFaculty)", con);
			cmd.Parameters.AddWithValue("@StudentID", txtStudentID.Text);
			cmd.Parameters.AddWithValue("@StudentName", txtStudentName.Text);
			cmd.Parameters.AddWithValue("@StudentPhoneNum", txtStudentPhoneNum.Text);
			cmd.Parameters.AddWithValue("@StudentPassword", EncryptData(txtStudentPassword.Text.Trim()));
			cmd.Parameters.AddWithValue("@StudentEmail", txtStudentEmail.Text);
			cmd.Parameters.AddWithValue("@StudentFaculty", txtStudentFaculty.Text);
			con.Open();
			cmd.ExecuteNonQuery();
			con.Close();
			Response.Write("<script>alert('Record Inserted Successfully')</script>");
		}
	}
}