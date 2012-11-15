using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ElectID {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ElectID electid) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ElectID>");
			sb.Append("<ElectIDNum>").Append(electid.ElectIDNum).Append("</ElectIDNum>");
			sb.Append("<PayorID>").Append(SerializeStringEscapes.EscapeForXml(electid.PayorID)).Append("</PayorID>");
			sb.Append("<CarrierName>").Append(SerializeStringEscapes.EscapeForXml(electid.CarrierName)).Append("</CarrierName>");
			sb.Append("<IsMedicaid>").Append((electid.IsMedicaid)?1:0).Append("</IsMedicaid>");
			sb.Append("<ProviderTypes>").Append(SerializeStringEscapes.EscapeForXml(electid.ProviderTypes)).Append("</ProviderTypes>");
			sb.Append("<Comments>").Append(SerializeStringEscapes.EscapeForXml(electid.Comments)).Append("</Comments>");
			sb.Append("</ElectID>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ElectID Deserialize(string xml) {
			OpenDentBusiness.ElectID electid=new OpenDentBusiness.ElectID();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ElectIDNum":
							electid.ElectIDNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PayorID":
							electid.PayorID=reader.ReadContentAsString();
							break;
						case "CarrierName":
							electid.CarrierName=reader.ReadContentAsString();
							break;
						case "IsMedicaid":
							electid.IsMedicaid=reader.ReadContentAsString()!="0";
							break;
						case "ProviderTypes":
							electid.ProviderTypes=reader.ReadContentAsString();
							break;
						case "Comments":
							electid.Comments=reader.ReadContentAsString();
							break;
					}
				}
			}
			return electid;
		}


	}
}