<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientList.aspx.cs" Inherits="MobileWeb.PatientList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
<div id="loggedin"><asp:Literal runat="server" ID="Message"></asp:Literal></div>
<div id="content">
            <form method="post" runat="server" enableviewstate="False">
			
                <ul>
                    <li><input type="text" placeholder="Search Patient" name="patientname" id="patientname" autocapitalize="off" autocorrect="off" autocomplete="off" /></li>
                </ul>

            </form>
	<ul>
		<asp:Repeater ID="Repeater1" runat="server">
		<ItemTemplate>
		<li class="arrow style1">
		<div>
			<%--<a linkattib="PatientDetails.aspx?id=<%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>" href="#PatientDetails">
			<asp:Label ID="Label1" runat="server" Text="<%#Container.DataItem %>"></asp:Label></a>
--%>
			<a linkattib="PatientDetails.aspx?PatNum=<%#((OpenDentBusiness.Mobile.Patientm)Container.DataItem).PatNum %>" href="#PatientDetails">
			<asp:Label ID="Label1" runat="server" Text="<%#((OpenDentBusiness.Mobile.Patientm)Container.DataItem).FName %>"></asp:Label></a>
			
		</div>
		</li>
		</ItemTemplate>
		</asp:Repeater>
	</ul>
</div>
</body>
</html>
