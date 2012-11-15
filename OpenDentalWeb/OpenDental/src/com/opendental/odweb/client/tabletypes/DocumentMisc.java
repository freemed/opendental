package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class DocumentMisc {
		/** Primary key. */
		public int DocMiscNum;
		/** Date created. */
		public Date DateCreated;
		/** The name the file would have if it was not in the database. Does not include any directory info. */
		public String FileName;
		/** Enum:DocumentMiscType Corresponds to the same subfolder within AtoZ folder. eg. UpdateFiles */
		public DocumentMiscType DocMiscType;
		/** The raw file data encoded as base64. */
		public String RawBase64;

		/** Deep copy of object. */
		public DocumentMisc Copy() {
			DocumentMisc documentmisc=new DocumentMisc();
			documentmisc.DocMiscNum=this.DocMiscNum;
			documentmisc.DateCreated=this.DateCreated;
			documentmisc.FileName=this.FileName;
			documentmisc.DocMiscType=this.DocMiscType;
			documentmisc.RawBase64=this.RawBase64;
			return documentmisc;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<DocumentMisc>");
			sb.append("<DocMiscNum>").append(DocMiscNum).append("</DocMiscNum>");
			sb.append("<DateCreated>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateCreated>");
			sb.append("<FileName>").append(Serializing.EscapeForXml(FileName)).append("</FileName>");
			sb.append("<DocMiscType>").append(DocMiscType.ordinal()).append("</DocMiscType>");
			sb.append("<RawBase64>").append(Serializing.EscapeForXml(RawBase64)).append("</RawBase64>");
			sb.append("</DocumentMisc>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				DocMiscNum=Integer.valueOf(doc.getElementsByTagName("DocMiscNum").item(0).getFirstChild().getNodeValue());
				DateCreated=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateCreated").item(0).getFirstChild().getNodeValue());
				FileName=doc.getElementsByTagName("FileName").item(0).getFirstChild().getNodeValue();
				DocMiscType=DocumentMiscType.values()[Integer.valueOf(doc.getElementsByTagName("DocMiscType").item(0).getFirstChild().getNodeValue())];
				RawBase64=doc.getElementsByTagName("RawBase64").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** More types will be added to correspond to most of the subfolders inside the AtoZ folder.  But no point adding them until we implement. */
		public enum DocumentMiscType {
			/** 0- There will just be zero or one row of this type.  It will contain a zipped archive. */
			UpdateFiles
		}


}
