<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientInformation.aspx.cs" Inherits="ODWebsite.PatientInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:Label ID="LabelPatientName" runat="server"></asp:Label>
	<br />
	<br />
	<asp:Label ID="LabelMedication" runat="server" Text="List of Medications"></asp:Label>
	<br />
	<asp:GridView ID="GridViewMedication" runat="server" 
		AutoGenerateColumns="False">
		<Columns>
			<asp:TemplateField HeaderText="Notes">
			<ItemTemplate>
			<%#OpenDentBusiness.Medications.GetMedication(((OpenDentBusiness.MedicationPat)Container.DataItem).MedicationNum).MedName%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Medication Name">
			<ItemTemplate>
			<%#((OpenDentBusiness.MedicationPat)Container.DataItem).PatNote%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Discontinued">
			<ItemTemplate>
			<%#GetDiscontinued(((OpenDentBusiness.MedicationPat)Container.DataItem).IsDiscontinued)%>
			</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
	<br />
	<br />
	<br />
</asp:Content>
