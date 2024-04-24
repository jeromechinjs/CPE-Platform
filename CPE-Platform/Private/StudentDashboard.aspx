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

<%--        <div class="col p-3 d-flex flex-column align-items-center">
            <div class="dashboard-figures d-flex" runat="server">
                <asp:Label ID="discounts_collected" runat="server" Text='<%# Eval("RewardsUsed") %>'>0%</asp:Label>
            </div>
            <asp:Label class="dashboard-text" ID="txt_discounts" runat="server">redeemable discounts</asp:Label>
        </div>--%>

        <div class="col me-5 p-3 d-flex flex-column align-items-center">
            <div class="dashboard-figures d-flex" runat="server">
                <asp:Label ID="num_active_courses" runat="server" Text='<%# Eval("RewardsUsed") %>'>0</asp:Label>
            </div>
            <a href="ActiveCourses.aspx" class="dashboard-text">active courses</a>
        </div>
      </div>
      <div class="row mt-5">
		<%--Rewards part --%>

		<div class="p-4 flex-column d-flex justify-content-center">
			<%--<p class="my-3 fs-2 fw-bolder">Rewards</p>--%>
			<div class="container">
				<asp:GridView class="table table-bordered table-condensed table-responsive table-hover" ID="gvRewardsView" runat="server" AutoGenerateColumns="False" DataKeyNames="CPECode"
					ShowHeaderWhenEmpty="True" AllowSorting="True">
					<%-- Theme --%>

					<Columns>
						<%-- CPE Code --%>

						<asp:TemplateField HeaderText="Course Code">
							<ItemTemplate>
								<asp:Label Text='<%# Eval("CPECode") %>' runat="server" />
							</ItemTemplate>

							<EditItemTemplate>
								<asp:TextBox ID="txtCPECode" Text='<%# Eval("CPECode") %>' runat="server" />
							</EditItemTemplate>

						</asp:TemplateField>

						<%-- Course Name --%>

						<asp:TemplateField HeaderText="Course Name">
							<ItemTemplate>
								<asp:Label Text='<%# Eval("CPEName") %>' runat="server" />
							</ItemTemplate>

							<EditItemTemplate>
								<asp:TextBox ID="txtCPEDesc" Text='<%# Eval("CPEName") %>' runat="server" />
							</EditItemTemplate>

						</asp:TemplateField>


						<%-- Progress --%>

						<asp:TemplateField HeaderText="Progress">
							<ItemTemplate>
								<asp:Label Text='<%# Eval("Progress") %>' runat="server" />
							</ItemTemplate>

							<EditItemTemplate>
								<asp:TextBox ID="txtProgress" Text='<%# Eval("Progress") %>' runat="server" />
							</EditItemTemplate>

						</asp:TemplateField>

						<%-- Rewards --%>

						<asp:TemplateField HeaderText="Rewards">
							<ItemTemplate>
								<asp:Label Text='<%# Eval("RewardAwarded") %>' runat="server" />
							</ItemTemplate>

							<EditItemTemplate>
								<asp:TextBox ID="txtRewards" Text='<%# Eval("RewardsAwarded") %>' runat="server" />
							</EditItemTemplate>

						</asp:TemplateField>



					</Columns>
				</asp:GridView>
			</div>
		</div>
      </div>
    </div>
</asp:Content>
