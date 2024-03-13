using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace CPE_Platform.Private
{
	public partial class Billing : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				lblRewardsReedem.Text = "- RM 0.00";
				if (Session["StudentID"] != null)
				{
					// retrieve rewards points from the student
					SqlConnection rewardsCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
					string rewardsQuery = "SELECT RewardsAmount from Student where StudentID=@StudentID";

					rewardsCon.Open();
					using (SqlCommand rewardsCmd = new SqlCommand(rewardsQuery, rewardsCon))
					{
						rewardsCmd.Parameters.AddWithValue("@StudentID", Session["StudentID"].ToString());

						// Execute the query and retrieve the result
						object result = rewardsCmd.ExecuteScalar();
						if (result != null) // Check if the result is not null
						{
							// Convert the result to an integer and assign it to lblCPEPoints.Text
							int rewardsAmount = Convert.ToInt32(result);
							lblCPEPoints.Text = rewardsAmount.ToString() + " Points";
						}
					}
					rewardsCon.Close();

					if (Session["Cart"] != null)
					{
						ArrayList cartList = (ArrayList)Session["Cart"];
						//String[] cart = Session["Cart"].ToString().Split(',');
						string[] cart = cartList.ToArray(typeof(string)) as string[];

						cart = cart.Where(x => !string.IsNullOrEmpty(x)).ToArray();

						//System.Diagnostics.Debug.WriteLine(cart);
						//lblTotalAmount.Text = string.Join(", ", cart);



						// declare total Price to sum up the total amount of CPE Course
						double totalPrice = 0;
						//int count = 0;
						foreach (string item in cart)
						{
							SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

							string query = "SELECT * from CPE_Course where CPECode= '" + item + "'";
							//SqlDataAdapter da = new SqlDataAdapter("SELECT * from CPE_Course where CPECode= '" + item + "'", con);
							//DataSet ds = new DataSet();

							//da.Fill(ds);
							con.Open();
							using (SqlCommand cmd = new SqlCommand(query, con))
							{

								using (SqlDataReader reader = cmd.ExecuteReader())
								{
									while (reader.Read())
									{
										// calculate the total amount of all the CPE Course
										double price = Convert.ToDouble(reader["CPEPrice"]);
										totalPrice += price;
									}
									lblTotalAmount.Text = "RM " + totalPrice.ToString() + ".00";
								}
							}
							con.Close();
						}
					}
					else
					{
						lblTotalAmount.Text = "null";
					}







					//if (Session["Total"] != null)
					//{
					//	String[] total = Session["Total"].ToString().Split(',');
					//	total = total.Where(x => !string.IsNullOrEmpty(x)).ToArray();

					//	dt.Rows.Add(ds.Tables[0].Rows[0]["ProductName"].ToString(), ds.Tables[0].Rows[0]["ProductImage"].ToString(), ds.Tables[0].Rows[0]["ProductPrice"].ToString(), total[count]);

					//}
					//else
					//{
					//	dt.Rows.Add(ds.Tables[0].Rows[0]["ProductName"].ToString(), ds.Tables[0].Rows[0]["ProductImage"].ToString(), ds.Tables[0].Rows[0]["ProductPrice"].ToString(), ds.Tables[0].Rows[0]["ProductPrice"].ToString());
					//}

					//GridView1.DataSource = dt;
					//GridView1.DataBind();

					//GridView1.FooterRow.Cells[4].Text = Session["Sum"].ToString();

					//System.Diagnostics.Debug.WriteLine(Session["Sum"].ToString());
					//GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;
					//GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;

					//count++;
					//}


					//if (Session["Quantity"] != null)
					//{
					//	String[] quantity = Session["Quantity"].ToString().Split(',');
					//	quantity = quantity.Where(x => !string.IsNullOrEmpty(x)).ToArray();
					//	int countQuantity = 0;
					//	foreach (GridViewRow row in GridView1.Rows)
					//	{
					//		TextBox rowQuantity = (TextBox)row.FindControl("TextBox1");
					//		rowQuantity.Text = quantity[countQuantity++];
					//		rowQuantity.BorderStyle = BorderStyle.None;
					//	}
					//}

					//}

				}
			}
		}

		protected void chkboxPoints_CheckedChanged(object sender, EventArgs e)
		{
			SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

			SqlCommand cmd = new SqlCommand("SELECT RewardsAmount from Student where StudentID=@StudentID", con);
			cmd.Parameters.AddWithValue("@StudentID", Session["StudentID"].ToString());
			con.Open();
			object result = cmd.ExecuteScalar();
			if (result != null) // Check if the result (RewardsAmount) is not null
			{
				// Convert the result to an integer and assign it to lblCPEPoints.Text
				int rewardsAmount = Convert.ToInt32(result);
				if (chkboxPoints.Checked)
				{
					
					rewardsAmount = rewardsAmount / 10;
					lblPointsChk.Text = "- RM " + rewardsAmount.ToString() + ".00";
					lblRewardsReedem.Text = "- RM " + rewardsAmount.ToString() + ".00";
					lblCPEPoints.Text = "0 Points";
				}
				else
				{
					lblPointsChk.Text = null;
					lblRewardsReedem.Text = "- RM 0.00";
					lblCPEPoints.Text = rewardsAmount.ToString() + " Points";
				}
			}
			con.Close();
		}
	}
}