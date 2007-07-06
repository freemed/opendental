<%@ Page Language="C#" MasterPageFile="~/MainModules/Modules.Master" AutoEventWireup="true" CodeBehind="ApptModule.aspx.cs" Inherits="WebApplication.ApptModule" Title="Untitled Page" %>

<%@ Register Src="../UserControls/ContrApptSheet.ascx" TagName="ContrApptSheet" TagPrefix="uc2" %>

<%@ Register Src="../UserControls/ContrApptSingle.ascx" TagName="ContrApptSingle"	TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<table style="width: 100%; height: 100%" border="0" cellpadding="1" cellspacing="0">
		<tr>
			<td style="position: relative; width: 100%; height: 689px; vertical-align: top; text-align: left;" valign="top">			
				<asp:Panel ID="panelSheet" CssClass="apptsheet" runat="server" Height="100%" Width="100%" Wrap="False" BackColor="White" ScrollBars="Vertical" >
					<uc2:ContrApptSheet ID="ContrApptSheet2" runat="server" EnableTheming="true" EnableViewState="true" />
				</asp:Panel>
			</td>
			<td style="vertical-align: top; height: 100%">
				<asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
			</td>
		</tr>
	</table>
</asp:Content>
