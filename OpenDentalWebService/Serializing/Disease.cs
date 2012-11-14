using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Disease {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Disease disease) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Disease>");
			sb.Append("<DiseaseNum>").Append(disease.DiseaseNum).Append("</DiseaseNum>");
			sb.Append("<PatNum>").Append(disease.PatNum).Append("</PatNum>");
			sb.Append("<DiseaseDefNum>").Append(disease.DiseaseDefNum).Append("</DiseaseDefNum>");
			sb.Append("<PatNote>").Append(SerializeStringEscapes.EscapeForXml(disease.PatNote)).Append("</PatNote>");
			sb.Append("<DateTStamp>").Append(disease.DateTStamp.ToString()).Append("</DateTStamp>");
			sb.Append("<ICD9Num>").Append(disease.ICD9Num).Append("</ICD9Num>");
			sb.Append("<ProbStatus>").Append((int)disease.ProbStatus).Append("</ProbStatus>");
			sb.Append("<DateStart>").Append(disease.DateStart.ToString()).Append("</DateStart>");
			sb.Append("<DateStop>").Append(disease.DateStop.ToString()).Append("</DateStop>");
			sb.Append("</Disease>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Disease Deserialize(string xml) {
			OpenDentBusiness.Disease disease=new OpenDentBusiness.Disease();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "DiseaseNum":
							disease.DiseaseNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							disease.PatNum=reader.ReadContentAsLong();
							break;
						case "DiseaseDefNum":
							disease.DiseaseDefNum=reader.ReadContentAsLong();
							break;
						case "PatNote":
							disease.PatNote=reader.ReadContentAsString();
							break;
						case "DateTStamp":
							disease.DateTStamp=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "ICD9Num":
							disease.ICD9Num=reader.ReadContentAsLong();
							break;
						case "ProbStatus":
							disease.ProbStatus=(OpenDentBusiness.ProblemStatus)reader.ReadContentAsInt();
							break;
						case "DateStart":
							disease.DateStart=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "DateStop":
							disease.DateStop=DateTime.Parse(reader.ReadContentAsString());
							break;
					}
				}
			}
			return disease;
		}


	}
}