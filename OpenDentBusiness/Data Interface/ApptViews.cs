using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary>Handles database commands related to the apptview table in the database.</summary>
	public class ApptViews{

		///<summary></summary>
		public static DataTable RefreshCache(){
			string c="SELECT * FROM apptview ORDER BY itemorder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="ApptView";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			ApptViewC.List=new ApptView[table.Rows.Count];
			for(int i=0;i<ApptViewC.List.Length;i++){
				ApptViewC.List[i]=new ApptView();
				ApptViewC.List[i].ApptViewNum = PIn.PInt   (table.Rows[i][0].ToString());
				ApptViewC.List[i].Description = PIn.PString(table.Rows[i][1].ToString());
				ApptViewC.List[i].ItemOrder   = PIn.PInt   (table.Rows[i][2].ToString());
				ApptViewC.List[i].RowsPerIncr = PIn.PInt   (table.Rows[i][3].ToString());	
			}
		}

		///<summary></summary>
		public static void Insert(ApptView Cur){
			string command = "INSERT INTO apptview (Description,ItemOrder,RowsPerIncr) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"', "
				+"'"+POut.PInt   (Cur.RowsPerIncr)+"')";
			//MessageBox.Show(string command);
			Cur.ApptViewNum=Db.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(ApptView Cur){
			string command= "UPDATE apptview SET "
				+"Description='"   +POut.PString(Cur.Description)+"'"
				+",ItemOrder = '"  +POut.PInt   (Cur.ItemOrder)+"'"
				+",RowsPerIncr = '"+POut.PInt   (Cur.RowsPerIncr)+"'"
				+" WHERE ApptViewNum = '"+POut.PInt(Cur.ApptViewNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ApptView Cur){
			string command="DELETE from apptview WHERE ApptViewNum = '"
				+POut.PInt(Cur.ApptViewNum)+"'";
			Db.NonQ(command);
		}

	

	


	}

	


}









