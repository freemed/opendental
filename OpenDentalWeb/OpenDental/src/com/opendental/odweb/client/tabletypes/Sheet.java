package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Sheet {
		/** Primary key. */
		public int SheetNum;
		/** Enum:SheetTypeEnum */
		public SheetTypeEnum SheetType;
		/** FK to patient.PatNum.  A saved sheet is always attached to a patient (except deposit slip).  There are a few sheets that are so minor that they don't get saved, such as a Carrier label. */
		public int PatNum;
		/** The date and time of the sheet as it will be displayed in the commlog. */
		public Date DateTimeSheet;
		/** The default fontSize for the sheet.  The actual font must still be saved with each sheetField. */
		public float FontSize;
		/** The default fontName for the sheet.  The actual font must still be saved with each sheetField. */
		public String FontName;
		/** Width of the sheet in pixels, 100 pixels per inch. */
		public int Width;
		/** Height of the sheet in pixels, 100 pixels per inch. */
		public int Height;
		/** . */
		public boolean IsLandscape;
		/** An internal note for the use of the office staff regarding the sheet.  Not to be printed on the sheet in any way. */
		public String InternalNote;
		/** Copied from the SheetDef description. */
		public String Description;
		/** The order that this sheet will show in the patient terminal for the patient to fill out.  Or zero if not set. */
		public byte ShowInTerminal;
		/** True if this sheet was downloaded from the webforms service. */
		public boolean IsWebForm;

		/** Deep copy of object. */
		public Sheet Copy() {
			Sheet sheet=new Sheet();
			sheet.SheetNum=this.SheetNum;
			sheet.SheetType=this.SheetType;
			sheet.PatNum=this.PatNum;
			sheet.DateTimeSheet=this.DateTimeSheet;
			sheet.FontSize=this.FontSize;
			sheet.FontName=this.FontName;
			sheet.Width=this.Width;
			sheet.Height=this.Height;
			sheet.IsLandscape=this.IsLandscape;
			sheet.InternalNote=this.InternalNote;
			sheet.Description=this.Description;
			sheet.ShowInTerminal=this.ShowInTerminal;
			sheet.IsWebForm=this.IsWebForm;
			return sheet;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Sheet>");
			sb.append("<SheetNum>").append(SheetNum).append("</SheetNum>");
			sb.append("<SheetType>").append(SheetType.ordinal()).append("</SheetType>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateTimeSheet>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeSheet)).append("</DateTimeSheet>");
			sb.append("<FontSize>").append(FontSize).append("</FontSize>");
			sb.append("<FontName>").append(Serializing.EscapeForXml(FontName)).append("</FontName>");
			sb.append("<Width>").append(Width).append("</Width>");
			sb.append("<Height>").append(Height).append("</Height>");
			sb.append("<IsLandscape>").append((IsLandscape)?1:0).append("</IsLandscape>");
			sb.append("<InternalNote>").append(Serializing.EscapeForXml(InternalNote)).append("</InternalNote>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<ShowInTerminal>").append(ShowInTerminal).append("</ShowInTerminal>");
			sb.append("<IsWebForm>").append((IsWebForm)?1:0).append("</IsWebForm>");
			sb.append("</Sheet>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				SheetNum=Integer.valueOf(doc.getElementsByTagName("SheetNum").item(0).getFirstChild().getNodeValue());
				SheetType=SheetTypeEnum.values()[Integer.valueOf(doc.getElementsByTagName("SheetType").item(0).getFirstChild().getNodeValue())];
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				DateTimeSheet=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTimeSheet").item(0).getFirstChild().getNodeValue());
				FontSize=Float.valueOf(doc.getElementsByTagName("FontSize").item(0).getFirstChild().getNodeValue());
				FontName=doc.getElementsByTagName("FontName").item(0).getFirstChild().getNodeValue();
				Width=Integer.valueOf(doc.getElementsByTagName("Width").item(0).getFirstChild().getNodeValue());
				Height=Integer.valueOf(doc.getElementsByTagName("Height").item(0).getFirstChild().getNodeValue());
				IsLandscape=(doc.getElementsByTagName("IsLandscape").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				InternalNote=doc.getElementsByTagName("InternalNote").item(0).getFirstChild().getNodeValue();
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				ShowInTerminal=Byte.valueOf(doc.getElementsByTagName("ShowInTerminal").item(0).getFirstChild().getNodeValue());
				IsWebForm=(doc.getElementsByTagName("IsWebForm").item(0).getFirstChild().getNodeValue()=="0")?false:true;
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
