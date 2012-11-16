package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class FeeSched {
		/** Primary key. */
		public int FeeSchedNum;
		/** The name of the fee schedule. */
		public String Description;
		/** Enum:FeeScheduleType */
		public FeeScheduleType FeeSchedType;
		/** Unlike with the old definition table, this ItemOrder is not as critical in the caching of data.  The item order is only for fee schedules of the same type. */
		public int ItemOrder;
		/** True if the fee schedule is hidden.  Can't delete fee schedules or change their type once created. */
		public boolean IsHidden;

		/** Deep copy of object. */
		public FeeSched Copy() {
			FeeSched feesched=new FeeSched();
			feesched.FeeSchedNum=this.FeeSchedNum;
			feesched.Description=this.Description;
			feesched.FeeSchedType=this.FeeSchedType;
			feesched.ItemOrder=this.ItemOrder;
			feesched.IsHidden=this.IsHidden;
			return feesched;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<FeeSched>");
			sb.append("<FeeSchedNum>").append(FeeSchedNum).append("</FeeSchedNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<FeeSchedType>").append(FeeSchedType.ordinal()).append("</FeeSchedType>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("</FeeSched>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"FeeSchedNum")!=null) {
					FeeSchedNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"FeeSchedNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"FeeSchedType")!=null) {
					FeeSchedType=FeeScheduleType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"FeeSchedType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.GetXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum FeeScheduleType {
			/** 0 */
			Normal,
			/** 1 */
			CoPay,
			/** 2 */
			Allowed
		}


}
