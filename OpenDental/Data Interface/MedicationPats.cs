using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{

	///<summary></summary>
	public class MedicationPats{
		///<summary></summary>
		public static MedicationPat[] List;//for current pat

		///<summary></summary>
		public static void Refresh(int patNum){
			string command =
				"SELECT * from medicationpat WHERE patnum = '"+patNum+"'";
			DataTable table=General.GetTable(command);
			List=new MedicationPat[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new MedicationPat();
				List[i].MedicationPatNum=PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum          =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].MedicationNum   =PIn.PInt   (table.Rows[i][2].ToString());
				List[i].PatNote         =PIn.PString(table.Rows[i][3].ToString());
				//HList.Add(List[i].MedicationNum,List[i]);
			}
		}

		///<summary></summary>
		public static void Update(MedicationPat Cur){
			string command = "UPDATE medicationpat SET " 
				+ "patnum = '"        +POut.PInt   (Cur.PatNum)+"'"
				+ ",medicationnum = '"+POut.PInt   (Cur.MedicationNum)+"'"
				+ ",patnote = '"      +POut.PString(Cur.PatNote)+"'"
				+" WHERE medicationpatnum = '" +POut.PInt   (Cur.MedicationPatNum)+"'";
			//MessageBox.Show(command);
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(MedicationPat Cur){
			if(PrefB.RandomKeys){
				Cur.MedicationPatNum=MiscData.GetKey("medicationpat","MedicationPatNum");
			}
			string command="INSERT INTO medicationpat (";
			if(PrefB.RandomKeys){
				command+="MedicationPatNum,";
			}
			command+="patnum,medicationnum,patnote"
				+") VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(Cur.MedicationPatNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.MedicationNum)+"', "
				+"'"+POut.PString(Cur.PatNote)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				Cur.MedicationPatNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Delete(MedicationPat Cur){
			string command = "DELETE from medicationpat WHERE medicationpatNum = '"
				+Cur.MedicationPatNum.ToString()+"'";
			General.NonQ(command);
		}
		
	}

	





}










