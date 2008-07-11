using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary>One sheet for one patient.  Loosly corresponds to the Sheet class, but reorganized to be stored in the database.</summary>
	[DataObject("sheetdata")]
	public class SheetData : DataObjectBase{
		[DataField("SheetDataNum",PrimaryKey=true,AutoNumber=true)]
		private int sheetDataNum;
		private bool sheetDataNumChanged;
		///<summary>Primary key.</summary>
		public int SheetDataNum{
			get{return sheetDataNum;}
			set{if(sheetDataNum!=value){sheetDataNum=value;MarkDirty();sheetDataNumChanged=true;}}
		}
		public bool SheetDataNumChanged{
			get{return sheetDataNumChanged;}
		}

		[DataField("SheetType")]
		private int sheetType;
		private bool sheetTypeChanged;
		///<summary>Enum:SheetTypeEnum</summary>
		public int SheetType{
			get{return sheetType;}
			set{if(sheetType!=value){sheetType=value;MarkDirty();sheetTypeChanged=true;}}
		}
		public bool SheetTypeChanged{
			get{return sheetTypeChanged;}
		}

		[DataField("PatNum")]
		private int patNum;
		private bool patNumChanged;
		///<summary>FK to patient.PatNum.  A saved sheet is always attached to a patient.  There are a few sheets that are so minor that they don't get saved, such as a Carrier label.</summary>
		public int PatNum{
			get{return patNum;}
			set{if(patNum!=value){patNum=value;MarkDirty();patNumChanged=true;}}
		}
		public bool PatNumChanged{
			get{return patNumChanged;}
		}

		[DataField("DateSheet")]
		private int dateSheet;
		private bool dateSheetChanged;
		///<summary>The date of the sheet as it will be displayed in the commlog.</summary>
		public int DateSheet{
			get{return dateSheet;}
			set{if(dateSheet!=value){dateSheet=value;MarkDirty();dateSheetChanged=true;}}
		}
		public bool DateSheetChanged{
			get{return dateSheetChanged;}
		}
		
		[DataField("FontSize")]
		private float fontSize;
		private bool fontSizeChanged;
		///<summary>The default fontSize for the sheet.  The actual font must still be saved with each sheetField.</summary>
		public float FontSize{
			get{return fontSize;}
			set{if(fontSize!=value){fontSize=value;MarkDirty();fontSizeChanged=true;}}
		}
		public bool FontSizeChanged{
			get{return fontSizeChanged;}
		}

		[DataField("FontName")]
		private string fontName;
		private bool fontNameChanged;
		///<summary>The default fontName for the sheet.  The actual font must still be saved with each sheetField.</summary>
		public string FontName{
			get{return fontName;}
			set{if(fontName!=value){fontName=value;MarkDirty();fontNameChanged=true;}}
		}
		public bool FontNameChanged{
			get{return fontNameChanged;}
		}

		[DataField("Width")]
		private int width;
		private bool widthChanged;
		///<summary>Width of the sheet in pixels, 100 pixels per inch.</summary>
		public int Width{
			get{return width;}
			set{if(width!=value){width=value;MarkDirty();widthChanged=true;}}
		}
		public bool WidthChanged{
			get{return widthChanged;}
		}

		[DataField("Height")]
		private int height;
		private bool heightChanged;
		///<summary>Height of the sheet in pixels, 100 pixels per inch.</summary>
		public int Height{
			get{return height;}
			set{if(height!=value){height=value;MarkDirty();heightChanged=true;}}
		}
		public bool HeightChanged{
			get{return heightChanged;}
		}

		[DataField("InternalNote")]
		private string internalNote;
		private bool internalNoteChanged;
		///<summary>An internal note for the use of the office staff regarding the sheet.  Not to be printed on the sheet in any way.</summary>
		public string InternalNote{
			get{return internalNote;}
			set{if(internalNote!=value){internalNote=value;MarkDirty();internalNoteChanged=true;}}
		}
		public bool InternalNoteChanged{
			get{return internalNoteChanged;}
		}
		
		public SheetData Copy(){
			return (SheetData)Clone();
		}	
	}
}


