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
<li> <span class="style2"><%Response.Write(pat.LName + " "+pat.MiddleI +" " + pat.FName  +" " + pat.Birthdate.ToShortDateString());%></span>
</li>
</ul>

<ul class="contact">
<li><span class="style1">Home:</span> <span class="style2"><%Response.Write(pat.HmPhone);%> </span></li>
<li><span class="style1">Work:</span> <span class="style2"><%Response.Write(pat.WkPhone);%> </span></li>
<li><span class="style1">Mobile:</span> <span class="style2"><%Response.Write(pat.WirelessPhone);%> </span></li>
<li><span class="style1">Email:</span> <span class="style2"><%Response.Write(pat.Email);%> </span></li>
</ul>
<h2>Appointments</h2>
<ul>
<li class="arrow style2">
		<div>
			<a linkattib="AppointmentDetails.aspx?id=1" href="#AppointmentDetails">
			<asp:Label ID="Label1" runat="server" Text="Appointment1"></asp:Label></a>
		</div>
</li>
<li class="arrow style2">
		<div>
			<a linkattib="AppointmentDetails.aspx?id=1" href="#AppointmentDetails">
			<asp:Label ID="Label2" runat="server" Text="Appointment2"></asp:Label></a>
		</div>
</li>
<li class="arrow style2">
		<div>
			<a linkattib="AppointmentDetails.aspx?id=1" href="#AppointmentDetails">
			<asp:Label ID="Label3" runat="server" Text="Appointment3"></asp:Label></a>
		</div>
</li>


</ul>

<h2>Prescriptions</h2>
<ul>
<li>
		<div>
		<span class="style2"><asp:Label ID="Label4" runat="server" Text="Penicillin"></asp:Label></span>
		</div>
</li>
<li>
		<div>
		<span class="style2"><asp:Label ID="Label5" runat="server" Text="Vicodin"></asp:Label></span>
		</div>
</li>
<li>
		<div>
		<span class="style2"><asp:Label ID="Label6" runat="server" Text="Peridex"></asp:Label></span>
		</div>
</li>


</ul>

</div>

</body>
</html>
