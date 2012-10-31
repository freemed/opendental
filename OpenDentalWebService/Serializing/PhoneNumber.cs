using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class PhoneNumber {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.PhoneNumber phonenumber) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<PhoneNumber>");
			sb.Append("<PhoneNumberNum>").Append(phonenumber.PhoneNumberNum).Append("</PhoneNumberNum>");
			sb.Append("<PatNum>").Append(phonenumber.PatNum).Append("</PatNum>");
			sb.Append("<PhoneNumberVal>").Append(SerializeStringEscapes.EscapeForXml(phonenumber.PhoneNumberVal)).Append("</PhoneNumberVal>");
			sb.Append("</PhoneNumber>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.PhoneNumber Deserialize(string xml) {
			OpenDentBusiness.PhoneNumber phonenumber=new OpenDentBusiness.PhoneNumber();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PhoneNumberNum":
							phonenumber.PhoneNumberNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							phonenumber.PatNum=reader.ReadContentAsLong();
							break;
						case "PhoneNumberVal":
							phonenumber.PhoneNumberVal=reader.ReadContentAsString();
							break;
					}
				}
			}
			return phonenumber;
		}


	}
}