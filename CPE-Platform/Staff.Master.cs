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
    public partial class StaffMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StaffID"] != null)
            {
				string staffID = Session["StaffID"].ToString();

				SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\DatabaseCPE.mdf\";Integrated Security=True");
				SqlCommand cmd = new SqlCommand("Select StaffName from Staff where StaffID = @StaffID", con);

				cmd.Parameters.AddWithValue("@StaffID", staffID);

				con.Open();
				cmd.ExecuteNonQuery();
				con.Close();

				SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				dataAdapter.Fill(ds, "Staff");


				name.Text = "" + ds.Tables["Staff"].Rows[0][0].ToString().ToUpper(); // retrieve logged in user's name (then UPPERCASE)

			}
			else
			{
				FormsAuthentication.SignOut();
			}

		}
        
        protected void Logout(object sender, EventArgs e)
		{
			FormsAuthentication.SignOut();
		}
	}
}