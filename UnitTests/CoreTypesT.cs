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
			DatabaseTools.SetDbConnection("unittest",isOracle);
			List<DbSchemaCol> cols=new List<DbSchemaCol>();
			cols.Add(new DbSchemaCol("TimeOfDayTest",OdDbType.TimeOfDay));
			cols.Add(new DbSchemaCol("TimeStampTest",OdDbType.DateTimeStamp));
			cols.Add(new DbSchemaCol("DateTest",OdDbType.Date));
			cols.Add(new DbSchemaCol("DateTimeTest",OdDbType.DateTime));
			cols.Add(new DbSchemaCol("TimeSpanTest",OdDbType.TimeSpan));
			cols.Add(new DbSchemaCol("CurrencyTest",OdDbType.Currency));
			cols.Add(new DbSchemaCol("BoolTest",OdDbType.Bool));
			cols.Add(new DbSchemaCol("TextSmallTest",OdDbType.Text,false,TextSizeMySqlOracle.Small,false));
			cols.Add(new DbSchemaCol("VarCharTest",OdDbType.VarChar255));
			cols.Add(new DbSchemaCol("TextLargeTest",OdDbType.Text,false,TextSizeMySqlOracle.Large,false));
			cols.Add(new DbSchemaCol("BlobTest",OdDbType.Blob));
			DbSchema.AddTable7_7("tempcore",cols);
			retVal+="Temp tables created.\r\n";
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
		public static string RunAllMySql() {
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
			retVal+="TimeSpan: Passed.\r\n";
			//timespan, negative------------------------------------------------------------------------------------
			timespan=new TimeSpan(0,-36,0);//This particular timespan value was found to fail in mysql with the old connector.
			//Don't know what's so special about this one value.  There are probably other values failing as well, but it doesn't matter.
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
			//double------------------------------------------------------------------------------------------
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
			retVal+="Double: Passed.\r\n";
			//group_concat------------------------------------------------------------------------------------
			/*
			command="INSERT INTO tempgroupconcat VALUES ('name1')";
			DataCore.NonQ(command);
			command="INSERT INTO tempgroupconcat VALUES ('name2')";
			DataCore.NonQ(command);
			command="SELECT "+DbHelper.GroupConcat("_name")+" allnames FROM tempgroupconcat";
			table=DataCore.GetTable(command);
			string allnames=PIn.ByteArray(table.Rows[0]["allnames"].ToString());
			if(allnames!="name1,name2") {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Group_concat: Passed.\r\n";*/
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
			//SHOW CREATE TABLE -----------------------------------------------------------------------
			//This command is needed in order to perform a backup.
			/*
			command="SHOW CREATE TABLE account";
			table=DataCore.GetTable(command);
			string createResult=PIn.ByteArray(table.Rows[0][1]);
			if(!createResult.StartsWith("CREATE TABLE")) {
				throw new Exception();
			}
			retVal+="SHOW CREATE TABLE: Passed.\r\n";*/
			//Cleanup---------------------------------------------------------------------------------------
			command="DROP TABLE IF EXISTS tempcore";
			DataCore.NonQ(command);
			command="DROP TABLE IF EXISTS tempgroupconcat";
			DataCore.NonQ(command);
			retVal+="CoreTypes test done.\r\n";
			return retVal;
		}

		/// <summary></summary>
		public static string RunAllOracle() {
			string retVal="";
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
			timespan=new TimeSpan(0,-36,0);//Oracle does not seem to like negative values.
			command="INSERT INTO tempcore (TimeSpanTest) VALUES ('"+POut.TSpan(timespan)+"')";
			DataCore.NonQ(command);
			command="SELECT TimeSpanTest FROM tempcore";
			table=DataCore.GetTable(command);
			string timespanstring2=PIn.String(table.Rows[0]["TimeSpanTest"].ToString());
			if(timespan.ToString()!=timespanstring2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="TimeSpan, negative: Passed.\r\n";
			//date----------------------------------------------------------------------------------------------
			DateTime date1;
			DateTime date2;
			date1=new DateTime(2003,5,23);
			command="INSERT INTO tempcore (datetest) VALUES ("+POut.Date(date1)+")";
			DataCore.NonQ(command);
			command="SELECT datetest FROM tempcore";
			table=DataCore.GetTable(command);
			date2=PIn.Date(table.Rows[0]["datetest"].ToString());
			if(date1!=date2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Date: Passed.\r\n";
			DateTime datet1;
			DateTime datet2;
			datet1=new DateTime(2003,5,23,10,18,0);
			command="INSERT INTO tempcore (datetest) VALUES ("+POut.DateT(datet1)+")";
			DataCore.NonQ(command);
			command="SELECT datetest FROM tempcore";
			table=DataCore.GetTable(command);
			datet2=PIn.DateT(table.Rows[0]["datetest"].ToString());
			if(datet1!=datet2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Date/Time: Passed.\r\n";
			double double1;
			double double2;
			double1=12.34d;
			command="INSERT INTO tempcore (doubletest) VALUES ("+POut.Double(double1)+")";
			DataCore.NonQ(command);
			command="SELECT doubletest FROM tempcore";
			table=DataCore.GetTable(command);
			double2=PIn.Double(table.Rows[0]["doubletest"].ToString());
			if(double1!=double2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Double: Passed.\r\n";
			//group_concat------------------------------------------------------------------------------------
			//(Michael) Use RTRIM(REPLACE(REPLACE(XMLAgg(XMLElement("x",column_name)),'<x>'),'</x>',',')) instead of GROUP_CONCAT(column_name) for Oracle.
			command="INSERT INTO tempgroupconcat VALUES ('name1')";
			DataCore.NonQ(command);
			command="INSERT INTO tempgroupconcat VALUES ('name2')";
			DataCore.NonQ(command);
			command="SELECT RTRIM(REPLACE(REPLACE(XMLAgg(XMLElement(\"x\",NAME)),'<x>'),'</x>',',')) allnames FROM tempgroupconcat";
			table=DataCore.GetTable(command);
			string allnames=PIn.ByteArray(table.Rows[0]["allnames"].ToString());
			if(allnames!="name1,name2,") {//RTRIM puts a ',' at the end. Jordan and Michael will look into this. Will come back to later.
				throw new Exception();
			}
			command="DELETE FROM tempgroupconcat";
			DataCore.NonQ(command);
			retVal+="Group_concat: Passed.\r\n";
			//bool,pos------------------------------------------------------------------------------------
			bool bool1;
			bool bool2;
			bool1=true;
			command="INSERT INTO tempcore (booltest) VALUES ("+POut.Bool(bool1)+")";
			DataCore.NonQ(command);
			command="SELECT booltest FROM tempcore";
			table=DataCore.GetTable(command);
			bool2=PIn.Bool(table.Rows[0]["booltest"].ToString());
			if(bool1!=bool2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Bool, true: Passed.\r\n";
			//bool,fal------------------------------------------------------------------------------------
			bool1=false;
			command="INSERT INTO tempcore (booltest) VALUES ("+POut.Bool(bool1)+")";
			DataCore.NonQ(command);
			command="SELECT booltest FROM tempcore";
			table=DataCore.GetTable(command);
			bool2=PIn.Bool(table.Rows[0]["booltest"].ToString());
			if(bool1!=bool2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Bool, false: Passed.\r\n";
			//VARCHAR2(4000)------------------------------------------------------------------------------
			string varchar1=CreateRandomAlphaNumericString(4000); //Tested 4001 and it was too large as expected.
			string varchar2="";
			command="INSERT INTO tempcore (varchar2test) VALUES ('"+POut.String(varchar1)+"')";
			DataCore.NonQ(command);
			command="SELECT varchar2test FROM tempcore";
			table=DataCore.GetTable(command);
			varchar2=PIn.String(table.Rows[0]["varchar2test"].ToString());
			if(varchar1!=varchar2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="VARCHAR2(4000): Passed.\r\n";
			//clob:-----------------------------------------------------------------------------------------
			//tested up to 20MB.  (50MB however was failing: Chunk size error)
			string clobstring1=CreateRandomAlphaNumericString(10485760); //10MB should be larger than anything we store.
			string clobstring2="";
			OdSqlParameter param=new OdSqlParameter(":param1",OdDbType.Text,clobstring1);
			command="INSERT INTO tempcore (clobtest) VALUES (:param1)";
			DataCore.NonQ(command,param);
			command="SELECT clobtest FROM tempcore";
			table=DataCore.GetTable(command);
			clobstring2=PIn.String(table.Rows[0]["clobtest"].ToString());
			if(clobstring1!=clobstring2) {
			  throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Clob: Alpha-Numeric Passed.\r\n";
			//clob:non-standard----------------------------------------------------------------------------------
			//tested up to 20MB.  (50MB however was failing: Chunk size error)
			clobstring1=CreateRandomNonStandardString(10485760); //10MB should be larger than anything we store.
			clobstring2="";
			param=new OdSqlParameter(":param1",OdDbType.Text,clobstring1);
			command="INSERT INTO tempcore (clobtest) VALUES (:param1)";
			DataCore.NonQ(command,param);
			command="SELECT clobtest FROM tempcore";
			table=DataCore.GetTable(command);
			clobstring2=PIn.String(table.Rows[0]["clobtest"].ToString());
			if(clobstring1!=clobstring2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Clob: Non-Standard Passed.\r\n";
			//clob:Rick Roller----------------------------------------------------------------------------------
			//tested up to 20MB.  (50MB however was failing: Chunk size error)
			clobstring1=RickRoller(10485760); //10MB should be larger than anything we store.
			clobstring2="";
			param=new OdSqlParameter(":param1",OdDbType.Text,clobstring1);
			command="INSERT INTO tempcore (clobtest) VALUES (:param1)";
			DataCore.NonQ(command,param);
			command="SELECT clobtest FROM tempcore";
			table=DataCore.GetTable(command);
			clobstring2=PIn.String(table.Rows[0]["clobtest"].ToString());
			if(clobstring1!=clobstring2) {
				throw new Exception();
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Clob: Rick Roller Passed.\r\n";
			//Blob:-----------------------------------------------------------------------------------------
			Image img=null;
			img=Image.FromFile(@"C:\temp\Koala.jpg");
			byte[] rawData;
			byte[] blobTest;
			using(MemoryStream stream=new MemoryStream()) {
				Bitmap bitmap=new Bitmap(img);
				bitmap.Save(stream,ImageFormat.Png);
				rawData=stream.ToArray();
			}
			OdSqlParameter paramBlob=new OdSqlParameter(":param1",OdDbType.Blob,rawData);
			command="INSERT INTO tempcore (blobtest) VALUES (:param1)";
			DataCore.NonQ(command,paramBlob);
			command="SELECT blobtest FROM tempcore";
			table=DataCore.GetTable(command);
			blobTest=(byte[])table.Rows[0]["blobtest"];
			if(blobTest.Length!=rawData.Length){
				throw new Exception();
			}
			for(int i=0;i<rawData.Length;i++) {
				if(blobTest[i]!=rawData[i]) {
					throw new Exception();
				}
			}
			command="DELETE FROM tempcore";
			DataCore.NonQ(command);
			retVal+="Blob: Passed.\r\n";
			retVal+="Oracle CoreTypes test done.\r\n";
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
			string randChrs="Were no strangers to love You know the rules and so do I A full commitments what Im thinking of You wouldnt get this from any other guy I just wanna tell you how Im feeling Gotta make you understand Never gonna give you up Never gonna let you down Never gonna run around and desert you Never gonna make you cry Never gonna say goodbye Never gonna tell a lie and hurt you Weve know each other for so long Your hearts been aching But youre too shy to say it Inside we both know whats been going on We know the game and were gonna play it And if you ask me how Im feeling Dont tell me youre too blind to see Never gonna give you up Never gonna let you down Never gonna run around and desert you Never gonna make you cry Never gonna say goodbye Never gonna tell a lie and hurt you Give you up give you up Give you up give you up Never gonna give Never gonna give give you up Never gonna give Never gonna give give you up I just wanna tell you how Im feeling Gotta make you understand";
			for(int i=0;i<length;i++) {
				result.Append(randChrs[i % randChrs.Length]);
			}
			return result.ToString();
		}

	}
}
