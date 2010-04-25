using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Sheets{
		
		///<Summary>Gets one Sheet from the database.</Summary>
		public static Sheet CreateObject(long sheetNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Sheet>(MethodBase.GetCurrentMethod(),sheetNum);
			}
			return Crud.SheetCrud.SelectOne(sheetNum);
		}

		///<summary>Gets a single sheet from the database.  Then, gets all the fields and parameters for it.  So it returns a fully functional sheet.</summary>
		public static Sheet GetSheet(long sheetNum) {
			//No need to check RemotingRole; no call to db.
			Sheet sheet=CreateObject(sheetNum);
			SheetFields.GetFieldsAndParameters(sheet);
			return sheet;
		}

		///<Summary>This is normally done in FormSheetFillEdit, but if we bypass that window for some reason, we can also save a new sheet here.  Does not save any drawings.  Does not save signatures.  Does not save any parameters (PatNum parameters never get saved anyway).</Summary>
		public static void SaveNewSheet(Sheet sheet) {
			//No need to check RemotingRole; no call to db.
			if(!sheet.IsNew) {
				throw new Exception("Only new sheets allowed");
			}
			WriteObject(sheet);
			foreach(SheetField fld in sheet.SheetFields) {
				fld.SheetNum=sheet.SheetNum;
				SheetFields.WriteObject(fld);
			}
		}

		///<summary>Used in FormRefAttachEdit to show all referral slips for the patient/referral combo.  Usually 0 or 1 results.</summary>
		public static List<Sheet> GetReferralSlips(long patNum,long referralNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Sheet>>(MethodBase.GetCurrentMethod(),patNum,referralNum);
			}
			string command="SELECT * FROM sheet WHERE PatNum="+POut.Long(patNum)
				+" AND EXISTS(SELECT * FROM sheetfield "
				+"WHERE sheet.SheetNum=sheetfield.SheetNum "
				+"AND sheetfield.FieldType="+POut.Long((int)SheetFieldType.Parameter)
				+" AND sheetfield.FieldName='ReferralNum' "
				+"AND sheetfield.FieldValue='"+POut.Long(referralNum)+"')"
				+" ORDER BY DateTimeSheet";
			return Crud.SheetCrud.SelectMany(command);
		}

		///<summary>Used in FormRxEdit to view an existing rx.  Will return null if none exist.</summary>
		public static Sheet GetRx(long patNum,long rxNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Sheet>(MethodBase.GetCurrentMethod(),patNum,rxNum);
			}
			string command="SELECT sheet.* FROM sheet,sheetfield "
				+"WHERE sheet.PatNum="+POut.Long(patNum)
				+" AND sheet.SheetType="+POut.Long((int)SheetTypeEnum.Rx)
				+" AND sheetfield.FieldType="+POut.Long((int)SheetFieldType.Parameter)
				+" AND sheetfield.FieldName='RxNum' "
				+"AND sheetfield.FieldValue='"+POut.Long(rxNum)+"'";
			return Crud.SheetCrud.SelectOne(command);
		}

		///<summary>Gets all sheets for a patient that have the terminal flag set.</summary>
		public static List<Sheet> GetForTerminal(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Sheet>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM sheet WHERE PatNum="+POut.Long(patNum)
				+" AND ShowInTerminal > 0 ORDER BY ShowInTerminal";
			return Crud.SheetCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long WriteObject(Sheet sheet) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				sheet.SheetNum=Meth.GetLong(MethodBase.GetCurrentMethod(),sheet);
				return sheet.SheetNum;
			}
			if(sheet.IsNew){
				return Crud.SheetCrud.Insert(sheet);
			}
			else{
				Crud.SheetCrud.Update(sheet);
				return sheet.SheetNum;
			}
		}

		///<summary></summary>
		public static void DeleteObject(long sheetNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sheetNum);
				return;
			}
			string command="DELETE FROM sheetfield WHERE SheetNum="+POut.Long(sheetNum);
			Db.NonQ(command);
			Crud.SheetCrud.Delete(sheetNum);
		}

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
				field.RadioButtonValue="";
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

		public static DataTable GetPatientFormsTable(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),patNum);
			}
			//DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("date");
			table.Columns.Add("dateOnly",typeof(DateTime));//to help with sorting
			table.Columns.Add("dateTime",typeof(DateTime));
			table.Columns.Add("description");
			table.Columns.Add("DocNum");
			table.Columns.Add("imageCat");
			table.Columns.Add("SheetNum");
			table.Columns.Add("showInTerminal");
			table.Columns.Add("time");
			table.Columns.Add("timeOnly",typeof(TimeSpan));//to help with sorting
			//but we won't actually fill this table with rows until the very end.  It's more useful to use a List<> for now.
			List<DataRow> rows=new List<DataRow>();
			//sheet---------------------------------------------------------------------------------------
			string command="SELECT DateTimeSheet,SheetNum,Description,ShowInTerminal "
				+"FROM sheet WHERE PatNum ="+POut.Long(patNum)+" "
				+"AND (SheetType="+POut.Long((int)SheetTypeEnum.PatientForm)+" OR SheetType="+POut.Long((int)SheetTypeEnum.MedicalHistory)+") ";
				//+"ORDER BY ShowInTerminal";//DATE(DateTimeSheet),ShowInTerminal,TIME(DateTimeSheet)";
			DataTable rawSheet=Db.GetTable(command);
			DateTime dateT;
			for(int i=0;i<rawSheet.Rows.Count;i++) {
				row=table.NewRow();
				dateT=PIn.DateT(rawSheet.Rows[i]["DateTimeSheet"].ToString());
				row["date"]=dateT.ToShortDateString();
				row["dateOnly"]=dateT.Date;
				row["dateTime"]=dateT;
				row["description"]=rawSheet.Rows[i]["Description"].ToString();
				row["DocNum"]="0";
				row["imageCat"]="";
				row["SheetNum"]=rawSheet.Rows[i]["SheetNum"].ToString();
				if(rawSheet.Rows[i]["ShowInTerminal"].ToString()=="0") {
					row["showInTerminal"]="";
				}
				else {
					row["showInTerminal"]=rawSheet.Rows[i]["ShowInTerminal"].ToString();
				}
				if(dateT.TimeOfDay!=TimeSpan.Zero) {
					row["time"]=dateT.ToString("h:mm")+dateT.ToString("%t").ToLower();
				}
				row["timeOnly"]=dateT.TimeOfDay;
				rows.Add(row);
			}
			//document---------------------------------------------------------------------------------------
			command="SELECT DateCreated,DocCategory,DocNum,Description "
				+"FROM document,definition "
				+"WHERE document.DocCategory=definition.DefNum"
				+" AND PatNum ="+POut.Long(patNum)
				+" AND definition.ItemValue LIKE '%F%'";
				//+" ORDER BY DateCreated";
			DataTable rawDoc=Db.GetTable(command);
			long docCat;
			for(int i=0;i<rawDoc.Rows.Count;i++) {
				row=table.NewRow();
				dateT=PIn.DateT(rawDoc.Rows[i]["DateCreated"].ToString());
				row["date"]=dateT.ToShortDateString();
				row["dateOnly"]=dateT.Date;
				row["dateTime"]=dateT;
				row["description"]=rawDoc.Rows[i]["Description"].ToString();
				row["DocNum"]=rawDoc.Rows[i]["DocNum"].ToString();
				docCat=PIn.Long(rawDoc.Rows[i]["DocCategory"].ToString());
				row["imageCat"]=DefC.GetName(DefCat.ImageCats,docCat);
				row["SheetNum"]="0";
				row["showInTerminal"]="";
				if(dateT.TimeOfDay!=TimeSpan.Zero) {
					row["time"]=dateT.ToString("h:mm")+dateT.ToString("%t").ToLower();
				}
				row["timeOnly"]=dateT.TimeOfDay;
				rows.Add(row);
			}
			//Sorting
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			DataView view = table.DefaultView;
			view.Sort = "dateOnly,showInTerminal,timeOnly";
			table = view.ToTable();
			return table;
		}

		public static bool ContainsStaticField(Sheet sheet,string fieldName) {
			//No need to check RemotingRole; no call to db
			foreach(SheetField field in sheet.SheetFields) {
				if(field.FieldType!=SheetFieldType.StaticText) {
					continue;
				}
				if(field.FieldValue.Contains("["+fieldName+"]")) {
					return true;
				}
			}
			return false;
		}

		///<summary></summary>
		public static byte GetBiggestShowInTerminal(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<byte>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT MAX(ShowInTerminal) FROM sheet WHERE PatNum="+POut.Long(patNum);
			return PIn.Byte(Db.GetScalar(command));
		}

		///<summary></summary>
		public static void ClearFromTerminal(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			string command="UPDATE sheet SET ShowInTerminal=0 WHERE PatNum="+POut.Long(patNum);
			Db.NonQ(command);
		}
		

		

	}
}