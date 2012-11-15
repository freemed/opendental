using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ApptField {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ApptField apptfield) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ApptField>");
			sb.Append("<ApptFieldNum>").Append(apptfield.ApptFieldNum).Append("</ApptFieldNum>");
			sb.Append("<AptNum>").Append(apptfield.AptNum).Append("</AptNum>");
			sb.Append("<FieldName>").Append(SerializeStringEscapes.EscapeForXml(apptfield.FieldName)).Append("</FieldName>");
			sb.Append("<FieldValue>").Append(SerializeStringEscapes.EscapeForXml(apptfield.FieldValue)).Append("</FieldValue>");
			sb.Append("</ApptField>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ApptField Deserialize(string xml) {
			OpenDentBusiness.ApptField apptfield=new OpenDentBusiness.ApptField();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ApptFieldNum":
							apptfield.ApptFieldNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "AptNum":
							apptfield.AptNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "FieldName":
							apptfield.FieldName=reader.ReadContentAsString();
							break;
						case "FieldValue":
							apptfield.FieldValue=reader.ReadContentAsString();
							break;
					}
				}
			}
			return apptfield;
		}


	}
}