using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{

	///<summary>A definition (template) for a sheet.  Can be pulled from the database, or it can be internally defined.</summary>
	[DataObject("sheetdef")]
	public class SheetDef : DataObjectBase{
		[DataField("SheetDefNum",PrimaryKey=true,AutoNumber=true)]
		private long sheetDefNum;
		private bool sheetDefNumChanged;
		///<summary>Primary key.</summary>
		public long SheetDefNum{
			get{return sheetDefNum;}
			set{if(sheetDefNum!=value){sheetDefNum=value;MarkDirty();sheetDefNumChanged=true;}}
		}
		public bool SheetDefNumChanged{
			get{return sheetDefNumChanged;}
		}

		[DataField("Description")]
		private string description;
		private bool descriptionChanged;
		///<summary>The description of this sheetdef.</summary>
		public string Description{
			get{return description;}
			set{if(description!=value){description=value;MarkDirty();descriptionChanged=true;}}
		}
		public bool DescriptionChanged{
			get{return descriptionChanged;}
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
		private long width;
		private bool widthChanged;
		///<summary>Width of the sheet in pixels, 100 pixels per inch.</summary>
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
		///<summary>Height of the sheet in pixels, 100 pixels per inch.</summary>
		public long Height{
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

		///<Summary>A collection of all parameters for this sheetdef.  There's usually only one parameter.  The first parameter will be a List long if it's a batch.  If a sheet has already been filled, saved to the database, and printed, then there is no longer any need for the parameters in order to fill the data.  So a retrieved sheet will have no parameters, signalling a skip in the fill phase.  There will still be parameters tucked away in the Field data in the database, but they won't become part of the sheet.</Summary>
		public List<SheetParameter> Parameters;
		///<Summary></Summary>
		public List<SheetFieldDef> SheetFieldDefs;		

		///<Summary></Summary>
		public Font GetFont(){
			return new Font(FontName,FontSize);
		}

		public SheetDef(){//required for use as a generic.
			
		}

		public SheetDef(SheetTypeEnum sheetType){
			SheetType=sheetType;
			Parameters=SheetParameter.GetForType(sheetType);
			SheetFieldDefs=new List<SheetFieldDef>();
		}

		public SheetDef Copy(){
			SheetDef sheetdef=(SheetDef)this.MemberwiseClone();
			//do I need to copy the lists?
			return sheetdef;
		}

		
		
		

	}

	

	

}
