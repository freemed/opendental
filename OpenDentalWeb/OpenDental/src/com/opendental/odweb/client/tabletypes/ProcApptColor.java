package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"ProcApptColorNum")!=null) {
					ProcApptColorNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProcApptColorNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CodeRange")!=null) {
					CodeRange=Serializing.GetXmlNodeValue(doc,"CodeRange");
				}
				if(Serializing.GetXmlNodeValue(doc,"ShowPreviousDate")!=null) {
					ShowPreviousDate=(Serializing.GetXmlNodeValue(doc,"ShowPreviousDate")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ColorText")!=null) {
					ColorText=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ColorText"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}