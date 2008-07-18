using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness{
	///<Summary></Summary>
	public class SheetFieldDef {
		///<Summary>OutputText, InputField, StaticText,Parameter(only used for SheetFieldData, not SheetField).</Summary>
		public SheetFieldType FieldType;
		///<Summary>Only for OutputText and InputField types.  Each sheet typically has a main datatable type.  For OutputText types, FieldName is usually the string representation of the database column for the main table.  For other tables, it can be of the form table.Column.  There may also be extra fields available that are not strictly pulled from the database.  Extra fields will start with lowercase to indicate that they are not pure database fields.  The list of available fields for each type in SheetFieldsAvailable.  Users could pick from that list.  Likewise, InputField types are internally tied to actions to persist the data.  So they are also hard coded and are available in SheetFieldsAvailable.</Summary>
		public string FieldName;
		///<Summary>For OutputText, this value is set before printing.  This is the data obtained from the database and ready to print.  For StaticText, this is set when designing the sheetDef.  For an archived sheet retrieved from the database (all SheetFieldData rows), this value will have been saved and will not be filled again.</Summary>
		public string FieldValue;
		public float FontSize;
		public string FontName;
		public bool FontIsBold;
		///<Summary>In pixels.</Summary>
		public int XPos;
		///<Summary>In pixels.</Summary>
		public int YPos;
		///<Summary>The field will be constrained horizontally to this size.  Not allowed to be zero.</Summary>
		public int Width;
		///<Summary>The field will be constrained vertically to this size.  Not allowed to be 0.  The Sheet constructor makes sure that if this is 0, then it will default to the size dictated by the font.  Once we build a sheet designer, the designer will handle the default size.  So it's not allowed to be zero so that it will be visible on the designer.</Summary>
		public int Height;
		///<Summary></Summary>
		public GrowthBehaviorEnum GrowthBehavior;
	
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
