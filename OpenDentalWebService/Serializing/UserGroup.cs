using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class UserGroup {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.UserGroup usergroup) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<UserGroup>");
			sb.Append("<UserGroupNum>").Append(usergroup.UserGroupNum).Append("</UserGroupNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(usergroup.Description)).Append("</Description>");
			sb.Append("</UserGroup>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.UserGroup Deserialize(string xml) {
			OpenDentBusiness.UserGroup usergroup=new OpenDentBusiness.UserGroup();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "UserGroupNum":
							usergroup.UserGroupNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Description":
							usergroup.Description=reader.ReadContentAsString();
							break;
					}
				}
			}
			return usergroup;
		}


	}
}