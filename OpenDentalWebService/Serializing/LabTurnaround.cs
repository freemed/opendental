using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class LabTurnaround {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.LabTurnaround labturnaround) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<LabTurnaround>");
			sb.Append("<LabTurnaroundNum>").Append(labturnaround.LabTurnaroundNum).Append("</LabTurnaroundNum>");
			sb.Append("<LaboratoryNum>").Append(labturnaround.LaboratoryNum).Append("</LaboratoryNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(labturnaround.Description)).Append("</Description>");
			sb.Append("<DaysPublished>").Append(labturnaround.DaysPublished).Append("</DaysPublished>");
			sb.Append("<DaysActual>").Append(labturnaround.DaysActual).Append("</DaysActual>");
			sb.Append("</LabTurnaround>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.LabTurnaround Deserialize(string xml) {
			OpenDentBusiness.LabTurnaround labturnaround=new OpenDentBusiness.LabTurnaround();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "LabTurnaroundNum":
							labturnaround.LabTurnaroundNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "LaboratoryNum":
							labturnaround.LaboratoryNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Description":
							labturnaround.Description=reader.ReadContentAsString();
							break;
						case "DaysPublished":
							labturnaround.DaysPublished=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "DaysActual":
							labturnaround.DaysActual=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return labturnaround;
		}


	}
}