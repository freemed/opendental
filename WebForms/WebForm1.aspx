<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebForm1.aspx.cs" Inherits="WebForms.WebForm1"
	Theme="Theme1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title></title>

	<script type="text/javascript">

		function pageLoad() {
		}
    
	</script>

</head>
<body id="bodytag" runat="server" class="style1">
	<div style="padding-top: 3px;" align="center">
		<form id="form1" runat="server">
		<div>
			<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
		</div>
		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
			<ContentTemplate>
				<asp:Panel ID="Panel1" runat="server" Width="680px" HorizontalAlign="Left" BorderColor="White"
					BorderWidth="60px" Style="border-bottom: 20px;" BackColor="White">
					<asp:Panel ID="PanelHeading1" runat="server" HorizontalAlign="Center">
						<asp:Label ID="LabelHeading1" runat="server" Text="PATIENT INFORMATION" Font-Bold="True"></asp:Label>
						<br />
					</asp:Panel>
					<div align="center">
						<asp:Panel ID="PanelHeading2" runat="server" HorizontalAlign="Center" BorderColor="Black"
							Width="500px" BorderWidth="1px" CssClass="style3">
							<asp:Label ID="LabelHeading2" runat="server" Text="We are pleased to welcome you to our office. Please take a few minutes to fill out this form 
            as completely as you can. if you have any questions we'll be glad to help you."></asp:Label>
						</asp:Panel>
						<br />
					</div>
					<asp:Panel ID="PanelPersonal" runat="server" HorizontalAlign="Center" CssClass="style2"
						BackColor="#B2B2B2">
						<asp:Label ID="LabelPersonal" runat="server" Text="PERSONAL" Font-Bold="True"></asp:Label>
					</asp:Panel>
					<div style="height: 10px">
					</div>

					<%-- validators start --%>
					
					<%-- name validators start --%>
						<asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxLastName" runat="server" ControlToValidate="TextBoxLastName"
						ErrorMessage="Last Name is a required field" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
					<asp:ValidatorCalloutExtender ID="RequiredFieldValidatorTextBoxLastName_ValidatorCalloutExtender"
						runat="server" Enabled="True" TargetControlID="RequiredFieldValidatorTextBoxLastName">
					</asp:ValidatorCalloutExtender>
					
					
					<asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxFirstName" runat="server" ControlToValidate="TextBoxFirstName"
						ErrorMessage="First Name is a required field" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
					<asp:ValidatorCalloutExtender ID="RequiredFieldValidatorTextBoxFirstName_ValidatorCalloutExtender"
						runat="server" Enabled="True" TargetControlID="RequiredFieldValidatorTextBoxFirstName">
					</asp:ValidatorCalloutExtender>
					
					<%-- name validators end --%>
					
					<%-- DOB validators start --%>
					<asp:CompareValidator ID="CompareValidatorDOB" runat="server" ErrorMessage="Invalid Date of Birth."
						ControlToValidate="TextBoxBirthdate" Type="Date" Display="None" Operator="DataTypeCheck">
					</asp:CompareValidator>
					
					<asp:RequiredFieldValidator ID="RequiredFieldValidatorDOB" runat="server" ControlToValidate="TextBoxBirthdate"
						ErrorMessage="Birth date is required" Display="None"></asp:RequiredFieldValidator>
					
					<asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidatorDOB"
						Enabled="true">
					</asp:ValidatorCalloutExtender>
					
					<asp:ValidatorCalloutExtender ID="valCalloutDOB" runat="server" TargetControlID="CompareValidatorDOB"
						Enabled="true">
					</asp:ValidatorCalloutExtender>
					
					<%-- DOB validators end --%>
					
					
					<%-- Email validators start --%>
					<asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxEmail" runat="server" ControlToValidate="TextBoxEmail"
						ErrorMessage="Email is a required field" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
					<asp:ValidatorCalloutExtender ID="RequiredFieldValidatorTextBoxEmail_ValidatorCalloutExtender"
						runat="server" Enabled="True" TargetControlID="RequiredFieldValidatorTextBoxEmail">
					</asp:ValidatorCalloutExtender>
					
					
					<%-- Email validators end --%>
					
					<%-- validators end --%>
					<asp:Label ID="LabelName" runat="server" Text="Name" Width="50px"></asp:Label>
					<asp:TextBox ID="TextBoxLastName" runat="server" Width="140px" CssClass="textboxstyle"></asp:TextBox>
					<span style="margin-left: 10px">
						<asp:TextBox ID="TextBoxFirstName" runat="server" Width="140px" CssClass="textboxstyle"></asp:TextBox></span>
					<span style="margin-left: 10px">
						<asp:TextBox ID="TextBoxMI" runat="server" Width="140px" CssClass="textboxstyle"></asp:TextBox></span>
					<span style="margin-left: 10px">
						<asp:TextBox ID="TextBoxPreferred" runat="server" Width="140px" CssClass="textboxstyle"></asp:TextBox></span>
					<br />
					<div style="font-size: 11px; position: relative; top: -2px; height: 13px">
						<span style="margin-left: 100px">&nbsp;</span>
						<asp:Label ID="LabelLast" runat="server" Text="Last" Width="160px"></asp:Label>
						<asp:Label ID="LabelFirst" runat="server" Text="First" Width="165px"></asp:Label>
						<asp:Label ID="LabelMI" runat="server" Text="MI" Width="140px"></asp:Label>
						<asp:Label ID="LabelPreferred" runat="server" Text="Preferred"></asp:Label>
					</div>
					<asp:Label ID="LabelBirthdate" runat="server" Text="Birthdate" Width="50px"></asp:Label>
					<%--
                   <asp:MaskedEditExtender ID="TextBoxBirthdate_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                        Enabled="True" Mask="99/99/9999" TargetControlID="TextBoxBirthdate" UserDateFormat="MonthDayYear">
                    </asp:MaskedEditExtender>
                    
                    <asp:CalendarExtender ID="TextBoxBirthdate_CalendarExtender" runat="server" Enabled="True"
                        Format="MM/dd/yyyy" TargetControlID="TextBoxBirthdate" Animated="true">
                    </asp:CalendarExtender>
                    

                    --%>
					<asp:TextBox ID="TextBoxBirthdate" runat="server" Width="140px" CssClass="textboxstyle"></asp:TextBox>
					<asp:CalendarExtender ID="calendarExtender" runat="server" Animated="true" PopupPosition="Right"
						Format="MM/dd/yyyy" TargetControlID="TextBoxBirthdate" DefaultView="Years" BehaviorID="CalendarBehaviorID"
						OnClientDateSelectionChanged="setCalendarModeToYears">
					</asp:CalendarExtender>

					<script type="text/javascript">
						function setCalendarModeToYears() {

							var CalendarBehavior = $find("CalendarBehaviorID");
							CalendarBehavior._switchMode("years", true);
						}
					</script>

				
					<span style="margin-left: 15px">&nbsp;</span>
					<asp:Label ID="LabelSS" runat="server" Text="SS#:" Width="30px"></asp:Label>
					<asp:TextBox ID="TextBoxSS" runat="server" Width="135px" CssClass="textboxstyle"></asp:TextBox>
					<span style="margin-left: 20px">&nbsp;</span>
					<asp:Label ID="LabelGender" runat="server" Text="Gender:" Width="40px"></asp:Label>
					<asp:RadioButtonList ID="RadioButtonListGender" runat="server" RepeatDirection="Horizontal"
						RepeatLayout="Flow">
						<asp:ListItem>M</asp:ListItem>
						<asp:ListItem>F</asp:ListItem>
					</asp:RadioButtonList>
					<span style="margin-left: 45px">&nbsp;</span>
					<asp:Label ID="LabelMarried" runat="server" Text="Married:" Width="40px"></asp:Label>
					<asp:RadioButtonList ID="RadioButtonListMarried" runat="server" RepeatDirection="Horizontal"
						RepeatLayout="Flow">
						<asp:ListItem>Y</asp:ListItem>
						<asp:ListItem>N</asp:ListItem>
					</asp:RadioButtonList>
					<br />
					<asp:Label ID="LabelWorkPhone" runat="server" Text="Work Phone"></asp:Label>
					<asp:TextBox ID="TextBoxWorkPhone" runat="server" Width="125px" CssClass="textboxstyle"></asp:TextBox>
					<span style="margin-left: 5px">&nbsp;</span>
					<asp:Label ID="LabelWirelessPhone" runat="server" Text="Wireless Phone"></asp:Label>
					<asp:TextBox ID="TextBoxWirelessPhone" runat="server" Width="125px" CssClass="textboxstyle"></asp:TextBox>
					<span style="margin-left: 5px">&nbsp;</span>
					<asp:Label ID="LabelWirelessCarrier" runat="server" Text="Wireless Carrier"></asp:Label>
					<asp:TextBox ID="TextBoxWirelessCarrier" runat="server" Width="125px" CssClass="textboxstyle"></asp:TextBox>
					<br />
					<asp:Label ID="LabelEmail" runat="server" Text="Email"></asp:Label>
					<asp:TextBox ID="TextBoxEmail" runat="server" CssClass="textboxstyle" Width="300px"></asp:TextBox>
					<br />
					<asp:Label ID="LabelMethodContact" runat="server" Text="Preferred contact method" Width="300px"></asp:Label>
					<asp:RadioButtonList ID="RadioButtonListMethodContact" runat="server" RepeatDirection="Horizontal"
						RepeatLayout="Flow">
						<asp:ListItem Value="HmPhone" Text="HmPhone&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="WkPhone" Text="WkPhone&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="WirelessPh" Text="WirelessPh&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="Email" Text="Email&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
					</asp:RadioButtonList>
					<br />
					<asp:Label ID="LabelMethodConf" runat="server" Text="Preferred contact method for confirmations"
						Width="300px"></asp:Label>
					<asp:RadioButtonList ID="RadioButtonListMethodConf" runat="server" RepeatDirection="Horizontal"
						RepeatLayout="Flow">
						<asp:ListItem Value="HmPhone" Text="HmPhone&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="WkPhone" Text="WkPhone&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="WirelessPh" Text="WirelessPh&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="Email" Text="Email&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
					</asp:RadioButtonList>
					<br />
					<asp:Label ID="LabelMethodRecall" runat="server" Text="Preferred contact method for recall"
						Width="300px"></asp:Label>
					<asp:RadioButtonList ID="RadioButtonListMethodRecall" runat="server" RepeatDirection="Horizontal"
						RepeatLayout="Flow">
						<asp:ListItem Value="HmPhone" Text="HmPhone&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="WkPhone" Text="WkPhone&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="WirelessPh" Text="WirelessPh&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="Email" Text="Email&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
					</asp:RadioButtonList>
					<br />
					<asp:Label ID="LabelStudentStatus" runat="server" Text="Student status if dependent over 19 (for ins)"
						Width="300px"></asp:Label>
					<asp:RadioButtonList ID="RadioButtonListStudentStatus" runat="server" RepeatDirection="Horizontal"
						RepeatLayout="Flow">
						<asp:ListItem Value="Nonstudent" Text="Nonstudent&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="Fulltime" Text="Fulltime&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="Parttime" Text="Parttime&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
					</asp:RadioButtonList>
					<br />
					<asp:Label ID="LabelHear" runat="server" Text="How did you hear about us?"></asp:Label>
					<br />
					<asp:TextBox ID="TextBoxHear" runat="server" Width="670px" CssClass="textboxstyle"></asp:TextBox>
					<br />
					<asp:Label ID="LabelReferred" runat="server" Text="(If someone referred you here, please write down their name so that we can thank them.)"></asp:Label>
					<br />
					<asp:Panel ID="Panel4" runat="server" HorizontalAlign="Center" CssClass="style2">
						<asp:Label ID="Label2" runat="server" Text="ADDRESS AND HOME PHONE" Font-Bold="True"></asp:Label>
						<br />
					</asp:Panel>
					<asp:Label ID="LabelWholeFamily" runat="server" Text="Check box if same for entire family"></asp:Label>
					<asp:CheckBox ID="CheckBoxWholeFamily" runat="server" />
					<br />
					<asp:Label ID="LabelAddress1" runat="server" Text="Address "></asp:Label>
					<asp:TextBox ID="TextBoxAddress1" runat="server" Width="400px" CssClass="textboxstyle"></asp:TextBox>
					<br />
					<asp:Label ID="LabelAddress2" runat="server" Text="Address2 "></asp:Label>
					<asp:TextBox ID="TextBoxAddress2" runat="server" Width="400px" CssClass="textboxstyle"></asp:TextBox>
					<br />
					<asp:Label ID="LabelCity" runat="server" Text="City "></asp:Label>
					<asp:TextBox ID="TextBoxCity" runat="server" CssClass="textboxstyle"></asp:TextBox>
					<span style="margin-left: 15px">&nbsp;</span>
					<asp:Label ID="LabelState" runat="server" Text="State "></asp:Label>
					<asp:TextBox ID="TextBoxState" runat="server" CssClass="textboxstyle"></asp:TextBox>
					<span style="margin-left: 15px">&nbsp;</span>
					<asp:Label ID="LabelZip" runat="server" Text="Zip "></asp:Label>
					<asp:TextBox ID="TextBoxZip" runat="server" CssClass="textboxstyle"></asp:TextBox>
					<br />
					<asp:Label ID="LabelHomePhone" runat="server" Text="Home Phone "></asp:Label>
					<asp:TextBox ID="TextBoxHomePhone" runat="server" CssClass="textboxstyle"></asp:TextBox>
					<div style="height: 5px">
					</div>
					<asp:Panel ID="PanelInsurancePolicy1" runat="server" HorizontalAlign="Center" CssClass="style2">
						<asp:Label ID="LabelInsurancePolicy1" runat="server" Text="INSURANCE POLICY 1" Font-Bold="True"></asp:Label>
						<br />
					</asp:Panel>
					<asp:Label ID="LabelPolicy1Relationship" runat="server" Text="Your relationship to subscriber: "></asp:Label>
					<asp:RadioButtonList ID="RadioButtonListPolicy1Relationship" runat="server" RepeatDirection="Horizontal"
						RepeatLayout="Flow">
						<asp:ListItem Value="Self" Text="Self&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="Spouse" Text="Spouse&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="Child" Text="Child&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
					</asp:RadioButtonList>
					<br />
					<asp:Label ID="LabelPolicy1SubscriberName" runat="server" Text="Subscriber Name "></asp:Label>
					<asp:TextBox ID="TextBoxPolicy1SubscriberName" runat="server" CssClass="textboxstyle"></asp:TextBox>
					<span style="margin-left: 15px">&nbsp;</span>
					<asp:Label ID="LabelPolicy1SubscriberID" runat="server" Text="Subscriber ID # "></asp:Label>
					<asp:TextBox ID="TextBoxPolicy1SubscriberID" runat="server" CssClass="textboxstyle"></asp:TextBox>
					<br />
					<asp:Label ID="LabelPolicy1InsuranceCompany" runat="server" Text="Insurance Company "></asp:Label>
					<asp:TextBox ID="TextBoxPolicy1InsuranceCompany" runat="server" CssClass="textboxstyle"
						Width="300px"></asp:TextBox>
					<span style="margin-left: 15px">&nbsp;</span>
					<asp:Label ID="LabelPolicy1Phone" runat="server" Text="Phone "></asp:Label>
					<asp:TextBox ID="TextBoxPolicy1Phone" runat="server" CssClass="textboxstyle"></asp:TextBox>
					<br />
					<asp:Label ID="LabelPolicy1Employer" runat="server" Text="Employer "></asp:Label>
					<asp:TextBox ID="TextBoxPolicy1Employer" runat="server" Width="135px" CssClass="textboxstyle"></asp:TextBox>
					<span style="margin-left: 5px">&nbsp;</span>
					<asp:Label ID="LabelPolicy1GroupName" runat="server" Text="Group Name "></asp:Label>
					<asp:TextBox ID="TextBoxPolicy1GroupName" runat="server" Width="135px" CssClass="textboxstyle"></asp:TextBox>
					<span style="margin-left: 5px">&nbsp;</span>
					<asp:Label ID="LabelPolicy1GroupNumber" runat="server" Text="Group # "></asp:Label>
					<asp:TextBox ID="TextBoxPolicy1GroupNumber" runat="server" Width="135px" CssClass="textboxstyle"></asp:TextBox>
					<br />
					<asp:Label ID="LabelPolicy1Present" runat="server" Text="Please Present card to Receptionist"></asp:Label>
					<asp:Panel ID="PanelInsurancePolicy2" runat="server" HorizontalAlign="Center" CssClass="style2">
						<asp:Label ID="LabelInsurancePolicy2" runat="server" Text="INSURANCE POLICY 2" Font-Bold="True"></asp:Label>
					</asp:Panel>
					<asp:Label ID="Label1" runat="server" Text="Your relationship to subscriber: "></asp:Label>
					<asp:RadioButtonList ID="RadioButtonListPolicy2Relationship" runat="server" RepeatDirection="Horizontal"
						RepeatLayout="Flow">
						<asp:ListItem Value="Self" Text="Self&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="Spouse" Text="Spouse&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
						<asp:ListItem Value="Child" Text="Child&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
					</asp:RadioButtonList>
					<br />
					<asp:Label ID="LabelPolicy2SubscriberName" runat="server" Text="Subscriber Name "></asp:Label>
					<asp:TextBox ID="TextBoxPolicy2SubscriberName" runat="server" CssClass="textboxstyle"></asp:TextBox>
					<span style="margin-left: 15px">&nbsp;</span>
					<asp:Label ID="LabelPolicy2SubscriberID" runat="server" Text="Subscriber ID # "></asp:Label>
					<asp:TextBox ID="TextBoxPolicy2SubscriberID" runat="server" CssClass="textboxstyle"></asp:TextBox>
					<br />
					<asp:Label ID="LabelPolicy2InsuranceCompany" runat="server" Text="Insurance Company "></asp:Label>
					<asp:TextBox ID="TextBoxPolicy2InsuranceCompany" runat="server" CssClass="textboxstyle"
						Width="300px"></asp:TextBox>
					<span style="margin-left: 15px">&nbsp;</span>
					<asp:Label ID="LabelPolicy2Phone" runat="server" Text="Phone "></asp:Label>
					<asp:TextBox ID="TextBoxPolicy2Phone" runat="server" CssClass="textboxstyle"></asp:TextBox>
					<br />
					<asp:Label ID="LabelPolicy2Employer" runat="server" Text="Employer "></asp:Label>
					<asp:TextBox ID="TextBoxPolicy2Employer" runat="server" Width="135px" CssClass="textboxstyle"></asp:TextBox>
					<span style="margin-left: 5px">&nbsp;</span>
					<asp:Label ID="LabelPolicy2GroupName" runat="server" Text="Group Name "></asp:Label>
					<asp:TextBox ID="TextBoxPolicy2GroupName" runat="server" Width="135px" CssClass="textboxstyle"></asp:TextBox>
					<span style="margin-left: 5px">&nbsp;</span>
					<asp:Label ID="LabelPolicy2GroupNumber" runat="server" Text="Group # "></asp:Label>
					<asp:TextBox ID="TextBoxPolicy2GroupNumber" runat="server" Width="135px" CssClass="textboxstyle"></asp:TextBox>
					<br />
					<asp:Label ID="LabelComments" runat="server" Text="Comments:"></asp:Label>
					<br />
					<asp:TextBox ID="TextBoxComments" runat="server" TextMode="MultiLine" Rows="3" Width="670px"
						CssClass="textboxstyle" Height="36px"></asp:TextBox>
					
					<br />
					<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
						<ProgressTemplate>
							Your information is being submitted...
						</ProgressTemplate>
					</asp:UpdateProgress>
					<asp:Panel ID="Panel3" runat="server" Style="text-align: right; font-weight: bold;">
						<asp:Button ID="Submit" runat="server" Text="SUBMIT" CssClass="buttonstyle" OnClick="Submit_Click" />
					</asp:Panel>
					<br />
					
				</asp:Panel>
				<asp:Panel ID="Panel2" runat="server" Width="680px" HorizontalAlign="Left" BorderColor="White"
					BorderWidth="60px" Style="border-bottom: 20px;text-align: center;" BackColor="White" 
					Visible="False" Height="300px">
					<br /><br /><br /><br />
						<asp:Label ID="LabelSubmitMessage" runat="server" Text=""></asp:Label>
		</asp:Panel>
			</ContentTemplate>
		</asp:UpdatePanel>
		</form>
		
	</div>
</body>
</html>
