using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SheetDefs{
		//<summary></summary>
		public static DataTable RefreshCache(){
			string c="SELECT * FROM sheetdef ORDER BY Description";
			DataTable table=General.GetTable(c);
			table.TableName="sheetdef";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			/*sheetDataC.List=new sheetData[table.Rows.Count];
			for(int i=0;i<sheetDataC.List.Length;i++){
				sheetDataC.List[i]=new sheetData();
				sheetDataC.List[i].IsNew=false;
				sheetDataC.List[i].sheetDataNum    = PIn.PInt   (table.Rows[i][0].ToString());
				sheetDataC.List[i].Description= PIn.PString(table.Rows[i][1].ToString());
				sheetDataC.List[i].Note       = PIn.PString(table.Rows[i][2].ToString());
			}*/
		}

		///<Summary>Gets one SheetDef from the database.</Summary>
		public static SheetDef CreateObject(int sheetDefNum){
			return DataObjectFactory<SheetDef>.CreateObject(sheetDefNum);
		}

		//<summary>Used in FormRefAttachEdit to show all referral slips for the patient/referral combo.  Usually 0 or 1 results.</summary>
		//public static List<Sheet> GetReferralSlips(int patNum,int referralNum){
		//	string command="SELECT * FROM sheet WHERE PatNum="+POut.PInt(patNum)
		//		+" ORDER BY DateTimeSheet";
			//still need to enhance query to filter by referralNum.
		//	return new List<Sheet>(DataObjectFactory<Sheet>.CreateObjects(command));
			//Collection<sheetData> collectState=DataObjectFactory<sheetData>.CreateObjects(sheetDataNums);
			//return new List<sheetData>(collectState);		
			//return list;
		//}

		///<summary></summary>
		public static void WriteObject(SheetDef sheetDef){
			DataObjectFactory<SheetDef>.WriteObject(sheetDef);
		}

		///<summary></summary>
		public static void DeleteObject(int sheetDefNum){
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
			string command="DELETE FROM sheetfielddef WHERE SheetDefNum="+POut.PInt(sheetDefNum);
			General.NonQ(command);
			DataObjectFactory<SheetDef>.DeleteObject(sheetDefNum);
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