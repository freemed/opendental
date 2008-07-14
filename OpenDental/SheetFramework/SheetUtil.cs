using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	public class SheetUtil {
		///<summary>Supply a template sheet as well as a list of primary keys.  This method creates a new collection of sheets which each have a parameter of int.  It also fills the sheets with data from the database, so no need to run that separately.</summary>
		public static List<Sheet> CreateBatch(Sheet sheet,List<int> priKeys){
			//we'll assume for now that a batch sheet has only one parameter, so no need to check for values.
			//foreach(SheetParameter param in sheet.Parameters){
			//	if(param.IsRequired && param.ParamValue==null){
			//		throw new ApplicationException(Lan.g("Sheet","Parameter not specified for sheet: ")+param.ParamName);
			//	}
			//}
			List<Sheet> retVal=new List<Sheet>();
			//List<int> paramVals=(List<int>)sheet.Parameters[0].ParamValue;
			Sheet newSheet;
			SheetParameter paramNew;
			for(int i=0;i<priKeys.Count;i++){
				newSheet=sheet.Copy();
				newSheet.Parameters=new List<SheetParameter>();
				paramNew=new SheetParameter(sheet.Parameters[0].IsRequired,sheet.Parameters[0].ParamName);
				paramNew.ParamValue=priKeys[i];
				newSheet.Parameters.Add(paramNew);
				SheetFiller.FillFields(newSheet);
				retVal.Add(newSheet);
			}
			return retVal;
		}

		///<summary>Just before printing or displaying the final sheet output, the heights and y positions of various fields are adjusted according to their growth behavior.</summary>
		public static void CalculateHeights(Sheet sheet,Graphics g){
			//Sheet sheetCopy=sheet.Copy();
			int calcH;
			foreach(SheetField field in sheet.SheetFields) {
				if(field.GrowthBehavior==GrowthBehaviorEnum.None){
					continue;
				}
				calcH=(int)g.MeasureString(field.FieldValue,field.Font).Height+2;//min 2 to prevent hidden text due to scroll.
				if(calcH<=field.Height){
					continue;
				}
				int amountOfGrowth=calcH-field.Height;
				field.Height=calcH;
				if(field.GrowthBehavior==GrowthBehaviorEnum.DownLocal){
					MoveAllDownWhichIntersect(sheet,field);
				}
				else if(field.GrowthBehavior==GrowthBehaviorEnum.DownGlobal){
					foreach(SheetField field2 in sheet.SheetFields) {
						if(field2.YPos>field.YPos) {//for all fields that are below this one
							field2.YPos+=amountOfGrowth;//bump down by amount that this one grew
						}
					}
				}
			}
			//g.Dispose();
			//return sheetCopy;
		}

		///<Summary>Supply the field that we are testing.  All other fields which intersect with it will be moved down.  Each time one is moved down, this method is called recursively.  The end result should be no intersections among fields near to the original field that grew.</Summary>
		private static void MoveAllDownWhichIntersect(Sheet sheet,SheetField field){
			//it turns out that order of operation is critical.
			//The recursion feature can't be called until everything below has been evenly moved down.
			//So... First phase
			foreach(SheetField field2 in sheet.SheetFields) {
				if(field2==field){
					continue;
				}
				if(field2.YPos<field.YPos){//only fields which are below this one
					continue;
				}
				if(field.Bounds.IntersectsWith(field2.Bounds)) {
					//Debug.WriteLine(field.FieldValue+" -forces-> "+field2.FieldValue+" -to-> "+field.Bounds.Bottom.ToString());
					field2.YPos=field.Bounds.Bottom;
					MoveAllDownWhichIntersect(sheet,field2);
				}
			}
		}

		///<summary>Just creates the initial SheetData object from the sheet.  Doesn't add fields.  Sets date to today.</summary>
		public static SheetData CreateSheetData(Sheet sheet,int patNum){
			SheetData sheetData=new SheetData();
			sheetData.IsNew=true;
			sheetData.DateTimeSheet=DateTime.Now;
			sheetData.FontName=sheet.Font.Name;
			sheetData.FontSize=sheet.Font.Size;
			sheetData.Height=sheet.Height;
			sheetData.SheetType=sheet.SheetType;
			sheetData.Width=sheet.Width;
			sheetData.PatNum=patNum;
			return sheetData;
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

		///<summary>Creates the initial fields from the sheet.Fields.</summary>
		public static List<SheetFieldData> CreateFieldList(List<SheetField> sheetFieldList){
			List<SheetFieldData> retVal=new List<SheetFieldData>();
			SheetFieldData field;
			for(int i=0;i<sheetFieldList.Count;i++){
				field=new SheetFieldData();
				field.IsNew=true;
				field.FieldName=sheetFieldList[i].FieldName;
				field.FieldType=sheetFieldList[i].FieldType;
				field.FieldValue=sheetFieldList[i].FieldValue;
				field.FontIsBold=sheetFieldList[i].Font.Bold;
				field.FontName=sheetFieldList[i].Font.Name;
				field.FontSize=sheetFieldList[i].Font.Size;
				field.GrowthBehavior=sheetFieldList[i].GrowthBehavior;
				field.Height=sheetFieldList[i].Height;
				//field.SheetDataNum=sheetFieldList[i];//set later
				field.Width=sheetFieldList[i].Width;
				field.XPos=sheetFieldList[i].XPos;
				field.YPos=sheetFieldList[i].YPos;
				retVal.Add(field);
			}
			return retVal;
		}
		


	}
}
