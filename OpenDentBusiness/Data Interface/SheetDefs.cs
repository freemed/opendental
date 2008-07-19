using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SheetDefs{
		///<summary></summary>
		public static DataTable RefreshCache(){
			string c="SELECT * FROM sheetdef ORDER BY Description";
			DataTable table=General.GetTable(c);
			table.TableName="sheetdef";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			SheetDefC.Listt=new List<SheetDef>();
			SheetDef sheetdef;
			for(int i=0;i<table.Rows.Count;i++){
				sheetdef=new SheetDef();
				sheetdef.IsNew=false;
				sheetdef.SheetDefNum = PIn.PInt   (table.Rows[i][0].ToString());
				sheetdef.Description = PIn.PString(table.Rows[i][1].ToString());
				sheetdef.SheetType   = (SheetTypeEnum)PIn.PInt(table.Rows[i][2].ToString());
				sheetdef.FontSize    = PIn.PFloat (table.Rows[i][3].ToString());
				sheetdef.FontName    = PIn.PString(table.Rows[i][4].ToString());
				sheetdef.Width       = PIn.PInt   (table.Rows[i][5].ToString());
				sheetdef.Height      = PIn.PInt   (table.Rows[i][6].ToString());
				sheetdef.IsLandscape = PIn.PBool  (table.Rows[i][7].ToString());
				SheetDefC.Listt.Add(sheetdef);
			}
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

		///<summary>Includes all attached fields.  It simply deletes all the old fields and inserts new ones.</summary>
		public static void WriteObject(SheetDef sheetDef){
			string command;
			if(sheetDef.SheetDefNum!=0){
				command="DELETE FROM sheetfielddef WHERE SheetDefNum="+POut.PInt(sheetDef.SheetDefNum);
				General.NonQ(command);
			}
			DataObjectFactory<SheetDef>.WriteObject(sheetDef);
			foreach(SheetFieldDef field in sheetDef.SheetFieldDefs){
				field.IsNew=true;
				field.SheetDefNum=sheetDef.SheetDefNum;
				SheetFieldDefs.WriteObject(field);
			}
		}

		///<summary></summary>
		public static void DeleteObject(int sheetDefNum){
			//validate that not already in use by a refferral, etc.
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

		///<summary>Sheetdefs and sheetfielddefs are archived separately.  So when we need to use a sheetdef, we must run this method to pull all the associated fields from the archive.  Then it will be ready for printing, copying, etc.</summary>
		public static void GetFieldsAndParameters(SheetDef sheetdef){
			sheetdef.Parameters=new List<SheetParameter>();
			sheetdef.SheetFieldDefs=new List<SheetFieldDef>();
			for(int i=0;i<SheetFieldDefC.Listt.Count;i++){
				if(SheetFieldDefC.Listt[i].SheetDefNum!=sheetdef.SheetDefNum){
					continue;
				}
				if(SheetFieldDefC.Listt[i].FieldType==SheetFieldType.Parameter){
					//actually, let's worry about this later.
					//sheetdef.Parameters.Add(new SheetParameter(
					//	SheetFieldDefC.Listt[i].Copy());
				}
				else{
					sheetdef.SheetFieldDefs.Add(SheetFieldDefC.Listt[i].Copy());
				}
			}
		}

	}
}