using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class QuickPasteCats {
		///<summary></summary>
		public static QuickPasteCat[] List;

		///<summary></summary>
		public static void Refresh() {
			string command=
				"SELECT * from quickpastecat "
				+"ORDER BY ItemOrder";
			DataTable table=General.GetTable(command);
			List=new QuickPasteCat[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new QuickPasteCat();
				List[i].QuickPasteCatNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].Description     = PIn.PString(table.Rows[i][1].ToString());
				List[i].ItemOrder       = PIn.PInt(table.Rows[i][2].ToString());
				List[i].DefaultForTypes = PIn.PString(table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static void Insert(QuickPasteCat cat){
			if(PrefB.RandomKeys){
				cat.QuickPasteCatNum=MiscData.GetKey("quickpastecat","QuickPasteCatNum");
			}
			string command= "INSERT INTO quickpastecat (";
			if(PrefB.RandomKeys){
				command+="QuickPasteCatNum,";
			}
			command+="Description,ItemOrder,DefaultForTypes) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(cat.QuickPasteCatNum)+"', ";
			}
			command+=
				 "'"+POut.PString(cat.Description)+"', "
				+"'"+POut.PInt   (cat.ItemOrder)+"', "
				+"'"+POut.PString(cat.DefaultForTypes)+"')";
			//MessageBox.Show(string command);
 			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				cat.QuickPasteCatNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(QuickPasteCat cat){
			string command="UPDATE quickpastecat SET "
				+"Description='"       +POut.PString(cat.Description)+"'"
				+",ItemOrder = '"      +POut.PInt   (cat.ItemOrder)+"'"
				+",DefaultForTypes = '"+POut.PString(cat.DefaultForTypes)+"'"
				+" WHERE QuickPasteCatNum = '"+POut.PInt (cat.QuickPasteCatNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(QuickPasteCat cat){
			string command="DELETE from quickpastecat WHERE QuickPasteCatNum = '"
				+POut.PInt(cat.QuickPasteCatNum)+"'";
 			General.NonQ(command);
		}


		///<summary>Called from FormQuickPaste and from QuickPasteNotes.Substitute(). Returns the index of the default category for the specified type. If user has entered more than one, only one is returned.</summary>
		public static int GetDefaultType(QuickPasteType type){
			if(List.Length==0){
				return -1;
			}
			if(type==QuickPasteType.None){
				return 0;//default to first line
			}
			string[] types;
			for(int i=0;i<List.Length;i++){
				if(List[i].DefaultForTypes==""){
					types=new string[0];
				}
				else{
					types=List[i].DefaultForTypes.Split(',');
				}
				for(int j=0;j<types.Length;j++){
					if(((int)type).ToString()==types[j]){
						return i;
					}
				}
			}
			return 0;
		}

		

		


		


	}

	


}









