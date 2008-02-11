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

		public static List<Statement> GetList(bool isSent){
			string command="SELECT * FROM statement WHERE IsSent="+POut.PBool(isSent);
			Collection<Statement> collectState=DataObjectFactory<Statement>.CreateObjects(command);
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

		


	}

	

	
	


}










