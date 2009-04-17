using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>Handles database commands related to the definition table in the db.  The related DefB class is referenced frequently from many different areas of the program.</summary>
	public class DefL{

		///<summary>Returns the new selected.</summary>
		public static int MoveUp(bool isSelected,int selected,Def[] list){
			if(isSelected==false){
				MessageBox.Show(Lan.g("Defs","Please select an item first."));
				return selected;
			}
			if(selected==0){
				return selected;
			}
			Defs.SetOrder(selected-1,list[selected].ItemOrder,list);
			Defs.SetOrder(selected,list[selected].ItemOrder-1,list);
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
			Defs.SetOrder(selected+1,list[selected].ItemOrder,list);
			Defs.SetOrder(selected,list[selected].ItemOrder+1,list);
			selected+=1;
			return selected;
		}		

	}
}