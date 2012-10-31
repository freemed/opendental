using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class EhrQuarterlyKey {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.EhrQuarterlyKey ehrquarterlykey) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<EhrQuarterlyKey>");
			sb.Append("<EhrQuarterlyKeyNum>").Append(ehrquarterlykey.EhrQuarterlyKeyNum).Append("</EhrQuarterlyKeyNum>");
			sb.Append("<YearValue>").Append(ehrquarterlykey.YearValue).Append("</YearValue>");
			sb.Append("<QuarterValue>").Append(ehrquarterlykey.QuarterValue).Append("</QuarterValue>");
			sb.Append("<PracticeName>").Append(SerializeStringEscapes.EscapeForXml(ehrquarterlykey.PracticeName)).Append("</PracticeName>");
			sb.Append("<KeyValue>").Append(SerializeStringEscapes.EscapeForXml(ehrquarterlykey.KeyValue)).Append("</KeyValue>");
			sb.Append("<PatNum>").Append(ehrquarterlykey.PatNum).Append("</PatNum>");
			sb.Append("<Notes>").Append(SerializeStringEscapes.EscapeForXml(ehrquarterlykey.Notes)).Append("</Notes>");
			sb.Append("</EhrQuarterlyKey>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.EhrQuarterlyKey Deserialize(string xml) {
			OpenDentBusiness.EhrQuarterlyKey ehrquarterlykey=new OpenDentBusiness.EhrQuarterlyKey();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EhrQuarterlyKeyNum":
							ehrquarterlykey.EhrQuarterlyKeyNum=reader.ReadContentAsLong();
							break;
						case "YearValue":
							ehrquarterlykey.YearValue=reader.ReadContentAsInt();
							break;
						case "QuarterValue":
							ehrquarterlykey.QuarterValue=reader.ReadContentAsInt();
							break;
						case "PracticeName":
							ehrquarterlykey.PracticeName=reader.ReadContentAsString();
							break;
						case "KeyValue":
							ehrquarterlykey.KeyValue=reader.ReadContentAsString();
							break;
						case "PatNum":
							ehrquarterlykey.PatNum=reader.ReadContentAsLong();
							break;
						case "Notes":
							ehrquarterlykey.Notes=reader.ReadContentAsString();
							break;
					}
				}
			}
			return ehrquarterlykey;
		}


	}
}