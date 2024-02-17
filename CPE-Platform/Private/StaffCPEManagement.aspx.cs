using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace CPE_Platform.Private
{

	public partial class StaffCPEManagement : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

				DataTable initialData = GetDataFromDatabase("");
				rptr1.DataSource = initialData;
				rptr1.DataBind();

			}

		}

		protected void modal_Click(object sender, EventArgs e)
		{
			string script = "$('#mymodal').modal('show');";
			ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
			Session["IsUpdateFlag"] = false;    // session to set whether the user clicked which button
			txtCPECode.Text = "";
			txtCPECode.ReadOnly = false;
			txtCPEDesc.Text = null;
			txtCPEPrice.Text = null;
			txtCPERewards.Text = null;
			dllDate.SelectedValue = null;
			txtCPESeat.Text = null;
			lblmsg.Text = null;
		}



		protected void btndlt_Command(object sender, CommandEventArgs e)
		{
			string id = e.CommandArgument.ToString(); // command argument refer to the button eval name and pass the value into here
			string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			using (SqlConnection conn = new SqlConnection(connectionstring))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand("delete from CPE_Course where CPECode=@id", conn);
				cmd.Parameters.AddWithValue("@id", id);
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
			string id = e.CommandArgument.ToString();
			txtCPECode.Text = id;
			string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			using (SqlConnection conn = new SqlConnection(connectionstring))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand("SELECT * FROM CPE_Course where CPECode=@id", conn);
				cmd.Parameters.AddWithValue("@id", id);
				SqlDataReader dataReader = cmd.ExecuteReader();
				if (dataReader.Read())
				{
					txtCPECode.Text = dataReader["CPECode"].ToString();
					txtCPEDesc.Text = dataReader["CPEDesc"].ToString();
					txtCPESeat.Text = dataReader["CPESeatAmount"].ToString();
					txtCPEPrice.Text = dataReader["CPEPrice"].ToString();
					dllDate.SelectedValue = dataReader["CPEDate"].ToString().Trim();
					txtCPERewards.Text = dataReader["Rewards"].ToString();
				}
				dataReader.Close();
			}
			txtCPECode.ReadOnly = true;
			Session["IsUpdateFlag"] = true;
			lblmsg.Text = null;
			ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#mymodal').modal('show');", true);
		}

		protected void btnsave_Click(object sender, EventArgs e)
		{

			string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			using (SqlConnection con = new SqlConnection(connectionstring))
			{
				SqlCommand cmd;
				int cpeSeatAmount, cpeRewards;
				con.Open();
				bool isUpdateFlag = Session["IsUpdateFlag"] != null && (bool)Session["IsUpdateFlag"];
				// check if the CPE code is exist

				if (!isUpdateFlag)
				{
					// to check whether the CPECode is exist
					cmd = new SqlCommand("SELECT COUNT(*) FROM CPE_Course WHERE CPECode = @CPECode", con);
					cmd.Parameters.AddWithValue("@CPECode", txtCPECode.Text);
					int existingCPECount = (int)cmd.ExecuteScalar();

					if (existingCPECount == 0)
					{
						if (string.IsNullOrEmpty(txtCPECode.Text) || string.IsNullOrEmpty(txtCPEDesc.Text) || string.IsNullOrEmpty(txtCPEPrice.Text) || string.IsNullOrEmpty(txtCPERewards.Text) || string.IsNullOrEmpty(txtCPESeat.Text))
						{
							lblmsg.Text = "Please fill up the information";
						}
						else
						{
							cmd = new SqlCommand("INSERT INTO CPE_Course (CPECode,CPEDesc,CPESeatAmount,CPEPrice,CPEDate,Rewards,ModifiedDate) VALUES (@id,@CPEDesc,@CPESeatAmount,@CPEPrice,@CPEDate,@Rewards,@ModifiedDate)", con);
							cmd.Parameters.AddWithValue("@id", txtCPECode.Text);
							cmd.Parameters.AddWithValue("@CPEDesc", txtCPEDesc.Text);

							if (int.TryParse(txtCPESeat.Text, out cpeSeatAmount))
							{
								// Conversion successful, add the parameter
								cmd.Parameters.AddWithValue("@CPESeatAmount", cpeSeatAmount);
							}



							cmd.Parameters.AddWithValue("@CPEPrice", txtCPEPrice.Text);
							cmd.Parameters.AddWithValue("@CPEDate", dllDate.SelectedItem.ToString());
							if (int.TryParse(txtCPERewards.Text, out cpeRewards))
							{
								// Conversion successful, add the parameter
								cmd.Parameters.AddWithValue("@Rewards", cpeRewards);
							}
							else
							{
								cmd.Parameters.AddWithValue("@Rewards", DBNull.Value);
							}

							DateTime currentDateTime = DateTime.Now;
							cmd.Parameters.AddWithValue("@ModifiedDate", currentDateTime);
							int rowsaffected = cmd.ExecuteNonQuery();

							if (rowsaffected > 0)
							{
								lblmsg.Text = "Data Insert Successfully";
							}
							else
							{
								lblmsg.Text = "CPE Code exists";

							}
						}
					}
					else
					{
						lblmsg.Text = "Please Enter a valid Data";
					}
				}

				else
				{

					string id = txtCPECode.Text;

					cmd = new SqlCommand("UPDATE CPE_Course SET CPEDesc=@CPEDesc, CPESeatAmount=@CPESeatAmount,CPEPrice=@CPEPrice," +
					"CPEDate=@CPEDate,Rewards=@Rewards, ModifiedDate=@ModifiedDate where CPECode=@id", con);
					cmd.Parameters.AddWithValue("@id", id);
					cmd.Parameters.AddWithValue("@CPEDesc", txtCPEDesc.Text);

					if (int.TryParse(txtCPESeat.Text, out cpeSeatAmount))
					{
						// Conversion successful, add the parameter
						cmd.Parameters.AddWithValue("@CPESeatAmount", cpeSeatAmount);
					}

					cmd.Parameters.AddWithValue("@CPEPrice", txtCPEPrice.Text);
					cmd.Parameters.AddWithValue("@CPEDate", dllDate.SelectedItem.ToString());
					if (int.TryParse(txtCPERewards.Text, out cpeRewards))
					{
						// Conversion successful, add the parameter
						cmd.Parameters.AddWithValue("@Rewards", cpeRewards);
					}
					DateTime currentDateTime = DateTime.Now;
					cmd.Parameters.AddWithValue("@ModifiedDate", currentDateTime);

					int rowsaffected = cmd.ExecuteNonQuery();

					if (rowsaffected > 0)
					{
						lblmsg.Text = "Data Update Successfully";
					}
					else
					{
						lblmsg.Text = "CPE Code is exist";
					}
				}
				//Response.Redirect("~/Private/StaffCPEManagement.aspx");
				string script = "$('#mymodal').modal('show');";
				ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
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
			string query = "SELECT CONCAT(CPECode, ' ', CPEDesc) AS CPECourse, * FROM CPE_Course WHERE CONCAT(CPECode, ' ', CPEDesc) LIKE @DescKeywords";

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