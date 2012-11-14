using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ErxLog {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ErxLog erxlog) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ErxLog>");
			sb.Append("<ErxLogNum>").Append(erxlog.ErxLogNum).Append("</ErxLogNum>");
			sb.Append("<PatNum>").Append(erxlog.PatNum).Append("</PatNum>");
			sb.Append("<MsgText>").Append(SerializeStringEscapes.EscapeForXml(erxlog.MsgText)).Append("</MsgText>");
			sb.Append("<DateTStamp>").Append(erxlog.DateTStamp.ToString()).Append("</DateTStamp>");
			sb.Append("</ErxLog>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ErxLog Deserialize(string xml) {
			OpenDentBusiness.ErxLog erxlog=new OpenDentBusiness.ErxLog();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ErxLogNum":
							erxlog.ErxLogNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							erxlog.PatNum=reader.ReadContentAsLong();
							break;
						case "MsgText":
							erxlog.MsgText=reader.ReadContentAsString();
							break;
						case "DateTStamp":
							erxlog.DateTStamp=DateTime.Parse(reader.ReadContentAsString());
							break;
					}
				}
			}
			return erxlog;
		}


	}
}