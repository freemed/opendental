using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using OpenDentBusiness;
using OpenDental.Eclaims;

namespace OpenDental{
	///<summary></summary>
	public class EtransL {

		///<summary>Sets the status of the claim to sent.  Also makes an entry in etrans.  If this is canadian eclaims, then this function gets run first.  Then, the messagetext is created and an attempt is made to send the claim.  Finally, the messagetext and added to the etrans.  This is necessary because the transaction numbers must be incremented and assigned to each claim before creating the message and attempting to send.  If it fails, Canadians will need to delete the etrans entries (or we will need to roll back the changes).</summary>
		public static Etrans SetClaimSentOrPrinted(int claimNum,int patNum,int clearinghouseNum,EtransType etype,
			string messageText,int batchNumber) {
			string command="UPDATE claim SET ClaimStatus = 'S',"
				+"DateSent= "+POut.PDate(MiscData.GetNowDateTime())
				+" WHERE claimnum = "+POut.PInt(claimNum);
			Db.NonQ(command);
			Etrans etrans=new Etrans();
			//etrans.DateTimeTrans handled automatically
			etrans.ClearinghouseNum=clearinghouseNum;
			etrans.Etype=etype;
			etrans.ClaimNum=claimNum;
			etrans.PatNum=patNum;
			//Get the primary and secondary carrierNums for this claim.
			command="SELECT carrier1.CarrierNum,carrier2.CarrierNum AS CarrierNum2 FROM claim "
				+"LEFT JOIN insplan insplan1 ON insplan1.PlanNum=claim.PlanNum "
				+"LEFT JOIN carrier carrier1 ON carrier1.CarrierNum=insplan1.CarrierNum "
				+"LEFT JOIN insplan insplan2 ON insplan2.PlanNum=claim.PlanNum2 "
				+"LEFT JOIN carrier carrier2 ON carrier2.CarrierNum=insplan2.CarrierNum "
				+"WHERE claim.ClaimNum="+POut.PInt(claimNum);
			DataTable table=Db.GetTable(command);
			etrans.CarrierNum=PIn.PInt(table.Rows[0][0].ToString());
			etrans.CarrierNum2=PIn.PInt(table.Rows[0][1].ToString());//might be 0 if no secondary on this claim
			etrans.MessageText=messageText;
			etrans.BatchNumber=batchNumber;
			if(X837.IsX12(messageText)) {
				X837 x837=new X837(messageText);
				etrans.TransSetNum=x837.GetTransNum(claimNum);
			}
			if(etype==EtransType.Claim_CA) {
				etrans.OfficeSequenceNumber=0;
				//find the next officeSequenceNumber
				command="SELECT MAX(OfficeSequenceNumber) FROM etrans";
				table=Db.GetTable(command);
				if(table.Rows.Count>0) {
					etrans.OfficeSequenceNumber=PIn.PInt(table.Rows[0][0].ToString());
					if(etrans.OfficeSequenceNumber==999999) {//if the office has sent > 1 million messages, and has looped back around to 1.
						throw new ApplicationException
							("OfficeSequenceNumber has maxed out at 999999.  This program will need to be enhanced.");
					}
				}
				etrans.OfficeSequenceNumber++;
				//find the next CarrierTransCounter for the primary carrier
				etrans.CarrierTransCounter=0;
				command="SELECT MAX(CarrierTransCounter) FROM etrans "
					+"WHERE CarrierNum="+POut.PInt(etrans.CarrierNum);
				table=Db.GetTable(command);
				int tempcounter=0;
				if(table.Rows.Count>0) {
					tempcounter=PIn.PInt(table.Rows[0][0].ToString());
				}
				if(tempcounter>etrans.CarrierTransCounter) {
					etrans.CarrierTransCounter=tempcounter;
				}
				command="SELECT MAX(CarrierTransCounter2) FROM etrans "
					+"WHERE CarrierNum2="+POut.PInt(etrans.CarrierNum);
				table=Db.GetTable(command);
				if(table.Rows.Count>0) {
					tempcounter=PIn.PInt(table.Rows[0][0].ToString());
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
						+"WHERE CarrierNum="+POut.PInt(etrans.CarrierNum2);
					table=Db.GetTable(command);
					if(table.Rows.Count>0) {
						tempcounter=PIn.PInt(table.Rows[0][0].ToString());
					}
					if(tempcounter>etrans.CarrierTransCounter2) {
						etrans.CarrierTransCounter2=tempcounter;
					}
					command="SELECT MAX(CarrierTransCounter2) FROM etrans "
						+"WHERE CarrierNum2="+POut.PInt(etrans.CarrierNum2);
					table=Db.GetTable(command);
					if(table.Rows.Count>0) {
						tempcounter=PIn.PInt(table.Rows[0][0].ToString());
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
			Etranss.Insert(etrans);
			return etrans;
		}

		///<summary>Etrans type will be figured out by this class.  Either TextReport, Acknowledge_997, or StatusNotify_277.</summary>
		public static void ProcessIncomingReport(DateTime dateTimeTrans,int clearinghouseNum,string messageText) {
			Etrans etrans=new Etrans();
			etrans.DateTimeTrans=dateTimeTrans;
			etrans.ClearinghouseNum=clearinghouseNum;
			etrans.MessageText=messageText;
			string command;
			if(X12object.IsX12(messageText)) {
				X12object Xobj=new X12object(messageText);
				if(Xobj.Is997()) {
					X997 x997=new X997(messageText);
					etrans.Etype=EtransType.Acknowledge_997;
					etrans.BatchNumber=x997.GetBatchNumber();
					string batchack=x997.GetBatchAckCode();
					if(batchack=="A"||batchack=="R") {//accepted or rejected
						command="UPDATE etrans SET AckCode='"+batchack+"' WHERE BatchNumber="+POut.PInt(etrans.BatchNumber)
							+" AND ClearinghouseNum="+POut.PInt(clearinghouseNum)
							+" AND DateTimeTrans > "+POut.PDateT(dateTimeTrans.AddDays(-14))
							+" AND DateTimeTrans < "+POut.PDateT(dateTimeTrans.AddDays(1));
						Db.NonQ(command);
					} else {//partially accepted
						List<int> transNums=x997.GetTransNums();
						string ack;
						for(int i=0;i<transNums.Count;i++) {
							ack=x997.GetAckForTrans(transNums[i]);
							if(ack=="A"||ack=="R") {//accepted or rejected
								command="UPDATE etrans SET AckCode='"+ack+"' WHERE BatchNumber="+POut.PInt(etrans.BatchNumber)
									+" AND TransSetNum="+POut.PInt(transNums[i])
									+" AND ClearinghouseNum="+POut.PInt(clearinghouseNum)
									+" AND DateTimeTrans > "+POut.PDateT(dateTimeTrans.AddDays(-14))
									+" AND DateTimeTrans < "+POut.PDateT(dateTimeTrans.AddDays(1));
								Db.NonQ(command);
							}
						}
					}
					//none of the other fields make sense, because this ack could refer to many claims.
				} else if(X277U.Is277U(Xobj)) {
					etrans.Etype=EtransType.StatusNotify_277;
					//later: analyze to figure out which e-claim is being referenced.
				} else {//unknown type of X12 report.
					etrans.Etype=EtransType.TextReport;
				}
			} else {//not X12
				etrans.Etype=EtransType.TextReport;
			}
			Etranss.Insert(etrans);
		}

	}
}