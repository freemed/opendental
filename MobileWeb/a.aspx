<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="a.aspx.cs" Inherits="MobileWeb.a" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
		<script type="text/javascript" src="scripts/jquery.js"></script>
		<script type="text/javascript" src="scripts/jqtouchN.js"></script>


			<script type="text/javascript">
				/*Dennis: the default slide animation is disabled on anchor tags with arrowless style and id searchbutton*/
				var jQT = $.jQTouch({
					icon: 'Mob.png',
					statusBar: 'black',
					slideSelector: 'body > * > ul li a, body > * > area'
				});
		</script>

		<script type="text/javascript">
			$(document).ready(function () {
	
				$('#Div1').live('click', function () {
					//console.log('Div1 clicked');

					$('#im1').html('		<div id="overlay" style="position: absolute;background-color:green;top: 20px;left: 20px;">'
					+'</div>'
					+	'<img src="app.jpg" width="724px" height="662px" usemap="#immap" />'
					+  ' <map name="immap" id="immap" style="position: relative;">'
					+'<area shape="rect" coords="272, 84, 369, 172" someatt="someatt1" onclick="" href="" alt="box1__"  />'
					+'<area shape="rect" coords="375, 557, 482, 622" alt="box2__"  />'
					+ '</map>');

				});

				
				$('area').live('click', function () {
				console.log('area live clicked');
				var url = $(this).attr('href');
				var coords = $(this).attr('coords').split(',');
				// Your code here 
				var linkattib = $(this).attr('linkattib');

				console.log('linkattib in live=' + linkattib);

				// To prevent default action 
				return false;
				});


			$('a[href="#AppointmentList"]').click(function (e) {
				//e.preventDefault();
				console.log('AppointmentList clicked');
			});
			


			});
		</script>



</head>
<body>

<div id="loggedin">LoggedIn</div>
<div id="content">
<div class="styleError"></div>


	<div id="im1">
					<img src="app.jpg" width="724px" height="662px" usemap="#immap" />
					<map name="immap" id="immap" style="position: relative;">
					<area shape="rect" coords="272, 84, 369, 172" linkattib="AppointmentDetails.aspx?AptNum=22" onclick="areaClicked('AppointmentDetails.aspx?AptNum=22')" alt="box1__"  />
					<area shape="rect" coords="375, 557, 482, 622" onclick="areaClicked('AppointmentDetails.aspx?AptNum=23')" alt="box2__"  />
					</map>

<%--		<div id="overlay" style="position: absolute;background-color:green;top: 20px;left: 20px;">
		</div>

	<img src="app.jpg" width="724px" height="662px" usemap="#immap" />
    <map name="immap" id="immap" style="position: relative;">
	<area shape="rect" coords="272, 84, 369, 172" href="href1" someatt="someatt1" alt="box1__"  />
	<area shape="rect" coords="375, 557, 482, 622" alt="box2__"  />
	</map>--%>

    </div>

<%--	<div id="Div1" style=" height:120px;background:yellow;"> hellohellohellohellohellohellohellohellohellohello</div>
--%>
</div>

</body>
</html>

