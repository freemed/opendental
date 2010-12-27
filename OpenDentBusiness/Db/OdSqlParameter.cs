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

		public OracleParameter GetOracleParameter() {
			OracleParameter param=new OracleParameter();
			param.ParameterName=this.parameterName;
			switch(this.dbType) {
				case OdDbType.Bool:
					param.OracleDbType=OracleDbType.Byte;
					break;
				case OdDbType.Byte:
					param.OracleDbType=OracleDbType.Byte;
					break;
				case OdDbType.Currency:
					param.OracleDbType=OracleDbType.Decimal;
					break;
				case OdDbType.Date:
					param.OracleDbType=OracleDbType.Date;
					break;
				case OdDbType.DateTime:
					param.OracleDbType=OracleDbType.Date;
					break;
				case OdDbType.DateTimeStamp:
					param.OracleDbType=OracleDbType.Date;
					break;
				case OdDbType.Float:
					param.OracleDbType=OracleDbType.Double;
					break;
				case OdDbType.Int:
					param.OracleDbType=OracleDbType.Int32;
					break;
				case OdDbType.Long:
					param.OracleDbType=OracleDbType.Int64;
					break;
				case OdDbType.Text:
					param.OracleDbType=OracleDbType.Clob;
					break;
				case OdDbType.TimeOfDay:
					param.OracleDbType=OracleDbType.Date;
					break;
				case OdDbType.TimeSpan:
					param.OracleDbType=OracleDbType.Varchar2;
					break;
				case OdDbType.VarChar255:
					param.OracleDbType=OracleDbType.Varchar2;
					break;
			}
			return param;
		}

	}


	

	
}
