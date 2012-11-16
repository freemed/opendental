package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

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
		public Provider Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Provider>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<Abbr>").append(Serializing.EscapeForXml(Abbr)).append("</Abbr>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<LName>").append(Serializing.EscapeForXml(LName)).append("</LName>");
			sb.append("<FName>").append(Serializing.EscapeForXml(FName)).append("</FName>");
			sb.append("<MI>").append(Serializing.EscapeForXml(MI)).append("</MI>");
			sb.append("<Suffix>").append(Serializing.EscapeForXml(Suffix)).append("</Suffix>");
			sb.append("<FeeSched>").append(FeeSched).append("</FeeSched>");
			sb.append("<Specialty>").append(Specialty.ordinal()).append("</Specialty>");
			sb.append("<SSN>").append(Serializing.EscapeForXml(SSN)).append("</SSN>");
			sb.append("<StateLicense>").append(Serializing.EscapeForXml(StateLicense)).append("</StateLicense>");
			sb.append("<DEANum>").append(Serializing.EscapeForXml(DEANum)).append("</DEANum>");
			sb.append("<IsSecondary>").append((IsSecondary)?1:0).append("</IsSecondary>");
			sb.append("<ProvColor>").append(ProvColor).append("</ProvColor>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<UsingTIN>").append((UsingTIN)?1:0).append("</UsingTIN>");
			sb.append("<BlueCrossID>").append(Serializing.EscapeForXml(BlueCrossID)).append("</BlueCrossID>");
			sb.append("<SigOnFile>").append((SigOnFile)?1:0).append("</SigOnFile>");
			sb.append("<MedicaidID>").append(Serializing.EscapeForXml(MedicaidID)).append("</MedicaidID>");
			sb.append("<OutlineColor>").append(OutlineColor).append("</OutlineColor>");
			sb.append("<SchoolClassNum>").append(SchoolClassNum).append("</SchoolClassNum>");
			sb.append("<NationalProvID>").append(Serializing.EscapeForXml(NationalProvID)).append("</NationalProvID>");
			sb.append("<CanadianOfficeNum>").append(Serializing.EscapeForXml(CanadianOfficeNum)).append("</CanadianOfficeNum>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<AnesthProvType>").append(AnesthProvType).append("</AnesthProvType>");
			sb.append("<TaxonomyCodeOverride>").append(Serializing.EscapeForXml(TaxonomyCodeOverride)).append("</TaxonomyCodeOverride>");
			sb.append("<IsCDAnet>").append((IsCDAnet)?1:0).append("</IsCDAnet>");
			sb.append("<EcwID>").append(Serializing.EscapeForXml(EcwID)).append("</EcwID>");
			sb.append("<EhrKey>").append(Serializing.EscapeForXml(EhrKey)).append("</EhrKey>");
			sb.append("<StateRxID>").append(Serializing.EscapeForXml(StateRxID)).append("</StateRxID>");
			sb.append("<EhrHasReportAccess>").append((EhrHasReportAccess)?1:0).append("</EhrHasReportAccess>");
			sb.append("<IsNotPerson>").append((IsNotPerson)?1:0).append("</IsNotPerson>");
			sb.append("<StateWhereLicensed>").append(Serializing.EscapeForXml(StateWhereLicensed)).append("</StateWhereLicensed>");
			sb.append("</Provider>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Abbr")!=null) {
					Abbr=Serializing.GetXmlNodeValue(doc,"Abbr");
				}
				if(Serializing.GetXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"LName")!=null) {
					LName=Serializing.GetXmlNodeValue(doc,"LName");
				}
				if(Serializing.GetXmlNodeValue(doc,"FName")!=null) {
					FName=Serializing.GetXmlNodeValue(doc,"FName");
				}
				if(Serializing.GetXmlNodeValue(doc,"MI")!=null) {
					MI=Serializing.GetXmlNodeValue(doc,"MI");
				}
				if(Serializing.GetXmlNodeValue(doc,"Suffix")!=null) {
					Suffix=Serializing.GetXmlNodeValue(doc,"Suffix");
				}
				if(Serializing.GetXmlNodeValue(doc,"FeeSched")!=null) {
					FeeSched=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"FeeSched"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Specialty")!=null) {
					Specialty=DentalSpecialty.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Specialty"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"SSN")!=null) {
					SSN=Serializing.GetXmlNodeValue(doc,"SSN");
				}
				if(Serializing.GetXmlNodeValue(doc,"StateLicense")!=null) {
					StateLicense=Serializing.GetXmlNodeValue(doc,"StateLicense");
				}
				if(Serializing.GetXmlNodeValue(doc,"DEANum")!=null) {
					DEANum=Serializing.GetXmlNodeValue(doc,"DEANum");
				}
				if(Serializing.GetXmlNodeValue(doc,"IsSecondary")!=null) {
					IsSecondary=(Serializing.GetXmlNodeValue(doc,"IsSecondary")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvColor")!=null) {
					ProvColor=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvColor"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.GetXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"UsingTIN")!=null) {
					UsingTIN=(Serializing.GetXmlNodeValue(doc,"UsingTIN")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"BlueCrossID")!=null) {
					BlueCrossID=Serializing.GetXmlNodeValue(doc,"BlueCrossID");
				}
				if(Serializing.GetXmlNodeValue(doc,"SigOnFile")!=null) {
					SigOnFile=(Serializing.GetXmlNodeValue(doc,"SigOnFile")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"MedicaidID")!=null) {
					MedicaidID=Serializing.GetXmlNodeValue(doc,"MedicaidID");
				}
				if(Serializing.GetXmlNodeValue(doc,"OutlineColor")!=null) {
					OutlineColor=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"OutlineColor"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SchoolClassNum")!=null) {
					SchoolClassNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SchoolClassNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"NationalProvID")!=null) {
					NationalProvID=Serializing.GetXmlNodeValue(doc,"NationalProvID");
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianOfficeNum")!=null) {
					CanadianOfficeNum=Serializing.GetXmlNodeValue(doc,"CanadianOfficeNum");
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AnesthProvType")!=null) {
					AnesthProvType=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AnesthProvType"));
				}
				if(Serializing.GetXmlNodeValue(doc,"TaxonomyCodeOverride")!=null) {
					TaxonomyCodeOverride=Serializing.GetXmlNodeValue(doc,"TaxonomyCodeOverride");
				}
				if(Serializing.GetXmlNodeValue(doc,"IsCDAnet")!=null) {
					IsCDAnet=(Serializing.GetXmlNodeValue(doc,"IsCDAnet")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"EcwID")!=null) {
					EcwID=Serializing.GetXmlNodeValue(doc,"EcwID");
				}
				if(Serializing.GetXmlNodeValue(doc,"EhrKey")!=null) {
					EhrKey=Serializing.GetXmlNodeValue(doc,"EhrKey");
				}
				if(Serializing.GetXmlNodeValue(doc,"StateRxID")!=null) {
					StateRxID=Serializing.GetXmlNodeValue(doc,"StateRxID");
				}
				if(Serializing.GetXmlNodeValue(doc,"EhrHasReportAccess")!=null) {
					EhrHasReportAccess=(Serializing.GetXmlNodeValue(doc,"EhrHasReportAccess")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"IsNotPerson")!=null) {
					IsNotPerson=(Serializing.GetXmlNodeValue(doc,"IsNotPerson")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"StateWhereLicensed")!=null) {
					StateWhereLicensed=Serializing.GetXmlNodeValue(doc,"StateWhereLicensed");
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
