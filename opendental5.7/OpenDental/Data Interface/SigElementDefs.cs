using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	///<summary></summary>
	public class SigElementDefs {
		///<summary>A list of all SigElementDefs.</summary>
		public static SigElementDef[] List;

		///<summary>Gets a list of all SigElementDefs when program first opens.</summary>
		public static void Refresh() {
			string command="SELECT * FROM sigelementdef ORDER BY ItemOrder";
			DataTable table=General.GetTable(command);
			List=new SigElementDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new SigElementDef();
				List[i].SigElementDefNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].LightRow        = PIn.PInt(table.Rows[i][1].ToString());
				List[i].LightColor      = Color.FromArgb(PIn.PInt(table.Rows[i][2].ToString()));
				List[i].SigElementType  = (SignalElementType)PIn.PInt(table.Rows[i][3].ToString());
				List[i].SigText         = PIn.PString(table.Rows[i][4].ToString());
				List[i].Sound           = PIn.PString(table.Rows[i][5].ToString());
				List[i].ItemOrder       = PIn.PInt(table.Rows[i][6].ToString());
			}
		}
	
		///<summary></summary>
		public static void Update(SigElementDef def) {
			string command="UPDATE sigelementdef SET " 
				+"LightRow = '"       +POut.PInt   (def.LightRow)+"'"
				+",LightColor = '"    +POut.PInt   (def.LightColor.ToArgb())+"'"
				+",SigElementType = '"+POut.PInt   ((int)def.SigElementType)+"'"
				+",SigText = '"       +POut.PString(def.SigText)+"'"
				+",Sound = '"         +POut.PString(def.Sound)+"'"
				+",ItemOrder = '"     +POut.PInt   (def.ItemOrder)+"'"
				+" WHERE SigElementDefNum  ='"+POut.PInt   (def.SigElementDefNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(SigElementDef def) {
			string command="INSERT INTO sigelementdef (LightRow,LightColor,SigElementType,SigText,Sound,"
				+"ItemOrder) VALUES("
				+"'"+POut.PInt   (def.LightRow)+"', "
				+"'"+POut.PInt   (def.LightColor.ToArgb())+"', "
				+"'"+POut.PInt   ((int)def.SigElementType)+"', "
				+"'"+POut.PString(def.SigText)+"', "
				+"'"+POut.PString(def.Sound)+"', "
				+"'"+POut.PInt   (def.ItemOrder)+"')";
			def.SigElementDefNum=General.NonQ(command,true);
		}

		///<summary>No need to surround with try/catch, because all deletions are allowed.  This routine, deletes references in the SigButDefElement table.  References in the SigElement table are left hanging.  The user interface needs to be able to handle missing elementdefs.</summary>
		public static void Delete(SigElementDef def) {
			string command="DELETE FROM sigbutdefelement WHERE SigElementDefNum="+POut.PInt(def.SigElementDefNum);
			General.NonQ(command);
			command="DELETE FROM sigelementdef WHERE SigElementDefNum ="+POut.PInt(def.SigElementDefNum);
			General.NonQ(command);
		}

		///<summary></summary>
		public static SigElementDef[] GetSubList(SignalElementType sigElementType){
			ArrayList AL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(sigElementType==List[i].SigElementType){
					AL.Add(List[i]);
				}
			}
			SigElementDef[] retVal=new SigElementDef[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary>Moves the selected item up in the supplied sub list.</summary>
		public static void MoveUp(int selected,SigElementDef[] subList){
			if(selected<0) {
				throw new ApplicationException(Lan.g("SigElementDefs","Please select an item first."));
			}
			if(selected==0) {//already at top
				return;
			}
			if(selected>subList.Length-1){
				throw new ApplicationException(Lan.g("SigElementDefs","Invalid selection."));
			}
			SetOrder(selected-1,subList[selected].ItemOrder,subList);
			SetOrder(selected,subList[selected].ItemOrder-1,subList);
			//Selected-=1;
		}

		///<summary></summary>
		public static void MoveDown(int selected,SigElementDef[] subList) {
			if(selected<0) {
				throw new ApplicationException(Lan.g("SigElementDefs","Please select an item first."));
			}
			if(selected==subList.Length-1){//already at bottom
				return;
			}
			if(selected>subList.Length-1) {
				throw new ApplicationException(Lan.g("SigElementDefs","Invalid selection."));
			}
			SetOrder(selected+1,subList[selected].ItemOrder,subList);
			SetOrder(selected,subList[selected].ItemOrder+1,subList);
			//selected+=1;
		}

		///<summary>Used by MoveUp and MoveDown.</summary>
		private static void SetOrder(int mySelNum,int myItemOrder,SigElementDef[] subList) {
			SigElementDef temp=subList[mySelNum];
			temp.ItemOrder=myItemOrder;
			Update(temp);
		}

		///<summary>Returns the SigElementDef with the specified num.</summary>
		public static SigElementDef GetElement(int SigElementDefNum) {
			for(int i=0;i<List.Length;i++) {
				if(List[i].SigElementDefNum==SigElementDefNum) {
					return List[i].Copy();
				}
			}
			return null;
		}
		
		
	}

		



		
	

	

	


}










