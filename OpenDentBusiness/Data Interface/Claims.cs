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
		public static List<ClaimPaySplit> RefreshByCheck(long claimPaymentNum,bool showUnattached) {
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
 				+" AND (claimproc.ClaimPaymentNum = '"+POut.Long(claimPaymentNum)+"'";
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
				split.DateClaim      =PIn.Date  (table.Rows[i]["DateService"].ToString());
				split.ProvAbbr       =Providers.GetAbbr(PIn.Long(table.Rows[i]["ProvTreat"].ToString()));
				split.PatName        =PIn.String(table.Rows[i]["_patName"].ToString());
				split.Carrier        =PIn.String(table.Rows[i]["CarrierName"].ToString());
				split.FeeBilled      =PIn.Double(table.Rows[i]["_feeBilled"].ToString());
				split.InsPayAmt      =PIn.Double(table.Rows[i]["_insPayAmt"].ToString());
				split.ClaimNum       =PIn.Long   (table.Rows[i]["ClaimNum"].ToString());
				split.ClaimPaymentNum=PIn.Long   (table.Rows[i]["ClaimPaymentNum"].ToString());
				split.PatNum         =PIn.Long   (table.Rows[i]["PatNum"].ToString());
				splits.Add(split);
			}
			return splits;
		}

		///<summary>Gets the specified claim from the database.</summary>
		public static Claim GetClaim(long claimNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Claim>(MethodBase.GetCurrentMethod(),claimNum);
			}
			string command="SELECT * FROM claim"
				+" WHERE ClaimNum = "+claimNum.ToString();
			DataTable table=Db.GetTable(command);
			Claim retClaim=SubmitAndFill(table)[0];
			command="SELECT * FROM claimattach WHERE ClaimNum = "+POut.Long(claimNum);
			table=Db.GetTable(command);
			retClaim.Attachments=new List<ClaimAttach>();
			ClaimAttach attach;
			for(int i=0;i<table.Rows.Count;i++){
				attach=new ClaimAttach();
				attach.ClaimAttachNum   =PIn.Long   (table.Rows[i][0].ToString());
				attach.ClaimNum         =PIn.Long   (table.Rows[i][1].ToString());
				attach.DisplayedFileName=PIn.String(table.Rows[i][2].ToString());
				attach.ActualFileName   =PIn.String(table.Rows[i][3].ToString());
				retClaim.Attachments.Add(attach);
			}
			return retClaim;
		}

		///<summary>Gets all claims for the specified patient. But without any attachments.</summary>
		public static List<Claim> Refresh(long patNum) {
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
				tempClaim.ClaimNum     =		PIn.Long   (table.Rows[i][0].ToString());
				tempClaim.PatNum       =		PIn.Long   (table.Rows[i][1].ToString());
				tempClaim.DateService  =		PIn.Date  (table.Rows[i][2].ToString());
				tempClaim.DateSent     =		PIn.Date  (table.Rows[i][3].ToString());
				tempClaim.ClaimStatus  =		PIn.String(table.Rows[i][4].ToString());
				tempClaim.DateReceived =		PIn.Date  (table.Rows[i][5].ToString());
				tempClaim.PlanNum      =		PIn.Long   (table.Rows[i][6].ToString());
				tempClaim.ProvTreat    =		PIn.Long   (table.Rows[i][7].ToString());
				tempClaim.ClaimFee     =		PIn.Double(table.Rows[i][8].ToString());
				tempClaim.InsPayEst    =		PIn.Double(table.Rows[i][9].ToString());
				tempClaim.InsPayAmt    =		PIn.Double(table.Rows[i][10].ToString());
				tempClaim.DedApplied   =		PIn.Double(table.Rows[i][11].ToString());
				tempClaim.PreAuthString=		PIn.String(table.Rows[i][12].ToString());
				tempClaim.IsProsthesis =		PIn.String(table.Rows[i][13].ToString());
				tempClaim.PriorDate    =		PIn.Date  (table.Rows[i][14].ToString());
				tempClaim.ReasonUnderPaid=	PIn.String(table.Rows[i][15].ToString());
				tempClaim.ClaimNote    =		PIn.String(table.Rows[i][16].ToString());
				tempClaim.ClaimType    =    PIn.String(table.Rows[i][17].ToString());
				tempClaim.ProvBill     =		PIn.Long   (table.Rows[i][18].ToString());
				tempClaim.ReferringProv=		PIn.Long   (table.Rows[i][19].ToString());
				tempClaim.RefNumString =		PIn.String(table.Rows[i][20].ToString());
				tempClaim.PlaceService = (PlaceOfService)PIn.Long(table.Rows[i][21].ToString());
				tempClaim.AccidentRelated=	PIn.String(table.Rows[i][22].ToString());
				tempClaim.AccidentDate  =		PIn.Date  (table.Rows[i][23].ToString());
				tempClaim.AccidentST    =		PIn.String(table.Rows[i][24].ToString());
				tempClaim.EmployRelated=(YN)PIn.Long   (table.Rows[i][25].ToString());
				tempClaim.IsOrtho       =		PIn.Bool  (table.Rows[i][26].ToString());
				tempClaim.OrthoRemainM  =		PIn.Int   (table.Rows[i][27].ToString());
				tempClaim.OrthoDate     =		PIn.Date  (table.Rows[i][28].ToString());
				tempClaim.PatRelat      =(Relat)PIn.Long(table.Rows[i][29].ToString());
				tempClaim.PlanNum2      =   PIn.Long   (table.Rows[i][30].ToString());
				tempClaim.PatRelat2     =(Relat)PIn.Long(table.Rows[i][31].ToString());
				tempClaim.WriteOff      =   PIn.Double(table.Rows[i][32].ToString());
				tempClaim.Radiographs   =   PIn.Int   (table.Rows[i][33].ToString());
				tempClaim.ClinicNum     =   PIn.Long   (table.Rows[i][34].ToString());
				tempClaim.ClaimForm     =   PIn.Long   (table.Rows[i][35].ToString());
				tempClaim.EFormat       =(EtransType)PIn.Long(table.Rows[i][36].ToString());
				tempClaim.AttachedImages=   PIn.Int   (table.Rows[i][37].ToString());
				tempClaim.AttachedModels=   PIn.Int   (table.Rows[i][38].ToString());
				tempClaim.AttachedFlags =   PIn.String(table.Rows[i][39].ToString());
				tempClaim.AttachmentID  =   PIn.String(table.Rows[i][40].ToString());
				claims.Add(tempClaim);
			}
			return claims;
		}

		public static Claim GetFromList(List<Claim> list,long claimNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<list.Count;i++) {
				if(list[i].ClaimNum==claimNum) {
					return list[i].Copy();
				}
			}
			return null;
		}

		///<summary></summary>
		public static long Insert(Claim Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.ClaimNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.ClaimNum;
			}
			if(PrefC.RandomKeys){
				Cur.ClaimNum=ReplicationServers.GetKey("claim","ClaimNum");
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
				command+="'"+POut.Long(Cur.ClaimNum)+"', ";
			}
			command+=
				 "'"+POut.Long   (Cur.PatNum)+"', "
				+POut.Date  (Cur.DateService)+", "
				+POut.Date  (Cur.DateSent)+", "
				+"'"+POut.String(Cur.ClaimStatus)+"', "
				+POut.Date  (Cur.DateReceived)+", "
				+"'"+POut.Long   (Cur.PlanNum)+"', "
				+"'"+POut.Long   (Cur.ProvTreat)+"', "
				+"'"+POut.Double(Cur.ClaimFee)+"', "
				+"'"+POut.Double(Cur.InsPayEst)+"', "
				+"'"+POut.Double(Cur.InsPayAmt)+"', "
				+"'"+POut.Double(Cur.DedApplied)+"', "
				+"'"+POut.String(Cur.PreAuthString)+"', "
				+"'"+POut.String(Cur.IsProsthesis)+"', "
				+POut.Date  (Cur.PriorDate)+", "
				+"'"+POut.String(Cur.ReasonUnderPaid)+"', "
				+"'"+POut.String(Cur.ClaimNote)+"', "
				+"'"+POut.String(Cur.ClaimType)+"', "
				+"'"+POut.Long   (Cur.ProvBill)+"', "
				+"'"+POut.Long   (Cur.ReferringProv)+"', "
				+"'"+POut.String(Cur.RefNumString)+"', "
				+"'"+POut.Long   ((int)Cur.PlaceService)+"', "
				+"'"+POut.String(Cur.AccidentRelated)+"', "
				+POut.Date  (Cur.AccidentDate)+", "
				+"'"+POut.String(Cur.AccidentST)+"', "
				+"'"+POut.Long   ((int)Cur.EmployRelated)+"', "
				+"'"+POut.Bool  (Cur.IsOrtho)+"', "
				+"'"+POut.Long   (Cur.OrthoRemainM)+"', "
				+POut.Date  (Cur.OrthoDate)+", "
				+"'"+POut.Long   ((int)Cur.PatRelat)+"', "
				+"'"+POut.Long   (Cur.PlanNum2)+"', "
				+"'"+POut.Long   ((int)Cur.PatRelat2)+"', "
				+"'"+POut.Double(Cur.WriteOff)+"', "
				+"'"+POut.Long   (Cur.Radiographs)+"', "
				+"'"+POut.Long   (Cur.ClinicNum)+"', "
				+"'"+POut.Long   (Cur.ClaimForm)+"', "
				+"'"+POut.Long   ((int)Cur.EFormat)+"', "
				+"'"+POut.Long   (Cur.AttachedImages)+"', "
				+"'"+POut.Long   (Cur.AttachedModels)+"', "
				+"'"+POut.String(Cur.AttachedFlags)+"', "
				+"'"+POut.String(Cur.AttachmentID)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				Cur.ClaimNum=Db.NonQ(command,true);
			}
			return Cur.ClaimNum;
		}

		///<summary></summary>
		public static void Update(Claim Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "UPDATE claim SET "
				+"patnum = '"          +POut.Long   (Cur.PatNum)+"' "
				+",dateservice = "    +POut.Date  (Cur.DateService)+" "
				+",datesent = "       +POut.Date  (Cur.DateSent)+" "
				+",claimstatus = '"    +POut.String(Cur.ClaimStatus)+"' "
				+",datereceived = "   +POut.Date  (Cur.DateReceived)+" "
				+",plannum = '"        +POut.Long   (Cur.PlanNum)+"' "
				+",provtreat = '"      +POut.Long   (Cur.ProvTreat)+"' "
				+",claimfee = '"       +POut.Double(Cur.ClaimFee)+"' "
				+",inspayest = '"      +POut.Double(Cur.InsPayEst)+"' "
				+",inspayamt = '"      +POut.Double(Cur.InsPayAmt)+"' "
				+",dedapplied = '"   +  POut.Double(Cur.DedApplied)+"' "
				+",preauthstring = '"+	POut.String(Cur.PreAuthString)+"' "
				+",isprosthesis = '" +	POut.String(Cur.IsProsthesis)+"' "
				+",priordate = "    +	POut.Date  (Cur.PriorDate)+" "
				+",reasonunderpaid = '"+POut.String(Cur.ReasonUnderPaid)+"' "
				+",claimnote = '"    +	POut.String(Cur.ClaimNote)+"' "
				+",claimtype='"      +	POut.String(Cur.ClaimType)+"' "
				+",provbill = '"     +	POut.Long   (Cur.ProvBill)+"' "
				+",referringprov = '"+	POut.Long   (Cur.ReferringProv)+"' "
				+",refnumstring = '" +	POut.String(Cur.RefNumString)+"' "
				+",placeservice = '" +	POut.Long   ((int)Cur.PlaceService)+"' "
				+",accidentrelated = '"+POut.String(Cur.AccidentRelated)+"' "//ask Derek why this was out of order.
				+",accidentdate = "   +	POut.Date  (Cur.AccidentDate)+" "
				+",accidentst = '"    +	POut.String(Cur.AccidentST)+"' "
				+",employrelated = '" +	POut.Long   ((int)Cur.EmployRelated)+"' "
				+",isortho = '"       +	POut.Bool  (Cur.IsOrtho)+"' "
				+",orthoremainm = '"  +	POut.Long   (Cur.OrthoRemainM)+"' "
				+",orthodate = "      +	POut.Date  (Cur.OrthoDate)+" "
				+",patrelat = '"      +	POut.Long   ((int)Cur.PatRelat)+"' "
				+",plannum2 = '"      +	POut.Long   (Cur.PlanNum2)+"' "
				+",patrelat2 = '"     +	POut.Long   ((int)Cur.PatRelat2)+"' "
				+",writeoff = '"      +	POut.Double(Cur.WriteOff)+"' "
				+",Radiographs = '"   + POut.Long   (Cur.Radiographs)+"' "
				+",ClinicNum = '"     + POut.Long   (Cur.ClinicNum)+"' "
				+",ClaimForm = '"     + POut.Long   (Cur.ClaimForm)+"' "
				+",EFormat = '"       + POut.Long   ((int)Cur.EFormat)+"' "
				+",AttachedImages = '"+ POut.Long   (Cur.AttachedImages)+"' "
				+",AttachedModels = '"+ POut.Long   (Cur.AttachedModels)+"' "
				+",AttachedFlags = '" + POut.String(Cur.AttachedFlags)+"' "
				+",AttachmentID = '"  + POut.String(Cur.AttachmentID)+"' "
				+"WHERE claimnum = '"+	POut.Long   (Cur.ClaimNum)+"'";
			Db.NonQ(command);
			//now, delete all attachments and recreate.
			command="DELETE FROM claimattach WHERE ClaimNum="+POut.Long(Cur.ClaimNum);
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
			string command = "DELETE FROM claim WHERE ClaimNum = '"+POut.Long(Cur.ClaimNum)+"'";
			Db.NonQ(command);
			command = "DELETE FROM canadianclaim WHERE ClaimNum = '"+POut.Long(Cur.ClaimNum)+"'";
			Db.NonQ(command);
			command = "DELETE FROM canadianextract WHERE ClaimNum = '"+POut.Long(Cur.ClaimNum)+"'";
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
				+"WHERE claimnum = '"+POut.Long(Cur.ClaimNum)+"'";
			//MessageBox.Show(string command);
			Db.NonQ(command);
		}

		/*
		///<summary>Called from claimsend window and from Claim edit window.  Use 0 to get all waiting claims, or an actual claimnum to get just one claim.</summary>
		public static ClaimSendQueueItem[] GetQueueList(){
			return GetQueueList(0,0);
		}*/

		///<summary>Called from claimsend window and from Claim edit window.  Use 0 to get all waiting claims, or an actual claimnum to get just one claim.</summary>
		public static ClaimSendQueueItem[] GetQueueList(long claimNum,long clinicNum) {
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
				command+="WHERE claim.ClaimNum="+POut.Long(claimNum)+" ";
			}
			if(clinicNum>0) {
				command+="AND claim.ClinicNum="+POut.Long(clinicNum)+" ";
			}
			command+="ORDER BY insplan.IsMedical, patient.LName";//this puts the medical claims at the end, helping with the looping in X12.
			//MessageBox.Show(string command);
			DataTable table=Db.GetTable(command);
			ClaimSendQueueItem[] listQueue=new ClaimSendQueueItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				listQueue[i]=new ClaimSendQueueItem();
				listQueue[i].ClaimNum        = PIn.Long   (table.Rows[i][0].ToString());
				listQueue[i].NoSendElect     = PIn.Bool  (table.Rows[i][1].ToString());
				listQueue[i].PatName         = PIn.String(table.Rows[i][2].ToString());
				listQueue[i].ClaimStatus     = PIn.String(table.Rows[i][3].ToString());
				listQueue[i].Carrier         = PIn.String(table.Rows[i][4].ToString());
				listQueue[i].PatNum          = PIn.Long   (table.Rows[i][5].ToString());
				listQueue[i].ClearinghouseNum=Clearinghouses.GetNumForPayor(PIn.String(table.Rows[i][6].ToString()));
				listQueue[i].IsMedical       = PIn.Bool  (table.Rows[i][7].ToString());
			}
			return listQueue;
		}

		///<summary>Supply claimnums. Called from X12 to begin the sorting process on claims going to one clearinghouse. Returns an array with Carrier,ProvBill,Subscriber,PatNum,ClaimNum, all in the correct order. Carrier is a string, the rest are int.</summary>
		public static object[,] GetX12TransactionInfo(long claimNum) {
			//No need to check RemotingRole; no call to db.
			List<long> claimNums=new List<long>();
			claimNums.Add(claimNum);
			return GetX12TransactionInfo(claimNums);
		}

		///<summary>Supply claimnums. Called from X12 to begin the sorting process on claims going to one clearinghouse. Returns an array with Carrier,ProvBill,Subscriber,PatNum,ClaimNum, all in the correct order. Carrier is a string, the rest are int.</summary>
		public static object[,] GetX12TransactionInfo(List<long> claimNums) {//ArrayList queueItemss){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<object[,]>(MethodBase.GetCurrentMethod(),claimNums);
			}
			StringBuilder str=new StringBuilder();
			for(int i=0;i<claimNums.Count;i++){
				if(i>0){
					str.Append(" OR");
				}
				str.Append(" claim.ClaimNum="+POut.Long(claimNums[i]));//((ClaimSendQueueItem)queueItems[i]).ClaimNum.ToString());
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
				myA[0,i]=PIn.String(table.Rows[i][0].ToString());
				myA[1,i]=PIn.Long   (table.Rows[i][1].ToString());
				myA[2,i]=PIn.Long   (table.Rows[i][2].ToString());
				myA[3,i]=PIn.Long   (table.Rows[i][3].ToString());
				myA[4,i]=PIn.Long   (table.Rows[i][4].ToString());
			}
			return myA;
		}

	}//end class Claims

	///<summary>Holds a list of claims to show in the claims 'queue' waiting to be sent.</summary>
	public class ClaimSendQueueItem{
		///<summary></summary>
		public long ClaimNum;
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
		public long PatNum;
		///<summary></summary>
		public long ClearinghouseNum;
		///<summary>True if the plan is a medical plan.</summary>
		public bool IsMedical;

		public ClaimSendQueueItem Copy(){
			return (ClaimSendQueueItem)MemberwiseClone();
		}
	}

	///<summary>Holds a list of claims to show in the Claim Check Edit window.</summary>
	public class ClaimPaySplit{
		///<summary></summary>
		public long ClaimNum;
		///<summary></summary>
		public string PatName;
		///<summary></summary>
		public long PatNum;
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
		public long ClaimPaymentNum;
	}
	
}