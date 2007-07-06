<%@ Page Language="C#" MasterPageFile="~/MainModules/Modules.Master" AutoEventWireup="true" CodeBehind="ApptModule.aspx.cs" Inherits="WebApplication.ApptModule" Title="Untitled Page" SmartNavigation="true"%>

<%@ Register Assembly="WebApplication" Namespace="WebApplication.CustomControls"
	TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="../UserControls/ContrApptSheet.ascx" TagName="ContrApptSheet" TagPrefix="uc2" %>

<%@ Register Src="../UserControls/ContrApptSingle.ascx" TagName="ContrApptSingle"	TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<table style="width: 100%; height: 100%" border="0" cellpadding="1" cellspacing="0">
		<tr>
			<td style="position: relative; width: 100%; height: 689px; vertical-align: top; text-align: left;" valign="top">			
				<cc2:StatefullScrollPanel ID="panelSheet" runat="server" Width="100%" height="100%" ScrollBars="Vertical" CssClass="panelsheet">
					&nbsp;<uc2:ContrApptSheet ID="ContrApptSheet2" runat="server" />
				</cc2:StatefullScrollPanel>
			

			</td>
			<td style="vertical-align: top; height: 100%">
				<asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
				
			</td>
		</tr>
	</table>
</asp:Content>
