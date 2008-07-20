using System;
using System.Collections;
using System.Collections.Generic;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
/*A better name for this object would be a Form, but that name is obviously too ambiguous and has been overused.  This internal framework will later be extended to let users customize sheets.  There are two different aspects of the future database tables:
	1. Customization of sheets
	2. Saving data filled in on sheets (done)
	Sheets will not include reports, which are better handled by the RDL framework or something even simpler.  Examples of what sheets might be used for include statements, tx plans, rx, lab slips, postcards, referral slips, patient registration forms, medical histories, consent forms, and labels.
	The interesting thing about this framework is that it is able to support incoming data as well as outgoing data using the following elements:
	-background image
	-static text
	-text generated from database
	-user input
	Images will be saved in their own table, while the other elements will be be saved separately with each sheet.  Therefore, background images do not need to be saved repeatedly with each printout.
	Class names:
	Data: Sheet, SheetField. (Parameters are saved as part of fields, except PatNum is part of Sheet)
	Defs:	SheetDef, SheetFieldDef (SheetParameters are hardcoded based on type)
  SheetImage
	
	Note that we have tried to do similar things before, but not with as much clarity and organization.  See the ReportingOld2 folder for an example of a similar framework that never took off because:
	a) It was overwhelming because it was trying to handle 'reporting' functions as its main purpose.
	b) It did not start with a simpler framework and build iteratively.
	c) It was modeled after Crystal Reports, which was only designed for reports, not forms.
	d) We did not have generics.
	*/
	///<summary>One sheet for one patient.</summary>
	[DataObject("sheet")]
	public class Sheet : DataObjectBase{
		[DataField("SheetNum",PrimaryKey=true,AutoNumber=true)]
		private int sheetNum;
		private bool sheetNumChanged;
		///<summary>Primary key.</summary>
		public int SheetNum{
			get{return sheetNum;}
			set{if(sheetNum!=value){sheetNum=value;MarkDirty();sheetNumChanged=true;}}
		}
		public bool SheetNumChanged{
			get{return sheetNumChanged;}
		}

		[DataField("SheetType")]
		private SheetTypeEnum sheetType;
		private bool sheetTypeChanged;
		///<summary>Enum:SheetTypeEnum</summary>
		public SheetTypeEnum SheetType{
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

		[DataField("DateTimeSheet")]
		private DateTime dateTimeSheet;
		private bool dateTimeSheetChanged;
		///<summary>The date and time of the sheet as it will be displayed in the commlog.</summary>
		public DateTime DateTimeSheet{
			get{return dateTimeSheet;}
			set{if(dateTimeSheet!=value){dateTimeSheet=value;MarkDirty();dateTimeSheetChanged=true;}}
		}
		public bool DateTimeSheetChanged{
			get{return dateTimeSheetChanged;}
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

		[DataField("IsLandscape")]
		private bool isLandscape;
		private bool isLandscapeChanged;
		///<summary></summary>
		public bool IsLandscape{
			get{return isLandscape;}
			set{if(isLandscape!=value){isLandscape=value;MarkDirty();isLandscapeChanged=true;}}
		}
		public bool IsLandscapeChanged{
			get{return isLandscapeChanged;}
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
		
		public Sheet Copy(){
			return (Sheet)Clone();
		}	

		///<Summary>A collection of all parameters for this sheetdef.  There's usually only one parameter.  The first parameter will be a List int if it's a batch.  If a sheet has already been filled, saved to the database, and printed, then there is no longer any need for the parameters in order to fill the data.  So a retrieved sheet will have no parameters, signalling a skip in the fill phase.  There will still be parameters tucked away in the Field data in the database, but they won't become part of the sheet.</Summary>
		public List<SheetParameter> Parameters;
		///<Summary></Summary>
		///<Summary></Summary>
		public List<SheetField> SheetFields;	











	}
}


