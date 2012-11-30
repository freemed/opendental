package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class Clinic {
		/** Primary key.  Used in patient,payment,claimpayment,appointment,procedurelog, etc. */
		public int ClinicNum;
		/** . */
		public String Description;
		/** . */
		public String Address;
		/** Second line of address. */
		public String Address2;
		/** . */
		public String City;
		/** 2 char in the US. */
		public String State;
		/** . */
		public String Zip;
		/** Does not include any punctuation.  Exactly 10 digits or blank in USA and Canada. */
		public String Phone;
		/** The account number for deposits. */
		public String BankNumber;
		/** Enum:PlaceOfService Usually 0 unless a mobile clinic for instance. */
		public PlaceOfService DefaultPlaceService;
		/** FK to provider.ProvNum.  0=Default practice provider, -1=Treating provider. */
		public int InsBillingProv;
		/** Does not include any punctuation.  Exactly 10 digits or empty in USA and Canada. */
		public String Fax;

		/** Deep copy of object. */
		public Clinic Copy() {
			Clinic clinic=new Clinic();
			clinic.ClinicNum=this.ClinicNum;
			clinic.Description=this.Description;
			clinic.Address=this.Address;
			clinic.Address2=this.Address2;
			clinic.City=this.City;
			clinic.State=this.State;
			clinic.Zip=this.Zip;
			clinic.Phone=this.Phone;
			clinic.BankNumber=this.BankNumber;
			clinic.DefaultPlaceService=this.DefaultPlaceService;
			clinic.InsBillingProv=this.InsBillingProv;
			clinic.Fax=this.Fax;
			return clinic;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Clinic>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<Address>").append(Serializing.EscapeForXml(Address)).append("</Address>");
			sb.append("<Address2>").append(Serializing.EscapeForXml(Address2)).append("</Address2>");
			sb.append("<City>").append(Serializing.EscapeForXml(City)).append("</City>");
			sb.append("<State>").append(Serializing.EscapeForXml(State)).append("</State>");
			sb.append("<Zip>").append(Serializing.EscapeForXml(Zip)).append("</Zip>");
			sb.append("<Phone>").append(Serializing.EscapeForXml(Phone)).append("</Phone>");
			sb.append("<BankNumber>").append(Serializing.EscapeForXml(BankNumber)).append("</BankNumber>");
			sb.append("<DefaultPlaceService>").append(DefaultPlaceService.ordinal()).append("</DefaultPlaceService>");
			sb.append("<InsBillingProv>").append(InsBillingProv).append("</InsBillingProv>");
			sb.append("<Fax>").append(Serializing.EscapeForXml(Fax)).append("</Fax>");
			sb.append("</Clinic>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"Address")!=null) {
					Address=Serializing.GetXmlNodeValue(doc,"Address");
				}
				if(Serializing.GetXmlNodeValue(doc,"Address2")!=null) {
					Address2=Serializing.GetXmlNodeValue(doc,"Address2");
				}
				if(Serializing.GetXmlNodeValue(doc,"City")!=null) {
					City=Serializing.GetXmlNodeValue(doc,"City");
				}
				if(Serializing.GetXmlNodeValue(doc,"State")!=null) {
					State=Serializing.GetXmlNodeValue(doc,"State");
				}
				if(Serializing.GetXmlNodeValue(doc,"Zip")!=null) {
					Zip=Serializing.GetXmlNodeValue(doc,"Zip");
				}
				if(Serializing.GetXmlNodeValue(doc,"Phone")!=null) {
					Phone=Serializing.GetXmlNodeValue(doc,"Phone");
				}
				if(Serializing.GetXmlNodeValue(doc,"BankNumber")!=null) {
					BankNumber=Serializing.GetXmlNodeValue(doc,"BankNumber");
				}
				if(Serializing.GetXmlNodeValue(doc,"DefaultPlaceService")!=null) {
					DefaultPlaceService=PlaceOfService.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DefaultPlaceService"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"InsBillingProv")!=null) {
					InsBillingProv=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"InsBillingProv"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Fax")!=null) {
					Fax=Serializing.GetXmlNodeValue(doc,"Fax");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum PlaceOfService {
			/** 0. CPT code 11 */
			Office,
			/** 1. CPT code 12 */
			PatientsHome,
			/** 2. CPT code 21 */
			InpatHospital,
			/** 3. CPT code 22 */
			OutpatHospital,
			/** 4. CPT code 31 */
			SkilledNursFac,
			/** 5. CPT code 33.  In X12, a similar code AdultLivCareFac 35 is mentioned. */
			CustodialCareFacility,
			/** 6. CPT code ?.  We use 11 for office. */
			OtherLocation,
			/** 7. CPT code 15 */
			MobileUnit,
			/** 8. CPT code 03 */
			School,
			/** 9. CPT code 26 */
			MilitaryTreatFac,
			/** 10. CPT code 50 */
			FederalHealthCenter,
			/** 11. CPT code 71 */
			PublicHealthClinic,
			/** 12. CPT code 72 */
			RuralHealthClinic,
			/** 13. CPT code 23 */
			EmergencyRoomHospital,
			/** 14. CPT code 24 */
			AmbulatorySurgicalCenter
		}


}
