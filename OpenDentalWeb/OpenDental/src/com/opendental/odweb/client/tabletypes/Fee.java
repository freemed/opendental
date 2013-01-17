package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public Fee deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Fee>");
			sb.append("<FeeNum>").append(FeeNum).append("</FeeNum>");
			sb.append("<Amount>").append(Amount).append("</Amount>");
			sb.append("<OldCode>").append(Serializing.escapeForXml(OldCode)).append("</OldCode>");
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
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"FeeNum")!=null) {
					FeeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FeeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Amount")!=null) {
					Amount=Double.valueOf(Serializing.getXmlNodeValue(doc,"Amount"));
				}
				if(Serializing.getXmlNodeValue(doc,"OldCode")!=null) {
					OldCode=Serializing.getXmlNodeValue(doc,"OldCode");
				}
				if(Serializing.getXmlNodeValue(doc,"FeeSched")!=null) {
					FeeSched=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FeeSched"));
				}
				if(Serializing.getXmlNodeValue(doc,"UseDefaultFee")!=null) {
					UseDefaultFee=(Serializing.getXmlNodeValue(doc,"UseDefaultFee")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"UseDefaultCov")!=null) {
					UseDefaultCov=(Serializing.getXmlNodeValue(doc,"UseDefaultCov")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"CodeNum")!=null) {
					CodeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CodeNum"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Fee: "+e.getMessage());
			}
		}


}
