using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using System.Web.UI.WebControls.WebParts;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Net;
using System.Web.SessionState;
using System.Reflection.Emit;


namespace CPE_Platform.Private
{
	public partial class Billing : System.Web.UI.Page
	{
		private static string PayPalClientId => System.Configuration.ConfigurationManager.AppSettings["PayPalClientId"];
		private static string PayPalClientSecret => System.Configuration.ConfigurationManager.AppSettings["PayPalClientSecret"];

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
						// declare total Price to sum up the total amount of CPE Course
						double totalPrice = 0, totalSST = 0;
						//int count = 0;
						StringBuilder courseDetails = new StringBuilder();
						StringBuilder courseDetailsPrice = new StringBuilder();
						foreach (string item in (ArrayList)Session["Cart"])
						{
							//lblTotalAmount.Text = string.Join(", ", item);
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
										double priceWithoutSST = price * 0.94;

										// display the cpe code and price individually
										string itemDetails = reader["CPECode"].ToString() + "<br/>";
										courseDetails.AppendLine(itemDetails);
										string itemDetailsPrice = " RM " + priceWithoutSST.ToString("F2") + "<br/>";
										courseDetailsPrice.AppendLine(itemDetailsPrice);

									}
									Session["FinalPrice"] = totalPrice;
									//lblCourse.Text = reader["CPECode"].ToString() + reader["CPEName"].ToString();
									lblTotalAmount.Text = "RM " + totalPrice.ToString("F2");
									lblTotalCPEPrice.Text = "RM " + totalPrice.ToString("F2");
									totalSST = totalPrice * 0.06;
									lblSST.Text = "RM " + totalSST.ToString("F2");
								}
							}
							con.Close();
						}
						lblCourse.Text = courseDetails.ToString();
						lblCPEPrice.Text = courseDetailsPrice.ToString();
					}
					else
					{
						string script = "alert('Cart is empty');";
						ScriptManager.RegisterStartupScript(this, GetType(), "Alert", script, true);

						string redirectScript = "setTimeout(function() { window.location.href = '../Private/Courses.aspx'; }, 0.1);"; // Redirect after 0.001 seconds (1 milliseconds)
						ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", redirectScript, true);
					}

					//HandlePaymentProcess();
					
				}
			}
			if (Request.QueryString != null && Request.QueryString.Count != 0)
			{
				string approvalToken = Request.QueryString["token"];
				var response = Task.Run(async () => await captureOrder(approvalToken));  // Use .Result since Page_Load is not asynchronous

				// Process the response or handle errors as needed
				if (response.Result != null)
				{
					Order result = response.Result.Result<Order>();
					lblResult.Text = result.Status;
					// Process the response or handle errors
				}
				else
				{
					// Handle null response (error handling)
				}


			}
		}
		private void HandlePaymentProcess()
		{

			//try
			//{
			//	string approvalToken = Request.QueryString["token"];
			//	var response = Task.Run(async () => await captureOrder(approvalToken));  // Use .Result since Page_Load is not asynchronous
			//	if (response.Result != null)
			//	{
			//		Order result = response.Result.Result<Order>();
			//		Response.Redirect("../Private/billing.aspx");
			//		lblResult.Text = result.Status;
			//	}
			//	else
			//	{
			//		// Response is null, indicating cancellation or error during payment process
			//		Response.Redirect("../Private/billing.aspx");
			//		lblResult.Text = "Payment process was canceled or encountered an error.";
			//	}
			//}
			//catch (PayPal.HttpException ex)
			//{
			//	// Handle PayPal API exceptions
			//	lblResult.Text = "An error occurred while processing your payment. Please try again later.";
			//	// Log the exception for further investigation
			//	Console.WriteLine("PayPal API Exception: " + ex.ToString());
			//}
			//catch (Exception ex)
			//{
			//	// Handle other exceptions
			//	lblResult.Text = "An unexpected error occurred. Please try again later.";
			//	// Log the exception for further investigation
			//	Console.WriteLine("Exception: " + ex.ToString());
			//}
		}


		public async static Task<string> createOrder(HttpSessionState Session)
		{
			string finalAmount = Session["FinalPrice"].ToString();
			// construct a request object and set the desired parameters
			var order = new OrderRequest()
			{
				CheckoutPaymentIntent = "CAPTURE",
				PurchaseUnits = new List<PurchaseUnitRequest>()
				{
					new PurchaseUnitRequest()
					{
						AmountWithBreakdown = new AmountWithBreakdown()
						{
							CurrencyCode = "MYR",
							Value = finalAmount
						}
					}
				},
				ApplicationContext = new ApplicationContext()
				{
					ReturnUrl = "http://localhost:62893/Private/Billing.aspx",
					CancelUrl = "http://localhost:62893/Private/Billing.aspx"

				}
			};

			// call API with client and get a response for your call
			var request = new OrdersCreateRequest();
			request.Prefer("return=representation");
			request.RequestBody(order);
			var environment = new SandboxEnvironment(PayPalClientId, PayPalClientSecret);
			var response = await (new PayPalHttpClient(environment).Execute(request));
			var statusCode = response.StatusCode;
			Order result = response.Result<Order>();
			Console.WriteLine("Status: {0}", result.Status);
			Console.WriteLine("OrderId: {0}", result.Id);
			Console.WriteLine("Intent: {0}", result.CheckoutPaymentIntent);
			Console.WriteLine("Links:");
			foreach (LinkDescription link in result.Links)
			{
				Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
			}
			return GetApprovalUrl(result);
		}


		public async static Task<PayPalHttp.HttpResponse> captureOrder(string token) // tbc
		{
			// Construct a request object and set desired parameters
			// Replace ORDER-ID with the approved order id from create order
			var request = new OrdersCaptureRequest(token);
			request.RequestBody(new OrderActionRequest());
			var environment = new SandboxEnvironment(PayPalClientId, PayPalClientSecret);
			var response = await (new PayPalHttpClient(environment).Execute(request));
			var statusCode = response.StatusCode;
			Order result = response.Result<Order>();
			Console.WriteLine("Status: {0}", result.Status);
			Console.WriteLine("Capture Id: {0}", result.Id);
			return response;
		}
		public static string GetApprovalUrl(Order result)
		{

			// Check if there are links in the response
			if (result.Links != null)
			{
				// Find the approval URL link in the response
				LinkDescription approvalLink = result.Links.Find(link => link.Rel.ToLower() == "approve");
				if (approvalLink != null)
				{
					return approvalLink.Href;
				}
			}


			// Return a default URL or handle the error as needed
			return "http://localhost:62893/Private/Billing.aspx"; // Replace with your default URL
		}
		protected void btnProceedPayment_Click(object sender, EventArgs e)
		{
			var response = Task.Run(async () => await createOrder(Session));
			Response.Redirect(response.Result);
		}

		protected void chkboxPoints_CheckedChanged(object sender, EventArgs e)
		{
			SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

			SqlCommand cmd = new SqlCommand("SELECT RewardsAmount from Student where StudentID=@StudentID", con);
			cmd.Parameters.AddWithValue("@StudentID", Session["StudentID"].ToString());
			con.Open();
			object result = cmd.ExecuteScalar();
			double totalPrice = 0;
			if (result != null) // Check if the result (RewardsAmount) is not null
			{
				// Convert the result to an integer and assign it to lblCPEPoints.Text
				int rewardsAmount = Convert.ToInt32(result);
				
				if (chkboxPoints.Checked)
				{
					rewardsAmount = rewardsAmount / 10;
					totalPrice = Convert.ToDouble(Session["FinalPrice"]) - rewardsAmount;
					lblPointsChk.Text = "- RM " + rewardsAmount.ToString("F2");
					lblRewardsReedem.Text = "- RM " + rewardsAmount.ToString("F2");
					lblCPEPoints.Text = "0 Points";
					lblTotalCPEPrice.Text = "RM " + totalPrice.ToString("F2");
					lblTotalAmount.Text = "RM " + totalPrice.ToString("F2");
				}
				else
				{
					Session["RewardsAmount"] = 0;
					lblPointsChk.Text = null;
					lblRewardsReedem.Text = "- RM 0.00";
					lblCPEPoints.Text = rewardsAmount.ToString() + " Points";
					rewardsAmount = rewardsAmount / 10;
					totalPrice = Convert.ToDouble(Session["FinalPrice"]) + rewardsAmount;
					lblTotalCPEPrice.Text = "RM " + totalPrice.ToString("F2");
					lblTotalAmount.Text = "RM " + totalPrice.ToString("F2");
				}
				
			}
			Session["FinalPrice"] = totalPrice;
			con.Close();
		}


	}
}