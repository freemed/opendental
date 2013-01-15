package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class QuickPasteCat {
		/** Primary key. */
		public int QuickPasteCatNum;
		/** . */
		public String Description;
		/** The order of this category within the list. 0-based. */
		public int ItemOrder;
		/** Enum:QuickPasteType  Each Category can be set to be the default category for multiple types of notes. Stored as integers separated by commas. */
		public String DefaultForTypes;

		/** Deep copy of object. */
		public QuickPasteCat deepCopy() {
			QuickPasteCat quickpastecat=new QuickPasteCat();
			quickpastecat.QuickPasteCatNum=this.QuickPasteCatNum;
			quickpastecat.Description=this.Description;
			quickpastecat.ItemOrder=this.ItemOrder;
			quickpastecat.DefaultForTypes=this.DefaultForTypes;
			return quickpastecat;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<QuickPasteCat>");
			sb.append("<QuickPasteCatNum>").append(QuickPasteCatNum).append("</QuickPasteCatNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<DefaultForTypes>").append(Serializing.escapeForXml(DefaultForTypes)).append("</DefaultForTypes>");
			sb.append("</QuickPasteCat>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"QuickPasteCatNum")!=null) {
					QuickPasteCatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"QuickPasteCatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"DefaultForTypes")!=null) {
					DefaultForTypes=Serializing.getXmlNodeValue(doc,"DefaultForTypes");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
