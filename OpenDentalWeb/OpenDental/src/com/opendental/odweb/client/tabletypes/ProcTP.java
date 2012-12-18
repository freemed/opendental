package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class ProcTP {
		/** Primary key. */
		public int ProcTPNum;
		/** FK to treatplan.TreatPlanNum.  The treatment plan to which this proc is attached. */
		public int TreatPlanNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** FK to procedurelog.ProcNum.  It is very common for the referenced procedure to be missing.  This procNum is only here to compare and test the existence of the referenced procedure.  If present, it will check to see whether the procedure is still status TP. */
		public int ProcNumOrig;
		/** The order of this proc within its tp.  This is set when the tp is first created and can't be changed.  Drastically simplifies loading the tp. */
		public int ItemOrder;
		/** FK to definition.DefNum which contains the text of the priority. */
		public int Priority;
		/** A simple string displaying the tooth number.  If international tooth numbers are used, then this will be in international format already. */
		public String ToothNumTP;
		/** Tooth surfaces or area.  This is already converted for international use.  If arch or quad, then it will have U,LR, etc. */
		public String Surf;
		/** Not a foreign key.  Simply display text.  Can be changed by user at any time. */
		public String ProcCode;
		/** Description is originally copied from procedurecode.Descript, but user can change it. */
		public String Descript;
		/** The fee charged to the patient. Never gets automatically updated. */
		public double FeeAmt;
		/** The amount primary insurance is expected to pay. Never gets automatically updated. */
		public double PriInsAmt;
		/** The amount secondary insurance is expected to pay. Never gets automatically updated. */
		public double SecInsAmt;
		/** The amount the patient is expected to pay. Never gets automatically updated. */
		public double PatAmt;
		/** The amount of discount.  Currently only used for PPOs. */
		public double Discount;
		/** Text from prognosis definition.  Can be changed by user at any time. */
		public String Prognosis;
		/** Text from diagnosis definition.  Can be changed by user at any time. */
		public String Dx;

		/** Deep copy of object. */
		public ProcTP deepCopy() {
			ProcTP proctp=new ProcTP();
			proctp.ProcTPNum=this.ProcTPNum;
			proctp.TreatPlanNum=this.TreatPlanNum;
			proctp.PatNum=this.PatNum;
			proctp.ProcNumOrig=this.ProcNumOrig;
			proctp.ItemOrder=this.ItemOrder;
			proctp.Priority=this.Priority;
			proctp.ToothNumTP=this.ToothNumTP;
			proctp.Surf=this.Surf;
			proctp.ProcCode=this.ProcCode;
			proctp.Descript=this.Descript;
			proctp.FeeAmt=this.FeeAmt;
			proctp.PriInsAmt=this.PriInsAmt;
			proctp.SecInsAmt=this.SecInsAmt;
			proctp.PatAmt=this.PatAmt;
			proctp.Discount=this.Discount;
			proctp.Prognosis=this.Prognosis;
			proctp.Dx=this.Dx;
			return proctp;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProcTP>");
			sb.append("<ProcTPNum>").append(ProcTPNum).append("</ProcTPNum>");
			sb.append("<TreatPlanNum>").append(TreatPlanNum).append("</TreatPlanNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ProcNumOrig>").append(ProcNumOrig).append("</ProcNumOrig>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<Priority>").append(Priority).append("</Priority>");
			sb.append("<ToothNumTP>").append(Serializing.escapeForXml(ToothNumTP)).append("</ToothNumTP>");
			sb.append("<Surf>").append(Serializing.escapeForXml(Surf)).append("</Surf>");
			sb.append("<ProcCode>").append(Serializing.escapeForXml(ProcCode)).append("</ProcCode>");
			sb.append("<Descript>").append(Serializing.escapeForXml(Descript)).append("</Descript>");
			sb.append("<FeeAmt>").append(FeeAmt).append("</FeeAmt>");
			sb.append("<PriInsAmt>").append(PriInsAmt).append("</PriInsAmt>");
			sb.append("<SecInsAmt>").append(SecInsAmt).append("</SecInsAmt>");
			sb.append("<PatAmt>").append(PatAmt).append("</PatAmt>");
			sb.append("<Discount>").append(Discount).append("</Discount>");
			sb.append("<Prognosis>").append(Serializing.escapeForXml(Prognosis)).append("</Prognosis>");
			sb.append("<Dx>").append(Serializing.escapeForXml(Dx)).append("</Dx>");
			sb.append("</ProcTP>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ProcTPNum")!=null) {
					ProcTPNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcTPNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"TreatPlanNum")!=null) {
					TreatPlanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TreatPlanNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProcNumOrig")!=null) {
					ProcNumOrig=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcNumOrig"));
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"Priority")!=null) {
					Priority=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Priority"));
				}
				if(Serializing.getXmlNodeValue(doc,"ToothNumTP")!=null) {
					ToothNumTP=Serializing.getXmlNodeValue(doc,"ToothNumTP");
				}
				if(Serializing.getXmlNodeValue(doc,"Surf")!=null) {
					Surf=Serializing.getXmlNodeValue(doc,"Surf");
				}
				if(Serializing.getXmlNodeValue(doc,"ProcCode")!=null) {
					ProcCode=Serializing.getXmlNodeValue(doc,"ProcCode");
				}
				if(Serializing.getXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.getXmlNodeValue(doc,"Descript");
				}
				if(Serializing.getXmlNodeValue(doc,"FeeAmt")!=null) {
					FeeAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"FeeAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"PriInsAmt")!=null) {
					PriInsAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"PriInsAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"SecInsAmt")!=null) {
					SecInsAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"SecInsAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatAmt")!=null) {
					PatAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"PatAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"Discount")!=null) {
					Discount=Double.valueOf(Serializing.getXmlNodeValue(doc,"Discount"));
				}
				if(Serializing.getXmlNodeValue(doc,"Prognosis")!=null) {
					Prognosis=Serializing.getXmlNodeValue(doc,"Prognosis");
				}
				if(Serializing.getXmlNodeValue(doc,"Dx")!=null) {
					Dx=Serializing.getXmlNodeValue(doc,"Dx");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
