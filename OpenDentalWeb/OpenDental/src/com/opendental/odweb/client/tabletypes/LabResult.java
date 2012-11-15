package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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
			sb.append("<DateTimeTest>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateTimeTest>");
			sb.append("<TestName>").append(Serializing.EscapeForXml(TestName)).append("</TestName>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateTStamp>");
			sb.append("<TestID>").append(Serializing.EscapeForXml(TestID)).append("</TestID>");
			sb.append("<ObsValue>").append(Serializing.EscapeForXml(ObsValue)).append("</ObsValue>");
			sb.append("<ObsUnits>").append(Serializing.EscapeForXml(ObsUnits)).append("</ObsUnits>");
			sb.append("<ObsRange>").append(Serializing.EscapeForXml(ObsRange)).append("</ObsRange>");
			sb.append("<AbnormalFlag>").append(AbnormalFlag.ordinal()).append("</AbnormalFlag>");
			sb.append("</LabResult>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				LabResultNum=Integer.valueOf(doc.getElementsByTagName("LabResultNum").item(0).getFirstChild().getNodeValue());
				LabPanelNum=Integer.valueOf(doc.getElementsByTagName("LabPanelNum").item(0).getFirstChild().getNodeValue());
				DateTimeTest=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTimeTest").item(0).getFirstChild().getNodeValue());
				TestName=doc.getElementsByTagName("TestName").item(0).getFirstChild().getNodeValue();
				DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue());
				TestID=doc.getElementsByTagName("TestID").item(0).getFirstChild().getNodeValue();
				ObsValue=doc.getElementsByTagName("ObsValue").item(0).getFirstChild().getNodeValue();
				ObsUnits=doc.getElementsByTagName("ObsUnits").item(0).getFirstChild().getNodeValue();
				ObsRange=doc.getElementsByTagName("ObsRange").item(0).getFirstChild().getNodeValue();
				AbnormalFlag=LabAbnormalFlag.values()[Integer.valueOf(doc.getElementsByTagName("AbnormalFlag").item(0).getFirstChild().getNodeValue())];
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
