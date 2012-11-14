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
				ReferralNum=Integer.valueOf(doc.getElementsByTagName("ReferralNum").item(0).getFirstChild().getNodeValue());
				LName=doc.getElementsByTagName("LName").item(0).getFirstChild().getNodeValue();
				FName=doc.getElementsByTagName("FName").item(0).getFirstChild().getNodeValue();
				MName=doc.getElementsByTagName("MName").item(0).getFirstChild().getNodeValue();
				SSN=doc.getElementsByTagName("SSN").item(0).getFirstChild().getNodeValue();
				UsingTIN=(doc.getElementsByTagName("UsingTIN").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				Specialty=DentalSpecialty.values()[Integer.valueOf(doc.getElementsByTagName("Specialty").item(0).getFirstChild().getNodeValue())];
				ST=doc.getElementsByTagName("ST").item(0).getFirstChild().getNodeValue();
				Telephone=doc.getElementsByTagName("Telephone").item(0).getFirstChild().getNodeValue();
				Address=doc.getElementsByTagName("Address").item(0).getFirstChild().getNodeValue();
				Address2=doc.getElementsByTagName("Address2").item(0).getFirstChild().getNodeValue();
				City=doc.getElementsByTagName("City").item(0).getFirstChild().getNodeValue();
				Zip=doc.getElementsByTagName("Zip").item(0).getFirstChild().getNodeValue();
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				Phone2=doc.getElementsByTagName("Phone2").item(0).getFirstChild().getNodeValue();
				IsHidden=(doc.getElementsByTagName("IsHidden").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				NotPerson=(doc.getElementsByTagName("NotPerson").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				Title=doc.getElementsByTagName("Title").item(0).getFirstChild().getNodeValue();
				EMail=doc.getElementsByTagName("EMail").item(0).getFirstChild().getNodeValue();
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				NationalProvID=doc.getElementsByTagName("NationalProvID").item(0).getFirstChild().getNodeValue();
				Slip=Integer.valueOf(doc.getElementsByTagName("Slip").item(0).getFirstChild().getNodeValue());
				IsDoctor=(doc.getElementsByTagName("IsDoctor").item(0).getFirstChild().getNodeValue()=="0")?false:true;
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
