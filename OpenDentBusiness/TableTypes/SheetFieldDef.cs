using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness{
	///<summary>One field on a sheetDef.</summary>
	[Serializable()]
	public class SheetFieldDef:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long SheetFieldDefNum;
		///<summary>FK to sheetdef.SheetDefNum.</summary>
		public long SheetDefNum;
		///<summary>Enum:SheetFieldType  OutputText, InputField, StaticText,Parameter(only used for SheetField, not SheetFieldDef),Image,Drawing,Line,Rectangle,CheckBox,SigBox,PatImage.</summary>
		public SheetFieldType FieldType;
		///<summary>Mostly for OutputText, InputField, and CheckBox types.  Each sheet typically has a main datatable type.  For OutputText types, FieldName is usually the string representation of the database column for the main table.  For other tables, it can be of the form table.Column.  There may also be extra fields available that are not strictly pulled from the database.  Extra fields will start with lowercase to indicate that they are not pure database fields.  The list of available fields for each type in SheetFieldsAvailable.  Users can pick from that list.  Likewise, InputField types are internally tied to actions to persist the data.  So they are also hard coded and are available in SheetFieldsAvailable.  For static images, this is the full file name including extension, but without path.  Static images paths are reconstructed by looking in the AtoZ folder, SheetImages folder.  For Pat Images, this is an long FK/DefNum to the default folder for the image.  The filename of a PatImage will later be stored in FieldValue.</summary>
		public string FieldName;
		///<summary>For StaticText, this text can include bracketed fields, like [nameLF].  For OutputText and InputField, this will be blank.  For CheckBoxes, either X or blank.  Even if the checkbox is set to behave like a radio button.</summary>
		public string FieldValue;
		///<summary>The fontSize for this field regardless of the default for the sheet.  The actual font must be saved with each sheetField.</summary>
		public float FontSize;
		///<summary>The fontName for this field regardless of the default for the sheet.  The actual font must be saved with each sheetField.</summary>
		public string FontName;
		///<summary></summary>
		public bool FontIsBold;
		///<summary>In pixels.</summary>
		public int XPos;
		///<summary>In pixels.</summary>
		public int YPos;
		///<summary>The field will be constrained horizontally to this size.  Not allowed to be zero.</summary>
		public int Width;
		///<summary>The field will be constrained vertically to this size.  Not allowed to be 0.  It's not allowed to be zero so that it will be visible on the designer.</summary>
		public int Height;
		///<summary>Enum:GrowthBehaviorEnum</summary>
		public GrowthBehaviorEnum GrowthBehavior;
		///<summary>This is only used for checkboxes that you want to behave like radiobuttons.  Set the FieldName the same for each Checkbox in the group.  The FieldValue will likely be X for one of them and empty string for the others.  Each of them will have a different RadioButtonValue.  Whichever box has X, the RadioButtonValue for that box will be used when importing..</summary>
		public string RadioButtonValue;
		///<summary>Name which identifies the group within which the radio button belongs. FieldName must be set to "misc" in order for the group to take effect.</summary>
		public string RadioButtonGroup;
		///<summary>Set to true if this field is required to have a value before the sheet is closed.</summary>
		public bool IsRequired;

		public SheetFieldDef(){
			//required for use as a generic.
		}
	
		public SheetFieldDef(SheetFieldType fieldType,string fieldName,string fieldValue,
			float fontSize,string fontName,bool fontIsBold,
			int xPos,int yPos,int width,int height,
			GrowthBehaviorEnum growthBehavior,string radioButtonValue) 
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
			RadioButtonValue=radioButtonValue;
		}

		public SheetFieldDef Copy(){
			return (SheetFieldDef)this.MemberwiseClone();
		}

		public override string ToString() {
			return FieldName+" "+FieldValue;
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
			return new SheetFieldDef(SheetFieldType.OutputText,fieldName,"",fontSize,fontName,fontIsBold,
				xPos,yPos,width,height,GrowthBehaviorEnum.None,"");
		}

		public static SheetFieldDef NewOutput(string fieldName,float fontSize,string fontName,bool fontIsBold,
			int xPos,int yPos,int width,int height,GrowthBehaviorEnum growthBehavior)
		{
			return new SheetFieldDef(SheetFieldType.OutputText,fieldName,"",fontSize,fontName,fontIsBold,
				xPos,yPos,width,height,growthBehavior,"");
		}

		public static SheetFieldDef NewStaticText(string fieldValue,float fontSize,string fontName,bool fontIsBold,
			int xPos,int yPos,int width,int height)
		{
			return new SheetFieldDef(SheetFieldType.StaticText,"",fieldValue,fontSize,fontName,fontIsBold,
				xPos,yPos,width,height,GrowthBehaviorEnum.None,"");
		}

		public static SheetFieldDef NewStaticText(string fieldValue,float fontSize,string fontName,bool fontIsBold,
			int xPos,int yPos,int width,int height,GrowthBehaviorEnum growthBehavior)
		{
			return new SheetFieldDef(SheetFieldType.StaticText,"",fieldValue,fontSize,fontName,fontIsBold,
				xPos,yPos,width,height,growthBehavior,"");
		}

		public static SheetFieldDef NewInput(string fieldName,float fontSize,string fontName,bool fontIsBold,
			int xPos,int yPos,int width,int height)
		{
			return new SheetFieldDef(SheetFieldType.InputField,fieldName,"",fontSize,fontName,fontIsBold,
				xPos,yPos,width,height,GrowthBehaviorEnum.None,"");
		}

		public static SheetFieldDef NewImage(string fileName,int xPos,int yPos,int width,int height) {
			return new SheetFieldDef(SheetFieldType.Image,fileName,"",0,"",false,
				xPos,yPos,width,height,GrowthBehaviorEnum.None,"");
		}

		public static SheetFieldDef NewLine(int xPos,int yPos,int width,int height) {
			return new SheetFieldDef(SheetFieldType.Line,"","",0,"",false,
				xPos,yPos,width,height,GrowthBehaviorEnum.None,"");
		}

		public static SheetFieldDef NewRect(int xPos,int yPos,int width,int height) {
			return new SheetFieldDef(SheetFieldType.Rectangle,"","",0,"",false,
				xPos,yPos,width,height,GrowthBehaviorEnum.None,"");
		}

		public static SheetFieldDef NewCheckBox(string fieldName,int xPos,int yPos,int width,int height) {
			return new SheetFieldDef(SheetFieldType.CheckBox,fieldName,"",0,"",false,
				xPos,yPos,width,height,GrowthBehaviorEnum.None,"");
		}

		public static SheetFieldDef NewRadioButton(string fieldName,string radioButtonValue,int xPos,int yPos,int width,int height) {
			return new SheetFieldDef(SheetFieldType.CheckBox,fieldName,"",0,"",false,
				xPos,yPos,width,height,GrowthBehaviorEnum.None,radioButtonValue);
		}

		public static SheetFieldDef NewSigBox(int xPos,int yPos,int width,int height) {
			return new SheetFieldDef(SheetFieldType.SigBox,"","",0,"",false,
				xPos,yPos,width,height,GrowthBehaviorEnum.None,"");
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
