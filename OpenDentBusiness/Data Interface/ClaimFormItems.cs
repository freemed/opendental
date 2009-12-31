using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ClaimFormItems {

		///<summary>Gets all claimformitems for all claimforms.  Items for individual claimforms can later be extracted as needed.</summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command=
				"SELECT * FROM claimformitem ORDER BY imagefilename desc";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ClaimFormItem";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			ClaimFormItemC.List=new ClaimFormItem[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				ClaimFormItemC.List[i]=new ClaimFormItem();
				ClaimFormItemC.List[i].ClaimFormItemNum= PIn.Long(table.Rows[i][0].ToString());
				ClaimFormItemC.List[i].ClaimFormNum    = PIn.Long(table.Rows[i][1].ToString());
				ClaimFormItemC.List[i].ImageFileName   = PIn.String(table.Rows[i][2].ToString());
				ClaimFormItemC.List[i].FieldName       = PIn.String(table.Rows[i][3].ToString());
				ClaimFormItemC.List[i].FormatString    = PIn.String(table.Rows[i][4].ToString());
				ClaimFormItemC.List[i].XPos            = PIn.Float(table.Rows[i][5].ToString());
				ClaimFormItemC.List[i].YPos            = PIn.Float(table.Rows[i][6].ToString());
				ClaimFormItemC.List[i].Width           = PIn.Float(table.Rows[i][7].ToString());
				ClaimFormItemC.List[i].Height          = PIn.Float(table.Rows[i][8].ToString());
			}
		}

		///<summary></summary>
		public static long Insert(ClaimFormItem item) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				item.ClaimFormItemNum=Meth.GetLong(MethodBase.GetCurrentMethod(),item);
				return item.ClaimFormItemNum;
			}
			if(PrefC.RandomKeys) {
				item.ClaimFormItemNum=ReplicationServers.GetKey("claimformitem","ClaimFormItemNum");
			}
			string command="INSERT INTO claimformitem (";
			if(PrefC.RandomKeys) {
				command+="ClaimFormItemNum,";
			}
			command+="ClaimFormNum,ImageFileName,FieldName,FormatString"
				+",XPos,YPos,Width,Height) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(item.ClaimFormItemNum)+", ";
			}
			command+=
				 "'"+POut.Long   (item.ClaimFormNum)+"', "
				+"'"+POut.String(item.ImageFileName)+"', "
				+"'"+POut.String(item.FieldName)+"', "
				+"'"+POut.String(item.FormatString)+"', "
				+"'"+POut.Float (item.XPos)+"', "
				+"'"+POut.Float (item.YPos)+"', "
				+"'"+POut.Float (item.Width)+"', "
				+"'"+POut.Float (item.Height)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				item.ClaimFormItemNum=Db.NonQ(command,true);
			}
			return item.ClaimFormItemNum;
		}

		///<summary></summary>
		public static void Update(ClaimFormItem item){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),item);
				return;
			}
			string command= "UPDATE claimformitem SET "
				+"claimformnum = '" +POut.Long   (item.ClaimFormNum)+"' "
				+",imagefilename = '"+POut.String(item.ImageFileName)+"' "
				+",fieldname = '"    +POut.String(item.FieldName)+"' "
				+",formatstring = '" +POut.String(item.FormatString)+"' "
				+",xpos = '"         +POut.Float (item.XPos)+"' "
				+",ypos = '"         +POut.Float (item.YPos)+"' "
				+",width = '"        +POut.Float (item.Width)+"' "
				+",height = '"       +POut.Float (item.Height)+"' "
				+"WHERE ClaimFormItemNum = '"+POut.Long   (item.ClaimFormItemNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ClaimFormItem item){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),item);
				return;
			}
			string command = "DELETE FROM claimformitem "
				+"WHERE ClaimFormItemNum = '"+POut.Long(item.ClaimFormItemNum)+"'";
 			Db.NonQ(command);
		}


		///<summary>Gets all claimformitems for the specified claimform from the preloaded List.</summary>
		public static ClaimFormItem[] GetListForForm(long claimFormNum) {
			//No need to check RemotingRole; no call to db.
			ArrayList tempAL=new ArrayList();
			for(int i=0;i<ClaimFormItemC.List.Length;i++){
				if(ClaimFormItemC.List[i].ClaimFormNum==claimFormNum){
					tempAL.Add(ClaimFormItemC.List[i]);
				}
			}
			ClaimFormItem[] ListForForm=new ClaimFormItem[tempAL.Count];
			tempAL.CopyTo(ListForForm);
			return ListForForm;
		}


	}

	

	

}









