using System;
using System.Collections;
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
			ListLong=new ClaimForm[table.Rows.Count];
			ArrayList tempAL=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++) {
				ListLong[i]=new ClaimForm();
				ListLong[i].ClaimFormNum=PIn.PLong(table.Rows[i][0].ToString());
				ListLong[i].Description=PIn.PString(table.Rows[i][1].ToString());
				ListLong[i].IsHidden=PIn.PBool(table.Rows[i][2].ToString());
				ListLong[i].FontName=PIn.PString(table.Rows[i][3].ToString());
				ListLong[i].FontSize=PIn.PFloat(table.Rows[i][4].ToString());
				ListLong[i].UniqueID=PIn.PString(table.Rows[i][5].ToString());
				ListLong[i].PrintImages=PIn.PBool(table.Rows[i][6].ToString());
				ListLong[i].OffsetX=PIn.PInt(table.Rows[i][7].ToString());
				ListLong[i].OffsetY=PIn.PInt(table.Rows[i][8].ToString());
				ListLong[i].Items=ClaimFormItems.GetListForForm(ListLong[i].ClaimFormNum);
				if(!ListLong[i].IsHidden)
					tempAL.Add(ListLong[i]);
			}
			ListShort=new ClaimForm[tempAL.Count];
			for(int i=0;i<ListShort.Length;i++) {
				ListShort[i]=(ClaimForm)tempAL[i];
			}
		}

		///<summary>Inserts this claimform into database and retrieves the new primary key.</summary>
		public static long Insert(ClaimForm cf) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				cf.ClaimFormNum=Meth.GetLong(MethodBase.GetCurrentMethod(),cf);
				return cf.ClaimFormNum;
			}
			if(PrefC.RandomKeys) {
				cf.ClaimFormNum=ReplicationServers.GetKey("claimform","ClaimFormNum");
			}
			string command="INSERT INTO claimform (";
			if(PrefC.RandomKeys) {
				command+="ClaimFormNum,";
			}
			command+="Description,IsHidden,FontName,FontSize"
				+",UniqueId,PrintImages,OffsetX,OffsetY) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PLong(cf.ClaimFormNum)+", ";
			}
			command+=
				 "'"+POut.PString(cf.Description)+"', "
				+"'"+POut.PBool  (cf.IsHidden)+"', "
				+"'"+POut.PString(cf.FontName)+"', "
				+"'"+POut.PFloat (cf.FontSize)+"', "
				+"'"+POut.PString(cf.UniqueID)+"', "
				+"'"+POut.PBool  (cf.PrintImages)+"', "
				+"'"+POut.PLong   (cf.OffsetX)+"', "
				+"'"+POut.PLong   (cf.OffsetY)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				cf.ClaimFormNum=Db.NonQ(command,true);
			}
			return cf.ClaimFormNum;
		}

		///<summary></summary>
		public static void Update(ClaimForm cf){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cf);
				return;
			}
			string command="UPDATE claimform SET "
				+"Description = '" +POut.PString(cf.Description)+"' "
				+",IsHidden = '"    +POut.PBool  (cf.IsHidden)+"' "
				+",FontName = '"    +POut.PString(cf.FontName)+"' "
				+",FontSize = '"    +POut.PFloat (cf.FontSize)+"' "
				+",UniqueID = '"    +POut.PString(cf.UniqueID)+"' "
				+",PrintImages = '" +POut.PBool  (cf.PrintImages)+"' "
				+",OffsetX = '"     +POut.PLong   (cf.OffsetX)+"' "
				+",OffsetY = '"     +POut.PLong   (cf.OffsetY)+"' "
				+"WHERE ClaimFormNum = '"+POut.PLong   (cf.ClaimFormNum)+"'";
 			Db.NonQ(command);
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
				+"WHERE ClaimFormNum = '"+POut.PLong(cf.ClaimFormNum)+"'";
			Db.NonQ(command);
			command="DELETE FROM claimformitem "
				+"WHERE ClaimFormNum = '"+POut.PLong(cf.ClaimFormNum)+"'";
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
				claimFormNums[i]=PIn.PLong   (table.Rows[i][0].ToString());
			}
			//loop through each matching claimform
			for(int i=0;i<claimFormNums.Length;i++){
				cf.ClaimFormNum=claimFormNums[i];
				Update(cf);
				command="DELETE FROM claimformitem "
					+"WHERE ClaimFormNum = '"+POut.PLong(claimFormNums[i])+"'";
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
			string command="UPDATE insplan SET ClaimFormNum="+POut.PLong(newClaimFormNum)
				+" WHERE ClaimFormNum="+POut.PLong(oldClaimFormNum);
			return Db.NonQ(command);
		}



	}

	



}









