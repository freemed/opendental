package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class Fee {
		/** Primary key. */
		public int FeeNum;
		/** The amount usually charged.  If an amount is unknown, then the entire Fee entry is deleted from the database.  The absence of a fee is sometimes shown in the user interface as a blank entry, and sometimes as 0.00. */
		public double Amount;
		/** Do not use. */
		public String OldCode;
		/** FK to feesched.FeeSchedNum. */
		public int FeeSched;
		/** Not used. */
		public boolean UseDefaultFee;
		/** Not used. */
		public boolean UseDefaultCov;
		/** FK to procedurecode.CodeNum. */
		public int CodeNum;

		/** Deep copy of object. */
		public Fee Copy() {
			Fee fee=new Fee();
			fee.FeeNum=this.FeeNum;
			fee.Amount=this.Amount;
			fee.OldCode=this.OldCode;
			fee.FeeSched=this.FeeSched;
			fee.UseDefaultFee=this.UseDefaultFee;
			fee.UseDefaultCov=this.UseDefaultCov;
			fee.CodeNum=this.CodeNum;
			return fee;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Fee>");
			sb.append("<FeeNum>").append(FeeNum).append("</FeeNum>");
			sb.append("<Amount>").append(Amount).append("</Amount>");
			sb.append("<OldCode>").append(Serializing.EscapeForXml(OldCode)).append("</OldCode>");
			sb.append("<FeeSched>").append(FeeSched).append("</FeeSched>");
			sb.append("<UseDefaultFee>").append((UseDefaultFee)?1:0).append("</UseDefaultFee>");
			sb.append("<UseDefaultCov>").append((UseDefaultCov)?1:0).append("</UseDefaultCov>");
			sb.append("<CodeNum>").append(CodeNum).append("</CodeNum>");
			sb.append("</Fee>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"FeeNum")!=null) {
					FeeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"FeeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Amount")!=null) {
					Amount=Double.valueOf(Serializing.GetXmlNodeValue(doc,"Amount"));
				}
				if(Serializing.GetXmlNodeValue(doc,"OldCode")!=null) {
					OldCode=Serializing.GetXmlNodeValue(doc,"OldCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"FeeSched")!=null) {
					FeeSched=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"FeeSched"));
				}
				if(Serializing.GetXmlNodeValue(doc,"UseDefaultFee")!=null) {
					UseDefaultFee=(Serializing.GetXmlNodeValue(doc,"UseDefaultFee")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"UseDefaultCov")!=null) {
					UseDefaultCov=(Serializing.GetXmlNodeValue(doc,"UseDefaultCov")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"CodeNum")!=null) {
					CodeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CodeNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
