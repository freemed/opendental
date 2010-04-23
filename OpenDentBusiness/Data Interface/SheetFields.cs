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
		public static SheetField CreateObject(long sheetFieldNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<SheetField>(MethodBase.GetCurrentMethod(),sheetFieldNum);
			}
			return Crud.SheetFieldCrud.SelectOne(sheetFieldNum);
		}

		///<summary>When we need to use a sheet, we must run this method to pull all the associated fields and parameters from the database.  Then it will be ready for printing, copying, etc.</summary>
		public static void GetFieldsAndParameters(Sheet sheet){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sheet);
				return;
			}
			string command="SELECT * FROM sheetfield WHERE SheetNum="+POut.Long(sheet.SheetNum)
				+" ORDER BY SheetFieldNum";//the ordering is CRITICAL because the signature key is based on order.
			sheet.SheetFields=Crud.SheetFieldCrud.SelectMany(command);
			//so parameters will also be in the field list, but they will just be ignored from here on out.
			//because we will have an explicit parameter list instead.
			sheet.Parameters=new List<SheetParameter>();
			SheetParameter param;
			//int paramVal;
			for(int i=0;i<sheet.SheetFields.Count;i++){
				if(sheet.SheetFields[i].FieldType==SheetFieldType.Parameter){
					param=new SheetParameter(true,sheet.SheetFields[i].FieldName,sheet.SheetFields[i].FieldValue);
					sheet.Parameters.Add(param);
				}
			}
		}

		///<summary></summary>
		public static long WriteObject(SheetField sheetField) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				sheetField.SheetFieldNum=Meth.GetLong(MethodBase.GetCurrentMethod(),sheetField);
				return sheetField.SheetFieldNum;
			}
			if(sheetField.IsNew){
				return Crud.SheetFieldCrud.Insert(sheetField);
			}
			else{
				Crud.SheetFieldCrud.Update(sheetField);
				return sheetField.SheetFieldNum;
			}
		}

		///<summary></summary>
		public static void DeleteObject(long sheetFieldNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sheetFieldNum);
				return;
			}
			Crud.SheetFieldCrud.Delete(sheetFieldNum);
		}

		///<summary>Deletes all existing drawing fields for a sheet from the database and then adds back the list supplied.</summary>
		public static void SetDrawings(List<SheetField> drawingList,long sheetNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),drawingList,sheetNum);
				return;
			}
			string command="DELETE FROM sheetfield WHERE SheetNum="+POut.Long(sheetNum)
				+" AND FieldType="+POut.Long((int)SheetFieldType.Drawing);
			Db.NonQ(command);
			foreach(SheetField field in drawingList){
				WriteObject(field);
			}
		}

	



	}
}