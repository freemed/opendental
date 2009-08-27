using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{

	///<summary></summary>
	[DataObject("popup")]
	public class Popup : DataObjectBase {
		[DataField("PopupNum", PrimaryKey=true, AutoNumber=true)]
		private long popupNum;
		bool popupNumChanged;
		/// <summary>Primary key.</summary>
		public long PopupNum {
			get { return popupNum; }
			set { popupNum = value; MarkDirty(); popupNumChanged = true; }
		}
		public bool PopupNumChanged {
			get { return popupNumChanged; }
		}

		[DataField("PatNum")]
		private long patNum;
		bool patNumChanged;
		/// <summary>FK to patient.PatNum.</summary>
		public long PatNum {
			get { return patNum; }
			set { patNum = value; MarkDirty(); patNumChanged = true; }
		}
		public bool PatNumChanged {
			get { return patNumChanged; }
		}

		[DataField("Description")]
		private string description;
		bool descriptionChanged;
		/// <summary>The text of the popup.</summary>
		public string Description {
			get { return description; }
			set { description = value; MarkDirty(); descriptionChanged = true; }
		}
		public bool DescriptionChanged {
			get { return descriptionChanged; }
		}

		[DataField("IsDisabled")]
		private bool isDisabled;
		bool isDisabledChanged;
		/// <summary>If true, then the popup won't ever automatically show.</summary>
		public bool IsDisabled {
			get { return isDisabled; }
			set { isDisabled = value; MarkDirty(); isDisabledChanged = true; }
		}
		public bool IsDisabledChanged {
			get { return isDisabledChanged; }
		}

			
	}

	

}









