using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class HL7Msgs{

		public static List<HL7Msg> GetAllPending(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<HL7Msg>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM hl7msg WHERE HL7Status="+POut.Long((int)HL7MessageStatus.OutPending);
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


	}
}