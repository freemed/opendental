using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ComputerPref {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ComputerPref computerpref) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ComputerPref>");
			sb.Append("<ComputerPrefNum>").Append(computerpref.ComputerPrefNum).Append("</ComputerPrefNum>");
			sb.Append("<ComputerName>").Append(SerializeStringEscapes.EscapeForXml(computerpref.ComputerName)).Append("</ComputerName>");
			sb.Append("<GraphicsUseHardware>").Append((computerpref.GraphicsUseHardware)?1:0).Append("</GraphicsUseHardware>");
			sb.Append("<GraphicsSimple>").Append((int)computerpref.GraphicsSimple).Append("</GraphicsSimple>");
			sb.Append("<SensorType>").Append(SerializeStringEscapes.EscapeForXml(computerpref.SensorType)).Append("</SensorType>");
			sb.Append("<SensorBinned>").Append((computerpref.SensorBinned)?1:0).Append("</SensorBinned>");
			sb.Append("<SensorPort>").Append(computerpref.SensorPort).Append("</SensorPort>");
			sb.Append("<SensorExposure>").Append(computerpref.SensorExposure).Append("</SensorExposure>");
			sb.Append("<GraphicsDoubleBuffering>").Append((computerpref.GraphicsDoubleBuffering)?1:0).Append("</GraphicsDoubleBuffering>");
			sb.Append("<PreferredPixelFormatNum>").Append(computerpref.PreferredPixelFormatNum).Append("</PreferredPixelFormatNum>");
			sb.Append("<AtoZpath>").Append(SerializeStringEscapes.EscapeForXml(computerpref.AtoZpath)).Append("</AtoZpath>");
			sb.Append("<TaskKeepListHidden>").Append((computerpref.TaskKeepListHidden)?1:0).Append("</TaskKeepListHidden>");
			sb.Append("<TaskDock>").Append(computerpref.TaskDock).Append("</TaskDock>");
			sb.Append("<TaskX>").Append(computerpref.TaskX).Append("</TaskX>");
			sb.Append("<TaskY>").Append(computerpref.TaskY).Append("</TaskY>");
			sb.Append("<DirectXFormat>").Append(SerializeStringEscapes.EscapeForXml(computerpref.DirectXFormat)).Append("</DirectXFormat>");
			sb.Append("<RecentApptView>").Append(computerpref.RecentApptView).Append("</RecentApptView>");
			sb.Append("<ScanDocSelectSource>").Append((computerpref.ScanDocSelectSource)?1:0).Append("</ScanDocSelectSource>");
			sb.Append("<ScanDocShowOptions>").Append((computerpref.ScanDocShowOptions)?1:0).Append("</ScanDocShowOptions>");
			sb.Append("<ScanDocDuplex>").Append((computerpref.ScanDocDuplex)?1:0).Append("</ScanDocDuplex>");
			sb.Append("<ScanDocGrayscale>").Append((computerpref.ScanDocGrayscale)?1:0).Append("</ScanDocGrayscale>");
			sb.Append("<ScanDocResolution>").Append(computerpref.ScanDocResolution).Append("</ScanDocResolution>");
			sb.Append("<ScanDocQuality>").Append(computerpref.ScanDocQuality).Append("</ScanDocQuality>");
			sb.Append("</ComputerPref>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ComputerPref Deserialize(string xml) {
			OpenDentBusiness.ComputerPref computerpref=new OpenDentBusiness.ComputerPref();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ComputerPrefNum":
							computerpref.ComputerPrefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ComputerName":
							computerpref.ComputerName=reader.ReadContentAsString();
							break;
						case "GraphicsUseHardware":
							computerpref.GraphicsUseHardware=reader.ReadContentAsString()!="0";
							break;
						case "GraphicsSimple":
							computerpref.GraphicsSimple=(OpenDentBusiness.DrawingMode)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "SensorType":
							computerpref.SensorType=reader.ReadContentAsString();
							break;
						case "SensorBinned":
							computerpref.SensorBinned=reader.ReadContentAsString()!="0";
							break;
						case "SensorPort":
							computerpref.SensorPort=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "SensorExposure":
							computerpref.SensorExposure=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "GraphicsDoubleBuffering":
							computerpref.GraphicsDoubleBuffering=reader.ReadContentAsString()!="0";
							break;
						case "PreferredPixelFormatNum":
							computerpref.PreferredPixelFormatNum=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "AtoZpath":
							computerpref.AtoZpath=reader.ReadContentAsString();
							break;
						case "TaskKeepListHidden":
							computerpref.TaskKeepListHidden=reader.ReadContentAsString()!="0";
							break;
						case "TaskDock":
							computerpref.TaskDock=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "TaskX":
							computerpref.TaskX=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "TaskY":
							computerpref.TaskY=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "DirectXFormat":
							computerpref.DirectXFormat=reader.ReadContentAsString();
							break;
						case "RecentApptView":
							computerpref.RecentApptView=System.Convert.ToByte(reader.ReadContentAsString());
							break;
						case "ScanDocSelectSource":
							computerpref.ScanDocSelectSource=reader.ReadContentAsString()!="0";
							break;
						case "ScanDocShowOptions":
							computerpref.ScanDocShowOptions=reader.ReadContentAsString()!="0";
							break;
						case "ScanDocDuplex":
							computerpref.ScanDocDuplex=reader.ReadContentAsString()!="0";
							break;
						case "ScanDocGrayscale":
							computerpref.ScanDocGrayscale=reader.ReadContentAsString()!="0";
							break;
						case "ScanDocResolution":
							computerpref.ScanDocResolution=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ScanDocQuality":
							computerpref.ScanDocQuality=System.Convert.ToByte(reader.ReadContentAsString());
							break;
					}
				}
			}
			return computerpref;
		}


	}
}