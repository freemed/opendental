<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientDetails.aspx.cs" Inherits="MobileWeb.PatientDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Patient</title>
</head>
<body>
<div id="loggedin"><asp:Literal runat="server" ID="Message"></asp:Literal></div>
<div id="content">
<h2></h2>
<ul>
<li> 
<span class="style1">
<%Response.Write(pat.LName + " "+pat.MiddleI+" "+pat.FName);%><br />
<%Response.Write(pat.Birthdate.ToShortDateString()+" ("+pat.Age+")");%>

</span>
</li>
</ul>

<ul class="contact">
<li><span class="style1">Home: <%Response.Write(pat.HmPhone);%> 
	&nbsp;&nbsp;&nbsp;<a href="tel:<%Response.Write(pat.HmPhone);%>" class="style2">dial</a>
</span></li>
<li><span class="style1">Work: <%Response.Write(pat.WkPhone);%>
	&nbsp;&nbsp;&nbsp;<a href="tel:<%Response.Write(pat.WkPhone);%>" class="style2">dial</a>
</span></li>
<li><span class="style1">Mobile: <%Response.Write(pat.WirelessPhone);%>
	&nbsp;&nbsp;&nbsp;<a href="tel:<%Response.Write(pat.WirelessPhone);%>" class="style2">dial</a>
</span></li>
<li><span class="style1">Email: <%Response.Write(pat.Email);%> </span></li>
</ul>
<h2>Appointments</h2>

<ul>
			<asp:Repeater ID="Repeater1" runat="server">
				<ItemTemplate>
					<li class="arrow style1">
						<div class="elladjust">
							<a linkattib="AppointmentDetails.aspx?AptNum=<%#((OpenDentBusiness.Mobile.Appointmentm)Container.DataItem).AptNum %>"
								href="#AppointmentDetails">
								<%#((OpenDentBusiness.Mobile.Appointmentm)Container.DataItem).AptDateTime.ToString("MM/dd/yyyy")%>&nbsp;&nbsp;&nbsp;&nbsp;
								<%#((OpenDentBusiness.Mobile.Appointmentm)Container.DataItem).Note%>
								</a>
						</div>
					</li>
				</ItemTemplate>
			</asp:Repeater>
</ul>

<h2>Prescriptions</h2>
<ul>
			<asp:Repeater ID="Repeater2" runat="server">
				<ItemTemplate>
					<li class="style1">
						<div class="elladjust">
							<%#((OpenDentBusiness.Mobile.RxPatm)Container.DataItem).Drug%>
						</div>
					</li>
				</ItemTemplate>
			</asp:Repeater>
</ul>

</div>

</body>
</html>
