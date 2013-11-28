using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeBase;
using System.Xml;
using System.Xml.XPath;

namespace OpenDentBusiness {
	public class EhrCCD {

		///<summary>2.16.840.1.113883.6.96</summary>
		private const string strCodeSystemSnomed="2.16.840.1.113883.6.96";
		///<summary>SNOMED CT</summary>
		private const string strCodeSystemNameSnomed="SNOMED CT";
		///<summary>2.16.840.1.113883.6.88</summary>
		private const string strCodeSystemRxNorm="2.16.840.1.113883.6.88";
		///<summary>RxNorm</summary>
		private const string strCodeSystemNameRxNorm="RxNorm";
		///<summary>2.16.840.1.113883.6.1</summary>
		private const string strCodeSystemLoinc="2.16.840.1.113883.6.1";
		///<summary>LOINC</summary>
		private const string strCodeSystemNameLoinc="LOINC";
		///<summary>Instantiated each time GenerateCCD() is called. Used by helper functions to avoid sending the writer as a parameter to each helper function.</summary>
		private static XmlWriter _w=null;
		///<summary>Instantiated each time GenerateCCD() is called. Used to generate unique "id" element "root" attribute identifiers. The Ids in this list are random alpha-numeric and 32 characters in length.</summary>
		private static HashSet<string> _hashCcdIds;
		///<summary>Instantiated each time GenerateCCD() is called. Used to generate unique "id" element "root" attribute identifiers. The Ids in this list are random GUIDs which are 36 characters in length.</summary>
		private static HashSet<string> _hashCcdUuids;

		public static string GenerateCCD(Patient pat) {
			_hashCcdIds=new HashSet<string>();//The IDs only need to be unique within each CCD document.
			_hashCcdUuids=new HashSet<string>();//The UUIDs only need to be unique within each CCD document.
			Medications.Refresh();
			XmlWriterSettings xmlSettings=new XmlWriterSettings();
			xmlSettings.Encoding=Encoding.UTF8;
			xmlSettings.OmitXmlDeclaration=true;
			xmlSettings.Indent=true;
			xmlSettings.IndentChars="   ";
			StringBuilder strBuilder=new StringBuilder();
			using(_w=XmlWriter.Create(strBuilder,xmlSettings)) {
				//Begin Clinical Document
				_w.WriteRaw("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n");
				_w.WriteProcessingInstruction("xml-stylesheet","type=\"text/xsl\" href=\"ccd.xsl\"");
				_w.WriteWhitespace("\r\n");
				_w.WriteStartElement("ClinicalDocument","urn:hl7-org:v3");
				Attribs("xmlns","xsi",null,"http://www.w3.org/2001/XMLSchema-instance");
				Attribs("xsi","noNamespaceSchemaLocation",null,"Registry_Payment.xsd");
				Attribs("xsi","schemaLocation",null,"urn:hl7-org:v3 http://xreg2.nist.gov:8080/hitspValidation/schema/cdar2c32/infrastructure/cda/C32_CDA.xsd");
				StartAndEnd("realmCode","code","US");
				StartAndEnd("typeId","root","2.16.840.1.113883.1.3","extension","POCD_HD000040");//template id to assert use of the CCD standard
				_w.WriteComment("US General Header Template");
				TemplateId("2.16.840.1.113883.10.20.22.1.1");
				_w.WriteComment("Conforms to CCD requirements");
				TemplateId("2.16.840.1.113883.10.20.22.1.2");
				Id();
				StartAndEnd("code","code","34133-9","codeSystemName",strCodeSystemNameLoinc,"codeSystem",strCodeSystemLoinc,"displayName","Summarization of Episode Note");
				_w.WriteElementString("title","Continuity of Care Document");
				StartAndEnd("effectiveTime","value",DateTime.Now.ToString("yyyyMMddHHmmsszzz").Replace(":",""));
				StartAndEnd("confidentialityCode","code","N","codeSystem","2.16.840.1.113883.5.25");//Fixed value.  Confidentiality Code System.  Codes: N=(Normal), R=(Restricted),V=(Very Restricted)
				StartAndEnd("languageCode","code","en-US");
				Start("recordTarget");
				Start("patientRole");
				StartAndEnd("id","extension",pat.PatNum.ToString(),"root","2.16.840.1.113883.19.5");
				Start("addr","use","HP");
				_w.WriteElementString("streetAddressLine",pat.Address.ToString());
				_w.WriteElementString("streetAddressLine",pat.Address2.ToString());
				_w.WriteElementString("city",pat.City.ToString());
				_w.WriteElementString("state",pat.State.ToString());
				_w.WriteElementString("country","");
				End("addr");
				StartAndEnd("telecom");
				Start("patient");
				Start("name","use","L");
				_w.WriteElementString("given",pat.FName.ToString());
				_w.WriteElementString("given",pat.MiddleI.ToString());
				_w.WriteElementString("family",pat.LName.ToString());
				Start("suffix","qualifier","TITLE");
				_w.WriteString(pat.Title.ToString());
				End("suffix");
				End("name");
				string strGender="M";
				if(pat.Gender==PatientGender.Female) {
					strGender="F";
				}
				StartAndEnd("administrativeGenderCode","code",strGender,"codeSystem","2.16.840.1.113883.5.1");
				DateElement("birthTime",pat.Birthdate);
				End("patient");
				End("patientRole");
				End("recordTarget");
				//author--------------------------------------------------------------------------------------------------
				Start("author");
				TimeElement("time",DateTime.Now);
				Start("assignedAuthor");
				Uuid();
				_w.WriteElementString("addr","");
				_w.WriteElementString("telecom","");
				Start("assignedPerson");
				_w.WriteElementString("name","");
				End("assignedPerson");
				Start("representedOrganization");
				_w.WriteElementString("name",PrefC.GetString(PrefName.PracticeTitle));
				_w.WriteElementString("telecom","");
				_w.WriteElementString("addr","");
				End("representedOrganization");
				End("assignedAuthor");
				End("author");
				//custodian------------------------------------------------------------------------------------------------
				Start("custodian");
				Start("assignedCustodian");
				Start("representedCustodianOrganization");
				_w.WriteElementString("id","");
				_w.WriteElementString("name","");
				_w.WriteElementString("telecom","");
				_w.WriteElementString("addr","");
				End("representedCustodianOrganization");
				End("assignedCustodian");
				End("custodian");
				//legalAuthenticator--------------------------------------------------------------------------------------
				//this is used by the style sheet to create the line at the bottom: Electronically Generated by: on date
				Start("legalAuthenticator");
				TimeElement("time",DateTime.Now);
				StartAndEnd("signatureCode","code","S");
				Start("assignedEntity");
				StartAndEnd("id","nullFlavor","NI");
				StartAndEnd("addr");
				StartAndEnd("telecom");
				Start("assignedPerson");
				StartAndEnd("name");
				End("assignedPerson");
				Start("representedOrganization");
				StartAndEnd("id","root","2.16.840.1.113883.19.5");
				_w.WriteElementString("name",PrefC.GetString(PrefName.PracticeTitle));
				StartAndEnd("telecom");
				StartAndEnd("addr");
				End("representedOrganization");
				End("assignedEntity");
				End("legalAuthenticator");
				_w.WriteComment(@"
=====================================================================================================
Body
=====================================================================================================");
				Start("component");
				Start("structuredBody");
				GenerateCcdSectionAllergies(pat);
				GenerateCcdSectionEncounters(pat);
				GenerateCcdSectionFunctionalStatus(pat);
				GenerateCcdSectionImmunizations(pat);
				GenerateCcdSectionMedications(pat);
				GenerateCcdSectionPlanOfCare(pat);
				GenerateCcdSectionProblems(pat);
				GenerateCcdSectionReasonForReferral(pat);
				GenerateCcdSectionResults(pat);//Lab Results
				GenerateCcdSectionVitalSigns(pat);
				End("structuredBody");
				End("component");
				End("ClinicalDocument");
			}
			SecurityLogs.MakeLogEntry(Permissions.Copy,pat.PatNum,"CCD generated");			//Create audit log entry.
			return strBuilder.ToString();
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionAllergies(Patient pat) {
			_w.WriteComment(@"
=====================================================================================================
Allergies
=====================================================================================================");
			List<Allergy> listAllergy=Allergies.Refresh(pat.PatNum);
			AllergyDef allergyDef;
			Medication med;
			Start("component");
			Start("section");
			TemplateId("2.16.840.1.113883.10.20.22.2.6.1");
			_w.WriteComment("Allergies section template");
			StartAndEnd("code","code","48765-2","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Allergies");
			_w.WriteElementString("title","Allergies and Adverse Reactions");
			Start("text");//The following text will be parsed as html with a style sheet to be human readable.
			Start("table","width","100%","border","1");
			Start("thead");
			Start("tr");
			_w.WriteElementString("th","Substance");
			_w.WriteElementString("th","Reaction");
			_w.WriteElementString("th","Allergy Type");
			_w.WriteElementString("th","Status");
			End("tr");
			End("thead");
			Start("tbody");
			for(int i=0;i<listAllergy.Count;i++) {
				String status=listAllergy[i].StatusIsActive?"Active":"Inactive";
				allergyDef=AllergyDefs.GetOne(listAllergy[i].AllergyDefNum);
				if(allergyDef.MedicationNum==0) {
					continue;
				}
				med=Medications.GetMedication(allergyDef.MedicationNum);
				if(med.RxCui==0) {
					continue;
				}
				Start("tr");
				_w.WriteElementString("td",med.RxCui.ToString()+" - "+med.MedName);
				_w.WriteElementString("td",listAllergy[i].Reaction);
				_w.WriteElementString("td",AllergyDefs.GetSnomedAllergyDesc(allergyDef.SnomedType));
				_w.WriteElementString("td",status);
				End("tr");
			}
			End("tbody");
			End("table");
			End("text");
			for(int i=0;i<listAllergy.Count;i++) {
				allergyDef=AllergyDefs.GetOne(listAllergy[i].AllergyDefNum);
				if(allergyDef.MedicationNum==0) {
					continue;
				}
				med=Medications.GetMedication(allergyDef.MedicationNum);
				if(med.RxCui==0) {
					continue;
				}
				string status=listAllergy[i].StatusIsActive?"Active":"Inactive";
				string allergyType="";
				string allergyTypeName="";
				#region Allergy Type
				if(allergyDef.SnomedType==SnomedAllergy.AdverseReactionsToDrug) {
					allergyType="419511003";
					allergyTypeName="Propensity to adverse reaction to drug";
				}
				else if(allergyDef.SnomedType==SnomedAllergy.AdverseReactionsToFood) {
					allergyType="418471000";
					allergyTypeName="Propensity to adverse reaction to food";
				}
				else if(allergyDef.SnomedType==SnomedAllergy.AdverseReactionsToSubstance) {
					allergyType="419199007";
					allergyTypeName="Propensity to adverse reaction to substance";
				}
				else if(allergyDef.SnomedType==SnomedAllergy.AllergyToSubstance) {
					allergyType="418038007";
					allergyTypeName="Allergy to substance";
				}
				else if(allergyDef.SnomedType==SnomedAllergy.DrugAllergy) {
					allergyType="416098002";
					allergyTypeName="Drug allergy";
				}
				else if(allergyDef.SnomedType==SnomedAllergy.DrugIntolerance) {
					allergyType="59037007";
					allergyTypeName="Drug intolerance";
				}
				else if(allergyDef.SnomedType==SnomedAllergy.FoodAllergy) {
					allergyType="414285001";
					allergyTypeName="Food allergy";
				}
				else if(allergyDef.SnomedType==SnomedAllergy.FoodIntolerance) {
					allergyType="235719002";
					allergyTypeName="Food intolerance";
				}
				else if(allergyDef.SnomedType==SnomedAllergy.AdverseReactions) {
					allergyType="420134006";
					allergyTypeName="Adverse reaction";
				}
				else {
					allergyType="";
					allergyTypeName="None";
				}
				#endregion
				allergyDef=AllergyDefs.GetOne(listAllergy[i].AllergyDefNum);
				if(allergyDef.MedicationNum==0) {
					continue;
				}
				med=Medications.GetMedication(allergyDef.MedicationNum);
				if(med.RxCui==0) {
					continue;
				}
				Start("entry","typeCode","DRIV");
				Start("act","classCode","ACT","moodCode","EVN");
				TemplateId("2.16.840.1.113883.10.20.22.4.30");
				Id();
				StartAndEnd("code","code","48765-2","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Allergies and adverse reactions");
				if(listAllergy[i].StatusIsActive) {
					StartAndEnd("statusCode","code","active");
				}
				else {
					StartAndEnd("statusCode","code","completed");
				}
				Start("effectiveTime");
				if(listAllergy[i].StatusIsActive) {
					StartAndEnd("low","value",listAllergy[i].DateTStamp.ToString("yyyymmdd"));
					StartAndEnd("high","nullFlavor","UNK");
				}
				else {
					StartAndEnd("low","nullFlavor","UNK");
					StartAndEnd("high","value",listAllergy[i].DateTStamp.ToString("yyyymmdd"));
				}
				End("effectiveTime");
				Start("entryRelationship","typeCode","SUBJ");
				Start("observation","classCode","OBS","moodCode","EVN");
				_w.WriteComment("Allergy Observation template");
				TemplateId("2.16.840.1.113883.10.20.22.4.7");
				Id();
				StartAndEnd("code","code","ASSERTION","codeSystem","2.16.840.1.113883.5.4");//Fixed Value
				StartAndEnd("statusCode","code","completed");//fixed value (required)
				Start("effectiveTime");
				StartAndEnd("low","nullFlavor","UNK");
				StartAndEnd("high","value",listAllergy[i].DateTStamp.ToString("yyyymmdd"));
				End("effectiveTime");
				Start("value");
				_w.WriteAttributeString("xsi","type",null,"CD");
				Attribs("code",allergyType,"displayName",allergyTypeName,"codeSystem",strCodeSystemSnomed,"codeSystemName",strCodeSystemNameSnomed);
				End("value");
				Start("participant","typeCode","CSM");
				Start("participantRole","classCode","MANU");
				Start("playingEntity","classCode","MMAT");
				StartAndEnd("code","code",med.RxCui.ToString(),"displayName",med.MedName,"codeSystem",strCodeSystemRxNorm,"codeSystemName",strCodeSystemNameRxNorm);
				End("playingEntity");
				End("participantRole");
				End("participant");
				Start("entryRelationship","typeCode","SUBJ");
				Start("observation","classCode","OBS","moodCode","EVN");
				_w.WriteComment("Allergy Status Observation template");
				TemplateId("2.16.840.1.113883.10.20.22.4.28");
				StartAndEnd("code","code","33999-4","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Status");
				StartAndEnd("statusCode","code","completed");//fixed value (required)
				if(status=="Active") {
					Start("value");
					_w.WriteAttributeString("xsi","type",null,"CE");
					Attribs("code","55561003","codeSystem",strCodeSystemSnomed,"displayName",status);
					End("value");
				}
				else {
					Start("value");
					_w.WriteAttributeString("xsi","type",null,"CE");
					Attribs("code","73425007","codeSystem",strCodeSystemSnomed,"displayName",status);
					End("value");
				}
				End("observation");
				End("entryRelationship");
				Start("entryRelationship","typeCode","SUBJ");
				Start("observation","classCode","OBS","moodCode","EVN");
				_w.WriteComment("Reaction Observation template");
				TemplateId("2.16.840.1.113883.10.20.22.4.9");
				Id();
				StartAndEnd("code","nullFlavor","NA");//Unknown why this is null, but can't find a good example of this
				StartAndEnd("statusCode","code","completed");//fixed value (required)
				Start("effectiveTime");
				if(listAllergy[i].StatusIsActive) {
					StartAndEnd("low","value",listAllergy[i].DateTStamp.ToString("yyyymmdd"));
				}
				else {
					StartAndEnd("low","nullFlavor","UNK");
				}
				End("effectiveTime");
				Start("value");
				_w.WriteAttributeString("xsi","type",null,"CD");
				Attribs("code",listAllergy[i].SnomedReaction,"codeSystem",strCodeSystemSnomed,"displayName",listAllergy[i].Reaction);
				End("value");
				End("observation");
				End("entryRelationship");
				End("observation");
				End("entryRelationship");
				End("act");
				End("entry");
			}
			End("section");
			End("component");
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionEncounters(Patient pat) {
			//TODO:
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionFunctionalStatus(Patient pat) {
			//TODO:
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionImmunizations(Patient pat) {
			//TODO:
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionMedications(Patient pat) {
			_w.WriteComment(@"
=====================================================================================================
Medications
=====================================================================================================");
			List<MedicationPat> listMedPat=MedicationPats.Refresh(pat.PatNum,true);
			Medication med;
			Start("component");
			Start("section");
			TemplateId("2.16.840.1.113883.10.20.22.2.1.1");//Required Medication Section
			_w.WriteComment("Medications section template");
			StartAndEnd("code","code","10160-0","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","History of medication use");
			_w.WriteElementString("title","Medications");
			Start("text");//The following text will be parsed as html with a style sheet to be human readable.
			Start("table","width","100%","border","1");
			Start("thead");
			Start("tr");
			_w.WriteElementString("th","Medication");
			_w.WriteElementString("th","Directions");
			_w.WriteElementString("th","Start Date");
			_w.WriteElementString("th","End Date");
			_w.WriteElementString("th","Status");
			_w.WriteElementString("th","Indications");
			_w.WriteElementString("th","Fill Instructions");
			End("tr");
			End("thead");
			Start("tbody");
			for(int i=0;i<listMedPat.Count;i++) {
				if(listMedPat[i].RxCui==0) {
					continue;
				}
				if(listMedPat[i].MedicationNum==0) {
					continue;
				}
				med=Medications.GetMedication(listMedPat[i].MedicationNum);
				Start("tr");
				_w.WriteElementString("td",med.MedName);//Medication
				_w.WriteElementString("td",med.Notes);//Directions
				_w.WriteElementString("td",listMedPat[i].DateStart.ToShortDateString());//Start Date
				DateText("td",listMedPat[i].DateStop);//End Date
				_w.WriteElementString("td",MedicationPats.IsMedActive(listMedPat[i])?"Active":"Inactive");//Status
				_w.WriteElementString("td",listMedPat[i].PatNote);//Indications
				_w.WriteElementString("td",listMedPat[i].MedDescript);//Fill Instructions
				End("tr");
			}
			End("tbody");
			End("table");
			End("text");
			string status;//May not be necessary, but no harm in setting it.
			for(int i=0;i<listMedPat.Count;i++) {
				long rxCui=listMedPat[i].RxCui;
				string strMedName=listMedPat[i].MedDescript;//This might be blank, for example not from NewCrop.  
				if(listMedPat[i].MedicationNum!=0) {//If NewCrop, this will be 0.  Also might be zero in the future when we start allowing freeform medications.
					med=Medications.GetMedication(listMedPat[i].MedicationNum);
					rxCui=med.RxCui;
					strMedName=med.MedName;
					if(listMedPat[i].DateStop.Year>1880 && listMedPat[i].DateStop>=DateTime.Now) {
						status="active";
					}
					else {
						status="completed";
					}
				}
				if(rxCui==0) {
					continue;
				}
				Start("entry","typeCode","DRIV");
				Start("substanceAdministration","classCode","SBADM","moodCode","EVN");
				TemplateId("2.16.840.1.113883.10.20.22.4.16");
				_w.WriteComment("Medication activity template");
				Id();
				StartAndEnd("statusCode","code","completed");//Fixed Value
				Start("effectiveTime");
				_w.WriteAttributeString("xsi","type",null,"IVL_TS");
				StartAndEnd("low","value",listMedPat[i].DateStart.ToString("yyyymmdd"));
				StartAndEnd("high","value",listMedPat[i].DateStop.ToString("yyyymmdd"));
				End("effectiveTime");
				Start("consumable");
				Start("manufacturedProduct");
				TemplateId("2.16.840.1.113883.10.20.22.4.23");
				Id();
				Start("manufacturedMaterial");
				Start("code","code",rxCui.ToString(),"codeSystem",strCodeSystemRxNorm,"displayName",strMedName,"codeSystemName",strCodeSystemNameRxNorm);
				End("code");
				End("manufacturedMaterial");
				End("manufacturedProduct");
				End("consumable");
				End("substanceAdministration");
				End("entry");
			}
			End("section");
			End("component");
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionPlanOfCare(Patient pat) {
			_w.WriteComment(@"
=====================================================================================================
Care Plan
=====================================================================================================");
			Start("component");
			Start("section");
			TemplateId("2.16.840.1.113883.10.20.22.2.10");
			_w.WriteComment("Plan of Care section template");
			StartAndEnd("code","code","18776-5","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Treatment plan");
			_w.WriteElementString("title","Care Plan");
			Start("text");//The following text will be parsed as html with a style sheet to be human readable.
			Start("table","width","100%","border","1");
			Start("thead");
			Start("tr");
			_w.WriteElementString("th","Planned Activity");
			_w.WriteElementString("th","Planned Date");
			End("tr");
			End("thead");
			Start("tbody");
			List<EhrCarePlan> listEhrCarePlans=EhrCarePlans.Refresh(pat.PatNum);
			for(int i=0;i<listEhrCarePlans.Count;i++) {
				Start("tr");
				_w.WriteElementString("td",listEhrCarePlans[i].Instructions);//Planned Activity
				DateText("td",listEhrCarePlans[i].DatePlanned);//Planned Date
				End("tr");
			}
			End("tbody");
			End("table");
			End("text");
			for(int i=0;i<listEhrCarePlans.Count;i++) {
				Start("entry","typeCode","DRIV");
				Start("act","classCode","ACT","moodCode","INT");
				TemplateId("2.16.840.1.113883.10.20.22.4.20");
				_w.WriteComment("Instructions template");
				Start("code");
				_w.WriteAttributeString("xsi","type",null,"CE");
				Snomed snomedEducation=Snomeds.GetByCode(listEhrCarePlans[i].SnomedEducation);
				Attribs("code",snomedEducation.SnomedCode,"codeSystem",strCodeSystemSnomed,"displayName",snomedEducation.Description);
				_w.WriteElementString("text",listEhrCarePlans[i].Instructions);
				StartAndEnd("statusCode","code","completed");
				End("act");
				End("entry");
			}
			End("section");
			End("component");
		}

		///<summary>Helper for GenerateCCD().  Problem section.</summary>
		private static void GenerateCcdSectionProblems(Patient pat) {
			_w.WriteComment(@"
=====================================================================================================
Problems
=====================================================================================================");
			List<Disease> listProblem=Diseases.Refresh(pat.PatNum);
			string status="Inactive";
			string statusCode="73425007";
			string statusOther="active";
			Start("component");
			Start("section");
			TemplateId("2.16.840.1.113883.10.20.22.2.5.1");
			_w.WriteComment("Problems section template");
			StartAndEnd("code","code","11450-4","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Problem list");
			_w.WriteElementString("title","PROBLEMS");
			Start("text");
			StartAndEnd("content","ID","problems");
			Start("list","listType","ordered");
			for(int i=0;i<listProblem.Count;i++) {//Fill Problems Table
				if(listProblem[i].SnomedProblemType!="55607006") {
					continue;
				}
				Start("item");
				_w.WriteString(DiseaseDefs.GetName(listProblem[i].DiseaseDefNum)+" : "+"Status - ");
				if(listProblem[i].ProbStatus==ProblemStatus.Active) {
					_w.WriteString("Active");
					status="Active";
					statusCode="55561003";
					statusOther="active";
				}
				else if(listProblem[i].ProbStatus==ProblemStatus.Inactive) {
					_w.WriteString("Inactive");
					status="Inactive";
					statusCode="73425007";
					statusOther="completed";
				}
				else {
					_w.WriteString("Resolved");
					status="Resolved";
					statusCode="413322009";
					statusOther="completed";
				}
				End("item");
			}
			End("list");
			End("text");
			for(int i=0;i<listProblem.Count;i++) {//Fill Problems Info
				if(listProblem[i].SnomedProblemType!="55607006") {
					continue;
				}
				Start("entry","typeCode","DRIV");
				Start("act","classCode","ACT","moodCode","EVN");
				_w.WriteComment("Problem Concern Act template");//Concern Act Section
				TemplateId("2.16.840.1.113883.10.20.22.4.3");
				Id();
				StartAndEnd("code","code","55607006","codeSystem",strCodeSystemSnomed,"displayName","Problem");
				StartAndEnd("statusCode","code",statusOther);
				Start("effectiveTime");
				StartAndEnd("low","value",listProblem[i].DateStart.ToString("yyyymmdd"));
				DateElement("high",listProblem[i].DateStop);
				End("effectiveTime");
				Start("entryRelationship","typeCode","SUBJ");
				Start("observation","classCode","OBS","moodCode","EVN");
				_w.WriteComment("Problem Observation template");//Observation Section
				TemplateId("2.16.840.1.113883.10.20.22.4.4");
				Id();
				StartAndEnd("code","code",listProblem[i].SnomedProblemType,"codeSystem",strCodeSystemSnomed,"displayName",Snomeds.GetByCode(listProblem[i].SnomedProblemType).Description);
				if(listProblem[i].ProbStatus==ProblemStatus.Active) {
					StartAndEnd("statusCode","code","active");
				}
				else {
					StartAndEnd("statusCode","code","completed");
				}
				Start("effectiveTime");
				DateElement("low",listProblem[i].DateStart);
				End("effectiveTime");
				Start("value");
				_w.WriteAttributeString("xsi","type",null,"CD");
				Attribs("code",DiseaseDefs.GetItem(listProblem[i].DiseaseDefNum).SnomedCode,"codeSystem",strCodeSystemSnomed,"displayName",DiseaseDefs.GetItem(listProblem[i].DiseaseDefNum).DiseaseName);
				End("value");
				Start("entryRelationship","typeCode","REFR");
				Start("observation","classCode","OBS","moodCode","EVN");
				_w.WriteComment("Status Observation template");//Status Observation Section
				TemplateId("2.16.840.1.113883.10.20.22.4.6");
				Start("code");
				_w.WriteAttributeString("xsi","type",null,"CE");
				Attribs("code","33999-4","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Status");
				End("code");
				if(listProblem[i].ProbStatus==ProblemStatus.Active) {
					StartAndEnd("statusCode","code","active");
				}
				else {
					StartAndEnd("statusCode","code","completed");
				}
				Start("value");
				_w.WriteAttributeString("xsi","type",null,"CD");
				Attribs("code",statusCode,"codeSystem",strCodeSystemSnomed,"displayName",status);
				End("value");
				End("observation");
				End("entryRelationship");
				End("observation");
				End("entryRelationship");
				End("act");
				End("entry");
			}
			End("section");
			End("component");
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionReasonForReferral(Patient pat) {
			//TODO:
		}

		///<summary>Helper for GenerateCCD().  Exports Labs.</summary>
		private static void GenerateCcdSectionResults(Patient pat) {
			_w.WriteComment(@"
=====================================================================================================
Laboratory Test Results
=====================================================================================================");
			List<LabResult> listLabResult=LabResults.GetAllForPatient(pat.PatNum);
			LabPanel labPanel;
			Start("component");
			Start("section");
			TemplateId("2.16.840.1.113883.3.88.11.83.122","HITSP/C83");
			TemplateId("1.3.6.1.4.1.19376.1.5.3.1.3.28","IHE PCC");
			_w.WriteComment("Diagnostic Results section template");
			StartAndEnd("code","code","30954-2","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Results");
			_w.WriteElementString("title","Diagnostic Results");
			Start("text");//The following text will be parsed as html with a style sheet to be human readable.
			Start("table","width","100%","border","1");
			Start("thead");
			Start("tr");
			_w.WriteElementString("th","LOINC Code");
			_w.WriteElementString("th","Test");
			_w.WriteElementString("th","Result");
			_w.WriteElementString("th","Abnormal Flag");
			_w.WriteElementString("th","Date Performed");
			End("tr");
			End("thead");
			Start("tbody");
			for(int i=0;i<listLabResult.Count;i++) {
				Start("tr");
				_w.WriteElementString("td",listLabResult[i].TestID);//LOINC Code
				_w.WriteElementString("td",listLabResult[i].TestName);//Test
				_w.WriteElementString("td",listLabResult[i].ObsValue+" "+listLabResult[i].ObsUnits);//Result
				_w.WriteElementString("td",listLabResult[i].AbnormalFlag.ToString());//Abnormal Flag
				_w.WriteElementString("td",listLabResult[i].DateTimeTest.ToShortDateString());//Date Performed
				End("tr");
			}
			End("tbody");
			End("table");
			End("text");
			for(int i=0;i<listLabResult.Count;i++) {
				labPanel=LabPanels.GetOne(listLabResult[i].LabPanelNum);
				Start("entry","typeCode","DRIV");
				Start("organizer","classCode","BATTERY","moodCode","EVN");
				StartAndEnd("templateId","root","2.16.840.1.113883.10.20.1.32");//no authority name
				_w.WriteComment("Result organizer template");
				StartAndEnd("id","root","7d5a02b0-67a4-11db-bd13-0800200c9a66");
				StartAndEnd("code","code",labPanel.ServiceId,"codeSystem",strCodeSystemSnomed,"displayName",labPanel.ServiceName);
				StartAndEnd("statusCode","code","completed");
				StartAndEnd("effectiveTime","value",listLabResult[i].DateTimeTest.ToString("yyyyMMddHHmm"));
				Start("component");
				Start("procedure","classCode","PROC","moodCode","EVN");
				TemplateId("2.16.840.1.113883.3.88.11.83.17","HITSP C83");
				TemplateId("2.16.840.1.113883.10.20.1.29","CCD");//procedure activity template id, according to pages 103-104 of CCD-final.pdf
				TemplateId("1.3.6.1.4.1.19376.1.5.3.1.4.19","IHE PCC");
				StartAndEnd("id");
				Start("code","code",labPanel.ServiceId,"codeSystem",strCodeSystemSnomed,"displayName",labPanel.ServiceName);
				Start("originalText");
				_w.WriteString(labPanel.ServiceName);
				StartAndEnd("reference","value","Ptr to text  in parent Section");
				End("originalText");
				End("code");
				Start("text");
				_w.WriteString(labPanel.ServiceName);
				StartAndEnd("reference","value","Ptr to text  in parent Section");
				End("text");
				StartAndEnd("statusCode","code","completed");
				StartAndEnd("effectiveTime","value",listLabResult[i].DateTimeTest.ToString("yyyyMMddHHmm"));
				End("procedure");
				End("component");
				Start("component");
				Start("observation","classCode","OBS","moodCode","EVN");
				TemplateId("2.16.840.1.113883.3.88.11.83.15.1","HITSP C83");
				TemplateId("2.16.840.1.113883.10.20.1.31","CCD");//result observation template id, according to pages 103-104 of CCD-final.pdf
				TemplateId("1.3.6.1.4.1.19376.1.5.3.1.4.13","IHE PCC");
				_w.WriteComment("Result observation template");
				Uuid();
				StartAndEnd("code","code",listLabResult[i].TestID,"codeSystem",strCodeSystemLoinc,"displayName",listLabResult[i].TestName);
				Start("text");
				StartAndEnd("reference","value","PtrToValueInsectionText");
				End("text");
				StartAndEnd("statusCode","code","completed");
				StartAndEnd("effectiveTime","value",listLabResult[i].DateTimeTest.ToString("yyyyMMddHHmm"));
				Start("value");
				_w.WriteAttributeString("xsi","type",null,"PQ");
				Attribs("value","13.2","unit","g/dl");
				End("value");
				StartAndEnd("interpretationCode","code","N","codeSystem","2.16.840.1.113883.5.83");
				End("observation");
				End("component");
				End("organizer");
				End("entry");
			}
			End("section");
			End("component");
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionVitalSigns(Patient pat) {
			//TODO:
		}
		
		///<summary>Helper for GenerateCCD(). Builds an "id" element and writes a random 32 character alpha-numeric string into the "root" attribute.</summary>
		private static void Id() {
			string id=MiscUtils.CreateRandomAlphaNumericString(32);
			while(_hashCcdIds.Contains(id)) {
				id=MiscUtils.CreateRandomAlphaNumericString(32);
			}
			_hashCcdIds.Add(id);
			StartAndEnd("id","root",id);
		}

		///<summary>Helper for GenerateCCD(). Builds an "id" element and writes a 36 character GUID string into the "root" attribute.
		///An example of how the uid might look: "20cf14fb-B65c-4c8c-A54d-b0cca834C18c"</summary>
		private static void Uuid() {
			Guid uuid=System.Guid.NewGuid();
			while(_hashCcdUuids.Contains(uuid.ToString())) {
				uuid=System.Guid.NewGuid();
			}
			_hashCcdUuids.Add(uuid.ToString());
			StartAndEnd("id","root",uuid.ToString());
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void TemplateId(string rootNumber) {
			_w.WriteStartElement("templateId");
			_w.WriteAttributeString("root",rootNumber);
			_w.WriteEndElement();
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void TemplateId(string rootNumber,string authorityName) {
			_w.WriteStartElement("templateId");
			_w.WriteAttributeString("root",rootNumber);
			_w.WriteAttributeString("assigningAuthorityName",authorityName);
			_w.WriteEndElement();
		}

		///<summary>Helper for GenerateCCD().  Performs a WriteStartElement, followed by any attributes.  Attributes must be in pairs: name, value.</summary>
		private static void Start(string elementName,params string[] attributes) {
			_w.WriteStartElement(elementName);
			for(int i=0;i<attributes.Length;i+=2) {
				_w.WriteAttributeString(attributes[i],attributes[i+1]);
			}
		}

		///<summary>Helper for GenerateCCD().  Performs a WriteEndElement.  The specified elementName is for readability only.</summary>
		private static void End(string elementName) {
			_w.WriteEndElement();
		}

		///<summary>Helper for GenerateCCD().  Performs a WriteStartElement, followed by any attributes, followed by a WriteEndElement.  Attributes must be in pairs: name, value.</summary>
		private static void StartAndEnd(string elementName,params string[] attributes) {
			_w.WriteStartElement(elementName);
			for(int i=0;i<attributes.Length;i+=2) {
				_w.WriteAttributeString(attributes[i],attributes[i+1]);
			}
			_w.WriteEndElement();
		}

		///<summary>Helper for GenerateCCD().  Performs a WriteAttributeString for each attribute.  Attributes must be in pairs: name, value.</summary>
		private static void Attribs(params string[] attributes) {
			for(int i=0;i<attributes.Length;i+=2) {
				_w.WriteAttributeString(attributes[i],attributes[i+1]);
			}
		}

		///<summary>Writes the element strElement name and writes the dateTime string in the required date format.  Will not write if year is before 1880.</summary>
		private static void DateText(string strElementName,DateTime dateTime) {
			Start(strElementName);
			if(dateTime.Year>1880) {
				_w.WriteString(dateTime.ToString("yyyyMMdd"));
			}
			End(strElementName);
		}

		///<summary>Writes the element strElement name and writes the dateTime in the required date format into the value attribute.
		///Will write nullFlavor="UNK" instead of value if year is before 1880.</summary>
		private static void DateElement(string strElementName,DateTime dateTime) {
			Start(strElementName);
			if(dateTime.Year<1880) {
				Attribs("nullFlavor","UNK");
			}
			else {
				Attribs("value",dateTime.ToString("yyyyMMdd"));
			}
			End(strElementName);
		}

		///<summary>Writes the element strElement name and writes the dateTime in the required date format into the value attribute.
		///Will write nullFlavor="UNK" instead of value if year is before 1880.</summary>
		private static void TimeElement(string strElementName,DateTime dateTime) {
			Start(strElementName);
			if(dateTime.Year<1880) {
				Attribs("nullFlavor","UNK");
			}
			else {
				Attribs("value",dateTime.ToString("yyyyMMddHHmmsszzz").Replace(":",""));
			}
			End(strElementName);
		}

		public static bool IsCCD(string strXml) {
			XmlDocument doc=new XmlDocument();
			try {
				doc.LoadXml(strXml);
			}
			catch {
				return false;
			}
			if(doc.DocumentElement.Name.ToLower()=="clinicaldocument") {//CCD and CCDA
				return true;
			}
			else if(doc.DocumentElement.Name.ToLower()=="continuityofcarerecord" || doc.DocumentElement.Name.ToLower()=="ccr:continuityofcarerecord") {//CCR
				return true;
			}
			return false;
		}

		///<summary>Returns the PatNum for the unique patient who matches the patient name and birthdate within the CCD document xmlDocCcd.
		///Returns 0 if there are no patient matches.  Returns the first match if there are multiple matches (unlikely).</summary>
		public static long GetCCDpat(XmlDocument xmlDocCcd) {
			XmlNodeList xmlNodeList=xmlDocCcd.GetElementsByTagName("patient");//According to the CCD documentation, there should be one patient node, or no patient node.
			if(xmlNodeList.Count==0) {
				return 0;//No patient node, therefore no patient to match.
			}
			XmlNode xmlNodePat=xmlNodeList[0];
			string fName="";
			string lName="";
			DateTime birthDate=DateTime.MinValue;
			for(int i=0;i<xmlNodePat.ChildNodes.Count;i++) {
				if(xmlNodePat.ChildNodes[i].Name.Trim().ToLower()=="name") {
					XmlNode xmlNodeName=xmlNodePat.ChildNodes[i];
					for(int j=0;j<xmlNodeName.ChildNodes.Count;j++) {
						if(xmlNodeName.ChildNodes[j].Name.Trim().ToLower()=="given") {
							if(fName=="") {//The first and middle names are both referred to as "given" name.  The first given name is the patient's first name.
								fName=xmlNodeName.ChildNodes[j].InnerText.Trim();
							}
						}
						else if(xmlNodeName.ChildNodes[j].Name.Trim().ToLower()=="family") {
							lName=xmlNodeName.ChildNodes[j].InnerText.Trim();
						}
					}
				}
				else if(xmlNodePat.ChildNodes[i].Name.Trim().ToLower()=="birthtime") {
					XmlNode xmlNodeBirthtime=xmlNodePat.ChildNodes[i];
					for(int j=0;j<xmlNodeBirthtime.Attributes.Count;j++) {
						if(xmlNodeBirthtime.Attributes[j].Name.Trim().ToLower()!="value") {
							continue;
						}
						string strBirthTimeVal=xmlNodeBirthtime.Attributes[j].Value;
						int birthYear=int.Parse(strBirthTimeVal.Substring(0,4));//Year will always be in the first 4 digits of the date, for all flavors of the HL7 TS type.
						int birthMonth=1;
						if(strBirthTimeVal.Length>=6) {
							birthMonth=int.Parse(strBirthTimeVal.Substring(4,2));//If specified, month will always be in the 5th-6th digits of the date, for all flavors of the HL7 TS type.
						}
						int birthDay=1;
						if(strBirthTimeVal.Length>=8) {
							birthDay=int.Parse(strBirthTimeVal.Substring(6,2));//If specified, day will always be in the 7th-8th digits of the date, for all flavors of the HL7 TS type.
						}
						birthDate=new DateTime(birthYear,birthMonth,birthDay);
					}
				}
			}
			//A match cannot be made unless the CCD message includes first and last name as well as a specified birthdate, 
			//because we do not want to automatically attach Direct messages to patients unless we are certain that the match makes sense.
			//The user can always manually attach the incoming Direct message to a patient if the automatic matching fails, so it is good that the automatic matching is strict.
			//Automatic matching is not required for EHR, but it is "highly recommended when possible" according to Drummond.
			if(lName=="" || fName=="" || birthDate.Year<1880) {
				return 0;
			}
			return Patients.GetPatNumByNameAndBirthday(lName,fName,birthDate);
		}

		///<summary>Recursive. Returns all nodes matching the specified tag name (case insensitive) which also have all of the specified attributes (case sensitive names).
		///Attributes must be listed in pairs by attribute name then attribute value.</summary>
		private static List<XmlNode> GetNodesByTagNameAndAttributes(XmlNode xmlNode,string strTagName,params string[] arrayAttributes) {
			//Test the current node for tag name and attributes.
			List<XmlNode> retVal=new List<XmlNode>();
			if(xmlNode.Name.Trim().ToLower()==strTagName.Trim().ToLower()) {//Tag name match.
				bool isAttributeMatch=true;
				for(int i=0;i<arrayAttributes.Length;i+=2) {
					string strAttributeName=arrayAttributes[i];
					string strAttributeValue=arrayAttributes[i+1];
					if(xmlNode.Attributes[strAttributeName].Value.Trim().ToLower()!=strAttributeValue.Trim().ToLower()) {
						isAttributeMatch=false;
						break;
					}
				}
				if(isAttributeMatch) {
					retVal.Add(xmlNode);
				}
			}
			//Test child nodes.
			for(int i=0;i<xmlNode.ChildNodes.Count;i++) {
				retVal.AddRange(GetNodesByTagNameAndAttributes(xmlNode.ChildNodes[i],strTagName,arrayAttributes));
			}
			return retVal;
		}

		private static List<XmlNode> GetParentNodes(List<XmlNode> listXmlNodes) {
			List<XmlNode> retVal=new List<XmlNode>();
			for(int i=0;i<listXmlNodes.Count;i++) {
				retVal.Add(listXmlNodes[i].ParentNode);
			}
			return retVal;
		}

		///<summary>Calls GetNodesByTagNameAndAttributes() for each item in listXmlNode.</summary>
		private static List<XmlNode> GetNodesByTagNameAndAttributesFromList(List <XmlNode> listXmlNode,string strTagName,params string[] arrayAttributes) {
			List<XmlNode> retVal=new List<XmlNode>();
			for(int i=0;i<listXmlNode.Count;i++) {
				retVal.AddRange(GetNodesByTagNameAndAttributes(listXmlNode[i],strTagName,arrayAttributes));
			}
			return retVal;
		}

		private static DateTime DateTimeFromString(string strDateTime) {
			string strDateTimeFormat="";
			if(strDateTime.Length==19) {
				strDateTimeFormat="yyyyMMddHHmmsszzz";
			}
			else if(strDateTime.Length==17) {
				strDateTimeFormat="yyyyMMddHHmmzzz";
			}
			else if(strDateTime.Length==8) {
				strDateTimeFormat="yyyyMMdd";
			}
			else if(strDateTime.Length==6) {
				strDateTimeFormat="yyyyMM";
			}
			else if(strDateTime.Length==4) {
				strDateTimeFormat="yyyy";
			}
			try {
				return DateTime.ParseExact(strDateTime,strDateTimeFormat,CultureInfo.CurrentCulture.DateTimeFormat);
			}
			catch {
			}
			return DateTime.MinValue;
		}

		///<summary>Gets the start date (aka low node) from the effectiveTime node passed in.  Returns the date time value set in the low node if present.  If low node does not exist, it returns the value within the effectiveTime node.  Returns MinValue if low attribute is "nullflavor" or if parsing fails.</summary>
		private static DateTime GetEffectiveTimeLow(XmlNode xmlNodeEffectiveTime) {
			DateTime strEffectiveTimeValue=DateTime.MinValue;
			List<XmlNode> listLowVals=GetNodesByTagNameAndAttributes(xmlNodeEffectiveTime,"low");
			if(listLowVals.Count>0 && listLowVals[0].Attributes["nullFlavor"]!=null) {
				return strEffectiveTimeValue;
			}
			if(listLowVals.Count>0 && listLowVals[0].Attributes["value"]!=null) {
				strEffectiveTimeValue=DateTimeFromString(listLowVals[0].Attributes["value"].Value);
			}
			else if(xmlNodeEffectiveTime.Attributes["value"]!=null) {
				strEffectiveTimeValue=DateTimeFromString(xmlNodeEffectiveTime.Attributes["value"].Value);
			}
			return strEffectiveTimeValue;
		}

		private static DateTime GetEffectiveTimeHigh(XmlNode xmlNodeEffectiveTime) {
			DateTime strEffectiveTimeValue=DateTime.MinValue;
			List<XmlNode> listLowVals=GetNodesByTagNameAndAttributes(xmlNodeEffectiveTime,"high");
			if(listLowVals.Count>0 && listLowVals[0].Attributes["nullFlavor"]!=null) {
				return strEffectiveTimeValue;
			}
			if(listLowVals.Count>0 && listLowVals[0].Attributes["value"]!=null) {
				strEffectiveTimeValue=DateTimeFromString(listLowVals[0].Attributes["value"].Value);
			}
			else {
				//We do not take the string from the xmlNodeEffectiveTime value attribute, because we need to be careful importing high/stop dates.
				//The examples suggest that th xmlNodeEffectiveTime value attribute will contain the minimum datetime.
			}
			return strEffectiveTimeValue;
		}

		///<summary>Fills listMedicationPats and listMedications using the information found in the CCD document xmlDocCcd.  Does NOT insert any records into the db.</summary>
		public static void GetListMedicationPats(XmlDocument xmlDocCcd,List<MedicationPat> listMedicationPats) {
			//The length of listMedicationPats and listMedications will be the same. The information in listMedications might have duplicates.
			//Neither list of objects will be inserted into the db, so there will be no primary or foreign keys.
			//List<XmlNode> listMedicationDispenseTemplates=GetNodesByTagNameAndAttributes(xmlDocCcd,"templateId","root","2.16.840.1.113883.10.20.22.4.18");//Medication Dispense template.
			List<XmlNode> listMedicationDispenseTemplates=GetNodesByTagNameAndAttributes(xmlDocCcd,"templateId","root","2.16.840.1.113883.10.20.22.4.16");//Medication Activity template.
			List<XmlNode> listSupply=GetParentNodes(listMedicationDispenseTemplates);//POCD_HD00040.xls line 485
			for(int i=0;i<listSupply.Count;i++) {
				//We have to start fairly high in the tree so that we can get the effective time if it is available.
				List<XmlNode> xmlNodeEffectiveTimes=GetNodesByTagNameAndAttributes(listSupply[i],"effectiveTime");//POCD_HD00040.xls line 492. Not required.
				DateTime dateTimeEffectiveLow=DateTime.MinValue;
				DateTime dateTimeEffectiveHigh=DateTime.MinValue;
				if(xmlNodeEffectiveTimes.Count>0) {
					XmlNode xmlNodeEffectiveTime=xmlNodeEffectiveTimes[0];
					dateTimeEffectiveLow=GetEffectiveTimeLow(xmlNodeEffectiveTime);
					dateTimeEffectiveHigh=GetEffectiveTimeHigh(xmlNodeEffectiveTime);
				}
				List<XmlNode> listMedicationActivityTemplates=GetNodesByTagNameAndAttributes(listSupply[i],"templateId","root","2.16.840.1.113883.10.20.22.4.23");//Medication Activity template.
				List<XmlNode> listProducts=GetParentNodes(listMedicationActivityTemplates);//List of manufaturedProduct and/or manufacturedLabeledDrug. POCD_HD00040.xls line 472.
				List<XmlNode> listCodes=GetNodesByTagNameAndAttributesFromList(listProducts,"code");
				for(int j=0;j<listCodes.Count;j++) {
					XmlNode xmlNodeCode=listCodes[j];
					string strCode=xmlNodeCode.Attributes["code"].Value;
					string strMedDescript=xmlNodeCode.Attributes["displayName"].Value;
					if(xmlNodeCode.Attributes["codeSystem"].Value!=strCodeSystemRxNorm) {
						continue;//We can only import RxNorms, because we have nowhere to pull in any other codes at this time (for example, SNOMED).
					}
					MedicationPat medicationPat=new MedicationPat();
					medicationPat.IsNew=true;//Needed for reconcile window to know this record is not in the db yet.
					medicationPat.RxCui=PIn.Long(strCode);
					medicationPat.MedDescript=strMedDescript;
					medicationPat.DateStart=dateTimeEffectiveLow;
					medicationPat.DateStop=dateTimeEffectiveHigh;
					listMedicationPats.Add(medicationPat);
				}
			}
			List<MedicationPat> listMedicationPatsNoDupes=new List<MedicationPat>();
			bool isDupe;
			for(int index=0;index<listMedicationPats.Count;index++) {
				isDupe=false;
				for(int index2=0;index2<listMedicationPatsNoDupes.Count;index2++) {
					if(listMedicationPatsNoDupes[index2].RxCui==listMedicationPats[index].RxCui) {
						isDupe=true;
						break;
					}
				}
				if(!isDupe) {
					listMedicationPatsNoDupes.Add(listMedicationPats[index]);
				}
			}
			listMedicationPats.Clear();
			for(int dupeIndex=0;dupeIndex<listMedicationPatsNoDupes.Count;dupeIndex++) {
				listMedicationPats.Add(listMedicationPatsNoDupes[dupeIndex]);
			}
		}

		///<summary>Fills listDiseases and listDiseaseDef using the information found in the CCD document xmlDocCcd.  Does NOT insert any records into the db.</summary>
		public static void GetListDiseases(XmlDocument xmlDocCcd,List<Disease> listDiseases,List<DiseaseDef> listDiseaseDef) {
			//The length of listDiseases and listDiseaseDef will be the same. The information in listDiseaseDef might have duplicates.
			//Neither list of objects will be inserted into the db, so there will be no primary or foreign keys.
			List<XmlNode> listProblemActTemplate=GetNodesByTagNameAndAttributes(xmlDocCcd,"templateId","root","2.16.840.1.113883.10.20.22.4.3");// problem act template.
			List<XmlNode> listProbs=GetParentNodes(listProblemActTemplate);
			for(int i=0;i<listProbs.Count;i++) {
				//We have to start fairly high in the tree so that we can get the effective time if it is available.
				List<XmlNode> xmlNodeEffectiveTimes=GetNodesByTagNameAndAttributes(listProbs[i],"effectiveTime");
				DateTime dateTimeEffectiveLow=DateTime.MinValue;
				DateTime dateTimeEffectiveHigh=DateTime.MinValue;
				if(xmlNodeEffectiveTimes.Count>0) {
					XmlNode xmlNodeEffectiveTime=xmlNodeEffectiveTimes[0];
					dateTimeEffectiveLow=GetEffectiveTimeLow(xmlNodeEffectiveTime);
					dateTimeEffectiveHigh=GetEffectiveTimeHigh(xmlNodeEffectiveTime);
				}
				List<XmlNode> listProblemObservTemplate=GetNodesByTagNameAndAttributes(listProbs[i],"templateId","root","2.16.840.1.113883.10.20.22.4.4");// problem act template.
				List<XmlNode> listProbObs=GetParentNodes(listProblemObservTemplate);
				List<XmlNode> listTypeCodes=GetNodesByTagNameAndAttributesFromList(listProbObs,"code");
				List<XmlNode> listCodes=GetNodesByTagNameAndAttributesFromList(listProbObs,"value");
				string probType=listTypeCodes[0].Attributes["code"].Value;
				string probCode=listCodes[0].Attributes["code"].Value;
				string probName=listCodes[0].Attributes["displayName"].Value;
				List<XmlNode> listStatusObservTemplate=GetNodesByTagNameAndAttributes(listProbs[i],"templateId","root","2.16.840.1.113883.10.20.22.4.6");// Status Observation template.
				List<XmlNode> listStatusObs=GetParentNodes(listStatusObservTemplate);
				List<XmlNode> listActive=GetNodesByTagNameAndAttributesFromList(listStatusObs,"value");
				Disease dis=new Disease();
				dis.SnomedProblemType=probType;
				dis.DateStart=dateTimeEffectiveLow;
				dis.IsNew=true;
				if(listActive.Count>0 && listActive[0].Attributes["code"].Value=="55561003") {//Active (qualifier value)
					dis.ProbStatus=ProblemStatus.Active;
				}
				else if(listActive.Count>0 && listActive[0].Attributes["code"].Value=="413322009") {//Problem resolved (finding)
					dis.ProbStatus=ProblemStatus.Resolved;
					dis.DateStop=dateTimeEffectiveHigh;
				}
				else {
					dis.ProbStatus=ProblemStatus.Inactive;
					dis.DateStop=dateTimeEffectiveHigh;
				}
				listDiseases.Add(dis);
				DiseaseDef disD=new DiseaseDef();
				disD.IsHidden=false;
				disD.IsNew=true;
				disD.SnomedCode=probCode;
				disD.DiseaseName=probName;
				listDiseaseDef.Add(disD);
			}
		}

		///<summary>Fills listAllergies and listAllergyDefs using the information found in the CCD document xmlDocCcd.  Inserts a medication in the db corresponding to the allergy.</summary>
		public static void GetListAllergies(XmlDocument xmlDocCcd,List<Allergy> listAllergies,List<AllergyDef> listAllergyDefs) {
			//The length of listAllergies and listAllergyDefs will be the same. The information in listAllergyDefs might have duplicates.
			//Neither list of objects will be inserted into the db, so there will be no primary or foreign keys.
			List<XmlNode> listAllergyProblemActTemplate=GetNodesByTagNameAndAttributes(xmlDocCcd,"templateId","root","2.16.840.1.113883.10.20.22.4.30");//Allergy problem act template.
			List<XmlNode> listActs=GetParentNodes(listAllergyProblemActTemplate);
			for(int i=0;i<listActs.Count;i++) {
				//We have to start fairly high in the tree so that we can get the effective time if it is available.
				List<XmlNode> xmlNodeEffectiveTimes=GetNodesByTagNameAndAttributes(listActs[i],"effectiveTime");//POCD_HD00040.xls line 492. Not required.
				DateTime dateTimeEffectiveLow=DateTime.MinValue;
				DateTime dateTimeEffectiveHigh=DateTime.MinValue;
				if(xmlNodeEffectiveTimes.Count>0) {
					XmlNode xmlNodeEffectiveTime=xmlNodeEffectiveTimes[0];
					dateTimeEffectiveLow=GetEffectiveTimeLow(xmlNodeEffectiveTime);
					dateTimeEffectiveHigh=GetEffectiveTimeHigh(xmlNodeEffectiveTime);
				}
				List<XmlNode> listAllergyObservationTemplates=GetNodesByTagNameAndAttributes(listActs[i],"templateId","root","2.16.840.1.113883.10.20.22.4.7");//Allergy observation template.
				List<XmlNode> listAllergy=GetParentNodes(listAllergyObservationTemplates);//List of Allergy Observations.
				List<XmlNode> listCodes=GetNodesByTagNameAndAttributesFromList(listAllergy,"value");
				#region Determine if Active
				bool isActive=true;
				string strStatus="";
				List<XmlNode> listAllergyObservationTemplatesActive=GetNodesByTagNameAndAttributes(listActs[i],"templateId","root","2.16.840.1.113883.10.20.22.4.28");//Allergy observation template.
				List<XmlNode> listAllergyActive=GetParentNodes(listAllergyObservationTemplatesActive);//List of Allergy Observations.
				List<XmlNode> listCodesActive=GetNodesByTagNameAndAttributesFromList(listAllergyActive,"value");
				if(listCodesActive.Count>0) {
					listCodes.Remove(listCodesActive[0]);
					XmlNode xmlNodeCode=listCodesActive[0];
					strStatus=xmlNodeCode.Attributes["code"].Value;
					if(xmlNodeCode.Attributes["codeSystem"].Value!=strCodeSystemSnomed) {
						continue;//We can only import Snomeds
					}
					isActive=(strStatus=="55561003");//Active (qualifier value)
				}
				#endregion
				#region Find Reaction Snomed
				List<XmlNode> listAllergyStatusObservationTemplates=GetNodesByTagNameAndAttributes(listActs[i],"templateId","root","2.16.840.1.113883.10.20.22.4.9");//Allergy status observation template.
				List<XmlNode> listAllergyStatus=GetParentNodes(listAllergyStatusObservationTemplates);//List of Allergy Observations.
				List<XmlNode> listAlgCodes=GetNodesByTagNameAndAttributesFromList(listAllergyStatus,"value");
				for(int j=0;j<listAlgCodes.Count;j++) {
					listCodes.Remove(listAlgCodes[j]);
					XmlNode xmlNodeCode=listAlgCodes[j];
					string strCodeReaction=xmlNodeCode.Attributes["code"].Value;
					string strAlgStatusDescript=xmlNodeCode.Attributes["displayName"].Value;
					if(xmlNodeCode.Attributes["codeSystem"].Value!=strCodeSystemSnomed) {
						continue;//We can only import Snomeds
					}
					Allergy allergy=new Allergy();
					allergy.IsNew=true;//Needed for reconcile window to know this record is not in the db yet.
					allergy.SnomedReaction=PIn.String(strCodeReaction);
					allergy.Reaction=PIn.String(strAlgStatusDescript);
					allergy.DateAdverseReaction=dateTimeEffectiveLow;
					allergy.StatusIsActive=isActive;
					listAllergies.Add(allergy);
				}
				#endregion
				#region Remove Severe Reaction
				List<XmlNode> listAllergySevereTemplates=GetNodesByTagNameAndAttributes(listActs[i],"templateId","root","2.16.840.1.113883.10.20.22.4.8");//Allergy observation template.
				List<XmlNode> listAllergySevere=GetParentNodes(listAllergySevereTemplates);//List of Allergy Observations.
				List<XmlNode> listCodesSevere=GetNodesByTagNameAndAttributesFromList(listAllergySevere,"value");
				for(int j=0;j<listCodesSevere.Count;j++) {
					listCodes.Remove(listCodesSevere[j]);
					XmlNode xmlNodeCode=listCodesSevere[j];
					string strCodeReaction=xmlNodeCode.Attributes["code"].Value;
					string strAlgStatusDescript=xmlNodeCode.Attributes["displayName"].Value;
					if(xmlNodeCode.Attributes["codeSystem"].Value!=strCodeSystemSnomed) {
						continue;//We can only import Snomeds
					}
				}
				#endregion
				#region Find RxNorm or Snomed
				string allergyDefName="";
				Medication med=new Medication();
				List<XmlNode> listRxCodes=GetNodesByTagNameAndAttributesFromList(listAllergy,"code");
				List<Medication> allergyMeds=new List<Medication>();
				for(int j=0;j<listRxCodes.Count;j++) {
					XmlNode xmlNodeCode=listRxCodes[j];
					if(xmlNodeCode.Attributes[0].Name!="code") {
						continue;
					}
					if(xmlNodeCode.Attributes["codeSystem"].Value!=strCodeSystemRxNorm) {
						continue;//We only want RxNorms here.
					}
					string strCodeRx=xmlNodeCode.Attributes["code"].Value;
					string strRxName=xmlNodeCode.Attributes["displayName"].Value;//Look into this being required or not.
					allergyDefName=strRxName;
					med=Medications.GetMedicationFromDbByRxCui(PIn.Long(strCodeRx));
					if(med==null) {
						med=new Medication();
						med.MedName=strRxName;
						med.RxCui=PIn.Long(strCodeRx);
						Medications.Insert(med);
						med.GenericNum=med.MedicationNum;
						Medications.Update(med);
					}
					allergyMeds.Add(med);
				}
				#endregion
				for(int j=0;j<listCodes.Count;j++) {
					XmlNode xmlNodeCode=listCodes[j];
					string strCode=xmlNodeCode.Attributes["code"].Value;
					if(xmlNodeCode.Attributes["codeSystem"].Value!=strCodeSystemSnomed) {
						continue;//We can only import Snomeds
					}
					AllergyDef allergyDef=new AllergyDef();
					allergyDef.IsNew=true;//Needed for reconcile window to know this record is not in the db yet.
					if(med.MedicationNum!=0) {
						allergyDef.MedicationNum=med.MedicationNum;
					}
					else {
						allergyDef.SnomedAllergyTo=PIn.String(strCode);
					}
					allergyDef.Description=allergyDefName;
					allergyDef.IsHidden=false;
					allergyDef.MedicationNum=allergyMeds[j].MedicationNum;
					#region Snomed type determination
					if(strCode=="419511003") {
						allergyDef.SnomedType=SnomedAllergy.AdverseReactionsToDrug;
					}
					else if(strCode=="418471000") {
						allergyDef.SnomedType=SnomedAllergy.AdverseReactionsToFood;
					}
					else if(strCode=="419199007") {
						allergyDef.SnomedType=SnomedAllergy.AdverseReactionsToSubstance;
					}
					else if(strCode=="418038007") {
						allergyDef.SnomedType=SnomedAllergy.AllergyToSubstance;
					}
					else if(strCode=="416098002") {
						allergyDef.SnomedType=SnomedAllergy.DrugAllergy;
					}
					else if(strCode=="59037007") {
						allergyDef.SnomedType=SnomedAllergy.DrugIntolerance;
					}
					else if(strCode=="414285001") {
						allergyDef.SnomedType=SnomedAllergy.FoodAllergy;
					}
					else if(strCode=="235719002") {
						allergyDef.SnomedType=SnomedAllergy.FoodIntolerance;
					}
					else if(strCode=="420134006") {
						allergyDef.SnomedType=SnomedAllergy.AdverseReactions;
					}
					else {
						allergyDef.SnomedType=SnomedAllergy.None;
					}
					#endregion
					listAllergyDefs.Add(allergyDef);
				}
			}
		}

		public static bool IsCcdEmailAttachment(EmailAttach emailAttach) {
			string strFilePathAttach=ODFileUtils.CombinePaths(EmailMessages.GetEmailAttachPath(),emailAttach.ActualFileName);
			if(Path.GetExtension(strFilePathAttach).ToLower()!=".xml") {
				return false;
			}
			string strTextXml=File.ReadAllText(strFilePathAttach);
			if(!EhrCCD.IsCCD(strTextXml)) {
				return false;
			}
			return true;
		}

		public static bool HasCcdEmailAttachment(EmailMessage emailMessage) {
			if(emailMessage.Attachments==null) {
				return false;
			}
			for(int i=0;i<emailMessage.Attachments.Count;i++) {
				if(EhrCCD.IsCcdEmailAttachment(emailMessage.Attachments[i])) {
					return true;
				}
			}
			return false;
		}

	}
}
