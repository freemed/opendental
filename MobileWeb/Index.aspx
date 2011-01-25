<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="MobileWeb.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head  runat="server">
        <title>Open Dental Mobile</title>
        <link type="text/css" rel="stylesheet" media="screen" href="css/themes/apple/theme.css">
        <link type="text/css" rel="stylesheet" media="screen" href="css/jqtouch.css">
        <link type="text/css" rel="stylesheet" media="screen" href="css/iphone.css">
        <script type="text/javascript" src="scripts/jquery.js"></script>
		<script type="text/javascript" src="scripts/jqtouch.js"></script>
		<script type="text/javascript" src="scripts/iphone.js"></script>
		<script type="text/javascript">
		/*Dennis: the default slide animation is disabled on anchor tags with arrowless style*/
            var jQT = $.jQTouch({
                icon: 'Mob.png',
                statusBar: 'black',
                slideSelector: 'body > * > ul li a:not(.arrowless, #searchbutton)'
            });
		</script>
    </head>
    <body>

	<div id="login">
    <div class="toolbar">
                <h1>Open Dental</h1>
            </div>
			<br />
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
            <form id="form1" method="post" runat="server">
                <ul>
                    <%--<li><input type="text" runat="server" placeholder="Username" name="username" id="username" autocapitalize="off" autocorrect="off" autocomplete="off" /></li>--%>
					<li><asp:TextBox ID="username" runat="server"></asp:TextBox></li>
                    <li><input type="password" placeholder="Password" name="password" id="password" autocapitalize="off" autocorrect="off" autocomplete="off" /></li>
                   <%--<li><div class="chk"><input runat="server" id="rememberusername" title="Remember username" type="checkbox" /></div></li>--%>
				   <li><asp:CheckBox ID="rememberusername" runat="server" /></li>
					<li><input type="submit" class="submit" name="action" value="Login" /></li>
                </ul>
            </form>
        </div>

        <div id="home">
            <div class="toolbar">
			<a class="button logout" href="#">Logout</a>
                <h1>Home</h1>
            </div>
			<div style="height:70px">
            </div>
            <ul class="rounded page1">
                <li><a class="arrowless" linkattib="AppointmentList.aspx" href="#AppointmentList">Appointments</a></li>
            </ul>
			 <ul class="rounded page1">
                <li><a class="arrowless" linkattib="PatientList.aspx" href="#PatientList">Patients</a></li>
            </ul>
        </div>

        <div id="AppointmentList">
            <div class="toolbar">
                <h1>Appointments</h1>
                <a class="button home" href="#">Home</a>
            </div>
            <div id="AppointmentListContents">
            </div>
        </div>


        <div id="PatientList">
            <div class="toolbar">
                <h1>Patients</h1>
                <a class="button home" href="#">Home</a>
            </div>
			 <ul style="width:71%; display:inline-block;">
                <li>
				<input type="text" placeholder="Search Patient" name="searchpatientbox" id="searchpatientbox" autocapitalize="off" autocorrect="off" autocomplete="off" />
				<%--<a class="searchbutton" href="#"><img src="css/themes/apple/img/searchfield.png" border="0" /></a>--%>
				
				</li>
			</ul>
			<a class="button" id="searchbutton" href="#">Search</a>
				
			<div id="PatientListContents">
             </div>
        </div>

   <div id="PatientDetails">
        <div class="toolbar">
        <h1>Patient</h1>
        <a class="button patients" linkattib="PatientList.aspx" href="#">Patients</a>
    </div>
   <div id="PatientDetailsContents">
   </div>
   </div>

   
   <div id="AppointmentDetails">
		<div class="toolbar">
		<h1>Appointment</h1>
		<a class="button appts" linkattib="AppointmentList.aspx" href="#">Appts</a>
		</div>
	   <div id="AppointmentDetailsContents">
	   </div>
   </div>
    </body>
</html>
