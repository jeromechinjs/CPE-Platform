<%@ Page Title= "Student Reset Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentResetPassword.aspx.cs" Inherits="CPE_Platform.StudentResetPassword" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div>
		<h1>TAR UMT Student Reset Password</h1>
	</div>
	
	<div>
		<asp:Label runat="server" ID="lblNewPassword" Text="New Password"></asp:Label><br />
		<asp:TextBox ID="txtNewPassword" runat="server"></asp:TextBox><br />

		<asp:Label runat="server" ID="lblConfirmPassword" Text="Confirm Password"></asp:Label><br />
		<asp:TextBox ID="txtConfirmPassword" runat="server"></asp:TextBox><br />

		<asp:Label runat="server" ID="lblErrorMsg"></asp:Label><br />
		<asp:Button ID="btnRetrieveStudentLogin" runat="server" Text="Back to Login" OnClick="btnRetrieveStudentLogin_Click"></asp:Button>
		<asp:Button ID="btnConfirmStudentForgetPassword" runat="server" Text="Submit"></asp:Button>
	</div>
	
</asp:Content>