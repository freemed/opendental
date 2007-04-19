using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	/// <summary>These are always attached to mountdefs.  Can be deleted without any problems.</summary>
	public class MountItem{
		///<summary>Primary key.</summary>
		public int MountItemNum;
		///<summary>FK to mount.MountNum.</summary>
		public int MountNum;
		///<summary>The x position, in pixels, of the item on the mount.</summary>
		public int Xpos;
		///<summary>The y position, in pixels, of the item on the mount.</summary>
		public int Ypos;

		///<summary></summary>
		public MountItem Copy() {
			MountItem m=new MountItem();
			m.MountItemNum=MountItemNum;
			m.MountNum=MountNum;
			m.Xpos=Xpos;
			m.Ypos=Ypos;
			return m;
		}

	}
}
