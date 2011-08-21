<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppointmentImage.aspx.cs" Inherits="MobileWeb.AppointmentImage" %>
<%@ Register Assembly="Microsoft.Web.GeneratedImage" Namespace="Microsoft.Web" TagPrefix="cc1" %>
<%@ Import namespace="OpenDentBusiness.Mobile" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Appointment</title>
	
</head>
<body>
<div id="loggedin"><asp:Literal runat="server" ID="Message"></asp:Literal></div>
<div id="content">
<div class="styleError">  
				 <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>
</div>
		<div class="datenavigation">
			<a id="previousApImage" linkattib="AppointmentImage.aspx?year=<%Response.Write(PreviousDateYear);%>&month=<%Response.Write(PreviousDateMonth);%>&day=<%Response.Write(PreviousDateDay);%>"
				href="#"><img src="css/themes/apple/img/listArrowSelFlipped.png" style="float:left;margin-top:4px;" /></a> <a class="button" id="datepickerbutton" href="#">View</a>
			<asp:Label ID="DayLabel" runat="server" class="datestring" Text=""></asp:Label>
			<a id="nextApImage" linkattib="AppointmentImage.aspx?year=<%Response.Write(NextDateYear);%>&month=<%Response.Write(NextDateMonth);%>&day=<%Response.Write(NextDateDay);%>"
				href="#"><img src="css/themes/apple/img/listArrowSel.png" style="float:right;margin-top:4px" /></a>
		</div>
		<br /><br /><br />


<div id="bd">	
<img src="app.jpg" style=" border: thick solid Green;" width="310px" height="280px" />
</div>

<%--	<asp:Image ID="Image1" runat="server" />--%>
<%--	<map name="immap" id="immap" style="position: relative;">
	<area shape="rect" coords="133, 46, 183, 94" 
			onclick="areaClicked('AppointmentDetails.aspx?AptNum=22')" alt="box1__"  />
	<area shape="rect" coords="272, 84, 369, 172" onclick="areaClicked('AppointmentDetails.aspx?AptNum=22')" alt="box1__"  />
	<area shape="rect" coords="375, 557, 482, 622" onclick="areaClicked('AppointmentDetails.aspx?AptNum=23')" alt="box2__"  />
	</map>--%>




<%--
<cc1:GeneratedImage ID="GeneratedImage1" runat="server" 
	ImageHandlerUrl="~/ImageHandler1.ashx">
</cc1:GeneratedImage>--%>

</div>
</body>
</html>
