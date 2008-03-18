using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
		///<summary></summary>
		public class AnestheticRecords{
		///<summary>List of all anesthetic records for the current patient.</summary>
		public static AnestheticRecord[] List;
		///<summary>Most recent date *first*. </summary>
		public static void Refresh(int patNum){
			
			string command =
				"SELECT * from anestheticrecord"
				+ " WHERE PatNum = '"+patNum.ToString()+"'"
				+ " ORDER BY anestheticrecord.AnestheticDate DESC";
				DataTable table = General.GetTable(command);
				List = new AnestheticRecord[table.Rows.Count];
				for (int i = 0; i < table.Rows.Count; i++){
				List[i] = new AnestheticRecord();
				List[i].AnestheticRecordNum = PIn.PInt(table.Rows[i][0].ToString());
				List[i].PatNum = PIn.PInt(table.Rows[i][1].ToString());
				List[i].AnestheticDate = PIn.PDateT(table.Rows[i][2].ToString());
				List[i].ProvNum = PIn.PInt(table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static void Update(AnestheticRecord Cur)
		{

			string command = "UPDATE anestheticrecord SET "
				+ "PatNum = '" + POut.PInt(Cur.PatNum) + "'"
				+ ",AnestheticDate = " + POut.PDateT(Cur.AnestheticDate) + "'"
				+ ",ProvNum = '" + POut.PInt(Cur.ProvNum) + "'"
				+ " WHERE AnestheticRecordNum = '" + POut.PInt(Cur.AnestheticRecordNum) + "'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(AnestheticRecord Cur){

			if (PrefB.RandomKeys)
			{
				Cur.AnestheticRecordNum = MiscData.GetKey("anestheticrecord", "AnestheticRecordNum");
			}
			string command = "INSERT INTO anestheticrecord (";
			if (PrefB.RandomKeys)
			{
				command += "AnestheticRecordNum,";
			}
				command += "PatNum,AnestheticDate,ProvNum"
				+ ") VALUES(";
			if (PrefB.RandomKeys)
			{
				command += "'" + POut.PInt(Cur.AnestheticRecordNum) + "', ";
			}
				command +=
				"'" + POut.PInt(Cur.PatNum) + "', "
				+ POut.PDateT(Cur.AnestheticDate) + ", "
				+ "'" + POut.PInt(Cur.ProvNum) + "')";
			if (PrefB.RandomKeys)
			{
				General.NonQ(command);
			}
			else
			{
				Cur.AnestheticRecordNum = General.NonQ(command, true);
			}
		}

		///<summary></summary>
		public static void Delete(AnestheticRecord Cur) {
			string command = "DELETE from anestheticrecord WHERE AnestheticRecordNum = '" + Cur.AnestheticRecordNum.ToString() + "'";
			General.NonQ(command);
		}

		


	}



}