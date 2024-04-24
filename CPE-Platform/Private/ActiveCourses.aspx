<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="ActiveCourses.aspx.cs" Inherits="CPE_Platform.Private.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	 <head>
		<link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
		<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
		<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
	</head>



	<div class="container p-3">
		<table id="noticeTable" class="card table table-responsive table-hover">
			<asp:Repeater ID="activeCourses" runat="server">
				<HeaderTemplate>
					<tr>
						<th>Record ID</th>
						<th>CPE Code</th>
						<th>Course Name</th>
						<th>Start Date</th>
						<th>End Date</th>
					</tr>
				</HeaderTemplate>
				<ItemTemplate>
					<tr class="separator">
						<td><%# Eval("RegistrationID") %></td>
						<td><%# Eval("CPECode") %></td>
						<td><%# Eval("CPEName") %></td>
						<td><%# Eval("CPEStartDate") %></td>
						<td><%# Eval("CPEEndDate") %></td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</table>
	</div>

</asp:Content>
