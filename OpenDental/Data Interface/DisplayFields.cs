using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public class DisplayFields {
		///<summary>A list of all DisplayFields</summary>
		public static List<DisplayField> Listt;

		public static void Refresh() {
			string command = "SELECT * FROM displayfield ORDER BY ItemOrder";
			DataTable table = General.GetTable(command);
			Listt=new List<DisplayField>();
			DisplayField field;
			for(int i=0;i<table.Rows.Count;i++){
				field = new DisplayField();
				field.DisplayFieldNum = PIn.PInt   (table.Rows[i][0].ToString());
				field.InternalName    = PIn.PString(table.Rows[i][1].ToString());
				field.ItemOrder       = PIn.PInt   (table.Rows[i][2].ToString());
				field.Description     = PIn.PString(table.Rows[i][3].ToString());
				field.ColumnWidth     = PIn.PInt   (table.Rows[i][4].ToString());
				Listt.Add(field);
			}
		}

		///<summary></summary>
		public static void Insert(DisplayField field) {		
			string command = "INSERT INTO displayfield (InternalName,ItemOrder,Description,ColumnWidth) VALUES ("			
				+"'"+POut.PString(field.InternalName)+"'," 
				+"'"+POut.PInt   (field.ItemOrder)+"',"
				+"'"+POut.PString(field.Description)+"'," 
				+"'"+POut.PInt   (field.ColumnWidth)+"')";
			General.NonQ(command);
		}

		/*
		///<summary></summary>
		public static void Update(DisplayField field) {			
			string command="UPDATE displayfield SET "
			+"DisplayFieldName = '"+POut.PString(DisplayField.DisplayFieldName)+"', "
			+"ControlsToInc = '"+POut.PString(DisplayField.ControlsToInc)+"' "
			+"WHERE DisplayFieldNum = '"+POut.PInt(DisplayField.DisplayFieldNum)+"'";
			General.NonQ(command);
		}
		*/

		///<Summary>There's only one category for now.  So this doesn't really do much.</Summary>
		public static List<DisplayField> GetForCategory(){
			if(DisplayFields.Listt.Count==0) {//default
				return DisplayFields.GetDefaultList();
			}
			return DisplayFields.Listt;//later, filter by category
		}

		public static List<DisplayField> GetDefaultList(){
			List<DisplayField> list=new List<DisplayField>();
			list.Add(new DisplayField("Date",67));
			//list.Add(new DisplayField("Time",40));
			list.Add(new DisplayField("Th",27));
			list.Add(new DisplayField("Surf",40));
			list.Add(new DisplayField("Dx",28));
			list.Add(new DisplayField("Description",218));
			list.Add(new DisplayField("Stat",25));
			list.Add(new DisplayField("Prov",42));
			list.Add(new DisplayField("Amount",48));
			list.Add(new DisplayField("ADA Code",62));
			list.Add(new DisplayField("User",62));
			list.Add(new DisplayField("Signed",55));
			return list;
		}

		public static List<DisplayField> GetAllAvailableList(){
			List<DisplayField> list=new List<DisplayField>();
			list.Add(new DisplayField("Date",67));
			list.Add(new DisplayField("Time",40));
			list.Add(new DisplayField("Th",27));
			list.Add(new DisplayField("Surf",40));
			list.Add(new DisplayField("Dx",28));
			list.Add(new DisplayField("Description",218));
			list.Add(new DisplayField("Stat",25));
			list.Add(new DisplayField("Prov",42));
			list.Add(new DisplayField("Amount",48));
			list.Add(new DisplayField("ADA Code",62));
			list.Add(new DisplayField("User",62));
			list.Add(new DisplayField("Signed",55));
			return list;
		}

		public static void SaveListForCategory(List<DisplayField> ListShowing){
			bool isDefault=true;
			List<DisplayField> defaultList=GetDefaultList();
			if(ListShowing.Count!=defaultList.Count){
				isDefault=false;
			}
			else{
				for(int i=0;i<ListShowing.Count;i++){
					if(ListShowing[i].Description!=""){
						isDefault=false;
						break;
					}
					if(ListShowing[i].InternalName!=defaultList[i].InternalName){
						isDefault=false;
						break;
					}
					if(ListShowing[i].ColumnWidth!=defaultList[i].ColumnWidth) {
						isDefault=false;
						break;
					}
				}
			}
			string command="DELETE FROM displayfield";
			General.NonQ(command);
			if(isDefault){
				return;
			}
			for(int i=0;i<ListShowing.Count;i++){
				ListShowing[i].ItemOrder=i;
				Insert(ListShowing[i]);
			}
		}

		

	}
}
