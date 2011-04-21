<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sheets.aspx.cs" Inherits="WebForms.Sheets" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title></title>
	<script type="text/javascript">
		function pageLoad() {
			//change date on the form which is visible to the user
			if ($get("dateToday") != null && $get("" + $get("dateToday").value) != null) {
				var str = $get("" + $get("dateToday").value).innerHTML;
				var strBrowserDateToday = (new Date()).localeFormat(Sys.CultureInfo.CurrentCulture.dateTimeFormat.ShortDatePattern);
				$get("" + $get("dateToday").value).innerHTML = str.replace("[dateToday]", strBrowserDateToday);
				//set local date on cookie to be read by server
				var today = new Date();
				setCookie("DateCookieY", today.getFullYear(), 1);
				setCookie("DateCookieM", today.getMonth() + 1, 1);
				setCookie("DateCookieD", today.getDate(), 1);
			}			
		}

		function setCookie(c_name, value, exdays) {
			var exdate = new Date();
			exdate.setDate(exdate.getDate() + exdays);
			var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
			document.cookie = c_name + "=" + c_value;
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
<asp:Panel ID="Panel3" Height="50px" runat="server" style="position:relative">&nbsp;
<%--This panel is only to give some space below the button an the edge of the page.--%>
</asp:Panel>
</asp:Panel>


<asp:Panel ID="Panel2" runat="server" Width="0px" HorizontalAlign="Left" BorderColor="White"
					BorderWidth="60px" Style="border-bottom: 20px;text-align: center;" BackColor="White" 
					Visible="False" Height="0px">
					<br /><br /><br /><br />
						<asp:Label ID="LabelSubmitMessage" runat="server" Text="" Font-Names="sans-serif" Font-Size="16px"></asp:Label>
		</asp:Panel>

</form>

</body>
</html>
