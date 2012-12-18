package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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
		public Commlog deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Commlog>");
			sb.append("<CommlogNum>").append(CommlogNum).append("</CommlogNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<CommDateTime>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(CommDateTime)).append("</CommDateTime>");
			sb.append("<CommType>").append(CommType).append("</CommType>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<Mode_>").append(Mode_.ordinal()).append("</Mode_>");
			sb.append("<SentOrReceived>").append(SentOrReceived.ordinal()).append("</SentOrReceived>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<Signature>").append(Serializing.escapeForXml(Signature)).append("</Signature>");
			sb.append("<SigIsTopaz>").append((SigIsTopaz)?1:0).append("</SigIsTopaz>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<DateTimeEnd>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeEnd)).append("</DateTimeEnd>");
			sb.append("</Commlog>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"CommlogNum")!=null) {
					CommlogNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CommlogNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CommDateTime")!=null) {
					CommDateTime=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"CommDateTime"));
				}
				if(Serializing.getXmlNodeValue(doc,"CommType")!=null) {
					CommType=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CommType"));
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"Mode_")!=null) {
					Mode_=CommItemMode.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"Mode_"))];
				}
				if(Serializing.getXmlNodeValue(doc,"SentOrReceived")!=null) {
					SentOrReceived=CommSentOrReceived.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"SentOrReceived"))];
				}
				if(Serializing.getXmlNodeValue(doc,"UserNum")!=null) {
					UserNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"UserNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Signature")!=null) {
					Signature=Serializing.getXmlNodeValue(doc,"Signature");
				}
				if(Serializing.getXmlNodeValue(doc,"SigIsTopaz")!=null) {
					SigIsTopaz=(Serializing.getXmlNodeValue(doc,"SigIsTopaz")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeEnd")!=null) {
					DateTimeEnd=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeEnd"));
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
