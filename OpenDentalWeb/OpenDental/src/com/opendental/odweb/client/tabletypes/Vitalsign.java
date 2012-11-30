package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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
			sb.append("<DateTaken>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTaken)).append("</DateTaken>");
			sb.append("<HasFollowupPlan>").append((HasFollowupPlan)?1:0).append("</HasFollowupPlan>");
			sb.append("<IsIneligible>").append((IsIneligible)?1:0).append("</IsIneligible>");
			sb.append("<Documentation>").append(Serializing.EscapeForXml(Documentation)).append("</Documentation>");
			sb.append("<ChildGotNutrition>").append((ChildGotNutrition)?1:0).append("</ChildGotNutrition>");
			sb.append("<ChildGotPhysCouns>").append((ChildGotPhysCouns)?1:0).append("</ChildGotPhysCouns>");
			sb.append("</Vitalsign>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"VitalsignNum")!=null) {
					VitalsignNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"VitalsignNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Height")!=null) {
					Height=Float.valueOf(Serializing.GetXmlNodeValue(doc,"Height"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Weight")!=null) {
					Weight=Float.valueOf(Serializing.GetXmlNodeValue(doc,"Weight"));
				}
				if(Serializing.GetXmlNodeValue(doc,"BpSystolic")!=null) {
					BpSystolic=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"BpSystolic"));
				}
				if(Serializing.GetXmlNodeValue(doc,"BpDiastolic")!=null) {
					BpDiastolic=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"BpDiastolic"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTaken")!=null) {
					DateTaken=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTaken"));
				}
				if(Serializing.GetXmlNodeValue(doc,"HasFollowupPlan")!=null) {
					HasFollowupPlan=(Serializing.GetXmlNodeValue(doc,"HasFollowupPlan")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"IsIneligible")!=null) {
					IsIneligible=(Serializing.GetXmlNodeValue(doc,"IsIneligible")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"Documentation")!=null) {
					Documentation=Serializing.GetXmlNodeValue(doc,"Documentation");
				}
				if(Serializing.GetXmlNodeValue(doc,"ChildGotNutrition")!=null) {
					ChildGotNutrition=(Serializing.GetXmlNodeValue(doc,"ChildGotNutrition")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ChildGotPhysCouns")!=null) {
					ChildGotPhysCouns=(Serializing.GetXmlNodeValue(doc,"ChildGotPhysCouns")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
