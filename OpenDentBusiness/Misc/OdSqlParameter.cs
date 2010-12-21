using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {

	public class OdSqlParameter {

		///<summary>parameterName should not include the leading character such as @ or :.</summary>
		public OdSqlParameter(string parameterName) {

		}
	}

	public enum OdDbType{
		///<summary>C#:bool, MySql:tinyint(3)or(1), Oracle:number(3), </summary>
		Bool,
		///<summary>C#:byte, MySql:tinyint, Oracle:number(), Range:0-255.</summary>
		Byte,
		///<summary>C#:double, MySql:double, Oracle:number(38,8), Need to change C# type to Decimal.  Need to change MySQL type.</summary>
		Currency,
		///<summary>C#:DateTime, MySql:date, Oracle:date, 0000-00-00 not allowed in Oracle and causes problems in MySql</summary>
		Date,
		///<summary>C#:DateTime, MySql:datetime, Oracle:date, </summary>
		DateTime,
//todo: Research existence of triggers
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
//todo: Jordan: assign CrudSpecialColType to all TimeSpans that require negatives. Jason: use list to edit schema file.
		///<summary>C#:TimeSpan, MySql:time, Oracle:varchar2, Range:Pos or neg spans of many days.  Oracle has no such type.</summary>
		TimeSpan,
		///<summary>C#:string, MySql:varchar(255), Oracle:varchar2(255), MaxSize:255</summary>
		VarChar255
	}
}
