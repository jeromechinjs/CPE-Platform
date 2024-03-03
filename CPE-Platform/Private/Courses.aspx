<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="CPE_Platform.Private.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="container">
            <div class="row">
                <div class="p-3 m-0">
                    <p>Search by</p>
                    <asp:DropDownList class="dropdown-center form-select" ID="DropDownList2" runat="server" DataSourceID="SqlDataSource2" DataTextField="CPEType" DataValueField="CPECode" AutoPostBack="True" AppendDataBoundItems="True">
                        <asp:ListItem Value="-1">Show All</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [CPE_Course]"></asp:SqlDataSource>
                </div>

                <div class="p-3 m-0">
                    <asp:DataList class="w-100" ID="DataList1" runat="server" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="DataList1_SelectedIndexChanged">
                        <ItemTemplate>
                            <div class="card mb-3">
                                <div class="row g-0">
                                    <div class="col-md-4 d-flex justify-content-center align-items-center ">
                                        <%--<asp:Image class="img-fluid rounded-start" ID="Image2" runat="server" ImageUrl='<%# Eval("ProductImage") %>' />--%>
                                    </div>
                                    <div class="col-md-8">
                                        <div class="card-body">
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
                                                <asp:Button class="btn btn-sm btn-primary" ID="Button2" runat="server" Text="Add To Cart" CommandArgument='<%# Eval("CPECode") %>' CommandName="AddToCart" OnClick="CartBtn_Click" OnClientClick="javascript:alert('Item added to Cart')" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [CPE_Course]">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList2" Name="CategoryID" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
</asp:Content>
