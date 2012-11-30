package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"OperatoryNum")!=null) {
					OperatoryNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"OperatoryNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"OpName")!=null) {
					OpName=Serializing.GetXmlNodeValue(doc,"OpName");
				}
				if(Serializing.GetXmlNodeValue(doc,"Abbrev")!=null) {
					Abbrev=Serializing.GetXmlNodeValue(doc,"Abbrev");
				}
				if(Serializing.GetXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.GetXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvDentist")!=null) {
					ProvDentist=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvDentist"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvHygienist")!=null) {
					ProvHygienist=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvHygienist"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsHygiene")!=null) {
					IsHygiene=(Serializing.GetXmlNodeValue(doc,"IsHygiene")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SetProspective")!=null) {
					SetProspective=(Serializing.GetXmlNodeValue(doc,"SetProspective")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
