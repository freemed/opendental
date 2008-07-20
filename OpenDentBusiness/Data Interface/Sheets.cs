using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Sheets{
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

		///<Summary>Gets one Sheet from the database.</Summary>
		public static Sheet CreateObject(int sheetNum){
			return DataObjectFactory<Sheet>.CreateObject(sheetNum);
		}

		///<summary>Used in FormRefAttachEdit to show all referral slips for the patient/referral combo.  Usually 0 or 1 results.</summary>
		public static List<Sheet> GetReferralSlips(int patNum,int referralNum){
			string command="SELECT * FROM sheet WHERE PatNum="+POut.PInt(patNum)
				+" ORDER BY DateTimeSheet";
			//still need to enhance query to filter by referralNum.
			return new List<Sheet>(DataObjectFactory<Sheet>.CreateObjects(command));
			//Collection<sheetData> collectState=DataObjectFactory<sheetData>.CreateObjects(sheetDataNums);
			//return new List<sheetData>(collectState);		
			//return list;
		}

		///<summary></summary>
		public static void WriteObject(Sheet sheet){
			DataObjectFactory<Sheet>.WriteObject(sheet);
		}

		///<summary></summary>
		public static void DeleteObject(int sheetNum){
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
			string command="DELETE FROM sheetfield WHERE SheetNum="+POut.PInt(sheetNum);
			General.NonQ(command);
			DataObjectFactory<Sheet>.DeleteObject(sheetNum);
		}

		//public static void DeleteObject(int sheetDataNum){
		//	DataObjectFactory<sheetData>.DeleteObject(sheetDataNum);
		//}

		///<summary>Converts parameters into sheetfield objects, and then saves those objects in the database.  The parameters will never again enjoy full parameter status, but will just be read-only fields from here on out.  It ignores PatNum parameters, since those are already part of the sheet itself.</summary>
		public static void SaveParameters(Sheet sheet){
			SheetField field;
			for(int i=0;i<sheet.Parameters.Count;i++){
				if(sheet.Parameters[i].ParamName=="PatNum"){
					continue;
				}
				field=new SheetField();
				field.IsNew=true;
				field.SheetNum=sheet.SheetNum;
				field.FieldType=SheetFieldType.Parameter;
				field.FieldName=sheet.Parameters[i].ParamName;
				field.FieldValue=sheet.Parameters[i].ParamValue.ToString();//the object will be an int. Stored as a string.
				field.FontSize=0;
				field.FontName="";
				field.FontIsBold=false;
				field.XPos=0;
				field.YPos=0;
				field.Width=0;
				field.Height=0;
				field.GrowthBehavior=GrowthBehaviorEnum.None;
				SheetFields.WriteObject(field);
			}
		}

		

	}
}