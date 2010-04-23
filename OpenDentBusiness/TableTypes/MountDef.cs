using System;
using System.Collections;

namespace OpenDentBusiness{
	/// <summary>THIS TABLE IS NOT BEING USED.  These can be freely deleted, renamed, moved, etc. without affecting any patient info.  mountitemdef</summary>
	[Serializable()]
	public class MountDef : TableBase {
		/// <summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long MountDefNum;
		/// <summary>.</summary>
		public string Description;
		/// <summary>The order that the mount defs will show in various lists.</summary>
		public int ItemOrder;
		/// <summary>Set to true if this is just xrays.  If true, this prevents image from being scaled to fit inside the mount.  If false (composite photographs for example) then the images will be scaled to fit inside the mount. Later, the basic appearance or background color might be set based on this flag as well.</summary>
		public bool IsRadiograph;
		/// <summary>The width of the mount, in pixels.  For radiograph mounts, this could be very large.  It must be large enough for the radiographs to fit in the mount without scaling.  For photos, it should also be large so that the scaling won't be too noticeable.  Shrinking to view or print will always produce nicer results than enlarging to view or print.</summary>
		public int Width;
		/// <summary>Height of the mount in pixels.</summary>
		public int Height;

		///<summary></summary>
		public MountDef Copy() {
			return (MountDef)this.MemberwiseClone();
		}

		
	}

		



		
	

	

	


}










