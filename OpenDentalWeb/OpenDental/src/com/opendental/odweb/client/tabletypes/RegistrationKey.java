package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class RegistrationKey {
		/** Primary Key. */
		public int RegistrationKeyNum;
		/** FK to patient.PatNum. The customer to which this registration key applies. */
		public int PatNum;
		/** The registration key as stored in the customer database. */
		public String RegKey;
		/** Db note about the registration key. Specifically, the note must include information about the location to which this key pertains, since once at least one key must be assigned to each location to be legal. */
		public String Note;
		/** This will help later with tracking for licensing. */
		public Date DateStarted;
		/** This is used to completely disable a key.  Might possibly even cripple the user's program.  Usually only used if reassigning another key due to abuse or error.  If no date specified, then this key is still valid. */
		public Date DateDisabled;
		/** This is used when the customer cancels monthly support.  This still allows the customer to get downloads for bug fixes, but only up through a certain version.  Our web server program will use this date to deduce which version they are allowed to have.  Any version that was released as a beta before this date is allowed to be downloaded. */
		public Date DateEnded;
		/** This is assigned automatically based on whether the registration key is a US version vs. a foreign version.  The foreign version is not able to unlock the procedure codes.  There are muliple layers of safeguards in place. */
		public boolean IsForeign;
		/** Deprecated. */
		public boolean UsesServerVersion;
		/** We have given this customer a free version.  Typically in India. */
		public boolean IsFreeVersion;
		/** This customer is not using the software with live patient data, but only for testing and development purposes. */
		public boolean IsOnlyForTesting;
		/** Typically 100, although it can be more for multilocation offices. */
		public int VotesAllotted;

		/** Deep copy of object. */
		public RegistrationKey Copy() {
			RegistrationKey registrationkey=new RegistrationKey();
			registrationkey.RegistrationKeyNum=this.RegistrationKeyNum;
			registrationkey.PatNum=this.PatNum;
			registrationkey.RegKey=this.RegKey;
			registrationkey.Note=this.Note;
			registrationkey.DateStarted=this.DateStarted;
			registrationkey.DateDisabled=this.DateDisabled;
			registrationkey.DateEnded=this.DateEnded;
			registrationkey.IsForeign=this.IsForeign;
			registrationkey.UsesServerVersion=this.UsesServerVersion;
			registrationkey.IsFreeVersion=this.IsFreeVersion;
			registrationkey.IsOnlyForTesting=this.IsOnlyForTesting;
			registrationkey.VotesAllotted=this.VotesAllotted;
			return registrationkey;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<RegistrationKey>");
			sb.append("<RegistrationKeyNum>").append(RegistrationKeyNum).append("</RegistrationKeyNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<RegKey>").append(Serializing.EscapeForXml(RegKey)).append("</RegKey>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<DateStarted>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStarted)).append("</DateStarted>");
			sb.append("<DateDisabled>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateDisabled)).append("</DateDisabled>");
			sb.append("<DateEnded>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateEnded)).append("</DateEnded>");
			sb.append("<IsForeign>").append((IsForeign)?1:0).append("</IsForeign>");
			sb.append("<UsesServerVersion>").append((UsesServerVersion)?1:0).append("</UsesServerVersion>");
			sb.append("<IsFreeVersion>").append((IsFreeVersion)?1:0).append("</IsFreeVersion>");
			sb.append("<IsOnlyForTesting>").append((IsOnlyForTesting)?1:0).append("</IsOnlyForTesting>");
			sb.append("<VotesAllotted>").append(VotesAllotted).append("</VotesAllotted>");
			sb.append("</RegistrationKey>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"RegistrationKeyNum")!=null) {
					RegistrationKeyNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"RegistrationKeyNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"RegKey")!=null) {
					RegKey=Serializing.GetXmlNodeValue(doc,"RegKey");
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
				if(Serializing.GetXmlNodeValue(doc,"DateStarted")!=null) {
					DateStarted=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateStarted"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateDisabled")!=null) {
					DateDisabled=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateDisabled"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateEnded")!=null) {
					DateEnded=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateEnded"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsForeign")!=null) {
					IsForeign=(Serializing.GetXmlNodeValue(doc,"IsForeign")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"UsesServerVersion")!=null) {
					UsesServerVersion=(Serializing.GetXmlNodeValue(doc,"UsesServerVersion")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"IsFreeVersion")!=null) {
					IsFreeVersion=(Serializing.GetXmlNodeValue(doc,"IsFreeVersion")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"IsOnlyForTesting")!=null) {
					IsOnlyForTesting=(Serializing.GetXmlNodeValue(doc,"IsOnlyForTesting")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"VotesAllotted")!=null) {
					VotesAllotted=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"VotesAllotted"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
