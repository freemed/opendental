using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class LetterMergeField {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.LetterMergeField lettermergefield) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<LetterMergeField>");
			sb.Append("<FieldNum>").Append(lettermergefield.FieldNum).Append("</FieldNum>");
			sb.Append("<LetterMergeNum>").Append(lettermergefield.LetterMergeNum).Append("</LetterMergeNum>");
			sb.Append("<FieldName>").Append(SerializeStringEscapes.EscapeForXml(lettermergefield.FieldName)).Append("</FieldName>");
			sb.Append("</LetterMergeField>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.LetterMergeField Deserialize(string xml) {
			OpenDentBusiness.LetterMergeField lettermergefield=new OpenDentBusiness.LetterMergeField();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "FieldNum":
							lettermergefield.FieldNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "LetterMergeNum":
							lettermergefield.LetterMergeNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "FieldName":
							lettermergefield.FieldName=reader.ReadContentAsString();
							break;
					}
				}
			}
			return lettermergefield;
		}


	}
}