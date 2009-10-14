using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class DiseaseDefs {
		private static DiseaseDef[] listLong;
		private static DiseaseDef[] list;

		///<summary>A list of all Diseases.</summary>
		public static DiseaseDef[] ListLong{
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

		///<summary>The list that is typically used. Does not include hidden diseases.</summary>
		public static DiseaseDef[] List {
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

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM diseasedef ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="DiseaseDef";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			ListLong=new DiseaseDef[table.Rows.Count];
			ArrayList AL=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++) {
				ListLong[i]=new DiseaseDef();
				ListLong[i].DiseaseDefNum=PIn.PLong(table.Rows[i][0].ToString());
				ListLong[i].DiseaseName=PIn.PString(table.Rows[i][1].ToString());
				ListLong[i].ItemOrder=PIn.PInt(table.Rows[i][2].ToString());
				ListLong[i].IsHidden=PIn.PBool(table.Rows[i][3].ToString());
				if(!ListLong[i].IsHidden) {
					AL.Add(ListLong[i]);
				}
			}
			List=new DiseaseDef[AL.Count];
			AL.CopyTo(List);
		}

		///<summary></summary>
		public static void Update(DiseaseDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			string command="UPDATE diseasedef SET " 
				+"DiseaseName = '" +POut.PString(def.DiseaseName)+"'"
				+",ItemOrder = '"   +POut.PLong   (def.ItemOrder)+"'"
				+",IsHidden = '"    +POut.PBool  (def.IsHidden)+"'"
				+" WHERE DiseaseDefNum  ='"+POut.PLong   (def.DiseaseDefNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(DiseaseDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				def.DiseaseDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),def);
				return def.DiseaseDefNum;
			}
			if(PrefC.RandomKeys) {
				def.DiseaseDefNum=ReplicationServers.GetKey("diseasedef","DiseaseDefNum");
			}
			string command="INSERT INTO diseasedef (";
			if(PrefC.RandomKeys) {
				command+="DiseaseDefNum,";
			}
			command+="DiseaseName,ItemOrder,IsHidden) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PLong(def.DiseaseDefNum)+", ";
			}
			command+=
				 "'"+POut.PString(def.DiseaseName)+"', "
				+"'"+POut.PLong   (def.ItemOrder)+"', "
				+"'"+POut.PBool  (def.IsHidden)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				def.DiseaseDefNum=Db.NonQ(command,true);
			}
			return def.DiseaseDefNum;
		}

		///<summary>Surround with try/catch, because it will throw an exception if any patient is using this def.</summary>
		public static void Delete(DiseaseDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			string command="SELECT LName,FName FROM patient,disease WHERE "
				+"patient.PatNum=disease.PatNum "
				+"AND disease.DiseaseDefNum='"+POut.PLong(def.DiseaseDefNum)+"'";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count>0){
				string s=Lans.g("DiseaseDef","Not allowed to delete. Already in use by ")+table.Rows.Count.ToString()
					+" "+Lans.g("DiseaseDef","patients, including")+" \r\n";
				for(int i=0;i<table.Rows.Count;i++){
					if(i>5){
						break;
					}
					s+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString()+"\r\n";
				}
				throw new ApplicationException(s);
			}
			command="DELETE FROM diseasedef WHERE DiseaseDefNum ="+POut.PLong(def.DiseaseDefNum);
			Db.NonQ(command);
		}

		///<summary>Moves the selected item up in the listLong.</summary>
		public static void MoveUp(int selected){
			//No need to check RemotingRole; no call to db.
			if(selected<0) {
				throw new ApplicationException(Lans.g("DiseaseDefs","Please select an item first."));
			}
			if(selected==0) {//already at top
				return;
			}
			if(selected>ListLong.Length-1){
				throw new ApplicationException(Lans.g("DiseaseDefs","Invalid selection."));
			}
			SetOrder(selected-1,ListLong[selected].ItemOrder);
			SetOrder(selected,ListLong[selected].ItemOrder-1);
			//Selected-=1;
		}

		///<summary></summary>
		public static void MoveDown(int selected) {
			//No need to check RemotingRole; no call to db.
			if(selected<0) {
				throw new ApplicationException(Lans.g("DiseaseDefs","Please select an item first."));
			}
			if(selected==ListLong.Length-1){//already at bottom
				return;
			}
			if(selected>ListLong.Length-1) {
				throw new ApplicationException(Lans.g("DiseaseDefs","Invalid selection."));
			}
			SetOrder(selected+1,ListLong[selected].ItemOrder);
			SetOrder(selected,ListLong[selected].ItemOrder+1);
			//selected+=1;
		}

		///<summary>Used by MoveUp and MoveDown.</summary>
		private static void SetOrder(int mySelNum,int myItemOrder) {
			//No need to check RemotingRole; no call to db.
			DiseaseDef temp=ListLong[mySelNum];
			temp.ItemOrder=myItemOrder;
			DiseaseDefs.Update(temp);
		}

		///<summary>Returns the order in ListLong, whether hidden or not.</summary>
		public static int GetOrder(long diseaseDefNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].DiseaseDefNum==diseaseDefNum){
					return ListLong[i].ItemOrder;
				}
			}
			return 0;
		}

		///<summary>Returns the name of the disease, whether hidden or not.</summary>
		public static string GetName(long diseaseDefNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].DiseaseDefNum==diseaseDefNum) {
					return ListLong[i].DiseaseName;
				}
			}
			return "";
		}

		///<summary>Returns the diseaseDef with the specified num.</summary>
		public static DiseaseDef GetItem(long diseaseDefNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].DiseaseDefNum==diseaseDefNum) {
					return ListLong[i].Copy();
				}
			}
			return null;
		}

		///<summary>Returns the diseaseDefNum that exactly matches the specified string.  Used in import functions when you only have the name to work with.  Can return 0 if no match.  Does not match hidden diseases.</summary>
		public static long GetNumFromName(string diseaseName){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++){
				if(diseaseName==List[i].DiseaseName){
					return List[i].DiseaseDefNum;
				}
			}
			return 0;
		}
		
		
	}

		



		
	

	

	


}










