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
	//[XmlRootAttribute("PurchaseOrder", Namespace="http://www.cpandl.com",IsNullable = false)]
	public class DataObjectBase : IDataObject, ICloneable {
		public DataObjectBase() {
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
			set { isDeleted=value; }
		}

		private bool isNew;
		///<summary>Always assume the object is not created from the database. The DataObjectFactory-T sets the isNew flag to false if appropriate.  If we create our own object from the db, we must set this to false.</summary>
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
			reader.Read();//
			//if(reader.NodeType==XmlNodeType.XmlDeclaration){
			//	reader.Read();
			//}
			//reader.Read();//Main node.  eg. Patient
			Type type = GetType();
			PropertyInfo[] props=type.GetProperties();
			string propVal;
			Type propType;
			foreach (PropertyInfo prop in props) {
				if(prop.Name.EndsWith("Changed")){
					continue;
				}
				//note that we are not checking the names of the elements like we probably should.
				XmlNodeType nodetype=reader.NodeType;
				string nodename=reader.Name;
				propVal=reader.ReadElementContentAsString();
				propType=prop.PropertyType;
				if(propVal!=null){
					try{
						if(propType== typeof(TimeSpan)) {
							prop.SetValue(this,TimeSpan.FromTicks(long.Parse(propVal)),null);
						}
						else if(propType==typeof(DateTime)){
							prop.SetValue(this,DateTime.Parse(propVal),null);
						}
						else if(propType==typeof(string)){
							prop.SetValue(this,propVal,null);
						}
						else if(propType==typeof(int)){
							prop.SetValue(this,int.Parse(propVal),null);
						}
						else if(propType==typeof(double)){
							prop.SetValue(this,double.Parse(propVal),null);
						}
						else if(propType.IsEnum){
							prop.SetValue(this,Enum.Parse(propType,propVal,false),null);
						}
						else if(propType==typeof(bool)){
							prop.SetValue(this,bool.Parse(propVal),null);
						}
						else if(propType==typeof(byte)){
							prop.SetValue(this,byte.Parse(propVal),null);
						}
						else{
							throw new NotImplementedException("DataObjectBase.ReadXml does not yet support this property type: "+propType.ToString());
						}
					}
					catch(Exception e){
						throw new Exception(e.Message+", Property name: "+prop.Name+",  Value: "+propVal);
					}
				}
			}
		}

		public void WriteXml(XmlWriter writer) {
			Type type = GetType();
			PropertyInfo[] props=type.GetProperties();
			object propVal;
			foreach (PropertyInfo prop in props) {
				//We don't want these for now because they take up space
				if(prop.Name.EndsWith("Changed")){
					continue;
				}
				writer.WriteStartElement(prop.Name);
				propVal=prop.GetValue(this,null);
				if(propVal!=null){
					if(prop.PropertyType==typeof(TimeSpan)) {
						writer.WriteValue(((TimeSpan)propVal).Ticks);
					}
					else if(prop.PropertyType==typeof(DateTime)){
						writer.WriteValue((DateTime)propVal);
					}
					else{
						writer.WriteValue(propVal.ToString());
					}
				}
				writer.WriteEndElement();
			}
		}

	}
}
