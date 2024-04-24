<%@ Page Title="Student Profile" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="StudentProfileManagement.aspx.cs" Inherits="CPE_Platform.StudentProfileManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <head>
		<link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
		<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
		<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
	</head>

    <div class="container p-3">
		<ul class="nav nav-tabs" id="myTab" role="tablist">
			<!-- tab 1 -->
			<li class="nav-item" role="presentation">
				<button class="nav-link active" id="profileTab" data-bs-toggle="tab" data-bs-target="#profile" type="button" role="tab" aria-controls="profile" aria-selected="true">Profile</button>
			</li>

			<!-- tab 2 -->
			<li class="nav-item" role="presentation">
				<button class="nav-link" id="resetPasswordTab" data-bs-toggle="tab" data-bs-target="#resetPassword" type="button" role="tab" aria-controls="rewards" aria-selected="false">Reset Password</button>
			</li>

			<!-- tab 3 -->
			<li class="nav-item" role="presentation">
				<button class="nav-link" id="rewardsTab" data-bs-toggle="tab" data-bs-target="#rewards" type="button" role="tab" aria-controls="rewards" aria-selected="false">CPE Rewards</button>
			</li>
		</ul>

		<div class="tab-content" id="myTabContent">

			<!-- tab 1 -->
			<div class="tab-pane fade show active p-3" id="profile" role="tabpanel" aria-labelledby="profile-tab">
				<h3 class="mb-5">Profile Information</h3>

				<asp:Label class="card-title fw-bold" ID="Label1" runat="server" Text='Student ID: '></asp:Label>
				<asp:Label class="card-title" ID="txtID" runat="server" Text='<%# Eval("StudentID") %>'></asp:Label>
				<br /><br />

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
                    
				<asp:LinkButton CssClass="btn btn-sm btn-primary" ID="btnEdit" CommandName="Edit Profile Information" OnCommand="edit_info" runat="server">Edit Info<span class="fa fa-edit"></span></asp:LinkButton>
				
				
				<%--modal popup--%>
				<div class="container">
					<div class="modal fade" id="updateDetails" data-backdrop="static" role="dialog">
						<div class=" modal-dialog modal-dailog-centered">
							<div class="modal-content">
								<div class="modal-header">
									<h4 class="modal-title">Edit Profile Information</h4>

									<button type="button" class="close" data-dismiss="modal">&times;</button>
								</div>
								<div class="modal-body">
									<asp:Label ID="lblmsg" Text="" ForeColor="IndianRed" runat="server" /><br />

									<label>Student ID</label>
									<asp:TextBox ID="modaltxtID" CssClass="form-control" placeholder="Student ID" runat="server" />
								
									<label>Phone Number</label>
									<asp:TextBox ID="modaltxtPhone" CssClass="form-control" placeholder="Phone Number" runat="server" />
								</div>
								<div class="modal-footer">
									<button type="button" class="btn btn-outline-danger" data-dismiss="modal">Close</button>
									<asp:Button ID="btnsave" CssClass="btn btn-outline-success" OnClick="btnsave_Click" Text="Save" runat="server" />
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>

			<!-- tab 2 -->
			<div class="tab-pane fade p-3" id="resetPassword" role="tabpanel" aria-labelledby="resetPassword-tab">
				<h3 class="mb-5">Reset Password</h3>

<%--				<asp:Label id="testlbl" runat="server">Old Password</asp:Label>--%>
				<label>Old Password</label>
				<asp:TextBox ID="oldPassword" CssClass="form-control mb-3" placeholder="Student ID" runat="server" />

				<label>New Password</label>
				<asp:TextBox ID="newPassword" CssClass="form-control mb-3" placeholder="Student ID" runat="server" />

				<label>Confirm Password</label>
				<asp:TextBox ID="confirmPassword" CssClass="form-control mb-3" placeholder="Student ID" runat="server" />

				<asp:Button ID="changePassword" CssClass="btn btn-sm btn-primary" OnClick="change_password" Text="Submit" runat="server" />
			</div>	
			
			<!-- tab 3 -->
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

	<!-- Toast Messages -->
	<!-- Profile nformation updated successfully toast message -->
	<div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
		<asp:Panel ID="updateSucess" CssClass="toast hide" role="alert" aria-live="assertive" aria-atomic="true" runat="server">
			<div class="toast-header">
				<img src="src/tarucLogo.png" class="rounded me-2" width="12">
				<strong class="me-auto">CPE Platform</strong>
				<small>Just now</small>
				<button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
			</div>
			<div class="toast-body">
				Profile information sucessfully saved.
			</div>
		</asp:Panel>
	</div>


	<!-- Old password not same as curent password toast message -->
	<div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
		<asp:Panel ID="originalPasswordWrong" CssClass="toast hide" role="alert" aria-live="assertive" aria-atomic="true" runat="server">
			<div class="toast-header">
				<img src="src/tarucLogo.png" class="rounded me-2" width="12">
				<strong class="me-auto">CPE Platform</strong>
				<small>Just now</small>
				<button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
			</div>
			<div class="toast-body">
				Your current password has been typed wrongly. Please try again.
			</div>
		</asp:Panel>
	</div>

	<!-- New password same as old password toast message -->
	<div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
		<asp:Panel ID="passwordSame" CssClass="toast hide" role="alert" aria-live="assertive" aria-atomic="true" runat="server">
			<div class="toast-header">
				<img src="src/tarucLogo.png" class="rounded me-2" width="12">
				<strong class="me-auto">CPE Platform</strong>
				<small>Just now</small>
				<button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
			</div>
			<div class="toast-body">
				Password is same as before. Kindly try a new password.
			</div>
		</asp:Panel>
	</div>

	<!-- New password not same as confirm password toast message -->
	<div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
		<asp:Panel ID="passwordDifferent" CssClass="toast hide" role="alert" aria-live="assertive" aria-atomic="true" runat="server">
			<div class="toast-header">
				<img src="src/tarucLogo.png" class="rounded me-2" width="12">
				<strong class="me-auto">CPE Platform</strong>
				<small>Just now</small>
				<button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
			</div>
			<div class="toast-body">
				New password doesn't match. Please try again.
			</div>
		</asp:Panel>
	</div>

	<!-- Password changed success toast message -->
	<div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
		<asp:Panel ID="passwordChangedSuccess" CssClass="toast hide" role="alert" aria-live="assertive" aria-atomic="true" runat="server">
			<div class="toast-header">
				<img src="src/tarucLogo.png" class="rounded me-2" width="12">
				<strong class="me-auto">CPE Platform</strong>
				<small>Just now</small>
				<button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
			</div>
			<div class="toast-body">
				Password successfully changed.
			</div>
		</asp:Panel>
	</div>
</asp:Panel>
</asp:Content>
