<%@ Page Title="Student Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentProfileManagement.aspx.cs" Inherits="CPE_Platform.StudentProfileManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


	<ul class="nav nav-tabs" id="myTab" role="tablist">
		<li class="nav-item" role="presentation">
			<button class="nav-link active" id="profile-tab" data-bs-toggle="tab" data-bs-target="#profile" type="button" role="tab" aria-controls="profile" aria-selected="true">Profile</button>
		</li>

		<li class="nav-item" role="presentation">
			<button class="nav-link" id="rewards-tab" data-bs-toggle="tab" data-bs-target="#rewards" type="button" role="tab" aria-controls="rewards" aria-selected="false">CPE Rewards</button>
		</li>
	</ul>
	<div class="tab-content" id="myTabContent">
		<div class="tab-pane fade show active" id="profile" role="tabpanel" aria-labelledby="profile-tab">
			<asp:Button ID="btnLogOut" runat="server" Text="Log Out" OnClick="btnLogOut_Click" />

		</div>
		<div class="tab-pane fade" id="rewards" role="tabpanel" aria-labelledby="rewards-tab">...</div>
	</div>
</asp:Content>
