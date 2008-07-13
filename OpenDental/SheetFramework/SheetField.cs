using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDental{
	///<Summary></Summary>
	public class SheetField {
		///<Summary>OutputText, InputField, StaticText.</Summary>
		public SheetFieldType FieldType;
		///<Summary>Only for OutputText and InputField types.  Each sheet typically has a main datatable type.  For OutputText types, FieldName is usually the string representation of the database column for the main table.  For other tables, it can be of the form table.Column.  There may also be extra fields available that are not strictly pulled from the database.  Extra fields will start with lowercase to indicate that they are not pure database fields.  The list of available fields for each type in SheetFieldsAvailable.  Users could pick from that list.  Likewise, InputField types are internally tied to actions to persist the data.  So they are also hard coded and are available in SheetFieldsAvailable.</Summary>
		public string FieldName;
		///<Summary>Overrides sheet font.</Summary>
		public Font Font;
		///<Summary>In pixels.</Summary>
		public int XPos;
		///<Summary>The field will be constrained horizontally to this size.  Not allowed to be zero.</Summary>
		public int Width;
		///<Summary></Summary>
		public GrowthBehaviorEnum GrowthBehavior;
		///<Summary>For OutputText, this value is set during printing.  This is the data obtained from the database and ready to print.  For StaticText, this is set ahead of time when designing the sheet.</Summary>
		public string FieldValue;

		///<Summary>The field will be constrained vertically to this size.  Not allowed to be 0.  The Sheet constructor makes sure that if this is 0, then it will default to the size dictated by the font.  Once we build a sheet designer, the designer will handle the default size.  So it's not allowed to be zero so that it will be visible on the designer.</Summary>
		private int height;
		///<Summary>In pixels.</Summary>
		private int yPos;

		//<Summary></Summary>
		//private int heightOriginal;
		//<Summary>Before printing, this will be the same as YPos.  But during printing, YPos will get changed with each sheet due to growthBehavior.  YPosOriginal allows us to return YPos to it's original setting.</Summary>
		//private int yPosOriginal;

		/*
		///<Summary>This overload is only for SheetFieldsAvailable.</Summary>
		public SheetField(SheetFieldType fieldType,string fieldName) {
			FieldType=fieldType;
			FieldName=fieldName;
		}*/

		public SheetField(SheetFieldType fieldType,string fieldName,string fieldValue,
			int xPos,int yPos,int width,int height,Font font,GrowthBehaviorEnum growthBehavior) 
		{
			FieldType=fieldType;
			FieldName=fieldName;
			FieldValue=fieldValue;
			XPos=xPos;
			this.yPos=yPos;
			//yPosOriginal=yPos;
			Width=width;
			this.height=height;
			//heightOriginal=height;
			Font=font;
			GrowthBehavior=growthBehavior;
		}

		public static SheetField NewOutput(string fieldName,int xPos,int yPos,int width,Font font){
			int _height=font.Height+1;//Height is automatic in this early implementation.
			return new SheetField(SheetFieldType.OutputText,fieldName,"",xPos,yPos,width,_height,font,GrowthBehaviorEnum.None);
		}

		public static SheetField NewOutput(string fieldName,int xPos,int yPos,int width,Font font,GrowthBehaviorEnum growthBehavior){
			int _height=font.Height+1;//Height is automatic in this early implementation.
			return new SheetField(SheetFieldType.OutputText,fieldName,"",xPos,yPos,width,_height,font,growthBehavior);
		}

		public static SheetField NewStaticText(string fieldValue,int xPos,int yPos,int width,Font font){
			int _height=font.Height+1;//Height is automatic in this early implementation.
			return new SheetField(SheetFieldType.StaticText,"",fieldValue,xPos,yPos,width,_height,font,GrowthBehaviorEnum.None);
		}

		public static SheetField NewInput(string fieldName,int xPos,int yPos,int width,int height,Font font){
			return new SheetField(SheetFieldType.InputField,fieldName,"",xPos,yPos,width,height,font,GrowthBehaviorEnum.None);
		}

		public int YPos{
			get{
				return yPos;
			}
			set{
				yPos=value;//but not YPosOriginal
			}
		}

		//public void SetHeightAndOriginal(int newH){
		//	height=newH;
		//	heightOriginal=newH;
		//}

		public int Height {
			get {
				return height;
			}
			set {
				height=value;//but not heightOriginal
			}
		}

		//public void ResetHeightAndYPosToOriginal(){
		//	yPos=yPosOriginal;
		//	height=heightOriginal;
		//}

		///<Summary>Should only be called after FieldValue has been set, due to GrowthBehavior.</Summary>
		public Rectangle Bounds {
			get {
				return new Rectangle(XPos,yPos,Width,height);
			}
		}

		///<Summary>Should only be called after FieldValue has been set, due to GrowthBehavior.</Summary>
		public RectangleF BoundsF {
			get {
				return new RectangleF(XPos,yPos,Width,height);
			}
		}
	}

	public enum GrowthBehaviorEnum {
		///<Summary>Not allowed to grow.  Max size would be Height(generated automatically for now) and Width.</Summary>
		None,
		///<Summary>Can grow down if needed, and will push nearby objects out of the way so that there is no overlap.</Summary>
		DownLocal,
		///<Summary>Can grow down, and will push down all objects on the sheet that are below it.  Mostly used when drawing grids.</Summary>
		DownGlobal
	}

	public enum SheetFieldType {
		///<Summary>Pulled from the database to be printed on the sheet.  Or also possibly just generated at runtime even though not pulled from the database.   Will probably allow user to change the output text as they are filling out the sheet so that it can different from what was initially generated.</Summary>
		OutputText,
		///<Summary>A blank box that the user is supposed to fill in.</Summary>
		InputField,
		///<Summary>This is text that is defined as part of the sheet and will never change from sheet to sheet.  </Summary>
		StaticText
		//<summary></summary>
		//CheckBox
		//<summary></summary>
		//RadioButton
		//<Summary>Not yet supported</Summary>
		//Image,
		//<Summary>Not yet supported</Summary>
		//Line,
		//<Summary>Not yet supported.  This might be redundant, and we might use border element instead as the preferred way of drawing a box.</Summary>
		//Box
	}

}
