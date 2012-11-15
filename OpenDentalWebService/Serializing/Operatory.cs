using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Operatory {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Operatory operatory) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Operatory>");
			sb.Append("<OperatoryNum>").Append(operatory.OperatoryNum).Append("</OperatoryNum>");
			sb.Append("<OpName>").Append(SerializeStringEscapes.EscapeForXml(operatory.OpName)).Append("</OpName>");
			sb.Append("<Abbrev>").Append(SerializeStringEscapes.EscapeForXml(operatory.Abbrev)).Append("</Abbrev>");
			sb.Append("<ItemOrder>").Append(operatory.ItemOrder).Append("</ItemOrder>");
			sb.Append("<IsHidden>").Append((operatory.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("<ProvDentist>").Append(operatory.ProvDentist).Append("</ProvDentist>");
			sb.Append("<ProvHygienist>").Append(operatory.ProvHygienist).Append("</ProvHygienist>");
			sb.Append("<IsHygiene>").Append((operatory.IsHygiene)?1:0).Append("</IsHygiene>");
			sb.Append("<ClinicNum>").Append(operatory.ClinicNum).Append("</ClinicNum>");
			sb.Append("<SetProspective>").Append((operatory.SetProspective)?1:0).Append("</SetProspective>");
			sb.Append("</Operatory>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Operatory Deserialize(string xml) {
			OpenDentBusiness.Operatory operatory=new OpenDentBusiness.Operatory();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "OperatoryNum":
							operatory.OperatoryNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "OpName":
							operatory.OpName=reader.ReadContentAsString();
							break;
						case "Abbrev":
							operatory.Abbrev=reader.ReadContentAsString();
							break;
						case "ItemOrder":
							operatory.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "IsHidden":
							operatory.IsHidden=reader.ReadContentAsString()!="0";
							break;
						case "ProvDentist":
							operatory.ProvDentist=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ProvHygienist":
							operatory.ProvHygienist=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "IsHygiene":
							operatory.IsHygiene=reader.ReadContentAsString()!="0";
							break;
						case "ClinicNum":
							operatory.ClinicNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "SetProspective":
							operatory.SetProspective=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return operatory;
		}


	}
}