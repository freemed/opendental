/*
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OpenDentBusiness{

	///<summary></summary>
	public class AnesthMedInvAdjs{
		///<summary>List of all anesthetic records for the current patient.</summary>
		public static AnesthMedsInventoryAdj[] List;

		//<summary>This data adapter is used for all queries to the database.</summary>
		//private MySqlDataAdapter da;
		//<summary>Stores the string of the command that will be sent to the database.</summary>
		//public MySqlCommand cmd;
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private static MySqlConnection con;


		///<summary>Most recent date *first*. </summary>
		public static void Refresh(int anestheticMedNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),anestheticMedNum);
				return;
			}
			string command =
				"SELECT * from anesthmedsinventoryadj"
				+ " WHERE AnestheticMedNum = '"+anestheticMedNum.ToString()+"'"
				+ " ORDER BY anesthmedsinventoryadj.TimeStamp DESC";
			DataTable table = Db.GetTable(command);
			List = new AnesthMedsInventoryAdj[table.Rows.Count];
			for (int i = 0; i < table.Rows.Count; i++){
				List[i] = new AnesthMedsInventoryAdj();
				List[i].AdjustNum = PIn.PInt(table.Rows[i][0].ToString());
				List[i].AnestheticMedNum = PIn.PInt(table.Rows[i][1].ToString());
				List[i].QtyAdj = PIn.PInt(table.Rows[i][2].ToString());
				List[i].UserNum = PIn.PInt(table.Rows[i][3].ToString());
				List[i].Notes = PIn.PString(table.Rows[i][4].ToString());
				List[i].TimeStamp = PIn.PDateT(table.Rows[i][5].ToString());
			}
		}

		///<summary></summary>
		public static void Update(AnesthMedsInventoryAdj Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "UPDATE anesthmedsinventoryadj SET "
				+ "AnestheticMedNum = '" + POut.PInt(Cur.AnestheticMedNum) + "' "
				+ ",QtyAdj = " + POut.PDouble(Cur.QtyAdj) + "' "
				+ ",UserNum = '" + POut.PInt(Cur.UserNum) + "' "
				+ ",Notes = '" + POut.PString(Cur.Notes) + "' "
				+ ",TimeStamp = " + POut.PDateT(Cur.TimeStamp) + "' "
				+ " WHERE AdjustNum = '" + POut.PInt(Cur.AdjustNum) + "' ";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static int Insert(AnesthMedsInventoryAdj Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.AdjustNum=Meth.GetInt(MethodBase.GetCurrentMethod(),Cur);
				return Cur.AdjustNum;
			}
			if (PrefC.RandomKeys){
				Cur.AdjustNum = MiscData.GetKey("anesthmedsinventoryadj", "AdjustNum");
			}
			string command = "INSERT INTO anesthmedsinventoryadj (";
			if(PrefC.RandomKeys){
				command += "AdjustNum,";
			}
			command += "AnestheticMedNum,QtyAdj,UserNum,Notes,TimeStamp"
				+ ") VALUES(";
			if (PrefC.RandomKeys){
				command +="'"+POut.PInt(Cur.AdjustNum)+"',";
			}
			command +=
				"'"+POut.PInt(Cur.AnestheticMedNum)+"', "
				+"'"+POut.PDouble(Cur.QtyAdj)+"', "
				+"'"+POut.PInt(Cur.UserNum)+"', "
				+"'"+POut.PString(Cur.Notes)+"', "
				+POut.PDateT(Cur.TimeStamp)+")"; 
			if (PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
				Cur.AdjustNum = Db.NonQ(command, true);
			}
			return Cur.AdjustNum;
		}

		///<summary></summary>
		public static void Delete(AnesthMedsInventoryAdj Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "DELETE from anesthmedsinventoryadj WHERE AdjustNum = '" + Cur.AdjustNum.ToString() + "'";
			Db.NonQ(command);
		}

		/// <summary>
		/// Gets the Anesthetic Record number from the anestheticrecord table.
		/// </summary>
		public static int getRecordNum(int anestheticMedNum)
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),anestheticMedNum);
			}
			MySqlCommand command2 = new MySqlCommand();
			con=new MySqlConnection();
			con.Open();
			command2.CommandText = "SELECT AdjustNum from anesthmedsinventoryadj WHERE AnestheticMedNum = '" + anestheticMedNum.ToString() + "'";    
 //"SELECT Max(AnestheticRecordNum) FROM anestheticrecord a, patient p where a.Patnum = p.Patnum and p.patnum = " + patnum + "";
			command2.Connection = con;
			int adjustNum = Convert.ToInt32(command2.ExecuteScalar());
			return adjustNum;
			//con.Close();
		}


	}



}*/