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
			set { isDirty=value; }
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
			if(isDirty)
				return;
			isDirty = true;
			OnModified(EventArgs.Empty);
		}

		///<summary>Deep MemberwiseClone.  The returned type is object.  Many of the DataObjects have a Copy function which returns an object of the correct type.</summary>
		public virtual object Clone() {
			return this.MemberwiseClone();
		}

		public void OnModified(EventArgs e) {
			if(Modified != null)
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
			//while(type != typeof(object)) {
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			string fieldVal;
			foreach (FieldInfo field in fields) {
				if(field.FieldType==typeof(EventHandler)){
					continue;
				}
				if(field.Name.EndsWith("Changed")){
					continue;
				}
				reader.ReadStartElement();
				//note that we are not checking the names of the elements like we probably should.
				fieldVal=reader.ReadElementContentAsString();
				if(fieldVal!=null){
					if(field.FieldType == typeof(TimeSpan)) {
						long ticks = reader.ReadElementContentAsLong();
						field.SetValue(this,TimeSpan.FromTicks(ticks));
						//TimeSpanSerializer serializer = new TimeSpanSerializer();
						//field.SetValue(this, serializer.Deserialize(reader));
					}
					else if(field.FieldType==typeof(DateTime)){
						field.SetValue(this,DateTime.Parse(fieldVal));
					}
					else if(field.FieldType==typeof(string)){
						field.SetValue(this,fieldVal);
					}
					else if(field.FieldType==typeof(int)){
						field.SetValue(this,int.Parse(fieldVal));
					}
					else if(field.FieldType==typeof(double)){
						field.SetValue(this,double.Parse(fieldVal));
					}
					
					//else if(field.FieldType==typeof(Enum)){
					//	field.SetValue(this,double.Parse(fieldVal));
					//}
					
				}
				reader.ReadEndElement();
			}
			reader.Read();
		}

		public void WriteXml(XmlWriter writer) {
			Type type = GetType();
			//writer.WriteStartElement(type.Name);
			//it looks like we will just be serializing the private fields, since the public ones are properties which don't seem to be picked up.
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			object fieldVal;
			foreach (FieldInfo field in fields) {
				if (field.FieldType == typeof(EventHandler))
					continue;
				//We don't want these for now because they take up space
				if(field.Name.EndsWith("Changed")){
					continue;
				}
				writer.WriteStartElement(field.Name);
				fieldVal=field.GetValue(this);
				if(fieldVal!=null){
					if(field.FieldType == typeof(TimeSpan)) {
						writer.WriteValue(((TimeSpan)fieldVal).Ticks);
					}
					else if(field.FieldType==typeof(DateTime)){
						writer.WriteValue((DateTime)fieldVal);
					}
					else{
						writer.WriteValue(fieldVal.ToString());
					}
				}
				writer.WriteEndElement();
			}
			//writer.WriteEndElement();
		}

	}
}
