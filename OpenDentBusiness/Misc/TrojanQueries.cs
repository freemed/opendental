using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class TrojanQueries {

		public static DataTable GetMaxProcedureDate(int PatNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),PatNum);
			}
			string command=@"SELECT MAX(ProcDate) FROM procedurelog,patient
				WHERE patient.PatNum=procedurelog.PatNum
				AND patient.Guarantor="+POut.PInt(PatNum);
			return Db.GetTable(command);
		}

		public static DataTable GetMaxPaymentDate(int PatNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),PatNum);
			}
			string command=@"SELECT MAX(DatePay) FROM paysplit,patient
				WHERE patient.PatNum=paysplit.PatNum
				AND patient.Guarantor="+POut.PInt(PatNum);
			return Db.GetTable(command);
		}

		public static int GetUniqueFileNum(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod());
			}
			string command="SELECT ValueString FROM preference WHERE PrefName='TrojanExpressCollectPreviousFileNumber'";
			DataTable table=Db.GetTable(command);
			int previousNum=PIn.PInt(table.Rows[0][0].ToString());
			int thisNum=previousNum+1;
			command="UPDATE preference SET ValueString='"+POut.PInt(thisNum)+
				"' WHERE PrefName='TrojanExpressCollectPreviousFileNumber'"
				+" AND ValueString='"+POut.PInt(previousNum)+"'";
			int result=Db.NonQ(command);
			while(result!=1) {//someone else sent one at the same time
				previousNum++;
				thisNum++;
				command="UPDATE preference SET ValueString='"+POut.PInt(thisNum)+
					"' WHERE PrefName='TrojanExpressCollectPreviousFileNumber'"
					+" AND ValueString='"+POut.PInt(previousNum)+"'";
				result=Db.NonQ(command);
			}
			return thisNum;
		}

		///<summary>Get the list of records for the pending plan deletion report for plans that need to be brought to the patient's attention.</summary>
		public static DataTable GetPendingDeletionTable(Collection<string[]> deletePatientRecords) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),deletePatientRecords);
			}
			string whereTrojanID="";
			for(int i=0;i<deletePatientRecords.Count;i++) {
				if(i>0) {
					whereTrojanID+="OR ";
				}
				whereTrojanID+="i.TrojanID='"+deletePatientRecords[i][0]+"' ";
			}
			string command="SELECT DISTINCT "+
					"p.FName,"+
					"p.LName,"+
					"p.FName,"+
					"p.LName,"+
					"p.SSN,"+
					"p.Birthdate,"+
					"i.GroupNum,"+
					"i.SubscriberID,"+
					"i.TrojanID,"+
					"CASE i.EmployerNum WHEN 0 THEN '' ELSE e.EmpName END,"+
					"CASE i.EmployerNum WHEN 0 THEN '' ELSE e.Phone END,"+
					"c.CarrierName,"+
					"c.Phone "+
					"FROM patient p,insplan i,employer e,carrier c "+
					"WHERE p.PatNum=i.Subscriber AND "+
					"("+whereTrojanID+") AND "+
					"i.CarrierNum=c.CarrierNum AND "+
					"(i.EmployerNum=e.EmployerNum OR i.EmployerNum=0) AND "+
					"(SELECT COUNT(*) FROM patplan a WHERE a.PlanNum=i.PlanNum) > 0 "+
					"ORDER BY i.TrojanID,p.LName,p.FName";
			return Db.GetTable(command);
		}

		///<summary>Get the list of records for the pending plan deletion report for plans which need to be bought to Trojan's attention.</summary>
		public static DataTable GetPendingDeletionTableTrojan(Collection<string[]> deleteTrojanRecords) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),deleteTrojanRecords);
			}
			string whereTrojanID="";
			for(int i=0;i<deleteTrojanRecords.Count;i++) {
				if(i>0) {
					whereTrojanID+="OR ";
				}
				whereTrojanID+="i.TrojanID='"+deleteTrojanRecords[i][0]+"' ";
			}
			string command="SELECT DISTINCT "+
					"p.FName,"+
					"p.LName,"+
					"p.FName,"+
					"p.LName,"+
					"p.SSN,"+
					"p.Birthdate,"+
					"i.GroupNum,"+
					"i.SubscriberID,"+
					"i.TrojanID,"+
					"CASE i.EmployerNum WHEN 0 THEN '' ELSE e.EmpName END,"+
					"CASE i.EmployerNum WHEN 0 THEN '' ELSE e.Phone END,"+
					"c.CarrierName,"+
					"c.Phone "+
					"FROM patient p,insplan i,employer e,carrier c "+
					"WHERE p.PatNum=i.Subscriber AND "+
					"("+whereTrojanID+") AND "+
					"i.CarrierNum=c.CarrierNum AND "+
					"(i.EmployerNum=e.EmployerNum OR i.EmployerNum=0) AND "+
					"(SELECT COUNT(*) FROM patplan a WHERE a.PlanNum=i.PlanNum) > 0 "+
					"ORDER BY i.TrojanID,p.LName,p.FName";
			return Db.GetTable(command);
		}

		///<summary></summary>
		public static int GetPlanNums(InsPlan plan,ArrayList benefitList) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),plan);
			}
			string command="SELECT PlanNum FROM insplan WHERE TrojanID='"+POut.PString(plan.TrojanID)+"'"; 
			DataTable table=Db.GetTable(command);
			int planNum;
			for(int i=0;i<table.Rows.Count;i++) {
				planNum=PIn.PInt(table.Rows[i][0].ToString());
				//update plan
				command="UPDATE insplan SET "
					+"EmployerNum='"+POut.PInt(plan.EmployerNum)+"', "
					+"GroupName='"  +POut.PString(plan.GroupName)+"', "
					+"GroupNum='"   +POut.PString(plan.GroupNum)+"', "
					+"CarrierNum='" +POut.PInt(plan.CarrierNum)+"', "
					+"BenefitNotes='"+POut.PString(plan.BenefitNotes)+"' "
					+"WHERE PlanNum="+POut.PInt(planNum);
				Db.NonQ(command);
				//clear benefits
				command="DELETE FROM benefit WHERE PlanNum="+POut.PInt(planNum);
				Db.NonQ(command);
				//benefitList
				for(int j=0;j<benefitList.Count;j++) {
					((Benefit)benefitList[j]).PlanNum=planNum;
					Benefits.Insert((Benefit)benefitList[j]);
				}
				InsPlans.ComputeEstimatesForPlan(planNum);
			}
			return table.Rows.Count;
		}

	}
}
