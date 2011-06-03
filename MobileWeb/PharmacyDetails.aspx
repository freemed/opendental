<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PharmacyDetails.aspx.cs" Inherits="MobileWeb.PharmacyDetails" %>
<%@ Import namespace="OpenDentBusiness.Mobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
	<div id="loggedin"><asp:Literal runat="server" ID="Message"></asp:Literal></div>
	<div id="content">
	<div class="styleError">  
				 <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>
</div>

<ul class="contact">
<li><span class="style1">Home: <%Response.Write(phar.StoreName);%>
</span></li>
<%if(String.IsNullOrEmpty(phar.Address)) {%>
<li><span class="style1">Address: <%Response.Write(phar.Address);%>
</span></li>
<%}%>
<li><span class="style1">Mobile: <%Response.Write(phar.Address2);%>
</span></li>

</ul>

</div>
</body>
</html>
