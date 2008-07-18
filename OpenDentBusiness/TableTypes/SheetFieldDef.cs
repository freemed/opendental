using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary>One field on a sheetDef.</summary>
	[DataObject("sheetfielddef")]
	public class SheetFieldDef : DataObjectBase{
		[DataField("SheetFieldDefNum",PrimaryKey=true,AutoNumber=true)]
		private int sheetFieldDefNum;
		private bool sheetFieldDefNumChanged;
		///<summary>Primary key.</summary>
		public int SheetFieldDefNum{
			get{return sheetFieldDefNum;}
			set{if(sheetFieldDefNum!=value){sheetFieldDefNum=value;MarkDirty();sheetFieldDefNumChanged=true;}}
		}
		public bool SheetFieldDefNumChanged{
			get{return sheetFieldDefNumChanged;}
		}

		[DataField("SheetDefNum")]
		private int sheetDefNum;
		private bool sheetDefNumChanged;
		///<summary>FK to sheetdef.SheetDefNum.</summary>
		public int SheetDefNum{
			get{return sheetDefNum;}
			set{if(sheetDefNum!=value){sheetDefNum=value;MarkDirty();sheetDefNumChanged=true;}}
		}
		public bool SheetDefNumChanged{
			get{return sheetDefNumChanged;}
		}

		[DataField("FieldType")]
		private SheetFieldType fieldType;
		private bool fieldTypeChanged;
		///<summary>Enum:SheetFieldType  OutputText, InputField, StaticText,Parameter(only used for SheetFieldData, not SheetField).</summary>
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
		///<summary>Only for OutputText and InputField types.  Each sheet typically has a main datatable type.  For OutputText types, FieldName is usually the string representation of the database column for the main table.  For other tables, it can be of the form table.Column.  There may also be extra fields available that are not strictly pulled from the database.  Extra fields will start with lowercase to indicate that they are not pure database fields.  The list of available fields for each type in SheetFieldsAvailable.  Users could pick from that list.  Likewise, InputField types are internally tied to actions to persist the data.  So they are also hard coded and are available in SheetFieldsAvailable.</summary>
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
		///<summary>For OutputText, this value is set before printing.  This is the data obtained from the database and ready to print.  For StaticText, this is set when designing the sheetDef.  For an archived sheet retrieved from the database (all SheetField rows), this value will have been saved and will not be filled again.</summary>
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

		public SheetFieldDef(){
			//required for use as a generic.
		}
	
		public SheetFieldDef(SheetFieldType fieldType,string fieldName,string fieldValue,
			float fontSize,string fontName,bool fontIsBold,
			int xPos,int yPos,int width,int height,
			GrowthBehaviorEnum growthBehavior) 
		{
			FieldType=fieldType;
			FieldName=fieldName;
			FieldValue=fieldValue;
			FontSize=fontSize;
			FontName=fontName;
			FontIsBold=fontIsBold;
			XPos=xPos;
			YPos=yPos;
			Width=width;
			Height=height;
			GrowthBehavior=growthBehavior;
		}

		///<Summary></Summary>
		public Font GetFont(){
			FontStyle style=FontStyle.Regular;
			if(FontIsBold){
				style=FontStyle.Bold;
			}
			return new Font(FontName,FontSize,style);
		}

		public static SheetFieldDef NewOutput(string fieldName,float fontSize,string fontName,bool fontIsBold,
			int xPos,int yPos,int width,int height)
		{
			//int _height=font.Height+1;//Height is automatic in this early implementation.
			return new SheetFieldDef(SheetFieldType.OutputText,fieldName,"",fontSize,fontName,fontIsBold,
				xPos,yPos,width,height,GrowthBehaviorEnum.None);
		}

		public static SheetFieldDef NewOutput(string fieldName,float fontSize,string fontName,bool fontIsBold,
			int xPos,int yPos,int width,int height,GrowthBehaviorEnum growthBehavior)
		{
			//int _height=font.Height+1;//Height is automatic in this early implementation.
			return new SheetFieldDef(SheetFieldType.OutputText,fieldName,"",fontSize,fontName,fontIsBold,
				xPos,yPos,width,height,growthBehavior);
		}

		public static SheetFieldDef NewStaticText(string fieldValue,float fontSize,string fontName,bool fontIsBold,
			int xPos,int yPos,int width,int height)
		{
			//int _height=font.Height+1;//Height is automatic in this early implementation.
			return new SheetFieldDef(SheetFieldType.StaticText,"",fieldValue,fontSize,fontName,fontIsBold,
				xPos,yPos,width,height,GrowthBehaviorEnum.None);
		}

		public static SheetFieldDef NewInput(string fieldName,float fontSize,string fontName,bool fontIsBold,
			int xPos,int yPos,int width,int height)
		{
			return new SheetFieldDef(SheetFieldType.InputField,fieldName,"",fontSize,fontName,fontIsBold,
				xPos,yPos,width,height,GrowthBehaviorEnum.None);
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
