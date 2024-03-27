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
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Org.BouncyCastle.Utilities.Collections;
using static iTextSharp.text.pdf.AcroFields;
using System.Drawing;
using Org.BouncyCastle.Asn1.X509;
using Font = iTextSharp.text.Font;



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



				}
			}
			string approvalToken = Request.QueryString["token"];
			if (Request.QueryString != null && Request.QueryString.Count != 0)
			{
				HandlePaymentProcess(approvalToken);

			}
		}
		private void HandlePaymentProcess(string approvalToken)
		{
			try
			{
				var response = Task.Run(async () => await captureOrder(approvalToken)).Result;

				if (response != null)
				{
					Order result = response.Result<Order>();
					if (result.Status.ToLower() == "completed")
					{
						double totalPrice = Convert.ToDouble(Session["FinalPrice"]);

						// Generate PDF invoice
						try
						{
							byte[] pdfInvoice = GeneratePDFInvoice(result);
							// Store Payment and invoice as pdf into database
							StorePaymentInDatabase(pdfInvoice, result);
							UpdateStudentInDatabase();
							UpdateCPECourseSeats();
							StoreIntoCPERegistration(result);
							Session["Cart"] = null;
							Session["numOfItems"] = 0;

                            // Show a pop-up message
                            string script = "alert('Payment successful! You will be redirected to the Courses page.');";
							ScriptManager.RegisterStartupScript(this, GetType(), "Alert", script, true);

							// Redirect to Courses.aspx after a short delay
							string redirectScript = "setTimeout(function() { window.location.href = '../Private/PaymentHistory.aspx'; }, 10);"; // Redirect after 2 seconds
							ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", redirectScript, true);

						}
						catch (Exception ex)
						{
							// Handle the exception
							Console.WriteLine("Error generating PDF invoice: " + ex.Message);
							// Log the exception for further investigation
						}

					}
					else
					{
						string script = "alert('Payment process was not completed successfully.');";
						ScriptManager.RegisterStartupScript(this, GetType(), "Alert", script, true);
					}
				}
				else
				{
					// Response is null, indicating cancellation or error during payment process
					Response.Redirect("../Private/billing.aspx");
					string script = "alert('Payment process was canceled or encountered an error.');";
					ScriptManager.RegisterStartupScript(this, GetType(), "Alert", script, true);
				}
			}
			catch (PayPal.HttpException ex)
			{
				// Handle PayPal API exceptions
				string script = "alert('An error occurred while processing your payment. Please try again later.');";
				ScriptManager.RegisterStartupScript(this, GetType(), "Alert", script, true);
				// Log the exception for further investigation
				Console.WriteLine("PayPal API Exception: " + ex.ToString());
			}
			catch (Exception ex)
			{
				// Handle other exceptions
				string script = "alert('An unexpected error occurred. Please try again later.');";
				ScriptManager.RegisterStartupScript(this, GetType(), "Alert", script, true);
				// Log the exception for further investigation
				Console.WriteLine("Exception: " + ex.ToString());
			}
		}
		// insert payment details into database
		public void StorePaymentInDatabase(byte[] pdfContent, Order order)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			string query = "INSERT INTO Payment (BillRefNo, Description, TotalPrice, Invoice, PaymentDate) VALUES (@BillRefNo, @Description, @TotalPrice, @Invoice, @PaymentDate)";
			double totalPrice = Convert.ToDouble(Session["FinalPrice"]);
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();


					using (SqlCommand command = new SqlCommand(query, connection))
					{
						DateTime currentDate = DateTime.Now.Date;
						command.Parameters.AddWithValue("@BillRefNo", order.Id);
						command.Parameters.AddWithValue("@Description", "CPE Course Bill");
						command.Parameters.AddWithValue("@TotalPrice", totalPrice);
						command.Parameters.AddWithValue("@Invoice", pdfContent);
						command.Parameters.AddWithValue("@PaymentDate", currentDate);
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				// Log the exception for further investigation
				Console.WriteLine("Error storing payment in database: " + ex.Message);
				Console.WriteLine(ex.StackTrace);
			}
		}

		// update Student Rewards used and total amount of rewards
		public void UpdateStudentInDatabase()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			string query = "UPDATE Student SET RewardsAmount=@RewardsAmount, RewardsUsed = @RewardsUsed where StudentID = @StudentID";
			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();


					using (SqlCommand command = new SqlCommand(query, connection))
					{
						DateTime currentDate = DateTime.Now.Date;
						command.Parameters.AddWithValue("RewardsAmount", Session["RewardsAmount"]);
						command.Parameters.AddWithValue("@RewardsUsed", Session["RewardsUsed"]);
						command.Parameters.AddWithValue("@StudentID", Session["StudentID"].ToString());
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				// Log the exception for further investigation
				Console.WriteLine("Error update Student details in database: " + ex.Message);
				Console.WriteLine(ex.StackTrace);
			}
		}

		// update CPE Course seats number (tbc)
		public void UpdateCPECourseSeats()
		{
			if (Session["Cart"] != null)
			{
				foreach (string item in (ArrayList)Session["Cart"])
				{
					int SeatNum = 0;
					SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
					string query = "UPDATE CPE_Course SET CPESeatAmount = @CPESeat where CPECode= '" + item + "'";
					SqlCommand cmd2 = new SqlCommand("SELECT CPESeatAmount from CPE_Course where CPECode= '" + item + "'", con);
					con.Open();
					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						using (SqlDataReader reader = cmd2.ExecuteReader())
						{
							while (reader.Read())
							{
								// calculate the total amount of all the CPE Course
								SeatNum = Convert.ToInt32(reader["CPESeatAmount"]);
								SeatNum -= 1;

							}
							Session["CPESeats"] = SeatNum;
						}
						cmd.Parameters.AddWithValue("@CPESeat", Session["CPESeats"]);
						cmd.ExecuteNonQuery();
					}
					con.Close();
				}
			}
		}

		//insert CPE_Registration info into database
		public void StoreIntoCPERegistration(Order order)
		{
			if (Session["Cart"] != null)
			{
				foreach (string item in (ArrayList)Session["Cart"])
				{
					SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
					string query = "INSERT INTO CPE_Registration(RegistrationDateTime, CPECode, StudentID, BillRefNo) VALUES(@RegistrationDateTime, @CPECode, @StudentID, @BillRefNo)";
					con.Open();
					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						DateTime currentDateTime = DateTime.Now;
						cmd.Parameters.AddWithValue("@RegistrationDateTime", currentDateTime);
						cmd.Parameters.AddWithValue("@CPECode", item);
						cmd.Parameters.AddWithValue("@StudentID", Session["StudentID"]);
						cmd.Parameters.AddWithValue("@BillRefNo", order.Id);
							
						cmd.ExecuteNonQuery();
					}
					con.Close();
				}
			}
		}

		// function to generate pdf for invoice
		public byte[] GeneratePDFInvoice(Order order)
		{
			// Get the total amount from the order
			double totalPrice = Convert.ToDouble(Session["FinalPrice"]);

			// Create a new PDF document
			MemoryStream msOutput = new MemoryStream();
			Document document = new Document();
			PdfWriter writer = PdfWriter.GetInstance(document, msOutput);
			document.Open();

			// Add content to the PDF document
			// Invoice Title
			Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
			Paragraph title = new Paragraph("Invoice", titleFont);
			title.SpacingAfter = 20f;  // Set the spacing after the paragraph
			title.Alignment = Element.ALIGN_CENTER;
			document.Add(title);

			// Order ID
			
			Font orderFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
			Paragraph orderID = new Paragraph($"Bill Ref No: {order.Id}", orderFont);
			orderID.SpacingBefore = 20f; // Set the spacing before the paragraph
			orderID.SpacingAfter = 20f;  // Set the spacing after the paragraph
			document.Add(orderID);

			// Date
			
			Font paymentFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
			Paragraph paymentDate = new Paragraph($"Payment Date: {DateTime.Now.ToString("MMMM dd, yyyy")}", paymentFont);
			paymentDate.SpacingBefore = 20f; // Set the spacing before the paragraph
			paymentDate.SpacingAfter = 20f;  // Set the spacing after the paragraph
			document.Add(paymentDate);

			// Student ID
			
			Paragraph studentID = new Paragraph($"Student ID: {Session["StudentID"]}", orderFont);
			studentID.SpacingBefore = 20f; // Set the spacing before the paragraph
			studentID.SpacingAfter = 20f;  // Set the spacing after the paragraph
			document.Add(studentID);

			// CPECourse

			Paragraph CPECourse = new Paragraph($"Payment Description: CPE Course Bill", orderFont);
			CPECourse.SpacingBefore = 20f; // Set the spacing before the paragraph
			CPECourse.SpacingAfter = 20f;  // Set the spacing after the paragraph
			document.Add(CPECourse);

			// Status

			Paragraph status = new Paragraph($"Payment Status: {order.Status}", orderFont);
			status.SpacingBefore = 20f; // Set the spacing before the paragraph
			status.SpacingAfter = 20f;  // Set the spacing after the paragraph
			document.Add(status);

			// Total Amount
			
			Paragraph totalAmount = new Paragraph($"Total Amount: RM {totalPrice:F2}", orderFont);
			totalAmount.SpacingBefore = 20f; // Set the spacing before the paragraph
			totalAmount.SpacingAfter = 20f;  // Set the spacing after the paragraph
			document.Add(totalAmount);

			// Add more details as needed

			document.Close();

			// Return the PDF content as a byte array
			return msOutput.ToArray();
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
			if (lblTotalCPEPrice.Text == "Total Price")
			{
				string script = "alert('The current Cart is empty');";
				ScriptManager.RegisterStartupScript(this, GetType(), "Alert", script, true);
				string redirectScript = "setTimeout(function() { window.location.href = '../Private/Courses.aspx'; }, 10);"; // Redirect after 2 seconds
				ScriptManager.RegisterStartupScript(this, GetType(), "Redirect", redirectScript, true);
			}
			else
			{
				var response = Task.Run(async () => await createOrder(Session));
				Response.Redirect(response.Result);
			}

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
					Session["RewardsAmount"] = 0;
					Session["RewardsUsed"] = rewardsAmount;
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
					Session["RewardsAmount"] = rewardsAmount;
					Session["RewardsUsed"] = 0;
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