package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class EduResource {
		/** Primary key. */
		public int EduResourceNum;
		/** FK to diseasedef.DiseaseDefNum.  */
		public int DiseaseDefNum;
		/** FK to medication.MedicationNum.  */
		public int MedicationNum;
		/** FK to labresult.TestID.  */
		public String LabResultID;
		/** Used for display in the grid. */
		public String LabResultName;
		/** String, example <43. Must start with < or > followed by int.  Only used if FK LabResultID is used. */
		public String LabResultCompare;
		/** . */
		public String ResourceUrl;
		/** FK to icd9.ICD9Num. */
		public int Icd9Num;

		/** Deep copy of object. */
		public EduResource Copy() {
			EduResource eduresource=new EduResource();
			eduresource.EduResourceNum=this.EduResourceNum;
			eduresource.DiseaseDefNum=this.DiseaseDefNum;
			eduresource.MedicationNum=this.MedicationNum;
			eduresource.LabResultID=this.LabResultID;
			eduresource.LabResultName=this.LabResultName;
			eduresource.LabResultCompare=this.LabResultCompare;
			eduresource.ResourceUrl=this.ResourceUrl;
			eduresource.Icd9Num=this.Icd9Num;
			return eduresource;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EduResource>");
			sb.append("<EduResourceNum>").append(EduResourceNum).append("</EduResourceNum>");
			sb.append("<DiseaseDefNum>").append(DiseaseDefNum).append("</DiseaseDefNum>");
			sb.append("<MedicationNum>").append(MedicationNum).append("</MedicationNum>");
			sb.append("<LabResultID>").append(Serializing.EscapeForXml(LabResultID)).append("</LabResultID>");
			sb.append("<LabResultName>").append(Serializing.EscapeForXml(LabResultName)).append("</LabResultName>");
			sb.append("<LabResultCompare>").append(Serializing.EscapeForXml(LabResultCompare)).append("</LabResultCompare>");
			sb.append("<ResourceUrl>").append(Serializing.EscapeForXml(ResourceUrl)).append("</ResourceUrl>");
			sb.append("<Icd9Num>").append(Icd9Num).append("</Icd9Num>");
			sb.append("</EduResource>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				EduResourceNum=Integer.valueOf(doc.getElementsByTagName("EduResourceNum").item(0).getFirstChild().getNodeValue());
				DiseaseDefNum=Integer.valueOf(doc.getElementsByTagName("DiseaseDefNum").item(0).getFirstChild().getNodeValue());
				MedicationNum=Integer.valueOf(doc.getElementsByTagName("MedicationNum").item(0).getFirstChild().getNodeValue());
				LabResultID=doc.getElementsByTagName("LabResultID").item(0).getFirstChild().getNodeValue();
				LabResultName=doc.getElementsByTagName("LabResultName").item(0).getFirstChild().getNodeValue();
				LabResultCompare=doc.getElementsByTagName("LabResultCompare").item(0).getFirstChild().getNodeValue();
				ResourceUrl=doc.getElementsByTagName("ResourceUrl").item(0).getFirstChild().getNodeValue();
				Icd9Num=Integer.valueOf(doc.getElementsByTagName("Icd9Num").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
