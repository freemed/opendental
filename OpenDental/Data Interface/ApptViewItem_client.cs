using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	public class ApptViewItem_client {
		public static void Refresh(){
			DataTable table=Gen.GetTable(MethodNameTable.ApptViewItem_RefreshCache);
			ApptViewItems.FillCache(table);//now, we have an arrays on both the client and the server.
		}

		public static void GetForCurView(int indexInList){
			if(indexInList==-1){
				GetForCurView(new ApptView());
			}
			else{
				GetForCurView(ApptViewC.List[indexInList]);
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
				for(int i=0;i<ApptViewItemC.List.Length;i++){
					if(ApptViewItemC.List[i].ApptViewNum==ApptViewCur.ApptViewNum){
						tempAL.Add(ApptViewItemC.List[i]);
						if(ApptViewItemC.List[i].OpNum>0){//op
							index=Operatories.GetOrder(ApptViewItemC.List[i].OpNum);
							if(index!=-1){
								ALops.Add(index);
							}
						}
						else if(ApptViewItemC.List[i].ProvNum>0){//prov
							index=Providers.GetIndex(ApptViewItemC.List[i].ProvNum);
							if(index!=-1){
								ALprov.Add(index);
							}
						}
						else{//element
							ALelements.Add(ApptViewItemC.List[i]);
						}
					}
				}
				ContrApptSheet.RowsPerIncr=ApptViewCur.RowsPerIncr;
			}
			ApptViewItems.ForCurView=new ApptViewItem[tempAL.Count];
			for(int i=0;i<tempAL.Count;i++){
				ApptViewItems.ForCurView[i]=(ApptViewItem)tempAL[i];
			}
			ApptViewItems.VisOps=new int[ALops.Count];
			for(int i=0;i<ALops.Count;i++){
				ApptViewItems.VisOps[i]=(int)ALops[i];
			}
			Array.Sort(ApptViewItems.VisOps);
			ApptViewItems.VisProvs=new int[ALprov.Count];
			for(int i=0;i<ALprov.Count;i++){
				ApptViewItems.VisProvs[i]=(int)ALprov[i];
			}
			Array.Sort(ApptViewItems.VisProvs);
			ApptViewItems.ApptRows=new ApptViewItem[ALelements.Count];
			for(int i=0;i<ALelements.Count;i++){
				ApptViewItems.ApptRows[i]=(ApptViewItem)ALelements[i];
			}
		}

		///<summary>Returns the index of the opNum within VisOps.  Returns -1 if not in visOps.</summary>
		public static int GetIndexOp(int opNum){
			for(int i=0;i<ApptViewItems.VisOps.Length;i++){
				if(Operatories.ListShort[ApptViewItems.VisOps[i]].OperatoryNum==opNum)
					return i;
			}		
			return -1;
		}
	}
}
