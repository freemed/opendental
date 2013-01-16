package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class EhrProvKey {
		/** Primary key. */
		public int EhrProvKeyNum;
		/** FK to patient.PatNum. There can be multiple EhrProvKeys per patient/customer. */
		public int PatNum;
		/** The provider LName. */
		public String LName;
		/** The provider FName. */
		public String FName;
		/** The key assigned to the provider */
		public String ProvKey;
		/** Usually 1.  Can be less, like .5 or .25 to indicate possible discount is justified. */
		public float FullTimeEquiv;
		/** Any notes that the tech wishes to include regarding this situation. */
		public String Notes;
		/** True if the provider has access to the reports needed to show MU.  Changing this will require a new provider key. */
		public boolean HasReportAccess;

		/** Deep copy of object. */
		public EhrProvKey deepCopy() {
			EhrProvKey ehrprovkey=new EhrProvKey();
			ehrprovkey.EhrProvKeyNum=this.EhrProvKeyNum;
			ehrprovkey.PatNum=this.PatNum;
			ehrprovkey.LName=this.LName;
			ehrprovkey.FName=this.FName;
			ehrprovkey.ProvKey=this.ProvKey;
			ehrprovkey.FullTimeEquiv=this.FullTimeEquiv;
			ehrprovkey.Notes=this.Notes;
			ehrprovkey.HasReportAccess=this.HasReportAccess;
			return ehrprovkey;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EhrProvKey>");
			sb.append("<EhrProvKeyNum>").append(EhrProvKeyNum).append("</EhrProvKeyNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<LName>").append(Serializing.escapeForXml(LName)).append("</LName>");
			sb.append("<FName>").append(Serializing.escapeForXml(FName)).append("</FName>");
			sb.append("<ProvKey>").append(Serializing.escapeForXml(ProvKey)).append("</ProvKey>");
			sb.append("<FullTimeEquiv>").append(FullTimeEquiv).append("</FullTimeEquiv>");
			sb.append("<Notes>").append(Serializing.escapeForXml(Notes)).append("</Notes>");
			sb.append("<HasReportAccess>").append((HasReportAccess)?1:0).append("</HasReportAccess>");
			sb.append("</EhrProvKey>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"EhrProvKeyNum")!=null) {
					EhrProvKeyNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EhrProvKeyNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"LName")!=null) {
					LName=Serializing.getXmlNodeValue(doc,"LName");
				}
				if(Serializing.getXmlNodeValue(doc,"FName")!=null) {
					FName=Serializing.getXmlNodeValue(doc,"FName");
				}
				if(Serializing.getXmlNodeValue(doc,"ProvKey")!=null) {
					ProvKey=Serializing.getXmlNodeValue(doc,"ProvKey");
				}
				if(Serializing.getXmlNodeValue(doc,"FullTimeEquiv")!=null) {
					FullTimeEquiv=Float.valueOf(Serializing.getXmlNodeValue(doc,"FullTimeEquiv"));
				}
				if(Serializing.getXmlNodeValue(doc,"Notes")!=null) {
					Notes=Serializing.getXmlNodeValue(doc,"Notes");
				}
				if(Serializing.getXmlNodeValue(doc,"HasReportAccess")!=null) {
					HasReportAccess=(Serializing.getXmlNodeValue(doc,"HasReportAccess")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
