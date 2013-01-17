package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public FeeSched deepCopy() {
			FeeSched feesched=new FeeSched();
			feesched.FeeSchedNum=this.FeeSchedNum;
			feesched.Description=this.Description;
			feesched.FeeSchedType=this.FeeSchedType;
			feesched.ItemOrder=this.ItemOrder;
			feesched.IsHidden=this.IsHidden;
			return feesched;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<FeeSched>");
			sb.append("<FeeSchedNum>").append(FeeSchedNum).append("</FeeSchedNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<FeeSchedType>").append(FeeSchedType.ordinal()).append("</FeeSchedType>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("</FeeSched>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"FeeSchedNum")!=null) {
					FeeSchedNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FeeSchedNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"FeeSchedType")!=null) {
					FeeSchedType=FeeScheduleType.valueOf(Serializing.getXmlNodeValue(doc,"FeeSchedType"));
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.getXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing FeeSched: "+e.getMessage());
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
