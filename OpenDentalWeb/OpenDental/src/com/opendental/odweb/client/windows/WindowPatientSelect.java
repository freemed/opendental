package com.opendental.odweb.client.windows;

import com.google.gwt.core.client.GWT;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.http.client.Request;
import com.google.gwt.http.client.RequestBuilder;
import com.google.gwt.http.client.RequestCallback;
import com.google.gwt.http.client.RequestException;
import com.google.gwt.http.client.Response;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.uibinder.client.UiHandler;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.HTMLPanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.data.DataTable;
import com.opendental.odweb.client.datainterface.Patients;
import com.opendental.odweb.client.remoting.RemotingClient;
import com.opendental.odweb.client.remoting.Serializing;
import com.opendental.odweb.client.ui.*;

public class WindowPatientSelect extends ODWindow {
	private DataTable PatientTable;
	private ODGrid GridMain;
	@UiField HTMLPanel panelContainer;
	@UiField Button butSearch;
	@UiField Button butClose;
	
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static WindowPatientSelectUiBinder uiBinder = GWT.create(WindowPatientSelectUiBinder.class);
	interface WindowPatientSelectUiBinder extends UiBinder<Widget, WindowPatientSelect> {
	}
	
	public WindowPatientSelect() {
		super("Patient Select");
		//Fills the @UiField objects.
		uiBinder.createAndBindUi(this);
		//Fill the container panel that will hold everything on this window.
		GridMain=new ODGrid("Patient Data Table Title");
		panelContainer.add(GridMain);
		panelContainer.add(butSearch);
		panelContainer.add(butClose);
		this.add(panelContainer);
	}
	
	/** Refreshes the patient grid with the information in the PatientTable.  Does nothing if PatientTable is null. */
	private void FillGrid() {
		if(PatientTable==null) {
			return;
		}
		GridMain.BeginUpdate();
		GridMain.Columns.clear();
		ODGridColumn col=new ODGridColumn("PatNum",80);
		GridMain.Columns.add(col);
		col=new ODGridColumn("Test",80);
		GridMain.Columns.add(col);
		col=new ODGridColumn("Test2",80);
		GridMain.Columns.add(col);
		GridMain.Rows.clear();
		ODGridRow row;
		for(int i=0;i<PatientTable.Rows.size();i++) {
			row=new ODGridRow();
			row.Cells.Add(PatientTable.Rows.get(i).GetCells().get(0).GetText());
			row.Cells.Add(PatientTable.Rows.get(i).GetCells().get(1).GetText());
			row.Cells.Add(PatientTable.Rows.get(i).GetCells().get(2).GetText());
			GridMain.Rows.add(row);
		}
		GridMain.EndUpdate();
	}

	@UiHandler("butSearch")
	void butSearch_Click(ClickEvent event) {
		RequestBuilder builder=RemotingClient.GetRequestBuilder(Patients.GetPtDataTableTest(2839,8321));
		try {//Try catch is required around http request.
			builder.sendRequest(null, new butSearch_RequestCallback());
		}
		catch (RequestException e) {
			MsgBox.Show("Error: "+e.getMessage());
		}
	}
	
	private class butSearch_RequestCallback implements RequestCallback {		
		public void onResponseReceived(Request request, Response response) {
			if(response.getStatusCode()==200) {
				try {
					PatientTable=(DataTable)Serializing.GetDeserializedObject(response.getText());
				} catch (Exception e) {
					MsgBox.Show(e.getMessage());//This will be a more specific error.
				}
				FillGrid();
      } 
			else {
      	MsgBox.Show("Error status text: "+response.getStatusText()
    			+"\r\nError status code: "+Integer.toString(response.getStatusCode())
    			+"\r\nError text: "+response.getText());
      }
		}
		
		public void onError(Request request, Throwable exception) {
			MsgBox.Show("Error: "+exception.getMessage());
		}
	}
	
	@UiHandler("butClose")
	void butClose_Click(ClickEvent event) {
		this.hide();
	}

}
