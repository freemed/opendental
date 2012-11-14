package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ProcApptColor {
		/** Primary key. */
		public int ProcApptColorNum;
		/** Procedure code range defined by user.  Includes commas and dashes, but no spaces.  The codes need not be valid since they are ranges. */
		public String CodeRange;
		/** Adds most recent completed date to ProcsColored */
		public boolean ShowPreviousDate;
		/** Color that shows in appointments */
		public int ColorText;

		/** Deep copy of object. */
		public ProcApptColor Copy() {
			ProcApptColor procapptcolor=new ProcApptColor();
			procapptcolor.ProcApptColorNum=this.ProcApptColorNum;
			procapptcolor.CodeRange=this.CodeRange;
			procapptcolor.ShowPreviousDate=this.ShowPreviousDate;
			procapptcolor.ColorText=this.ColorText;
			return procapptcolor;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProcApptColor>");
			sb.append("<ProcApptColorNum>").append(ProcApptColorNum).append("</ProcApptColorNum>");
			sb.append("<CodeRange>").append(Serializing.EscapeForXml(CodeRange)).append("</CodeRange>");
			sb.append("<ShowPreviousDate>").append((ShowPreviousDate)?1:0).append("</ShowPreviousDate>");
			sb.append("<ColorText>").append(ColorText).append("</ColorText>");
			sb.append("</ProcApptColor>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ProcApptColorNum=Integer.valueOf(doc.getElementsByTagName("ProcApptColorNum").item(0).getFirstChild().getNodeValue());
				CodeRange=doc.getElementsByTagName("CodeRange").item(0).getFirstChild().getNodeValue();
				ShowPreviousDate=(doc.getElementsByTagName("ShowPreviousDate").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				ColorText=Integer.valueOf(doc.getElementsByTagName("ColorText").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
