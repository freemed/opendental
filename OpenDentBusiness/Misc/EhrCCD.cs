using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeBase;
using System.Xml;
using System.Xml.XPath;

namespace OpenDentBusiness {
	public class EhrCCD {

		public static string GenerateCCD(Patient pat) {
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
				w.WriteAttributeString("root","2.16.840.1.113883.1.3");
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
				w.WriteAttributeString("codeSystemName","LOINC");
				w.WriteAttributeString("codeSystem","2.16.840.1.113883.6.1");
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
				w.WriteComment(@"
=====================================================================================================
Problems
=====================================================================================================");
				List<Disease> listProblem=Diseases.Refresh(pat.PatNum);
				ICD9 icd9;
				w.WriteStartElement("component");
				w.WriteStartElement("section");
				TemplateId(w,"2.16.840.1.113883.3.88.11.83.103","HITSP/C83");
				TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.3.6","IHE PCC");
				TemplateId(w,"2.16.840.1.113883.10.20.1.11","HL7 CCD");
				w.WriteComment("Problems section template");
				StartAndEnd(w,"code","code","11450-4","codeSystem","2.16.840.1.113883.6.1","codeSystemName","LOINC","displayName","Problem list");
				w.WriteElementString("title","Problems");
				w.WriteStartElement("text");//The following text will be parsed as html with a style sheet to be human readable.
				w.WriteStartElement("table");
				w.WriteAttributeString("width","100%");
				w.WriteAttributeString("border","1");
				w.WriteStartElement("thead");
				w.WriteStartElement("tr");
				w.WriteStartElement("th");
				w.WriteString("ICD-9 Code");
				w.WriteEndElement();
				w.WriteStartElement("th");
				w.WriteString("Patient Problem");
				w.WriteEndElement();
				w.WriteStartElement("th");
				w.WriteString("Date Diagnosed");
				w.WriteEndElement();
				w.WriteStartElement("th");
				w.WriteString("Status");
				w.WriteEndElement();
				w.WriteEndElement();//End tr
				w.WriteEndElement();//End thead
				w.WriteStartElement("tbody");
				for(int i=0;i<listProblem.Count;i++) {
					if(DiseaseDefs.GetItem(listProblem[i].DiseaseDefNum).ICD9Code=="") {
						continue;
					}
					icd9=ICD9s.GetByCode(DiseaseDefs.GetItem(listProblem[i].DiseaseDefNum).ICD9Code);
					w.WriteStartElement("tr");
					w.WriteStartElement("td");
					if(icd9==null) {
						w.WriteString(DiseaseDefs.GetItem(listProblem[i].DiseaseDefNum).ICD9Code);
						w.WriteEndElement();//td
						w.WriteStartElement("td");
						w.WriteString(DiseaseDefs.GetItem(listProblem[i].DiseaseDefNum).DiseaseName);
					}
					else {
						w.WriteString(icd9.ICD9Code);
						w.WriteEndElement();//td
						w.WriteStartElement("td");
						w.WriteString(icd9.Description);
					}
					w.WriteEndElement();
					w.WriteStartElement("td");
					w.WriteString(listProblem[i].DateStart.ToShortDateString());
					w.WriteEndElement();
					w.WriteStartElement("td");
					w.WriteString(listProblem[i].ProbStatus.ToString());
					w.WriteEndElement();
					w.WriteEndElement();//End tr
				}
				w.WriteEndElement();//tbody
				w.WriteEndElement();//table
				w.WriteEndElement();//text
				for(int i=0;i<listProblem.Count;i++) {
					if(DiseaseDefs.GetItem(listProblem[i].DiseaseDefNum).ICD9Code=="") {
						continue;
					}
					icd9=ICD9s.GetByCode(DiseaseDefs.GetItem(listProblem[i].DiseaseDefNum).ICD9Code);
					Start(w,"entry","typeCode","DRIV");
					Start(w,"act","classCode","ACT","moodCode","EVN");
					TemplateId(w,"2.16.840.1.113883.3.88.11.83.7","HITSP C83");
					TemplateId(w,"2.16.840.1.113883.10.20.1.27","CCD");
					TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.4.5.1","IHE PCC");
					TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.4.5.2","IHE PCC");
					w.WriteComment("Problem act template");
					StartAndEnd(w,"id","root","6a2fa88d-4174-4909-aece-db44b60a3abb");
					StartAndEnd(w,"code","nullFlavor","NA");
					StartAndEnd(w,"statusCode","code","completed");
					Start(w,"effectiveTime");
					StartAndEnd(w,"low","value","1950");
					StartAndEnd(w,"high","nullFlavor","UNK");
					End(w,"effectiveTime");
					Start(w,"entryRelationship","typeCode","SUBJ","inversionInd","false");
					Start(w,"observation","classCode","OBS","moodCode","EVN");
					TemplateId(w,"2.16.840.1.113883.10.20.1.28","CCD");
					TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.4.5","IHE PCC");
					w.WriteComment("Problem observation template - NOT episode template");
					StartAndEnd(w,"id","root","d11275e7-67ae-11db-bd13-0800200c9a66");
					Start(w,"code","nullFlavor","UNK");
					if(icd9==null) {
						StartAndEnd(w,"translation","code",DiseaseDefs.GetItem(listProblem[i].DiseaseDefNum).ICD9Code,"codeSystem","2.16.840.1.113883.6.103");
					}
					else {
						StartAndEnd(w,"translation","code",icd9.ICD9Code,"codeSystem","2.16.840.1.113883.6.103");
					}
					End(w,"code");
					Start(w,"text");
					StartAndEnd(w,"reference","value","#PROBSUMMARY_1");
					End(w,"text");
					StartAndEnd(w,"statusCode","code","completed");
					Start(w,"effectiveTime");
					StartAndEnd(w,"low","value","1950");
					End(w,"effectiveTime");
					Start(w,"value");
					w.WriteAttributeString("xsi","type",null,"CD");
					Attribs(w,"nullFlavor","UNK");
					Start(w,"translation");
					w.WriteAttributeString("xsi","type",null,"CD");
					if(icd9==null) {
						Attribs(w,"code",DiseaseDefs.GetItem(listProblem[i].DiseaseDefNum).ICD9Code,"codeSystem","2.16.840.1.113883.6.103");
					}
					else {
						Attribs(w,"code",icd9.ICD9Code,"codeSystem","2.16.840.1.113883.6.103");
					}
					End(w,"translation");
					End(w,"value");
					End(w,"observation");
					End(w,"entryRelationship");
					End(w,"act");
					End(w,"entry");
				}
				w.WriteEndElement();//section
				w.WriteEndElement();//component
				w.WriteComment(@"
=====================================================================================================
Allergies
=====================================================================================================");
				List<Allergy> listAllergy=Allergies.Refresh(pat.PatNum);
				AllergyDef allergyDef;
				Medication med;
				w.WriteStartElement("component");
				w.WriteStartElement("section");
				TemplateId(w,"2.16.840.1.113883.3.88.11.83.102","HITSP/C83");
				TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.3.13","IHE PCC");
				TemplateId(w,"2.16.840.1.113883.10.20.1.2","HL7 CCD");
				w.WriteComment("Allergies/Reactions section template");
				StartAndEnd(w,"code","code","48765-2","codeSystem","2.16.840.1.113883.6.1","codeSystemName","LOINC","displayName","Allergies");
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
				w.WriteString("SNOMED Allergy Type Code");
				w.WriteEndElement();
				w.WriteStartElement("th");
				w.WriteString("Medication/Agent Allergy");
				w.WriteEndElement();//
				w.WriteStartElement("th");
				w.WriteString("Reaction");
				w.WriteEndElement();
				w.WriteStartElement("th");
				w.WriteString("Adverse Event Date");
				w.WriteEndElement();
				w.WriteEndElement();//End tr
				w.WriteEndElement();//End thead
				w.WriteStartElement("tbody");
				for(int i=0;i<listAllergy.Count;i++) {
					allergyDef=AllergyDefs.GetOne(listAllergy[i].AllergyDefNum);
					if(allergyDef.MedicationNum==0) {
						continue;
					}
					med=Medications.GetMedication(allergyDef.MedicationNum);
					w.WriteStartElement("tr");
					w.WriteStartElement("td");
					w.WriteString(AllergyDefs.GetSnomedAllergyDesc(allergyDef.Snomed));
					w.WriteEndElement();
					w.WriteStartElement("td");
					w.WriteString(med.RxCui.ToString()+" - "+med.MedName);
					w.WriteEndElement();
					w.WriteStartElement("td");
					w.WriteString(listAllergy[i].Reaction);
					w.WriteEndElement();
					w.WriteStartElement("td");
					w.WriteString(listAllergy[i].DateAdverseReaction.ToShortDateString());
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
					Start(w,"entry","typeCode","DRIV");
					Start(w,"act","classCode","ACT","moodCode","EVN");
					TemplateId(w,"2.16.840.1.113883.3.88.11.83.6","HITSP C83");
					TemplateId(w,"2.16.840.1.113883.10.20.1.27","CCD");
					TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.4.5.1","IHE PCC");
					TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.4.5.3","IHE PCC");
					StartAndEnd(w,"id","root","36e3e930-7b14-11db-9fe1-0800200c9a66");
					StartAndEnd(w,"code","nullFlavor","NA");
					StartAndEnd(w,"statusCode","code","completed");
					Start(w,"effectiveTime");
					StartAndEnd(w,"low","nullFlavor","UNK");
					StartAndEnd(w,"high","nullFlavor","UNK");
					End(w,"effectiveTime");
					Start(w,"entryRelationship","typeCode","SUBJ","inversionInd","false");
					Start(w,"observation","classCode","OBS","moodCode","EVN");
					TemplateId(w,"2.16.840.1.113883.10.20.1.18","CCD");
					TemplateId(w,"2.16.840.1.113883.10.20.1.28","CCD");
					TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.4.5","IHE PCC");
					TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.4.6","IHE PCC");
					StartAndEnd(w,"id","root","4adc1020-7b14-11db-9fe1-0800200c9a66");
					StartAndEnd(w,"code","code","416098002","codeSystem","2.16.840.1.113883.6.96",
						"displayName","drug allergy","codeSystemName","SNOMED CT");
					Start(w,"text");
					StartAndEnd(w,"reference","value","#ALGSUMMARY_1");
					End(w,"text");
					StartAndEnd(w,"statusCode","code","completed");
					Start(w,"effectiveTime");
					StartAndEnd(w,"low","nullFlavor","UNK");
					End(w,"effectiveTime");
					//Note that IHE/PCC and HITSP/C32 differ in how to represent the drug, substance, or food that one is allergic to.
					//IHE/PCC expects to see that information in <value> and HITSP/C32 expects to see it in <participant>.
					Start(w,"value");
					w.WriteAttributeString("xsi","type",null,"CD");
					Attribs(w,"code",med.RxCui.ToString(),"codeSystem","2.16.840.1.113883.6.88");
					Attribs(w,"displayName",med.MedName,"codeSystemName","RxNorm");
					Start(w,"originalText");
					StartAndEnd(w,"reference","value","#ALGSUB_1");
					End(w,"originalText");
					End(w,"value");
					Start(w,"participant","typeCode","CSM");
					Start(w,"participantRole","classCode","MANU");
					StartAndEnd(w,"addr");
					StartAndEnd(w,"telecom");
					Start(w,"playingEntity","classCode","MMAT");
					Start(w,"code","code","70618","codeSystem","2.16.840.1.113883.6.88");
					Attribs(w,"displayName",med.MedName,"codeSystemName","RxNorm");
					Start(w,"originalText");
					StartAndEnd(w,"reference","value","#ALGSUB_1");
					End(w,"originalText");
					End(w,"code");
					w.WriteElementString("name",med.MedName);
					End(w,"playingEntity");
					End(w,"participantRole");
					End(w,"participant");
					End(w,"observation");
					End(w,"entryRelationship");
					End(w,"act");
					End(w,"entry");
				}
				w.WriteEndElement();//section
				w.WriteEndElement();//component Alerts
				w.WriteComment(@"
=====================================================================================================
Medications
=====================================================================================================");
				List<MedicationPat> listMedPat=MedicationPats.Refresh(pat.PatNum,true);
				w.WriteStartElement("component");
				//TODO: component typeCode and contextConductionInd
				w.WriteStartElement("section");
				TemplateId(w,"2.16.840.1.113883.3.88.11.83.112","HITSP/C83");
				TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.3.19","IHE PCC");
				TemplateId(w,"2.16.840.1.113883.10.20.1.8","HL7 CCD");
				w.WriteComment("Medications section template");
				StartAndEnd(w,"code","code","10160-0","codeSystem","2.16.840.1.113883.6.1","codeSystemName","LOINC",
					"displayName","History of medication use");
				w.WriteElementString("title","Medications");
				w.WriteStartElement("text");//The following text will be parsed as html with a style sheet to be human readable.
				w.WriteStartElement("table");
				w.WriteAttributeString("width","100%");
				w.WriteAttributeString("border","1");
				w.WriteStartElement("thead");
				w.WriteStartElement("tr");
				w.WriteStartElement("th");
				w.WriteString("RxNorm Code");
				w.WriteEndElement();
				w.WriteStartElement("th");
				w.WriteString("Product");
				w.WriteEndElement();
				w.WriteStartElement("th");
				w.WriteString("Generic Name");
				w.WriteEndElement();
				w.WriteStartElement("th");
				w.WriteString("Brand Name");
				w.WriteEndElement();
				w.WriteStartElement("th");
				w.WriteString("Instructions");
				w.WriteEndElement();
				w.WriteStartElement("th");
				w.WriteString("Date Started");
				w.WriteEndElement();
				w.WriteStartElement("th");
				w.WriteString("Status");
				w.WriteEndElement();
				w.WriteEndElement();//End tr
				w.WriteEndElement();//End thead
				w.WriteStartElement("tbody");
				for(int i=0;i<listMedPat.Count;i++) {
					string strMedName="";
					long rxCui=0;//Not our usual pattern name, because we should have used a string instead of a long for the database type.
					string strMedNameGeneric="";
					if(listMedPat[i].MedicationNum==0) {
						strMedName=listMedPat[i].MedDescript;
						rxCui=listMedPat[i].RxCui;
					}
					else {
						med=Medications.GetMedication(listMedPat[i].MedicationNum);
						strMedName=med.MedName;
						rxCui=med.RxCui;
						strMedNameGeneric=Medications.GetGenericName(med.GenericNum);
					}
					w.WriteStartElement("tr");
					w.WriteStartElement("td");
					w.WriteString(rxCui.ToString());//RxNorm Code
					w.WriteEndElement();
					w.WriteStartElement("td");
					w.WriteString("Medication");//Product
					w.WriteEndElement();
					w.WriteStartElement("td");
					w.WriteString(strMedNameGeneric);//Generic Name
					w.WriteEndElement();
					w.WriteStartElement("td");
					w.WriteString(strMedName);//Brand Name
					w.WriteEndElement();
					w.WriteStartElement("td");
					w.WriteString(listMedPat[i].PatNote);//Instruction
					w.WriteEndElement();
					w.WriteStartElement("td");
					w.WriteString(listMedPat[i].DateStart.ToShortDateString());//Date Started
					w.WriteEndElement();
					w.WriteStartElement("td");
					w.WriteString(MedicationPats.IsMedActive(listMedPat[i])?"Active":"Inactive");//Status
					w.WriteEndElement();
					w.WriteEndElement();//End tr
				}
				w.WriteEndElement();//End tbody
				w.WriteEndElement();//End table
				w.WriteEndElement();//End text
				for(int i=0;i<listMedPat.Count;i++) {
					long strMedRxCui=listMedPat[i].RxCui;
					string strMedName=listMedPat[i].MedDescript;
					if(listMedPat[i].MedicationNum!=0) {
						med=Medications.GetMedication(listMedPat[i].MedicationNum);
						strMedRxCui=med.RxCui;
						strMedName=med.MedName;
					}
					Start(w,"entry","typeCode","DRIV");
					Start(w,"substanceAdministration","classCode","SBADM","moodCode","EVN");
					TemplateId(w,"2.16.840.1.113883.3.88.11.83.8","HITSP C83");
					TemplateId(w,"2.16.840.1.113883.10.20.1.24","CCD");
					TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.4.7","IHE PCC");
					TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.4.7.1","IHE PCC");
					w.WriteComment("Medication activity template");
					StartAndEnd(w,"id","root","cdbd33f0-6cde-11db-9fe1-0800200c9a66");
					Start(w,"text");
					StartAndEnd(w,"reference","value","#SIGTEXT_1");
					End(w,"text");
					StartAndEnd(w,"statusCode","code","completed");
					Start(w,"effectiveTime");
					w.WriteAttributeString("xsi","type",null,"IVL_TS");
					StartAndEnd(w,"low","value","200507");
					StartAndEnd(w,"high","nullFlavor","UNK");
					End(w,"effectiveTime");
					Start(w,"effectiveTime");
					w.WriteAttributeString("xsi","type",null,"PIVL_TS");
					StartAndEnd(w,"period","value","6","unit","h");
					End(w,"effectiveTime");
					Start(w,"consumable");
					Start(w,"manufacturedProduct");
					TemplateId(w,"2.16.840.1.113883.3.88.11.83.8.2","HITSP C83");
					TemplateId(w,"2.16.840.1.113883.10.20.1.53","CCD");
					TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.4.7.2","IHE PCC");
					w.WriteComment("Product template");
					Start(w,"manufacturedMaterial");
					Start(w,"code","code",strMedRxCui.ToString(),"codeSystem","2.16.840.1.113883.6.88",
						"displayName",strMedName,"codeSystemName","RxNorm");
					Start(w,"originalText");
					w.WriteString(strMedName);
					StartAndEnd(w,"reference");
					End(w,"originalText");
					End(w,"code");
					End(w,"manufacturedMaterial");
					End(w,"manufacturedProduct");
					End(w,"consumable");
					End(w,"substanceAdministration");
					End(w,"entry");
				}
				w.WriteEndElement();//section
				w.WriteEndElement();//component: Medication
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
				StartAndEnd(w,"code","code","30954-2","codeSystem","2.16.840.1.113883.6.1","codeSystemName","LOINC",
					"displayName","Results");
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
					StartAndEnd(w,"code","code",labPanel.ServiceId,
						"codeSystem","2.16.840.1.113883.6.96","displayName",labPanel.ServiceName);
					StartAndEnd(w,"statusCode","code","completed");
					StartAndEnd(w,"effectiveTime","value",listLabResult[i].DateTimeTest.ToString("yyyyMMddHHmm"));
					Start(w,"component");
					Start(w,"procedure","classCode","PROC","moodCode","EVN");
					TemplateId(w,"2.16.840.1.113883.3.88.11.83.17","HITSP C83");
					TemplateId(w,"2.16.840.1.113883.10.20.1.29","CCD");
					TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.4.19","IHE PCC");
					StartAndEnd(w,"id");
					Start(w,"code","code",labPanel.ServiceId,
						"codeSystem","2.16.840.1.113883.6.96","displayName",labPanel.ServiceName);
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
					TemplateId(w,"2.16.840.1.113883.10.20.1.31","CCD");
					TemplateId(w,"1.3.6.1.4.1.19376.1.5.3.1.4.13","IHE PCC");
					w.WriteComment("Result observation template");
					StartAndEnd(w,"id","root","107c2dc0-67a5-11db-bd13-0800200c9a66");
					StartAndEnd(w,"code","code",listLabResult[i].TestID,
						"codeSystem","2.16.840.1.113883.6.1","displayName",listLabResult[i].TestName);
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
				End(w,"structuredBody");
				End(w,"component");
				End(w,"ClinicalDocument");
			}
			return strBuilder.ToString();
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

		public static bool IsCCD(string strXml) {
			XmlDocument doc=new XmlDocument();
			try {
				doc.LoadXml(strXml);
			}
			catch {
				return false;
			}
			if(doc.DocumentElement.Name.ToLower()=="clinicaldocument") {//CCD and CCDA. TODO: Use a different style sheet for CCDA?
				return true;
			}
			else if(doc.DocumentElement.Name.ToLower()=="continuityofcarerecord" || doc.DocumentElement.Name.ToLower()=="ccr:continuityofcarerecord") {//CCR
				return true;
			}
			return false;
		}

		///<summary>Returns the PatNum for the unique patient who matches the patient name and birthdate within the CCD message strXmlCCD.
		///Returns 0 if there are no patient matches.  Returns the first match if there are multiple matches (unlikely).</summary>
		public static long GetCCDpat(string strXmlCCD) {
			XmlDocument doc=new XmlDocument();
			try {
				doc.LoadXml(strXmlCCD);
			}
			catch {
				throw new XmlException("Invalid XML");
			}
			XmlNodeList xmlNodeList=doc.GetElementsByTagName("patient");//According to the CCD documentation, there should be one patient node, or no patient node.
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
							string strGivenName=xmlNodeName.ChildNodes[j].Value;
							if(fName=="") {//The first and middle names are both referred to as "given" name, so we assume the first given name is the patient's first name.
								fName=strGivenName;
							}
						}
						else if(xmlNodeName.ChildNodes[j].Name.Trim().ToLower()=="family") {
							lName=xmlNodeName.ChildNodes[j].Value;
						}
					}
				}
				else if(xmlNodePat.ChildNodes[i].Name.Trim().ToLower()=="birthtime") {
					int birthYear=int.Parse(xmlNodePat.ChildNodes[i].Value.Substring(0,4));
					int birthMonth=int.Parse(xmlNodePat.ChildNodes[i].Value.Substring(4,2));
					int birthDay=int.Parse(xmlNodePat.ChildNodes[i].Value.Substring(6,2));
					birthDate=new DateTime(birthYear,birthMonth,birthDay);
				}
			}
			if(lName=="" || fName=="" || birthDate.Year<1880) {
				return 0;
			}
			return Patients.GetPatNumByNameAndBirthday(lName,fName,birthDate);
		}

	}
}
