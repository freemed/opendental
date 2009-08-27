using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary>One field on a sheetDef.</summary>
	[DataObject("sheetfielddef")]
	public class SheetFieldDef : DataObjectBase{
		[DataField("SheetFieldDefNum",PrimaryKey=true,AutoNumber=true)]
		private long sheetFieldDefNum;
		private bool sheetFieldDefNumChanged;
		///<summary>Primary key.</summary>
		public long SheetFieldDefNum{
			get{return sheetFieldDefNum;}
			set{if(sheetFieldDefNum!=value){sheetFieldDefNum=value;MarkDirty();sheetFieldDefNumChanged=true;}}
		}
		public bool SheetFieldDefNumChanged{
			get{return sheetFieldDefNumChanged;}
		}

		[DataField("SheetDefNum")]
		private long sheetDefNum;
		private bool sheetDefNumChanged;
		///<summary>FK to sheetdef.SheetDefNum.</summary>
		public long SheetDefNum{
			get{return sheetDefNum;}
			set{if(sheetDefNum!=value){sheetDefNum=value;MarkDirty();sheetDefNumChanged=true;}}
		}
		public bool SheetDefNumChanged{
			get{return sheetDefNumChanged;}
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
		///<summary>Mostly for OutputText, InputField, and CheckBox types.  Each sheet typically has a main datatable type.  For OutputText types, FieldName is usually the string representation of the database column for the main table.  For other tables, it can be of the form table.Column.  There may also be extra fields available that are not strictly pulled from the database.  Extra fields will start with lowercase to indicate that they are not pure database fields.  The list of available fields for each type in SheetFieldsAvailable.  Users can pick from that list.  Likewise, InputField types are internally tied to actions to persist the data.  So they are also hard coded and are available in SheetFieldsAvailable.  For static images, this is the full file name including extension, but without path.  Static images paths are reconstructed by looking in the AtoZ folder, SheetImages folder.  For Pat Images, this is an long FK/DefNum to the default folder for the image.  The filename of a PatImage will later be stored in FieldValue.</summary>
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
		///<summary>For StaticText, this text can include bracketed fields, like [nameLF].  For OutputText and InputField, this will be blank.</summary>
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
		private long xPos;
		private bool xPosChanged;
		///<summary>In pixels.</summary>
		public long XPos{
			get{return xPos;}
			set{if(xPos!=value){xPos=value;MarkDirty();xPosChanged=true;}}
		}
		public bool XPosChanged{
			get{return xPosChanged;}
		}

		[DataField("YPos")]
		private long yPos;
		private bool yPosChanged;
		///<summary>In pixels.</summary>
		public long YPos{
			get{return yPos;}
			set{if(yPos!=value){yPos=value;MarkDirty();yPosChanged=true;}}
		}
		public bool YPosChanged{
			get{return yPosChanged;}
		}

		[DataField("Width")]
		private long width;
		private bool widthChanged;
		///<summary>The field will be constrained horizontally to this size.  Not allowed to be zero.</summary>
		public long Width{
			get{return width;}
			set{if(width!=value){width=value;MarkDirty();widthChanged=true;}}
		}
		public bool WidthChanged{
			get{return widthChanged;}
		}

		[DataField("Height")]
		private long height;
		private bool heightChanged;
		///<summary>The field will be constrained vertically to this size.  Not allowed to be 0.  It's not allowed to be zero so that it will be visible on the designer.</summary>
		public long Height{
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
			long xPos,long yPos,long width,long height,
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

		public SheetFieldDef Copy(){
			return (SheetFieldDef)this.MemberwiseClone();
		}

		public override string ToString() {
			return fieldName+" "+fieldValue;
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
			long xPos,long yPos,long width,long height)
		{
			return new SheetFieldDef(SheetFieldType.OutputText,fieldName,"",fontSize,fontName,fontIsBold,
				xPos,yPos,width,height,GrowthBehaviorEnum.None);
		}

		public static SheetFieldDef NewOutput(string fieldName,float fontSize,string fontName,bool fontIsBold,
			long xPos,long yPos,long width,long height,GrowthBehaviorEnum growthBehavior)
		{
			return new SheetFieldDef(SheetFieldType.OutputText,fieldName,"",fontSize,fontName,fontIsBold,
				xPos,yPos,width,height,growthBehavior);
		}

		public static SheetFieldDef NewStaticText(string fieldValue,float fontSize,string fontName,bool fontIsBold,
			long xPos,long yPos,long width,long height)
		{
			return new SheetFieldDef(SheetFieldType.StaticText,"",fieldValue,fontSize,fontName,fontIsBold,
				xPos,yPos,width,height,GrowthBehaviorEnum.None);
		}

		public static SheetFieldDef NewStaticText(string fieldValue,float fontSize,string fontName,bool fontIsBold,
			long xPos,long yPos,long width,long height,GrowthBehaviorEnum growthBehavior)
		{
			return new SheetFieldDef(SheetFieldType.StaticText,"",fieldValue,fontSize,fontName,fontIsBold,
				xPos,yPos,width,height,growthBehavior);
		}

		public static SheetFieldDef NewInput(string fieldName,float fontSize,string fontName,bool fontIsBold,
			long xPos,long yPos,long width,long height)
		{
			return new SheetFieldDef(SheetFieldType.InputField,fieldName,"",fontSize,fontName,fontIsBold,
				xPos,yPos,width,height,GrowthBehaviorEnum.None);
		}

		public static SheetFieldDef NewImage(string fileName,long xPos,long yPos,long width,long height){
			return new SheetFieldDef(SheetFieldType.Image,fileName,"",0,"",false,
				xPos,yPos,width,height,GrowthBehaviorEnum.None);
		}

		public static SheetFieldDef NewLine(long xPos,long yPos,long width,long height){
			return new SheetFieldDef(SheetFieldType.Line,"","",0,"",false,
				xPos,yPos,width,height,GrowthBehaviorEnum.None);
		}

		public static SheetFieldDef NewRect(long xPos,long yPos,long width,long height){
			return new SheetFieldDef(SheetFieldType.Rectangle,"","",0,"",false,
				xPos,yPos,width,height,GrowthBehaviorEnum.None);
		}

		public static SheetFieldDef NewCheckBox(string fieldName,long xPos,long yPos,long width,long height){
			return new SheetFieldDef(SheetFieldType.CheckBox,fieldName,"",0,"",false,
				xPos,yPos,width,height,GrowthBehaviorEnum.None);
		}

		public static SheetFieldDef NewSigBox(long xPos,long yPos,long width,long height){
			return new SheetFieldDef(SheetFieldType.SigBox,"","",0,"",false,
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
