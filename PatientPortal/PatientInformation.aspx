<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientInformation.aspx.cs" Inherits="PatientPortal.PatientInformation" %>
<%@ Import namespace="OpenDentBusiness.Mobile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	Patient Name: <asp:Label ID="LabelPatientName" runat="server" Font-Bold="True"></asp:Label>
	
		<br />
	<br />
	<asp:Label ID="LabelLabPanel" runat="server" Text="Lab Panel:"></asp:Label>
	<br />
	<asp:GridView ID="GridViewLabPanel" runat="server" 
		AutoGenerateColumns="False" onrowdatabound="GridViewLabPanel_RowDataBound">
		<Columns>
			<asp:TemplateField HeaderText="" Visible="false">
			<ItemTemplate>
			<%#((LabPanelm)Container.DataItem).LabPanelNum%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Lab Name & Address">
			<ItemTemplate>
			<%#((LabPanelm)Container.DataItem).LabNameAddress%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Lab Results">
			<ItemTemplate>
			<asp:GridView ID="GridViewLabResult" runat="server" 
				AutoGenerateColumns="False">
				<Columns>
					<asp:TemplateField HeaderText="Date Time">
							<ItemTemplate>
							<%#((LabResultm)Container.DataItem).DateTimeTest%>
							</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Test Name">
							<ItemTemplate>
							<%#((LabResultm)Container.DataItem).TestName%>
							</ItemTemplate>
					</asp:TemplateField>
			</Columns>
			</asp:GridView>
			</ItemTemplate>
			</asp:TemplateField>

		</Columns>
	</asp:GridView>
	<br />
	<br />
	<br />
	<asp:Label ID="LabelMedication" runat="server" Text="List of Medications:"></asp:Label>
	<br />
	<asp:GridView ID="GridViewMedication" runat="server" 
		AutoGenerateColumns="False">
		<Columns>
			<asp:TemplateField HeaderText="Medication Name">
			<ItemTemplate>
			<%#Medicationms.GetOne(((MedicationPatm)Container.DataItem).CustomerNum,((MedicationPatm)Container.DataItem).MedicationNum).MedName%>
			</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
	<br />
	<br />
	<br />
	<asp:Label ID="LabelProblem" runat="server" Text="List of Problems:"></asp:Label>
	<br />

	<asp:GridView ID="GridViewProblem" runat="server" 
		AutoGenerateColumns="False">
		<Columns>
			<asp:TemplateField HeaderText="Name">
			<ItemTemplate>
			<%#DiseaseDefms.GetOne(((Diseasem)Container.DataItem).CustomerNum,((Diseasem)Container.DataItem).DiseaseDefNum).DiseaseName%>
			</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="ICD">
			<ItemTemplate>
			<%#ICD9ms.GetOne(((Diseasem)Container.DataItem).CustomerNum,((Diseasem)Container.DataItem).DiseaseDefNum).Description%>
			</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
	<br />
	<br />
	<br />
		<asp:Label ID="LabelAllergy" runat="server" Text="List of Allergies:"></asp:Label>
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
