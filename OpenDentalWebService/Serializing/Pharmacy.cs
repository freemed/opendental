using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Pharmacy {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Pharmacy pharmacy) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Pharmacy>");
			sb.Append("<PharmacyNum>").Append(pharmacy.PharmacyNum).Append("</PharmacyNum>");
			sb.Append("<PharmID>").Append(SerializeStringEscapes.EscapeForXml(pharmacy.PharmID)).Append("</PharmID>");
			sb.Append("<StoreName>").Append(SerializeStringEscapes.EscapeForXml(pharmacy.StoreName)).Append("</StoreName>");
			sb.Append("<Phone>").Append(SerializeStringEscapes.EscapeForXml(pharmacy.Phone)).Append("</Phone>");
			sb.Append("<Fax>").Append(SerializeStringEscapes.EscapeForXml(pharmacy.Fax)).Append("</Fax>");
			sb.Append("<Address>").Append(SerializeStringEscapes.EscapeForXml(pharmacy.Address)).Append("</Address>");
			sb.Append("<Address2>").Append(SerializeStringEscapes.EscapeForXml(pharmacy.Address2)).Append("</Address2>");
			sb.Append("<City>").Append(SerializeStringEscapes.EscapeForXml(pharmacy.City)).Append("</City>");
			sb.Append("<State>").Append(SerializeStringEscapes.EscapeForXml(pharmacy.State)).Append("</State>");
			sb.Append("<Zip>").Append(SerializeStringEscapes.EscapeForXml(pharmacy.Zip)).Append("</Zip>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(pharmacy.Note)).Append("</Note>");
			sb.Append("<DateTStamp>").Append(pharmacy.DateTStamp.ToString("yyyyMMddHHmmss")).Append("</DateTStamp>");
			sb.Append("</Pharmacy>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Pharmacy Deserialize(string xml) {
			OpenDentBusiness.Pharmacy pharmacy=new OpenDentBusiness.Pharmacy();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PharmacyNum":
							pharmacy.PharmacyNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PharmID":
							pharmacy.PharmID=reader.ReadContentAsString();
							break;
						case "StoreName":
							pharmacy.StoreName=reader.ReadContentAsString();
							break;
						case "Phone":
							pharmacy.Phone=reader.ReadContentAsString();
							break;
						case "Fax":
							pharmacy.Fax=reader.ReadContentAsString();
							break;
						case "Address":
							pharmacy.Address=reader.ReadContentAsString();
							break;
						case "Address2":
							pharmacy.Address2=reader.ReadContentAsString();
							break;
						case "City":
							pharmacy.City=reader.ReadContentAsString();
							break;
						case "State":
							pharmacy.State=reader.ReadContentAsString();
							break;
						case "Zip":
							pharmacy.Zip=reader.ReadContentAsString();
							break;
						case "Note":
							pharmacy.Note=reader.ReadContentAsString();
							break;
						case "DateTStamp":
							pharmacy.DateTStamp=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
					}
				}
			}
			return pharmacy;
		}


	}
}