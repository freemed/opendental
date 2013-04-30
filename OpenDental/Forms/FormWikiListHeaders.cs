using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using CodeBase;
using System.Text.RegularExpressions;

namespace OpenDental {
	public partial class FormWikiListHeaders:Form {
		public string WikiListCurName;
		///<summary>Widths must always be specified.  Not optional.</summary>
		private List<WikiListHeaderWidth> TableHeaders;

		public FormWikiListHeaders() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiListHeaders_Load(object sender,EventArgs e) {
			TableHeaders=WikiListHeaderWidths.GetForList(WikiListCurName);
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			for(int i=0;i<TableHeaders.Count;i++) {
				col=new ODGridColumn("",75,true);//blank table-column names. List column names are contained in the cells of the table.
				//if(i==0) {
				//  col.IsEditable=false;//primary key
				//}
				gridMain.Columns.Add(col);
			}
			gridMain.Rows.Clear();
			ODGridRow row0=new ODGridRow();
			ODGridRow row1=new ODGridRow();
			for(int i=0;i<TableHeaders.Count;i++) {
				row0.Cells.Add(TableHeaders[i].ColName);
				row1.Cells.Add(TableHeaders[i].ColWidth.ToString());
			}
			gridMain.Rows.Add(row0);
			gridMain.Rows.Add(row1);
			gridMain.EndUpdate();
		}

		private void butOK_Click(object sender,EventArgs e) {
			//Validate column names---------------------------------------------------------------------------------
			foreach(ODGridCell colNameCell in gridMain.Rows[0].Cells){
				if(Regex.IsMatch(colNameCell.Text,@"^\d")){
					MsgBox.Show(this,"Column cannot start with numbers.");
					return;
				}
				if(Regex.IsMatch(colNameCell.Text,@"\s")) {
					MsgBox.Show(this,"Column names cannot contain spaces.");
					return;
				}
				if(Regex.IsMatch(colNameCell.Text,@"\W")) {
					MsgBox.Show(this,"Column names cannot contain special characters.");
					return;
				}
			}
			//Check for reserved words--------------------------------------------------------------------------------
			foreach(ODGridCell colNameCell in gridMain.Rows[0].Cells){
				if(isReservedWordHelper(colNameCell.Text)){
					MessageBox.Show(Lan.g(this,"Column name is a reserved word in MySQL")+":"+colNameCell.Text);
					return;
				}
				//primary key is caught by duplicate column name logic.
			}
			//Check for duplicates-----------------------------------------------------------------------------------
			List<string> colNamesCheck=new List<string>();
			foreach(ODGridCell colNameCell in gridMain.Rows[0].Cells){
				if(colNamesCheck.Contains(colNameCell.Text)){
					MessageBox.Show(Lan.g(this,"Duplicate column name detected")+":"+colNameCell.Text);
					return;
				}
				colNamesCheck.Add(colNameCell.Text);
			}			
			//Validate column widths---------------------------------------------------------------------------------
			foreach(ODGridCell colWidthCell in gridMain.Rows[1].Cells){
				if(!Regex.IsMatch(colWidthCell.Text,"(\\d)+")){
					MsgBox.Show(this,"Column widths must only contain positive integers.");
					return;
				}
				if(colWidthCell.Text.Contains("-")
					|| colWidthCell.Text.Contains(".")) {
					MsgBox.Show(this,"Column widths must only contain positive integers.");
					return;
				}
			}
			//save values to List<WikiListHeaderWidth> TableHeaders
			for(int i=0;i<TableHeaders.Count;i++) {
				TableHeaders[i].ColName=PIn.String(gridMain.Rows[0].Cells[i].Text);
				TableHeaders[i].ColWidth=PIn.Int(gridMain.Rows[1].Cells[i].Text);
			}
			//Save data to database-----------------------------------------------------------------------------------
			try {
				WikiListHeaderWidths.UpdateNamesAndWidths(WikiListCurName,TableHeaders);
			}
			catch (Exception ex){
				MessageBox.Show(ex.Message);//will throw exception if table schema has changed since the window was opened.
				DialogResult=DialogResult.Cancel;
			}
			DataValid.SetInvalid(InvalidType.Wiki);
			DialogResult=DialogResult.OK;
		}

		///<summary>Returns true if the input string is a reserved word in MySQL 5.5.31.
		///This is a useful tool and could probably be moved somewhere more publicly accessible.</summary>
		public static bool isReservedWordHelper(string input) {
			bool retval;
			//using a switch statement makes this method run in constant time. (As opposed to linear time with a for loop.)
			switch(input.ToUpper()) {
				case "ACCESSIBLE":
				case "ADD":
				case "ALL":
				case "ALTER":
				case "ANALYZE":
				case "AND":
				case "AS":
				case "ASC":
				case "ASENSITIVE":
				case "BEFORE":
				case "BETWEEN":
				case "BIGINT":
				case "BINARY":
				case "BLOB":
				case "BOTH":
				case "BY":
				case "CALL":
				case "CASCADE":
				case "CHANGE":
				case "CHAR":
				case "CHARACTER":
				case "CHECK":
				case "COLLATE":
				case "COLUMN":
				case "CONDITION":
				case "CONSTRAINT":
				case "CONTINUE":
				case "CONVERT":
				case "CREATE":
				case "CROSS":
				case "CURRENT_DATE":
				case "CURRENT_TIME":
				case "CURRENT_TIMESTAMP":
				case "CURRENT_USER":
				case "CURSOR":
				case "DATABASE":
				case "DATABASES":
				case "DAY_HOUR":
				case "DAY_MICROSECOND":
				case "DAY_MINUTE":
				case "DAY_SECOND":
				case "DEC":
				case "DECIMAL":
				case "DECLARE":
				case "DEFAULT":
				case "DELAYED":
				case "DELETE":
				case "DESC":
				case "DESCRIBE":
				case "DETERMINISTIC":
				case "DISTINCT":
				case "DISTINCTROW":
				case "DIV":
				case "DOUBLE":
				case "DROP":
				case "DUAL":
				case "EACH":
				case "ELSE":
				case "ELSEIF":
				case "ENCLOSED":
				case "ESCAPED":
				case "EXISTS":
				case "EXIT":
				case "EXPLAIN":
				case "FALSE":
				case "FETCH":
				case "FLOAT":
				case "FLOAT4":
				case "FLOAT8":
				case "FOR":
				case "FORCE":
				case "FOREIGN":
				case "FROM":
				case "FULLTEXT":
				case "GRANT":
				case "GROUP":
				case "HAVING":
				case "HIGH_PRIORITY":
				case "HOUR_MICROSECOND":
				case "HOUR_MINUTE":
				case "HOUR_SECOND":
				case "IF":
				case "IGNORE":
				case "IN":
				case "INDEX":
				case "INFILE":
				case "INNER":
				case "INOUT":
				case "INSENSITIVE":
				case "INSERT":
				case "INT":
				case "INT1":
				case "INT2":
				case "INT3":
				case "INT4":
				case "INT8":
				case "INTEGER":
				case "INTERVAL":
				case "INTO":
				case "IS":
				case "ITERATE":
				case "JOIN":
				case "KEY":
				case "KEYS":
				case "KILL":
				case "LEADING":
				case "LEAVE":
				case "LEFT":
				case "LIKE":
				case "LIMIT":
				case "LINEAR":
				case "LINES":
				case "LOAD":
				case "LOCALTIME":
				case "LOCALTIMESTAMP":
				case "LOCK":
				case "LONG":
				case "LONGBLOB":
				case "LONGTEXT":
				case "LOOP":
				case "LOW_PRIORITY":
				case "MASTER_SSL_VERIFY_SERVER_CERT":
				case "MATCH":
				case "MAXVALUE":
				case "MEDIUMBLOB":
				case "MEDIUMINT":
				case "MEDIUMTEXT":
				case "MIDDLEINT":
				case "MINUTE_MICROSECOND":
				case "MINUTE_SECOND":
				case "MOD":
				case "MODIFIES":
				case "NATURAL":
				case "NOT":
				case "NO_WRITE_TO_BINLOG":
				case "NULL":
				case "NUMERIC":
				case "ON":
				case "OPTIMIZE":
				case "OPTION":
				case "OPTIONALLY":
				case "OR":
				case "ORDER":
				case "OUT":
				case "OUTER":
				case "OUTFILE":
				case "PRECISION":
				case "PRIMARY":
				case "PROCEDURE":
				case "PURGE":
				case "RANGE":
				case "READ":
				case "READS":
				case "READ_WRITE":
				case "REAL":
				case "REFERENCES":
				case "REGEXP":
				case "RELEASE":
				case "RENAME":
				case "REPEAT":
				case "REPLACE":
				case "REQUIRE":
				case "RESIGNAL":
				case "RESTRICT":
				case "RETURN":
				case "REVOKE":
				case "RIGHT":
				case "RLIKE":
				case "SCHEMA":
				case "SCHEMAS":
				case "SECOND_MICROSECOND":
				case "SELECT":
				case "SENSITIVE":
				case "SEPARATOR":
				case "SET":
				case "SHOW":
				case "SIGNAL":
				case "SMALLINT":
				case "SPATIAL":
				case "SPECIFIC":
				case "SQL":
				case "SQLEXCEPTION":
				case "SQLSTATE":
				case "SQLWARNING":
				case "SQL_BIG_RESULT":
				case "SQL_CALC_FOUND_ROWS":
				case "SQL_SMALL_RESULT":
				case "SSL":
				case "STARTING":
				case "STRAIGHT_JOIN":
				case "TABLE":
				case "TERMINATED":
				case "THEN":
				case "TINYBLOB":
				case "TINYINT":
				case "TINYTEXT":
				case "TO":
				case "TRAILING":
				case "TRIGGER":
				case "TRUE":
				case "UNDO":
				case "UNION":
				case "UNIQUE":
				case "UNLOCK":
				case "UNSIGNED":
				case "UPDATE":
				case "USAGE":
				case "USE":
				case "USING":
				case "UTC_DATE":
				case "UTC_TIME":
				case "UTC_TIMESTAMP":
				case "VALUES":
				case "VARBINARY":
				case "VARCHAR":
				case "VARCHARACTER":
				case "VARYING":
				case "WHEN":
				case "WHERE":
				case "WHILE":
				case "WITH":
				case "WRITE":
				case "XOR":
				case "YEAR_MONTH":
				case "ZEROFILL":
				//New as of mysql 5.5
				case "GENERAL":
				case "IGNORE_SERVER_IDS":
				case "MASTER_HEARTBEAT_PERIOD":
				case "SLOW":
					retval=true;
					break;
				default:
					retval=false;
					break;
			}
			if(Regex.IsMatch(input,WikiListHeaderWidths.dummyColName)) {
				retval=true;
			}
			return retval;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		
	

	

		

		

		

	

	}
}