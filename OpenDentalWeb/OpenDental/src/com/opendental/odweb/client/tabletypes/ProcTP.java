package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ProcTPNum=Integer.valueOf(doc.getElementsByTagName("ProcTPNum").item(0).getFirstChild().getNodeValue());
				TreatPlanNum=Integer.valueOf(doc.getElementsByTagName("TreatPlanNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				ProcNumOrig=Integer.valueOf(doc.getElementsByTagName("ProcNumOrig").item(0).getFirstChild().getNodeValue());
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
				Priority=Integer.valueOf(doc.getElementsByTagName("Priority").item(0).getFirstChild().getNodeValue());
				ToothNumTP=doc.getElementsByTagName("ToothNumTP").item(0).getFirstChild().getNodeValue();
				Surf=doc.getElementsByTagName("Surf").item(0).getFirstChild().getNodeValue();
				ProcCode=doc.getElementsByTagName("ProcCode").item(0).getFirstChild().getNodeValue();
				Descript=doc.getElementsByTagName("Descript").item(0).getFirstChild().getNodeValue();
				FeeAmt=Double.valueOf(doc.getElementsByTagName("FeeAmt").item(0).getFirstChild().getNodeValue());
				PriInsAmt=Double.valueOf(doc.getElementsByTagName("PriInsAmt").item(0).getFirstChild().getNodeValue());
				SecInsAmt=Double.valueOf(doc.getElementsByTagName("SecInsAmt").item(0).getFirstChild().getNodeValue());
				PatAmt=Double.valueOf(doc.getElementsByTagName("PatAmt").item(0).getFirstChild().getNodeValue());
				Discount=Double.valueOf(doc.getElementsByTagName("Discount").item(0).getFirstChild().getNodeValue());
				Prognosis=doc.getElementsByTagName("Prognosis").item(0).getFirstChild().getNodeValue();
				Dx=doc.getElementsByTagName("Dx").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
