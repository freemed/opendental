package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Clearinghouse {
		/** Primary key. */
		public int ClearinghouseNum;
		/** Description of this clearinghouse */
		public String Description;
		/** The path to export the X12 file to. \ is now optional. */
		public String ExportPath;
		/** A list of all payors which should have claims sent to this clearinghouse. Comma delimited with no spaces.  Not necessary if IsDefault. */
		public String Payors;
		/** Enum:ElectronicClaimFormat The format of the file that gets sent electronically. */
		public ElectronicClaimFormat Eformat;
		/** Sender ID Qualifier. Usually ZZ, sometimes 30. Seven other values are allowed as specified in X12 document, but probably never used. */
		public String ISA05;
		/** Used in ISA06, GS02, 1000A NM1, and 1000A PER.  If blank, then 810624427 is used to indicate Open Dental. */
		public String SenderTIN;
		/** Receiver ID Qualifier.  Usually ZZ, sometimes 30. Seven other values are allowed as specified in X12 document, but probably never used. */
		public String ISA07;
		/** Receiver ID. Also used in GS03. Provided by clearinghouse. Examples: BCBSGA or 0135WCH00(webMD) */
		public String ISA08;
		/** "P" for Production or "T" for Test. */
		public String ISA15;
		/** Password is usually combined with the login ID for user validation. */
		public String Password;
		/** The path that all incoming response files will be saved to. \ is now optional. */
		public String ResponsePath;
		/** Enum:EclaimsCommBridge  One of the included hard-coded communications briges.  Or none to just create the claim files without uploading. */
		public EclaimsCommBridge CommBridge;
		/** If applicable, this is the name of the client program to launch.  It is even used by the hard-coded comm bridges, because the user may have changed the installation directory or exe name. */
		public String ClientProgram;
		/** Each clearinghouse increments their batch numbers by one each time a claim file is sent.  User never sees this number.  Maxes out at 999, then loops back to 1.  This field must NOT be cached and must be ignored in the code except where it explicitly retrieves it from the db.  Defaults to 0 for brand new clearinghouses, which causes the first batch to go out as #1. */
		public int LastBatchNumber;
		/** Was not used.  1,2,3,or 4. The port that the modem is connected to if applicable. Always uses 9600 baud and standard settings. Will crash if port or modem not valid. */
		public byte ModemPort;
		/** A clearinghouse usually has a login ID that is used with the password in order to access the remote server.  This value is not usualy used within the actual claim. */
		public String LoginID;
		/** Used in 1000A NM1 and 1000A PER.  But if SenderTIN is blank, then OPEN DENTAL SOFTWARE is used instead. */
		public String SenderName;
		/** Used in 1000A PER.  But if SenderTIN is blank, then 8776861248 is used instead.  10 digit phone is required by WebMD and is universally assumed, so for now, this must be either blank or 10 digits. */
		public String SenderTelephone;
		/** Usually the same as ISA08, but at least one clearinghouse uses a different number here. */
		public String GS03;
		/** Authorization information. Almost always blank. Used for Denti-Cal. */
		public String ISA02;
		/** Security information. Almost always blank. Used for Denti-Cal. */
		public String ISA04;
		/** X12 component element separator. Two digit hexadecimal string representing an ASCII character or blank. Usually blank, implying 3A which represents ':'. For Denti-Cal, hexadecimal value 22 must be used, corresponding to '"'. */
		public String ISA16;
		/** X12 data element separator. Two digit hexadecimal string representing an ASCII character or blank. Usually blank, implying 2A which represents '*'. For Denti-Cal, hexadecimal value 1D must be used, corresponding to the "group separator" character which has no visual representation. */
		public String SeparatorData;
		/** X12 segment terminator. Two digit hexadecimal string representing an ASCII character or blank. Usually blank, implying 7E which represents '~'. For Denti-Cal, hexadecimal value 1C must be used, corresponding to the "file separator" character which has no visual representation. */
		public String SeparatorSegment;

		/** Deep copy of object. */
		public Clearinghouse Copy() {
			Clearinghouse clearinghouse=new Clearinghouse();
			clearinghouse.ClearinghouseNum=this.ClearinghouseNum;
			clearinghouse.Description=this.Description;
			clearinghouse.ExportPath=this.ExportPath;
			clearinghouse.Payors=this.Payors;
			clearinghouse.Eformat=this.Eformat;
			clearinghouse.ISA05=this.ISA05;
			clearinghouse.SenderTIN=this.SenderTIN;
			clearinghouse.ISA07=this.ISA07;
			clearinghouse.ISA08=this.ISA08;
			clearinghouse.ISA15=this.ISA15;
			clearinghouse.Password=this.Password;
			clearinghouse.ResponsePath=this.ResponsePath;
			clearinghouse.CommBridge=this.CommBridge;
			clearinghouse.ClientProgram=this.ClientProgram;
			clearinghouse.LastBatchNumber=this.LastBatchNumber;
			clearinghouse.ModemPort=this.ModemPort;
			clearinghouse.LoginID=this.LoginID;
			clearinghouse.SenderName=this.SenderName;
			clearinghouse.SenderTelephone=this.SenderTelephone;
			clearinghouse.GS03=this.GS03;
			clearinghouse.ISA02=this.ISA02;
			clearinghouse.ISA04=this.ISA04;
			clearinghouse.ISA16=this.ISA16;
			clearinghouse.SeparatorData=this.SeparatorData;
			clearinghouse.SeparatorSegment=this.SeparatorSegment;
			return clearinghouse;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Clearinghouse>");
			sb.append("<ClearinghouseNum>").append(ClearinghouseNum).append("</ClearinghouseNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<ExportPath>").append(Serializing.EscapeForXml(ExportPath)).append("</ExportPath>");
			sb.append("<Payors>").append(Serializing.EscapeForXml(Payors)).append("</Payors>");
			sb.append("<Eformat>").append(Eformat.ordinal()).append("</Eformat>");
			sb.append("<ISA05>").append(Serializing.EscapeForXml(ISA05)).append("</ISA05>");
			sb.append("<SenderTIN>").append(Serializing.EscapeForXml(SenderTIN)).append("</SenderTIN>");
			sb.append("<ISA07>").append(Serializing.EscapeForXml(ISA07)).append("</ISA07>");
			sb.append("<ISA08>").append(Serializing.EscapeForXml(ISA08)).append("</ISA08>");
			sb.append("<ISA15>").append(Serializing.EscapeForXml(ISA15)).append("</ISA15>");
			sb.append("<Password>").append(Serializing.EscapeForXml(Password)).append("</Password>");
			sb.append("<ResponsePath>").append(Serializing.EscapeForXml(ResponsePath)).append("</ResponsePath>");
			sb.append("<CommBridge>").append(CommBridge.ordinal()).append("</CommBridge>");
			sb.append("<ClientProgram>").append(Serializing.EscapeForXml(ClientProgram)).append("</ClientProgram>");
			sb.append("<LastBatchNumber>").append(LastBatchNumber).append("</LastBatchNumber>");
			sb.append("<ModemPort>").append(ModemPort).append("</ModemPort>");
			sb.append("<LoginID>").append(Serializing.EscapeForXml(LoginID)).append("</LoginID>");
			sb.append("<SenderName>").append(Serializing.EscapeForXml(SenderName)).append("</SenderName>");
			sb.append("<SenderTelephone>").append(Serializing.EscapeForXml(SenderTelephone)).append("</SenderTelephone>");
			sb.append("<GS03>").append(Serializing.EscapeForXml(GS03)).append("</GS03>");
			sb.append("<ISA02>").append(Serializing.EscapeForXml(ISA02)).append("</ISA02>");
			sb.append("<ISA04>").append(Serializing.EscapeForXml(ISA04)).append("</ISA04>");
			sb.append("<ISA16>").append(Serializing.EscapeForXml(ISA16)).append("</ISA16>");
			sb.append("<SeparatorData>").append(Serializing.EscapeForXml(SeparatorData)).append("</SeparatorData>");
			sb.append("<SeparatorSegment>").append(Serializing.EscapeForXml(SeparatorSegment)).append("</SeparatorSegment>");
			sb.append("</Clearinghouse>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ClearinghouseNum=Integer.valueOf(doc.getElementsByTagName("ClearinghouseNum").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				ExportPath=doc.getElementsByTagName("ExportPath").item(0).getFirstChild().getNodeValue();
				Payors=doc.getElementsByTagName("Payors").item(0).getFirstChild().getNodeValue();
				Eformat=ElectronicClaimFormat.values()[Integer.valueOf(doc.getElementsByTagName("Eformat").item(0).getFirstChild().getNodeValue())];
				ISA05=doc.getElementsByTagName("ISA05").item(0).getFirstChild().getNodeValue();
				SenderTIN=doc.getElementsByTagName("SenderTIN").item(0).getFirstChild().getNodeValue();
				ISA07=doc.getElementsByTagName("ISA07").item(0).getFirstChild().getNodeValue();
				ISA08=doc.getElementsByTagName("ISA08").item(0).getFirstChild().getNodeValue();
				ISA15=doc.getElementsByTagName("ISA15").item(0).getFirstChild().getNodeValue();
				Password=doc.getElementsByTagName("Password").item(0).getFirstChild().getNodeValue();
				ResponsePath=doc.getElementsByTagName("ResponsePath").item(0).getFirstChild().getNodeValue();
				CommBridge=EclaimsCommBridge.values()[Integer.valueOf(doc.getElementsByTagName("CommBridge").item(0).getFirstChild().getNodeValue())];
				ClientProgram=doc.getElementsByTagName("ClientProgram").item(0).getFirstChild().getNodeValue();
				LastBatchNumber=Integer.valueOf(doc.getElementsByTagName("LastBatchNumber").item(0).getFirstChild().getNodeValue());
				ModemPort=Byte.valueOf(doc.getElementsByTagName("ModemPort").item(0).getFirstChild().getNodeValue());
				LoginID=doc.getElementsByTagName("LoginID").item(0).getFirstChild().getNodeValue();
				SenderName=doc.getElementsByTagName("SenderName").item(0).getFirstChild().getNodeValue();
				SenderTelephone=doc.getElementsByTagName("SenderTelephone").item(0).getFirstChild().getNodeValue();
				GS03=doc.getElementsByTagName("GS03").item(0).getFirstChild().getNodeValue();
				ISA02=doc.getElementsByTagName("ISA02").item(0).getFirstChild().getNodeValue();
				ISA04=doc.getElementsByTagName("ISA04").item(0).getFirstChild().getNodeValue();
				ISA16=doc.getElementsByTagName("ISA16").item(0).getFirstChild().getNodeValue();
				SeparatorData=doc.getElementsByTagName("SeparatorData").item(0).getFirstChild().getNodeValue();
				SeparatorSegment=doc.getElementsByTagName("SeparatorSegment").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** For every type of electronic claim format that Open Dental can create, there will be an item in this enumeration.  All e-claim formats are hard coded due to complexity. */
		public enum ElectronicClaimFormat {
			/** 0-Not in database, but used in various places in program. */
			None,
			/** 1-The American standard through 12/31/11. */
			x837D_4010,
			/** 2-Proprietary format for Renaissance. */
			Renaissance,
			/** 3-CDAnet format version 4. */
			Canadian,
			/** 4-CSV file adaptable for use in Netherlands. */
			Dutch,
			/** 5-The American standard starting on 1/1/12. */
			x837D_5010_dental,
			/** 6-Either professional or medical.  The distiction is stored at the claim level. */
			x837_5010_med_inst
		}

		/** Each clearinghouse can have a hard-coded comm bridge which handles all the communications of transfering the claim files to the clearinghouse/carrier.  Does not just include X12, but can include any format at all. */
		public enum EclaimsCommBridge {
			/** 0-No comm bridge will be activated. The claim files will be created to the specified path, but they will not be uploaded. */
			None,
			/** 1 */
			WebMD,
			/** 2 */
			BCBSGA,
			/** 3 */
			Renaissance,
			/** 4 */
			ClaimConnect,
			/** 5 */
			RECS,
			/** 6 */
			Inmediata,
			/** 7 */
			AOS,
			/** 8 */
			PostnTrack,
			/** 9 Canada */
			CDAnet,
			/** 10 */
			Tesia,
			/** 11 */
			MercuryDE,
			/** 12 */
			ClaimX,
			/** 13 */
			DentiCal,
			/** 14 */
			EmdeonMedical
		}


}
