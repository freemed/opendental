using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Contacts{
		///<summary></summary>
		public static Contact[] List;//for one category only. Not refreshed with local data

		///<summary></summary>
		public static void Refresh(int category){
			string command="SELECT * from contact WHERE category = '"+category+"'"
				+" ORDER BY LName";
			DataTable table=General.GetTable(command);
			List = new Contact[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new Contact();
				List[i].ContactNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].LName      = PIn.PString(table.Rows[i][1].ToString());
				List[i].FName      = PIn.PString(table.Rows[i][2].ToString());
				List[i].WkPhone    = PIn.PString(table.Rows[i][3].ToString());
				List[i].Fax        = PIn.PString(table.Rows[i][4].ToString());
				List[i].Category   = PIn.PInt   (table.Rows[i][5].ToString());
				List[i].Notes      = PIn.PString(table.Rows[i][6].ToString());
			}
		}

		///<summary></summary>
		public static void Insert(Contact Cur){
			if(PrefB.RandomKeys){
				Cur.ContactNum=MiscData.GetKey("contact","ContactNum");
			}
			string command="INSERT INTO contact (";
			if(PrefB.RandomKeys){
				command+="ContactNum,";
			}
			command+="LName,FName,WkPhone,Fax,Category,"
				+"Notes) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(Cur.ContactNum)+"', ";
			}
			command+=
				 "'"+POut.PString(Cur.LName)+"', "
				+"'"+POut.PString(Cur.FName)+"', "
				+"'"+POut.PString(Cur.WkPhone)+"', "
				+"'"+POut.PString(Cur.Fax)+"', "
				+"'"+POut.PInt   (Cur.Category)+"', "
				+"'"+POut.PString(Cur.Notes)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				Cur.ContactNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(Contact Cur){
			string command = "UPDATE contact SET "
				+"lname = '"    +POut.PString(Cur.LName)+"' "
				+",fname = '"   +POut.PString(Cur.FName)+"' "
				+",wkphone = '" +POut.PString(Cur.WkPhone)+"' "
				+",fax = '"     +POut.PString(Cur.Fax)+"' "
				+",category = '"+POut.PInt   (Cur.Category)+"' "
				+",notes = '"   +POut.PString(Cur.Notes)+"' "
				+"WHERE contactnum = '"+POut.PInt  (Cur.ContactNum)+"'";
			//MessageBox.Show(string command);
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(Contact Cur){
			string command = "DELETE FROM contact WHERE contactnum = '"+Cur.ContactNum.ToString()+"'";
			General.NonQ(command);
		}

	}

	
}