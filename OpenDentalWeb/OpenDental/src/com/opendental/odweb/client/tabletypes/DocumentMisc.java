package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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
		public DocumentMisc deepCopy() {
			DocumentMisc documentmisc=new DocumentMisc();
			documentmisc.DocMiscNum=this.DocMiscNum;
			documentmisc.DateCreated=this.DateCreated;
			documentmisc.FileName=this.FileName;
			documentmisc.DocMiscType=this.DocMiscType;
			documentmisc.RawBase64=this.RawBase64;
			return documentmisc;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<DocumentMisc>");
			sb.append("<DocMiscNum>").append(DocMiscNum).append("</DocMiscNum>");
			sb.append("<DateCreated>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateCreated)).append("</DateCreated>");
			sb.append("<FileName>").append(Serializing.escapeForXml(FileName)).append("</FileName>");
			sb.append("<DocMiscType>").append(DocMiscType.ordinal()).append("</DocMiscType>");
			sb.append("<RawBase64>").append(Serializing.escapeForXml(RawBase64)).append("</RawBase64>");
			sb.append("</DocumentMisc>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"DocMiscNum")!=null) {
					DocMiscNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DocMiscNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateCreated")!=null) {
					DateCreated=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateCreated"));
				}
				if(Serializing.getXmlNodeValue(doc,"FileName")!=null) {
					FileName=Serializing.getXmlNodeValue(doc,"FileName");
				}
				if(Serializing.getXmlNodeValue(doc,"DocMiscType")!=null) {
					DocMiscType=DocumentMiscType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"DocMiscType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"RawBase64")!=null) {
					RawBase64=Serializing.getXmlNodeValue(doc,"RawBase64");
				}
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
