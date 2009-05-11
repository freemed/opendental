using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Claims{
		
		///<summary></summary>
		public static List<ClaimPaySplit> RefreshByCheck(int claimPaymentNum, bool showUnattached){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClaimPaySplit>>(MethodBase.GetCurrentMethod(),claimPaymentNum,showUnattached);
			}
			string command=
				"SELECT claim.DateService,claim.ProvTreat,CONCAT(CONCAT(patient.LName,', '),patient.FName) _patName"
				+",carrier.CarrierName,SUM(claimproc.FeeBilled) _feeBilled,SUM(claimproc.InsPayAmt) _insPayAmt,claim.ClaimNum"
				+",claimproc.ClaimPaymentNum,claim.PatNum"
				+" FROM claim,patient,insplan,carrier,claimproc"
				+" WHERE claimproc.ClaimNum = claim.ClaimNum"
				+" AND patient.PatNum = claim.PatNum"
				+" AND insplan.PlanNum = claim.PlanNum"
				+" AND insplan.CarrierNum = carrier.CarrierNum"
				+" AND (claimproc.Status = '1' OR claimproc.Status = '4' OR claimproc.Status=5)"//received or supplemental or capclaim
 				+" AND (claimproc.ClaimPaymentNum = '"+POut.PInt(claimPaymentNum)+"'";
			if(showUnattached){
				command+=" OR (claimproc.InsPayAmt != 0 AND claimproc.ClaimPaymentNum = '0'))"
					+" GROUP BY claimproc.ClaimNum";
			}
			else{//shows only items attached to this payment
				command+=")"
					+" GROUP BY claimproc.ClaimNum";
			}
			command+=" ORDER BY _patName";
			DataTable table=Db.GetTable(command);
			List<ClaimPaySplit> splits=new List<ClaimPaySplit>();
			ClaimPaySplit split;
			for(int i=0;i<table.Rows.Count;i++){
				split=new ClaimPaySplit();
				split.DateClaim      =PIn.PDate  (table.Rows[i]["DateService"].ToString());
				split.ProvAbbr       =Providers.GetAbbr(PIn.PInt(table.Rows[i]["ProvTreat"].ToString()));
				split.PatName        =PIn.PString(table.Rows[i]["_patName"].ToString());
				split.Carrier        =PIn.PString(table.Rows[i]["CarrierName"].ToString());
				split.FeeBilled      =PIn.PDouble(table.Rows[i]["_feeBilled"].ToString());
				split.InsPayAmt      =PIn.PDouble(table.Rows[i]["_insPayAmt"].ToString());
				split.ClaimNum       =PIn.PInt   (table.Rows[i]["ClaimNum"].ToString());
				split.ClaimPaymentNum=PIn.PInt   (table.Rows[i]["ClaimPaymentNum"].ToString());
				split.PatNum         =PIn.PInt   (table.Rows[i]["PatNum"].ToString());
				splits.Add(split);
			}
			return splits;
		}

		///<summary>Gets the specified claim from the database.</summary>
		public static Claim GetClaim(int claimNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Claim>(MethodBase.GetCurrentMethod(),claimNum);
			}
			string command="SELECT * FROM claim"
				+" WHERE ClaimNum = "+claimNum.ToString();
			DataTable table=Db.GetTable(command);
			Claim retClaim=SubmitAndFill(table)[0];
			command="SELECT * FROM claimattach WHERE ClaimNum = "+POut.PInt(claimNum);
			table=Db.GetTable(command);
			retClaim.Attachments=new List<ClaimAttach>();
			ClaimAttach attach;
			for(int i=0;i<table.Rows.Count;i++){
				attach=new ClaimAttach();
				attach.ClaimAttachNum   =PIn.PInt   (table.Rows[i][0].ToString());
				attach.ClaimNum         =PIn.PInt   (table.Rows[i][1].ToString());
				attach.DisplayedFileName=PIn.PString(table.Rows[i][2].ToString());
				attach.ActualFileName   =PIn.PString(table.Rows[i][3].ToString());
				retClaim.Attachments.Add(attach);
			}
			return retClaim;
		}

		///<summary>Gets all claims for the specified patient. But without any attachments.</summary>
		public static List<Claim> Refresh(int patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Claim>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * FROM claim"
				+" WHERE PatNum = "+patNum.ToString()
				+" ORDER BY dateservice";
			DataTable table=Db.GetTable(command);
			return SubmitAndFill(table);
		}

		private static List<Claim> SubmitAndFill(DataTable table){
			//No need to check RemotingRole; no call to db.
			Claim tempClaim;
			List<Claim> claims=new List<Claim>();
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
				tempClaim.AttachedImages=   PIn.PInt   (table.Rows[i][37].ToString());
				tempClaim.AttachedModels=   PIn.PInt   (table.Rows[i][38].ToString());
				tempClaim.AttachedFlags =   PIn.PString(table.Rows[i][39].ToString());
				tempClaim.AttachmentID  =   PIn.PString(table.Rows[i][40].ToString());
				claims.Add(tempClaim);
			}
			return claims;
		}

		public static Claim GetFromList(List<Claim> list,int claimNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<list.Count;i++) {
				if(list[i].ClaimNum==claimNum) {
					return list[i].Copy();
				}
			}
			return null;
		}

		///<summary></summary>
		public static void Insert(Claim Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			if(PrefC.RandomKeys){
				Cur.ClaimNum=MiscData.GetKey("claim","ClaimNum");
			}
			string command="INSERT INTO claim (";
			if(PrefC.RandomKeys){
				command+="ClaimNum,";
			}
			command+="patnum,dateservice,datesent,claimstatus,datereceived"
				+",plannum,provtreat,claimfee,inspayest,inspayamt,dedapplied"
				+",preauthstring,isprosthesis,priordate,reasonunderpaid,claimnote"
				+",claimtype,provbill,referringprov"
				+",refnumstring,placeservice,accidentrelated,accidentdate,accidentst"
				+",employrelated,isortho,orthoremainm,orthodate,patrelat,plannum2"
				+",patrelat2,writeoff,Radiographs,ClinicNum,ClaimForm,EFormat,"
				+"AttachedImages,AttachedModels,AttachedFlags,AttachmentID) VALUES(";
			if(PrefC.RandomKeys){
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
				+"'"+POut.PInt   ((int)Cur.EFormat)+"', "
				+"'"+POut.PInt   (Cur.AttachedImages)+"', "
				+"'"+POut.PInt   (Cur.AttachedModels)+"', "
				+"'"+POut.PString(Cur.AttachedFlags)+"', "
				+"'"+POut.PString(Cur.AttachmentID)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
				Cur.ClaimNum=Db.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(Claim Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
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
				+",accidentrelated = '"+POut.PString(Cur.AccidentRelated)+"' "//ask Derek why this was out of order.
				+",accidentdate = "   +	POut.PDate  (Cur.AccidentDate)+" "
				+",accidentst = '"    +	POut.PString(Cur.AccidentST)+"' "
				+",employrelated = '" +	POut.PInt   ((int)Cur.EmployRelated)+"' "
				+",isortho = '"       +	POut.PBool  (Cur.IsOrtho)+"' "
				+",orthoremainm = '"  +	POut.PInt   (Cur.OrthoRemainM)+"' "
				+",orthodate = "      +	POut.PDate  (Cur.OrthoDate)+" "
				+",patrelat = '"      +	POut.PInt   ((int)Cur.PatRelat)+"' "
				+",plannum2 = '"      +	POut.PInt   (Cur.PlanNum2)+"' "
				+",patrelat2 = '"     +	POut.PInt   ((int)Cur.PatRelat2)+"' "
				+",writeoff = '"      +	POut.PDouble(Cur.WriteOff)+"' "
				+",Radiographs = '"   + POut.PInt   (Cur.Radiographs)+"' "
				+",ClinicNum = '"     + POut.PInt   (Cur.ClinicNum)+"' "
				+",ClaimForm = '"     + POut.PInt   (Cur.ClaimForm)+"' "
				+",EFormat = '"       + POut.PInt   ((int)Cur.EFormat)+"' "
				+",AttachedImages = '"+ POut.PInt   (Cur.AttachedImages)+"' "
				+",AttachedModels = '"+ POut.PInt   (Cur.AttachedModels)+"' "
				+",AttachedFlags = '" + POut.PString(Cur.AttachedFlags)+"' "
				+",AttachmentID = '"  + POut.PString(Cur.AttachmentID)+"' "
				+"WHERE claimnum = '"+	POut.PInt   (Cur.ClaimNum)+"'";
			Db.NonQ(command);
			//now, delete all attachments and recreate.
			command="DELETE FROM claimattach WHERE ClaimNum="+POut.PInt(Cur.ClaimNum);
			Db.NonQ(command);
			for(int i=0;i<Cur.Attachments.Count;i++) {
				Cur.Attachments[i].ClaimNum=Cur.ClaimNum;
				ClaimAttaches.Insert(Cur.Attachments[i]);
			}
		}

		///<summary></summary>
		public static void Delete(Claim Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "DELETE FROM claim WHERE ClaimNum = '"+POut.PInt(Cur.ClaimNum)+"'";
			Db.NonQ(command);
			command = "DELETE FROM canadianclaim WHERE ClaimNum = '"+POut.PInt(Cur.ClaimNum)+"'";
			Db.NonQ(command);
			command = "DELETE FROM canadianextract WHERE ClaimNum = '"+POut.PInt(Cur.ClaimNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void DetachProcsFromClaim(Claim Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "UPDATE procedurelog SET "
				+"claimnum = '0' "
				+"WHERE claimnum = '"+POut.PInt(Cur.ClaimNum)+"'";
			//MessageBox.Show(string command);
			Db.NonQ(command);
		}

		/*
		///<summary>Called from claimsend window and from Claim edit window.  Use 0 to get all waiting claims, or an actual claimnum to get just one claim.</summary>
		public static ClaimSendQueueItem[] GetQueueList(){
			return GetQueueList(0,0);
		}*/

		///<summary>Called from claimsend window and from Claim edit window.  Use 0 to get all waiting claims, or an actual claimnum to get just one claim.</summary>
		public static ClaimSendQueueItem[] GetQueueList(int claimNum,int clinicNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ClaimSendQueueItem[]>(MethodBase.GetCurrentMethod(),claimNum,clinicNum);
			}
			string command=
				"SELECT claim.ClaimNum,carrier.NoSendElect"
				+",CONCAT(CONCAT(CONCAT(concat(patient.LName,', '),patient.FName),' '),patient.MiddleI)"
				+",claim.ClaimStatus,carrier.CarrierName,patient.PatNum,carrier.ElectID,insplan.IsMedical "
				+"FROM claim "
				+"Left join insplan on claim.PlanNum = insplan.PlanNum "
				+"Left join carrier on insplan.CarrierNum = carrier.CarrierNum "
				+"Left join patient on patient.PatNum = claim.PatNum ";
			if(claimNum==0){
				command+="WHERE (claim.ClaimStatus = 'W' OR claim.ClaimStatus = 'P') ";
			}
			else{
				command+="WHERE claim.ClaimNum="+POut.PInt(claimNum)+" ";
			}
			if(clinicNum>0) {
				command+="AND claim.ClinicNum="+POut.PInt(clinicNum)+" ";
			}
			command+="ORDER BY insplan.IsMedical, patient.LName";//this puts the medical claims at the end, helping with the looping in X12.
			//MessageBox.Show(string command);
			DataTable table=Db.GetTable(command);
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
			//No need to check RemotingRole; no call to db.
			List <int> claimNums=new List <int> ();
			claimNums.Add(claimNum);
			return GetX12TransactionInfo(claimNums);
		}

		///<summary>Supply claimnums. Called from X12 to begin the sorting process on claims going to one clearinghouse. Returns an array with Carrier,ProvBill,Subscriber,PatNum,ClaimNum, all in the correct order. Carrier is a string, the rest are int.</summary>
		public static object[,] GetX12TransactionInfo(List <int> claimNums){//ArrayList queueItemss){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<object[,]>(MethodBase.GetCurrentMethod(),claimNums);
			}
			StringBuilder str=new StringBuilder();
			for(int i=0;i<claimNums.Count;i++){
				if(i>0){
					str.Append(" OR");
				}
				str.Append(" claim.ClaimNum="+POut.PInt(claimNums[i]));//((ClaimSendQueueItem)queueItems[i]).ClaimNum.ToString());
			}
			string command;
			if(DataConnection.DBtype==DatabaseType.Oracle){//FIXME:ORDER-BY. Probably Fixed.  ??
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
			DataTable table=Db.GetTable(command);
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

		public ClaimSendQueueItem Copy(){
			return (ClaimSendQueueItem)MemberwiseClone();
		}
	}

	///<summary>Holds a list of claims to show in the Claim Check Edit window.</summary>
	public class ClaimPaySplit{
		///<summary></summary>
		public int ClaimNum;
		///<summary></summary>
		public string PatName;
		///<summary></summary>
		public int PatNum;
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