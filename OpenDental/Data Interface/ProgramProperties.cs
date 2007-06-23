using System;
using System.Collections;
using System.Data;
using OpenDentBusiness;

namespace OpenDental{

	///<summary></summary>
	public class ProgramProperties{
		///<summary></summary>
		public static ProgramProperty[] List;

		///<summary></summary>
		public static void Refresh(){
			string command = 
				"SELECT * from programproperty";
			DataTable table=General.GetTable(command);
			List=new ProgramProperty[table.Rows.Count];
			for (int i=0;i<table.Rows.Count;i++){
				List[i]=new ProgramProperty();
				List[i].ProgramPropertyNum =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ProgramNum         =PIn.PInt   (table.Rows[i][1].ToString());
				List[i].PropertyDesc       =PIn.PString(table.Rows[i][2].ToString());
				List[i].PropertyValue      =PIn.PString(table.Rows[i][3].ToString());
				//List[i].ValueType          =(FieldValueType)PIn.PInt(table.Rows[i][4].ToString());
			}
			//MessageBox.Show();
		}

		///<summary></summary>
		public static void Update(ProgramProperty Cur){
			string command= "UPDATE programproperty SET "
				+"ProgramNum = '"     +POut.PInt   (Cur.ProgramNum)+"'"
				+",PropertyDesc  = '" +POut.PString(Cur.PropertyDesc)+"'"
				+",PropertyValue = '" +POut.PString(Cur.PropertyValue)+"'"
				+" WHERE ProgramPropertyNum = '"+POut.PInt(Cur.ProgramPropertyNum)+"'";
			General.NonQ(command);
		}

		///<summary>This can only be called from ClassConversions. Users not allowed to add properties so there is no user interface.</summary>
		public static void Insert(ProgramProperty Cur){
			string command = "INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
				+") VALUES("
				+"'"+POut.PInt   (Cur.ProgramNum)+"', "
				+"'"+POut.PString(Cur.PropertyDesc)+"', "
				+"'"+POut.PString(Cur.PropertyValue)+"')";
			//MessageBox.Show(string command);
			General.NonQ(command);
			//Cur.ProgramNum=InsertID;
		}

		
		///<summary>This can only be called from ClassConversions. Users not allowed to delete properties so there is no user interface.</summary>
		public static void Delete(ProgramProperty Cur){
			string command= "DELETE from programproperty WHERE programpropertynum = '"+Cur.ProgramPropertyNum.ToString()+"'";
			General.NonQ(command);
		}

		///<summary>Returns an ArrayList of programproperties attached to the specified programNum</summary>
		public static ArrayList GetForProgram(int programNum){
			ArrayList ForProgram=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProgramNum==programNum){
					ForProgram.Add(List[i]);
				}
			}
			return ForProgram;
		}

		///<summary>After GetForProgram has been run, this gets one of those properties.</summary>
		public static ProgramProperty GetCur(ArrayList ForProgram, string desc){
			for(int i=0;i<ForProgram.Count;i++){
				if(((ProgramProperty)ForProgram[i]).PropertyDesc==desc){
					return (ProgramProperty)ForProgram[i];
				}
			}
			return null;
		}




		
	}

	

	


}










