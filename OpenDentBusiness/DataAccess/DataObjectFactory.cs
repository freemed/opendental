using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;
using OpenDentBusiness;
using OpenDentBusiness.Properties;
using System.Drawing;

namespace OpenDental.DataAccess {
	/// <summary>
	/// Provides common methods for creating, reading, updating and deleting <see cref="IDataObject"/> objects.
	/// </summary>
	/// <typeparam name="T">The type of object.</typeparam>
	public static class DataObjectFactory<T> where T : DataObjectBase, new() {
		private const char ParameterPrefix = '?';

		private static bool useParameters;
		public static bool UseParameters {
			get { return useParameters; }
			set { useParameters = value; }
		}

		/// <summary>
		/// Creates a new <typeparamref name="T"/>.
		/// </summary>
		/// <returns>A new <typeparamref name="T"/>.</returns>
		public static T NewObject() {
			return new T();
		}

		/// <summary>
		/// Loads a collection of objects of type <typeparamref name="T"/> from the database, using the
		/// given <paramref name="query"/>.
		/// </summary>
		/// <param name="query">The query to execute on the server.</param>
		/// <returns>A collection of objects of type <typeparamref name="T"/>.</returns>
		public static Collection<T> CreateObjects(string query) {
			if (query == null)
				throw new ArgumentNullException("query");

			if (!RemotingClient.OpenDentBusinessIsLocal)
				return (Collection<T>)FactoryClient<T>.SendRequest("CreateObjects", default(T), new object[] { query });

			Collection<T> values;

			using (IDbConnection connection = DataSettings.GetConnection())
			using (IDbCommand command = connection.CreateCommand()) {
				command.CommandText = query;

				connection.Open();
				using (IDataReader reader = command.ExecuteReader()) {
					values = CreateObjects(reader);
				}
				connection.Close();
			}

			return values;
		}

		/// <summary>
		/// Loads an objects of type <typeparamref name="T"/> from the database, using the
		/// given <paramref name="query"/>.
		/// </summary>
		/// <param name="query">The query to execute on the server.</param>
		/// <returns>An objects of type <typeparamref name="T"/>.</returns>
		public static T CreateObject(string query) {
			if (query == null)
				throw new ArgumentNullException("query");

			if (!RemotingClient.OpenDentBusinessIsLocal)
				return (T)FactoryClient<T>.SendRequest("CreateObject", default(T), new object[] { query });

			T value;

			using (IDbConnection connection = DataSettings.GetConnection())
			using (IDbCommand command = connection.CreateCommand()) {
				command.CommandText = query;

				connection.Open();
				using (IDataReader reader = command.ExecuteReader()) {
					reader.Read();
					value = CreateObject(reader);
				}
				connection.Close();
			}

			return value;
		}

		/// <summary>
		/// Retrieves the objects of type <typeparamref name="T"/> whose Id match the ids specified
		/// from the database.
		/// </summary>
		/// <param name="id">The ids of the objects.</param>
		/// <returns>A collection of objects of type <typeparamref name="T"/>.</returns>
		/// <remarks>This method assumes the table corresponding to this object has a single, integer primary key.</remarks>
		/// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/>.</exception>
		/// <exception cref="InvalidOperationException">The object does not have a single primary key.</exception>
		/// <exception cref="InvalidOperationException">The object does have a single primary key, but it is not of the <see cref="System.Int32"/> type.</exception>
		public static Collection<T> CreateObjects(int[] id) {
			if (id == null)
				throw new ArgumentNullException("id");

			// Specific case. Create a list of objects, base on IDs.
			// Construct the query
			string primaryKeyFieldName = DataObjectInfo<T>.GetPrimaryKeyFieldName();
			string tableName = DataObjectInfo<T>.GetTableName();

			StringBuilder queryBuilder = new StringBuilder();
			queryBuilder.Append(string.Format("SELECT * FROM {0} WHERE {1} IN (", tableName, primaryKeyFieldName));
			for (int i = 0; i < id.Length; i++) {
				queryBuilder.Append(id[i]);
				if (i != id.Length - 1)
					queryBuilder.Append(',');
			}
			queryBuilder.Append(")");

			return CreateObjects(queryBuilder.ToString());
		}

		/// <summary>
		/// Retrieves the object of type <typeparamref name="T"/> whose Id match the id specified
		/// from the database.
		/// </summary>
		/// <param name="id">The id of the object.</param>
		/// <returns>An object of type <typeparamref name="T"/>.</returns>
		/// <remarks>This method assumes the table corresponding to this object has a single, integer primary key.</remarks>
		/// <exception cref="InvalidOperationException">The object does not have a single primary key.</exception>
		/// <exception cref="InvalidOperationException">The object does have a single primary key, but it is not of the <see cref="System.Int32"/> type.</exception>
		public static T CreateObject(int id) {
			// Specific case. Create an object, based on an ID. 
			// Construct the query.
			string primaryKeyFieldName = DataObjectInfo<T>.GetPrimaryKeyFieldName();
			string tableName = DataObjectInfo<T>.GetTableName();
			string query = string.Format("SELECT * FROM {0} WHERE {1} = {2}", tableName, primaryKeyFieldName, id);

			return CreateObject(query);
		}

		/// <summary>
		/// Reads one row from the <paramref name="reader"/> specified and creates an object of type <typeparamref name="T"/>.
		/// </summary>
		/// <param name="reader">A <see cref="IDataReader"/> that provides access to the raw database data.</param>
		/// <returns>An object of type <typeparamref name="T"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="reader"/> is <see langword="null"/>.</exception>
		public static T CreateObject(IDataReader reader) {
			if (reader == null)
				throw new ArgumentNullException("reader");

			T value = new T();
			Collection<DataFieldInfo> dataFields = DataObjectInfo<T>.GetDataFields();

			foreach (DataFieldInfo dataField in dataFields) {
				if (dataField.WriteOnly)
					continue;

				int ordinal = reader.GetOrdinal(dataField.DatabaseName);

				// Special case for certain types
				if (dataField.Field.FieldType == typeof(bool)) {
					// Booleans are sometimes stored as TINYINT(3), to enable conversion to
					// Enums if required. Hence, we need to do an explicit conversion.
					dataField.Field.SetValue(value, reader.GetBoolean(ordinal));
				}
				else if (dataField.Field.FieldType == typeof(DateTime)) {
					// DateTime fields can have various minimum or default values depending on
					// the database type. In MySql, for example, it is 00-00-00, which is not supported
					// by .NET. So for now, we catch a cast exception and set it to DateTime.MinValue.
					try {
						dataField.Field.SetValue(value, reader.GetDateTime(ordinal));
					}
					catch {
						dataField.Field.SetValue(value, DateTime.MinValue);
					}
				}
				else {
					dataField.Field.SetValue(value, reader.GetValue(ordinal));
				}
			}

			value.IsNew = false;
			return value;
		}

		/// <summary>
		///  Creates an object of type <typeparamref name="T"/> based on a <see cref="DataRow"/>.
		///  The fields of the <see cref="DataRow"/> can be either of the corresponding type, or of the 
		///  <see cref="String"/> type. In the case of a <see cref="String"/>, conversion will be done using
		///  the <see cref="PIn"/> data class.
		/// </summary>
		/// <param name="row">
		///  A <see cref="DataRow"/> containing rows corresponding to objects of type <typeparamref name="T"/>.
		/// </param>
		/// <returns>
		///  An objects of type <typeparamref name="T"/>.
		/// </returns>
		/// <remarks>
		///  <para>
		///  This method is for compatibility purposes only.
		///  </para>
		///  <para>
		///  This method needs to be tested.
		///  </para>
		/// </remarks>
		public static T CreateObject(DataRow row) {
			if (row == null)
				throw new ArgumentNullException("row");

			Collection<T> values = new Collection<T>();
			Collection<DataFieldInfo> dataFields = DataObjectInfo<T>.GetDataFields();

			T value = new T();

			foreach (DataFieldInfo dataField in dataFields) {
				if (dataField.WriteOnly)
					continue;

				// Retrieve the value and its type, both in the database and code.
				object dataValue = row[dataField.DatabaseName];
				Type dataType = row.Table.Columns[dataField.DatabaseName].DataType;
				Type codeType = dataField.Field.FieldType;

				if (codeType != typeof(string) && dataType == typeof(string)) {
					// If the type in the dataset is "string", but the type in the code
					// object isn't "string", we use the PIn class.
					if (dataType == typeof(Bitmap)) {
						dataValue = PIn.PBitmap((string)dataValue);
					}
					else if (dataType == typeof(bool)) {
						dataValue = PIn.PBool((string)dataValue);
					}
					else if (dataType == typeof(Byte)) {
						dataValue = PIn.PByte((string)dataValue);
					}
					else if (dataType == typeof(DateTime)) {
						// NOTE: Is there any difference between PIn.PDate and PIn.PDateT?
						dataValue = PIn.PDate((string)dataValue);
					}
					else if (dataType == typeof(double)) {
						dataValue = PIn.PDouble((string)dataValue);
					}
					else if (dataType == typeof(float)) {
						dataValue = PIn.PFloat((string)dataValue);
					}
					else if (dataType == typeof(int)) {
						dataValue = PIn.PInt((string)dataValue);
					}
					else {
						// NOTE: Support for "Sound" is not here yet. Maybe it should be exported
						// to a byte[] type and then saved, don't know.
						throw new NotSupportedException(Resources.DataTypeNotSupportedByPIn);
					}
				}
				else {
					// The object is stored in it's "true" type in the DataSet as well (no conversions to
					// string types have been done). We can, normally, directly use it, except for a couple
					// of special cases.

					if (codeType == typeof(bool)) {
						// Booleans are sometimes stored as TINYINT(3), to enable conversion to
						// Enums if required. Hence, we need to do an explicit conversion.
						dataField.Field.SetValue(value, Convert.ToBoolean(dataValue));
					}
					else if (codeType == typeof(DateTime)) {
						// DateTime fields can have various minimum or default values depending on
						// the database type. In MySql, for example, it is 00-00-00, which is not supported
						// by .NET. So for now, we catch a cast exception and set it to DateTime.MinValue.
						try {
							dataField.Field.SetValue(value, DateTime.Parse(dataValue.ToString()));
						}
						catch {
							dataField.Field.SetValue(value, DateTime.MinValue);
						}
					}
					else {
						dataField.Field.SetValue(value, dataValue);
					}
				}
			}

			return value;
		}

		/// <summary>
		///  Creates a collection of objects of type <typeparamref name="T"/> based on a <see cref="DataTable"/>.
		///  The fields of the <see cref="DataTable"/> can be either of the corresponding type, or of the 
		///  <see cref="String"/> type. In the case of a <see cref="String"/>, conversion will be done using
		///  the <see cref="PIn"/> data class.
		/// </summary>
		/// <param name="table">
		///   A <see cref="DataTable"/> containing rows corresponding to objects of type <typeparamref name="T"/>.
		/// </param>
		/// <returns>
		///  A collection of objects of type <typeparamref name="T"/>.
		/// </returns>
		/// <remarks>
		///  <para>
		///  This method is for compatibility purposes only.
		///  </para>
		///  <para>
		///  This method needs to be tested.
		///  </para>
		/// </remarks>
		public static Collection<T> CreateObjects(DataTable table) {
			if (table == null)
				throw new ArgumentNullException("table");

			Collection<T> values = new Collection<T>();
			foreach (DataRow row in table.Rows) {
				T value = CreateObject(row);
				values.Add(value);
			}

			return values;
		}

		/// <summary>
		/// Reads all rows from the <paramref name="reader"/> specified and creates a collection of
		/// objects of type <typeparamref name="T"/>.
		/// </summary>
		/// <param name="reader">A <see cref="IDataReader"/> that provides access to the raw database data.</param>
		/// <returns>An object of type <typeparamref name="T"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="reader"/> is <see langword="null"/>.</exception>
		public static Collection<T> CreateObjects(IDataReader reader) {
			if (reader == null)
				throw new ArgumentNullException("reader");

			Collection<T> values = new Collection<T>();

			while (reader.Read()) {
				values.Add(CreateObject(reader));
			}

			return values;
		}

		/// <summary>
		///  <para>
		///   Saves an object to the database. If the IsNew propery of the object is set to false, an existing
		///   row will be updated. If the IsNew property is set to true, a new row will be created.
		///  </para>
		///  <para>
		///  If the object contains a single PrimaryKey field, with AutoNumber set to true, a new primary key 
		///  will be generated. Depending on the current settings, this may be a new, random key or an incremental
		///  key.
		/// </summary>
		/// <remarks>
		///  <para>
		///   When an existing object is saved to the database, only modified values are being sent to the database.
		///   This is to avoid concurrency issues as much as possible.
		///  </para>
		///  <para>
		///   This method is not thread safe.
		///  </para>
		/// </remarks>
		/// <param name="value">The object to be saved.</param>
		public static void WriteObject(T value) {
			WriteObject(value, false);
		}

		/// <summary>
		///  <para>
		///   Saves an object to the database. If the IsNew propery of the object is set to false, an existing
		///   row will be updated. If the IsNew property is set to true, a new row will be created.
		///  </para>
		///  <para>
		///  If the object contains a single PrimaryKey field, with AutoNumber set to true, a new primary key 
		///  will be generated. Depending on the current settings, this may be a new, random key or an incremental
		///  key.
		/// </summary>
		/// <remarks>
		///  <para>
		///   When an existing object is saved to the database, only modified values are being sent to the database.
		///   This is to avoid concurrency issues as much as possible.
		///  </para>
		///  <para>
		///   This method is not thread safe.
		///  </para>
		/// </remarks>
		/// <param name="value">The object to be saved.</param>
		/// <param name="overrideAutoNumber">
		///  Sometimes a new object will be added to the database, but the primary key will be set beforehand.
		///  To prevent the code (or database) from auto-assigning a new key, this parameter should be set.
		/// </param>
		public static void WriteObject(T value, bool overrideAutoNumber) {
			if (value == null)
				throw new ArgumentNullException("value");

			if (value.IsDeleted)
				throw new InvalidOperationException(Resources.CannotSaveDeletedObject);

			// If the value is not new, and none of the values has changed, there is nothing we
			// should do.
			if (!value.IsNew && !value.IsDirty)
				return;

			// Should we update the primary key? Yes, if the value is new and the data object has a primary key
			// which is auto-numbered (IDENTITY in SQL).
			bool updatePrimaryKey = value.IsNew && DataObjectInfo<T>.HasPrimaryKeyWithAutoNumber();

			// Make sure the overrideAutoNumber parameter is set only if the code would attempt 
			// to update the primary key.
			if (overrideAutoNumber && !updatePrimaryKey)
				throw new InvalidOperationException(Resources.CannotOverridePrimaryKey);

			// If overrideAutoNumber is set, we don't update the primary key -- obviously!
			if (overrideAutoNumber)
				updatePrimaryKey = false;

			// Should we generate a new, random key?
			bool generateRandomKey = updatePrimaryKey && PrefB.RandomKeys;
			if (generateRandomKey) {
				int key = MiscData.GetKey(DataObjectInfo<T>.GetTableName(), DataObjectInfo<T>.GetPrimaryKeyFieldName());
				DataObjectInfo<T>.SetPrimaryKey(value, key);

				// The primary key as already been updated. No need to retrieve it from the database.
				updatePrimaryKey = false;
			}

			if (!RemotingClient.OpenDentBusinessIsLocal) {
				DtoObjectInsertedAck ack = (DtoObjectInsertedAck)FactoryClient<T>.SendRequest("WriteObject", value, new object[] { overrideAutoNumber });
				DataObjectInfo<T>.SetPrimaryKeys(value, ack.PrimaryKeys);
				value.OnSaved(EventArgs.Empty);
				return;
			}

			Collection<DataFieldInfo> dataFields = DataObjectInfo<T>.GetDataFields(DataFieldMask.Data);
			Collection<DataFieldInfo> primaryKeyFields = DataObjectInfo<T>.GetDataFields(DataFieldMask.PrimaryKey);
			Collection<DataFieldInfo> allFields = DataObjectInfo<T>.GetDataFields();

			if (allFields.Count == 0)
				throw new InvalidOperationException(Resources.NoFields);

			// In queries, the first field always is special (because of the use of commas). This helper variable
			// helps us generate correct queries.
			bool isFirstField = true;

			using (IDbConnection connection = DataSettings.GetConnection())
			using (IDbCommand command = connection.CreateCommand()) {
				if (useParameters) {
					// For each field, create a parameter
					foreach (DataFieldInfo dataField in allFields) {
						IDbDataParameter parameter = command.CreateParameter();
						parameter.ParameterName = ParameterPrefix + dataField.DatabaseName;

						// Get the value of the field
						object fieldValue = dataField.Field.GetValue(value);

						// If the value is of type string and the value is null, we replace it
						// by an empty string.
						if (fieldValue == null && dataField.Field.FieldType == typeof(string))
							fieldValue = string.Empty;

						parameter.Value = fieldValue;
						command.Parameters.Add(parameter);
					}
				}

				// Create the SQL query. If it is a new field, create an "INSERT" statement, else
				// an "UPDATE" statement.

				StringBuilder commandTextBuilder = new StringBuilder();
				if (value.IsNew) {
					// Create a new row, using an INSERT statement. 
					// The values to set always include the data values (not part of the PK)
					commandTextBuilder.Append(string.Format("INSERT INTO {0} (", DataObjectInfo<T>.GetTableName()));
					foreach (DataFieldInfo field in dataFields) {
						if (isFirstField)
							isFirstField = false;
						else
							commandTextBuilder.Append(',');

						commandTextBuilder.Append(field.DatabaseName);
					}

					// If the PK is auto-generated, it it shouldn't be included. If the PK is generated by the code
					// (be it that is some external variable or that the PK is a random number generated previously);
					// it should be included
					if (!updatePrimaryKey) {
						foreach (DataFieldInfo primaryKeyField in primaryKeyFields) {
							if (isFirstField)
								isFirstField = false;
							else
								commandTextBuilder.Append(',');

							commandTextBuilder.Append(primaryKeyField.DatabaseName);
						}
					}

					commandTextBuilder.Append(") VALUES (");
					isFirstField = true;

					foreach (DataFieldInfo field in dataFields) {
						if (isFirstField)
							isFirstField = false;
						else
							commandTextBuilder.Append(',');

						if (useParameters) {
							commandTextBuilder.Append(ParameterPrefix + field.DatabaseName);
						}
						else {
							commandTextBuilder.AppendFormat("{0}", POut.PObject(field.Field.GetValue(value)));
						}
					}

					if (!updatePrimaryKey) {
						foreach (DataFieldInfo primaryKeyField in primaryKeyFields) {
							if (isFirstField)
								isFirstField = false;
							else
								commandTextBuilder.Append(',');

							if (useParameters) {
								commandTextBuilder.Append(ParameterPrefix + primaryKeyField.DatabaseName);
							}
							else {
								commandTextBuilder.AppendFormat("{0}", POut.PObject(primaryKeyField.Field.GetValue(value)));
							}
						}
					}

					commandTextBuilder.Append(')');
				}
				else {
					// Update an existing row, using the UPDATE statement.
					// The WHERE clause contains all PK fields, other data fields go directly into
					// the SET clause.
					commandTextBuilder.Append(string.Format("UPDATE {0} SET ", DataObjectInfo<T>.GetTableName()));
					foreach (DataFieldInfo field in dataFields) {
						// If the data field has not changed, we don't need to update it -- obviously
						if (!DataObjectInfo<T>.HasChanged(field, value))
							continue;

						if (isFirstField)
							isFirstField = false;
						else
							commandTextBuilder.Append(',');

						if (useParameters) {
							commandTextBuilder.Append(string.Format("{0} = {1}{0}", field.DatabaseName, ParameterPrefix));
						}
						else {
							commandTextBuilder.Append(string.Format("{0} = {1}", field.DatabaseName, POut.PObject(field.Field.GetValue(value))));
						}
					}

					commandTextBuilder.Append(" WHERE ");

					isFirstField = true;
					foreach (DataFieldInfo field in primaryKeyFields) {
						if (isFirstField)
							isFirstField = false;
						else
							commandTextBuilder.Append(',');

						if (useParameters) {
							commandTextBuilder.Append(string.Format("{0} = {1}{0}", field.DatabaseName, ParameterPrefix));
						}
						else {
							commandTextBuilder.Append(string.Format("{0} = {1}", field.DatabaseName, POut.PObject(field.Field.GetValue(value))));
						}
					}
				}

				command.CommandText = commandTextBuilder.ToString();

				connection.Open();

				// This executes the UPDATE/INSERT command.
				command.ExecuteNonQuery();

				// Update the PK if required
				if (updatePrimaryKey) {
					// This is currently not implemented for Oracle. Open Dental uses a special mechanism, OracleInsertId,
					// but using a SEQUENCE might be better. See http://coldfusion.sys-con.com/read/43794.htm .
					//
					// For MS SQL (not implemented, either), this would be SCOPE_IDENTITY()
					if (DataSettings.DbType != DatabaseType.MySql)
						throw new NotImplementedException();

					command.CommandText = "SELECT LAST_INSERT_ID()";

					// The type returned by command.ExecuteScalar() is System.Int64.
					// We need to cast it to System.Int32, that's what we use here.
					int key = Convert.ToInt32(command.ExecuteScalar());
					DataObjectInfo<T>.SetPrimaryKey(value, key);
				}

				connection.Close();

				// The object has been saved
				value.OnSaved(EventArgs.Empty);
			}
		}

		public static void WriteObjects(Collection<T> values) {
			if (values == null)
				throw new ArgumentNullException("values");

			foreach (T value in values) {
				WriteObject(value);
			}
		}

		public static void DeleteObject(int id) {
			if (!RemotingClient.OpenDentBusinessIsLocal) {
				FactoryClient<T>.SendRequest("DeleteObject", default(T), new object[] { id });
				return;
			}

			string primaryKeyFieldName = DataObjectInfo<T>.GetPrimaryKeyFieldName();
			string tableName = DataObjectInfo<T>.GetTableName();
			string query;

			if (useParameters) {
				query = string.Format("DELETE FROM {0} WHERE {1} = {2}{1}", tableName, primaryKeyFieldName, ParameterPrefix);
			}
			else {
				query = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", tableName, primaryKeyFieldName, POut.PInt(id));
			}

			using (IDbConnection connection = DataSettings.GetConnection())
			using (IDbCommand command = connection.CreateCommand()) {
				if (useParameters) {
					IDataParameter parameter = command.CreateParameter();
					parameter.ParameterName = ParameterPrefix + primaryKeyFieldName;
					parameter.Value = id;
					command.Parameters.Add(parameter);
				}

				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}

		public static void DeleteObject(T value) {
			if (value == null)
				throw new ArgumentNullException("value");

			if (value.IsDeleted)
				throw new InvalidOperationException(Resources.ObjectAlreadyDeleted);

			if (value.IsNew)
				throw new InvalidOperationException(Resources.ObjectNotSaved);

			if (!RemotingClient.OpenDentBusinessIsLocal) {
				FactoryClient<T>.SendRequest("DeleteObject", value, new object[] {});
				value.OnDeleted(EventArgs.Empty);
				return;
			}

			Collection<DataFieldInfo> identityFields = DataObjectInfo<T>.GetDataFields(DataFieldMask.PrimaryKey);

			using (IDbConnection connection = DataSettings.GetConnection())
			using (IDbCommand command = connection.CreateCommand()) {
				StringBuilder commandTextBuilder = new StringBuilder();

				commandTextBuilder.Append(string.Format("DELETE FROM {0} WHERE ", DataObjectInfo<T>.GetTableName()));

				if (useParameters) {
					// For each field, create a parameter
					foreach (DataFieldInfo dataField in identityFields) {
						IDbDataParameter parameter = command.CreateParameter();
						parameter.ParameterName = ParameterPrefix + dataField.DatabaseName;
						parameter.Value = dataField.Field.GetValue(value);
						command.Parameters.Add(parameter);
					}
				}

				for (int i = 0; i < identityFields.Count; i++) {
					if (useParameters) {
						commandTextBuilder.Append(string.Format("{0} = {1}{0}", identityFields[i].DatabaseName, ParameterPrefix));
					}
					else {
						commandTextBuilder.Append(string.Format("{0} = '{1}'", identityFields[i].DatabaseName, POut.PObject(identityFields[i].Field.GetValue(value))));
					}

					if (i != identityFields.Count - 1)
						commandTextBuilder.Append(" AND ");
				}

				command.CommandText = commandTextBuilder.ToString();

				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}

			// The object has been deleted
			value.OnDeleted(EventArgs.Empty);
		}
	}
}
