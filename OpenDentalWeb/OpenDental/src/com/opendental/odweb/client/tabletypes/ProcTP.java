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
		public ProcTP Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ProcTP>");
			sb.append("<ProcTPNum>").append(ProcTPNum).append("</ProcTPNum>");
			sb.append("<TreatPlanNum>").append(TreatPlanNum).append("</TreatPlanNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ProcNumOrig>").append(ProcNumOrig).append("</ProcNumOrig>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<Priority>").append(Priority).append("</Priority>");
			sb.append("<ToothNumTP>").append(Serializing.EscapeForXml(ToothNumTP)).append("</ToothNumTP>");
			sb.append("<Surf>").append(Serializing.EscapeForXml(Surf)).append("</Surf>");
			sb.append("<ProcCode>").append(Serializing.EscapeForXml(ProcCode)).append("</ProcCode>");
			sb.append("<Descript>").append(Serializing.EscapeForXml(Descript)).append("</Descript>");
			sb.append("<FeeAmt>").append(FeeAmt).append("</FeeAmt>");
			sb.append("<PriInsAmt>").append(PriInsAmt).append("</PriInsAmt>");
			sb.append("<SecInsAmt>").append(SecInsAmt).append("</SecInsAmt>");
			sb.append("<PatAmt>").append(PatAmt).append("</PatAmt>");
			sb.append("<Discount>").append(Discount).append("</Discount>");
			sb.append("<Prognosis>").append(Serializing.EscapeForXml(Prognosis)).append("</Prognosis>");
			sb.append("<Dx>").append(Serializing.EscapeForXml(Dx)).append("</Dx>");
			sb.append("</ProcTP>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"ProcTPNum")!=null) {
					ProcTPNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProcTPNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"TreatPlanNum")!=null) {
					TreatPlanNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TreatPlanNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProcNumOrig")!=null) {
					ProcNumOrig=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProcNumOrig"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Priority")!=null) {
					Priority=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Priority"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ToothNumTP")!=null) {
					ToothNumTP=Serializing.GetXmlNodeValue(doc,"ToothNumTP");
				}
				if(Serializing.GetXmlNodeValue(doc,"Surf")!=null) {
					Surf=Serializing.GetXmlNodeValue(doc,"Surf");
				}
				if(Serializing.GetXmlNodeValue(doc,"ProcCode")!=null) {
					ProcCode=Serializing.GetXmlNodeValue(doc,"ProcCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.GetXmlNodeValue(doc,"Descript");
				}
				if(Serializing.GetXmlNodeValue(doc,"FeeAmt")!=null) {
					FeeAmt=Double.valueOf(Serializing.GetXmlNodeValue(doc,"FeeAmt"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PriInsAmt")!=null) {
					PriInsAmt=Double.valueOf(Serializing.GetXmlNodeValue(doc,"PriInsAmt"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SecInsAmt")!=null) {
					SecInsAmt=Double.valueOf(Serializing.GetXmlNodeValue(doc,"SecInsAmt"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatAmt")!=null) {
					PatAmt=Double.valueOf(Serializing.GetXmlNodeValue(doc,"PatAmt"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Discount")!=null) {
					Discount=Double.valueOf(Serializing.GetXmlNodeValue(doc,"Discount"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Prognosis")!=null) {
					Prognosis=Serializing.GetXmlNodeValue(doc,"Prognosis");
				}
				if(Serializing.GetXmlNodeValue(doc,"Dx")!=null) {
					Dx=Serializing.GetXmlNodeValue(doc,"Dx");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
