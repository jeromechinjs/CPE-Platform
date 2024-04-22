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
            // hide all toasts
            updateSucess.CssClass = updateSucess.CssClass.Replace("show", "hide");
            originalPasswordWrong.CssClass = originalPasswordWrong.CssClass.Replace("show", "hide");
            passwordSame.CssClass = passwordSame.CssClass.Replace("show", "hide");
            passwordDifferent.CssClass = passwordDifferent.CssClass.Replace("show", "hide");
            passwordChangedSuccess.CssClass = passwordChangedSuccess.CssClass.Replace("show", "hide");

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
                        //modaltxtIC.Text = dataReader["StudentIC"].ToString();
                        //modaltxtName.Text = dataReader["StudentName"].ToString();
                        modaltxtPhone.Text = dataReader["StudentPhoneNum"].ToString();
                        //modaltxtEmail.Text = dataReader["StudentEmail"].ToString();
                        //modaltxtFaculty.Text = dataReader["StudentFaculty"].ToString();
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
                string updateProfileInfo = "UPDATE Student SET StudentPhoneNum=@StudentPhoneNum WHERE StudentID=@StudentID";
                
                string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    using (SqlCommand getPtsCollected = new SqlCommand(updateProfileInfo, con))
                    {
                        // edit update command
                        getPtsCollected.Parameters.AddWithValue("@StudentID", studentID);
                        getPtsCollected.Parameters.AddWithValue("@StudentPhoneNum", modaltxtPhone.Text);
                        
                        // add parameters to update
                        //cmd.Parameters.AddWithValue("@CPEName", txtCPEName.Text);
                        //cmd.Parameters.AddWithValue("@CPEDesc", txtCourseDesc.Text);
                        //cmd.Parameters.AddWithValue("@CPEVenue", txtVenue.Text);

                        con.Open();
                        int rowsAltered = getPtsCollected.ExecuteNonQuery();

                        if (rowsAltered > 0)
                        {
                            // show update sucessful message
                            updateSucess.CssClass = updateSucess.CssClass.Replace("hide", "show");
                            getProfileInformation(studentID); // update view
                        }

                        con.Close();
                    }
                }
            }
        }

        protected void change_password(object sender, EventArgs e)
        {
            if (Session["StudentID"] != null)
            {
                string studentID = Session["StudentID"].ToString();
                string queryGetPassword = "SELECT * FROM Student where studentID=@studentID";
                string queryUpdatePassword = "UPDATE Student SET StudentPassword=@StudentPassword WHERE studentID=@studentID";
                bool passwordInputsOk = false;

                string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    // first command, retreive current password for checking if new password same as current password 
                    using (SqlCommand getPassword = new SqlCommand(queryGetPassword, con))
                    {
                        con.Open();
                        getPassword.Parameters.AddWithValue("@studentID", studentID);

                        // validation checks 
                        using (SqlDataReader dataReader = getPassword.ExecuteReader())
                        {
                            if (dataReader.Read()) // returns true if have more rows to read, else false
                            {
                                string currentPassword = dataReader["StudentPassword"].ToString();
                                testlbl.Text = currentPassword;
                                if (oldPassword.Text != currentPassword) // old password same as current password
                                {
                                    originalPasswordWrong.CssClass = originalPasswordWrong.CssClass.Replace("hide", "show");
                                }
                                else if (oldPassword.Text == newPassword.Text) // new password same as old password
                                {
                                    passwordSame.CssClass = passwordSame.CssClass.Replace("hide", "show");
                                }
                                else if (newPassword.Text != confirmPassword.Text) // new password not same as confirm password
                                {
                                    passwordDifferent.CssClass = passwordDifferent.CssClass.Replace("hide", "show");
                                }
                                else // change password if all inputs ok
                                {
                                    passwordInputsOk = true;

                                }
                            }
                        }
                        con.Close();
                    }

                    // secomd command, update password into db
                    using (SqlCommand updatePassword = new SqlCommand(queryUpdatePassword, con))
                    {
                        if (passwordInputsOk)
                        {
                            // edit update command
                            updatePassword.Parameters.AddWithValue("@StudentID", studentID);
                            updatePassword.Parameters.AddWithValue("@StudentPassword", newPassword.Text);

                            con.Open();
                            int rowsAltered = updatePassword.ExecuteNonQuery();

                            if (rowsAltered > 0)
                            {
                                // show update sucessful message
                                passwordChangedSuccess.CssClass = passwordChangedSuccess.CssClass.Replace("hide", "show");
                            }

                            con.Close();
                        }

                    }
                }
            }
        }

        protected void getProfileInformation(String studentID)
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