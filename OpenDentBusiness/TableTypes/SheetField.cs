using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>One field on a sheet.</summary>
	[Serializable()]
	public class SheetField:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long SheetFieldNum;
		///<summary>FK to sheet.SheetNum.</summary>
		public long SheetNum;
		///<summary>Enum:SheetFieldType  OutputText, InputField, StaticText,Parameter(only used for SheetField, not SheetFieldDef),Image,Drawing,Line,Rectangle,CheckBox,SigBox,PatImage.</summary>
		public SheetFieldType FieldType;
		///<summary>Mostly for OutputText and InputField types.  Each sheet typically has a main datatable type.  For OutputText types, FieldName is usually the string representation of the database column for the main table.  For other tables, it can be of the form table.Column.  There may also be extra fields available that are not strictly pulled from the database.  Extra fields will start with lowercase to indicate that they are not pure database fields.  The list of available fields for each type in SheetFieldsAvailable.  Users can pick from that list.  Likewise, InputField types are internally tied to actions to persist the data.  So they are also hard coded and are available in SheetFieldsAvailable.  For static images, this is the full file name including extension, but without path.  Static images paths are reconstructed by looking in the AtoZ folder, SheetImages folder.  For a PatImage, this now contains the long FK to the document.  A join must be done to that table to find the filename.   When a SheetField has a fieldType of Parameter, then the FieldName stores the name of the parameter.</summary>
		public string FieldName;
		///<summary>For OutputText, this value is set before printing.  This is the data obtained from the database and ready to print.  For StaticText, this is copied from the sheetFieldDef, but in-line fields like [this] will have been filled.  For an archived sheet retrieved from the database (all SheetField rows), this value will have been saved and will not be filled again automatically.  For a parameter fieldtype, this will store the value of the parameter. For a Drawing fieldtype, this will be the point data for the lines.  The format would look similar to this: 45,68;48,70;49,72;0,0;55,88;etc.  It's simply a sequence of points, separated by semicolons.  For CheckBox, it will either be an X or empty.  For SigBox, the first char will be 0 or 1 to indicate SigIsTopaz, and all subsequent chars will be the Signature itself.   For Pat Image, this contains image size and image position info.  Like this: "X=0,Y=20,W=100,H=60".  This is initially generated automatically to fit the object.  It can later be changed by the user to "zoom and pan" within the confines of the object.</summary>
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
				
		public SheetField Copy(){
			return (SheetField)this.MemberwiseClone();
		}

		public override string ToString() {
			return FieldName+" "+FieldValue;
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


