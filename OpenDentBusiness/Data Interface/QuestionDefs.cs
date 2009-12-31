using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class QuestionDefs {

		///<summary>Gets a list of all QuestionDefs.</summary>
		public static QuestionDef[] Refresh() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<QuestionDef[]>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM questiondef ORDER BY ItemOrder";
			DataTable table=Db.GetTable(command);
			QuestionDef[] List=new QuestionDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new QuestionDef();
				List[i].QuestionDefNum= PIn.Long(table.Rows[i][0].ToString());
				List[i].Description   = PIn.String(table.Rows[i][1].ToString());
				List[i].ItemOrder     = PIn.Int(table.Rows[i][2].ToString());
				List[i].QuestType     = (QuestionType)PIn.Long(table.Rows[i][3].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(QuestionDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			string command="UPDATE questiondef SET " 
				+"QuestionDefNum = '"+POut.Long   (def.QuestionDefNum)+"'"
				+",Description = '"  +POut.String(def.Description)+"'"
				+",ItemOrder = '"    +POut.Long   (def.ItemOrder)+"'"
				+",QuestType = '"    +POut.Long   ((int)def.QuestType)+"'"
				+" WHERE QuestionDefNum  ='"+POut.Long   (def.QuestionDefNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(QuestionDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				def.QuestionDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),def);
				return def.QuestionDefNum;
			}
			if(PrefC.RandomKeys) {
				def.QuestionDefNum=ReplicationServers.GetKey("questiondef","QuestionDefNum");
			}
			string command="INSERT INTO questiondef (";
			if(PrefC.RandomKeys) {
				command+="QuestionDefNum,";
			}
			command+="Description,ItemOrder,QuestType) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(def.QuestionDefNum)+", ";
			}
			command+=
				 "'"+POut.String(def.Description)+"', "
				+"'"+POut.Long   (def.ItemOrder)+"', "
				+"'"+POut.Long   ((int)def.QuestType)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				def.QuestionDefNum=Db.NonQ(command,true);
			}
			return def.QuestionDefNum;
		}

		///<summary>Ok to delete whenever, because no patients are tied to this table by any dependencies.</summary>
		public static void Delete(QuestionDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			string command="DELETE FROM questiondef WHERE QuestionDefNum ="+POut.Long(def.QuestionDefNum);
			Db.NonQ(command);
		}



		///<summary>Moves the selected item up in the list.</summary>
		public static void MoveUp(int selected,QuestionDef[] List){
			//No need to check RemotingRole; no call to db.
			if(selected<0) {
				throw new ApplicationException(Lans.g("QuestionDefs","Please select an item first."));
			}
			if(selected==0) {//already at top
				return;
			}
			if(selected>List.Length-1){
				throw new ApplicationException(Lans.g("QuestionDefs","Invalid selection."));
			}
			SetOrder(selected-1,List[selected].ItemOrder,List);
			SetOrder(selected,List[selected].ItemOrder-1,List);
		}

		///<summary></summary>
		public static void MoveDown(int selected,QuestionDef[] List) {
			//No need to check RemotingRole; no call to db.
			if(selected<0) {
				throw new ApplicationException(Lans.g("QuestionDefs","Please select an item first."));
			}
			if(selected==List.Length-1){//already at bottom
				return;
			}
			if(selected>List.Length-1) {
				throw new ApplicationException(Lans.g("QuestionDefs","Invalid selection."));
			}
			SetOrder(selected+1,List[selected].ItemOrder,List);
			SetOrder(selected,List[selected].ItemOrder+1,List);
			//selected+=1;
		}

		///<summary>Used by MoveUp and MoveDown.</summary>
		private static void SetOrder(int mySelNum,int myItemOrder,QuestionDef[] List) {
			//No need to check RemotingRole; no call to db.
			QuestionDef temp=List[mySelNum];
			temp.ItemOrder=myItemOrder;
			QuestionDefs.Update(temp);
		}

		
		
	}

		



		
	

	

	


}










