using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class FormPat {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.FormPat formpat) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<FormPat>");
			sb.Append("<FormPatNum>").Append(formpat.FormPatNum).Append("</FormPatNum>");
			sb.Append("<PatNum>").Append(formpat.PatNum).Append("</PatNum>");
			sb.Append("<FormDateTime>").Append(formpat.FormDateTime.ToString("yyyyMMddHHmmss")).Append("</FormDateTime>");
			sb.Append("</FormPat>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.FormPat Deserialize(string xml) {
			OpenDentBusiness.FormPat formpat=new OpenDentBusiness.FormPat();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "FormPatNum":
							formpat.FormPatNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							formpat.PatNum=reader.ReadContentAsLong();
							break;
						case "FormDateTime":
							formpat.FormDateTime=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
					}
				}
			}
			return formpat;
		}


	}
}