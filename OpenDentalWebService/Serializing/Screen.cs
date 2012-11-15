using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Screen {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Screen screen) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Screen>");
			sb.Append("<ScreenNum>").Append(screen.ScreenNum).Append("</ScreenNum>");
			sb.Append("<ScreenDate>").Append(screen.ScreenDate.ToString("yyyyMMddHHmmss")).Append("</ScreenDate>");
			sb.Append("<GradeSchool>").Append(SerializeStringEscapes.EscapeForXml(screen.GradeSchool)).Append("</GradeSchool>");
			sb.Append("<County>").Append(SerializeStringEscapes.EscapeForXml(screen.County)).Append("</County>");
			sb.Append("<PlaceService>").Append((int)screen.PlaceService).Append("</PlaceService>");
			sb.Append("<ProvNum>").Append(screen.ProvNum).Append("</ProvNum>");
			sb.Append("<ProvName>").Append(SerializeStringEscapes.EscapeForXml(screen.ProvName)).Append("</ProvName>");
			sb.Append("<Gender>").Append((int)screen.Gender).Append("</Gender>");
			sb.Append("<Race>").Append((int)screen.Race).Append("</Race>");
			sb.Append("<GradeLevel>").Append((int)screen.GradeLevel).Append("</GradeLevel>");
			sb.Append("<Age>").Append(screen.Age).Append("</Age>");
			sb.Append("<Urgency>").Append((int)screen.Urgency).Append("</Urgency>");
			sb.Append("<HasCaries>").Append((int)screen.HasCaries).Append("</HasCaries>");
			sb.Append("<NeedsSealants>").Append((int)screen.NeedsSealants).Append("</NeedsSealants>");
			sb.Append("<CariesExperience>").Append((int)screen.CariesExperience).Append("</CariesExperience>");
			sb.Append("<EarlyChildCaries>").Append((int)screen.EarlyChildCaries).Append("</EarlyChildCaries>");
			sb.Append("<ExistingSealants>").Append((int)screen.ExistingSealants).Append("</ExistingSealants>");
			sb.Append("<MissingAllTeeth>").Append((int)screen.MissingAllTeeth).Append("</MissingAllTeeth>");
			sb.Append("<Birthdate>").Append(screen.Birthdate.ToString("yyyyMMddHHmmss")).Append("</Birthdate>");
			sb.Append("<ScreenGroupNum>").Append(screen.ScreenGroupNum).Append("</ScreenGroupNum>");
			sb.Append("<ScreenGroupOrder>").Append(screen.ScreenGroupOrder).Append("</ScreenGroupOrder>");
			sb.Append("<Comments>").Append(SerializeStringEscapes.EscapeForXml(screen.Comments)).Append("</Comments>");
			sb.Append("</Screen>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Screen Deserialize(string xml) {
			OpenDentBusiness.Screen screen=new OpenDentBusiness.Screen();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ScreenNum":
							screen.ScreenNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ScreenDate":
							screen.ScreenDate=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "GradeSchool":
							screen.GradeSchool=reader.ReadContentAsString();
							break;
						case "County":
							screen.County=reader.ReadContentAsString();
							break;
						case "PlaceService":
							screen.PlaceService=(OpenDentBusiness.PlaceOfService)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ProvNum":
							screen.ProvNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ProvName":
							screen.ProvName=reader.ReadContentAsString();
							break;
						case "Gender":
							screen.Gender=(OpenDentBusiness.PatientGender)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Race":
							screen.Race=(OpenDentBusiness.PatientRace)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "GradeLevel":
							screen.GradeLevel=(OpenDentBusiness.PatientGrade)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Age":
							screen.Age=System.Convert.ToByte(reader.ReadContentAsString());
							break;
						case "Urgency":
							screen.Urgency=(OpenDentBusiness.TreatmentUrgency)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "HasCaries":
							screen.HasCaries=(OpenDentBusiness.YN)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "NeedsSealants":
							screen.NeedsSealants=(OpenDentBusiness.YN)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "CariesExperience":
							screen.CariesExperience=(OpenDentBusiness.YN)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "EarlyChildCaries":
							screen.EarlyChildCaries=(OpenDentBusiness.YN)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ExistingSealants":
							screen.ExistingSealants=(OpenDentBusiness.YN)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "MissingAllTeeth":
							screen.MissingAllTeeth=(OpenDentBusiness.YN)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Birthdate":
							screen.Birthdate=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "ScreenGroupNum":
							screen.ScreenGroupNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ScreenGroupOrder":
							screen.ScreenGroupOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Comments":
							screen.Comments=reader.ReadContentAsString();
							break;
					}
				}
			}
			return screen;
		}


	}
}