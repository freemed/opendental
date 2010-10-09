using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public static class TimeAdjusts {

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

		///<summary>-H:mm.  If zero, then returns empty string.  Hours can be greater than 24.</summary>
		public static string ToStringHmm(this TimeSpan tspan){
			//No need to check RemotingRole; no call to db.
			if(tspan==TimeSpan.Zero) {
				return "";
			}
			string retVal="";
			if(tspan < TimeSpan.Zero){
				retVal+="-";
				tspan=tspan.Duration();
			}
			//It has to be done this way to support hours greater than 24.
			int hours=(tspan.Days*24)+tspan.Hours;
			retVal+=hours.ToString()+":"+tspan.Minutes.ToString().PadLeft(2,'0');
			return retVal;
		}
	
		///<summary>-H:mm:ss.  If zero, then returns empty string.</summary>
		public static string ToStringHmmss(this TimeSpan tspan){
			//No need to check RemotingRole; no call to db.
			if(tspan==TimeSpan.Zero) {
				return "";
			}
			string retVal="";
			if(tspan < TimeSpan.Zero){
				retVal+="-";
				tspan=tspan.Duration();
			}
			int hours=(tspan.Days*24)+tspan.Hours;
			retVal+=hours.ToString()+":"+tspan.Minutes.ToString().PadLeft(2,'0')+":"+tspan.Seconds.ToString().PadLeft(2,'0');
			return retVal;
		}
	

		




	}

	
}




