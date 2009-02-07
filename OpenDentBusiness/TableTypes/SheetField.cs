using System;
using System.Collections;
using System.Drawing;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary>One field on a sheet.</summary>
	[DataObject("sheetfield")]
	public class SheetField : DataObjectBase{
		[DataField("SheetFieldNum",PrimaryKey=true,AutoNumber=true)]
		private int sheetFieldNum;
		private bool sheetFieldNumChanged;
		///<summary>Primary key.</summary>
		public int SheetFieldNum{
			get{return sheetFieldNum;}
			set{if(sheetFieldNum!=value){sheetFieldNum=value;MarkDirty();sheetFieldNumChanged=true;}}
		}
		public bool SheetFieldNumChanged{
			get{return sheetFieldNumChanged;}
		}

		[DataField("SheetNum")]
		private int sheetNum;
		private bool sheetNumChanged;
		///<summary>FK to sheet.SheetNum.</summary>
		public int SheetNum{
			get{return sheetNum;}
			set{if(sheetNum!=value){sheetNum=value;MarkDirty();sheetNumChanged=true;}}
		}
		public bool SheetNumChanged{
			get{return sheetNumChanged;}
		}

		[DataField("FieldType")]
		private SheetFieldType fieldType;
		private bool fieldTypeChanged;
		///<summary>Enum:SheetFieldType  OutputText, InputField, StaticText,Parameter(only used for SheetField, not SheetFieldDef),Image,Drawing,Line,Rectangle,CheckBox,SigBox,PatImage.</summary>
		public SheetFieldType FieldType{
			get{return fieldType;}
			set{if(fieldType!=value){fieldType=value;MarkDirty();fieldTypeChanged=true;}}
		}
		public bool FieldTypeChanged{
			get{return fieldTypeChanged;}
		}

		[DataField("FieldName")]
		private string fieldName;
		private bool fieldNameChanged;
		///<summary>Mostly for OutputText and InputField types.  Each sheet typically has a main datatable type.  For OutputText types, FieldName is usually the string representation of the database column for the main table.  For other tables, it can be of the form table.Column.  There may also be extra fields available that are not strictly pulled from the database.  Extra fields will start with lowercase to indicate that they are not pure database fields.  The list of available fields for each type in SheetFieldsAvailable.  Users can pick from that list.  Likewise, InputField types are internally tied to actions to persist the data.  So they are also hard coded and are available in SheetFieldsAvailable.  For static images, this is the full file name including extension, but without path.  Static images paths are reconstructed by looking in the AtoZ folder, SheetImages folder.  For a PatImage, this now contains the int FK to the document.  A join must be done to that table to find the filename.   When a SheetField has a fieldType of Parameter, then the FieldName stores the name of the parameter.</summary>
		public string FieldName{
			get{return fieldName;}
			set{if(fieldName!=value){fieldName=value;MarkDirty();fieldNameChanged=true;}}
		}
		public bool FieldNameChanged{
			get{return fieldNameChanged;}
		}

		[DataField("FieldValue")]
		private string fieldValue;
		private bool fieldValueChanged;
		///<summary>For OutputText, this value is set before printing.  This is the data obtained from the database and ready to print.  For StaticText, this is copied from the sheetFieldDef, but in-line fields like [this] will have been filled.  For an archived sheet retrieved from the database (all SheetField rows), this value will have been saved and will not be filled again automatically.  For a parameter fieldtype, this will store the value of the parameter. For a Drawing fieldtype, this will be the point data for the lines.  The format would look similar to this: 45,68;48,70;49,72;0,0;55,88;etc.  It's simply a sequence of points, separated by semicolons.  For CheckBox, it will either be an X or empty.  For SigBox, the first char will be 0 or 1 to indicate SigIsTopaz, and all subsequent chars will be the Signature itself.   For Pat Image, this contains image size and image position info.  Like this: "X=0,Y=20,W=100,H=60".  This is initially generated automatically to fit the object.  It can later be changed by the user to "zoom and pan" within the confines of the object.</summary>
		public string FieldValue{
			get{return fieldValue;}
			set{if(fieldValue!=value){fieldValue=value;MarkDirty();fieldValueChanged=true;}}
		}
		public bool FieldValueChanged{
			get{return fieldValueChanged;}
		}
		
		[DataField("FontSize")]
		private float fontSize;
		private bool fontSizeChanged;
		///<summary>The fontSize for this field regardless of the default for the sheet.  The actual font must be saved with each sheetField.</summary>
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
		///<summary>The fontName for this field regardless of the default for the sheet.  The actual font must be saved with each sheetField.</summary>
		public string FontName{
			get{return fontName;}
			set{if(fontName!=value){fontName=value;MarkDirty();fontNameChanged=true;}}
		}
		public bool FontNameChanged{
			get{return fontNameChanged;}
		}

		[DataField("FontIsBold")]
		private bool fontIsBold;
		private bool fontIsBoldChanged;
		///<summary></summary>
		public bool FontIsBold{
			get{return fontIsBold;}
			set{if(fontIsBold!=value){fontIsBold=value;MarkDirty();fontIsBoldChanged=true;}}
		}
		public bool FontIsBoldChanged{
			get{return fontIsBoldChanged;}
		}

		[DataField("XPos")]
		private int xPos;
		private bool xPosChanged;
		///<summary>In pixels.</summary>
		public int XPos{
			get{return xPos;}
			set{if(xPos!=value){xPos=value;MarkDirty();xPosChanged=true;}}
		}
		public bool XPosChanged{
			get{return xPosChanged;}
		}

		[DataField("YPos")]
		private int yPos;
		private bool yPosChanged;
		///<summary>In pixels.</summary>
		public int YPos{
			get{return yPos;}
			set{if(yPos!=value){yPos=value;MarkDirty();yPosChanged=true;}}
		}
		public bool YPosChanged{
			get{return yPosChanged;}
		}

		[DataField("Width")]
		private int width;
		private bool widthChanged;
		///<summary>The field will be constrained horizontally to this size.  Not allowed to be zero.</summary>
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
		///<summary>The field will be constrained vertically to this size.  Not allowed to be 0.  It's not allowed to be zero so that it will be visible on the designer.</summary>
		public int Height{
			get{return height;}
			set{if(height!=value){height=value;MarkDirty();heightChanged=true;}}
		}
		public bool HeightChanged{
			get{return heightChanged;}
		}

		[DataField("GrowthBehavior")]
		private GrowthBehaviorEnum growthBehavior;
		private bool growthBehaviorChanged;
		///<summary>Enum:GrowthBehaviorEnum</summary>
		public GrowthBehaviorEnum GrowthBehavior{
			get{return growthBehavior;}
			set{if(growthBehavior!=value){growthBehavior=value;MarkDirty();growthBehaviorChanged=true;}}
		}
		public bool GrowthBehaviorChanged{
			get{return growthBehaviorChanged;}
		}
				
		public SheetField Copy(){
			return (SheetField)Clone();
		}

		public override string ToString() {
			return fieldName+" "+fieldValue;
		}

		///<Summary>Should only be called after FieldValue has been set, due to GrowthBehavior.</Summary>
		public Rectangle Bounds {
			get {
				return new Rectangle(XPos,YPos,Width,Height);
			}
		}

		///<Summary>Should only be called after FieldValue has been set, due to GrowthBehavior.</Summary>
		public RectangleF BoundsF {
			get {
				return new RectangleF(XPos,YPos,Width,Height);
			}
		}

		



	}
}


