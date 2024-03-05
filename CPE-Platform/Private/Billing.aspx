<%@ Page Title="Billing" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="CPE_Platform.Private.Billing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>

	<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title></title>
	</head>
	<body>
	</body>
	</html>

	<div class="container py-5" style="background-color: #eee;">
		<div class="card">
			<div class="card-body">
				<div class="row d-flex justify-content-center pb-5">
					<div class="col-md-7 col-xl-5 mb-4 mb-md-0">
						<div class="py-4 d-flex flex-row">
							<h5><i class="fa-brands fa-paypal" style="font-family: FontAwesome"></i><i><b style="color: darkblue"> Pay</b><b style="color: dodgerblue">Pal</b></i> |</h5>
							<b><span class="ps-2" style="color: dodgerblue">Pay</span></b>
						</div>
						<h4 class="text-success">
							<asp:Label ID="lblTotalAmount" Text="Total Amount" runat="server"></asp:Label>
						</h4>
						<h4>CPE Courses</h4>

						<p>
							Kindly confirm with the CPE Courses as CPE courses will be submitted to the office and are non-refundable after payment is confirmed.
						</p>
						<hr />
						<div class="pt-2">
							<div class="d-flex pb-2">
								<div>
									<p>
										<b>CPE Points <span class="text-success">
											<asp:Label ID="CPEPoints" Text="Points" runat="server"></asp:Label>
										</span>
										</b>
									</p>
								</div>
								<div class="ms-auto">
									<p class="text-primary">
										<asp:CheckBox ID="chkboxPoints" runat="server" />
									</p>
								</div>
							</div>
							<%--will change after retrieve from db--%>

							<p style="color: indianred">
								*Note: 10 points can be exchanged for RM 1.00*
							</p>
							<%--<form class="pb-3">--%>
							<div class="d-flex flex-row pb-3">

								<div class="d-flex align-items-center pe-2">
									<input class="form-check-input" type="radio" name="radiobtnPaypal" id="radiobtnPaypal"
										value="" checked runat="server" />
								</div>
								<div class="rounded border d-flex w-100 p-3 align-items-center">
									<p class="mb-0">
										<i class="fa-brands fa-paypal" style="font-family: FontAwesome"></i><i><b style="color: darkblue"> Pay</b><b style="color: dodgerblue">Pal</b></i>
									</p>
								</div>
							</div>

							<%-- </form>--%>
							<asp:Button ID="btnProceedPayment" class="btn btn-primary btn-block btn-lg" runat="server" Text="Proceed to payment" />
						</div>
					</div>

					<%--recap of all the course selected from cart--%>
					<div class="col-md-5 col-xl-4 offset-xl-1">
						<div class="py-4 d-flex justify-content-end">
							<%-- will change after created cart webpage--%>
							<h6><a href="#">Cancel and return to Cart</a></h6>
						</div>
						<div class="rounded d-flex flex-column p-2" style="background-color: #f8f9fa;">
							<div class="p-2 me-3">
								<h4>Recap</h4>
							</div>
							<div class="p-2 d-flex">
								<div class="col-8">
									<asp:Label ID="lblCourse" runat="server" Text="CPE Course"></asp:Label>
								</div>
								<div class="ms-auto">
									<asp:Label ID="lblCPEPrice" runat="server" Text="Price"></asp:Label>
								</div>
							</div>

							<div class="border-top px-2 mx-2"></div>
							<div class="p-2 d-flex pt-3">
								<div class="col-8">Discount</div>
								<div class="ms-auto">- RM 0.00</div>
							</div>
							<div class="border-top px-2 mx-2"></div>
							<div class="p-2 d-flex pt-3">
								<div class="col-8">Points Redeem <span class="fa fa-question-circle text-dark"></div>
								<div class="ms-auto">
									<b>
										<asp:Label ID="lblRewardsReedem" runat="server" Text="Rewards"></asp:Label></b>

								</div>
							</div>
							<div class="p-2 d-flex">
								<div class="col-8">
									SST( 6% )
								</div>
								<div class="ms-auto">
									<b>
										<asp:Label ID="lblSST" runat="server" Text="SST"></asp:Label></b>

								</div>
							</div>
							<div class="border-top px-2 mx-2"></div>
							<div class="p-2 d-flex pt-3">
								<div class="col-8"><b>Total</b></div>
								<div class="ms-auto">
									<b class="text-success">
										<asp:Label ID="lblTotalCPEPrice" runat="server" Text="Total Price"></asp:Label></b>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
