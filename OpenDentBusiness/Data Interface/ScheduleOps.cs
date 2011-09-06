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

		///<summary></summary>
		public static List<ScheduleOp> GetForSched(long scheduleNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ScheduleOp>>(MethodBase.GetCurrentMethod(), scheduleNum);
			}
			string command="SELECT * FROM scheduleOp ";
			command+="WHERE schedulenum = "+scheduleNum;
			return Crud.ScheduleOpCrud.SelectMany(command);
		}

		///<summary>Gets all the ScheduleOps for the list of schedules.</summary>
		public static List<ScheduleOp> GetForSchedList(List<Schedule> schedules) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ScheduleOp>>(MethodBase.GetCurrentMethod(),schedules);
			}
			string command="SELECT * FROM scheduleOp ";
			command+="WHERE ScheduleNum IN ( ";
			for(int i=0;i<schedules.Count;i++) {
				command+=(i>0?",":"")+schedules[i].ScheduleNum.ToString();
			}
			command+=")";
			return Crud.ScheduleOpCrud.SelectMany(command);
		}

		

		
	}

	

	

}













