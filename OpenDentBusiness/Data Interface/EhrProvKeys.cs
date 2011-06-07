using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrProvKeys{
		///<summary></summary>
		public static List<EhrProvKey> RefreshForFam(long guarantor){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrProvKey>>(MethodBase.GetCurrentMethod(),guarantor);
			}
//todo: fix
			string command="SELECT * FROM ehrprovkey WHERE PatNum = "+POut.Long(guarantor);
			return Crud.EhrProvKeyCrud.SelectMany(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		

		///<summary>Gets one EhrProvKey from the db.</summary>
		public static EhrProvKey GetOne(long ehrProvKeyNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrProvKey>(MethodBase.GetCurrentMethod(),ehrProvKeyNum);
			}
			return Crud.EhrProvKeyCrud.SelectOne(ehrProvKeyNum);
		}

		///<summary></summary>
		public static long Insert(EhrProvKey ehrProvKey){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrProvKey.EhrProvKeyNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrProvKey);
				return ehrProvKey.EhrProvKeyNum;
			}
			return Crud.EhrProvKeyCrud.Insert(ehrProvKey);
		}

		///<summary></summary>
		public static void Update(EhrProvKey ehrProvKey){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrProvKey);
				return;
			}
			Crud.EhrProvKeyCrud.Update(ehrProvKey);
		}

		///<summary></summary>
		public static void Delete(long ehrProvKeyNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrProvKeyNum);
				return;
			}
			string command= "DELETE FROM ehrprovkey WHERE EhrProvKeyNum = "+POut.Long(ehrProvKeyNum);
			Db.NonQ(command);
		}
		*/



	}
}