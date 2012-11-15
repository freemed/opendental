using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class GroupPermission {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.GroupPermission grouppermission) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<GroupPermission>");
			sb.Append("<GroupPermNum>").Append(grouppermission.GroupPermNum).Append("</GroupPermNum>");
			sb.Append("<NewerDate>").Append(grouppermission.NewerDate.ToString("yyyyMMddHHmmss")).Append("</NewerDate>");
			sb.Append("<NewerDays>").Append(grouppermission.NewerDays).Append("</NewerDays>");
			sb.Append("<UserGroupNum>").Append(grouppermission.UserGroupNum).Append("</UserGroupNum>");
			sb.Append("<PermType>").Append((int)grouppermission.PermType).Append("</PermType>");
			sb.Append("</GroupPermission>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.GroupPermission Deserialize(string xml) {
			OpenDentBusiness.GroupPermission grouppermission=new OpenDentBusiness.GroupPermission();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "GroupPermNum":
							grouppermission.GroupPermNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "NewerDate":
							grouppermission.NewerDate=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "NewerDays":
							grouppermission.NewerDays=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "UserGroupNum":
							grouppermission.UserGroupNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PermType":
							grouppermission.PermType=(OpenDentBusiness.Permissions)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return grouppermission;
		}


	}
}