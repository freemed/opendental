using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {
	public class ErxXml {

		public static string BuildClickThroughXml() {
			NCScript ncScript = new NCScript();

			ncScript.Credentials = new CredentialsType();
			ncScript.Credentials.partnerName = "demo";
			ncScript.Credentials.name = "demo";
			ncScript.Credentials.password = "demo";
			ncScript.Credentials.productName = "SuperDuperSoftware";
			ncScript.Credentials.productVersion = "V5.3";

			ncScript.UserRole = new UserRoleType();
			ncScript.UserRole.user = UserType.LicensedPrescriber;
			ncScript.UserRole.role = RoleType.doctor;

			ncScript.Destination = new DestinationType();
			ncScript.Destination.requestedPage = RequestedPageType.compose;

			ncScript.Account = new AccountTypeRx();
			ncScript.Account.ID = "demo";
			ncScript.Account.accountName = "demo";
			ncScript.Account.siteID = "demo";
			ncScript.Account.AccountAddress = new AddressType();
			ncScript.Account.AccountAddress.address1 = "232323 Test";
			ncScript.Account.AccountAddress.address2 = "Suite 240";
			ncScript.Account.AccountAddress.city = "Boston";
			ncScript.Account.AccountAddress.state = "MA";
			ncScript.Account.AccountAddress.zip = "10409";
			ncScript.Account.AccountAddress.zip4 = "1234";
			ncScript.Account.AccountAddress.country = "US";
			ncScript.Account.accountPrimaryPhoneNumber = "5555551212";
			ncScript.Account.accountPrimaryFaxNumber = "5555551313";

			ncScript.Location = new LocationType();
			ncScript.Location.ID = "DEMOLOC1";
			ncScript.Location.locationName = "Family Practice Center of Boston";
			ncScript.Location.LocationAddress = new AddressType();
			ncScript.Location.LocationAddress.address1 = "232323 Test";
			ncScript.Location.LocationAddress.address2 = "Suite 240";
			ncScript.Location.LocationAddress.city = "Boston";
			ncScript.Location.LocationAddress.state = "MA";
			ncScript.Location.LocationAddress.zip = "10409";
			ncScript.Location.LocationAddress.zip4 = "1234";
			ncScript.Location.LocationAddress.country = "US";
			ncScript.Location.primaryPhoneNumber = "5555551212";
			ncScript.Location.primaryFaxNumber = "5555551213";
			ncScript.Location.pharmacyContactNumber = "5555551212";

			ncScript.LicensedPrescriber = new LicensedPrescriberType();
			ncScript.LicensedPrescriber.ID = "DEMOLP1";
			ncScript.LicensedPrescriber.LicensedPrescriberName = new PersonNameType();
			ncScript.LicensedPrescriber.LicensedPrescriberName.last = "Smith";
			ncScript.LicensedPrescriber.LicensedPrescriberName.first = "Doctor";
			ncScript.LicensedPrescriber.LicensedPrescriberName.middle = "J";
			ncScript.LicensedPrescriber.dea = "DEA20";
			ncScript.LicensedPrescriber.upin = "12345678";
			ncScript.LicensedPrescriber.licenseState = "TX";
			ncScript.LicensedPrescriber.licenseNumber = "12345678";

			ncScript.Patient = new PatientType();
			ncScript.Patient.ID = "DEMOPT1";
			ncScript.Patient.PatientName = new PersonNameType();
			ncScript.Patient.PatientName.last = "Wilson";
			ncScript.Patient.PatientName.first = "Patient";
			ncScript.Patient.PatientName.middle = "J";
			ncScript.Patient.medicalRecordNumber = "123456";
			ncScript.Patient.socialSecurityNumber = "555443333";
			ncScript.Patient.PatientAddress = new AddressOptionalType();
			ncScript.Patient.PatientAddress.address1 = "23223 Test";
			ncScript.Patient.PatientAddress.address2 = "Suite 240";
			ncScript.Patient.PatientAddress.city = "Boston";
			ncScript.Patient.PatientAddress.state = "MA";
			ncScript.Patient.PatientAddress.zip = "10455";
			ncScript.Patient.PatientAddress.country = "US";
			ncScript.Patient.PatientContact = new ContactType();
			ncScript.Patient.PatientContact.homeTelephone = "1234567890";
			ncScript.Patient.PatientCharacteristics = new PatientCharacteristicsType();
			//DOB must be in CCYYMMDD format
			ncScript.Patient.PatientCharacteristics.dob = "19800115";
			ncScript.Patient.PatientCharacteristics.gender = GenderType.M;
			ncScript.Patient.PatientCharacteristics.genderSpecified = true;

			//Serialize the ncScript object into XML for passing to newCrop!
			System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
			System.Xml.Serialization.XmlSerializer xmlSerializer =
            new System.Xml.Serialization.XmlSerializer(typeof(NCScript));

			xmlSerializer.Serialize(memoryStream,ncScript);
			byte[] memoryStreamInBytes = memoryStream.ToArray();
			return System.Text.Encoding.UTF8.GetString(memoryStreamInBytes,0,memoryStreamInBytes.Length);
		} // End method BuildClickThroughXml


	}
}
