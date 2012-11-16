package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public ApptViewItem Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ApptViewItem>");
			sb.append("<ApptViewItemNum>").append(ApptViewItemNum).append("</ApptViewItemNum>");
			sb.append("<ApptViewNum>").append(ApptViewNum).append("</ApptViewNum>");
			sb.append("<OpNum>").append(OpNum).append("</OpNum>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<ElementDesc>").append(Serializing.EscapeForXml(ElementDesc)).append("</ElementDesc>");
			sb.append("<ElementOrder>").append(ElementOrder).append("</ElementOrder>");
			sb.append("<ElementColor>").append(ElementColor).append("</ElementColor>");
			sb.append("<ElementAlignment>").append(ElementAlignment.ordinal()).append("</ElementAlignment>");
			sb.append("<ApptFieldDefNum>").append(ApptFieldDefNum).append("</ApptFieldDefNum>");
			sb.append("<PatFieldDefNum>").append(PatFieldDefNum).append("</PatFieldDefNum>");
			sb.append("</ApptViewItem>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"ApptViewItemNum")!=null) {
					ApptViewItemNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ApptViewItemNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ApptViewNum")!=null) {
					ApptViewNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ApptViewNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"OpNum")!=null) {
					OpNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"OpNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ElementDesc")!=null) {
					ElementDesc=Serializing.GetXmlNodeValue(doc,"ElementDesc");
				}
				if(Serializing.GetXmlNodeValue(doc,"ElementOrder")!=null) {
					ElementOrder=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"ElementOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ElementColor")!=null) {
					ElementColor=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ElementColor"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ElementAlignment")!=null) {
					ElementAlignment=ApptViewAlignment.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ElementAlignment"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"ApptFieldDefNum")!=null) {
					ApptFieldDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ApptFieldDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatFieldDefNum")!=null) {
					PatFieldDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatFieldDefNum"));
				}
			}
			catch(Exception e) {
				throw e;
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
