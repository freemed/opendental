using System;
using System.Collections;
using OpenDentBusiness.DataAccess;


namespace OpenDentBusiness{

	/// <summary>THIS TABLE IS NOT BEING USED.  These are always attached to mountdefs.  Can be deleted without any problems.</summary>
	[DataObject("mountitemdef")]
	public class MountItemDef : DataObjectBase {
		///<summary>Primary key.</summary>
		[DataField("MountItemDefNum")]
		private long mountItemDefNum;

		/// <summary>Primary key.</summary>
		public long MountItemDefNum {
			get { return mountItemDefNum; }
			set { mountItemDefNum = value; MarkDirty(); mountItemDefNumChanged = true; }
		}

		bool mountItemDefNumChanged;

		public bool MountItemDefNumChanged {
			get { return mountItemDefNumChanged; }
			
		}
		///<summary>FK to mountdef.MountDefNum.</summary>
		[DataField("MountDefNum")]
		private long mountDefNum;

		/// <summary>FK to mountdef.MountDefNum.</summary>
		public long MountDefNum {
			get { return mountDefNum; }
			set { mountDefNum = value; MarkDirty(); mountDefNumChanged = true; }
		}

		bool mountDefNumChanged;

		public bool MountDefNumChanged {
			get { return mountDefNumChanged; }
			
		}
		///<summary>The x position, in pixels, of the item on the mount.</summary>
		[DataField("Xpos")]
		private long xpos;

		/// <summary>The x position, in pixels, of the item on the mount.</summary>
		public long Xpos {
			get { return xpos; }
			set { xpos = value; MarkDirty(); xposChanged = true; }
		}

		bool xposChanged;

		public bool XposChanged {
			get { return xposChanged; }
			
		}
		///<summary>The y position, in pixels, of the item on the mount.</summary>
		[DataField("Ypos")]
		private long ypos;

		/// <summary>The y position, in pixels, of the item on the mount.</summary>
		public long Ypos {
			get { return ypos; }
			set { ypos = value; MarkDirty(); yposChanged = true; }
		}

		bool yposChanged;

		public bool YposChanged {
			get { return yposChanged; }
			
		}
		///<summary>Ignored if mount IsRadiograph.  For other mounts, the image will be scaled to fit within this space.  Any cropping, rotating, etc, will all be defined in the original image itself.</summary>
		[DataField("Width")]
		private long width;

		/// <summary>Ignored if mount IsRadiograph.  For other mounts, the image will be scaled to fit within this space.  Any cropping, rotating, etc, will all be defined in the original image itself.</summary>
		public long Width {
			get { return width; }
			set { width = value; MarkDirty(); widthChanged = true; }
		}

		bool widthChanged;

		public bool WidthChanged {
			get { return widthChanged; }
			
		}
		///<summary>Ignored if mount IsRadiograph.  For other mounts, the image will be scaled to fit within this space.  Any cropping, rotating, etc, will all be defined in the original image itself.</summary>
		[DataField("Height")]
		private long height;

		/// <summary>Ignored if mount IsRadiograph.  For other mounts, the image will be scaled to fit within this space.  Any cropping, rotating, etc, will all be defined in the original image itself.</summary>
		public long Height {
			get { return height; }
			set { height = value; MarkDirty(); heightChanged = true; }
		}

		bool heightChanged;

		public bool HeightChanged {
			get { return heightChanged; }
			
		}

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










