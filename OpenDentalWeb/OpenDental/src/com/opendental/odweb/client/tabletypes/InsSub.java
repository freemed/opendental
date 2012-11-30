package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

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
		public InsSub Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<InsSub>");
			sb.append("<InsSubNum>").append(InsSubNum).append("</InsSubNum>");
			sb.append("<PlanNum>").append(PlanNum).append("</PlanNum>");
			sb.append("<Subscriber>").append(Subscriber).append("</Subscriber>");
			sb.append("<DateEffective>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateEffective)).append("</DateEffective>");
			sb.append("<DateTerm>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTerm)).append("</DateTerm>");
			sb.append("<ReleaseInfo>").append((ReleaseInfo)?1:0).append("</ReleaseInfo>");
			sb.append("<AssignBen>").append((AssignBen)?1:0).append("</AssignBen>");
			sb.append("<SubscriberID>").append(Serializing.EscapeForXml(SubscriberID)).append("</SubscriberID>");
			sb.append("<BenefitNotes>").append(Serializing.EscapeForXml(BenefitNotes)).append("</BenefitNotes>");
			sb.append("<SubscNote>").append(Serializing.EscapeForXml(SubscNote)).append("</SubscNote>");
			sb.append("</InsSub>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"InsSubNum")!=null) {
					InsSubNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"InsSubNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PlanNum")!=null) {
					PlanNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PlanNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Subscriber")!=null) {
					Subscriber=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Subscriber"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateEffective")!=null) {
					DateEffective=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateEffective"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTerm")!=null) {
					DateTerm=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTerm"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ReleaseInfo")!=null) {
					ReleaseInfo=(Serializing.GetXmlNodeValue(doc,"ReleaseInfo")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"AssignBen")!=null) {
					AssignBen=(Serializing.GetXmlNodeValue(doc,"AssignBen")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"SubscriberID")!=null) {
					SubscriberID=Serializing.GetXmlNodeValue(doc,"SubscriberID");
				}
				if(Serializing.GetXmlNodeValue(doc,"BenefitNotes")!=null) {
					BenefitNotes=Serializing.GetXmlNodeValue(doc,"BenefitNotes");
				}
				if(Serializing.GetXmlNodeValue(doc,"SubscNote")!=null) {
					SubscNote=Serializing.GetXmlNodeValue(doc,"SubscNote");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
