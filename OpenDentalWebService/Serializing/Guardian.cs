using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Guardian {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Guardian guardian) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Guardian>");
			sb.Append("<GuardianNum>").Append(guardian.GuardianNum).Append("</GuardianNum>");
			sb.Append("<PatNumChild>").Append(guardian.PatNumChild).Append("</PatNumChild>");
			sb.Append("<PatNumGuardian>").Append(guardian.PatNumGuardian).Append("</PatNumGuardian>");
			sb.Append("<Relationship>").Append((int)guardian.Relationship).Append("</Relationship>");
			sb.Append("</Guardian>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Guardian Deserialize(string xml) {
			OpenDentBusiness.Guardian guardian=new OpenDentBusiness.Guardian();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "GuardianNum":
							guardian.GuardianNum=reader.ReadContentAsLong();
							break;
						case "PatNumChild":
							guardian.PatNumChild=reader.ReadContentAsLong();
							break;
						case "PatNumGuardian":
							guardian.PatNumGuardian=reader.ReadContentAsLong();
							break;
						case "Relationship":
							guardian.Relationship=(OpenDentBusiness.GuardianRelationship)reader.ReadContentAsInt();
							break;
					}
				}
			}
			return guardian;
		}


	}
}