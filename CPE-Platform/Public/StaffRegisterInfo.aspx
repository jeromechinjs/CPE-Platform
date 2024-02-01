<%@ Page Title= "Staff Registration" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="StaffRegisterInfo.aspx.cs" Inherits="CPE_Platform.StaffRegisterInfo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div>
		<h1>TAR UMT Student Registration</h1>
	</div>
	
	<div>
		<asp:Label ID="lblStaffID" runat="server" Text="StaffID"></asp:Label>
		<asp:TextBox ID="txtStaffID" runat="server"></asp:TextBox><br />
		
		<asp:Label ID="lblStaffName" runat="server" Text="StaffName"></asp:Label>
		<asp:TextBox ID="txtStaffName" runat="server"></asp:TextBox><br />

		<asp:Label ID="lblStaffPhoneNum" runat="server" Text="Staff Phone Number"></asp:Label>
		<asp:TextBox ID="txtStaffPhoneNum" runat="server"></asp:TextBox><br />

		<asp:Label ID="lblStaffPassword" runat="server" Text="Staff Passoword"></asp:Label>
		<asp:TextBox ID="txtStaffPassword" runat="server" TextMode="Password"></asp:TextBox><br />

		<asp:Label ID="lblStaffEmail" runat="server" Text="Staff Email"></asp:Label>
		<asp:TextBox ID="txtStaffEmail" runat="server"></asp:TextBox><br />

		<asp:Label ID="lblStaffPosition" runat="server" Text="Staff Position"></asp:Label>
		<asp:TextBox ID="txtStaffPosition" runat="server"></asp:TextBox><br />

		<asp:Label runat="server" ID="lblErrorMsg"></asp:Label><br />
		<asp:Button ID="btnStaffRegister" runat="server" Text="Register" OnClick="btnStaffRegister_Click" />
	</div>
	
</asp:Content>
