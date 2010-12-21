using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ScheduleOps {
		///<summary></summary>
		public static long Insert(ScheduleOp op) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				op.ScheduleOpNum=Meth.GetLong(MethodBase.GetCurrentMethod(),op);
				return op.ScheduleOpNum;
			}
			return Crud.ScheduleOpCrud.Insert(op);
		}

		
	}

	

	

}













