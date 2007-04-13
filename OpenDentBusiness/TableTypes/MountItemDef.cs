using System;
using System.Collections;


namespace OpenDentBusiness{

	/// <summary>These are always attached to mountdefs.  Can be deleted without any problems.</summary>
	public class MountItemDef{
		///<summary>Primary key.</summary>
		public int MountItemDefNum;
		///<summary>FK to mountdef.MountDefNum.</summary>
		public int MountDefNum;
		///<summary>The x position, in pixels, of the item on the mount.</summary>
		public int Xpos;
		///<summary>The y position, in pixels, of the item on the mount.</summary>
		public int Ypos;
		///<summary>Ignored if mount IsRadiograph.  For other mounts, the image will be scaled to fit within this space.  Any cropping, rotating, etc, will all be defined in the original image itself.</summary>
		public int Width;
		///<summary>Ignored if mount IsRadiograph.  For other mounts, the image will be scaled to fit within this space.  Any cropping, rotating, etc, will all be defined in the original image itself.</summary>
		public int Height;

		///<summary></summary>
		public MountItemDef Copy() {
			MountItemDef m=new MountItemDef();
			m.MountItemDefNum=MountItemDefNum;
			m.MountDefNum=MountDefNum;
			m.Xpos=Xpos;
			m.Ypos=Ypos;
			m.Width=Width;
			m.Height=Height;
			return m;
		}

		
	}

		



		
	

	

	


}










