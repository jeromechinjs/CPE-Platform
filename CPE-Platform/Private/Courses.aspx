﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="CPE_Platform.Private.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <head>
		<link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
		<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
		<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
	</head>

    <div class="container px-4">
        <div class="row">
<%--            <div class="p-3 m-0">
                <p>Search by</p>
                <asp:DropDownList class="dropdown-center form-select" ID="dropdownCourseTypes" runat="server"  DataTextField="CPEType" DataValueField="CPEType" AutoPostBack="True" AppendDataBoundItems="True">
                    <asp:ListItem Value="-1">Show All</asp:ListItem>
                    <asp:ListItem Value="Professional Programmes">Professional Programmes</asp:ListItem>
                    <asp:ListItem Value="Micro-Credential">Micro-Credential</asp:ListItem>
                    <asp:ListItem Value="Corporate Programmes">Corporate Programmes</asp:ListItem>

                </asp:DropDownList>
            </div>--%>

            <!-- Course Cards -->
            <div class="p-3 m-0">
                <asp:DataList class="w-50" ID="courseCards" runat="server" DataSourceID="allCourses">
                    <ItemTemplate>
                        <div class="card mb-3">
                            <div class="row g-0 w-100">
                                <div class="card-body px-5 py-3">
                                    <asp:Label class="card-title fw-bold" ID="txtCPECode" runat="server" Text='<%# Eval("CPECode") %>'></asp:Label>
                                    <br />
                                    <asp:Label class="card-title fw-bold" ID="txtCPEName" runat="server" Text='<%# Eval("CPEName") %>'></asp:Label>
                                    <br />
                                    <asp:Label class="card-text" ID="Label6" runat="server" Text='<%# Eval("CPEStartDate")%>'></asp:Label>
                                    <span>to </span>
                                    <asp:Label class="card-text" ID="Label1" runat="server" Text='<%# Eval("CPEEndDate")%>'></asp:Label>
                                    <br />
                                    <span>Available Seats Left: </span>
                                    <asp:Label class="card-text" ID="CPESeatAmount" runat="server" Text='<%# Eval("CPESeatAmount") %>'></asp:Label>
                                    <br />
                                    <div class="d-flex flex-row-reverse mt-3">
										<asp:LinkButton CssClass="btn btn-outline-info" ID="modal" CommandName="View More" OnCommand="view_course_info" CommandArgument='<%#Eval("CPECode") %>' runat="server">View More</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="allCourses" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>

    <%--modal popup--%>
	<div class="container">
		<div class="modal fade" id="courseDetailsModal" data-backdrop="static" role="dialog">
			<div class=" modal-dialog modal-dailog-centered">
				<div class="modal-content">
					<div class="modal-header">
						<h4 class="modal-title">Course Details</h4>

						<button type="button" class="close" data-dismiss="modal">&times;</button>
					</div>
					<div class="modal-body">
                        <asp:Label class="card-title fw-bold" ID="txtCPECode" runat="server" Text='<%# Eval("CPECode") %>'></asp:Label>
                        <br />

                        <asp:Label class="card-title fw-bold" ID="txtCPEName" runat="server" Text='<%# Eval("CPEName") %>'></asp:Label>
                        <br /><br />

                        <asp:Label class="card-title" ID="txtCPEDesc" runat="server" Text='<%# Eval("CPEDesc") %>'></asp:Label>
                        <br /><br />

                        <asp:Label class="card-title fw-bold" ID="Label2" runat="server" Text='Venue: '></asp:Label>
                        <asp:Label class="card-title" ID="txtCPEVenue" runat="server" Text='<%# Eval("CPEVenue") %>'></asp:Label>
                        <br /><br />

                        <asp:Label class="card-title fw-bold" ID="Label3" runat="server" Text='Trainer: '></asp:Label>
                         <asp:Label class="card-title" ID="txtCPETrainer" runat="server" Text='<%# Eval("CPETrainer") %>'></asp:Label>
                        <br /><br />

                        <asp:Label class="card-title fw-bold" ID="Label8" runat="server" Text='Date: '></asp:Label>
                        <asp:Label class="card-text" ID="dllStartDate" runat="server" Text='<%# Eval("CPEStartDate")%>'></asp:Label>
                        <span>to </span>
                        <asp:Label class="card-text" ID="dllEndDate" runat="server" Text='<%# Eval("CPEEndDate")%>'></asp:Label>
                        <br /><br />

                        <asp:Label class="card-title fw-bold" ID="Label4" runat="server" Text='Time: '></asp:Label>
                        <asp:Label class="card-title" ID="txtStartTime" runat="server" Text='<%# Eval("CPEStartTime") %>'></asp:Label>
                         <span>to </span>
                         <asp:Label class="card-title" ID="txtEndTime" runat="server" Text='<%# Eval("CPEEndTime") %>'></asp:Label>
                        <br /><br />

                        <asp:Label class="card-title fw-bold" ID="Label5" runat="server" Text='Contact: '></asp:Label>
                        <asp:Label class="card-title" ID="txtContact" runat="server" Text='<%# Eval("CPEContact") %>'></asp:Label>
                        <br /><br />

                        <asp:Label class="card-title fw-bold" ID="Label7" runat="server" Text='Email: '></asp:Label>
                        <asp:Label class="card-title" ID="txtEmail" runat="server" Text='<%# Eval("CPEEmail") %>'></asp:Label>
                        <br /><br />

                        <asp:Label class="card-title fw-bold" ID="Label9" runat="server" Text='Price: '></asp:Label>
                        <span>RM </span>
                        <asp:Label class="card-text" ID="txtCPEPrice" runat="server" Text='<%# Eval("CPEPrice") %>'></asp:Label>
                        <br /><br />

                        <asp:Label class="card-title fw-bold" ID="Label10" runat="server" Text='Available Seats Left: '></asp:Label>
                        <asp:Label class="card-text" ID="txtCPESeat" runat="server" Text='<%# Eval("CPESeatAmount") %>'></asp:Label>
                        <span>seats</span>
                        <br /><br />

                        <asp:Label class="card-title fw-bold" ID="Label11" runat="server" Text='Reward points: '></asp:Label>
                        <asp:Label class="card-title" ID="txtRewards" runat="server" Text='<%# Eval("Rewards") %>'></asp:Label>
					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-sm btn-outline-danger" data-dismiss="modal">Close</button>
                         <asp:LinkButton CssClass="btn btn-sm btn-primary" ID="addCart" CommandName="Add To Cart" OnCommand="CartBtn_Click" CommandArgument='<%# Eval("CPECode") %>' runat="server">Add To Cart</asp:LinkButton>
					</div>
				</div>
			</div>
		</div>
	</div>

    <!-- cart toast messages -->
    <!-- Course added to cart message -->
    <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
        <asp:Panel ID="toast1" CssClass="toast hide" role="alert" aria-live="assertive" aria-atomic="true" runat="server">
            <div class="toast-header">
                <img src="src/tarucLogo.png" class="rounded me-2" width="12">
                <strong class="me-auto">CPE Platform</strong>
                <small>Just now</small>
                <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body">
                Course sucessfully added to cart.
            </div>
        </asp:Panel>
    </div>

    <!-- Course already existed in cart message -->
    <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
        <asp:Panel ID="toast2" CssClass="toast hide" role="alert" aria-live="assertive" aria-atomic="true" runat="server">
            <div class="toast-header">
                <img src="src/tarucLogo.png" class="rounded me-2" width="12">
                <strong class="me-auto">CPE Platform</strong>
                <small>Just now</small>
                <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body">
                Course already existed in cart.
            </div>
        </asp:Panel>
    </div>

    <!-- Out of seats message -->
    <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
        <asp:Panel ID="toast3" CssClass="toast hide" role="alert" aria-live="assertive" aria-atomic="true" runat="server">
            <div class="toast-header">
                <img src="src/tarucLogo.png" class="rounded me-2" width="12">
                <strong class="me-auto">CPE Platform</strong>
                <small>Just now</small>
                <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body">
                No more seats left for this course.
            </div>
        </asp:Panel>
    </div>

    <!-- already registered for course (already active course) message -->
    <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
        <asp:Panel ID="toast4" CssClass="toast hide" role="alert" aria-live="assertive" aria-atomic="true" runat="server">
            <div class="toast-header">
                <img src="src/tarucLogo.png" class="rounded me-2" width="12">
                <strong class="me-auto">CPE Platform</strong>
                <small>Just now</small>
                <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body">
                You have registered for this course. Kindly select other courses.
            </div>
        </asp:Panel>
    </div>

</asp:Content>
