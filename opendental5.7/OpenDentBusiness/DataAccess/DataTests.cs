#if NUNIT
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenDental.Framework.Data;
using System.Collections.ObjectModel;
using OpenDental;
using OpenDentServer;
using System.ServiceProcess;
using System.Threading;
using OpenDentBusiness.Framework.Remoting;
using System.Reflection;

namespace OpenDental.DataAccess {
	[TestFixture()]
	public class DataTests {
		private const int DatabaseMaxInteger = 65535;

		Random random;
		AppDomain domain;
		ServiceWrapper wrapper;

		[SetUp()]
		public void Intialize() {
			random = new Random();
			StartService();
		}

		[TearDown()]
		public void CleanUp() {
			StopService();
		}

		private void StartService() {
			// Start the OD service in a new app domain
			domain = AppDomain.CreateDomain("ServiceDomain");
			wrapper = (ServiceWrapper)domain.CreateInstanceFromAndUnwrap(Assembly.GetExecutingAssembly().CodeBase, typeof(ServiceWrapper).FullName);
			wrapper.Start();
		}

		private void StopService() {
			wrapper.Stop();
			AppDomain.Unload(domain);
		}

		[Test()]
		public void PatientTest() {
			RemotingClient.OpenDentBusinessIsLocal = true;
			DataObjectFactory<Patient>.UseParameters = false;
			TestTableType<Patient>();
			DataObjectFactory<Patient>.UseParameters = true;
			TestTableType<Patient>();

			RemotingClient.OpenDentBusinessIsLocal = false;
			RemotingClient.ServerName = "localhost";
			RemotingClient.ServerPort = 9390;

			DataObjectFactory<Patient>.UseParameters = false;
			TestTableType<Patient>();
			DataObjectFactory<Patient>.UseParameters = true;
			TestTableType<Patient>();
		}

		private void TestTableType<T>()
			where T : DataObjectBase, new() {

			// Create a new object
			T t = DataObjectFactory<T>.NewObject();
			Assert.IsNotNull(t, "#1 (Object should not be null)");
			Assert.IsTrue(t.IsNew, "#5 (IsNew should be set)");
			Assert.IsFalse(t.IsDeleted, "#6 (IsDeleted should be false)");
			Assert.IsTrue(t.IsDirty, "#7 (IsDirty should be true)");

			// Fill the data
			Collection<DataFieldInfo> dataFields = DataObjectInfo<T>.GetDataFields(DataFieldMask.Data);
			foreach (DataFieldInfo dataField in dataFields) {
				DataObjectInfo<T>.SetProperty(dataField, t, Random(dataField.Field.FieldType));
			}

			// Save the object to the database
			DataObjectFactory<T>.WriteObject(t);

			if (!DataObjectInfo<T>.HasPrimaryKeyWithAutoNumber())
				throw new NotSupportedException();

			int id = DataObjectInfo<T>.GetPrimaryKey(t);
			Assert.AreNotEqual(0, id, "#2 (ID should not be zero)");
			Assert.IsFalse(t.IsDirty, "#8 (IsDirty should be false)");
			Assert.IsFalse(t.IsDeleted, "#9 (IsDeleted should be false)");
			Assert.IsFalse(t.IsNew, "#16 (IsNew should be false)");

			// Load a new object
			T database = DataObjectFactory<T>.CreateObject(id);
			foreach (DataFieldInfo dataField in dataFields) {
				object localValue = dataField.Field.GetValue(t);
				object databaseValue = dataField.Field.GetValue(database);

				// Text strings may be trimmed by the database. For now,
				// ignore that.
				if (dataField.Field.FieldType == typeof(string)) {
					string localText = (string)localValue;
					string databaseText = (string)databaseValue;

					if (databaseText.Length < localText.Length) {
						localValue = localText.Substring(0, databaseText.Length);
					}
				}

				if (dataField.Field.FieldType == typeof(double)) {
					Assert.AreEqual((double)localValue, (double)databaseValue, 0.01, "#3a - " + dataField.Field.Name + " (Value retrieved from database should equal stored value)");
				}
				else if (dataField.Field.FieldType == typeof(float)) {
					Assert.AreEqual((float)localValue, (float)databaseValue, 0.01, "#3b - " + dataField.Field.Name + " (Value retrieved from database should equal stored value)");
				}
				else {
					Assert.AreEqual(localValue, databaseValue, "#3x - " + dataField.Field.Name + " (Value retrieved from database should equal stored value)");
				}
			}

			// Modify the object
			foreach (DataFieldInfo dataField in dataFields) {
				DataObjectInfo<T>.SetProperty(dataField, t, Random(dataField.Field.FieldType));
				Assert.IsTrue(DataObjectInfo<T>.HasChanged(dataField, t), "#11 - " + dataField.Field.Name + " (Field should be marked as dirty)");
			}

			Assert.IsTrue(t.IsDirty, "#12 (Object should be dirty)");

			DataObjectFactory<T>.WriteObject(t);
			Assert.AreEqual(id, DataObjectInfo<T>.GetPrimaryKey(t), "#13 (Id should not change after saving)");
			Assert.IsFalse(t.IsNew, "#14 (IsDirty should not be set");
			Assert.IsFalse(t.IsNew, "#15 (IsNew should not be set");

			database = DataObjectFactory<T>.CreateObject(id);
			foreach (DataFieldInfo dataField in dataFields) {
				object localValue = dataField.Field.GetValue(t);
				object databaseValue = dataField.Field.GetValue(database);

				// Text strings may be trimmed by the database. For now,
				// ignore that.
				if (dataField.Field.FieldType == typeof(string)) {
					string localText = (string)localValue;
					string databaseText = (string)databaseValue;

					if (databaseText.Length < localText.Length) {
						localValue = localText.Substring(0, databaseText.Length);
					}
				}

				if (dataField.Field.FieldType == typeof(double)) {
					Assert.AreEqual((double)localValue, (double)databaseValue, 0.01, "#4a - " + dataField.Field.Name);
				}
				else if (dataField.Field.FieldType == typeof(float)) {
					Assert.AreEqual((float)localValue, (float)databaseValue, 0.01, "#4b - " + dataField.Field.Name);
				}
				else {
					Assert.AreEqual(localValue, databaseValue, "#4x - " + dataField.Field.Name);
				}
			}

			// Delete the object
			DataObjectFactory<T>.DeleteObject(t);
		}

		private object Random(Type dataType) {
			if (dataType == typeof(string)) {
				string value = string.Empty;
				for (int i = 0; i < 20; i++) {
					value += (char)(random.Next(33, 126));
				}
				return value;
			}
			else if (dataType == typeof(bool)) {
				return Convert.ToBoolean(random.Next(0, 1));
			}
			else if (dataType == typeof(Byte)) {
				byte[] buffer = new byte[1];
				random.NextBytes(buffer);
				return buffer[0];
			}
			else if (dataType == typeof(DateTime)) {
				DateTime value = DateTime.Today;
				value = value.AddDays(random.Next(-1000, 1000));
				return value;
			}
			else if (dataType == typeof(TimeSpan)) {
				return new TimeSpan(0, random.Next(0, 60), 0);
			}
			else if (dataType == typeof(double)) {
				return random.NextDouble();
			}
			else if (dataType == typeof(float)) {
				return (float)random.NextDouble();
			}
			else if (dataType == typeof(int)) {
				return random.Next(DatabaseMaxInteger + 1);
			}
			else if (dataType.IsEnum) {
				Array values = Enum.GetValues(dataType);
				int index = random.Next(0, values.Length);
				return values.GetValue(index);
			}
			else {
				throw new NotSupportedException("The type " + dataType.Name + " is not supported");
			}
		}
	}
}
#endif