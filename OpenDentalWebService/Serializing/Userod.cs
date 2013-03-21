using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Userod {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Userod userod) {
			StringBuilder sb=new StringBuilder();
			if(userod==null) {
				sb.Append("<null />");
				return sb.ToString();
			}
			sb.Append("<Userod>");
			sb.Append("<UserNum>").Append(userod.UserNum).Append("</UserNum>");
			sb.Append("<UserName>").Append(SerializeStringEscapes.EscapeForXml(userod.UserName)).Append("</UserName>");
			sb.Append("<Password>").Append(SerializeStringEscapes.EscapeForXml(userod.Password)).Append("</Password>");
			sb.Append("<UserGroupNum>").Append(userod.UserGroupNum).Append("</UserGroupNum>");
			sb.Append("<EmployeeNum>").Append(userod.EmployeeNum).Append("</EmployeeNum>");
			sb.Append("<ClinicNum>").Append(userod.ClinicNum).Append("</ClinicNum>");
			sb.Append("<ProvNum>").Append(userod.ProvNum).Append("</ProvNum>");
			sb.Append("<IsHidden>").Append((userod.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("<TaskListInBox>").Append(userod.TaskListInBox).Append("</TaskListInBox>");
			sb.Append("<AnesthProvType>").Append(userod.AnesthProvType).Append("</AnesthProvType>");
			sb.Append("<DefaultHidePopups>").Append((userod.DefaultHidePopups)?1:0).Append("</DefaultHidePopups>");
			sb.Append("<PasswordIsStrong>").Append((userod.PasswordIsStrong)?1:0).Append("</PasswordIsStrong>");
			sb.Append("<ClinicIsRestricted>").Append((userod.ClinicIsRestricted)?1:0).Append("</ClinicIsRestricted>");
			sb.Append("</Userod>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Userod Deserialize(string xml) {
			OpenDentBusiness.Userod userod=new OpenDentBusiness.Userod();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "UserNum":
							userod.UserNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "UserName":
							userod.UserName=reader.ReadContentAsString();
							break;
						case "Password":
							userod.Password=reader.ReadContentAsString();
							break;
						case "UserGroupNum":
							userod.UserGroupNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "EmployeeNum":
							userod.EmployeeNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ClinicNum":
							userod.ClinicNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ProvNum":
							userod.ProvNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "IsHidden":
							userod.IsHidden=reader.ReadContentAsString()!="0";
							break;
						case "TaskListInBox":
							userod.TaskListInBox=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "AnesthProvType":
							userod.AnesthProvType=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "DefaultHidePopups":
							userod.DefaultHidePopups=reader.ReadContentAsString()!="0";
							break;
						case "PasswordIsStrong":
							userod.PasswordIsStrong=reader.ReadContentAsString()!="0";
							break;
						case "ClinicIsRestricted":
							userod.ClinicIsRestricted=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return userod;
		}


	}
}