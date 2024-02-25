<%@ Page Title="Login Selection" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="loginSelection.aspx.cs" Inherits="CPE_Platform.loginSelection" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>
	<html>
	<head>
		<link rel="stylesheet" href="css/style.css">
		<link rel="stylesheet" href="font-awesome/css/font-awesome.min.css">
		<style>
			.px-6 {
				padding-right: 4rem;
				padding-left: 4rem;
			}

			.mx-5 {
				margin-right: 3rem !important;
				margin-left: 6rem !important;
			}

			.font-weight-light {
				font-weight: lighter;
			}

			.background-img {
				background-image: url('/Resources/empty-blackboard.jpg');
				background-size: cover;
				background-position: center;
				width: 100%;
				height: 100vh;
			}
		</style>
	</head>
	</html>

	<section class="background-img">
		<div class="container py-3 ">
			<div class="row d-flex justify-content-center align-items-center h-50">
				<div class="col-12 col-md-8 col-lg-6 col-xl-4">
					<img class="img-fluid p-2 mx-5" style="max-width: 50%;" alt="TARUMTLogo" src="Resources/tarumt-logo.png" />
					<h2 class="text-lg-center mb-3 mx-5-custom" style="color:antiquewhite;"><span style="color: palevioletred">TAR </span><span style="color: cornflowerblue">UMT</span> Login Selection</h2>
					<div class="card bg-light text-blue" style="border-radius: 1rem; background-color: aliceblue"">

						<div class="card-body p-2 text-center">
							<div class="mb-md-5 mt-md-4 pb-2">
								<h5 class="text-black-70 mb-5 font-weight-light" style="color:dodgerblue;"><i class="ace-icon fa fa-leaf" style="font-family:FontAwesome; color:limegreen;"></i> Please Select Login Options</h5>

								<div class="mb-md-2 mt-md-2 pb-2">
									<asp:Button ID="btnStaffLogin" class="btn btn-outline-dark btn-lg px-6" runat="server" Text="Login As Staff" OnClick="btnStaffLogin_Click" />
									<br />
								</div>
								<div class="mb-md-4 mt-md-4">
									<asp:Button ID="btnStudentLogin" class="btn btn-outline-dark btn-lg px-5" runat="server" Text="Login As Student" OnClick="btnStudentLogin_Click" />
								</div>


							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>

</asp:Content>
