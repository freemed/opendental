using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness {
	public class PhoneEmpDefaults {
		private static List<PhoneEmpDefault> listt;

		///<summary>This list will get refreshed only once on startup and then not again.</summary>
		public static List<PhoneEmpDefault> Listt {
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
			string command="SELECT * FROM phoneempdefault";
			DataTable table=Db.GetTable(command);
			listt=new List<PhoneEmpDefault>();
			PhoneEmpDefault phoneEmpDefault;
			for(int i=0;i<table.Rows.Count;i++) {
				phoneEmpDefault=new PhoneEmpDefault();
				phoneEmpDefault.EmployeeNum      = PIn.Long(table.Rows[i]["EmployeeNum"].ToString());
				phoneEmpDefault.NoGraph          = PIn.Bool(table.Rows[i]["NoGraph"].ToString());
				phoneEmpDefault.NoColor          = PIn.Bool(table.Rows[i]["NoColor"].ToString());
				listt.Add(phoneEmpDefault);
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



