using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class MedicalOrder {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.MedicalOrder medicalorder) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<MedicalOrder>");
			sb.Append("<MedicalOrderNum>").Append(medicalorder.MedicalOrderNum).Append("</MedicalOrderNum>");
			sb.Append("<MedOrderType>").Append((int)medicalorder.MedOrderType).Append("</MedOrderType>");
			sb.Append("<PatNum>").Append(medicalorder.PatNum).Append("</PatNum>");
			sb.Append("<DateTimeOrder>").Append(medicalorder.DateTimeOrder.ToString()).Append("</DateTimeOrder>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(medicalorder.Description)).Append("</Description>");
			sb.Append("<IsDiscontinued>").Append((medicalorder.IsDiscontinued)?1:0).Append("</IsDiscontinued>");
			sb.Append("<ProvNum>").Append(medicalorder.ProvNum).Append("</ProvNum>");
			sb.Append("</MedicalOrder>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.MedicalOrder Deserialize(string xml) {
			OpenDentBusiness.MedicalOrder medicalorder=new OpenDentBusiness.MedicalOrder();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "MedicalOrderNum":
							medicalorder.MedicalOrderNum=reader.ReadContentAsLong();
							break;
						case "MedOrderType":
							medicalorder.MedOrderType=(OpenDentBusiness.MedicalOrderType)reader.ReadContentAsInt();
							break;
						case "PatNum":
							medicalorder.PatNum=reader.ReadContentAsLong();
							break;
						case "DateTimeOrder":
							medicalorder.DateTimeOrder=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "Description":
							medicalorder.Description=reader.ReadContentAsString();
							break;
						case "IsDiscontinued":
							medicalorder.IsDiscontinued=reader.ReadContentAsString()!="0";
							break;
						case "ProvNum":
							medicalorder.ProvNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return medicalorder;
		}


	}
}