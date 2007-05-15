using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>Handles database commands related to the apptview table in the database.</summary>
	public class ApptViews{
		///<summary>A list of all apptviews, in order.</summary>
		public static ApptView[] List;

		///<summary></summary>
		public static void Refresh(){
			string c="SELECT * from apptview ORDER BY itemorder";
			DataTable table=General.GetTable(c);
			List=new ApptView[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new ApptView();
				List[i].ApptViewNum = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description = PIn.PString(table.Rows[i][1].ToString());
				List[i].ItemOrder   = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].RowsPerIncr = PIn.PInt   (table.Rows[i][3].ToString());	
			}
		}

		///<summary></summary>
		public static void Insert(ApptView Cur){
			string command = "INSERT INTO apptview (Description,ItemOrder,RowsPerIncr) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"', "
				+"'"+POut.PInt   (Cur.RowsPerIncr)+"')";
			//MessageBox.Show(string command);
			Cur.ApptViewNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(ApptView Cur){
			string command= "UPDATE apptview SET "
				+"Description='"   +POut.PString(Cur.Description)+"'"
				+",ItemOrder = '"  +POut.PInt   (Cur.ItemOrder)+"'"
				+",RowsPerIncr = '"+POut.PInt   (Cur.RowsPerIncr)+"'"
				+" WHERE ApptViewNum = '"+POut.PInt(Cur.ApptViewNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ApptView Cur){
			string command="DELETE from apptview WHERE ApptViewNum = '"
				+POut.PInt(Cur.ApptViewNum)+"'";
			General.NonQ(command);
		}

		public static ApptView GetView(int apptViewNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].ApptViewNum==apptViewNum){
					return List[i];
				}
			}
			return null;//should never happen
		}

		/*
		/// <summary>Used in appt module.  Can be -1 if no category selected </summary>
		public static void SetCur(int index){
			if(index==-1){
				Cur=new ApptView();
			}
			else{
				Cur=List[index];
			}
		}*/


	}

	


}









