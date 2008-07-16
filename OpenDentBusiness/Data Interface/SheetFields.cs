using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SheetFields{

		///<Summary>Gets one SheetField from the database.</Summary>
		public static SheetField CreateObject(int sheetFieldNum){
			return DataObjectFactory<SheetField>.CreateObject(sheetFieldNum);
		}

		///<summary>Gets all fields for one sheet.</summary>
		public static List<SheetField> GetForSheet(int sheetNum){
			string command="SELECT * FROM sheetfield WHERE SheetNum="+POut.PInt(sheetNum);
			return new List<SheetField>(DataObjectFactory<SheetField>.CreateObjects(command));
		}

		///<summary></summary>
		public static void WriteObject(SheetField sheetField){
			DataObjectFactory<SheetField>.WriteObject(sheetField);
		}

		///<summary></summary>
		public static void DeleteObject(int sheetFieldNum){
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
			DataObjectFactory<SheetField>.DeleteObject(sheetFieldNum);
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