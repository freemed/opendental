using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Computers {
		///<summary>A list of all computers that have logged into the database in the past.  Might be some extra computer names in the list unless user has cleaned it up.</summary>
		public static Computer[] List;

		///<summary></summary>
		public static void Refresh() {
			//first, make sure this computer is in the db:
			string command=
				"SELECT * from computer "
				+"WHERE compname = '"+SystemInformation.ComputerName+"'";
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0) {
				Computer Cur=new Computer();
				Cur.CompName=SystemInformation.ComputerName;
				Computers.Insert(Cur);
			}
			//then, refresh List:
			List=GetList();
		}

		///<summary></summary>
		public static Computer[] GetList() {
			string command="SELECT * FROM computer ORDER BY CompName";
			DataTable table=General.GetTable(command);
			Computer[] list=new Computer[table.Rows.Count];
			for(int i=0;i<list.Length;i++) {
				list[i]=new Computer();
				list[i].ComputerNum = PIn.PInt(table.Rows[i][0].ToString());
				list[i].CompName    = PIn.PString(table.Rows[i][1].ToString());
			}
			return list;
		}

			///<summary>ONLY use this if compname is not already present</summary>
		public static void Insert(Computer comp){
			if(PrefB.RandomKeys){
				comp.ComputerNum=MiscData.GetKey("computer","ComputerNum");
			}
			string command= "INSERT INTO computer (";
			if(PrefB.RandomKeys){
				command+="ComputerNum,";
			}
			command+="CompName"
				+") VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(comp.ComputerNum)+"', ";
			}
			command+=
				"'"+POut.PString(comp.CompName)+"')";
				//+"'"+POut.PString(PrinterName)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				comp.ComputerNum=General.NonQ(command,true);
			}
		}

		/*
		///<summary></summary>
		public static void Update(){
			string command= "UPDATE computer SET "
				+"compname = '"    +POut.PString(CompName)+"' "
				//+"printername = '" +POut.PString(PrinterName)+"' "
				+"WHERE ComputerNum = '"+POut.PInt(ComputerNum)+"'";
			//MessageBox.Show(string command);
			DataConnection dcon=new DataConnection();
 			General.NonQ(command);
		}*/

		///<summary></summary>
		public static void Delete(Computer comp){
			string command= "DELETE FROM computer WHERE computernum = '"+comp.ComputerNum.ToString()+"'";
 			General.NonQ(command);
		}

		///<summary>Only called from Printers.GetForSit</summary>
		public static Computer GetCur(){
			for(int i=0;i<List.Length;i++){
				if(SystemInformation.ComputerName==List[i].CompName){
					return List[i];
				}
			}
			return null;//this will never happen
		}
   

	

	}

	

	



}









