package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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
		public EduResource deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EduResource>");
			sb.append("<EduResourceNum>").append(EduResourceNum).append("</EduResourceNum>");
			sb.append("<DiseaseDefNum>").append(DiseaseDefNum).append("</DiseaseDefNum>");
			sb.append("<MedicationNum>").append(MedicationNum).append("</MedicationNum>");
			sb.append("<LabResultID>").append(Serializing.escapeForXml(LabResultID)).append("</LabResultID>");
			sb.append("<LabResultName>").append(Serializing.escapeForXml(LabResultName)).append("</LabResultName>");
			sb.append("<LabResultCompare>").append(Serializing.escapeForXml(LabResultCompare)).append("</LabResultCompare>");
			sb.append("<ResourceUrl>").append(Serializing.escapeForXml(ResourceUrl)).append("</ResourceUrl>");
			sb.append("<Icd9Num>").append(Icd9Num).append("</Icd9Num>");
			sb.append("</EduResource>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"EduResourceNum")!=null) {
					EduResourceNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EduResourceNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DiseaseDefNum")!=null) {
					DiseaseDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DiseaseDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"MedicationNum")!=null) {
					MedicationNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MedicationNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"LabResultID")!=null) {
					LabResultID=Serializing.getXmlNodeValue(doc,"LabResultID");
				}
				if(Serializing.getXmlNodeValue(doc,"LabResultName")!=null) {
					LabResultName=Serializing.getXmlNodeValue(doc,"LabResultName");
				}
				if(Serializing.getXmlNodeValue(doc,"LabResultCompare")!=null) {
					LabResultCompare=Serializing.getXmlNodeValue(doc,"LabResultCompare");
				}
				if(Serializing.getXmlNodeValue(doc,"ResourceUrl")!=null) {
					ResourceUrl=Serializing.getXmlNodeValue(doc,"ResourceUrl");
				}
				if(Serializing.getXmlNodeValue(doc,"Icd9Num")!=null) {
					Icd9Num=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Icd9Num"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
