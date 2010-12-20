using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class TimeAdjusts {

		///<summary></summary>
		public static List<TimeAdjust> Refresh(long empNum,DateTime fromDate,DateTime toDate) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TimeAdjust>>(MethodBase.GetCurrentMethod(),empNum,fromDate,toDate);
			}
			string command=
				"SELECT * FROM timeadjust WHERE "
				+"EmployeeNum = "+POut.Long(empNum)+" "
				+"AND DATE(TimeEntry) >= "+POut.Date(fromDate)+" "
				+"AND DATE(TimeEntry) <= "+POut.Date(toDate)+" "
				+"ORDER BY TimeEntry";
			return Crud.TimeAdjustCrud.SelectMany(command);
		}
	
		///<summary></summary>
		public static long Insert(TimeAdjust timeAdjust) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				timeAdjust.TimeAdjustNum=Meth.GetLong(MethodBase.GetCurrentMethod(),timeAdjust);
				return timeAdjust.TimeAdjustNum;
			}
			return Crud.TimeAdjustCrud.Insert(timeAdjust);
		}

		///<summary></summary>
		public static void Update(TimeAdjust timeAdjust) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),timeAdjust);
				return;
			}
			Crud.TimeAdjustCrud.Update(timeAdjust);
		}

		///<summary></summary>
		public static void Delete(TimeAdjust adj) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),adj);
				return;
			}
			string command= "DELETE FROM timeadjust WHERE TimeAdjustNum = "+POut.Long(adj.TimeAdjustNum);
			Db.NonQ(command);
		}

		
	

		




	}

	
}




