package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
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
		public Operatory deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Operatory>");
			sb.append("<OperatoryNum>").append(OperatoryNum).append("</OperatoryNum>");
			sb.append("<OpName>").append(Serializing.escapeForXml(OpName)).append("</OpName>");
			sb.append("<Abbrev>").append(Serializing.escapeForXml(Abbrev)).append("</Abbrev>");
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
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"OperatoryNum")!=null) {
					OperatoryNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"OperatoryNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"OpName")!=null) {
					OpName=Serializing.getXmlNodeValue(doc,"OpName");
				}
				if(Serializing.getXmlNodeValue(doc,"Abbrev")!=null) {
					Abbrev=Serializing.getXmlNodeValue(doc,"Abbrev");
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.getXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"ProvDentist")!=null) {
					ProvDentist=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvDentist"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProvHygienist")!=null) {
					ProvHygienist=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvHygienist"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsHygiene")!=null) {
					IsHygiene=(Serializing.getXmlNodeValue(doc,"IsHygiene")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SetProspective")!=null) {
					SetProspective=(Serializing.getXmlNodeValue(doc,"SetProspective")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
