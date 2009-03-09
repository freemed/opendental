using System;
using System.Collections;
using System.Data;
using OpenDental.DataAccess;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OpenDental;

namespace OpenDentBusiness{

	///<summary></summary>
	public class AnestheticRecords
	{
		///<summary>List of all anesthetic records for the current patient.</summary>
		public static AnestheticRecord[] List;

		///<summary>Stores the string of the command that will be sent to the database.</summary>
		public MySqlCommand cmd;
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private static MySqlConnection con;


		///<summary>Most recent date *first*. </summary>
		public static void Refresh(int patNum)
		{
			string command =
				"SELECT * FROM anestheticrecord"
				+ " WHERE PatNum = '" + patNum.ToString() + "'"
				+ " ORDER BY anestheticrecord.AnestheticDate DESC";
			DataTable table = General.GetTable(command);
			List = new AnestheticRecord[table.Rows.Count];
			for (int i = 0; i < table.Rows.Count; i++)
			{
				List[i] = new AnestheticRecord();
				List[i].AnestheticRecordNum = PIn.PInt(table.Rows[i][0].ToString());
				List[i].PatNum = PIn.PInt(table.Rows[i][1].ToString());
				List[i].AnestheticDate = PIn.PDateT(table.Rows[i][2].ToString());
				List[i].ProvNum = PIn.PInt(table.Rows[i][3].ToString());
			}

		}
		public AnestheticRecords Copy()
		{
			return (AnestheticRecords)this.MemberwiseClone();
		}
		///<summary></summary>
		///
		public static void Update(AnestheticRecord Cur)
		{
			string command = "UPDATE anestheticrecord SET "
				+ "PatNum = '" + POut.PInt(Cur.PatNum) + "'"
				+ ",AnestheticDate = " + POut.PDateT(Cur.AnestheticDate) + "'"
				+ ",ProvNum = '" + POut.PInt(Cur.ProvNum) + "'"
				+ " WHERE AnestheticRecordNum = '" + POut.PInt(Cur.AnestheticRecordNum) + "'";
			General.NonQ(command);
		}

		///<summary>Creates a new AnestheticRecord in the db</summary>
		public static void Insert(AnestheticRecord Cur)
		{
			if (PrefC.RandomKeys)
			{
				Cur.AnestheticRecordNum = MiscData.GetKey("anestheticrecord", "AnestheticRecordNum");
			}
			string command = "INSERT INTO anestheticrecord (";
			if (PrefC.RandomKeys)
			{
				command += "AnestheticRecordNum,";
			}
			command += "PatNum,AnestheticDate,ProvNum"
				+ ") VALUES(";
			if (PrefC.RandomKeys)
			{
				command += "'" + POut.PInt(Cur.AnestheticRecordNum) + "', ";
			}
			command +=
				"'" + POut.PInt(Cur.PatNum) + "', "
				+ POut.PDateT(Cur.AnestheticDate) + ", "
				+ "'" + POut.PInt(Cur.ProvNum) + "')";
			if (PrefC.RandomKeys)
			{
				General.NonQ(command);
			}
			else
			{
				Cur.AnestheticRecordNum = General.NonQ(command, true);
			}
		}

		///<summary>Creates a corresponding AnestheticData record in the db</summary>
		public static void InsertAnestheticData(AnestheticRecord Cur)
		{

			if (PrefC.RandomKeys)
			{
				Cur.AnestheticRecordNum = MiscData.GetKey("anestheticrecord", "AnestheticRecordNum");
			}
			string command = "INSERT INTO anestheticdata (";
			if (PrefC.RandomKeys)
			{
				command += "AnestheticRecordNum,";
			}
			command += "AnestheticRecordNum"
				+ ") VALUES(";
			if (PrefC.RandomKeys)
			{
				command += "'" + POut.PInt(Cur.AnestheticRecordNum) + "', ";
			}
			command +=
				"" + POut.PInt(Cur.AnestheticRecordNum) + ")";
			if (PrefC.RandomKeys)
			{
				General.NonQ(command);
			}
			else
			{
				Cur.AnestheticRecordNum = General.NonQ(command, true);
			}
		}
		///<summary>Deletes an Anesthetic Record and the corresponding Anesthetic Data</summary>
		public static void Delete(AnestheticRecord Cur)
		{
			string command = "DELETE FROM anestheticrecord WHERE AnestheticRecordNum = '" + Cur.AnestheticRecordNum.ToString() + "'";
			General.NonQ(command);
			string command2 = "DELETE FROM anestheticdata WHERE AnestheticRecordNum = '" + Cur.AnestheticRecordNum.ToString() + "'";
			General.NonQ(command2);
		}

		/// <summary>/// Gets the Anesthetic Record number from the anestheticrecord table./// </summary>

		public static int GetRecordNum(int patnum)
		{
			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if (con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnestheticRecordNum FROM anestheticrecord WHERE PatNum = '" + patnum.ToString() + "'";    /*"SELECT Max(AnestheticRecordNum) FROM anestheticrecord a, patient p where a.Patnum = p.Patnum and p.patnum = " + patnum + "";*/
			cmd.Connection = con;
			int anestheticRecordNum = Convert.ToInt32(cmd.ExecuteScalar());
			con.Close();
			return anestheticRecordNum;

		}

		/// <summary>/// Returns the date shown in the listAnesthetic.SelectedItem so it can be used to pull the correct AnestheticRecordCur from the db/// </summary>

		public static int GetRecordNumByDate(string AnestheticDateCur)
		{

			DateTime anestheticDate = Convert.ToDateTime(AnestheticDateCur);
			//need to format so it matches DateTime format as that's what's in the db; yyyy/MM/dd hh:mm:ss tt is what's displayed in listAnesthetic.SelectedItem
			string newdate = anestheticDate.ToString("yyyy-MM-dd HH:mm:ss");

			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if (con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnestheticRecordNum FROM anestheticrecord WHERE AnestheticDate = '" + (newdate) + "'";
			cmd.Connection = con;
			int anestheticRecordNum = Convert.ToInt32(cmd.ExecuteScalar());
			con.Close();
			return anestheticRecordNum;

		}
		public AnestheticRecord GetAnestheticData(int anestheticRecordNum)
		{
			AnestheticRecord retVal = null;
			for (int i = 0; i < List.Length; i++)
			{
				if (List[i].AnestheticRecordNum == anestheticRecordNum)
				{
					retVal = List[i].Copy();
				}
			}
			return retVal;
		}
		///<summary>Creates an Anesthesia Score record in the db</summary>
		public static void InsertAnesthScore(int AnestheticRecordNum, int QActivity, int QResp, int QCirc, int QConc, int QColor, int AnesthesiaScore, int DischAmb, int DischWheelChr, int DischAmbulance, int DischCondStable, int DischCondUnstable)
		{

			string command = "INSERT INTO anesthscore(AnestheticRecordNum,QActivity,QResp,QCirc,QConc,QColor,AnesthesiaScore,DischAmb,DischWheelChr,DischAmbulance,DischCondStable,DischCondUnstable) VALUES('" + AnestheticRecordNum + "','" + QActivity + "','" + QResp + "','" + QCirc + "','" + QConc + "','" + QColor + "','" + AnesthesiaScore + "','" + DischAmb + "','" + DischWheelChr + "','" + DischAmbulance + "','" + DischCondStable + "','" + DischCondUnstable + "'" + ")";
			General.NonQ(command);

		}

		public static void UpdateAnesthScore(int AnestheticRecordNum, int QActivity, int QResp, int QCirc, int QConc, int QColor, int AnesthesiaScore, int DischAmb, int DischWheelChr, int DischAmbulance, int DischCondStable, int DischCondUnstable)
		{

			string command = "UPDATE anesthscore SET "
				+ "QActivity = '" + POut.PInt(QActivity) + "'"
				+ ",QResp = '" + POut.PInt(QResp) + "'"
				+ ",QCirc = '" + POut.PInt(QCirc) + "'"
				+ ",QConc = '" + POut.PInt(QConc) + "'"
				+ ",QColor = '" + POut.PInt(QColor) + "'"
				+ ",AnesthesiaScore = '" + POut.PInt(AnesthesiaScore) + "'"
				+ ",DischAmb = '" + POut.PInt(DischAmb) + "'"
				+ ",DischWheelChr = '" + POut.PInt(DischWheelChr) + "'"
				+ ",DischAmbulance = '" + POut.PInt(DischAmbulance) + "'"
				+ ",DischCondStable = '" + POut.PInt(DischCondStable) + "'"
				+ ",DischCondUnstable = '" + POut.PInt(DischCondUnstable) + "'"
				+ " WHERE AnestheticRecordNum = '" + POut.PInt(AnestheticRecordNum) + "'";
			General.NonQ(command);
		}

		public static int GetAnesthScore(int AnestheticRecordNum)
		{


			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if (con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnesthesiaScore FROM anesthscore WHERE AnestheticRecordNum = '" + AnestheticRecordNum + "'";
			cmd.Connection = con;
			int anesthScore = Convert.ToInt32(cmd.ExecuteScalar());
			con.Close();
			return anesthScore;
		}

		public static int GetScoreRecordNum(int AnestheticRecordNum)
		{

			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if (con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnestheticRecordNum FROM anesthscore WHERE AnestheticRecordNum = '" + AnestheticRecordNum + "'";
			cmd.Connection = con;
			int anestheticRecordNum = Convert.ToInt32(cmd.ExecuteScalar());
			con.Close();
			if (anestheticRecordNum == 0)
			{
				return 0;
			}
			else return anestheticRecordNum;

		}

		public static string GetAnesthCloseTime(int AnestheticRecordNum)
		{

			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if (con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnesthClose FROM anestheticdata WHERE AnestheticRecordNum = '" + AnestheticRecordNum + "'";
			cmd.Connection = con;
			string anesthClose = Convert.ToString(cmd.ExecuteScalar());
			con.Close();
			return anesthClose;

		}

		public static string GetAnesthDate(int AnestheticRecordNum){

			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if (con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnestheticDate FROM anestheticrecord WHERE AnestheticRecordNum = '" + AnestheticRecordNum + "'";
			cmd.Connection = con;
			string anestheticDate = Convert.ToString(cmd.ExecuteScalar());
			DateTime anesthDate = Convert.ToDateTime(anestheticDate);
			string anestheticdate = anesthDate.ToString("MM/dd/yyyy");
			con.Close();
			return anestheticdate;
		}

		public static int GetAnesthProvType(int ProvNum)
		{

			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if (con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT AnesthProvType FROM provider WHERE ProvNum = '" + ProvNum + "'";
			cmd.Connection = con;
			string provType = Convert.ToString(cmd.ExecuteScalar());
			int anesthProvType = Convert.ToInt32(provType);
			con.Close();
			return anesthProvType;
		}

	}
}