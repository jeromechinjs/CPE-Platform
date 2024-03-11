<%@ Page Title="Session Expired" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="SessionEnd.aspx.cs" Inherits="CPE_Platform.SessionEnd" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>

	<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title></title>
		<link rel="stylesheet" href="font-awesome/css/font-awesome.min.css">
		<style>
			.mx-5 {
				margin-right: 3rem !important;
				margin-left: 6rem !important;
			}

			a.custom-link {
				text-decoration: none;
			}

				a.custom-link:hover {
					text-decoration: underline;
				}
		</style>

	</head>
	<body>
	</body>
	</html>
	<div class="container py-5 ">
		<div class="row d-flex justify-content-center align-items-center h-50">
			<div class="col-8 col-lg-6 col-xl-4">
				<h2 class="text-lg-center mb-3 mx-5-custom" style="color: darkblue;"><i class="fa fa-hourglass-end" style="font-family: FontAwesome; color: dodgerblue"> The Session Is TimeOut </i></h2>
			</div>
			<div class="row justify-content-center py-5">
				<div class="col-8 col-lg-6 col-xl-4">
					<p class="text-lg-center mb-3 mx-5-custom">
						<asp:HyperLink ID="navRegister" Style="color: darkblue" class="text-blue-50 fw-bold custom-link" NavigateUrl="~/LoginSelection.aspx" runat="server">Back To Login <i class="ace-icon fa fa-arrow-right" style="font-family: FontAwesome; color: dodgerblue;"></i></asp:HyperLink>
					</p>
				</div>
			</div>

		</div>
	</div>
</asp:Content>
