<%@ Page Title="Staff Rewards Allocation" Language="C#" MasterPageFile="~/Staff.Master" AutoEventWireup="true" CodeBehind="StaffRewardsAllocation.aspx.cs" Inherits="CPE_Platform.Private.StaffRewardsAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPECard" runat="server">
	<!DOCTYPE html>
	<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title></title>
		<style>
		</style>
	</head>
	<body>

		<script>	
</script>
	</body>
	</html>
	<div class="container">
		<div class="row row-cols-1 row-cols-md-4 g-4">
			<asp:Repeater ID="rptrCPE" runat="server" OnItemCommand="CPESelected">
				<ItemTemplate>
					<div class="col mb-4">
						<div class="card h-100" style="width: 18rem;" runat="server">
							<img src="https://img.freepik.com/free-photo/education-day-arrangement-table-with-copy-space_23-2148721266.jpg?size=626&ext=jpg" class="card-img-top" alt="" />
							<div class="card-body">
								<h5 class="card-title" style="height: 140px;"><%# Eval("CPECourse") %></h5>
								<p class="card-text" style="height: 50px;"><%# Eval("CPEStartDate") %> - <%# Eval("CPEEndDate") %></p>
								<asp:Button runat="server" ID="btnClickHere" CssClass="btn btn-primary btn-block" CommandName="Select" CommandArgument='<%# Eval("CPECourse") %>' Text="Click Here" />
							</div>
						</div>
					</div>
				</ItemTemplate>
			</asp:Repeater>
		</div>
	</div>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DetailsContent" runat="server">
	<!DOCTYPE html>
	<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title></title>
		<style>
	</style>
	</head>
	<body>
	</body>
	</html>
	<div class="container">
		<div class="row d-flex justify-content-left mt-100">
			<div class="col-md-7 mb-2">

				<h3 style="height: 70px;">
					<asp:Label ID="lblCPECourse" CssClass="mt-3 col-md-1" runat="server"></asp:Label></h3>
				<div class="row">
					<%--<asp:Label ID="lblCourseDropdown" runat="server" Text="Course"></asp:Label>
<asp:DropDownList ID="CPECourse_DropDown" runat="server" class="form-select" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="CPECourse_DropDown_SelectedIndexChanged">
	<asp:ListItem Value="0">Select Course</asp:ListItem>
</asp:DropDownList>--%>
					<div class="col-md-4 mb-4">
						<%--<h6>
						<asp:Label ID="lblStudentList" runat="server" Text="List Of Students:"></asp:Label></h6>
					<asp:ListBox CssClass="list-group-item" Style="width: 100vh;" ID="lstStudent" runat="server" SelectionMode="Multiple" AutoPostBack="true" aria-describedby="listHelp"></asp:ListBox>
					<div id="listHelp" class="form-text" style="color:indianred">*Press Ctrl key to multiselect*</div>--%>


						<h6>
							<asp:Label ID="lblStudentList" runat="server" Text="Available Students:"></asp:Label></h6>
						<asp:ListBox class="list-group-item list-group-item-action list-group-item-light" Style="width: 250px; height: 200px;" ID="lstStudent" runat="server" SelectionMode="Multiple" aria-describedby="listHelpleft"></asp:ListBox>
						<div id="listHelpleft" class="form-text" style="color:indianred">*Press Ctrl key to multiselect*</div>
					</div>
					<!-- Controls for moving items between lists -->
					<div class="col-md-4 mb-2 d-flex align-items-center justify-content-center flex-column">
						<asp:Button ID="btnAdd" runat="server" Text="&gt;&gt;" CssClass="btn btn-outline-secondary" OnClick="btnAdd_Click" /></br>
						<asp:Button ID="btnRemove" runat="server" Text="&lt;&lt;" CssClass="btn btn-outline-secondary" OnClick="btnRemove_Click" />
					</div>

					<div class="col-md-4 mb-4">
						<!-- Right Listbox -->
						<h6>
							<asp:Label ID="lblStudentSelectedList" runat="server" Text="Selected Students:"></asp:Label></h6>
						<asp:ListBox CssClass="list-group-item list-group-item-action list-group-item-light" Style="width: 250px; height: 200px;" ID="lstSelectedStudents" runat="server" SelectionMode="Multiple" aria-describedby="listHelpright"></asp:ListBox>
						<div id="listHelpright" class="form-text" style="color:indianred">*Press Ctrl key to multiselect*</div>
					</div>
				</div>
			</div>
			<div class="row">

				<div class="col-md-6 mb-4">
					<h6>
						<asp:Label ID="lblRewards" runat="server" Text="Rewards (Points):"></asp:Label></h6>
					<asp:TextBox ID="txtRewards" class="form-control" runat="server" placeholder="Points" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
					<%--<asp:TextBox ID="txtStudentID" runat="server" placeholder="StudentID" ReadOnly="True"></asp:TextBox>--%>
					<%--<asp:TextBox ID="txtCPECode" runat="server" placeholder="CPECode" ReadOnly="True"></asp:TextBox>--%>
				</div>
				<div class="row">
					<div class="col-md-6 mb-4">
						<asp:Button ID="btnBack" CssClass="btn btn-danger" Text="Back" runat="server" OnClick="btnBack_Click" />
						<asp:Button ID="btnAssignRewards" CssClass="btn btn-success" Text="Assign" runat="server" OnClick="AssignRewards" />
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
