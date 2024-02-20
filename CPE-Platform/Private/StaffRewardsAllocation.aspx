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
							<img src="https://cdn.vox-cdn.com/thumbor/1wMWCITHsPv5xhwGd3Mwghsi3tE=/11x17:1898x1056/320x213/filters:focal(807x387:1113x693):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/72921759/vlcsnap_2023_12_01_10h37m31s394.0.jpg" class="card-img-top" alt="" />
							<div class="card-body">
								<h5 class="card-title" style="height: 80px;"><%# Eval("CPECourse") %></h5>
								<p class="card-text"><%# Eval("CPEDate") %></p>
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
		<asp:Label ID="lblCPECourse" runat="server"></asp:Label>

	<%--<asp:Label ID="lblCourseDropdown" runat="server" Text="Course"></asp:Label>
<asp:DropDownList ID="CPECourse_DropDown" runat="server" class="form-select" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="CPECourse_DropDown_SelectedIndexChanged">
	<asp:ListItem Value="0">Select Course</asp:ListItem>
</asp:DropDownList>--%>
	<asp:Label ID="lblStudentList" runat="server" Text="Students"></asp:Label>
	<asp:ListBox ID="lstStudent" runat="server" SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
	<asp:Label ID="lblRewards" runat="server" Text="Rewards (Points)"></asp:Label>
	<asp:TextBox ID="txtRewards" runat="server" placeholder="Points" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
	<%--<asp:TextBox ID="txtStudentID" runat="server" placeholder="StudentID" ReadOnly="True"></asp:TextBox>--%>
	<%--<asp:TextBox ID="txtCPECode" runat="server" placeholder="CPECode" ReadOnly="True"></asp:TextBox>--%>
	<asp:Button ID="btnAssignRewards" Text="Assign" runat="server" OnClick="AssignRewards" />
</asp:Content>
