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
		<%--<script type="text/javascript" src="https://getfirebug.com/firebug-lite.js">
		
		{
    overrideConsole: false,
    startInNewWindow: true,
    startOpened: true,
    enableTrace: true
}
		
		</script>--%>
		<script type="text/javascript">
		/*Dennis: the default slide animation is disabled on anchor tags with arrowless style*/
            var jQT = $.jQTouch({
                icon: 'Mob.png',
                statusBar: 'black',
                slideSelector: 'body > * > ul li a:not(.arrowless)'
            });
		</script>
    </head>
    <body>

	<div id="login">
    <div class="toolbar">
                <h1>Login</h1>
            </div>
			<br />
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
            <form method="post" id="ajaxform" runat="server" enableviewstate="False">
			
                <ul>
                    <li><input type="text" placeholder="Username" name="username" id="username" autocapitalize="off" autocorrect="off" autocomplete="off" /></li>
                    <li><input type="password" placeholder="Password" name="password" id="password" autocapitalize="off" autocorrect="off" autocomplete="off" /></li>
                   <li><div class="chk"><input id="rememberusername" title="Remember username" type="checkbox" /></div></li>
					
					<%--<div class="searchbox">
					<input id="search" placeholder="search character" type="text" name="key" value="">
					</div>--%>
					
					<li><input type="submit" class="submit" name="action" value="Login" /></li>
					

                </ul>
            </form>
        </div>


    <%--menulevel 1--%>
        <div id="home">
            <div class="toolbar">
                <h1>Open Dental Mobile</h1>
                <a class="button logout" href="#">Logout</a>
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
   <%--menulevel 1 ends here--%>


    <%--menulevel 2--%>
        <div id="AppointmentList">
            <div class="toolbar">
                <h1>Appointments</h1>
                <a class="button home" href="#">Home</a>
               <%-- <a class="button logout" href="#">Logout</a>--%>
            </div>

            <div id="AppointmentListContents">
              
            </div>
        </div>


        <div id="PatientList">
            <div class="toolbar">
                <h1>Patients</h1>
                <a class="button home" href="#">Home</a>
                <%--<a class="button logout" href="#">Logout</a>--%>
            </div>
			<div id="PatientListContents">
             </div>
        
        </div>
        

   <%--menulevel 2 ends here--%>

   <%--menulevel 3--%>

   <div id="PatientDetails">
   
               <div class="toolbar">
                <h1>Patient Details</h1>
                <a class="button patients" linkattib="PatientList.aspx" href="#">Patients</a>
               <%-- <a class="button logout" href="#">Logout</a>--%>
            </div>
   <div id="PatientDetailsContents">
   </div>
   </div>

   
   <div id="AppointmentDetails">
   
               <div class="toolbar">
                <h1>Appointment Details</h1>
                <a class="button appts" linkattib="AppointmentList.aspx" href="#">Appts</a>
                <%--<a class="button logout" href="#">Logout</a>--%>
            </div>
   <div id="AppointmentDetailsContents">
   </div>
   </div>

    <%--menulevel 3 ends here--%>


    </body>
</html>
