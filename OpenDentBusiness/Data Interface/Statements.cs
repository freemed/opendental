using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	
	///<summary></summary>
	public class Statements{
		///<Summary>Gets one statement from the database.</Summary>
		public static Statement CreateObject(long statementNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Statement>(MethodBase.GetCurrentMethod(),statementNum);
			}
			//string command="SELECT * FROM statement WHERE SupplyNum="+POut.PInt(supplyNum);
			return DataObjectFactory<Statement>.CreateObject(statementNum);
		}

		public static List<Statement> GetStatements(List<long> statementNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Statement>>(MethodBase.GetCurrentMethod(),statementNums);
			} 
			Collection<Statement> collectState=DataObjectFactory<Statement>.CreateObjects(statementNums);
			return new List<Statement>(collectState);		
		}

		///<summary></summary>
		public static long WriteObject(Statement statement) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				statement.StatementNum=Meth.GetInt(MethodBase.GetCurrentMethod(),statement);
				return statement.StatementNum;
			}
			DataObjectFactory<Statement>.WriteObject(statement);
			return statement.StatementNum;
		}

		///<summary></summary>
		public static void DeleteObject(Statement statement){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),statement);
				return;
			}
			//validate that not already in use.
			//string command="SELECT COUNT(*) FROM supplyorderitem WHERE SupplyNum="+POut.PInt(supp.SupplyNum);
			//int count=PIn.PInt(Db.GetCount(command));
			//if(count>0){
			//	throw new ApplicationException(Lans.g("Supplies","Supply is already in use on an order. Not allowed to delete."));
			//}
			DataObjectFactory<Statement>.DeleteObject(statement);
		}

		public static void DeleteObject(long statementNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),statementNum);
				return;
			}
			DataObjectFactory<Statement>.DeleteObject(statementNum);
		}

		///<summary>Queries the database to determine if there are any unsent statements.</summary>
		public static bool UnsentStatementsExist(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod());
			}
			string command="SELECT COUNT(*) FROM statement WHERE IsSent=0";
			if(Db.GetCount(command)=="0"){
				return false;
			}
			return true;
		}

		public static void MarkSent(long statementNum,DateTime dateSent) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),statementNum,dateSent);
				return;
			}
			string command="UPDATE statement SET DateSent="+POut.PDate(dateSent)+", "
				+"IsSent=1 WHERE StatementNum="+POut.PInt(statementNum);
			Db.NonQ(command);
		}

		public static void AttachDoc(long statementNum,long docNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),statementNum,docNum);
				return;
			}
			string command="UPDATE statement SET DocNum="+POut.PInt(docNum)
				+" WHERE StatementNum="+POut.PInt(statementNum);
			Db.NonQ(command);
		}

		///<summary>For orderBy, use 0 for BillingType and 1 for PatientName.</summary>
		public static DataTable GetBilling(bool isSent,int orderBy,DateTime dateFrom,DateTime dateTo){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),isSent,orderBy,dateFrom,dateTo);
			}
			DataTable table=new DataTable();
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("amountDue");
			table.Columns.Add("balTotal");
			table.Columns.Add("billingType");
			table.Columns.Add("insEst");
			table.Columns.Add("IsSent");
			table.Columns.Add("lastStatement");
			table.Columns.Add("mode");
			table.Columns.Add("name");
			table.Columns.Add("PatNum");
			table.Columns.Add("payPlanDue");
			table.Columns.Add("StatementNum");
			List<DataRow> rows=new List<DataRow>();
			string command="SELECT BalTotal,BillingType,FName,InsEst,statement.IsSent,"
				+"IFNULL(MAX(s2.DateSent),'0001-01-01') LastStatement,"
				+"LName,MiddleI,statement.Mode_,PayPlanDue,Preferred,"
				+"statement.PatNum,statement.StatementNum "
				+"FROM statement "
				+"LEFT JOIN patient ON statement.PatNum=patient.PatNum "
				+"LEFT JOIN statement s2 ON s2.PatNum=patient.PatNum "
				+"AND s2.IsSent=1 ";
			if(PrefC.GetBool("BillingIgnoreInPerson")) {
				command+="AND s2.Mode_ !=1 ";
			}
			if(orderBy==0){//BillingType
				command+="LEFT JOIN definition ON patient.BillingType=definition.DefNum ";
			}
			command+="WHERE statement.IsSent="+POut.PBool(isSent)+" ";
			//if(dateFrom.Year>1800){
				command+="AND statement.DateSent>="+POut.PDate(dateFrom)+" ";//greater than midnight this morning
			//}
			//if(dateFrom.Year>1800){
				command+="AND statement.DateSent<"+POut.PDate(dateTo.AddDays(1))+" ";//less than midnight tonight
			//}
			command+="GROUP BY statement.StatementNum ";
			if(orderBy==0){//BillingType
				command+="ORDER BY definition.ItemOrder,LName,FName";
			}
			else{
				command+="ORDER BY LName,FName";
			}
			DataTable rawTable=Db.GetTable(command);
			Patient pat;
			StatementMode mode;
			double balTotal;
			double insEst;
			double payPlanDue;
			DateTime lastStatement;
			for(int i=0;i<rawTable.Rows.Count;i++){
				row=table.NewRow();
				balTotal=PIn.PDouble(rawTable.Rows[i]["BalTotal"].ToString());
				insEst=PIn.PDouble(rawTable.Rows[i]["InsEst"].ToString());
				payPlanDue=PIn.PDouble(rawTable.Rows[i]["PayPlanDue"].ToString());
				row["amountDue"]=(balTotal-insEst).ToString("F");
				row["balTotal"]=balTotal.ToString("F");;
				row["billingType"]=DefC.GetName(DefCat.BillingTypes,PIn.PInt(rawTable.Rows[i]["BillingType"].ToString()));
				if(insEst==0){
					row["insEst"]="";
				}
				else{
					row["insEst"]=insEst.ToString("F");
				}
				row["IsSent"]=rawTable.Rows[i]["IsSent"].ToString();
				lastStatement=PIn.PDate(rawTable.Rows[i]["LastStatement"].ToString());
				if(lastStatement.Year<1880){
					row["lastStatement"]="";
				}
				else{
					row["lastStatement"]=lastStatement.ToShortDateString();
				}
				mode=(StatementMode)PIn.PInt(rawTable.Rows[i]["Mode_"].ToString());
				row["mode"]=Lans.g("enumStatementMode",mode.ToString());
				pat=new Patient();
				pat.LName=rawTable.Rows[i]["LName"].ToString();
				pat.FName=rawTable.Rows[i]["FName"].ToString();
				pat.Preferred=rawTable.Rows[i]["Preferred"].ToString();
				pat.MiddleI=rawTable.Rows[i]["MiddleI"].ToString();
				row["name"]=pat.GetNameLF();
				row["PatNum"]=rawTable.Rows[i]["PatNum"].ToString();
				if(payPlanDue==0){
					row["payPlanDue"]="";
				}
				else{
					row["payPlanDue"]=payPlanDue.ToString("F");
				}
				row["StatementNum"]=rawTable.Rows[i]["StatementNum"].ToString();
				rows.Add(row);
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

		public static DataTable GetStatementNotes(long PatientID) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),PatientID);
			}
			string command=@"SELECT Note FROM Statement Where Patnum="+PatientID;
			return Db.GetTable(command);
		}

		public static DataTable GetStatementInfo(long PatientID) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),PatientID);
			}
			string command=@"Select SinglePatient,DateRangeFrom,DateRangeTo,Intermingled
                        FROM statement WHERE PatNum = "+PatientID;
			return Db.GetTable(command);
		}


	}

	

	
	


}










