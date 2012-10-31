using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class FeeSched {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.FeeSched feesched) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<FeeSched>");
			sb.Append("<FeeSchedNum>").Append(feesched.FeeSchedNum).Append("</FeeSchedNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(feesched.Description)).Append("</Description>");
			sb.Append("<FeeSchedType>").Append((int)feesched.FeeSchedType).Append("</FeeSchedType>");
			sb.Append("<ItemOrder>").Append(feesched.ItemOrder).Append("</ItemOrder>");
			sb.Append("<IsHidden>").Append((feesched.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("</FeeSched>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.FeeSched Deserialize(string xml) {
			OpenDentBusiness.FeeSched feesched=new OpenDentBusiness.FeeSched();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "FeeSchedNum":
							feesched.FeeSchedNum=reader.ReadContentAsLong();
							break;
						case "Description":
							feesched.Description=reader.ReadContentAsString();
							break;
						case "FeeSchedType":
							feesched.FeeSchedType=(OpenDentBusiness.FeeScheduleType)reader.ReadContentAsInt();
							break;
						case "ItemOrder":
							feesched.ItemOrder=reader.ReadContentAsInt();
							break;
						case "IsHidden":
							feesched.IsHidden=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return feesched;
		}


	}
}