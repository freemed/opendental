using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class PatField {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.PatField patfield) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<PatField>");
			sb.Append("<PatFieldNum>").Append(patfield.PatFieldNum).Append("</PatFieldNum>");
			sb.Append("<PatNum>").Append(patfield.PatNum).Append("</PatNum>");
			sb.Append("<FieldName>").Append(SerializeStringEscapes.EscapeForXml(patfield.FieldName)).Append("</FieldName>");
			sb.Append("<FieldValue>").Append(SerializeStringEscapes.EscapeForXml(patfield.FieldValue)).Append("</FieldValue>");
			sb.Append("</PatField>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.PatField Deserialize(string xml) {
			OpenDentBusiness.PatField patfield=new OpenDentBusiness.PatField();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PatFieldNum":
							patfield.PatFieldNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							patfield.PatNum=reader.ReadContentAsLong();
							break;
						case "FieldName":
							patfield.FieldName=reader.ReadContentAsString();
							break;
						case "FieldValue":
							patfield.FieldValue=reader.ReadContentAsString();
							break;
					}
				}
			}
			return patfield;
		}


	}
}