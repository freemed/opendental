using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ProcGroupItem {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ProcGroupItem procgroupitem) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ProcGroupItem>");
			sb.Append("<ProcGroupItemNum>").Append(procgroupitem.ProcGroupItemNum).Append("</ProcGroupItemNum>");
			sb.Append("<ProcNum>").Append(procgroupitem.ProcNum).Append("</ProcNum>");
			sb.Append("<GroupNum>").Append(procgroupitem.GroupNum).Append("</GroupNum>");
			sb.Append("</ProcGroupItem>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ProcGroupItem Deserialize(string xml) {
			OpenDentBusiness.ProcGroupItem procgroupitem=new OpenDentBusiness.ProcGroupItem();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ProcGroupItemNum":
							procgroupitem.ProcGroupItemNum=reader.ReadContentAsLong();
							break;
						case "ProcNum":
							procgroupitem.ProcNum=reader.ReadContentAsLong();
							break;
						case "GroupNum":
							procgroupitem.GroupNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return procgroupitem;
		}


	}
}