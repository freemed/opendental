using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class DrugManufacturer {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.DrugManufacturer drugmanufacturer) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<DrugManufacturer>");
			sb.Append("<DrugManufacturerNum>").Append(drugmanufacturer.DrugManufacturerNum).Append("</DrugManufacturerNum>");
			sb.Append("<ManufacturerName>").Append(SerializeStringEscapes.EscapeForXml(drugmanufacturer.ManufacturerName)).Append("</ManufacturerName>");
			sb.Append("<ManufacturerCode>").Append(SerializeStringEscapes.EscapeForXml(drugmanufacturer.ManufacturerCode)).Append("</ManufacturerCode>");
			sb.Append("</DrugManufacturer>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.DrugManufacturer Deserialize(string xml) {
			OpenDentBusiness.DrugManufacturer drugmanufacturer=new OpenDentBusiness.DrugManufacturer();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "DrugManufacturerNum":
							drugmanufacturer.DrugManufacturerNum=reader.ReadContentAsLong();
							break;
						case "ManufacturerName":
							drugmanufacturer.ManufacturerName=reader.ReadContentAsString();
							break;
						case "ManufacturerCode":
							drugmanufacturer.ManufacturerCode=reader.ReadContentAsString();
							break;
					}
				}
			}
			return drugmanufacturer;
		}


	}
}