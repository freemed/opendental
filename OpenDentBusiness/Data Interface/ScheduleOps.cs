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
			if(PrefC.RandomKeys){
				op.ScheduleOpNum=ReplicationServers.GetKey("scheduleop","ScheduleOpNum");
			}
			string command= "INSERT INTO scheduleop (";
			if(PrefC.RandomKeys){
				command+="ScheduleOpNum,";
			}
			command+="ScheduleNum,OperatoryNum) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PLong(op.ScheduleOpNum)+"', ";
			}
			command+=
				 "'"+POut.PLong   (op.ScheduleNum)+"', "
				+"'"+POut.PLong   (op.OperatoryNum)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				op.ScheduleOpNum=Db.NonQ(command,true);
			}
			return op.ScheduleOpNum;
		}

		
	}

	

	

}













