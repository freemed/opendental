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
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateTStamp>");
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
				ProvNum=Integer.valueOf(doc.getElementsByTagName("ProvNum").item(0).getFirstChild().getNodeValue());
				Abbr=doc.getElementsByTagName("Abbr").item(0).getFirstChild().getNodeValue();
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
				LName=doc.getElementsByTagName("LName").item(0).getFirstChild().getNodeValue();
				FName=doc.getElementsByTagName("FName").item(0).getFirstChild().getNodeValue();
				MI=doc.getElementsByTagName("MI").item(0).getFirstChild().getNodeValue();
				Suffix=doc.getElementsByTagName("Suffix").item(0).getFirstChild().getNodeValue();
				FeeSched=Integer.valueOf(doc.getElementsByTagName("FeeSched").item(0).getFirstChild().getNodeValue());
				Specialty=DentalSpecialty.values()[Integer.valueOf(doc.getElementsByTagName("Specialty").item(0).getFirstChild().getNodeValue())];
				SSN=doc.getElementsByTagName("SSN").item(0).getFirstChild().getNodeValue();
				StateLicense=doc.getElementsByTagName("StateLicense").item(0).getFirstChild().getNodeValue();
				DEANum=doc.getElementsByTagName("DEANum").item(0).getFirstChild().getNodeValue();
				IsSecondary=(doc.getElementsByTagName("IsSecondary").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				ProvColor=Integer.valueOf(doc.getElementsByTagName("ProvColor").item(0).getFirstChild().getNodeValue());
				IsHidden=(doc.getElementsByTagName("IsHidden").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				UsingTIN=(doc.getElementsByTagName("UsingTIN").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				BlueCrossID=doc.getElementsByTagName("BlueCrossID").item(0).getFirstChild().getNodeValue();
				SigOnFile=(doc.getElementsByTagName("SigOnFile").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				MedicaidID=doc.getElementsByTagName("MedicaidID").item(0).getFirstChild().getNodeValue();
				OutlineColor=Integer.valueOf(doc.getElementsByTagName("OutlineColor").item(0).getFirstChild().getNodeValue());
				SchoolClassNum=Integer.valueOf(doc.getElementsByTagName("SchoolClassNum").item(0).getFirstChild().getNodeValue());
				NationalProvID=doc.getElementsByTagName("NationalProvID").item(0).getFirstChild().getNodeValue();
				CanadianOfficeNum=doc.getElementsByTagName("CanadianOfficeNum").item(0).getFirstChild().getNodeValue();
				DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue());
				AnesthProvType=Integer.valueOf(doc.getElementsByTagName("AnesthProvType").item(0).getFirstChild().getNodeValue());
				TaxonomyCodeOverride=doc.getElementsByTagName("TaxonomyCodeOverride").item(0).getFirstChild().getNodeValue();
				IsCDAnet=(doc.getElementsByTagName("IsCDAnet").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				EcwID=doc.getElementsByTagName("EcwID").item(0).getFirstChild().getNodeValue();
				EhrKey=doc.getElementsByTagName("EhrKey").item(0).getFirstChild().getNodeValue();
				StateRxID=doc.getElementsByTagName("StateRxID").item(0).getFirstChild().getNodeValue();
				EhrHasReportAccess=(doc.getElementsByTagName("EhrHasReportAccess").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				IsNotPerson=(doc.getElementsByTagName("IsNotPerson").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				StateWhereLicensed=doc.getElementsByTagName("StateWhereLicensed").item(0).getFirstChild().getNodeValue();
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
