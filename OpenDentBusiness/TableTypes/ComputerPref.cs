using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {

	///<summary>Enables preference specification for individual computers on a customer network.</summary>
	public class ComputerPref {
		///<summary>Primary key.</summary>
		public long ComputerPrefNum;
		///<summary>The human-readable name of the computer on the network (not the IP address).</summary>
		public string ComputerName;
		///<summary>Set to true if the tooth chart is to use a hardware accelerated OpenGL window when available. Set to false to use software rendering when available. Of course, the final pixel format on the customer machine depends on the list of available formats. Best match pixel format is always used. This option only applies if GraphicsSimple is set to false.</summary>
		public bool GraphicsUseHardware;
		///<summary>Enum:DrawingMode Set to 1 to use the low-quality 2D tooth chart in the chart module. Set to 0 to use a 3D DirectX based tooth chart in the chart module. This option helps the program run even when the local graphics hardware is buggy or unavailable.</summary>
		public DrawingMode GraphicsSimple;
		///<summary>Indicates the type of Suni sensor connected to the local computer (if any). This can be a value of A, B, C, or D.</summary>
		public string SensorType;
		///<summary>Indicates wether or not the Suni sensor uses binned operation.</summary>
		public bool SensorBinned;
		///<summary>Indicates which Suni box port to connect with. There are 2 ports on a box (ports 0 and 1).</summary>
		public int SensorPort;
		///<summary>Indicates the exposure level to use when capturing from a Suni sensor. Values can be 1 through 7.</summary>
		public int SensorExposure;
		///<summary>Indicates if the user prefers double-buffered 3D tooth-chart (where applicable).</summary>
		public bool GraphicsDoubleBuffering;
		///<summary>Indicates the current pixel format by number which the user prefers.</summary>
		public int PreferredPixelFormatNum;
		///<summary>The path of the A-Z folder for the specified computer.  Overrides the officewide default.  Used when multiple locations are on a single virtual database and they each want to look to the local data folder for images.</summary>
		public string AtoZpath;
		///<summary>If the global setting for showing the Task List is on, this controls if it should be hidden on this specified computer</summary>
		public bool TaskKeepListHidden;
		///<summary>Dock task bar on bottom (0) or right (1).</summary>
		public int TaskDock;
		///<summary>X pos for right docked task list.</summary>
		public int TaskX;
		///<summary>Y pos for bottom docked task list.</summary>
		public int TaskY;
		///<summary>Holds a semi-colon separated list of enumeration names and values representing a DirectX format. If blank, then
		///no format is currently set and the best theoretical foramt will be chosen at program startup. If this value is set to
		///'opengl' then this computer is using OpenGL and a DirectX format will not be picked.</summary>
		public string DirectXFormat;


		public ComputerPref Copy(){
			return (ComputerPref)MemberwiseClone();
		}
	}

	public enum DrawingMode{
		///<summary>0</summary>
		DirectX,
		///<summary>1</summary>
		Simple2D,
		///<summary>2</summary>
		OpenGL
	}

}
