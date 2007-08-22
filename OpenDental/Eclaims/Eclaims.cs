using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Eclaims
{
	/// <summary>
	/// Summary description for Eclaims.
	/// </summary>
	public class Eclaims{
		/// <summary></summary>
		public Eclaims()
		{
			
		}

		///<summary>Supply a list of ClaimSendQueueItems. Called from FormClaimSend.  Can send to multiple clearinghouses simultaneously or can also just send one claim.</summary>
		public static void SendBatches(List<ClaimSendQueueItem> queueItems){
			List<ClaimSendQueueItem>[] claimsByCHouse=new List<ClaimSendQueueItem>[Clearinghouses.List.Length];
			//ArrayList[Clearinghouses.List.Length];
			for(int i=0;i<claimsByCHouse.Length;i++){
				claimsByCHouse[i]=new List<ClaimSendQueueItem>();
				//claimsByCHouse[i]=new ArrayList();
			}
			//divide the items by clearinghouse:
			for(int i=0;i<queueItems.Count;i++){
				claimsByCHouse[Clearinghouses.GetIndex(queueItems[i].ClearinghouseNum)].Add(queueItems[i]);
			}
			//for any clearinghouses with claims, send them:
			int batchNum;
			//bool result=true;
			string messageText="";
			for(int i=0;i<claimsByCHouse.Length;i++){
				if(claimsByCHouse[i].Count==0){
					continue;
				}
				//get next batch number for this clearinghouse
				batchNum=Clearinghouses.GetNextBatchNumber(Clearinghouses.List[i]);
				//---------------------------------------------------------------------------------------
				//Create the claim file(s) for this clearinghouse
				if(Clearinghouses.List[i].Eformat==ElectronicClaimFormat.X12){
					messageText=X12.SendBatch(claimsByCHouse[i],batchNum);
				}
				else if(Clearinghouses.List[i].Eformat==ElectronicClaimFormat.Renaissance){
					messageText=Renaissance.SendBatch(claimsByCHouse[i],batchNum);
				}
				else if(Clearinghouses.List[i].Eformat==ElectronicClaimFormat.Canadian) {
					//Canadian is a little different because we need the sequence numbers.
					//So all programs are launched and statuses changed from within Canadian.SendBatch()
					//We don't care what the result is.
					Canadian.SendBatch(claimsByCHouse[i],batchNum);
					continue;
				}
				else{
					messageText="";//(ElectronicClaimFormat.None does not get sent)
				}
				if(messageText==""){//if failed to create claim file properly,
					continue;//don't launch program or change claim status
				}
				//----------------------------------------------------------------------------------------
				//Launch Client Program for this clearinghouse if applicable
				if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.None){
					AttemptLaunch(Clearinghouses.List[i],batchNum);
				}
				else if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.WebMD){
					if(!WebMD.Launch(Clearinghouses.List[i],batchNum)){
						MessageBox.Show(Lan.g("Eclaims","Error sending."));
						continue;
					}
				}
				else if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.BCBSGA){
					if(!BCBSGA.Launch(Clearinghouses.List[i],batchNum)){
						MessageBox.Show(Lan.g("Eclaims","Error sending."));
						continue;
					}
				}
				else if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.Renaissance){
					AttemptLaunch(Clearinghouses.List[i],batchNum);
				}
				else if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.ClaimConnect){
					if(!WebClaim.Launch(Clearinghouses.List[i],batchNum)){
						MessageBox.Show(Lan.g("Eclaims","Error sending."));
						continue;
					}
				}
				else if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.RECS){
					if(!RECS.Launch(Clearinghouses.List[i],batchNum)){
						MessageBox.Show("Claim file created, but could not launch RECS client.");
						//continue;
					}
				}
				else if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.Inmediata){
					if(!Inmediata.Launch(Clearinghouses.List[i],batchNum)){
						MessageBox.Show("Claim file created, but could not launch Inmediata client.");
						//continue;
					}
				}
				else if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.AOS){ // added by SPK 7/13/05
					if(!AOS.Launch(Clearinghouses.List[i],batchNum)){
						MessageBox.Show("Claim file created, but could not launch AOS Communicator.");
						//continue;
					}
				}
				else if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.PostnTrack){
					AttemptLaunch(Clearinghouses.List[i],batchNum);
					//if(!PostnTrack.Launch(Clearinghouses.List[i],batchNum)){
					//	MessageBox.Show("Claim file created, but could not launch AOS Communicator.");
						//continue;
					//}
				}
				else if(Clearinghouses.List[i].CommBridge==EclaimsCommBridge.Tesia) {
					if(!Tesia.Launch(Clearinghouses.List[i],batchNum)) {
						MessageBox.Show(Lan.g("Eclaims","Error sending."));
						continue;
					}
				}
				//----------------------------------------------------------------------------------------
				//finally, mark the claims sent. (only if not Canadian)
				EtransType etype=EtransType.ClaimSent;
				if(Clearinghouses.List[i].Eformat==ElectronicClaimFormat.Renaissance){
					etype=EtransType.Claim_Ren;
				}
				if(Clearinghouses.List[i].Eformat!=ElectronicClaimFormat.Canadian){
					for(int j=0;j<claimsByCHouse[i].Count;j++){
						Etranss.SetClaimSentOrPrinted(claimsByCHouse[i][j].ClaimNum,claimsByCHouse[i][j].PatNum,
							Clearinghouses.List[i].ClearinghouseNum,etype,messageText,batchNum);
					}
				}
			}//for(int i=0;i<claimsByCHouse.Length;i++){
		}

		///<summary>If no comm bridge is selected for a clearinghouse, this launches any client program the user has entered.  We do not want to cause a rollback, so no return value.</summary>
		private static void AttemptLaunch(Clearinghouse clearhouse,int batchNum){
			if(clearhouse.ClientProgram==""){
				return;
			}
			if(!File.Exists(clearhouse.ClientProgram)){
				MessageBox.Show(clearhouse.ClientProgram+" "+Lan.g("Eclaims","does not exist."));
				return;
			}
			try{
				Process.Start(clearhouse.ClientProgram);
			}
			catch{
				MessageBox.Show(Lan.g("Eclaims","Client program could not be started.  It may already be running. You must open your client program to finish sending claims."));
			}
		}

		///<summary>Returns a string describing all missing data on this claim.  Claim will not be allowed to be sent electronically unless this string comes back empty.</summary>
		public static string GetMissingData(ClaimSendQueueItem queueItem){
			Clearinghouse clearhouse=Clearinghouses.GetClearinghouse(queueItem.ClearinghouseNum);
			if(clearhouse.Eformat==ElectronicClaimFormat.X12){
				return X12.GetMissingData(queueItem);
			}
			else if(clearhouse.Eformat==ElectronicClaimFormat.Renaissance){
				return Renaissance.GetMissingData(queueItem);
			}
			else if(clearhouse.Eformat==ElectronicClaimFormat.Canadian) {
				return Canadian.GetMissingData(queueItem);
			}
			return "";
		}


	}
}



























