using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Operatories {

		///<summary>Refresh all operatories</summary>
		public static DataTable RefreshCache() {
			string command="SELECT * FROM operatory "
				+"ORDER BY ItemOrder";
			DataTable table=General.GetTable(command);
			table.TableName="Operatory";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			OperatoryC.Listt=new List<Operatory>();
			OperatoryC.ListShort=new List<Operatory>();
			Operatory op;
			for(int i=0;i<table.Rows.Count;i++) {
				op=new Operatory();
				op.OperatoryNum = PIn.PInt(table.Rows[i][0].ToString());
				op.OpName       = PIn.PString(table.Rows[i][1].ToString());
				op.Abbrev       = PIn.PString(table.Rows[i][2].ToString());
				op.ItemOrder    = PIn.PInt(table.Rows[i][3].ToString());
				op.IsHidden     = PIn.PBool(table.Rows[i][4].ToString());
				op.ProvDentist  = PIn.PInt(table.Rows[i][5].ToString());
				op.ProvHygienist= PIn.PInt(table.Rows[i][6].ToString());
				op.IsHygiene    = PIn.PBool(table.Rows[i][7].ToString());
				op.ClinicNum    = PIn.PInt(table.Rows[i][8].ToString());
				OperatoryC.Listt.Add(op);
				if(!op.IsHidden) {
					OperatoryC.ListShort.Add(op);
				}
			}
		}

		///<summary></summary>
		private static void Insert(Operatory op){
			string command= "INSERT INTO operatory (OpName,Abbrev,ItemOrder,IsHidden,ProvDentist,ProvHygienist,"
				+"IsHygiene,ClinicNum) VALUES("
				+"'"+POut.PString(op.OpName)+"', "
				+"'"+POut.PString(op.Abbrev)+"', "
				+"'"+POut.PInt   (op.ItemOrder)+"', "
				+"'"+POut.PBool  (op.IsHidden)+"', "
				+"'"+POut.PInt   (op.ProvDentist)+"', "
				+"'"+POut.PInt   (op.ProvHygienist)+"', "
				+"'"+POut.PBool  (op.IsHygiene)+"', "
				+"'"+POut.PInt   (op.ClinicNum)+"')";
 			op.OperatoryNum=General.NonQ(command,true);
		}

		///<summary></summary>
		private static void Update(Operatory op){
			string command= "UPDATE operatory SET " 
				+ "OpName = '"        +POut.PString(op.OpName)+"'"
				+ ",Abbrev = '"       +POut.PString(op.Abbrev)+"'"
				+ ",ItemOrder = '"    +POut.PInt   (op.ItemOrder)+"'"
				+ ",IsHidden = '"     +POut.PBool  (op.IsHidden)+"'"
				+ ",ProvDentist = '"  +POut.PInt   (op.ProvDentist)+"'"
				+ ",ProvHygienist = '"+POut.PInt   (op.ProvHygienist)+"'"
				+ ",IsHygiene = '"    +POut.PBool  (op.IsHygiene)+"'"
				+ ",ClinicNum = '"    +POut.PInt   (op.ClinicNum)+"'"				
				+" WHERE OperatoryNum = '" +POut.PInt(op.OperatoryNum)+"'";
			//MessageBox.Show(string command);
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void InsertOrUpdate(Operatory op, bool IsNew){
			//if(){
				//throw new ApplicationException(Lan.g(this,""));
			//}
			if(IsNew){
				Insert(op);
			}
			else{
				Update(op);
			}
		}

		//<summary>Checks dependencies first.  Throws exception if can't delete.</summary>
		//public void Delete(){//no such thing as delete.  Hide instead
		//}

		
	
	}
	


}













