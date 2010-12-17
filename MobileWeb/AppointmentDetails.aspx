<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppointmentDetails.aspx.cs" Inherits="MobileWeb.AppointmentDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
<div id="loggedin"><asp:Literal runat="server" ID="Message"></asp:Literal></div>
<div id="content">

	<ul>
		<li class="arrow style2">
		<div>
			<a linkattib="PatientDetails.aspx?id=<%Response.Write(id);%>" href="#PatientDetails">
			<asp:Label ID="Label1" runat="server" Text="Patient name <%Response.Write(id);%>Patient name Patient name Patient name Patient name"></asp:Label></a>
		</div>
		</li>
	</ul>


<ul>
<li> <span class="style2">12/05/2010 Monday <br />
10:30 a.m, 90 min.<br />
hhhhhhhhhhhh<br />
asdasddddddddd<br />
ffffffffff
</span>
</li>
</ul>

</div>

</body>
</html>
