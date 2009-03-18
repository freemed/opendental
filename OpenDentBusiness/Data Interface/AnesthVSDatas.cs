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
	}

}

	











