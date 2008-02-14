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
			table.Columns.Add("amountDue");
			table.Columns.Add("balTotal");
			table.Columns.Add("billingType");
			table.Columns.Add("insEst");
			table.Columns.Add("IsSent");
			table.Columns.Add("lastStatement");
			table.Columns.Add("mode");
			table.Columns.Add("name");
			table.Columns.Add("PatNum");
			table.Columns.Add("StatementNum");
			List<DataRow> rows=new List<DataRow>();
			string command="SELECT BalTotal,BillingType,FName,InsEst,statement.IsSent,"
				+"IFNULL(MAX(s2.DateSent),'0001-01-01') LastStatement,"
				+"LName,MiddleI,statement.Mode_,Preferred,"
				+"statement.PatNum,statement.StatementNum "
				+"FROM statement "
				+"LEFT JOIN patient ON statement.PatNum=patient.PatNum "
				+"LEFT JOIN statement s2 ON s2.PatNum=patient.PatNum "
				+"AND s2.IsSent=1 ";
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
			DataTable rawTable=General.GetTable(command);
			Patient pat;
			StatementMode mode;
			double balTotal;
			double insEst;
			DateTime lastStatement;
			for(int i=0;i<rawTable.Rows.Count;i++){
				row=table.NewRow();
				balTotal=PIn.PDouble(rawTable.Rows[i]["BalTotal"].ToString());
				insEst=PIn.PDouble(rawTable.Rows[i]["InsEst"].ToString());
				row["amountDue"]=(balTotal-insEst).ToString("F");
				row["balTotal"]=balTotal.ToString("F");;
				row["billingType"]=DefB.GetName(DefCat.BillingTypes,PIn.PInt(rawTable.Rows[i]["BillingType"].ToString()));
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
				row["mode"]=Lan.g("enumStatementMode",mode.ToString());
				pat=new Patient();
				pat.LName=rawTable.Rows[i]["LName"].ToString();
				pat.FName=rawTable.Rows[i]["FName"].ToString();
				pat.Preferred=rawTable.Rows[i]["Preferred"].ToString();
				pat.MiddleI=rawTable.Rows[i]["MiddleI"].ToString();
				row["name"]=pat.GetNameLF();
				row["PatNum"]=rawTable.Rows[i]["PatNum"].ToString();
				row["StatementNum"]=rawTable.Rows[i]["StatementNum"].ToString();
				rows.Add(row);
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}


	}

	

	
	


}










