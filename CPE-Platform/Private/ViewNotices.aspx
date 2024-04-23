<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="ViewNotices.aspx.cs" Inherits="CPE_Platform.Private.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	 <head>
		<link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
		<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
		<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
	</head>



	<div class="container p-3">
		<table id="noticeTable" class="card table table-responsive table-hover">
			<asp:Repeater ID="rptr1" runat="server">
				<HeaderTemplate>
					<tr>
						<th>Notice Title</th>
						<th>Notice Sender</th>
						<th>Notice Date</th>
						<th></th>
					</tr>
				</HeaderTemplate>
				<ItemTemplate>
					<tr class="separator">
						<td><%# Eval("NoticeTitle") %></td>
						<td><%# Eval("NoticeSender") %></td>
						<td><%# Eval("NoticeDate") %></td>
						<td>
							<asp:LinkButton CssClass="btn btn-sm btn-primary" ID="btnView" CommandName="View Notice" OnCommand="btn_view" CommandArgument='<%#Eval("NoticeID") %>' runat="server">View</asp:LinkButton>
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</table>
	</div>

    <%--modal popup--%>
	<div class="container">
		<div class="modal fade" id="noticeInfoModal" data-backdrop="static" role="dialog">
			<div class=" modal-dialog modal-dailog-centered">
				<div class="modal-content">
					<div class="modal-header">
						<h4 class="modal-title">Notice Details</h4>

						<button type="button" class="close" data-dismiss="modal">&times;</button>
					</div>
					<div class="modal-body">
                        <asp:Label class="card-title fw-bold" ID="txtNoticeTitle" runat="server" Text='<%# Eval("NoticeTitle") %>'></asp:Label>
                        <br /><br />

                        <asp:Label class="card-title" ID="txtNoticeDesc" runat="server" Text='<%# Eval("NoticeDesc") %>'></asp:Label>
                        <br /><br />
					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-sm btn-outline-danger" data-dismiss="modal">Close</button>
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
