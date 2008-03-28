using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Drawing;

namespace OpenDentBusiness {
	public class AccountModuleB {
		private static DataSet retVal;
		private static Family fam;
		private static Patient pat;

		///<summary>Parameters: 0:patNum, 1:viewingInRecall, 2:fromDate, 3:toDate, 4:intermingled.  If intermingled=1, the patnum of any family member will get entire family intermingled.</summary>
		public static DataSet GetAll(string[] parameters){
			int patNum=PIn.PInt(parameters[0]);
			fam=Patients.GetFamily(patNum);
			bool intermingled=PIn.PBool(parameters[4]);
			if(intermingled){
				patNum=fam.List[0].PatNum;//guarantor
			}
			pat=fam.GetPatient(patNum);
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
			bool singlePatient=!intermingled;//so one or the other will be true
			//Gets 3 tables: account(or account###,account###,etc), patient, payplan.
			GetAccount(patNum,fromDate,toDate,intermingled,singlePatient,false);
			//GetPayPlans(patNum,fromDate,toDate,isFamily);
			return retVal;
		}

		///<summary>Parameters: 0:patNum, 1:singlePatient, 2:fromDate, 3:toDate, 4:intermingled,   If intermingled=1 the patnum of any family member will get entire family intermingled.  toDate should not be Max, or PayPlan amort will include too many charges.  The 10 days will not be added to toDate until creating the actual amortization schedule.</summary>
		public static DataSet GetStatement(string[] parameters){
			int patNum=PIn.PInt(parameters[0]);
			fam=Patients.GetFamily(patNum);
			bool intermingled=PIn.PBool(parameters[4]);
			if(intermingled){
				patNum=fam.List[0].PatNum;//guarantor
			}
			pat=fam.GetPatient(patNum);
			//bool viewingInRecall=PIn.PBool(parameters[1]);
			bool singlePatient=PIn.PBool(parameters[1]);
			DateTime fromDate=PIn.PDate(parameters[2]);
			DateTime toDate=PIn.PDate(parameters[3]);
			retVal=new DataSet();
			//if(viewingInRecall) {
			//	retVal.Tables.Add(ChartModuleB.GetProgNotes(patNum, false));
			//}
			//else {
			//	GetCommLog(patNum);
			//}
			//Gets 3 tables: account(or account###,account###,etc), patient, payplan.
			GetAccount(patNum,fromDate,toDate,intermingled,singlePatient,true);
			//GetPayPlans(patNum,fromDate,toDate,isFamily);
			GetApptTable(fam,singlePatient,patNum);//table= appts
			return retVal;
		}

		///<summary>Gets a table of charges mixed with payments to show in the payplan edit window.  Parameters: 0:payPlanNum</summary>
		public static DataSet GetPayPlanAmort(string[] parameters){
			int payPlanNum=PIn.PInt(parameters[0]);
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("payplanamort");
			DataRow row;
			SetTableColumns(table);
			List<DataRow> rows=new List<DataRow>();
			string command="SELECT ChargeDate,Interest,Note,PayPlanChargeNum,Principal,ProvNum "
				+"FROM payplancharge WHERE PayPlanNum="+POut.PInt(payPlanNum);
			DataTable rawCharge=dcon.GetTable(command);
			DateTime dateT;
			double principal;
			double interest;
			double total;
			for(int i=0;i<rawCharge.Rows.Count;i++){
				interest=PIn.PDouble(rawCharge.Rows[i]["Interest"].ToString());
				principal=PIn.PDouble(rawCharge.Rows[i]["Principal"].ToString());
				total=principal+interest;
				row=table.NewRow();
				row["AdjNum"]="0";
				row["balance"]="";//fill this later
				row["balanceDouble"]=0;//fill this later
				row["chargesDouble"]=total;
				row["charges"]=((double)row["chargesDouble"]).ToString("n");
				row["ClaimNum"]="0";
				row["ClaimPaymentNum"]="0";
				row["colorText"]=Color.Black.ToArgb().ToString();
				row["creditsDouble"]=0;
				row["credits"]="";//((double)row["creditsDouble"]).ToString("n");
				dateT=PIn.PDateT(rawCharge.Rows[i]["ChargeDate"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
				row["description"]="";//"Princ: "+principal.ToString("n")+
				if(interest!=0){
					row["description"]+="Interest: "+interest.ToString("n");//+"Princ: "+principal.ToString("n")+;
				}
				if(rawCharge.Rows[i]["Note"].ToString()!=""){
					if(row["description"].ToString()!=""){
						row["description"]+="  ";	
					}
					row["description"]+=rawCharge.Rows[i]["Note"].ToString();
				}
				row["extraDetail"]="";
				row["patient"]="";
				row["PatNum"]="0";
				row["PayNum"]="0";
				row["PayPlanNum"]="0";
				row["PayPlanChargeNum"]=rawCharge.Rows[i]["PayPlanChargeNum"].ToString();
				row["ProcCode"]=Lan.g("AccountModule","PPcharge");
				row["ProcNum"]="0";
				row["procsOnClaim"]="";
				row["prov"]=Providers.GetAbbr(PIn.PInt(rawCharge.Rows[i]["ProvNum"].ToString()));
				row["StatementNum"]="0";
				row["tth"]="";
				rows.Add(row);
			}
			//Paysplits
			command="SELECT CheckNum,DatePay,paysplit.PatNum,PayAmt,paysplit.PayNum,PayPlanNum,"
				+"PayType,ProcDate,ProvNum,SplitAmt "
				+"FROM paysplit "
				+"LEFT JOIN payment ON paysplit.PayNum=payment.PayNum "
				+"WHERE ("
				+"paysplit.PayPlanNum="+POut.PInt(payPlanNum);
			/*for(int i=0;i<fam.List.Length;i++){
				if(i!=0){
					command+="OR ";
				}
				command+="paysplit.PatNum ="+POut.PInt(fam.List[i].PatNum)+" ";
			}*/
			command+=") ORDER BY ProcDate";
			DataTable rawPay=dcon.GetTable(command);
			double payamt;
			double amt;
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
				if(rawPay.Rows[i]["CheckNum"].ToString()!=""){
					row["description"]+=" #"+rawPay.Rows[i]["CheckNum"].ToString();
				}
				payamt=PIn.PDouble(rawPay.Rows[i]["PayAmt"].ToString());
				row["description"]+=" "+payamt.ToString("c");
				if(payamt!=amt){
					row["description"]+=" "+Lan.g("ContrAccount","(split)");
				}
				//we might use DatePay here to add to description
				row["extraDetail"]="";
				row["patient"]="";
				row["PatNum"]=rawPay.Rows[i]["PatNum"].ToString();
				row["PayNum"]=rawPay.Rows[i]["PayNum"].ToString();
				row["PayPlanNum"]="0";
				row["PayPlanChargeNum"]="0";
				row["ProcCode"]=Lan.g("AccountModule","Pay");
				row["ProcNum"]="0";
				row["procsOnClaim"]="";
				row["prov"]=Providers.GetAbbr(PIn.PInt(rawPay.Rows[i]["ProvNum"].ToString()));
				row["StatementNum"]="0";
				row["tth"]="";
				rows.Add(row);
			}
			//Sorting-----------------------------------------------------------------------------------------
			rows.Sort(new AccountLineComparer());
			//Add # indicators to charges
			int num=1;
			for(int i=0;i<rows.Count;i++) {
				if(rows[i]["PayPlanChargeNum"].ToString()=="0"){//if not a payplancharge
					continue;
				}
				rows[i]["description"]="#"+num.ToString()+" "+rows[i]["description"].ToString();
				num++;
			}
			//Compute balances-------------------------------------------------------------------------------------
			double bal=0;
			for(int i=0;i<rows.Count;i++) {
				bal+=(double)rows[i]["chargesDouble"];
				bal-=(double)rows[i]["creditsDouble"];
				rows[i]["balanceDouble"]=bal;
				//if(rows[i]["ClaimPaymentNum"].ToString()=="0" && rows[i]["ClaimNum"].ToString()!="0"){//claims
				//	rows[i]["balance"]="";
				//}
				//else{
					rows[i]["balance"]=bal.ToString("n");
				//}
			}
			retVal=new DataSet();
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			retVal.Tables.Add(table);
			return retVal;
		}

		/*private static void GetPayPlanCharges(){
			string command="SELECT "
				+"(SELECT SUM(Principal) FROM payplancharge WHERE payplancharge.PayPlanNum=payplan.PayPlanNum) _principal,"
				+"(SELECT SUM(Interest) FROM payplancharge WHERE payplancharge.PayPlanNum=payplan.PayPlanNum) _interest,"
				+"(SELECT SUM(Principal) FROM payplancharge WHERE payplancharge.PayPlanNum=payplan.PayPlanNum "
					+"AND ChargeDate <= CURDATE()) _principalDue,"
				+"(SELECT SUM(Interest) FROM payplancharge WHERE payplancharge.PayPlanNum=payplan.PayPlanNum "
					+"AND ChargeDate <= CURDATE()) _interestDue,"
				+"CarrierName,payplan.Guarantor,"
				+"payplan.PatNum,PayPlanDate,payplan.PayPlanNum,"
				+"payplan.PlanNum "
				+"FROM payplan "
				+"LEFT JOIN insplan ON insplan.PlanNum=payplan.PlanNum "
				+"LEFT JOIN carrier ON carrier.CarrierNum=insplan.CarrierNum "
				+"WHERE  (";
			for(int i=0;i<fam.List.Length;i++){
				if(i!=0){
					command+="OR ";
				}
				command+="payplan.Guarantor ="+POut.PInt(fam.List[i].PatNum)+" "
					+"OR payplan.PatNum ="+POut.PInt(fam.List[i].PatNum)+" ";
			}
			command+=") GROUP BY payplan.PayPlanNum ORDER BY PayPlanDate";
			DataTable rawPayPlan=dcon.GetTable(command);
		}*/

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

		private static void SetTableColumns(DataTable table){
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
			table.Columns.Add("extraDetail");
			table.Columns.Add("patient");
			table.Columns.Add("PatNum");
			table.Columns.Add("PayNum");//even though we only show split objects
			table.Columns.Add("PayPlanNum");
			table.Columns.Add("PayPlanChargeNum");
			table.Columns.Add("ProcCode");
			table.Columns.Add("ProcNum");
			table.Columns.Add("procsOnClaim");//for a claim, the ProcNums, comma delimited.
			table.Columns.Add("prov");
			table.Columns.Add("StatementNum");
			table.Columns.Add("tth");
		}
		
		///<summary>Also gets the patient table, which has one row for each family member. Also currently runs aging.  Also gets payplan table.  If isForStatement, then the resulting payplan table looks totally different.</summary>
		private static void GetAccount(int patNum,DateTime fromDate,DateTime toDate,bool intermingled,bool singlePatient,bool isForStatement) {
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("account");
			//run aging.  This need serious optimization-------------------------------------------------------
			if(PrefB.GetBool("AgingCalculatedMonthlyInsteadOfDaily")){
				Ledgers.ComputeAging(pat.Guarantor,PIn.PDate(PrefB.GetString("DateLastAging")));
			}
			else{
				Ledgers.ComputeAging(pat.Guarantor,DateTime.Today);
			}
			Patients.UpdateAging(pat.Guarantor,Ledgers.Bal[0],Ledgers.Bal[1],Ledgers.Bal[2]
				,Ledgers.Bal[3],Ledgers.InsEst,Ledgers.BalTotal,Ledgers.PayPlanDue);
			//Now, back to getting the tables------------------------------------------------------------------
			DataRow row;
			SetTableColumns(table);
			//but we won't actually fill this table with rows until the very end.  It's more useful to use a List<> for now.
			List<DataRow> rows=new List<DataRow>();
			DateTime dateT;
			double qty;
			double amt;
			string command;
			//claimprocs (ins payments)----------------------------------------------------------------------------
			command="SELECT ClaimNum,ClaimPaymentNum,DateCP,SUM(InsPayAmt) _InsPayAmt,PatNum,ProcDate,"
				+"ProvNum,SUM(WriteOff) _WriteOff "
				+"FROM claimproc "
				+"WHERE (Status=1 OR Status=4) "//received or supplemental, (5, capclaim handled on procedure row)
				+"AND (";
			for(int i=0;i<fam.List.Length;i++){
				if(i!=0){
					command+="OR ";
				}
				command+="PatNum ="+POut.PInt(fam.List[i].PatNum)+" ";
			}
			command+=") GROUP BY ClaimNum,DateCP ORDER BY DateCP";
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
				row["ClaimPaymentNum"]="1";//this is now just a boolean flag indicating that it is a payment.
				//this is because it will frequently not be attached to an actual claim payment.
				row["colorText"]=DefB.Long[(int)DefCat.AccountColors][7].ItemColor.ToArgb().ToString();
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
					row["description"]+="\r\n"+Lan.g("AccountModule","Payment:")+" "+amt.ToString("c")+"\r\n"
						+Lan.g("AccountModule","Writeoff:")+" "+writeoff.ToString("c");
				}
				row["extraDetail"]="";
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawClaimPay.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawClaimPay.Rows[i]["PatNum"].ToString();
				row["PayNum"]="0";
				row["PayPlanNum"]="0";
				row["PayPlanChargeNum"]="0";
				row["ProcCode"]=Lan.g("AccountModule","InsPay");
				row["ProcNum"]="0";
				row["procsOnClaim"]="";
				row["prov"]=Providers.GetAbbr(PIn.PInt(rawClaimPay.Rows[i]["ProvNum"].ToString()));
				row["StatementNum"]="0";
				row["tth"]="";
				rows.Add(row);
			}
			//Procedures------------------------------------------------------------------------------------------
			command="SELECT procedurelog.BaseUnits,Descript,SUM(cp1.InsPayAmt) _insPayAmt,"
				+"LaymanTerm,procedurelog.MedicalCode,MAX(cp1.NoBillIns) _noBillIns,procedurelog.PatNum,ProcCode,"
				+"procedurelog.ProcDate,ProcFee,procedurelog.ProcNum,procedurelog.ProvNum,ToothNum,UnitQty,"
				+"SUM(cp1.WriteOff) _writeOff, "
				+"(SELECT SUM(WriteOff) FROM claimproc cp2 WHERE procedurelog.ProcNum=cp2.ProcNum "
				+"AND (cp2.Status=7 OR cp2.Status=5)) _writeOffCap "
				+"FROM procedurelog "
				+"LEFT JOIN procedurecode ON procedurelog.CodeNum=procedurecode.CodeNum "
				+"LEFT JOIN claimproc cp1 ON procedurelog.ProcNum=cp1.ProcNum "
				+"WHERE ProcStatus=2 "//complete
				+"AND (";
			for(int i=0;i<fam.List.Length;i++){
				if(i!=0){
					command+="OR ";
				}
				command+="procedurelog.PatNum ="+POut.PInt(fam.List[i].PatNum)+" ";
			}
			command+=") GROUP BY procedurelog.ProcNum ORDER BY ProcDate";
			DataTable rawProc=dcon.GetTable(command);
			double insPayAmt;
			double writeOff;
			double writeOffCap;
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
				writeOffCap=PIn.PDouble(rawProc.Rows[i]["_writeOffCap"].ToString());
				amt-=writeOffCap;
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
				row["description"]="";
				if(rawProc.Rows[i]["MedicalCode"].ToString()!=""){
					row["description"]+=Lan.g("ContrAccount","(medical)")+" ";
				}
				row["description"]+=rawProc.Rows[i]["Descript"].ToString();
				if(rawProc.Rows[i]["LaymanTerm"].ToString()!=""){
					row["description"]=rawProc.Rows[i]["LaymanTerm"].ToString();
				}
				if(rawProc.Rows[i]["_noBillIns"].ToString()!="" && rawProc.Rows[i]["_noBillIns"].ToString()!="0"){
					row["description"]+=" "+Lan.g("ContrAccount","(NoBillIns)");
				}
				insPayAmt=PIn.PDouble(rawProc.Rows[i]["_insPayAmt"].ToString());
				writeOff=PIn.PDouble(rawProc.Rows[i]["_writeOff"].ToString());
				row["extraDetail"]="";
				if(insPayAmt>0 || writeOff>0){
					row["extraDetail"]+=Lan.g("AccountModule","Ins Paid: ")+insPayAmt.ToString("c");
					if(writeOff>0){
						row["extraDetail"]+=", "+Lan.g("AccountModule","Writeoff: ")+writeOff.ToString("c");
					}
				}
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawProc.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawProc.Rows[i]["PatNum"].ToString();
				row["PayNum"]="0";
				row["PayPlanNum"]="0";
				row["PayPlanChargeNum"]="0";
				row["ProcCode"]=rawProc.Rows[i]["ProcCode"].ToString();
				row["ProcNum"]=rawProc.Rows[i]["ProcNum"].ToString();
				row["procsOnClaim"]="";
				row["prov"]=Providers.GetAbbr(PIn.PInt(rawProc.Rows[i]["ProvNum"].ToString()));
				row["StatementNum"]="0";
				row["tth"]=Tooth.GetToothLabel(rawProc.Rows[i]["ToothNum"].ToString());
				rows.Add(row);
			}
			//Adjustments---------------------------------------------------------------------------------------
			command="SELECT AdjAmt,AdjDate,AdjNum,AdjType,PatNum,ProvNum,AdjNote "
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
				row["extraDetail"] = rawAdj.Rows[i]["AdjNote"].ToString();
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawAdj.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawAdj.Rows[i]["PatNum"].ToString();
				row["PayNum"]="0";
				row["PayPlanNum"]="0";
				row["PayPlanChargeNum"]="0";
				row["ProcCode"]=Lan.g("AccountModule","Adjust");
				row["ProcNum"]="0";
				row["procsOnClaim"]="";
				row["prov"]=Providers.GetAbbr(PIn.PInt(rawAdj.Rows[i]["ProvNum"].ToString()));
				row["StatementNum"]="0";
				row["tth"]="";
				rows.Add(row);
			}
			//paysplits-----------------------------------------------------------------------------------------
			command="SELECT CheckNum,DatePay,paysplit.PatNum,payment.PatNum _patNumPayment,PayAmt,"
				+"paysplit.PayNum,PayPlanNum,"
				+"PayType,ProcDate,ProvNum,SplitAmt,payment.PayNote "
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
				//do not add rows that are attached to payment plans
				if(rawPay.Rows[i]["PayPlanNum"].ToString()!="0"){
					continue;
				}
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
				if(rawPay.Rows[i]["CheckNum"].ToString()!=""){
					row["description"]+=" #"+rawPay.Rows[i]["CheckNum"].ToString();
				}
				payamt=PIn.PDouble(rawPay.Rows[i]["PayAmt"].ToString());
				row["description"]+=" "+payamt.ToString("c");
				if(rawPay.Rows[i]["PatNum"].ToString() != rawPay.Rows[i]["_patNumPayment"].ToString()){
					row["description"]+=" ("+Lan.g("ContrAccount","Paid by ")
						+fam.GetNameInFamFirst(PIn.PInt(rawPay.Rows[i]["_patNumPayment"].ToString()))+")";
				}
				if(payamt!=amt){
					row["description"]+=" "+Lan.g("ContrAccount","(split)");
				}
				//we might use DatePay here to add to description
				row["extraDetail"] = rawPay.Rows[i]["PayNote"].ToString();
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawPay.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawPay.Rows[i]["PatNum"].ToString();
				row["PayNum"]=rawPay.Rows[i]["PayNum"].ToString();
				row["PayPlanNum"]="0";
				row["PayPlanChargeNum"]="0";
				row["ProcCode"]=Lan.g("AccountModule","Pay");
				row["ProcNum"]="0";
				row["procsOnClaim"]="";
				row["prov"]=Providers.GetAbbr(PIn.PInt(rawPay.Rows[i]["ProvNum"].ToString()));
				row["StatementNum"]="0";
				row["tth"]="";
				rows.Add(row);
			}
			//claims (do not affect balance)-------------------------------------------------------------------------
			command="SELECT CarrierName,ClaimFee,claim.ClaimNum,ClaimStatus,ClaimType,DateReceived,DateService,"
				+"claim.InsPayEst,"
				+"claim.InsPayAmt,claim.PatNum,GROUP_CONCAT(claimproc.ProcNum) _ProcNums,ProvTreat,claim.WriteOff "
				+"FROM claim "
				+"LEFT JOIN insplan ON claim.PlanNum=insplan.PlanNum "
				+"LEFT JOIN carrier ON carrier.CarrierNum=insplan.CarrierNum "
				+"INNER JOIN claimproc ON claimproc.ClaimNum=claim.ClaimNum "
				+"WHERE (";
			for(int i=0;i<fam.List.Length;i++){
				if(i!=0){
					command+="OR ";
				}
				command+="claim.PatNum ="+POut.PInt(fam.List[i].PatNum)+" ";
			}
			command+=") GROUP BY claim.ClaimNum ORDER BY DateService";
			DataTable rawClaim=dcon.GetTable(command);
			DateTime daterec;
			double amtpaid;
			double insest;
			for(int i=0;i<rawClaim.Rows.Count;i++){
				row=table.NewRow();
				row["AdjNum"]="0";
				row["balance"]="";//fill this later
				row["balanceDouble"]=0;//fill this later
				row["chargesDouble"]=0;
				row["charges"]="";
				row["ClaimNum"]=rawClaim.Rows[i]["ClaimNum"].ToString();
				row["ClaimPaymentNum"]="0";
				//moved down lower to use different colors depending on the claim status
				//row["colorText"] = DefB.Long[(int)DefCat.AccountColors][4].ItemColor.ToArgb().ToString();
				row["creditsDouble"] = 0;
				row["credits"]="";
				dateT=PIn.PDateT(rawClaim.Rows[i]["DateService"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
				if(rawClaim.Rows[i]["ClaimType"].ToString()=="P"){
					row["description"]=Lan.g("ContrAccount","Pri")+" ";
					row["colorText"] = DefB.Long[(int)DefCat.AccountColors][4].ItemColor.ToArgb().ToString();
					//if the claim is received, the color will change below
				}
				else if(rawClaim.Rows[i]["ClaimType"].ToString()=="S"){
					row["description"]=Lan.g("ContrAccount","Sec")+" ";
					row["colorText"] = DefB.Long[(int)DefCat.AccountColors][4].ItemColor.ToArgb().ToString();
				}
				else if(rawClaim.Rows[i]["ClaimType"].ToString()=="PreAuth"){
					row["description"]=Lan.g("ContrAccount","PreAuth")+" ";
					if (rawClaim.Rows[i]["ClaimStatus"].ToString() == "R") {//only change color on pre-auths that are recieved
						row["colorText"] = DefB.Long[(int)DefCat.AccountColors][9].ItemColor.ToArgb().ToString();
					}
					else{
						row["colorText"] = DefB.Long[(int)DefCat.AccountColors][4].ItemColor.ToArgb().ToString();
					}
				}
				else if(rawClaim.Rows[i]["ClaimType"].ToString()=="Other"){
					row["description"]="";
					row["colorText"] = DefB.Long[(int)DefCat.AccountColors][4].ItemColor.ToArgb().ToString();
				}
				else if(rawClaim.Rows[i]["ClaimType"].ToString()=="Cap"){
					row["description"]=Lan.g("ContrAccount","Cap")+" ";
					row["colorText"] = DefB.Long[(int)DefCat.AccountColors][4].ItemColor.ToArgb().ToString();
				}
				amt=PIn.PDouble(rawClaim.Rows[i]["ClaimFee"].ToString());
				row["description"]+=Lan.g("ContrAccount","Claim")+" "+amt.ToString("c")+" "
					+rawClaim.Rows[i]["CarrierName"].ToString();
				daterec=PIn.PDateT(rawClaim.Rows[i]["DateReceived"].ToString());
				if (daterec.Year > 1880)
				{//and claimstatus=R
					row["description"] += "\r\n" + Lan.g("ContrAccount", "Received") + " " + daterec.ToShortDateString();
					if (rawClaim.Rows[i]["ClaimStatus"].ToString() == "R"){
						if (rawClaim.Rows[i]["ClaimType"].ToString() == "PreAuth") {
							row["colorText"] = DefB.Long[(int)DefCat.AccountColors][9].ItemColor.ToArgb().ToString();
						} 
						else {
							row["colorText"] = DefB.Long[(int)DefCat.AccountColors][8].ItemColor.ToArgb().ToString();
						}
					} 
					else if (rawClaim.Rows[i]["ClaimType"].ToString() == "PreAuth" && rawClaim.Rows[i]["ClaimStatus"].ToString() == "R")
					{
						row["colorText"] = DefB.Long[(int)DefCat.AccountColors][9].ItemColor.ToArgb().ToString();
					} 
					else{
						row["description"] += "\r\n" + Lan.g("ContrAccount", "Re-Sent");
						row["colorText"] = DefB.Long[(int)DefCat.AccountColors][4].ItemColor.ToArgb().ToString();
					}

				} else if (rawClaim.Rows[i]["ClaimStatus"].ToString() == "U")
				{
					row["description"] += "\r\n" + Lan.g("ContrAccount", "Unsent");
				} else if (rawClaim.Rows[i]["ClaimStatus"].ToString() == "H")
				{
					row["description"] += "\r\n" + Lan.g("ContrAccount", "Hold until Pri received");
				} else if (rawClaim.Rows[i]["ClaimStatus"].ToString() == "W")
				{
					row["description"] += "\r\n" + Lan.g("ContrAccount", "Waiting to Send");
				} else if (rawClaim.Rows[i]["ClaimStatus"].ToString() == "S")
				{
					row["description"] += "\r\n" + Lan.g("ContrAccount", "Sent");
				}
				insest = PIn.PDouble(rawClaim.Rows[i]["InsPayEst"].ToString());
				amtpaid = PIn.PDouble(rawClaim.Rows[i]["InsPayAmt"].ToString());
				if (rawClaim.Rows[i]["ClaimStatus"].ToString() == "W"
					|| rawClaim.Rows[i]["ClaimStatus"].ToString() == "S")
				{
					if (rawClaim.Rows[i]["ClaimType"].ToString() == "PreAuth") {
						if (amtpaid != 0 && ((insest - amtpaid) >= 0)) {//show additional info on PreAuth resubmits
							row["description"] += "\r\n" + Lan.g("ContrAccount", "Est. Pre-Authorization Pending:") + " " + (insest - amtpaid).ToString("c");
						}
						else {
							row["description"] += "\r\n" + Lan.g("ContrAccount", "Est. Pre-Authorization Pending:") + " " + insest.ToString("c");
						}
					}
					else if (amtpaid != 0 && ((insest - amtpaid) >= 0)) {//show additional info on resubmits
						row["description"] += "\r\n" + Lan.g("ContrAccount", "Remaining Est. Payment Pending:") + " " + (insest - amtpaid).ToString("c");
					}
					else {
						row["description"] += "\r\n" + Lan.g("ContrAccount", "Estimated Payment Pending:") + " " + insest.ToString("c");
					}
				}
				if (amtpaid != 0){
					if (rawClaim.Rows[i]["ClaimType"].ToString() == "PreAuth") {
						row["description"] += "\r\n" + Lan.g("ContrAccount", "Estimated Payment From Pre-Auth:") + " " + amtpaid.ToString("c");
					}
					else {
						row["description"] += "\r\n" + Lan.g("ContrAccount", "Payment:") + " " + amtpaid.ToString("c");
					}
				} 
				else if(amtpaid == 0 && (rawClaim.Rows[i]["ClaimStatus"].ToString() == "R")){
					if (rawClaim.Rows[i]["ClaimType"].ToString() == "PreAuth") {
						row["description"] += "\r\n" + Lan.g("ContrAccount", "No Payment Authorized by Pre-Auth");
					}
					else {
						row["description"] += "\r\n" + Lan.g("ContrAccount", "NO PAYMENT");
					}
				}
				writeoff=PIn.PDouble(rawClaim.Rows[i]["WriteOff"].ToString());
				if(writeoff!=0){
					row["description"]+="\r\n"+Lan.g("ContrAccount","Writeoff:")+" "+writeoff.ToString("c");
				}
				row["extraDetail"]="";
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawClaim.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawClaim.Rows[i]["PatNum"].ToString();
				row["PayNum"]="0";
				row["PayPlanNum"]="0";
				row["PayPlanChargeNum"]="0";
				row["ProcCode"]=Lan.g("AccountModule","Claim");
				row["ProcNum"]="0";
				row["procsOnClaim"]=PIn.PByteArray(rawClaim.Rows[i]["_ProcNums"]);
				row["prov"]=Providers.GetAbbr(PIn.PInt(rawClaim.Rows[i]["ProvTreat"].ToString()));
				row["StatementNum"]="0";
				row["tth"]="";
				rows.Add(row);
			}
			//Statement----------------------------------------------------------------------------------------
			command="SELECT DateSent,IsSent,Mode_,StatementNum,PatNum, Note, NoteBold "
				+"FROM statement "
				+"WHERE (";
			for(int i=0;i<fam.List.Length;i++){
				if(i!=0){
					command+="OR ";
				}
				command+="PatNum ="+POut.PInt(fam.List[i].PatNum)+" ";
			}
			command+=") ORDER BY DateSent";
			DataTable rawState=dcon.GetTable(command);
			StatementMode _mode;
			for(int i=0;i<rawState.Rows.Count;i++){
				row=table.NewRow();
				row["AdjNum"]="0";
				row["balance"]="";//fill this later
				row["balanceDouble"]=0;//fill this later
				row["chargesDouble"]=0;
				row["charges"]="";
				row["ClaimNum"]="0";
				row["ClaimPaymentNum"]="0";
				row["colorText"]=DefB.Long[(int)DefCat.AccountColors][5].ItemColor.ToArgb().ToString();
				row["creditsDouble"]=0;
				row["credits"]="";
				dateT=PIn.PDateT(rawState.Rows[i]["DateSent"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
				row["description"]+=Lan.g("ContrAccount","Statement");
				_mode=(StatementMode)PIn.PInt(rawState.Rows[i]["Mode_"].ToString());
				row["description"]+="-"+Lan.g("enumStatementMode",_mode.ToString());
				if(rawState.Rows[i]["IsSent"].ToString()=="0"){
					row["description"]+=" "+Lan.g("ContrAccount","(unsent)");
				}
				row["extraDetail"] = "";
				if(rawState.Rows[i]["NoteBold"].ToString() != ""){
					row["extraDetail"] += rawState.Rows[i]["NoteBold"].ToString() + "\r\n";
				}
				if (rawState.Rows[i]["Note"].ToString() != "") {
					row["extraDetail"] += rawState.Rows[i]["Note"].ToString();
				}
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawState.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawState.Rows[i]["PatNum"].ToString();
				row["PayNum"]="0";
				row["PayPlanNum"]="0";
				row["PayPlanChargeNum"]="0";
				row["ProcCode"]=Lan.g("AccountModule","Stmt");
				row["ProcNum"]="0";
				row["procsOnClaim"]="";
				row["prov"]="";
				row["StatementNum"]=rawState.Rows[i]["StatementNum"].ToString();
				row["tth"]="";
				rows.Add(row);
			}
			//Payment plans----------------------------------------------------------------------------------
			command="SELECT "
				+"(SELECT SUM(Principal) FROM payplancharge WHERE payplancharge.PayPlanNum=payplan.PayPlanNum) _principal,"
				+"(SELECT SUM(Interest) FROM payplancharge WHERE payplancharge.PayPlanNum=payplan.PayPlanNum) _interest,"
				+"(SELECT SUM(Principal) FROM payplancharge WHERE payplancharge.PayPlanNum=payplan.PayPlanNum "
					+"AND ChargeDate <= CURDATE()) _principalDue,"
				+"(SELECT SUM(Interest) FROM payplancharge WHERE payplancharge.PayPlanNum=payplan.PayPlanNum "
					+"AND ChargeDate <= CURDATE()) _interestDue,"
				+"CarrierName,payplan.Guarantor,"
				+"payplan.PatNum,PayPlanDate,payplan.PayPlanNum,"
				+"payplan.PlanNum "
				+"FROM payplan "
				+"LEFT JOIN insplan ON insplan.PlanNum=payplan.PlanNum "
				+"LEFT JOIN carrier ON carrier.CarrierNum=insplan.CarrierNum "
				+"WHERE  (";
			for(int i=0;i<fam.List.Length;i++){
				if(i!=0){
					command+="OR ";
				}
				command+="payplan.Guarantor ="+POut.PInt(fam.List[i].PatNum)+" "
					+"OR payplan.PatNum ="+POut.PInt(fam.List[i].PatNum)+" ";
			}
			command+=") GROUP BY payplan.PayPlanNum ORDER BY PayPlanDate";
			DataTable rawPayPlan=dcon.GetTable(command);
			for(int i=0;i<rawPayPlan.Rows.Count;i++){
				row=table.NewRow();
				row["AdjNum"]="0";
				row["balance"]="";//fill this later
				row["balanceDouble"]=0;//fill this later
				row["chargesDouble"]=0;
				row["charges"]="";
				row["ClaimNum"]="0";
				row["ClaimPaymentNum"]="0";
				row["colorText"]=DefB.Long[(int)DefCat.AccountColors][6].ItemColor.ToArgb().ToString();
				amt=PIn.PDouble(rawPayPlan.Rows[i]["_principal"].ToString());
				row["creditsDouble"]=amt;
				row["credits"]=((double)row["creditsDouble"]).ToString("n");
				dateT=PIn.PDateT(rawPayPlan.Rows[i]["PayPlanDate"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
				if(rawPayPlan.Rows[i]["PlanNum"].ToString()=="0"){
					row["description"]=Lan.g("ContrAccount","Payment Plan");
				}
				else{
					row["description"]=Lan.g("ContrAccount","Expected payments from ")
						+rawPayPlan.Rows[i]["CarrierName"].ToString();
				}
				row["extraDetail"]="";
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawPayPlan.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawPayPlan.Rows[i]["PatNum"].ToString();
				row["PayNum"]="0";
				row["PayPlanNum"]=rawPayPlan.Rows[i]["PayPlanNum"].ToString();
				row["PayPlanChargeNum"]="0";
				row["ProcCode"]=Lan.g("AccountModule","PayPln");
				row["ProcNum"]="0";
				row["procsOnClaim"]="";
				row["prov"]="";
				row["StatementNum"]="0";
				row["tth"]="";
				rows.Add(row);
			}
			if(isForStatement){
				GetPayPlansForStatement(rawPayPlan,rawPay,fromDate,toDate);
			}
			else{
				GetPayPlans(rawPayPlan,rawPay);
			}
			//Sorting-----------------------------------------------------------------------------------------
			rows.Sort(new AccountLineComparer());
			//rows.Sort(CompareCommRows);
			//Pass off all the rows for the whole family in order to compute the patient balances----------------
			GetPatientTable(fam,rows);
			//Regroup rows by patient---------------------------------------------------------------------------
			DataTable[] rowsByPat=null;//will only used if multiple patients not intermingled
			if(singlePatient){//This is usually used for Account module grid.
				for(int i=rows.Count-1;i>=0;i--) {//go backwards and remove from end
					if(rows[i]["PatNum"].ToString()!=patNum.ToString()){
						rows.RemoveAt(i);
					}
				}
			}
			else if(intermingled){
				//leave the rows alone
			}
			else{//multiple patients not intermingled.  This is most common for an ordinary statement.
				for(int i=0;i<rows.Count;i++){
					table.Rows.Add(rows[i]);
				}
				rowsByPat=new DataTable[fam.List.Length];
				for(int p=0;p<rowsByPat.Length;p++){
					rowsByPat[p]=new DataTable();
					SetTableColumns(rowsByPat[p]);
					for(int i=0;i<rows.Count;i++){
						if(rows[i]["PatNum"].ToString()==fam.List[p].PatNum.ToString()){
							rowsByPat[p].ImportRow(rows[i]);
						}
					}
				}
			}
			//Compute balances-------------------------------------------------------------------------------------
			double bal;
			if(rowsByPat==null){//just one table
				bal=0;
				for(int i=0;i<rows.Count;i++) {
					bal+=(double)rows[i]["chargesDouble"];
					bal-=(double)rows[i]["creditsDouble"];
					rows[i]["balanceDouble"]=bal;
					if(rows[i]["ClaimPaymentNum"].ToString()=="0" && rows[i]["ClaimNum"].ToString()!="0"){//claims
						rows[i]["balance"]="";
					}
					else if(rows[i]["StatementNum"].ToString()!="0"){

					}
					else{
						rows[i]["balance"]=bal.ToString("n");
					}
				}
			}
			else{
				for(int p=0;p<rowsByPat.Length;p++){
					bal=0;
					for(int i=0;i<rowsByPat[p].Rows.Count;i++) {
						bal+=(double)rowsByPat[p].Rows[i]["chargesDouble"];
						bal-=(double)rowsByPat[p].Rows[i]["creditsDouble"];
						rowsByPat[p].Rows[i]["balanceDouble"]=bal;
						if(rowsByPat[p].Rows[i]["ClaimPaymentNum"].ToString()=="0" 
							&& rowsByPat[p].Rows[i]["ClaimNum"].ToString()!="0")//claims
						{
							rowsByPat[p].Rows[i]["balance"]="";
						}
						else if(rowsByPat[p].Rows[i]["StatementNum"].ToString()!="0"){

						}
						else{
							rowsByPat[p].Rows[i]["balance"]=bal.ToString("n");
						}
					}
				}
			}
			//Remove rows outside of daterange-------------------------------------------------------------------
			double balanceForward=0;
			bool foundBalForward;
			if(rowsByPat==null){
				balanceForward=0;
				foundBalForward=false;
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
				//Add balance forward row
				if(foundBalForward){
					//add a balance forward row
					row=table.NewRow();
					SetBalForwardRow(row,balanceForward);
					rows.Insert(0,row);
				}
			}
			else{
				for(int p=0;p<rowsByPat.Length;p++){
					balanceForward=0;
					foundBalForward=false;
					for(int i=rowsByPat[p].Rows.Count-1;i>=0;i--) {//go backwards and remove from end
						if(((DateTime)rowsByPat[p].Rows[i]["DateTime"])>toDate){
							rowsByPat[p].Rows.RemoveAt(i);
						}
						if(((DateTime)rowsByPat[p].Rows[i]["DateTime"])<fromDate){
							if(!foundBalForward){
								foundBalForward=true;
								balanceForward=(double)rowsByPat[p].Rows[i]["balanceDouble"];
							}
							rowsByPat[p].Rows.RemoveAt(i);
						}
					}
					//Add balance forward row
					if(foundBalForward){
						//add a balance forward row
						row=rowsByPat[p].NewRow();
						SetBalForwardRow(row,balanceForward);
						rowsByPat[p].Rows.InsertAt(row,0);
					}
				}
			}
			//Finally, add rows to new table(s)-----------------------------------------------------------------------
			if(rowsByPat==null){
				table.Rows.Clear();
				for(int i=0;i<rows.Count;i++) {
					table.Rows.Add(rows[i]);
				}
				retVal.Tables.Add(table);
			}
			else{
				for(int p=0;p<rowsByPat.Length;p++){
					if(p>0 && isForStatement && fam.List[p].PatStatus==PatientStatus.Deceased && fam.List[p].EstBalance==0){
						continue;
					}
					DataTable tablep=new DataTable("account"+fam.List[p].PatNum.ToString());
					SetTableColumns(tablep);
					for(int i=0;i<rowsByPat[p].Rows.Count;i++) {
						tablep.ImportRow(rowsByPat[p].Rows[i]);
					}
					retVal.Tables.Add(tablep);
				}
			}
			//DataView view = table.DefaultView;
			//view.Sort = "DateTime";
			//table = view.ToTable();
			//return table;
		}

		private static void SetBalForwardRow(DataRow row,double amt){
			row["AdjNum"]="0";
			row["balance"]=amt.ToString("n");
			row["balanceDouble"]=amt;
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
			row["extraDetail"]="";
			row["patient"]="";
			row["PatNum"]="0";
			row["PayNum"]="0";
			row["PayPlanNum"]="0";
			row["PayPlanChargeNum"]="0";
			row["ProcCode"]="";
			row["ProcNum"]="0";
			row["procsOnClaim"]="";
			row["prov"]="";
			row["StatementNum"]="0";
			row["tth"]="";
		}

		///<summary>Gets payment plans for the family.  RawPay will include any paysplits for anyone in the family, so it's guaranteed to include all paysplits for a given payplan since payplans only show in the guarantor's family.  Database maint tool enforces paysplit.patnum=payplan.guarantor just in case. </summary>
		private static void GetPayPlans(DataTable rawPayPlan,DataTable rawPay){
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("payplan");
			DataRow row;
			table.Columns.Add("accumDue");
			table.Columns.Add("balance");
			table.Columns.Add("date");
			table.Columns.Add("DateTime",typeof(DateTime));
			table.Columns.Add("due");
			table.Columns.Add("guarantor");
			table.Columns.Add("isIns");
			table.Columns.Add("paid");
			table.Columns.Add("patient");
			table.Columns.Add("PayPlanNum");
			table.Columns.Add("principal");
			table.Columns.Add("princPaid");
			table.Columns.Add("totalCost");
			List<DataRow> rows=new List<DataRow>();
			DateTime dateT;
			double paid;
			double princ;
			double princDue;
			double interestDue;
			double accumDue;
			double princPaid;
			double totCost;
			double due;
			double balance;
			for(int i=0;i<rawPayPlan.Rows.Count;i++){
				//first, calculate the numbers-------------------------------------------------------------
				paid=0;
				for(int p=0;p<rawPay.Rows.Count;p++){
					if(rawPay.Rows[p]["PayPlanNum"].ToString()==rawPayPlan.Rows[i]["PayPlanNum"].ToString()){
						paid+=PIn.PDouble(rawPay.Rows[p]["SplitAmt"].ToString());
					}
				}
				princ=PIn.PDouble(rawPayPlan.Rows[i]["_principal"].ToString());
				princDue=PIn.PDouble(rawPayPlan.Rows[i]["_principalDue"].ToString());
				interestDue=PIn.PDouble(rawPayPlan.Rows[i]["_interestDue"].ToString());
				accumDue=princDue+interestDue;
				princPaid=paid-interestDue;
				if(princPaid<0){
					princPaid=0;
				}
				totCost=princ+PIn.PDouble(rawPayPlan.Rows[i]["_interest"].ToString());
				due=accumDue-paid;
				balance=princ-princPaid;
				//then fill the row----------------------------------------------------------------------
				row=table.NewRow();
				row["accumDue"]=accumDue.ToString("n");
				row["balance"]=balance.ToString("n");
				dateT=PIn.PDateT(rawPayPlan.Rows[i]["PayPlanDate"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
				row["due"]=due.ToString("n");
				row["guarantor"]=fam.GetNameInFamLF(PIn.PInt(rawPayPlan.Rows[i]["Guarantor"].ToString()));
				if(rawPayPlan.Rows[i]["PlanNum"].ToString()=="0"){
					row["isIns"]="";
				}
				else{
					row["isIns"]="X";
				}
				row["paid"]=paid.ToString("n");
				row["patient"]=fam.GetNameInFamLF(PIn.PInt(rawPayPlan.Rows[i]["PatNum"].ToString()));
				row["PayPlanNum"]=rawPayPlan.Rows[i]["PayPlanNum"].ToString();
				row["principal"]=princ.ToString("n");
				row["princPaid"]=princPaid.ToString("n");
				row["totalCost"]=totCost.ToString("n");
				rows.Add(row);
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			retVal.Tables.Add(table);
		}

		///<summary>Gets payment plans for the family.  RawPay will include any paysplits for anyone in the family, so it's guaranteed to include all paysplits for a given payplan since payplans only show in the guarantor's family.  Database maint tool enforces paysplit.patnum=payplan.guarantor just in case.  fromDate and toDate are only used if isForStatement.  From date lets us restrict how many amortization items to show.  toDate is typically 10 days in the future.</summary>
		private static void GetPayPlansForStatement(DataTable rawPayPlan,DataTable rawPay,DateTime fromDate,DateTime toDate){
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("payplan");
			DataRow row;
			SetTableColumns(table);//this will allow it to later be fully integrated into a single grid.
			List<DataRow> rows=new List<DataRow>();
			double princ;
			DataTable rawAmort;
			int payPlanNum;
			for(int i=0;i<rawPayPlan.Rows.Count;i++){//loop through the payment plans (usually zero or one)
				princ=PIn.PDouble(rawPayPlan.Rows[i]["_principal"].ToString());
				//summary row----------------------------------------------------------------------
				row=table.NewRow();
				row["AdjNum"]="0";
				row["balance"]="";
				row["balanceDouble"]=0;
				row["chargesDouble"]=0;
				row["charges"]="";
				row["ClaimNum"]="0";
				row["ClaimPaymentNum"]="0";
				row["colorText"]=Color.Black.ToArgb().ToString();
				row["creditsDouble"]=0;
				row["credits"]="";
				row["DateTime"]=DateTime.MinValue;
				row["date"]="";
				row["description"]=Lan.g("AccountModule","Payment Plan.  Total loan amount: ")+princ.ToString("c");
				row["extraDetail"]="";
				row["patient"]="";
				row["PatNum"]="0";
				row["PayNum"]="0";
				row["PayPlanNum"]="0";
				row["PayPlanChargeNum"]="0";
				row["ProcCode"]="";
				row["ProcNum"]="0";
				row["procsOnClaim"]="";
				row["prov"]="";
				row["StatementNum"]="0";
				row["tth"]="";
				rows.Add(row);
				//detail rows-------------------------------------------------------------------------------
				payPlanNum=PIn.PInt(rawPayPlan.Rows[i]["PayPlanNum"].ToString());
				rawAmort = null;// GetPayPlanAmortTable(payPlanNum);
				//remove rows out of date range, going backwards
				for(int d=rawAmort.Rows.Count-1;d>=0;d--){
					if((DateTime)rawAmort.Rows[d]["DateTime"]>toDate.AddDays(PrefB.GetInt("PayPlansBillInAdvanceDays"))){
						rawAmort.Rows.RemoveAt(d);
					}
					else if((DateTime)rawAmort.Rows[d]["DateTime"]<fromDate){
						rawAmort.Rows.RemoveAt(d);
					}
				}
				for(int d=0;d<rawAmort.Rows.Count;d++){
					row=table.NewRow();
					row["AdjNum"]="0";
					row["balance"]=rawAmort.Rows[d]["balance"];
					row["balanceDouble"]=rawAmort.Rows[d]["balanceDouble"];
					row["chargesDouble"]=rawAmort.Rows[d]["chargesDouble"];
					row["charges"]=rawAmort.Rows[d]["charges"];
					row["ClaimNum"]="0";
					row["ClaimPaymentNum"]="0";
					row["colorText"]=Color.Black.ToArgb().ToString();
					row["creditsDouble"]=rawAmort.Rows[d]["creditsDouble"];
					row["credits"]=rawAmort.Rows[d]["credits"];
					row["DateTime"]=rawAmort.Rows[d]["DateTime"];
					row["date"]=rawAmort.Rows[d]["date"];
					row["description"]=rawAmort.Rows[d]["description"];
					row["extraDetail"]="";
					row["patient"]=rawAmort.Rows[d]["patient"];
					row["PatNum"]=rawAmort.Rows[d]["PatNum"];
					row["PayNum"]=rawAmort.Rows[d]["PayNum"];
					row["PayPlanNum"]="0";
					row["PayPlanChargeNum"]=rawAmort.Rows[d]["PayPlanChargeNum"];
					row["ProcCode"]="";
					row["ProcNum"]="0";
					row["procsOnClaim"]="";
					row["prov"]=rawAmort.Rows[d]["prov"];
					row["StatementNum"]="0";
					row["tth"]="";
					rows.Add(row);
				}
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
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
				if(bal!=fam.List[p].EstBalance){
					Patient patnew=fam.List[p].Copy();
					patnew.EstBalance=bal;
					Patients.Update(patnew,fam.List[p]);
				}
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

		///<summary>Future appointments.</summary>
		private static void GetApptTable(Family fam,bool singlePatient,int patNum){
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("appts");
			DataRow row;
			table.Columns.Add("descript");
			table.Columns.Add("PatNum");
			List<DataRow> rows=new List<DataRow>();
			string command="SELECT AptDateTime,PatNum,ProcDescript "
				+"FROM appointment "
				+"WHERE AptDateTime > "+POut.PDate(DateTime.Today.AddDays(1))+" "//midnight tonight
				+"AND AptStatus !="+POut.PInt((int)ApptStatus.PtNote)+" "
				+"AND AptStatus !="+POut.PInt((int)ApptStatus.PtNoteCompleted)+" "
				+"AND AptStatus !="+POut.PInt((int)ApptStatus.UnschedList)+" "
				+"AND (";
			if(singlePatient){
				command+="PatNum ="+POut.PInt(patNum);
			}
			else{
				for(int i=0;i<fam.List.Length;i++){
					if(i!=0){
						command+="OR ";
					}
					command+="PatNum ="+POut.PInt(fam.List[i].PatNum)+" ";
				}
			}
			command+=") ORDER BY PatNum,AptDateTime";
			DataTable raw=dcon.GetTable(command);
			DateTime dateT;
			int patNumm;
			for(int i=0;i<raw.Rows.Count;i++){
				row=table.NewRow();
				patNumm=PIn.PInt(raw.Rows[i]["PatNum"].ToString());
				dateT=PIn.PDateT(raw.Rows[i]["AptDateTime"].ToString());
				row["descript"]=fam.GetNameInFamFL(patNumm)+":  "
					+dateT.ToString("dddd")+",  "
					+dateT.ToShortDateString()
					+",  "+dateT.ToShortTimeString()+",  "+raw.Rows[i]["ProcDescript"].ToString();
				row["PatNum"]=patNumm.ToString();
				rows.Add(row);
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
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
		///<summary>A generic comparison that sorts the rows of the account table by date and type.</summary>
		public int Compare (DataRow rowA,DataRow rowB){
			//if dates are different, then sort by date
			if((DateTime)rowA["DateTime"]!=(DateTime)rowB["DateTime"]){
				return ((DateTime)rowA["DateTime"]).CompareTo((DateTime)rowB["DateTime"]);
			}
			//Procedures come before other types
			if(rowA["ProcNum"].ToString()!="0" && rowB["ProcNum"].ToString()=="0"){
				return -1;
			}
			if(rowA["ProcNum"].ToString()=="0" && rowB["ProcNum"].ToString()!="0"){
				return 1;
			}
			//PayPlanCharges come before other types on same date, but rare to be on same date anyway.
			//if(rowA["PayPlanChargeNum"].ToString()!="0" && rowB["PayNum"].ToString()=="0"){
			//	return -1;
			//}
			//if(rowA["PayNum"].ToString()=="0" && rowB["PayPlanChargeNum"].ToString()!="0"){
			//	return 1;
			//}
			return 0;
		}
	}

	/*
	///<summary>A generic comparison that sorts the rows of the payplanamort table by date and type.</summary>
	class PayPlanLineComparer : IComparer<DataRow>	{
		///<summary>A generic comparison that sorts the rows of the payplanamort table by date and type.</summary>
		public int Compare (DataRow rowA,DataRow rowB){
			//if dates are different, then sort by date
			if((DateTime)rowA["DateTime"]!=(DateTime)rowB["DateTime"]){
				return ((DateTime)rowA["DateTime"]).CompareTo((DateTime)rowB["DateTime"]);
			}
			//Charges come before paysplits, but rare to be on same date anyway.
			if(rowA["PayPlanChargeNum"].ToString()!="0" && rowB["PaySplitNum"].ToString()=="0"){
				return -1;
			}
			if(rowA["PaySplitNum"].ToString()=="0" && rowB["PayPlanChargeNum"].ToString()!="0"){
				return 1;
			}
			return 0;
		}
	}*/


}
