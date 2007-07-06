using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace OpenDental.DataAccess {
	/// <summary>
	/// The base class for classes that correspond to a table in the database.
	/// </summary>
	/// <remarks>
	///	 <para>
	///	  When serialized, only the fields are serialized. It is important to note that serialization can not,
	///   for example, handle events or call backs. (More info).
	///	 </para>
	/// </remarks>
	public class DataObjectBase : IDataObject, ICloneable {
		public DataObjectBase() {
			// Always assume the object is not created from the database. The DataObjectFactory<T> sets the
			// isNew flag to false if appropriate.
			isNew = true;
			isDirty = true;
		}

		private bool isDirty;
		public bool IsDirty {
			get { return isDirty; }
		}

		private bool isDeleted;
		public bool IsDeleted {
			get { return isDeleted; }
		}

		private bool isNew;
		public bool IsNew {
			get { return isNew; }
			set { isNew = value; }
		}

		protected void MarkDirty() {
			if (isDirty)
				return;

			isDirty = true;

			OnModified(EventArgs.Empty);
		}

		public virtual object Clone() {
			return this.MemberwiseClone();
		}

		public void OnModified(EventArgs e) {
			if (Modified != null)
				Modified(this, e);
		}

		public void OnSaved(EventArgs e) {
			isDirty = false;
			isNew = false;

			if (Saved != null)
				Saved(this, e);
		}

		public void OnDeleted(EventArgs e) {
			isDirty = true;
			isNew = false;
			isDeleted = true;

			if (Deleted != null)
				Deleted(this, e);
		}

		public event EventHandler Saved;
		public event EventHandler Modified;
		public event EventHandler Deleted;


		public XmlSchema GetSchema() {
			return null;
		}

		public void ReadXml(XmlReader reader) {
			// Move to the first value
			reader.Read();

			// Go over all fields
			Type type = GetType();
			while (type != typeof(object)) {
				FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
				foreach (FieldInfo field in fields) {
					if (field.FieldType == typeof(EventHandler))
						continue;

					if (field.FieldType == typeof(TimeSpan)) {
						TimeSpanSerializer serializer = new TimeSpanSerializer();
						field.SetValue(this, serializer.Deserialize(reader));
					}
					else {
						XmlSerializer serializer = new XmlSerializer(field.FieldType);
						field.SetValue(this, serializer.Deserialize(reader));
					}
				}

				type = type.BaseType;
			}
			// Move to the next node
			reader.Read();
		}

		public void WriteXml(XmlWriter writer) {
			// Go over all fields
			Type type = GetType();
			while (type != typeof(object)) {
				FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
				foreach (FieldInfo field in fields) {
					if (field.FieldType == typeof(EventHandler))
						continue;

					if (field.FieldType == typeof(TimeSpan)) {
						TimeSpanSerializer serializer = new TimeSpanSerializer();
						serializer.Serialize(writer, (TimeSpan)field.GetValue(this));
					}
					else {
						XmlSerializer serializer = new XmlSerializer(field.FieldType);
						serializer.Serialize(writer, field.GetValue(this));
					}
				}
				type = type.BaseType;
			}
		}
	}
}
