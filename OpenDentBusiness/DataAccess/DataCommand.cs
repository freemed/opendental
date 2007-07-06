using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace OpenDental.DataAccess
{
	public static class DataCommand
	{
		public static void NonQ(string query)
		{
			using (IDbConnection connection = DataSettings.GetConnection())
			using (IDbCommand command = connection.CreateCommand()) 
			{
				command.CommandText = query;

				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}

		public static DataTable GetDataTable(string query) {
			DataTable table = new DataTable();

			using (IDbConnection connection = DataSettings.GetConnection())
			using (IDbCommand command = connection.CreateCommand()) {
				command.CommandText = query;

				connection.Open();

				using (IDataReader reader = command.ExecuteReader()) {
					table.Load(reader);
				}

				connection.Close();
			}

			return table;
		}
	}
}
