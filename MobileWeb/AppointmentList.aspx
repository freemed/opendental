<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppointmentList.aspx.cs"
	Inherits="MobileWeb.AppointmentList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<div id="loggedin"><asp:Literal runat="server" ID="Message"></asp:Literal></div>
	<div id="content">
	<div class="styleError">  
				 <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>
	</div>
		<div class="datenavigation">
			<a id="previous" linkattib="AppointmentList.aspx?year=<%Response.Write(PreviousDateYear);%>&month=<%Response.Write(PreviousDateMonth);%>&day=<%Response.Write(PreviousDateDay);%>"
				href="#"><img src="css/themes/apple/img/listArrowSelFlipped.png" style="float:left;margin-top:4px;" /></a> <a class="button" id="today" href="#">&nbsp;.&nbsp;.&nbsp;.&nbsp;</a>
			<asp:Label ID="DayLabel" runat="server" class="datestring" Text=""></asp:Label>
			<a id="next" linkattib="AppointmentList.aspx?year=<%Response.Write(NextDateYear);%>&month=<%Response.Write(NextDateMonth);%>&day=<%Response.Write(NextDateDay);%>"
				href="#"><img src="css/themes/apple/img/listArrowSel.png" style="float:right;margin-top:4px" /></a>
		</div>
		<ul>
			<asp:Repeater ID="Repeater1" runat="server">
				<ItemTemplate>
					<li class="arrow style1" style="background-color:<%#GetProviderColor((OpenDentBusiness.Mobile.Appointmentm)Container.DataItem)%>;">
						<div>
							<a linkattib="AppointmentDetails.aspx?AptNum=<%#((OpenDentBusiness.Mobile.Appointmentm)Container.DataItem).AptNum %>"
								href="#AppointmentDetails">
								<div style="float:left; width:30%;">
								<%#((OpenDentBusiness.Mobile.Appointmentm)Container.DataItem).AptDateTime.ToString("hh:mm tt")%>&nbsp;&nbsp;&nbsp;&nbsp;
								</div>
								<div style="float:left; width:auto;"><%#GetPatientName(((OpenDentBusiness.Mobile.Appointmentm)Container.DataItem).PatNum)%> <br />
								<%#((OpenDentBusiness.Mobile.Appointmentm)Container.DataItem).ProcDescript%>
								</div>
								</a>
								
						</div>
					</li>
				</ItemTemplate>
			</asp:Repeater>
		</ul>
	</div>
</body>
</html>
