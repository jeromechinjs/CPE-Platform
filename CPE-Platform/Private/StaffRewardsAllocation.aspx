<%@ Page Title="Staff Rewards Allocation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StaffRewardsAllocation.aspx.cs" Inherits="CPE_Platform.Private.StaffRewardsAllocation" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>
	<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<script type="text/javascript">
			$(function () {
				$('[id*=lstStudent]').multiselect({
					includeSelectAllOption: true,
					templates: {
						button: '<button type="button" class="multiselect dropdown-toggle" data-bs-toggle="dropdown"><span class="multiselect-selected-text"></span></button>'
					}
				});
			});
		</script>
		<style>
			
		</style>
	</head>
	</html>
	<asp:DropDownList ID="CPECourse_DropDown" runat="server" class="form-select" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="CPECourse_DropDown_SelectedIndexChanged">
		<asp:ListItem Value="0">Select Course</asp:ListItem>
	</asp:DropDownList>
	<asp:ListBox ID="lstStudent" runat="server" SelectionMode="Multiple" AutoPostBack="true" ></asp:ListBox>
	<asp:Button Text="Submit" runat="server" OnClick="Submit" />

</asp:Content>
