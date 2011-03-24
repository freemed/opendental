using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using OpenDentBusiness;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Etranss {

		///<summary>Gets data for the history grid in the SendClaims window.</summary>
		public static DataTable RefreshHistory(DateTime dateFrom,DateTime dateTo) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),dateFrom,dateTo);
			}
			string command="Select CONCAT(CONCAT(patient.LName,', '),patient.FName) AS PatName,carrier.CarrierName,"
				+"clearinghouse.Description AS Clearinghouse,DateTimeTrans,etrans.OfficeSequenceNumber,"
				+"etrans.CarrierTransCounter,Etype,etrans.ClaimNum,etrans.EtransNum,etrans.AckCode,etrans.Note "
				+"FROM etrans "
				+"LEFT JOIN carrier ON etrans.CarrierNum=carrier.CarrierNum "
				+"LEFT JOIN patient ON patient.PatNum=etrans.PatNum "
				+"LEFT JOIN clearinghouse ON clearinghouse.ClearinghouseNum=etrans.ClearinghouseNum WHERE "
				//if(DataConnection.DBtype==DatabaseType.Oracle){
				//	command+="TO_";
				//}
				+DbHelper.DateColumn("DateTimeTrans")+" >= "+POut.Date(dateFrom)+" AND "
				//if(DataConnection.DBtype==DatabaseType.Oracle){
				//	command+="TO_";
				//}
				+DbHelper.DateColumn("DateTimeTrans")+" <= "+POut.Date(dateTo)+" "
				+"AND Etype!="+POut.Long((int)EtransType.Acknowledge_997)+" "
				+"AND Etype!="+POut.Long((int)EtransType.BenefitInquiry270)+" "
				+"AND Etype!="+POut.Long((int)EtransType.BenefitResponse271)+" "
				+"AND Etype!="+POut.Long((int)EtransType.AckError)+" "
				+"AND Etype!="+POut.Long((int)EtransType.ClaimAck_CA)+" "
				+"AND Etype!="+POut.Long((int)EtransType.ClaimEOB_CA)+" "
				+"ORDER BY DateTimeTrans";
			DataTable table=Db.GetTable(command);
			DataTable tHist=new DataTable("Table");
			tHist.Columns.Add("patName");
			tHist.Columns.Add("CarrierName");
			tHist.Columns.Add("Clearinghouse");
			tHist.Columns.Add("dateTimeTrans");
			tHist.Columns.Add("OfficeSequenceNumber");
			tHist.Columns.Add("CarrierTransCounter");
			tHist.Columns.Add("etype");
			tHist.Columns.Add("Etype");
			tHist.Columns.Add("ClaimNum");
			tHist.Columns.Add("EtransNum");
			tHist.Columns.Add("ack");
			tHist.Columns.Add("Note");
			DataRow row;
			string etype;
			for(int i=0;i<table.Rows.Count;i++) {
				row=tHist.NewRow();
				row["patName"]=table.Rows[i]["PatName"].ToString();
				row["CarrierName"]=table.Rows[i]["CarrierName"].ToString();
				row["Clearinghouse"]=table.Rows[i]["Clearinghouse"].ToString();
				row["dateTimeTrans"]=PIn.DateT(table.Rows[i]["DateTimeTrans"].ToString()).ToShortDateString();
				row["OfficeSequenceNumber"]=table.Rows[i]["OfficeSequenceNumber"].ToString();
				row["CarrierTransCounter"]=table.Rows[i]["CarrierTransCounter"].ToString();
				row["Etype"]=table.Rows[i]["Etype"].ToString();
				etype=Lans.g("enumEtransType",((EtransType)PIn.Long(table.Rows[i]["Etype"].ToString())).ToString());
				if(etype.EndsWith("_CA")){
					etype=etype.Substring(0,etype.Length-3);
				}
				row["etype"]=etype;
				row["ClaimNum"]=table.Rows[i]["ClaimNum"].ToString();
				row["EtransNum"]=table.Rows[i]["EtransNum"].ToString();
				if(table.Rows[i]["AckCode"].ToString()=="A"){
					row["ack"]=Lans.g("Etrans","Accepted");
				}
				else if(table.Rows[i]["AckCode"].ToString()=="R") {
					row["ack"]=Lans.g("Etrans","Rejected");
				}
				row["Note"]=table.Rows[i]["Note"].ToString();
				tHist.Rows.Add(row);
			}
			return tHist;
		}

		///<summary></summary>
		public static List<Etrans> GetHistoryOneClaim(long claimNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Etrans>>(MethodBase.GetCurrentMethod(),claimNum);
			}
			string command="SELECT * FROM etrans WHERE ClaimNum="+POut.Long(claimNum)+" "
				+"AND (Etype="+POut.Int((int)EtransType.Claim_CA)+" "
				+"OR Etype="+POut.Int((int)EtransType.ClaimSent)+")";
			return Crud.EtransCrud.SelectMany(command);
		}

		///<summary></summary>
		public static Etrans GetEtrans(long etransNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Etrans>(MethodBase.GetCurrentMethod(),etransNum);
			}
			string command="SELECT * FROM etrans WHERE EtransNum="+POut.Long(etransNum);
			return Crud.EtransCrud.SelectOne(command);
		}

		///<summary>Gets a list of all 270's and Canadian eligibilities for this plan.</summary>
		public static List<Etrans> GetList270ForPlan(long planNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Etrans>>(MethodBase.GetCurrentMethod(),planNum);
			}
			string command="SELECT * FROM etrans WHERE PlanNum="+POut.Long(planNum)
				+" AND (Etype="+POut.Long((int)EtransType.BenefitInquiry270)
				+" OR Etype="+POut.Long((int)EtransType.Eligibility_CA)+")";
			return Crud.EtransCrud.SelectMany(command);
		}

		///<summary>Use for Canadian claims only. Finds the most recent etrans record which matches the unique officeSequenceNumber specified. The officeSequenceNumber corresponds to field A02.</summary>
		public static Etrans GetForSequenceNumberCanada(string officeSequenceNumber) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Etrans>(MethodBase.GetCurrentMethod(),officeSequenceNumber);
			}
			string command="SELECT * FROM etrans WHERE OfficeSequenceNumber="+POut.String(officeSequenceNumber)+" ORDER BY EtransNum DESC LIMIT 1";
			return Crud.EtransCrud.SelectOne(command);

		}

		/*
		///<summary></summary>
		public static Etrans GetAckForTrans(int etransNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Etrans>(MethodBase.GetCurrentMethod(),etransNum);
			}
			//first, get the actual trans.
			string command="SELECT * FROM etrans WHERE EtransNum="+POut.PInt(etransNum);
			DataTable table=Db.GetTable(command);
			Etrans etrans=SubmitAndFill(table);
			command="SELECT * FROM etrans WHERE "
				+"Etype=21 "//ack997
				+"AND ClearingHouseNum="+POut.PInt(etrans.ClearingHouseNum)
				+" AND BatchNumber= "+POut.PInt(etrans.BatchNumber)
				+" AND DateTimeTrans < "+POut.PDateT(etrans.DateTimeTrans.AddDays(14))//less than 2wks in the future
				+" AND DateTimeTrans > "+POut.PDateT(etrans.DateTimeTrans.AddDays(-1));//and no more than one day before claim
			table=Db.GetTable(command);
			return SubmitAndFill(table);
		}*/

		/*
		private static List<Etrans> SubmitAndFill(DataTable table){
			//No need to check RemotingRole; no call to db.
			//if(table.Rows.Count==0){
			//	return null;
			//}
			List<Etrans> retVal=new List<Etrans>();
			Etrans etrans;
			for(int i=0;i<table.Rows.Count;i++) {
				etrans=new Etrans();
				etrans.EtransNum           =PIn.Long(table.Rows[i][0].ToString());
				etrans.DateTimeTrans       =PIn.DateT(table.Rows[i][1].ToString());
				etrans.ClearingHouseNum    =PIn.Long(table.Rows[i][2].ToString());
				etrans.Etype               =(EtransType)PIn.Long(table.Rows[i][3].ToString());
				etrans.ClaimNum            =PIn.Long(table.Rows[i][4].ToString());
				etrans.OfficeSequenceNumber=PIn.Int(table.Rows[i][5].ToString());
				etrans.CarrierTransCounter =PIn.Int(table.Rows[i][6].ToString());
				etrans.CarrierTransCounter2=PIn.Int(table.Rows[i][7].ToString());
				etrans.CarrierNum          =PIn.Long(table.Rows[i][8].ToString());
				etrans.CarrierNum2         =PIn.Long(table.Rows[i][9].ToString());
				etrans.PatNum              =PIn.Long(table.Rows[i][10].ToString());
				etrans.BatchNumber         =PIn.Int(table.Rows[i][11].ToString());
				etrans.AckCode             =PIn.String(table.Rows[i][12].ToString());
				etrans.TransSetNum         =PIn.Int(table.Rows[i][13].ToString());
				etrans.Note                =PIn.String(table.Rows[i][14].ToString());
				etrans.EtransMessageTextNum=PIn.Long(table.Rows[i][15].ToString());
				etrans.AckEtransNum        =PIn.Long(table.Rows[i][16].ToString());
				etrans.PlanNum             =PIn.Long(table.Rows[i][17].ToString());
				retVal.Add(etrans);
			}
			return retVal;
		}*/

		///<summary>DateTimeTrans handled automatically here.</summary>
		public static long Insert(Etrans etrans) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				etrans.EtransNum=Meth.GetLong(MethodBase.GetCurrentMethod(),etrans);
				return etrans.EtransNum;
			}
			return Crud.EtransCrud.Insert(etrans);
		}

		///<summary></summary>
		public static void Update(Etrans etrans) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),etrans);
				return;
			}
			Crud.EtransCrud.Update(etrans);
		}

		///<summary>Not for claim types, just other types, including Eligibility. This function gets run first.  Then, the messagetext is created and an attempt is made to send the message.  Finally, the messagetext is added to the etrans.  This is necessary because the transaction numbers must be incremented and assigned to each message before creating the message and attempting to send.  If it fails, we will need to roll back.  Provide EITHER a carrierNum OR a canadianNetworkNum.  Many transactions can be sent to a carrier or to a network.</summary>
		public static Etrans CreateCanadianOutput(long patNum,long carrierNum,long canadianNetworkNum,long clearinghouseNum,EtransType etype,long planNum,long insSubNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Etrans>(MethodBase.GetCurrentMethod(),patNum,carrierNum,canadianNetworkNum,clearinghouseNum,etype,planNum,insSubNum);
			}
			//validation of carrier vs network
			if(etype==EtransType.Eligibility_CA){
				//only carrierNum is allowed (and required)
				if(carrierNum==0){
					throw new ApplicationException("Carrier not supplied for Etranss.CreateCanadianOutput.");
				}
				if(canadianNetworkNum!=0){
					throw new ApplicationException("NetworkNum not allowed for Etranss.CreateCanadianOutput.");
				}
			}
			Etrans etrans=new Etrans();
			//etrans.DateTimeTrans handled automatically
			etrans.ClearingHouseNum=clearinghouseNum;
			etrans.Etype=etype;
			etrans.ClaimNum=0;//no claim involved
			etrans.PatNum=patNum;
			//CanadianNetworkNum?
			etrans.CarrierNum=carrierNum;
			etrans.PlanNum=planNum;
			etrans.InsSubNum=insSubNum;
			//Get next OfficeSequenceNumber-----------------------------------------------------------------------------------------
			etrans.OfficeSequenceNumber=0;
			string command="SELECT MAX(OfficeSequenceNumber) FROM etrans";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count>0) {
				etrans.OfficeSequenceNumber=PIn.Int(table.Rows[0][0].ToString());
				if(etrans.OfficeSequenceNumber==999999){//if the office has sent > 1 million messages, and has looped back around to 1.
					//get the date of the most recent max
					//This works, but it got even more complex for CarrierTransCounter, so we will just throw an exception for now.
					/*command="SELECT MAX(DateTimeTrans) FROM etrans WHERE OfficeSequenceNumber=999999";
					table=Db.GetTable(command);
					DateTime maxDateT=PIn.PDateT(table.Rows[0][0].ToString());
					//then, just get the max that's newer than that.
					command="SELECT MAX(OfficeSequenceNumber) FROM etrans WHERE DateTimeTrans > '"+POut.PDateT(maxDateT)+"'";
					table=Db.GetTable(command);
					if(table.Rows.Count>0) {
						etrans.OfficeSequenceNumber=PIn.PInt(table.Rows[0][0].ToString());
					}*/
					throw new ApplicationException("OfficeSequenceNumber has maxed out at 999999.  This program will need to be enhanced.");
				}
			}
			#if DEBUG
				etrans.OfficeSequenceNumber=PIn.Int(File.ReadAllText(@"..\..\..\TestCanada\LastOfficeSequenceNumber.txt"));
				File.WriteAllText(@"..\..\..\TestCanada\LastOfficeSequenceNumber.txt",(etrans.OfficeSequenceNumber+1).ToString());
			#endif
			etrans.OfficeSequenceNumber++;
			//if(etype==EtransType.Eligibility_CA){ //The counter must be incremented for all transaction types, according to the documentation for field A09 Carrier Transaction Counter.
				//find the next CarrierTransCounter------------------------------------------------------------------------------------
				etrans.CarrierTransCounter=0;
				command="SELECT MAX(CarrierTransCounter) FROM etrans "
					+"WHERE CarrierNum="+POut.Long(etrans.CarrierNum);
				table=Db.GetTable(command);
				int tempcounter=0;
				if(table.Rows.Count>0) {
					tempcounter=PIn.Int(table.Rows[0][0].ToString());
				}
				if(tempcounter>etrans.CarrierTransCounter) {
					etrans.CarrierTransCounter=tempcounter;
				}
				command="SELECT MAX(CarrierTransCounter2) FROM etrans "
					+"WHERE CarrierNum2="+POut.Long(etrans.CarrierNum);
				table=Db.GetTable(command);
				if(table.Rows.Count>0) {
					tempcounter=PIn.Int(table.Rows[0][0].ToString());
				}
				if(tempcounter>etrans.CarrierTransCounter) {
					etrans.CarrierTransCounter=tempcounter;
				}
				if(etrans.CarrierTransCounter==99999){
					throw new ApplicationException("CarrierTransCounter has maxed out at 99999.  This program will need to be enhanced.");
					//maybe by adding a reset date to the preference table which will apply to all counters as a whole.
				}
				etrans.CarrierTransCounter++;
			//}
			Insert(etrans);
			return etrans;
		}

		///<summary>Only used by Canadian code right now.  CAUTION!  This does not update the EtransMessageTextNum field of an object in memory without a refresh.</summary>
		public static void SetMessage(long etransNum,string messageText) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),etransNum,messageText);
				return;
			}
			EtransMessageText msg=new EtransMessageText();
			msg.MessageText=messageText;
			EtransMessageTexts.Insert(msg);
			//string command=
			string command= "UPDATE etrans SET EtransMessageTextNum="+POut.Long(msg.EtransMessageTextNum)+" "
				+"WHERE EtransNum = '"+POut.Long(etransNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Deletes the etrans entry and changes the status of the claim back to W.  If it encounters an entry that's not a claim, it skips it for now.  Later, it will handle all types of undo.  It will also check Canadian claims to prevent alteration if an ack or EOB has been received.</summary>
		public static void Undo(long etransNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),etransNum);
				return;
			}
			//see if it's a claim.
			string command="SELECT ClaimNum FROM etrans WHERE EtransNum="+POut.Long(etransNum);
			DataTable table=Db.GetTable(command);
			long claimNum=PIn.Long(table.Rows[0][0].ToString());
			if(claimNum==0){//if no claim
				return;//for now
			}
			//future Canadian check will go here

			//Change the claim back to W.
			command="UPDATE claim SET ClaimStatus='W' WHERE ClaimNum="+POut.Long(claimNum);
			Db.NonQ(command);
			//Delete this etrans
			command="DELETE FROM etrans WHERE EtransNum="+POut.Long(etransNum);
			Db.NonQ(command);
		}

		///<summary>Deletes the etrans entry.  Mostly used when the etrans entry was created, but then the communication with the clearinghouse failed.  So this is just a rollback function.  Will not delete the message associated with the etrans.  That must be done separately.  Will throw exception if the etrans does not exist.</summary>
		public static void Delete(long etransNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),etransNum);
				return;
			}
			//see if there's a message
			string command;//="SELECT EtransMessageTextNum FROM etrans WHERE EtransNum="+POut.PInt(etransNum);
			//DataTable table=Db.GetTable(command);
			//if(table.Rows[0][0].ToString()!="0"){//this throws exception if 0 rows.
			//	throw new ApplicationException("Error. Etrans must not have messagetext attached yet.");
			//}
			command="DELETE FROM etrans WHERE EtransNum="+POut.Long(etransNum);
			Db.NonQ(command);
		}

		///<summary>Sets the status of the claim to sent, usually as part of printing.  Also makes an entry in etrans.  If this is Canadian eclaims, then this function gets run first, and it doesn't actually set the claim as sent.  If the claim is to be sent elecronically, then the messagetext is created after this method and an attempt is made to send the claim.  Finally, the messagetext is added to the etrans.  This is necessary because the transaction numbers must be incremented and assigned to each claim before creating the message and attempting to send.  For Canadians, it will always record the attempt as an etrans even if claim is not set to status of sent.</summary>
		public static Etrans SetClaimSentOrPrinted(long claimNum,long patNum,long clearinghouseNum,EtransType etype,int batchNumber) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Etrans>(MethodBase.GetCurrentMethod(),claimNum,patNum,clearinghouseNum,etype,batchNumber);
			}
			string command;
			Etrans etrans=new Etrans();
			//etrans.DateTimeTrans handled automatically
			etrans.ClearingHouseNum=clearinghouseNum;
			etrans.Etype=etype;
			etrans.ClaimNum=claimNum;
			etrans.PatNum=patNum;
			//Get the primary and secondary carrierNums for this claim.
			command="SELECT carrier1.CarrierNum,carrier2.CarrierNum AS CarrierNum2 FROM claim "
				+"LEFT JOIN insplan insplan1 ON insplan1.PlanNum=claim.PlanNum "
				+"LEFT JOIN carrier carrier1 ON carrier1.CarrierNum=insplan1.CarrierNum "
				+"LEFT JOIN insplan insplan2 ON insplan2.PlanNum=claim.PlanNum2 "
				+"LEFT JOIN carrier carrier2 ON carrier2.CarrierNum=insplan2.CarrierNum "
				+"WHERE claim.ClaimNum="+POut.Long(claimNum);
			DataTable table=Db.GetTable(command);
			etrans.CarrierNum=PIn.Long(table.Rows[0][0].ToString());
			etrans.CarrierNum2=PIn.Long(table.Rows[0][1].ToString());//might be 0 if no secondary on this claim
			etrans.BatchNumber=batchNumber;
			//if(X837.IsX12(messageText)) {
			//	X837 x837=new X837(messageText);
			//	etrans.TransSetNum=x837.GetTransNum(claimNum);
			//}
			if(etype==EtransType.Claim_CA || etype==EtransType.ClaimCOB_CA) {
				etrans.OfficeSequenceNumber=0;
				//find the next officeSequenceNumber
				command="SELECT MAX(OfficeSequenceNumber) FROM etrans";
				table=Db.GetTable(command);
				if(table.Rows.Count>0) {
					etrans.OfficeSequenceNumber=PIn.Int(table.Rows[0][0].ToString());
					if(etrans.OfficeSequenceNumber==999999) {//if the office has sent > 1 million messages, and has looped back around to 1.
						throw new ApplicationException
							("OfficeSequenceNumber has maxed out at 999999.  This program will need to be enhanced.");
					}
				}
#if DEBUG
				etrans.OfficeSequenceNumber=PIn.Int(File.ReadAllText(@"..\..\..\TestCanada\LastOfficeSequenceNumber.txt"));
				File.WriteAllText(@"..\..\..\TestCanada\LastOfficeSequenceNumber.txt",(etrans.OfficeSequenceNumber+1).ToString());
#endif
				etrans.OfficeSequenceNumber++;
				//find the next CarrierTransCounter for the primary carrier
				etrans.CarrierTransCounter=0;
				command="SELECT MAX(CarrierTransCounter) FROM etrans "
					+"WHERE CarrierNum="+POut.Long(etrans.CarrierNum);
				table=Db.GetTable(command);
				int tempcounter=0;
				if(table.Rows.Count>0) {
					tempcounter=PIn.Int(table.Rows[0][0].ToString());
				}
				if(tempcounter>etrans.CarrierTransCounter) {
					etrans.CarrierTransCounter=tempcounter;
				}
				command="SELECT MAX(CarrierTransCounter2) FROM etrans "
					+"WHERE CarrierNum2="+POut.Long(etrans.CarrierNum);
				table=Db.GetTable(command);
				if(table.Rows.Count>0) {
					tempcounter=PIn.Int(table.Rows[0][0].ToString());
				}
				if(tempcounter>etrans.CarrierTransCounter) {
					etrans.CarrierTransCounter=tempcounter;
				}
				if(etrans.CarrierTransCounter==99999) {
					throw new ApplicationException("CarrierTransCounter has maxed out at 99999.  This program will need to be enhanced.");
				}
				etrans.CarrierTransCounter++;
				if(etrans.CarrierNum2>0) {//if there is secondary coverage on this claim
					etrans.CarrierTransCounter2=1;
					command="SELECT MAX(CarrierTransCounter) FROM etrans "
						+"WHERE CarrierNum="+POut.Long(etrans.CarrierNum2);
					table=Db.GetTable(command);
					if(table.Rows.Count>0) {
						tempcounter=PIn.Int(table.Rows[0][0].ToString());
					}
					if(tempcounter>etrans.CarrierTransCounter2) {
						etrans.CarrierTransCounter2=tempcounter;
					}
					command="SELECT MAX(CarrierTransCounter2) FROM etrans "
						+"WHERE CarrierNum2="+POut.Long(etrans.CarrierNum2);
					table=Db.GetTable(command);
					if(table.Rows.Count>0) {
						tempcounter=PIn.Int(table.Rows[0][0].ToString());
					}
					if(tempcounter>etrans.CarrierTransCounter2) {
						etrans.CarrierTransCounter2=tempcounter;
					}
					if(etrans.CarrierTransCounter2==99999) {
						throw new ApplicationException("CarrierTransCounter has maxed out at 99999.  This program will need to be enhanced.");
					}
					etrans.CarrierTransCounter2++;
				}
			}
			else {//not Canadian
				command="UPDATE claim SET ClaimStatus = 'S',"
					+"DateSent= "+POut.Date(MiscData.GetNowDateTime())
					+" WHERE claimnum = "+POut.Long(claimNum);
				Db.NonQ(command);
			}
			EtransMessageText etransMessageText=new EtransMessageText();
			etransMessageText.MessageText="";
			EtransMessageTexts.Insert(etransMessageText);
			etrans.EtransMessageTextNum=etransMessageText.EtransMessageTextNum;
			Etranss.Insert(etrans);
			return etrans;
		}

		///<summary>Etrans type will be figured out by this class.  Either TextReport, Acknowledge_997, or StatusNotify_277.</summary>
		public static void ProcessIncomingReport(DateTime dateTimeTrans,long clearinghouseNum,string messageText) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),dateTimeTrans,clearinghouseNum,messageText);
				return;
			}
			Etrans etrans=new Etrans();
			etrans.DateTimeTrans=dateTimeTrans;
			etrans.ClearingHouseNum=clearinghouseNum;
			EtransMessageText etransMessageText=new EtransMessageText();
			etransMessageText.MessageText=messageText;
			EtransMessageTexts.Insert(etransMessageText);
			etrans.EtransMessageTextNum=etransMessageText.EtransMessageTextNum;
			string command;
			if(X12object.IsX12(messageText)) {
				X12object Xobj=new X12object(messageText);
				if(Xobj.Is997()) {
					X997 x997=new X997(messageText);
					etrans.Etype=EtransType.Acknowledge_997;
					etrans.BatchNumber=x997.GetBatchNumber();
					Etranss.Insert(etrans);
					string batchack=x997.GetBatchAckCode();
					if(batchack=="A"||batchack=="R") {//accepted or rejected
						command="UPDATE etrans SET AckCode='"+batchack+"', "
							+"AckEtransNum="+POut.Long(etrans.EtransNum)
							+" WHERE BatchNumber="+POut.Long(etrans.BatchNumber)
							+" AND ClearinghouseNum="+POut.Long(clearinghouseNum)
							+" AND DateTimeTrans > "+POut.DateT(dateTimeTrans.AddDays(-14))
							+" AND DateTimeTrans < "+POut.DateT(dateTimeTrans.AddDays(1))
							+" AND AckEtransNum=0";
						Db.NonQ(command);
					}
					else {//partially accepted
						List<int> transNums=x997.GetTransNums();
						string ack;
						for(int i=0;i<transNums.Count;i++) {
							ack=x997.GetAckForTrans(transNums[i]);
							if(ack=="A"||ack=="R") {//accepted or rejected
								command="UPDATE etrans SET AckCode='"+ack+"', "
									+"AckEtransNum="+POut.Long(etrans.EtransNum)
									+" WHERE BatchNumber="+POut.Long(etrans.BatchNumber)
									+" AND TransSetNum="+POut.Long(transNums[i])
									+" AND ClearinghouseNum="+POut.Long(clearinghouseNum)
									+" AND DateTimeTrans > "+POut.DateT(dateTimeTrans.AddDays(-14))
									+" AND DateTimeTrans < "+POut.DateT(dateTimeTrans.AddDays(1))
									+" AND AckEtransNum=0";
								Db.NonQ(command);
							}
						}
					}
					//none of the other fields make sense, because this ack could refer to many claims.
				}
				else if(X277U.Is277U(Xobj)) {
					etrans.Etype=EtransType.StatusNotify_277;
					//later: analyze to figure out which e-claim is being referenced.
					Etranss.Insert(etrans);
				}
				else {//unknown type of X12 report.
					etrans.Etype=EtransType.TextReport;
					Etranss.Insert(etrans);
				}
			}
			else {//not X12
				etrans.Etype=EtransType.TextReport;
				Etranss.Insert(etrans);
			}
		}

		/// <summary>Or Canadian elig.</summary>
		public static DateTime GetLastDate270(long planNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<DateTime>(MethodBase.GetCurrentMethod(),planNum);
			}
			string command="SELECT MAX(DateTimeTrans) FROM etrans "
				+"WHERE (Etype="+POut.Int((int)EtransType.BenefitInquiry270)+" "
				+"OR Etype="+POut.Int((int)EtransType.Eligibility_CA)+") "
				+" AND PlanNum="+POut.Long(planNum);
			return PIn.Date(Db.GetScalar(command));
		}



		
	}
}