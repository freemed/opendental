using System;
using System.Collections;
using OpenDental.DataAccess;


namespace OpenDentBusiness{

	/// <summary>THIS TABLE IS NOT BEING USED.  These can be freely deleted, renamed, moved, etc. without affecting any patient info.  mountitemdef</summary>
	[DataObject("mountdef")]
	public class MountDef : DataObjectBase {
		///<summary>Primary key.</summary>
		[DataField("MountDefNum", PrimaryKey=true, AutoNumber=true)]
		private int mountDefNum;

		/// <summary>Primary key.</summary>
		public int MountDefNum {
			get { return mountDefNum; }
			set { mountDefNum = value; MarkDirty(); mountDefNumChanged = true; }
		}

		bool mountDefNumChanged;

		public bool MountDefNumChanged {
			get { return mountDefNumChanged; }
			
		}
		///<summary>.</summary>
		[DataField("Description")]
		private string description;

		/// <summary>.</summary>
		public string Description {
			get { return description; }
			set { description = value; MarkDirty(); descriptionChanged = true; }
		}

		bool descriptionChanged;

		public bool DescriptionChanged {
			get { return descriptionChanged; }
			
		}
		///<summary>The order that the mount defs will show in various lists.</summary>
		[DataField("ItemOrder")]
		private int itemOrder;

		/// <summary>The order that the mount defs will show in various lists.</summary>
		public int ItemOrder {
			get { return itemOrder; }
			set { itemOrder = value; MarkDirty(); itemOrderChanged = true; }
		}

		bool itemOrderChanged;

		public bool ItemOrderChanged {
			get { return itemOrderChanged; }
			
		}
		///<summary>Set to true if this is just xrays.  If true, this prevents image from being scaled to fit inside the mount.  If false (composite photographs for example) then the images will be scaled to fit inside the mount. Later, the basic appearance or background color might be set based on this flag as well.</summary>
		[DataField("IsRadiograph")]
		private bool isRadiograph;

		/// <summary>Set to true if this is just xrays.  If true, this prevents image from being scaled to fit inside the mount.  If false (composite photographs for example) then the images will be scaled to fit inside the mount. Later, the basic appearance or background color might be set based on this flag as well.</summary>
		public bool IsRadiograph {
			get { return isRadiograph; }
			set { isRadiograph = value; MarkDirty(); isRadiographChanged = true; }
		}

		bool isRadiographChanged;

		public bool IsRadiographChanged {
			get { return isRadiographChanged; }
			
		}
		///<summary>The width of the mount, in pixels.  For radiograph mounts, this could be very large.  It must be large enough for the radiographs to fit in the mount without scaling.  For photos, it should also be large so that the scaling won't be too noticeable.  Shrinking to view or print will always produce nicer results than enlarging to view or print.</summary>
		[DataField("Width")]
		private int width;

		/// <summary>The width of the mount, in pixels.  For radiograph mounts, this could be very large.  It must be large enough for the radiographs to fit in the mount without scaling.  For photos, it should also be large so that the scaling won't be too noticeable.  Shrinking to view or print will always produce nicer results than enlarging to view or print.</summary>
		public int Width {
			get { return width; }
			set { width = value; MarkDirty(); widthChanged = true; }
		}

		bool widthChanged;

		public bool WidthChanged {
			get { return widthChanged; }
			
		}
		///<summary>Height of the mount in pixels.</summary>
		[DataField("Height")]
		private int height;

		/// <summary>Height of the mount in pixels.</summary>
		public int Height {
			get { return height; }
			set { height = value; MarkDirty(); heightChanged = true; }
		}

		bool heightChanged;

		public bool HeightChanged {
			get { return heightChanged; }
			
		}

		///<summary></summary>
		public MountDef Copy() {
			MountDef m=new MountDef();
			m.MountDefNum=MountDefNum;
			m.Description=Description;
			m.ItemOrder=ItemOrder;
			m.IsRadiograph=IsRadiograph;
			m.Width=Width;
			m.Height=Height;
			return m;
		}

		
	}

		



		
	

	

	


}










