<%@ Page Language="C#" MasterPageFile="~/MainModules/Modules.Master" AutoEventWireup="true" CodeBehind="ApptModule.aspx.cs" Inherits="WebApplication.ApptModule" Title="Untitled Page" %>

<%@ Register Src="../UserControls/ContrApptSheet.ascx" TagName="ContrApptSheet" TagPrefix="uc2" %>

<%@ Register Src="../UserControls/ContrApptSingle.ascx" TagName="ContrApptSingle"	TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<table style="width: 100%; height: 100%" border="0" cellpadding="1" cellspacing="0">
		<tr>
			<td style="width: 100%; height: 689px">
				&nbsp;
				
				<asp:Panel ID="ContrApptSheet2" runat="server" Height="700px" Width="600px" Wrap="False">
					
				</asp:Panel>
			</td>
			<td style="vertical-align: top; height: 689px">
				<asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
			</td>
		</tr>
	</table>
</asp:Content>
