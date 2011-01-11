<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppointmentList.aspx.cs"
	Inherits="MobileWeb.AppointmentList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<div id="loggedin"><asp:Literal runat="server" ID="Message"></asp:Literal></div>
	<div id="content">
		<div class="datenavigation">
			<a class="button previous" linkattib="AppointmentList.aspx?year=<%Response.Write(PreviousDateYear);%>&month=<%Response.Write(PreviousDateMonth);%>&day=<%Response.Write(PreviousDateDay);%>"
				href="#">&nbsp;&nbsp;</a> <a class="button today" href="#">Today</a>
			<asp:Label ID="DayLabel" runat="server" class="datestring" Text=""></asp:Label>
			<a class="button next" linkattib="AppointmentList.aspx?year=<%Response.Write(NextDateYear);%>&month=<%Response.Write(NextDateMonth);%>&day=<%Response.Write(NextDateDay);%>"
				href="#">&nbsp;&nbsp;</a>
		</div>
		<ul>
			<asp:Repeater ID="Repeater1" runat="server">
				<ItemTemplate>
					<li class="arrow style1">
						<div>
							<a linkattib="AppointmentDetails.aspx?AptNum=<%#((OpenDentBusiness.Mobile.Appointmentm)Container.DataItem).AptNum %>"
								href="#AppointmentDetails">
								<%#((OpenDentBusiness.Mobile.Appointmentm)Container.DataItem).AptDateTime.ToString("hh:mm tt")%>&nbsp;&nbsp;&nbsp;&nbsp;
								<%#GetPatientName(((OpenDentBusiness.Mobile.Appointmentm)Container.DataItem).PatNum)%>
								</a>
								
						</div>
					</li>
				</ItemTemplate>
			</asp:Repeater>
		</ul>
	</div>
</body>
</html>
