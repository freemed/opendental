using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDentBusiness.DataAccess;
using MySql.Data.MySqlClient;

namespace OpenDentBusiness {
	///<summary></summary>
	public class AnesthVSDatas {

		public MySqlCommand cmd;
		//<summary>This is the connection that is used by the data adapter for all queries.  js-not allowed</summary>
		//private static MySqlConnection con;

		///<summary>Gets those vital signs tied to a unique AnestheticRecordNum from the database</summary>
		public static List<AnestheticVSData> CreateObjects(int anestheticRecordNum) {
			string command="SELECT * FROM anesthvsdata WHERE AnestheticRecordNum='" + anestheticRecordNum + "'" + "ORDER BY VSTimeStamp DESC";
			return new List<AnestheticVSData>(DataObjectFactory<AnestheticVSData>.CreateObjects(command));
		}

		public static DataTable RefreshCache(int anestheticRecordNum) {
			int ARNum = anestheticRecordNum;
			string c="SELECT * FROM anesthvsdata WHERE AnestheticRecordNum ='" + anestheticRecordNum+ "'" + "ORDER BY VSTimeStamp DESC"; //most recent at top of list
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="AnesthVSData";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table) {
			AnesthVSDataC.Listt=new List<AnestheticVSData>();
			AnestheticVSData vsCur;
			for(int i=0;i<table.Rows.Count;i++) {
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

		public static void InsertVSData(int anestheticRecordNum,int patNum,string VSMName,string VSMSerNum,int NBPs,int NBPd,int NBPm,int HR,int SpO2,int temp,int EtCO2,string VSTimeStamp,string MessageID,string HL7Message) {
			string command = "INSERT INTO anesthvsdata (AnestheticRecordNum,PatNum,VSMName,VSMSerNum, BPSys, BPDias, BPMAP, HR, SpO2, Temp, EtCO2,VSTimeStamp, MessageID, HL7Message)" +
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
				+ POut.PString(VSTimeStamp) + "','"
				+	POut.PString(MessageID) + "','"
				+ POut.PString(HL7Message)+ "')";
			Db.NonQ(command);

		}

		public static int UpdateVSData(int anestheticRecordNum,int patNum,string VSMName,string VSMSerNum,int NBPs,int NBPd,int NBPm,int HR,int SpO2,int temp,int EtCO2,string VSTimeStamp,string MessageID,string HL7Message) {
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
						+ " ,MessageID = '" + POut.PString(MessageID) + "' "
						+ " ,HL7Message = '" + POut.PString(HL7Message) + "' "
						+ "WHERE VSTimeStamp='" + Convert.ToString(VSTimeStamp)+ "'" + " AND AnestheticRecordNum = " + anestheticRecordNum;
			int val = Db.NonQ(command);
			return val;

		}

		///<summary>jsparks-It would be better to use Db here.  But I don't understand what ExecuteScalar is doing.</summary>
		public static string GetVSTimeStamp(string vSTimeStamp) {
			string VSTimeStamp = vSTimeStamp;
			MySqlCommand cmd = new MySqlCommand();
			MySqlConnection con = new MySqlConnection(DataSettings.ConnectionString);
			cmd.Connection = con;
			if(con.State == ConnectionState.Open)
				con.Close();
			con.Open();
			cmd.CommandText = "SELECT * FROM anesthvsdata WHERE VSTimeStamp = '"+ vSTimeStamp +"'" + "ORDER BY VSTimeStamp DESC";
			try {
				vSTimeStamp = Convert.ToString(cmd.ExecuteScalar()); //might be null on the first pass 
			}
			catch {
				vSTimeStamp = "";
			}

			con.Close();
			return vSTimeStamp;

		}
	}
}











