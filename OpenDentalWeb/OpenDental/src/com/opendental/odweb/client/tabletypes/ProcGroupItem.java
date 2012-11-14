package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ProcGroupItem {
		/** Primary key. */
		public int ProcGroupItemNum;
		/** FK to procedurelog.ProcNum. */
		public int ProcNum;
		/** FK to procedurelog.ProcNum. */
		public int GroupNum;

		/** Deep copy of object. */
		public ProcGroupItem Copy() {
			ProcGroupItem procgroupitem=new ProcGroupItem();
			procgroupitem.ProcGroupItemNum=this.ProcGroupItemNum;
			procgroupitem.ProcNum=this.ProcNum;
			procgroupitem.GroupNum=this.GroupNum;
			return procgroupitem;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProcGroupItem>");
			sb.append("<ProcGroupItemNum>").append(ProcGroupItemNum).append("</ProcGroupItemNum>");
			sb.append("<ProcNum>").append(ProcNum).append("</ProcNum>");
			sb.append("<GroupNum>").append(GroupNum).append("</GroupNum>");
			sb.append("</ProcGroupItem>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ProcGroupItemNum=Integer.valueOf(doc.getElementsByTagName("ProcGroupItemNum").item(0).getFirstChild().getNodeValue());
				ProcNum=Integer.valueOf(doc.getElementsByTagName("ProcNum").item(0).getFirstChild().getNodeValue());
				GroupNum=Integer.valueOf(doc.getElementsByTagName("GroupNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
