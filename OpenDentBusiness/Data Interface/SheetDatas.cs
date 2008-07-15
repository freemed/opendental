using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SheetDatas{
		/*
		//<summary></summary>
		public static DataTable RefreshCache(){
			string c="SELECT * from sheetData ORDER BY Description";
			DataTable table=General.GetTable(c);
			table.TableName="sheetData";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			sheetDataC.List=new sheetData[table.Rows.Count];
			for(int i=0;i<sheetDataC.List.Length;i++){
				sheetDataC.List[i]=new sheetData();
				sheetDataC.List[i].IsNew=false;
				sheetDataC.List[i].sheetDataNum    = PIn.PInt   (table.Rows[i][0].ToString());
				sheetDataC.List[i].Description= PIn.PString(table.Rows[i][1].ToString());
				sheetDataC.List[i].Note       = PIn.PString(table.Rows[i][2].ToString());
			}
		}*/

		///<Summary>Gets one SheetData from the database.</Summary>
		public static SheetData CreateObject(int sheetDataNum){
			return DataObjectFactory<SheetData>.CreateObject(sheetDataNum);
		}

		///<summary>Used in FormRefAttachEdit to show all referral slips for the patient/referral combo.  Usually 0 or 1 results.</summary>
		public static List<SheetData> GetReferralSlips(int patNum,int referralNum){
			string command="SELECT * FROM sheetdata WHERE PatNum="+POut.PInt(patNum)
				+" ORDER BY DateTimeSheet";
			//still need to enhance query to filter by referralNum.
			return new List<SheetData>(DataObjectFactory<SheetData>.CreateObjects(command));
			//Collection<sheetData> collectState=DataObjectFactory<sheetData>.CreateObjects(sheetDataNums);
			//return new List<sheetData>(collectState);		
			//return list;
		}

		///<summary></summary>
		public static void WriteObject(SheetData sheetData){
			DataObjectFactory<SheetData>.WriteObject(sheetData);
		}

		///<summary></summary>
		public static void DeleteObject(int sheetDataNum){
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
			string command="DELETE FROM sheetfielddata WHERE SheetDataNum="+POut.PInt(sheetDataNum);
			General.NonQ(command);
			DataObjectFactory<SheetData>.DeleteObject(sheetDataNum);
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