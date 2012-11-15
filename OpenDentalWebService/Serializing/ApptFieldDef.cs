using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ApptFieldDef {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ApptFieldDef apptfielddef) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ApptFieldDef>");
			sb.Append("<ApptFieldDefNum>").Append(apptfielddef.ApptFieldDefNum).Append("</ApptFieldDefNum>");
			sb.Append("<FieldName>").Append(SerializeStringEscapes.EscapeForXml(apptfielddef.FieldName)).Append("</FieldName>");
			sb.Append("<FieldType>").Append((int)apptfielddef.FieldType).Append("</FieldType>");
			sb.Append("<PickList>").Append(SerializeStringEscapes.EscapeForXml(apptfielddef.PickList)).Append("</PickList>");
			sb.Append("</ApptFieldDef>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ApptFieldDef Deserialize(string xml) {
			OpenDentBusiness.ApptFieldDef apptfielddef=new OpenDentBusiness.ApptFieldDef();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ApptFieldDefNum":
							apptfielddef.ApptFieldDefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "FieldName":
							apptfielddef.FieldName=reader.ReadContentAsString();
							break;
						case "FieldType":
							apptfielddef.FieldType=(OpenDentBusiness.ApptFieldType)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "PickList":
							apptfielddef.PickList=reader.ReadContentAsString();
							break;
					}
				}
			}
			return apptfielddef;
		}


	}
}