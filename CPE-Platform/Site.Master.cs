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
            SqlCommand cmd = new SqlCommand("Select StudentName from Student where StudentID = @StudentID", con);
            cmd.Parameters.AddWithValue("@StudentID", Session["StudentID"]);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Student");

            name.Text = "" + ds.Tables["Student"].Rows[0][0].ToString().ToUpper(); // retrieve logged in user's name (then UPPERCASE)
        }
        
        protected void Logout(object sender, EventArgs e)
		{
			FormsAuthentication.SignOut();
		}
	}
}