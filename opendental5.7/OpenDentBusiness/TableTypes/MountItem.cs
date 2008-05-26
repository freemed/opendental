using System;
using System.Collections.Generic;
using System.Text;
using OpenDental.DataAccess;

namespace OpenDentBusiness {
	/// <summary>These are always attached to a mount and are constant. Should not be deleted, but rather updated if geometry changes.</summary>
	[DataObject("mountitem")]
	public class MountItem : DataObjectBase {
		///<summary>Primary key.</summary>
		[DataField("MountItemNum", PrimaryKey=true, AutoNumber=true)]
		private int mountItemNum;

		/// <summary>Primary key.</summary>
		public int MountItemNum {
			get { return mountItemNum; }
			set { mountItemNum = value; MarkDirty(); mountItemNumChanged = true; }
		}

		bool mountItemNumChanged;

		public bool MountItemNumChanged {
			get { return mountItemNumChanged; }
			
		}
		///<summary>FK to mount.MountNum.</summary>
		[DataField("MountNum")]
		private int mountNum;

		/// <summary>FK to mount.MountNum.</summary>
		public int MountNum {
			get { return mountNum; }
			set { mountNum = value; MarkDirty(); mountNumChanged = true; }
		}

		bool mountNumChanged;

		public bool MountNumChanged {
			get { return mountNumChanged; }
			
		}
		///<summary>The x position, in pixels, of the item on the mount.</summary>
		[DataField("Xpos")]
		private int xpos;

		/// <summary>The x position, in pixels, of the item on the mount.</summary>
		public int Xpos {
			get { return xpos; }
			set { xpos = value; MarkDirty(); xposChanged = true; }
		}

		bool xposChanged;

		public bool XposChanged {
			get { return xposChanged; }
			
		}
		///<summary>The y position, in pixels, of the item on the mount.</summary>
		[DataField("Ypos")]
		private int ypos;

		/// <summary>The y position, in pixels, of the item on the mount.</summary>
		public int Ypos {
			get { return ypos; }
			set { ypos = value; MarkDirty(); yposChanged = true; }
		}

		bool yposChanged;

		public bool YposChanged {
			get { return yposChanged; }
			
		}
		///<summary>The ordinal position of the item on the mount.</summary>
		[DataField("OrdinalPos")]
		private int ordinalPos;

		/// <summary>The ordinal position of the item on the mount.</summary>
		public int OrdinalPos {
			get { return ordinalPos; }
			set { ordinalPos = value; MarkDirty(); ordinalPosChanged = true; }
		}

		bool ordinalPosChanged;

		public bool OrdinalPosChanged {
			get { return ordinalPosChanged; }
			
		}
		///<summary>The scaled or unscaled width of the mount item in pixels.</summary>
		[DataField("Width")]
		private int width;

		/// <summary>The scaled or unscaled width of the mount item in pixels.</summary>
		public int Width {
			get { return width; }
			set { width = value; MarkDirty(); widthChanged = true; }
		}

		bool widthChanged;

		public bool WidthChanged {
			get { return widthChanged; }
			
		}
		///<summary>The scaled or unscaled height of the mount item in pixels.</summary>
		[DataField("Height")]
		private int height;

		/// <summary>The scaled or unscaled height of the mount item in pixels.</summary>
		public int Height {
			get { return height; }
			set { height = value; MarkDirty(); heightChanged = true; }
		}

		bool heightChanged;

		public bool HeightChanged {
			get { return heightChanged; }
			
		}

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
