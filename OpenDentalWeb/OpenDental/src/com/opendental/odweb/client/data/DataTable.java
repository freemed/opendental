package com.opendental.odweb.client.data;

import java.util.ArrayList;

public class DataTable {

	private String TableName;
	public ArrayList<DataColumn> Columns;
	public ArrayList<DataRow> Rows;
	
	public DataTable() {
		Columns=new ArrayList<DataColumn>();
		Rows=new ArrayList<DataRow>();
		setTableName("");
	}
	
	public DataTable(String tableName) {
		Columns=new ArrayList<DataColumn>();
		Rows=new ArrayList<DataRow>();
		setTableName(tableName);
	}

	public String getTableName() {
		return TableName;
	}

	public void setTableName(String tableName) {
		TableName=tableName;
	}

	
}
