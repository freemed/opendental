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

	public enum OdDbType{
		///<summary>C#:bool, MySql:tinyint(3)or(1), Oracle:number(3), </summary>
		Bool,
		///<summary>C#:byte, MySql:tinyint unsigned, Oracle:number(3), Range:0-255.</summary>
		Byte,
		///<summary>C#:double, MySql:double, Oracle:number(38,8), Need to change C# type to Decimal.  Need to change MySQL type.</summary>
		Currency,
		///<summary>C#:DateTime, MySql:date, Oracle:date, 0000-00-00 not allowed in Oracle and causes problems in MySql</summary>
		Date,
		///<summary>C#:DateTime, MySql:datetime, Oracle:date, </summary>
		DateTime,
//todo: Research existence of triggers.  Jason: We will have to write custom trigger.
		///<summary>C#:DateTime, MySql:timestamp, Oracle:date + trigger, </summary>
		DateTimeStamp,
		///<summary>C#:float, MySql:float, Oracle:number(38,8), </summary>
		Float,
		///<summary>C#:int32, MySql:int,smallint(if careful), Oracle:number(11), Range:-2,147,483,647-2,147,483,647</summary>
		Int,
		///<summary>C#:long, MySql:bigint, Oracle:number(20), Range:–9,223,372,036,854,775,808 to 9,223,372,036,854,775,807</summary>
		Long,
//todo: Make a list of these that can be less than 4000, because they must be set manually in Oracle to varchar2:
		///<summary>C#:string, MySql:text,mediumtext, Oracle:varchar2,clob, Range:256+. MaxSizes: Varies.</summary>
		Text,
		///<summary>C#:TimeSpan, MySql:time, Oracle:date, Range:Valid time of day.</summary>
		TimeOfDay,
//todo: Make a list of these based on CrudSpecialColType.  Used list to edit schema file to change type from date to varchar2.
		///<summary>C#:TimeSpan, MySql:time, Oracle:varchar2, Range:Pos or neg spans of many days.  Oracle has no such type.</summary>
		TimeSpan,
		///<summary>C#:string, MySql:varchar(255), Oracle:varchar2(255), MaxSize:255</summary>
		VarChar255
	}

	
}
