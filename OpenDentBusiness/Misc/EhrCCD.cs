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
		///<summary>2.16.840.1.113883.12.292</summary>
		private const string strCodeSystemCvx="2.16.840.1.113883.12.292";
		///<summary>CVX</summary>
		private const string strCodeSystemNameCvx="CVX";
		///<summary>Set each time GenerateCCD() is called. Used by helper functions to avoid sending the patient as a parameter to each helper function.</summary>
		private static Patient _patOutCcd=null;
		///<summary>Instantiated each time GenerateCCD() is called. Used by helper functions to avoid sending the writer as a parameter to each helper function.</summary>
		private static XmlWriter _w=null;
		///<summary>Instantiated each time GenerateCCD() is called. Used to generate unique "id" element "root" attribute identifiers. The Ids in this list are random alpha-numeric and 32 characters in length.</summary>
		private static HashSet<string> _hashCcdIds;
		///<summary>Instantiated each time GenerateCCD() is called. Used to generate unique "id" element "root" attribute identifiers. The Ids in this list are random GUIDs which are 36 characters in length.</summary>
		private static HashSet<string> _hashCcdGuids;

		public static string GenerateCCD(Patient pat) {
			_patOutCcd=pat;
			_hashCcdIds=new HashSet<string>();//The IDs only need to be unique within each CCD document.
			_hashCcdGuids=new HashSet<string>();//The UUIDs only need to be unique within each CCD document.
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
				_w.WriteAttributeString("xmlns","xsi",null,"http://www.w3.org/2001/XMLSchema-instance");
				_w.WriteAttributeString("xsi","noNamespaceSchemaLocation",null,"Registry_Payment.xsd");
				_w.WriteAttributeString("xsi","schemaLocation",null,"urn:hl7-org:v3 http://xreg2.nist.gov:8080/hitspValidation/schema/cdar2c32/infrastructure/cda/C32_CDA.xsd");
				StartAndEnd("realmCode","code","US");
				StartAndEnd("typeId","root","2.16.840.1.113883.1.3","extension","POCD_HD000040");//template id to assert use of the CCD standard
				_w.WriteComment("US General Header Template");
				TemplateId("2.16.840.1.113883.10.20.22.1.1");
				_w.WriteComment("Conforms to CCD requirements");
				TemplateId("2.16.840.1.113883.10.20.22.1.2");
				Guid();
				StartAndEnd("code","code","34133-9","codeSystemName",strCodeSystemNameLoinc,"codeSystem",strCodeSystemLoinc,"displayName","Summarization of Episode Note");
				_w.WriteElementString("title","Continuity of Care Document");
				TimeElement("effectiveTime",DateTime.Now);
				StartAndEnd("confidentialityCode","code","N","codeSystem","2.16.840.1.113883.5.25");//Fixed value.  Confidentiality Code System.  Codes: N=(Normal), R=(Restricted),V=(Very Restricted)
				StartAndEnd("languageCode","code","en-US");
				Start("recordTarget");
				Start("patientRole");
				StartAndEnd("id","extension",pat.PatNum.ToString(),"root","2.16.840.1.113883.4.6");//TODO: We might need to assign a global GUID for each office so that the patient can be uniquely identified anywhere in the world.
				if(pat.SSN.Length==9) {
					StartAndEnd("id","extension",pat.SSN,"root","2.16.840.1.113883.4.1");//TODO: We might need to assign a global GUID for each office so that the patient can be uniquely identified anywhere in the world.
				}
				AddressUnitedStates(pat.Address,pat.Address2,pat.City,pat.State);
				StartAndEnd("telecom","use","HP","value","tel:"+pat.HmPhone);
				Start("patient");
				Start("name","use","L");
				_w.WriteElementString("given",pat.FName);
				if(pat.MiddleI!="") {
					_w.WriteElementString("given",pat.MiddleI);
				}
				_w.WriteElementString("family",pat.LName);
				if(pat.Title!="") {
					Start("suffix","qualifier","TITLE");
					_w.WriteString(pat.Title);
					End("suffix");
				}
				End("name");
				string strGender="M";
				if(pat.Gender==PatientGender.Female) {
					strGender="F";
				}
				StartAndEnd("administrativeGenderCode","code",strGender,"codeSystem","2.16.840.1.113883.5.1");
				DateElement("birthTime",pat.Birthdate);
				if(pat.Position==PatientPosition.Divorced) {
					StartAndEnd("maritalStatusCode","code","D","displayName","Divorced","codeSystem","2.16.840.1.113883.5.2","codeSystemName","MaritalStatusCode");
				}
				else if(pat.Position==PatientPosition.Married) {
					StartAndEnd("maritalStatusCode","code","M","displayName","Married","codeSystem","2.16.840.1.113883.5.2","codeSystemName","MaritalStatusCode");
				}
				else if(pat.Position==PatientPosition.Widowed) {
					StartAndEnd("maritalStatusCode","code","W","displayName","Widowed","codeSystem","2.16.840.1.113883.5.2","codeSystemName","MaritalStatusCode");
				}
				else {//Single and child
					StartAndEnd("maritalStatusCode","code","S","displayName","Never Married","codeSystem","2.16.840.1.113883.5.2","codeSystemName","MaritalStatusCode");
				}
				bool isRaceFound=true;
				PatRace patRace=PatRace.DeclinedToSpecify;
				bool isHispanicOrLatino=false;
				List <PatientRace> listPatientRaces=PatientRaces.GetForPatient(pat.PatNum);
				for(int i=0;i<listPatientRaces.Count;i++) {
					if(listPatientRaces[i].Race==PatRace.Hispanic) {
						isHispanicOrLatino=true;
					}
					else if(listPatientRaces[i].Race==PatRace.NotHispanic) {
						//Nothing to do. Flag is set to false by default.
					}
					else if(listPatientRaces[i].Race==PatRace.DeclinedToSpecify) {
						isRaceFound=false;
					}
					else if(patRace==PatRace.DeclinedToSpecify) {//Only once race can be specified in the CCD document.
						patRace=listPatientRaces[i].Race;
					}
				}
				if(isRaceFound) {//The patient did not decline to specify.
					if(patRace==PatRace.AfricanAmerican) {
						StartAndEnd("raceCode","code","2054-5","displayName","Black or African American","codeSystem","2.16.840.1.113883.6.238","codeSystemName","Race &amp; Ethnicity - CDC");
					}
					else if(patRace==PatRace.AmericanIndian) {
						StartAndEnd("raceCode","code","1002-5","displayName","American Indian or Alaska Native","codeSystem","2.16.840.1.113883.6.238","codeSystemName","Race &amp; Ethnicity - CDC");
					}
					else if(patRace==PatRace.Asian) {
						StartAndEnd("raceCode","code","2028-9","displayName","Asian","codeSystem","2.16.840.1.113883.6.238","codeSystemName","Race &amp; Ethnicity - CDC");
					}
					else if(patRace==PatRace.HawaiiOrPacIsland) {
						StartAndEnd("raceCode","code","2076-8","displayName","Native Hawaiian or Other Pacific Islander","codeSystem","2.16.840.1.113883.6.238","codeSystemName","Race &amp; Ethnicity - CDC");
					}
					else if(patRace==PatRace.White) {
						StartAndEnd("raceCode","code","2106-3","displayName","White","codeSystem","2.16.840.1.113883.6.238","codeSystemName","Race &amp; Ethnicity - CDC");
					}
					else {//Aboriginal, Other, Multiracial
						StartAndEnd("raceCode","code","2131-1","displayName","Other Race","codeSystem","2.16.840.1.113883.6.238","codeSystemName","Race &amp; Ethnicity - CDC");
					}
				}
				if(isHispanicOrLatino) {
					StartAndEnd("ethnicGroupCode","code","2135-2","displayName","Hispanic or Latino","codeSystem","2.16.840.1.113883.6.238","codeSystemName","Race &amp; Ethnicity - CDC");
				}
				else {//Not hispanic
					StartAndEnd("ethnicGroupCode","code","2186-5","displayName","Not Hispanic or Latino","codeSystem","2.16.840.1.113883.6.238","codeSystemName","Race &amp; Ethnicity - CDC");
				}
				End("patient");
				End("patientRole");
				End("recordTarget");
				//author--------------------------------------------------------------------------------------------------
				//"Represents the creator of the clinical document." Section 2.1.2, page 65
				Provider provAuthor=Providers.GetProv(PrefC.GetLong(PrefName.PracticeDefaultProv));
				Start("author");
				TimeElement("time",DateTime.Now);
				Start("assignedAuthor");
				StartAndEnd("id","extension",provAuthor.NationalProvID,"root","2.16.840.1.113883.4.6");//TODO: We might need to assign a global GUID for each office so that the provider can be uniquely identified anywhere in the world.
				//TODO: SHOULD contain a code representing provider specialty. Example: <code code="200000000X" codeSystem="2.16.840.1.113883.6.101" displayName="Allopathic &amp; Osteopathic Physicians"/>
				AddressUnitedStates(PrefC.GetString(PrefName.PracticeAddress),PrefC.GetString(PrefName.PracticeAddress2),PrefC.GetString(PrefName.PracticeCity),PrefC.GetString(PrefName.PracticeST));
				string strPracticePhone=PrefC.GetString(PrefName.PracticePhone);
				strPracticePhone=strPracticePhone.Substring(0,3)+"-"+strPracticePhone.Substring(3,3)+"-"+strPracticePhone.Substring(6);
				StartAndEnd("telecom","use","WP","value","tel:"+strPracticePhone);
				Start("assignedPerson");
				Start("name");
				_w.WriteElementString("given",provAuthor.FName);
				_w.WriteElementString("family",provAuthor.LName);
				End("name");
				End("assignedPerson");
				End("assignedAuthor");
				End("author");
				//custodian------------------------------------------------------------------------------------------------
				//"Represents the organization in charge of maintaining the document." Section 2.1.5, page 72
				Provider provCustodian=Providers.GetProv(PrefC.GetLong(PrefName.PracticeDefaultProv));
				Start("custodian");
				Start("assignedCustodian");
				Start("representedCustodianOrganization");
				StartAndEnd("id","extension",provAuthor.NationalProvID,"root","2.16.840.1.113883.4.6");//TODO: We might need to assign a global GUID for each office so that the provider can be uniquely identified anywhere in the world.
				_w.WriteElementString("name",PrefC.GetString(PrefName.PracticeTitle));
				StartAndEnd("telecom","use","WP","value","tel:"+strPracticePhone);
				AddressUnitedStates(PrefC.GetString(PrefName.PracticeAddress),PrefC.GetString(PrefName.PracticeAddress2),PrefC.GetString(PrefName.PracticeCity),PrefC.GetString(PrefName.PracticeST));
				End("representedCustodianOrganization");
				End("assignedCustodian");
				End("custodian");
				_w.WriteComment(@"
=====================================================================================================
Body
=====================================================================================================");
				Start("component");
				Start("structuredBody");
				GenerateCcdSectionAllergies();
				GenerateCcdSectionEncounters();
				GenerateCcdSectionFunctionalStatus();
				GenerateCcdSectionImmunizations();
				GenerateCcdSectionMedications();
				GenerateCcdSectionPlanOfCare();
				GenerateCcdSectionProblems();
				GenerateCcdSectionProcedures();
				GenerateCcdSectionReasonForReferral();
				GenerateCcdSectionResults();//Lab Results
				GenerateCcdSectionSocialHistory();
				GenerateCcdSectionVitalSigns();
				End("structuredBody");
				End("component");
				End("ClinicalDocument");
			}
			SecurityLogs.MakeLogEntry(Permissions.Copy,pat.PatNum,"CCD generated");			//Create audit log entry.
			return strBuilder.ToString();
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionAllergies() {
			_w.WriteComment(@"
=====================================================================================================
Allergies
=====================================================================================================");
			AllergyDef allergyDef;
			List<Allergy> listAllergiesAll=Allergies.Refresh(_patOutCcd.PatNum);
			List<Allergy> listAllergiesFiltered=new List<Allergy>();
			for(int i=0;i<listAllergiesAll.Count;i++) {
				allergyDef=AllergyDefs.GetOne(listAllergiesAll[i].AllergyDefNum);
				bool isMedAllergy=false;
				if(allergyDef.MedicationNum!=0) {
					Medication med=Medications.GetMedication(allergyDef.MedicationNum);
					if(med.RxCui!=0) {
						isMedAllergy=true;
					}
				}
				bool isSnomedAllergy=false;
				if(allergyDef.SnomedAllergyTo!="") {
					isSnomedAllergy=true;
				}
				if(!isMedAllergy && !isSnomedAllergy) {
					continue;
				}
				listAllergiesFiltered.Add(listAllergiesAll[i]);
			}
			Start("component");
			Start("section");
			if(listAllergiesFiltered.Count==0) {
				TemplateId("2.16.840.1.113883.10.20.22.2.6");//page 230 Allergy template with optional entries.
			}
			else {
				TemplateId("2.16.840.1.113883.10.20.22.2.6.1");//page 230 Allergy template with required entries.
			}
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
			for(int i=0;i<listAllergiesFiltered.Count;i++) {
				Allergy allergy=listAllergiesFiltered[i];
				allergyDef=AllergyDefs.GetOne(allergy.AllergyDefNum);
				Start("tr");
				if(allergyDef.SnomedAllergyTo!="") {//Is Snomed allergy.
					Snomed snomedAllergyTo=Snomeds.GetByCode(allergyDef.SnomedAllergyTo);
					_w.WriteElementString("td",snomedAllergyTo.SnomedCode+" - "+snomedAllergyTo.Description);
				}
				else {//Medication allergy
					Medication med=Medications.GetMedication(allergyDef.MedicationNum);
					_w.WriteElementString("td",med.RxCui.ToString()+" - "+med.MedName);
				}
				_w.WriteElementString("td",allergy.Reaction);
				_w.WriteElementString("td",AllergyDefs.GetSnomedAllergyDesc(allergyDef.SnomedType));
				_w.WriteElementString("td",allergy.StatusIsActive?"Active":"Inactive");
				End("tr");
			}
			End("tbody");
			End("table");
			End("text");
			for(int i=0;i<listAllergiesFiltered.Count;i++) {
				Allergy allergy=listAllergiesFiltered[i];
				allergyDef=AllergyDefs.GetOne(allergy.AllergyDefNum);
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
				Start("entry","typeCode","DRIV");
				Start("act","classCode","ACT","moodCode","EVN");
				TemplateId("2.16.840.1.113883.10.20.22.4.30");
				Guid();
				StartAndEnd("code","code","48765-2","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Allergies and adverse reactions");
				//statusCode values allowed: active, suspended, aborted, completed.
				if(allergy.StatusIsActive) {
					StartAndEnd("statusCode","code","active");
				}
				else {
					StartAndEnd("statusCode","code","completed");
				}
				Start("effectiveTime");
				if(allergy.StatusIsActive) {
					StartAndEnd("low","value",allergy.DateTStamp.ToString("yyyymmdd"));
					StartAndEnd("high","nullFlavor","UNK");
				}
				else {
					StartAndEnd("low","nullFlavor","UNK");
					StartAndEnd("high","value",allergy.DateTStamp.ToString("yyyymmdd"));
				}
				End("effectiveTime");
				Start("entryRelationship","typeCode","SUBJ");
				Start("observation","classCode","OBS","moodCode","EVN");
				_w.WriteComment("Allergy Observation template");
				TemplateId("2.16.840.1.113883.10.20.22.4.7");
				Guid();
				StartAndEnd("code","code","ASSERTION","codeSystem","2.16.840.1.113883.5.4");//Fixed Value
				StartAndEnd("statusCode","code","completed");//fixed value (required)
				Start("effectiveTime");
				StartAndEnd("low","nullFlavor","UNK");
				StartAndEnd("high","value",allergy.DateTStamp.ToString("yyyymmdd"));
				End("effectiveTime");
				Start("value");
				_w.WriteAttributeString("xsi","type",null,"CD");
				Attribs("code",allergyType,"displayName",allergyTypeName,"codeSystem",strCodeSystemSnomed,"codeSystemName",strCodeSystemNameSnomed);
				End("value");
				Start("participant","typeCode","CSM");
				Start("participantRole","classCode","MANU");
				Start("playingEntity","classCode","MMAT");
				//pg. 331 item 9. The code must be a "2.16.840.1.113883.3.88.12.80.16 Medication Brand", or "2.16.840.1.113883.3.88.12.80.18 Medication Drug", or "2.16.840.1.113883.3.88.12.80.20 Ingredient Name"
				if(allergyDef.SnomedAllergyTo!="") {//Is Snomed allergy.
					Snomed snomedAllergyTo=Snomeds.GetByCode(allergyDef.SnomedAllergyTo);
					StartAndEnd("code","code",snomedAllergyTo.SnomedCode,"displayName",snomedAllergyTo.Description,"codeSystem",strCodeSystemSnomed,"codeSystemName",strCodeSystemNameSnomed);
				}
				else {//Medication allergy
					Medication med=Medications.GetMedication(allergyDef.MedicationNum);
					StartAndEnd("code","code",med.RxCui.ToString(),"displayName",med.MedName,"codeSystem",strCodeSystemRxNorm,"codeSystemName",strCodeSystemNameRxNorm);
				}
				End("playingEntity");
				End("participantRole");
				End("participant");
				Start("entryRelationship","typeCode","SUBJ","inversionInd","true");
				Start("observation","classCode","OBS","moodCode","EVN");
				_w.WriteComment("Allergy Status Observation template");
				TemplateId("2.16.840.1.113883.10.20.22.4.28");
				StartAndEnd("code","code","33999-4","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Status");
				StartAndEnd("statusCode","code","completed");//fixed value (required)
				string status=allergy.StatusIsActive?"Active":"Inactive";
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
				if(allergy.SnomedReaction!="") {//Blank codes are invalid
					Start("entryRelationship","typeCode","SUBJ");
					Start("observation","classCode","OBS","moodCode","EVN");
					_w.WriteComment("Reaction Observation template");
					TemplateId("2.16.840.1.113883.10.20.22.4.9");
					Id();
					StartAndEnd("code","nullFlavor","NA");//Unknown why this is null, but can't find a good example of this
					StartAndEnd("statusCode","code","completed");//fixed value (required)
					Start("effectiveTime");
					if(allergy.StatusIsActive) {
						StartAndEnd("low","value",allergy.DateTStamp.ToString("yyyymmdd"));
					}
					else {
						StartAndEnd("low","nullFlavor","UNK");
					}
					End("effectiveTime");
					Start("value");
					_w.WriteAttributeString("xsi","type",null,"CD");
					Attribs("code",allergy.SnomedReaction,"codeSystem",strCodeSystemSnomed,"displayName",allergy.Reaction);
					End("value");
					End("observation");
					End("entryRelationship");
				}//end snomed reaction
				End("observation");
				End("entryRelationship");
				End("act");
				End("entry");
			}
			End("section");
			End("component");
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionEncounters() {
			//TODO: Allen
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionFunctionalStatus() {
			//TODO: Allen
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionImmunizations() {
			_w.WriteComment(@"
=====================================================================================================
Immunizations
=====================================================================================================");
			List<VaccinePat> listVaccinePatsAll=VaccinePats.Refresh(_patOutCcd.PatNum);
			List<VaccinePat> listVaccinePatsFiltered=new List<VaccinePat>();
			for(int i=0;i<listVaccinePatsAll.Count;i++) {
				VaccineDef vaccineDef=VaccineDefs.GetOne(listVaccinePatsAll[i].VaccineDefNum);
				if(!Cvxs.CodeExists(vaccineDef.CVXCode)) {
					continue;//This code is required, so we must skip any vaccines missing a CVX.
				}
				listVaccinePatsFiltered.Add(listVaccinePatsAll[i]);
			}
			Start("component");
			Start("section");
			if(listVaccinePatsFiltered.Count==0) {
				TemplateId("2.16.840.1.113883.10.20.22.2.2");//Immunizations section with coded entries optional.
			}
			else {
				TemplateId("2.16.840.1.113883.10.20.22.2.2.1");//Immunizations section with coded entries required.
			}
			_w.WriteComment("Immunizations section template");
			StartAndEnd("code","code","11369-6","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","History of immunizations");
			_w.WriteElementString("title","Immunizations");
			Start("text");//The following text will be parsed as html with a style sheet to be human readable.
			Start("table","width","100%","border","1");
			Start("thead");
			Start("tr");
			_w.WriteElementString("th","Vaccine");
			_w.WriteElementString("th","Date");
			_w.WriteElementString("th","Status");
			End("tr");
			End("thead");
			Start("tbody");
			for(int i=0;i<listVaccinePatsFiltered.Count;i++) {
				VaccineDef vaccineDef=VaccineDefs.GetOne(listVaccinePatsFiltered[i].VaccineDefNum);
				Start("tr");
				_w.WriteElementString("td",vaccineDef.VaccineName);
				DateText("td",listVaccinePatsFiltered[i].DateTimeStart);
				_w.WriteElementString("td","Completed");
				End("tr");
			}
			End("tbody");
			End("table");
			End("text");
			for(int i=0;i<listVaccinePatsFiltered.Count;i++) {
				VaccineDef vaccineDef=VaccineDefs.GetOne(listVaccinePatsFiltered[i].VaccineDefNum);
				Start("entry","typeCode","DRIV");
				Start("substanceAdministration","classCode","SBADM","moodCode","EVN","negationInd",listVaccinePatsFiltered[i].NotGiven?"true":"false");
				TemplateId("2.16.840.1.113883.10.20.22.4.52");
				_w.WriteComment("Immunization Activity Template");
				Guid();
				StartAndEnd("statusCode","code","completed");
				Start("effectiveTime");
				_w.WriteAttributeString("xsi","type",null,"IVL_TS");
				Attribs("value",listVaccinePatsFiltered[i].DateTimeStart.ToString("yyyyMMdd"));
				End("effectiveTime");
				Start("consumable");
				Start("manufacturedProduct","classCode","MANU");
				TemplateId("2.16.840.1.113883.10.20.22.4.54");
				_w.WriteComment("Immunization Medication Information");
				Start("manufacturedMaterial");
				Cvx cvx=Cvxs.GetOneFromDb(vaccineDef.CVXCode);
				StartAndEnd("code","code",cvx.CvxCode,"codeSystem",strCodeSystemCvx,"displayName",cvx.Description,"codeSystemName",strCodeSystemNameCvx);
				End("manufacturedMaterial");
				End("manufacturedProduct");
				End("consumable");
				//Possibly add an Instructions Template
				End("substanceAdministration");
				End("entry");
			}
			End("section");
			End("component");
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionMedications() {
			_w.WriteComment(@"
=====================================================================================================
Medications
=====================================================================================================");
			List<MedicationPat> listMedPatsAll=MedicationPats.Refresh(_patOutCcd.PatNum,true);
			List<MedicationPat> listMedPatsFiltered=new List<MedicationPat>();
			for(int i=0;i<listMedPatsAll.Count;i++) {
				if(listMedPatsAll[i].RxCui==0) {
					continue;
				}
				if(listMedPatsAll[i].DateStart.Year<1880 && listMedPatsAll[i].DateStop.Year<1880) {
					continue;//We cannot export these, because the format requires at least one of these dates.
				}
				listMedPatsFiltered.Add(listMedPatsAll[i]);
			}
			Start("component");
			Start("section");
			if(listMedPatsFiltered.Count==0) {
				TemplateId("2.16.840.1.113883.10.20.22.2.1");//Medication section with coded entries optional.
			}
			else {
				TemplateId("2.16.840.1.113883.10.20.22.2.1.1");//Medication section with coded entries required.
			}
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
			for(int i=0;i<listMedPatsFiltered.Count;i++) {
				string strMedName=listMedPatsFiltered[i].MedDescript;
				if(listMedPatsFiltered[i].MedicationNum!=0) {
					strMedName=Medications.GetNameOnly(listMedPatsFiltered[i].MedicationNum);
				}
				Start("tr");
				_w.WriteElementString("td",strMedName);//Medication
				_w.WriteElementString("td",listMedPatsFiltered[i].PatNote);//Directions
				DateText("td",listMedPatsFiltered[i].DateStart);//Start Date
				DateText("td",listMedPatsFiltered[i].DateStop);//End Date
				_w.WriteElementString("td",MedicationPats.IsMedActive(listMedPatsFiltered[i])?"Active":"Inactive");//Status
				_w.WriteElementString("td","");//Indications (The conditions which make the medication necessary). We do not record this information anywhere.
				_w.WriteElementString("td","");//Fill Instructions (Generic substitution allowed or not). We do not record this information anywhere.
				End("tr");
			}
			End("tbody");
			End("table");
			End("text");
			for(int i=0;i<listMedPatsFiltered.Count;i++) {
				string strMedName=listMedPatsFiltered[i].MedDescript;//This might be blank, for example not from NewCrop.  
				if(listMedPatsFiltered[i].MedicationNum!=0) {//If NewCrop, this will be 0.  Also might be zero in the future when we start allowing freeform medications.
					strMedName=Medications.GetNameOnly(listMedPatsFiltered[i].MedicationNum);					
				}
				Start("entry","typeCode","DRIV");
				Start("substanceAdministration","classCode","SBADM","moodCode","EVN");
				TemplateId("2.16.840.1.113883.10.20.22.4.16");
				_w.WriteComment("Medication activity template");
				Guid();
				string strStatus="completed";
				if(MedicationPats.IsMedActive(listMedPatsFiltered[i])) {
					strStatus="active";
				}
				StartAndEnd("statusCode","code",strStatus);
				_w.WriteElementString("text",listMedPatsFiltered[i].PatNote);
				Start("effectiveTime");
				_w.WriteAttributeString("xsi","type",null,"IVL_TS");
				DateElement("low",listMedPatsFiltered[i].DateStart);//Only one of these dates can be null, because of our filter above.
				DateElement("high",listMedPatsFiltered[i].DateStop);//Only one of these dates can be null, because of our filter above.
				End("effectiveTime");		
				Start("consumable");
				Start("manufacturedProduct","classCode","MANU");
				TemplateId("2.16.840.1.113883.10.20.22.4.23");
				Guid();
				Start("manufacturedMaterial");
				Start("code","code",listMedPatsFiltered[i].RxCui.ToString(),"codeSystem",strCodeSystemRxNorm,"displayName",strMedName,"codeSystemName",strCodeSystemNameRxNorm);
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
		private static void GenerateCcdSectionPlanOfCare() {
			_w.WriteComment(@"
=====================================================================================================
Care Plan
=====================================================================================================");
			List<EhrCarePlan> listEhrCarePlansAll=EhrCarePlans.Refresh(_patOutCcd.PatNum);
			List<EhrCarePlan> listEhrCarePlansFiltered=new List<EhrCarePlan>();
			for(int i=0;i<listEhrCarePlansAll.Count;i++) {
				//No filters yet. This loop is here to match our pattern. If we need to add filters later, the change will be safer and more obvious.
				listEhrCarePlansFiltered.Add(listEhrCarePlansAll[i]);
			}
			Start("component");
			Start("section");
			TemplateId("2.16.840.1.113883.10.20.22.2.10");//Only one template id allowed (unlike other sections).
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
			for(int i=0;i<listEhrCarePlansFiltered.Count;i++) {
				Start("tr");
				_w.WriteElementString("td",listEhrCarePlansFiltered[i].Instructions);//Planned Activity
				DateText("td",listEhrCarePlansFiltered[i].DatePlanned);//Planned Date
				End("tr");
			}
			End("tbody");
			End("table");
			End("text");
			for(int i=0;i<listEhrCarePlansFiltered.Count;i++) {
				Start("entry","typeCode","DRIV");
				Start("act","classCode","ACT","moodCode","INT");
				TemplateId("2.16.840.1.113883.10.20.22.4.20");
				_w.WriteComment("Instructions template");
				Start("code");
				_w.WriteAttributeString("xsi","type",null,"CE");
				Snomed snomedEducation=Snomeds.GetByCode(listEhrCarePlansFiltered[i].SnomedEducation);
				Attribs("code",snomedEducation.SnomedCode,"codeSystem",strCodeSystemSnomed,"displayName",snomedEducation.Description);
				End("code");
				_w.WriteElementString("text",listEhrCarePlansFiltered[i].Instructions);
				StartAndEnd("statusCode","code","completed");
				End("act");
				End("entry");
			}
			End("section");
			End("component");
		}

		///<summary>Helper for GenerateCCD().  Problem section.</summary>
		private static void GenerateCcdSectionProblems() {
			string snomedProblemType="55607006";
			_w.WriteComment(@"
=====================================================================================================
Problems
=====================================================================================================");
			List<Disease> listProblemsAll=Diseases.Refresh(_patOutCcd.PatNum);
			List<Disease> listProblemsFiltered=new List<Disease>();
			for(int i=0;i<listProblemsAll.Count;i++) {
				if(listProblemsAll[i].SnomedProblemType!="" && listProblemsAll[i].SnomedProblemType!=snomedProblemType) {
					continue;//Not a "problem".
				}
				if(listProblemsAll[i].FunctionStatus!=FunctionalStatus.Problem) {
					continue;//Not a "problem".
				}
				listProblemsFiltered.Add(listProblemsAll[i]);
			}
			string status="Inactive";
			string statusCode="73425007";
			string statusOther="active";
			Start("component");
			Start("section");
			if(listProblemsFiltered.Count==0) {
				TemplateId("2.16.840.1.113883.10.20.22.2.5");//Problems section with coded entries optional.
			}
			else {
				TemplateId("2.16.840.1.113883.10.20.22.2.5.1");//Problems section with coded entries required.
			}
			_w.WriteComment("Problems section template");
			StartAndEnd("code","code","11450-4","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Problem list");
			_w.WriteElementString("title","Problems");
			Start("text");
			StartAndEnd("content","ID","problems");
			Start("list","listType","ordered");
			for(int i=0;i<listProblemsFiltered.Count;i++) {//Fill Problems Table				
				Start("item");
				_w.WriteString(DiseaseDefs.GetName(listProblemsFiltered[i].DiseaseDefNum)+" : "+"Status - ");
				if(listProblemsFiltered[i].ProbStatus==ProblemStatus.Active) {
					_w.WriteString("Active");
					status="Active";
					statusCode="55561003";
					statusOther="active";
				}
				else if(listProblemsFiltered[i].ProbStatus==ProblemStatus.Inactive) {
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
			for(int i=0;i<listProblemsFiltered.Count;i++) {//Fill Problems Info
				Start("entry","typeCode","DRIV");
				Start("act","classCode","ACT","moodCode","EVN");
				_w.WriteComment("Problem Concern Act template");//Concern Act Section
				TemplateId("2.16.840.1.113883.10.20.22.4.3");
				Guid();
				StartAndEnd("code","code","CONC","codeSystem","2.16.840.1.113883.5.6","displayName","Concern");
				StartAndEnd("statusCode","code",statusOther);//Allowed values: active, suspended, aborted, completed.
				Start("effectiveTime");
				StartAndEnd("low","value",listProblemsFiltered[i].DateStart.ToString("yyyymmdd"));
				DateElement("high",listProblemsFiltered[i].DateStop);
				End("effectiveTime");
				Start("entryRelationship","typeCode","SUBJ");
				Start("observation","classCode","OBS","moodCode","EVN");
				_w.WriteComment("Problem Observation template");//Observation Section
				TemplateId("2.16.840.1.113883.10.20.22.4.4");
				Guid();
				StartAndEnd("code","code",snomedProblemType,"codeSystem",strCodeSystemSnomed,"displayName","Problem");
				StartAndEnd("statusCode","code","completed");//Allowed values: completed.
				Start("effectiveTime");
				DateElement("low",listProblemsFiltered[i].DateStart);
				End("effectiveTime");
				Start("value");
				_w.WriteAttributeString("xsi","type",null,"CD");
				Attribs("code",DiseaseDefs.GetItem(listProblemsFiltered[i].DiseaseDefNum).SnomedCode,"codeSystem",strCodeSystemSnomed,"displayName",DiseaseDefs.GetItem(listProblemsFiltered[i].DiseaseDefNum).DiseaseName);
				End("value");
				Start("entryRelationship","typeCode","REFR");
				Start("observation","classCode","OBS","moodCode","EVN");
				_w.WriteComment("Status Observation template");//Status Observation Section
				TemplateId("2.16.840.1.113883.10.20.22.4.6");
				Start("code");
				_w.WriteAttributeString("xsi","type",null,"CE");
				Attribs("code","33999-4","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Status");
				End("code");
				StartAndEnd("statusCode","code","completed");//Allowed values: completed.
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
		private static void GenerateCcdSectionProcedures() {
			_w.WriteComment(@"
=====================================================================================================
Procedures
=====================================================================================================");
			List<Procedure> listProcsAll=Procedures.Refresh(_patOutCcd.PatNum);
			List<Procedure> listProcsFiltered=new List<Procedure>();
			for(int i=0;i<listProcsAll.Count;i++) {
				if(listProcsAll[i].ProcStatus==ProcStat.D) {
					continue;
				}
				listProcsFiltered.Add(listProcsAll[i]);
			}
			Start("component");
			Start("section");
			if(listProcsFiltered.Count==0) {
				TemplateId("2.16.840.1.113883.10.20.22.2.7");//Procedures section with coded entries optional (Page 285).
			}
			else {
				TemplateId("2.16.840.1.113883.10.20.22.2.7.1");//Procedures section with coded entries required (Page 285).
			}
			_w.WriteComment("Procedures section template");
			StartAndEnd("code","code","47519-4","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","History of procedures");
			_w.WriteElementString("title","PROCEDURES");
			Start("text");//The following text will be parsed as html with a style sheet to be human readable.
			Start("table","width","100%","border","1");
			Start("thead");
			Start("tr");
			_w.WriteElementString("th","Procedure");
			_w.WriteElementString("th","Date");
			End("tr");
			End("thead");
			Start("tbody");
			for(int i=0;i<listProcsFiltered.Count;i++) {
				Start("tr");
				_w.WriteElementString("td","INSERT PROCEDURE NAME");//TODO: Fill ProcName
				DateElement("td",listProcsFiltered[i].DateTP);//TODO: Decide on a date
				End("tr");
			}
			End("tbody");
			End("table");
			End("text");
			for(int i=0;i<listProcsFiltered.Count;i++) {
				Start("entry","typeCode","DRIV");
				Start("procedure","classCode","PROC","moodCode","EVN");
				TemplateId("2.16.840.1.113883.10.20.22.4.14");//Procedure Activity Section (Page 487).
				_w.WriteComment("Procedure Activity Template");
				Guid();
				StartAndEnd("code","code","SNOMEDCODE","codeSystem",strCodeSystemSnomed,"displayName","PROCEDURENAME","codeSystemName",strCodeSystemNameSnomed);//TODO: Fill in Snomeds
				StartAndEnd("statusCode","code","completed");
				StartAndEnd("effectiveTime","value","CHOOSEADATE");//Todo: choose a date
				StartAndEnd("targetSiteCode","code","SNOMEDCODE","codeSystem",strCodeSystemSnomed,"codeSystemName",strCodeSystemNameSnomed,"displayName","SITEOFTHEPROC");//TODO: I included this, but I am not sure if it is necessary or if we can populate it with the relevant info.
				//The next section I am not sure if we can populate or if we need to, but it isn't hurting anything to add if we just want to comment it out for now.
				Start("performer");
				Start("assignedEntity");
				Guid();
				Start("addr");
				_w.WriteElementString("streetAddressLine","STREETNAME");
				_w.WriteElementString("city","CITYNAME");
				_w.WriteElementString("state","STATEABRV");
				_w.WriteElementString("postalCode","ZIP");
				_w.WriteElementString("country","COUNTRY");//Might be able to default this to US
				End("addr");
				StartAndEnd("telecom","use","CHOOSEAPHONETYPE","value","PHONENUMBER");//TODO: Phone type is something like (WP) for work phone
				Start("representedOrganization");//This section is really completely optional inside the Performer, but it does display the organization name if it is a large group
				Guid();
				_w.WriteElementString("name","PRACTICENAME");
				StartAndEnd("telecom","nullFlavor","UNK");//these are set to UNK because they would be the same as above.
				StartAndEnd("addr","nullFlavor","UNK");//these are set to UNK because they would be the same as above.
				End("representedOrganization");
				End("assignedEntity");
				End("performer");
				End("procedure");
				End("entry");
			}
			End("section");
			End("component");
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionReasonForReferral() {
			//TODO: Allen
		}

		///<summary>Helper for GenerateCCD().  Exports Labs.</summary>
		private static void GenerateCcdSectionResults() {
			_w.WriteComment(@"
=====================================================================================================
Laboratory Test Results
=====================================================================================================");
			List<LabResult> listLabResultAll=LabResults.GetAllForPatient(_patOutCcd.PatNum);
			List<LabResult> listLabResultFiltered=new List<LabResult>();
			for(int i=0;i<listLabResultAll.Count;i++) {
				if(listLabResultAll[i].TestID=="") {
					continue;//Blank codes not allowed in format.
				}
				listLabResultFiltered.Add(listLabResultAll[i]);
			}
			LabPanel labPanel;
			Start("component");
			Start("section");
			if(listLabResultFiltered.Count==0) {
				TemplateId("2.16.840.1.113883.10.20.22.2.3");//page 309 Results section with coded entries optional.
			}
			else {
				TemplateId("2.16.840.1.113883.10.20.22.2.3.1");//page 309 Results section with coded entries required.
			}
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
			for(int i=0;i<listLabResultFiltered.Count;i++) {
				Start("tr");
				_w.WriteElementString("td",listLabResultFiltered[i].TestID);//LOINC Code
				_w.WriteElementString("td",listLabResultFiltered[i].TestName);//Test
				_w.WriteElementString("td",listLabResultFiltered[i].ObsValue+" "+listLabResultFiltered[i].ObsUnits);//Result
				_w.WriteElementString("td",listLabResultFiltered[i].AbnormalFlag.ToString());//Abnormal Flag
				_w.WriteElementString("td",listLabResultFiltered[i].DateTimeTest.ToShortDateString());//Date Performed
				End("tr");
			}
			End("tbody");
			End("table");
			End("text");
			for(int i=0;i<listLabResultFiltered.Count;i++) {
				labPanel=LabPanels.GetOne(listLabResultFiltered[i].LabPanelNum);
				Start("entry","typeCode","DRIV");
				Start("organizer","classCode","BATTERY","moodCode","EVN");
				StartAndEnd("templateId","root","2.16.840.1.113883.10.20.22.4.1");
				_w.WriteComment("Result organizer template");
				Guid();
				if(labPanel.ServiceId=="") {
					StartAndEnd("code","nullFlavor","NA");//Null allowed for this code.
				}
				else {
					StartAndEnd("code","code",labPanel.ServiceId,"codeSystem",strCodeSystemLoinc,"displayName",labPanel.ServiceName);//Code systems allowed: LOINC, or other "local codes".
				}
				StartAndEnd("statusCode","code","completed");//page 532 Allowed values: aborted, active, cancelled, completed, held, suspended.
				//StartAndEnd("effectiveTime","value",listLabResultValid[i].DateTimeTest.ToString("yyyyMMddHHmm"));
				//Start("component");
				//Start("procedure","classCode","PROC","moodCode","EVN");
				//TemplateId("2.16.840.1.113883.3.88.11.83.17","HITSP C83");
				//TemplateId("2.16.840.1.113883.10.20.1.29","CCD");//procedure activity template id, according to pages 103-104 of CCD-final.pdf
				//TemplateId("1.3.6.1.4.1.19376.1.5.3.1.4.19","IHE PCC");
				//StartAndEnd("id");
				//Start("code","code",labPanel.ServiceId,"codeSystem",strCodeSystemSnomed,"displayName",labPanel.ServiceName);
				//Start("originalText");
				//_w.WriteString(labPanel.ServiceName);
				//StartAndEnd("reference","value","Ptr to text  in parent Section");
				//End("originalText");
				//End("code");
				//Start("text");
				//_w.WriteString(labPanel.ServiceName);
				//StartAndEnd("reference","value","Ptr to text  in parent Section");
				//End("text");
				//StartAndEnd("statusCode","code","completed");
				//StartAndEnd("effectiveTime","value",listLabResultValid[i].DateTimeTest.ToString("yyyyMMddHHmm"));
				//End("procedure");
				//End("component");
				Start("component");
				Start("observation","classCode","OBS","moodCode","EVN");
				TemplateId("2.16.840.1.113883.10.20.22.4.2");
				_w.WriteComment("Result observation template");
				Guid();
				StartAndEnd("code","code",listLabResultFiltered[i].TestID,"displayName",listLabResultFiltered[i].TestName,"codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc);
				StartAndEnd("statusCode","code","completed");//Allowed values: aborted, active, cancelled, completed, held, or suspended.
				StartAndEnd("effectiveTime","value",listLabResultFiltered[i].DateTimeTest.ToString("yyyyMMddHHmm"));
				Start("value");
				_w.WriteAttributeString("xsi","type",null,"PQ");
				Attribs("value",listLabResultFiltered[i].ObsValue,"unit",listLabResultFiltered[i].ObsUnits);
				End("value");
				StartAndEnd("interpretationCode","code","N","codeSystem","2.16.840.1.113883.5.83");
				Start("referenceRange");
				Start("observationRange");
				_w.WriteElementString("text",listLabResultFiltered[i].ObsRange);
				End("observationRange");
				End("referenceRange");
				End("observation");
				End("component");
				End("organizer");
				End("entry");
			}
			End("section");
			End("component");
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionSocialHistory() {
			_w.WriteComment(@"
=====================================================================================================
Social History
=====================================================================================================");
			List<Procedure> listProcsAll=Procedures.Refresh(_patOutCcd.PatNum);//Not sure which lists to use, if we even need one for this section
			List<Procedure> listProcsFiltered=new List<Procedure>();
			for(int i=0;i<listProcsAll.Count;i++) {
				if(listProcsAll[i].ProcStatus==ProcStat.D) {
					continue;
				}
				listProcsFiltered.Add(listProcsAll[i]);
			}
			Start("component");
			Start("section");
			if(listProcsFiltered.Count==0) {
				TemplateId("2.16.840.1.113883.10.20.22.2.17");//Social History section with coded entries optional (Page 293).
			}
			else {
				TemplateId("2.16.840.1.113883.10.20.22.2.17.1");//Social History section with coded entries required (Not Sure if this exists).
			}
			_w.WriteComment("Social History section template");
			StartAndEnd("code","code","29762-2","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Social History");
			_w.WriteElementString("title","SOCIAL HISTORY");
			Start("text");//The following text will be parsed as html with a style sheet to be human readable.
			Start("table","width","100%","border","1");
			Start("thead");
			Start("tr");
			_w.WriteElementString("th","Social History Element");
			_w.WriteElementString("th","Description");
			_w.WriteElementString("th","Effective Dates");
			End("tr");
			End("thead");
			Start("tbody");
			for(int i=0;i<listProcsFiltered.Count;i++) {
				Start("tr");
				_w.WriteElementString("td","SOCIAL HISTORY");//TODO: Fill Social history
				_w.WriteElementString("td","SOCIAL HISTORY DESCRIPTION");//TODO: Fill Social history description
				DateElement("td",listProcsFiltered[i].DateTP);//TODO: Decide on a date
				End("tr");
			}
			End("tbody");
			End("table");
			End("text");
			for(int i=0;i<listProcsFiltered.Count;i++) {
				//Pregnancy Observation Template could easily be added in the future, but for now it is skipped (Page 453)
				Start("entry","typeCode","DRIV");
				Start("observation","classCode","OBS","moodCode","EVN");
				TemplateId("2.16.840.1.113883.10.22.4.78");//Smoking Status Observation Section (Page 519).
				_w.WriteComment("Smokin Status Observation Template");
				Guid();
				StartAndEnd("code","code","ASSERTION","codeSystem","2.16.840.1.113883.5.4");
				StartAndEnd("statusCode","code","completed");
				Start("effectiveTime");
				StartAndEnd("low","value","STARTDATE");//TODO: Fill Dates
				StartAndEnd("high","value","ENDDATE");
				End("effectiveTime");
				Start("value");
				_w.WriteAttributeString("xsi","type",null,"CD");
				Attribs("code","SMOKING STATUS CODE","displayName","SMOKING STATUS NAME","codeSystem",strCodeSystemSnomed,"codeSystemName",strCodeSystemNameSnomed);
				End("value");
				End("observation");
				End("entry");
			}
			End("section");
			End("component");
		}

		///<summary>Helper for GenerateCCD().</summary>
		private static void GenerateCcdSectionVitalSigns() {//Currently just a skeleton
			_w.WriteComment(@"
=====================================================================================================
Vital Signs
=====================================================================================================");
			List<Vitalsign> listVitalSignsAll=Vitalsigns.Refresh(_patOutCcd.PatNum);
			List<Vitalsign> listVitalSignsFiltered=new List<Vitalsign>();
			for(int i=0;i<listVitalSignsAll.Count;i++) {
				Vitalsign vitalsign=listVitalSignsAll[i];
				if(vitalsign.DateTaken.Year<1880) {
					continue;
				}
				//Each of the vital sign values are optional, so we must skip filter out empty vital signs.
				float bmi=Vitalsigns.CalcBMI(vitalsign.Weight,vitalsign.Height);//will be 0 if either wight is 0 or height is 0.
				if(vitalsign.Height==0 && vitalsign.Weight==0 && bmi==0 && (vitalsign.BpSystolic==0 || vitalsign.BpDiastolic==0)) {
					continue;//Nothing to report.
				}
				listVitalSignsFiltered.Add(listVitalSignsAll[i]);
			}
			Start("component");
			Start("section");
			if(listVitalSignsFiltered.Count==0) {
				TemplateId("2.16.840.1.113883.10.20.22.2.4");//Vital signs section with coded entries optional.
			}
			else {
				TemplateId("2.16.840.1.113883.10.20.22.2.4.1");//Vital signs section with coded entries required.
			}
			_w.WriteComment("Problems section template");
			StartAndEnd("code","code","8716-3","codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Vital Signs");
			_w.WriteElementString("title","Vital Signs");
			Start("text");//The following text will be parsed as html with a style sheet to be human readable.
			Start("table","width","100%","border","1");
			Start("thead");
			Start("tr");
			_w.WriteElementString("th","Date");
			_w.WriteElementString("th","Height");
			_w.WriteElementString("th","Weight");
			_w.WriteElementString("th","BMI");
			_w.WriteElementString("th","Blood Pressure");
			End("tr");
			End("thead");
			Start("tbody");
			for(int i=0;i<listVitalSignsFiltered.Count;i++) {
				Vitalsign vitalsign=listVitalSignsFiltered[i];
				Start("tr");
				DateText("td",vitalsign.DateTaken);
				if(vitalsign.Height>0) {
					_w.WriteElementString("td",vitalsign.Height.ToString("f0")+" in");
				}
				else {
					_w.WriteElementString("td","");
				}
				if(vitalsign.Weight>0) {
					_w.WriteElementString("td",vitalsign.Weight.ToString("f0")+" lbs");
				}
				else {
					_w.WriteElementString("td","");
				}
				float bmi=Vitalsigns.CalcBMI(vitalsign.Weight,vitalsign.Height);//will be 0 if either wight is 0 or height is 0.
				if(bmi>0) {
					_w.WriteElementString("td",bmi.ToString("f0")+" lbs/in");
				}
				else {
					_w.WriteElementString("td","");
				}
				if(vitalsign.BpSystolic>0 && vitalsign.BpDiastolic>0) {
					_w.WriteElementString("td",vitalsign.BpSystolic.ToString("f0")+"/"+vitalsign.BpDiastolic.ToString("f0"));
				}
				else {
					_w.WriteElementString("td","");
				}
				End("tr");
			}
			End("tbody");
			End("table");
			End("text");
			for(int i=0;i<listVitalSignsFiltered.Count;i++) {//Fill Vital Signs Info
				Vitalsign vitalsign=listVitalSignsFiltered[i];
				Start("entry","typeCode","DRIV");
				Start("organizer","classCode","CLUSTER","moodCode","EVN");
				_w.WriteComment("Vital Signs Organizer template");//Vital Signs Organizer
				TemplateId("2.16.840.1.113883.10.20.22.4.26");
				Guid();
				StartAndEnd("code","code","46680005","codeSystem",strCodeSystemSnomed,"codeSystemName",strCodeSystemNameSnomed,"displayName","Vital signs");
				StartAndEnd("statusCode","code","completed");
				DateElement("effectiveTime",vitalsign.DateTaken);
				if(vitalsign.Height>0) {
					GenerateCcdVitalSign("8302-2",vitalsign.DateTaken,vitalsign.Height,"in");//Height
				}
				if(vitalsign.Weight>0) {
					GenerateCcdVitalSign("3141-9",vitalsign.DateTaken,vitalsign.Weight,"lbs");//Weight
				}
				float bmi=Vitalsigns.CalcBMI(vitalsign.Weight,vitalsign.Height);//will be 0 if either wight is 0 or height is 0.
				if(bmi>0) {
					GenerateCcdVitalSign("39156-5",vitalsign.DateTaken,bmi,"lbs/in");//BMI
				}
				if(vitalsign.BpSystolic>0 && vitalsign.BpDiastolic>0) {
					GenerateCcdVitalSign("8480-6",vitalsign.DateTaken,vitalsign.BpSystolic,"mmHg");//Blood Pressure Systolic
					GenerateCcdVitalSign("8462-4",vitalsign.DateTaken,vitalsign.BpDiastolic,"mmHg");//Blood Pressure Diastolic
				}
			}
			End("section");
			End("component");
		}

		///<summary>Helper for GenerateCcdSectionVitalSigns(). Writes on observation. 
		///Allowed vital sign observation template LOINC codes (strLoincObservationCode):
		///9279-1		Respiratory Rate
		///8867-4		Heart Rate
		///2710-2		O2 % BldC Oximetry,
		///8480-6		BP Systolic
		///8462-4		BP Diastolic
		///8310-5		Body Temperature,
		///8302-2		Height
		///8306-3		Height (Lying)
		///8287-5		Head Circumference,
		///3141-9		Weight Measured
		///39156-5	BMI (Body Mass Index)
		///3140-1 BSA (Body Surface Area)</summary>
		private static void GenerateCcdVitalSign(string strLoincObservationCode,DateTime dateTimeObservation,float observationValue,string observationUnits) {
			Start("component");
			Start("observation","classCode","OBS","moodCode","EVN");
			_w.WriteComment("Vital Sign Observation template");//Vital Sign Observation Section
			TemplateId("2.16.840.1.113883.10.20.22.4.27");
			Guid();
			StartAndEnd("code","code",strLoincObservationCode,"codeSystem",strCodeSystemLoinc,"codeSystemName",strCodeSystemNameLoinc,"displayName","Height");
			StartAndEnd("statusCode","code","completed");//Allowed values: completed.
			DateElement("effectiveTime",dateTimeObservation);
			Start("value");
			_w.WriteAttributeString("xsi","type",null,"PQ");
			Attribs("value",observationValue.ToString("f0"),"unit",observationUnits);
			End("value");
			End("observation");
			End("component");
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
		private static void Guid() {
			Guid uuid=System.Guid.NewGuid();
			while(_hashCcdGuids.Contains(uuid.ToString())) {
				uuid=System.Guid.NewGuid();
			}
			_hashCcdGuids.Add(uuid.ToString());
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

		///<summary>Use for HTML tables. Writes the element strElement name and writes the dateTime string in the required date format.  Will not write if year is before 1880.</summary>
		private static void DateText(string strElementName,DateTime dateTime) {
			Start(strElementName);
			if(dateTime.Year>1880) {
				_w.WriteString(dateTime.ToString("yyyyMMdd"));
			}
			End(strElementName);
		}

		///<summary>Use for XML. Writes the element strElement name and writes the dateTime in the required date format into the value attribute.
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

		private static void AddressUnitedStates(string strAddress1,string strAddress2,string strCity,string strState) {
			Start("addr","use","HP");
			_w.WriteElementString("streetAddressLine",strAddress1);
			if(strAddress2!="") {
				_w.WriteElementString("streetAddressLine",strAddress2);
			}
			_w.WriteElementString("city",strCity);
			_w.WriteElementString("state",strState);
			_w.WriteElementString("country","United States");
			End("addr");
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
