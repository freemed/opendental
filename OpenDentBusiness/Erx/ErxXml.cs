using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentBusiness {
	public class ErxXml {

		public static string BuildClickThroughXml(Provider prov,Employee emp,Patient pat) {
			NCScript ncScript=new NCScript();

			ncScript.Credentials=new CredentialsType();
			ncScript.Credentials.partnerName="OpenDental";
			ncScript.Credentials.name="demo";
			ncScript.Credentials.password="demo";
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
			string practicePhone=PrefC.GetString(PrefName.PracticePhone);//Verified to be 10 digits within the chart.
			string practiceFax=PrefC.GetString(PrefName.PracticeFax);//Verified to be 10 digits within the chart.
			string practiceAddress=PrefC.GetString(PrefName.PracticeAddress);
			string practiceAddress2=PrefC.GetString(PrefName.PracticeAddress2);
			string practiceCity=PrefC.GetString(PrefName.PracticeCity);
			string practiceState=PrefC.GetString(PrefName.PracticeST);
			string practiceZip=Regex.Replace(PrefC.GetString(PrefName.PracticeZip),"[^0-9]*","");//Zip with all non-numeric characters removed.
			string practiceZip4=practiceZip4=practiceZip.Substring(5);
			practiceZip=practiceZip.Substring(0,5);
			string country="US";
			if(CultureInfo.CurrentCulture.Name.Length>=2) {
				country=CultureInfo.CurrentCulture.Name.Substring(CultureInfo.CurrentCulture.Name.Length-2);
			}
			ncScript.Account=new AccountTypeRx();
			ncScript.Account.ID="1";
			ncScript.Account.accountName=practiceTitle;
			ncScript.Account.siteID="1";
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
			ncScript.Location.ID="1";
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

			ncScript.LicensedPrescriber=new LicensedPrescriberType();
			ncScript.LicensedPrescriber.ID=prov.ProvNum.ToString();
			//UPIN is obsolete
			ncScript.LicensedPrescriber.LicensedPrescriberName=new PersonNameType();
			ncScript.LicensedPrescriber.LicensedPrescriberName.last=prov.LName;
			ncScript.LicensedPrescriber.LicensedPrescriberName.first=prov.FName;
			ncScript.LicensedPrescriber.LicensedPrescriberName.middle=prov.MI;
			ncScript.LicensedPrescriber.dea=prov.DEANum;
			ncScript.LicensedPrescriber.licenseState=prov.StateLicensed;
			ncScript.LicensedPrescriber.licenseNumber=prov.StateLicense;

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
			System.IO.MemoryStream memoryStream=new System.IO.MemoryStream();
			System.Xml.Serialization.XmlSerializer xmlSerializer=new System.Xml.Serialization.XmlSerializer(typeof(NCScript));
			xmlSerializer.Serialize(memoryStream,ncScript);
			byte[] memoryStreamInBytes=memoryStream.ToArray();
			return System.Text.Encoding.UTF8.GetString(memoryStreamInBytes,0,memoryStreamInBytes.Length);
		}

	}
}
