<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="MobileWeb.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head  runat="server">
        <title>Open Dental Mobile</title>
        <link type="text/css" rel="stylesheet" media="screen" href="css/themes/jqt/theme.css">
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
            var jQT = $.jQTouch({
                icon: 'Mob.png',
                statusBar: 'black'
            });
		</script>
    </head>
    <body>
        <div id="login">
            <div class="toolbar">
                <h1>Login</h1>
                
            </div>
            <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
            <form method="post" id="ajaxform" runat="server" enableviewstate="False" jqattrib="normalformdd">
                <ul class="rounded">
                    <li><input type="text" placeholder="Username" name="username" id="username" autocapitalize="off" autocorrect="off" autocomplete="off" /></li>
                    <li><input type="password" placeholder="Password" name="password" id="password" autocapitalize="off" autocorrect="off" autocomplete="off" /></li>
                    <!--<li><input id="rememberme" type="checkbox" /></li>
					
					<div class="searchbox">
					<input id="search" placeholder="search character" type="text" name="key" value="">
					</div>
					-->
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
            <ul class="rounded">
                <li class="arrow"><a linkattib="AppointmentList.aspx" href="#AppointmentList">Appointments</a></li>
                <li class="arrow"><a linkattib="PatientList.aspx" href="#PatientList">Patients</a></li>
            </ul>
        </div>
   <%--menulevel 1 ends here--%>


    <%--menulevel 2--%>
        <div id="AppointmentList">
            <div class="toolbar">
                <h1>Appointments</h1>
                <a class="button back" href="#">Back</a>
                <a class="button logout" href="#">Logout</a>
            </div>

            <div id="AppointmentListContents" class="contents">
              
            </div>
        </div>


        <div id="PatientList">
            <div class="toolbar">
                <h1>Patients</h1>
                <a class="button back" href="#">Back</a>
                <a class="button logout" href="#">Logout</a>
            </div>
			<div id="PatientListContents">
             </div>
        
        </div>
        

   <%--menulevel 2 ends here--%>

   <%--menulevel 3--%>

   <div id="PatientDetails">
   
               <div class="toolbar">
                <h1>Patient Details</h1>
                <a class="button back" href="#">Back</a>
                <a class="button logout" href="#">Logout</a>
            </div>
   <div id="PatientDetailsContents">
   </div>
   </div>

   
   <div id="AppointmentDetails">
   
               <div class="toolbar">
                <h1>Appointment Details</h1>
                <a class="button back" href="#">Back</a>
                <a class="button logout" href="#">Logout</a>
            </div>
   <div id="AppointmentDetailsContents">
   </div>
   </div>

    <%--menulevel 3 ends here--%>


    </body>
</html>
