using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
	public class SheetUtil {
		///<summary>Supply a template sheet as well as a list of primary keys.  This method creates a new collection of sheets which each have a parameter of int.  It also fills the sheets with data from the database, so no need to run that separately.</summary>
		public static List<Sheet> CreateBatch(SheetDef sheetDef,List<long> priKeys) {
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
				newSheet=CreateSheet(sheetDef);
				newSheet.Parameters=new List<SheetParameter>();
				paramNew=new SheetParameter(sheetDef.Parameters[0].IsRequired,sheetDef.Parameters[0].ParamName);
				paramNew.ParamValue=priKeys[i];
				newSheet.Parameters.Add(paramNew);
				SheetFiller.FillFields(newSheet);
				retVal.Add(newSheet);
			}
			return retVal;
		}

		///<summary>Just before printing or displaying the final sheet output, the heights and y positions of various fields are adjusted according to their growth behavior.  This also now gets run every time a user changes the value of a textbox while filling out a sheet.</summary>
		public static void CalculateHeights(Sheet sheet,Graphics g){
			//Sheet sheetCopy=sheet.Copy();
			int calcH;
			Font font;
			FontStyle fontstyle;
			foreach(SheetField field in sheet.SheetFields) {
				if(field.GrowthBehavior==GrowthBehaviorEnum.None){
					continue;
				}
				fontstyle=FontStyle.Regular;
				if(field.FontIsBold){
					fontstyle=FontStyle.Bold;
				}
				font=new Font(field.FontName,field.FontSize,fontstyle);
				//calcH=(int)g.MeasureString(field.FieldValue,font).Height;//this was too short
				calcH=GraphicsHelper.MeasureStringH(g,field.FieldValue,font,field.Width);
				if(calcH<=field.Height){
					continue;
				}
				int amountOfGrowth=calcH-field.Height;
				field.Height=calcH;
				if(field.GrowthBehavior==GrowthBehaviorEnum.DownLocal){
					MoveAllDownWhichIntersect(sheet,field);
				}
				else if(field.GrowthBehavior==GrowthBehaviorEnum.DownGlobal){
					MoveAllDownBelowThis(sheet,field,amountOfGrowth);
					
				}
			}
			//g.Dispose();
			//return sheetCopy;
		}

		public static void MoveAllDownBelowThis(Sheet sheet,SheetField field,int amountOfGrowth){
			foreach(SheetField field2 in sheet.SheetFields) {
				if(field2.YPos>field.YPos) {//for all fields that are below this one
					field2.YPos+=amountOfGrowth;//bump down by amount that this one grew
				}
			}
		}

		///<Summary>Supply the field that we are testing.  All other fields which intersect with it will be moved down.  Each time one is moved down, this method is called recursively.  The end result should be no intersections among fields near to the original field that grew.</Summary>
		public static void MoveAllDownWhichIntersect(Sheet sheet,SheetField field){
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
				if(field2.FieldType==SheetFieldType.Drawing){
					continue;
					//drawings do not get moved down.
				}
				if(field.Bounds.IntersectsWith(field2.Bounds)) {
					//Debug.WriteLine(field.FieldValue+" -forces-> "+field2.FieldValue+" -to-> "+field.Bounds.Bottom.ToString());
					field2.YPos=field.Bounds.Bottom;
					MoveAllDownWhichIntersect(sheet,field2);
				}
			}
			//js- did I forget the second phase?
		}

		///<summary>Creates a Sheet object from a sheetDef, complete with fields and parameters.  This overload is only to be used when the sheet will not be saved to the database, such as for labels</summary>
		public static Sheet CreateSheet(SheetDef sheetDef){
			return CreateSheet(sheetDef,0);
		}

		///<summary>Creates a Sheet object from a sheetDef, complete with fields and parameters.  Sets date to today.</summary>
		public static Sheet CreateSheet(SheetDef sheetDef,long patNum) {
			Sheet sheet=new Sheet();
			sheet.IsNew=true;
			sheet.DateTimeSheet=DateTime.Now;
			sheet.FontName=sheetDef.FontName;
			sheet.FontSize=sheetDef.FontSize;
			sheet.Height=sheetDef.Height;
			sheet.SheetType=sheetDef.SheetType;
			sheet.Width=sheetDef.Width;
			sheet.PatNum=patNum;
			sheet.Description=sheetDef.Description;
			sheet.IsLandscape=sheetDef.IsLandscape;
			sheet.SheetFields=CreateFieldList(sheetDef.SheetFieldDefs);
			sheet.Parameters=sheetDef.Parameters;
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
		private static List<SheetField> CreateFieldList(List<SheetFieldDef> sheetFieldDefList){
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
				field.RadioButtonValue=sheetFieldDefList[i].RadioButtonValue;
				//field.SheetNum=sheetFieldList[i];//set later
				field.Width=sheetFieldDefList[i].Width;
				field.XPos=sheetFieldDefList[i].XPos;
				field.YPos=sheetFieldDefList[i].YPos;
				retVal.Add(field);
			}
			return retVal;
		}

		///<summary>Typically returns something similar to \\SERVER\OpenDentImages\SheetImages</summary>
		public static string GetImagePath(){
			string imagePath;
			if(!PrefC.UsingAtoZfolder) {
				throw new ApplicationException("Must be using AtoZ folders.");
			}
			imagePath=ODFileUtils.CombinePaths(ImageStore.GetPreferredImagePath(),"SheetImages");
			if(!Directory.Exists(imagePath)) {
				Directory.CreateDirectory(imagePath);
			}
			return imagePath;
		}
		


	}
}
