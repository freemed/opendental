package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public ProcedureCode deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProcedureCode>");
			sb.append("<CodeNum>").append(CodeNum).append("</CodeNum>");
			sb.append("<ProcCode>").append(Serializing.escapeForXml(ProcCode)).append("</ProcCode>");
			sb.append("<Descript>").append(Serializing.escapeForXml(Descript)).append("</Descript>");
			sb.append("<AbbrDesc>").append(Serializing.escapeForXml(AbbrDesc)).append("</AbbrDesc>");
			sb.append("<ProcTime>").append(Serializing.escapeForXml(ProcTime)).append("</ProcTime>");
			sb.append("<ProcCat>").append(ProcCat).append("</ProcCat>");
			sb.append("<TreatArea>").append(TreatArea.ordinal()).append("</TreatArea>");
			sb.append("<NoBillIns>").append((NoBillIns)?1:0).append("</NoBillIns>");
			sb.append("<IsProsth>").append((IsProsth)?1:0).append("</IsProsth>");
			sb.append("<DefaultNote>").append(Serializing.escapeForXml(DefaultNote)).append("</DefaultNote>");
			sb.append("<IsHygiene>").append((IsHygiene)?1:0).append("</IsHygiene>");
			sb.append("<GTypeNum>").append(GTypeNum).append("</GTypeNum>");
			sb.append("<AlternateCode1>").append(Serializing.escapeForXml(AlternateCode1)).append("</AlternateCode1>");
			sb.append("<MedicalCode>").append(Serializing.escapeForXml(MedicalCode)).append("</MedicalCode>");
			sb.append("<IsTaxed>").append((IsTaxed)?1:0).append("</IsTaxed>");
			sb.append("<PaintType>").append(PaintType.ordinal()).append("</PaintType>");
			sb.append("<GraphicColor>").append(GraphicColor).append("</GraphicColor>");
			sb.append("<LaymanTerm>").append(Serializing.escapeForXml(LaymanTerm)).append("</LaymanTerm>");
			sb.append("<IsCanadianLab>").append((IsCanadianLab)?1:0).append("</IsCanadianLab>");
			sb.append("<PreExisting>").append((PreExisting)?1:0).append("</PreExisting>");
			sb.append("<BaseUnits>").append(BaseUnits).append("</BaseUnits>");
			sb.append("<SubstitutionCode>").append(Serializing.escapeForXml(SubstitutionCode)).append("</SubstitutionCode>");
			sb.append("<SubstOnlyIf>").append(SubstOnlyIf.ordinal()).append("</SubstOnlyIf>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<IsMultiVisit>").append((IsMultiVisit)?1:0).append("</IsMultiVisit>");
			sb.append("<DrugNDC>").append(Serializing.escapeForXml(DrugNDC)).append("</DrugNDC>");
			sb.append("<RevenueCodeDefault>").append(Serializing.escapeForXml(RevenueCodeDefault)).append("</RevenueCodeDefault>");
			sb.append("<ProvNumDefault>").append(ProvNumDefault).append("</ProvNumDefault>");
			sb.append("</ProcedureCode>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"CodeNum")!=null) {
					CodeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CodeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProcCode")!=null) {
					ProcCode=Serializing.getXmlNodeValue(doc,"ProcCode");
				}
				if(Serializing.getXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.getXmlNodeValue(doc,"Descript");
				}
				if(Serializing.getXmlNodeValue(doc,"AbbrDesc")!=null) {
					AbbrDesc=Serializing.getXmlNodeValue(doc,"AbbrDesc");
				}
				if(Serializing.getXmlNodeValue(doc,"ProcTime")!=null) {
					ProcTime=Serializing.getXmlNodeValue(doc,"ProcTime");
				}
				if(Serializing.getXmlNodeValue(doc,"ProcCat")!=null) {
					ProcCat=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcCat"));
				}
				if(Serializing.getXmlNodeValue(doc,"TreatArea")!=null) {
					TreatArea=TreatmentArea.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"TreatArea"))];
				}
				if(Serializing.getXmlNodeValue(doc,"NoBillIns")!=null) {
					NoBillIns=(Serializing.getXmlNodeValue(doc,"NoBillIns")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"IsProsth")!=null) {
					IsProsth=(Serializing.getXmlNodeValue(doc,"IsProsth")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"DefaultNote")!=null) {
					DefaultNote=Serializing.getXmlNodeValue(doc,"DefaultNote");
				}
				if(Serializing.getXmlNodeValue(doc,"IsHygiene")!=null) {
					IsHygiene=(Serializing.getXmlNodeValue(doc,"IsHygiene")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"GTypeNum")!=null) {
					GTypeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"GTypeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AlternateCode1")!=null) {
					AlternateCode1=Serializing.getXmlNodeValue(doc,"AlternateCode1");
				}
				if(Serializing.getXmlNodeValue(doc,"MedicalCode")!=null) {
					MedicalCode=Serializing.getXmlNodeValue(doc,"MedicalCode");
				}
				if(Serializing.getXmlNodeValue(doc,"IsTaxed")!=null) {
					IsTaxed=(Serializing.getXmlNodeValue(doc,"IsTaxed")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"PaintType")!=null) {
					PaintType=ToothPaintingType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"PaintType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"GraphicColor")!=null) {
					GraphicColor=Integer.valueOf(Serializing.getXmlNodeValue(doc,"GraphicColor"));
				}
				if(Serializing.getXmlNodeValue(doc,"LaymanTerm")!=null) {
					LaymanTerm=Serializing.getXmlNodeValue(doc,"LaymanTerm");
				}
				if(Serializing.getXmlNodeValue(doc,"IsCanadianLab")!=null) {
					IsCanadianLab=(Serializing.getXmlNodeValue(doc,"IsCanadianLab")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"PreExisting")!=null) {
					PreExisting=(Serializing.getXmlNodeValue(doc,"PreExisting")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"BaseUnits")!=null) {
					BaseUnits=Integer.valueOf(Serializing.getXmlNodeValue(doc,"BaseUnits"));
				}
				if(Serializing.getXmlNodeValue(doc,"SubstitutionCode")!=null) {
					SubstitutionCode=Serializing.getXmlNodeValue(doc,"SubstitutionCode");
				}
				if(Serializing.getXmlNodeValue(doc,"SubstOnlyIf")!=null) {
					SubstOnlyIf=SubstitutionCondition.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"SubstOnlyIf"))];
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsMultiVisit")!=null) {
					IsMultiVisit=(Serializing.getXmlNodeValue(doc,"IsMultiVisit")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"DrugNDC")!=null) {
					DrugNDC=Serializing.getXmlNodeValue(doc,"DrugNDC");
				}
				if(Serializing.getXmlNodeValue(doc,"RevenueCodeDefault")!=null) {
					RevenueCodeDefault=Serializing.getXmlNodeValue(doc,"RevenueCodeDefault");
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNumDefault")!=null) {
					ProvNumDefault=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNumDefault"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing ProcedureCode: "+e.getMessage());
			}
		}

		/** Used in procedurecode setup to specify the treatment area for a procedure.  This determines what fields are available when editing an appointment. */
		public enum TreatmentArea {
			/** 0-Same as 3 mouth. */
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
