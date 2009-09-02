using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SheetFieldDefs{
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT * FROM sheetfielddef ORDER BY SheetDefNum";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="sheetfielddef";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			SheetFieldDefC.Listt=new List<SheetFieldDef>();
			SheetFieldDef sfd;
			for(int i=0;i<table.Rows.Count;i++){
				sfd=new SheetFieldDef();
				sfd.IsNew=false;
				sfd.SheetFieldDefNum= PIn.PInt   (table.Rows[i][0].ToString());
				sfd.SheetDefNum     = PIn.PInt   (table.Rows[i][1].ToString());
				sfd.FieldType       = (SheetFieldType)PIn.PInt(table.Rows[i][2].ToString());
				sfd.FieldName       = PIn.PString(table.Rows[i][3].ToString());
				sfd.FieldValue      = PIn.PString(table.Rows[i][4].ToString());
				sfd.FontSize        = PIn.PFloat (table.Rows[i][5].ToString());
				sfd.FontName        = PIn.PString(table.Rows[i][6].ToString());
				sfd.FontIsBold      = PIn.PBool  (table.Rows[i][7].ToString());
				sfd.XPos            = PIn.PInt32   (table.Rows[i][8].ToString());
				sfd.YPos            = PIn.PInt32   (table.Rows[i][9].ToString());
				sfd.Width           = PIn.PInt32   (table.Rows[i][10].ToString());
				sfd.Height          = PIn.PInt32   (table.Rows[i][11].ToString());
				sfd.GrowthBehavior  = (GrowthBehaviorEnum)PIn.PInt(table.Rows[i][12].ToString());
				SheetFieldDefC.Listt.Add(sfd);
			}
		}

		///<Summary>Gets one SheetFieldDef from the database.</Summary>
		public static SheetFieldDef CreateObject(long sheetFieldDefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<SheetFieldDef>(MethodBase.GetCurrentMethod(),sheetFieldDefNum);
			}
			return DataObjectFactory<SheetFieldDef>.CreateObject(sheetFieldDefNum);
		}

		/*Better to get the data from cache
		///<summary>Gets all fieldDefs for one sheetDef.</summary>
		public static List<SheetFieldDef> GetForSheetDef(int sheetDefNum){
			string command="SELECT * FROM sheetfielddef WHERE SheetDefNum="+POut.PInt(sheetDefNum);
			return new List<SheetFieldDef>(DataObjectFactory<SheetFieldDef>.CreateObjects(command));
		}*/

		///<summary></summary>
		public static long WriteObject(SheetFieldDef sheetFieldDef) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				sheetFieldDef.SheetFieldDefNum=Meth.GetInt(MethodBase.GetCurrentMethod(),sheetFieldDef);
				return sheetFieldDef.SheetFieldDefNum;
			}
			DataObjectFactory<SheetFieldDef>.WriteObject(sheetFieldDef);
			return sheetFieldDef.SheetFieldDefNum;
		}

		///<summary></summary>
		public static void DeleteObject(long sheetFieldDefNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sheetFieldDefNum);
				return;
			}
			//validate that not already in use.
			/*string command="SELECT LName,FName FROM patient WHERE sheetDataNum="+POut.PInt(sheetDataNum);
			DataTable table=Db.GetTable(command);
			//int count=PIn.PInt(Db.GetCount(command));
			string pats="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					pats+=", ";
				}
				pats+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lans.g("sheetDatas","sheetData is already in use by patient(s). Not allowed to delete. "+pats));
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