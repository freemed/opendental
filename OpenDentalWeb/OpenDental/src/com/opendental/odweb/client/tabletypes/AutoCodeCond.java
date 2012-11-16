package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class AutoCodeCond {
		/** Primary key. */
		public int AutoCodeCondNum;
		/** FK to autocodeitem.AutoCodeItemNum. */
		public int AutoCodeItemNum;
		/** Enum:AutoCondition  */
		public AutoCondition Cond;

		/** Deep copy of object. */
		public AutoCodeCond Copy() {
			AutoCodeCond autocodecond=new AutoCodeCond();
			autocodecond.AutoCodeCondNum=this.AutoCodeCondNum;
			autocodecond.AutoCodeItemNum=this.AutoCodeItemNum;
			autocodecond.Cond=this.Cond;
			return autocodecond;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AutoCodeCond>");
			sb.append("<AutoCodeCondNum>").append(AutoCodeCondNum).append("</AutoCodeCondNum>");
			sb.append("<AutoCodeItemNum>").append(AutoCodeItemNum).append("</AutoCodeItemNum>");
			sb.append("<Cond>").append(Cond.ordinal()).append("</Cond>");
			sb.append("</AutoCodeCond>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"AutoCodeCondNum")!=null) {
					AutoCodeCondNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AutoCodeCondNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AutoCodeItemNum")!=null) {
					AutoCodeItemNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AutoCodeItemNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Cond")!=null) {
					Cond=AutoCondition.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Cond"))];
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum AutoCondition {
			/** 0 */
			Anterior,
			/** 1 */
			Posterior,
			/** 2 */
			Premolar,
			/** 3 */
			Molar,
			/** 4 */
			One_Surf,
			/** 5 */
			Two_Surf,
			/** 6 */
			Three_Surf,
			/** 7 */
			Four_Surf,
			/** 8 */
			Five_Surf,
			/** 9 */
			First,
			/** 10 */
			EachAdditional,
			/** 11 */
			Maxillary,
			/** 12 */
			Mandibular,
			/** 13 */
			Primary,
			/** 14 */
			Permanent,
			/** 15 */
			Pontic,
			/** 16 */
			Retainer,
			/** 17 */
			AgeOver18
		}


}
