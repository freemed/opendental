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
		private const string strCodeSystemNameSnomed="SNOMED CT";
		///<summary>2.16.840.1.113883.6.88</summary>
		private const string strCodeSystemRxNorm="2.16.840.1.113883.6.88";
		private const string strCodeSystemNameRxNorm="RxNorm";
		///<summary>2.16.840.1.113883.6.1</summary>
		private const string strCodeSystemLoinc="2.16.840.1.113883.6.1";
		private const string strCodeSystemNameLoinc="LOINC";
		private static HashSet<string> _hashCcdIds;

		public static string GenerateCCD(Patient pat) {
			_hashCcdIds=new HashSet<string>();//The IDs only need to be unique within each CCD document.
			Medications.Refresh();
			XmlWriterSettings xmlSettings=new XmlWriterSettings();
			xmlSettings.Encoding=Encoding.UTF8;
			xmlSettings.OmitXmlDeclaration=true;
			xmlSettings.Indent=true;
			xmlSettings.IndentChars="   ";
			StringBuilder strBuilder=new StringBuilder();
			using(XmlWriter w=XmlWriter.Create(strBuilder,xmlSettings)) {
				//Begin Clinical Document
				w.WriteRaw("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n");
				w.WriteProcessingInstruction("xml-stylesheet","type=\"text/xsl\" href=\"ccd.xsl\"");
				w.WriteWhitespace("\r\n");
				w.WriteStartElement("ClinicalDocument","urn:hl7-org:v3");
				w.WriteAttributeString("xmlns","xsi",null,"http://www.w3.org/2001/XMLSchema-instance");
				w.WriteAttributeString("xsi","noNamespaceSchemaLocation",null,"Registry_Payment.xsd");
				w.WriteAttributeString("xsi","schemaLocation",null,"urn:hl7-org:v3 http://xreg2.nist.gov:8080/hitspValidation/schema/cdar2c32/infrastructure/cda/C32_CDA.xsd");
				w.WriteStartElement("realmCode");
				w.WriteAttributeString("code","US");
				w.WriteEndElement();//realmCode
				w.WriteStartElement("typeId");
				w.WriteAttributeString("extension","POCD_HD000040");
				w.WriteAttributeString("root","2.16.840.1.113883.1.3");//template id to assert use of the CCD standard
				w.WriteEndElement();//typeId
				w.WriteStartElement("templateId");
				w.WriteAttributeString("root","2.16.840.1.113883.3.27.1776");
				w.WriteAttributeString("assigningAuthorityName","CDA/R2");
				w.WriteEndElement();//templateId
				w.WriteStartElement("templateId");
				w.WriteAttributeString("root","2.16.840.1.113883.10.20.3");
				w.WriteAttributeString("assigningAuthorityName","HL7/CDT Header");
				w.WriteEndElement();//templateId
				w.WriteStartElement("templateId");
				w.WriteAttributeString("root","1.3.6.1.4.1.19376.1.5.3.1.1.1");
				w.WriteAttributeString("assigningAuthorityName","IHE/PCC");
				w.WriteEndElement();//templateId
				w.WriteStartElement("templateId");
				w.WriteAttributeString("root","2.16.840.1.113883.3.88.11.32.1");
				w.WriteAttributeString("assigningAuthorityName","HITSP/C32");
				w.WriteEndElement();//templateId
				w.WriteStartElement("id");
				w.WriteAttributeString("root","db734647-fc99-424c-a864-7e3cda82e703");
				w.WriteEndElement();
				w.WriteStartElement("code");
				w.WriteAttributeString("code","34133-9");
				w.WriteAttributeString("codeSystemName",strCodeSystemNameLoinc);
				w.WriteAttributeString("codeSystem",strCodeSystemLoinc);
				w.WriteAttributeString("displayName","Summary of episode note");
				w.WriteEndElement();
				w.WriteElementString("title","Continuity of Care Document");
				w.WriteStartElement("effectiveTime");
				string ccrCreationDateTime=DateTime.Now.ToString("yyyyMMddHHmmsszzz").Replace(":","");
				w.WriteAttributeString("value",ccrCreationDateTime);
				w.WriteEndElement();
				w.WriteStartElement("confidentialityCode");//not documented
				w.WriteAttributeString("code","N");
				w.WriteAttributeString("codeSystem","2.16.840.1.113883.5.25");
				w.WriteEndElement();
				w.WriteStartElement("languageCode");
				w.WriteAttributeString("code","en-US");
				w.WriteEndElement();
				w.WriteStartElement("recordTarget");
				w.WriteStartElement("patientRole");
				w.WriteStartElement("id");//not documented
				w.WriteAttributeString("extension",pat.PatNum.ToString());//"996-756-495");
				w.WriteAttributeString("root","2.16.840.1.113883.19.5");
				w.WriteEndElement();
				w.WriteStartElement("addr");
				w.WriteAttributeString("use","HP");
				w.WriteStartElement("streetAddressLine");
				w.WriteString(pat.Address.ToString());
				w.WriteEndElement();
				w.WriteStartElement("streetAddressLine");
				w.WriteString(pat.Address2.ToString());
				w.WriteEndElement();
				w.WriteStartElement("city");
				w.WriteString(pat.City.ToString());
				w.WriteEndElement();
				w.WriteStartElement("state");
				w.WriteString(pat.State.ToString());
				w.WriteEndElement();
				w.WriteStartElement("country");
				w.WriteString("");
				w.WriteEndElement();
				w.WriteEndElement();//addr
				w.WriteStartElement("telecom");
				w.WriteEndElement();//telecom
				w.WriteStartElement("patient");
				w.WriteStartElement("name");
				w.WriteAttributeString("use","L");
				w.WriteStartElement("given");
				w.WriteString(pat.FName.ToString());
				w.WriteEndElement();
				w.WriteStartElement("given");
				w.WriteString(pat.MiddleI.ToString());
				w.WriteEndElement();
				w.WriteStartElement("family");
				w.WriteString(pat.LName.ToString());
				w.WriteEndElement();
				w.WriteStartElement("suffix");
				w.WriteAttributeString("qualifier","TITLE");
				w.WriteString(pat.Title.ToString());
				w.WriteEndElement();//suffix
				w.WriteEndElement();//name
				w.WriteStartElement("administrativeGenderCode");
				if(pat.Gender==PatientGender.Female) {
					w.WriteAttributeString("code","F");
				}
				else {
					w.WriteAttributeString("code","M");
				}
				w.WriteAttributeString("codeSystem","2.16.840.1.113883.5.1");
				w.WriteEndElement();//administrativeGenderCode
				w.WriteStartElement("birthTime");
				w.WriteAttributeString("value",pat.Birthdate.ToString("yyyyMMdd"));//missing birthdate should be represented with null
				w.WriteEndElement();//birthTime
				w.WriteEndElement();//patient
				w.WriteEndElement();//patientRole
				w.WriteEndElement();//recordTarget
				//author--------------------------------------------------------------------------------------------------
				w.WriteStartElement("author");
				w.WriteStartElement("time");
				w.WriteAttributeString("value",DateTime.Now.ToString("yyyyMMddHHmmsszzz").Replace(":",""));
				w.WriteEndElement();//time
				w.WriteStartElement("assignedAuthor");
				w.WriteStartElement("id");
				w.WriteAttributeString("root","20cf14fb-b65c-4c8c-a54d-b0cca834c18c");//not documented
				w.WriteEndElement();//id
				w.WriteElementString("addr","");
				w.WriteElementString("telecom","");
				w.WriteStartElement("assignedPerson");
				w.WriteStartElement("name");
				w.WriteEndElement();//name
				w.WriteEndElement();//assignedPerson
				w.WriteStartElement("representedOrganization");
				w.WriteElementString("name",PrefC.GetString(PrefName.PracticeTitle));
				w.WriteElementString("telecom","");
				w.WriteElementString("addr","");
				w.WriteEndElement();//representedOrganization
				w.WriteEndElement();//assignedAuthor
				w.WriteEndElement();//author
				//custodian------------------------------------------------------------------------------------------------
				w.WriteStartElement("custodian");
				w.WriteStartElement("assignedCustodian");
				w.WriteStartElement("representedCustodianOrganization");
				w.WriteElementString("id","");
				w.WriteElementString("name","");
				w.WriteElementString("telecom","");
				w.WriteElementString("addr","");
				w.WriteEndElement();//representedCustodianOrganization
				w.WriteEndElement();//assignedCustodian
				w.WriteEndElement();//custodian
				//legalAuthenticator--------------------------------------------------------------------------------------
				//this is used by the style sheet to create the line at the bottom: Electronically Generated by: on date
				Start(w,"legalAuthenticator");
				StartAndEnd(w,"time","value",DateTime.Now.ToString("yyyyMMddHHmmsszzz").Replace(":",""));
				StartAndEnd(w,"signatureCode","code","S");
				Start(w,"assignedEntity");
				StartAndEnd(w,"id","nullFlavor","NI");
				StartAndEnd(w,"addr");
				StartAndEnd(w,"telecom");
				Start(w,"assignedPerson");
				StartAndEnd(w,"name");
				End(w,"assignedPerson");
				Start(w,"representedOrganization");
				StartAndEnd(w,"id","root","2.16.840.1.113883.19.5");
				w.WriteElementString("name",PrefC.GetString(PrefName.PracticeTitle));
				StartAndEnd(w,"telecom");
				StartAndEnd(w,"addr");
				End(w,"representedOrganization");
				End(w,"assignedEntity");
				End(w,"legalAuthenticator");
				w.WriteComment(@"
=====================================================================================================
Body
=====================================================================================================");
				w.WriteStartElement("component");
				w.WriteStartElement("structuredBody");
				GenerateCcdSectionAllergies(w,pat);
				GenerateCcdSectionEncounters(w,pat);
				GenerateCcdSectionFunctionalStatus(w,pat);
				GenerateCcdSectionImmunizations(w,pat);
				GenerateCcdSectionMedications(w,pat);
				GenerateCcdSectionPlanOfCare(w,pat);
				GenerateCcdSectionProblems(w,pat);
				GenerateCcdSectionReasonForReferral(w,pat);
				GenerateCcdSectionResults(w,pat);//Lab Results
				GenerateCcdSectionVitalSigns(w,pat);
				End(w,"structuredBody");
				End(w,"component");
				End(w,"ClinicalDocument");
			}
			SecurityLogs.MakeLogEntry(Permissions.Copy,pat.PatNum,"CCD generated");			//Create audit log entry.
			return strBuilder.ToString();
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionAllergies(XmlWriter w,Patient pat) {
			w.WriteComment(@"
=====================================================================================================
Allergies
=====================================================================================================");
			List<Allergy> listAllergy=Allergies.Refresh(pat.PatNum);
			AllergyDef allergyDef;
			Medication med;
			w.WriteStartElement("component");
			w.WriteStartElement("section");
			TemplateId(w,"2.16.840.1.113883.10.20.22.2.6.1");
			w.WriteComment("Allergies section template");
			StartAndEnd(w,"code","code","48765-2","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Allergies");
			w.WriteStartElement("title");
			w.WriteString("Allergies and Adverse Reactions");
			w.WriteEndElement();
			w.WriteStartElement("text");//The following text will be parsed as html with a style sheet to be human readable.
			w.WriteStartElement("table");
			w.WriteAttributeString("width","100%");
			w.WriteAttributeString("border","1");
			w.WriteStartElement("thead");
			w.WriteStartElement("tr");
			w.WriteStartElement("th");
			w.WriteString("Substance");
			w.WriteEndElement();
			w.WriteStartElement("th");
			w.WriteString("Reaction");
			w.WriteEndElement();//
			w.WriteStartElement("th");
			w.WriteString("Allergy Type");
			w.WriteEndElement();
			w.WriteStartElement("th");
			w.WriteString("Status");
			w.WriteEndElement();
			w.WriteEndElement();//End tr
			w.WriteEndElement();//End thead
			w.WriteStartElement("tbody");
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
				w.WriteStartElement("tr");
				w.WriteStartElement("td");
				w.WriteString(med.RxCui.ToString()+" - "+med.MedName);
				w.WriteEndElement();
				w.WriteStartElement("td");
				w.WriteString(listAllergy[i].Reaction);
				w.WriteEndElement();
				w.WriteStartElement("td");
				w.WriteString(AllergyDefs.GetSnomedAllergyDesc(allergyDef.SnomedType));
				w.WriteEndElement();
				w.WriteStartElement("td");
				w.WriteString(status);
				w.WriteEndElement();
				w.WriteEndElement();//End tr
			}
			w.WriteEndElement();//End tbody
			w.WriteEndElement();//End table
			w.WriteEndElement();//End text
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
				Start(w,"entry","typeCode","DRIV");
				Start(w,"act","classCode","ACT","moodCode","EVN");
				TemplateId(w,"2.16.840.1.113883.10.20.22.4.30");
				BuildIdAndWrite(w);
				StartAndEnd(w,"code","code","48765-2","codeSystem",strCodeSystemLoinc,"codeSystemName","LOINC","displayName","Allergies and adverse reactions");
				if(listAllergy[i].StatusIsActive) {
					StartAndEnd(w,"statusCode","code","active");
				}
				else {
					StartAndEnd(w,"statusCode","code","completed");
				}
				Start(w,"effectiveTime");
				if(listAllergy[i].StatusIsActive) {
					StartAndEnd(w,"low","value",listAllergy[i].DateTStamp.ToString("yyyymmdd"));
					StartAndEnd(w,"high","nullFlavor","UNK");
				}
				else {
					StartAndEnd(w,"low","nullFlavor","UNK");
					StartAndEnd(w,"high","value",listAllergy[i].DateTStamp.ToString("yyyymmdd"));
				}
				End(w,"effectiveTime");
				Start(w,"entryRelationship","typeCode","SUBJ");
				Start(w,"observation","classCode","OBS","moodCode","EVN");
				w.WriteComment("Allergy Observation template");
				TemplateId(w,"2.16.840.1.113883.10.20.22.4.7");
				BuildIdAndWrite(w);
				StartAndEnd(w,"code","code","ASSERTION","codeSystem","2.16.840.1.113883.5.4");//Fixed Value
				StartAndEnd(w,"statusCode","code","completed");//fixed value (required)
				Start(w,"effectiveTime");
				StartAndEnd(w,"low","nullFlavor","UNK");
				StartAndEnd(w,"high","value",listAllergy[i].DateTStamp.ToString("yyyymmdd"));
				End(w,"effectiveTime");
				Start(w,"value");
				w.WriteAttributeString("xsi","type",null,"CD");
				Attribs(w,"code",allergyType,"displayName",allergyTypeName,"codeSystem",strCodeSystemSnomed,"codeSystemName","SNOMED CT");
				End(w,"value");
				Start(w,"participant","typeCode","CSM");
				Start(w,"participantRole","classCode","MANU");
				Start(w,"playingEntity","classCode","MMAT");
				StartAndEnd(w,"code","code",med.RxCui.ToString(),"displayName",med.MedName,"codeSystem",strCodeSystemRxNorm,"codeSystemName","RxNorm");
				End(w,"playingEntity");
				End(w,"participantRole");
				End(w,"participant");
				Start(w,"entryRelationship","typeCode","SUBJ");
				Start(w,"observation","classCode","OBS","moodCode","EVN");
				w.WriteComment("Allergy Status Observation template");
				TemplateId(w,"2.16.840.1.113883.10.20.22.4.28");
				StartAndEnd(w,"code","code","33999-4","codeSystem",strCodeSystemLoinc,"codeSystemName","LOINC","displayName","Status");
				StartAndEnd(w,"statusCode","code","completed");//fixed value (required)
				if(status=="Active") {
					Start(w,"value");
					w.WriteAttributeString("xsi","type",null,"CE");
					Attribs(w,"code","55561003","codeSystem",strCodeSystemSnomed,"displayName",status);
					End(w,"value");
				}
				else {
					Start(w,"value");
					w.WriteAttributeString("xsi","type",null,"CE");
					Attribs(w,"code","73425007","codeSystem",strCodeSystemSnomed,"displayName",status);
					End(w,"value");
				}
				End(w,"observation");
				End(w,"entryRelationship");
				Start(w,"entryRelationship","typeCode","SUBJ");
				Start(w,"observation","classCode","OBS","moodCode","EVN");
				w.WriteComment("Reaction Observation template");
				TemplateId(w,"2.16.840.1.113883.10.20.22.4.9");
				BuildIdAndWrite(w);
				StartAndEnd(w,"code","nullFlavor","NA");//Unknown why this is null, but can't find a good example of this
				StartAndEnd(w,"statusCode","code","completed");//fixed value (required)
				Start(w,"effectiveTime");
				if(listAllergy[i].StatusIsActive) {
					StartAndEnd(w,"low","value",listAllergy[i].DateTStamp.ToString("yyyymmdd"));
				}
				else {
					StartAndEnd(w,"low","nullFlavor","UNK");
				}
				End(w,"effectiveTime");
				Start(w,"value");
				w.WriteAttributeString("xsi","type",null,"CD");
				Attribs(w,"code",listAllergy[i].SnomedReaction,"codeSystem",strCodeSystemSnomed,"displayName",listAllergy[i].Reaction);
				End(w,"value");
				End(w,"observation");
				End(w,"entryRelationship");
				End(w,"observation");
				End(w,"entryRelationship");
				End(w,"act");
				End(w,"entry");
			}
			End(w,"section");
			End(w,"component");
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionEncounters(XmlWriter w,Patient pat) {
			//TODO:
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionFunctionalStatus(XmlWriter w,Patient pat) {
			//TODO:
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionImmunizations(XmlWriter w,Patient pat) {
			//TODO:
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionMedications(XmlWriter w,Patient pat) {
			w.WriteComment(@"
=====================================================================================================
Medications
=====================================================================================================");
			List<MedicationPat> listMedPat=MedicationPats.Refresh(pat.PatNum,true);
			Medication med;
			w.WriteStartElement("component");
			w.WriteStartElement("section");
			TemplateId(w,"2.16.840.1.113883.10.20.22.2.1.1");//Required Medication Section
			w.WriteComment("Medications section template");
			StartAndEnd(w,"code","code","10160-0","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","History of medication use");
			w.WriteElementString("title","Medications");
			w.WriteStartElement("text");//The following text will be parsed as html with a style sheet to be human readable.
			w.WriteStartElement("table");
			w.WriteAttributeString("width","100%");
			w.WriteAttributeString("border","1");
			w.WriteStartElement("thead");
			w.WriteStartElement("tr");
			w.WriteStartElement("th");
			w.WriteString("Medication");
			w.WriteEndElement();
			w.WriteStartElement("th");
			w.WriteString("Directions");
			w.WriteEndElement();
			w.WriteStartElement("th");
			w.WriteString("Start Date");
			w.WriteEndElement();
			w.WriteStartElement("th");
			w.WriteString("End Date");
			w.WriteEndElement();
			w.WriteStartElement("th");
			w.WriteString("Status");
			w.WriteEndElement();
			w.WriteStartElement("th");
			w.WriteString("Indications");
			w.WriteEndElement();
			w.WriteStartElement("th");
			w.WriteString("Fill Instructions");
			w.WriteEndElement();
			w.WriteEndElement();//End tr
			w.WriteEndElement();//End thead
			w.WriteStartElement("tbody");
			for(int i=0;i<listMedPat.Count;i++) {
				if(listMedPat[i].RxCui==0) {
					continue;
				}
				if(listMedPat[i].MedicationNum==0) {
					continue;
				}
				med=Medications.GetMedication(listMedPat[i].MedicationNum);
				w.WriteStartElement("tr");
				w.WriteStartElement("td");
				w.WriteString(med.MedName);//Medication
				w.WriteEndElement();
				w.WriteStartElement("td");
				w.WriteString(med.Notes);//Directions
				w.WriteEndElement();
				w.WriteStartElement("td");
				w.WriteString(listMedPat[i].DateStart.ToShortDateString());//Start Date
				w.WriteEndElement();
				w.WriteStartElement("td");
				if(listMedPat[i].DateStop.Year>1880) {
					w.WriteString(listMedPat[i].DateStop.ToShortDateString());//End Date
				}
				else {
					w.WriteString("");
				}
				w.WriteEndElement();
				w.WriteStartElement("td");
				w.WriteString(MedicationPats.IsMedActive(listMedPat[i])?"Active":"Inactive");//Status
				w.WriteEndElement();
				w.WriteStartElement("td");
				w.WriteString(listMedPat[i].PatNote);//Indications
				w.WriteEndElement();
				w.WriteStartElement("td");
				w.WriteString(listMedPat[i].MedDescript);//Fill Instructions
				w.WriteEndElement();
				w.WriteEndElement();//End tr
			}
			w.WriteEndElement();//End tbody
			w.WriteEndElement();//End table
			w.WriteEndElement();//End text
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
				Start(w,"entry","typeCode","DRIV");
				Start(w,"substanceAdministration","classCode","SBADM","moodCode","EVN");
				TemplateId(w,"2.16.840.1.113883.10.20.22.4.16");
				w.WriteComment("Medication activity template");
				BuildIdAndWrite(w);
				StartAndEnd(w,"statusCode","code","completed");//Fixed Value
				Start(w,"effectiveTime");
				w.WriteAttributeString("xsi","type",null,"IVL_TS");
				StartAndEnd(w,"low","value",listMedPat[i].DateStart.ToString("yyyymmdd"));
				StartAndEnd(w,"high","value",listMedPat[i].DateStop.ToString("yyyymmdd"));
				End(w,"effectiveTime");
				Start(w,"consumable");
				Start(w,"manufacturedProduct");
				TemplateId(w,"2.16.840.1.113883.10.20.22.4.23");
				BuildIdAndWrite(w);
				Start(w,"manufacturedMaterial");
				Start(w,"code","code",rxCui.ToString(),"codeSystem",strCodeSystemRxNorm,"displayName",strMedName,"codeSystemName",strCodeSystemNameRxNorm);
				End(w,"code");
				End(w,"manufacturedMaterial");
				End(w,"manufacturedProduct");
				End(w,"consumable");
				End(w,"substanceAdministration");
				End(w,"entry");
			}
			w.WriteEndElement();//section
			w.WriteEndElement();//component: Medication
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionPlanOfCare(XmlWriter w,Patient pat) {
			w.WriteComment(@"
=====================================================================================================
Care Plan
=====================================================================================================");
			Start(w,"component");
			Start(w,"section");
			TemplateId(w,"2.16.840.1.113883.10.20.22.2.10","HL7 CCD");
			w.WriteComment("Plan of Care section template");
			StartAndEnd(w,"code","code","18776-5","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Treatment plan");
			Start(w,"title");
			w.WriteString("Care Plan");
			End(w,"title");
			Start(w,"text");//The following text will be parsed as html with a style sheet to be human readable.
			Start(w,"table","width","100%","border","1");
			Start(w,"thead");
			Start(w,"tr");
			Start(w,"th");
			w.WriteString("Planned Activity");
			End(w,"th");
			Start(w,"th");
			w.WriteString("Planned Date");
			End(w,"th");
			End(w,"tr");
			End(w,"thead");
			Start(w,"tbody");
			List<EhrCarePlan> listEhrCarePlans=EhrCarePlans.Refresh(pat.PatNum);
			for(int i=0;i<listEhrCarePlans.Count;i++) {
				Start(w,"tr");
				Start(w,"td");
				w.WriteString(listEhrCarePlans[i].Instructions);
				End(w,"td");
				Start(w,"td");
				WriteDate(w,listEhrCarePlans[i].DatePlanned);
				End(w,"td");
				End(w,"tr");
			}
			End(w,"tbody");
			End(w,"table");
			End(w,"text");
			for(int i=0;i<listEhrCarePlans.Count;i++) {
				Start(w,"entry","typeCode","DRIV");
				Start(w,"act","classCode","ACT","moodCode","INT");
				TemplateId(w,"2.16.840.1.113883.10.20.22.4.20");
				w.WriteComment("Instructions template");
				Start(w,"code");
				w.WriteAttributeString("type","xsi","CE");
				Snomed snomedEducation=Snomeds.GetByCode(listEhrCarePlans[i].SnomedEducation);
				Attribs(w,"code",snomedEducation.SnomedCode,"codeSystem",strCodeSystemSnomed,"displayName",snomedEducation.Description);
				Start(w,"text");
				w.WriteString(listEhrCarePlans[i].Instructions);
				End(w,"text");
				StartAndEnd(w,"statusCode","code","completed");
				End(w,"act");
				End(w,"entry");
			}
			End(w,"section");
			End(w,"component");
		}

		///<summary>Helper for GenerateCCD().  Problem section.</summary>
		private static void GenerateCcdSectionProblems(XmlWriter w,Patient pat) {
			w.WriteComment(@"
=====================================================================================================
Problems
=====================================================================================================");
			List<Disease> listProblem=Diseases.Refresh(pat.PatNum);
			string status="Inactive";
			string statusCode="73425007";
			string statusOther="active";
			w.WriteStartElement("component");
			w.WriteStartElement("section");
			TemplateId(w,"2.16.840.1.113883.10.20.22.2.5.1");
			w.WriteComment("Problems section template");
			StartAndEnd(w,"code","code","11450-4","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Problem list");
			w.WriteElementString("title","PROBLEMS");
			w.WriteStartElement("text");
			StartAndEnd(w,"content","ID","problems");
			Start(w,"list","listType","ordered");
			for(int i=0;i<listProblem.Count;i++) {//Fill Problems Table
				if(listProblem[i].SnomedProblemType!="55607006") {
					continue;
				}
				w.WriteStartElement("item");
				w.WriteString(DiseaseDefs.GetName(listProblem[i].DiseaseDefNum)+" : "+"Status - ");
				if(listProblem[i].ProbStatus==ProblemStatus.Active) {
					w.WriteString("Active");
					status="Active";
					statusCode="55561003";
					statusOther="active";
				}
				else if(listProblem[i].ProbStatus==ProblemStatus.Inactive) {
					w.WriteString("Inactive");
					status="Inactive";
					statusCode="73425007";
					statusOther="completed";
				}
				else {
					w.WriteString("Resolved");
					status="Resolved";
					statusCode="413322009";
					statusOther="completed";
				}
				w.WriteEndElement();//item end
			}
			w.WriteEndElement();//list end
			w.WriteEndElement();//text end
			for(int i=0;i<listProblem.Count;i++) {//Fill Problems Info
				if(listProblem[i].SnomedProblemType!="55607006") {
					continue;
				}
				Start(w,"entry","typeCode","DRIV");
				Start(w,"act","classCode","ACT","moodCode","EVN");
				w.WriteComment("Problem Concern Act template");//Concern Act Section
				TemplateId(w,"2.16.840.1.113883.10.20.22.4.3");
				BuildIdAndWrite(w);
				StartAndEnd(w,"code","code","55607006","codeSystem","2.16.840.1.113883.6.96","displayName","Problem");
				StartAndEnd(w,"statusCode","code",statusOther);
				Start(w,"effectiveTime");
				StartAndEnd(w,"low","value",listProblem[i].DateStart.ToString("yyyymmdd"));
				if(listProblem[i].DateStop.Year>1880) {
					StartAndEnd(w,"high","value",listProblem[i].DateStop.ToString("yyyymmdd"));
				}
				End(w,"effectiveTime");
				Start(w,"entryRelationship","typeCode","SUBJ");
				Start(w,"observation","classCode","OBS","moodCode","EVN");
				w.WriteComment("Problem Observation template");//Observation Section
				TemplateId(w,"2.16.840.1.113883.10.20.22.4.4");
				BuildIdAndWrite(w);
				StartAndEnd(w,"code","code",listProblem[i].SnomedProblemType,"codeSystem","2.16.840.1.113883.6.96","displayName",Snomeds.GetByCode(listProblem[i].SnomedProblemType).Description);
				if(listProblem[i].ProbStatus==ProblemStatus.Active) {
					StartAndEnd(w,"statusCode","code","active");
				}
				else {
					StartAndEnd(w,"statusCode","code","completed");
				}
				Start(w,"effectiveTime");
				StartAndEnd(w,"low","value",listProblem[i].DateStart.ToString("yyyymmdd"));
				End(w,"effectiveTime");
				Start(w,"value");
				w.WriteAttributeString("xsi","type",null,"CD");
				Attribs(w,"code",DiseaseDefs.GetItem(listProblem[i].DiseaseDefNum).SnomedCode,"codeSystem",strCodeSystemSnomed,"displayName",DiseaseDefs.GetItem(listProblem[i].DiseaseDefNum).DiseaseName);
				End(w,"value");
				Start(w,"entryRelationship","typeCode","REFR");
				Start(w,"observation","classCode","OBS","moodCode","EVN");
				w.WriteComment("Status Observation template");//Status Observation Section
				TemplateId(w,"2.16.840.1.113883.10.20.22.4.6");
				Start(w,"code");
				w.WriteAttributeString("xsi","type",null,"CE");
				Attribs(w,"code","33999-4","codeSystem",strCodeSystemLoinc,"codeSystemName","LOINC","displayName","Status");
				End(w,"cpde");
				if(listProblem[i].ProbStatus==ProblemStatus.Active) {
					StartAndEnd(w,"statusCode","code","active");
				}
				else {
					StartAndEnd(w,"statusCode","code","completed");
				}
				Start(w,"value");
				w.WriteAttributeString("xsi","type",null,"CD");
				Attribs(w,"code",statusCode,"codeSystem",strCodeSystemSnomed,"displayName",status);
				End(w,"value");
				End(w,"observation");
				End(w,"entryRelationship");
				End(w,"observation");
				End(w,"entryRelationship");
				End(w,"act");
				End(w,"entry");
			}
			End(w,"section");
			End(w,"component");
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionReasonForReferral(XmlWriter w,Patient pat) {
			//TODO:
		}

		///<summary>Helper for GenerateCCD().  Exports Labs.</summary>
		private static void GenerateCcdSectionResults(XmlWriter w,Patient pat) {
			w.WriteComment(@"
=====================================================================================================
Laboratory Test Results
=====================================================================================================");
			List<LabResult> listLabResult=LabResults.GetAllForPatient(pat.PatNum);
			LabPanel labPanel;
			w.WriteStartElement("component");
			w.WriteStartElement("section");
			TemplateId(w,"2.16.840.1.113883.3.88.11.83.122","HITSP/C83");
			TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.3.28","IHE PCC");
			w.WriteComment("Diagnostic Results section template");
			StartAndEnd(w,"code","code","30954-2","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Results");
			w.WriteElementString("title","Diagnostic Results");
			w.WriteStartElement("text");//The following text will be parsed as html with a style sheet to be human readable.
			w.WriteStartElement("table");
			w.WriteAttributeString("width","100%");
			w.WriteAttributeString("border","1");
			w.WriteStartElement("thead");
			w.WriteStartElement("tr");
			w.WriteStartElement("th");
			w.WriteString("LOINC Code");
			w.WriteEndElement();
			w.WriteStartElement("th");
			w.WriteString("Test");
			w.WriteEndElement();//
			w.WriteStartElement("th");
			w.WriteString("Result");
			w.WriteEndElement();
			w.WriteStartElement("th");
			w.WriteString("Abnormal Flag");
			w.WriteEndElement();
			w.WriteStartElement("th");
			w.WriteString("Date Performed");
			w.WriteEndElement();
			w.WriteEndElement();//tr
			w.WriteEndElement();//thead
			w.WriteStartElement("tbody");
			for(int i=0;i<listLabResult.Count;i++) {
				w.WriteStartElement("tr");
				w.WriteStartElement("td");
				w.WriteString(listLabResult[i].TestID);
				w.WriteEndElement();
				w.WriteStartElement("td");
				w.WriteString(listLabResult[i].TestName);
				w.WriteEndElement();
				w.WriteStartElement("td");
				w.WriteString(listLabResult[i].ObsValue+" "+listLabResult[i].ObsUnits);
				w.WriteEndElement();
				w.WriteStartElement("td");
				w.WriteString(listLabResult[i].AbnormalFlag.ToString());
				w.WriteEndElement();
				w.WriteStartElement("td");
				w.WriteString(listLabResult[i].DateTimeTest.ToShortDateString());
				w.WriteEndElement();
				w.WriteEndElement();//tr
			}
			w.WriteEndElement();//tbody
			w.WriteEndElement();//table
			w.WriteEndElement();//text
			for(int i=0;i<listLabResult.Count;i++) {
				labPanel=LabPanels.GetOne(listLabResult[i].LabPanelNum);
				Start(w,"entry","typeCode","DRIV");
				Start(w,"organizer","classCode","BATTERY","moodCode","EVN");
				StartAndEnd(w,"templateId","root","2.16.840.1.113883.10.20.1.32");//no authority name
				w.WriteComment("Result organizer template");
				StartAndEnd(w,"id","root","7d5a02b0-67a4-11db-bd13-0800200c9a66");
				StartAndEnd(w,"code","code",labPanel.ServiceId,"codeSystem",strCodeSystemSnomed,"displayName",labPanel.ServiceName);
				StartAndEnd(w,"statusCode","code","completed");
				StartAndEnd(w,"effectiveTime","value",listLabResult[i].DateTimeTest.ToString("yyyyMMddHHmm"));
				Start(w,"component");
				Start(w,"procedure","classCode","PROC","moodCode","EVN");
				TemplateId(w,"2.16.840.1.113883.3.88.11.83.17","HITSP C83");
				TemplateId(w,"2.16.840.1.113883.10.20.1.29","CCD");//procedure activity template id, according to pages 103-104 of CCD-final.pdf
				TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.4.19","IHE PCC");
				StartAndEnd(w,"id");
				Start(w,"code","code",labPanel.ServiceId,"codeSystem",strCodeSystemSnomed,"displayName",labPanel.ServiceName);
				Start(w,"originalText");
				w.WriteString(labPanel.ServiceName);
				StartAndEnd(w,"reference","value","Ptr to text  in parent Section");
				End(w,"originalText");
				End(w,"code");
				Start(w,"text");
				w.WriteString(labPanel.ServiceName);
				StartAndEnd(w,"reference","value","Ptr to text  in parent Section");
				End(w,"text");
				StartAndEnd(w,"statusCode","code","completed");
				StartAndEnd(w,"effectiveTime","value",listLabResult[i].DateTimeTest.ToString("yyyyMMddHHmm"));
				End(w,"procedure");
				End(w,"component");
				Start(w,"component");
				Start(w,"observation","classCode","OBS","moodCode","EVN");
				TemplateId(w,"2.16.840.1.113883.3.88.11.83.15.1","HITSP C83");
				TemplateId(w,"2.16.840.1.113883.10.20.1.31","CCD");//result observation template id, according to pages 103-104 of CCD-final.pdf
				TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.4.13","IHE PCC");
				w.WriteComment("Result observation template");
				StartAndEnd(w,"id","root","107c2dc0-67a5-11db-bd13-0800200c9a66");
				StartAndEnd(w,"code","code",listLabResult[i].TestID,"codeSystem",strCodeSystemLoinc,"displayName",listLabResult[i].TestName);
				Start(w,"text");
				StartAndEnd(w,"reference","value","PtrToValueInsectionText");
				End(w,"text");
				StartAndEnd(w,"statusCode","code","completed");
				StartAndEnd(w,"effectiveTime","value",listLabResult[i].DateTimeTest.ToString("yyyyMMddHHmm"));
				Start(w,"value");
				w.WriteAttributeString("xsi","type",null,"PQ");
				Attribs(w,"value","13.2","unit","g/dl");
				End(w,"value");
				StartAndEnd(w,"interpretationCode","code","N","codeSystem","2.16.840.1.113883.5.83");
				End(w,"observation");
				End(w,"component");
				End(w,"organizer");
				End(w,"entry");
			}
			End(w,"section");
			End(w,"component");
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionVitalSigns(XmlWriter w,Patient pat) {
			//TODO:
		}
		
		///<summary>Helper for GenerateCCD(). Builds an Id element an writes it to the given XmlWriter.</summary>
		private static void BuildIdAndWrite(XmlWriter w) {
			string id=MiscUtils.CreateRandomAlphaNumericString(32);
			while(_hashCcdIds.Contains(id)) {
				id=MiscUtils.CreateRandomAlphaNumericString(32);
			}
			_hashCcdIds.Add(id);
			StartAndEnd(w,"id","root",id);
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void TemplateId(XmlWriter writer,string rootNumber) {
			writer.WriteStartElement("templateId");
			writer.WriteAttributeString("root",rootNumber);
			writer.WriteEndElement();
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void TemplateId(XmlWriter writer,string rootNumber,string authorityName) {
			writer.WriteStartElement("templateId");
			writer.WriteAttributeString("root",rootNumber);
			writer.WriteAttributeString("assigningAuthorityName",authorityName);
			writer.WriteEndElement();
		}

		///<summary>Helper for GenerateCCD().  Performs a WriteStartElement, followed by any attributes.  Attributes must be in pairs: name, value.</summary>
		private static void Start(XmlWriter writer,string elementName,params string[] attributes) {
			writer.WriteStartElement(elementName);
			for(int i=0;i<attributes.Length;i+=2) {
				writer.WriteAttributeString(attributes[i],attributes[i+1]);
			}
		}

		///<summary>Helper for GenerateCCD().  Performs a WriteEndElement.</summary>
		private static void End(XmlWriter writer,string elementName) {
			writer.WriteEndElement();
		}

		///<summary>Helper for GenerateCCD().  Performs a WriteStartElement, followed by any attributes, followed by a WriteEndElement.  Attributes must be in pairs: name, value.</summary>
		private static void StartAndEnd(XmlWriter writer,string elementName,params string[] attributes) {
			writer.WriteStartElement(elementName);
			for(int i=0;i<attributes.Length;i+=2) {
				writer.WriteAttributeString(attributes[i],attributes[i+1]);
			}
			writer.WriteEndElement();
		}

		///<summary>Helper for GenerateCCD().  Performs a WriteAttributeString for each attribute.  Attributes must be in pairs: name, value.</summary>
		private static void Attribs(XmlWriter writer,params string[] attributes) {
			for(int i=0;i<attributes.Length;i+=2) {
				writer.WriteAttributeString(attributes[i],attributes[i+1]);
			}
		}

		///<summary>Writes the dateTime in the required date format.  Will not write if year is before 1880.</summary>
		private static void WriteDate(XmlWriter writer,DateTime dateTime) {
			if(dateTime.Year<1880) {
				return;
			}
			writer.WriteString(dateTime.ToString("yyyyMMdd"));
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
