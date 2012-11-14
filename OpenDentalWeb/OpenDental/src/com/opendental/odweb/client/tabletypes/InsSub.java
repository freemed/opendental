package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class InsSub {
		/** Primary key. */
		public int InsSubNum;
		/** FK to insplan.PlanNum. */
		public int PlanNum;
		/** FK to patient.PatNum. */
		public int Subscriber;
		/** Date plan became effective. */
		public String DateEffective;
		/** Date plan was terminated */
		public String DateTerm;
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
			sb.append("<DateEffective>").append(Serializing.EscapeForXml(DateEffective)).append("</DateEffective>");
			sb.append("<DateTerm>").append(Serializing.EscapeForXml(DateTerm)).append("</DateTerm>");
			sb.append("<ReleaseInfo>").append((ReleaseInfo)?1:0).append("</ReleaseInfo>");
			sb.append("<AssignBen>").append((AssignBen)?1:0).append("</AssignBen>");
			sb.append("<SubscriberID>").append(Serializing.EscapeForXml(SubscriberID)).append("</SubscriberID>");
			sb.append("<BenefitNotes>").append(Serializing.EscapeForXml(BenefitNotes)).append("</BenefitNotes>");
			sb.append("<SubscNote>").append(Serializing.EscapeForXml(SubscNote)).append("</SubscNote>");
			sb.append("</InsSub>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				InsSubNum=Integer.valueOf(doc.getElementsByTagName("InsSubNum").item(0).getFirstChild().getNodeValue());
				PlanNum=Integer.valueOf(doc.getElementsByTagName("PlanNum").item(0).getFirstChild().getNodeValue());
				Subscriber=Integer.valueOf(doc.getElementsByTagName("Subscriber").item(0).getFirstChild().getNodeValue());
				DateEffective=doc.getElementsByTagName("DateEffective").item(0).getFirstChild().getNodeValue();
				DateTerm=doc.getElementsByTagName("DateTerm").item(0).getFirstChild().getNodeValue();
				ReleaseInfo=(doc.getElementsByTagName("ReleaseInfo").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				AssignBen=(doc.getElementsByTagName("AssignBen").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				SubscriberID=doc.getElementsByTagName("SubscriberID").item(0).getFirstChild().getNodeValue();
				BenefitNotes=doc.getElementsByTagName("BenefitNotes").item(0).getFirstChild().getNodeValue();
				SubscNote=doc.getElementsByTagName("SubscNote").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
