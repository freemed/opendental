using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using OpenDentBusiness;
using OpenDental.Eclaims;

namespace OpenDental{
	///<summary></summary>
	public class EtransL {

		

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