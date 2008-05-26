using System;
using System.Collections;
using System.Data;
using System.Drawing;
using OpenDentBusiness;

namespace OpenDentBusiness{
	///<summary>Handles database commands related to the apptviewitem table in the database.</summary>
	public class ApptViewItems{
		///<summary>A list of the ApptViewItems for the current view.</summary>
		public static ApptViewItem[] ForCurView;
		//these two are subsets of provs and ops. You can't include hidden prov or op in this list.
		///<summary>Visible providers in appt module.  List of indices to ProviderC.List(short).  Also see VisOps.  This is a subset of the available provs.  You can't include a hidden prov in this list.</summary>
		public static int[] VisProvs;
		///<summary>Visible ops in appt module.  List of indices to Operatories.ListShort[ops].  Also see VisProvs.  This is a subset of the available ops.  You can't include a hidden op in this list.</summary>
		public static int[] VisOps;
		///<summary>Subset of ForCurView. Just items for rowElements. If no view is selected, then the elements are filled with default info.</summary>
		public static ApptViewItem[] ApptRows;

		///<summary></summary>
		public static DataTable RefreshCache(){
			string command="SELECT * from apptviewitem ORDER BY ElementOrder";
			DataTable table=General.GetTable(command);
			table.TableName="ApptViewItem";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			ApptViewItemC.List=new ApptViewItem[table.Rows.Count];
			for(int i=0;i<ApptViewItemC.List.Length;i++){
				ApptViewItemC.List[i]=new ApptViewItem();
				ApptViewItemC.List[i].ApptViewItemNum = PIn.PInt   (table.Rows[i][0].ToString());
				ApptViewItemC.List[i].ApptViewNum     = PIn.PInt   (table.Rows[i][1].ToString());
				ApptViewItemC.List[i].OpNum           = PIn.PInt   (table.Rows[i][2].ToString());
				ApptViewItemC.List[i].ProvNum         = PIn.PInt   (table.Rows[i][3].ToString());
				ApptViewItemC.List[i].ElementDesc     = PIn.PString(table.Rows[i][4].ToString());
				ApptViewItemC.List[i].ElementOrder    = PIn.PInt   (table.Rows[i][5].ToString());
				ApptViewItemC.List[i].ElementColor    = Color.FromArgb(PIn.PInt(table.Rows[i][6].ToString()));
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

		///<summary>Returns the index of the provNum within VisProvs.</summary>
		public static int GetIndexProv(int provNum){
			for(int i=0;i<VisProvs.Length;i++){
				if(ProviderC.List[VisProvs[i]].ProvNum==provNum)
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
			for(int i=0;i<ApptViewItemC.List.Length;i++){
				if(ApptViewItemC.List[i].ApptViewNum==apptViewNum && ApptViewItemC.List[i].OpNum!=0){
					AL.Add(ApptViewItemC.List[i].OpNum);
				}
			}
			//int[] retVal=new int[AL.Count]();
			return (int[])AL.ToArray(typeof(int));
		}



	}

	


}









