using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class CoreTypesT {
		/// <summary></summary>
		public static string CreateTempTable(bool isOracle) {
			string retVal="";
			if(!isOracle) {
				DatabaseTools.SetDbConnection("unittest",isOracle);
				string command="";
				command="DROP TABLE IF EXISTS tempcore";
				DataCore.NonQ(command);
				command=@"CREATE TABLE tempcore (
				_timespan time NOT NULL default '00:00:00',
				_date date NOT NULL default '0001-01-01',
				_datetime datetime NOT NULL default '0001-01-01 00:00:00',
				_double double NOT NULL,
				_bool tinyint(1) NOT NULL
				) DEFAULT CHARSET=utf8";
				DataCore.NonQ(command);
				command="DROP TABLE IF EXISTS tempgroupconcat";
				DataCore.NonQ(command);
				command=@"CREATE TABLE tempgroupconcat (
				_name varchar(255) NOT NULL 
				) DEFAULT CHARSET=utf8";
				DataCore.NonQ(command);
			}
			else {
				DatabaseTools.SetDbConnection("",isOracle);
				string command="";
				command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE TEMPCORE'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				DataCore.NonQ(command);
				command="CREATE TABLE TEMPCORE "
				+"(TIMETEST TIMESTAMP, "
				+"TIMESTAMPTEST DATE, "
				+"DATETEST DATE, "
				+"DOUBLETEST FLOAT(24), "
				+"BOOLTEST NUMBER(3,0), "
				+"VARCHAR2TEST VARCHAR2(4000), "
				+"CHARTEST CHAR(1), "
				+"CLOBTEST CLOB, "
				+"BLOBTEST BLOB)";
				DataCore.NonQ(command);
				command=command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE UNITTEST.TEMPGROUPCONCAT'; EXCEPTION WHEN OTHERS THEN NULL; END;";
				DataCore.NonQ(command);
				command="CREATE TABLE UNITTEST.TEMPGROUPCONCAT " 
				+"(	NAME VARCHAR2(255) NOT NULL ENABLE )";
				DataCore.NonQ(command);
			}
			retVal+="Temp tables created.\r\n";
			return retVal;
		}

		/// <summary></summary>
		public static string RunAll(bool isOracle) {
			string retVal="";
			if(!isOracle) {
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
				//timespan----------------------------------------------------------------------------------------------
				timespan=new TimeSpan(1,2,3);//1hr,2min,3sec
				command="INSERT INTO tempcore (_timespan) VALUES ("+POut.TimeSpan(timespan)+")";
				DataCore.NonQ(command);
				command="SELECT _timespan FROM tempcore";
				table=DataCore.GetTable(command);
				timespan2=PIn.TimeSpan(table.Rows[0]["_timespan"].ToString());
				if(timespan!=timespan2) {
					throw new Exception();
				}
				command="DELETE FROM tempcore";
				DataCore.NonQ(command);
				retVal+="TimeSpan: Passed.\r\n";
				//timespan, negative------------------------------------------------------------------------------------
				timespan=new TimeSpan(0,-36,0);//This particular timespan value was found to fail in mysql with the old connector.
				//Don't know what's so special about this one value.  There are probably other values failing as well, but it doesn't matter.
				command="INSERT INTO tempcore (_timespan) VALUES ("+POut.TimeSpan(timespan)+")";
				DataCore.NonQ(command);
				command="SELECT _timespan FROM tempcore";
				table=DataCore.GetTable(command);
				timespan2=PIn.TimeSpan(table.Rows[0]["_timespan"].ToString());
				if(timespan!=timespan2) {
					throw new Exception();
				}
				command="DELETE FROM tempcore";
				DataCore.NonQ(command);
				retVal+="TimeSpan, negative: Passed.\r\n";
				//timespan, over 24 hours-----------------------------------------------------------------------------
				timespan=new TimeSpan(432,5,17);
				command="INSERT INTO tempcore (_timespan) VALUES ("+POut.TimeSpan(timespan)+")";
				DataCore.NonQ(command);
				command="SELECT _timespan FROM tempcore";
				table=DataCore.GetTable(command);
				timespan2=PIn.TimeSpan(table.Rows[0]["_timespan"].ToString());
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
				command="INSERT INTO tempcore (_date) VALUES ("+POut.Date(date1)+")";
				DataCore.NonQ(command);
				command="SELECT _date FROM tempcore";
				table=DataCore.GetTable(command);
				date2=PIn.Date(table.Rows[0]["_date"].ToString());
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
				command="INSERT INTO tempcore (_datetime) VALUES ("+POut.DateT(datet1)+")";
				DataCore.NonQ(command);
				command="SELECT _datetime FROM tempcore";
				table=DataCore.GetTable(command);
				datet2=PIn.DateT(table.Rows[0]["_datetime"].ToString());
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
				command="INSERT INTO tempcore (_double) VALUES ("+POut.Double(double1)+")";
				DataCore.NonQ(command);
				command="SELECT _double FROM tempcore";
				table=DataCore.GetTable(command);
				double2=PIn.Double(table.Rows[0]["_double"].ToString());
				if(double1!=double2) {
					throw new Exception();
				}
				command="DELETE FROM tempcore";
				DataCore.NonQ(command);
				retVal+="Double: Passed.\r\n";
				//group_concat------------------------------------------------------------------------------------
				command="INSERT INTO tempgroupconcat VALUES ('name1')";
				DataCore.NonQ(command);
				command="INSERT INTO tempgroupconcat VALUES ('name2')";
				DataCore.NonQ(command);
				command="SELECT GROUP_CONCAT(_name) allnames FROM tempgroupconcat";
				table=DataCore.GetTable(command);
				string allnames=PIn.ByteArray(table.Rows[0]["allnames"].ToString());
				if(allnames!="name1,name2") {
					throw new Exception();
				}
				command="DELETE FROM tempcore";
				DataCore.NonQ(command);
				retVal+="Group_concat: Passed.\r\n";
				//bool,pos------------------------------------------------------------------------------------
				bool bool1;
				bool bool2;
				bool1=true;
				command="INSERT INTO tempcore (_bool) VALUES ("+POut.Bool(bool1)+")";
				DataCore.NonQ(command);
				command="SELECT _bool FROM tempcore";
				table=DataCore.GetTable(command);
				bool2=PIn.Bool(table.Rows[0]["_bool"].ToString());
				if(bool1!=bool2) {
					throw new Exception();
				}
				command="DELETE FROM tempcore";
				DataCore.NonQ(command);
				retVal+="Bool, true: Passed.\r\n";
				//bool,pos------------------------------------------------------------------------------------
				bool1=false;
				command="INSERT INTO tempcore (_bool) VALUES ("+POut.Bool(bool1)+")";
				DataCore.NonQ(command);
				command="SELECT _bool FROM tempcore";
				table=DataCore.GetTable(command);
				bool2=PIn.Bool(table.Rows[0]["_bool"].ToString());
				if(bool1!=bool2) {
					throw new Exception();
				}
				command="DELETE FROM tempcore";
				DataCore.NonQ(command);
				retVal+="Bool, false: Passed.\r\n";
				//SHOW CREATE TABLE -----------------------------------------------------------------------
				//This command is needed in order to perform a backup.
				command="SHOW CREATE TABLE account";
				table=DataCore.GetTable(command);
				string createResult=PIn.ByteArray(table.Rows[0][1]);
				if(!createResult.StartsWith("CREATE TABLE")) {
					throw new Exception();
				}
				retVal+="SHOW CREATE TABLE: Passed.\r\n";





				command="DROP TABLE IF EXISTS tempcore";
				DataCore.NonQ(command);
				command="DROP TABLE IF EXISTS tempgroupconcat";
				DataCore.NonQ(command);
				retVal+="CoreTypes test done.\r\n";
				return retVal;
			}
			else {
				string command="";
				DataTable table;
				TimeSpan timespan;
				TimeSpan timespan2;
				//timespan----------------------------------------------------------------------------------------------
				timespan=new TimeSpan(1,2,3);//1hr,2min,3sec
				command="INSERT INTO tempcore (timetest) VALUES (TO_TIMESTAMP("+POut.TimeSpan(timespan)+",'HH24:MI:SS'))";
				DataCore.NonQ(command);
				command="SELECT TO_CHAR(timetest,'HH24:MI:SS') timetest FROM tempcore";
				table=DataCore.GetTable(command);
				timespan2=PIn.TimeSpan(table.Rows[0]["timetest"].ToString());
				if(timespan!=timespan2) {
					throw new Exception();
				}
				command="DELETE FROM tempcore";
				DataCore.NonQ(command);
				retVal+="TimeSpan: Passed. Although not a true 'timespan' just happens to be a valid 'timestamp'.\r\n";
				/*
				//timespan, negative------------------------------------------------------------------------------------
				timespan=new TimeSpan(0,-36,0);//Oracle does not seem to like negative values.
				//Usually says "Hr between 1-12 (unless you specify 24 then 0-23), min between 1-59 and sec between 1-59
				command="INSERT INTO tempcore (timetest) VALUES (TO_TIMESTAMP("+POut.TimeSpan(timespan)+",'HH24:MI:SS'))";
				DataCore.NonQ(command);
				command="SELECT TO_CHAR(timetest,'HH24:MI:SS') timetest FROM tempcore";
				table=DataCore.GetTable(command);
				timespan2=PIn.TimeSpan(table.Rows[0]["timetest"].ToString());
				if(timespan!=timespan2) {
					throw new Exception();
				}
				command="DELETE FROM tempcore";
				DataCore.NonQ(command);
				retVal+="TimeSpan, negative: Passed.\r\n";
				 */
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
				/*
				//group_concat------------------------------------------------------------------------------------
				//Oracle does not have something that acts like GROUP_CONCAT might look into later.
				command="INSERT INTO tempgroupconcat VALUES ('name1')";
				DataCore.NonQ(command);
				command="INSERT INTO tempgroupconcat VALUES ('name2')";
				DataCore.NonQ(command);
				command="SELECT GROUP_CONCAT(NAME) allnames FROM tempgroupconcat";
				table=DataCore.GetTable(command);
				string allnames=PIn.ByteArray(table.Rows[0]["allnames"].ToString());
				if(allnames!="name1,name2") {
					throw new Exception();
				}
				command="DELETE FROM tempgroupconcat";
				DataCore.NonQ(command);
				retVal+="Group_concat: Passed.\r\n";
				 */
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
				//tested up to 20MB.  50MB however was failing: Chunk size error
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
				retVal+="Clob: Passed.\r\n";
				//clob:foreign----------------------------------------------------------------------------------
				clobstring1="是像电子和质子这样的亚原子粒子之间的产生排斥力和吸引";
				clobstring2="";
				command="INSERT INTO tempcore (clobtest) VALUES ('"+POut.String(clobstring1)+"')";
				DataCore.NonQ(command);
				command="SELECT clobtest FROM tempcore";
				table=DataCore.GetTable(command);
				clobstring2=PIn.String(table.Rows[0]["clobtest"].ToString());
				if(clobstring1!=clobstring2) {
					throw new Exception();
				}
				command="DELETE FROM tempcore";
				DataCore.NonQ(command);
				retVal+="Clob:Foreign Passed.\r\n";
				
				/*
				//Blob:-----------------------------------------------------------------------------------------
				byte[] array1 = new byte[10 * 1024 * 1024];
				byte[] array2;
				//OdSqlParameter param=new OdSqlParameter(":param1",OdDbType.Text,clobstring1);
				command="INSERT INTO tempcore (blobtest) VALUES ("+array1+")";
				DataCore.NonQ(command,param);
				command="SELECT blobtest FROM tempcore";
				table=DataCore.GetTable(command);
				//array2=PIn.ByteArray(table.Rows[0]["blobtest"].ToString());
				//if(array1!=array2) {
				//  throw new Exception();
				//}
				command="DELETE FROM tempcore";
				DataCore.NonQ(command);
				retVal+="Blob: Passed.\r\n";
				 */
				return retVal+="Oracle CoreTypes test done.\r\n";
			}
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
	}
}
