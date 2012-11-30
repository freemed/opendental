package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class SheetDef {
		/** Primary key. */
		public int SheetDefNum;
		/** The description of this sheetdef. */
		public String Description;
		/** Enum:SheetTypeEnum */
		public SheetTypeEnum SheetType;
		/** The default fontSize for the sheet.  The actual font must still be saved with each sheetField. */
		public float FontSize;
		/** The default fontName for the sheet.  The actual font must still be saved with each sheetField. */
		public String FontName;
		/** Width of the sheet in pixels, 100 pixels per inch. */
		public int Width;
		/** Height of the sheet in pixels, 100 pixels per inch. */
		public int Height;
		/** Set to true to print landscape. */
		public boolean IsLandscape;

		/** Deep copy of object. */
		public SheetDef Copy() {
			SheetDef sheetdef=new SheetDef();
			sheetdef.SheetDefNum=this.SheetDefNum;
			sheetdef.Description=this.Description;
			sheetdef.SheetType=this.SheetType;
			sheetdef.FontSize=this.FontSize;
			sheetdef.FontName=this.FontName;
			sheetdef.Width=this.Width;
			sheetdef.Height=this.Height;
			sheetdef.IsLandscape=this.IsLandscape;
			return sheetdef;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SheetDef>");
			sb.append("<SheetDefNum>").append(SheetDefNum).append("</SheetDefNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<SheetType>").append(SheetType.ordinal()).append("</SheetType>");
			sb.append("<FontSize>").append(FontSize).append("</FontSize>");
			sb.append("<FontName>").append(Serializing.EscapeForXml(FontName)).append("</FontName>");
			sb.append("<Width>").append(Width).append("</Width>");
			sb.append("<Height>").append(Height).append("</Height>");
			sb.append("<IsLandscape>").append((IsLandscape)?1:0).append("</IsLandscape>");
			sb.append("</SheetDef>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"SheetDefNum")!=null) {
					SheetDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SheetDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"SheetType")!=null) {
					SheetType=SheetTypeEnum.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SheetType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"FontSize")!=null) {
					FontSize=Float.valueOf(Serializing.GetXmlNodeValue(doc,"FontSize"));
				}
				if(Serializing.GetXmlNodeValue(doc,"FontName")!=null) {
					FontName=Serializing.GetXmlNodeValue(doc,"FontName");
				}
				if(Serializing.GetXmlNodeValue(doc,"Width")!=null) {
					Width=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Width"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Height")!=null) {
					Height=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Height"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsLandscape")!=null) {
					IsLandscape=(Serializing.GetXmlNodeValue(doc,"IsLandscape")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum SheetTypeEnum {
			/**  */
			LabelPatient,
			/**  */
			LabelCarrier,
			/**  */
			LabelReferral,
			/**  */
			ReferralSlip,
			/**  */
			LabelAppointment,
			/**  */
			Rx,
			/** 6-Requires SheetParameter for PatNum. */
			Consent,
			/** 7-Requires SheetParameter for PatNum. */
			PatientLetter,
			/** 8-Requires SheetParameters for PatNum,ReferralNum. */
			ReferralLetter,
			/**  */
			PatientForm,
			/**  */
			RoutingSlip,
			/**  */
			MedicalHistory,
			/**  */
			LabSlip,
			/**  */
			ExamSheet,
			/** 14-Requires SheetParameter for PatNum. */
			DepositSlip
		}


}
