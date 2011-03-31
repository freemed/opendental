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

	<asp:GridView ID="GridViewProblem" runat="server" 
		AutoGenerateColumns="False">
		<Columns>
		<asp:TemplateField HeaderText="ICD">
			<ItemTemplate>
			<%#OpenDentBusiness.Mobile.ICD9ms.GetOne(((OpenDentBusiness.Mobile.Diseasem)Container.DataItem).CustomerNum,((OpenDentBusiness.Mobile.Diseasem)Container.DataItem).DiseaseDefNum).Description%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Problem">
			<ItemTemplate>
			<%#OpenDentBusiness.Mobile.DiseaseDefms.GetOne(((OpenDentBusiness.Mobile.Diseasem)Container.DataItem).CustomerNum,((OpenDentBusiness.Mobile.Diseasem)Container.DataItem).DiseaseDefNum).DiseaseName%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Notes">
			<ItemTemplate>
			<%#((OpenDentBusiness.Mobile.Diseasem)Container.DataItem).PatNote%>
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
			<%#OpenDentBusiness.Mobile.AllergyDefms.GetOne(((OpenDentBusiness.Mobile.Allergym)Container.DataItem).CustomerNum,((OpenDentBusiness.Mobile.Allergym)Container.DataItem).AllergyDefNum).Description%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Reaction">
			<ItemTemplate>
			<%#((OpenDentBusiness.Mobile.Allergym)Container.DataItem).Reaction%>
			</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>

</asp:Content>
