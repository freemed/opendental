using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class DeletedObject {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.DeletedObject deletedobject) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<DeletedObject>");
			sb.Append("<DeletedObjectNum>").Append(deletedobject.DeletedObjectNum).Append("</DeletedObjectNum>");
			sb.Append("<ObjectNum>").Append(deletedobject.ObjectNum).Append("</ObjectNum>");
			sb.Append("<ObjectType>").Append((int)deletedobject.ObjectType).Append("</ObjectType>");
			sb.Append("<DateTStamp>").Append(deletedobject.DateTStamp.ToString("yyyyMMddHHmmss")).Append("</DateTStamp>");
			sb.Append("</DeletedObject>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.DeletedObject Deserialize(string xml) {
			OpenDentBusiness.DeletedObject deletedobject=new OpenDentBusiness.DeletedObject();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "DeletedObjectNum":
							deletedobject.DeletedObjectNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ObjectNum":
							deletedobject.ObjectNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ObjectType":
							deletedobject.ObjectType=(OpenDentBusiness.DeletedObjectType)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "DateTStamp":
							deletedobject.DateTStamp=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
					}
				}
			}
			return deletedobject;
		}


	}
}