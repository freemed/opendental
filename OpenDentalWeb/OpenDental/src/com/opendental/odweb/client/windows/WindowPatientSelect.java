package com.opendental.odweb.client.windows;

import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.event.dom.client.ClickHandler;
import com.google.gwt.http.client.Request;
import com.google.gwt.http.client.RequestBuilder;
import com.google.gwt.http.client.RequestCallback;
import com.google.gwt.http.client.RequestException;
import com.google.gwt.http.client.Response;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.DialogBox;
import com.google.gwt.user.client.ui.HorizontalPanel;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.opendental.odweb.client.datainterface.Patients;
import com.opendental.odweb.client.remoting.RemotingClient;
import com.opendental.odweb.client.ui.MsgBox;

public class WindowPatientSelect extends DialogBox {
	
	public WindowPatientSelect() {
		this.setText("Patient Select");
		this.setAnimationEnabled(true);
		this.setGlassEnabled(true);
		
		VerticalPanel vp=new VerticalPanel();
		HorizontalPanel hp=new HorizontalPanel();
		Button butOK=new Button("Search");
		butOK.addClickHandler(new butOK_Click());
		hp.add(butOK);
		Button butClose=new Button("Close");
		butClose.addClickHandler(new butClose_Click());
		hp.add(butClose);
		hp.setSpacing(5);
		vp.add(hp);
		
		this.add(vp);
	}
	
	private void FillGrid() {
		// TODO Make this function accept a list of patients to fill a grid with
	}
	
	private class butOK_Click implements ClickHandler {
		public void onClick(ClickEvent event) {
			RequestBuilder builder=RemotingClient.GetRequestBuilder(Patients.GetPtDataTable());
			try {//Try catch is required around http request.
				builder.sendRequest(null, new RequestCallback(){
					public void onResponseReceived(Request request,Response response) {	
						if(200 == response.getStatusCode()) {
							//The response might be a DtoException.  
							// TODO Figure out a generalized way to deserialize responses from the server.
							FillGrid();
						}	else {
							MsgBox.Show("Error status text: "+ response.getStatusText()
								+"\r\nError status code:"+Integer.toString(response.getStatusCode())
								+"\r\nError text: "+response.getText());
						}
					}
					public void onError(Request request, Throwable exception) {		
						MsgBox.Show("Error: "+exception.getMessage());
					}
				});
			}
			catch (RequestException e) {
				MsgBox.Show("Error: "+e.getMessage());
			}
		}
	}
	
	private class butClose_Click implements ClickHandler {
		public void onClick(ClickEvent event) {
			Close();
		}
	}
	
	private void Close(){
		this.hide();
	}

}
