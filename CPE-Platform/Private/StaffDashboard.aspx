<%@ Page Title="" Language="C#" MasterPageFile="~/Staff.Master" AutoEventWireup="true" CodeBehind="StaffDashboard.aspx.cs" Inherits="CPE_Platform.Private.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>

	<html xmlns="http://www.w3.org/1999/xhtml">
     <head>
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
						<h4 class="modal-title">Add New Notice</h4>

						<button type="button" class="close" data-dismiss="modal">&times;</button>
					</div>
					<div class="modal-body">
						<asp:Label ID="lblmsg" Text="" ForeColor="IndianRed" runat="server" /><br />

						<asp:Label ID="lblNoticeID" runat="server" Text="Notice ID"></asp:Label>
						<asp:TextBox ID="txtNoticeID" CssClass="form-control" placeholder="Notice ID" runat="server" />

						<label>Notice Title</label>
						<asp:TextBox ID="txtNoticeTitle" CssClass="form-control" placeholder="Notice Title" runat="server" />

						<label>Notice Description</label>
						<asp:TextBox ID="txtNoticeDesc" CssClass="form-control" TextMode="MultiLine" placeholder="Notice Description" runat="server" BorderColor="#999999" BorderStyle="Dotted" height="500" EnableTheming="True"/>
						
						<label>Notice Sender</label>
						<asp:TextBox ID="txtNoticeSender" CssClass="form-control" placeholder="Notice Sender" runat="server" />
						
						<asp:Label ID="lblNoticeDate" runat="server" Text="Notice Date"></asp:Label>
						<asp:TextBox ID="txtNoticeDate" CssClass="form-control" placeholder="Notice Date" runat="server" />

					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-outline-danger" data-dismiss="modal">Close</button>
						<asp:Button ID="btnsave" CssClass="btn btn-outline-success" OnClick="btnsave_Click" Text="Save" runat="server" />
					</div>
				</div>
			</div>
		</div>
	</div>

	<!-- Notice added sucessfully toast message -->
	<div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
		<asp:Panel ID="noticeAdded" CssClass="toast hide" role="alert" aria-live="assertive" aria-atomic="true" runat="server">
			<div class="toast-header">
				<img src="src/tarucLogo.png" class="rounded me-2" width="12">
				<strong class="me-auto">CPE Platform</strong>
				<small>Just now</small>
				<button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
			</div>
			<div class="toast-body">
				New notice added successfully.
			</div>
		</asp:Panel>
	</div>

	<!-- Notice updated sucessfully toast message -->
	<div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
		<asp:Panel ID="noticeUpdated" CssClass="toast hide" role="alert" aria-live="assertive" aria-atomic="true" runat="server">
			<div class="toast-header">
				<img src="src/tarucLogo.png" class="rounded me-2" width="12">
				<strong class="me-auto">CPE Platform</strong>
				<small>Just now</small>
				<button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
			</div>
			<div class="toast-body">
				Notice updated successfully.
			</div>
		</asp:Panel>
	</div>

	<%--data source--%>
	<section id="section">
		<div class="row match-height">
			<div class="col-12">
				<div class="container">
					<div class="card-header">
						<div class="row">
							<div class="col-sm">
								<asp:Button Text="Add New Notice" ID="modal" CssClass="btn btn-outline-info" OnClick="modal_Click" runat="server" />
							</div>

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
									<table id="noticeTable" class="card table table-condensed table-responsive table-hover">
										<asp:Repeater ID="rptr1" runat="server">
											<HeaderTemplate>
												<tr class="w-100">
													<th>Notice ID</th>
													<th>Notice Title</th>
													<th>Notice Sender</th>
													<th>Notice Date</th>
													<th>Edit</th>
													<th>Delete</th>
												</tr>
											</HeaderTemplate>
											<ItemTemplate>
												<tr class="separator">
													<td><%# Eval("NoticeID") %></td>
													<td><%# Eval("NoticeTitle") %></td>
													<td><%# Eval("NoticeSender") %></td>
													<td><%# Eval("NoticeDate") %></td>
													<td>
														<asp:LinkButton ID="btnupdate" CommandName="Update" OnCommand="btnupdate_Command" CommandArgument='<%#Eval("NoticeID") %>' runat="server"><span class="fa fa-edit"></span></asp:LinkButton>
													</td>
													<td>
														<asp:LinkButton CommandName="Delete" ID="btndlt" CommandArgument='<%#Eval("NoticeID") %>'
															OnClientClick="return confirm('Are you sure you want to delete this !');"
															OnCommand="btndlt_Command" runat="server"><i class="fa fa-trash text-danger"></i></asp:LinkButton>
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
</asp:Content>
