package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Vitalsign {
		/** Primary key. */
		public int VitalsignNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** Height of patient in inches. Fractions might be needed some day.  Allowed to be 0. */
		public float Height;
		/** Lbs.  Allowed to be 0. */
		public float Weight;
		/** Allowed to be 0. */
		public int BpSystolic;
		/** Allowed to be 0. */
		public int BpDiastolic;
		/** The date that the vitalsigns were taken. */
		public Date DateTaken;
		/** For an abnormal BMI measurement this must be true in order to meet quality measurement. */
		public boolean HasFollowupPlan;
		/** If a BMI was not recored, this must be true in order to meet quality measurement.  For children, this is used as an IsPregnant flag, the only valid reason for not taking BMI on children. */
		public boolean IsIneligible;
		/** For HasFollowupPlan or IsIneligible, this documents the specifics. */
		public String Documentation;
		/** . */
		public boolean ChildGotNutrition;
		/** . */
		public boolean ChildGotPhysCouns;

		/** Deep copy of object. */
		public Vitalsign Copy() {
			Vitalsign vitalsign=new Vitalsign();
			vitalsign.VitalsignNum=this.VitalsignNum;
			vitalsign.PatNum=this.PatNum;
			vitalsign.Height=this.Height;
			vitalsign.Weight=this.Weight;
			vitalsign.BpSystolic=this.BpSystolic;
			vitalsign.BpDiastolic=this.BpDiastolic;
			vitalsign.DateTaken=this.DateTaken;
			vitalsign.HasFollowupPlan=this.HasFollowupPlan;
			vitalsign.IsIneligible=this.IsIneligible;
			vitalsign.Documentation=this.Documentation;
			vitalsign.ChildGotNutrition=this.ChildGotNutrition;
			vitalsign.ChildGotPhysCouns=this.ChildGotPhysCouns;
			return vitalsign;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Vitalsign>");
			sb.append("<VitalsignNum>").append(VitalsignNum).append("</VitalsignNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<Height>").append(Height).append("</Height>");
			sb.append("<Weight>").append(Weight).append("</Weight>");
			sb.append("<BpSystolic>").append(BpSystolic).append("</BpSystolic>");
			sb.append("<BpDiastolic>").append(BpDiastolic).append("</BpDiastolic>");
			sb.append("<DateTaken>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateTaken>");
			sb.append("<HasFollowupPlan>").append((HasFollowupPlan)?1:0).append("</HasFollowupPlan>");
			sb.append("<IsIneligible>").append((IsIneligible)?1:0).append("</IsIneligible>");
			sb.append("<Documentation>").append(Serializing.EscapeForXml(Documentation)).append("</Documentation>");
			sb.append("<ChildGotNutrition>").append((ChildGotNutrition)?1:0).append("</ChildGotNutrition>");
			sb.append("<ChildGotPhysCouns>").append((ChildGotPhysCouns)?1:0).append("</ChildGotPhysCouns>");
			sb.append("</Vitalsign>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				VitalsignNum=Integer.valueOf(doc.getElementsByTagName("VitalsignNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				Height=Float.valueOf(doc.getElementsByTagName("Height").item(0).getFirstChild().getNodeValue());
				Weight=Float.valueOf(doc.getElementsByTagName("Weight").item(0).getFirstChild().getNodeValue());
				BpSystolic=Integer.valueOf(doc.getElementsByTagName("BpSystolic").item(0).getFirstChild().getNodeValue());
				BpDiastolic=Integer.valueOf(doc.getElementsByTagName("BpDiastolic").item(0).getFirstChild().getNodeValue());
				DateTaken=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTaken").item(0).getFirstChild().getNodeValue());
				HasFollowupPlan=(doc.getElementsByTagName("HasFollowupPlan").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				IsIneligible=(doc.getElementsByTagName("IsIneligible").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				Documentation=doc.getElementsByTagName("Documentation").item(0).getFirstChild().getNodeValue();
				ChildGotNutrition=(doc.getElementsByTagName("ChildGotNutrition").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				ChildGotPhysCouns=(doc.getElementsByTagName("ChildGotPhysCouns").item(0).getFirstChild().getNodeValue()=="0")?false:true;
			}
			catch(Exception e) {
				throw e;
			}
		}


}
