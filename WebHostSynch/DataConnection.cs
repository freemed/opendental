/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Configuration;

namespace WebHostSynch {

	///<summary></summary>
	public class DataConnection{//
		///<summary>This data adapter is used for all queries to the database.</summary>
		private MySqlDataAdapter da;
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private MySqlConnection con;
		///<summary>Used to get very small bits of data from the db when the data adapter would be overkill.  For instance retrieving the response after a command is sent.</summary>
		private MySqlDataReader dr;
		///<summary>Stores the string of the command that will be sent to the database.</summary>
		public MySqlCommand cmd;
		///<summary>After inserting a row, this variable will contain the primary key for the newly inserted row.  This can frequently save an additional query to the database.</summary>
		public int InsertID;

		///<summary></summary>
		public DataConnection(){
			string connectStr="";
			connectStr= ConfigurationManager.ConnectionStrings["DBRegKey"].ConnectionString;
			con=new MySqlConnection(connectStr);
			//dr = null;
			cmd = new MySqlCommand();
			cmd.Connection=con;
			//table=new DataTable();
		}

		///<summary>Fills table with data from the database.</summary>
		public DataTable GetTable(string command){
			DataTable table=new DataTable();
			cmd.CommandText=command;
			da=new MySqlDataAdapter(cmd);
			da.Fill(table);
			con.Close();
 			return table;
		}
		
		///<summary>Fills dataset with data from the database.</summary>
		public DataSet GetDs(string commands) {
			DataSet ds=new DataSet();
			cmd.CommandText=commands;
			da=new MySqlDataAdapter(cmd);
			da.Fill(ds);
			con.Close();
			return ds;
		}

		///<summary>Sends a non query command to the database and returns the number of rows affected. If true, then InsertID will be set to the value of the primary key of the newly inserted row.</summary>
		public int NonQ(string commands,bool getInsertID){
			int rowsChanged=0;
			cmd.CommandText=commands;
			con.Open();
			rowsChanged=cmd.ExecuteNonQuery();
			if(getInsertID) {
				cmd.CommandText="SELECT LAST_INSERT_ID()";
				dr=(MySqlDataReader)cmd.ExecuteReader();
				if(dr.Read())
					InsertID=Convert.ToInt32(dr[0].ToString());
			}
			con.Close();
			return rowsChanged;
		}
		
		///<summary>Sends a non query command to the database and returns the number of rows affected. If true, then InsertID will be set to the value of the primary key of the newly inserted row.</summary>
		public int NonQ(string command){
			return NonQ(command,false);
		}

		///<summary>Use this for count(*) queries.  They are always guaranteed to return one and only one value.  Uses datareader instead of datatable, so faster.  Can also be used when retrieving prefs manually, since they will also return exactly one value</summary>
		public string GetCount(string command){
			string retVal="";
			cmd.CommandText=command;
			con.Open();
			dr=(MySqlDataReader)cmd.ExecuteReader();
			dr.Read();
			retVal=dr[0].ToString();
			con.Close();
			return retVal;
		}

	}
}
