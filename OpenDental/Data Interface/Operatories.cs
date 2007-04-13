using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Operatories {
		///<summary></summary>
		public static Operatory[] List;
		///<summary>A list of only those operatories that are visible.</summary>
		public static Operatory[] ListShort;

		///<summary>Refresh all operatories</summary>
		public static void Refresh() {
			string command="SELECT * FROM operatory "
				+"ORDER BY ItemOrder";
			DataTable table=General.GetTable(command);
			List=new Operatory[table.Rows.Count];
			ArrayList AL=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Operatory();
				List[i].OperatoryNum = PIn.PInt(table.Rows[i][0].ToString());
				List[i].OpName       = PIn.PString(table.Rows[i][1].ToString());
				List[i].Abbrev       = PIn.PString(table.Rows[i][2].ToString());
				List[i].ItemOrder    = PIn.PInt(table.Rows[i][3].ToString());
				List[i].IsHidden     = PIn.PBool(table.Rows[i][4].ToString());
				List[i].ProvDentist  = PIn.PInt(table.Rows[i][5].ToString());
				List[i].ProvHygienist= PIn.PInt(table.Rows[i][6].ToString());
				List[i].IsHygiene    = PIn.PBool(table.Rows[i][7].ToString());
				List[i].ClinicNum    = PIn.PInt(table.Rows[i][8].ToString());
				if(!List[i].IsHidden) {
					AL.Add(List[i]);
				}
			}
			ListShort=new Operatory[AL.Count];
			AL.CopyTo(ListShort);
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

		///<summary>Gets the order of the op within ListShort or -1 if not found.</summary>
		public static int GetOrder(int opNum){
			for(int i=0;i<ListShort.Length;i++){
				if(ListShort[i].OperatoryNum==opNum){
					return i;
				}
			}
			return -1;
		}

		///<summary>Gets the abbreviation of an op.</summary>
		public static string GetAbbrev(int opNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].OperatoryNum==opNum){
					return List[i].Abbrev;
				}
			}
			return "";
		}

		///<summary></summary>
		public static Operatory GetOperatory(int operatoryNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].OperatoryNum==operatoryNum){
					return List[i].Copy();
				}
			}
			return null;
		}
	
	}
	


}













