using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>Handles database commands related to the definition table in the db.  The related DefB class is referenced frequently from many different areas of the program.</summary>\
	public class Defs{
		///<summary></summary>
		public static void Refresh(){
			DataSet ds=null;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ds=DefB.Refresh();
				}
				else {
					DtoDefRefresh dto=new DtoDefRefresh();
					ds=RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return;
			}
			DefB.FillArrays(ds.Tables[0]);//now, we have an arrays on both the client and the server.
		}

		///<summary>Only used in FormDefinitions</summary>
		public static Def[] GetCatList(int myCat){
			string command=
				"SELECT * from definition"
				+" WHERE category = '"+myCat+"'"
				+" ORDER BY ItemOrder";
			DataSet ds=null;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ds=GeneralB.GetTable(command);
				}
				else {
					DtoGeneralGetTable dto=new DtoGeneralGetTable();
					dto.Command=command;
					ds=RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			DataTable table=ds.Tables[0];
			Def[] List=new Def[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Def();
				List[i].DefNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Category  = (DefCat)PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ItemOrder = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ItemName  = PIn.PString(table.Rows[i][3].ToString());
				List[i].ItemValue = PIn.PString(table.Rows[i][4].ToString());
				List[i].ItemColor = Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
				List[i].IsHidden  = PIn.PBool  (table.Rows[i][6].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(Def def) {
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					DefB.Update(def);
				}
				else {
					DtoDefUpdate dto=new DtoDefUpdate();
					dto.DefCur=def;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return;
			}
		}

		///<summary></summary>
		public static void Insert(Def def) {
			int defNum;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					defNum=DefB.Insert(def);
				}
				else {
					DtoDefInsert dto=new DtoDefInsert();
					dto.DefCur=def;
					defNum=RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return;
			}
			def.DefNum=defNum;			
		}

		///<summary></summary>
		public static void HideDef(Def def){
			def.IsHidden=true;
			Update(def);
		}

		///<summary>Returns the new selected.</summary>
		public static int MoveUp(bool isSelected,int selected,Def[] list){
			if(isSelected==false){
				MessageBox.Show(Lan.g("Defs","Please select an item first."));
				return selected;
			}
			if(selected==0){
				return selected;
			}
			SetOrder(selected-1,list[selected].ItemOrder,list);
			SetOrder(selected,list[selected].ItemOrder-1,list);
			selected-=1;
			return selected;
		}

		///<summary></summary>
		public static int MoveDown(bool isSelected,int selected,Def[] list){
			if(isSelected==false){
				MessageBox.Show(Lan.g("Defs","Please select an item first."));
				return selected;
			}
			if(selected==list.Length-1){
				return selected;
			}
			SetOrder(selected+1,list[selected].ItemOrder,list);
			SetOrder(selected,list[selected].ItemOrder+1,list);
			selected+=1;
			return selected;
		}

		///<summary></summary>
		private static void SetOrder(int mySelNum, int myItemOrder,Def[] list){
			Def def=list[mySelNum];
			def.ItemOrder=myItemOrder;
			//Cur=temp;
			Update(def);
		}

		

	}

	

	

}









