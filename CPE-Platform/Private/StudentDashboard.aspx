<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="CPE_Platform.Private.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container dashboard py-5">
      <div class="row mt-3">
        <div class="col p-3 d-flex flex-column align-items-center">
            <div class="dashboard-figures d-flex" runat="server">
                <asp:Label ID="pts_collected" runat="server" Text='<%# Eval("RewardsUsed") %>'>0</asp:Label>
            </div>
            <asp:Label class="dashboard-text" ID="txt_pts_collected" runat="server">points collected</asp:Label>
        </div>

        <div class="col p-3 d-flex flex-column align-items-center">
            <div class="dashboard-figures d-flex" runat="server">
                <asp:Label ID="discounts_collected" runat="server" Text='<%# Eval("RewardsUsed") %>'>0%</asp:Label>
            </div>
            <asp:Label class="dashboard-text" ID="txt_discounts" runat="server">redeemable discounts</asp:Label>
        </div>

        <div class="col me-5 p-3 d-flex flex-column align-items-center">
            <div class="dashboard-figures d-flex" runat="server">
                <asp:Label ID="num_active_courses" runat="server" Text='<%# Eval("RewardsUsed") %>'>0</asp:Label>
            </div>
            <a href="#" class="dashboard-text">active courses</a>
        </div>
      </div>
      <div class="row">

      </div>
    </div>
</asp:Content>
