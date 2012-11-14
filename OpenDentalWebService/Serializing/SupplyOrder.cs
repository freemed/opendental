using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class SupplyOrder {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.SupplyOrder supplyorder) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<SupplyOrder>");
			sb.Append("<SupplyOrderNum>").Append(supplyorder.SupplyOrderNum).Append("</SupplyOrderNum>");
			sb.Append("<SupplierNum>").Append(supplyorder.SupplierNum).Append("</SupplierNum>");
			sb.Append("<DatePlaced>").Append(supplyorder.DatePlaced.ToString()).Append("</DatePlaced>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(supplyorder.Note)).Append("</Note>");
			sb.Append("<AmountTotal>").Append(supplyorder.AmountTotal).Append("</AmountTotal>");
			sb.Append("</SupplyOrder>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.SupplyOrder Deserialize(string xml) {
			OpenDentBusiness.SupplyOrder supplyorder=new OpenDentBusiness.SupplyOrder();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SupplyOrderNum":
							supplyorder.SupplyOrderNum=reader.ReadContentAsLong();
							break;
						case "SupplierNum":
							supplyorder.SupplierNum=reader.ReadContentAsLong();
							break;
						case "DatePlaced":
							supplyorder.DatePlaced=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "Note":
							supplyorder.Note=reader.ReadContentAsString();
							break;
						case "AmountTotal":
							supplyorder.AmountTotal=reader.ReadContentAsDouble();
							break;
					}
				}
			}
			return supplyorder;
		}


	}
}