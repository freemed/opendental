using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ICD9 {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ICD9 icd9) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ICD9>");
			sb.Append("<ICD9Num>").Append(icd9.ICD9Num).Append("</ICD9Num>");
			sb.Append("<ICD9Code>").Append(SerializeStringEscapes.EscapeForXml(icd9.ICD9Code)).Append("</ICD9Code>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(icd9.Description)).Append("</Description>");
			sb.Append("<DateTStamp>").Append(icd9.DateTStamp.ToLongDateString()).Append("</DateTStamp>");
			sb.Append("</ICD9>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ICD9 Deserialize(string xml) {
			OpenDentBusiness.ICD9 icd9=new OpenDentBusiness.ICD9();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ICD9Num":
							icd9.ICD9Num=reader.ReadContentAsLong();
							break;
						case "ICD9Code":
							icd9.ICD9Code=reader.ReadContentAsString();
							break;
						case "Description":
							icd9.Description=reader.ReadContentAsString();
							break;
						case "DateTStamp":
							icd9.DateTStamp=DateTime.Parse(reader.ReadContentAsString());
							break;
					}
				}
			}
			return icd9;
		}


	}
}