<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="CPE_Platform.Private.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" id="cartContainer" runat="server">
        <asp:GridView class="card w-100 my-5 p-3" ID="cartItemCards" runat="server" AutoGenerateColumns="False" ShowFooter="true">
            <Columns>

                <asp:BoundField DataField="CPECode" HeaderText="Course Code">
                    <ItemStyle CssClass="card-title" Width="30%" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="CPEName" HeaderText="Course Name">
                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>

<%--                <asp:BoundField DataField="CPEPrice" HeaderText="Course Price">
                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>--%>

                <asp:TemplateField HeaderText="Course Price">
                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <%# Eval("CPEPrice") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:Button class="btn btn-sm btn-primary w-10" ID="Button1" runat="server" Text="Remove" CommandArgument='<%# Eval("CPEName") %>' CommandName="RemoveCartItem" OnClick="removeItem" />
                    </ItemTemplate>
                </asp:TemplateField>                    
            </Columns>
        </asp:GridView>

        <div class="text-center">
            <asp:Button class="btn btn-sm btn-primary" ID="Button2" runat="server" Text="Check Out" OnClick="Button2_Click" />
        </div>
    </div>
    <br />
</asp:Content>
