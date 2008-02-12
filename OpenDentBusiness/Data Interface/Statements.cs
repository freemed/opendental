using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	
	///<summary></summary>
	public class Statements{
		///<Summary>Gets one statement from the database.</Summary>
		public static Statement CreateObject(int statementNum){
			//string command="SELECT * FROM statement WHERE SupplyNum="+POut.PInt(supplyNum);
			return DataObjectFactory<Statement>.CreateObject(statementNum);
		}

		public static List<Statement> GetStatements(int[] statementNums){
			Collection<Statement> collectState=DataObjectFactory<Statement>.CreateObjects(statementNums);
			return new List<Statement>(collectState);		
		}

		///<summary></summary>
		public static void WriteObject(Statement statement){
			DataObjectFactory<Statement>.WriteObject(statement);
		}

		///<summary></summary>
		public static void DeleteObject(Statement statement){
			//validate that not already in use.
			//string command="SELECT COUNT(*) FROM supplyorderitem WHERE SupplyNum="+POut.PInt(supp.SupplyNum);
			//int count=PIn.PInt(General.GetCount(command));
			//if(count>0){
			//	throw new ApplicationException(Lan.g("Supplies","Supply is already in use on an order. Not allowed to delete."));
			//}
			DataObjectFactory<Statement>.DeleteObject(statement);
		}

		public static void DeleteObject(int statementNum){
			DataObjectFactory<Statement>.DeleteObject(statementNum);
		}

		public static bool UnsentStatementsExist(){
			string command="SELECT COUNT(*) FROM statement WHERE IsSent=0";
			if(General.GetCount(command)=="0"){
				return false;
			}
			return true;
		}

		///<summary>For orderBy, use 0 for BillingType and 1 for PatientName.</summary>
		public static DataTable GetBilling(bool isSent,int orderBy,DateTime dateFrom,DateTime dateTo){
			DataTable table=new DataTable();
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("amount");
			table.Columns.Add("billingType");
			table.Columns.Add("insEst");
			table.Columns.Add("IsSent");
			table.Columns.Add("lastStatement");
			table.Columns.Add("mode");
			table.Columns.Add("name");
			table.Columns.Add("PatNum");
			table.Columns.Add("StatementNum");
			table.Columns.Add("total");
			List<DataRow> rows=new List<DataRow>();
			string command="SELECT BillingType,FName,IsSent,LName,MiddleI,Mode_,Preferred,"
				+"statement.PatNum,StatementNum "
				+"FROM statement "
				+"LEFT JOIN patient ON statement.PatNum=patient.PatNum ";
			if(orderBy==0){//BillingType
				command+="LEFT JOIN definition ON patient.BillingType=definition.DefNum ";
			}
			command+="WHERE IsSent="+POut.PBool(isSent)+" ";
			//if(dateFrom.Year>1800){
				command+="AND DateSent>"+POut.PDate(dateFrom)+" ";
			//}
			//if(dateFrom.Year>1800){
				command+="AND DateSent<"+POut.PDate(dateTo)+" ";
			//}
			if(orderBy==0){//BillingType
				command+="ORDER BY definition.ItemOrder,LName,FName";
			}
			else{
				command+="ORDER BY LName,FName";
			}
			DataTable rawTable=General.GetTable(command);
			Patient pat;
			StatementMode mode;
			for(int i=0;i<rawTable.Rows.Count;i++){
				row=table.NewRow();
				row["amount"]="";
				row["billingType"]=DefB.GetName(DefCat.BillingTypes,PIn.PInt(rawTable.Rows[i]["BillingType"].ToString()));
				row["insEst"]="";
				row["IsSent"]=rawTable.Rows[i]["IsSent"].ToString();
				row["lastStatement"]="";
				mode=(StatementMode)PIn.PInt(rawTable.Rows[i]["Mode_"].ToString());
				row["mode"]=Lan.g("enumStatementMode",mode.ToString());
				pat=new Patient();
				pat.LName=rawTable.Rows[i]["LName"].ToString();
				pat.FName=rawTable.Rows[i]["FName"].ToString();
				pat.Preferred=rawTable.Rows[i]["Preferred"].ToString();
				pat.MiddleI=rawTable.Rows[i]["MiddleI"].ToString();
				row["name"]=pat.GetNameLF();
				row["PatNum"]=rawTable.Rows[i]["PatNum"].ToString();
				row["StatementNum"]=rawTable.Rows[i]["StatementNum"].ToString();
				row["total"]="";
				rows.Add(row);
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}


	}

	

	
	


}










