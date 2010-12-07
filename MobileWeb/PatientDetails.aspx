<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientDetails.aspx.cs" Inherits="MobileWeb.PatientDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
<div id="loggedin"><asp:Literal runat="server" ID="Message"></asp:Literal></div>
<div id="content">
put stuff here <%Response.Write(id);%>
<h2></h2>
<ul class="rounded">
<li> Patient name <%Response.Write(id);%>
</li>
</ul>

<ul class="rounded">
<li><span class="style1">Home:</span>333-555-1234<%Response.Write(id);%> </li>
<li><span class="style1">Work:</span>333-555-1234<%Response.Write(id);%> </li>
<li><span class="style1">Mobile:</span>333-555-1234<%Response.Write(id);%> </li>
<li><span class="style1">Email:</span>333-555-1234@gggg.com<%Response.Write(id);%> </li>
</ul>
<h2>Appointmnets</h2>
<ul class="rounded">
<li class="arrow">
		<div>
			<a linkattib="AppointmentDetails.aspx?id=1" href="#AppointmentDetails">
			<asp:Label ID="Label1" runat="server" Text="Appointmnets1"></asp:Label></a>
		</div>
</li>
<li class="arrow">
		<div>
			<a linkattib="AppointmentDetails.aspx?id=1" href="#AppointmentDetails">
			<asp:Label ID="Label2" runat="server" Text="Appointmnet1"></asp:Label></a>
		</div>
</li>
<li class="arrow">
		<div>
			<a linkattib="AppointmentDetails.aspx?id=1" href="#AppointmentDetails">
			<asp:Label ID="Label3" runat="server" Text="Appointmnet3"></asp:Label></a>
		</div>
</li>


</ul>

<h2>Precriptions</h2>
<ul class="rounded">
<li>
		<div>
			<a linkattib="" href="#AppointmentDetails">
			<asp:Label ID="Label4" runat="server" Text="Penicillin"></asp:Label></a>
		</div>
</li>
<li>
		<div>
			<a linkattib="" href="#AppointmentDetails">
			<asp:Label ID="Label5" runat="server" Text="Vicodin"></asp:Label></a>
		</div>
</li>
<li>
		<div>
			<a linkattib="" href="#AppointmentDetails">
			<asp:Label ID="Label6" runat="server" Text="Peridex"></asp:Label></a>
		</div>
</li>


</ul>

</div>

</body>
</html>
