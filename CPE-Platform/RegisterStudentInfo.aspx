<%@ Page Title= "Student Registration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterStudentInfo.aspx.cs" Inherits="CPE_Platform.RegisterStudentInfo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div>
		<h1>TAR UMT Student Registration</h1>
	</div>
	
	<div>
		<asp:Label ID="lblStudentID" runat="server" Text="StudentID"></asp:Label>
		<asp:TextBox ID="txtStudentID" runat="server"></asp:TextBox><br />
		
		<asp:Label ID="lblStudentName" runat="server" Text="StudentName"></asp:Label>
		<asp:TextBox ID="txtStudentName" runat="server"></asp:TextBox><br />

		<asp:Label ID="lblStudentPhoneNum" runat="server" Text="Student Phone Number"></asp:Label>
		<asp:TextBox ID="txtStudentPhoneNum" runat="server"></asp:TextBox><br />

		<asp:Label ID="lblStudentPassword" runat="server" Text="Student Passoword"></asp:Label>
		<asp:TextBox ID="txtStudentPassword" runat="server" TextMode="Password"></asp:TextBox><br />

		<asp:Label ID="lblStudentEmail" runat="server" Text="Student Email"></asp:Label>
		<asp:TextBox ID="txtStudentEmail" runat="server"></asp:TextBox><br />

		<asp:Label ID="lblStudentFaculty" runat="server" Text="Student Faculty"></asp:Label>
		<asp:TextBox ID="txtStudentFaculty" runat="server"></asp:TextBox><br />

		<asp:Label runat="server" ID="lblErrorMsg"></asp:Label><br />
		<asp:Button ID="btnStudentRegister" runat="server" Text="Register" OnClick="btnStudentRegister_Click" />
	</div>
	
</asp:Content>

