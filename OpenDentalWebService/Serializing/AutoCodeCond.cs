using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class AutoCodeCond {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.AutoCodeCond autocodecond) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<AutoCodeCond>");
			sb.Append("<AutoCodeCondNum>").Append(autocodecond.AutoCodeCondNum).Append("</AutoCodeCondNum>");
			sb.Append("<AutoCodeItemNum>").Append(autocodecond.AutoCodeItemNum).Append("</AutoCodeItemNum>");
			sb.Append("<Cond>").Append((int)autocodecond.Cond).Append("</Cond>");
			sb.Append("</AutoCodeCond>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.AutoCodeCond Deserialize(string xml) {
			OpenDentBusiness.AutoCodeCond autocodecond=new OpenDentBusiness.AutoCodeCond();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "AutoCodeCondNum":
							autocodecond.AutoCodeCondNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "AutoCodeItemNum":
							autocodecond.AutoCodeItemNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Cond":
							autocodecond.Cond=(OpenDentBusiness.AutoCondition)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return autocodecond;
		}


	}
}