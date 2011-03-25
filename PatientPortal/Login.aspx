<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ODWebsite.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<p>
	<asp:Label ID="Label1" runat="server" Text="User Name"></asp:Label>
</p>
<p>
	<asp:TextBox ID="TextBoxUserName" runat="server"></asp:TextBox>
	<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
		ControlToValidate="TextBoxUserName" ErrorMessage="RequiredFieldValidator" 
		ForeColor="Red" ValidationGroup="LoginValidation">This field is required</asp:RequiredFieldValidator>
</p>
<p>
	<asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
</p>
<p>
	<asp:TextBox ID="TextBoxPassword" runat="server" 
		ValidationGroup="LoginValidation" TextMode="Password"></asp:TextBox>
	<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
		ControlToValidate="TextBoxPassword" ErrorMessage="RequiredFieldValidator" 
		ForeColor="Red" ValidationGroup="LoginValidation">This field is required</asp:RequiredFieldValidator>
</p>
<p>
	<asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
	<br />
	<asp:Button ID="ButtonLogin" runat="server" Text="Login" 
		ValidationGroup="LoginValidation" onclick="ButtonLogin_Click" />
</p>
</asp:Content>
