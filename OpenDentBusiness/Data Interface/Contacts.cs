using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Contacts{
		//<summary></summary>
		//public static Contact[] List;//for one category only. Not refreshed with local data

		///<summary></summary>
		public static Contact[] Refresh(long category) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Contact[]>(MethodBase.GetCurrentMethod(),category);
			}
			string command="SELECT * from contact WHERE category = '"+category+"'"
				+" ORDER BY LName";
			DataTable table=Db.GetTable(command);
			Contact[] List = new Contact[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new Contact();
				List[i].ContactNum = PIn.Long   (table.Rows[i][0].ToString());
				List[i].LName      = PIn.String(table.Rows[i][1].ToString());
				List[i].FName      = PIn.String(table.Rows[i][2].ToString());
				List[i].WkPhone    = PIn.String(table.Rows[i][3].ToString());
				List[i].Fax        = PIn.String(table.Rows[i][4].ToString());
				List[i].Category   = PIn.Long   (table.Rows[i][5].ToString());
				List[i].Notes      = PIn.String(table.Rows[i][6].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static long Insert(Contact Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.ContactNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.ContactNum;
			}
			if(PrefC.RandomKeys){
				Cur.ContactNum=ReplicationServers.GetKey("contact","ContactNum");
			}
			string command="INSERT INTO contact (";
			if(PrefC.RandomKeys){
				command+="ContactNum,";
			}
			command+="LName,FName,WkPhone,Fax,Category,"
				+"Notes) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.Long(Cur.ContactNum)+"', ";
			}
			command+=
				 "'"+POut.String(Cur.LName)+"', "
				+"'"+POut.String(Cur.FName)+"', "
				+"'"+POut.String(Cur.WkPhone)+"', "
				+"'"+POut.String(Cur.Fax)+"', "
				+"'"+POut.Long   (Cur.Category)+"', "
				+"'"+POut.String(Cur.Notes)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				Cur.ContactNum=Db.NonQ(command,true);
			}
			return Cur.ContactNum;
		}

		///<summary></summary>
		public static void Update(Contact Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "UPDATE contact SET "
				+"lname = '"    +POut.String(Cur.LName)+"' "
				+",fname = '"   +POut.String(Cur.FName)+"' "
				+",wkphone = '" +POut.String(Cur.WkPhone)+"' "
				+",fax = '"     +POut.String(Cur.Fax)+"' "
				+",category = '"+POut.Long   (Cur.Category)+"' "
				+",notes = '"   +POut.String(Cur.Notes)+"' "
				+"WHERE contactnum = '"+POut.Long  (Cur.ContactNum)+"'";
			//MessageBox.Show(string command);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(Contact Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "DELETE FROM contact WHERE contactnum = '"+Cur.ContactNum.ToString()+"'";
			Db.NonQ(command);
		}

	}

	
}