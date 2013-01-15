package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class Provider {
		/** Primary key. */
		public int ProvNum;
		/** Abbreviation.  There was a limit of 5 char before version 5.4.  The new limit is 255 char.  This will allow more elegant solutions to various problems.  Providers will no longer be referred to by FName and LName.  Abbr is used as a human readable primary key. */
		public String Abbr;
		/** Order that provider will show in lists. Was 1-based, now 0-based. */
		public int ItemOrder;
		/** Last name. */
		public String LName;
		/** First name. */
		public String FName;
		/** Middle inital or name. */
		public String MI;
		/** eg. DMD or DDS. Was 'title' in previous versions. */
		public String Suffix;
		/** FK to feesched.FeeSchedNum. */
		public int FeeSched;
		/** Enum:DentalSpecialty */
		public DentalSpecialty Specialty;
		/** or TIN.  No punctuation */
		public String SSN;
		/** can include punctuation */
		public String StateLicense;
		/** . */
		public String DEANum;
		/** True if hygienist. */
		public boolean IsSecondary;
		/** Color that shows in appointments */
		public int ProvColor;
		/** If true, provider will not show on any lists */
		public boolean IsHidden;
		/** True if the SSN field is actually a Tax ID Num */
		public boolean UsingTIN;
		/** No longer used since each state assigns a different ID.  Use the providerident instead which allows you to assign a different BCBS ID for each Payor ID. */
		public String BlueCrossID;
		/** Signature on file. */
		public boolean SigOnFile;
		/** . */
		public String MedicaidID;
		/** Color that shows in appointments as outline when highlighted. */
		public int OutlineColor;
		/** FK to schoolclass.SchoolClassNum Used in dental schools.  Each student is a provider.  This keeps track of which class they are in. */
		public int SchoolClassNum;
		/** US NPI, and Canadian CDA provider number. */
		public String NationalProvID;
		/** Canadian field required for e-claims.  Assigned by CDA.  It's OK to have multiple providers with the same OfficeNum.  Max length should be 4. */
		public String CanadianOfficeNum;
		/** . */
		public Date DateTStamp;
		/**  FK to ??. Field used to set the Anesthesia Provider type. Used to filter the provider dropdowns on FormAnestheticRecord */
		public int AnesthProvType;
		/** If none of the supplied taxonomies works.  This will show on claims. */
		public String TaxonomyCodeOverride;
		/** For Canada. Set to true if CDA Net provider. */
		public boolean IsCDAnet;
		/** UserID from eCW so that prov abbreviations can be human readable. */
		public String EcwID;
		/** Allows using ehr module for this provider.  Tied to provider fname and lname. */
		public String EhrKey;
		/** Provider medical State ID. */
		public String StateRxID;
		/** True if the provider key for this provider is set up for report access. */
		public boolean EhrHasReportAccess;
		/** Default is false because most providers are persons.  But some dummy providers used for practices or billing entities are not persons.  This is needed on 837s. */
		public boolean IsNotPerson;
		/** The state abbreviation where the state license number in the StateLicense field is legally registered. */
		public String StateWhereLicensed;

		/** Deep copy of object. */
		public Provider deepCopy() {
			Provider provider=new Provider();
			provider.ProvNum=this.ProvNum;
			provider.Abbr=this.Abbr;
			provider.ItemOrder=this.ItemOrder;
			provider.LName=this.LName;
			provider.FName=this.FName;
			provider.MI=this.MI;
			provider.Suffix=this.Suffix;
			provider.FeeSched=this.FeeSched;
			provider.Specialty=this.Specialty;
			provider.SSN=this.SSN;
			provider.StateLicense=this.StateLicense;
			provider.DEANum=this.DEANum;
			provider.IsSecondary=this.IsSecondary;
			provider.ProvColor=this.ProvColor;
			provider.IsHidden=this.IsHidden;
			provider.UsingTIN=this.UsingTIN;
			provider.BlueCrossID=this.BlueCrossID;
			provider.SigOnFile=this.SigOnFile;
			provider.MedicaidID=this.MedicaidID;
			provider.OutlineColor=this.OutlineColor;
			provider.SchoolClassNum=this.SchoolClassNum;
			provider.NationalProvID=this.NationalProvID;
			provider.CanadianOfficeNum=this.CanadianOfficeNum;
			provider.DateTStamp=this.DateTStamp;
			provider.AnesthProvType=this.AnesthProvType;
			provider.TaxonomyCodeOverride=this.TaxonomyCodeOverride;
			provider.IsCDAnet=this.IsCDAnet;
			provider.EcwID=this.EcwID;
			provider.EhrKey=this.EhrKey;
			provider.StateRxID=this.StateRxID;
			provider.EhrHasReportAccess=this.EhrHasReportAccess;
			provider.IsNotPerson=this.IsNotPerson;
			provider.StateWhereLicensed=this.StateWhereLicensed;
			return provider;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Provider>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<Abbr>").append(Serializing.escapeForXml(Abbr)).append("</Abbr>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<LName>").append(Serializing.escapeForXml(LName)).append("</LName>");
			sb.append("<FName>").append(Serializing.escapeForXml(FName)).append("</FName>");
			sb.append("<MI>").append(Serializing.escapeForXml(MI)).append("</MI>");
			sb.append("<Suffix>").append(Serializing.escapeForXml(Suffix)).append("</Suffix>");
			sb.append("<FeeSched>").append(FeeSched).append("</FeeSched>");
			sb.append("<Specialty>").append(Specialty.ordinal()).append("</Specialty>");
			sb.append("<SSN>").append(Serializing.escapeForXml(SSN)).append("</SSN>");
			sb.append("<StateLicense>").append(Serializing.escapeForXml(StateLicense)).append("</StateLicense>");
			sb.append("<DEANum>").append(Serializing.escapeForXml(DEANum)).append("</DEANum>");
			sb.append("<IsSecondary>").append((IsSecondary)?1:0).append("</IsSecondary>");
			sb.append("<ProvColor>").append(ProvColor).append("</ProvColor>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<UsingTIN>").append((UsingTIN)?1:0).append("</UsingTIN>");
			sb.append("<BlueCrossID>").append(Serializing.escapeForXml(BlueCrossID)).append("</BlueCrossID>");
			sb.append("<SigOnFile>").append((SigOnFile)?1:0).append("</SigOnFile>");
			sb.append("<MedicaidID>").append(Serializing.escapeForXml(MedicaidID)).append("</MedicaidID>");
			sb.append("<OutlineColor>").append(OutlineColor).append("</OutlineColor>");
			sb.append("<SchoolClassNum>").append(SchoolClassNum).append("</SchoolClassNum>");
			sb.append("<NationalProvID>").append(Serializing.escapeForXml(NationalProvID)).append("</NationalProvID>");
			sb.append("<CanadianOfficeNum>").append(Serializing.escapeForXml(CanadianOfficeNum)).append("</CanadianOfficeNum>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<AnesthProvType>").append(AnesthProvType).append("</AnesthProvType>");
			sb.append("<TaxonomyCodeOverride>").append(Serializing.escapeForXml(TaxonomyCodeOverride)).append("</TaxonomyCodeOverride>");
			sb.append("<IsCDAnet>").append((IsCDAnet)?1:0).append("</IsCDAnet>");
			sb.append("<EcwID>").append(Serializing.escapeForXml(EcwID)).append("</EcwID>");
			sb.append("<EhrKey>").append(Serializing.escapeForXml(EhrKey)).append("</EhrKey>");
			sb.append("<StateRxID>").append(Serializing.escapeForXml(StateRxID)).append("</StateRxID>");
			sb.append("<EhrHasReportAccess>").append((EhrHasReportAccess)?1:0).append("</EhrHasReportAccess>");
			sb.append("<IsNotPerson>").append((IsNotPerson)?1:0).append("</IsNotPerson>");
			sb.append("<StateWhereLicensed>").append(Serializing.escapeForXml(StateWhereLicensed)).append("</StateWhereLicensed>");
			sb.append("</Provider>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Abbr")!=null) {
					Abbr=Serializing.getXmlNodeValue(doc,"Abbr");
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"LName")!=null) {
					LName=Serializing.getXmlNodeValue(doc,"LName");
				}
				if(Serializing.getXmlNodeValue(doc,"FName")!=null) {
					FName=Serializing.getXmlNodeValue(doc,"FName");
				}
				if(Serializing.getXmlNodeValue(doc,"MI")!=null) {
					MI=Serializing.getXmlNodeValue(doc,"MI");
				}
				if(Serializing.getXmlNodeValue(doc,"Suffix")!=null) {
					Suffix=Serializing.getXmlNodeValue(doc,"Suffix");
				}
				if(Serializing.getXmlNodeValue(doc,"FeeSched")!=null) {
					FeeSched=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FeeSched"));
				}
				if(Serializing.getXmlNodeValue(doc,"Specialty")!=null) {
					Specialty=DentalSpecialty.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"Specialty"))];
				}
				if(Serializing.getXmlNodeValue(doc,"SSN")!=null) {
					SSN=Serializing.getXmlNodeValue(doc,"SSN");
				}
				if(Serializing.getXmlNodeValue(doc,"StateLicense")!=null) {
					StateLicense=Serializing.getXmlNodeValue(doc,"StateLicense");
				}
				if(Serializing.getXmlNodeValue(doc,"DEANum")!=null) {
					DEANum=Serializing.getXmlNodeValue(doc,"DEANum");
				}
				if(Serializing.getXmlNodeValue(doc,"IsSecondary")!=null) {
					IsSecondary=(Serializing.getXmlNodeValue(doc,"IsSecondary")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"ProvColor")!=null) {
					ProvColor=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvColor"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.getXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"UsingTIN")!=null) {
					UsingTIN=(Serializing.getXmlNodeValue(doc,"UsingTIN")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"BlueCrossID")!=null) {
					BlueCrossID=Serializing.getXmlNodeValue(doc,"BlueCrossID");
				}
				if(Serializing.getXmlNodeValue(doc,"SigOnFile")!=null) {
					SigOnFile=(Serializing.getXmlNodeValue(doc,"SigOnFile")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"MedicaidID")!=null) {
					MedicaidID=Serializing.getXmlNodeValue(doc,"MedicaidID");
				}
				if(Serializing.getXmlNodeValue(doc,"OutlineColor")!=null) {
					OutlineColor=Integer.valueOf(Serializing.getXmlNodeValue(doc,"OutlineColor"));
				}
				if(Serializing.getXmlNodeValue(doc,"SchoolClassNum")!=null) {
					SchoolClassNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SchoolClassNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"NationalProvID")!=null) {
					NationalProvID=Serializing.getXmlNodeValue(doc,"NationalProvID");
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianOfficeNum")!=null) {
					CanadianOfficeNum=Serializing.getXmlNodeValue(doc,"CanadianOfficeNum");
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"AnesthProvType")!=null) {
					AnesthProvType=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AnesthProvType"));
				}
				if(Serializing.getXmlNodeValue(doc,"TaxonomyCodeOverride")!=null) {
					TaxonomyCodeOverride=Serializing.getXmlNodeValue(doc,"TaxonomyCodeOverride");
				}
				if(Serializing.getXmlNodeValue(doc,"IsCDAnet")!=null) {
					IsCDAnet=(Serializing.getXmlNodeValue(doc,"IsCDAnet")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"EcwID")!=null) {
					EcwID=Serializing.getXmlNodeValue(doc,"EcwID");
				}
				if(Serializing.getXmlNodeValue(doc,"EhrKey")!=null) {
					EhrKey=Serializing.getXmlNodeValue(doc,"EhrKey");
				}
				if(Serializing.getXmlNodeValue(doc,"StateRxID")!=null) {
					StateRxID=Serializing.getXmlNodeValue(doc,"StateRxID");
				}
				if(Serializing.getXmlNodeValue(doc,"EhrHasReportAccess")!=null) {
					EhrHasReportAccess=(Serializing.getXmlNodeValue(doc,"EhrHasReportAccess")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"IsNotPerson")!=null) {
					IsNotPerson=(Serializing.getXmlNodeValue(doc,"IsNotPerson")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"StateWhereLicensed")!=null) {
					StateWhereLicensed=Serializing.getXmlNodeValue(doc,"StateWhereLicensed");
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
