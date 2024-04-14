<%@ Page Title="Student Reset Password" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="StudentResetPassword.aspx.cs" Inherits="CPE_Platform.StudentResetPassword" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>
	<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<link rel="stylesheet" href="css/style.css">
		<link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css">
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
				font-size: 5px;
			}

			.mx-5 {
				margin-right: 3rem !important;
				margin-left: 6rem !important;
			}

			.font-weight-light {
				font-weight: lighter;
			}

			.background-img {
				background-image: url('/Resources/student_bg.jpg');
				background-size: cover;
				background-position: center;
				width: 100%;
				height: 100vh;
			}
		</style>
	</head>
	<body>
	</body>
	</html>

	<section class="background-img">
		<div class="container py-3">
			<div class="row d-flex justify-content-center align-items-center">
				<div class="col-12 col-md-8 col-lg-6 col-xl-4">
					<img class="img-fluid p-2 mx-5" style="max-width: 50%;" alt="tarumt logo" src="../Resources/tarumt-logo.png" />
					<h2 class="text-lg-center mb-3 mx-5-custom" style="color: antiquewhite;"><span style="color: palevioletred">TAR </span><span style="color: cornflowerblue">UMT</span> Student Intranet</h2>
					<div class="card text-black" style="border-radius: 1rem; background-color: aliceblue">
						<div class="card-body p-6 text-center">

							<div class="mb-md-5 mt-md-4 pb-2">
								<h3 class="text-black-70 mb-4" style="color: indianred;"><i class="ace-icon fa fa-unlock-alt" style="font-family: FontAwesome; color: indianred;"></i> Reset Password</h3>
								<p class="text-black-70 mb-4" style="color: indianred;">Please enter your New Password</p>
								<div>
									<div class="form-outline form-white mb-4">
										<%--<asp:Label runat="server" ID="lblNewPassword" Text="New Password"></asp:Label>--%>
										<asp:TextBox ID="txtNewPassword" class="form-control custom-form-control custom-input" Style="background-color: rgb(232, 240, 254);" placeholder="New Password" runat="server" TextMode="Password"></asp:TextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidatorStuNewPassword" Text="*" ForeColor="Red" runat="server" ErrorMessage="New Password is required." ControlToValidate="txtNewPassword"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator ID="RegularExpressionValidatorStuNewPassword" runat="server" ForeColor="Red" Text="*" ErrorMessage="Password must contain at least 8 characters, including one uppercase letter, one lowercase letter, one number, and one special character." ControlToValidate="txtNewPassword" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"></asp:RegularExpressionValidator>
									</div>

									<div class="form-outline form-white mb-4">
										<%--<asp:Label runat="server" ID="lblConfirmPassword" Text="Confirm Password"></asp:Label>--%>
										<asp:TextBox ID="txtConfirmPassword" class="form-control custom-form-control custom-input" Style="background-color: rgb(232, 240, 254);" placeholder="Confirm Password" runat="server" TextMode="Password"></asp:TextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidatorStuConfirmPassword" Text="*" ForeColor="Red" runat="server" ErrorMessage="Confirm New Password is required." ControlToValidate="txtConfirmPassword"></asp:RequiredFieldValidator>
										<asp:CompareValidator ID="CompareValidatorConfirmPassword" Text="*" ForeColor="Red" runat="server" ErrorMessage="Both New Password are not match with each other." ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
									</div>

								</div>
								<div id="lblErrorColor" class="small mb-3 pb" role="alert" runat="server">
									<asp:ValidationSummary ID="ValidationSum" ForeColor="Red" DisplayMode="SingleParagraph" runat="server" />
								</div>


								<asp:Button ID="btnConfirmStudentForgetPassword" class="btn btn-danger btn-lg px-5 rounded-0" runat="server" Text="Submit" OnClick="btnConfirmStudentForgetPassword_Click"></asp:Button>
							</div>

							<div>
								<p class="mb-2" style="color: indianred">

									<asp:HyperLink ID="navRetrieveStudentLogin" class="text-red-50 fw-bold custom-link" Style="color: indianred" NavigateUrl="~/StudentLogin.aspx" runat="server">Back To Login <i class="ace-icon fa fa-arrow-right" style="font-family: FontAwesome; color: indianred;"></i></asp:HyperLink>
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
