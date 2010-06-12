using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Clearinghouses {
		///<summary>List of all clearinghouses.</summary>
		private static Clearinghouse[] listt;
		///<summary>Key=PayorID. Value=ClearinghouseNum.</summary>
		private static Hashtable HList;

		public static Clearinghouse[] Listt{
			//No need to check RemotingRole; no call to db.
			get{
				if(listt==null){
					RefreshCache();
				}
				return listt;
			}
			set{
				listt=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM clearinghouse";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Clearinghouse";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			listt=Crud.ClearinghouseCrud.TableToList(table).ToArray();
			HList=new Hashtable();
			string[] payors;
			for(int i=0;i<listt.Length;i++) {
				payors=listt[i].Payors.Split(',');
				for(int j=0;j<payors.Length;j++) {
					if(!HList.ContainsKey(payors[j])) {
						HList.Add(payors[j],listt[i].ClearinghouseNum);
					}
				}
			}
		}

		///<summary>Inserts this clearinghouse into database.</summary>
		public static long Insert(Clearinghouse clearhouse){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				clearhouse.ClearinghouseNum=Meth.GetLong(MethodBase.GetCurrentMethod(),clearhouse);
				return clearhouse.ClearinghouseNum;
			}
			return Crud.ClearinghouseCrud.Insert(clearhouse);
		}

		///<summary></summary>
		public static void Update(Clearinghouse clearhouse){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clearhouse);
				return;
			}
			Crud.ClearinghouseCrud.Update(clearhouse);
		}

		///<summary></summary>
		public static void Delete(Clearinghouse clearhouse){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),clearhouse);
				return;
			}
			string command="DELETE FROM clearinghouse "
				+"WHERE ClearinghouseNum = '"+POut.Long(clearhouse.ClearinghouseNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Gets the last batch number for this clearinghouse and increments it by one.  Saves the new value, then returns it.  So even if the new value is not used for some reason, it will have already been incremented. Remember that LastBatchNumber is never accurate with local data in memory.</summary>
		public static int GetNextBatchNumber(Clearinghouse clearhouse){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),clearhouse);
			}
			//get last batch number
			string command="SELECT LastBatchNumber FROM clearinghouse "
				+"WHERE ClearinghouseNum = "+POut.Long(clearhouse.ClearinghouseNum);
 			DataTable table=Db.GetTable(command);
			int batchNum=PIn.Int(table.Rows[0][0].ToString());
			//and increment it by one
			if(clearhouse.Eformat==ElectronicClaimFormat.Canadian){
				if(batchNum==999999){
					batchNum=1;
				}
				else{
					batchNum++;
				}
			}
			else{
				if(batchNum==999){
					batchNum=1;
				}
				else{
					batchNum++;
				}
			}
			//save the new batch number. Even if user cancels, it will have incremented.
			command="UPDATE clearinghouse SET LastBatchNumber="+batchNum.ToString()
				+" WHERE ClearinghouseNum = "+POut.Long(clearhouse.ClearinghouseNum);
			Db.NonQ(command);
			return batchNum;
		}

		///<summary>Returns the clearinghouseNum for claims for the supplied payorID.  If the payorID was not entered or if no default was set, then 0 is returned.</summary>
		public static long GetNumForPayor(string payorID){
			//No need to check RemotingRole; no call to db.
			//this is not done because Renaissance does not require payorID
			//if(payorID==""){
			//	return ElectronicClaimFormat.None;
			//}
			if(HList==null) {
				RefreshCache();
			}
			if(payorID!="" && HList.ContainsKey(payorID)){
				return (long)HList[payorID];
			}
			//payorID not found
			Clearinghouse defaultCH=GetDefault();
			if(defaultCH==null){
				return 0;//ElectronicClaimFormat.None;
			}
			return defaultCH.ClearinghouseNum;
		}

		///<summary>Returns the default clearinghouse. If no default present, returns null.</summary>
		public static Clearinghouse GetDefault(){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Length;i++){
				if(Listt[i].IsDefault){
					return Listt[i];
				}
			}
			return null;
		}

	}
}