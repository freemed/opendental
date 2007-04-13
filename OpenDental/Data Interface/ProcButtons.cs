using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ProcButtons {
		///<summary></summary>
		public static ProcButton[] List;

		///<summary></summary>
		public static void Refresh() {
			string command="SELECT * FROM procbutton ORDER BY ItemOrder";
			DataTable table=General.GetTable(command);
			List=new ProcButton[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new ProcButton();
				List[i].ProcButtonNum=PIn.PInt(table.Rows[i][0].ToString());
				List[i].Description  =PIn.PString(table.Rows[i][1].ToString());
				List[i].ItemOrder    =PIn.PInt(table.Rows[i][2].ToString());
				List[i].Category     =PIn.PInt(table.Rows[i][3].ToString());
				List[i].ButtonImage  =PIn.PBitmap(table.Rows[i][4].ToString());
			}
		}

		///<summary>must have already checked ADACode for nonduplicate.</summary>
		public static void Insert(ProcButton but) {
			string command= "INSERT INTO procbutton (Description,ItemOrder,Category,ButtonImage) VALUES("
				+"'"+POut.PString(but.Description)+"', "
				+"'"+POut.PInt   (but.ItemOrder)+"', "
				+"'"+POut.PInt   (but.Category)+"', "
				+"'"+POut.PBitmap(but.ButtonImage)+"')";
			but.ProcButtonNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(ProcButton but) {
			string command="UPDATE procbutton SET " 
				+ "Description = '" +POut.PString(but.Description)+"'"
				+ ",ItemOrder = '"  +POut.PInt   (but.ItemOrder)+"'"
				+ ",Category = '"   +POut.PInt   (but.Category)+"'"
				+ ",ButtonImage = '"+POut.PBitmap(but.ButtonImage)+"'"
				+" WHERE ProcButtonNum = '"+POut.PInt(but.ProcButtonNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ProcButton but) {
			string command="DELETE FROM procbuttonitem WHERE ProcButtonNum = '"
				+POut.PInt(but.ProcButtonNum)+"'";
			General.NonQ(command);
			command="DELETE FROM procbutton WHERE ProcButtonNum = '"
				+POut.PInt(but.ProcButtonNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static ProcButton[] GetForCat(int selectedCat){
			ArrayList AL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].Category==selectedCat){
					AL.Add(List[i]);
				}
			}
			ProcButton[] retVal=new ProcButton[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		/*//<summary>Used when a button is moved out of a category.  This leaves a 'hole' in the order, so we need to clean up the orders.  Remember to run Refresh before this.</summary>
		public static void ResetOrder(int cat){

		}*/

		
	}

	

	


}










