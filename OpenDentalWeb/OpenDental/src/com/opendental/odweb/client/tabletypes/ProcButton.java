package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public ProcButton deepCopy() {
			ProcButton procbutton=new ProcButton();
			procbutton.ProcButtonNum=this.ProcButtonNum;
			procbutton.Description=this.Description;
			procbutton.ItemOrder=this.ItemOrder;
			procbutton.Category=this.Category;
			procbutton.ButtonImage=this.ButtonImage;
			return procbutton;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProcButton>");
			sb.append("<ProcButtonNum>").append(ProcButtonNum).append("</ProcButtonNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<Category>").append(Category).append("</Category>");
			sb.append("<ButtonImage>").append(Serializing.escapeForXml(ButtonImage)).append("</ButtonImage>");
			sb.append("</ProcButton>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ProcButtonNum")!=null) {
					ProcButtonNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcButtonNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"Category")!=null) {
					Category=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Category"));
				}
				if(Serializing.getXmlNodeValue(doc,"ButtonImage")!=null) {
					ButtonImage=Serializing.getXmlNodeValue(doc,"ButtonImage");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing ProcButton: "+e.getMessage());
			}
		}


}
