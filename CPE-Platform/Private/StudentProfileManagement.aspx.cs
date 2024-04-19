﻿using System;
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

		SqlCommand cmd;
		SqlDataAdapter dataAdapter;
		DataSet ds = new DataSet();
		//SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Session["StudentID"] != null)
				{
                    // Profile part
                    string studentID = Session["StudentID"].ToString();

                    string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    using (SqlConnection con2 = new SqlConnection(connectionstring))
                    {
                        con2.Open();
                        SqlCommand cmd = new SqlCommand("SELECT * FROM Student where studentID=@studentID", con2);
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


                    // Rewards part
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
        protected void editInfo()
		{

		}


    }
}