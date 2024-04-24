using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPE_Platform.Private
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadRecords();
            }

        }

        protected void loadRecords()
        {
            if (Session["StudentID"] != null) {
                string studentID = Session["StudentID"].ToString();

                DataTable dt = new DataTable();
                dt.Columns.Add("RegistrationID");
                dt.Columns.Add("CPECode");
                dt.Columns.Add("CPEName");
                dt.Columns.Add("CPEStartDate");
                dt.Columns.Add("CPEEndDate");

                string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionstring);
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT R.RegistrationID, C.CPECode, C.CPEName, C.CPEStartDate, C.CPEEndDate FROM CPE_Course C, CPE_Registration R WHERE R.StudentID=@StudentID AND R.CPECode=C.CPECode", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@StudentID", studentID);

                da.Fill(dt);

                activeCourses.DataSource = dt;
                activeCourses.DataBind();

            }
        }
    }
}