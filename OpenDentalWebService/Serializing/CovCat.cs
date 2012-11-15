using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class CovCat {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.CovCat covcat) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<CovCat>");
			sb.Append("<CovCatNum>").Append(covcat.CovCatNum).Append("</CovCatNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(covcat.Description)).Append("</Description>");
			sb.Append("<DefaultPercent>").Append(covcat.DefaultPercent).Append("</DefaultPercent>");
			sb.Append("<CovOrder>").Append(covcat.CovOrder).Append("</CovOrder>");
			sb.Append("<IsHidden>").Append((covcat.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("<EbenefitCat>").Append((int)covcat.EbenefitCat).Append("</EbenefitCat>");
			sb.Append("</CovCat>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.CovCat Deserialize(string xml) {
			OpenDentBusiness.CovCat covcat=new OpenDentBusiness.CovCat();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "CovCatNum":
							covcat.CovCatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Description":
							covcat.Description=reader.ReadContentAsString();
							break;
						case "DefaultPercent":
							covcat.DefaultPercent=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "CovOrder":
							covcat.CovOrder=System.Convert.ToByte(reader.ReadContentAsString());
							break;
						case "IsHidden":
							covcat.IsHidden=reader.ReadContentAsString()!="0";
							break;
						case "EbenefitCat":
							covcat.EbenefitCat=(OpenDentBusiness.EbenefitCategory)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return covcat;
		}


	}
}