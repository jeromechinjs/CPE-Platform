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
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\DatabaseCPE.mdf\";Integrated Security=True");

            if (Session["userType"] == "student")
            {
                SqlCommand cmd = new SqlCommand("Select StudentName from Student where StudentID = @StudentID", con);

                cmd.Parameters.AddWithValue("@StudentID", Session["StudentID"].ToString());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Student");


                name.Text = "" + ds.Tables["Student"].Rows[0][0].ToString().ToUpper(); // retrieve logged in user's name (then UPPERCASE)
            } else if (Session["userType"] == "staff")
            {
                SqlCommand cmd = new SqlCommand("Select StaffName from Staff where StaffID = @StaffID", con);

                cmd.Parameters.AddWithValue("@StaffID", Session["StaffID"].ToString());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Staff");


                name.Text = "" + ds.Tables["Staff"].Rows[0][0].ToString().ToUpper(); // retrieve logged in user's name (then UPPERCASE)
            }
        }
        
        protected void Logout(object sender, EventArgs e)
		{
			FormsAuthentication.SignOut();
		}
	}
}