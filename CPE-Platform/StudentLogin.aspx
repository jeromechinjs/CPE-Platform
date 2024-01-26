<%@ Page Title="Student Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentLogin.aspx.cs" Inherits="CPE_Platform.StudentLogin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>
	<html>
	<head>
		<link rel="stylesheet" href="css/StudentProfileManagement.css">
		<style>
			.custom-form-control {
				display: inline-block;
			}

		</style>
	</head>
	</html>

	<section class="vh-50 gradient-custom">
		<div class="container py-5 h-100">
			<div class="row d-flex justify-content-center align-items-center h-50">
				<div class="col-12 col-md-8 col-lg-6 col-xl-5">
					<div class="card bg-dark text-white" style="border-radius: 1rem;">
						<div class="card-body p-6 text-center">

							<div class="mb-md-5 mt-md-4 pb-5">
								<h2 class="text-lg-center mb-4 text-uppercase">TAR UMT Student Intranet</h2>
								<h3 class="mb-4 text-sm-center ">Login</h3>
								<p class="text-white-50 mb-5">Please enter your login and password!</p>
								<div>
									<div class="form-outline form-white mb-4">
										<%--<asp:Label runat="server" class="form-label" ID="lblStudentID" Text="Student ID"></asp:Label>--%>
										<asp:TextBox class="form-control custom-form-control" placeholder="Student ID" ID="txtStudentID" runat="server"></asp:TextBox>
										<%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorStudentID" Text="*" runat="server" ErrorMessage="Student ID is required" ForeColor="Red" ControlToValidate="txtStudentID"></asp:RequiredFieldValidator>
										--%>
									</div>

									<div class="form-outline form-white mb-4">
										<%--<asp:Label runat="server" class="form-label" ID="lblStudentPassword" Text="Password"></asp:Label>--%>
										<asp:TextBox ID="txtStudentPassword" class="form-control custom-form-control" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
										<%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorStudentPassword" Text="*" runat="server" ErrorMessage="Password is required" ForeColor="Red" ControlToValidate="txtStudentPassword"></asp:RequiredFieldValidator><br />
										--%>
									</div>
								</div>

								<p class="small mb-3 pb-lg-2">
									<asp:Label runat="server" ID="lblErrorMsg"></asp:Label>
								</p>
								<p class="small mb-3 pb-lg-2">
									<asp:HyperLink class="text-white-50" ID="navForgetPassword" NavigateUrl="~/Public/StudentForgetPassword.aspx" runat="server">Forgot password?</asp:HyperLink>
								</p>

								<asp:Button ID="btnStudentLogin" class="btn btn-outline-light btn-lg px-5" runat="server" Text="Login" OnClick="btnStudentLogin_Click"></asp:Button>

							</div>

							<div>
								<p class="mb-4">
									Don't have an account?
									<asp:HyperLink ID="navRegisterStudent" class="text-white-50 fw-bold" NavigateUrl="~/Public/RegisterStudentInfo.aspx" runat="server">Sign Up</asp:HyperLink>
								</p>
							</div>

						</div>
					</div>
				</div>
			</div>
		</div>
	</section>

</asp:Content>
