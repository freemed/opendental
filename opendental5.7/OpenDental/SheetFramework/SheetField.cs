using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDental{
	///<Summary></Summary>
	class SheetField {
		///<Summary>An Out field is pulled from the database to be printed on the sheet.  An In field (not supported yet) is for user input.</Summary>
		public InOutEnum InOut;
		///<Summary>Each sheet typically has a main datatable type.  For Out types, FieldName is usually the string representation of the database column for the main table.  For other tables, it can be of the form table.Column.  There may also be extra fields available that are not strictly pulled from the database.  Extra fields will start with lowercase to indicate that they are not pure database fields.  The list of available fields for each type in SheetFieldsAvailable.  Users could pick from that list.  Likewise, In types are internally tied to actions to persist the data.  So they are also hard coded and are available in SheetFieldsAvailable.</Summary>
		public string FieldName;
		///<Summary>Overrides sheet font.</Summary>
		public Font Font;
		///<Summary>In pixels.</Summary>
		public int XPos;
		///<Summary>In pixels.</Summary>
		private int yPos;
		///<Summary>Before printing, this will be the same as YPos.  But during printing, YPos will get changed with each sheet due to growthBehavior.  YPosOriginal allows us to return YPos to it's original setting.</Summary>
		private int yPosOriginal;
		///<Summary>The field will be constrained horizontally to this size.  Not allowed to be zero.</Summary>
		public int Width;
		///<Summary>The Sheet constructor makes sure that if this is 0, then it will default to the size dictated by the font.  Once we build a sheet designer, the designer will handle the default size.  So it's not allowed to be zero so that it will be visible on the designer.</Summary>
		private int height;
		///<Summary></Summary>
		private int heightOriginal;
		///<Summary></Summary>
		public GrowthBehaviorEnum GrowthBehavior;
		///<Summary>For Out types, this value is set during printing.  This is the data obtained from the database and ready to print.</Summary>
		public string FieldValue;

		///<Summary>This overload is only for SheetFieldsAvailable.</Summary>
		public SheetField(InOutEnum inOut,string fieldName) {
			InOut=inOut;
			FieldName=fieldName;
		}

		public SheetField(InOutEnum inOut,string fieldName,int xPos,int yPos,int width,Font font,GrowthBehaviorEnum growthBehavior) {
			InOut=inOut;
			FieldName=fieldName;
			Font=font;//there must always be a font.
			XPos=xPos;
			this.yPos=yPos;//this constructor is currently the only way to set yPosOriginal
			yPosOriginal=yPos;
			Width=width;
			height=font.Height;//Height is automatic
			heightOriginal=height;
			GrowthBehavior=growthBehavior;
		}

		public int YPos{
			get{
				return yPos;
			}
			set{
				yPos=value;//but not YPosOriginal
			}
		}

		public void SetHeightAndOriginal(int newH){
			height=newH;
			heightOriginal=newH;
		}

		public int Height {
			get {
				return height;
			}
			set {
				height=value;//but not heightOriginal
			}
		}

		public void ResetHeightAndYPosToOriginal(){
			yPos=yPosOriginal;
			height=heightOriginal;
		}

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

	public enum InOutEnum {
		In,
		Out
	}

	public enum GrowthBehaviorEnum {
		///<Summary>Not allowed to grow.  Max size would be height of one row of text, and Width.</Summary>
		None,
		///<Summary>Can grow down if needed, and will push nearby objects out of the way so that there is no overlap.</Summary>
		DownLocal,
		///<Summary>Can grow down, and will push down all objects on the sheet that are below it.  Mostly used when drawing grids.</Summary>
		DownGlobal
	}

}
