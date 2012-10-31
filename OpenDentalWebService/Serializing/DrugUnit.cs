using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class DrugUnit {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.DrugUnit drugunit) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<DrugUnit>");
			sb.Append("<DrugUnitNum>").Append(drugunit.DrugUnitNum).Append("</DrugUnitNum>");
			sb.Append("<UnitIdentifier>").Append(SerializeStringEscapes.EscapeForXml(drugunit.UnitIdentifier)).Append("</UnitIdentifier>");
			sb.Append("<UnitText>").Append(SerializeStringEscapes.EscapeForXml(drugunit.UnitText)).Append("</UnitText>");
			sb.Append("</DrugUnit>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.DrugUnit Deserialize(string xml) {
			OpenDentBusiness.DrugUnit drugunit=new OpenDentBusiness.DrugUnit();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "DrugUnitNum":
							drugunit.DrugUnitNum=reader.ReadContentAsLong();
							break;
						case "UnitIdentifier":
							drugunit.UnitIdentifier=reader.ReadContentAsString();
							break;
						case "UnitText":
							drugunit.UnitText=reader.ReadContentAsString();
							break;
					}
				}
			}
			return drugunit;
		}


	}
}