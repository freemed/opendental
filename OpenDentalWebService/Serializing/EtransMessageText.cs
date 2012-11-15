using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class EtransMessageText {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.EtransMessageText etransmessagetext) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<EtransMessageText>");
			sb.Append("<EtransMessageTextNum>").Append(etransmessagetext.EtransMessageTextNum).Append("</EtransMessageTextNum>");
			sb.Append("<MessageText>").Append(SerializeStringEscapes.EscapeForXml(etransmessagetext.MessageText)).Append("</MessageText>");
			sb.Append("</EtransMessageText>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.EtransMessageText Deserialize(string xml) {
			OpenDentBusiness.EtransMessageText etransmessagetext=new OpenDentBusiness.EtransMessageText();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EtransMessageTextNum":
							etransmessagetext.EtransMessageTextNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "MessageText":
							etransmessagetext.MessageText=reader.ReadContentAsString();
							break;
					}
				}
			}
			return etransmessagetext;
		}


	}
}