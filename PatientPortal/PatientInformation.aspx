<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientInformation.aspx.cs" Inherits="ODWebsite.PatientInformation" %>
<%@ Import namespace="OpenDentBusiness.Mobile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	Patient Name:<br /><asp:Label ID="LabelPatientName" runat="server"></asp:Label>
	
		<br />
	<br />
	<asp:Label ID="LabelLabPanel" runat="server" Text="Lab Panel:"></asp:Label>
	<br />
	<asp:GridView ID="GridViewLabPanel" runat="server" 
		AutoGenerateColumns="False">
		<Columns>
			<asp:TemplateField HeaderText="SpecimenCode">
			<ItemTemplate>
			<%#((LabPanelm)Container.DataItem).SpecimenCode%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="SpecimenDesc">
			<ItemTemplate>
			<%#((LabPanelm)Container.DataItem).SpecimenDesc%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="LabNameAddress">
			<ItemTemplate>
			<%#((LabPanelm)Container.DataItem).LabNameAddress%>
			</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
	<br />
	<br />
	<br />
	
	<br />
	<br />
	<asp:Label ID="LabelMedication" runat="server" Text="List of Medications:"></asp:Label>
	<br />
	<asp:GridView ID="GridViewMedication" runat="server" 
		AutoGenerateColumns="False">
		<Columns>
			<asp:TemplateField HeaderText="Notes">
			<ItemTemplate>
			<%#Medicationms.GetOne(((MedicationPatm)Container.DataItem).CustomerNum,((MedicationPatm)Container.DataItem).MedicationNum).MedName%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Medication Name">
			<ItemTemplate>
			<%#((MedicationPatm)Container.DataItem).PatNote%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Discontinued">
			<ItemTemplate>
			<%#GetDiscontinued(((MedicationPatm)Container.DataItem).IsDiscontinued)%>
			</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
	<br />
	<br />
	<br />

	Problems:
	<br />

	<asp:GridView ID="GridViewProblem" runat="server" 
		AutoGenerateColumns="False">
		<Columns>
		<asp:TemplateField HeaderText="ICD">
			<ItemTemplate>
			<%#ICD9ms.GetOne(((Diseasem)Container.DataItem).CustomerNum,((Diseasem)Container.DataItem).DiseaseDefNum).Description%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Problem">
			<ItemTemplate>
			<%#DiseaseDefms.GetOne(((Diseasem)Container.DataItem).CustomerNum,((Diseasem)Container.DataItem).DiseaseDefNum).DiseaseName%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Notes">
			<ItemTemplate>
			<%#((Diseasem)Container.DataItem).PatNote%>
			</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
	<br />
	<br />
	<br />
		Allergies:
	<br />

	<asp:GridView ID="GridViewAllergy" runat="server" 
		AutoGenerateColumns="False">
		<Columns>
			<asp:TemplateField HeaderText="Allergy">
			<ItemTemplate>
			<%#AllergyDefms.GetOne(((Allergym)Container.DataItem).CustomerNum,((Allergym)Container.DataItem).AllergyDefNum).Description%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Reaction">
			<ItemTemplate>
			<%#((Allergym)Container.DataItem).Reaction%>
			</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>

</asp:Content>
