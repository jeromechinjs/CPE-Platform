using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPE_Platform
{
	public partial class StaffRegisterInfo : System.Web.UI.Page
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

        protected void btnStaffRegister_Click(object sender, EventArgs e)
        {
			SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\FYP Project 2\\CPE-Platform\\App_Data\\DatabaseCPE.mdf\";Integrated Security=True");
			SqlCommand cmd = new SqlCommand("Insert into Staff (StaffID, StaffName, StaffPhoneNum, StaffPassword, StaffEmail, StaffPosition) Values (@StaffID, @StaffName, @StaffPhoneNum, @StaffPassword, @StaffEmail, @StaffPosition)", con);
			cmd.Parameters.AddWithValue("@StaffID", txtStaffID.Text);
			cmd.Parameters.AddWithValue("@StaffName", txtStaffName.Text);
			cmd.Parameters.AddWithValue("@StaffPhoneNum", txtStaffPhoneNum.Text);
			cmd.Parameters.AddWithValue("@StaffPassword", EncryptData(txtStaffPassword.Text.Trim()));
			cmd.Parameters.AddWithValue("@StaffEmail", txtStaffEmail.Text);
			cmd.Parameters.AddWithValue("@StaffPosition", txtStaffPosition.Text);

			con.Open();
			cmd.ExecuteNonQuery();
			con.Close();
			Response.Write("<script>alert('Record Inserted Successfully')</script>");
		}
    }
}