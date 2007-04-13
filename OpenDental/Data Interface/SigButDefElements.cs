//using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class SigButDefElements {
		///<summary>A list of all elements for all buttons</summary>
		public static SigButDefElement[] List;

		///<summary>Gets all SigButDefElements for all buttons, ordered by type: user,extras, message.</summary>
		public static void Refresh() {
			string command="SELECT * FROM sigbutdefelement";
			DataTable table=General.GetTable(command);
			List=new SigButDefElement[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new SigButDefElement();
				List[i].ElementNum      = PIn.PInt(table.Rows[i][0].ToString());
				List[i].SigButDefNum    = PIn.PInt(table.Rows[i][1].ToString());
				List[i].SigElementDefNum= PIn.PInt(table.Rows[i][2].ToString());
			}
			//Array.Sort(List);
		}

		/*
		///<summary>This will never happen</summary>
		public void Update(){
			string command= "UPDATE SigButDefElement SET " 
				+"FromUser = '"    +POut.PString(FromUser)+"'"
				+",ITypes = '"     +POut.PInt   ((int)ITypes)+"'"
				+",DateViewing = '"+POut.PDate  (DateViewing)+"'"
				+",SigType = '"    +POut.PInt   ((int)SigType)+"'"
				+" WHERE SigButDefElementNum = '"+POut.PInt(SigButDefElementNum)+"'";
			DataConnection dcon=new DataConnection();
 			General.NonQ(command);
		}*/

		///<summary></summary>
		public static void Insert(SigButDefElement element){
			string command= "INSERT INTO sigbutdefelement (";
			command+="SigButDefNum,SigElementDefNum"
				+") VALUES(";
			command+=
				 "'"+POut.PInt   (element.SigButDefNum)+"', "
				+"'"+POut.PInt   (element.SigElementDefNum)+"')";
 			element.ElementNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Delete(SigButDefElement element){
			string command= "DELETE from sigbutdefelement WHERE ElementNum = '"+POut.PInt(element.ElementNum)+"'";
 			General.NonQ(command);
		}



		///<summary>Loops through the SigButDefElement list and pulls out all elements for one specific button.</summary>
		public static SigButDefElement[] GetForButton(int sigButDefNum){
			ArrayList AL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].SigButDefNum==sigButDefNum){
					AL.Add(List[i].Copy());
				}
			}
			SigButDefElement[] retVal=new SigButDefElement[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
			//already ordered because List is ordered.
		}

		

	
	}

	

	


}




















