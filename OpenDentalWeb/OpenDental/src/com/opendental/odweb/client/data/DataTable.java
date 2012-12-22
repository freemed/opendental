package com.opendental.odweb.client.data;

import java.util.ArrayList;

public class DataTable {

	private String TableName;
	private ArrayList<DataColumn> Columns;
	private String[] data;
	private int numRows;
	
	public DataTable() {
		this("");
	}
	
	public DataTable(String tableName) {
		this(tableName,0,new ArrayList<DataColumn>());
	}
	
	public DataTable(String tableName,int rowCount,ArrayList<DataColumn> cols) {
		setTableName(tableName);
		Columns=cols;
		setNumRows(rowCount);
		data=new String[rowCount*cols.size()];
	}

	public String getTableName() {
		return TableName;
	}

	public void setTableName(String tableName) {
		TableName=tableName;
	}
	
	public int getNumRows() {
		return numRows;
	}

	public void setNumRows(int numRows) {
		this.numRows = numRows;
	}

	public String GetCell(int row,int col) {
//		if(row<0 || row>getNumRows()) {
//			throw new Exception("Row index is "+row+", but must be non-negative and less than "+getNumRows());
//		}
//		if(col<0 || col>Columns.size()) {
//			throw new Exception("Column index is "+col+", but must be non-negative and less than "+Columns.size());
//		}		
		return data[row*Columns.size()+col];
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
//		try {
//			return GetCell(row,column);
//		}
//		catch(Exception ex) {
//		}
//		return "";
		return GetCell(row,column);
	}

	public void SetCell(int row,int col,String val) {
//		if(row<0 || row>getNumRows()) {
//			throw new Exception("Row index is "+row+", but must be non-negative and less than "+getNumRows());
//		}
//		if(col<0 || col>Columns.size()) {
//			throw new Exception("Column index is "+col+", but must be non-negative and less than "+Columns.size());
//		}		
		data[row*Columns.size()+col]=val;
	}
	
}
