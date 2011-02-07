<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientList.aspx.cs" Inherits="MobileWeb.PatientList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<div id="loggedin"><asp:Literal runat="server" ID="Message"></asp:Literal></div>
	<div id="content">

	<div style="position:relative;left:25px"><asp:Literal runat="server" ID="MessageNoPatients"></asp:Literal></div>
		<ul>
			<asp:Repeater ID="Repeater1" runat="server">
				<ItemTemplate>
					<li class="arrow style1">
						<div>
							<a linkattib="PatientDetails.aspx?PatNum=<%#((OpenDentBusiness.Mobile.Patientm)Container.DataItem).PatNum %>"
								href="#PatientDetails">
								<asp:Label ID="Label1" runat="server" Text="<%#((OpenDentBusiness.Mobile.Patientm)Container.DataItem).LName%> "></asp:Label>
								<asp:Label ID="Label2" runat="server" Text="<%#((OpenDentBusiness.Mobile.Patientm)Container.DataItem).FName%> "></asp:Label>
								</a>
						</div>
					</li>
				</ItemTemplate>
			</asp:Repeater>
		</ul>
		<div class="styleError">  
				 <asp:Label ID="LabelError" runat="server" Text="" ForeColor="Red"></asp:Label>
		</div>
	</div>
</body>
</html>
