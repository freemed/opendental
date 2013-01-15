package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
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
		public LabResult deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<LabResult>");
			sb.append("<LabResultNum>").append(LabResultNum).append("</LabResultNum>");
			sb.append("<LabPanelNum>").append(LabPanelNum).append("</LabPanelNum>");
			sb.append("<DateTimeTest>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeTest)).append("</DateTimeTest>");
			sb.append("<TestName>").append(Serializing.escapeForXml(TestName)).append("</TestName>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<TestID>").append(Serializing.escapeForXml(TestID)).append("</TestID>");
			sb.append("<ObsValue>").append(Serializing.escapeForXml(ObsValue)).append("</ObsValue>");
			sb.append("<ObsUnits>").append(Serializing.escapeForXml(ObsUnits)).append("</ObsUnits>");
			sb.append("<ObsRange>").append(Serializing.escapeForXml(ObsRange)).append("</ObsRange>");
			sb.append("<AbnormalFlag>").append(AbnormalFlag.ordinal()).append("</AbnormalFlag>");
			sb.append("</LabResult>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"LabResultNum")!=null) {
					LabResultNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"LabResultNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"LabPanelNum")!=null) {
					LabPanelNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"LabPanelNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeTest")!=null) {
					DateTimeTest=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeTest"));
				}
				if(Serializing.getXmlNodeValue(doc,"TestName")!=null) {
					TestName=Serializing.getXmlNodeValue(doc,"TestName");
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"TestID")!=null) {
					TestID=Serializing.getXmlNodeValue(doc,"TestID");
				}
				if(Serializing.getXmlNodeValue(doc,"ObsValue")!=null) {
					ObsValue=Serializing.getXmlNodeValue(doc,"ObsValue");
				}
				if(Serializing.getXmlNodeValue(doc,"ObsUnits")!=null) {
					ObsUnits=Serializing.getXmlNodeValue(doc,"ObsUnits");
				}
				if(Serializing.getXmlNodeValue(doc,"ObsRange")!=null) {
					ObsRange=Serializing.getXmlNodeValue(doc,"ObsRange");
				}
				if(Serializing.getXmlNodeValue(doc,"AbnormalFlag")!=null) {
					AbnormalFlag=LabAbnormalFlag.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"AbnormalFlag"))];
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
