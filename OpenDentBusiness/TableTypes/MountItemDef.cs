using System;
using System.Collections;

namespace OpenDentBusiness{
	/// <summary>THIS TABLE IS NOT BEING USED.  These are always attached to mountdefs.  Can be deleted without any problems.</summary>
	[Serializable()]
	public class MountItemDef : TableBase {
		/// <summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long MountItemDefNum;
		/// <summary>FK to mountdef.MountDefNum.</summary>
		public long MountDefNum;
		/// <summary>The x position, in pixels, of the item on the mount.</summary>
		public int Xpos;
		/// <summary>The y position, in pixels, of the item on the mount.</summary>
		public int Ypos;
		/// <summary>Ignored if mount IsRadiograph.  For other mounts, the image will be scaled to fit within this space.  Any cropping, rotating, etc, will all be defined in the original image itself.</summary>
		public int Width;
		/// <summary>Ignored if mount IsRadiograph.  For other mounts, the image will be scaled to fit within this space.  Any cropping, rotating, etc, will all be defined in the original image itself.</summary>
		public int Height;

		///<summary></summary>
		public MountItemDef Copy() {
			return (MountItemDef)this.MemberwiseClone();
		}

		
	}

		



		
	

	

	


}










