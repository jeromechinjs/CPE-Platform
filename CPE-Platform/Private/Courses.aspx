<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="CPE_Platform.Private.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <head>
		<link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
		<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
		<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
	</head>

    <div class="container">
        <div class="row">
            <div class="p-3 m-0">
                <p>Search by</p>
                <asp:DropDownList class="dropdown-center form-select" ID="dropdown_courseTypes" runat="server" DataSourceID="courseTypes" DataTextField="CPEType" DataValueField="CPEType" AutoPostBack="True" AppendDataBoundItems="True">
                    <asp:ListItem Value="-1">Show All</asp:ListItem>
                </asp:DropDownList>
                <!-- Remaining list items are derived from sql data source below-->
                <asp:SqlDataSource ID="courseTypes" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>
            </div>

            <!-- Course Cards -->
            <div class="p-3 m-0">
                <asp:DataList class="w-50" ID="courseCards" runat="server" DataSourceID="allCourses">
                    <ItemTemplate>
                        <div class="card mb-3">
                            <div class="row g-0 w-100">
                                <div class="card-body px-5 py-3">
                                    <asp:Label class="card-title fw-bold" ID="txtCPECode" runat="server" Text='<%# Eval("CPECode") %>'></asp:Label>
                                    <br />
                                    <asp:Label class="card-text" ID="Label6" runat="server" Text='<%# Eval("CPEStartDate")%>'></asp:Label>
                                    <span>to </span>
                                    <asp:Label class="card-text" ID="Label1" runat="server" Text='<%# Eval("CPEEndDate")%>'></asp:Label>
                                    <br />
                                    <span>RM </span>
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
                        <br />
                        <asp:Label class="card-title fw-bold" ID="txtCPEDesc" runat="server" Text='<%# Eval("CPEDesc") %>'></asp:Label>
                        <br />
                        <asp:Label class="card-title fw-bold" ID="txtCPEVenue" runat="server" Text='<%# Eval("CPEVenue") %>'></asp:Label>
                        <br />
                         <asp:Label class="card-title fw-bold" ID="txtCPETrainer" runat="server" Text='<%# Eval("CPETrainer") %>'></asp:Label>
                        <br />
                        <asp:Label class="card-title fw-bold" ID="txtStartTime" runat="server" Text='<%# Eval("CPEStartTime") %>'></asp:Label>
                        <br />
                         <asp:Label class="card-title fw-bold" ID="txtEndTime" runat="server" Text='<%# Eval("CPEEndTime") %>'></asp:Label>
                        <br />
                        <asp:Label class="card-title fw-bold" ID="txtContact" runat="server" Text='<%# Eval("CPEContact") %>'></asp:Label>
                        <br />
                         <asp:Label class="card-title fw-bold" ID="txtEmail" runat="server" Text='<%# Eval("CPEEmail") %>'></asp:Label>
                        <br />

                        <asp:Label class="card-text" ID="dllStartDate" runat="server" Text='<%# Eval("CPEStartDate")%>'></asp:Label>
                        <span>to </span>
                        <asp:Label class="card-text" ID="dllEndDate" runat="server" Text='<%# Eval("CPEEndDate")%>'></asp:Label>
                        <br />
                        <span>RM </span>
                        <asp:Label class="card-text" ID="txtCPEPrice" runat="server" Text='<%# Eval("CPEPrice") %>'></asp:Label>
                        &nbsp;
                        <span>Available Seats Left: </span>
                        <asp:Label class="card-text" ID="txtCPESeat" runat="server" Text='<%# Eval("CPESeatAmount") %>'></asp:Label>
					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-sm btn-outline-danger" data-dismiss="modal">Close</button>
                         <asp:LinkButton CssClass="btn btn-sm btn-primary" ID="addCart" CommandName="Add To Cart" OnCommand="CartBtn_Click" CommandArgument='<%# Eval("CPECode") %>' runat="server">Add To Cart</asp:LinkButton>
					</div>
				</div>
			</div>
		</div>
	</div>


</asp:Content>
