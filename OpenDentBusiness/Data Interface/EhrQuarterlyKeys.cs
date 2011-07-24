using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrQuarterlyKeys{
		///<summary>Pass in a guarantor of 0 when not using from OD tech station.</summary>
		public static List<EhrQuarterlyKey> Refresh(long guarantor){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrQuarterlyKey>>(MethodBase.GetCurrentMethod(),guarantor);
			}
			string command;
			if(guarantor==0){//customer looking at their own quarterly keys
				command="SELECT * FROM ehrquarterlykey";
			}
			else{//
				command="SELECT ehrquarterlykey.* FROM ehrquarterlykey,patient "
					+"WHERE ehrquarterlykey.PatNum=patient.PatNum "
					+"AND patient.Guarantor="+POut.Long(guarantor)+" "
					+"GROUP BY ehrquarterlykey.EhrQuarterlyKeyNum "
					+"ORDER BY ehrquarterlykey.YearValue,ehrquarterlykey.QuarterValue";
			}
			return Crud.EhrQuarterlyKeyCrud.SelectMany(command);
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

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		

		///<summary>Gets one EhrQuarterlyKey from the db.</summary>
		public static EhrQuarterlyKey GetOne(long ehrQuarterlyKeyNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrQuarterlyKey>(MethodBase.GetCurrentMethod(),ehrQuarterlyKeyNum);
			}
			return Crud.EhrQuarterlyKeyCrud.SelectOne(ehrQuarterlyKeyNum);
		}

		
		*/



	}
}