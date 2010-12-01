<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppointmentList.aspx.cs" Inherits="MobileWeb.AppointmentList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
<div id="loggedin"><asp:Literal runat="server" ID="Message"></asp:Literal></div>
<div id="content">
	<ul class="edgetoedge">
		<asp:Repeater ID="Repeater1" runat="server">
		<ItemTemplate>
		<li class="arrow">
		<div>
			<a linkattib="AppointmentDetails.aspx?id=<%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>" href="#AppointmentDetails">
			<asp:Label ID="Label1" runat="server" Text="<%#Container.DataItem %>"></asp:Label></a>
		</div>
		</li>
		</ItemTemplate>
		</asp:Repeater>
	</ul>
</div>
</body>
</html>
