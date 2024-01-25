<%@ Page Title= "Login Selection" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="loginSelection.aspx.cs" Inherits="CPE_Platform.loginSelection" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<asp:Button ID="btnStaffLogin" runat="server" Text="Login As Staff" OnClick="btnStaffLogin_Click" /> <br />
	<asp:Button ID="btnStudentLogin" runat="server" Text="Login As Student" OnClick="btnStudentLogin_Click" />

</asp:Content>
