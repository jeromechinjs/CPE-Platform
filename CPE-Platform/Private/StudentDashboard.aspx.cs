using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPE_Platform.Private
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getDashboardFigures(); // get the points collected, discounts, and courses taking
            }
        }
        protected void getDashboardFigures()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                String studentID = "asdf";
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CPE_Course where CPECode=@CPECode", con);
                cmd.Parameters.AddWithValue("@CPECode", studentID);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) // returns true if have more rows to read, else false
                {
                    pts_collected.Text = dataReader["CPECode"].ToString();
                    discounts_collected.Text = dataReader["CPEName"].ToString();
                    num_active_courses.Text = dataReader["CPEDesc"].ToString();
                }
                dataReader.Close();
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#courseDetailsModal').modal('show');", true);
        }

    }

}