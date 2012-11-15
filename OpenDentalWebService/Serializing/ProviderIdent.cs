using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ProviderIdent {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ProviderIdent providerident) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ProviderIdent>");
			sb.Append("<ProviderIdentNum>").Append(providerident.ProviderIdentNum).Append("</ProviderIdentNum>");
			sb.Append("<ProvNum>").Append(providerident.ProvNum).Append("</ProvNum>");
			sb.Append("<PayorID>").Append(SerializeStringEscapes.EscapeForXml(providerident.PayorID)).Append("</PayorID>");
			sb.Append("<SuppIDType>").Append((int)providerident.SuppIDType).Append("</SuppIDType>");
			sb.Append("<IDNumber>").Append(SerializeStringEscapes.EscapeForXml(providerident.IDNumber)).Append("</IDNumber>");
			sb.Append("</ProviderIdent>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ProviderIdent Deserialize(string xml) {
			OpenDentBusiness.ProviderIdent providerident=new OpenDentBusiness.ProviderIdent();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ProviderIdentNum":
							providerident.ProviderIdentNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ProvNum":
							providerident.ProvNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PayorID":
							providerident.PayorID=reader.ReadContentAsString();
							break;
						case "SuppIDType":
							providerident.SuppIDType=(OpenDentBusiness.ProviderSupplementalID)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "IDNumber":
							providerident.IDNumber=reader.ReadContentAsString();
							break;
					}
				}
			}
			return providerident;
		}


	}
}