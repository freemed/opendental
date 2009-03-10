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


namespace OpenDental{
	///<summary></summary>
	public class AnesthVSData{

		public MySqlCommand cmd;
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private static MySqlConnection con;

		///<summary>Gets those vital signs tied to a unique AnestheticRecordNum from the database</summary>
		public static List<AnestheticVSData> CreateObjects(int anestheticRecordNum) {
			string command="SELECT * FROM anesthvsdata WHERE AnestheticRecordNum='" + anestheticRecordNum + "'" + "ORDER BY VSTimeStamp DESC";
			return new List<AnestheticVSData>(DataObjectFactory<AnestheticVSData>.CreateObjects(command));
		}


	}

}

	











