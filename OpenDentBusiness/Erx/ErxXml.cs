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
			ncScript.Credentials.name=CodeBase.MiscUtils.Decrypt("Xv40GArhEXYjEZxAE3Fw9g==");//Assigned by NewCrop. Used globally for all customers.
			ncScript.Credentials.password=CodeBase.MiscUtils.Decrypt("Xv40GArhEXYjEZxAE3Fw9g==");//Assigned by NewCrop. Used globally for all customers.
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
			ncScript.Destination.requestedPage=RequestedPageType.compose;
			string practiceTitle=PrefC.GetString(PrefName.PracticeTitle);
			string practicePhone=PrefC.GetString(PrefName.PracticePhone);//Validated to be 10 digits within the chart.
			string practiceFax=PrefC.GetString(PrefName.PracticeFax);//Validated to be 10 digits within the chart.
			string practiceAddress=PrefC.GetString(PrefName.PracticeAddress);
			string practiceAddress2=PrefC.GetString(PrefName.PracticeAddress2);
			string practiceCity=PrefC.GetString(PrefName.PracticeCity);
			string practiceState=PrefC.GetString(PrefName.PracticeST);
			string practiceZip=Regex.Replace(PrefC.GetString(PrefName.PracticeZip),"[^0-9]*","");//Zip with all non-numeric characters removed. Validated to be 9 digits in chart.
			string practiceZip4=practiceZip.Substring(5);
			practiceZip=practiceZip.Substring(0,5);
			string country="US";
			//if(CultureInfo.CurrentCulture.Name.Length>=2) {
			//  country=CultureInfo.CurrentCulture.Name.Substring(CultureInfo.CurrentCulture.Name.Length-2);
			//}
			ncScript.Account=new AccountTypeRx();
			//Each LicensedPrescriberID must be unique within an account. Since we send ProvNum for LicensedPrescriberID, each OD database must have a unique AccountID.
			ncScript.Account.ID=PrefC.GetString(PrefName.NewCropAccountId);//Customer account number then a dash then a random alpha-numeric string of 3 characters.
			ncScript.Account.accountName=practiceTitle;
			ncScript.Account.siteID="1";//Always send 1.  For each AccountID/SiteID pair, a separate database will be created in NewCrop.
			ncScript.Account.AccountAddress=new AddressType();
			ncScript.Account.AccountAddress.address1=practiceAddress;
			ncScript.Account.AccountAddress.address2=practiceAddress2;
			ncScript.Account.AccountAddress.city=practiceCity;
			ncScript.Account.AccountAddress.state=practiceState;
			ncScript.Account.AccountAddress.zip=practiceZip;
			ncScript.Account.AccountAddress.zip4=practiceZip4;
			ncScript.Account.AccountAddress.country=country;
			ncScript.Account.accountPrimaryPhoneNumber=practicePhone;
			ncScript.Account.accountPrimaryFaxNumber=practiceFax;
			ncScript.Location=new LocationType();
			if(PrefC.GetBool(PrefName.EasyNoClinics) || pat.ClinicNum==0) { //No clinics.
				ncScript.Location.ID="0";
				ncScript.Location.locationName=practiceTitle;
				ncScript.Location.LocationAddress=new AddressType();
				ncScript.Location.LocationAddress.address1=practiceAddress;
				ncScript.Location.LocationAddress.address2=practiceAddress2;
				ncScript.Location.LocationAddress.city=practiceCity;
				ncScript.Location.LocationAddress.state=practiceState;
				ncScript.Location.LocationAddress.zip=practiceZip;
				ncScript.Location.LocationAddress.zip4=practiceZip4;
				ncScript.Location.LocationAddress.country=country;
				ncScript.Location.primaryPhoneNumber=practicePhone;
				ncScript.Location.primaryFaxNumber=practiceFax;
				ncScript.Location.pharmacyContactNumber=practicePhone;
			}
			else { //Using clinics.
				Clinic clinic=null;
				//if(pat.ClinicNum==0) {
				//	clinic=Clinics.List[0];//Use the default clinic. There must be at least one, validated in chart before this point.
				//}
				//else {
				clinic=Clinics.GetClinic(pat.ClinicNum);
				//}
				ncScript.Location.ID=clinic.ClinicNum.ToString();
				ncScript.Location.locationName=clinic.Description;
				ncScript.Location.LocationAddress=new AddressType();
				ncScript.Location.LocationAddress.address1=clinic.Address;
				ncScript.Location.LocationAddress.address2=clinic.Address2;
				ncScript.Location.LocationAddress.city=clinic.City;
				ncScript.Location.LocationAddress.state=clinic.State;
				string clinicZip=Regex.Replace(clinic.Zip,"[^0-9]*","");//Zip with all non-numeric characters removed. Validated to be 9 digits in chart.
				string clinicZip4=clinicZip.Substring(5);
				clinicZip=clinicZip.Substring(0,5);
				ncScript.Location.LocationAddress.zip=clinicZip;
				ncScript.Location.LocationAddress.zip4=clinicZip4;
				ncScript.Location.LocationAddress.country=country;
				ncScript.Location.primaryPhoneNumber=clinic.Phone;
				ncScript.Location.primaryFaxNumber=clinic.Fax;
				ncScript.Location.pharmacyContactNumber=clinic.Phone;
			}
			ncScript.LicensedPrescriber=new LicensedPrescriberType();
			ncScript.LicensedPrescriber.ID=prov.ProvNum.ToString();
			//UPIN is obsolete
			ncScript.LicensedPrescriber.LicensedPrescriberName=new PersonNameType();
			ncScript.LicensedPrescriber.LicensedPrescriberName.last=prov.LName;
			ncScript.LicensedPrescriber.LicensedPrescriberName.first=prov.FName;
			ncScript.LicensedPrescriber.LicensedPrescriberName.middle=prov.MI;
			ncScript.LicensedPrescriber.dea=prov.DEANum;
			ncScript.LicensedPrescriber.licenseState=prov.StateWhereLicensed;
			ncScript.LicensedPrescriber.licenseNumber=prov.StateLicense;
			if(emp!=null) {
				ncScript.Staff=new StaffType();
				ncScript.Staff.ID=emp.EmployeeNum.ToString();
				ncScript.Staff.StaffName=new PersonNameType();
				ncScript.Staff.StaffName.first=emp.FName;
				ncScript.Staff.StaffName.last=emp.LName;
				ncScript.Staff.StaffName.middle=emp.MiddleI;
			}
			ncScript.Patient=new PatientType();
			ncScript.Patient.ID=pat.PatNum.ToString();
			ncScript.Patient.PatientName=new PersonNameType();
			ncScript.Patient.PatientName.last=pat.LName;
			ncScript.Patient.PatientName.first=pat.FName;
			ncScript.Patient.PatientName.middle=pat.MiddleI;
			ncScript.Patient.medicalRecordNumber=pat.PatNum.ToString();
			ncScript.Patient.socialSecurityNumber=Regex.Replace(pat.SSN,"[^0-9]*","");//Removes all non-numerical characters.
			ncScript.Patient.PatientAddress=new AddressOptionalType();
			ncScript.Patient.PatientAddress.address1=pat.Address;
			ncScript.Patient.PatientAddress.address2=pat.Address2;
			ncScript.Patient.PatientAddress.city=pat.City;
			ncScript.Patient.PatientAddress.state=pat.State;
			ncScript.Patient.PatientAddress.zip=pat.Zip;
			ncScript.Patient.PatientAddress.country=country;
			ncScript.Patient.PatientContact=new ContactType();
			ncScript.Patient.PatientContact.homeTelephone=pat.HmPhone;
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
