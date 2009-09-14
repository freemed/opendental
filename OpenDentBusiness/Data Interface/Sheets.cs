using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Text;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Sheets{
		/*
		//<summary></summary>
		public static DataTable RefreshCache(){
			string c="SELECT * from sheetData ORDER BY Description";
			DataTable table=Meth.GetTable(MethodInfo.GetCurrentMethod(),c);
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
		public static Sheet CreateObject(long sheetNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Sheet>(MethodBase.GetCurrentMethod(),sheetNum);
			}
			return DataObjectFactory<Sheet>.CreateObject(sheetNum);
		}

		///<summary>Gets a single sheet from the database.  Then, gets all the fields and parameters for it.  So it returns a fully functional sheet.</summary>
		public static Sheet GetSheet(long sheetNum) {
			//No need to check RemotingRole; no call to db.
			Sheet sheet=CreateObject(sheetNum);
			SheetFields.GetFieldsAndParameters(sheet);
			return sheet;
		}

		///<summary>Used in FormRefAttachEdit to show all referral slips for the patient/referral combo.  Usually 0 or 1 results.</summary>
		public static List<Sheet> GetReferralSlips(long patNum,long referralNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Sheet>>(MethodBase.GetCurrentMethod(),patNum,referralNum);
			}
			string command="SELECT * FROM sheet WHERE PatNum="+POut.PLong(patNum)
				+" AND EXISTS(SELECT * FROM sheetfield "
				+"WHERE sheet.SheetNum=sheetfield.SheetNum "
				+"AND sheetfield.FieldType="+POut.PLong((int)SheetFieldType.Parameter)
				+" AND sheetfield.FieldName='ReferralNum' "
				+"AND sheetfield.FieldValue='"+POut.PLong(referralNum)+"')"
				+" ORDER BY DateTimeSheet";
			return new List<Sheet>(DataObjectFactory<Sheet>.CreateObjects(command));
			//Collection<sheetData> collectState=DataObjectFactory<sheetData>.CreateObjects(sheetDataNums);
			//return new List<sheetData>(collectState);		
			//return list;
		}

		///<summary>Used in FormRxEdit to view an existing rx.  Will return null if none exist.</summary>
		public static Sheet GetRx(long patNum,long rxNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Sheet>(MethodBase.GetCurrentMethod(),patNum,rxNum);
			}
			string command="SELECT sheet.* FROM sheet,sheetfield "
				+"WHERE sheet.PatNum="+POut.PLong(patNum)
				+" AND sheet.SheetType="+POut.PLong((int)SheetTypeEnum.Rx)
				+" AND sheetfield.FieldType="+POut.PLong((int)SheetFieldType.Parameter)
				+" AND sheetfield.FieldName='RxNum' "
				+"AND sheetfield.FieldValue='"+POut.PLong(rxNum)+"'";
			List<Sheet> sheetlist=new List<Sheet>(DataObjectFactory<Sheet>.CreateObjects(command));
			if(sheetlist.Count==0){
				return null;
			}
			return sheetlist[0];
		}

		///<summary></summary>
		public static long WriteObject(Sheet sheet) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				sheet.SheetNum=Meth.GetInt(MethodBase.GetCurrentMethod(),sheet);
				return sheet.SheetNum;
			}
			DataObjectFactory<Sheet>.WriteObject(sheet);
			return sheet.SheetNum;
		}

		///<summary></summary>
		public static void DeleteObject(long sheetNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sheetNum);
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
			string command="DELETE FROM sheetfield WHERE SheetNum="+POut.PLong(sheetNum);
			Db.NonQ(command);
			DataObjectFactory<Sheet>.DeleteObject(sheetNum);
		}

		//public static void DeleteObject(int sheetDataNum){
		//	DataObjectFactory<sheetData>.DeleteObject(sheetDataNum);
		//}

		///<summary>Converts parameters into sheetfield objects, and then saves those objects in the database.  The parameters will never again enjoy full parameter status, but will just be read-only fields from here on out.  It ignores PatNum parameters, since those are already part of the sheet itself.</summary>
		public static void SaveParameters(Sheet sheet){
			//No need to check RemotingRole; no call to db
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

		///<summary>Loops through all the fields in the sheet and appends together all the FieldValues.  It obviously excludes all SigBox fieldtypes.  It does include Drawing fieldtypes, so any change at all to any drawing will invalidate the signature.  It does include Image fieldtypes, although that's just a filename and does not really have any meaningful data about the image itself.  The order is absolutely critical.</summary>
		public static string GetSignatureKey(Sheet sheet){
			//No need to check RemotingRole; no call to db
			StringBuilder strBuild=new StringBuilder();
			for(int i=0;i<sheet.SheetFields.Count;i++){
				if(sheet.SheetFields[i].FieldValue==""){
					continue;
				}
				if(sheet.SheetFields[i].FieldType==SheetFieldType.SigBox){
					continue;
				}
				strBuild.Append(sheet.SheetFields[i].FieldValue);
			}
			return strBuild.ToString();
		}

		

	}
}