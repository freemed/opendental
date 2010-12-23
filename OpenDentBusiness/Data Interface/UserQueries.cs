using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{

///<summary></summary>
	public class UserQueries{

		///<summary></summary>
		public static List<UserQuery> Refresh(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<UserQuery>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM userquery ORDER BY description";
			return Crud.UserQueryCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(UserQuery Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.QueryNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.QueryNum;
			}
			return Crud.UserQueryCrud.Insert(Cur);
		}
		
		///<summary></summary>
		public static void Delete(UserQuery Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "DELETE from userquery WHERE querynum = '"+POut.Long(Cur.QueryNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Update(UserQuery Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			Crud.UserQueryCrud.Update(Cur);
		}
	}

	

	
}













