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
import com.google.gwt.user.client.ui.DockPanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.data.DataTable;
import com.opendental.odweb.client.datainterface.Patients;
import com.opendental.odweb.client.remoting.RemotingClient;
import com.opendental.odweb.client.remoting.Serializing;
import com.opendental.odweb.client.ui.*;

public class WindowPatientSelect extends ODWindow {
	private DataTable PatientTable=new DataTable();
	@UiField(provided=true) ODGrid gridMain;
	@UiField DockPanel panelContainer;
	@UiField Button butSearch;
	@UiField Button butCancel;
	
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static WindowPatientSelectUiBinder uiBinder = GWT.create(WindowPatientSelectUiBinder.class);
	interface WindowPatientSelectUiBinder extends UiBinder<Widget, WindowPatientSelect> {
	}
	
	public WindowPatientSelect() {
		super("Patient Select");
		//Fills the @UiField objects.
		//Fill the container panel that will hold everything on this window.
		gridMain=new ODGrid("Select Patient");
		gridMain.setHeightAndWidth(500,500);
		uiBinder.createAndBindUi(this);
		this.add(panelContainer);
		fillGrid();
	}
	
	/** Refreshes the patient grid with the information in the PatientTable.  Does nothing if PatientTable is null. */
	private void fillGrid() {
		gridMain.beginUpdate();
		gridMain.Columns.clear();
		ODGridColumn col=new ODGridColumn("PatNum",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("Test",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("Test2",80);
		gridMain.Columns.add(col);
		gridMain.Rows.clear();
		ODGridRow row;
		for(int i=0;i<PatientTable.Rows.size();i++) {
			row=new ODGridRow();
			row.Cells.Add(PatientTable.Rows.get(i).getCells().get(0).getText());
			row.Cells.Add(PatientTable.Rows.get(i).getCells().get(1).getText());
			row.Cells.Add(PatientTable.Rows.get(i).getCells().get(2).getText());
			gridMain.Rows.add(row);
		}
		gridMain.endUpdate();
	}

	@UiHandler("butSearch")
	void butSearch_Click(ClickEvent event) {
		RequestBuilder builder=RemotingClient.GetRequestBuilder(Patients.GetPtDataTableTest(2839,8321));
		try {//Try catch is required around http request.
			builder.sendRequest(null, new butSearch_RequestCallback());
		}
		catch (RequestException e) {
			MsgBox.show("Error: "+e.getMessage());
		}
	}
	
	private class butSearch_RequestCallback implements RequestCallback {		
		public void onResponseReceived(Request request, Response response) {
			if(response.getStatusCode()==200) {
				try {
					PatientTable=(DataTable)Serializing.getDeserializedObject(response.getText());
				} catch (Exception e) {
					MsgBox.show(e.getMessage());//This will be a more specific error.
				}
				fillGrid();
      } 
			else {
      	MsgBox.show("Error status text: "+response.getStatusText()
    			+"\r\nError status code: "+Integer.toString(response.getStatusCode())
    			+"\r\nError text: "+response.getText());
      }
		}
		
		public void onError(Request request, Throwable exception) {
			MsgBox.show("Error: "+exception.getMessage());
		}
	}
	
	@UiHandler("butOK")
	void butOK_Click(ClickEvent event) {
		this.hide();
	}
	
	@UiHandler("butCancel")
	void butCancel_Click(ClickEvent event) {
		this.hide();
	}

}
