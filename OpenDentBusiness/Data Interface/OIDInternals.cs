using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class OIDInternals{

		///<summary>Returns the currently defined OID for a given IndentifierType.  If not defined, returns empty string.</summary>
		public static string GetForType(IdentifierType IDType) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),IDType);
			}
			string command="SELECT * FROM oidinternal WHERE IDType='"+IDType.ToString()+"'";//should only return one row.
			OIDInternal tempOID=Crud.OIDInternalCrud.SelectOne(command);
			if(tempOID==null) {
				return "";//row not in DB for some reason.
			}
			return tempOID.IDRoot;
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<OIDInternal> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<OIDInternal>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM oidinternal WHERE PatNum = "+POut.Long(patNum);
			return Crud.OIDInternalCrud.SelectMany(command);
		}

		///<summary>Gets one OIDInternal from the db.</summary>
		public static OIDInternal GetOne(long ehrOIDNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<OIDInternal>(MethodBase.GetCurrentMethod(),ehrOIDNum);
			}
			return Crud.OIDInternalCrud.SelectOne(ehrOIDNum);
		}

		///<summary></summary>
		public static long Insert(OIDInternal oIDInternal){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				oIDInternal.EhrOIDNum=Meth.GetLong(MethodBase.GetCurrentMethod(),oIDInternal);
				return oIDInternal.EhrOIDNum;
			}
			return Crud.OIDInternalCrud.Insert(oIDInternal);
		}

		///<summary></summary>
		public static void Update(OIDInternal oIDInternal){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),oIDInternal);
				return;
			}
			Crud.OIDInternalCrud.Update(oIDInternal);
		}

		///<summary></summary>
		public static void Delete(long ehrOIDNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrOIDNum);
				return;
			}
			string command= "DELETE FROM oidinternal WHERE EhrOIDNum = "+POut.Long(ehrOIDNum);
			Db.NonQ(command);
		}
		*/



	}
}