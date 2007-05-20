using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class UserGroups {
		///<summary>A list of all user groups, ordered by description.</summary>
		public static UserGroup[] List;

		///<summary></summary>
		public static void Refresh() {
			string command="SELECT * from usergroup ORDER BY Description";
			DataTable table=General.GetTable(command);
			List=new UserGroup[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new UserGroup();
				List[i].UserGroupNum  = PIn.PInt(table.Rows[i][0].ToString());
				List[i].Description   = PIn.PString(table.Rows[i][1].ToString());
			}
		}

		///<summary></summary>
		public static void Update(UserGroup group){
			string command= "UPDATE usergroup SET " 
				+"Description = '"  +POut.PString(group.Description)+"'"
				+" WHERE UserGroupNum = '"+POut.PInt(group.UserGroupNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(UserGroup group){
			string command= "INSERT INTO usergroup (Description) VALUES("
				+"'"+POut.PString(group.Description)+"')";
 			group.UserGroupNum=General.NonQ(command,true);
		}

		///<summary>Checks for dependencies first</summary>
		public static void Delete(UserGroup group){
			string command="SELECT COUNT(*) FROM userod WHERE UserGroupNum='"
				+POut.PInt(group.UserGroupNum)+"'";
			DataTable table=General.GetTable(command);
			if(table.Rows[0][0].ToString()!="0"){
				throw new Exception(Lan.g("UserGroups","Must move users to another group first."));
			}
			command= "DELETE FROM usergroup WHERE UserGroupNum='"
				+POut.PInt(group.UserGroupNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static UserGroup GetGroup(int userGroupNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].UserGroupNum==userGroupNum){
					return List[i].Copy();
				}
			}
			return null;
		}

		

	}
 
	

	
}













