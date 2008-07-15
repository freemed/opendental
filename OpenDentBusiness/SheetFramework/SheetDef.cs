using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;

namespace OpenDentBusiness{
	
	///<summary>A definition (template) for a sheet.  Can be pulled from the database, or it can be internally defined.</summary>
	public class SheetDef {
		///<Summary>Every single sheet must have a type, and only the types listed in the enum will be supported as part of the Sheet framework.</Summary>
		public SheetTypeEnum SheetType;
		///<Summary>A collection of all parameters for this sheet.  There's usually only one parameter.  The first parameter will be a List int if it's a batch.  If a sheet has already been filled, saved to the database, and printed, then there is no longer any need for the parameters in order to fill the data.  So a retrieved sheet will have no parameters, signalling a skip in the fill phase.  There will still be parameters tucked away in the Field data in the database, but they won't become part of the sheet.</Summary>
		public List<SheetParameter> Parameters;
		///<Summary></Summary>
		public List<SheetFieldDef> SheetFieldDefs;
		///<Summary></Summary>
		public Font Font;
		public int Width;
		public int Height;

		//private Font fontDefault=

		public SheetDef(SheetTypeEnum sheetType){
			SheetType=sheetType;
			Parameters=SheetParameter.GetForType(sheetType);
			SheetFieldDefs=new List<SheetFieldDef>();
			Font=new Font(FontFamily.GenericSansSerif,8.5f);
		}

		public SheetDef Copy(){
			SheetDef sheetdef=(SheetDef)this.MemberwiseClone();
			//do I need to copy the lists?
			return sheetdef;
		}

		public void SetParameter(string paramName,object paramValue){
			SheetParameter param=GetParamByName(paramName);
			if(param==null){
				throw new ApplicationException(Lan.g("Sheet","Parameter not found: ")+paramName);
			}
			param.ParamValue=paramValue;
		}

		private SheetParameter GetParamByName(string paramName){
			foreach(SheetParameter param in Parameters){
				if(param.ParamName==paramName){
					return param;
				}
			}
			return null;
		}

		

	}

	

	

}
