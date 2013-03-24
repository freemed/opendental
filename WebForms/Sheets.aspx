<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sheets.aspx.cs" Inherits="WebForms.Sheets" Culture="auto"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title></title>
	<script type="text/javascript">
		function pageLoad() {
			setDateToday();
			setDatesInCookies();
		}

		///<summary> The visible date seen by the patient on the webform is set here, if it exists. This text is static is cannot be changed by the patient</summary>
		function setDateToday() {
			// dateToday is a hiddenfield, set from the server code. The date however is not set on the server.
			// The existence of this variable merely indicates that the form contains visible static labels which are DateToday fields
			if ($get("dateToday") != null) {
				var dateTodayIdArray = $get("dateToday").value.split(" ");
				for (var i = 0; i < dateTodayIdArray.length; i++) {
					if ($get("" + dateTodayIdArray[i]) != null) {
						var str = $get("" + dateTodayIdArray[i]).innerHTML;
						var strBrowserDateToday = (new Date()).localeFormat(Sys.CultureInfo.CurrentCulture.dateTimeFormat.ShortDatePattern); // read browser date
						$get("" + dateTodayIdArray[i]).innerHTML = str.replace("[dateToday]", strBrowserDateToday); // set static date text here
					}
				}
			}
		}

		///<summary>Set browser's date in cookies to be read by server</summary>
		function setDatesInCookies() {
			var today=new Date();
			setCookie("DateCookieY", today.getFullYear(), 1);
			setCookie("DateCookieM", today.getMonth() + 1, 1);
			setCookie("DateCookieD", today.getDate(), 1);
		}

		function setCookie(c_name, value, exdays) {
			var exdate=new Date();
			exdate.setDate(exdate.getDate()+exdays);
			var c_value=escape(value)+((exdays==null)? "":"; expires="+exdate.toUTCString());
			document.cookie=c_name+"="+c_value;
		}

		///<summary> this is the one of the client validation methods which is triggered when when the submit button is hit.
		///The client validation method in this case  is tied down to textboxes (and not checkboxes) because one cannot easily do a validation on a checkbox using conventional procedures. 
		///Ultimately however it's the required checkboex that are validated. The information regarding the required checkbox fields are found in hidden variables
		/// See example below. There are 4 hidden variable. The hfAllGroupsList hidden variable holds the ids of the other 3 hidden variable. The 3 hidden variable contain the ids of the checkboxes that are mutually exculsive (only one can be selected at a time)
		/// type="hidden" name="hfAllGroupsList" id="hfAllGroupsList" value="hiddenChkBoxGroupGender hiddenChkBoxGroupPosition hiddenChkBoxGroupSingleCheckbox"
		///input type="hidden" name="hiddenChkBoxGroupGender" id="hiddenChkBoxGroupGender" value="622 628"
		///input type="hidden" name="hiddenChkBoxGroupPosition" id="hiddenChkBoxGroupPosition" value="611 612 615 619 630"
		///type="hidden" name="hiddenChkBoxGroupSingleCheckbox" id="hiddenChkBoxGroupSingleCheckbox" value="613"
		///</summary>
		function CheckCheckBoxes(sender, args) {
			var AllGroupsArray=$get("hfAllGroupsList").value.split(" ");
			args.IsValid=false;
			ChkBoxGroupToBeChecked="";
			for (var i=0;i<AllGroupsArray.length;i++) {
				var ChkBoxArray=$get(AllGroupsArray[i]).value.split(" ");
				for (var j=0;j<ChkBoxArray.length;j++) {
					if (sender.id=="CustomValidatorTextBoxForCheckbox"+ChkBoxArray[j]) { // detect which textbox fired called this function
						ChkBoxGroupToBeChecked=AllGroupsArray[i];
						break;// break out of j loop if a match is found
					}
				}
				if (ChkBoxGroupToBeChecked!="") { // if a match is found
				break; // break out of i loop
				}
			}
			if (ChkBoxGroupToBeChecked=="") {
				args.IsValid=true;
				return;
			}
			var ChkBoxArray=$get(ChkBoxGroupToBeChecked).value.split(" ");
			for (var j=0;j<ChkBoxArray.length;j++) {
				var chk=document.getElementById(ChkBoxArray[j]);
				if (chk.checked) {
					args.IsValid=true;
					break;
				}
			}
		}

	</script>
</head>
<body id="bodytag" runat="server">
<form id="form1" runat="server" defaultbutton="ButtonDisableEnterKey">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"/>
<asp:Panel ID="Panel1" runat="server">
<div>
	<%--This button is used as a dummy, invisible, default button of form1 so that when 
	the user hits the "Enter" key the form is not submitted. This button does nothing when 
	it is clicked or more acurately when the click is called when the "Enter" key is hit--%>
	<asp:Button ID="ButtonDisableEnterKey" runat="server" Text="" style="display:none" />
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
