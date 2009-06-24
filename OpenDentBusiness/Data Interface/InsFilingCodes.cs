using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class InsFilingCodes{

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT * FROM insfilingcode ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="InsFilingCode";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			InsFilingCodeC.Listt=new List <InsFilingCode>();
			InsFilingCode insFilingCode;
			for(int i=0;i<table.Rows.Count;i++) {
				insFilingCode=new InsFilingCode();
				insFilingCode.IsNew=false;
				insFilingCode.InsFilingCodeNum=PIn.PInt(table.Rows[i][0].ToString());
				insFilingCode.Descript=PIn.PString(table.Rows[i][1].ToString());
				insFilingCode.EclaimCode=PIn.PString(table.Rows[i][2].ToString());
				insFilingCode.ItemOrder=PIn.PInt(table.Rows[i][3].ToString());
				InsFilingCodeC.Listt.Add(insFilingCode);
			}
		}

		///<Summary>Gets one InsFilingCode from the database.</Summary>
		public static InsFilingCode CreateObject(int insFilingCodeNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<InsFilingCode>(MethodBase.GetCurrentMethod(),insFilingCodeNum);
			}
			return DataObjectFactory<InsFilingCode>.CreateObject(insFilingCodeNum);
		}

		public static List<InsFilingCode> GetInsFilingCodes(List <int> insFilingCodeNums){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<InsFilingCode>>(MethodBase.GetCurrentMethod(),insFilingCodeNums);
			}
			Collection<InsFilingCode> collectState=DataObjectFactory<InsFilingCode>.CreateObjects(insFilingCodeNums);
			return new List<InsFilingCode>(collectState);		
		}

		///<summary></summary>
		public static int WriteObject(InsFilingCode insFilingCode){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				insFilingCode.InsFilingCodeNum=Meth.GetInt(MethodBase.GetCurrentMethod(),insFilingCode);
				return insFilingCode.InsFilingCodeNum;
			}
			DataObjectFactory<InsFilingCode>.WriteObject(insFilingCode);
			return insFilingCode.InsFilingCodeNum;
		}

		///<summary></summary>
		public static void DeleteObject(int insFilingCodeNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),insFilingCodeNum);
				return;
			}
			//TODO: validate that not currently in use.
			DataObjectFactory<InsFilingCode>.DeleteObject(insFilingCodeNum);
		}


	}
}