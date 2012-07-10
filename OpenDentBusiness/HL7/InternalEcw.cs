using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.HL7 {
	///<summary></summary>
	public class InternalEcw {

		public static HL7Def GetHL7Def() {
			HL7Def def=new HL7Def();
			def.Description="eClinicalWorks";
			def.ModeTx=ModeTxHL7.File;
			def.IncomingFolder=@"C:\HL7\In";
			def.OutgoingFolder=@"C:\HL7\Out";
			def.IncomingPort="";
			def.OutgoingIpPort="";
			def.FieldSeparator="|";
			def.ComponentSeparator="^";
			def.SubcomponentSeparator="&";
			def.RepetitionSeparator="~";
			def.EscapeCharacter=@"\";
			def.IsInternal=true;
			def.InternalType="eCW";
			def.InternalTypeVersion=Assembly.GetAssembly(typeof(Db)).GetName().Version.ToString();
			def.IsEnabled=false;
			def.Note="";
			def.hl7DefMessages=new List<HL7DefMessage> ();
			//-----------------------------------------------------------------------
			//eCW incoming patient information (ADT).
			HL7DefMessage adt=new HL7DefMessage();
			adt.HL7DefNum=def.HL7DefNum;
			adt.MessageType=MessageTypeHL7.ADT;
			adt.EventType=EventTypeHL7.A04;
			adt.InOrOut=InOutHL7.Incoming;
			adt.ItemOrder=1;
			adt.Note="";
			def.hl7DefMessages.Add(adt);








			return def;
		}


	}
}
