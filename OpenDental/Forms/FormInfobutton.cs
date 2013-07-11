using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	public partial class FormInfobutton:Form {
		public Patient PatCur;
		public Disease ProblemCur;//should this be named disease or problem? Also snomed/medication
		public Medication MedicationCur;
		public LabResult LabCur;
		public bool PerformerIsProvider;
		public bool RecipientIsProvider;

		public FormInfobutton() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormInfobutton_Load(object sender,EventArgs e) {
			fillComboBox();
			fillContext();
			//Fill context with provider and/or patient information.
			if(ProblemCur!=null) {
				groupBoxProblem.Show();
				fillProblem();
				comboRequestType.SelectedIndex=0;
			}
			else if(MedicationCur!=null) {
				groupBoxMedication.Show();
				fillMedication();
				comboRequestType.SelectedIndex=1;
			}
			else if(LabCur!=null) {
				groupBoxLab.Show();
				fillLabResult();
				comboRequestType.SelectedIndex=2;
			}
			else {
				//dislpay nothing until user selects a request type??
				//or should we get generic information?
			}
			displayGroupBoxesHelper();
		}

		private void fillComboBox() {
			comboRequestType.Items.Add(Lan.g(this,"Problem"));
			comboRequestType.Items.Add(Lan.g(this,"Medication"));
			comboRequestType.Items.Add(Lan.g(this,"Lab Result"));
		}

		private void displayGroupBoxesHelper() {
			groupBoxProblem.Hide();
			groupBoxMedication.Hide();
			groupBoxLab.Hide();
			switch(comboRequestType.SelectedIndex) {
				case 0://Problem
					groupBoxProblem.Show();
					break;
				case 1://Medication
					groupBoxMedication.Show();
					break;
				case 2://Lab Result
					groupBoxLab.Show();
					break;
				default:
					//should never happen.
					break;
			}
		}

		private void fillContext() {
		}

		private void fillProblem() {
			//ProblemCur.DiseaseDefNum
		}

		private void fillMedication() {
			textMedName.Text=MedicationCur.MedName;
			//throw new NotImplementedException();
		}

		private void fillLabResult() {
			//throw new NotImplementedException();
		}

		private void comboRequestType_SelectedIndexChanged(object sender,EventArgs e) {
			displayGroupBoxesHelper();
		}

		private void butPreview_Click(object sender,EventArgs e) {
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste("");//generateKnowledgeRequestNotification());
		}

		#region generateKnowledeRequestNotification XML
		///<summary></summary>
		private string generateKnowledgeRequestNotification() {//This is also known as the HL7 CDS message type.
			StringBuilder strb = new StringBuilder();
			XmlWriter xmlw = XmlWriter.Create(strb);
			//Each item is followed by a cardinality in the form of [n..m] where n is the least number of times the element may occur and m is the 
			//greatest number of times an element may occur. *(asterisk) means any number of times. [1..1] is mandatory, [0..*] is optional, etc.
			strb.Append(@"<knowledgeRequestNotification classCode=""ACT"" moodCode=""DEF"">\r\n");//[1..1]
			strb.Append(@"<id />\r\n");//[0..*]
			strb.Append(@"<effectiveTime value="""+DateTime.Now.ToString("YYYYMMDDhhmmss")+@"""/>\r\n");//[0..1]
			strb.Append(subjectHelper());//[0..1]
			strb.Append(holderHelper());//[0..1]
			strb.Append(performerHelper());//[0..1]
			strb.Append(informationRecipientHelper());//[0..1]
			strb.Append(subject1Helper());//[0..1]
			strb.Append(subject2Helper());//[0..1]
			strb.Append(subject3Helper());//[1..1] mandatory
			strb.Append(component1Helper());//[0..1]
			strb.Append(@"</knowledgeRequestNotification>");
			return strb.ToString();
		}

		///<summary>"Subject1" is a "subject" object, not a "subject1" object. Per HL7 documentation.</summary>
		private string subjectHelper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<subject1 typeCode=""SBJ"">\r\n");//[1..1]
			strb.Append(patientContextHelper());//[1..1]
			strb.Append(@"</subject1>\r\n");//[1..1]
			return strb.ToString();
		}

		private string patientContextHelper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<patientContext classCode=""PAT"">\r\n");//[1..1]
			strb.Append(patientPersonHelper());//[0..1]
			strb.Append(subject6Helper());//[0..*]
			strb.Append(@"</patientContext>\r\n");//[1..1]
			return strb.ToString();
		}

		private string patientPersonHelper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<patientPerson classCode=""PSN"" determinerCode=""INSTANCE"">\r\n");//[1..1]
			strb.Append(administrativeGenderHelper());//[0..1]
			strb.Append(@"</patientPerson>\r\n");//[1..1]
			return "";
		}

		private string administrativeGenderHelper() {
			switch(PatCur.Gender) {
				case PatientGender.Female:
					return @"<administrativeGender code=""F"" codeSystem=""2.16.840.1.113883.5.1"" codeSystemName=""AdministrativeGender"" displayName=""Female""/>\r\n";
				case PatientGender.Male:
					return @"<administrativeGender code=""M"" codeSystem=""2.16.840.1.113883.5.1"" codeSystemName=""AdministrativeGender"" displayName=""Male""/>\r\n";
				case PatientGender.Unknown:
				default://should never happen
					return @"<administrativeGender code=""UN"" codeSystem=""2.16.840.1.113883.5.1"" codeSystemName=""AdministrativeGender"" displayName=""Undifferentiated""/>\r\n";
			}
		}

		///<summary>Also called both Subject6 and subjectOf in the HL7 documentation. Not to be confused with the other Subject Objects.</summary>
		private string subject6Helper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<subjectOf typeCode=""SBJ"">\r\n");//[1..1]
			strb.Append(ageChoiceHelper(true,true));//[1..1]
			strb.Append(@"</subjectOf>\r\n");//[1..1]
			return "";
		}

		///<summary>Must use one or both of the Age/AgeGroup options.</summary>
		private string ageChoiceHelper(bool useAge, bool useAgeGroups) {
			if(useAge&useAgeGroups==false) {
				useAge=true;
				useAgeGroups=true;
			}
			string retVal="";
			if(useAge) {
				retVal+=ageHelper();
			}
			if(useAgeGroups){
				retVal+=ageGroupHelper();
			}
			return retVal;
		}

		private string ageHelper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<age classCode=""OBS"" moodCode=""DEF"">\r\n");//[1..1]
			strb.Append(@"<code=""30525-0"" codeSystem=""2.16.840.1.113883.6.1"" codeSystemName=""LN"" displayName=""AGE""/>");//[1..1]
			strb.Append(@"<value value="""+PatCur.Age+@""" unit=""a""/>");//[1..1]
			strb.Append(@"</age>\r\n");//[1..1]
			return strb.ToString();
		}

		private string ageGroupHelper() {
			#region MeSH (Medical Subject Headers) codes used for age groups.
						//*NEWRECORD
						//RECTYPE = D
						//MH = Infant, NewbornGM = birth to 1 month age group
						//MS = An infant during the first month after birth.
						//UI = D007231
						//
						//*NEWRECORD
						//RECTYPE = D
						//MH = Infant
						//GM = 1 month to 2 year age group; + includes birth to 2 years; for birth to 1 month, use Infant, Newborn +
						//MS = A child between 1 and 23 months of age.
						//UI = D007223
						//
						//*NEWRECORD
						//RECTYPE = D
						//MH = Child, Preschool
						//GM = 2-5 age group; for 1 month to 2 years use Infant +
						//MS = A child between the ages of 2 and 5.
						//UI = D002675
						//
						//*NEWRECORD
						//RECTYPE = D
						//MH = Child
						//MH = ChildGM = 6-12 age group; for 2-5 use Child, Preschool; + includes birth to 18 year age group
						//MS = A person 6 to 12 years of age. An individual 2 to 5 years old is CHILD, PRESCHOOL.
						//UI = D002648
						//
						//*NEWRECORD
						//RECTYPE = D
						//MH = Adolescent
						//AN = age 13-18 yr; IM as psychol & sociol entity; check tag ADOLESCENT for NIM; Manual 18.5.12, 34.9.5
						//MS = A person 13 to 18 years of age.
						//UI = D000293
						//
						//*NEWRECORD
						//RECTYPE = D
						//MH = Adult
						//GM = 19-44 age group; older than 44, use Middle Age, Aged +, or + for all
						//MS = A person having attained full growth or maturity. Adults are of 19 through 44 years of age. For a person between 19 and 24 years of age, YOUNG ADULT is available.
						//UI = D000328
						//
						//*NEWRECORD
						//RECTYPE = D
						//MH = Middle Aged
						//AN = age 45-64; IM as psychol, sociol entity: Manual 18.5.12; NIM as check tag; Manual 34.10 for indexing examples
						//UI = D008875
						//
						//*NEWRECORD
						//RECTYPE = D
						//MH = Aged
						//GM = 65 and older; consider also Aged, 80 and over
						//MS = A person 65 through 79 years of age. For a person older than 79 years, AGED, 80 AND OVER is available.
						//UI = D000368
						//
						//*NEWRECORD
						//RECTYPE = D
						//MH = Aged, 80 and over
						//GM = consider also Aged + (65 and older)
						//MS = A person 80 years of age and older.
						//UI = D000369
			#endregion
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<ageGroup classCode=""OBS"" moodCode=""DEF"">\r\n");//[1..1]
			strb.Append(@"<code=""46251-5"" codeSystem=""2.16.840.1.113883.6.1"" codeSystemName=""LN""/>\r\n");//[1..1]
			if(PatCur.Birthdate.AddMonths(1)>DateTime.Now) {//less than 1mo old, newborn
				strb.Append(@"<value code=""D007231"" codeSystem=""2.16.840.1.113883.6.1"" codeSystemName=""MSH"" displayName=""Newborn""/>\r\n");//[1..1]
			}
			else if(PatCur.Birthdate.AddYears(2)>DateTime.Now) {//less than 2 yrs old, Infant
				strb.Append(@"<value code=""D007223"" codeSystem=""2.16.840.1.113883.6.1"" codeSystemName=""MSH"" displayName=""Infant""/>\r\n");//[1..1]
			}
			else if(PatCur.Birthdate.AddYears(5)>DateTime.Now) {//2 to 5 yrs old, Preschool
				strb.Append(@"<value code=""D007675"" codeSystem=""2.16.840.1.113883.6.1"" codeSystemName=""MSH"" displayName=""Preschool""/>\r\n");//[1..1]
			}
			else if(PatCur.Birthdate.AddYears(12)>DateTime.Now) {//6 to 12 yrs old, Child
				strb.Append(@"<value code=""D002648"" codeSystem=""2.16.840.1.113883.6.1"" codeSystemName=""MSH"" displayName=""Child""/>\r\n");//[1..1]
			}
			else if(PatCur.Birthdate.AddYears(18)>DateTime.Now) {//13 to 18 yrs old, Adolescent
				strb.Append(@"<value code=""D000293"" codeSystem=""2.16.840.1.113883.6.1"" codeSystemName=""MSH"" displayName=""Adolescent""/>\r\n");//[1..1]
			}
			else if(PatCur.Birthdate.AddYears(44)>DateTime.Now) {//19 to 44 yrs old, Adult
				strb.Append(@"<value code=""D000328"" codeSystem=""2.16.840.1.113883.6.1"" codeSystemName=""MSH"" displayName=""Adult""/>\r\n");//[1..1]
			}
			else if(PatCur.Birthdate.AddYears(64)>DateTime.Now) {//45 to 64 yrs old, Middle Aged
				strb.Append(@"<value code=""D008875"" codeSystem=""2.16.840.1.113883.6.1"" codeSystemName=""MSH"" displayName=""Middle Aged""/>\r\n");//[1..1]
			}
			else if(PatCur.Birthdate.AddYears(79)>DateTime.Now) {//65 to 79 yrs old, Aged
				strb.Append(@"<value code=""D000368"" codeSystem=""2.16.840.1.113883.6.1"" codeSystemName=""MSH"" displayName=""Aged""/>\r\n");//[1..1]
			}
			else{ //if(PatCur.Birthdate.AddYears(79)>DateTime.Now) {//80 yrs old or older, Aged, 80 and over
				strb.Append(@"<value code=""D000369"" codeSystem=""2.16.840.1.113883.6.1"" codeSystemName=""MSH"" displayName=""Aged, 80 and over""/>\r\n");//[1..1]
			}
			strb.Append(@"</ageGroup>\r\n");//[1..1]
			return strb.ToString();
		}

		private string holderHelper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<holder typeCode=""HLD"">\r\n");//[1..1]
			strb.Append(assignedEntityHelper());//[1..1]
			strb.Append(@"</holder>\r\n");//[1..1]
			return strb.ToString();
		}

		private string assignedEntityHelper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<assignedEntity classCode=""ASSIGNED"">\r\n");//[1..1]
			strb.Append(@"<name>"+Security.CurUser.UserName+"</name>\r\n");//[0..1]
			strb.Append(@"<certificateText>"+Security.CurUser.Password+"</certificateText>\r\n");//[0..1]
			strb.Append(authorizedPersonHelper());//[0..1]
			strb.Append(organizationHelper());//[0..1]
			strb.Append(@"</assignedEntity>\r\n");//[1..1]
			return strb.ToString();
		}

		///<summary>Refered to as both assignedAuthorizedPerson and authorizedPerson in the HL7 documentation.</summary>
		private string authorizedPersonHelper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<authorizedPerson classCode=""PSN"" determinerCode=""INSTANCE"">\r\n");//[1..1]
			strb.Append(@"<id value="""+Security.CurUser.UserNum+@"""/>\r\n");//[0..*]
			strb.Append(@"</holder>\r\n");//[1..1]
			return strb.ToString();
		}

		///<summary>refered to as both representedOrganization and organization</summary>
		private string organizationHelper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<organization classCode=""ORG"" determinerCode=""INSTANCE"">\r\n");//[1..1]
			//strb.Append(@"<id value="""+PrefC.GetLong(PrefName.RegistrationKey)+@"""/>\r\n");//[0..*]
			strb.Append(@"<name value="""+PrefC.GetLong(PrefName.PracticeTitle)+@"""/>\r\n");//[0..1]
			strb.Append(@"</organization>\r\n");//[1..1]
			return strb.ToString();
		}

		private string performerHelper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<performer typeCode=""PRF"">\r\n");//[1..1]
			strb.Append(performerChoiceHelper(PerformerIsProvider));//[1..1]
			strb.Append(@"</performer>\r\n");//[1..1]
			return strb.ToString();
		}

		private string informationRecipientHelper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<informationRecipient typeCode=""IRCP"">\r\n");//[1..1]
			strb.Append(performerChoiceHelper(RecipientIsProvider));//[1..1]
			strb.Append(@"</informationRecipient>\r\n");//[1..1]
			return strb.ToString();
		}

		private string performerChoiceHelper(bool isProvider) {
			if(isProvider) {
				return healthCareProviderHelper();
			}
			else {
				return patientHelper();
			}
		}

		private string healthCareProviderHelper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<healthCareProvider classCode=""PROV"">\r\n");//[1..1]
			strb.Append(@"<code code=""120000000X"" codeSystem=""2.16.840.1.113883.6.101"" codeSystemName=""NUCC Health Care Provider Taxonomy"" displayName=""Dental Providers""/>\r\n");//[0..1] 120000000X covers all dental provider specialties ranging from Dental Hygenists to Dentists.
			strb.Append(personHelper(true));//[0..1]
			strb.Append(@"</healthCareProvider>\r\n");//[1..1]
			return strb.ToString();
		}

		///<summary>Refered to as person, patientPerson, and healthCarePerson in HL7 documentation.</summary>
		private string personHelper(bool isHealthCarePerson) {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<person typeCode=""PSN"" determinerCode=""INSTANCE"">\r\n");//[1..1]
			strb.Append(languageCommunicationHelper(System.Globalization.CultureInfo.CurrentCulture.ThreeLetterISOLanguageName,System.Globalization.CultureInfo.CurrentCulture.Name));//[1..*]
			strb.Append(@"</person>\r\n");//[1..1]
			return strb.ToString();
		}

		///<summary>Currently only supports language used on local machine. No user input allowed yet.</summary>
		/// <param name="cultureID">Three letter ISO 639-2 language code. System.Globalization.CultureInfo.CurrentCulture.ThreeLetterISOLanguageName</param>
		/// <param name="cultureName">Display name for language. System.Globalization.CultureInfo.CurrentCulture.Name</param>
		/// <returns></returns>
		private string languageCommunicationHelper(string cultureID, string cultureName) {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<languageCommunication>\r\n");//[1..1]
			strb.Append(@"<languageCode code="""+cultureID+@""" codeSystem=""1.0.639.2"" codeSystemName=""ISO 639-2: Codes for the representation of names of languages -- Part 2: Alpha-3 code"" displayName="""+cultureName+@"""/>\r\n");//[1..1] default to requesting information in the language being used on the local system.
			strb.Append(@"</languageCommunication>\r\n");//[1..1]
			return strb.ToString();
		}

		private string patientHelper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<patient classCode=""PAT"">\r\n");//[1..1]
			strb.Append(personHelper(false));//[1..1]
			strb.Append(@"</patient>\r\n");//[1..1]
			return strb.ToString();
		}

		///<summary>"Subject2" is a "subject1" object, not a "subject2" object. Per HL7 documentation.</summary>
		private string subject1Helper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<subject2 typeCode=""SUBJ"">\r\n");//[1..1]
			strb.Append(taskContextHelper());//[1..1]
			strb.Append(@"</subject2>\r\n");//[1..1]
			return strb.ToString();
		}

		private string taskContextHelper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<taskContext classCode=""ACT"" moodCode=""DEF"">\r\n");//[1..1]
			strb.Append(@"<code code=""TODO"" codeSystem=""2.16.840.1.113883.5.4"" codeSystemName=""ActCode"" displayName=""TODO""");//[1..1]
			strb.Append(@"</taskContext>\r\n");//[1..1]
			return strb.ToString();
		}

		///<summary>"Subject3" is a "subject2" object, not a "subject3" object. Per HL7 documentation.</summary>
		private string subject2Helper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<subject3 typeCode=""SBJ"">\r\n");//[1..1]
			strb.Append(patientContextHelper());//[1..1]
			strb.Append(@"</subject3>\r\n");//[1..1]
			return strb.ToString();
		}

		///<summary>"Subject4" is a "subject3" object. Per HL7 documentation.</summary>
		private string subject3Helper() {
			StringBuilder strb = new StringBuilder();
			strb.Append(@"<subject4 typeCode=""SBJ"">\r\n");//[1..1]
			strb.Append(patientContextHelper());//[1..1]
			strb.Append(@"</subject4>\r\n");//[1..1]
			return strb.ToString();
		}

		private string component1Helper() {
			throw new NotImplementedException();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		#endregion
	}
}