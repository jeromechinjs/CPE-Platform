using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace CPE_Platform.Private
{
	public partial class StaffCPEManagement : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void modal_Click(object sender, EventArgs e)
		{ 
			string script = "$('#mymodal').modal('show');";
			ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
		}

		protected void btnsave_Click(object sender, EventArgs e)
		{
			string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			using (SqlConnection con = new SqlConnection(connectionstring))
			{
				con.Open();
				SqlCommand cmd;
				if (!string.IsNullOrEmpty(txtCPECode.Text))
				{
					string id = txtCPECode.Text;
					cmd = new SqlCommand("UPDATE CPE_Course SET CPEDesc=@CPEDesc, CPESeatAmount=@CPESeatAmount,CPEPrice=@CPEPrice," +
					"CPEDate=@CPEDate,Rewards=@Rewards where CPECode=@id", con);
					cmd.Parameters.AddWithValue("@id", id);

					// to be continued
				}
				else
				{
					cmd = new SqlCommand("INSERT INTO CPE_Course(CPECode,CPEDesc,CPESeatAmount,CPEPrice,CPEDate,Rewards)" +
					" values(@CPECode,@CPEDesc,@CPESeatAmount,@CPEPrice,@CPEDate,@Rewards)", con);
				}
				cmd.Parameters.AddWithValue("@CPECode", txtCPECode.Text);
				cmd.Parameters.AddWithValue("@CPEDesc", txtCPEDesc.Text);

				int cpeSeatAmount, cpeRewards;
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
				
				
				int rowsaffected = cmd.ExecuteNonQuery();
				//cmd.ExecuteNonQuery();
				con.Close();
				if (rowsaffected > 0)
				{
					lblmsg.Text = "Data Insert Successfully";
				}
				else
				{
					lblmsg.Text = "Error While inserting Data";
				}
				string script = "$('#mymodal').modal('show');";
				ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
				rptr1.DataBind();
			}
		}

		protected void btndlt_Command(object sender, CommandEventArgs e)
		{
			string id = e.CommandArgument.ToString(); // command argument refer to the button eval name and pass the value into here
			string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			using (SqlConnection conn = new SqlConnection(connectionstring))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand("delete from CPE_Course where CPECourse=@id", conn);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.ExecuteNonQuery();
			}
			rptr1.DataBind();
		}

		protected void btnupdate_Command(object sender, CommandEventArgs e)
		{
			string id = e.CommandArgument.ToString();
			txtCPECode.Text = id;
			string connectionstring = ConfigurationManager.ConnectionStrings["connection_"].ConnectionString;
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
					dllDate.SelectedValue = dataReader["CPEDate"].ToString();
					txtCPERewards.Text = dataReader["Rewards"].ToString();
				}
				dataReader.Close();
			}
			ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#mymodal').modal('show');", true);
		}

	}
}