using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Supply {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Supply supply) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Supply>");
			sb.Append("<SupplyNum>").Append(supply.SupplyNum).Append("</SupplyNum>");
			sb.Append("<SupplierNum>").Append(supply.SupplierNum).Append("</SupplierNum>");
			sb.Append("<CatalogNumber>").Append(SerializeStringEscapes.EscapeForXml(supply.CatalogNumber)).Append("</CatalogNumber>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(supply.Descript)).Append("</Descript>");
			sb.Append("<Category>").Append(supply.Category).Append("</Category>");
			sb.Append("<ItemOrder>").Append(supply.ItemOrder).Append("</ItemOrder>");
			sb.Append("<LevelDesired>").Append(supply.LevelDesired).Append("</LevelDesired>");
			sb.Append("<IsHidden>").Append((supply.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("<Price>").Append(supply.Price).Append("</Price>");
			sb.Append("</Supply>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Supply Deserialize(string xml) {
			OpenDentBusiness.Supply supply=new OpenDentBusiness.Supply();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SupplyNum":
							supply.SupplyNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "SupplierNum":
							supply.SupplierNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "CatalogNumber":
							supply.CatalogNumber=reader.ReadContentAsString();
							break;
						case "Descript":
							supply.Descript=reader.ReadContentAsString();
							break;
						case "Category":
							supply.Category=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ItemOrder":
							supply.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "LevelDesired":
							supply.LevelDesired=System.Convert.ToSingle(reader.ReadContentAsString());
							break;
						case "IsHidden":
							supply.IsHidden=reader.ReadContentAsString()!="0";
							break;
						case "Price":
							supply.Price=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
					}
				}
			}
			return supply;
		}


	}
}