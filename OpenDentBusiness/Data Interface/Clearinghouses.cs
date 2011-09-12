using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Clearinghouses {
		///<summary>List of all clearinghouses.</summary>
		private static Clearinghouse[] listt;
		///<summary>Key=PayorID. Value=ClearingHouseNum.</summary>
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
			string command="DELETE FROM clearinghouse WHERE ClearinghouseNum = '"+POut.Long(clearhouse.ClearinghouseNum)+"'";
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
		public static long AutomateClearinghouseSelection(string payorID,EnumClaimMedType medType){
			//No need to check RemotingRole; no call to db.
			//payorID can be blank.  For example, Renaissance does not require payorID.
			if(HList==null) {
				RefreshCache();
			}
			Clearinghouse clearinghouse=null;
			if(medType==EnumClaimMedType.Dental){
				if(PrefC.GetLong(PrefName.ClearinghouseDefaultDent)==0){
					return 0;
				}
				clearinghouse=GetClearinghouse(PrefC.GetLong(PrefName.ClearinghouseDefaultDent));
			}
			if(medType==EnumClaimMedType.Medical || medType==EnumClaimMedType.Institutional){
				if(PrefC.GetLong(PrefName.ClearinghouseDefaultMed)==0){
					return 0;
				}
				clearinghouse=GetClearinghouse(PrefC.GetLong(PrefName.ClearinghouseDefaultMed));
			}
			if(clearinghouse==null){//we couldn't find a default clearinghouse for that medType.  Needs to always be a default.
				return 0;
			}
			if(payorID!="" && HList.ContainsKey(payorID)){//an override exists for this payorID
				Clearinghouse ch=GetClearinghouse((long)HList[payorID]);
				if(ch.Eformat==ElectronicClaimFormat.x837D_4010 || ch.Eformat==ElectronicClaimFormat.x837D_5010_dental){
					if(medType==EnumClaimMedType.Dental){//med type matches
						return ch.ClearinghouseNum;
					}
				}
				if(ch.Eformat==ElectronicClaimFormat.x837_5010_med_inst){
					if(medType==EnumClaimMedType.Medical || medType==EnumClaimMedType.Institutional){//med type matches
						return ch.ClearinghouseNum;
					}
				}
			}
			//no override, so just return the default.
			return clearinghouse.ClearinghouseNum;
		}

		///<summary>Returns the default clearinghouse. If no default present, returns null.</summary>
		public static Clearinghouse GetDefaultDental(){
			//No need to check RemotingRole; no call to db.
			return GetClearinghouse(PrefC.GetLong(PrefName.ClearinghouseDefaultDent));
		}

		///<summary>Gets a clearinghouse from cache.  Will return null if invalid.</summary>
		public static Clearinghouse GetClearinghouse(long clearinghouseNum){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Length;i++){
				if(clearinghouseNum==Listt[i].ClearinghouseNum){
					return Listt[i];
				}
			}
			return null;
		}

	}
}