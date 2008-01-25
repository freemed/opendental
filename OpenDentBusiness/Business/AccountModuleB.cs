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

		///<summary>Parameters: 0:patNum, 1:viewingInRecall, 2:fromDate, 3:toDate, 4:isFamily.  If isFamily=1, also pass in a PatNum of guarantor to get entire family intermingled.</summary>
		public static DataSet GetAll(string[] parameters){
			int patNum=PIn.PInt(parameters[0]);
			fam=Patients.GetFamily(patNum);
			pat=fam.GetPatient(patNum);
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
			GetAccount(patNum,fromDate,toDate,isFamily);//Gets 2 tables: account(or account###,account###,etc), patient.
			//GetPayPlans(patNum,fromDate,toDate,isFamily);
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
				row["CommlogNum"]="0";
				row["creditsDouble"]=0;
				row["credits"]="";//((double)row["creditsDouble"]).ToString("n");
				dateT=PIn.PDateT(rawCharge.Rows[i]["ChargeDate"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
				row["description"]="";//"Princ: "+principal.ToString("n")+
				if(interest!=0){
					row["description"]+="Interest: "+interest.ToString("n");//+"Princ: "+principal.ToString("n")+;
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
				row["CommlogNum"]="0";
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

		private static void GetPayPlanCharges(){
			/*
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
			DataTable rawPayPlan=dcon.GetTable(command);*/
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
			table.Columns.Add("CommlogNum");
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
			table.Columns.Add("tth");
		}
		
		///<summary>Also gets the patient table, which has one row for each family member. Also currently runs aging.  Also gets payplan table.</summary>
		private static void GetAccount(int patNum,DateTime fromDate,DateTime toDate,bool isFamily) {
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
				,Ledgers.Bal[3],Ledgers.InsEst,Ledgers.BalTotal);
			//Now, back to getting the tables------------------------------------------------------------------
			DataRow row;
			SetTableColumns(table);
			//but we won't actually fill this table with rows until the very end.  It's more useful to use a List<> for now.
			List<DataRow> rows=new List<DataRow>();
			//Procedures------------------------------------------------------------------------------------------
			string command="SELECT procedurelog.BaseUnits,Descript,LaymanTerm,procedurelog.MedicalCode,procedurelog.PatNum,ProcCode,"
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
				row["CommlogNum"]="0";
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
				row["extraDetail"]="extra detail";
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawProc.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawProc.Rows[i]["PatNum"].ToString();
				row["PayNum"]="0";
				row["PayPlanNum"]="0";
				row["PayPlanChargeNum"]="0";
				row["ProcCode"]=rawProc.Rows[i]["ProcCode"].ToString();
				row["ProcNum"]=rawProc.Rows[i]["ProcNum"].ToString();
				row["procsOnClaim"]="";
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
				row["CommlogNum"]="0";
				dateT=PIn.PDateT(rawAdj.Rows[i]["AdjDate"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
				row["description"]=DefB.GetName(DefCat.AdjTypes,PIn.PInt(rawAdj.Rows[i]["AdjType"].ToString()));
				row["extraDetail"]="";
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawAdj.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawAdj.Rows[i]["PatNum"].ToString();
				row["PayNum"]="0";
				row["PayPlanNum"]="0";
				row["PayPlanChargeNum"]="0";
				row["ProcCode"]=Lan.g("AccountModule","Adjust");
				row["ProcNum"]="0";
				row["procsOnClaim"]="";
				row["prov"]=Providers.GetAbbr(PIn.PInt(rawAdj.Rows[i]["ProvNum"].ToString()));
				row["tth"]="";
				rows.Add(row);
			}
			//paysplits-----------------------------------------------------------------------------------------
			command="SELECT CheckNum,DatePay,paysplit.PatNum,PayAmt,paysplit.PayNum,PayPlanNum,"
				+"PayType,ProcDate,ProvNum,SplitAmt "
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
				row["CommlogNum"]="0";
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
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawPay.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawPay.Rows[i]["PatNum"].ToString();
				row["PayNum"]=rawPay.Rows[i]["PayNum"].ToString();
				row["PayPlanNum"]="0";
				row["PayPlanChargeNum"]="0";
				row["ProcCode"]=Lan.g("AccountModule","Pay");
				row["ProcNum"]="0";
				row["procsOnClaim"]="";
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
//todo(maybe): change this color. 3 means payment. 4 means claim.  get a new color for inspayments.
				row["colorText"]=DefB.Long[(int)DefCat.AccountColors][4].ItemColor.ToArgb().ToString();
				row["CommlogNum"]="0";
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
				row["colorText"]=DefB.Long[(int)DefCat.AccountColors][4].ItemColor.ToArgb().ToString();
				row["CommlogNum"]="0";
				row["creditsDouble"]=0;
				row["credits"]="";
				dateT=PIn.PDateT(rawClaim.Rows[i]["DateService"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
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
				daterec=PIn.PDateT(rawClaim.Rows[i]["DateReceived"].ToString());
				if(daterec.Year>1880){//and claimstatus=R
					row["description"]+="\r\n"+Lan.g("ContrAccount","Received")+" "+daterec.ToShortDateString();
				}
				else if(rawClaim.Rows[i]["ClaimStatus"].ToString()=="U"){
					row["description"]+="\r\n"+Lan.g("ContrAccount","Unsent");
				}
				else if(rawClaim.Rows[i]["ClaimStatus"].ToString()=="H"){
					row["description"]+="\r\n"+Lan.g("ContrAccount","Hold until Pri received");
				}
				else if(rawClaim.Rows[i]["ClaimStatus"].ToString()=="W"){
					row["description"]+="\r\n"+Lan.g("ContrAccount","Waiting to Send");
				}
				//else if(rawClaim.Rows[i]["ClaimStatus"].ToString()=="S"){
				//	row["description"]+="\r\n"+Lan.g("ContrAccount","Sent");
				//}
				insest=PIn.PDouble(rawClaim.Rows[i]["InsPayEst"].ToString());
				if(rawClaim.Rows[i]["ClaimStatus"].ToString()=="W"
					|| rawClaim.Rows[i]["ClaimStatus"].ToString()=="S")
				{
					row["description"]+="\r\n"+Lan.g("ContrAccount","Estimated Payment:")+" "+insest.ToString("c");
				}
				amtpaid=PIn.PDouble(rawClaim.Rows[i]["InsPayAmt"].ToString());
				if(amtpaid!=0){
					row["description"]+="\r\n"+Lan.g("ContrAccount","Payment:")+" "+amtpaid.ToString("c");
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
				row["tth"]="";
				rows.Add(row);
			}
			//Commlog----------------------------------------------------------------------------------------
			command="SELECT CommDateTime,Mode_,CommlogNum,PatNum "
				+"FROM commlog "
				+"WHERE IsStatementSent=1 "
				+"AND (";
			for(int i=0;i<fam.List.Length;i++){
				if(i!=0){
					command+="OR ";
				}
				command+="PatNum ="+POut.PInt(fam.List[i].PatNum)+" ";
			}
			command+=") ORDER BY CommDateTime";
			DataTable rawComm=dcon.GetTable(command);
			CommItemMode _mode;
			for(int i=0;i<rawComm.Rows.Count;i++){
				row=table.NewRow();
				row["AdjNum"]="0";
				row["balance"]="";//fill this later
				row["balanceDouble"]=0;//fill this later
				row["chargesDouble"]=0;
				row["charges"]="";
				row["ClaimNum"]="0";
				row["ClaimPaymentNum"]="0";
				row["colorText"]=DefB.Long[(int)DefCat.AccountColors][5].ItemColor.ToArgb().ToString();
				row["CommlogNum"]=rawComm.Rows[i]["CommlogNum"].ToString();
				row["creditsDouble"]=0;
				row["credits"]="";
				dateT=PIn.PDateT(rawComm.Rows[i]["CommDateTime"].ToString());
				row["DateTime"]=dateT;
				row["date"]=dateT.ToShortDateString();
				row["description"]+=Lan.g("ContrAccount","Sent Statement");
				_mode=(CommItemMode)PIn.PInt(rawComm.Rows[i]["Mode_"].ToString());
				if(_mode!=CommItemMode.None){
					row["description"]+="-"+Lan.g("enumCommItemMode",_mode.ToString());
				}
				row["extraDetail"]="";
				row["patient"]=fam.GetNameInFamFirst(PIn.PInt(rawComm.Rows[i]["PatNum"].ToString()));
				row["PatNum"]=rawComm.Rows[i]["PatNum"].ToString();
				row["PayNum"]="0";
				row["PayPlanNum"]="0";
				row["PayPlanChargeNum"]="0";
				row["ProcCode"]=Lan.g("AccountModule","Comm");
				row["ProcNum"]="0";
				row["procsOnClaim"]="";
				row["prov"]="";
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
				row["CommlogNum"]="0";
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
				row["tth"]="";
				rows.Add(row);
			}
			GetPayPlans(rawPayPlan,rawPay);
			//Sorting-----------------------------------------------------------------------------------------
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
				else if(rows[i]["CommlogNum"].ToString()!="0"){

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
				row["CommlogNum"]="0";
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
				row["ProcCode"]="";
				row["ProcNum"]="0";
				row["procsOnClaim"]="";
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

		///<summary>Gets payment plans for the family.  RawPay will include any paysplits for anyone in the family, so it's guaranteed to include all paysplits for a given payplan since payplans only show in the guarantor's family.  Database maint tool enforces paysplit.patnum=payplan.guarantor just in case.</summary>
		private static void GetPayPlans(DataTable rawPayPlan,DataTable rawPay){
			//,int patNum,DateTime fromDate,DateTime toDate,bool isFamily){
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
			if(rowA["PayPlanChargeNum"].ToString()!="0" && rowB["PayNum"].ToString()=="0"){
				return -1;
			}
			if(rowA["PayNum"].ToString()=="0" && rowB["PayPlanChargeNum"].ToString()!="0"){
				return 1;
			}
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
