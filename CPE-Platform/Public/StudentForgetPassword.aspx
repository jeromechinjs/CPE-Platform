<%@ Page Title="Student Forget Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentForgetPassword.aspx.cs" Inherits="CPE_Platform.StudentForgetPassword" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>
	<html>
	<head>
		<link rel="stylesheet" href="css/StudentProfileManagement.css">
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
		</style>
	</head>
	</html>

	<section class="vh-50 gradient-custom">
		<div class="container py-5 h-100">
			<div class="row d-flex justify-content-center align-items-center h-50">
				<div class="col-12 col-md-8 col-lg-6 col-xl-5">
					<div class="card bg-dark text-white" style="border-radius: 1rem;">
						<div class="card-body p-6 text-center">

							<div class="mb-md-5 mt-md-4 pb-2">
								<h2 class="text-lg-center mb-4 text-uppercase">TAR UMT Student Intranet</h2>
								<h3 class="mb-4 text-sm-center ">Retrieve Password</h3>
								<p class="text-white-50 mb-5">Enter your NRIC no. to retrieve your password</p>

								<div class="form-outline form-white mb-4">
									<%--<asp:Label runat="server" ID="lblStudentICFP" Text="Student IC"></asp:Label>--%>
									<asp:TextBox class="form-control custom-form-control" placeholder="NRIC Without dash" ID="txtStudentIC" runat="server"></asp:TextBox>
									<%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorStudentIC" Text="*" ForeColor="Red" runat="server" ErrorMessage="IC Number is required" ControlToValidate="txtStudentIC"></asp:RequiredFieldValidator>
									--%>
								</div>

								<p class="small mb-3 pb-lg-2">
									<asp:Label runat="server" ID="lblErrorMsg"></asp:Label>
								</p>

								<asp:Button ID="btnConfirmStudentForgetPassword" class="btn btn-outline-light btn-lg px-5" runat="server" Text="Submit" OnClick="btnConfirmStudentForgetPassword_Click"></asp:Button>

							</div>

							<div>
								<p class="mb-4">
									<asp:HyperLink ID="navRegisterStudent" class="text-white-50 fw-bold custom-link" NavigateUrl="~/StudentLogin.aspx" runat="server">Back To Login</asp:HyperLink>
								</p>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>

	</section>

</asp:Content>
