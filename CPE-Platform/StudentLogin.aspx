<%@ Page Title= "Student Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentLogin.aspx.cs" Inherits="CPE_Platform.StudentLogin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div>
		<h1>TAR UMT Student Intranet</h1>
	</div>
	
	<div>
		<asp:TextBox ID="txtStudentID" runat="server"></asp:TextBox><br />
		<asp:TextBox ID="txtStudentPassword" runat="server" TextMode="Password"></asp:TextBox><br />
		<asp:Label runat="server" ID="lblErrorMsg"></asp:Label><br />
		<asp:Button ID="btnStudentLogin" runat="server" Text="Login" OnClick="btnStudentLogin_Click"></asp:Button>
		<asp:Button ID="btnStudentForgetPasswordLogin" runat="server" Text="Forget Password" OnClick="btnStudentForgetPasswordLogin_Click"></asp:Button>
	</div>
	
</asp:Content>
