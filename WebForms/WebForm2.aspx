<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebForms.WebForm2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title></title>
	<script type="text/javascript">
		function pageLoad() {
		}		
	</script>
</head>
<body id="bodytag" runat="server">
<form id="form1" runat="server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
<asp:Panel ID="Panel1" runat="server">
<div>

	<asp:Button ID="Button1" runat="server" Text="Submit" onclick="Button1_Click" />

</div>

</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="680px" HorizontalAlign="Left" BorderColor="White"
					BorderWidth="60px" Style="border-bottom: 20px;text-align: center;" BackColor="White" 
					Visible="False" Height="300px">
					<br /><br /><br /><br />
						<asp:Label ID="LabelSubmitMessage" runat="server" Text=""></asp:Label>
		</asp:Panel>

</form>

</body>
</html>
