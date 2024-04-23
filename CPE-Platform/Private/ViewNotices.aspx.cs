using System;
using System.Collections;
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
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadNotices();
            }

        }

        protected void loadNotices()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NoticeTitle");
            dt.Columns.Add("NoticeSender");
            dt.Columns.Add("NoticeDate");

            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Notices", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            rptr1.DataSource = dt;
            rptr1.DataBind();

        }

        protected void btn_view(object sender, CommandEventArgs e)
        {
            string NoticeID = e.CommandArgument.ToString();

            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Notices where NoticeID=@NoticeID", con);
                cmd.Parameters.AddWithValue("@NoticeID", NoticeID);

                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) // returns true if have more rows to read, else false
                {
                    txtNoticeTitle.Text = dataReader["NoticeTitle"].ToString();
                    txtNoticeDesc.Text = dataReader["NoticeDesc"].ToString();
                }
                dataReader.Close();
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#noticeInfoModal').modal('show');", true);
        }
    }
}