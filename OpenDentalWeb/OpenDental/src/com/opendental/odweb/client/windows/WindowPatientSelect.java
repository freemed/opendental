package com.opendental.odweb.client.windows;

import java.util.Date;

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
import com.google.gwt.user.client.ui.CheckBox;
import com.google.gwt.user.client.ui.DockPanel;
import com.google.gwt.user.client.ui.ListBox;
import com.google.gwt.user.client.ui.TextBox;
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
	@UiField TextBox textLName;
	@UiField TextBox textFName;
	@UiField TextBox textHmPhone;
	@UiField TextBox textAddress;
	@UiField TextBox textCity;
	@UiField TextBox textState;
	@UiField TextBox textSSN;
	@UiField TextBox textPatNum;
	@UiField TextBox textChartNumber;
	@UiField TextBox textBirthdate;
	@UiField TextBox textSubscriberID;
	@UiField TextBox textEmail;
	@UiField ListBox comboBillingType;
	@UiField ListBox comboSite;
	@UiField CheckBox checkGuarantors;
	@UiField CheckBox checkHideInactive;
	@UiField CheckBox checkShowArchived;
	@UiField Button butSearch;
	@UiField Button butGetAll;
	@UiField CheckBox checkRefresh;
	@UiField Button butAddPt;
	@UiField Button butAddAll;
	@UiField Button butOK;
	@UiField Button butCancel;
	
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static WindowPatientSelectUiBinder uiBinder=GWT.create(WindowPatientSelectUiBinder.class);
	interface WindowPatientSelectUiBinder extends UiBinder<Widget, WindowPatientSelect> {
	}
	
	public WindowPatientSelect() {		
		super("Patient Select");		
		gridMain=new ODGrid("Select Patient");
		gridMain.setHeightAndWidth(500,625);
		//Fills the @UiField objects.
		uiBinder.createAndBindUi(this);
		this.add(panelContainer);
		fillGrid();
	}
	
	/** Refreshes the patient grid with the information in the PatientTable.  Does nothing if PatientTable is null. */
	private void fillGrid() {
		gridMain.beginUpdate();
		gridMain.Columns.clear();
		ODGridColumn col=new ODGridColumn("LName",100);
		gridMain.Columns.add(col);
		col=new ODGridColumn("FName",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("MI",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("Pref Name",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("Age",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("SSN",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("Hm Phone",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("WkPhone",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("PatNum",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("ChartNum",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("Address",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("Status",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("Bill Type",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("City",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("State",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("Pri Prov",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("Birthdate",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("Site",80);
		gridMain.Columns.add(col);
		col=new ODGridColumn("Email",80);
		gridMain.Columns.add(col);
		gridMain.Rows.clear();
		ODGridRow row;
		for(int i=0;i<PatientTable.Rows.size();i++) {
			row=new ODGridRow();
			row.Cells.Add(PatientTable.getCellText(i,"LName"));
			row.Cells.Add(PatientTable.getCellText(i,"FName"));
			row.Cells.Add(PatientTable.getCellText(i,"MiddleI"));
			row.Cells.Add(PatientTable.getCellText(i,"Preferred"));
			row.Cells.Add(PatientTable.getCellText(i,"age"));
			row.Cells.Add(PatientTable.getCellText(i,"SSN"));
			row.Cells.Add(PatientTable.getCellText(i,"HmPhone"));
			row.Cells.Add(PatientTable.getCellText(i,"WkPhone"));
			row.Cells.Add(PatientTable.getCellText(i,"PatNum"));
			row.Cells.Add(PatientTable.getCellText(i,"ChartNumber"));
			row.Cells.Add(PatientTable.getCellText(i,"Address"));
			row.Cells.Add(PatientTable.getCellText(i,"PatStatus"));
			row.Cells.Add(PatientTable.getCellText(i,"BillingType"));
			row.Cells.Add(PatientTable.getCellText(i,"City"));
			row.Cells.Add(PatientTable.getCellText(i,"State"));
			row.Cells.Add(PatientTable.getCellText(i,"PriProv"));
			row.Cells.Add(PatientTable.getCellText(i,"Birthdate"));
			row.Cells.Add(PatientTable.getCellText(i,"site"));
			row.Cells.Add(PatientTable.getCellText(i,"Email"));
			gridMain.Rows.add(row);
		}
		gridMain.endUpdate();
	}
	
	/** Makes a request to the server for the patient data table based on the text boxes filled in. 
	 *  @param limit Adds a LIMIT restriction to the SQL query so that the query doesn't take as long. */
	private void request_GetPtDataTable(boolean limit) {
		int billingType=0;
//				if(comboBillingType.SelectedIndex!=0){
//					billingType=DefC.Short[(int)DefCat.BillingTypes][comboBillingType.SelectedIndex-1].DefNum;
//				}
		int siteNum=0;
//				if(!PrefC.GetBool(PrefName.EasyHidePublicHealth) && comboSite.SelectedIndex!=0) {
//					siteNum=SiteC.List[comboSite.SelectedIndex-1].SiteNum;
//				}
		Date birthdate=new Date();//PIn.Date(textBirthdate.Text);//this will frequently be 0001-01-01.
		int clinicNum=0;//all clinics
//				if(Security.CurUser.ClinicNum!=0 && Security.CurUser.ClinicIsRestricted){
//					clinicNum=Security.CurUser.ClinicNum;
//				}
		RequestBuilder builder=RemotingClient.GetRequestBuilder(
				Patients.getPtDataTable(limit, textLName.getText(), textFName.getText(), textHmPhone.getText(),
						textAddress.getText(), checkHideInactive.getValue(), textCity.getText(), textState.getText(),
						textSSN.getText(), textPatNum.getText(), textChartNumber.getText(), billingType,
						checkGuarantors.getValue(), checkShowArchived.getValue(), clinicNum, birthdate, siteNum, textSubscriberID.getText(), textEmail.getText()));
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
	
	/** If refresh while typing is checked, this will make a call to the database on each key stroke in any search by field. */
	@SuppressWarnings("unused")
	private void onDataEntered() {
		if(checkRefresh.getValue()) {
			request_GetPtDataTable(true);
		}
	}

	@UiHandler("butSearch")
	void butSearch_Click(ClickEvent event) {
		request_GetPtDataTable(true);
	}
	
	@UiHandler("butGetAll")
	void butGetAll_Click(ClickEvent event) {
		request_GetPtDataTable(false);
	}
	
	@UiHandler("butAddPt")
	void butAddPt_Click(ClickEvent event) {
		
	}
	
	@UiHandler("butAddAll")
	void butAddAll_Click(ClickEvent event) {
		
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
