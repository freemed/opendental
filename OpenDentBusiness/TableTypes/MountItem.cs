using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	/// <summary>These are always attached to a mount and are constant. Should not be deleted, but rather updated if geometry changes.  Documents are then attached to MountItems using Document.MountItemNum field.</summary>
	[Serializable()]
	public class MountItem : TableBase {
		/// <summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long MountItemNum ;
		/// <summary>FK to mount.MountNum.</summary>
		public long MountNum;
		/// <summary>The x position, in pixels, of the item on the mount.</summary>
		public int Xpos;
		/// <summary>The y position, in pixels, of the item on the mount.</summary>
		public int Ypos;
		/// <summary>The ordinal position of the item on the mount.</summary>
		public int OrdinalPos;
		/// <summary>The scaled or unscaled width of the mount item in pixels.</summary>
		public int Width;
		/// <summary>The scaled or unscaled height of the mount item in pixels.</summary>
		public int Height;

		///<summary></summary>
		public MountItem Copy() {
			return (MountItem)this.MemberwiseClone();
		}

	}
}
