using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Claims{
		///<summary></summary>
		public static Claim[] List;
		///<summary></summary>
		public static Hashtable HList;
		
		///<summary></summary>
		public static ClaimPaySplit[] RefreshByCheck(int claimPaymentNum, bool showUnattached){
			string command=
				"SELECT claim.DateService,claim.ProvTreat,CONCAT(CONCAT(patient.LName,', '),patient.FName) AS PatName"
				+",carrier.CarrierName,SUM(claimproc.FeeBilled),SUM(claimproc.InsPayAmt),claim.ClaimNum"
				+",claimproc.ClaimPaymentNum"
				+" FROM claim,patient,insplan,carrier,claimproc" // added carrier, SPK 8/04
				+" WHERE claimproc.ClaimNum = claim.ClaimNum"
				+" AND patient.PatNum = claim.PatNum"
				+" AND insplan.PlanNum = claim.PlanNum"
				+" AND insplan.CarrierNum = carrier.CarrierNum"	// added SPK
				+" AND (claimproc.Status = '1' OR claimproc.Status = '4')"//received or supplemental
 				+" AND (claimproc.ClaimPaymentNum = '"+POut.PInt(claimPaymentNum)+"'";
			if(showUnattached){
				command+=" OR (claimproc.InsPayAmt != 0 AND claimproc.ClaimPaymentNum = '0'))"
					+" GROUP BY claimproc.ClaimNum";
			}
			else{//shows only items attached to this payment
				command+=")"
					+" GROUP BY claimproc.ClaimNum";
			}
			command+=" ORDER BY PatName";
			//MessageBox.Show(
			DataTable table=General.GetTable(command);
			ClaimPaySplit[] splits=new ClaimPaySplit[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				splits[i]=new ClaimPaySplit();
				splits[i].DateClaim      =PIn.PDate  (table.Rows[i][0].ToString());
				splits[i].ProvAbbr       =Providers.GetAbbr(PIn.PInt(table.Rows[i][1].ToString()));
				splits[i].PatName        =PIn.PString(table.Rows[i][2].ToString());
				splits[i].Carrier        =PIn.PString(table.Rows[i][3].ToString());
				splits[i].FeeBilled      =PIn.PDouble(table.Rows[i][4].ToString());
				splits[i].InsPayAmt      =PIn.PDouble(table.Rows[i][5].ToString());
				splits[i].ClaimNum       =PIn.PInt   (table.Rows[i][6].ToString());
				splits[i].ClaimPaymentNum=PIn.PInt   (table.Rows[i][7].ToString());
			}
			return splits;
		}

		///<summary>Gets the specified claim from the database.</summary>
		public static Claim GetClaim(int claimNum){
			string command="SELECT * FROM claim"
				+" WHERE ClaimNum = "+claimNum.ToString();
			Claim retClaim=SubmitAndFill(command,true);
			return retClaim;
		}

		///<summary>Gets all claims for the specified patient.</summary>
		public static void Refresh(int patNum){
			string command=
				"SELECT * FROM claim"
				+" WHERE PatNum = "+patNum.ToString()
				+" ORDER BY dateservice";
			SubmitAndFill(command,false);
		}

		public static Claim[] GetAllClaims(int patNum){
		  string command=
		    "SELECT * FROM claim"
		    +" WHERE PatNum = '"+patNum.ToString()+"'";
			//DataTable table=General.NonQ(command);
			//Claim tempClaim;
			SubmitAndFill(command,false);
			return List;
		}

		private static Claim SubmitAndFill(string command,bool single){
			DataTable table=General.GetTable(command);
			Claim tempClaim;
			if(!single){
				List=new Claim[table.Rows.Count];
				HList=new Hashtable();
			}
			Claim retVal=new Claim();
			for(int i=0;i<table.Rows.Count;i++){
				tempClaim=new Claim();
				tempClaim.ClaimNum     =		PIn.PInt   (table.Rows[i][0].ToString());
				tempClaim.PatNum       =		PIn.PInt   (table.Rows[i][1].ToString());
				tempClaim.DateService  =		PIn.PDate  (table.Rows[i][2].ToString());
				tempClaim.DateSent     =		PIn.PDate  (table.Rows[i][3].ToString());
				tempClaim.ClaimStatus  =		PIn.PString(table.Rows[i][4].ToString());
				tempClaim.DateReceived =		PIn.PDate  (table.Rows[i][5].ToString());
				tempClaim.PlanNum      =		PIn.PInt   (table.Rows[i][6].ToString());
				tempClaim.ProvTreat    =		PIn.PInt   (table.Rows[i][7].ToString());
				tempClaim.ClaimFee     =		PIn.PDouble(table.Rows[i][8].ToString());
				tempClaim.InsPayEst    =		PIn.PDouble(table.Rows[i][9].ToString());
				tempClaim.InsPayAmt    =		PIn.PDouble(table.Rows[i][10].ToString());
				tempClaim.DedApplied   =		PIn.PDouble(table.Rows[i][11].ToString());
				tempClaim.PreAuthString=		PIn.PString(table.Rows[i][12].ToString());
				tempClaim.IsProsthesis =		PIn.PString(table.Rows[i][13].ToString());
				tempClaim.PriorDate    =		PIn.PDate  (table.Rows[i][14].ToString());
				tempClaim.ReasonUnderPaid=	PIn.PString(table.Rows[i][15].ToString());
				tempClaim.ClaimNote    =		PIn.PString(table.Rows[i][16].ToString());
				tempClaim.ClaimType    =    PIn.PString(table.Rows[i][17].ToString());
				tempClaim.ProvBill     =		PIn.PInt   (table.Rows[i][18].ToString());
				tempClaim.ReferringProv=		PIn.PInt   (table.Rows[i][19].ToString());
				tempClaim.RefNumString =		PIn.PString(table.Rows[i][20].ToString());
				tempClaim.PlaceService = (PlaceOfService)PIn.PInt(table.Rows[i][21].ToString());
				tempClaim.AccidentRelated=	PIn.PString(table.Rows[i][22].ToString());
				tempClaim.AccidentDate  =		PIn.PDate  (table.Rows[i][23].ToString());
				tempClaim.AccidentST    =		PIn.PString(table.Rows[i][24].ToString());
				tempClaim.EmployRelated=(YN)PIn.PInt   (table.Rows[i][25].ToString());
				tempClaim.IsOrtho       =		PIn.PBool  (table.Rows[i][26].ToString());
				tempClaim.OrthoRemainM  =		PIn.PInt   (table.Rows[i][27].ToString());
				tempClaim.OrthoDate     =		PIn.PDate  (table.Rows[i][28].ToString());
				tempClaim.PatRelat      =(Relat)PIn.PInt(table.Rows[i][29].ToString());
				tempClaim.PlanNum2      =   PIn.PInt   (table.Rows[i][30].ToString());
				tempClaim.PatRelat2     =(Relat)PIn.PInt(table.Rows[i][31].ToString());
				tempClaim.WriteOff      =   PIn.PDouble(table.Rows[i][32].ToString());
				tempClaim.Radiographs   =   PIn.PInt   (table.Rows[i][33].ToString());
				tempClaim.ClinicNum     =   PIn.PInt   (table.Rows[i][34].ToString());
				tempClaim.ClaimForm     =   PIn.PInt   (table.Rows[i][35].ToString());
				tempClaim.EFormat       =(EtransType)PIn.PInt(table.Rows[i][36].ToString());
				if(single){
					retVal=tempClaim;
				}
				else{
					List[i]=tempClaim.Copy();
					HList.Add(tempClaim.ClaimNum,tempClaim.Copy());
				}
			}//end for
			return retVal;//only really used if single
		}

		///<summary></summary>
		public static void Insert(Claim Cur){
			if(PrefB.RandomKeys){
				Cur.ClaimNum=MiscData.GetKey("claim","ClaimNum");
			}
			string command="INSERT INTO claim (";
			if(PrefB.RandomKeys){
				command+="ClaimNum,";
			}
			command+="patnum,dateservice,datesent,claimstatus,datereceived"
				+",plannum,provtreat,claimfee,inspayest,inspayamt,dedapplied"
				+",preauthstring,isprosthesis,priordate,reasonunderpaid,claimnote"
				+",claimtype,provbill,referringprov"
				+",refnumstring,placeservice,accidentrelated,accidentdate,accidentst"
				+",employrelated,isortho,orthoremainm,orthodate,patrelat,plannum2"
				+",patrelat2,writeoff,Radiographs,ClinicNum,ClaimForm,EFormat) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(Cur.ClaimNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (Cur.PatNum)+"', "
				+POut.PDate  (Cur.DateService)+", "
				+POut.PDate  (Cur.DateSent)+", "
				+"'"+POut.PString(Cur.ClaimStatus)+"', "
				+POut.PDate  (Cur.DateReceived)+", "
				+"'"+POut.PInt   (Cur.PlanNum)+"', "
				+"'"+POut.PInt   (Cur.ProvTreat)+"', "
				+"'"+POut.PDouble(Cur.ClaimFee)+"', "
				+"'"+POut.PDouble(Cur.InsPayEst)+"', "
				+"'"+POut.PDouble(Cur.InsPayAmt)+"', "
				+"'"+POut.PDouble(Cur.DedApplied)+"', "
				+"'"+POut.PString(Cur.PreAuthString)+"', "
				+"'"+POut.PString(Cur.IsProsthesis)+"', "
				+POut.PDate  (Cur.PriorDate)+", "
				+"'"+POut.PString(Cur.ReasonUnderPaid)+"', "
				+"'"+POut.PString(Cur.ClaimNote)+"', "
				+"'"+POut.PString(Cur.ClaimType)+"', "
				+"'"+POut.PInt   (Cur.ProvBill)+"', "
				+"'"+POut.PInt   (Cur.ReferringProv)+"', "
				+"'"+POut.PString(Cur.RefNumString)+"', "
				+"'"+POut.PInt   ((int)Cur.PlaceService)+"', "
				+"'"+POut.PString(Cur.AccidentRelated)+"', "
				+POut.PDate  (Cur.AccidentDate)+", "
				+"'"+POut.PString(Cur.AccidentST)+"', "
				+"'"+POut.PInt   ((int)Cur.EmployRelated)+"', "
				+"'"+POut.PBool  (Cur.IsOrtho)+"', "
				+"'"+POut.PInt   (Cur.OrthoRemainM)+"', "
				+POut.PDate  (Cur.OrthoDate)+", "
				+"'"+POut.PInt   ((int)Cur.PatRelat)+"', "
				+"'"+POut.PInt   (Cur.PlanNum2)+"', "
				+"'"+POut.PInt   ((int)Cur.PatRelat2)+"', "
				+"'"+POut.PDouble(Cur.WriteOff)+"', "
				+"'"+POut.PInt   (Cur.Radiographs)+"', "
				+"'"+POut.PInt   (Cur.ClinicNum)+"', "
				+"'"+POut.PInt   (Cur.ClaimForm)+"', "
				+"'"+POut.PInt   ((int)Cur.EFormat)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
				Cur.ClaimNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(Claim Cur){
			string command= "UPDATE claim SET "
				+"patnum = '"          +POut.PInt   (Cur.PatNum)+"' "
				+",dateservice = "    +POut.PDate  (Cur.DateService)+" "
				+",datesent = "       +POut.PDate  (Cur.DateSent)+" "
				+",claimstatus = '"    +POut.PString(Cur.ClaimStatus)+"' "
				+",datereceived = "   +POut.PDate  (Cur.DateReceived)+" "
				+",plannum = '"        +POut.PInt   (Cur.PlanNum)+"' "
				+",provtreat = '"      +POut.PInt   (Cur.ProvTreat)+"' "
				+",claimfee = '"       +POut.PDouble(Cur.ClaimFee)+"' "
				+",inspayest = '"      +POut.PDouble(Cur.InsPayEst)+"' "
				+",inspayamt = '"      +POut.PDouble(Cur.InsPayAmt)+"' "
				+",dedapplied = '"   +  POut.PDouble(Cur.DedApplied)+"' "
				+",preauthstring = '"+	POut.PString(Cur.PreAuthString)+"' "
				+",isprosthesis = '" +	POut.PString(Cur.IsProsthesis)+"' "
				+",priordate = "    +	POut.PDate  (Cur.PriorDate)+" "
				+",reasonunderpaid = '"+POut.PString(Cur.ReasonUnderPaid)+"' "
				+",claimnote = '"    +	POut.PString(Cur.ClaimNote)+"' "
				+",claimtype='"      +	POut.PString(Cur.ClaimType)+"' "
				+",provbill = '"     +	POut.PInt   (Cur.ProvBill)+"' "
				+",referringprov = '"+	POut.PInt   (Cur.ReferringProv)+"' "
				+",refnumstring = '" +	POut.PString(Cur.RefNumString)+"' "
				+",placeservice = '" +	POut.PInt   ((int)Cur.PlaceService)+"' "
				+",accidentrelated = '"+POut.PString(Cur.AccidentRelated)+"' "
				+",accidentdate = " +	POut.PDate  (Cur.AccidentDate)+" "
				+",accidentst = '"   +	POut.PString(Cur.AccidentST)+"' "
				+",employrelated = '"+	POut.PInt   ((int)Cur.EmployRelated)+"' "
				+",isortho = '"      +	POut.PBool  (Cur.IsOrtho)+"' "
				+",orthoremainm = '" +	POut.PInt   (Cur.OrthoRemainM)+"' "
				+",orthodate = "    +	POut.PDate  (Cur.OrthoDate)+" "
				+",patrelat = '"     +	POut.PInt   ((int)Cur.PatRelat)+"' "
				+",plannum2 = '"     +	POut.PInt   (Cur.PlanNum2)+"' "
				+",patrelat2 = '"    +	POut.PInt   ((int)Cur.PatRelat2)+"' "
				+",writeoff = '"     +	POut.PDouble(Cur.WriteOff)+"' "
				+",Radiographs = '"  +  POut.PInt   (Cur.Radiographs)+"' "
				+",ClinicNum = '"    +  POut.PInt   (Cur.ClinicNum)+"' "
				+",ClaimForm = '"    +  POut.PInt   (Cur.ClaimForm)+"' "
				+",EFormat = '"      +  POut.PInt   ((int)Cur.EFormat)+"' "
				+"WHERE claimnum = '"+	POut.PInt   (Cur.ClaimNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(Claim Cur){
			string command = "DELETE FROM claim WHERE ClaimNum = '"+POut.PInt(Cur.ClaimNum)+"'";
			General.NonQ(command);
			command = "DELETE FROM canadianclaim WHERE ClaimNum = '"+POut.PInt(Cur.ClaimNum)+"'";
			General.NonQ(command);
			command = "DELETE FROM canadianextract WHERE ClaimNum = '"+POut.PInt(Cur.ClaimNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void DetachProcsFromClaim(Claim Cur){
			string command = "UPDATE procedurelog SET "
				+"claimnum = '0' "
				+"WHERE claimnum = '"+POut.PInt(Cur.ClaimNum)+"'";
			//MessageBox.Show(string command);
			General.NonQ(command);
		}

		///<summary>Called from claimsend window and from Claim edit window.  Use -1 to get all waiting claims, or an actual claimnum to get just one claim.</summary>
		public static ClaimSendQueueItem[] GetQueueList(){
			return GetQueueList(0);
		}

		///<summary>Called from claimsend window and from Claim edit window.  Use -1 to get all waiting claims, or an actual claimnum to get just one claim.</summary>
		public static ClaimSendQueueItem[] GetQueueList(int claimNum){
			string command=
				"SELECT claim.ClaimNum,carrier.NoSendElect"
				+",CONCAT(CONCAT(CONCAT(concat(patient.LName,', '),patient.FName),' '),patient.MiddleI)"
				+",claim.ClaimStatus,carrier.CarrierName,patient.PatNum,carrier.ElectID,insplan.IsMedical "
				+"FROM claim "
				+"Left join insplan on claim.PlanNum = insplan.PlanNum "
				+"Left join carrier on insplan.CarrierNum = carrier.CarrierNum "
				+"Left join patient on patient.PatNum = claim.PatNum ";
			if(claimNum==0){
				command+="WHERE claim.ClaimStatus = 'W' OR claim.ClaimStatus = 'P' ";
			}
			else{
				command+="WHERE claim.ClaimNum="+POut.PInt(claimNum)+" ";
			}
			command+="ORDER BY insplan.IsMedical";//this puts the medical claims at the end, helping with the looping in X12.
			//MessageBox.Show(string command);
			DataTable table=General.GetTable(command);
			ClaimSendQueueItem[] listQueue=new ClaimSendQueueItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				listQueue[i]=new ClaimSendQueueItem();
				listQueue[i].ClaimNum        = PIn.PInt   (table.Rows[i][0].ToString());
				listQueue[i].NoSendElect     = PIn.PBool  (table.Rows[i][1].ToString());
				listQueue[i].PatName         = PIn.PString(table.Rows[i][2].ToString());
				listQueue[i].ClaimStatus     = PIn.PString(table.Rows[i][3].ToString());
				listQueue[i].Carrier         = PIn.PString(table.Rows[i][4].ToString());
				listQueue[i].PatNum          = PIn.PInt   (table.Rows[i][5].ToString());
				listQueue[i].ClearinghouseNum=Clearinghouses.GetNumForPayor(PIn.PString(table.Rows[i][6].ToString()));
				listQueue[i].IsMedical       = PIn.PBool  (table.Rows[i][7].ToString());
			}
			return listQueue;
		}

		///<summary>Supply claimnums. Called from X12 to begin the sorting process on claims going to one clearinghouse. Returns an array with Carrier,ProvBill,Subscriber,PatNum,ClaimNum, all in the correct order. Carrier is a string, the rest are int.</summary>
		public static object[,] GetX12TransactionInfo(int claimNum){
			return GetX12TransactionInfo(new int[1] {claimNum});
		}

		///<summary>Supply claimnums. Called from X12 to begin the sorting process on claims going to one clearinghouse. Returns an array with Carrier,ProvBill,Subscriber,PatNum,ClaimNum, all in the correct order. Carrier is a string, the rest are int.</summary>
		public static object[,] GetX12TransactionInfo(int[] claimNums){//ArrayList queueItemss){
			StringBuilder str=new StringBuilder();
			for(int i=0;i<claimNums.Length;i++){
				if(i>0){
					str.Append(" OR");
				}
				str.Append(" claim.ClaimNum="+POut.PInt(claimNums[i]));//((ClaimSendQueueItem)queueItems[i]).ClaimNum.ToString());
			}
			string command;
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle){//FIXME:ORDER-BY. Probably Fixed.  ??
				command="SELECT carrier.ElectID,claim.ProvBill,insplan.Subscriber,"
				+"claim.PatNum,claim.ClaimNum, "
				+"CASE WHEN claim.PatNum=insplan.Subscriber THEN 0 ELSE 1 END AS issubscriber "
				+"FROM claim,insplan,carrier "
				+"WHERE claim.PlanNum=insplan.PlanNum "
				+"AND carrier.CarrierNum=insplan.CarrierNum "
				+"AND ("+str.ToString()+") "
				+"ORDER BY carrier.ElectID,claim.ProvBill,insplan.Subscriber,6,claim.PatNum";
			}
			else{
				command="SELECT carrier.ElectID,claim.ProvBill,insplan.Subscriber,"
				+"claim.PatNum,claim.ClaimNum "
				+"FROM claim,insplan,carrier "
				+"WHERE claim.PlanNum=insplan.PlanNum "
				+"AND carrier.CarrierNum=insplan.CarrierNum "
				+"AND ("+str.ToString()+") "
				+"ORDER BY carrier.ElectID,claim.ProvBill,insplan.Subscriber,insplan.Subscriber!=claim.PatNum,claim.PatNum";
			}
			DataTable table=General.GetTable(command);
			object[,] myA=new object[5,table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				myA[0,i]=PIn.PString(table.Rows[i][0].ToString());
				myA[1,i]=PIn.PInt   (table.Rows[i][1].ToString());
				myA[2,i]=PIn.PInt   (table.Rows[i][2].ToString());
				myA[3,i]=PIn.PInt   (table.Rows[i][3].ToString());
				myA[4,i]=PIn.PInt   (table.Rows[i][4].ToString());
			}
			return myA;
		}

		///<summary>Updates all claimproc estimates and also updates claim totals to db. Must supply all claimprocs for this patient (or for this plan if fam max or ded).  Must supply procList which includes all procedures that this claim is linked to.  Will also need to refresh afterwards to see the results</summary>
		public static void CalculateAndUpdate(ClaimProc[] ClaimProcList,Procedure[] procList,InsPlan[] PlanList,Claim ClaimCur,PatPlan[] patPlans,Benefit[] benefitList){
			ClaimProc[] ClaimProcsForClaim=ClaimProcs.GetForClaim(ClaimProcList,ClaimCur.ClaimNum);
			double claimFee=0;
			double dedApplied=0;
			double insPayEst=0;
			double insPayAmt=0;
			double writeoff=0;
			InsPlan PlanCur=InsPlans.GetPlan(ClaimCur.PlanNum,PlanList);
			if(PlanCur==null){
				return;
			}
			int provNum;
			double dedRem;
			int patPlanNum=PatPlans.GetPatPlanNum(patPlans,ClaimCur.PlanNum);
			//this next line has to be done outside the loop.  Takes annual max into consideration 
			double insRem;//no changes get made to insRem in the loop.
			if(patPlanNum==0){//patient does not have current coverage
				insRem=0;
			}
			else{
				insRem=InsPlans.GetInsRem(ClaimProcList,ClaimProcsForClaim[0].ProcDate,ClaimCur.PlanNum,
						patPlanNum,ClaimCur.ClaimNum,PlanList,benefitList);
			}
			//first loop handles totals for received items.
			for(int i=0;i<ClaimProcsForClaim.Length;i++){
				if(ClaimProcsForClaim[i].Status!=ClaimProcStatus.Received){
					continue;//disregard any status except Receieved.
				}
				claimFee+=ClaimProcsForClaim[i].FeeBilled;
				dedApplied+=ClaimProcsForClaim[i].DedApplied;
				insPayEst+=ClaimProcsForClaim[i].InsPayEst;
				insPayAmt+=ClaimProcsForClaim[i].InsPayAmt;
			}
			//loop again only for procs not received.
			//And for preauth.
			Procedure ProcCur;
			for(int i=0;i<ClaimProcsForClaim.Length;i++){
				if(ClaimProcsForClaim[i].Status!=ClaimProcStatus.NotReceived
					&& ClaimProcsForClaim[i].Status!=ClaimProcStatus.Preauth){
					continue;
				}
				ProcCur=Procedures.GetProc(procList,ClaimProcsForClaim[i].ProcNum);
				if(ProcCur.ProcNum==0){
					continue;//ignores payments, etc
				}
				//fee:
				int qty=ProcCur.UnitQty + ProcCur.BaseUnits;
				if(qty==0){
					qty=1;
				}
				if(PlanCur.ClaimsUseUCR){//use UCR for the provider of the procedure
					provNum=ProcCur.ProvNum;
					if(provNum==0){//if no prov set, then use practice default.
						provNum=PrefB.GetInt("PracticeDefaultProv");
					}
					ClaimProcsForClaim[i].FeeBilled=qty*(Fees.GetAmount0(//get the fee based on code and prov fee sched
						ProcCur.CodeNum,Providers.ListLong[Providers.GetIndexLong(provNum)].FeeSched));
				}
				else{//don't use ucr.  Use the procedure fee instead.
					ClaimProcsForClaim[i].FeeBilled=qty*ProcCur.ProcFee;
				}
				claimFee+=ClaimProcsForClaim[i].FeeBilled;
				if(ClaimCur.ClaimType=="PreAuth" || ClaimCur.ClaimType=="Other"){
					//only the fee gets calculated, the rest does not
					ClaimProcs.Update(ClaimProcsForClaim[i]);
					continue;
				}
				//deduct:
				if(patPlanNum==0){//patient does not have current coverage
					dedRem=0;
				}
				else{
					dedRem=InsPlans.GetDedRem(ClaimProcList,ClaimProcsForClaim[i].ProcDate,ClaimCur.PlanNum,patPlanNum,
						ClaimCur.ClaimNum,PlanList,benefitList,ProcedureCodes.GetStringProcCode(ProcCur.CodeNum))
						-dedApplied;//subtracts deductible amounts already applied on this claim
					if(dedRem<0) {
						dedRem=0;
					}
				}
				if(dedRem > ClaimProcsForClaim[i].FeeBilled){//if deductible is more than cost of procedure
					ClaimProcsForClaim[i].DedApplied=ClaimProcsForClaim[i].FeeBilled;
				}
				else{
					ClaimProcsForClaim[i].DedApplied=dedRem;
				}
				if(ClaimCur.ClaimType=="P"){//primary
					ClaimProcs.ComputeBaseEst(ClaimProcsForClaim[i],ProcCur,PriSecTot.Pri,PlanList,patPlans,benefitList);//handles dedBeforePerc
					ClaimProcsForClaim[i].InsPayEst=Procedures.GetEst(ProcCur,ClaimProcList,PriSecTot.Pri,patPlans,true);	
				}
				else if(ClaimCur.ClaimType=="S"){//secondary
					ClaimProcs.ComputeBaseEst(ClaimProcsForClaim[i],ProcCur,PriSecTot.Sec,PlanList,patPlans,benefitList);
					ClaimProcsForClaim[i].InsPayEst=Procedures.GetEst(ProcCur,ClaimProcList,PriSecTot.Sec,patPlans,true);
				}
				if(ClaimCur.ClaimType=="P" || ClaimCur.ClaimType=="S"){
					if(ClaimProcsForClaim[i].DedBeforePerc) {
						int percent=100;
						if(ClaimProcsForClaim[i].Percentage!=-1) {
							percent=ClaimProcsForClaim[i].Percentage;
						}
						if(ClaimProcsForClaim[i].PercentOverride!=-1) {
							percent=ClaimProcsForClaim[i].PercentOverride;
						}
						ClaimProcsForClaim[i].InsPayEst-=ClaimProcsForClaim[i].DedApplied*(double)percent/100d;
					}
					else {
						ClaimProcsForClaim[i].InsPayEst-=ClaimProcsForClaim[i].DedApplied;
					}
				}
				//claimtypes other than P and S only changed manually
				if(ClaimProcsForClaim[i].InsPayEst < 0){
					//example: if inspayest = 19 - 50(ded) for total of -31.
					ClaimProcsForClaim[i].DedApplied+=ClaimProcsForClaim[i].InsPayEst;//eg. 50+(-31)=19
					ClaimProcsForClaim[i].InsPayEst=0;
					//so only 19 of deductible gets applied, and inspayest is 0
				}
				if(insRem-insPayEst<0) {//total remaining ins-Estimated so far on this claim
					ClaimProcsForClaim[i].InsPayEst=0;
				}
				else if(ClaimProcsForClaim[i].InsPayEst>insRem-insPayEst) {
					ClaimProcsForClaim[i].InsPayEst=insRem-insPayEst;
				}
				if(ClaimProcsForClaim[i].Status==ClaimProcStatus.NotReceived){
					ClaimProcsForClaim[i].WriteOff=0;
					if(ClaimCur.ClaimType=="P" && PlanCur.PlanType=="p"){//Primary && PPO
						double insplanAllowed=Fees.GetAmount(ProcCur.CodeNum,PlanCur.FeeSched);
						if(insplanAllowed!=-1) {
							ClaimProcsForClaim[i].WriteOff=ProcCur.ProcFee-insplanAllowed;
						}
						//else, if -1 fee not found, then do not show a writeoff. User can change writeoff if they disagree.
					}
					writeoff+=ClaimProcsForClaim[i].WriteOff;
				}
				dedApplied+=ClaimProcsForClaim[i].DedApplied;
				insPayEst+=ClaimProcsForClaim[i].InsPayEst;
				ClaimProcs.Update(ClaimProcsForClaim[i]);
				//but notice that the ClaimProcs lists are not refreshed until the loop is finished.
			}//for claimprocs.forclaim
			ClaimCur.ClaimFee=claimFee;
			ClaimCur.DedApplied=dedApplied;
			ClaimCur.InsPayEst=insPayEst;
			ClaimCur.InsPayAmt=insPayAmt;
			ClaimCur.WriteOff=writeoff;
			//Cur=ClaimCur;
			Update(ClaimCur);
		}
	}//end class Claims



	///<summary>Holds a list of claims to show in the claims 'queue' waiting to be sent.</summary>
	public class ClaimSendQueueItem{
		///<summary></summary>
		public int ClaimNum;
		///<summary></summary>
		public bool NoSendElect;
		///<summary></summary>
		public string PatName;
		///<summary>Single char: U,H,W,P,S,or R.</summary>
		///<remarks>U=Unsent, H=Hold until pri received, W=Waiting in queue, P=Probably sent, S=Sent, R=Received.  A(adj) is no longer used.</remarks>
		public string ClaimStatus;
		///<summary></summary>
		public string Carrier;
		///<summary></summary>
		public int PatNum;
		///<summary></summary>
		public int ClearinghouseNum;
		///<summary>True if the plan is a medical plan.</summary>
		public bool IsMedical;
	}

	///<summary>Holds a list of claims to show in the Claim Check Edit window.</summary>
	public class ClaimPaySplit{
		///<summary></summary>
		public int ClaimNum;
		///<summary></summary>
		public string PatName;
		///<summary></summary>
		public string Carrier;
		///<summary></summary>
		public DateTime DateClaim;
		///<summary></summary>
		public string ProvAbbr;
		///<summary></summary>
		public double FeeBilled;
		///<summary></summary>
		public double InsPayAmt;
		///<summary></summary>
		public int ClaimPaymentNum;
	}



}









