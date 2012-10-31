using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class PatFieldDef {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.PatFieldDef patfielddef) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<PatFieldDef>");
			sb.Append("<PatFieldDefNum>").Append(patfielddef.PatFieldDefNum).Append("</PatFieldDefNum>");
			sb.Append("<FieldName>").Append(SerializeStringEscapes.EscapeForXml(patfielddef.FieldName)).Append("</FieldName>");
			sb.Append("<FieldType>").Append((int)patfielddef.FieldType).Append("</FieldType>");
			sb.Append("<PickList>").Append(SerializeStringEscapes.EscapeForXml(patfielddef.PickList)).Append("</PickList>");
			sb.Append("</PatFieldDef>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.PatFieldDef Deserialize(string xml) {
			OpenDentBusiness.PatFieldDef patfielddef=new OpenDentBusiness.PatFieldDef();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PatFieldDefNum":
							patfielddef.PatFieldDefNum=reader.ReadContentAsLong();
							break;
						case "FieldName":
							patfielddef.FieldName=reader.ReadContentAsString();
							break;
						case "FieldType":
							patfielddef.FieldType=(OpenDentBusiness.PatFieldType)reader.ReadContentAsInt();
							break;
						case "PickList":
							patfielddef.PickList=reader.ReadContentAsString();
							break;
					}
				}
			}
			return patfielddef;
		}


	}
}