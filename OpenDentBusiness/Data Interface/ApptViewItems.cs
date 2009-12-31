using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using OpenDentBusiness;

namespace OpenDentBusiness{
	///<summary>Handles database commands related to the apptviewitem table in the database.</summary>
	public class ApptViewItems{
		
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * from apptviewitem ORDER BY ElementOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ApptViewItem";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			ApptViewItemC.List=new ApptViewItem[table.Rows.Count];
			for(int i=0;i<ApptViewItemC.List.Length;i++){
				ApptViewItemC.List[i]=new ApptViewItem();
				ApptViewItemC.List[i].ApptViewItemNum = PIn.Long   (table.Rows[i][0].ToString());
				ApptViewItemC.List[i].ApptViewNum     = PIn.Long   (table.Rows[i][1].ToString());
				ApptViewItemC.List[i].OpNum           = PIn.Long   (table.Rows[i][2].ToString());
				ApptViewItemC.List[i].ProvNum         = PIn.Long   (table.Rows[i][3].ToString());
				ApptViewItemC.List[i].ElementDesc     = PIn.String(table.Rows[i][4].ToString());
				ApptViewItemC.List[i].ElementOrder    = PIn.Int   (table.Rows[i][5].ToString());
				ApptViewItemC.List[i].ElementColor    = Color.FromArgb(PIn.Int(table.Rows[i][6].ToString()));
			}
		}

		///<summary></summary>
		public static long Insert(ApptViewItem Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.ApptViewItemNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.ApptViewItemNum;
			}
			if(PrefC.RandomKeys) {
				Cur.ApptViewItemNum=ReplicationServers.GetKey("apptviewitem","ApptViewItemNum");
			}
			string command="INSERT INTO apptviewitem (";
			if(PrefC.RandomKeys) {
				command+="ApptViewItemNum,";
			}
			command+="ApptViewNum,OpNum,ProvNum,ElementDesc,"
				+"ElementOrder,ElementColor) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(Cur.ApptViewItemNum)+", ";
			}
			command+=
				 "'"+POut.Long   (Cur.ApptViewNum)+"', "
				+"'"+POut.Long   (Cur.OpNum)+"', "
				+"'"+POut.Long   (Cur.ProvNum)+"', "
				+"'"+POut.String(Cur.ElementDesc)+"', "
				+"'"+POut.Long   (Cur.ElementOrder)+"', "
				+"'"+POut.Long   (Cur.ElementColor.ToArgb())+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				Cur.ApptViewItemNum=Db.NonQ(command,true);
			}
			return Cur.ApptViewItemNum;
		}

		///<summary></summary>
		public static void Update(ApptViewItem Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "UPDATE apptviewitem SET "
				+"ApptViewNum='"    +POut.Long   (Cur.ApptViewNum)+"'"
				+",OpNum = '"       +POut.Long   (Cur.OpNum)+"'"
				+",ProvNum = '"     +POut.Long   (Cur.ProvNum)+"'"
				+",ElementDesc = '" +POut.String(Cur.ElementDesc)+"'"
				+",ElementOrder = '"+POut.Long   (Cur.ElementOrder)+"'"
				+",ElementColor = '"+POut.Long   (Cur.ElementColor.ToArgb())+"'"
				+" WHERE ApptViewItemNum = '"+POut.Long(Cur.ApptViewItemNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ApptViewItem Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command="DELETE from apptviewitem WHERE ApptViewItemNum = '"
				+POut.Long(Cur.ApptViewItemNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Deletes all apptviewitems for the current apptView.</summary>
		public static void DeleteAllForView(ApptView view){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),view);
				return;
			}
			string c="DELETE from apptviewitem WHERE ApptViewNum = '"
				+POut.Long(view.ApptViewNum)+"'";
			Db.NonQ(c);
		}

		public static List<long> GetOpsForView(long apptViewNum) {
			//No need to check RemotingRole; no call to db.
			//ArrayList AL=new ArrayList();
			List<long> retVal=new List<long>();
			for(int i=0;i<ApptViewItemC.List.Length;i++){
				if(ApptViewItemC.List[i].ApptViewNum==apptViewNum && ApptViewItemC.List[i].OpNum!=0){
					retVal.Add(ApptViewItemC.List[i].OpNum);
				}
			}
			//int[] retVal=new int[AL.Count]();
			return retVal;//(int[])AL.ToArray(typeof(int));
		}

		

	}

	


}









