using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class SigElementDefs {
		///<summary>A list of all SigElementDefs.</summary>
		private static SigElementDef[] list;

		public static SigElementDef[] List {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		///<summary>Gets a list of all SigElementDefs when program first opens.</summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM sigelementdef ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="SigElementDef";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=Crud.SigElementDefCrud.TableToList(table).ToArray();
		}
	
		///<summary></summary>
		public static void Update(SigElementDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			Crud.SigElementDefCrud.Update(def);
		}

		///<summary></summary>
		public static long Insert(SigElementDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				def.SigElementDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),def);
				return def.SigElementDefNum;
			}
			return Crud.SigElementDefCrud.Insert(def);
		}

		///<summary>No need to surround with try/catch, because all deletions are allowed.  This routine, deletes references in the SigButDefElement table.  References in the SigElement table are left hanging.  The user interface needs to be able to handle missing elementdefs.</summary>
		public static void Delete(SigElementDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			string command="DELETE FROM sigbutdefelement WHERE SigElementDefNum="+POut.Long(def.SigElementDefNum);
			Db.NonQ(command);
			command="DELETE FROM sigelementdef WHERE SigElementDefNum ="+POut.Long(def.SigElementDefNum);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static SigElementDef[] GetSubList(SignalElementType sigElementType){
			//No need to check RemotingRole; no call to db.
			ArrayList AL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(sigElementType==List[i].SigElementType){
					AL.Add(List[i]);
				}
			}
			SigElementDef[] retVal=new SigElementDef[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>Moves the selected item up in the supplied sub list.</summary>
		public static void MoveUp(int selected,SigElementDef[] subList){
			//No need to check RemotingRole; no call to db.
			if(selected<0) {
				throw new ApplicationException(Lans.g("SigElementDefs","Please select an item first."));
			}
			if(selected==0) {//already at top
				return;
			}
			if(selected>subList.Length-1){
				throw new ApplicationException(Lans.g("SigElementDefs","Invalid selection."));
			}
			SetOrder(selected-1,subList[selected].ItemOrder,subList);
			SetOrder(selected,subList[selected].ItemOrder-1,subList);
			//Selected-=1;
		}

		///<summary></summary>
		public static void MoveDown(int selected,SigElementDef[] subList) {
			//No need to check RemotingRole; no call to db.
			if(selected<0) {
				throw new ApplicationException(Lans.g("SigElementDefs","Please select an item first."));
			}
			if(selected==subList.Length-1){//already at bottom
				return;
			}
			if(selected>subList.Length-1) {
				throw new ApplicationException(Lans.g("SigElementDefs","Invalid selection."));
			}
			SetOrder(selected+1,subList[selected].ItemOrder,subList);
			SetOrder(selected,subList[selected].ItemOrder+1,subList);
			//selected+=1;
		}

		///<summary>Used by MoveUp and MoveDown.</summary>
		private static void SetOrder(int mySelNum,int myItemOrder,SigElementDef[] subList) {
			//No need to check RemotingRole; no call to db.
			SigElementDef temp=subList[mySelNum];
			temp.ItemOrder=myItemOrder;
			Update(temp);
		}

		///<summary>Returns the SigElementDef with the specified num.</summary>
		public static SigElementDef GetElement(long SigElementDefNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++) {
				if(List[i].SigElementDefNum==SigElementDefNum) {
					return List[i].Copy();
				}
			}
			return null;
		}
		
		
	}

		



		
	

	

	


}










