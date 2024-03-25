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
						<asp:Label ID="lblmsg" Text="" ForeColor="IndianRed" runat="server" /><br />
						<asp:Label ID="lblCPECode" runat="server" Text="CPE Code"></asp:Label>
						<%--<label id ="lblCPECode">CPE Code</label>--%>
						<asp:TextBox ID="txtCPECode" CssClass="form-control" placeholder="CPE Code" runat="server" />

						<label>CPE Course Name</label>
						<asp:TextBox ID="txtCPEName" CssClass="form-control" placeholder="CPE Course Name" runat="server" />
						<label>CPE Course Description</label>
						<asp:TextBox ID="txtCourseDesc" CssClass="form-control" placeholder="CPE Course Description" TextMode="MultiLine" BorderColor="#999999" BorderStyle="Dotted" Columns="1" EnableTheming="True" runat="server" />
						
						<label>CPE Type</label>
						<asp:DropDownList ID="DropDownListType" CssClass="form-control" runat="server">
							<asp:ListItem Value="">Select Type</asp:ListItem>
							<asp:ListItem Value="Professional Programmes">Professional Programmes</asp:ListItem>
							<asp:ListItem Value="Corporate Programmes">Corporate Programmes</asp:ListItem>
							<asp:ListItem Value="Micro-Credential">Micro-Credential</asp:ListItem>
						</asp:DropDownList>

						<label>CPE Venue</label>
						<asp:TextBox ID="txtVenue" CssClass="form-control" placeholder="CPE Venue" runat="server" />
						<label>CPE Seat Amount</label>
						<asp:TextBox ID="txtCPESeat" CssClass="form-control" placeholder="CPE Course Seat Amount" runat="server" />
						<label>CPE Price</label>
						<asp:TextBox ID="txtCPEPrice" CssClass="form-control" placeholder="Price" runat="server" />
						<label>CPE Trainer</label>
						<asp:TextBox ID="txtTrainer" CssClass="form-control" placeholder="CPE Trainer" runat="server" />

						<%--<asp:HiddenField ID="CPECode" runat="server" />--%>
						<label>CPE Upcoming Start Date</label>
						<asp:DropDownList ID="dllStartDate" CssClass="form-control" runat="server">
							<asp:ListItem Value="">Select Date</asp:ListItem>
							<asp:ListItem Value="23 February 2024">23 February 2024</asp:ListItem>
							<asp:ListItem Value="11 March 2024">11 March 2024</asp:ListItem>
							<asp:ListItem Value="16 March 2024">16 March 2024</asp:ListItem>
							<asp:ListItem Value="12 April 2024">12 April 2024</asp:ListItem>
							<asp:ListItem Value="01 May 2024">01 May 2024</asp:ListItem>
							<asp:ListItem Value="02 May 2024">02 May 2024</asp:ListItem>
							<asp:ListItem Value="15 May 2024">15 May 2024</asp:ListItem>
							<asp:ListItem Value="07 June 2024">07 June 2024</asp:ListItem>
							<asp:ListItem Value="13 June 2024">13 June 2024</asp:ListItem>
							<asp:ListItem Value="16 June 2024">16 June 2024</asp:ListItem>
							<asp:ListItem Value="27 July 2024">27 July 2024</asp:ListItem>
							<asp:ListItem Value="05 August 2024">05 August 2024</asp:ListItem>
							<asp:ListItem Value="10 September 2024">10 September 2024</asp:ListItem>
							<asp:ListItem Value="04 November 2024">04 November 2024</asp:ListItem>
							<asp:ListItem Value="11 November 2024">11 November 2024</asp:ListItem>
							<asp:ListItem Value="02 December 2024">02 December 2024</asp:ListItem>
							<asp:ListItem Value="06 December 2024">06 December 2024</asp:ListItem>
							<asp:ListItem Value="15 December 2024">15 December 2024</asp:ListItem>
							
							
						</asp:DropDownList>
						<label>CPE Upcoming End Date</label>
						<asp:DropDownList ID="dllEndDate" CssClass="form-control" runat="server">
							<asp:ListItem Value="">Select Date</asp:ListItem>
							<asp:ListItem Value="03 February 2024">03 February 2024</asp:ListItem>
							<asp:ListItem Value="24 February 2024">24 February 2024</asp:ListItem>
							<asp:ListItem Value="03 March 2024">03 March 2024</asp:ListItem>
							<asp:ListItem Value="26 March 2024">26 March 2024</asp:ListItem>
							<asp:ListItem Value="13 April 2024">13 April 2024</asp:ListItem>
							<asp:ListItem Value="02 May 2024">02 May 2024</asp:ListItem>
							<asp:ListItem Value="19 May 2024">19 May 2024</asp:ListItem>
							<asp:ListItem Value="08 June 2024">08 June 2024</asp:ListItem>
							<asp:ListItem Value="15 June 2024">15 June 2024</asp:ListItem>
							<asp:ListItem Value="25 June 2024">25 June 2024</asp:ListItem>
							<asp:ListItem Value="03 July 2024">03 July 2024</asp:ListItem>
							<asp:ListItem Value="28 July 2024">28 July 2024</asp:ListItem>
							<asp:ListItem Value="10 August 2024">10 August 2024</asp:ListItem>
							<asp:ListItem Value="14 October 2024">14 October 2024</asp:ListItem>
							<asp:ListItem Value="11 November 2024">11 November 2024</asp:ListItem>
							<asp:ListItem Value="20 November 2024">20 November 2024</asp:ListItem>
							<asp:ListItem Value="26 November 2024">26 November 2024</asp:ListItem>
							<asp:ListItem Value="07 December 2024">07 December 2024</asp:ListItem>
							<asp:ListItem Value="15 December 2024">15 December 2024</asp:ListItem>
							
						</asp:DropDownList>

						<label>Start Time</label>
						<asp:TextBox ID="txtStartTime" CssClass="form-control" placeholder="Start Time" runat="server" />
						<label>End Time</label>
						<asp:TextBox ID="txtEndTime" CssClass="form-control" placeholder="End Time" runat="server" />

						<label>CPE Contact</label>
						<asp:DropDownList ID="DropDownListContact" CssClass="form-control" runat="server">
							<asp:ListItem Value="">Select Contact</asp:ListItem>
							<asp:ListItem Value="03-4145 0170">03-4145 0170</asp:ListItem>
							<asp:ListItem Value="03-4145 0123">03-4145 0123</asp:ListItem>
						</asp:DropDownList>

						<label>CPE Email</label>
						<asp:DropDownList ID="DropDownListEmail" CssClass="form-control" runat="server">
							<asp:ListItem Value="">Select Email</asp:ListItem>
							<asp:ListItem Value="training@tarc.edu.my">training@tarc.edu.my</asp:ListItem>
						</asp:DropDownList>

						<label>Rewards Of the Course</label>
						<asp:TextBox ID="txtCPERewards" CssClass="form-control" placeholder="Points" runat="server" />
						
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
								<asp:Button Text="Add Record" ID="modal" CssClass="btn btn-outline-info" OnClick="modal_Click" runat="server" />
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
									<table class="card table table-condensed table-responsive table-hover">
										<asp:Repeater ID="rptr1" runat="server">
											<HeaderTemplate>
												<tr>
													<th>CPE Code</th>
													<th>CPE Course Name</th>
													<th>CPE Total Seat Number</th>
													<th>Price</th>
													<th>Start Date</th>
													<th>End Date</th>
													<th>Reward</th>
													<th>Modified Date</th>
													<th>Edit</th>
													<th>Delete</th>

												</tr>
											</HeaderTemplate>
											<ItemTemplate>
												<tr class="separator">
													<td><%# Eval("CPECode") %></td>
													<td><%# Eval("CPEName") %></td>
													<td><%# Eval("CPESeatAmount") %></td>
													<td><%# Eval("CPEPrice") %></td>
													<td><%# Eval("CPEStartDate") %></td>
													<td><%# Eval("CPEEndDate") %></td>
													<td><%# Eval("Rewards") %></td>
													<td><%# Eval("ModifiedDate") %></td>
													<td>
														<asp:LinkButton ID="btnupdate" CommandName="Update" OnCommand="btnupdate_Command" CommandArgument='<%#Eval("CPECode") %>' runat="server"><span class="fa fa-edit"></span></asp:LinkButton>
													</td>
													<td>
														<asp:LinkButton CommandName="Delete" ID="btndlt" CommandArgument='<%#Eval("CPECode") %>'
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
