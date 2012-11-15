using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class RegistrationKey {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.RegistrationKey registrationkey) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<RegistrationKey>");
			sb.Append("<RegistrationKeyNum>").Append(registrationkey.RegistrationKeyNum).Append("</RegistrationKeyNum>");
			sb.Append("<PatNum>").Append(registrationkey.PatNum).Append("</PatNum>");
			sb.Append("<RegKey>").Append(SerializeStringEscapes.EscapeForXml(registrationkey.RegKey)).Append("</RegKey>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(registrationkey.Note)).Append("</Note>");
			sb.Append("<DateStarted>").Append(registrationkey.DateStarted.ToString("yyyyMMddHHmmss")).Append("</DateStarted>");
			sb.Append("<DateDisabled>").Append(registrationkey.DateDisabled.ToString("yyyyMMddHHmmss")).Append("</DateDisabled>");
			sb.Append("<DateEnded>").Append(registrationkey.DateEnded.ToString("yyyyMMddHHmmss")).Append("</DateEnded>");
			sb.Append("<IsForeign>").Append((registrationkey.IsForeign)?1:0).Append("</IsForeign>");
			sb.Append("<UsesServerVersion>").Append((registrationkey.UsesServerVersion)?1:0).Append("</UsesServerVersion>");
			sb.Append("<IsFreeVersion>").Append((registrationkey.IsFreeVersion)?1:0).Append("</IsFreeVersion>");
			sb.Append("<IsOnlyForTesting>").Append((registrationkey.IsOnlyForTesting)?1:0).Append("</IsOnlyForTesting>");
			sb.Append("<VotesAllotted>").Append(registrationkey.VotesAllotted).Append("</VotesAllotted>");
			sb.Append("</RegistrationKey>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.RegistrationKey Deserialize(string xml) {
			OpenDentBusiness.RegistrationKey registrationkey=new OpenDentBusiness.RegistrationKey();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "RegistrationKeyNum":
							registrationkey.RegistrationKeyNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							registrationkey.PatNum=reader.ReadContentAsLong();
							break;
						case "RegKey":
							registrationkey.RegKey=reader.ReadContentAsString();
							break;
						case "Note":
							registrationkey.Note=reader.ReadContentAsString();
							break;
						case "DateStarted":
							registrationkey.DateStarted=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateDisabled":
							registrationkey.DateDisabled=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateEnded":
							registrationkey.DateEnded=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "IsForeign":
							registrationkey.IsForeign=reader.ReadContentAsString()!="0";
							break;
						case "UsesServerVersion":
							registrationkey.UsesServerVersion=reader.ReadContentAsString()!="0";
							break;
						case "IsFreeVersion":
							registrationkey.IsFreeVersion=reader.ReadContentAsString()!="0";
							break;
						case "IsOnlyForTesting":
							registrationkey.IsOnlyForTesting=reader.ReadContentAsString()!="0";
							break;
						case "VotesAllotted":
							registrationkey.VotesAllotted=reader.ReadContentAsInt();
							break;
					}
				}
			}
			return registrationkey;
		}


	}
}