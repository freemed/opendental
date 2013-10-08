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
				+"AND "+DbHelper.DateColumn("TimeEntry")+" >= "+POut.Date(fromDate)+" "
				+"AND "+DbHelper.DateColumn("TimeEntry")+" <= "+POut.Date(toDate)+" "
				+"ORDER BY TimeEntry";
			return Crud.TimeAdjustCrud.SelectMany(command);
		}

		///<summary>Validates and throws exceptions. Gets all time adjusts between date range and time adjusts made during the current work week. </summary>
		public static List<TimeAdjust> GetValidList(long empNum,DateTime fromDate,DateTime toDate) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TimeAdjust>>(MethodBase.GetCurrentMethod(),empNum,fromDate,toDate);
			}
			List<TimeAdjust> retVal=new List<TimeAdjust>();
			string command=
				"SELECT * FROM timeadjust WHERE "
				+"EmployeeNum = "+POut.Long(empNum)+" "
				+"AND "+DbHelper.DateColumn("TimeEntry")+" >= "+POut.Date(fromDate)+" "
				+"AND "+DbHelper.DateColumn("TimeEntry")+" <= "+POut.Date(toDate)+" "
				+"ORDER BY TimeEntry";
			retVal=Crud.TimeAdjustCrud.SelectMany(command);
			//Validate---------------------------------------------------------------------------------------------------------------
			//none necessary at this time.
			return retVal;
		}

		///<summary>Validates and throws exceptions.  Deletes automatic adjustments that fall within the pay period.</summary>
		public static List<TimeAdjust> GetListForTimeCardManage(long empNum,DateTime fromDate,DateTime toDate) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TimeAdjust>>(MethodBase.GetCurrentMethod(),empNum,fromDate,toDate);
			}
			List<TimeAdjust> retVal=new List<TimeAdjust>();
			//List<TimeAdjust> listTimeAdjusts=new List<TimeAdjust>();
			string command=
				"SELECT * FROM timeadjust WHERE "
				+"EmployeeNum = "+POut.Long(empNum)+" "
				+"AND "+DbHelper.DateColumn("TimeEntry")+" >= "+POut.Date(fromDate)+" "
				+"AND "+DbHelper.DateColumn("TimeEntry")+" <= "+POut.Date(toDate)+" "
				+"ORDER BY TimeEntry";
			//listTimeAdjusts=Crud.TimeAdjustCrud.SelectMany(command);
			return Crud.TimeAdjustCrud.SelectMany(command);
			//Delete automatic adjustments.------------------------------------------------------------------------------------------
			//for(int i=0;i<listTimeAdjusts.Count;i++) {
			//	if(!listTimeAdjusts[i].IsAuto) {//skip and never delete manual adjustments
			//		retVal.Add(listTimeAdjusts[i]);
			//		continue;
			//	}
			//	TimeAdjusts.Delete(listTimeAdjusts[i]);//delete auto adjustments for current pay period
			//}
			//Validate---------------------------------------------------------------------------------------------------------------
			//none necessary at this time.
			//return retVal;
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

		///<summary>Returns all automatically generated timeAdjusts for a given employee between the date range (inclusive).</summary>
		internal static List<TimeAdjust> GetSimpleListAuto(long employeeNum,DateTime startDate,DateTime stopDate) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TimeAdjust>>(MethodBase.GetCurrentMethod(),employeeNum,startDate,stopDate);
			}
			List<TimeAdjust> retVal=new List<TimeAdjust>();
			//List<TimeAdjust> listTimeAdjusts=new List<TimeAdjust>();
			string command=
				"SELECT * FROM timeadjust WHERE "
				+"EmployeeNum = "+POut.Long(employeeNum)+" "
				+"AND "+DbHelper.DateColumn("TimeEntry")+" >= "+POut.Date(startDate)+" "
				+"AND "+DbHelper.DateColumn("TimeEntry")+" < "+POut.Date(stopDate.AddDays(1))+" "//add one day to go the end of the specified date.
				+"AND IsAuto=1";
			//listTimeAdjusts=Crud.TimeAdjustCrud.SelectMany(command);
			return Crud.TimeAdjustCrud.SelectMany(command);
			
		}









	}

	
}




