<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="CPE_Platform.Private.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" id="cartContainer" runat="server">
        <asp:GridView class="card w-100 my-5 p-3" ID="cartItemCards" runat="server" AutoGenerateColumns="False" ShowFooter="true">
            <Columns>

                <asp:BoundField DataField="CPECode" HeaderText="Course Code">
                    <ItemStyle CssClass="card-title" Width="20%" HorizontalAlign="Left"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>

                <asp:BoundField DataField="CPEName" HeaderText="Course Name">
                    <ItemStyle Width="40%" CssClass="pe-5 py-3" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="CPEPrice" HeaderText="Course Price (RM)">
                    <ItemStyle Width="20%" CssClass="" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>

                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton CssClass="btn btn-sm btn-primary w-10" ID="Button1" CommandName="RemoveCartItem" OnCommand="removeItem" CommandArgument='<%# Eval("CPECode") %>' runat="server">Remove</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>  
                
            </Columns>
        </asp:GridView>

        <asp:Button class="btn btn-sm btn-primary" ID="Button2" runat="server" Text="Check Out" OnClick="Button2_Click" />
    </div>
    <br />
</asp:Content>
