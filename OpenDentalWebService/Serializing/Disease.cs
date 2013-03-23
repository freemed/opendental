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
			if(disease==null) {
				sb.Append("<null />");
				return sb.ToString();
			}
			sb.Append("<Disease>");
			sb.Append("<DiseaseNum>").Append(disease.DiseaseNum).Append("</DiseaseNum>");
			sb.Append("<PatNum>").Append(disease.PatNum).Append("</PatNum>");
			sb.Append("<DiseaseDefNum>").Append(disease.DiseaseDefNum).Append("</DiseaseDefNum>");
			sb.Append("<PatNote>").Append(SerializeStringEscapes.EscapeForXml(disease.PatNote)).Append("</PatNote>");
			sb.Append("<DateTStamp>").Append(disease.DateTStamp.ToString("yyyyMMddHHmmss")).Append("</DateTStamp>");
			sb.Append("<ICD9Num>").Append(disease.ICD9Num).Append("</ICD9Num>");
			sb.Append("<ProbStatus>").Append((int)disease.ProbStatus).Append("</ProbStatus>");
			sb.Append("<DateStart>").Append(disease.DateStart.ToString("yyyyMMddHHmmss")).Append("</DateStart>");
			sb.Append("<DateStop>").Append(disease.DateStop.ToString("yyyyMMddHHmmss")).Append("</DateStop>");
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
							disease.DiseaseNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							disease.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DiseaseDefNum":
							disease.DiseaseDefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNote":
							disease.PatNote=reader.ReadContentAsString();
							break;
						case "DateTStamp":
							disease.DateTStamp=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "ICD9Num":
							disease.ICD9Num=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ProbStatus":
							disease.ProbStatus=(OpenDentBusiness.ProblemStatus)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "DateStart":
							disease.DateStart=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateStop":
							disease.DateStop=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
					}
				}
			}
			return disease;
		}


	}
}