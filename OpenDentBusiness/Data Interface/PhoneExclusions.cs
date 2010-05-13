using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness {
	public class PhoneExclusions {

		public static List<PhoneExclusion> Refresh() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PhoneExclusion>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM phoneexclusion";
			DataTable table=Db.GetTable(command);
			List<PhoneExclusion> retVal=new List<PhoneExclusion>();
			PhoneExclusion phoneExclusion;
			for(int i=0;i<table.Rows.Count;i++) {
				phoneExclusion=new PhoneExclusion();
				phoneExclusion.PhoneExclusionNum= PIn.Long(table.Rows[i]["PhoneExclusionNum"].ToString());
				phoneExclusion.EmployeeNum      = PIn.Long(table.Rows[i]["EmployeeNum"].ToString());
				phoneExclusion.NoGraph          = PIn.Bool(table.Rows[i]["NoGraph"].ToString());
				phoneExclusion.NoColor          = PIn.Bool(table.Rows[i]["NoColor"].ToString());
				retVal.Add(phoneExclusion);
			}
			return retVal;
		}

		public static bool IsNoGraph(List<PhoneExclusion> exclusionList,long employeeNum) {
			for(int i=0;i<exclusionList.Count;i++) {
				if(exclusionList[i].EmployeeNum==employeeNum) {
					return exclusionList[i].NoGraph;
				}
			}
			return false;
		}
	}



}



