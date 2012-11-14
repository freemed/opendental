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
			sb.Append("<NewerDate>").Append(grouppermission.NewerDate.ToString()).Append("</NewerDate>");
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
							grouppermission.GroupPermNum=reader.ReadContentAsLong();
							break;
						case "NewerDate":
							grouppermission.NewerDate=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "NewerDays":
							grouppermission.NewerDays=reader.ReadContentAsInt();
							break;
						case "UserGroupNum":
							grouppermission.UserGroupNum=reader.ReadContentAsLong();
							break;
						case "PermType":
							grouppermission.PermType=(OpenDentBusiness.Permissions)reader.ReadContentAsInt();
							break;
					}
				}
			}
			return grouppermission;
		}


	}
}