using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	/// <summary>These are always attached to a mount and are constant. Should not be deleted, but rather updated if geometry changes.</summary>
	public class MountItem{
		///<summary>Primary key.</summary>
		public int MountItemNum;
		///<summary>FK to mount.MountNum.</summary>
		public int MountNum;
		///<summary>The x position, in pixels, of the item on the mount.</summary>
		public int Xpos;
		///<summary>The y position, in pixels, of the item on the mount.</summary>
		public int Ypos;
		///<summary>The ordinal position of the item on the mount.</summary>
		public int OrdinalPos;
		///<summary>The scaled or unscaled width of the mount item in pixels.</summary>
		public int Width;
		///<summary>The scaled or unscaled height of the mount item in pixels.</summary>
		public int Height;

		///<summary></summary>
		public MountItem Copy() {
			MountItem m=new MountItem();
			m.MountItemNum=MountItemNum;
			m.MountNum=MountNum;
			m.Xpos=Xpos;
			m.Ypos=Ypos;
			m.OrdinalPos=OrdinalPos;
			m.Width=Width;
			m.Height=Height;
			return m;
		}

	}
}
