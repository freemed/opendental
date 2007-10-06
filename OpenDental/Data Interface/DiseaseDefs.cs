using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	///<summary></summary>
	public class DiseaseDefs {
		///<summary>A list of all Diseases.</summary>
		public static DiseaseDef[] ListLong;
		///<summary>The list that is typically used. Does not include hidden diseases.</summary>
		public static DiseaseDef[] List;

		///<summary>Gets a list of all DiseaseDefs when program first opens.</summary>
		public static void Refresh() {
			string command="SELECT * FROM diseasedef ORDER BY ItemOrder";
			DataTable table=General.GetTable(command);
			ListLong=new DiseaseDef[table.Rows.Count];
			ArrayList AL=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++) {
				ListLong[i]=new DiseaseDef();
				ListLong[i].DiseaseDefNum= PIn.PInt(table.Rows[i][0].ToString());
				ListLong[i].DiseaseName  = PIn.PString(table.Rows[i][1].ToString());
				ListLong[i].ItemOrder    = PIn.PInt(table.Rows[i][2].ToString());
				ListLong[i].IsHidden     = PIn.PBool(table.Rows[i][3].ToString());
				if(!ListLong[i].IsHidden) {
					AL.Add(ListLong[i]);
				}
			}
			List=new DiseaseDef[AL.Count];
			AL.CopyTo(List);
		}

		///<summary></summary>
		public static void Update(DiseaseDef def) {
			string command="UPDATE diseasedef SET " 
				+"DiseaseName = '" +POut.PString(def.DiseaseName)+"'"
				+",ItemOrder = '"   +POut.PInt   (def.ItemOrder)+"'"
				+",IsHidden = '"    +POut.PBool  (def.IsHidden)+"'"
				+" WHERE DiseaseDefNum  ='"+POut.PInt   (def.DiseaseDefNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(DiseaseDef def) {
			string command="INSERT INTO diseasedef (DiseaseName,ItemOrder,IsHidden) VALUES("
				+"'"+POut.PString(def.DiseaseName)+"', "
				+"'"+POut.PInt   (def.ItemOrder)+"', "
				+"'"+POut.PBool  (def.IsHidden)+"')";
			def.DiseaseDefNum=General.NonQ(command,true);
		}

		///<summary>Surround with try/catch, because it will throw an exception if any patient is using this def.</summary>
		public static void Delete(DiseaseDef def) {
			string command="SELECT LName,FName FROM patient,disease WHERE "
				+"patient.PatNum=disease.PatNum "
				+"AND disease.DiseaseDefNum='"+POut.PInt(def.DiseaseDefNum)+"'";
			DataTable table=General.GetTable(command);
			if(table.Rows.Count>0){
				string s=Lan.g("DiseaseDef","Not allowed to delete. Already in use by ")+table.Rows.Count.ToString()
					+" "+Lan.g("DiseaseDef","patients, including")+" \r\n";
				for(int i=0;i<table.Rows.Count;i++){
					if(i>5){
						break;
					}
					s+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString()+"\r\n";
				}
				throw new ApplicationException(s);
			}
			command="DELETE FROM diseasedef WHERE DiseaseDefNum ="+POut.PInt(def.DiseaseDefNum);
			General.NonQ(command);
		}

		///<summary>Moves the selected item up in the listLong.</summary>
		public static void MoveUp(int selected){
			if(selected<0) {
				throw new ApplicationException(Lan.g("DiseaseDefs","Please select an item first."));
			}
			if(selected==0) {//already at top
				return;
			}
			if(selected>ListLong.Length-1){
				throw new ApplicationException(Lan.g("DiseaseDefs","Invalid selection."));
			}
			SetOrder(selected-1,ListLong[selected].ItemOrder);
			SetOrder(selected,ListLong[selected].ItemOrder-1);
			//Selected-=1;
		}

		///<summary></summary>
		public static void MoveDown(int selected) {
			if(selected<0) {
				throw new ApplicationException(Lan.g("DiseaseDefs","Please select an item first."));
			}
			if(selected==ListLong.Length-1){//already at bottom
				return;
			}
			if(selected>ListLong.Length-1) {
				throw new ApplicationException(Lan.g("DiseaseDefs","Invalid selection."));
			}
			SetOrder(selected+1,ListLong[selected].ItemOrder);
			SetOrder(selected,ListLong[selected].ItemOrder+1);
			//selected+=1;
		}

		///<summary>Used by MoveUp and MoveDown.</summary>
		private static void SetOrder(int mySelNum,int myItemOrder) {
			DiseaseDef temp=ListLong[mySelNum];
			temp.ItemOrder=myItemOrder;
			DiseaseDefs.Update(temp);
		}

		///<summary>Returns the order in ListLong, whether hidden or not.</summary>
		public static int GetOrder(int diseaseDefNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].DiseaseDefNum==diseaseDefNum){
					return ListLong[i].ItemOrder;
				}
			}
			return 0;
		}

		///<summary>Returns the name of the disease, whether hidden or not.</summary>
		public static string GetName(int diseaseDefNum) {
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].DiseaseDefNum==diseaseDefNum) {
					return ListLong[i].DiseaseName;
				}
			}
			return "";
		}

		///<summary>Returns the diseaseDef with the specified num.</summary>
		public static DiseaseDef GetItem(int diseaseDefNum) {
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].DiseaseDefNum==diseaseDefNum) {
					return ListLong[i].Copy();
				}
			}
			return null;
		}

		///<summary>Returns the diseaseDefNum that exactly matches the specified string.  Used in import functions when you only have the name to work with.  Can return 0 if no match.  Does not match hidden diseases.</summary>
		public static int GetNumFromName(string diseaseName){
			for(int i=0;i<List.Length;i++){
				if(diseaseName==List[i].DiseaseName){
					return List[i].DiseaseDefNum;
				}
			}
			return 0;
		}
		
		
	}

		



		
	

	

	


}










