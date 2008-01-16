using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Drawing;

namespace OpenDentBusiness {
	public class AccountModuleB {
		private static DataSet retVal;

		///<summary>Parameters: 0:patNum, 1:viewingInRecall, 2:fromDate, 3:toDate, 4:isFamily.  If isFamily=1, also pass in a PatNum of guarantor to get entire family intermingled.</summary>
		public static DataSet GetAll(string[] parameters){
			int patNum=PIn.PInt(parameters[0]);
			bool viewingInRecall=PIn.PBool(parameters[1]);
			DateTime fromDate=PIn.PDate(parameters[2]);
			DateTime toDate=PIn.PDate(parameters[3]);
			bool isFamily=PIn.PBool(parameters[4]);
			retVal=new DataSet();
			if(viewingInRecall) {
				retVal.Tables.Add(ChartModuleB.GetProgNotes(patNum, false));
			}
			else {
				GetCommLog(patNum);
			}
			GetAccount(patNum,fromDate,toDate,isFamily);//Gets 2 tables: account(or account###,account###,etc) and patient
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
		private static void GetAccount(int patNum,DateTime fromDate,DateTime toDate,bool isFamily) {
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
			double amt;
			for(int i=0;i<rawProc.Rows.Count;i++){
				row=table.NewRow();
				row["AdjNum"]="0";
				row["balance"]="";//fill this later
				row["balanceDouble"]=0;//fill this later
				qty=PIn.PInt(rawProc.Rows[i]["UnitQty"].ToString()) + PIn.PInt(rawProc.Rows[i]["BaseUnits"].ToString());
				if(qty==0){
					qty=1;
				}
				amt=PIn.PDouble(rawProc.Rows[i]["ProcFee"].ToString());
				row["chargesDouble"]=amt*qty;
				row["charges"]=((double)row["chargesDouble"]).ToString("n");
				row["ClaimNum"]="0";
				row["ClaimPaymentNum"]="0";
				row["colorText"]=DefB.Long[(int)DefCat.AccountColors][0].ItemColor.ToArgb().ToString();
				row["creditsDouble"]=0;
				row["credits"]="";
				dateT=PIn.PDateT(rawProc.Rows[i]["ProcDate"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
				row["description"]=rawProc.Rows[i]["Descript"].ToString();
				if(rawProc.Rows[i]["LaymanTerm"].ToString()!=""){
					row["description"]=rawProc.Rows[i]["LaymanTerm"].ToString();
				}
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawProc.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawProc.Rows[i]["PatNum"].ToString();
				row["PayNum"]="0";
				row["ProcCode"]=rawProc.Rows[i]["ProcCode"].ToString();
				row["ProcNum"]=rawProc.Rows[i]["ProcNum"].ToString();
				row["prov"]=Providers.GetAbbr(PIn.PInt(rawProc.Rows[i]["ProvNum"].ToString()));
				row["tth"]=Tooth.ToInternat(rawProc.Rows[i]["ToothNum"].ToString());
				rows.Add(row);
			}
			//Adjustments---------------------------------------------------------------------------------------
			command="SELECT AdjAmt,AdjDate,AdjNum,AdjType,PatNum,ProvNum "
				+"FROM adjustment "
				+"WHERE (";
			for(int i=0;i<fam.List.Length;i++){
				if(i!=0){
					command+="OR ";
				}
				command+="PatNum ="+POut.PInt(fam.List[i].PatNum)+" ";
			}
			command+=") ORDER BY AdjDate";
			DataTable rawAdj=dcon.GetTable(command);
			for(int i=0;i<rawAdj.Rows.Count;i++){
				row=table.NewRow();
				row["AdjNum"]=rawAdj.Rows[i]["AdjNum"].ToString();
				row["balance"]="";//fill this later
				row["balanceDouble"]=0;//fill this later
				amt=PIn.PDouble(rawAdj.Rows[i]["AdjAmt"].ToString());
				if(amt<0){
					row["chargesDouble"]=0;
					row["charges"]="";
					row["creditsDouble"]=-amt;
					row["credits"]=(-amt).ToString("n");
				}
				else{
					row["chargesDouble"]=amt;
					row["charges"]=amt.ToString("n");
					row["creditsDouble"]=0;
					row["credits"]="";
				}
				row["ClaimNum"]="0";
				row["ClaimPaymentNum"]="0";
				row["colorText"]=DefB.Long[(int)DefCat.AccountColors][1].ItemColor.ToArgb().ToString();
				dateT=PIn.PDateT(rawAdj.Rows[i]["AdjDate"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
				row["description"]=DefB.GetName(DefCat.AdjTypes,PIn.PInt(rawAdj.Rows[i]["AdjType"].ToString()));
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawAdj.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawAdj.Rows[i]["PatNum"].ToString();
				row["PayNum"]="0";
				row["ProcCode"]=Lan.g("AccountModule","Adjust");
				row["ProcNum"]="0";
				row["prov"]=Providers.GetAbbr(PIn.PInt(rawAdj.Rows[i]["ProvNum"].ToString()));
				row["tth"]="";
				rows.Add(row);
			}
			//paysplits-----------------------------------------------------------------------------------------
			command="SELECT DatePay,paysplit.PatNum,PayAmt,paysplit.PayNum,PayType,ProcDate,ProvNum,SplitAmt "
				+"FROM paysplit "
				+"LEFT JOIN payment ON paysplit.PayNum=payment.PayNum "
				+"WHERE (";
			for(int i=0;i<fam.List.Length;i++){
				if(i!=0){
					command+="OR ";
				}
				command+="paysplit.PatNum ="+POut.PInt(fam.List[i].PatNum)+" ";
			}
			command+=") ORDER BY ProcDate";
			DataTable rawPay=dcon.GetTable(command);
			double payamt;
			for(int i=0;i<rawPay.Rows.Count;i++){
				row=table.NewRow();
				row["AdjNum"]="0";
				row["balance"]="";//fill this later
				row["balanceDouble"]=0;//fill this later
				row["chargesDouble"]=0;
				row["charges"]="";
				row["ClaimNum"]="0";
				row["ClaimPaymentNum"]="0";
				row["colorText"]=DefB.Long[(int)DefCat.AccountColors][3].ItemColor.ToArgb().ToString();
				amt=PIn.PDouble(rawPay.Rows[i]["SplitAmt"].ToString());
				row["creditsDouble"]=amt;
				row["credits"]=((double)row["creditsDouble"]).ToString("n");
				dateT=PIn.PDateT(rawPay.Rows[i]["ProcDate"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
				row["description"]=DefB.GetName(DefCat.PaymentTypes,PIn.PInt(rawPay.Rows[i]["PayType"].ToString()));
				payamt=PIn.PDouble(rawPay.Rows[i]["PayAmt"].ToString());
				if(payamt!=amt){
					row["description"]+=" "+payamt.ToString("c")+" "+Lan.g("ContrAccount","(split)");
				}
				//we might use DatePay here to add to description
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawPay.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawPay.Rows[i]["PatNum"].ToString();
				row["PayNum"]=rawPay.Rows[i]["PayNum"].ToString();
				row["ProcCode"]=Lan.g("AccountModule","Pay");
				row["ProcNum"]="0";
				row["prov"]=Providers.GetAbbr(PIn.PInt(rawPay.Rows[i]["ProvNum"].ToString()));
				row["tth"]="";
				rows.Add(row);
			}
			//claimpayments-------------------------------------------------------------------------------------
			command="SELECT ClaimNum,ClaimPaymentNum,DateCP,SUM(InsPayAmt) _InsPayAmt,PatNum,ProcDate,"
				+"ProvNum,SUM(WriteOff) _WriteOff "
				+"FROM claimproc "
				+"WHERE (Status=1 OR Status=4 OR Status=5) "//received,supplemental, or capclaim
				+"AND (";
			for(int i=0;i<fam.List.Length;i++){
				if(i!=0){
					command+="OR ";
				}
				command+="PatNum ="+POut.PInt(fam.List[i].PatNum)+" ";
			}
			command+=") GROUP BY ClaimNum ORDER BY DateCP";
			DataTable rawClaimPay=dcon.GetTable(command);
			DateTime procdate;
			double writeoff;
			for(int i=0;i<rawClaimPay.Rows.Count;i++){
				row=table.NewRow();
				row["AdjNum"]="0";
				row["balance"]="";//fill this later
				row["balanceDouble"]=0;//fill this later
				row["chargesDouble"]=0;
				row["charges"]="";
				row["ClaimNum"]=rawClaimPay.Rows[i]["ClaimNum"].ToString();
				row["ClaimPaymentNum"]=rawClaimPay.Rows[i]["ClaimPaymentNum"].ToString();
//todo: change this color. 3 means payment. 4 means claim.  get a new color for inspayments.
				row["colorText"]=DefB.Long[(int)DefCat.AccountColors][4].ItemColor.ToArgb().ToString();
				amt=PIn.PDouble(rawClaimPay.Rows[i]["_InsPayAmt"].ToString());
				writeoff=PIn.PDouble(rawClaimPay.Rows[i]["_WriteOff"].ToString());
				row["creditsDouble"]=amt+writeoff;
				row["credits"]=((double)row["creditsDouble"]).ToString("n");
				dateT=PIn.PDateT(rawClaimPay.Rows[i]["DateCP"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
				procdate=PIn.PDateT(rawClaimPay.Rows[i]["ProcDate"].ToString());
				row["description"]=Lan.g("AccountModule","Insurance Payment for Claim ")+procdate.ToShortDateString();
				if(writeoff!=0){
					row["description"]+="/r/n"+Lan.g("AccountModule","Payment: ")+amt.ToString("c")+"/r/n"
						+Lan.g("AccountModule","Writeoff: ")+writeoff.ToString("c");
				}
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawClaimPay.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawClaimPay.Rows[i]["PatNum"].ToString();
				row["PayNum"]="0";
				row["ProcCode"]=Lan.g("AccountModule","InsPay");
				row["ProcNum"]="0";
				row["prov"]=Providers.GetAbbr(PIn.PInt(rawClaimPay.Rows[i]["ProvNum"].ToString()));
				row["tth"]="";
				rows.Add(row);
			}
			//claims (do not affect balance)-------------------------------------------------------------------------
			command="SELECT CarrierName,ClaimFee,ClaimNum,ClaimStatus,ClaimType,DateService,PatNum,ProvTreat "
				+"FROM claim "
				+"LEFT JOIN insplan ON claim.PlanNum=insplan.PlanNum "
				+"LEFT JOIN carrier ON carrier.CarrierNum=insplan.CarrierNum "
				+"WHERE (";
			for(int i=0;i<fam.List.Length;i++){
				if(i!=0){
					command+="OR ";
				}
				command+="PatNum ="+POut.PInt(fam.List[i].PatNum)+" ";
			}
			command+=") ORDER BY DateService";
			DataTable rawClaim=dcon.GetTable(command);
			for(int i=0;i<rawClaim.Rows.Count;i++){
				row=table.NewRow();
				row["AdjNum"]="0";
				row["balance"]="";//fill this later
				row["balanceDouble"]=0;//fill this later
				row["chargesDouble"]=0;
				row["charges"]="";
				row["ClaimNum"]=rawClaim.Rows[i]["ClaimNum"].ToString();
				row["ClaimPaymentNum"]="0";
				row["colorText"]=DefB.Long[(int)DefCat.AccountColors][4].ItemColor.ToArgb().ToString();
				row["creditsDouble"]=0;
				row["credits"]="";
				dateT=PIn.PDateT(rawClaim.Rows[i]["DateService"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
				//procdate=PIn.PDateT(rawClaim.Rows[i]["ProcDate"].ToString());
				if(rawClaim.Rows[i]["ClaimType"].ToString()=="P"){
					row["description"]=Lan.g("ContrAccount","Pri")+" ";
				}
				else if(rawClaim.Rows[i]["ClaimType"].ToString()=="S"){
					row["description"]=Lan.g("ContrAccount","Sec")+" ";
				}
				else if(rawClaim.Rows[i]["ClaimType"].ToString()=="PreAuth"){
					row["description"]=Lan.g("ContrAccount","PreAuth")+" ";
				}
				else if(rawClaim.Rows[i]["ClaimType"].ToString()=="Other"){
					row["description"]="";
				}
				else if(rawClaim.Rows[i]["ClaimType"].ToString()=="Cap"){
					row["description"]=Lan.g("ContrAccount","Cap")+" ";
				}
				amt=PIn.PDouble(rawClaim.Rows[i]["ClaimFee"].ToString());
				row["description"]+=Lan.g("ContrAccount","Claim")+" "+amt.ToString("c")+" "
					+rawClaim.Rows[i]["CarrierName"].ToString();
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawClaim.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawClaim.Rows[i]["PatNum"].ToString();
				row["PayNum"]="0";
				row["ProcCode"]=Lan.g("AccountModule","Claim");
				row["ProcNum"]="0";
				row["prov"]=Providers.GetAbbr(PIn.PInt(rawClaim.Rows[i]["ProvTreat"].ToString()));
				row["tth"]="";
				rows.Add(row);
			}







			//Sorting
			rows.Sort(new AccountLineComparer());
			//rows.Sort(CompareCommRows);
			//Pass off all the rows for the whole family in order to compute the patient balances----------------
			GetPatientTable(fam,rows);
			//Filter out patients that we are not interested in--------------------------------------------------
			if(!isFamily){//if isFamily, then we will be showing all patients with no filtering
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
				if(rows[i]["ClaimPaymentNum"].ToString()=="0" && rows[i]["ClaimNum"].ToString()!="0"){//claims
					rows[i]["balance"]="";
				}
				else{
					rows[i]["balance"]=bal.ToString("n");
				}
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
				row["balance"]=balanceForward.ToString("n");
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
			double balfam=0;
			for(int p=0;p<fam.List.Length;p++){
				row=table.NewRow();
				bal=0;
				for(int i=0;i<rows.Count;i++) {
					if(fam.List[p].PatNum.ToString()==rows[i]["PatNum"].ToString()){
						bal+=(double)rows[i]["chargesDouble"];
						bal-=(double)rows[i]["creditsDouble"];
					}
				}
				balfam+=bal;
				row["balanceDouble"]=bal;
				row["balance"]=bal.ToString("n");
				row["name"]=fam.List[p].GetNameLF();
				row["PatNum"]=fam.List[p].PatNum.ToString();
				rowspat.Add(row);
			}
			//Row for entire family
			row=table.NewRow();
			row["balanceDouble"]=balfam;
			row["balance"]=balfam.ToString("f");
			row["name"]=Lan.g("AccountModule","Entire Family");
			row["PatNum"]=fam.List[0].PatNum.ToString();
			rowspat.Add(row);
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
