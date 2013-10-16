using System;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>MapArea  An object which will be placed on a ClinicMapPanel and shown when user is viewing clinic layout. Currently only used at HQ for projector view of phone panel.</summary>
	[Serializable]
	public class MapArea:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long MapAreaNum;
		///<summary>FK to Phone.Extension.  Typically 0.  Only used by HQ and when [enum] is set to cubicle.</summary>
		public int Extension;
		///<summary>X-position in the clinical map layout.  Indicates how many feet the ClinicMapItem should be placed from the left edge.</summary>
		public double XPos;
		///<summary>Y-position in the clinical map layout.  Indicates how many feet the ClinicMapItem should be placed from the top edge.</summary>
		public double YPos;
		///<summary>ClinicMapItem width measured in feet.  Not allowed to be zero.</summary>
		public double Width;
		///<summary>ClinicMapItem height measured in feet.</summary>
		public double Height;
		///<summary>Any text that the user types in.  When [enum] is set to cubicle this text will be shown when Extension is 0.  It allows the cubicle to exist and be referenced without having an extension explicitly linked to it.  Limit 255 char.</summary>
		public string Description;
		///<summary>Enum:MapItemType 0-Cubicle,1-DisplayLabel</summary>
		public MapItemType ItemType;
		
		public MapArea Copy() {
			return (MapArea)this.MemberwiseClone();
		}
	}

	/// <summary>Indicate which type of ClinicalMapItem we are dealing with.</summary>
	public enum MapItemType {
		///<summary>0 - A cubicle object.</summary>
		Room,
		///<summary>1 - A label object.</summary>
		DisplayLabel
	}
}
