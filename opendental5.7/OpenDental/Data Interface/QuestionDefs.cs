using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	///<summary></summary>
	public class QuestionDefs {

		///<summary>Gets a list of all QuestionDefs.</summary>
		public static QuestionDef[] Refresh() {
			string command="SELECT * FROM questiondef ORDER BY ItemOrder";
			DataTable table=General.GetTable(command);
			QuestionDef[] List=new QuestionDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new QuestionDef();
				List[i].QuestionDefNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].Description   = PIn.PString(table.Rows[i][1].ToString());
				List[i].ItemOrder     = PIn.PInt(table.Rows[i][2].ToString());
				List[i].QuestType     = (QuestionType)PIn.PInt(table.Rows[i][3].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(QuestionDef def) {
			string command="UPDATE questiondef SET " 
				+"QuestionDefNum = '"+POut.PInt   (def.QuestionDefNum)+"'"
				+",Description = '"  +POut.PString(def.Description)+"'"
				+",ItemOrder = '"    +POut.PInt   (def.ItemOrder)+"'"
				+",QuestType = '"    +POut.PInt   ((int)def.QuestType)+"'"
				+" WHERE QuestionDefNum  ='"+POut.PInt   (def.QuestionDefNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(QuestionDef def) {
			string command="INSERT INTO questiondef (Description,ItemOrder,QuestType) VALUES("
				+"'"+POut.PString(def.Description)+"', "
				+"'"+POut.PInt   (def.ItemOrder)+"', "
				+"'"+POut.PInt   ((int)def.QuestType)+"')";
			def.QuestionDefNum=General.NonQ(command,true);
		}

		///<summary>Ok to delete whenever, because no patients are tied to this table by any dependencies.</summary>
		public static void Delete(QuestionDef def) {
			string command="DELETE FROM questiondef WHERE QuestionDefNum ="+POut.PInt(def.QuestionDefNum);
			General.NonQ(command);
		}



		///<summary>Moves the selected item up in the list.</summary>
		public static void MoveUp(int selected,QuestionDef[] List){
			if(selected<0) {
				throw new ApplicationException(Lan.g("QuestionDefs","Please select an item first."));
			}
			if(selected==0) {//already at top
				return;
			}
			if(selected>List.Length-1){
				throw new ApplicationException(Lan.g("QuestionDefs","Invalid selection."));
			}
			SetOrder(selected-1,List[selected].ItemOrder,List);
			SetOrder(selected,List[selected].ItemOrder-1,List);
		}

		///<summary></summary>
		public static void MoveDown(int selected,QuestionDef[] List) {
			if(selected<0) {
				throw new ApplicationException(Lan.g("QuestionDefs","Please select an item first."));
			}
			if(selected==List.Length-1){//already at bottom
				return;
			}
			if(selected>List.Length-1) {
				throw new ApplicationException(Lan.g("QuestionDefs","Invalid selection."));
			}
			SetOrder(selected+1,List[selected].ItemOrder,List);
			SetOrder(selected,List[selected].ItemOrder+1,List);
			//selected+=1;
		}

		///<summary>Used by MoveUp and MoveDown.</summary>
		private static void SetOrder(int mySelNum,int myItemOrder,QuestionDef[] List) {
			QuestionDef temp=List[mySelNum];
			temp.ItemOrder=myItemOrder;
			QuestionDefs.Update(temp);
		}

		
		
	}

		



		
	

	

	


}










