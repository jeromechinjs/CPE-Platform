<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="CPE_Platform.Private.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <head>
		<link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
		<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
		<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
	</head>

    <div class="container">
        <div class="row">
             <asp:Label class="card-title fw-bold" ID="testlbl" runat="server">'asdfsdfsfasfasdfasdfs</asp:Label>

            <div class="p-3 m-0">
                <p>Search by</p>
                <asp:DropDownList class="dropdown-center form-select" ID="courseTypes" runat="server" DataSourceID="courseTypesDataSource" DataTextField="CPEType" DataValueField="CPEType" AutoPostBack="True" AppendDataBoundItems="True">
                    <asp:ListItem Value="-1">Show All</asp:ListItem>
                </asp:DropDownList>
                <!-- Remaining list items are derived from sql data source below-->
                <asp:SqlDataSource ID="courseTypesDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT DISTINCT CPEType FROM [CPE_Course]"></asp:SqlDataSource>
            </div>

            <!-- Course Cards -->
            <div class="p-3 m-0">
                <asp:DataList class="w-100" ID="DataList1" runat="server" DataSourceID="allCourses" OnSelectedIndexChanged="DataList1_SelectedIndexChanged">
                    <ItemTemplate>
                        <div class="card mb-3">
                            <div class="row g-0">
                                <div class="col-md-4 d-flex justify-content-center align-items-center ">
                                    <%--<asp:Image class="img-fluid rounded-start" ID="Image2" runat="server" ImageUrl='<%# Eval("ProductImage") %>' />--%>
                                </div>
                                <div class="col-md-8">
                                    <div class="card-body">
                                        <asp:Label class="card-title fw-bold" ID="txtCPECode" runat="server" Text='<%# Eval("CPECode") %>'></asp:Label>
                                        <br />
                                        <asp:Label class="card-title fw-bold" ID="Label5" runat="server" Text='<%# Eval("CPEDesc") %>'></asp:Label>
                                        <br />
                                        <asp:Label class="card-text" ID="Label6" runat="server" Text='<%# Eval("CPEStartDate")%>'></asp:Label>
                                        <span>to </span>
                                        <asp:Label class="card-text" ID="Label1" runat="server" Text='<%# Eval("CPEEndDate")%>'></asp:Label>
                                        <br />
                                        <span>RM </span>
                                        <asp:Label class="card-text" ID="Label7" runat="server" Text='<%# Eval("CPEPrice") %>'></asp:Label>
                                        &nbsp;
                                        <span>Available Seats Left: </span>
                                        <asp:Label class="card-text" ID="CPESeatAmount" runat="server" Text='<%# Eval("CPESeatAmount") %>'></asp:Label>
                                        <br />
                                        <div class="d-flex flex-row-reverse mt-3">
										    <asp:LinkButton CssClass="btn btn-outline-info" ID="modal" CommandName="View More" OnCommand="view_course_info" CommandArgument='<%#Eval("CPECode") %>' runat="server">View More</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="allCourses" runat="server"></asp:SqlDataSource>

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
                        <asp:Label class="card-title fw-bold" ID="txtCPEDesc" runat="server" Text='<%# Eval("CPEDesc") %>'></asp:Label>
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
