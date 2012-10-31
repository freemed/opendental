using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ReplicationServer {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ReplicationServer replicationserver) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ReplicationServer>");
			sb.Append("<ReplicationServerNum>").Append(replicationserver.ReplicationServerNum).Append("</ReplicationServerNum>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(replicationserver.Descript)).Append("</Descript>");
			sb.Append("<ServerId>").Append(replicationserver.ServerId).Append("</ServerId>");
			sb.Append("<RangeStart>").Append(replicationserver.RangeStart).Append("</RangeStart>");
			sb.Append("<RangeEnd>").Append(replicationserver.RangeEnd).Append("</RangeEnd>");
			sb.Append("<AtoZpath>").Append(SerializeStringEscapes.EscapeForXml(replicationserver.AtoZpath)).Append("</AtoZpath>");
			sb.Append("<UpdateBlocked>").Append((replicationserver.UpdateBlocked)?1:0).Append("</UpdateBlocked>");
			sb.Append("<SlaveMonitor>").Append(SerializeStringEscapes.EscapeForXml(replicationserver.SlaveMonitor)).Append("</SlaveMonitor>");
			sb.Append("</ReplicationServer>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ReplicationServer Deserialize(string xml) {
			OpenDentBusiness.ReplicationServer replicationserver=new OpenDentBusiness.ReplicationServer();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ReplicationServerNum":
							replicationserver.ReplicationServerNum=reader.ReadContentAsLong();
							break;
						case "Descript":
							replicationserver.Descript=reader.ReadContentAsString();
							break;
						case "ServerId":
							replicationserver.ServerId=reader.ReadContentAsInt();
							break;
						case "RangeStart":
							replicationserver.RangeStart=reader.ReadContentAsLong();
							break;
						case "RangeEnd":
							replicationserver.RangeEnd=reader.ReadContentAsLong();
							break;
						case "AtoZpath":
							replicationserver.AtoZpath=reader.ReadContentAsString();
							break;
						case "UpdateBlocked":
							replicationserver.UpdateBlocked=reader.ReadContentAsString()!="0";
							break;
						case "SlaveMonitor":
							replicationserver.SlaveMonitor=reader.ReadContentAsString();
							break;
					}
				}
			}
			return replicationserver;
		}


	}
}