<%@ Page Title= "Staff Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StaffLogin.aspx.cs" Inherits="CPE_Platform.StaffLogin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div>
		<h1>TAR UMT Staff Intranet</h1>
	</div>
	
	<div>
		<asp:TextBox ID="txtStaffID" runat="server"></asp:TextBox><br />
		<asp:TextBox ID="txtStaffPassword" runat="server" TextMode="Password"></asp:TextBox>
		<br />
		<asp:Label runat="server" ID="lblErrorMsg"></asp:Label><br />
		<asp:Button ID="btnStaffLogin" runat="server" Text="Login" OnClick="btnStaffLogin_Click" />
		<asp:Button ID="btnStaffForgetPasswordLogin" runat="server" Text="Forget Password" OnClick="btnStaffForgetPasswordLogin_Click"></asp:Button>
	</div>
	
</asp:Content>
