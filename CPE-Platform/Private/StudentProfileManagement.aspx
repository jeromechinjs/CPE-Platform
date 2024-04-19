<%@ Page Title="Student Profile" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="StudentProfileManagement.aspx.cs" Inherits="CPE_Platform.StudentProfileManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container p-3">
		<ul class="nav nav-tabs" id="myTab" role="tablist">
			<li class="nav-item" role="presentation">
				<button class="nav-link active" id="profile-tab" data-bs-toggle="tab" data-bs-target="#profile" type="button" role="tab" aria-controls="profile" aria-selected="true">Profile</button>
			</li>

			<li class="nav-item" role="presentation">
				<button class="nav-link" id="rewards-tab" data-bs-toggle="tab" data-bs-target="#rewards" type="button" role="tab" aria-controls="rewards" aria-selected="false">CPE Rewards</button>
			</li>
		</ul>

		<div class="tab-content" id="myTabContent">
			<div class="tab-pane fade show active p-3" id="profile" role="tabpanel" aria-labelledby="profile-tab">
				<h3>Profile Information</h3>
                    <asp:Label class="card-title fw-bold" ID="Label1" runat="server" Text='Student ID: '></asp:Label>
                    <asp:Label class="card-title" ID="txtID" runat="server" Text='<%# Eval("StudentID") %>'></asp:Label>
                    <br />

                    <asp:Label class="card-title fw-bold" ID="Label4" runat="server" Text='Student IC Number: '></asp:Label>
                    <asp:Label class="card-title" ID="txtIC" runat="server" Text='<%# Eval("StudentIC") %>'></asp:Label>
                    <br /><br />

                    <asp:Label class="card-title fw-bold" ID="Label6" runat="server" Text='Name: '></asp:Label>
                    <asp:Label class="card-title" ID="txtName" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>
                    <br /><br />

                    <asp:Label class="card-title fw-bold" ID="Label2" runat="server" Text='Phone Number: '></asp:Label>
                    <asp:Label class="card-title" ID="txtPhone" runat="server" Text='<%# Eval("StudentPhoneNum") %>'></asp:Label>
                    <br /><br />

                    <asp:Label class="card-title fw-bold" ID="Label3" runat="server" Text='Email: '></asp:Label>
                    <asp:Label class="card-title" ID="txtEmail" runat="server" Text='<%# Eval("StudentEmail") %>'></asp:Label>
                    <br /><br />

                    <asp:Label class="card-title fw-bold" ID="Label5" runat="server" Text='Faculty: '></asp:Label>
                    <asp:Label class="card-title" ID="txtFaculty" runat="server" Text='<%# Eval("StudentFaculty") %>'></asp:Label>
                    <br /><br />
				    <%--<asp:LinkButton CssClass="btn btn-sm btn-primary" ID="edit" CommandName="Edit" OnCommand="editInfo" CommandArgument='<%# Eval("CPECode") %>' runat="server">Add To Cart</asp:LinkButton>--%>

			</div>

			<%--Rewards part --%>
			<div class="tab-pane fade p-3" id="rewards" role="tabpanel" aria-labelledby="rewards-tab">
				<h3>CPE Course Completed</h3>

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
    </div>
</asp:Content>
