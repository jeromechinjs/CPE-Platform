<%@ Page Title="Login Selection" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="loginSelection.aspx.cs" Inherits="CPE_Platform.loginSelection" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>
<html>
<head>
	<link rel="stylesheet" href="css/style.css">
	<style>
		.px-6 {
			padding-right: 4rem;
			padding-left: 4rem;
		}

	</style>
</head>
</html>

	<section class="vh-50 gradient-custom">
		<div class="container py-5 h-100">
			<div class="row d-flex justify-content-center align-items-center h-50">
				<div class="col-12 col-md-8 col-lg-6 col-xl-5">
					<div class="card bg-dark text-white" style="border-radius: 1rem;">
						<div class="card-body p-5 text-center">

							<div class="mb-md-5 mt-md-4 pb-5">
								<h1 class="fw-bold mb-2 text-uppercase">TAR UMT Login Method</h1>
								<p class="text-white-50 mb-5">Please Select your Login Method</p>

								<div class="mb-md-2 mt-md-2 pb-2">
									<asp:Button ID="btnStaffLogin" class="btn btn-outline-light btn-lg px-6" runat="server" Text="Login As Staff" OnClick="btnStaffLogin_Click" />
									<br />
								</div>
								<div class="mb-md-4 mt-md-4">
									<asp:Button ID="btnStudentLogin" class="btn btn-outline-light btn-lg px-5" runat="server" Text="Login As Student" OnClick="btnStudentLogin_Click" />
								</div>
								

							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>

</asp:Content>
