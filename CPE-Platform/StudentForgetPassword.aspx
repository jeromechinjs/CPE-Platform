<%@ Page Title= "Student Forget Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentForgetPassword.aspx.cs" Inherits="CPE_Platform.StudentForgetPassword" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div>
		<h1>TAR UMT Student Retrieve Password</h1>
	</div>
	
	<div>
		<%--<asp:Label runat="server" ID="lblStudentIDFP" Text="Student ID"></asp:Label>
		<asp:TextBox ID="txtStudentID" runat="server"></asp:TextBox><br />--%>

		<asp:Label runat="server" ID="lblStudentICFP" Text="Student IC"></asp:Label>
		<asp:TextBox ID="txtStudentIC" runat="server"></asp:TextBox>
		<asp:RequiredFieldValidator ID="RequiredFieldValidatorStudentIC" Text="*" ForeColor="Red" runat="server" ErrorMessage="IC Number is required" ControlToValidate="txtStudentIC"></asp:RequiredFieldValidator><br />

		<asp:Label runat="server" ID="lblErrorMsg"></asp:Label><br />
		<asp:Button ID="btnRetrieveStudentLogin" runat="server" Text="Back to Login" OnClick="btnRetrieveStudentLogin_Click"></asp:Button>
		<asp:Button ID="btnConfirmStudentForgetPassword" runat="server" Text="Submit" OnClick="btnConfirmStudentForgetPassword_Click"></asp:Button>
	</div>
	
</asp:Content>
