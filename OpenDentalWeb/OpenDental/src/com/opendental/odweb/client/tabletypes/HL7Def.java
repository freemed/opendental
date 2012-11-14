package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class HL7Def {
		/** Primary key. */
		public int HL7DefNum;
		/**  */
		public String Description;
		/** Enum:ModeTxHL7 File, TcpIp. */
		public ModeTxHL7 ModeTx;
		/** Only used for File mode */
		public String IncomingFolder;
		/** Only used for File mode */
		public String OutgoingFolder;
		/** Only used for tcpip mode. Example: 1461 */
		public String IncomingPort;
		/** Only used for tcpip mode. Example: 192.168.0.23:1462 */
		public String OutgoingIpPort;
		/** Only relevant for outgoing. Incoming field separators are defined in MSH. Default |. */
		public String FieldSeparator;
		/** Only relevant for outgoing. Incoming field separators are defined in MSH. Default ^. */
		public String ComponentSeparator;
		/**  */
		public String SubcomponentSeparator;
		/** Only relevant for outgoing. Incoming field separators are defined in MSH. Default ~. */
		public String RepetitionSeparator;
		/** Only relevant for outgoing. Incoming field separators are defined in MSH. Default \. */
		public String EscapeCharacter;
		/** If this is set, then there will be no child tables. Internal types are fully defined within the C# code rather than in the database. */
		public boolean IsInternal;
		/** This will always have a value because we always start with a copy of some internal type. */
		public String InternalType;
		/** Example: 12.2.14. This will be empty if IsInternal. This records the version at which they made their copy. We might have made significant improvements since their copy. */
		public String InternalTypeVersion;
		/** . */
		public boolean IsEnabled;
		/**  */
		public String Note;
		/** The machine name of the computer where the OpenDentHL7 service for this def is running. */
		public String HL7Server;
		/** The name of the HL7 service for this def.  Must begin with OpenDent... */
		public String HL7ServiceName;
		/** Enum:HL7ShowDemographics Hide,Show,Change,ChangeAndAdd */
		public HL7ShowDemographics ShowDemographics;
		/** Show Appointments module. */
		public boolean ShowAppts;
		/** Show Account module */
		public boolean ShowAccount;

		/** Deep copy of object. */
		public HL7Def Copy() {
			HL7Def hl7def=new HL7Def();
			hl7def.HL7DefNum=this.HL7DefNum;
			hl7def.Description=this.Description;
			hl7def.ModeTx=this.ModeTx;
			hl7def.IncomingFolder=this.IncomingFolder;
			hl7def.OutgoingFolder=this.OutgoingFolder;
			hl7def.IncomingPort=this.IncomingPort;
			hl7def.OutgoingIpPort=this.OutgoingIpPort;
			hl7def.FieldSeparator=this.FieldSeparator;
			hl7def.ComponentSeparator=this.ComponentSeparator;
			hl7def.SubcomponentSeparator=this.SubcomponentSeparator;
			hl7def.RepetitionSeparator=this.RepetitionSeparator;
			hl7def.EscapeCharacter=this.EscapeCharacter;
			hl7def.IsInternal=this.IsInternal;
			hl7def.InternalType=this.InternalType;
			hl7def.InternalTypeVersion=this.InternalTypeVersion;
			hl7def.IsEnabled=this.IsEnabled;
			hl7def.Note=this.Note;
			hl7def.HL7Server=this.HL7Server;
			hl7def.HL7ServiceName=this.HL7ServiceName;
			hl7def.ShowDemographics=this.ShowDemographics;
			hl7def.ShowAppts=this.ShowAppts;
			hl7def.ShowAccount=this.ShowAccount;
			return hl7def;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<HL7Def>");
			sb.append("<HL7DefNum>").append(HL7DefNum).append("</HL7DefNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<ModeTx>").append(ModeTx.ordinal()).append("</ModeTx>");
			sb.append("<IncomingFolder>").append(Serializing.EscapeForXml(IncomingFolder)).append("</IncomingFolder>");
			sb.append("<OutgoingFolder>").append(Serializing.EscapeForXml(OutgoingFolder)).append("</OutgoingFolder>");
			sb.append("<IncomingPort>").append(Serializing.EscapeForXml(IncomingPort)).append("</IncomingPort>");
			sb.append("<OutgoingIpPort>").append(Serializing.EscapeForXml(OutgoingIpPort)).append("</OutgoingIpPort>");
			sb.append("<FieldSeparator>").append(Serializing.EscapeForXml(FieldSeparator)).append("</FieldSeparator>");
			sb.append("<ComponentSeparator>").append(Serializing.EscapeForXml(ComponentSeparator)).append("</ComponentSeparator>");
			sb.append("<SubcomponentSeparator>").append(Serializing.EscapeForXml(SubcomponentSeparator)).append("</SubcomponentSeparator>");
			sb.append("<RepetitionSeparator>").append(Serializing.EscapeForXml(RepetitionSeparator)).append("</RepetitionSeparator>");
			sb.append("<EscapeCharacter>").append(Serializing.EscapeForXml(EscapeCharacter)).append("</EscapeCharacter>");
			sb.append("<IsInternal>").append((IsInternal)?1:0).append("</IsInternal>");
			sb.append("<InternalType>").append(Serializing.EscapeForXml(InternalType)).append("</InternalType>");
			sb.append("<InternalTypeVersion>").append(Serializing.EscapeForXml(InternalTypeVersion)).append("</InternalTypeVersion>");
			sb.append("<IsEnabled>").append((IsEnabled)?1:0).append("</IsEnabled>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<HL7Server>").append(Serializing.EscapeForXml(HL7Server)).append("</HL7Server>");
			sb.append("<HL7ServiceName>").append(Serializing.EscapeForXml(HL7ServiceName)).append("</HL7ServiceName>");
			sb.append("<ShowDemographics>").append(ShowDemographics.ordinal()).append("</ShowDemographics>");
			sb.append("<ShowAppts>").append((ShowAppts)?1:0).append("</ShowAppts>");
			sb.append("<ShowAccount>").append((ShowAccount)?1:0).append("</ShowAccount>");
			sb.append("</HL7Def>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				HL7DefNum=Integer.valueOf(doc.getElementsByTagName("HL7DefNum").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				ModeTx=ModeTxHL7.values()[Integer.valueOf(doc.getElementsByTagName("ModeTx").item(0).getFirstChild().getNodeValue())];
				IncomingFolder=doc.getElementsByTagName("IncomingFolder").item(0).getFirstChild().getNodeValue();
				OutgoingFolder=doc.getElementsByTagName("OutgoingFolder").item(0).getFirstChild().getNodeValue();
				IncomingPort=doc.getElementsByTagName("IncomingPort").item(0).getFirstChild().getNodeValue();
				OutgoingIpPort=doc.getElementsByTagName("OutgoingIpPort").item(0).getFirstChild().getNodeValue();
				FieldSeparator=doc.getElementsByTagName("FieldSeparator").item(0).getFirstChild().getNodeValue();
				ComponentSeparator=doc.getElementsByTagName("ComponentSeparator").item(0).getFirstChild().getNodeValue();
				SubcomponentSeparator=doc.getElementsByTagName("SubcomponentSeparator").item(0).getFirstChild().getNodeValue();
				RepetitionSeparator=doc.getElementsByTagName("RepetitionSeparator").item(0).getFirstChild().getNodeValue();
				EscapeCharacter=doc.getElementsByTagName("EscapeCharacter").item(0).getFirstChild().getNodeValue();
				IsInternal=(doc.getElementsByTagName("IsInternal").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				InternalType=doc.getElementsByTagName("InternalType").item(0).getFirstChild().getNodeValue();
				InternalTypeVersion=doc.getElementsByTagName("InternalTypeVersion").item(0).getFirstChild().getNodeValue();
				IsEnabled=(doc.getElementsByTagName("IsEnabled").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				HL7Server=doc.getElementsByTagName("HL7Server").item(0).getFirstChild().getNodeValue();
				HL7ServiceName=doc.getElementsByTagName("HL7ServiceName").item(0).getFirstChild().getNodeValue();
				ShowDemographics=HL7ShowDemographics.values()[Integer.valueOf(doc.getElementsByTagName("ShowDemographics").item(0).getFirstChild().getNodeValue())];
				ShowAppts=(doc.getElementsByTagName("ShowAppts").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				ShowAccount=(doc.getElementsByTagName("ShowAccount").item(0).getFirstChild().getNodeValue()=="0")?false:true;
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum ModeTxHL7 {
			/** 0 */
			File,
			/** 1 */
			TcpIp
		}

		/**  */
		public enum HL7ShowDemographics {
			/** Cannot see or change. */
			Hide,
			/** Can see, but not change. */
			Show,
			/** Can change, but not add patients.  Might get overwritten by next incoming message. */
			Change,
			/** Can change and add patients.  Might get overwritten by next incoming message. */
			ChangeAndAdd
		}


}
