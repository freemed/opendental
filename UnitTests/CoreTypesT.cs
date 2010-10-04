using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class CoreTypesT {
		/// <summary></summary>
		public static string CreateTempTable() {
			string retVal="";
			DatabaseTools.SetDbConnection("unittest");
			string command="";
			command="DROP TABLE IF EXISTS tempcore";
			DataCore.NonQ(command);
			command=@"CREATE TABLE tempcore (
				_timespan time NOT NULL default '00:00:00',
				_date date NOT NULL default '0001-01-01',
				_datetime datetime NOT NULL default '0001-01-01 00:00:00',
				_double double NOT NULL
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
		public static string RunAll() {
			//Things that we might later add to this series of tests:
			//Foreign language testing (utf8)
			//Special column types such as timestamp
			//Computer set to other region, affecting string parsing of types such dates and decimals
			//Test types without casting back and forth to strings.
			//Retrieval using a variety of techniques, such as getting a table, scalar, and reading a row.
			//Blobs
			string retVal="";
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





			command="DROP TABLE IF EXISTS tempcore";
			DataCore.NonQ(command);
			command="DROP TABLE IF EXISTS tempgroupconcat";
			DataCore.NonQ(command);
			retVal+="CoreTypes test done.\r\n";
			return retVal;
		}
	}
}
