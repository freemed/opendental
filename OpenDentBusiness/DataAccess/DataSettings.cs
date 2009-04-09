using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Data;
using System.Data.Common;
using OpenDentBusiness;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;

namespace OpenDentBusiness.DataAccess {
	public static class DataSettings {
		private static string connectionString = "Server=localhost;Database=opendental;Uid=root;Pwd=;";
		public static string ConnectionString {
			get { return connectionString; }
			set { connectionString = value; }
		}

		public static string ProviderInvariantName {
			get {
				switch (DbType) {
					case DatabaseType.MySql:
						return "MySql.Data.MySqlClient";
					case DatabaseType.Oracle:
						return "Oracle.DataAccess.Client";
					default:
						throw new NotSupportedException();
				}
			}
		}

		private static DatabaseType dbType = DatabaseType.MySql;
		public static DatabaseType DbType {
			get { return dbType; }
			set { dbType = value; }
		}

		//public static void CreateConnectionString(string server, string database, string username, string password) {
		//	CreateConnectionString(server, DefaultPortNum, database, username, password);
		//}

		/// <summary>The server can include the port after a colon.</summary>
		public static void CreateConnectionString(string server, string database, string username, string password) {
			string serverName=server;
			string port=DefaultPortNum.ToString();
			if(server.Contains(":")) {
				string[] serverNamePort=server.Split(new char[] { ':' },StringSplitOptions.RemoveEmptyEntries);
				serverName=serverNamePort[0];
				port=serverNamePort[1];
			}
			switch (DbType) {
				case DatabaseType.Oracle:
					ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST="
					+ "(ADDRESS=(PROTOCOL=TCP)(HOST=" + serverName + ")(PORT=" + port + ")))"
					+ "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + database + ")));"
					+ "User Id=" + username + ";Password=" + password + ";";
					break;
				case DatabaseType.MySql:
					ConnectionString = "Server=" + serverName
						+ ";Port=" + port
						+ ";Database=" + database
						+ ";User ID=" + username
						+ ";Password=" + password
						+ ";CharSet=utf8";
					break;
				default:
					throw new NotSupportedException();
			}
		}

		public static int DefaultPortNum {
			get {
				switch (DbType) {
					case DatabaseType.Oracle:
						return 1521;
					case DatabaseType.MySql:
						return 3306;
					default:
						throw new NotSupportedException();
				}
			}
		}

		public static IDbConnection GetConnection() {
			switch (DbType) {
				case DatabaseType.MySql:
					return new MySqlConnection(ConnectionString);
				case DatabaseType.Oracle:
					return new OracleConnection(ConnectionString);
				default:
					throw new NotSupportedException();
			}
			/*
			DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderInvariantName);
			DbConnection connection = factory.CreateConnection();
			connection.ConnectionString = ConnectionString;
			return connection;
			 */
		}
	}
}
