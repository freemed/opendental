package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Operatory {
		/** Primary key */
		public int OperatoryNum;
		/** The full name to show in the column. */
		public String OpName;
		/** 5 char or less. Not used much. */
		public String Abbrev;
		/** The order that this op column will show.  Changing views only hides some ops; it does not change their order.  Zero based. */
		public int ItemOrder;
		/** Used instead of deleting to hide an op that is no longer used. */
		public boolean IsHidden;
		/** FK to provider.ProvNum.  The dentist assigned to this op.  If more than one dentist might be assigned to an op, then create a second op and use one for each dentist. If 0, then no dentist is assigned. */
		public int ProvDentist;
		/** FK to provider.ProvNum.  The hygienist assigned to this op.  If 0, then no hygienist is assigned. */
		public int ProvHygienist;
		/** Set true if this is a hygiene operatory.  The hygienist will then be considered the main provider for this op. */
		public boolean IsHygiene;
		/** FK to clinic.ClinicNum.  0 if no clinic. */
		public int ClinicNum;
		/** If true patients put into this operatory will have status set to prospective. */
		public boolean SetProspective;

		/** Deep copy of object. */
		public Operatory Copy() {
			Operatory operatory=new Operatory();
			operatory.OperatoryNum=this.OperatoryNum;
			operatory.OpName=this.OpName;
			operatory.Abbrev=this.Abbrev;
			operatory.ItemOrder=this.ItemOrder;
			operatory.IsHidden=this.IsHidden;
			operatory.ProvDentist=this.ProvDentist;
			operatory.ProvHygienist=this.ProvHygienist;
			operatory.IsHygiene=this.IsHygiene;
			operatory.ClinicNum=this.ClinicNum;
			operatory.SetProspective=this.SetProspective;
			return operatory;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Operatory>");
			sb.append("<OperatoryNum>").append(OperatoryNum).append("</OperatoryNum>");
			sb.append("<OpName>").append(Serializing.EscapeForXml(OpName)).append("</OpName>");
			sb.append("<Abbrev>").append(Serializing.EscapeForXml(Abbrev)).append("</Abbrev>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<ProvDentist>").append(ProvDentist).append("</ProvDentist>");
			sb.append("<ProvHygienist>").append(ProvHygienist).append("</ProvHygienist>");
			sb.append("<IsHygiene>").append((IsHygiene)?1:0).append("</IsHygiene>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<SetProspective>").append((SetProspective)?1:0).append("</SetProspective>");
			sb.append("</Operatory>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				OperatoryNum=Integer.valueOf(doc.getElementsByTagName("OperatoryNum").item(0).getFirstChild().getNodeValue());
				OpName=doc.getElementsByTagName("OpName").item(0).getFirstChild().getNodeValue();
				Abbrev=doc.getElementsByTagName("Abbrev").item(0).getFirstChild().getNodeValue();
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
				IsHidden=(doc.getElementsByTagName("IsHidden").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				ProvDentist=Integer.valueOf(doc.getElementsByTagName("ProvDentist").item(0).getFirstChild().getNodeValue());
				ProvHygienist=Integer.valueOf(doc.getElementsByTagName("ProvHygienist").item(0).getFirstChild().getNodeValue());
				IsHygiene=(doc.getElementsByTagName("IsHygiene").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				ClinicNum=Integer.valueOf(doc.getElementsByTagName("ClinicNum").item(0).getFirstChild().getNodeValue());
				SetProspective=(doc.getElementsByTagName("SetProspective").item(0).getFirstChild().getNodeValue()=="0")?false:true;
			}
			catch(Exception e) {
				throw e;
			}
		}


}
