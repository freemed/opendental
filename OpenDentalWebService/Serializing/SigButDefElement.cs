using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class SigButDefElement {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.SigButDefElement sigbutdefelement) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<SigButDefElement>");
			sb.Append("<ElementNum>").Append(sigbutdefelement.ElementNum).Append("</ElementNum>");
			sb.Append("<SigButDefNum>").Append(sigbutdefelement.SigButDefNum).Append("</SigButDefNum>");
			sb.Append("<SigElementDefNum>").Append(sigbutdefelement.SigElementDefNum).Append("</SigElementDefNum>");
			sb.Append("</SigButDefElement>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.SigButDefElement Deserialize(string xml) {
			OpenDentBusiness.SigButDefElement sigbutdefelement=new OpenDentBusiness.SigButDefElement();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ElementNum":
							sigbutdefelement.ElementNum=reader.ReadContentAsLong();
							break;
						case "SigButDefNum":
							sigbutdefelement.SigButDefNum=reader.ReadContentAsLong();
							break;
						case "SigElementDefNum":
							sigbutdefelement.SigElementDefNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return sigbutdefelement;
		}


	}
}