using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class RxAlert {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.RxAlert rxalert) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<RxAlert>");
			sb.Append("<RxAlertNum>").Append(rxalert.RxAlertNum).Append("</RxAlertNum>");
			sb.Append("<RxDefNum>").Append(rxalert.RxDefNum).Append("</RxDefNum>");
			sb.Append("<DiseaseDefNum>").Append(rxalert.DiseaseDefNum).Append("</DiseaseDefNum>");
			sb.Append("<AllergyDefNum>").Append(rxalert.AllergyDefNum).Append("</AllergyDefNum>");
			sb.Append("<MedicationNum>").Append(rxalert.MedicationNum).Append("</MedicationNum>");
			sb.Append("<NotificationMsg>").Append(SerializeStringEscapes.EscapeForXml(rxalert.NotificationMsg)).Append("</NotificationMsg>");
			sb.Append("</RxAlert>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.RxAlert Deserialize(string xml) {
			OpenDentBusiness.RxAlert rxalert=new OpenDentBusiness.RxAlert();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "RxAlertNum":
							rxalert.RxAlertNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "RxDefNum":
							rxalert.RxDefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DiseaseDefNum":
							rxalert.DiseaseDefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "AllergyDefNum":
							rxalert.AllergyDefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "MedicationNum":
							rxalert.MedicationNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "NotificationMsg":
							rxalert.NotificationMsg=reader.ReadContentAsString();
							break;
					}
				}
			}
			return rxalert;
		}


	}
}