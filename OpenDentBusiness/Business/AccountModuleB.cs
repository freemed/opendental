using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Drawing;

namespace OpenDentBusiness {
	public class AccountModuleB {
		private static DataSet retVal;

		///<summary>Parameters: 0:patNum, 1:viewingInRecall, 2:fromDate, 3:toDate</summary>
		public static DataSet GetAll(string[] parameters){
			int patNum=PIn.PInt(parameters[0]);
			bool viewingInRecall=PIn.PBool(parameters[1]);
			DateTime fromDate=PIn.PDate(parameters[2]);
			DateTime toDate=PIn.PDate(parameters[3]);
			retVal=new DataSet();
			if(viewingInRecall) {
				retVal.Tables.Add(ChartModuleB.GetProgNotes(patNum, false));
			}
			else {
				GetCommLog(patNum);
			}
			GetAccount(patNum,fromDate,toDate);//Gets 2 tables: account(or account###,account###,etc) and patient
			return retVal;
		}

		private static void GetCommLog(int patNum) {
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("Commlog");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("CommDateTime",typeof(DateTime));
			table.Columns.Add("commDate");
			table.Columns.Add("commTime");
			table.Columns.Add("CommlogNum");
			table.Columns.Add("commType");
			table.Columns.Add("EmailMessageNum");
			table.Columns.Add("FormPatNum");
			table.Columns.Add("mode");
			table.Columns.Add("Note");
			table.Columns.Add("patName");
			//table.Columns.Add("sentOrReceived");			
			//table.Columns.Add("");
			//but we won't actually fill this table with rows until the very end.  It's more useful to use a List<> for now.
			List<DataRow> rows=new List<DataRow>();
			//Commlog------------------------------------------------------------------------------------------
			string command="SELECT CommDateTime,CommType,Mode_,SentOrReceived,Note,CommlogNum,IsStatementSent,p1.FName,commlog.PatNum "
				+"FROM commlog,patient p1,patient p2 "
				+"WHERE commlog.PatNum=p1.PatNum "
				+"AND p1.Guarantor=p2.Guarantor "
				+"AND p2.PatNum ="+POut.PInt(patNum)+" ORDER BY CommDateTime";
			DataTable rawComm=dcon.GetTable(command);
			DateTime dateT;
			for(int i=0;i<rawComm.Rows.Count;i++){
				if(rawComm.Rows[i]["IsStatementSent"].ToString()=="1"){
					continue;
				}
				row=table.NewRow();
				dateT=PIn.PDateT(rawComm.Rows[i]["CommDateTime"].ToString());
				row["CommDateTime"]=dateT;
				row["commDate"]=dateT.ToShortDateString();
				if(dateT.TimeOfDay!=TimeSpan.Zero) {
					row["commTime"]=dateT.ToString("h:mm")+dateT.ToString("%t").ToLower();
				}
				row["CommlogNum"]=rawComm.Rows[i]["CommlogNum"].ToString();
				row["commType"]=DefB.GetName(DefCat.CommLogTypes,PIn.PInt(rawComm.Rows[i]["CommType"].ToString()));
				row["EmailMessageNum"]="0";
				row["FormPatNum"]="0";
				if(rawComm.Rows[i]["Mode_"].ToString()!="0"){//anything except none
					row["mode"]=Lan.g("enumCommItemMode",((CommItemMode)PIn.PInt(rawComm.Rows[i]["Mode_"].ToString())).ToString());
				}
				row["Note"]=rawComm.Rows[i]["Note"].ToString();
				if(rawComm.Rows[i]["PatNum"].ToString()!=patNum.ToString()){
					row["patName"]=rawComm.Rows[i]["FName"].ToString();
				}
				//row["sentOrReceived"]=Lan.g("enumCommSentOrReceived",
				//	((CommSentOrReceived)PIn.PInt(rawComm.Rows[i]["SentOrReceived"].ToString())).ToString());
				rows.Add(row);
			}
			//emailmessage---------------------------------------------------------------------------------------
			command="SELECT MsgDateTime,SentOrReceived,Subject,EmailMessageNum "
				+"FROM emailmessage WHERE PatNum ='"+POut.PInt(patNum)+"' ORDER BY MsgDateTime";
			DataTable rawEmail=dcon.GetTable(command);
			string txt;
			for(int i=0;i<rawEmail.Rows.Count;i++) {
				row=table.NewRow();
				dateT=PIn.PDateT(rawEmail.Rows[i]["MsgDateTime"].ToString());
				row["CommDateTime"]=dateT;
				row["commDate"]=dateT.ToShortDateString();
				if(dateT.TimeOfDay!=TimeSpan.Zero){
					row["commTime"]=dateT.ToString("h:mm")+dateT.ToString("%t").ToLower();
				}
				row["CommlogNum"]="0";
				//type
				row["EmailMessageNum"]=rawEmail.Rows[i]["EmailMessageNum"].ToString();
				row["FormPatNum"]="0";
				row["mode"]=Lan.g("enumCommItemMode",CommItemMode.Email.ToString());
				txt="";
				if(rawEmail.Rows[i]["SentOrReceived"].ToString()=="0") {
					txt="("+Lan.g("AccountModule","Unsent")+") ";
				}
				row["Note"]=txt+rawEmail.Rows[i]["Subject"].ToString();
				//if(rawEmail.Rows[i]["SentOrReceived"].ToString()=="0") {
				//	row["sentOrReceived"]=Lan.g("AccountModule","Unsent");
				//}
				//else {
				//	row["sentOrReceived"]=Lan.g("enumCommSentOrReceived",
				//		((CommSentOrReceived)PIn.PInt(rawEmail.Rows[i]["SentOrReceived"].ToString())).ToString());
				//}
				rows.Add(row);
			}
			//formpat---------------------------------------------------------------------------------------
			command="SELECT FormDateTime,FormPatNum "
				+"FROM formpat WHERE PatNum ='"+POut.PInt(patNum)+"' ORDER BY FormDateTime";
			DataTable rawForm=dcon.GetTable(command);
			for(int i=0;i<rawForm.Rows.Count;i++) {
				row=table.NewRow();
				dateT=PIn.PDateT(rawForm.Rows[i]["FormDateTime"].ToString());
				row["CommDateTime"]=dateT;
				row["commDate"]=dateT.ToShortDateString();
				if(dateT.TimeOfDay!=TimeSpan.Zero) {
					row["commTime"]=dateT.ToString("h:mm")+dateT.ToString("%t").ToLower();
				}
				row["CommlogNum"]="0";
				row["commType"]=Lan.g("AccountModule","Questionnaire");
				row["EmailMessageNum"]="0";
				row["FormPatNum"]=rawForm.Rows[i]["FormPatNum"].ToString();
				row["mode"]="";
				row["Note"]="";
				//row["sentOrReceived"]="";
				rows.Add(row);
			}
			//Sorting
			//rows.Sort(CompareCommRows);
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			DataView view = table.DefaultView;
			view.Sort = "CommDateTime";
			table = view.ToTable();
			//return table;
			retVal.Tables.Add(table);
		}

		///<summary>Also gets the patient table, which has one row for each family member.</summary>
		private static void GetAccount(int patNum,DateTime fromDate,DateTime toDate) {
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("account");
			Family fam=Patients.GetFamily(patNum);
			Patient pat=fam.GetPatient(patNum);
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("AdjNum");
			table.Columns.Add("balance");
			table.Columns.Add("balanceDouble",typeof(double));
			table.Columns.Add("charges");
			table.Columns.Add("chargesDouble",typeof(double));
			table.Columns.Add("ClaimNum");
			table.Columns.Add("ClaimPaymentNum");//if this is set, also set ClaimNum
			table.Columns.Add("colorText");
			table.Columns.Add("credits");
			table.Columns.Add("creditsDouble",typeof(double));
			table.Columns.Add("date");
			table.Columns.Add("DateTime",typeof(DateTime));
			table.Columns.Add("description");
			table.Columns.Add("patient");
			table.Columns.Add("PatNum");
			table.Columns.Add("PayNum");//even though we only show split objects
			table.Columns.Add("ProcCode");
			table.Columns.Add("ProcNum");
			table.Columns.Add("prov");
			table.Columns.Add("tth");
			//but we won't actually fill this table with rows until the very end.  It's more useful to use a List<> for now.
			List<DataRow> rows=new List<DataRow>();
			//Procedures------------------------------------------------------------------------------------------
			string command="SELECT procedurelog.BaseUnits,Descript,LaymanTerm,procedurelog.PatNum,ProcCode,"
				+"ProcDate,ProcFee,ProcNum,ProvNum,ToothNum,UnitQty "
				+"FROM procedurelog "
				+"LEFT JOIN procedurecode ON procedurelog.CodeNum=procedurecode.CodeNum "
				+"WHERE ProcStatus=2 "//complete
				+"AND (";
			for(int i=0;i<fam.List.Length;i++){
				if(i!=0){
					command+="OR ";
				}
				command+="procedurelog.PatNum ="+POut.PInt(fam.List[i].PatNum)+" ";
			}
			command+=") ORDER BY ProcDate";
			DataTable rawProc=dcon.GetTable(command);
			DateTime dateT;
			double qty;
			for(int i=0;i<rawProc.Rows.Count;i++){
				row=table.NewRow();
				row["AdjNum"]="0";
				row["balance"]="";//fill this later
				row["balanceDouble"]=0;//fill this later
				qty=PIn.PInt(rawProc.Rows[i]["UnitQty"].ToString()) + PIn.PInt(rawProc.Rows[i]["BaseUnits"].ToString());
				if(qty==0){
					qty=1;
				}
				row["chargesDouble"]=PIn.PDouble(rawProc.Rows[i]["ProcFee"].ToString())*qty;
				row["charges"]=((double)row["chargesDouble"]).ToString("f");
				row["ClaimNum"]="0";
				row["ClaimPaymentNum"]="0";
				row["colorText"]=DefB.Long[(int)DefCat.AccountColors][0].ItemColor.ToArgb().ToString();
				row["creditsDouble"]="0";
				row["credits"]="";
				dateT=PIn.PDateT(rawProc.Rows[i]["ProcDate"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
				row["description"]=rawProc.Rows[i]["Descript"].ToString();
				if(rawProc.Rows[i]["LaymanTerm"].ToString()!=""){
					row["description"]=rawProc.Rows[i]["LaymanTerm"].ToString();
				}
				row["patient"]=pat.GetNameFirst();
				row["PatNum"]=rawProc.Rows[i]["PatNum"].ToString();
				row["PayNum"]="0";
				row["ProcCode"]=rawProc.Rows[i]["ProcCode"].ToString();
				row["ProcNum"]=PIn.PInt(rawProc.Rows[i]["ProcNum"].ToString());
				row["prov"]=Providers.GetAbbr(PIn.PInt(rawProc.Rows[i]["ProvNum"].ToString()));
				row["tth"]=Tooth.ToInternat(rawProc.Rows[i]["ToothNum"].ToString());
				rows.Add(row);
			}
			//Other table types here--------------------------------------------------------------------------------

			//Sorting
			rows.Sort(new AccountLineComparer());
			//rows.Sort(CompareCommRows);
			//Pass off all the rows for the whole family in order to compute the patient balances----------------
			GetPatientTable(fam,rows);
			//Filter out patients that we are not interested in--------------------------------------------------
			if(patNum!=0){//if it is 0, then we will be showing all patients with no filtering
				for(int i=rows.Count-1;i>=0;i--) {//go backwards and remove from end
					if(rows[i]["PatNum"].ToString()!=patNum.ToString()){
						rows.RemoveAt(i);
					}
				}
			}
			//Compute balances-------------------------------------------------------------------------------------
			double bal=0;
			for(int i=0;i<rows.Count;i++) {
				bal+=(double)rows[i]["chargesDouble"];
				bal-=(double)rows[i]["creditsDouble"];
				rows[i]["balanceDouble"]=bal;
				rows[i]["balance"]=bal.ToString("f");
			}
			//Remove rows outside of daterange-------------------------------------------------------------------
			double balanceForward=0;
			bool foundBalForward=false;
			for(int i=rows.Count-1;i>=0;i--) {//go backwards and remove from end
				if(((DateTime)rows[i]["DateTime"])>toDate){
					rows.RemoveAt(i);
				}
				if(((DateTime)rows[i]["DateTime"])<fromDate){
					if(!foundBalForward){
						foundBalForward=true;
						balanceForward=(double)rows[i]["balanceDouble"];
					}
					rows.RemoveAt(i);
				}
			}
			//Add balance forward row-------------------------------------------------------------------------
			if(foundBalForward){
				//add a balance forward row
				row=table.NewRow();
				row["AdjNum"]="0";
				row["balance"]=balanceForward.ToString("f");
				row["balanceDouble"]=balanceForward;
				row["chargesDouble"]=0;
				row["charges"]="";
				row["ClaimNum"]="0";
				row["ClaimPaymentNum"]="0";
				row["colorText"]=Color.Black.ToArgb().ToString();
				row["creditsDouble"]="0";
				row["credits"]="";
				row["DateTime"]=DateTime.MinValue;
				row["date"]="";
				row["description"]=Lan.g("AccountModule","Balance Forward");
				row["patient"]="";
				row["PayNum"]="0";
				row["ProcCode"]="";
				row["ProcNum"]="0";
				row["prov"]="";
				row["tth"]="";
				rows.Insert(0,row);
			}
			//Finally, add rows to new table-----------------------------------------------------------------------
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			//DataView view = table.DefaultView;
			//view.Sort = "DateTime";
			//table = view.ToTable();
			//return table;
			retVal.Tables.Add(table);
		}

		///<summary>All rows for the entire family are getting passed in here.  They have already been sorted.  Balances have not been computed, and we will do that here, separately for each patient.</summary>
		private static void GetPatientTable(Family fam,List<DataRow> rows){
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("patient");
			DataRow row;
			table.Columns.Add("balance");
			table.Columns.Add("balanceDouble",typeof(double));
			table.Columns.Add("name");
			table.Columns.Add("PatNum");
			List<DataRow> rowspat=new List<DataRow>();
			double bal;
			for(int p=0;p<fam.List.Length;p++){
				row=table.NewRow();
				bal=0;
				for(int i=0;i<rows.Count;i++) {
					if(fam.List[p].PatNum.ToString()==rows[i]["PatNum"].ToString()){
						bal+=(double)rows[i]["chargesDouble"];
						bal-=(double)rows[i]["creditsDouble"];
					}
				}
				row["balanceDouble"]=bal;
				row["balance"]=bal.ToString("f");
				row["name"]=fam.List[p].GetNameLF();
				row["PatNum"]=fam.List[p].PatNum.ToString();
				rowspat.Add(row);
			}
			for(int i=0;i<rowspat.Count;i++) {
				table.Rows.Add(rowspat[i]);
			}
			retVal.Tables.Add(table);
		}

		

		/*
		///<summary>The supplied DataRows must include the following columns: ProcNum,ProcDate,Priority,ToothRange,ToothNum,ProcCode.
		///This sorts all objects in Chart module based on their dates, times, priority, and toothnum.</summary>
		public static int CompareChartRows(DataRow x, DataRow y) {
			if (x["ProcNum"].ToString() != "0" && y["ProcNum"].ToString() != "0") {//if both are procedures
				if (((DateTime)x["ProcDate"]).Date == ((DateTime)y["ProcDate"]).Date) {//and the dates are the same
					return ProcedureB.CompareProcedures(x, y);
					//IComparer procComparer=new ProcedureComparer();
					//return procComparer.Compare(x,y);//sort by priority, toothnum, procCode
					//return 0;
				}
			}
			//In all other situations, all we care about is the dates.
			return ((DateTime)x["ProcDate"]).Date.CompareTo(((DateTime)y["ProcDate"]).Date);
			//IComparer myComparer = new ObjectDateComparer();
			//return myComparer.Compare(x,y);
		}*/




	}

	///<summary>A generic comparison that sorts the rows of the account table by date and type.</summary>
	class AccountLineComparer : IComparer<DataRow>	{
		//public AccountLineComparer(){

		//}
		
		///<summary>A generic comparison that sorts the rows of the account table by date and type.</summary>
		public int Compare (DataRow rowA,DataRow rowB){
			//if different types

			//if same type
			return ((DateTime)rowA["DateTime"]).CompareTo((DateTime)rowB["DateTime"]);
		}
	}


}
