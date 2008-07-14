using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SheetFieldDatas{

		///<Summary>Gets one SheetFieldData from the database.</Summary>
		public static SheetFieldData CreateObject(int sheetFieldDataNum){
			return DataObjectFactory<SheetFieldData>.CreateObject(sheetFieldDataNum);
		}

		///<summary>Gets all fields for one sheet.</summary>
		public static List<SheetFieldData> GetForSheet(int sheetDataNum){
			string command="SELECT * FROM sheetfielddata WHERE SheetDataNum="+POut.PInt(sheetDataNum);
			return new List<SheetFieldData>(DataObjectFactory<SheetFieldData>.CreateObjects(command));
		}

		///<summary></summary>
		public static void WriteObject(SheetFieldData sheetFieldData){
			DataObjectFactory<SheetFieldData>.WriteObject(sheetFieldData);
		}

		///<summary></summary>
		public static void DeleteObject(int sheetFieldDataNum){
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
			DataObjectFactory<SheetFieldData>.DeleteObject(sheetFieldDataNum);
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