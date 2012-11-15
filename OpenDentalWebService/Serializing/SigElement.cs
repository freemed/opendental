using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class SigElement {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.SigElement sigelement) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<SigElement>");
			sb.Append("<SigElementNum>").Append(sigelement.SigElementNum).Append("</SigElementNum>");
			sb.Append("<SigElementDefNum>").Append(sigelement.SigElementDefNum).Append("</SigElementDefNum>");
			sb.Append("<SignalNum>").Append(sigelement.SignalNum).Append("</SignalNum>");
			sb.Append("</SigElement>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.SigElement Deserialize(string xml) {
			OpenDentBusiness.SigElement sigelement=new OpenDentBusiness.SigElement();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SigElementNum":
							sigelement.SigElementNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "SigElementDefNum":
							sigelement.SigElementDefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "SignalNum":
							sigelement.SignalNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
					}
				}
			}
			return sigelement;
		}


	}
}