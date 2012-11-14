using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class CustRefEntry {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.CustRefEntry custrefentry) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<CustRefEntry>");
			sb.Append("<CustRefEntryNum>").Append(custrefentry.CustRefEntryNum).Append("</CustRefEntryNum>");
			sb.Append("<PatNumCust>").Append(custrefentry.PatNumCust).Append("</PatNumCust>");
			sb.Append("<PatNumRef>").Append(custrefentry.PatNumRef).Append("</PatNumRef>");
			sb.Append("<DateEntry>").Append(custrefentry.DateEntry.ToString()).Append("</DateEntry>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(custrefentry.Note)).Append("</Note>");
			sb.Append("</CustRefEntry>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.CustRefEntry Deserialize(string xml) {
			OpenDentBusiness.CustRefEntry custrefentry=new OpenDentBusiness.CustRefEntry();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "CustRefEntryNum":
							custrefentry.CustRefEntryNum=reader.ReadContentAsLong();
							break;
						case "PatNumCust":
							custrefentry.PatNumCust=reader.ReadContentAsLong();
							break;
						case "PatNumRef":
							custrefentry.PatNumRef=reader.ReadContentAsLong();
							break;
						case "DateEntry":
							custrefentry.DateEntry=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "Note":
							custrefentry.Note=reader.ReadContentAsString();
							break;
					}
				}
			}
			return custrefentry;
		}


	}
}