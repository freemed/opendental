using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDental.DataAccess;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;


namespace OpenDentBusiness{
	///<summary></summary>
	public class AnesthVSDatas{

		public MySqlCommand cmd;
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private static MySqlConnection con;

		///<summary>Gets those vital signs tied to a unique AnestheticRecordNum from the database</summary>
		public static List<AnestheticVSData> CreateObjects(int anestheticRecordNum) {
			string command="SELECT * FROM anesthvsdata WHERE AnestheticRecordNum='" + anestheticRecordNum + "'" + "ORDER BY VSTimeStamp DESC";
			return new List<AnestheticVSData>(DataObjectFactory<AnestheticVSData>.CreateObjects(command));
		}

		public static DataTable RefreshCache(int anestheticRecordNum){
			int ARNum = anestheticRecordNum;
			string c="SELECT * FROM anesthvsdata WHERE AnestheticRecordNum ='" + anestheticRecordNum+ "'" + "ORDER BY VSTimeStamp DESC"; //most recent at top of list
			DataTable table=General.GetTable(c);
			table.TableName="AnesthVSData";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			AnesthVSDataC.Listt=new List<AnestheticVSData>();
			AnestheticVSData vsCur;
			for(int i=0;i<table.Rows.Count;i++){
				vsCur=new AnestheticVSData();
				vsCur.IsNew = false;
				vsCur.AnesthVSDataNum		=	PIn.PInt(table.Rows[i][0].ToString());
				vsCur.AnestheticRecordNum	=	PIn.PInt(table.Rows[i][1].ToString());
				vsCur.PatNum				=	PIn.PString(table.Rows[i][2].ToString());
				vsCur.VSMName				=	PIn.PString(table.Rows[i][3].ToString());
				vsCur.VSMSerNum				=	PIn.PString(table.Rows[i][4].ToString());
				vsCur.BPSys					=	PIn.PString(table.Rows[i][5].ToString());
				vsCur.BPDias				=	PIn.PString(table.Rows[i][6].ToString());
				vsCur.BPMAP					=	PIn.PString(table.Rows[i][7].ToString());
				vsCur.HR					=	PIn.PString(table.Rows[i][8].ToString());
				vsCur.SpO2					=	PIn.PString(table.Rows[i][9].ToString());
				vsCur.EtCO2					=	PIn.PString(table.Rows[i][10].ToString());
				vsCur.Temp					=	PIn.PString(table.Rows[i][11].ToString());
				vsCur.VSTimeStamp		=	PIn.PString(table.Rows[i][12].ToString());
				AnesthVSDataC.Listt.Add(vsCur);
			}
		}
			
		public static int InsertVSData(int anestheticRecordNum,int patNum, string VSMName, string VSMSerNum,int NBPs, int NBPd, int NBPm, int HR, int SpO2, int temp, int EtCO2, string VSTimeStamp ){
			string command = "INSERT INTO anesthvsdata (AnestheticRecordNum,PatNum,VSMName,VSMSerNum, BPSys, BPDias, BPMAP, HR, SpO2, Temp, EtCO2,VSTimeStamp)" +
				"VALUES(" + POut.PInt(anestheticRecordNum) + "," 
				+ POut.PInt(patNum) + ",'"
				+ POut.PString(VSMName) + "','"
				+ POut.PString(VSMSerNum) + "',"
				+ POut.PInt(NBPs) + ","
				+ POut.PInt(NBPd) + ","
				+ POut.PInt(NBPm) + ","
				+ POut.PInt(HR) + ","
				+ POut.PInt(SpO2) + ","
				+ POut.PInt(temp) + ","
				+ POut.PInt(EtCO2) + ",'"
				+ POut.PString(VSTimeStamp)+"')";
				int val =  General.NonQ(command);
				return val;

		}

						public static int UpdateVSData(int anestheticRecordNum, int patNum, string VSMName, string VSMSerNum,int NBPs, int NBPd, int NBPm, int HR, int SpO2, int temp, int EtCO2, string VSTimeStamp){
						string command = "UPDATE anesthvsdata SET "
						+ " PatNum = " + POut.PInt(patNum) + " " 
						+ " ,VSMName = '" + POut.PString(VSMName) + "' "
						+ " ,VSMSerNum = '" + POut.PString(VSMSerNum) + "' "
						+ " ,BPSys = "+ POut.PInt(NBPs) + " "
						+ " ,BPDias = "+ POut.PInt(NBPd) + " "
						+ " ,BPMAP = "+ POut.PInt(NBPm) + " "
						+ " ,HR = "+ POut.PInt(HR) + " "
						+ " ,SpO2 = "+ POut.PInt(SpO2) + " "
						+ " ,Temp = "+ POut.PInt(temp) + " "
						+ " ,EtCO2 = "+ POut.PInt(EtCO2) + " "
						+ "WHERE VSTimeStamp='" + Convert.ToString(VSTimeStamp)+ "'" + " AND AnestheticRecordNum = " + anestheticRecordNum;
				int val = General.NonQ(command);
							return val;
				
		}

			public static string GetVSTimeStamp(int anestheticRecordNum){
			MySqlCommand cmd = new MySqlCommand();
			con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if (con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT VSTimeStamp FROM anesthvsdata WHERE AnestheticRecordNum=" + anestheticRecordNum +"";
			string VSTimeStamp = Convert.ToString(cmd.ExecuteScalar());
			con.Close();
			return VSTimeStamp;
		}

	}

}











