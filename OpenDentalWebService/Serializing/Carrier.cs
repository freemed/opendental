using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Carrier {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Carrier carrier) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Carrier>");
			sb.Append("<CarrierNum>").Append(carrier.CarrierNum).Append("</CarrierNum>");
			sb.Append("<CarrierName>").Append(SerializeStringEscapes.EscapeForXml(carrier.CarrierName)).Append("</CarrierName>");
			sb.Append("<Address>").Append(SerializeStringEscapes.EscapeForXml(carrier.Address)).Append("</Address>");
			sb.Append("<Address2>").Append(SerializeStringEscapes.EscapeForXml(carrier.Address2)).Append("</Address2>");
			sb.Append("<City>").Append(SerializeStringEscapes.EscapeForXml(carrier.City)).Append("</City>");
			sb.Append("<State>").Append(SerializeStringEscapes.EscapeForXml(carrier.State)).Append("</State>");
			sb.Append("<Zip>").Append(SerializeStringEscapes.EscapeForXml(carrier.Zip)).Append("</Zip>");
			sb.Append("<Phone>").Append(SerializeStringEscapes.EscapeForXml(carrier.Phone)).Append("</Phone>");
			sb.Append("<ElectID>").Append(SerializeStringEscapes.EscapeForXml(carrier.ElectID)).Append("</ElectID>");
			sb.Append("<NoSendElect>").Append((carrier.NoSendElect)?1:0).Append("</NoSendElect>");
			sb.Append("<IsCDA>").Append((carrier.IsCDA)?1:0).Append("</IsCDA>");
			sb.Append("<CDAnetVersion>").Append(SerializeStringEscapes.EscapeForXml(carrier.CDAnetVersion)).Append("</CDAnetVersion>");
			sb.Append("<CanadianNetworkNum>").Append(carrier.CanadianNetworkNum).Append("</CanadianNetworkNum>");
			sb.Append("<IsHidden>").Append((carrier.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("<CanadianEncryptionMethod>").Append(carrier.CanadianEncryptionMethod).Append("</CanadianEncryptionMethod>");
			sb.Append("<CanadianSupportedTypes>").Append((int)carrier.CanadianSupportedTypes).Append("</CanadianSupportedTypes>");
			sb.Append("</Carrier>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Carrier Deserialize(string xml) {
			OpenDentBusiness.Carrier carrier=new OpenDentBusiness.Carrier();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "CarrierNum":
							carrier.CarrierNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "CarrierName":
							carrier.CarrierName=reader.ReadContentAsString();
							break;
						case "Address":
							carrier.Address=reader.ReadContentAsString();
							break;
						case "Address2":
							carrier.Address2=reader.ReadContentAsString();
							break;
						case "City":
							carrier.City=reader.ReadContentAsString();
							break;
						case "State":
							carrier.State=reader.ReadContentAsString();
							break;
						case "Zip":
							carrier.Zip=reader.ReadContentAsString();
							break;
						case "Phone":
							carrier.Phone=reader.ReadContentAsString();
							break;
						case "ElectID":
							carrier.ElectID=reader.ReadContentAsString();
							break;
						case "NoSendElect":
							carrier.NoSendElect=reader.ReadContentAsString()!="0";
							break;
						case "IsCDA":
							carrier.IsCDA=reader.ReadContentAsString()!="0";
							break;
						case "CDAnetVersion":
							carrier.CDAnetVersion=reader.ReadContentAsString();
							break;
						case "CanadianNetworkNum":
							carrier.CanadianNetworkNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "IsHidden":
							carrier.IsHidden=reader.ReadContentAsString()!="0";
							break;
						case "CanadianEncryptionMethod":
							carrier.CanadianEncryptionMethod=System.Convert.ToByte(reader.ReadContentAsString());
							break;
						case "CanadianSupportedTypes":
							carrier.CanadianSupportedTypes=(OpenDentBusiness.CanSupTransTypes)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return carrier;
		}


	}
}