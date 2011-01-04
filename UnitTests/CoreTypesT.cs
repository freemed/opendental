using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using OpenDentBusiness;
using System.IO;
using System.Drawing.Imaging;

namespace UnitTests {
	public class CoreTypesT {
		/// <summary></summary>
		public static string CreateTempTable(bool isOracle) {
			string retVal=""; 
			/*
			DatabaseTools.SetDbConnection("unittest",isOracle);
			List<DbSchemaCol> cols=new List<DbSchemaCol>();
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
			DbSchema.AddTable7_7("tempcore",cols);
			cols=new List<DbSchemaCol>();
			cols.Add(new DbSchemaCol("Names",OdDbType.VarChar255));
			DbSchema.AddTable7_7("tempgroupconcat",cols);
			retVal+="Temp tables created.\r\n";*/
			retVal+="Temp tables cannot yet be created.\r\n";
			return retVal;
		}

		/*
		/// <summary></summary>
		public static string CreateTempTableMySql() {
			string retVal="";
			DatabaseTools.SetDbConnection("unittest",false);
			string command="";
			command="DROP TABLE IF EXISTS tempcore";
			DataCore.NonQ(command);
			command=@"CREATE TABLE tempcore (
			TimeOfDayTest time NOT NULL default '00:00:00',
			TimeStampTest timestamp,
			DateTest date NOT NULL default '0001-01-01',
			DateTimeTest datetime NOT NULL default '0001-01-01 00:00:00',
			TimeSpanTest time NOT NULL default '00:00:00',
			CurrencyTest double NOT NULL,
			BoolTest tinyint NOT NULL,
			TextTest text NOT NULL,
			CharTest char(1) NOT NULL,
			ClobTest mediumtext NOT NULL,
			BlobTest mediumblob NOT NULL
			) DEFAULT CHARSET=utf8";
			DataCore.NonQ(command);
			command="DROP TABLE IF EXISTS tempgroupconcat";
			DataCore.NonQ(command);
			command=@"CREATE TABLE tempgroupconcat (
			_name varchar(255) NOT NULL 
			) DEFAULT CHARSET=utf8";
			DataCore.NonQ(command);
			retVal+="Temp tables created.\r\n";
			return retVal;
		}

		/// <summary></summary>
		public static string CreateTempTableOracle() {
			string retVal="";
			DatabaseTools.SetDbConnection("",true);
			string command="";
			command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE TEMPCORE'; EXCEPTION WHEN OTHERS THEN NULL; END;";
			DataCore.NonQ(command);
			command="CREATE TABLE TEMPCORE "
			+"(TimeOfDayTest TIMESTAMP, "
			+"TimeStampTest DATE, "
			+"DateTest DATE, "
			+"TimeSpanTest VARCHAR2(255), "
			+"DoubleTest FLOAT(24), "
			+"BoolTest NUMBER(3,0), "
			+"Varchar2Test VARCHAR2(4000), "
			+"CharTest CHAR(1), "
			+"ClobTest CLOB, "
			+"BlobTest BLOB)";
			DataCore.NonQ(command);
			command=command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE UNITTEST.TEMPGROUPCONCAT'; EXCEPTION WHEN OTHERS THEN NULL; END;";
			DataCore.NonQ(command);
			command="CREATE TABLE UNITTEST.TEMPGROUPCONCAT " 
			+"(	NAME VARCHAR2(255) NOT NULL ENABLE )";
			DataCore.NonQ(command);
			retVal+="Temp tables created.\r\n";
			return retVal;
		}*/

		/// <summary></summary>
		public static string RunAll() {
			string retVal="";
			//Things that we might later add to this series of tests:
			//Foreign language testing (utf8)
			//Special column types such as timestamp
			//Computer set to other region, affecting string parsing of types such dates and decimals
			//Test types without casting back and forth to strings.
			//Retrieval using a variety of techniques, such as getting a table, scalar, and reading a row.
			//Blobs
			string command="";
			DataTable table;
			TimeSpan timespan;
			TimeSpan timespan2;
			//timespan(timeOfDay)----------------------------------------------------------------------------------------------
			timespan=new TimeSpan(1,2,3);//1hr,2min,3sec
			command="INSERT INTO tempcore (TimeOfDayTest) VALUES ("+POut.Time(timespan)+")";
			DataCore.NonQ(command);
			command="SELECT TimeOfDayTest FROM tempcore";
			table=DataCore.GetTable(command);
			timespan2=PIn.TimeSpan(table.Rows[0]["TimeOfDayTest"].ToString());
			if(timespan!=timespan2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="TimeSpan (time of day): Passed.\r\n";
			//timespan, negative------------------------------------------------------------------------------------
			timespan=new TimeSpan(0,-36,0);//This particular timespan value was found to fail in mysql with the old connector.
			//Don't know what's so special about this one value.  There are probably other values failing as well, but it doesn't matter.
			//Oracle does not seem to like negative values.
			command="INSERT INTO tempcore (TimeSpanTest) VALUES ('"+POut.TSpan(timespan)+"')";
			DataCore.NonQ(command);
			command="SELECT TimeSpanTest FROM tempcore";
			table=DataCore.GetTable(command);
			timespan2=PIn.TimeSpan(table.Rows[0]["TimeSpanTest"].ToString());
			if(timespan!=timespan2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="TimeSpan, negative: Passed.\r\n";
			//timespan, over 24 hours-----------------------------------------------------------------------------
			timespan=new TimeSpan(432,5,17);
			command="INSERT INTO tempcore (TimeSpanTest) VALUES ('"+POut.TSpan(timespan)+"')";
			DataCore.NonQ(command);
			command="SELECT TimeSpanTest FROM tempcore";
			table=DataCore.GetTable(command);
			timespan2=PIn.TimeSpan(table.Rows[0]["TimeSpanTest"].ToString());
			if(timespan!=timespan2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="TimeSpan, large: Passed.\r\n";
			//date----------------------------------------------------------------------------------------------
			DateTime date1;
			DateTime date2;
			date1=new DateTime(2003,5,23);
			command="INSERT INTO tempcore (DateTest) VALUES ("+POut.Date(date1)+")";
			DataCore.NonQ(command);
			command="SELECT DateTest FROM tempcore";
			table=DataCore.GetTable(command);
			date2=PIn.Date(table.Rows[0]["DateTest"].ToString());
			if(date1!=date2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Date: Passed.\r\n";
			//datetime------------------------------------------------------------------------------------------
			DateTime datet1;
			DateTime datet2;
			datet1=new DateTime(2003,5,23,10,18,0);
			command="INSERT INTO tempcore (DateTimeTest) VALUES ("+POut.DateT(datet1)+")";
			DataCore.NonQ(command);
			command="SELECT DateTimeTest FROM tempcore";
			table=DataCore.GetTable(command);
			datet2=PIn.DateT(table.Rows[0]["DateTimeTest"].ToString());
			if(datet1!=datet2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Date/Time: Passed.\r\n";
			//currency------------------------------------------------------------------------------------------
			double double1;
			double double2;
			double1=12.34d;
			command="INSERT INTO tempcore (CurrencyTest) VALUES ("+POut.Double(double1)+")";
			DataCore.NonQ(command);
			command="SELECT CurrencyTest FROM tempcore";
			table=DataCore.GetTable(command);
			double2=PIn.Double(table.Rows[0]["CurrencyTest"].ToString());
			if(double1!=double2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Currency: Passed.\r\n";
			//group_concat------------------------------------------------------------------------------------
			command="INSERT INTO tempgroupconcat VALUES ('name1')";
			DataCore.NonQ(command);
			command="INSERT INTO tempgroupconcat VALUES ('name2')";
			DataCore.NonQ(command);
			command="SELECT "+DbHelper.GroupConcat("Names")+" allnames FROM tempgroupconcat";
			table=DataCore.GetTable(command);
			string allnames=PIn.ByteArray(table.Rows[0]["allnames"].ToString());
			//if(DataConnection.DBtype==DatabaseType.Oracle) {
			//	allnames=allnames.TrimEnd(',');//known issue.  Should already be fixed:
				//Use RTRIM(REPLACE(REPLACE(XMLAgg(XMLElement("x",column_name)),'<x>'),'</x>',','))
			//}
			if(allnames!="name1,name2") {
				throw new Exception();
			}
			command="DELETE FROM tempgroupconcat";
			DataCore.NonQ(command);
			retVal+="Group_concat: Passed.\r\n";
			//bool,pos------------------------------------------------------------------------------------
			bool bool1;
			bool bool2;
			bool1=true;
			command="INSERT INTO tempcore (BoolTest) VALUES ("+POut.Bool(bool1)+")";
			DataCore.NonQ(command);
			command="SELECT BoolTest FROM tempcore";
			table=DataCore.GetTable(command);
			bool2=PIn.Bool(table.Rows[0]["BoolTest"].ToString());
			if(bool1!=bool2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Bool, true: Passed.\r\n";
			//bool,neg------------------------------------------------------------------------------------
			bool1=false;
			command="INSERT INTO tempcore (BoolTest) VALUES ("+POut.Bool(bool1)+")";
			DataCore.NonQ(command);
			command="SELECT BoolTest FROM tempcore";
			table=DataCore.GetTable(command);
			bool2=PIn.Bool(table.Rows[0]["BoolTest"].ToString());
			if(bool1!=bool2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Bool, false: Passed.\r\n";
			//VARCHAR2(4000)------------------------------------------------------------------------------
			string varchar1=CreateRandomAlphaNumericString(4000); //Tested 4001 and it was too large as expected.
			string varchar2="";
			command="INSERT INTO tempcore (TextSmallTest) VALUES ('"+POut.String(varchar1)+"')";
			DataCore.NonQ(command);
			command="SELECT TextSmallTest FROM tempcore";
			table=DataCore.GetTable(command);
			varchar2=PIn.String(table.Rows[0]["TextSmallTest"].ToString());
			if(varchar1!=varchar2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="VarChar2(4000): Passed.\r\n";
			//clob:-----------------------------------------------------------------------------------------
			//tested up to 20MB.  (50MB however was failing: Chunk size error)
			//mysql mediumtext maxes out at about 16MB.
			string clobstring1=CreateRandomAlphaNumericString(10485760); //10MB should be larger than anything we store.
			string clobstring2="";
			OdSqlParameter param=new OdSqlParameter("param1",OdDbType.Text,clobstring1);
			command="INSERT INTO tempcore (TextLargeTest) VALUES ("+DbHelper.ParamChar+"param1)";
			DataCore.NonQ(command,param);
			command="SELECT TextLargeTest FROM tempcore";
			table=DataCore.GetTable(command);
			clobstring2=PIn.String(table.Rows[0]["TextLargeTest"].ToString());
			if(clobstring1!=clobstring2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Clob, Alpha-Numeric 10MB: Passed.\r\n";
			//clob:non-standard----------------------------------------------------------------------------------
			clobstring1=CreateRandomNonStandardString(8000000); //8MB is the max because many chars takes 2 bytes, and mysql maxes out at 16MB
			clobstring2="";
			param=new OdSqlParameter("param1",OdDbType.Text,clobstring1);
			command="INSERT INTO tempcore (TextLargeTest) VALUES ("+DbHelper.ParamChar+"param1)";
			DataCore.NonQ(command,param);
			command="SELECT TextLargeTest FROM tempcore";
			table=DataCore.GetTable(command);
			clobstring2=PIn.String(table.Rows[0]["TextLargeTest"].ToString());
			if(clobstring1!=clobstring2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Clob, Symbols and Chinese: Passed.\r\n";
			//clob:Rick Roller----------------------------------------------------------------------------------
			clobstring1=RickRoller(10485760); //10MB should be larger than anything we store.
			clobstring2="";
			param=new OdSqlParameter("param1",OdDbType.Text,clobstring1);
			command="INSERT INTO tempcore (TextLargeTest) VALUES ("+DbHelper.ParamChar+"param1)";
			DataCore.NonQ(command,param);
			command="SELECT TextLargeTest FROM tempcore";
			table=DataCore.GetTable(command);
			clobstring2=PIn.String(table.Rows[0]["TextLargeTest"].ToString());
			if(clobstring1!=clobstring2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Clob, Rick Roller: Passed.\r\n";
			//SHOW CREATE TABLE -----------------------------------------------------------------------
			//This command is needed in order to perform a backup.
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="SHOW CREATE TABLE account";
				table=DataCore.GetTable(command);
				string createResult=PIn.ByteArray(table.Rows[0][1]);
				if(!createResult.StartsWith("CREATE TABLE")) {
					throw new Exception();
				}
				retVal+="SHOW CREATE TABLE: Passed.\r\n";
			}
			else {
				retVal+="SHOW CREATE TABLE: Not applicable to Oracle.\r\n";
			}
			//Cleanup---------------------------------------------------------------------------------------
			//DbSchema.DropTable7_7("tempcore");
			//DbSchema.DropTable7_7("tempgroupconcat");
			retVal+="CoreTypes test done.\r\n";
			return retVal;
		}

		public static string CreateRandomAlphaNumericString(int length){
			StringBuilder result=new StringBuilder(length);
			Random rand=new Random();
			string randChrs="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			for(int i=0;i<length;i++){
				result.Append(randChrs[rand.Next(0,randChrs.Length-1)]);
			}
			return result.ToString();
		}

		public static string CreateRandomNonStandardString(int length) {
			StringBuilder result=new StringBuilder(length);
			Random rand=new Random();
			string randChrs="'!@#$%^&*()-+[{]}\\`~,<.>/?'\";:=_是像电子和质子这样的亚原子粒子之间的产生排斥力和吸引";
			for(int i=0;i<length;i++) {
				result.Append(randChrs[rand.Next(0,randChrs.Length-1)]);
			}
			return result.ToString();
		}

		public static string RickRoller(int length) {
			StringBuilder result=new StringBuilder(length);
			Random rand=new Random();
			string randChrs="I just couldn't take it anymore.  Kept getting the d--- song stuck in my head.";
			for(int i=0;i<length;i++) {
				result.Append(randChrs[i % randChrs.Length]);
			}
			return result.ToString();
		}

	}
}
