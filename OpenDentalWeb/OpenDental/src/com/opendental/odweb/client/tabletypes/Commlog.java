package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Commlog {
		/** Primary key. */
		public int CommlogNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** Date and time of entry */
		public Date CommDateTime;
		/** FK to definition.DefNum. This will be 0 if IsStatementSent.  Used to be an enumeration in previous versions. */
		public int CommType;
		/** Note for this commlog entry. */
		public String Note;
		/** Enum:CommItemMode Phone, email, etc. */
		public CommItemMode Mode_;
		/** Enum:CommSentOrReceived Neither=0,Sent=1,Received=2. */
		public CommSentOrReceived SentOrReceived;
		/** FK to userod.UserNum. */
		public int UserNum;
		/** Signature.  For details, see procedurelog.Signature. */
		public String Signature;
		/** True if signed using the Topaz signature pad, false otherwise. */
		public boolean SigIsTopaz;
		/** Automatically updated by MySQL every time a row is added or changed. */
		public Date DateTStamp;
		/** Date and time when commlog ended.  Mainly for internal use. */
		public Date DateTimeEnd;

		/** Deep copy of object. */
		public Commlog Copy() {
			Commlog commlog=new Commlog();
			commlog.CommlogNum=this.CommlogNum;
			commlog.PatNum=this.PatNum;
			commlog.CommDateTime=this.CommDateTime;
			commlog.CommType=this.CommType;
			commlog.Note=this.Note;
			commlog.Mode_=this.Mode_;
			commlog.SentOrReceived=this.SentOrReceived;
			commlog.UserNum=this.UserNum;
			commlog.Signature=this.Signature;
			commlog.SigIsTopaz=this.SigIsTopaz;
			commlog.DateTStamp=this.DateTStamp;
			commlog.DateTimeEnd=this.DateTimeEnd;
			return commlog;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Commlog>");
			sb.append("<CommlogNum>").append(CommlogNum).append("</CommlogNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<CommDateTime>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(CommDateTime)).append("</CommDateTime>");
			sb.append("<CommType>").append(CommType).append("</CommType>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<Mode_>").append(Mode_.ordinal()).append("</Mode_>");
			sb.append("<SentOrReceived>").append(SentOrReceived.ordinal()).append("</SentOrReceived>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<Signature>").append(Serializing.EscapeForXml(Signature)).append("</Signature>");
			sb.append("<SigIsTopaz>").append((SigIsTopaz)?1:0).append("</SigIsTopaz>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<DateTimeEnd>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeEnd)).append("</DateTimeEnd>");
			sb.append("</Commlog>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"CommlogNum")!=null) {
					CommlogNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CommlogNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CommDateTime")!=null) {
					CommDateTime=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"CommDateTime"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CommType")!=null) {
					CommType=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CommType"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
				if(Serializing.GetXmlNodeValue(doc,"Mode_")!=null) {
					Mode_=CommItemMode.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Mode_"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"SentOrReceived")!=null) {
					SentOrReceived=CommSentOrReceived.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SentOrReceived"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"UserNum")!=null) {
					UserNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"UserNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Signature")!=null) {
					Signature=Serializing.GetXmlNodeValue(doc,"Signature");
				}
				if(Serializing.GetXmlNodeValue(doc,"SigIsTopaz")!=null) {
					SigIsTopaz=(Serializing.GetXmlNodeValue(doc,"SigIsTopaz")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTimeEnd")!=null) {
					DateTimeEnd=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTimeEnd"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum CommItemMode {
			/** 0-  */
			None,
			/** 1-  */
			Email,
			/** 2 */
			Mail,
			/** 3 */
			Phone,
			/** 4 */
			InPerson,
			/** 5 */
			Text
		}

		/** 0=neither, 1=sent, 2=received. */
		public enum CommSentOrReceived {
			/** 0 */
			Neither,
			/** 1 */
			Sent,
			/** 2 */
			Received
		}


}
