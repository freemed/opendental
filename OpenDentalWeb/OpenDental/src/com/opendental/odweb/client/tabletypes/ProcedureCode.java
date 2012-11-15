package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class ProcedureCode {
		/** Primary Key.  This happened in version 4.8.7. */
		public int CodeNum;
		/** Was Primary key, but now CodeNum is primary key.  Can hold dental codes, medical codes, custom codes, etc. */
		public String ProcCode;
		/** The main description. */
		public String Descript;
		/** Abbreviated description. */
		public String AbbrDesc;
		/** X's and /'s describe Dr's time and assistant's time in the same increments as the user has set. */
		public String ProcTime;
		/** FK to definition.DefNum.  The category that this code will be found under in the search window.  Has nothing to do with insurance categories. */
		public int ProcCat;
		/** Enum:TreatmentArea  */
		public TreatmentArea TreatArea;
		/** If true, do not usually bill this procedure to insurance. */
		public boolean NoBillIns;
		/** True if Crown,Bridge,Denture, or RPD. Forces user to enter Initial or Replacement and Date. */
		public boolean IsProsth;
		/** The default procedure note to copy when marking complete. */
		public String DefaultNote;
		/** Identifies hygiene procedures so that the correct provider can be selected. */
		public boolean IsHygiene;
		/** No longer used. */
		public int GTypeNum;
		/** For Medicaid.  There may be more later. */
		public String AlternateCode1;
		/** FK to procedurecode.ProcCode.  The actual medical code that is being referenced must be setup first.  Anytime a procedure it added, this medical code will also be added to that procedure.  The user can change it in procedurelog. */
		public String MedicalCode;
		/** Used by some offices even though no user interface built yet.  SalesTaxPercentage has been added to the preference table to store the amount of sales tax to apply as an adjustment attached to a procedurelog entry. */
		public boolean IsTaxed;
		/** Enum:ToothPaintingType  */
		public ToothPaintingType PaintType;
		/** If set to anything but 0, then this will override the graphic color for all procedures of this code, regardless of the status. */
		public int GraphicColor;
		/** When creating treatment plans, this description will be used instead of the technical description. */
		public String LaymanTerm;
		/** Only used in Canada.  Set to true if this procedure code is only used as an adjunct to track the lab fee. */
		public boolean IsCanadianLab;
		/** This is true if this procedure code existed before ADA code distribution changed at version 4.8, false otherwise. */
		public boolean PreExisting;
		/** Support for Base Units for a Code (like anesthesia).  Should normally be zero. */
		public int BaseUnits;
		/** FK to procedurecode.ProcCode.  Used for posterior composites because insurance substitutes the amalgam code when figuring the coverage. */
		public String SubstitutionCode;
		/** Enum:SubstitutionCondition Used so that posterior composites only substitute if tooth is molar.  Ins usually pays for premolar composites. */
		public SubstitutionCondition SubstOnlyIf;
		/** Last datetime that this row was inserted or updated. */
		public Date DateTStamp;
		/** Set to true if the procedure takes more than one appointment to complete. */
		public boolean IsMultiVisit;
		/** 11 digits or blank, enforced.  For 837I */
		public String DrugNDC;
		/** Gets copied to procedure.RevCode.  For 837I */
		public String RevenueCodeDefault;
		/** FK to provider.ProvNum.  0 for none. Otherwise, this provider will be used for this code instead of the normal provider. */
		public int ProvNumDefault;

		/** Deep copy of object. */
		public ProcedureCode Copy() {
			ProcedureCode procedurecode=new ProcedureCode();
			procedurecode.CodeNum=this.CodeNum;
			procedurecode.ProcCode=this.ProcCode;
			procedurecode.Descript=this.Descript;
			procedurecode.AbbrDesc=this.AbbrDesc;
			procedurecode.ProcTime=this.ProcTime;
			procedurecode.ProcCat=this.ProcCat;
			procedurecode.TreatArea=this.TreatArea;
			procedurecode.NoBillIns=this.NoBillIns;
			procedurecode.IsProsth=this.IsProsth;
			procedurecode.DefaultNote=this.DefaultNote;
			procedurecode.IsHygiene=this.IsHygiene;
			procedurecode.GTypeNum=this.GTypeNum;
			procedurecode.AlternateCode1=this.AlternateCode1;
			procedurecode.MedicalCode=this.MedicalCode;
			procedurecode.IsTaxed=this.IsTaxed;
			procedurecode.PaintType=this.PaintType;
			procedurecode.GraphicColor=this.GraphicColor;
			procedurecode.LaymanTerm=this.LaymanTerm;
			procedurecode.IsCanadianLab=this.IsCanadianLab;
			procedurecode.PreExisting=this.PreExisting;
			procedurecode.BaseUnits=this.BaseUnits;
			procedurecode.SubstitutionCode=this.SubstitutionCode;
			procedurecode.SubstOnlyIf=this.SubstOnlyIf;
			procedurecode.DateTStamp=this.DateTStamp;
			procedurecode.IsMultiVisit=this.IsMultiVisit;
			procedurecode.DrugNDC=this.DrugNDC;
			procedurecode.RevenueCodeDefault=this.RevenueCodeDefault;
			procedurecode.ProvNumDefault=this.ProvNumDefault;
			return procedurecode;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProcedureCode>");
			sb.append("<CodeNum>").append(CodeNum).append("</CodeNum>");
			sb.append("<ProcCode>").append(Serializing.EscapeForXml(ProcCode)).append("</ProcCode>");
			sb.append("<Descript>").append(Serializing.EscapeForXml(Descript)).append("</Descript>");
			sb.append("<AbbrDesc>").append(Serializing.EscapeForXml(AbbrDesc)).append("</AbbrDesc>");
			sb.append("<ProcTime>").append(Serializing.EscapeForXml(ProcTime)).append("</ProcTime>");
			sb.append("<ProcCat>").append(ProcCat).append("</ProcCat>");
			sb.append("<TreatArea>").append(TreatArea.ordinal()).append("</TreatArea>");
			sb.append("<NoBillIns>").append((NoBillIns)?1:0).append("</NoBillIns>");
			sb.append("<IsProsth>").append((IsProsth)?1:0).append("</IsProsth>");
			sb.append("<DefaultNote>").append(Serializing.EscapeForXml(DefaultNote)).append("</DefaultNote>");
			sb.append("<IsHygiene>").append((IsHygiene)?1:0).append("</IsHygiene>");
			sb.append("<GTypeNum>").append(GTypeNum).append("</GTypeNum>");
			sb.append("<AlternateCode1>").append(Serializing.EscapeForXml(AlternateCode1)).append("</AlternateCode1>");
			sb.append("<MedicalCode>").append(Serializing.EscapeForXml(MedicalCode)).append("</MedicalCode>");
			sb.append("<IsTaxed>").append((IsTaxed)?1:0).append("</IsTaxed>");
			sb.append("<PaintType>").append(PaintType.ordinal()).append("</PaintType>");
			sb.append("<GraphicColor>").append(GraphicColor).append("</GraphicColor>");
			sb.append("<LaymanTerm>").append(Serializing.EscapeForXml(LaymanTerm)).append("</LaymanTerm>");
			sb.append("<IsCanadianLab>").append((IsCanadianLab)?1:0).append("</IsCanadianLab>");
			sb.append("<PreExisting>").append((PreExisting)?1:0).append("</PreExisting>");
			sb.append("<BaseUnits>").append(BaseUnits).append("</BaseUnits>");
			sb.append("<SubstitutionCode>").append(Serializing.EscapeForXml(SubstitutionCode)).append("</SubstitutionCode>");
			sb.append("<SubstOnlyIf>").append(SubstOnlyIf.ordinal()).append("</SubstOnlyIf>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateTStamp>");
			sb.append("<IsMultiVisit>").append((IsMultiVisit)?1:0).append("</IsMultiVisit>");
			sb.append("<DrugNDC>").append(Serializing.EscapeForXml(DrugNDC)).append("</DrugNDC>");
			sb.append("<RevenueCodeDefault>").append(Serializing.EscapeForXml(RevenueCodeDefault)).append("</RevenueCodeDefault>");
			sb.append("<ProvNumDefault>").append(ProvNumDefault).append("</ProvNumDefault>");
			sb.append("</ProcedureCode>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				CodeNum=Integer.valueOf(doc.getElementsByTagName("CodeNum").item(0).getFirstChild().getNodeValue());
				ProcCode=doc.getElementsByTagName("ProcCode").item(0).getFirstChild().getNodeValue();
				Descript=doc.getElementsByTagName("Descript").item(0).getFirstChild().getNodeValue();
				AbbrDesc=doc.getElementsByTagName("AbbrDesc").item(0).getFirstChild().getNodeValue();
				ProcTime=doc.getElementsByTagName("ProcTime").item(0).getFirstChild().getNodeValue();
				ProcCat=Integer.valueOf(doc.getElementsByTagName("ProcCat").item(0).getFirstChild().getNodeValue());
				TreatArea=TreatmentArea.values()[Integer.valueOf(doc.getElementsByTagName("TreatArea").item(0).getFirstChild().getNodeValue())];
				NoBillIns=(doc.getElementsByTagName("NoBillIns").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				IsProsth=(doc.getElementsByTagName("IsProsth").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				DefaultNote=doc.getElementsByTagName("DefaultNote").item(0).getFirstChild().getNodeValue();
				IsHygiene=(doc.getElementsByTagName("IsHygiene").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				GTypeNum=Integer.valueOf(doc.getElementsByTagName("GTypeNum").item(0).getFirstChild().getNodeValue());
				AlternateCode1=doc.getElementsByTagName("AlternateCode1").item(0).getFirstChild().getNodeValue();
				MedicalCode=doc.getElementsByTagName("MedicalCode").item(0).getFirstChild().getNodeValue();
				IsTaxed=(doc.getElementsByTagName("IsTaxed").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				PaintType=ToothPaintingType.values()[Integer.valueOf(doc.getElementsByTagName("PaintType").item(0).getFirstChild().getNodeValue())];
				GraphicColor=Integer.valueOf(doc.getElementsByTagName("GraphicColor").item(0).getFirstChild().getNodeValue());
				LaymanTerm=doc.getElementsByTagName("LaymanTerm").item(0).getFirstChild().getNodeValue();
				IsCanadianLab=(doc.getElementsByTagName("IsCanadianLab").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				PreExisting=(doc.getElementsByTagName("PreExisting").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				BaseUnits=Integer.valueOf(doc.getElementsByTagName("BaseUnits").item(0).getFirstChild().getNodeValue());
				SubstitutionCode=doc.getElementsByTagName("SubstitutionCode").item(0).getFirstChild().getNodeValue();
				SubstOnlyIf=SubstitutionCondition.values()[Integer.valueOf(doc.getElementsByTagName("SubstOnlyIf").item(0).getFirstChild().getNodeValue())];
				DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue());
				IsMultiVisit=(doc.getElementsByTagName("IsMultiVisit").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				DrugNDC=doc.getElementsByTagName("DrugNDC").item(0).getFirstChild().getNodeValue();
				RevenueCodeDefault=doc.getElementsByTagName("RevenueCodeDefault").item(0).getFirstChild().getNodeValue();
				ProvNumDefault=Integer.valueOf(doc.getElementsByTagName("ProvNumDefault").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** Used in procedurecode setup to specify the treatment area for a procedure.  This determines what fields are available when editing an appointment. */
		public enum TreatmentArea {
			/** 0-never used */
			None,
			/** 1 */
			Surf,
			/** 2 */
			Tooth,
			/** 3 */
			Mouth,
			/** 4 */
			Quad,
			/** 5 */
			Sextant,
			/** 6 */
			Arch,
			/** 7 */
			ToothRange
		}

		/**  */
		public enum ToothPaintingType {
			/** 0 */
			None,
			/** 1 */
			Extraction,
			/** 2 */
			Implant,
			/** 3 */
			RCT,
			/** 4 */
			PostBU,
			/** 5 */
			FillingDark,
			/** 6 */
			FillingLight,
			/** 7 */
			CrownDark,
			/** 8 */
			CrownLight,
			/** 9 */
			BridgeDark,
			/** 10 */
			BridgeLight,
			/** 11 */
			DentureDark,
			/** 12 */
			DentureLight,
			/** 13 */
			Sealant,
			/** 14 */
			Veneer,
			/** 15 */
			Watch
		}

		/**  */
		public enum SubstitutionCondition {
			/**  */
			Always,
			/**  */
			Molar,
			/**  */
			SecondMolar
		}


}
