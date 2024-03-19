<%@ Page Page Title="Payment History" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="PaymentHistory.aspx.cs" Inherits="CPE_Platform.Private.PaymentHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
 <script type="text/javascript">
	function handleKeyPress(e) {
		// Check if the key pressed is "Enter"
		if (e.keyCode === 13) {
			// Prevent default behavior (form submission)
			e.preventDefault();
			// Trigger the click event of the search button
			document.getElementById('<%= btnSearch.ClientID %>').click();
			return false;
		}
		return true;
	}
 </script>
</body>
</html>

    <section id="section">
	<div class="row match-height">
		<div class="col-12">
			<div class="container">
				<div class="card-header">
					<div class="row">

						<div class="col-sm">
							<div class="input-group justify-content-end">
								<asp:TextBox ID="txtSearch" CssClass="form-control" placeholder="&#xF002; Search" Style="font-family: FontAwesome" runat="server" onkeypress="return handleKeyPress(event)" />
								<asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info" Text="Search" OnClick="btnSearch_Click" />

							</div>
						</div>
					</div>
				</div>
				<div class="card-content">
					<div class="card-body">
						<div class="row">
							<div class="col-md-12 col-12">
								<table class="card table table-condensed table-responsive table-hover">
									<asp:Repeater ID="rptr1" runat="server">
										<HeaderTemplate>
											<tr>
												<th>No</th>
												<th>Description </th>
												<th>Amount</th>
												<th>Receipt Payment</th>
												<th>Payment Date</th>
											</tr>
										</HeaderTemplate>
										<ItemTemplate>
											<tr class="separator">
												<td><%# Eval("PaymentID") %></td>
												<td><%# Eval("Description") %></td>
												<td><%# Eval("TotalPrice") %></td>
												<td><%# Eval("Invoice") %></td>
												<td><%# Eval("PaymentDate") %></td>
												
											</tr>
										</ItemTemplate>
									</asp:Repeater>
								</table>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</section>
</asp:Content>
