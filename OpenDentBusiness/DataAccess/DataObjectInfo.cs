using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections.ObjectModel;
using OpenDentBusiness.Properties;

namespace OpenDental.DataAccess
{
	public static class DataObjectInfo<T>
	{
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

		public static Collection<DataFieldInfo> GetDataFields()
		{
			return GetDataFields(DataFieldMask.All);
		}

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

		public static void SetProperty(DataFieldInfo field, T target, object value) {
			Type type = typeof(T);
			string propertyName = field.Field.Name;
			propertyName = char.ToUpper(propertyName[0]) + propertyName.Substring(1, propertyName.Length - 1);
			PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);

			property.SetValue(target, value, null);
		}

		public static bool HasChanged(DataFieldInfo field, T value) {
			Type type = typeof(T);

			string changedFieldName = field.Field.Name + "Changed";
			FieldInfo changedField = type.GetField(changedFieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

			return (bool)changedField.GetValue(value);
		}

		public static DataFieldInfo GetPrimaryKeyField() {
			Collection<DataFieldInfo> dataFields = DataObjectInfo<T>.GetDataFields(DataFieldMask.PrimaryKey);
			if (dataFields.Count != 1)
				throw new InvalidOperationException(Resources.NotASinglePrimaryKey);

			DataFieldInfo primaryKey = dataFields[0];
			if (primaryKey.Field.FieldType != typeof(int))
				throw new InvalidOperationException(Resources.PrimaryKeyNotAnInteger);

			return primaryKey;
		}

		public static string GetPrimaryKeyFieldName()
		{
			DataFieldInfo primaryKey = GetPrimaryKeyField();
			return primaryKey.DatabaseName;
		}

		public static bool HasPrimaryKeyWithAutoNumber() {
			Collection<DataFieldInfo> dataFields = DataObjectInfo<T>.GetDataFields(DataFieldMask.PrimaryKey);
			if (dataFields.Count != 1)
				return false;

			return dataFields[0].AutoNumber;
		}

		public static void SetPrimaryKey(T value, int key) {
			DataFieldInfo primaryKeyField = GetPrimaryKeyField();
			primaryKeyField.Field.SetValue(value, key);
		}

		public static int GetPrimaryKey(T value) {
			DataFieldInfo primaryKeyField = GetPrimaryKeyField();
			return (int)primaryKeyField.Field.GetValue(value);
		}

		public static object[] GetPrimaryKeys(T value) {
			Collection<DataFieldInfo> primaryKeyFields = GetDataFields(DataFieldMask.PrimaryKey);
			object[] values = new object[primaryKeyFields.Count];

			for (int i = 0; i < primaryKeyFields.Count; i++)
				values[i] = primaryKeyFields[i].Field.GetValue(value);

			return values;
		}

		public static void SetPrimaryKeys(T value, object[] keys) {
			Collection<DataFieldInfo> primaryKeyFields = GetDataFields(DataFieldMask.PrimaryKey);

			for (int i = 0; i < primaryKeyFields.Count; i++)
				primaryKeyFields[i].Field.SetValue(value, keys[i]);
		}
	}
}
