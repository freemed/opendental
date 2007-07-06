using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Data;
using System.Data.Common;
using OpenDentBusiness;

namespace OpenDental.DataAccess
{
	public static class DataSettings
	{
#warning Hard-coded connection string
		private static string connectionString = "Server=localhost;Database=opendental;Uid=root;Pwd=;";
		public static string ConnectionString
		{
			get { return connectionString; }
			set { connectionString = value; }
		}

#warning Hard-coded ProviderInvariantName
		private static string providerInvariantName = "MySql.Data.MySqlClient";
		public static string ProviderInvariantName
		{
			get { return providerInvariantName; }
			set { providerInvariantName = value; }
		}

		public static DatabaseType DbType
		{
			get { return DatabaseType.MySql; }
		}

		public static IDbConnection GetConnection()
		{
			DbProviderFactory factory = DbProviderFactories.GetFactory(providerInvariantName);
			DbConnection connection = factory.CreateConnection();
			connection.ConnectionString = ConnectionString;
			return connection;
		}

		public static bool LogOn(string username, string password)
		{
			// TODO: Get the connection string and provider from saved settings
			connectionString = string.Format("Data Source=localhost;Database=opendental;User ID={0};Password={1};", username, password);
			providerInvariantName = "MySql.Data.MySqlClient";

			using (IDbConnection connection = GetConnection())
			{
				try
				{
					connection.Open();
				}
				catch(DbException)
				{
					return false;
				}
			}

			return true;
		}
	}
}
