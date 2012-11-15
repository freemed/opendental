using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ZipCode {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ZipCode zipcode) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ZipCode>");
			sb.Append("<ZipCodeNum>").Append(zipcode.ZipCodeNum).Append("</ZipCodeNum>");
			sb.Append("<ZipCodeDigits>").Append(SerializeStringEscapes.EscapeForXml(zipcode.ZipCodeDigits)).Append("</ZipCodeDigits>");
			sb.Append("<City>").Append(SerializeStringEscapes.EscapeForXml(zipcode.City)).Append("</City>");
			sb.Append("<State>").Append(SerializeStringEscapes.EscapeForXml(zipcode.State)).Append("</State>");
			sb.Append("<IsFrequent>").Append((zipcode.IsFrequent)?1:0).Append("</IsFrequent>");
			sb.Append("</ZipCode>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ZipCode Deserialize(string xml) {
			OpenDentBusiness.ZipCode zipcode=new OpenDentBusiness.ZipCode();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ZipCodeNum":
							zipcode.ZipCodeNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ZipCodeDigits":
							zipcode.ZipCodeDigits=reader.ReadContentAsString();
							break;
						case "City":
							zipcode.City=reader.ReadContentAsString();
							break;
						case "State":
							zipcode.State=reader.ReadContentAsString();
							break;
						case "IsFrequent":
							zipcode.IsFrequent=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return zipcode;
		}


	}
}