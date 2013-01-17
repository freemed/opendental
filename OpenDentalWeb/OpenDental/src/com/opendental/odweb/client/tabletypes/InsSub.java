package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class InsSub {
		/** Primary key. */
		public int InsSubNum;
		/** FK to insplan.PlanNum. */
		public int PlanNum;
		/** FK to patient.PatNum. */
		public int Subscriber;
		/** Date plan became effective. */
		public Date DateEffective;
		/** Date plan was terminated */
		public Date DateTerm;
		/** Release of information signature is on file. */
		public boolean ReleaseInfo;
		/** Assignment of benefits signature is on file.  For Canada, this handles Payee Code, F01.  Option to pay other third party is not included. */
		public boolean AssignBen;
		/** Usually SSN, but can also be changed by user.  No dashes. Not allowed to be blank. */
		public String SubscriberID;
		/** User doesn't usually put these in.  Only used when automatically requesting benefits, such as with Trojan.  All the benefits get stored here in text form for later reference.  Not at plan level because might be specific to subscriber.  If blank, we try to display a benefitNote for another subscriber to the plan. */
		public String BenefitNotes;
		/** Use to store any other info that affects coverage. */
		public String SubscNote;

		/** Deep copy of object. */
		public InsSub deepCopy() {
			InsSub inssub=new InsSub();
			inssub.InsSubNum=this.InsSubNum;
			inssub.PlanNum=this.PlanNum;
			inssub.Subscriber=this.Subscriber;
			inssub.DateEffective=this.DateEffective;
			inssub.DateTerm=this.DateTerm;
			inssub.ReleaseInfo=this.ReleaseInfo;
			inssub.AssignBen=this.AssignBen;
			inssub.SubscriberID=this.SubscriberID;
			inssub.BenefitNotes=this.BenefitNotes;
			inssub.SubscNote=this.SubscNote;
			return inssub;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<InsSub>");
			sb.append("<InsSubNum>").append(InsSubNum).append("</InsSubNum>");
			sb.append("<PlanNum>").append(PlanNum).append("</PlanNum>");
			sb.append("<Subscriber>").append(Subscriber).append("</Subscriber>");
			sb.append("<DateEffective>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateEffective)).append("</DateEffective>");
			sb.append("<DateTerm>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTerm)).append("</DateTerm>");
			sb.append("<ReleaseInfo>").append((ReleaseInfo)?1:0).append("</ReleaseInfo>");
			sb.append("<AssignBen>").append((AssignBen)?1:0).append("</AssignBen>");
			sb.append("<SubscriberID>").append(Serializing.escapeForXml(SubscriberID)).append("</SubscriberID>");
			sb.append("<BenefitNotes>").append(Serializing.escapeForXml(BenefitNotes)).append("</BenefitNotes>");
			sb.append("<SubscNote>").append(Serializing.escapeForXml(SubscNote)).append("</SubscNote>");
			sb.append("</InsSub>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"InsSubNum")!=null) {
					InsSubNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"InsSubNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PlanNum")!=null) {
					PlanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PlanNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Subscriber")!=null) {
					Subscriber=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Subscriber"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateEffective")!=null) {
					DateEffective=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateEffective"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTerm")!=null) {
					DateTerm=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTerm"));
				}
				if(Serializing.getXmlNodeValue(doc,"ReleaseInfo")!=null) {
					ReleaseInfo=(Serializing.getXmlNodeValue(doc,"ReleaseInfo")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"AssignBen")!=null) {
					AssignBen=(Serializing.getXmlNodeValue(doc,"AssignBen")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"SubscriberID")!=null) {
					SubscriberID=Serializing.getXmlNodeValue(doc,"SubscriberID");
				}
				if(Serializing.getXmlNodeValue(doc,"BenefitNotes")!=null) {
					BenefitNotes=Serializing.getXmlNodeValue(doc,"BenefitNotes");
				}
				if(Serializing.getXmlNodeValue(doc,"SubscNote")!=null) {
					SubscNote=Serializing.getXmlNodeValue(doc,"SubscNote");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing InsSub: "+e.getMessage());
			}
		}


}
