using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using Oracle.DataAccess.Client;

namespace OpenDentBusiness {
	///<summary>Hold parameter info in a database independent manner.</summary>
	public class OdSqlParameter {
		private string parameterName;
		private OdDbType dbType;
		private Object value;

		public OdDbType DbType {
			get { return dbType; }
			set { dbType = value; }
		}

		public string ParameterName {
			get { return parameterName; }
			set { parameterName = value; }
		}
	
		public Object Value {
			get { return this.value; }
			set { this.value = value; }
		}

		///<summary>parameterName should include the leading character such as @ or : for now. (although I'm working on a better approach).</summary>
		public OdSqlParameter(string parameterName,OdDbType dbType,Object value) {
			this.parameterName=parameterName;
			this.dbType=dbType;
			this.value=value;
		}

		public MySqlParameter GetMySqlParameter() {
			MySqlParameter param=new MySqlParameter();
			param.ParameterName=this.parameterName;
			switch(this.dbType) {
				case OdDbType.Bool:
					param.MySqlDbType=MySqlDbType.UByte;
					break;
				case OdDbType.Byte:
					param.MySqlDbType=MySqlDbType.UByte;
					break;
				case OdDbType.Currency:
					param.MySqlDbType=MySqlDbType.Double;
					break;
				case OdDbType.Date:
					param.MySqlDbType=MySqlDbType.Date;
					break;
				case OdDbType.DateTime:
					param.MySqlDbType=MySqlDbType.DateTime;
					break;
				case OdDbType.DateTimeStamp:
					param.MySqlDbType=MySqlDbType.Timestamp;
					break;
				case OdDbType.Float:
					param.MySqlDbType=MySqlDbType.Float;
					break;
				case OdDbType.Int:
					param.MySqlDbType=MySqlDbType.Int32;
					break;
				case OdDbType.Long:
					param.MySqlDbType=MySqlDbType.Int64;
					break;
				case OdDbType.Text:
					param.MySqlDbType=MySqlDbType.MediumText;//hope this will work
					break;
				case OdDbType.TimeOfDay:
					param.MySqlDbType=MySqlDbType.Time;
					break;
				case OdDbType.TimeSpan:
					param.MySqlDbType=MySqlDbType.Time;
					break;
				case OdDbType.VarChar255:
					param.MySqlDbType=MySqlDbType.VarChar;
					break;
			}
			return param;
		}

		public OracleDbType GetOracleDbType() {
			switch(this.dbType) {
				case OdDbType.Blob:
					return OracleDbType.Blob;
				case OdDbType.Bool:
					return OracleDbType.Byte;
				case OdDbType.Byte:
					return OracleDbType.Byte;
				case OdDbType.Currency:
					return OracleDbType.Decimal;
				case OdDbType.Date:
					return OracleDbType.Date;
				case OdDbType.DateTime:
					return OracleDbType.Date;
				case OdDbType.DateTimeStamp:
					return OracleDbType.Date;
				case OdDbType.Float:
					return OracleDbType.Double;
				case OdDbType.Int:
					return OracleDbType.Int32;
				case OdDbType.Long:
					return OracleDbType.Int64;
				case OdDbType.Text:
					return OracleDbType.Clob;
				case OdDbType.TimeOfDay:
					return OracleDbType.Date;
				case OdDbType.TimeSpan:
					return OracleDbType.Varchar2;
				case OdDbType.VarChar255:
					return OracleDbType.Varchar2;
				default://should never happen
					return OracleDbType.Char;
			}
		}

		public OracleParameter GetOracleParameter() {
			OracleParameter param=new OracleParameter();
			param.ParameterName=this.parameterName;
			param.OracleDbType=GetOracleDbType();
			return param;
		}

	}


	

	
}
