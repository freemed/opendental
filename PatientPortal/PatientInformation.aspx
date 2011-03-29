<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientInformation.aspx.cs" Inherits="ODWebsite.PatientInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	Patient Name:<br /><asp:Label ID="LabelPatientName" runat="server"></asp:Label>
	<br />
	<br />
	<asp:Label ID="LabelMedication" runat="server" Text="List of Medications:"></asp:Label>
	<br />
	<asp:GridView ID="GridViewMedication" runat="server" 
		AutoGenerateColumns="False">
		<Columns>
			<asp:TemplateField HeaderText="Notes">
			<ItemTemplate>
			<%#OpenDentBusiness.Mobile.Medicationms.GetOne(((OpenDentBusiness.Mobile.MedicationPatm)Container.DataItem).CustomerNum,((OpenDentBusiness.Mobile.MedicationPatm)Container.DataItem).MedicationNum).MedName%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Medication Name">
			<ItemTemplate>
			<%#((OpenDentBusiness.Mobile.MedicationPatm)Container.DataItem).PatNote%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Discontinued">
			<ItemTemplate>
			<%#GetDiscontinued(((OpenDentBusiness.Mobile.MedicationPatm)Container.DataItem).IsDiscontinued)%>
			</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
	<br />
	<br />
	<br />

	Problems:
	<br />


</asp:Content>
