using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace CPE_Platform
{
    public partial class StudentMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StudentID"] != null)
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\DatabaseCPE.mdf\";Integrated Security=True");
                SqlCommand cmd = new SqlCommand("Select StudentName from Student where StudentID = @StudentID", con);

                cmd.Parameters.AddWithValue("@StudentID", Session["StudentID"].ToString());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Student");


                name.Text = "" + ds.Tables["Student"].Rows[0][0].ToString().ToUpper(); // retrieve logged in user's name (then UPPERCASE)
            }
            else
            {
				FormsAuthentication.SignOut();
			}


        }
    }
}