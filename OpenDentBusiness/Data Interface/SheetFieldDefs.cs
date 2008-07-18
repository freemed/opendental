using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SheetFieldDefs{

		///<Summary>Gets one SheetFieldDef from the database.</Summary>
		public static SheetFieldDef CreateObject(int sheetFieldDefNum){
			return DataObjectFactory<SheetFieldDef>.CreateObject(sheetFieldDefNum);
		}

		///<summary>Gets all fieldDefs for one sheetDef.</summary>
		public static List<SheetFieldDef> GetForSheetDef(int sheetDefNum){
			string command="SELECT * FROM sheetfielddef WHERE SheetDefNum="+POut.PInt(sheetDefNum);
			return new List<SheetFieldDef>(DataObjectFactory<SheetFieldDef>.CreateObjects(command));
		}

		///<summary></summary>
		public static void WriteObject(SheetFieldDef sheetFieldDef){
			DataObjectFactory<SheetFieldDef>.WriteObject(sheetFieldDef);
		}

		///<summary></summary>
		public static void DeleteObject(int sheetFieldDefNum){
			//validate that not already in use.
			/*string command="SELECT LName,FName FROM patient WHERE sheetDataNum="+POut.PInt(sheetDataNum);
			DataTable table=General.GetTable(command);
			//int count=PIn.PInt(General.GetCount(command));
			string pats="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					pats+=", ";
				}
				pats+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lan.g("sheetDatas","sheetData is already in use by patient(s). Not allowed to delete. "+pats));
			}*/
			DataObjectFactory<SheetFieldDef>.DeleteObject(sheetFieldDefNum);
		}

		//public static void DeleteObject(int sheetDataNum){
		//	DataObjectFactory<sheetData>.DeleteObject(sheetDataNum);
		//}

		/*public static string GetDescription(int sheetDataNum){
			if(sheetDataNum==0){
				return "";
			}
			for(int i=0;i<sheetDataC.List.Length;i++){
				if(sheetDataC.List[i].sheetDataNum==sheetDataNum){
					return sheetDataC.List[i].Description;
				}
			}
			return "";
		}*/

	}
}