using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SheetFieldDefs{
		///<summary></summary>
		public static DataTable RefreshCache(){
			string c="SELECT * FROM sheetfielddef ORDER BY SheetDefNum";
			DataTable table=General.GetTable(c);
			table.TableName="sheetfielddef";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
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
				sfd.XPos            = PIn.PInt   (table.Rows[i][8].ToString());
				sfd.YPos            = PIn.PInt   (table.Rows[i][9].ToString());
				sfd.Width           = PIn.PInt   (table.Rows[i][10].ToString());
				sfd.Height          = PIn.PInt   (table.Rows[i][11].ToString());
				sfd.GrowthBehavior  = (GrowthBehaviorEnum)PIn.PInt(table.Rows[i][12].ToString());
				SheetFieldDefC.Listt.Add(sfd);
			}
		}

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