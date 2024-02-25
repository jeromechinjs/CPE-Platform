<%@ Page Title="Student Login" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="StudentLogin.aspx.cs" Inherits="CPE_Platform.StudentLogin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>
	<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<link rel="stylesheet" href="css/style.css">
		<link rel="stylesheet" href="font-awesome/css/font-awesome.min.css">
		<style>
			.custom-form-control {
				display: inline-block;
			}

			a.custom-link {
				text-decoration: none;
			}

				a.custom-link:hover {
					text-decoration: underline;
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
		<div class="container py-3">
			<div class="row d-flex justify-content-center align-items-center">
				<div class="col-12 col-md-8 col-lg-6 col-xl-4">
					<img class="img-fluid p-2 mx-5" style="max-width: 50%;" alt="tarumt logo" src="Resources/tarumt-logo.png" />
					<h2 class="text-lg-center mb-3 mx-5-custom" style="color: antiquewhite;"><span style="color: palevioletred">TAR </span><span style="color: cornflowerblue">UMT</span> Student Intranet</h2>
					<div class="card text-black" style="border-radius: 1rem; background-color: aliceblue">
						<div class="card-body p-6 text-center">

							<div class="mb-md-5 mt-md-4 pb-2">
								<h5 class="text-black-70 mb-4 font-weight-light" style="color:dodgerblue;"><i class="ace-icon fa fa-leaf" style="font-family:FontAwesome; color:limegreen;"></i> Please Enter Your Information</h5>
								<div>
									<div class="form-outline form-black mb-4">
										<%--<asp:Label runat="server" class="form-label" ID="lblStudentID" Text="Student ID"></asp:Label>--%>
										<asp:TextBox class="form-control custom-form-control" Style="background-color: rgb(232, 240, 254);" placeholder="Student ID" ID="txtStudentID" runat="server"></asp:TextBox>
										<%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorStudentID" Text="*" runat="server" ErrorMessage="Student ID is required" ForeColor="Red" ControlToValidate="txtStudentID"></asp:RequiredFieldValidator>
										--%>
									</div>

									<div class="form-outline form-black mb-4">
										<%--<asp:Label runat="server" class="form-label" ID="lblStudentPassword" Text="Password"></asp:Label>--%>
										<asp:TextBox ID="txtStudentPassword" class="form-control custom-form-control" Style="background-color: rgb(232, 240, 254);" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
										<%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorStudentPassword" Text="*" runat="server" ErrorMessage="Password is required" ForeColor="Red" ControlToValidate="txtStudentPassword"></asp:RequiredFieldValidator><br />
										--%>
									</div>
								</div>

								<div id="lblErrorColor" class="alert alert-danger small mb-3 pb" role="alert" runat="server">
									<asp:Label runat="server" ID="lblErrorMsg"></asp:Label>
								</div>
								<p class="small mb-3 pb-lg-2">
									<asp:HyperLink class="text-blue-50 custom-link" ID="navForgetPassword" NavigateUrl="~/Public/StudentForgetPassword.aspx" runat="server">Forgot password?</asp:HyperLink>
								</p>

								<asp:Button ID="btnStudentLogin" class="btn btn-primary btn-lg px-5 rounded-0" runat="server" Text="Login" OnClick="btnStudentLogin_Click"></asp:Button>

							</div>

							<div>
								<p class="mb-4" style="color:dodgerblue">
									Don't have an account?
									<asp:HyperLink ID="navRegisterStudent" class="text-blue-50 fw-bold custom-link" NavigateUrl="~/Public/RegisterStudentInfo.aspx" runat="server">Sign Up</asp:HyperLink>
								</p>
							</div>

						</div>
					</div>
				</div>
			</div>
		</div>
	</section>

</asp:Content>
