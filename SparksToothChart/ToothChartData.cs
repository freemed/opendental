using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SparksToothChart {
	public class ToothChartData {
		///<summary>A strongly typed collection of ToothGraphics.  This includes all 32 perm and all 20 primary teeth, whether they will be drawn or not.  If a tooth is missing, it gets marked as visible false.  If it's set to primary, then the permanent tooth gets repositioned under the primary, and a primary gets set to visible true.  If a tooth is impacted, it gets repositioned.  Supernumerary graphics are not yet supported, but they might be handled by adding to this list.  "implant" is also stored as another tooth in this collection.  It is just used to store the graphics for any implant.</summary>
		public ToothGraphicCollection ListToothGraphics;
		public Color ColorBackground;

		public ToothChartData() {
			ListToothGraphics=new ToothGraphicCollection();
			ColorBackground=Color.FromArgb(150,145,152);//defaults to gray
		}
		
	}
}
