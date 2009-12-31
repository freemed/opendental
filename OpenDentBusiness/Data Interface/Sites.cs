using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Sites{
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT * FROM site ORDER BY Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="Site";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			SiteC.List=new Site[table.Rows.Count];
			for(int i=0;i<SiteC.List.Length;i++){
				SiteC.List[i]=new Site();
				SiteC.List[i].IsNew=false;
				SiteC.List[i].SiteNum    = PIn.Long   (table.Rows[i][0].ToString());
				SiteC.List[i].Description= PIn.String(table.Rows[i][1].ToString());
				SiteC.List[i].Note       = PIn.String(table.Rows[i][2].ToString());
			}
		}

		///<Summary>Gets one Site from the database.</Summary>
		public static Site CreateObject(long siteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Site>(MethodBase.GetCurrentMethod(),siteNum);
			}
			return DataObjectFactory<Site>.CreateObject(siteNum);
		}

		public static List<Site> GetSites(List<long> siteNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Site>>(MethodBase.GetCurrentMethod(),siteNums);
			}
			Collection<Site> collectState=DataObjectFactory<Site>.CreateObjects(siteNums);
			return new List<Site>(collectState);		
		}

		///<summary></summary>
		public static long WriteObject(Site site) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				site.SiteNum=Meth.GetLong(MethodBase.GetCurrentMethod(),site);
				return site.SiteNum;
			}
			DataObjectFactory<Site>.WriteObject(site);
			return site.SiteNum;
		}

		///<summary></summary>
		public static void DeleteObject(long siteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),siteNum);
				return;
			}
			//validate that not already in use.
			string command="SELECT LName,FName FROM patient WHERE SiteNum="+POut.Long(siteNum);
			DataTable table=Db.GetTable(command);
			//int count=PIn.PInt(Db.GetCount(command));
			string pats="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					pats+=", ";
				}
				pats+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lans.g("Sites","Site is already in use by patient(s). Not allowed to delete. ")+pats);
			}
			DataObjectFactory<Site>.DeleteObject(siteNum);
		}

		//public static void DeleteObject(int siteNum){
		//	DataObjectFactory<Site>.DeleteObject(siteNum);
		//}

		public static string GetDescription(long siteNum) {
			//No need to check RemotingRole; no call to db.
			if(siteNum==0){
				return "";
			}
			for(int i=0;i<SiteC.List.Length;i++){
				if(SiteC.List[i].SiteNum==siteNum){
					return SiteC.List[i].Description;
				}
			}
			return "";
		}

		public static List<Site> GetListFiltered(string snippet) {
			//No need to check RemotingRole; no call to db.
			List<Site> retVal=new List<Site>();
			if(snippet=="") {
				return retVal;
			}
			for(int i=0;i<SiteC.List.Length;i++) {
				if(SiteC.List[i].Description.ToLower().Contains(snippet.ToLower())) {
					retVal.Add(SiteC.List[i]);
				}
			}
			return retVal;
		}

		///<summary>Will return -1 if no match.</summary>
		public static long FindMatchSiteNum(string description) {
			//No need to check RemotingRole; no call to db.
			if(description=="") {
				return 0;
			}
			for(int i=0;i<SiteC.List.Length;i++) {
				if(SiteC.List[i].Description.ToLower()==description.ToLower()) {
					return SiteC.List[i].SiteNum;
				}
			}
			return -1;
		}


	}
}