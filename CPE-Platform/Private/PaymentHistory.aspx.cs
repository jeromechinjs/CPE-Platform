using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CPE_Platform.Private
{
	public partial class PaymentHistory : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Session["StudentID"] != null)
				{
					string studentID = Session["StudentID"].ToString();

					SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

					string query = "SELECT DISTINCT PaymentID, P.BillRefNo, Description, TotalPrice, Invoice, PaymentDate FROM Payment P, CPE_Registration R WHERE R.StudentID =@StudentID AND P.BillRefNo = R.BillRefNo";

					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@StudentID", studentID);
						con.Open();
						SqlDataReader reader = cmd.ExecuteReader();

						// bind data to grid view
						DataTable table = new DataTable();
						table.Load(reader);
						gvPaymentHistory.DataSource = table;
						if (table.Rows.Count > 0)
						{
							gvPaymentHistory.DataBind();
							//close reader
							reader.Close();
							con.Close();
						}
						else
						{
							lblErrorText.Text = "No Records Is Found";
						}
						

					}
				}
			}
		}

		protected void lnkDownloadInvoice_Click(object sender, EventArgs e)
		{
			LinkButton lnkDownloadInvoice = (LinkButton)sender;
			string billRefNo = lnkDownloadInvoice.CommandArgument;

			// Retrieve the invoice binary data from the database
			byte[] invoiceData = GetInvoiceData(billRefNo);

			if (invoiceData != null)
			{
				// Create a PDF document from the byte[] data
				using (MemoryStream memoryStream = new MemoryStream(invoiceData))
				{
					// Set the appropriate headers for file download
					Response.ContentType = "application/pdf";
					Response.AddHeader("Content-Disposition", $"inline; filename=invoice_{billRefNo}.pdf");

					// Create a PDF reader
					PdfReader pdfReader = new PdfReader(memoryStream);

					// Create a PDF document
					Document document = new Document(pdfReader.GetPageSizeWithRotation(1));

					// Create a PDF writer
					PdfWriter pdfWriter = PdfWriter.GetInstance(document, Response.OutputStream);

					// Open the document
					document.Open();

					// Add each page of the PDF to the new document
					for (int i = 1; i <= pdfReader.NumberOfPages; i++)
					{
						document.NewPage();
						PdfImportedPage page = pdfWriter.GetImportedPage(pdfReader, i);
						pdfWriter.DirectContent.AddTemplate(page, 0, 0);
					}

					// Close the document
					document.Close();

					// End the response (this is important)
					Response.End();
				}
			}
			else
			{
				string script = "alert('The invoice data is empty.');";
				ScriptManager.RegisterStartupScript(this, GetType(), "Alert", script, true);
			}
		}

		private byte[] GetInvoiceData(string billRefNo)
		{
			string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			string query = "SELECT Invoice FROM Payment WHERE BillRefNo = @BillRefNo";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@BillRefNo", billRefNo);

					object result = command.ExecuteScalar();
					if (result != null && result != DBNull.Value)
					{
						return (byte[])result;
					}
				}
			}

			return null;
		}
	}
}