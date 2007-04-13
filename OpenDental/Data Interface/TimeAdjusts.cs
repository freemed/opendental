using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class TimeAdjusts {

		///<summary></summary>
		public static TimeAdjust[] Refresh(int empNum,DateTime fromDate,DateTime toDate) {
			string command=
				"SELECT * from timeadjust WHERE"
				+" EmployeeNum = '"+POut.PInt(empNum)+"'"
				+" AND TimeEntry >= "+POut.PDate(fromDate)
				//adding a day takes it to midnight of the specified toDate
				+" AND TimeEntry <= "+POut.PDate(toDate.AddDays(1));
			command+=" ORDER BY TimeEntry";
			DataTable table=General.GetTable(command);
			TimeAdjust[] List=new TimeAdjust[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new TimeAdjust();
				List[i].TimeAdjustNum = PIn.PInt(table.Rows[i][0].ToString());
				List[i].EmployeeNum   = PIn.PInt(table.Rows[i][1].ToString());
				List[i].TimeEntry     = PIn.PDateT(table.Rows[i][2].ToString());
				List[i].RegHours      =TimeSpan.FromHours(PIn.PDouble(table.Rows[i][3].ToString()));
				List[i].OTimeHours    =TimeSpan.FromHours(PIn.PDouble(table.Rows[i][4].ToString()));
				List[i].Note          = PIn.PString(table.Rows[i][5].ToString());
			}
			return List;
		}
	
		///<summary></summary>
		public static void Insert(TimeAdjust adj) {
			if(PrefB.RandomKeys) {
				adj.TimeAdjustNum=MiscData.GetKey("timeadjust","TimeAdjustNum");
			}
			string command="INSERT INTO timeadjust (";
			if(PrefB.RandomKeys) {
				command+="TimeAdjustNum,";
			}
			command+="EmployeeNum,TimeEntry,RegHours,OTimeHours,Note) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(adj.TimeAdjustNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (adj.EmployeeNum)+"', "
				+POut.PDateT (adj.TimeEntry)+", "
				+"'"+POut.PDouble(adj.RegHours.TotalHours)+"', "
				+"'"+POut.PDouble(adj.OTimeHours.TotalHours)+"', "
				+"'"+POut.PString(adj.Note)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				adj.TimeAdjustNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(TimeAdjust adj) {
			string command= "UPDATE timeadjust SET "
				+"EmployeeNum = '"+POut.PInt   (adj.EmployeeNum)+"' "
				+",TimeEntry = " +POut.PDateT (adj.TimeEntry)+" "
				+",RegHours = '"  +POut.PDouble(adj.RegHours.TotalHours)+"' "
				+",OTimeHours = '"+POut.PDouble(adj.OTimeHours.TotalHours)+"' "
				+",Note = '"      +POut.PString(adj.Note)+"' "
				+"WHERE TimeAdjustNum = '"+POut.PInt(adj.TimeAdjustNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(TimeAdjust adj) {
			string command= "DELETE FROM timeadjust WHERE TimeAdjustNum = "+POut.PInt(adj.TimeAdjustNum);
			General.NonQ(command);
		}

	
	


		




	}

	
}




