using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Dunning {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Dunning dunning) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Dunning>");
			sb.Append("<DunningNum>").Append(dunning.DunningNum).Append("</DunningNum>");
			sb.Append("<DunMessage>").Append(SerializeStringEscapes.EscapeForXml(dunning.DunMessage)).Append("</DunMessage>");
			sb.Append("<BillingType>").Append(dunning.BillingType).Append("</BillingType>");
			sb.Append("<AgeAccount>").Append(dunning.AgeAccount).Append("</AgeAccount>");
			sb.Append("<InsIsPending>").Append((int)dunning.InsIsPending).Append("</InsIsPending>");
			sb.Append("<MessageBold>").Append(SerializeStringEscapes.EscapeForXml(dunning.MessageBold)).Append("</MessageBold>");
			sb.Append("</Dunning>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Dunning Deserialize(string xml) {
			OpenDentBusiness.Dunning dunning=new OpenDentBusiness.Dunning();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "DunningNum":
							dunning.DunningNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DunMessage":
							dunning.DunMessage=reader.ReadContentAsString();
							break;
						case "BillingType":
							dunning.BillingType=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "AgeAccount":
							dunning.AgeAccount=System.Convert.ToByte(reader.ReadContentAsString());
							break;
						case "InsIsPending":
							dunning.InsIsPending=(OpenDentBusiness.YN)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "MessageBold":
							dunning.MessageBold=reader.ReadContentAsString();
							break;
					}
				}
			}
			return dunning;
		}


	}
}