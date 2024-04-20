using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection.Emit;

namespace CPE_Platform
{
	public partial class StudentProfileManagement : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
			{
                if (Session["StudentID"] != null)
				{
                    string studentID = Session["StudentID"].ToString();

                    // Profile part
                    getProfileInformation(studentID);

                    // Rewards part
                    getRewardsInfo(studentID);

				}

			}
            // hide toasts
            updateSucess.CssClass = updateSucess.CssClass.Replace("show", "hide");

        }
        protected void edit_info(object sender, CommandEventArgs e)
		{
            if (Session["StudentID"] != null)
            {
                string studentID = Session["StudentID"].ToString();

                string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Student where studentID=@studentID", con);
                    cmd.Parameters.AddWithValue("@studentID", studentID);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read()) // returns true if have more rows to read, else false
                    {
                        modaltxtID.Text = dataReader["StudentID"].ToString();
                        modaltxtIC.Text = dataReader["StudentIC"].ToString();
                        modaltxtName.Text = dataReader["StudentName"].ToString();
                        modaltxtPhone.Text = dataReader["StudentPhoneNum"].ToString();
                        modaltxtEmail.Text = dataReader["StudentEmail"].ToString();
                        modaltxtFaculty.Text = dataReader["StudentFaculty"].ToString();
                    } 
                    dataReader.Close();
                }
            }

            modaltxtID.ReadOnly = true;
            Session["IsUpdateFlag"] = true;
            lblmsg.Text = null;
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#updateDetails').modal('show');", true);

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Session["StudentID"] != null)
            {
                string studentID = Session["StudentID"].ToString();
                string updateProfileInfo = "UPDATE CPE_Course SET CPEName=@CPEName, CPEDesc=@CPEDesc, CPEType=@CPEType,CPEVenue=@CPEVenue, CPESeatAmount=@CPESeatAmount,CPEPrice=@CPEPrice,\" +\r\n                        \"CPETrainer=@CPETrainer, CPEStartDate=@CPEStartDate,CPEEndDate=@CPEEndDate,CPEStartTime=@CPEStartTime, CPEEndTime=@CPEEndTime, CPEContact=@CPEContact, CPEEmail=@CPEEmail, Rewards=@Rewards, ModifiedDate=@ModifiedDate where CPECode=@id";

                string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    using (SqlCommand getPtsCollected = new SqlCommand(updateProfileInfo, con))
                    {
                        con.Open();

                        // edit update command
                        getPtsCollected.Parameters.AddWithValue("@studentID", studentID);
                        // add parameters to update

                        //cmd.Parameters.AddWithValue("@CPEName", txtCPEName.Text);
                        //cmd.Parameters.AddWithValue("@CPEDesc", txtCourseDesc.Text);

                        //cmd.Parameters.AddWithValue("@CPEVenue", txtVenue.Text);

                        updateSucess.CssClass = updateSucess.CssClass.Replace("hide", "show");

                    }
                }
            }
        }

        protected  void getProfileInformation(String studentID)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Student where studentID=@studentID", con);
                cmd.Parameters.AddWithValue("@studentID", studentID);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) // returns true if have more rows to read, else false
                {
                    txtID.Text = dataReader["StudentID"].ToString();
                    txtIC.Text = dataReader["StudentIC"].ToString();
                    txtName.Text = dataReader["StudentName"].ToString();
                    txtPhone.Text = dataReader["StudentPhoneNum"].ToString();
                    txtEmail.Text = dataReader["StudentEmail"].ToString();
                    txtFaculty.Text = dataReader["StudentFaculty"].ToString();
                }
                dataReader.Close();
            }
        }
        protected void getRewardsInfo(String studentID)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            string query = "SELECT R.CPECode, C.CPEName, R.Progress, R.RewardAwarded FROM CPE_Course C, Rewards_Assign R WHERE R.StudentID =@StudentID AND R.CPECode =C.CPECode";


            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // bind data to grid view
                DataTable table = new DataTable();
                table.Load(reader);
                gvRewardsView.DataSource = table;
                if (table.Rows.Count > 0)
                {
                    gvRewardsView.DataBind();
                    //close reader
                    reader.Close();
                    con.Close();
                }
                else
                {
                    table.Rows.Add(table.NewRow());

                    gvRewardsView.DataSource = table;
                    gvRewardsView.DataBind();
                    gvRewardsView.Rows[0].Cells.Clear();
                    gvRewardsView.Rows[0].Cells.Add(new TableCell());
                    gvRewardsView.Rows[0].Cells[0].ColumnSpan = table.Columns.Count;
                    gvRewardsView.Rows[0].Cells[0].Text = "No Record is found !!";
                    gvRewardsView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }

            }
        }

    }
}