package com.opendental.opendentbusiness.data;

import java.util.ArrayList;

public class DataTable {

	private String TableName;
	public ArrayList<DataColumn> Columns;
	public ArrayList<DataRow> Rows;
	
	public DataTable() {
		this("");
	}
	
	public DataTable(String tableName) {
		this(tableName,new ArrayList<DataColumn>(),new ArrayList<DataRow>());
	}
	
	public DataTable(String tableName,ArrayList<DataColumn> columns) {
		this(tableName,columns,new ArrayList<DataRow>());		
	}
	
	public DataTable(String tableName,ArrayList<DataColumn> columns,ArrayList<DataRow> rows) {
		setTableName(tableName);
		Columns=columns;
		Rows=rows;
	}

	public String getTableName() {
		return TableName;
	}

	public void setTableName(String tableName) {
		TableName=tableName;
	}
	
	/** Gets the text in the row with the corresponding column header.
	 *  This is just a helper function designed to get the text of a certain cell in a certain row under a certain column heading.  
	 *  The goal is to make a Java equivalent to C#'s "table.Rows[i]["ColName"].ToString();" 
	 *  @param row The index of the row to use.
	 *  @param colName The name of the column to use. */
	public String getCellText(int row,String colName) {
		int column=-1;
		//Figure out the index of the column which has the matching heading.
		for(int i=0;i<Columns.size();i++) {
			if(Columns.get(i).getHeading().equals(colName)) {
				column=i;
				break;
			}
		}
		//Return the text in the cell at the given row with the column name. 
		return Rows.get(row).getCells().get(column).getText();
	}

	
}