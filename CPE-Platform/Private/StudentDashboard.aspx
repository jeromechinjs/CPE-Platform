<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="CPE_Platform.Private.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
      <div class="row mt-3">
        <div class="col p-3">
            <asp:Panel class="dashboard-figures d-flex" ID="pts_collected" runat="server" Text='<%# Eval("RewardsUsed") %>'>0</asp:Panel>
            <asp:Panel class="dashboard-text" ID="txt_pts_collected" runat="server">points collected</asp:Panel>
        </div>
        <div class="col p-3">
            <asp:Panel class="dashboard-figures d-flex" ID="Panel1" runat="server" Text='<%# Eval("RewardsUsed") %>'>0</asp:Panel>
        </div>
        <div class="col p-3">
            <asp:Panel class="dashboard-figures d-flex" ID="Panel2" runat="server" Text='<%# Eval("RewardsUsed") %>'>0</asp:Panel>
        </div>

      </div>
      <div class="row">

      </div>
    </div>
</asp:Content>
