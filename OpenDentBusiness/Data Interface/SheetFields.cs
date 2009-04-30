using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SheetFields{

		///<Summary>Gets one SheetField from the database.</Summary>
		public static SheetField CreateObject(int sheetFieldNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<SheetField>(MethodBase.GetCurrentMethod(),sheetFieldNum);
			}
			return DataObjectFactory<SheetField>.CreateObject(sheetFieldNum);
		}

		///<summary>When we need to use a sheet, we must run this method to pull all the associated fields and parameters from the database.  Then it will be ready for printing, copying, etc.</summary>
		public static void GetFieldsAndParameters(Sheet sheet){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sheet);
				return;
			}
			string command="SELECT * FROM sheetfield WHERE SheetNum="+POut.PInt(sheet.SheetNum)
				+" ORDER BY SheetFieldNum";//the ordering is CRITICAL because the signature key is based on order.
			sheet.SheetFields=new List<SheetField>(DataObjectFactory<SheetField>.CreateObjects(command));
			//so parameters will also be in the field list, but they will just be ignored from here on out.
			//because we will have an explicit parameter list instead.
			sheet.Parameters=new List<SheetParameter>();
			SheetParameter param;
			int paramVal;
			for(int i=0;i<sheet.SheetFields.Count;i++){
				if(sheet.SheetFields[i].FieldType==SheetFieldType.Parameter){
					param=new SheetParameter(true,sheet.SheetFields[i].FieldName,sheet.SheetFields[i].FieldValue);
					sheet.Parameters.Add(param);
				}
			}
		}

		///<summary></summary>
		public static void WriteObject(SheetField sheetField){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sheetField);
				return;
			}
			DataObjectFactory<SheetField>.WriteObject(sheetField);
		}

		///<summary></summary>
		public static void DeleteObject(int sheetFieldNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sheetFieldNum);
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
				throw new ApplicationException(Lan.g("sheetDatas","sheetData is already in use by patient(s). Not allowed to delete. "+pats));
			}*/
			DataObjectFactory<SheetField>.DeleteObject(sheetFieldNum);
		}

		///<summary>Deletes all existing drawing fields for a sheet from the database and then adds back the list supplied.</summary>
		public static void SetDrawings(List<SheetField> drawingList,int sheetNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),drawingList,sheetNum);
				return;
			}
			string command="DELETE FROM sheetfield WHERE SheetNum="+POut.PInt(sheetNum)
				+" AND FieldType="+POut.PInt((int)SheetFieldType.Drawing);
			Db.NonQ(command);
			foreach(SheetField field in drawingList){
				WriteObject(field);
			}
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