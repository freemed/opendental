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

		///<summary>Just before printing or displaying the final sheet output, the heights and y positions of various fields are adjusted according to their growth behavior.  The adjustments are all made on a copy of the original sheet in order to not alter the original positions.  This returns the sheet copy.</summary>
		public static Sheet CalculateHeights(Sheet sheet,Graphics g){
			//Bitmap bitmap=new Bitmap(1000,1000);
			//Graphics g=Graphics.FromImage(bitmap);
			Sheet sheetCopy=sheet.Copy();
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
			return sheetCopy;
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

		


	}
}
