using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness {
	public class PhoneExclusions {
		private static List<PhoneExclusion> listt;

		///<summary>This list will get refreshed only once on startup and then not again.</summary>
		public static List<PhoneExclusion> Listt {
			get {
				if(listt==null) {
					Refresh();
				}
				return listt;
			}
			set { 
				listt=Listt; 
			}
		}

		public static void Refresh() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="SELECT * FROM phoneexclusion";
			DataTable table=Db.GetTable(command);
			listt=new List<PhoneExclusion>();
			PhoneExclusion phoneExclusion;
			for(int i=0;i<table.Rows.Count;i++) {
				phoneExclusion=new PhoneExclusion();
				phoneExclusion.PhoneExclusionNum= PIn.Long(table.Rows[i]["PhoneExclusionNum"].ToString());
				phoneExclusion.EmployeeNum      = PIn.Long(table.Rows[i]["EmployeeNum"].ToString());
				phoneExclusion.NoGraph          = PIn.Bool(table.Rows[i]["NoGraph"].ToString());
				phoneExclusion.NoColor          = PIn.Bool(table.Rows[i]["NoColor"].ToString());
				listt.Add(phoneExclusion);
			}
		}

		public static bool IsNoGraph(long employeeNum) {
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].EmployeeNum==employeeNum) {
					return Listt[i].NoGraph;
				}
			}
			return false;
		}

		public static bool IsNoColor(long employeeNum) {
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].EmployeeNum==employeeNum) {
					return Listt[i].NoColor;
				}
			}
			return false;
		}


	}



}



