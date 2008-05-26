using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections.ObjectModel;
using OpenDentBusiness.Properties;

namespace OpenDental.DataAccess
{
	/// <summary>
	/// Exposes various methods for obtaining more information about a <see cref="IDataObject"/> class.
	/// </summary>
	/// <typeparam name="T">The type to obtain information on.</typeparam>
	public static class DataObjectInfo<T>
	{
		/// <summary>
		/// Gets the name of the data table associated with the <see cref="IDataObject"/> class.
		/// </summary>
		/// <returns>The name of the data table associated with the <see cref="IDataObject"/> class.</returns>
		public static string GetTableName()
		{
			Type type = typeof(T);
			object[] attributes = type.GetCustomAttributes(typeof(DataObjectAttribute), true);

			if (attributes.Length == 0)
				throw new InvalidDataObjectException();

			if (attributes.Length > 1)
				throw new InvalidDataObjectException();
			
			return ((DataObjectAttribute)attributes[0]).TableName;
		}

		/// <summary>
		/// Enumerates all data fields contained by the <see cref="IDataObject"/> class.
		/// </summary>
		/// <returns>All data fields contained by the <see cref="IDataObject"/> class.</returns>
		public static Collection<DataFieldInfo> GetDataFields()
		{
			return GetDataFields(DataFieldMask.All);
		}

		/// <summary>
		/// Enumerates certain data fields contained by the <see cref="IDataObject"/> class that, as specified
		/// by the <paramref name="mask"/> parameter.
		/// </summary>
		/// <param name="mask">Specifies which data fields to enumerate.</param>
		/// <returns>Certain data fields contained by the <see cref="IDataObject"/> class that, as specified
		/// by the <paramref name="mask"/> parameter.</returns>
		public static Collection<DataFieldInfo> GetDataFields(DataFieldMask mask)
		{
			Type type = typeof(T);
			Collection<DataFieldInfo> dataFields = new Collection<DataFieldInfo>();

			// Go through a list of all fields
			foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
			{
				object[] attributes = field.GetCustomAttributes(typeof(DataFieldAttribute), true);
				if (attributes.Length == 0)
					continue;

				if (attributes.Length > 1)
					throw new ArgumentOutOfRangeException(Resources.TooManyDataFieldAttributes);

				DataFieldInfo dataField = new DataFieldInfo(field, (DataFieldAttribute)attributes[0]);
				if ((!dataField.PrimaryKey && (mask & DataFieldMask.Data) == DataFieldMask.Data)
					||
					(dataField.PrimaryKey && (mask & DataFieldMask.PrimaryKey) == DataFieldMask.PrimaryKey))
				{
					dataFields.Add(dataField);
				}
			}

			return dataFields;
		}

		/// <summary>
		/// Sets the value of a property on a given <see cref="IDataObject"/>
		/// </summary>
		/// <param name="field">The field whose value should be set.</param>
		/// <param name="target">The object whose values hould be set.</param>
		/// <param name="value">The value to be assigned.</param>
		public static void SetProperty(DataFieldInfo field, T target, object value) {
			Type type = typeof(T);
			string propertyName = field.Field.Name;
			propertyName = char.ToUpper(propertyName[0]) + propertyName.Substring(1, propertyName.Length - 1);
			PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);

			property.SetValue(target, value, null);
		}

		/// <summary>
		/// Gets a value indicating if the value of a certain field on a given object has changed.
		/// </summary>
		/// <param name="field">The field to inspect.</param>
		/// <param name="value">The object to inspect.</param>
		/// <returns><see langword="true"/> if the value of <paramref name="field"/> on object <paramref name="value"/> has
		/// changed, otherwise, <see langword="false"/>.</returns>
		public static bool HasChanged(DataFieldInfo field, T value) {
			Type type = typeof(T);

			string changedFieldName = field.Field.Name + "Changed";
			FieldInfo changedField = type.GetField(changedFieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

			return (bool)changedField.GetValue(value);
		}

		/// <summary>
		/// Gets the <see cref="DataFieldInfo"/> describing the integer primary key of the object.
		/// </summary>
		/// <returns>The <see cref="DataFieldInfo"/> describing the integer primary key of the object.</returns>
		/// <remarks>
		/// An object may, but is not required to, have a single integer primary key field. If this object not has a single integer primary
		/// key field, an <see cref="InvalidOperationException"/> is thrown.
		/// </remarks>
		/// <exception cref="InvalidOperationException">The object does not have a single primary key.</exception>
		/// <exception cref="InvalidOperationException">The object does have a single primary key, but it is not of the <see cref="System.Int32"/> type.</exception>
		public static DataFieldInfo GetPrimaryKeyField() {
			Collection<DataFieldInfo> dataFields = DataObjectInfo<T>.GetDataFields(DataFieldMask.PrimaryKey);
			if (dataFields.Count != 1)
				throw new InvalidOperationException(Resources.NotASinglePrimaryKey);

			DataFieldInfo primaryKey = dataFields[0];
			if (primaryKey.Field.FieldType != typeof(int))
				throw new InvalidOperationException(Resources.PrimaryKeyNotAnInteger);

			return primaryKey;
		}

		/// <summary>
		/// Gets the name of the integer primary key of the object.
		/// </summary>
		/// <returns>The name of the integer primary key of the object.</returns>
		/// <remarks>
		/// An object may, but is not required to, have a single integer primary key field. If this object not has a single integer primary
		/// key field, an <see cref="InvalidOperationException"/> is thrown.
		/// </remarks>
		/// <exception cref="InvalidOperationException">The object does not have a single primary key.</exception>
		/// <exception cref="InvalidOperationException">The object does have a single primary key, but it is not of the <see cref="System.Int32"/> type.</exception>
		public static string GetPrimaryKeyFieldName()
		{
			DataFieldInfo primaryKey = GetPrimaryKeyField();
			return primaryKey.DatabaseName;
		}

		/// <summary>
		/// Gets a value indicating if the object has a primary key of the type integer, which is auto-incremented.
		/// </summary>
		/// <returns><see langword="true"/> if object has a primary key of the type integer, which is auto-incremented, otherwise,
		/// <see langword="false"/>.</returns>
		public static bool HasPrimaryKeyWithAutoNumber() {
			Collection<DataFieldInfo> dataFields = DataObjectInfo<T>.GetDataFields(DataFieldMask.PrimaryKey);
			if (dataFields.Count != 1)
				return false;

			return dataFields[0].AutoNumber;
		}

		/// <summary>
		/// Sets the integer primary key of the object.
		/// </summary>
		/// <param name="value">The object on which to set the primary key.</param>
		/// <param name="key">The value of the primary key.</param>
		/// <remarks>
		/// An object may, but is not required to, have a single integer primary key field. If this object not has a single integer primary
		/// key field, an <see cref="InvalidOperationException"/> is thrown.
		/// </remarks>
		/// <exception cref="InvalidOperationException">The object does not have a single primary key.</exception>
		/// <exception cref="InvalidOperationException">The object does have a single primary key, but it is not of the <see cref="System.Int32"/> type.</exception>
		public static void SetPrimaryKey(T value, int key) {
			DataFieldInfo primaryKeyField = GetPrimaryKeyField();
			primaryKeyField.Field.SetValue(value, key);
		}

		/// <summary>
		/// Gets the integer primary key of the object.
		/// </summary>
		/// <param name="value">The object on which to set the primary key.</param>
		/// <returns>The value of the primary key.</returns>
		/// <remarks>
		/// An object may, but is not required to, have a single integer primary key field. If this object not has a single integer primary
		/// key field, an <see cref="InvalidOperationException"/> is thrown.
		/// </remarks>
		/// <exception cref="InvalidOperationException">The object does not have a single primary key.</exception>
		/// <exception cref="InvalidOperationException">The object does have a single primary key, but it is not of the <see cref="System.Int32"/> type.</exception>
		public static int GetPrimaryKey(T value) {
			DataFieldInfo primaryKeyField = GetPrimaryKeyField();
			return (int)primaryKeyField.Field.GetValue(value);
		}

		/// <summary>
		/// Gets the value of the primary key fields of the object.
		/// </summary>
		/// <param name="value">The object for which to obtain the value of the primary key fields.</param>
		/// <returns>The value of the primary key fields.</returns>
		public static object[] GetPrimaryKeys(T value) {
			Collection<DataFieldInfo> primaryKeyFields = GetDataFields(DataFieldMask.PrimaryKey);
			object[] values = new object[primaryKeyFields.Count];

			for (int i = 0; i < primaryKeyFields.Count; i++)
				values[i] = primaryKeyFields[i].Field.GetValue(value);

			return values;
		}

		/// <summary>
		/// Sets the value of the primary key fields of the object.
		/// </summary>
		/// <param name="value">The object on which to set the value of the primary key fields.</param>
		/// <param name="keys">The value of the primary key fields.</param>
		public static void SetPrimaryKeys(T value, object[] keys) {
			Collection<DataFieldInfo> primaryKeyFields = GetDataFields(DataFieldMask.PrimaryKey);

			for (int i = 0; i < primaryKeyFields.Count; i++)
				primaryKeyFields[i].Field.SetValue(value, keys[i]);
		}
	}
}
