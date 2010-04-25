using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class InsFilingCodeSubtypes {
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM insfilingcodesubtype ORDER BY Descript";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="InsFilingCodeSubtype";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			InsFilingCodeSubtypeC.Listt=Crud.InsFilingCodeSubtypeCrud.TableToList(table);
		}

		///<Summary>Gets one InsFilingCodeSubtype from the database.</Summary>
		public static InsFilingCodeSubtype GetOne(long insFilingCodeSubtypeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<InsFilingCodeSubtype>(MethodBase.GetCurrentMethod(),insFilingCodeSubtypeNum);
			}
			return Crud.InsFilingCodeSubtypeCrud.SelectOne(insFilingCodeSubtypeNum);
		}

		///<summary></summary>
		public static long Insert(InsFilingCodeSubtype insFilingCodeSubtype) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				insFilingCodeSubtype.InsFilingCodeSubtypeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),insFilingCodeSubtype);
				return insFilingCodeSubtype.InsFilingCodeSubtypeNum;
			}
			return Crud.InsFilingCodeSubtypeCrud.Insert(insFilingCodeSubtype);
		}

		///<summary></summary>
		public static void Update(InsFilingCodeSubtype insFilingCodeSubtype) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),insFilingCodeSubtype);
				return;
			}
			Crud.InsFilingCodeSubtypeCrud.Update(insFilingCodeSubtype);
		}

		///<summary>Surround with try/catch</summary>
		public static void Delete(long insFilingCodeSubtypeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),insFilingCodeSubtypeNum);
				return;
			}
			string command="SELECT COUNT(*) FROM insplan WHERE FilingCodeSubtype="+POut.Long(insFilingCodeSubtypeNum);
			if(Db.GetScalar(command) != "0") {
				throw new ApplicationException(Lans.g("InsFilingCodeSubtype","Already in use by insplans."));
			}
			Crud.InsFilingCodeSubtypeCrud.Delete(insFilingCodeSubtypeNum);
		}

		public static List<InsFilingCodeSubtype> GetForInsFilingCode(long insFilingCodeNum) {
			List <InsFilingCodeSubtype> insFilingCodeSubtypes=new List<InsFilingCodeSubtype>();
			for(int i=0;i<InsFilingCodeSubtypeC.Listt.Count;i++){
				if(InsFilingCodeSubtypeC.Listt[i].InsFilingCodeNum==insFilingCodeNum){
					insFilingCodeSubtypes.Add(InsFilingCodeSubtypeC.Listt[i]);
				}
			}
			return insFilingCodeSubtypes;
		}

		public static void DeleteForInsFilingCode(long insFilingCodeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),insFilingCodeNum);
				return;
			}
			string command="DELETE FROM insfilingcodesubtype "+
				"WHERE InsFilingCodeNum="+POut.Long(insFilingCodeNum);
			Db.NonQ(command);
		}

	}
}