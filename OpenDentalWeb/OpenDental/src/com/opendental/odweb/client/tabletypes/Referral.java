package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public Referral deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Referral>");
			sb.append("<ReferralNum>").append(ReferralNum).append("</ReferralNum>");
			sb.append("<LName>").append(Serializing.escapeForXml(LName)).append("</LName>");
			sb.append("<FName>").append(Serializing.escapeForXml(FName)).append("</FName>");
			sb.append("<MName>").append(Serializing.escapeForXml(MName)).append("</MName>");
			sb.append("<SSN>").append(Serializing.escapeForXml(SSN)).append("</SSN>");
			sb.append("<UsingTIN>").append((UsingTIN)?1:0).append("</UsingTIN>");
			sb.append("<Specialty>").append(Specialty.ordinal()).append("</Specialty>");
			sb.append("<ST>").append(Serializing.escapeForXml(ST)).append("</ST>");
			sb.append("<Telephone>").append(Serializing.escapeForXml(Telephone)).append("</Telephone>");
			sb.append("<Address>").append(Serializing.escapeForXml(Address)).append("</Address>");
			sb.append("<Address2>").append(Serializing.escapeForXml(Address2)).append("</Address2>");
			sb.append("<City>").append(Serializing.escapeForXml(City)).append("</City>");
			sb.append("<Zip>").append(Serializing.escapeForXml(Zip)).append("</Zip>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<Phone2>").append(Serializing.escapeForXml(Phone2)).append("</Phone2>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<NotPerson>").append((NotPerson)?1:0).append("</NotPerson>");
			sb.append("<Title>").append(Serializing.escapeForXml(Title)).append("</Title>");
			sb.append("<EMail>").append(Serializing.escapeForXml(EMail)).append("</EMail>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<NationalProvID>").append(Serializing.escapeForXml(NationalProvID)).append("</NationalProvID>");
			sb.append("<Slip>").append(Slip).append("</Slip>");
			sb.append("<IsDoctor>").append((IsDoctor)?1:0).append("</IsDoctor>");
			sb.append("</Referral>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ReferralNum")!=null) {
					ReferralNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ReferralNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"LName")!=null) {
					LName=Serializing.getXmlNodeValue(doc,"LName");
				}
				if(Serializing.getXmlNodeValue(doc,"FName")!=null) {
					FName=Serializing.getXmlNodeValue(doc,"FName");
				}
				if(Serializing.getXmlNodeValue(doc,"MName")!=null) {
					MName=Serializing.getXmlNodeValue(doc,"MName");
				}
				if(Serializing.getXmlNodeValue(doc,"SSN")!=null) {
					SSN=Serializing.getXmlNodeValue(doc,"SSN");
				}
				if(Serializing.getXmlNodeValue(doc,"UsingTIN")!=null) {
					UsingTIN=(Serializing.getXmlNodeValue(doc,"UsingTIN")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"Specialty")!=null) {
					Specialty=DentalSpecialty.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"Specialty"))];
				}
				if(Serializing.getXmlNodeValue(doc,"ST")!=null) {
					ST=Serializing.getXmlNodeValue(doc,"ST");
				}
				if(Serializing.getXmlNodeValue(doc,"Telephone")!=null) {
					Telephone=Serializing.getXmlNodeValue(doc,"Telephone");
				}
				if(Serializing.getXmlNodeValue(doc,"Address")!=null) {
					Address=Serializing.getXmlNodeValue(doc,"Address");
				}
				if(Serializing.getXmlNodeValue(doc,"Address2")!=null) {
					Address2=Serializing.getXmlNodeValue(doc,"Address2");
				}
				if(Serializing.getXmlNodeValue(doc,"City")!=null) {
					City=Serializing.getXmlNodeValue(doc,"City");
				}
				if(Serializing.getXmlNodeValue(doc,"Zip")!=null) {
					Zip=Serializing.getXmlNodeValue(doc,"Zip");
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"Phone2")!=null) {
					Phone2=Serializing.getXmlNodeValue(doc,"Phone2");
				}
				if(Serializing.getXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.getXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"NotPerson")!=null) {
					NotPerson=(Serializing.getXmlNodeValue(doc,"NotPerson")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"Title")!=null) {
					Title=Serializing.getXmlNodeValue(doc,"Title");
				}
				if(Serializing.getXmlNodeValue(doc,"EMail")!=null) {
					EMail=Serializing.getXmlNodeValue(doc,"EMail");
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"NationalProvID")!=null) {
					NationalProvID=Serializing.getXmlNodeValue(doc,"NationalProvID");
				}
				if(Serializing.getXmlNodeValue(doc,"Slip")!=null) {
					Slip=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Slip"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsDoctor")!=null) {
					IsDoctor=(Serializing.getXmlNodeValue(doc,"IsDoctor")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Referral: "+e.getMessage());
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
