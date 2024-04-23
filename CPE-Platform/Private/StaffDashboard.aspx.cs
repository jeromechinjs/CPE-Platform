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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataTable initialData = GetDataFromDatabase("");
                rptr1.DataSource = initialData;
                rptr1.DataBind();

            }
            // hide all toasts
            noticeUpdated.CssClass = noticeUpdated.CssClass.Replace("show", "hide");
            noticeAdded.CssClass = noticeAdded.CssClass.Replace("show", "hide");



        }

        protected void modal_Click(object sender, EventArgs e)
        {
            string script = "$('#mymodal').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);

            Session["gotNoticeUpdate"] = false; // keep track if user adding new notice or updating existing ones

            // prevent edit, will auto assign (read-only)
            txtNoticeID.ReadOnly = true;
            txtNoticeDate.ReadOnly = true;

            // let staff fill in these empty boxes
            txtNoticeTitle.Text = null;
            txtNoticeDesc.Text = null;
            txtNoticeSender.Text = null;
            lblmsg.Text = null;

            // auto assign date and time
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Notices", conn);

                // execute scalar counts number of rows retreived when cmd is used
                int numOfRecords = (int)cmd.ExecuteScalar() + 1; // increment by one, to add new record with new notice ID

                // get today date
                DateTime today = DateTime.Today;

                // auto assign
                txtNoticeID.Text = numOfRecords.ToString();
                txtNoticeDate.Text = today.ToString("d"); // get date only (given format is mm/dd/yyyy)
            }
        }



        protected void btndlt_Command(object sender, CommandEventArgs e)
        {
            string NoticeID = e.CommandArgument.ToString(); // command argument refer to the button eval name and pass the value into here
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from Notices where NoticeID=@NoticeID", conn);
                cmd.Parameters.AddWithValue("@NoticeID", NoticeID);
                cmd.ExecuteNonQuery();
            }

            rptr1.DataBind();
            string message = "Successfully Delete the record";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            DataTable initialData = GetDataFromDatabase("");
            rptr1.DataSource = initialData;
            rptr1.DataBind();
        }

        protected void btnupdate_Command(object sender, CommandEventArgs e)
        {
            string NoticeID = e.CommandArgument.ToString();
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Notices where NoticeID=@NoticeID", conn);
                cmd.Parameters.AddWithValue("@NoticeID", NoticeID);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    // display existing notice info into text box
                    txtNoticeID.Text = dataReader["NoticeID"].ToString();
                    txtNoticeTitle.Text = dataReader["NoticeTitle"].ToString();
                    txtNoticeDesc.Text = dataReader["NoticeDesc"].ToString();
                    txtNoticeSender.Text = dataReader["NoticeSEnder"].ToString();
                    txtNoticeDate.Text = dataReader["NoticeDate"].ToString();
                }
                dataReader.Close();
            }
            txtNoticeID.ReadOnly = true; // prevent edit, will auto assign
            txtNoticeDate.ReadOnly = true;

            Session["gotNoticeUpdate"] = true; // user updating, update the flag. So will use update command to modify existing record
            lblmsg.Text = null;
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#mymodal').modal('show');", true);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {

            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd;

                string NoticeID = txtNoticeID.Text;
                bool gotNoticeUpdate = Session["gotNoticeUpdate"] != null && (bool)Session["gotNoticeUpdate"];

                if (gotNoticeUpdate) // user wants to update existing notice
                {
                    cmd = new SqlCommand("UPDATE Notices SET NoticeTitle=@NoticeTitle,  NoticeDesc=@NoticeDesc, NoticeSender=@NoticeSender WHERE NoticeID=@NoticeID", con);
                    cmd.Parameters.AddWithValue("@NoticeID", NoticeID);
                    cmd.Parameters.AddWithValue("@NoticeTitle", txtNoticeTitle.Text);
                    cmd.Parameters.AddWithValue("@NoticeDesc", txtNoticeDesc.Text);
                    cmd.Parameters.AddWithValue("@NoticeSender", txtNoticeSender.Text);

                    con.Open();
                    int rowsAltered = cmd.ExecuteNonQuery();

                    if (rowsAltered > 0)
                    {
                        // show update sucessful message
                        noticeUpdated.CssClass = noticeUpdated.CssClass.Replace("hide", "show");
                    }
                    con.Close();

                }
                else // user wants to add new notice
                {
                    cmd = new SqlCommand("INSERT INTO Notices (NoticeID,NoticeTitle,NoticeDesc,NoticeSender,NoticeDate) VALUES (@NoticeID,@NoticeTitle,@NoticeDesc,@NoticeSender,@NoticeDate)", con);

                    if (string.IsNullOrEmpty(txtNoticeTitle.Text) || string.IsNullOrEmpty(txtNoticeDesc.Text) || string.IsNullOrEmpty(txtNoticeSender.Text))
                    {
                        lblmsg.Text = "Please fill up the information"; // validation checks
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@NoticeID", NoticeID);
                        cmd.Parameters.AddWithValue("@NoticeTitle", txtNoticeTitle.Text);
                        cmd.Parameters.AddWithValue("@NoticeDesc", txtNoticeDesc.Text);
                        cmd.Parameters.AddWithValue("@NoticeSender", txtNoticeSender.Text);
                        cmd.Parameters.AddWithValue("@NoticeDate", txtNoticeDate.Text);

                        con.Open();
                        int rowsAltered = cmd.ExecuteNonQuery();

                        if (rowsAltered > 0)
                        {
                            // show notice added sucessful message
                            noticeAdded.CssClass = noticeAdded.CssClass.Replace("hide", "show");
                        }
                        con.Close();
                    }
                }

                string script = "$('#mymodal').modal('show');";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);

                // refresh data view
                rptr1.DataBind();
                con.Close();

                DataTable initialData = GetDataFromDatabase("");
                rptr1.DataSource = initialData;
                rptr1.DataBind();

            }
        }

        private DataTable GetDataFromDatabase(string searchKeyword)
        {
            // Define your SQL query to fetch data based on the search query
            string query = "SELECT NoticeTitle, * FROM Notices WHERE NoticeTitle LIKE @DescKeywords ORDER BY NoticeDate DESC";

            // Execute the query using ADO.NET and fetch data into a DataTable
            DataTable searchData = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to the command for search query
                    cmd.Parameters.AddWithValue("@DescKeywords", "%" + searchKeyword + "%");

                    // Open connection and execute command
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(searchData);
                    //con.Close();
                }
            }
            return searchData;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Get the search criteria from the text box txtSearch
            string searchKeyword = txtSearch.Text.Trim();
            // Fetch data from the database based on the search query
            DataTable searchData = GetDataFromDatabase(searchKeyword); // Implement this method to fetch data

            // Check if search results are empty
            if (searchData.Rows.Count == 0)
            {

                // Clear any existing rows in the DataTable
                searchData.Rows.Clear();

                // Bind the DataTable to the repeater to display the message
                rptr1.DataSource = searchData;
                rptr1.DataBind();
                HideEditAndDeleteButtons();

            }
            else
            {
                // Bind the search result to the repeater
                rptr1.DataSource = searchData;
                rptr1.DataBind();

                ShowEditAndDeleteButtons();
            }


        }

        // Method to hide the edit and delete buttons
        private void HideEditAndDeleteButtons()
        {
            foreach (RepeaterItem item in rptr1.Items)
            {
                LinkButton btnUpdate = (LinkButton)item.FindControl("btnupdate");
                LinkButton btnDelete = (LinkButton)item.FindControl("btndlt");
                if (btnUpdate != null && btnDelete != null)
                {
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }

        // Method to show the edit and delete buttons
        private void ShowEditAndDeleteButtons()
        {
            foreach (RepeaterItem item in rptr1.Items)
            {
                LinkButton btnUpdate = (LinkButton)item.FindControl("btnupdate");
                LinkButton btnDelete = (LinkButton)item.FindControl("btndlt");
                if (btnUpdate != null && btnDelete != null)
                {
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                }
            }
        }
    }
}