using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Supplier {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Supplier supplier) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Supplier>");
			sb.Append("<SupplierNum>").Append(supplier.SupplierNum).Append("</SupplierNum>");
			sb.Append("<Name>").Append(SerializeStringEscapes.EscapeForXml(supplier.Name)).Append("</Name>");
			sb.Append("<Phone>").Append(SerializeStringEscapes.EscapeForXml(supplier.Phone)).Append("</Phone>");
			sb.Append("<CustomerId>").Append(SerializeStringEscapes.EscapeForXml(supplier.CustomerId)).Append("</CustomerId>");
			sb.Append("<Website>").Append(SerializeStringEscapes.EscapeForXml(supplier.Website)).Append("</Website>");
			sb.Append("<UserName>").Append(SerializeStringEscapes.EscapeForXml(supplier.UserName)).Append("</UserName>");
			sb.Append("<Password>").Append(SerializeStringEscapes.EscapeForXml(supplier.Password)).Append("</Password>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(supplier.Note)).Append("</Note>");
			sb.Append("</Supplier>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Supplier Deserialize(string xml) {
			OpenDentBusiness.Supplier supplier=new OpenDentBusiness.Supplier();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SupplierNum":
							supplier.SupplierNum=reader.ReadContentAsLong();
							break;
						case "Name":
							supplier.Name=reader.ReadContentAsString();
							break;
						case "Phone":
							supplier.Phone=reader.ReadContentAsString();
							break;
						case "CustomerId":
							supplier.CustomerId=reader.ReadContentAsString();
							break;
						case "Website":
							supplier.Website=reader.ReadContentAsString();
							break;
						case "UserName":
							supplier.UserName=reader.ReadContentAsString();
							break;
						case "Password":
							supplier.Password=reader.ReadContentAsString();
							break;
						case "Note":
							supplier.Note=reader.ReadContentAsString();
							break;
					}
				}
			}
			return supplier;
		}


	}
}