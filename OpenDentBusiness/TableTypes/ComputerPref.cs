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
		///<summary>Set to true to use the low-quality 2D tooth chart in the chart module. Set to false to use an 3D OpenGL based tooth chart in the chart module. This option is a work-around for machines where the OpenGL implementation on the local graphics hardware is buggy or unavailable (i.e. MONO).</summary>
		public bool GraphicsSimple;
		///<summary>Indicates the type of Suni sensor connected to the local computer (if any). This can be a value of A, B, C, or D.</summary>
		public string SensorType;
		///<summary>Indicates wether or not the Suni sensor uses binned operation.</summary>
		public bool SensorBinned;
		///<summary>Indicates which Suni box port to connect with. There are 2 ports on a box (ports 0 and 1).</summary>
		public int SensorPort;
		///<summary>Indicates the exposure level to use when capturing from a Suni sensor. Values can be 1 through 7.</summary>
		public long SensorExposure;
		///<summary>Indicates if the user prefers double-buffered 3D tooth-chart (where applicable).</summary>
		public bool GraphicsDoubleBuffering;
		///<summary>Indicates the current pixel format by number which the user prefers.</summary>
		public long PreferredPixelFormatNum;
		///<summary>The path of the A-Z folder for the specified computer.  Overrides the officewide default.  Used when multiple locations are on a single virtual database and they each want to look to the local data folder for images.</summary>
		public string AtoZpath;
		///<summary>If the global setting for showing the Task List is on, this controls if it should be hidden on this specified computer</summary>
		public bool TaskKeepListHidden;
		///<summary>Dock task bar on bottom (0) or right (1).</summary>
		public long TaskDock;
		///<summary>X pos for right docked task list.</summary>
		public long TaskX;
		///<summary>Y pos for bottom docked task list.</summary>
		public long TaskY;

		public ComputerPref Copy(){
			return (ComputerPref)MemberwiseClone();
		}
	}

}
