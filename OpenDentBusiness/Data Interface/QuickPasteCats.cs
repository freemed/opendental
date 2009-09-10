using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class QuickPasteCats {
		///<summary></summary>
		private static QuickPasteCat[] list;

		public static QuickPasteCat[] List {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command=
				"SELECT * from quickpastecat "
				+"ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="QuickPasteCat";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List=new QuickPasteCat[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new QuickPasteCat();
				List[i].QuickPasteCatNum=PIn.PInt(table.Rows[i][0].ToString());
				List[i].Description=PIn.PString(table.Rows[i][1].ToString());
				List[i].ItemOrder=PIn.PInt32(table.Rows[i][2].ToString());
				List[i].DefaultForTypes=PIn.PString(table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static long Insert(QuickPasteCat cat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				cat.QuickPasteCatNum=Meth.GetInt(MethodBase.GetCurrentMethod(),cat);
				return cat.QuickPasteCatNum;
			}
			if(PrefC.RandomKeys){
				cat.QuickPasteCatNum=ReplicationServers.GetKey("quickpastecat","QuickPasteCatNum");
			}
			string command= "INSERT INTO quickpastecat (";
			if(PrefC.RandomKeys){
				command+="QuickPasteCatNum,";
			}
			command+="Description,ItemOrder,DefaultForTypes) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(cat.QuickPasteCatNum)+"', ";
			}
			command+=
				 "'"+POut.PString(cat.Description)+"', "
				+"'"+POut.PInt   (cat.ItemOrder)+"', "
				+"'"+POut.PString(cat.DefaultForTypes)+"')";
			//MessageBox.Show(string command);
 			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				cat.QuickPasteCatNum=Db.NonQ(command,true);
			}
			return cat.QuickPasteCatNum;
		}

		///<summary></summary>
		public static void Update(QuickPasteCat cat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cat);
				return;
			}
			string command="UPDATE quickpastecat SET "
				+"Description='"       +POut.PString(cat.Description)+"'"
				+",ItemOrder = '"      +POut.PInt   (cat.ItemOrder)+"'"
				+",DefaultForTypes = '"+POut.PString(cat.DefaultForTypes)+"'"
				+" WHERE QuickPasteCatNum = '"+POut.PInt (cat.QuickPasteCatNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(QuickPasteCat cat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cat);
				return;
			}
			string command="DELETE from quickpastecat WHERE QuickPasteCatNum = '"
				+POut.PInt(cat.QuickPasteCatNum)+"'";
 			Db.NonQ(command);
		}


		///<summary>Called from FormQuickPaste and from QuickPasteNotes.Substitute(). Returns the index of the default category for the specified type. If user has entered more than one, only one is returned.</summary>
		public static int GetDefaultType(QuickPasteType type){
			//No need to check RemotingRole; no call to db.
			if(List.Length==0){
				return -1;
			}
			if(type==QuickPasteType.None){
				return 0;//default to first line
			}
			string[] types;
			for(int i=0;i<List.Length;i++){
				if(List[i].DefaultForTypes==""){
					types=new string[0];
				}
				else{
					types=List[i].DefaultForTypes.Split(',');
				}
				for(int j=0;j<types.Length;j++){
					if(((int)type).ToString()==types[j]){
						return i;
					}
				}
			}
			return 0;
		}

		

		


		


	}

	


}









