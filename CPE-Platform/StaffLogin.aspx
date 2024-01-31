<%@ Page Title="Staff Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StaffLogin.aspx.cs" Inherits="CPE_Platform.StaffLogin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>
	<html xmlns="http://www.w3.org/1999/xhtml">
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
								<h2 class="text-lg-center mb-4 text-uppercase">TAR UMT Staff Intranet</h2>
								<h3 class="mb-4 text-sm-center ">Login</h3>
								<p class="text-white-50 mb-5">Please enter your login and password!</p>
								<div>
									<div class="form-outline form-white mb-4">
										<%--<asp:Label runat="server" class="form-label" ID="lblStaffID" Text="Staff ID"></asp:Label>--%>
										<asp:TextBox class="form-control custom-form-control" placeholder="Staff ID" ID="txtStaffID" runat="server"></asp:TextBox>
										<%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorStaffID" Text="*" runat="server" ErrorMessage="Staff ID is required" ForeColor="Red" ControlToValidate="txtStaffID"></asp:RequiredFieldValidator>
										--%>
									</div>

									<div class="form-outline form-white mb-4">
										<%--<asp:Label runat="server" class="form-label" ID="lblStaffPassword" Text="Password"></asp:Label>--%>
										<asp:TextBox ID="txtStaffPassword" class="form-control custom-form-control" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
										<%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorStaffPassword" Text="*" runat="server" ErrorMessage="Password is required" ForeColor="Red" ControlToValidate="txtStaffPassword"></asp:RequiredFieldValidator><br />
										--%>
									</div>
								</div>

								<p class="small mb-3 pb-lg-2">
									<asp:Label runat="server" ID="lblErrorMsg"></asp:Label>
								</p>
								<p class="small mb-3 pb-lg-2">
									<asp:HyperLink class="text-white-50 custom-link" ID="navForgetPassword" NavigateUrl="~/Public/StaffForgetPassword.aspx" runat="server">Forgot password?</asp:HyperLink>
								</p>

								<asp:Button ID="btnStaffLogin" class="btn btn-outline-light btn-lg px-5" runat="server" Text="Login" OnClick="btnStaffLogin_Click"></asp:Button>

							</div>

							<div>
								<p class="mb-4">
									Don't have an account?
								<asp:HyperLink ID="navRegisterStudent" class="text-white-50 fw-bold custom-link" NavigateUrl="~/Public/StaffRegisterInfo.aspx" runat="server">Sign Up</asp:HyperLink>
								</p>
							</div>

						</div>
					</div>
				</div>
			</div>
		</div>
	</section>

</asp:Content>
