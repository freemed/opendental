using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using OpenDentBusiness;

namespace Crud {
	///<summary>See UnitTests.SchemaT.cs.  This class generates the SchemaCrudTest file.</summary>
	public class CrudSchemaForUnitTest {
		public static string Create() {
			StringBuilder strb=new StringBuilder();
			//This is a stub that is to be replaced with some good code generation:
			strb.Append(@"using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Please ignore this class.  It's used only for testing.</summary>
	public class SchemaCrudTest {
		///<summary>Example only</summary>
		public static void AddTableTempcore() {
			string command="""";");
			Type typeClass=typeof(SchemaTable);
			FieldInfo[] fields=typeClass.GetFields();
			FieldInfo priKey=CrudGenHelper.GetPriKey(fields,typeClass.Name);
			List<FieldInfo> fieldsExceptPri=CrudGenHelper.GetFieldsExceptPriKey(fields,priKey);
			List<DbSchemaCol> cols=CrudQueries.GetListColumns(priKey.Name,null,fieldsExceptPri,false);
			CrudSchemaRaw.AddTable("tempcore",cols,3);
			/*
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command=""DROP TABLE IF EXISTS tempcore"";
				Db.NonQ(command);
				command=@""CREATE TABLE tempcore (
					TimeOfDayTest time NOT NULL default '00:00:00',
					TimeStampTest timestamp,
					DateTest date NOT NULL default '0001-01-01',
					DateTimeTest datetime NOT NULL default '0001-01-01 00:00:00',
					TimeSpanTest time NOT NULL default '00:00:00',
					CurrencyTest double NOT NULL,
					BoolTest tinyint NOT NULL,
					TextSmallTest text NOT NULL,
					TextMediumTest text NOT NULL,
					TextLargeTest mediumtext NOT NULL,
					varCharTest varchar(255) NOT NULL
					) DEFAULT CHARSET=utf8"";
				Db.NonQ(command);
			}
			else {//oracle
				try {
					command=""DROP TABLE tempcore"";
					Db.NonQ(command);
				}
				catch(Exception e) {
				}
				command=@""CREATE TABLE tempcore (
					TimeOfDayTest date,
					TimeStampTest timestamp,
					DateTest date,
					DateTimeTest date,
					TimeSpanTest date,
					CurrencyTest number(20),
					BoolTest number(3),
					TextSmallTest varchar2(4000),
					TextMediumTest clob,
					TextLargeTest clob,
					VarCharTest varchar2(255)
					)"";
				Db.NonQ(command);
				command=@""ALTER TABLE tempcore MODIFY(
					TimeOfDayTest NOT NULL,
					DateTest NOT NULL,
					DateTimeTest NOT NULL,
					TimeSpanTest NOT NULL,
					CurrencyTest NOT NULL,
					BoolTest NOT NULL
					)"";
				Db.NonQ(command);
				command=@""ALTER TABLE tempcore MODIFY(
					TimeOfDayTest default TO_DATE('0001-01-01','YYYY-MM-DD'),
					TimeStampTest default TO_DATE('0001-01-01','YYYY-MM-DD'),
					DateTest default TO_DATE('0001-01-01','YYYY-MM-DD'),
					DateTimeTest default TO_DATE('0001-01-01','YYYY-MM-DD'),
					TimeSpanTest default TO_DATE('0001-01-01','YYYY-MM-DD'),
					CurrencyTest default 0,
					BoolTest default 0
					)"";
				Db.NonQ(command);
			}*/
			strb.Append(@"
		}

		///<summary>Example only</summary>
		public static void AddColumnEndClob() {
			string command="""";");
			DbSchemaCol col=new DbSchemaCol("ColEndClob",OdDbType.Text,TextSizeMySqlOracle.Medium);
			strb.Append("\r\n"+CrudSchemaRaw.AddColumnEnd("tempcore",col,3));
			strb.Append(@"
		}

		///<summary>Example only</summary>
		public static void AddColumnEndInt() {
			string command="""";");
			col = new DbSchemaCol("ColEndInt",OdDbType.Int);
			strb.Append("\r\n"+CrudSchemaRaw.AddColumnEnd("tempcore",col,3));
			strb.Append(@"
		}

		//AddColumnEndTimeStamp
		//AddIndex
		//AddColumnAfter
		//DropColumn
		//DropColumnTimeStamp
		//DropIndex
		//AddTable
		//etc.


	}
}");
			return strb.ToString();
		}



	}
}
