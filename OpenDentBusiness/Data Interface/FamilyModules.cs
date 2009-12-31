using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class FamilyModules {
		public static DataSet GetAll(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetDS(MethodBase.GetCurrentMethod(),patNum);
			} 
			DataSet ds=new DataSet();
			string command=Patients.GetFamilySelectCommand(patNum);
			DataTable table=Db.GetTable(command);
			table.TableName="Patient";
			table.Columns.Add("Age");
			table.Columns.Add("PrimaryTeethOld");
			for(int i=0;i<table.Rows.Count;i++){
				table.Rows[i]["Age"]="0";
				table.Rows[i]["PrimaryTeethOld"]="";
				if(table.Rows[i]["Title"].ToString()==""){
					table.Rows[i]["Title"]="";//handles null
				}
				if(table.Rows[i]["SchedBeforeTime"].ToString()==""){
					table.Rows[i]["SchedBeforeTime"]=POut.TimeSpan(TimeSpan.Zero,false);
				}
				if(table.Rows[i]["SchedAfterTime"].ToString()==""){
					table.Rows[i]["SchedAfterTime"]=POut.TimeSpan(TimeSpan.Zero,false);
				}
			}
			Family fam=new Family();
			fam.ListPats=Patients.TableToList(table).ToArray();
			ds.Tables.Add(table);
			ds.Tables.Add(GetPatPlanList(patNum));
			ds.Tables.Add(GetPlanList(fam));
			return ds;
		}

		public static DataTable GetPatPlanList(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * from patplan"
				+" WHERE PatNum = "+patNum.ToString()
				+" ORDER BY Ordinal";
			DataTable table=Db.GetTable(command);
			table.TableName="PatPlan";
			return table;
		}

		///<summary>Gets new List for the specified family.  The only plans it misses are for claims with no current coverage.  These are handled as needed.</summary>
		public static DataTable GetPlanList(Family fam) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),fam);
			}
			string command=
				"(SELECT insplan.*,'0' FROM insplan "
				+"WHERE";
			//subscribers in family
			for(int i=0;i<fam.ListPats.Length;i++) {
				if(i>0) {
					command+=" OR";
				}
				command+=" Subscriber="+POut.Long(fam.ListPats[i].PatNum);
			}
			//in union, distinct is implied
			command+=") UNION (SELECT insplan.*,'0' FROM insplan,patplan WHERE insplan.PlanNum=patplan.PlanNum AND (";
			for(int i=0;i<fam.ListPats.Length;i++) {
				if(i>0) {
					command+=" OR";
				}
				command+=" patplan.PatNum="+POut.Long(fam.ListPats[i].PatNum);
			}
			//command+=")) ORDER BY DateEffective";//FIXME:UNION-ORDER-BY
			command+=")) ORDER BY 3";//***ORACLE ORDINAL
			//Debug.WriteLine(command);
			DataTable table=Db.GetTable(command);
			table.TableName="InsPlan";
			return table;
		}

	}
}
