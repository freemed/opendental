using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SheetDefs{
		///<summary></summary>
		public static DataTable RefreshCache() {
			string c="SELECT * FROM sheetdef ORDER BY Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
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

		///<Summary>Gets one SheetDef from the cache.  Also includes the fields and parameters for the sheetdef.</Summary>
		public static SheetDef GetSheetDef(int sheetDefNum){
			SheetDef sheetdef=null;
			for(int i=0;i<SheetDefC.Listt.Count;i++){
				if(SheetDefC.Listt[i].SheetDefNum==sheetDefNum){
					sheetdef=SheetDefC.Listt[i].Copy();
					break;
				}
			}
			//if sheetdef is null, it will crash, just as it should.
			GetFieldsAndParameters(sheetdef);
			return sheetdef;
			//return DataObjectFactory<SheetDef>.CreateObject(sheetDefNum);
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
			if(!sheetDef.IsNew){
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
			//validate that not already in use by a refferral.
			string command="SELECT LName,FName FROM referral WHERE Slip="+POut.PInt(sheetDefNum);
			DataTable table=General.GetTable(command);
			//int count=PIn.PInt(General.GetCount(command));
			string names="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					names+=", ";
				}
				names+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lan.g("sheetDefs","SheetDef is already in use by referrals(s). Not allowed to delete. ")+names);
			}
			command="DELETE FROM sheetfielddef WHERE SheetDefNum="+POut.PInt(sheetDefNum);
			General.NonQ(command);
			DataObjectFactory<SheetDef>.DeleteObject(sheetDefNum);
		}

		//public static void DeleteObject(int sheetDataNum){
		//	DataObjectFactory<sheetData>.DeleteObject(sheetDataNum);
		//}

		///<summary>Sheetdefs and sheetfielddefs are archived separately.  So when we need to use a sheetdef, we must run this method to pull all the associated fields from the archive.  Then it will be ready for printing, copying, etc.</summary>
		public static void GetFieldsAndParameters(SheetDef sheetdef){
			sheetdef.SheetFieldDefs=new List<SheetFieldDef>();
			sheetdef.Parameters=SheetParameter.GetForType(sheetdef.SheetType);
			//images first
			for(int i=0;i<SheetFieldDefC.Listt.Count;i++){
				if(SheetFieldDefC.Listt[i].SheetDefNum!=sheetdef.SheetDefNum){
					continue;
				}
				if(SheetFieldDefC.Listt[i].FieldType!=SheetFieldType.Image){
					continue;
				}
				sheetdef.SheetFieldDefs.Add(SheetFieldDefC.Listt[i].Copy());
			}
			//then all other fields
			for(int i=0;i<SheetFieldDefC.Listt.Count;i++){
				if(SheetFieldDefC.Listt[i].SheetDefNum!=sheetdef.SheetDefNum){
					continue;
				}
				if(SheetFieldDefC.Listt[i].FieldType==SheetFieldType.Image){
					continue;
				}
				if(SheetFieldDefC.Listt[i].FieldType==SheetFieldType.Parameter){
					continue;
					//sheetfielddefs never store parameters.
					//sheetfields do store filled parameters, but that's different.
				}
				//else{
				sheetdef.SheetFieldDefs.Add(SheetFieldDefC.Listt[i].Copy());
				//}
			}
		}

		///<summary>Gets all custom sheetdefs(without fields or parameters) for a particular type.</summary>
		public static List<SheetDef> GetCustomForType(SheetTypeEnum sheettype){
			List<SheetDef> retVal=new List<SheetDef>();
			for(int i=0;i<SheetDefC.Listt.Count;i++){
				if(SheetDefC.Listt[i].SheetType==sheettype){
					retVal.Add(SheetDefC.Listt[i].Copy());
				}
			}
			return retVal;
		}



	}
}