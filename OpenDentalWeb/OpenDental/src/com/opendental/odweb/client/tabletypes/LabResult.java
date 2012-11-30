package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class LabResult {
		/** Primary key. */
		public int LabResultNum;
		/** FK to labpanel.LabPanelNum. */
		public int LabPanelNum;
		/** OBX-14. */
		public Date DateTimeTest;
		/** OBX-3-1, text portion. */
		public String TestName;
		/** To be used for synch with web server. */
		public Date DateTStamp;
		/** OBX-3-0, id portion, LOINC.  For example, 10676-5. */
		public String TestID;
		/** OBX-5. Value always stored as a string because the type might vary in the future. */
		public String ObsValue;
		/** OBX-6  For example, mL.  Was FK to drugunit.DrugUnitNum, but that would make reliable import problematic, so now it's just text. */
		public String ObsUnits;
		/** OBX-7  For example, <200 or >=40. */
		public String ObsRange;
		/** Enum:LabAbnormalFlag 0-None, 1-Below, 2-Normal, 3-Above. */
		public LabAbnormalFlag AbnormalFlag;

		/** Deep copy of object. */
		public LabResult Copy() {
			LabResult labresult=new LabResult();
			labresult.LabResultNum=this.LabResultNum;
			labresult.LabPanelNum=this.LabPanelNum;
			labresult.DateTimeTest=this.DateTimeTest;
			labresult.TestName=this.TestName;
			labresult.DateTStamp=this.DateTStamp;
			labresult.TestID=this.TestID;
			labresult.ObsValue=this.ObsValue;
			labresult.ObsUnits=this.ObsUnits;
			labresult.ObsRange=this.ObsRange;
			labresult.AbnormalFlag=this.AbnormalFlag;
			return labresult;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<LabResult>");
			sb.append("<LabResultNum>").append(LabResultNum).append("</LabResultNum>");
			sb.append("<LabPanelNum>").append(LabPanelNum).append("</LabPanelNum>");
			sb.append("<DateTimeTest>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeTest)).append("</DateTimeTest>");
			sb.append("<TestName>").append(Serializing.EscapeForXml(TestName)).append("</TestName>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<TestID>").append(Serializing.EscapeForXml(TestID)).append("</TestID>");
			sb.append("<ObsValue>").append(Serializing.EscapeForXml(ObsValue)).append("</ObsValue>");
			sb.append("<ObsUnits>").append(Serializing.EscapeForXml(ObsUnits)).append("</ObsUnits>");
			sb.append("<ObsRange>").append(Serializing.EscapeForXml(ObsRange)).append("</ObsRange>");
			sb.append("<AbnormalFlag>").append(AbnormalFlag.ordinal()).append("</AbnormalFlag>");
			sb.append("</LabResult>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"LabResultNum")!=null) {
					LabResultNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"LabResultNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"LabPanelNum")!=null) {
					LabPanelNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"LabPanelNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTimeTest")!=null) {
					DateTimeTest=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTimeTest"));
				}
				if(Serializing.GetXmlNodeValue(doc,"TestName")!=null) {
					TestName=Serializing.GetXmlNodeValue(doc,"TestName");
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.GetXmlNodeValue(doc,"TestID")!=null) {
					TestID=Serializing.GetXmlNodeValue(doc,"TestID");
				}
				if(Serializing.GetXmlNodeValue(doc,"ObsValue")!=null) {
					ObsValue=Serializing.GetXmlNodeValue(doc,"ObsValue");
				}
				if(Serializing.GetXmlNodeValue(doc,"ObsUnits")!=null) {
					ObsUnits=Serializing.GetXmlNodeValue(doc,"ObsUnits");
				}
				if(Serializing.GetXmlNodeValue(doc,"ObsRange")!=null) {
					ObsRange=Serializing.GetXmlNodeValue(doc,"ObsRange");
				}
				if(Serializing.GetXmlNodeValue(doc,"AbnormalFlag")!=null) {
					AbnormalFlag=LabAbnormalFlag.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AbnormalFlag"))];
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum LabAbnormalFlag {
			/** 0-No value. */
			None,
			/** 1-Below normal. */
			Below,
			/** 2-Normal. */
			Normal,
			/** 3-Above high normal. */
			Above
		}


}
