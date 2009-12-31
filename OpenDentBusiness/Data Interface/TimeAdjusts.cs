using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class TimeAdjusts {

		///<summary></summary>
		public static TimeAdjust[] Refresh(long empNum,DateTime fromDate,DateTime toDate) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<TimeAdjust[]>(MethodBase.GetCurrentMethod(),empNum,fromDate,toDate);
			}
			string command=
				"SELECT * from timeadjust WHERE"
				+" EmployeeNum = '"+POut.Long(empNum)+"'"
				+" AND TimeEntry >= "+POut.Date(fromDate)
				//adding a day takes it to midnight of the specified toDate
				+" AND TimeEntry <= "+POut.Date(toDate.AddDays(1));
			command+=" ORDER BY TimeEntry";
			DataTable table=Db.GetTable(command);
			TimeAdjust[] List=new TimeAdjust[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new TimeAdjust();
				List[i].TimeAdjustNum = PIn.Long(table.Rows[i][0].ToString());
				List[i].EmployeeNum   = PIn.Long(table.Rows[i][1].ToString());
				List[i].TimeEntry     = PIn.DateT(table.Rows[i][2].ToString());
				List[i].RegHours      =TimeSpan.FromHours(PIn.Double(table.Rows[i][3].ToString()));
				List[i].OTimeHours    =TimeSpan.FromHours(PIn.Double(table.Rows[i][4].ToString()));
				List[i].Note          = PIn.String(table.Rows[i][5].ToString());
			}
			return List;
		}
	
		///<summary></summary>
		public static long Insert(TimeAdjust adj) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				adj.TimeAdjustNum=Meth.GetLong(MethodBase.GetCurrentMethod(),adj);
				return adj.TimeAdjustNum;
			}
			if(PrefC.RandomKeys) {
				adj.TimeAdjustNum=ReplicationServers.GetKey("timeadjust","TimeAdjustNum");
			}
			string command="INSERT INTO timeadjust (";
			if(PrefC.RandomKeys) {
				command+="TimeAdjustNum,";
			}
			command+="EmployeeNum,TimeEntry,RegHours,OTimeHours,Note) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.Long(adj.TimeAdjustNum)+"', ";
			}
			command+=
				 "'"+POut.Long   (adj.EmployeeNum)+"', "
				+POut.DateT (adj.TimeEntry)+", "
				+"'"+POut.Double(adj.RegHours.TotalHours)+"', "
				+"'"+POut.Double(adj.OTimeHours.TotalHours)+"', "
				+"'"+POut.String(adj.Note)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				adj.TimeAdjustNum=Db.NonQ(command,true);
			}
			return adj.TimeAdjustNum;
		}

		///<summary></summary>
		public static void Update(TimeAdjust adj) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),adj);
				return;
			}
			string command= "UPDATE timeadjust SET "
				+"EmployeeNum = '"+POut.Long   (adj.EmployeeNum)+"' "
				+",TimeEntry = " +POut.DateT (adj.TimeEntry)+" "
				+",RegHours = '"  +POut.Double(adj.RegHours.TotalHours)+"' "
				+",OTimeHours = '"+POut.Double(adj.OTimeHours.TotalHours)+"' "
				+",Note = '"      +POut.String(adj.Note)+"' "
				+"WHERE TimeAdjustNum = '"+POut.Long(adj.TimeAdjustNum)+"'";
			Db.NonQ(command);
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




