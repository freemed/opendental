using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace OpenDentBusiness {
	public class ErxXml {

		public static string BuildClickThroughXml(Provider prov,Employee emp,Patient pat) {
			NCScript ncScript=new NCScript();
			ncScript.Credentials=new CredentialsType();
			ncScript.Credentials.partnerName="OpenDental";//Assigned by NewCrop. Used globally for all customers.
			string newCropName=PrefC.GetString(PrefName.NewCropName);
			if(newCropName=="") { //Resellers use this field to send different credentials. Thus, if blank, then send OD credentials.
				ncScript.Credentials.name=CodeBase.MiscUtils.Decrypt("Xv40GArhEXYjEZxAE3Fw9g==");//Assigned by NewCrop. Used globally for all customers.
				ncScript.Credentials.password=CodeBase.MiscUtils.Decrypt("Xv40GArhEXYjEZxAE3Fw9g==");//Assigned by NewCrop. Used globally for all customers.
			}
			else { //Reseller
				ncScript.Credentials.name=newCropName;
				ncScript.Credentials.password=PrefC.GetString(PrefName.NewCropPassword);
			}
			ncScript.Credentials.productName="OpenDental";
			ncScript.Credentials.productVersion=Assembly.GetAssembly(typeof(Db)).GetName().Version.ToString();
			ncScript.UserRole=new UserRoleType();
			if(emp==null) {
				ncScript.UserRole.user=UserType.LicensedPrescriber;
				ncScript.UserRole.role=RoleType.doctor;
			}
			else {
				ncScript.UserRole.user=UserType.Staff;
				ncScript.UserRole.role=RoleType.nurse;
			}
			ncScript.Destination=new DestinationType();
			ncScript.Destination.requestedPage=RequestedPageType.compose;//This is the tab that the user will want 90% of the time.
			string practiceTitle=PrefC.GetString(PrefName.PracticeTitle);//May be blank.
			string practicePhone=PrefC.GetString(PrefName.PracticePhone);//Validated to be 10 digits within the chart.
			string practiceFax=PrefC.GetString(PrefName.PracticeFax);//Validated to be 10 digits within the chart.
			string practiceAddress=PrefC.GetString(PrefName.PracticeAddress);//Validated to exist in chart.
			string practiceAddress2=PrefC.GetString(PrefName.PracticeAddress2);//May be blank.
			string practiceCity=PrefC.GetString(PrefName.PracticeCity);//Validated to exist in chart.
			string practiceState=PrefC.GetString(PrefName.PracticeST);//Validated to be a US state code in chart.
			string practiceZip=Regex.Replace(PrefC.GetString(PrefName.PracticeZip),"[^0-9]*","");//Zip with all non-numeric characters removed. Validated to be 9 digits in chart.
			string practiceZip4=practiceZip.Substring(5);//Last 4 digits of zip.
			practiceZip=practiceZip.Substring(0,5);//First 5 digits of zip.
			string country="US";//Always United States for now.
			//if(CultureInfo.CurrentCulture.Name.Length>=2) {
			//  country=CultureInfo.CurrentCulture.Name.Substring(CultureInfo.CurrentCulture.Name.Length-2);
			//}
			ncScript.Account=new AccountTypeRx();
			//Each LicensedPrescriberID must be unique within an account. Since we send ProvNum for LicensedPrescriberID, each OD database must have a unique AccountID.
			ncScript.Account.ID=PrefC.GetString(PrefName.NewCropAccountId);//Customer account number then a dash then a random alpha-numeric string of 3 characters, followed by 2 digits.
			ncScript.Account.accountName=practiceTitle;//May be blank.
			ncScript.Account.siteID="1";//Always send 1.  For each AccountID/SiteID pair, a separate database will be created in NewCrop.
			ncScript.Account.AccountAddress=new AddressType();
			ncScript.Account.AccountAddress.address1=practiceAddress;//Validated to exist in chart.
			ncScript.Account.AccountAddress.address2=practiceAddress2;//May be blank.
			ncScript.Account.AccountAddress.city=practiceCity;//Validated to exist in chart.
			ncScript.Account.AccountAddress.state=practiceState;//Validated to be a US state code in chart.
			ncScript.Account.AccountAddress.zip=practiceZip;//Validated to be 9 digits in chart. First 5 digits go in this field.
			ncScript.Account.AccountAddress.zip4=practiceZip4;//Validated to be 9 digits in chart. Last 4 digits go in this field.
			ncScript.Account.AccountAddress.country=country;//Validated above.
			ncScript.Account.accountPrimaryPhoneNumber=practicePhone;//Validated to be 10 digits within the chart.
			ncScript.Account.accountPrimaryFaxNumber=practiceFax;//Validated to be 10 digits within the chart.
			ncScript.Location=new LocationType();
			if(PrefC.GetBool(PrefName.EasyNoClinics) || pat.ClinicNum==0) { //No clinics.
				ncScript.Location.ID="0";//Always 0, since clinicnums must be >= 1, will never overlap with a clinic if the office turns clinics on after first use.
				ncScript.Location.locationName=practiceTitle;//May be blank.
				ncScript.Location.LocationAddress=new AddressType();
				ncScript.Location.LocationAddress.address1=practiceAddress;//Validated to exist in chart.
				ncScript.Location.LocationAddress.address2=practiceAddress2;//May be blank.
				ncScript.Location.LocationAddress.city=practiceCity;//Validated to exist in chart.
				ncScript.Location.LocationAddress.state=practiceState;//Validated to be a US state code in chart.
				ncScript.Location.LocationAddress.zip=practiceZip;//Validated to be 9 digits in chart. First 5 digits go in this field.
				ncScript.Location.LocationAddress.zip4=practiceZip4;//Validated to be 9 digits in chart. Last 4 digits go in this field.
				ncScript.Location.LocationAddress.country=country;//Validated above.
				ncScript.Location.primaryPhoneNumber=practicePhone;//Validated to be 10 digits within the chart.
				ncScript.Location.primaryFaxNumber=practiceFax;//Validated to be 10 digits within the chart.
				ncScript.Location.pharmacyContactNumber=practicePhone;//Validated to be 10 digits within the chart.
			}
			else { //Using clinics.
				Clinic clinic=Clinics.GetClinic(pat.ClinicNum);
				ncScript.Location.ID=clinic.ClinicNum.ToString();//A positive integer.
				ncScript.Location.locationName=clinic.Description;//May be blank.
				ncScript.Location.LocationAddress=new AddressType();
				ncScript.Location.LocationAddress.address1=clinic.Address;//Validated to exist in chart.
				ncScript.Location.LocationAddress.address2=clinic.Address2;//May be blank.
				ncScript.Location.LocationAddress.city=clinic.City;//Validated to exist in chart.
				ncScript.Location.LocationAddress.state=clinic.State;//Validated to be a US state code in chart.
				string clinicZip=Regex.Replace(clinic.Zip,"[^0-9]*","");//Zip with all non-numeric characters removed. Validated to be 9 digits in chart.
				string clinicZip4=clinicZip.Substring(5);//Last 4 digits of zip.
				clinicZip=clinicZip.Substring(0,5);//First 5 digits of zip.
				ncScript.Location.LocationAddress.zip=clinicZip;//Validated to be 9 digits in chart. First 5 digits go in this field.
				ncScript.Location.LocationAddress.zip4=clinicZip4;//Validated to be 9 digits in chart. Last 4 digits go in this field.
				ncScript.Location.LocationAddress.country=country;//Validated above.
				ncScript.Location.primaryPhoneNumber=clinic.Phone;//Validated to be 10 digits within the chart.
				ncScript.Location.primaryFaxNumber=clinic.Fax;//Validated to be 10 digits within the chart.
				ncScript.Location.pharmacyContactNumber=clinic.Phone;//Validated to be 10 digits within the chart.
			}
			ncScript.LicensedPrescriber=new LicensedPrescriberType();
			ncScript.LicensedPrescriber.ID=prov.ProvNum.ToString();//A positive integer.
			//UPIN is obsolete
			ncScript.LicensedPrescriber.LicensedPrescriberName=new PersonNameType();
			ncScript.LicensedPrescriber.LicensedPrescriberName.last=prov.LName;//May be blank.
			ncScript.LicensedPrescriber.LicensedPrescriberName.first=prov.FName;//May be blank.
			ncScript.LicensedPrescriber.LicensedPrescriberName.middle=prov.MI;//May be blank.
			ncScript.LicensedPrescriber.dea=prov.DEANum;//May be blank.
			ncScript.LicensedPrescriber.licenseState=prov.StateWhereLicensed;//Validated to be a US state code in the chart.
			ncScript.LicensedPrescriber.licenseNumber=prov.StateLicense;//Validated to exist in chart.
			ncScript.LicensedPrescriber.npi=prov.NationalProvID;//Validated to be 10 digits in chart.
			if(emp!=null) {
				ncScript.Staff=new StaffType();
				ncScript.Staff.ID=emp.EmployeeNum.ToString();//A positive integer.
				ncScript.Staff.StaffName=new PersonNameType();
				ncScript.Staff.StaffName.first=emp.FName;//First name or last name will not be blank. Validated in Employee Edit window.
				ncScript.Staff.StaffName.last=emp.LName;//First name or last name will not be blank. Validated in Employee Edit window.
				ncScript.Staff.StaffName.middle=emp.MiddleI;//May be blank.
			}
			ncScript.Patient=new PatientType();
			ncScript.Patient.ID=pat.PatNum.ToString();//A positive integer.
			ncScript.Patient.PatientName=new PersonNameType();
			ncScript.Patient.PatientName.last=pat.LName;//Validated to exist in Patient Edit window.
			ncScript.Patient.PatientName.first=pat.FName;//May be blank.
			ncScript.Patient.PatientName.middle=pat.MiddleI;//May be blank.
			ncScript.Patient.medicalRecordNumber=pat.PatNum.ToString();//A positive integer.
			//NewCrop specifically requested that we do not send SSN.
			//ncScript.Patient.socialSecurityNumber=Regex.Replace(pat.SSN,"[^0-9]*","");//Removes all non-numerical characters.
			ncScript.Patient.PatientAddress=new AddressOptionalType();
			ncScript.Patient.PatientAddress.address1=pat.Address;//May be blank.
			ncScript.Patient.PatientAddress.address2=pat.Address2;//May be blank.
			ncScript.Patient.PatientAddress.city=pat.City;//May be blank.
			ncScript.Patient.PatientAddress.state=pat.State;//May be blank. Validated in chart to be blank or to be a valid US state code.
			ncScript.Patient.PatientAddress.zip=pat.Zip;//May be blank.
			ncScript.Patient.PatientAddress.country=country;//Validated above.
			ncScript.Patient.PatientContact=new ContactType();
			ncScript.Patient.PatientContact.homeTelephone=pat.HmPhone;//May be blank. Does not need to be 10 digits.
			ncScript.Patient.PatientCharacteristics=new PatientCharacteristicsType();
			ncScript.Patient.PatientCharacteristics.dob=pat.Birthdate.ToString("yyyyMMdd");//DOB must be in CCYYMMDD format.
			if(pat.Gender==PatientGender.Male) {
				ncScript.Patient.PatientCharacteristics.gender=GenderType.M;
			}
			else if(pat.Gender==PatientGender.Female) {
				ncScript.Patient.PatientCharacteristics.gender=GenderType.F;
			}
			else {
				ncScript.Patient.PatientCharacteristics.gender=GenderType.U;
			}
			ncScript.Patient.PatientCharacteristics.genderSpecified=true;
			//Serialize
			MemoryStream memoryStream=new MemoryStream();
			XmlSerializer xmlSerializer=new XmlSerializer(typeof(NCScript));
			xmlSerializer.Serialize(memoryStream,ncScript);
			byte[] memoryStreamInBytes=memoryStream.ToArray();
			return Encoding.UTF8.GetString(memoryStreamInBytes,0,memoryStreamInBytes.Length);
		}

	}
}
