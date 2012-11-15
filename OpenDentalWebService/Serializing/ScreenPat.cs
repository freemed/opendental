using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ScreenPat {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ScreenPat screenpat) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ScreenPat>");
			sb.Append("<ScreenPatNum>").Append(screenpat.ScreenPatNum).Append("</ScreenPatNum>");
			sb.Append("<PatNum>").Append(screenpat.PatNum).Append("</PatNum>");
			sb.Append("<ScreenGroupNum>").Append(screenpat.ScreenGroupNum).Append("</ScreenGroupNum>");
			sb.Append("<SheetNum>").Append(screenpat.SheetNum).Append("</SheetNum>");
			sb.Append("</ScreenPat>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ScreenPat Deserialize(string xml) {
			OpenDentBusiness.ScreenPat screenpat=new OpenDentBusiness.ScreenPat();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ScreenPatNum":
							screenpat.ScreenPatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							screenpat.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ScreenGroupNum":
							screenpat.ScreenGroupNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "SheetNum":
							screenpat.SheetNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
					}
				}
			}
			return screenpat;
		}


	}
}