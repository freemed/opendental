using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class SigButDefs {
		private static SigButDef[] listt;

		///<summary>A list of all SigButDefs.</summary>
		public static SigButDef[] Listt {
			//No need to check RemotingRole; no call to db.
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

		///<summary>Gets a list of all SigButDefs when program first opens.  Also refreshes SigButDefElements and attaches all elements to the appropriate buttons.</summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM sigbutdef ORDER BY ButtonIndex";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="SigButDef";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			listt=Crud.SigButDefCrud.TableToList(table).ToArray();
		}

		///<summary></summary>
		public static void Update(SigButDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			Crud.SigButDefCrud.Update(def);
		}

		///<summary></summary>
		public static long Insert(SigButDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				def.SigButDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),def);
				return def.SigButDefNum;
			}
			return Crud.SigButDefCrud.Insert(def);
		}

		///<summary>No need to surround with try/catch, because all deletions are allowed.</summary>
		public static void Delete(SigButDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			string command="DELETE FROM sigbutdefelement WHERE SigButDefNum="+POut.Long(def.SigButDefNum);
			Db.NonQ(command);
			command="DELETE FROM sigbutdef WHERE SigButDefNum ="+POut.Long(def.SigButDefNum);
			Db.NonQ(command);
		}

		///<summary>Used in the Button edit dialog.</summary>
		public static void DeleteElements(SigButDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			string command="DELETE FROM sigbutdefelement WHERE SigButDefNum="+POut.Long(def.SigButDefNum);
			Db.NonQ(command);
		}

		///<summary>Loops through the element list and pulls out one element of a specific type. Used in the button edit window.</summary>
		public static SigButDefElement GetElement(SigButDef def,SignalElementType elementType) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<def.ElementList.Length;i++) {
				if(SigElementDefs.GetElement(def.ElementList[i].SigElementDefNum).SigElementType==elementType) {
					return def.ElementList[i].Copy();
				}
			}
			return null;
		}




		///<summary>Used in Setup.  The returned list also includes defaults if not overridden by one with a computername.  The supplied computer name can be blank for the default setup.</summary>
		public static SigButDef[] GetByComputer(string computerName) {
			//No need to check RemotingRole; no call to db.
			//first, get a default list, because we will always need that
			ArrayList AL=new ArrayList();
			for(int i=0;i<Listt.Length;i++) {
				if(Listt[i].ComputerName=="") {
					AL.Add(Listt[i]);
				}
			}
			SigButDef[] defaultList=new SigButDef[AL.Count];
			AL.CopyTo(defaultList);
			if(computerName=="") {//if all we are interested in is the default list, then done.
				return defaultList;
			}
			//for any other computer:
			List<SigButDef> listSigButDefs=new List<SigButDef>();
			for(int i=0;i<Listt.Length;i++) {
				if(computerName==Listt[i].ComputerName) {
					listSigButDefs.Add(Listt[i]);
				}
			}
			//but we are still missing some defaults
			SigButDef matchingBut;
			for(int i=0;i<defaultList.Length;i++) {
				matchingBut=GetByIndex(defaultList[i].ButtonIndex,listSigButDefs);//retVal);
				if(matchingBut!=null) {//There is a button for this computer which overrides the default, so don't add the default.
					continue;
				}
				//AL.Add(defaultList[i]);
				listSigButDefs.Add(defaultList[i]);
			}
			listSigButDefs.Sort(CompareButtonsByIndex);
			SigButDef[] retVal=new SigButDef[listSigButDefs.Count];
			listSigButDefs.CopyTo(retVal);
			return retVal;
		}

		private static int CompareButtonsByIndex(SigButDef x,SigButDef y) {
			//No need to check RemotingRole; no call to db.
			return x.ButtonIndex.CompareTo(y.ButtonIndex);
		}

		///<summary>Moves the selected item up in the supplied sub list.</summary>
		public static void MoveUp(SigButDef selected,SigButDef[] subList) {
			//No need to check RemotingRole; no call to db.
			if(selected.ButtonIndex==0) {//already at top
				return;
			}
			SigButDef occupied=null;
			for(int i=0;i<subList.Length;i++) {
				if(subList[i].SigButDefNum!=selected.SigButDefNum//if not the selected object
					&& subList[i].ButtonIndex==selected.ButtonIndex-1)//and position occupied
				{
					occupied=subList[i].Copy();
				}
			}
			if(occupied!=null) {
				occupied.ButtonIndex++;
				Update(occupied);
			}
			selected.ButtonIndex--;
			Update(selected);
		}

		///<summary></summary>
		public static void MoveDown(SigButDef selected,SigButDef[] subList) {
			//No need to check RemotingRole; no call to db.
			if(selected.ButtonIndex==20) {
				throw new ApplicationException(Lans.g("SigButDefs","Max 20 buttons."));
			}
			SigButDef occupied=null;
			for(int i=0;i<subList.Length;i++) {
				if(subList[i].SigButDefNum!=selected.SigButDefNum//if not the selected object
					&& subList[i].ButtonIndex==selected.ButtonIndex+1)//and position occupied
				{
					occupied=subList[i].Copy();
				}
			}
			if(occupied!=null) {
				occupied.ButtonIndex--;
				Update(occupied);
			}
			selected.ButtonIndex++;
			Update(selected);
		}

		///<summary>Returns the SigButDef with the specified buttonIndex.  Used from the setup page.  The supplied list will already have been filtered by computername.  Supply buttonIndex in 0-based format.</summary>
		public static SigButDef GetByIndex(int buttonIndex,List<SigButDef> subList) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<subList.Count;i++) {
				if(subList[i].ButtonIndex==buttonIndex) {
					return subList[i].Copy();
				}
			}
			return null;
		}

		///<summary>Returns the SigButDef with the specified buttonIndex.  Used from the setup page.  The supplied list will already have been filtered by computername.  Supply buttonIndex in 0-based format.</summary>
		public static SigButDef GetByIndex(int buttonIndex,SigButDef[] subList) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<subList.Length;i++) {
				if(subList[i].ButtonIndex==buttonIndex) {
					return subList[i].Copy();
				}
			}
			return null;
		}


	}













}










