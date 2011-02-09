<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppointmentDetails.aspx.cs" Inherits="MobileWeb.AppointmentDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Appointment</title>
</head>
<body>
<div id="loggedin"><asp:Literal runat="server" ID="Message"></asp:Literal></div>
<div id="content">
<%//try{%>
<div class="styleError">  
				 <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>
</div>
	<ul>
		<li class="arrow style1">
		<div>
			<a linkattib="PatientDetails.aspx?PatNum=<%Response.Write(pat.PatNum);%>" href="#PatientDetails">
			<asp:Label ID="Label1" runat="server" Text=""><%Response.Write(PatName);%></asp:Label></a>
		</div>
		</li>
	</ul>


<ul>
<li> <span class="style1"><%Response.Write(apt.AptDateTime.ToShortDateString());%>&nbsp;&nbsp;<%Response.Write(apt.AptDateTime.ToString("dddd"));%><br />
<%Response.Write(apt.AptDateTime.ToString("hh:mm tt"));%>, <%Response.Write((apt.Pattern.Length*5).ToString()+" min");%><br />
<%Response.Write(apt.ProcDescript);%><br />
</span>
</li>
</ul>

<%//}
 // catch(Exception ex) {
	// LabelErrordd.Text="There has been an error in processing your request. from aspx";
	  
// } %>
</div>

</body>
</html>
