using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrLabImages{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrLabImages.</summary>
		private static List<EhrLabImage> listt;

		///<summary>A list of all EhrLabImages.</summary>
		public static List<EhrLabImage> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM ehrlabimage ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrLabImage";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrLabImageCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EhrLabImage> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrLabImage>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrlabimage WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrLabImageCrud.SelectMany(command);
		}

		///<summary>Gets one EhrLabImage from the db.</summary>
		public static EhrLabImage GetOne(long ehrLabImageNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrLabImage>(MethodBase.GetCurrentMethod(),ehrLabImageNum);
			}
			return Crud.EhrLabImageCrud.SelectOne(ehrLabImageNum);
		}

		///<summary></summary>
		public static long Insert(EhrLabImage ehrLabImage){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrLabImage.EhrLabImageNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrLabImage);
				return ehrLabImage.EhrLabImageNum;
			}
			return Crud.EhrLabImageCrud.Insert(ehrLabImage);
		}

		///<summary></summary>
		public static void Update(EhrLabImage ehrLabImage){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrLabImage);
				return;
			}
			Crud.EhrLabImageCrud.Update(ehrLabImage);
		}

		///<summary></summary>
		public static void Delete(long ehrLabImageNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrLabImageNum);
				return;
			}
			string command= "DELETE FROM ehrlabimage WHERE EhrLabImageNum = "+POut.Long(ehrLabImageNum);
			Db.NonQ(command);
		}
		*/



	}
}