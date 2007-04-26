using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {

	///<summary>Enables preference specification for individual computers on a customer network.</summary>
	public class ComputerPref {
		///<summary>Primary key.</summary>
		public int ComputerPrefNum;
		///<summary>The human-readable name of the computer on the network (not the IP address).</summary>
		public string ComputerName;
		///<summary>Set to true if the tooth chart is to use a hardware accelerated OpenGL window when available. Set to false to use software rendering when available. Of course, the final pixel format on the customer machine depends on the list of available formats. Best match pixel format is always used. This option only applies if GraphicsSimple is set to false.</summary>
		public bool GraphicsUseHardware;
		///<summary>Set to true to use the low-quality 2D tooth chart in the chart module. Set to false to use an 3D OpenGL based tooth chart in the chart module. This option is a work-around for machines where the OpenGL implementation on the local graphics hardware is buggy or unavailable (i.e. MONO).</summary>
		public bool GraphicsSimple;

		public ComputerPref(){
			//OpenGL tooth chart not supported on Unix systems.
			if(Environment.OSVersion.Platform==PlatformID.Unix){
				GraphicsSimple=true;
			}
		}

		public ComputerPref Copy(){
			ComputerPref cp=new ComputerPref();
			cp.ComputerPrefNum=ComputerPrefNum;
			cp.ComputerName=ComputerName;
			cp.GraphicsUseHardware=GraphicsUseHardware;
			cp.GraphicsSimple=GraphicsSimple;
			return cp;
		}
	}

}
