<%@ Page Title="Payment History" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="PaymentHistory.aspx.cs" Inherits="CPE_Platform.Private.PaymentHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>

	<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title></title>
	</head>
	<body>
	</body>
	</html>

	<section id="section">
		<div class="card-content">
			<div class="card-body">
				<div class="row">
					<div class="col-md-12 col-12">
						<table class="card table table-condensed table-responsive table-hover">
							<asp:GridView class="table table-bordered table-condensed table-responsive table-hover" ID="gvPaymentHistory" runat="server" AutoGenerateColumns="False" DataKeyNames="PaymentID"
								ShowHeaderWhenEmpty="True" AllowSorting="True">
								<%-- Theme --%>

								<Columns>

									<asp:TemplateField HeaderText="No">
										<ItemTemplate>
											<asp:Label Text='<%# Eval("PaymentID") %>' runat="server" />
										</ItemTemplate>

										<EditItemTemplate>
											<asp:TextBox ID="txtCPECode" Text='<%# Eval("PaymentID") %>' runat="server" />
										</EditItemTemplate>

									</asp:TemplateField>


									<asp:TemplateField HeaderText="Bill Reference No">
										<ItemTemplate>
											<asp:Label Text='<%# Eval("BillRefNo") %>' runat="server" />
										</ItemTemplate>

										<EditItemTemplate>
											<asp:TextBox ID="txtCPEDesc" Text='<%# Eval("BillRefNo") %>' runat="server" />
										</EditItemTemplate>

									</asp:TemplateField>




									<asp:TemplateField HeaderText="Description">
										<ItemTemplate>
											<asp:Label Text='<%# Eval("Description") %>' runat="server" />
										</ItemTemplate>

										<EditItemTemplate>
											<asp:TextBox ID="txtProgress" Text='<%# Eval("Description") %>' runat="server" />
										</EditItemTemplate>

									</asp:TemplateField>



									<asp:TemplateField HeaderText="Amount">
										<ItemTemplate>
											<asp:Label Text='<%# Eval("TotalPrice") %>' runat="server" />
										</ItemTemplate>

										<EditItemTemplate>
											<asp:TextBox ID="txtRewards" Text='<%# Eval("TotalPrice") %>' runat="server" />
										</EditItemTemplate>

									</asp:TemplateField>


									<asp:TemplateField HeaderText="Receipt Payment">
										<ItemTemplate>
											<asp:LinkButton ID="lnkDownloadInvoice" runat="server" Text="Invoice.pdf" CommandArgument='<%# Eval("BillRefNo") %>' OnClick="lnkDownloadInvoice_Click" />
										</ItemTemplate>
									</asp:TemplateField>

									<asp:TemplateField HeaderText="Payment Date">
										<ItemTemplate>
											<asp:Label Text='<%# Eval("PaymentDate", "{0:dd/MM/yyyy}") %>' runat="server" />
										</ItemTemplate>
									</asp:TemplateField>
								</Columns>
							</asp:GridView>
						</table>
						<h4><asp:Label ID="lblErrorText" runat="server"></asp:Label></h4>
					</div>
				</div>
			</div>
		</div>
	</section>
</asp:Content>
