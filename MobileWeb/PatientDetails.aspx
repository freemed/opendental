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
<li><span class="style1">Home: <%Response.Write(pat.HmPhone);%> <%Response.Write(DialLinkHmPhone);%>
	
</span></li>
<li><span class="style1">Work: <%Response.Write(pat.WkPhone);%><%Response.Write(DialLinkWkPhone);%>
</span></li>
<li><span class="style1">Mobile: <%Response.Write(pat.WirelessPhone);%><%Response.Write(DialLinkWirelessPhone);%>
</span></li>
<%--<li><span class="style1">Email: <%Response.Write(EmailString);%> </span></li>--%>
<li><span class="style1">Email: <a href="mailto:hello" class="style2">sebmathew________________seb@siberiacrawlmail.com</a> </span></li>
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
								<%#((OpenDentBusiness.Mobile.Appointmentm)Container.DataItem).ProcDescript%>
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
							<%#((OpenDentBusiness.Mobile.RxPatm)Container.DataItem).RxDate.ToString("MM/dd/yyyy")%>&nbsp;&nbsp;&nbsp;&nbsp;
							<%#((OpenDentBusiness.Mobile.RxPatm)Container.DataItem).Drug%>, <%#((OpenDentBusiness.Mobile.RxPatm)Container.DataItem).Disp%>
						</div>
					</li>
				</ItemTemplate>
			</asp:Repeater>
</ul>

</div>

</body>
</html>
