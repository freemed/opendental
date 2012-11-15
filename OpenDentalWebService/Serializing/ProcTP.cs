using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ProcTP {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ProcTP proctp) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ProcTP>");
			sb.Append("<ProcTPNum>").Append(proctp.ProcTPNum).Append("</ProcTPNum>");
			sb.Append("<TreatPlanNum>").Append(proctp.TreatPlanNum).Append("</TreatPlanNum>");
			sb.Append("<PatNum>").Append(proctp.PatNum).Append("</PatNum>");
			sb.Append("<ProcNumOrig>").Append(proctp.ProcNumOrig).Append("</ProcNumOrig>");
			sb.Append("<ItemOrder>").Append(proctp.ItemOrder).Append("</ItemOrder>");
			sb.Append("<Priority>").Append(proctp.Priority).Append("</Priority>");
			sb.Append("<ToothNumTP>").Append(SerializeStringEscapes.EscapeForXml(proctp.ToothNumTP)).Append("</ToothNumTP>");
			sb.Append("<Surf>").Append(SerializeStringEscapes.EscapeForXml(proctp.Surf)).Append("</Surf>");
			sb.Append("<ProcCode>").Append(SerializeStringEscapes.EscapeForXml(proctp.ProcCode)).Append("</ProcCode>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(proctp.Descript)).Append("</Descript>");
			sb.Append("<FeeAmt>").Append(proctp.FeeAmt).Append("</FeeAmt>");
			sb.Append("<PriInsAmt>").Append(proctp.PriInsAmt).Append("</PriInsAmt>");
			sb.Append("<SecInsAmt>").Append(proctp.SecInsAmt).Append("</SecInsAmt>");
			sb.Append("<PatAmt>").Append(proctp.PatAmt).Append("</PatAmt>");
			sb.Append("<Discount>").Append(proctp.Discount).Append("</Discount>");
			sb.Append("<Prognosis>").Append(SerializeStringEscapes.EscapeForXml(proctp.Prognosis)).Append("</Prognosis>");
			sb.Append("<Dx>").Append(SerializeStringEscapes.EscapeForXml(proctp.Dx)).Append("</Dx>");
			sb.Append("</ProcTP>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ProcTP Deserialize(string xml) {
			OpenDentBusiness.ProcTP proctp=new OpenDentBusiness.ProcTP();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ProcTPNum":
							proctp.ProcTPNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "TreatPlanNum":
							proctp.TreatPlanNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							proctp.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ProcNumOrig":
							proctp.ProcNumOrig=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ItemOrder":
							proctp.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Priority":
							proctp.Priority=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ToothNumTP":
							proctp.ToothNumTP=reader.ReadContentAsString();
							break;
						case "Surf":
							proctp.Surf=reader.ReadContentAsString();
							break;
						case "ProcCode":
							proctp.ProcCode=reader.ReadContentAsString();
							break;
						case "Descript":
							proctp.Descript=reader.ReadContentAsString();
							break;
						case "FeeAmt":
							proctp.FeeAmt=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "PriInsAmt":
							proctp.PriInsAmt=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "SecInsAmt":
							proctp.SecInsAmt=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "PatAmt":
							proctp.PatAmt=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "Discount":
							proctp.Discount=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "Prognosis":
							proctp.Prognosis=reader.ReadContentAsString();
							break;
						case "Dx":
							proctp.Dx=reader.ReadContentAsString();
							break;
					}
				}
			}
			return proctp;
		}


	}
}