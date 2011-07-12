<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="a.aspx.cs" Inherits="MobileWeb.a" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<script type="text/javascript" src="scripts/jquery.js"></script>

		<script type="text/javascript">
			$(document).ready(function () {

			$('area').each(function () {
				
				var area = $(this),
				alt = area.attr('alt');
				area.mouseenter(function () {
					var coords = area.attr('coords').split(',');
					$('#overlay').css('left', coords[2] + 'px');
					$('#overlay').css('top', coords[1] + 'px');
				$('#overlay').html(alt);
				}).mouseleave(function () {
					$('#overlay').html('');
				});
			});

		});
		</script>



</head>
<body>
    <form id="form1" runat="server">
    <div>
	
<%--    <asp:imagemap runat="server" ImageUrl="app.jpg">
		<asp:RectangleHotSpot Bottom="183" Left="281" Right="381" Top="95" AlternateText="box1" />
		<asp:RectangleHotSpot Bottom="628" Left="381" Right="488" Top="583" AlternateText="box2" />
		</asp:imagemap>--%>
    </div>

	    <div>
		<div id="overlay" style="position: absolute;background-color:white;top: 20px;left: 20px;">
		</div>
		<img src="app.jpg" width="724px" height="662px" usemap="#immap" />

    <map name="immap" id="immap" style="position: relative;">
	<area shape="rect" coords="272, 84, 369, 172" alt="box1__"  />
	<area shape="rect" coords="375, 557, 482, 622" alt="box2__"  />
	
	</map>
    </div>

    </form>
</body>
</html>

