using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>Handles database commands related to the definition table in the db.  The related DefB class is referenced frequently from many different areas of the program.</summary>
	public class Defs{
		///<summary></summary>
		public static void RefreshClient(){
			DataTable table=General.GetDS(MethodName.Definition_Refresh).Tables[0];
			DefD.FillArrays(table);//now, we have an arrays on both the client and the server.
		}

		///<summary></summary>
		public static void HideDef(Def def){
			def.IsHidden=true;
			DefD.Update(def);
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
			DefD.Update(def);
		}

		

		

	}

	

	

}









