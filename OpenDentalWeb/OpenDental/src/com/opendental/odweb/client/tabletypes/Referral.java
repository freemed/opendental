package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Referral {
		/** Primary key. */
		public int ReferralNum;
		/** Last name. */
		public String LName;
		/** First name. */
		public String FName;
		/** Middle name or initial. */
		public String MName;
		/** SSN or TIN, no punctuation.  For Canada, this holds the referring provider CDA num for claims. */
		public String SSN;
		/** Specificies if SSN is real SSN. */
		public boolean UsingTIN;
		/** Enum:DentalSpecialty -1 is allowed to indicate no specialty.  -1 is what all non-professionals will be set to. */
		public DentalSpecialty Specialty;
		/** State */
		public String ST;
		/** Primary phone, restrictive, must only be 10 digits and only numbers. */
		public String Telephone;
		/** . */
		public String Address;
		/** . */
		public String Address2;
		/** . */
		public String City;
		/** . */
		public String Zip;
		/** Holds important info about the referral. */
		public String Note;
		/** Additional phone no restrictions */
		public String Phone2;
		/** Can't delete a referral, but can hide if not needed any more. */
		public boolean IsHidden;
		/** Set to true for referralls such as Yellow Pages. */
		public boolean NotPerson;
		/** i.e. DMD or DDS */
		public String Title;
		/** . */
		public String EMail;
		/** FK to patient.PatNum for referrals that are patients. */
		public int PatNum;
		/** NPI for the referral */
		public String NationalProvID;
		/** FK to sheetdef.SheetDefNum.  Referral slips can be set for individual referral sources.  If zero, then the default internal referral slip will be used instead of a custom referral slip. */
		public int Slip;
		/** True if another dentist or physician.  Cannot be a patient. */
		public boolean IsDoctor;

		/** Deep copy of object. */
		public Referral Copy() {
			Referral referral=new Referral();
			referral.ReferralNum=this.ReferralNum;
			referral.LName=this.LName;
			referral.FName=this.FName;
			referral.MName=this.MName;
			referral.SSN=this.SSN;
			referral.UsingTIN=this.UsingTIN;
			referral.Specialty=this.Specialty;
			referral.ST=this.ST;
			referral.Telephone=this.Telephone;
			referral.Address=this.Address;
			referral.Address2=this.Address2;
			referral.City=this.City;
			referral.Zip=this.Zip;
			referral.Note=this.Note;
			referral.Phone2=this.Phone2;
			referral.IsHidden=this.IsHidden;
			referral.NotPerson=this.NotPerson;
			referral.Title=this.Title;
			referral.EMail=this.EMail;
			referral.PatNum=this.PatNum;
			referral.NationalProvID=this.NationalProvID;
			referral.Slip=this.Slip;
			referral.IsDoctor=this.IsDoctor;
			return referral;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Referral>");
			sb.append("<ReferralNum>").append(ReferralNum).append("</ReferralNum>");
			sb.append("<LName>").append(Serializing.EscapeForXml(LName)).append("</LName>");
			sb.append("<FName>").append(Serializing.EscapeForXml(FName)).append("</FName>");
			sb.append("<MName>").append(Serializing.EscapeForXml(MName)).append("</MName>");
			sb.append("<SSN>").append(Serializing.EscapeForXml(SSN)).append("</SSN>");
			sb.append("<UsingTIN>").append((UsingTIN)?1:0).append("</UsingTIN>");
			sb.append("<Specialty>").append(Specialty.ordinal()).append("</Specialty>");
			sb.append("<ST>").append(Serializing.EscapeForXml(ST)).append("</ST>");
			sb.append("<Telephone>").append(Serializing.EscapeForXml(Telephone)).append("</Telephone>");
			sb.append("<Address>").append(Serializing.EscapeForXml(Address)).append("</Address>");
			sb.append("<Address2>").append(Serializing.EscapeForXml(Address2)).append("</Address2>");
			sb.append("<City>").append(Serializing.EscapeForXml(City)).append("</City>");
			sb.append("<Zip>").append(Serializing.EscapeForXml(Zip)).append("</Zip>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<Phone2>").append(Serializing.EscapeForXml(Phone2)).append("</Phone2>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<NotPerson>").append((NotPerson)?1:0).append("</NotPerson>");
			sb.append("<Title>").append(Serializing.EscapeForXml(Title)).append("</Title>");
			sb.append("<EMail>").append(Serializing.EscapeForXml(EMail)).append("</EMail>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<NationalProvID>").append(Serializing.EscapeForXml(NationalProvID)).append("</NationalProvID>");
			sb.append("<Slip>").append(Slip).append("</Slip>");
			sb.append("<IsDoctor>").append((IsDoctor)?1:0).append("</IsDoctor>");
			sb.append("</Referral>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"ReferralNum")!=null) {
					ReferralNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ReferralNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"LName")!=null) {
					LName=Serializing.GetXmlNodeValue(doc,"LName");
				}
				if(Serializing.GetXmlNodeValue(doc,"FName")!=null) {
					FName=Serializing.GetXmlNodeValue(doc,"FName");
				}
				if(Serializing.GetXmlNodeValue(doc,"MName")!=null) {
					MName=Serializing.GetXmlNodeValue(doc,"MName");
				}
				if(Serializing.GetXmlNodeValue(doc,"SSN")!=null) {
					SSN=Serializing.GetXmlNodeValue(doc,"SSN");
				}
				if(Serializing.GetXmlNodeValue(doc,"UsingTIN")!=null) {
					UsingTIN=(Serializing.GetXmlNodeValue(doc,"UsingTIN")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"Specialty")!=null) {
					Specialty=DentalSpecialty.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Specialty"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"ST")!=null) {
					ST=Serializing.GetXmlNodeValue(doc,"ST");
				}
				if(Serializing.GetXmlNodeValue(doc,"Telephone")!=null) {
					Telephone=Serializing.GetXmlNodeValue(doc,"Telephone");
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
				if(Serializing.GetXmlNodeValue(doc,"Zip")!=null) {
					Zip=Serializing.GetXmlNodeValue(doc,"Zip");
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
				if(Serializing.GetXmlNodeValue(doc,"Phone2")!=null) {
					Phone2=Serializing.GetXmlNodeValue(doc,"Phone2");
				}
				if(Serializing.GetXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.GetXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"NotPerson")!=null) {
					NotPerson=(Serializing.GetXmlNodeValue(doc,"NotPerson")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"Title")!=null) {
					Title=Serializing.GetXmlNodeValue(doc,"Title");
				}
				if(Serializing.GetXmlNodeValue(doc,"EMail")!=null) {
					EMail=Serializing.GetXmlNodeValue(doc,"EMail");
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"NationalProvID")!=null) {
					NationalProvID=Serializing.GetXmlNodeValue(doc,"NationalProvID");
				}
				if(Serializing.GetXmlNodeValue(doc,"Slip")!=null) {
					Slip=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Slip"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsDoctor")!=null) {
					IsDoctor=(Serializing.GetXmlNodeValue(doc,"IsDoctor")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum DentalSpecialty {
			/** 0 */
			General,
			/** 1 */
			Hygienist,
			/** 2 */
			Endodontics,
			/** 3 */
			Pediatric,
			/** 4 */
			Perio,
			/** 5 */
			Prosth,
			/** 6 */
			Ortho,
			/** 7 */
			Denturist,
			/** 8 */
			Surgery,
			/** 9 */
			Assistant,
			/** 10 */
			LabTech,
			/** 11 */
			Pathology,
			/** 12 */
			PublicHealth,
			/** 13 */
			Radiology
		}


}
