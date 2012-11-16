package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ProcButton {
		/** Primary key */
		public int ProcButtonNum;
		/** The text to show on the button. */
		public String Description;
		/** Order that they will show in the Chart module. */
		public int ItemOrder;
		/** FK to definition.DefNum. */
		public int Category;
		/** If no image, then the clob will be an empty string.  In this case, the bitmap will be null when loaded from the database. */
		public String ButtonImage;

		/** Deep copy of object. */
		public ProcButton Copy() {
			ProcButton procbutton=new ProcButton();
			procbutton.ProcButtonNum=this.ProcButtonNum;
			procbutton.Description=this.Description;
			procbutton.ItemOrder=this.ItemOrder;
			procbutton.Category=this.Category;
			procbutton.ButtonImage=this.ButtonImage;
			return procbutton;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProcButton>");
			sb.append("<ProcButtonNum>").append(ProcButtonNum).append("</ProcButtonNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<Category>").append(Category).append("</Category>");
			sb.append("<ButtonImage>").append(Serializing.EscapeForXml(ButtonImage)).append("</ButtonImage>");
			sb.append("</ProcButton>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"ProcButtonNum")!=null) {
					ProcButtonNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProcButtonNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Category")!=null) {
					Category=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Category"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ButtonImage")!=null) {
					ButtonImage=Serializing.GetXmlNodeValue(doc,"ButtonImage");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
