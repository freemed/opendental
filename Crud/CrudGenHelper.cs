using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using OpenDentBusiness;

namespace Crud {
	public class CrudGenHelper {
		///<summary>Will throw exception if no primary key attribute defined.</summary>
		public static FieldInfo GetPriKey(FieldInfo[] fields,string tableName){
			for(int i=0;i<fields.Length;i++) {
				object[] attributes = fields[i].GetCustomAttributes(typeof(CrudColumnAttribute),true);
				if(attributes.Length!=1) {
					continue;
				}
				if(((CrudColumnAttribute)attributes[0]).IsPriKey) {
					return fields[i];
				}
			}
			throw new ApplicationException("No primary key defined for "+tableName);
		}

		///<summary>The name of the table in the database.  By default, the lowercase name of the class type.</summary>
		public static string GetTableName(Type typeClass) {
			object[] attributes = typeClass.GetCustomAttributes(typeof(CrudTableAttribute),true);
			if(attributes.Length==0) {
				return typeClass.Name.ToLower();
			}
			return((CrudTableAttribute)attributes[0]).TableName;
		}

		public static List<FieldInfo> GetFieldsExceptPriKey(FieldInfo[] fields,FieldInfo priKey) {
			List<FieldInfo> retVal=new List<FieldInfo>();
			for(int i=0;i<fields.Length;i++) {
				if(fields[i].Name!=priKey.Name) {
					retVal.Add(fields[i]);
				}
			}
			return retVal;
		}

		public static EnumCrudSpecialColType GetSpecialType(FieldInfo field) {
			object[] attributes = field.GetCustomAttributes(typeof(CrudColumnAttribute),true);
			if(attributes.Length==0) {
				return EnumCrudSpecialColType.None;
			}
			return ((CrudColumnAttribute)attributes[0]).SpecialType;
		}

	}
}
