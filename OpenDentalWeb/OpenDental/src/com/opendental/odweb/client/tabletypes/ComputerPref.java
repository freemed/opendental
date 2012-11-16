package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ComputerPref {
		/** Primary key. */
		public int ComputerPrefNum;
		/** The human-readable name of the computer on the network (not the IP address). */
		public String ComputerName;
		/** Set to true if the tooth chart is to use a hardware accelerated OpenGL window when available. Set to false to use software rendering when available. Of course, the final pixel format on the customer machine depends on the list of available formats. Best match pixel format is always used. This option only applies if GraphicsSimple is set to false. */
		public boolean GraphicsUseHardware;
		/** Enum:DrawingMode Set to 1 to use the low-quality 2D tooth chart in the chart module. Set to 0 to use a 3D DirectX based tooth chart in the chart module. This option helps the program run even when the local graphics hardware is buggy or unavailable. */
		public DrawingMode GraphicsSimple;
		/** Indicates the type of Suni sensor connected to the local computer (if any). This can be a value of A, B, C, or D. */
		public String SensorType;
		/** Indicates wether or not the Suni sensor uses binned operation. */
		public boolean SensorBinned;
		/** Indicates which Suni box port to connect with. There are 2 ports on a box (ports 0 and 1). */
		public int SensorPort;
		/** Indicates the exposure level to use when capturing from a Suni sensor. Values can be 1 through 7. */
		public int SensorExposure;
		/** Indicates if the user prefers double-buffered 3D tooth-chart (where applicable). */
		public boolean GraphicsDoubleBuffering;
		/** Indicates the current pixel format by number which the user prefers. */
		public int PreferredPixelFormatNum;
		/** The path of the A-Z folder for the specified computer.  Overrides the officewide default.  Used when multiple locations are on a single virtual database and they each want to look to the local data folder for images. */
		public String AtoZpath;
		/** If the global setting for showing the Task List is on, this controls if it should be hidden on this specified computer */
		public boolean TaskKeepListHidden;
		/** Dock task bar on bottom (0) or right (1). */
		public int TaskDock;
		/** X pos for right docked task list. */
		public int TaskX;
		/** Y pos for bottom docked task list. */
		public int TaskY;
		/** Holds a semi-colon separated list of enumeration names and values representing a DirectX format. If blank, then
            no format is currently set and the best theoretical foramt will be chosen at program startup. If this value is set to
            'opengl' then this computer is using OpenGL and a DirectX format will not be picked. */
		public String DirectXFormat;
		/** The index of the most recent appt view for this computer.  Uses it when opening. */
		public byte RecentApptView;
		/** Show the select scanner dialog when scanning documents. */
		public boolean ScanDocSelectSource;
		/** Show the scanner options dialog when scanning documents. */
		public boolean ScanDocShowOptions;
		/** Attempt to scan in duplex mode when scanning multipage documents with an ADF. */
		public boolean ScanDocDuplex;
		/** Scan in gray scale when scanning documents. */
		public boolean ScanDocGrayscale;
		/** Scan at the specified resolution when scanning documents. */
		public int ScanDocResolution;
		/** 0-100. Quality of jpeg after compression when scanning documents.  100 indicates full quality.  Opposite of compression. */
		public byte ScanDocQuality;

		/** Deep copy of object. */
		public ComputerPref Copy() {
			ComputerPref computerpref=new ComputerPref();
			computerpref.ComputerPrefNum=this.ComputerPrefNum;
			computerpref.ComputerName=this.ComputerName;
			computerpref.GraphicsUseHardware=this.GraphicsUseHardware;
			computerpref.GraphicsSimple=this.GraphicsSimple;
			computerpref.SensorType=this.SensorType;
			computerpref.SensorBinned=this.SensorBinned;
			computerpref.SensorPort=this.SensorPort;
			computerpref.SensorExposure=this.SensorExposure;
			computerpref.GraphicsDoubleBuffering=this.GraphicsDoubleBuffering;
			computerpref.PreferredPixelFormatNum=this.PreferredPixelFormatNum;
			computerpref.AtoZpath=this.AtoZpath;
			computerpref.TaskKeepListHidden=this.TaskKeepListHidden;
			computerpref.TaskDock=this.TaskDock;
			computerpref.TaskX=this.TaskX;
			computerpref.TaskY=this.TaskY;
			computerpref.DirectXFormat=this.DirectXFormat;
			computerpref.RecentApptView=this.RecentApptView;
			computerpref.ScanDocSelectSource=this.ScanDocSelectSource;
			computerpref.ScanDocShowOptions=this.ScanDocShowOptions;
			computerpref.ScanDocDuplex=this.ScanDocDuplex;
			computerpref.ScanDocGrayscale=this.ScanDocGrayscale;
			computerpref.ScanDocResolution=this.ScanDocResolution;
			computerpref.ScanDocQuality=this.ScanDocQuality;
			return computerpref;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ComputerPref>");
			sb.append("<ComputerPrefNum>").append(ComputerPrefNum).append("</ComputerPrefNum>");
			sb.append("<ComputerName>").append(Serializing.EscapeForXml(ComputerName)).append("</ComputerName>");
			sb.append("<GraphicsUseHardware>").append((GraphicsUseHardware)?1:0).append("</GraphicsUseHardware>");
			sb.append("<GraphicsSimple>").append(GraphicsSimple.ordinal()).append("</GraphicsSimple>");
			sb.append("<SensorType>").append(Serializing.EscapeForXml(SensorType)).append("</SensorType>");
			sb.append("<SensorBinned>").append((SensorBinned)?1:0).append("</SensorBinned>");
			sb.append("<SensorPort>").append(SensorPort).append("</SensorPort>");
			sb.append("<SensorExposure>").append(SensorExposure).append("</SensorExposure>");
			sb.append("<GraphicsDoubleBuffering>").append((GraphicsDoubleBuffering)?1:0).append("</GraphicsDoubleBuffering>");
			sb.append("<PreferredPixelFormatNum>").append(PreferredPixelFormatNum).append("</PreferredPixelFormatNum>");
			sb.append("<AtoZpath>").append(Serializing.EscapeForXml(AtoZpath)).append("</AtoZpath>");
			sb.append("<TaskKeepListHidden>").append((TaskKeepListHidden)?1:0).append("</TaskKeepListHidden>");
			sb.append("<TaskDock>").append(TaskDock).append("</TaskDock>");
			sb.append("<TaskX>").append(TaskX).append("</TaskX>");
			sb.append("<TaskY>").append(TaskY).append("</TaskY>");
			sb.append("<DirectXFormat>").append(Serializing.EscapeForXml(DirectXFormat)).append("</DirectXFormat>");
			sb.append("<RecentApptView>").append(RecentApptView).append("</RecentApptView>");
			sb.append("<ScanDocSelectSource>").append((ScanDocSelectSource)?1:0).append("</ScanDocSelectSource>");
			sb.append("<ScanDocShowOptions>").append((ScanDocShowOptions)?1:0).append("</ScanDocShowOptions>");
			sb.append("<ScanDocDuplex>").append((ScanDocDuplex)?1:0).append("</ScanDocDuplex>");
			sb.append("<ScanDocGrayscale>").append((ScanDocGrayscale)?1:0).append("</ScanDocGrayscale>");
			sb.append("<ScanDocResolution>").append(ScanDocResolution).append("</ScanDocResolution>");
			sb.append("<ScanDocQuality>").append(ScanDocQuality).append("</ScanDocQuality>");
			sb.append("</ComputerPref>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"ComputerPrefNum")!=null) {
					ComputerPrefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ComputerPrefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ComputerName")!=null) {
					ComputerName=Serializing.GetXmlNodeValue(doc,"ComputerName");
				}
				if(Serializing.GetXmlNodeValue(doc,"GraphicsUseHardware")!=null) {
					GraphicsUseHardware=(Serializing.GetXmlNodeValue(doc,"GraphicsUseHardware")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"GraphicsSimple")!=null) {
					GraphicsSimple=DrawingMode.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"GraphicsSimple"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"SensorType")!=null) {
					SensorType=Serializing.GetXmlNodeValue(doc,"SensorType");
				}
				if(Serializing.GetXmlNodeValue(doc,"SensorBinned")!=null) {
					SensorBinned=(Serializing.GetXmlNodeValue(doc,"SensorBinned")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"SensorPort")!=null) {
					SensorPort=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SensorPort"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SensorExposure")!=null) {
					SensorExposure=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SensorExposure"));
				}
				if(Serializing.GetXmlNodeValue(doc,"GraphicsDoubleBuffering")!=null) {
					GraphicsDoubleBuffering=(Serializing.GetXmlNodeValue(doc,"GraphicsDoubleBuffering")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"PreferredPixelFormatNum")!=null) {
					PreferredPixelFormatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PreferredPixelFormatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AtoZpath")!=null) {
					AtoZpath=Serializing.GetXmlNodeValue(doc,"AtoZpath");
				}
				if(Serializing.GetXmlNodeValue(doc,"TaskKeepListHidden")!=null) {
					TaskKeepListHidden=(Serializing.GetXmlNodeValue(doc,"TaskKeepListHidden")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"TaskDock")!=null) {
					TaskDock=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TaskDock"));
				}
				if(Serializing.GetXmlNodeValue(doc,"TaskX")!=null) {
					TaskX=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TaskX"));
				}
				if(Serializing.GetXmlNodeValue(doc,"TaskY")!=null) {
					TaskY=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TaskY"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DirectXFormat")!=null) {
					DirectXFormat=Serializing.GetXmlNodeValue(doc,"DirectXFormat");
				}
				if(Serializing.GetXmlNodeValue(doc,"RecentApptView")!=null) {
					RecentApptView=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"RecentApptView"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ScanDocSelectSource")!=null) {
					ScanDocSelectSource=(Serializing.GetXmlNodeValue(doc,"ScanDocSelectSource")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ScanDocShowOptions")!=null) {
					ScanDocShowOptions=(Serializing.GetXmlNodeValue(doc,"ScanDocShowOptions")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ScanDocDuplex")!=null) {
					ScanDocDuplex=(Serializing.GetXmlNodeValue(doc,"ScanDocDuplex")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ScanDocGrayscale")!=null) {
					ScanDocGrayscale=(Serializing.GetXmlNodeValue(doc,"ScanDocGrayscale")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ScanDocResolution")!=null) {
					ScanDocResolution=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ScanDocResolution"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ScanDocQuality")!=null) {
					ScanDocQuality=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"ScanDocQuality"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum DrawingMode {
			/** 0 */
			DirectX,
			/** 1 */
			Simple2D,
			/** 2 */
			OpenGL
		}


}
