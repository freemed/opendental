using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class OIDInternals{

		///<summary>Returns the currently defined OID for a given IndentifierType.  If not defined, returns empty string.</summary>
		public static OIDInternal GetForType(IdentifierType IDType) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<OIDInternal>(MethodBase.GetCurrentMethod(),IDType);
			}
			string command="SELECT * FROM oidinternal WHERE IDType='"+IDType.ToString()+"'";//should only return one row.
			return Crud.OIDInternalCrud.SelectOne(command);
		}

		///<summary>There should always be one entry in the DB per IdentifierType enumeration.</summary>
		public static void InsertMissingValues() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			//string command= "SELECT COUNT(*) FROM oidinternal";
			//if(PIn.Long(Db.GetCount(command))==Enum.GetValues(typeof(IdentifierType)).Length) {
			//	return;//The DB table has the right count. Which means there is probably nothing wrong with the values in it. This may need to be enhanced if customers have any issues.
			//}
			string command="SELECT * FROM oidinternal";
			List<OIDInternal> listOIDInternals=Crud.OIDInternalCrud.SelectMany(command);
			List<IdentifierType> listIDTypes=new List<IdentifierType>();
			for(int i=0;i<listOIDInternals.Count;i++) {
				listIDTypes.Add(listOIDInternals[i].IDType);
			}
			for(int i=0;i<Enum.GetValues(typeof(IdentifierType)).Length;i++) {
				if(listIDTypes.Contains((IdentifierType)i)) {
					continue;//DB contains a row for this enum value.
				}
				//Insert missing row with blank OID.
				if(DataConnection.DBtype==DatabaseType.MySql) {
						command="INSERT INTO oidinternal (IDType,IDRoot) "
						+"VALUES('"+((IdentifierType)i).ToString()+"','')";
						Db.NonQ32(command);
				}
				else {//oracle
					command="INSERT INTO oidinternal (OIDInternalNum,IDType,IDRoot) "
						+"VALUES((SELECT MAX(OIDInternalNum)+1 FROM oidinternal),'"+((IdentifierType)i).ToString()+"','')";
					Db.NonQ32(command);
				}
			}
		}

		///<summary></summary>
		public static List<OIDInternal> GetAll() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<OIDInternal>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM oidinternal";
			return Crud.OIDInternalCrud.SelectMany(command);
		}

		///<summary></summary>
		public static void Update(OIDInternal oIDInternal) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),oIDInternal);
				return;
			}
			Crud.OIDInternalCrud.Update(oIDInternal);
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