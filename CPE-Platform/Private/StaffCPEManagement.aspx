<%@ Page Title="Staff CPE Management" Language="C#" MasterPageFile="~/Staff.Master" AutoEventWireup="true" CodeBehind="StaffCPEManagement.aspx.cs" Inherits="CPE_Platform.Private.StaffCPEManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>

	<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title></title>
		<link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
		<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
		<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

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

	<%--modal popup--%>
	<div class="container">
		<div class="modal fade" id="mymodal" data-backdrop="static" role="dialog">
			<div class=" modal-dialog modal-dailog-centered">
				<div class="modal-content">
					<div class="modal-header">
						<h4 class="modal-title">Add Record</h4>

						<button type="button" class="close" data-dismiss="modal">&times;</button>
					</div>
					<div class="modal-body">
						<asp:Label ID="lblCPECode" runat="server" Text="CPE Code"></asp:Label>
						<%--<label id ="lblCPECode">CPE Code</label>--%>
						<asp:TextBox ID="txtCPECode" CssClass="form-control" placeholder="CPE Code" runat="server" />
						
						<label>CPE Course Name</label>
						<asp:TextBox ID="txtCPEDesc" CssClass="form-control" placeholder="CPE Course Name" runat="server" />
						<label>CPE Seat Amount</label>
						<asp:TextBox ID="txtCPESeat" CssClass="form-control" placeholder="CPE Course Seat Amount" runat="server" />
						<label>CPE Price</label>
						<asp:TextBox ID="txtCPEPrice" CssClass="form-control" placeholder="Price" runat="server" />
						<%--<asp:HiddenField ID="CPECode" runat="server" />--%>
						<label>CPE Start and End Date</label>
						<asp:DropDownList ID="dllDate" CssClass="form-control" runat="server">
							<asp:ListItem Value="">Select Date</asp:ListItem>
							<asp:ListItem Value="01 Feb 2024 - 03 March 2024">01 Feb 2024 - 03 March 2024</asp:ListItem>
							<asp:ListItem Value="01 May 2024 - 01 June 2024">01 May 2024 - 01 June 2024</asp:ListItem>
						</asp:DropDownList>
						<label>Rewards Of the Course</label>
						<asp:TextBox ID="txtCPERewards" CssClass="form-control" placeholder="Points" runat="server" />
						<asp:Label ID="lblmsg" Text="" runat="server" />
					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-outline-danger" data-dismiss="modal">Close</button>
						<asp:Button ID="btnsave" CssClass="btn btn-outline-success" OnClick="btnsave_Click" Text="Save" runat="server" />
					</div>
				</div>
			</div>
		</div>
	</div>

	<%--data source--%>
	<section id="section">
		<div class="row match-height">
			<div class="col-12">
				<div class="container">
					<div class="card-header">
						<div class="row">
							<div class="col-sm">
								<asp:Button Text="Open Modal" ID="modal" CssClass="btn btn-outline-info" OnClick="modal_Click" runat="server" />
							</div>

							<div class="col-sm">
								<div class="input-group justify-content-end">
									<asp:TextBox ID="txtSearch" CssClass="form-control" placeholder="Search" runat="server" onkeypress="return handleKeyPress(event)" />
									<asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info" Text="Search" OnClick="btnSearch_Click"/><i class="fas fa-search"></i>
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
													<th>CPE Code</th>
													<th>CPE Course Name</th>
													<th>CPE Total Seat Number</th>
													<th>Price</th>
													<th>Start and End Date</th>
													<th>Reward</th>
													<th>Modified Date</th>
													<th>Edit</th>
													<th>Delete</th>
													
												</tr>
											</HeaderTemplate>
											<ItemTemplate>
												<tr class="separator">
													<td><%# Eval("CPECode") %></td>
													<td><%# Eval("CPEDesc") %></td>
													<td><%# Eval("CPESeatAmount") %></td>
													<td><%# Eval("CPEPrice") %></td>
													<td><%# Eval("CPEDate") %></td>
													<td><%# Eval("Rewards") %></td>
													<td><%# Eval("ModifiedDate") %></td>
													<td>
														<asp:LinkButton ID="btnupdate" CommandName="Update" OnCommand="btnupdate_Command" CommandArgument='<%#Eval("CPECode") %>' CssClass="btn btn-sm btn-primary" runat="server"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
													</td>
													<td>
														<asp:LinkButton CommandName="Delete" ID="btndlt" CommandArgument='<%#Eval("CPECode") %>'
															OnClientClick="return confirm('Are you sure you want to delete this !');"
															OnCommand="btndlt_Command" CssClass="btn btn-sm btn-danger" runat="server"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
													</td>
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

	<%--<asp:SqlDataSource ID="ds1"
		ConnectionString="<%$ConnectionStrings:ConnectionString %>" runat="server"
		SelectCommand="select * from CPE_Course" />--%>

</asp:Content>
