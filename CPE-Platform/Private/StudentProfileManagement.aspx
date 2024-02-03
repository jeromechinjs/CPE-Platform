<%@ Page Title="Student Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentProfileManagement.aspx.cs" Inherits="CPE_Platform.StudentProfileManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


	<ul class="nav nav-tabs" id="myTab" role="tablist">
		<li class="nav-item" role="presentation">
			<button class="nav-link active" id="profile-tab" data-bs-toggle="tab" data-bs-target="#profile" type="button" role="tab" aria-controls="profile" aria-selected="true">Profile</button>
		</li>

		<li class="nav-item" role="presentation">
			<button class="nav-link" id="rewards-tab" data-bs-toggle="tab" data-bs-target="#rewards" type="button" role="tab" aria-controls="rewards" aria-selected="false">CPE Rewards</button>
		</li>
	</ul>
	<div class="tab-content" id="myTabContent">
		<div class="tab-pane fade show active" id="profile" role="tabpanel" aria-labelledby="profile-tab">
			<asp:Button ID="btnLogOut" runat="server" Text="Log Out" OnClick="btnLogOut_Click" />
			<%--temporary for log out --%>
		</div>

		<%--Rewards part --%>
		<div class="tab-pane fade" id="rewards" role="tabpanel" aria-labelledby="rewards-tab">
			<h3 class="mb-1 ms-5 text-sm">CPE Course Completed</h3>
			<div>
				<asp:Label ID="lblStudentID" runat="server"></asp:Label>
			</div>

			<div class="col-10 p-4 flex-column d-flex justify-content-center">
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
									<asp:Label Text='<%# Eval("CPEDesc") %>' runat="server" />
								</ItemTemplate>

								<EditItemTemplate>
									<asp:TextBox ID="txtCPEDesc" Text='<%# Eval("CPEDesc") %>' runat="server" />
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
