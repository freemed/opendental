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
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT * FROM sheetdef ORDER BY Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="sheetdef";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			SheetDefC.Listt=Crud.SheetDefCrud.TableToList(table);
		}

		///<Summary>Gets one SheetDef from the cache.  Also includes the fields and parameters for the sheetdef.</Summary>
		public static SheetDef GetSheetDef(long sheetDefNum) {
			//No need to check RemotingRole; no call to db.
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
		}

		///<summary>Includes all attached fields.  It simply deletes all the old fields and inserts new ones.</summary>
		public static long WriteObject(SheetDef sheetDef){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				sheetDef.SheetDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),sheetDef);
				return sheetDef.SheetDefNum;
			}
			string command;
			if(!sheetDef.IsNew){
				command="DELETE FROM sheetfielddef WHERE SheetDefNum="+POut.Long(sheetDef.SheetDefNum);
				Db.NonQ(command);
			}
			if(sheetDef.IsNew){
				sheetDef.SheetDefNum=Crud.SheetDefCrud.Insert(sheetDef);
			}
			else{
				Crud.SheetDefCrud.Update(sheetDef);
			}
			foreach(SheetFieldDef field in sheetDef.SheetFieldDefs){
				field.IsNew=true;
				field.SheetDefNum=sheetDef.SheetDefNum;
				SheetFieldDefs.WriteObject(field);
			}
			return sheetDef.SheetDefNum;
		}

		///<summary></summary>
		public static void DeleteObject(long sheetDefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sheetDefNum);
				return;
			}
			//validate that not already in use by a refferral.
			string command="SELECT LName,FName FROM referral WHERE Slip="+POut.Long(sheetDefNum);
			DataTable table=Db.GetTable(command);
			//int count=PIn.PInt(Db.GetCount(command));
			string names="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					names+=", ";
				}
				names+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lans.g("sheetDefs","SheetDef is already in use by referrals(s). Not allowed to delete. ")+names);
			}
			command="DELETE FROM sheetfielddef WHERE SheetDefNum="+POut.Long(sheetDefNum);
			Db.NonQ(command);
			Crud.SheetDefCrud.Delete(sheetDefNum);
		}

		///<summary>Sheetdefs and sheetfielddefs are archived separately.  So when we need to use a sheetdef, we must run this method to pull all the associated fields from the archive.  Then it will be ready for printing, copying, etc.</summary>
		public static void GetFieldsAndParameters(SheetDef sheetdef){
			//No need to check RemotingRole; no call to db.
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
			//No need to check RemotingRole; no call to db.
			List<SheetDef> retVal=new List<SheetDef>();
			for(int i=0;i<SheetDefC.Listt.Count;i++){
				if(SheetDefC.Listt[i].SheetType==sheettype){
					retVal.Add(SheetDefC.Listt[i].Copy());
				}
			}
			return retVal;
		}

		///<summary>Gets the description from the cache.</summary>
		public static string GetDescription(long sheetDefNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<SheetDefC.Listt.Count;i++) {
				if(SheetDefC.Listt[i].SheetDefNum==sheetDefNum) {
					return SheetDefC.Listt[i].Description;
				}
			}
			return "";
		}



	}
}