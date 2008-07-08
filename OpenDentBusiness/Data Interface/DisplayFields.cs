using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDentBusiness {
	public class DisplayFields {

		public static DataTable Refresh() {
			string command = "SELECT * FROM displayfield ORDER BY ItemOrder";
			DataTable table = General.GetTable(command);
			table.TableName="DisplayField";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			DisplayFieldC.Listt=new List<DisplayField>();
			DisplayField field;
			for(int i=0;i<table.Rows.Count;i++){
				field = new DisplayField();
				field.DisplayFieldNum = PIn.PInt   (table.Rows[i][0].ToString());
				field.InternalName    = PIn.PString(table.Rows[i][1].ToString());
				field.ItemOrder       = PIn.PInt   (table.Rows[i][2].ToString());
				field.Description     = PIn.PString(table.Rows[i][3].ToString());
				field.ColumnWidth     = PIn.PInt   (table.Rows[i][4].ToString());
				field.Category        = (DisplayFieldCategory)PIn.PInt(table.Rows[i][5].ToString());
				DisplayFieldC.Listt.Add(field);
			}
		}

		///<summary></summary>
		public static void Insert(DisplayField field) {		
			string command = "INSERT INTO displayfield (InternalName,ItemOrder,Description,ColumnWidth,Category) VALUES ("			
				+"'"+POut.PString(field.InternalName)+"'," 
				+"'"+POut.PInt   (field.ItemOrder)+"',"
				+"'"+POut.PString(field.Description)+"'," 
				+"'"+POut.PInt   (field.ColumnWidth)+"', "
				+"'"+POut.PInt   ((int)field.Category)+"')";
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

		///<Summary>Returns an ordered list for just one category</Summary>
		public static List<DisplayField> GetForCategory(DisplayFieldCategory category){
			List<DisplayField> retVal=new List<DisplayField>();
			for(int i=0;i<DisplayFieldC.Listt.Count;i++){
				if(DisplayFieldC.Listt[i].Category==category){
					retVal.Add(DisplayFieldC.Listt[i].Copy());
				}
			}
			if(retVal.Count==0) {//default
				return DisplayFields.GetDefaultList(category);
			}
			return retVal;
		}

		public static List<DisplayField> GetDefaultList(DisplayFieldCategory category){
			List<DisplayField> list=new List<DisplayField>();
			if(category==DisplayFieldCategory.ProgressNotes){
				list.Add(new DisplayField("Date",67,category));
				//list.Add(new DisplayField("Time",40));
				list.Add(new DisplayField("Th",27,category));
				list.Add(new DisplayField("Surf",40,category));
				list.Add(new DisplayField("Dx",28,category));
				list.Add(new DisplayField("Description",218,category));
				list.Add(new DisplayField("Stat",25,category));
				list.Add(new DisplayField("Prov",42,category));
				list.Add(new DisplayField("Amount",48,category));
				list.Add(new DisplayField("ADA Code",62,category));
				list.Add(new DisplayField("User",62,category));
				list.Add(new DisplayField("Signed",55,category));
			}
			else if(category==DisplayFieldCategory.PatientSelect){
				list.Add(new DisplayField("LastName",75,category));
				list.Add(new DisplayField("First Name",75,category));
				//list.Add(new DisplayField("MI",25,category));
				list.Add(new DisplayField("Pref Name",60,category));
				list.Add(new DisplayField("Age",30,category));
				list.Add(new DisplayField("SSN",65,category));
				list.Add(new DisplayField("Hm Phone",90,category));
				list.Add(new DisplayField("Wk Phone",90,category));
				list.Add(new DisplayField("PatNum",80,category));
				//list.Add(new DisplayField("ChartNum",60,category));
				list.Add(new DisplayField("Address",100,category));
				list.Add(new DisplayField("Status",65,category));
				//list.Add(new DisplayField("Bill Type",90,category));
				//list.Add(new DisplayField("City",80,category));
				//list.Add(new DisplayField("State",55,category));
				//list.Add(new DisplayField("Pri Prov",85,category));
				//list.Add(new DisplayField("Birthdate",70,category));
				//list.Add(new DisplayField("Site",90,category));
			}
			return list;
		}

		public static List<DisplayField> GetAllAvailableList(DisplayFieldCategory category){
			List<DisplayField> list=new List<DisplayField>();
			if(category==DisplayFieldCategory.ProgressNotes){
				list.Add(new DisplayField("Date",67,category));
				list.Add(new DisplayField("Time",40,category));
				list.Add(new DisplayField("Th",27,category));
				list.Add(new DisplayField("Surf",40,category));
				list.Add(new DisplayField("Dx",28,category));
				list.Add(new DisplayField("Description",218,category));
				list.Add(new DisplayField("Stat",25,category));
				list.Add(new DisplayField("Prov",42,category));
				list.Add(new DisplayField("Amount",48,category));
				list.Add(new DisplayField("ADA Code",62,category));
				list.Add(new DisplayField("User",62,category));
				list.Add(new DisplayField("Signed",55,category));
			}
			else if(category==DisplayFieldCategory.PatientSelect){
				list.Add(new DisplayField("LastName",75,category));
				list.Add(new DisplayField("First Name",75,category));
				list.Add(new DisplayField("MI",25,category));
				list.Add(new DisplayField("Pref Name",60,category));
				list.Add(new DisplayField("Age",30,category));
				list.Add(new DisplayField("SSN",65,category));
				list.Add(new DisplayField("Hm Phone",90,category));
				list.Add(new DisplayField("Wk Phone",90,category));
				list.Add(new DisplayField("PatNum",80,category));
				list.Add(new DisplayField("ChartNum",60,category));
				list.Add(new DisplayField("Address",100,category));
				list.Add(new DisplayField("Status",65,category));
				list.Add(new DisplayField("Bill Type",90,category));
				list.Add(new DisplayField("City",80,category));
				list.Add(new DisplayField("State",55,category));
				list.Add(new DisplayField("Pri Prov",85,category));
				list.Add(new DisplayField("Birthdate",70,category));
				list.Add(new DisplayField("Site",90,category));
			}
			return list;
		}

		public static void SaveListForCategory(List<DisplayField> ListShowing,DisplayFieldCategory category){
			bool isDefault=true;
			List<DisplayField> defaultList=GetDefaultList(category);
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
			string command="DELETE FROM displayfield WHERE Category="+POut.PInt((int)category);
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
