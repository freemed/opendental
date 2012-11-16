package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Statement {
		/** Primary key. */
		public int StatementNum;
		/** FK to patient.PatNum. Typically the guarantor.  Can also be the patient for walkout statements. */
		public int PatNum;
		/** This will always be a valid and reasonable date regardless of whether it's actually been sent yet. */
		public Date DateSent;
		/** Typically 45 days before dateSent */
		public Date DateRangeFrom;
		/** Any date >= year 2200 is considered max val.  We generally try to automate this value to be the same date as the statement rather than the max val.  This is so that when payment plans are displayed, we can add approximately 10 days to effectively show the charge that will soon be due.  Adding the 10 days is not done until display time. */
		public Date DateRangeTo;
		/** Can include line breaks.  This ordinary note will be in the standard font. */
		public String Note;
		/** More important notes may go here.  Font will be bold.  Color and size of text will be customizable in setup. */
		public String NoteBold;
		/** Enum:StatementMode Mail, InPerson, Email, Electronic. */
		public StatementMode Mode_;
		/** Set true to hide the credit card section, and the please pay box. */
		public boolean HidePayment;
		/** One patient on statement instead of entire family. */
		public boolean SinglePatient;
		/** If entire family, then this determines whether they are all intermingled into one big grid, or whether they are all listed in separate grids. */
		public boolean Intermingled;
		/** True */
		public boolean IsSent;
		/** FK to document.DocNum when a pdf has been archived. */
		public int DocNum;
		/** Date/time last altered. */
		public Date DateTStamp;
		/** The only effect of this flag is to change the text at the top of a statement from "statement" to "receipt".  It might later do more. */
		public boolean IsReceipt;
		/** This flag is for marking a statement as Invoice.  In this case, it must have procedures and/or adjustments attached. */
		public boolean IsInvoice;
		/** Only used if IsInvoice=true.  The first printout will not be a copy.  Subsequent printouts will show "copy" on them. */
		public boolean IsInvoiceCopy;

		/** Deep copy of object. */
		public Statement Copy() {
			Statement statement=new Statement();
			statement.StatementNum=this.StatementNum;
			statement.PatNum=this.PatNum;
			statement.DateSent=this.DateSent;
			statement.DateRangeFrom=this.DateRangeFrom;
			statement.DateRangeTo=this.DateRangeTo;
			statement.Note=this.Note;
			statement.NoteBold=this.NoteBold;
			statement.Mode_=this.Mode_;
			statement.HidePayment=this.HidePayment;
			statement.SinglePatient=this.SinglePatient;
			statement.Intermingled=this.Intermingled;
			statement.IsSent=this.IsSent;
			statement.DocNum=this.DocNum;
			statement.DateTStamp=this.DateTStamp;
			statement.IsReceipt=this.IsReceipt;
			statement.IsInvoice=this.IsInvoice;
			statement.IsInvoiceCopy=this.IsInvoiceCopy;
			return statement;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Statement>");
			sb.append("<StatementNum>").append(StatementNum).append("</StatementNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateSent>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateSent)).append("</DateSent>");
			sb.append("<DateRangeFrom>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateRangeFrom)).append("</DateRangeFrom>");
			sb.append("<DateRangeTo>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateRangeTo)).append("</DateRangeTo>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<NoteBold>").append(Serializing.EscapeForXml(NoteBold)).append("</NoteBold>");
			sb.append("<Mode_>").append(Mode_.ordinal()).append("</Mode_>");
			sb.append("<HidePayment>").append((HidePayment)?1:0).append("</HidePayment>");
			sb.append("<SinglePatient>").append((SinglePatient)?1:0).append("</SinglePatient>");
			sb.append("<Intermingled>").append((Intermingled)?1:0).append("</Intermingled>");
			sb.append("<IsSent>").append((IsSent)?1:0).append("</IsSent>");
			sb.append("<DocNum>").append(DocNum).append("</DocNum>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<IsReceipt>").append((IsReceipt)?1:0).append("</IsReceipt>");
			sb.append("<IsInvoice>").append((IsInvoice)?1:0).append("</IsInvoice>");
			sb.append("<IsInvoiceCopy>").append((IsInvoiceCopy)?1:0).append("</IsInvoiceCopy>");
			sb.append("</Statement>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"StatementNum")!=null) {
					StatementNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"StatementNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateSent")!=null) {
					DateSent=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateSent"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateRangeFrom")!=null) {
					DateRangeFrom=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateRangeFrom"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateRangeTo")!=null) {
					DateRangeTo=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateRangeTo"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
				if(Serializing.GetXmlNodeValue(doc,"NoteBold")!=null) {
					NoteBold=Serializing.GetXmlNodeValue(doc,"NoteBold");
				}
				if(Serializing.GetXmlNodeValue(doc,"Mode_")!=null) {
					Mode_=StatementMode.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Mode_"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"HidePayment")!=null) {
					HidePayment=(Serializing.GetXmlNodeValue(doc,"HidePayment")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"SinglePatient")!=null) {
					SinglePatient=(Serializing.GetXmlNodeValue(doc,"SinglePatient")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"Intermingled")!=null) {
					Intermingled=(Serializing.GetXmlNodeValue(doc,"Intermingled")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"IsSent")!=null) {
					IsSent=(Serializing.GetXmlNodeValue(doc,"IsSent")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"DocNum")!=null) {
					DocNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DocNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsReceipt")!=null) {
					IsReceipt=(Serializing.GetXmlNodeValue(doc,"IsReceipt")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"IsInvoice")!=null) {
					IsInvoice=(Serializing.GetXmlNodeValue(doc,"IsInvoice")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"IsInvoiceCopy")!=null) {
					IsInvoiceCopy=(Serializing.GetXmlNodeValue(doc,"IsInvoiceCopy")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum StatementMode {
			/** 0 */
			Mail,
			/** 1 */
			InPerson,
			/** 2 */
			Email,
			/** 3 */
			Electronic
		}


}
