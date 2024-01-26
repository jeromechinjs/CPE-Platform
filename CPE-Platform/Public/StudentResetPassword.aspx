<%@ Page Title="Student Reset Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentResetPassword.aspx.cs" Inherits="CPE_Platform.StudentResetPassword" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>
	<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<link rel="stylesheet" href="css/StudentProfileManagement.css">
		<style>
			.custom-form-control {
				display: inline;
			}

			a.custom-link {
				text-decoration: none;
			}

				a.custom-link:hover {
					text-decoration: underline;
				}

			.custom-input {
				max-width: 300px;
			}

			.single-validator {
				font-size: 20px; /* Adjust width as needed */
			}

			.double-validator {
				font-size:5px;
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
								<h3 class="mb-4 text-sm-center ">Reset Password</h3>
								<p class="text-white-50 mb-5">Please enter your New Password</p>
								<div>
									<div class="form-outline form-white mb-4">
										<%--<asp:Label runat="server" ID="lblNewPassword" Text="New Password"></asp:Label>--%>
										<asp:TextBox ID="txtNewPassword" class="form-control custom-form-control custom-input" placeholder="New Password" runat="server" TextMode="Password"></asp:TextBox>
										<asp:RequiredFieldValidator CssClass="single-validator" ID="RequiredFieldValidatorStuNewPassword" Text="*" ForeColor="Red" runat="server" ErrorMessage="New Password is required" ControlToValidate="txtNewPassword"></asp:RequiredFieldValidator>
									</div>

									<div class="form-outline form-white mb-4">
										<%--<asp:Label runat="server" ID="lblConfirmPassword" Text="Confirm Password"></asp:Label>--%>
										<asp:TextBox ID="txtConfirmPassword" class="form-control custom-form-control custom-input" placeholder="Confirm Password" runat="server" TextMode="Password"></asp:TextBox>
										<asp:RequiredFieldValidator CssClass="double-validator" ID="RequiredFieldValidatorStuConfirmPassword" Text="*" ForeColor="Red" runat="server" ErrorMessage="Confirm New Password is required" ControlToValidate="txtConfirmPassword"></asp:RequiredFieldValidator>
										<asp:CompareValidator CssClass="double-validator" ID="CompareValidatorConfirmPassword" Text="*" ForeColor="Red" runat="server" ErrorMessage="Password do not match with the new password." ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
									</div>
								</div>

								<p class="small mb-3 pb-lg-2">
									<asp:Label runat="server" ID="lblErrorMsg"></asp:Label>
								</p>

								<asp:Button ID="btnConfirmStudentForgetPassword" class="btn btn-outline-light btn-lg px-5" runat="server" Text="Submit" OnClick="btnConfirmStudentForgetPassword_Click"></asp:Button>
							</div>

							<div>
								<p class="mb-4">

									<asp:HyperLink ID="navRetrieveStudentLogin" class="text-white-50 fw-bold custom-link" NavigateUrl="~/StudentLogin.aspx" runat="server">Back To Login</asp:HyperLink>
								</p>
							</div>

						</div>
					</div>
				</div>
			</div>
		</div>
	</section>


	<div>
	</div>

</asp:Content>
