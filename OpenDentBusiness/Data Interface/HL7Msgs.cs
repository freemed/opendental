using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class HL7Msgs{

		public static List<HL7Msg> GetOnePending(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<HL7Msg>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM hl7msg WHERE HL7Status="+POut.Long((int)HL7MessageStatus.OutPending)+" "+DbHelper.LimitAnd(1);
			return Crud.HL7MsgCrud.SelectMany(command);//Just 0 or 1 item in list for now.
		}

		///<summary>When called we will make sure to send a startDate and endDate.  Status parameter 0:All, 1:OutPending, 2:OutSent, 3:OutFailed, 4:InProcessed, 5:InFailed</summary>
		public static List<HL7Msg> GetHL7Msgs(DateTime startDate,DateTime endDate,long patNum,int status) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<HL7Msg>>(MethodBase.GetCurrentMethod(),startDate,endDate,patNum,status);
			}
			//join with the patient table so we can display patient name instead of PatNum
			string command=@"SELECT *	FROM hl7msg	WHERE DATE(hl7msg.DateTStamp) BETWEEN "+POut.Date(startDate)+" AND "+POut.Date(endDate)+" ";
			if(patNum>0) {
				command+="AND hl7msg.PatNum="+POut.Long(patNum)+" ";
			}
			if(status>0) {
				command+="AND hl7msg.HL7Status="+POut.Long(status-1)+" ";//minus 1 because 0=All but our enum starts at 0
			}
			command+="ORDER BY hl7msg.DateTStamp";
			return Crud.HL7MsgCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(HL7Msg hL7Msg) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				hL7Msg.HL7MsgNum=Meth.GetLong(MethodBase.GetCurrentMethod(),hL7Msg);
				return hL7Msg.HL7MsgNum;
			}
			return Crud.HL7MsgCrud.Insert(hL7Msg);
		}

		///<summary></summary>
		public static void Update(HL7Msg hL7Msg) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),hL7Msg);
				return;
			}
			Crud.HL7MsgCrud.Update(hL7Msg);
		}

		///<summary></summary>
		public static bool MessageWasSent(long aptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),aptNum);
			}
			string command="SELECT COUNT(*) FROM hl7msg WHERE AptNum="+POut.Long(aptNum);
			if(Db.GetCount(command)=="0") {
				return false;
			}
			return true;
		}

		///<summary>Doesn't delete the old messages, but just the text of the message.  This avoids breaking MessageWasSent().  Only affects messages that are at least four months old, regardless of status.</summary>
		public static void DeleteOldMsgText() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="UPDATE hl7msg SET MsgText='' "
				+"WHERE DateTStamp < ADDDATE(CURDATE(),INTERVAL -4 MONTH)";
			Db.NonQ(command);
		}

		public static List<HL7Msg> GetOneExisting(HL7Msg hl7Msg) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<HL7Msg>>(MethodBase.GetCurrentMethod(),hl7Msg);
			}
			string command="SELECT * FROM hl7msg WHERE MsgText='"+POut.String(hl7Msg.MsgText)+"' "+DbHelper.LimitAnd(1);
			return Crud.HL7MsgCrud.SelectMany(command);//Just 0 or 1 item in list for now.
		}

		public static void UpdateDateTStamp(HL7Msg hl7Msg) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),hl7Msg);
				return;
			}
			string command="UPDATE hl7msg SET DateTStamp=CURRENT_TIMESTAMP WHERE MsgText='"+POut.String(hl7Msg.MsgText)+"' ";
			Db.NonQ(command);
		}
	}
}