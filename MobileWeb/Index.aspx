<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MobileWeb.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head  runat="server">
        <title>Open Dental Mobile</title>
		<% if (HttpContext.Current.IsDebuggingEnabled) { %>
        <link type="text/css" rel="stylesheet" media="screen" href="css/themes/apple/theme.css">
        <link type="text/css" rel="stylesheet" media="screen" href="css/jqtouch.css">
        <link type="text/css" rel="stylesheet" media="screen" href="css/iphone.css">
        <script type="text/javascript" src="scripts/jquery.js"></script>
		<script type="text/javascript" src="scripts/jqtouch.js"></script>
		<script type="text/javascript" src="scripts/iphone.js"></script>
		<% } else { %>
          <link type="text/css" rel="stylesheet" media="screen" href="css/themes/apple/theme.min.css">
        <link type="text/css" rel="stylesheet" media="screen" href="css/jqtouch.css"><%--no minified version for this file--%>
        <link type="text/css" rel="stylesheet" media="screen" href="css/iphone.min.css">
        <script type="text/javascript" src="scripts/jquery.min.js"></script>
		<script type="text/javascript" src="scripts/jqtouch.min.js"></script>
		<script type="text/javascript" src="scripts/iphone.min.js"></script>
		<% } %>
		<script type="text/javascript">
			/*Dennis: the default slide animation is disabled on anchor tags with arrowless style and id searchbutton*/
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
            <span style="margin-left:15px;text-align:center;">For a demo, use the User name:<span style="color:Blue">demo</span></span>
			<br /><br />
            <form id="form1" method="post" runat="server">
			<span class="style1" style="font-weight:bold;position:relative;left:15px;">User name</span><br />
                <ul style="margin-top:4px">
                    <%--<li><input type="text" runat="server" placeholder="Username" name="username" id="username" autocapitalize="off" autocorrect="off" autocomplete="off" /></li>--%>
					<%--<li><input type="submit" class="submit" name="action" value="Login" /></li>--%>
					<%--				<ul style="width:80%;margin:auto;">
					<li style="text-align:center;padding-top:0px;padding-bottom:0px;"><input style="padding-top:0px;padding-bottom:0px;height:24px;font-weight:bold;" type="submit" class="submit" name="action" value="Login" /></li>
					</ul>--%>
					<li><asp:TextBox placeholder="" ID="username" runat="server"></asp:TextBox></li>
                </ul>
				<span class="style1" style="font-weight:bold;position:relative;left:15px;">Password</span>
                <ul style="margin-top:4px">
					<li><input type="password" placeholder="" name="password" id="password" autocapitalize="off" autocorrect="off" autocomplete="off" /></li>
                </ul>
                <div style="margin-left:15px;margin-bottom:10px">  
					<asp:CheckBox ID="rememberusername" title="Remember username" runat="server" /><span class="style1" style="margin-left:15px;font-weight:bold;position:relative;top:0px;left:0px;">Remember username</span>
                </div>	

							 <ul class="rounded page1">
                <li><a id="loginbutton" class="arrowless" href="#">Login</a></li>
            </ul>
				 <div class="styleError" style="margin-left:15px;">  
				 <asp:Label ID="LabelMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
				 </div>	
            </form>
        </div>
		<div id="home">
			<div class="toolbar">
				<a class="button logout" href="#">Logout</a>
				<h1>
					Home</h1>
			</div>
			<div style="height: 70px">
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
                <a class="home" href="#">Home</a>
            </div>
            <div id="AppointmentListContents">
            </div>
        </div>


        <div id="PatientList">
            <div class="toolbar">
                <h1>Patients</h1>
                <a class="home" href="#">Home</a>
            </div>
			 <ul style="width:71%; display:inline-block;">
                <li>
				<input type="text" placeholder="Search Patient" name="searchpatientbox" id="searchpatientbox" autocapitalize="off" autocorrect="off" autocomplete="off" />
				</li>
			</ul>
			<a class="button" id="searchbutton" href="#">Search</a>
				
			<div id="PatientListContents">
             </div>
        </div>

   <div id="PatientDetails">
        <div class="toolbar">
        <h1>Patient</h1>
        <a class="patients" linkattib="PatientList.aspx" href="#">Patients</a>
    </div>
   <div id="PatientDetailsContents">
   </div>
   </div>

   
   <div id="AppointmentDetails">
		<div class="toolbar">
		<h1>Appointment</h1>
		<a class="appts" linkattib="AppointmentList.aspx" href="#">Appts</a>
		</div>
	   <div id="AppointmentDetailsContents">
	   </div>
   </div>
    </body>
</html>
