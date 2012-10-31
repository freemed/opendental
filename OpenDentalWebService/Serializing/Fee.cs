using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Fee {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Fee fee) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Fee>");
			sb.Append("<FeeNum>").Append(fee.FeeNum).Append("</FeeNum>");
			sb.Append("<Amount>").Append(fee.Amount).Append("</Amount>");
			sb.Append("<OldCode>").Append(SerializeStringEscapes.EscapeForXml(fee.OldCode)).Append("</OldCode>");
			sb.Append("<FeeSched>").Append(fee.FeeSched).Append("</FeeSched>");
			sb.Append("<UseDefaultFee>").Append((fee.UseDefaultFee)?1:0).Append("</UseDefaultFee>");
			sb.Append("<UseDefaultCov>").Append((fee.UseDefaultCov)?1:0).Append("</UseDefaultCov>");
			sb.Append("<CodeNum>").Append(fee.CodeNum).Append("</CodeNum>");
			sb.Append("</Fee>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Fee Deserialize(string xml) {
			OpenDentBusiness.Fee fee=new OpenDentBusiness.Fee();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "FeeNum":
							fee.FeeNum=reader.ReadContentAsLong();
							break;
						case "Amount":
							fee.Amount=reader.ReadContentAsDouble();
							break;
						case "OldCode":
							fee.OldCode=reader.ReadContentAsString();
							break;
						case "FeeSched":
							fee.FeeSched=reader.ReadContentAsLong();
							break;
						case "UseDefaultFee":
							fee.UseDefaultFee=reader.ReadContentAsString()!="0";
							break;
						case "UseDefaultCov":
							fee.UseDefaultCov=reader.ReadContentAsString()!="0";
							break;
						case "CodeNum":
							fee.CodeNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return fee;
		}


	}
}