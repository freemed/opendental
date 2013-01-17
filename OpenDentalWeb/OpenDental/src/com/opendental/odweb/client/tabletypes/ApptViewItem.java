package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class ApptViewItem {
		/** Primary key. */
		public int ApptViewItemNum;
		/** FK to apptview. */
		public int ApptViewNum;
		/** FK to operatory.OperatoryNum. */
		public int OpNum;
		/** FK to provider.ProvNum. */
		public int ProvNum;
		/** Must be one of the hard coded strings picked from the available list. */
		public String ElementDesc;
		/** If this is a row Element, then this is the 0-based order within its area.  For example, UR starts over with 0 ordering. */
		public byte ElementOrder;
		/** If this is an element, then this is the color. */
		public int ElementColor;
		/** Enum:ApptViewAlignment. If this is an element, then this is the alignment of the element within the appointment. */
		public ApptViewAlignment ElementAlignment;
		/** FK to apptfielddef.ApptFieldDefNum.  If this is an element, and the element is an appt field, then this tells us which one. */
		public int ApptFieldDefNum;
		/** FK to patfielddef.PatFieldDefNum.  If this is an element, and the element is an appt field, then this tells us which one.  Not implemented yet. */
		public int PatFieldDefNum;

		/** Deep copy of object. */
		public ApptViewItem deepCopy() {
			ApptViewItem apptviewitem=new ApptViewItem();
			apptviewitem.ApptViewItemNum=this.ApptViewItemNum;
			apptviewitem.ApptViewNum=this.ApptViewNum;
			apptviewitem.OpNum=this.OpNum;
			apptviewitem.ProvNum=this.ProvNum;
			apptviewitem.ElementDesc=this.ElementDesc;
			apptviewitem.ElementOrder=this.ElementOrder;
			apptviewitem.ElementColor=this.ElementColor;
			apptviewitem.ElementAlignment=this.ElementAlignment;
			apptviewitem.ApptFieldDefNum=this.ApptFieldDefNum;
			apptviewitem.PatFieldDefNum=this.PatFieldDefNum;
			return apptviewitem;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ApptViewItem>");
			sb.append("<ApptViewItemNum>").append(ApptViewItemNum).append("</ApptViewItemNum>");
			sb.append("<ApptViewNum>").append(ApptViewNum).append("</ApptViewNum>");
			sb.append("<OpNum>").append(OpNum).append("</OpNum>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<ElementDesc>").append(Serializing.escapeForXml(ElementDesc)).append("</ElementDesc>");
			sb.append("<ElementOrder>").append(ElementOrder).append("</ElementOrder>");
			sb.append("<ElementColor>").append(ElementColor).append("</ElementColor>");
			sb.append("<ElementAlignment>").append(ElementAlignment.ordinal()).append("</ElementAlignment>");
			sb.append("<ApptFieldDefNum>").append(ApptFieldDefNum).append("</ApptFieldDefNum>");
			sb.append("<PatFieldDefNum>").append(PatFieldDefNum).append("</PatFieldDefNum>");
			sb.append("</ApptViewItem>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ApptViewItemNum")!=null) {
					ApptViewItemNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ApptViewItemNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ApptViewNum")!=null) {
					ApptViewNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ApptViewNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"OpNum")!=null) {
					OpNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"OpNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ElementDesc")!=null) {
					ElementDesc=Serializing.getXmlNodeValue(doc,"ElementDesc");
				}
				if(Serializing.getXmlNodeValue(doc,"ElementOrder")!=null) {
					ElementOrder=Byte.valueOf(Serializing.getXmlNodeValue(doc,"ElementOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"ElementColor")!=null) {
					ElementColor=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ElementColor"));
				}
				if(Serializing.getXmlNodeValue(doc,"ElementAlignment")!=null) {
					ElementAlignment=ApptViewAlignment.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"ElementAlignment"))];
				}
				if(Serializing.getXmlNodeValue(doc,"ApptFieldDefNum")!=null) {
					ApptFieldDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ApptFieldDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatFieldDefNum")!=null) {
					PatFieldDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatFieldDefNum"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing ApptViewItem: "+e.getMessage());
			}
		}

		/**  */
		public enum ApptViewAlignment {
			/** 0 */
			Main,
			/** 1 */
			UR,
			/** 2 */
			LR
		}


}
