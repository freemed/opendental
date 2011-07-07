using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrQuarterlyKeys{
	
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EhrQuarterlyKey> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrQuarterlyKey>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrquarterlykey WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrQuarterlyKeyCrud.SelectMany(command);
		}

		///<summary>Gets one EhrQuarterlyKey from the db.</summary>
		public static EhrQuarterlyKey GetOne(long ehrQuarterlyKeyNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrQuarterlyKey>(MethodBase.GetCurrentMethod(),ehrQuarterlyKeyNum);
			}
			return Crud.EhrQuarterlyKeyCrud.SelectOne(ehrQuarterlyKeyNum);
		}

		///<summary></summary>
		public static long Insert(EhrQuarterlyKey ehrQuarterlyKey){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrQuarterlyKey.EhrQuarterlyKeyNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrQuarterlyKey);
				return ehrQuarterlyKey.EhrQuarterlyKeyNum;
			}
			return Crud.EhrQuarterlyKeyCrud.Insert(ehrQuarterlyKey);
		}

		///<summary></summary>
		public static void Update(EhrQuarterlyKey ehrQuarterlyKey){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrQuarterlyKey);
				return;
			}
			Crud.EhrQuarterlyKeyCrud.Update(ehrQuarterlyKey);
		}

		///<summary></summary>
		public static void Delete(long ehrQuarterlyKeyNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrQuarterlyKeyNum);
				return;
			}
			string command= "DELETE FROM ehrquarterlykey WHERE EhrQuarterlyKeyNum = "+POut.Long(ehrQuarterlyKeyNum);
			Db.NonQ(command);
		}
		*/



	}
}