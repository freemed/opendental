using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Each item is attached to a row in the apptview table.  Each item specifies ONE of: OpNum, ProvNum, ElementDesc, or ApptFieldDefNum.  The other three will be 0 or "".</summary>
	[Serializable]
	public class ApptViewItem:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ApptViewItemNum;//
		///<summary>FK to apptview.</summary>
		public long ApptViewNum;
		///<summary>FK to operatory.OperatoryNum.</summary>
		public long OpNum;
		///<summary>FK to provider.ProvNum.</summary>
		public long ProvNum;
		///<summary>Must be one of the hard coded strings picked from the available list.</summary>
		public string ElementDesc;
		///<summary>If this is a row Element, then this is the 0-based order within its area.  For example, UR starts over with 0 ordering.</summary>
		public byte ElementOrder;
		///<summary>If this is an element, then this is the color.</summary>
		[XmlIgnore]
		public Color ElementColor;
		///<summary>Enum:ApptViewAlignment. If this is an element, then this is the alignment of the element within the appointment.</summary>
		public ApptViewAlignment ElementAlignment;
		///<summary>FK to apptfielddef.ApptFieldDefNum.  If this is an element, and the element is an appt field, then this tells us which one.</summary>
		public long ApptFieldDefNum;

		public ApptViewItem(){

		}

		///<summary>this constructor is just used in GetForCurView when no view selected.</summary>
		public ApptViewItem(string elementDesc,byte elementOrder,Color elementColor) {
			ApptViewItemNum=0;
			ApptViewNum=0;
			OpNum=0;
			ProvNum=0;
			ElementDesc=elementDesc;
			ElementOrder=elementOrder;
			ElementColor=elementColor;
			ElementAlignment=ApptViewAlignment.Main;
		}

		///<summary>Used only for serialization purposes</summary>
		[XmlElement("ElementColor",typeof(int))]
		public int ElementColorXml {
			get {
				return ElementColor.ToArgb();
			}
			set {
				ElementColor = Color.FromArgb(value);
			}
		}
	}

	public enum ApptViewAlignment {
		///<summary>0</summary>
		Main,
		///<summary>1</summary>
		UR,
		///<summary>2</summary>
		LR
	}
	
}
