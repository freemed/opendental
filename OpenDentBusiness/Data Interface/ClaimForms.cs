using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ClaimForms {
		private static ClaimForm[] listLong;
		private static ClaimForm[] listShort;

		///<summary>List of all claim forms.</summary>
		public static ClaimForm[] ListLong {
			//No need to check RemotingRole; no call to db.
			get {
				if(listLong==null) {
					RefreshCache();
				}
				return listLong;
			}
			set {
				listLong=value;
			}
		}

		///<summary>List of all claim forms except those marked as hidden.</summary>
		public static ClaimForm[] ListShort {
			//No need to check RemotingRole; no call to db.
			get {
				if(listShort==null) {
					RefreshCache();
				}
				return listShort;
			}
			set {
				listShort=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM claimform";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ClaimForm";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			listLong=Crud.ClaimFormCrud.TableToList(table).ToArray();
			List<ClaimForm> ls=new List<ClaimForm>();
			for(int i=0;i<listLong.Length;i++) {
				if(!listLong[i].IsHidden){
					ls.Add(ListLong[i]);
				}
			}
			listShort=ls.ToArray();
		}

		///<summary>Inserts this claimform into database and retrieves the new primary key.</summary>
		public static long Insert(ClaimForm cf) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				cf.ClaimFormNum=Meth.GetLong(MethodBase.GetCurrentMethod(),cf);
				return cf.ClaimFormNum;
			}
			return Crud.ClaimFormCrud.Insert(cf);
		}

		///<summary></summary>
		public static void Update(ClaimForm cf){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cf);
				return;
			}
			Crud.ClaimFormCrud.Update(cf);
		}

		///<summary> Called when cancelling out of creating a new claimform, and from the claimform window when clicking delete. Returns true if successful or false if dependencies found.</summary>
		public static bool Delete(ClaimForm cf){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),cf);
			}
			//first, do dependency testing
			string command="SELECT * FROM insplan WHERE claimformnum = '"
				+cf.ClaimFormNum.ToString()+"' ";
			if(DataConnection.DBtype==DatabaseType.Oracle){
				command+="AND ROWNUM <= 1";
			}else{//Assume MySQL
				command+="LIMIT 1";
			}
 			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==1){
				return false;
			}
			//Then, delete the claimform
			command="DELETE FROM claimform "
				+"WHERE ClaimFormNum = '"+POut.Long(cf.ClaimFormNum)+"'";
			Db.NonQ(command);
			command="DELETE FROM claimformitem "
				+"WHERE ClaimFormNum = '"+POut.Long(cf.ClaimFormNum)+"'";
			Db.NonQ(command);
			return true;
		}

		///<summary>Updates all claimforms with this unique id including all attached items.</summary>
		public static void UpdateByUniqueID(ClaimForm cf){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cf);
				return;
			}
			//first get a list of the ClaimFormNums with this UniqueId
			string command=
				"SELECT ClaimFormNum FROM claimform WHERE UniqueID ='"+cf.UniqueID.ToString()+"'";
 			DataTable table=Db.GetTable(command);
			long[] claimFormNums=new long[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				claimFormNums[i]=PIn.Long   (table.Rows[i][0].ToString());
			}
			//loop through each matching claimform
			for(int i=0;i<claimFormNums.Length;i++){
				cf.ClaimFormNum=claimFormNums[i];
				Update(cf);
				command="DELETE FROM claimformitem "
					+"WHERE ClaimFormNum = '"+POut.Long(claimFormNums[i])+"'";
				Db.NonQ(command);
				for(int j=0;j<cf.Items.Length;j++){
					cf.Items[j].ClaimFormNum=claimFormNums[i];
					ClaimFormItems.Insert(cf.Items[j]);
				}
			}
		}

		///<summary>Returns the claim form specified by the given claimFormNum</summary>
		public static ClaimForm GetClaimForm(long claimFormNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ClaimFormNum==claimFormNum){
					return ListLong[i];
				}
			}
			MessageBox.Show("Error. Could not locate Claim Form.");
			return null;
		}

		///<summary>Returns the claim form specified by the given claimFormNum</summary>
		public static ClaimForm GetClaimFormByUniqueId(string uniqueId) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].UniqueID==uniqueId) {
					return ListLong[i];
				}
			}
			//MessageBox.Show("Error. Could not locate Claim Form.");
			return null;
		}


		///<summary>Returns number of insplans affected.</summary>
		public static long Reassign(long oldClaimFormNum,long newClaimFormNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),oldClaimFormNum,newClaimFormNum);
			}
			string command="UPDATE insplan SET ClaimFormNum="+POut.Long(newClaimFormNum)
				+" WHERE ClaimFormNum="+POut.Long(oldClaimFormNum);
			return Db.NonQ(command);
		}



	}

	



}









