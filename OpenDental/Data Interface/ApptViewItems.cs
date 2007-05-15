using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>Handles database commands related to the apptviewitem table in the database.</summary>
	public class ApptViewItems{
		//<summary>Current.  A single row of data.</summary>
		//public static ApptViewItem Cur;
		///<summary>A list of all ApptViewItems.</summary>
		public static ApptViewItem[] List;
		///<summary>A list of the ApptViewItems for the current view.</summary>
		public static ApptViewItem[] ForCurView;
		//these two are subsets of provs and ops. You can't include hidden prov or op in this list.
		///<summary>Visible providers in appt module.  List of indices to providers.List(short).  Also see VisOps.  This is a subset of the available provs.  You can't include a hidden prov in this list.</summary>
		public static int[] VisProvs;
		///<summary>Visible ops in appt module.  List of indices to Operatories.ListShort[ops].  Also see VisProvs.  This is a subset of the available ops.  You can't include a hidden op in this list.</summary>
		public static int[] VisOps;
		///<summary>Subset of ForCurView. Just items for rowElements. If no view is selected, then the elements are filled with default info.</summary>
		public static ApptViewItem[] ApptRows;

		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * from apptviewitem ORDER BY ElementOrder";
			DataTable table=General.GetTable(command);
			List=new ApptViewItem[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new ApptViewItem();
				List[i].ApptViewItemNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ApptViewNum     = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].OpNum           = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ProvNum         = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].ElementDesc     = PIn.PString(table.Rows[i][4].ToString());
				List[i].ElementOrder    = PIn.PInt   (table.Rows[i][5].ToString());
				List[i].ElementColor    = Color.FromArgb(PIn.PInt(table.Rows[i][6].ToString()));
			}
		}

		///<summary></summary>
		public static void Insert(ApptViewItem Cur){
			string command= "INSERT INTO apptviewitem (ApptViewNum,OpNum,ProvNum,ElementDesc,"
				+"ElementOrder,ElementColor) "
				+"VALUES ("
				+"'"+POut.PInt   (Cur.ApptViewNum)+"', "
				+"'"+POut.PInt   (Cur.OpNum)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PString(Cur.ElementDesc)+"', "
				+"'"+POut.PInt   (Cur.ElementOrder)+"', "
				+"'"+POut.PInt   (Cur.ElementColor.ToArgb())+"')";
			//MessageBox.Show(string command);
			General.NonQ(command);
			//Cur.ApptViewNum=InsertID;
		}

		///<summary></summary>
		public static void Update(ApptViewItem Cur){
			string command= "UPDATE apptviewitem SET "
				+"ApptViewNum='"    +POut.PInt   (Cur.ApptViewNum)+"'"
				+",OpNum = '"       +POut.PInt   (Cur.OpNum)+"'"
				+",ProvNum = '"     +POut.PInt   (Cur.ProvNum)+"'"
				+",ElementDesc = '" +POut.PString(Cur.ElementDesc)+"'"
				+",ElementOrder = '"+POut.PInt   (Cur.ElementOrder)+"'"
				+",ElementColor = '"+POut.PInt   (Cur.ElementColor.ToArgb())+"'"
				+" WHERE ApptViewItemNum = '"+POut.PInt(Cur.ApptViewItemNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ApptViewItem Cur){
			string command="DELETE from apptviewitem WHERE ApptViewItemNum = '"
				+POut.PInt(Cur.ApptViewItemNum)+"'";
			General.NonQ(command);
		}

		///<summary>Deletes all apptviewitems for the current apptView.</summary>
		public static void DeleteAllForView(ApptView view){
			string c="DELETE from apptviewitem WHERE ApptViewNum = '"
				+POut.PInt(view.ApptViewNum)+"'";
			General.NonQ(c);
		}

		public static void GetForCurView(int indexInList){
			if(indexInList==-1){
				GetForCurView(new ApptView());
			}
			else{
				GetForCurView(ApptViews.List[indexInList]);
			}
		}

		///<summary>Gets (list)ForCurView, VisOps, VisProvs, and ApptRows.  Also sets TwoRows. Works even if supply -1 to indicate no apptview is selected.</summary>
		public static void GetForCurView(ApptView ApptViewCur){
			ArrayList tempAL=new ArrayList();
			ArrayList ALprov=new ArrayList();
			ArrayList ALops=new ArrayList();
			ArrayList ALelements=new ArrayList();
			if(ApptViewCur.ApptViewNum==0){
				//MessageBox.Show("apptcategorynum:"+ApptCategories.Cur.ApptCategoryNum.ToString());
				//make visible ops exactly the same as the short ops list (all except hidden)
				for(int i=0;i<Operatories.ListShort.Length;i++){
					ALops.Add(i);
				}
				//make visible provs exactly the same as the prov list (all except hidden)
				for(int i=0;i<Providers.List.Length;i++){
					ALprov.Add(i);
				}
				//Hard coded elements showing
				ALelements.Add(new ApptViewItem("PatientName",0,Color.Black));
				ALelements.Add(new ApptViewItem("Lab",1,Color.DarkRed));
				ALelements.Add(new ApptViewItem("Procs",2,Color.Black));
				ALelements.Add(new ApptViewItem("Note",3,Color.Black));
				ContrApptSheet.RowsPerIncr=1;
			}
			else{
				int index;
				for(int i=0;i<List.Length;i++){
					if(List[i].ApptViewNum==ApptViewCur.ApptViewNum){
						tempAL.Add(List[i]);
						if(List[i].OpNum>0){//op
							index=Operatories.GetOrder(List[i].OpNum);
							if(index!=-1){
								ALops.Add(index);
							}
						}
						else if(List[i].ProvNum>0){//prov
							index=Providers.GetIndex(List[i].ProvNum);
							if(index!=-1){
								ALprov.Add(index);
							}
						}
						else{//element
							ALelements.Add(List[i]);
						}
					}
				}
				ContrApptSheet.RowsPerIncr=ApptViewCur.RowsPerIncr;
			}
			ForCurView=new ApptViewItem[tempAL.Count];
			for(int i=0;i<tempAL.Count;i++){
				ForCurView[i]=(ApptViewItem)tempAL[i];
			}
			VisOps=new int[ALops.Count];
			for(int i=0;i<ALops.Count;i++){
				VisOps[i]=(int)ALops[i];
			}
			Array.Sort(VisOps);
			VisProvs=new int[ALprov.Count];
			for(int i=0;i<ALprov.Count;i++){
				VisProvs[i]=(int)ALprov[i];
			}
			Array.Sort(VisProvs);
			ApptRows=new ApptViewItem[ALelements.Count];
			for(int i=0;i<ALelements.Count;i++){
				ApptRows[i]=(ApptViewItem)ALelements[i];
			}
		}

		///<summary>Returns the index of the provNum within VisProvs.</summary>
		public static int GetIndexProv(int provNum){
			for(int i=0;i<VisProvs.Length;i++){
				if(Providers.List[VisProvs[i]].ProvNum==provNum)
					return i;
			}		
			return -1;
		}

		///<summary>Returns the index of the opNum within VisOps.  Returns -1 if not in visOps.</summary>
		public static int GetIndexOp(int opNum){
			for(int i=0;i<VisOps.Length;i++){
				if(Operatories.ListShort[VisOps[i]].OperatoryNum==opNum)
					return i;
			}		
			return -1;
		}

		///<summary>Only used in ApptViewItem setup. Must have run GetForCurView first.</summary>
		public static bool OpIsInView(int opNum){
			for(int i=0;i<ForCurView.Length;i++){
				if(ForCurView[i].OpNum==opNum)
					return true;
			}
			return false;
		}

		///<summary>Only used in ApptViewItem setup. Must have run GetForCurView first.</summary>
		public static bool ProvIsInView(int provNum){
			for(int i=0;i<ForCurView.Length;i++){
				if(ForCurView[i].ProvNum==provNum)
					return true;
			}
			return false;
		}

		public static int[] GetOpsForView(int apptViewNum){
			ArrayList AL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ApptViewNum==apptViewNum && List[i].OpNum!=0){
					AL.Add(List[i].OpNum);
				}
			}
			//int[] retVal=new int[AL.Count]();
			return (int[])AL.ToArray(typeof(int));
		}



	}

	


}









