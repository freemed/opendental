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




<%--<div style="width:700px;background-color:Aqua;-webkit-transform: scale(0.25);-webkit-transform-origin: 0 0;" >
<img src="app.jpg" style=" border: thick solid Green;"  usemap="#immap1" />

<map name="immap1" id="immap1" style="position: relative;">
	<area shape="rect" coords="0, 1, 16, 17" 
			onclick="areaClicked('AppointmentDetails.aspx?AptNum=22')" alt="box1__"  />
	<area shape="rect" coords="272, 84, 369, 172" onclick="areaClicked('AppointmentDetails.aspx?AptNum=22')" alt="box1__"  />
	<area shape="rect" coords="372, 558, 479, 623" 
		onclick="areaClicked('AppointmentDetails.aspx?AptNum=23')" alt="box2__"  />
	</map>


</div>--%>
 
	<div id="divappimage">
	<br /><br /><br /><br /><br />
<cc1:GeneratedImage ID="GeneratedImage1" runat="server" 
	ImageHandlerUrl="~/ImageHandler1.ashx" usemap="#immap">
</cc1:GeneratedImage>


<map name="immap" id="immap" style="position: relative;">
	<asp:Repeater ID="Repeater1" runat="server">
		<ItemTemplate>
		<area shape="rect" coords="<%#Container.DataItem%>"  onclick="areaClicked('AppointmentDetails.aspx?AptNum=23')" alt="box2__"  />
		</ItemTemplate>
	</asp:Repeater>
	</map>
	
	</div>


<script type="text/javascript">
	$(document).ready(function () {
		$("#GeneratedImage1").load(function () {
			var scale = $(window).width()/$(this).width();
			$('#divappimage').attr('style', '-webkit-transform: scale('+scale+'); -webkit-transform-origin: 0 0;');
		});
	});
			
</script>	

</div>
</body>
</html>
