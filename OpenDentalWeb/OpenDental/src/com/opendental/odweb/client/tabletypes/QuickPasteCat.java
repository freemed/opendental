package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public QuickPasteCat Copy() {
			QuickPasteCat quickpastecat=new QuickPasteCat();
			quickpastecat.QuickPasteCatNum=this.QuickPasteCatNum;
			quickpastecat.Description=this.Description;
			quickpastecat.ItemOrder=this.ItemOrder;
			quickpastecat.DefaultForTypes=this.DefaultForTypes;
			return quickpastecat;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<QuickPasteCat>");
			sb.append("<QuickPasteCatNum>").append(QuickPasteCatNum).append("</QuickPasteCatNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<DefaultForTypes>").append(Serializing.EscapeForXml(DefaultForTypes)).append("</DefaultForTypes>");
			sb.append("</QuickPasteCat>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"QuickPasteCatNum")!=null) {
					QuickPasteCatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"QuickPasteCatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DefaultForTypes")!=null) {
					DefaultForTypes=Serializing.GetXmlNodeValue(doc,"DefaultForTypes");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
