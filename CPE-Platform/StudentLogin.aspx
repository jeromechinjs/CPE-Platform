<%@ Page Title= "Student Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentLogin.aspx.cs" Inherits="CPE_Platform.StudentLogin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div>
		<h1>TAR UMT Student Intranet</h1>
	</div>
	
	<div>
		<asp:TextBox ID="StudentID" runat="server"></asp:TextBox><br />
		<asp:TextBox ID="StudentPassword" runat="server"></asp:TextBox><br />
		<asp:Button ID="btnStudentLogin" runat="server" Text="Login" />
	</div>
	
</asp:Content>
