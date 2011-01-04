using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace Crud {
	class SchemaTable {
		//This defines a test table from which the schema insert table will be generated.
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
		public DateTime dateTimeColumn;
		///<summary>Column of data type dateTime, used for Date Time Stamp</summary>
		public DateTime dateTimeStampColumn;
		///<summary>Column of data type string, less than 4000 characters.</summary>
		public string shortStringColumn;
		///<summary>Column of data type string, more than 4000 characters, less than 65k characters</summary>
		public string mediumStringColumn;
		///<summary>Column of data type string, more than 65k characters</summary>
		public string longStringColumn;


	}
}
