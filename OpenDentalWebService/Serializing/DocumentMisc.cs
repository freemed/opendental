using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class DocumentMisc {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.DocumentMisc documentmisc) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<DocumentMisc>");
			sb.Append("<DocMiscNum>").Append(documentmisc.DocMiscNum).Append("</DocMiscNum>");
			sb.Append("<DateCreated>").Append(documentmisc.DateCreated.ToString("yyyyMMddHHmmss")).Append("</DateCreated>");
			sb.Append("<FileName>").Append(SerializeStringEscapes.EscapeForXml(documentmisc.FileName)).Append("</FileName>");
			sb.Append("<DocMiscType>").Append((int)documentmisc.DocMiscType).Append("</DocMiscType>");
			sb.Append("<RawBase64>").Append(SerializeStringEscapes.EscapeForXml(documentmisc.RawBase64)).Append("</RawBase64>");
			sb.Append("</DocumentMisc>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.DocumentMisc Deserialize(string xml) {
			OpenDentBusiness.DocumentMisc documentmisc=new OpenDentBusiness.DocumentMisc();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "DocMiscNum":
							documentmisc.DocMiscNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateCreated":
							documentmisc.DateCreated=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "FileName":
							documentmisc.FileName=reader.ReadContentAsString();
							break;
						case "DocMiscType":
							documentmisc.DocMiscType=(OpenDentBusiness.DocumentMiscType)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "RawBase64":
							documentmisc.RawBase64=reader.ReadContentAsString();
							break;
					}
				}
			}
			return documentmisc;
		}


	}
}