using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	public class SheetUtil {
		///<summary>Supply a template sheet as well as a list of primary keys.  This method creates a new collection of sheets which each have a parameter of int.  It also fills the sheets with data from the database, so no need to run that separately.</summary>
		public static List<SheetDef> CreateBatch(SheetDef sheetDef,List<int> priKeys){
			//we'll assume for now that a batch sheet has only one parameter, so no need to check for values.
			//foreach(SheetParameter param in sheet.Parameters){
			//	if(param.IsRequired && param.ParamValue==null){
			//		throw new ApplicationException(Lan.g("Sheet","Parameter not specified for sheet: ")+param.ParamName);
			//	}
			//}
			List<SheetDef> retVal=new List<SheetDef>();
			//List<int> paramVals=(List<int>)sheet.Parameters[0].ParamValue;
			SheetDef newSheetDef;
			SheetParameter paramNew;
			for(int i=0;i<priKeys.Count;i++){
				newSheetDef=sheetDef.Copy();
				newSheetDef.Parameters=new List<SheetParameter>();
				paramNew=new SheetParameter(sheetDef.Parameters[0].IsRequired,sheetDef.Parameters[0].ParamName);
				paramNew.ParamValue=priKeys[i];
				newSheetDef.Parameters.Add(paramNew);
				SheetFiller.FillFields(newSheetDef);
				retVal.Add(newSheetDef);
			}
			return retVal;
		}

		///<summary>Just before printing or displaying the final sheet output, the heights and y positions of various fields are adjusted according to their growth behavior.</summary>
		public static void CalculateHeights(SheetDef sheetDef,Graphics g){
			//Sheet sheetCopy=sheet.Copy();
			int calcH;
			Font font;
			FontStyle fontstyle;
			foreach(SheetFieldDef fieldDef in sheetDef.SheetFieldDefs) {
				if(fieldDef.GrowthBehavior==GrowthBehaviorEnum.None){
					continue;
				}
				fontstyle=FontStyle.Regular;
				if(fieldDef.FontIsBold){
					fontstyle=FontStyle.Bold;
				}
				font=new Font(fieldDef.FontName,fieldDef.FontSize,fontstyle);
				calcH=(int)g.MeasureString(fieldDef.FieldValue,font).Height+2;//min 2 to prevent hidden text due to scroll.
				if(calcH<=fieldDef.Height){
					continue;
				}
				int amountOfGrowth=calcH-fieldDef.Height;
				fieldDef.Height=calcH;
				if(fieldDef.GrowthBehavior==GrowthBehaviorEnum.DownLocal){
					MoveAllDownWhichIntersect(sheetDef,fieldDef);
				}
				else if(fieldDef.GrowthBehavior==GrowthBehaviorEnum.DownGlobal){
					foreach(SheetFieldDef fieldDef2 in sheetDef.SheetFieldDefs) {
						if(fieldDef2.YPos>fieldDef.YPos) {//for all fields that are below this one
							fieldDef2.YPos+=amountOfGrowth;//bump down by amount that this one grew
						}
					}
				}
			}
			//g.Dispose();
			//return sheetCopy;
		}

		///<Summary>Supply the field that we are testing.  All other fields which intersect with it will be moved down.  Each time one is moved down, this method is called recursively.  The end result should be no intersections among fields near to the original field that grew.</Summary>
		private static void MoveAllDownWhichIntersect(SheetDef sheetDef,SheetFieldDef fieldDef){
			//it turns out that order of operation is critical.
			//The recursion feature can't be called until everything below has been evenly moved down.
			//So... First phase
			foreach(SheetFieldDef fieldDef2 in sheetDef.SheetFieldDefs) {
				if(fieldDef2==fieldDef){
					continue;
				}
				if(fieldDef2.YPos<fieldDef.YPos){//only fields which are below this one
					continue;
				}
				if(fieldDef.Bounds.IntersectsWith(fieldDef2.Bounds)) {
					//Debug.WriteLine(field.FieldValue+" -forces-> "+field2.FieldValue+" -to-> "+field.Bounds.Bottom.ToString());
					fieldDef2.YPos=fieldDef.Bounds.Bottom;
					MoveAllDownWhichIntersect(sheetDef,fieldDef2);
				}
			}
		}

		///<summary>Just creates the initial Sheet object from the sheetDef.  Doesn't add fields.  Sets date to today.</summary>
		public static Sheet CreateSheet(SheetDef sheetDef,int patNum){
			Sheet sheet=new Sheet();
			sheet.IsNew=true;
			sheet.DateTimeSheet=DateTime.Now;
			sheet.FontName=sheetDef.FontName;
			sheet.FontSize=sheetDef.FontSize;
			sheet.Height=sheetDef.Height;
			sheet.SheetType=sheetDef.SheetType;
			sheet.Width=sheetDef.Width;
			sheet.PatNum=patNum;
			sheet.IsLandscape=sheetDef.IsLandscape;
			return sheet;
		}

		/*
		///<summary>After pulling a list of SheetFieldData objects from the database, we use this to convert it to a list of SheetFields as we create the Sheet.</summary>
		public static List<SheetField> CreateSheetFields(List<SheetFieldData> sheetFieldDataList){
			List<SheetField> retVal=new List<SheetField>();
			SheetField field;
			FontStyle style;
			for(int i=0;i<sheetFieldDataList.Count;i++){
				style=FontStyle.Regular;
				if(sheetFieldDataList[i].FontIsBold){
					style=FontStyle.Bold;
				}
				field=new SheetField(sheetFieldDataList[i].FieldType,sheetFieldDataList[i].FieldName,sheetFieldDataList[i].FieldValue,
					sheetFieldDataList[i].XPos,sheetFieldDataList[i].YPos,sheetFieldDataList[i].Width,sheetFieldDataList[i].Height,
					new Font(sheetFieldDataList[i].FontName,sheetFieldDataList[i].FontSize,style),sheetFieldDataList[i].GrowthBehavior);
				retVal.Add(field);
			}
			return retVal;
		}*/

		///<summary>Creates the initial fields from the sheetDef.FieldDefs.</summary>
		public static List<SheetField> CreateFieldList(List<SheetFieldDef> sheetFieldDefList){
			List<SheetField> retVal=new List<SheetField>();
			SheetField field;
			for(int i=0;i<sheetFieldDefList.Count;i++){
				field=new SheetField();
				field.IsNew=true;
				field.FieldName=sheetFieldDefList[i].FieldName;
				field.FieldType=sheetFieldDefList[i].FieldType;
				field.FieldValue=sheetFieldDefList[i].FieldValue;
				field.FontIsBold=sheetFieldDefList[i].FontIsBold;
				field.FontName=sheetFieldDefList[i].FontName;
				field.FontSize=sheetFieldDefList[i].FontSize;
				field.GrowthBehavior=sheetFieldDefList[i].GrowthBehavior;
				field.Height=sheetFieldDefList[i].Height;
				//field.SheetNum=sheetFieldList[i];//set later
				field.Width=sheetFieldDefList[i].Width;
				field.XPos=sheetFieldDefList[i].XPos;
				field.YPos=sheetFieldDefList[i].YPos;
				retVal.Add(field);
			}
			return retVal;
		}
		


	}
}
