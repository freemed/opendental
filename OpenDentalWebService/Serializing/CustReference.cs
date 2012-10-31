using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class CustReference {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.CustReference custreference) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<CustReference>");
			sb.Append("<CustReferenceNum>").Append(custreference.CustReferenceNum).Append("</CustReferenceNum>");
			sb.Append("<PatNum>").Append(custreference.PatNum).Append("</PatNum>");
			sb.Append("<DateMostRecent>").Append(custreference.DateMostRecent.ToLongDateString()).Append("</DateMostRecent>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(custreference.Note)).Append("</Note>");
			sb.Append("<IsBadRef>").Append((custreference.IsBadRef)?1:0).Append("</IsBadRef>");
			sb.Append("</CustReference>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.CustReference Deserialize(string xml) {
			OpenDentBusiness.CustReference custreference=new OpenDentBusiness.CustReference();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "CustReferenceNum":
							custreference.CustReferenceNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							custreference.PatNum=reader.ReadContentAsLong();
							break;
						case "DateMostRecent":
							custreference.DateMostRecent=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "Note":
							custreference.Note=reader.ReadContentAsString();
							break;
						case "IsBadRef":
							custreference.IsBadRef=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return custreference;
		}


	}
}