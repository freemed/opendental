using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace Crud {
	class SchemaTable {
/*		
			//This defines a test table from which the schema insert table will be generated.
			cols.Add(new DbSchemaCol("TimeOfDayTest",OdDbType.TimeOfDay));
			cols.Add(new DbSchemaCol("TimeStampTest",OdDbType.DateTimeStamp));
			cols.Add(new DbSchemaCol("DateTest",OdDbType.Date));
			cols.Add(new DbSchemaCol("DateTimeTest",OdDbType.DateTime));
			cols.Add(new DbSchemaCol("TimeSpanTest",OdDbType.TimeSpan));
			cols.Add(new DbSchemaCol("CurrencyTest",OdDbType.Currency));
			cols.Add(new DbSchemaCol("BoolTest",OdDbType.Bool));
			cols.Add(new DbSchemaCol("TextSmallTest",OdDbType.Text,false,TextSizeMySqlOracle.Small,false));//<4k
			cols.Add(new DbSchemaCol("VarCharTest",OdDbType.VarChar255));
			cols.Add(new DbSchemaCol("TextLargeTest",OdDbType.Text,false,TextSizeMySqlOracle.Large,false));//>65k
*/
		///<summary>Column of data type TimeOfDayTest</summary>
		public DateTime TimeOfDayTest;
		///<summary>Column of data type byte</summary>
//		[CrudColumn(CrudSpecialColType=CrudSpecialColType.TimeStamp)]
		public DateTime TimeStampTest;
		///<summary>Column of data type byte</summary>
		public DateTime DateTest;
		///<summary>Column of data type byte</summary>
//		[CrudColumn(CrudSpecialColType=CrudSpecialColType.DateT)]
		public DateTime DateTimeTest;
		///<summary>Column of data type byte</summary>
//		[CrudColumn(CrudSpecialColType=CrudSpecialColType.TimeSpanNeg)]
		public TimeSpan TimeSpanTest;
		///<summary>Column of data type byte</summary>
		public double CurrencyTest;
		///<summary>Column of data type byte</summary>
		public bool BoolTest;
		///<summary>Column of data type byte</summary>
		public string TextSmallTest;// <4k
		///<summary>Column of data type byte</summary>
//		[CrudColumn(CrudSpecialColType=CrudSpecialColType.TextIsClob)]
		public string TextMediumTest;// >4k & <65k
		///<summary>Column of data type byte</summary>
//		[CrudColumn(CrudSpecialColType=CrudSpecialColType.TextIsClob)]
		public string TextLargeTest;// >65k
		///<summary>Column of data type byte</summary>
		public string VarCharTest;// <255

/*
		///<summary>Primary key</summary>
		[CrudColumn(IsPriKey=true)]
		public long primaryKeyColumn;
		///<summary>Column of data type bool</summary>
		public bool boolColumn;
		///<summary>Column of data type byte</summary>
		public byte byteColumn;
		///<summary>Column of data type int</summary>
		public int intColumn;
		///<summary>Column of data type long</summary>
		public long longColumn;
		///<summary>Column of data type dateTime, used for storing a date</summary>
		public DateTime dateColumn;
		///<summary>Column of data type dateTime</summary>
		[CrudColumn(CrudSpecialColType=CrudSpecialColType.DateT)]
		public DateTime dateTimeColumn;
		///<summary>Column of data type dateTime, used for Date Time Stamp</summary>
		[CrudColumn(CrudSpecialColType=CrudSpecialColType.TimeStamp)]
		public DateTime dateTimeStampColumn;
		///<summary>Column of data type string, less than 4000 characters.</summary>
		public string shortStringColumn;
		///<summary>Column of data type string, more than 4000 characters, less than 65k characters</summary>
		[CrudColumn(CrudSpecialColType=CrudSpecialColType.TextIsClob)]
		public string mediumStringColumn;
		///<summary>Column of data type string, more than 65k characters</summary>
		[CrudColumn(CrudSpecialColType=CrudSpecialColType.TextIsClob)]
		public string longStringColumn;
*/		

	}
}
