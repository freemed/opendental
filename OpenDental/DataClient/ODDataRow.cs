using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using OpenDentBusiness;
using OpenDentBusiness.DataAccess;

namespace OpenDental {
	public class ODDataRow:Dictionary<string,string>{
		public string this[int index]{
      get{
				return this.ElementAt(index).Value;
      }
		}

		public Object ToObject(Type objectType) {
			ConstructorInfo constructor=objectType.GetConstructor(System.Type.EmptyTypes);
			Object obj=constructor.Invoke(null);
			if(objectType.BaseType==typeof(DataObjectBase)){
				PropertyInfo[] propertyInfo=objectType.GetProperties();
				for(int f=0;f<propertyInfo.Length;f++){
					if(propertyInfo[f].Name.EndsWith("Changed")){
						continue;
					}
					if(propertyInfo[f].Name=="IsDirty" || propertyInfo[f].Name=="IsDeleted" || propertyInfo[f].Name=="IsNew"){
						continue;
					}
					if(propertyInfo[f].PropertyType==typeof(int)){
						propertyInfo[f].SetValue(obj,PIn.PInt(this[propertyInfo[f].Name]),null);
					}
					else if(propertyInfo[f].PropertyType==typeof(bool)){
						propertyInfo[f].SetValue(obj,PIn.PBool(this[propertyInfo[f].Name]),null);
					}
					else if(propertyInfo[f].PropertyType==typeof(string)){
						propertyInfo[f].SetValue(obj,PIn.PString(this[propertyInfo[f].Name]),null);
					}
					else if(propertyInfo[f].PropertyType==typeof(DateTime)){
						//since this does not differentiate between date and datetime, there is potential for bugs.
						propertyInfo[f].SetValue(obj,PIn.PDateT(this[propertyInfo[f].Name]),null);
					}
					else if(propertyInfo[f].PropertyType.IsEnum){
						object val=((object[])Enum.GetValues(propertyInfo[f].PropertyType))[PIn.PInt(this[propertyInfo[f].Name])];
						propertyInfo[f].SetValue(obj,val,null);
					}
					else if(propertyInfo[f].PropertyType==typeof(double)){
						propertyInfo[f].SetValue(obj,PIn.PDouble(this[propertyInfo[f].Name]),null);
					}
					else if(propertyInfo[f].PropertyType==typeof(TimeSpan)){
						propertyInfo[f].SetValue(obj,PIn.PTimeSpan(this[propertyInfo[f].Name]),null);
					}
					else if(propertyInfo[f].PropertyType==typeof(Byte)){
						propertyInfo[f].SetValue(obj,PIn.PByte(this[propertyInfo[f].Name]),null);
					}
					else{
						throw new System.NotImplementedException();
					}
				}
			}
			else{
				FieldInfo[] fieldInfo=objectType.GetFields();
				for(int f=0;f<fieldInfo.Length;f++){
					if(fieldInfo[f].FieldType==typeof(int)){
						fieldInfo[f].SetValue(obj,PIn.PInt(this[f]));
					}
					else if(fieldInfo[f].FieldType==typeof(bool)){
						fieldInfo[f].SetValue(obj,PIn.PBool(this[f]));
					}
					else if(fieldInfo[f].FieldType==typeof(string)){
						fieldInfo[f].SetValue(obj,PIn.PString(this[f]));
					}
					else if(fieldInfo[f].FieldType.IsEnum){
						object val=((object[])Enum.GetValues(fieldInfo[f].FieldType))[PIn.PInt(this[f])];
						fieldInfo[f].SetValue(obj,val);
					}
					else{
						throw new System.NotImplementedException();
					}
				}
			}
			return obj;
		}

	}
}
