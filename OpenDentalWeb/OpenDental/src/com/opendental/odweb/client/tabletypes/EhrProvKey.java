package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public EhrProvKey Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EhrProvKey>");
			sb.append("<EhrProvKeyNum>").append(EhrProvKeyNum).append("</EhrProvKeyNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<LName>").append(Serializing.EscapeForXml(LName)).append("</LName>");
			sb.append("<FName>").append(Serializing.EscapeForXml(FName)).append("</FName>");
			sb.append("<ProvKey>").append(Serializing.EscapeForXml(ProvKey)).append("</ProvKey>");
			sb.append("<FullTimeEquiv>").append(FullTimeEquiv).append("</FullTimeEquiv>");
			sb.append("<Notes>").append(Serializing.EscapeForXml(Notes)).append("</Notes>");
			sb.append("<HasReportAccess>").append((HasReportAccess)?1:0).append("</HasReportAccess>");
			sb.append("</EhrProvKey>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"EhrProvKeyNum")!=null) {
					EhrProvKeyNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EhrProvKeyNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"LName")!=null) {
					LName=Serializing.GetXmlNodeValue(doc,"LName");
				}
				if(Serializing.GetXmlNodeValue(doc,"FName")!=null) {
					FName=Serializing.GetXmlNodeValue(doc,"FName");
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvKey")!=null) {
					ProvKey=Serializing.GetXmlNodeValue(doc,"ProvKey");
				}
				if(Serializing.GetXmlNodeValue(doc,"FullTimeEquiv")!=null) {
					FullTimeEquiv=Float.valueOf(Serializing.GetXmlNodeValue(doc,"FullTimeEquiv"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Notes")!=null) {
					Notes=Serializing.GetXmlNodeValue(doc,"Notes");
				}
				if(Serializing.GetXmlNodeValue(doc,"HasReportAccess")!=null) {
					HasReportAccess=(Serializing.GetXmlNodeValue(doc,"HasReportAccess")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
