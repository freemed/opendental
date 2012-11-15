using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ApptView {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ApptView apptview) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ApptView>");
			sb.Append("<ApptViewNum>").Append(apptview.ApptViewNum).Append("</ApptViewNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(apptview.Description)).Append("</Description>");
			sb.Append("<ItemOrder>").Append(apptview.ItemOrder).Append("</ItemOrder>");
			sb.Append("<RowsPerIncr>").Append(apptview.RowsPerIncr).Append("</RowsPerIncr>");
			sb.Append("<OnlyScheduledProvs>").Append((apptview.OnlyScheduledProvs)?1:0).Append("</OnlyScheduledProvs>");
			sb.Append("<OnlySchedBeforeTime>").Append(apptview.OnlySchedBeforeTime.ToString()).Append("</OnlySchedBeforeTime>");
			sb.Append("<OnlySchedAfterTime>").Append(apptview.OnlySchedAfterTime.ToString()).Append("</OnlySchedAfterTime>");
			sb.Append("<StackBehavUR>").Append((int)apptview.StackBehavUR).Append("</StackBehavUR>");
			sb.Append("<StackBehavLR>").Append((int)apptview.StackBehavLR).Append("</StackBehavLR>");
			sb.Append("<ClinicNum>").Append(apptview.ClinicNum).Append("</ClinicNum>");
			sb.Append("</ApptView>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ApptView Deserialize(string xml) {
			OpenDentBusiness.ApptView apptview=new OpenDentBusiness.ApptView();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ApptViewNum":
							apptview.ApptViewNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Description":
							apptview.Description=reader.ReadContentAsString();
							break;
						case "ItemOrder":
							apptview.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "RowsPerIncr":
							apptview.RowsPerIncr=System.Convert.ToByte(reader.ReadContentAsString());
							break;
						case "OnlyScheduledProvs":
							apptview.OnlyScheduledProvs=reader.ReadContentAsString()!="0";
							break;
						case "OnlySchedBeforeTime":
							apptview.OnlySchedBeforeTime=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "OnlySchedAfterTime":
							apptview.OnlySchedAfterTime=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "StackBehavUR":
							apptview.StackBehavUR=(OpenDentBusiness.ApptViewStackBehavior)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "StackBehavLR":
							apptview.StackBehavLR=(OpenDentBusiness.ApptViewStackBehavior)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ClinicNum":
							apptview.ClinicNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
					}
				}
			}
			return apptview;
		}


	}
}