using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	///<summary></summary>
	public class PatFieldDefs {
		///<summary>A list of all allowable patFields.</summary>
		public static PatFieldDef[] List;

		///<summary>Gets a list of all PatFieldDefs when program first opens.</summary>
		public static void Refresh() {
			string command="SELECT * FROM patfielddef";
			DataTable table=General.GetTable(command);
			List=new PatFieldDef[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new PatFieldDef();
				List[i].PatFieldDefNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].FieldName     = PIn.PString(table.Rows[i][1].ToString());
			}
		}

		///<summary>Must supply the old field name so that the patient lists can be updated.</summary>
		public static void Update(PatFieldDef p, string oldFieldName) {
			string command="UPDATE patfielddef SET " 
				+"FieldName = '"        +POut.PString(p.FieldName)+"'"
				+" WHERE PatFieldDefNum  ='"+POut.PInt   (p.PatFieldDefNum)+"'";
			General.NonQ(command);
			command="UPDATE patfield SET FieldName='"+POut.PString(p.FieldName)+"'"
				+" WHERE FieldName='"+POut.PString(oldFieldName)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(PatFieldDef p) {
			string command="INSERT INTO patfielddef (FieldName) VALUES("
				+"'"+POut.PString(p.FieldName)+"')";
			p.PatFieldDefNum=General.NonQ(command,true);
		}

		///<summary>Surround with try/catch, because it will throw an exception if any patient is using this def.</summary>
		public static void Delete(PatFieldDef p) {
			string command="SELECT LName,FName FROM patient,patfield WHERE "
				+"patient.PatNum=patfield.PatNum "
				+"AND FieldName='"+POut.PString(p.FieldName)+"'";
			DataTable table=General.GetTable(command);
			if(table.Rows.Count>0){
				string s=Lan.g("PatFieldDef","Not allowed to delete. Already in use by ")+table.Rows.Count.ToString()
					+" "+Lan.g("PatFieldDef","patients, including")+" \r\n";
				for(int i=0;i<table.Rows.Count;i++){
					if(i>5){
						break;
					}
					s+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString()+"\r\n";
				}
				throw new ApplicationException(s);
			}
			command="DELETE FROM patfielddef WHERE PatFieldDefNum ="+POut.PInt(p.PatFieldDefNum);
			General.NonQ(command);
		}

	
	
		
		
	}

		



		
	

	

	


}










