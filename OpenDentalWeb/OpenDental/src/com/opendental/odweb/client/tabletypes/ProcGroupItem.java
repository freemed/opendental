package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class ProcGroupItem {
		/** Primary key. */
		public int ProcGroupItemNum;
		/** FK to procedurelog.ProcNum. */
		public int ProcNum;
		/** FK to procedurelog.ProcNum. */
		public int GroupNum;

		/** Deep copy of object. */
		public ProcGroupItem deepCopy() {
			ProcGroupItem procgroupitem=new ProcGroupItem();
			procgroupitem.ProcGroupItemNum=this.ProcGroupItemNum;
			procgroupitem.ProcNum=this.ProcNum;
			procgroupitem.GroupNum=this.GroupNum;
			return procgroupitem;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProcGroupItem>");
			sb.append("<ProcGroupItemNum>").append(ProcGroupItemNum).append("</ProcGroupItemNum>");
			sb.append("<ProcNum>").append(ProcNum).append("</ProcNum>");
			sb.append("<GroupNum>").append(GroupNum).append("</GroupNum>");
			sb.append("</ProcGroupItem>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ProcGroupItemNum")!=null) {
					ProcGroupItemNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcGroupItemNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProcNum")!=null) {
					ProcNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"GroupNum")!=null) {
					GroupNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"GroupNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
