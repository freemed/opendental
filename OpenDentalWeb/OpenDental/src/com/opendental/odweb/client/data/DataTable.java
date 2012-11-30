package com.opendental.odweb.client.data;

import java.util.ArrayList;

public class DataTable {

	private String TableName;
	public ArrayList<DataColumn> Columns;
	public ArrayList<DataRow> Rows;
	
	public DataTable() {
		Columns=new ArrayList<DataColumn>();
		Rows=new ArrayList<DataRow>();
		SetTableName("");
	}
	
	public DataTable(String tableName) {
		Columns=new ArrayList<DataColumn>();
		Rows=new ArrayList<DataRow>();
		SetTableName(tableName);
	}

	public String GetTableName() {
		return TableName;
	}

	public void SetTableName(String tableName) {
		TableName=tableName;
	}

	
}
