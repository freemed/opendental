package com.opendental.odweb.client.windows;

import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.event.dom.client.ClickHandler;
import com.google.gwt.http.client.Request;
import com.google.gwt.http.client.RequestBuilder;
import com.google.gwt.http.client.RequestCallback;
import com.google.gwt.http.client.RequestException;
import com.google.gwt.http.client.Response;
import com.google.gwt.http.client.URL;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.DialogBox;
import com.google.gwt.user.client.ui.HorizontalPanel;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.opendental.odweb.client.request.HttpRequestTest;
import com.opendental.odweb.client.ui.MsgBox;

public class WindowPatientSelect extends DialogBox {
	private Label labelHello;
	
	public WindowPatientSelect() {
		this.setText("Patient Select");
		this.setAnimationEnabled(true);
		this.setGlassEnabled(true);
		
		VerticalPanel vp=new VerticalPanel();
		HorizontalPanel hp=new HorizontalPanel();
		Button butOK=new Button("OK");
		butOK.addClickHandler(new butOK_Click());
		hp.add(butOK);
		Button butCancel=new Button("Cancel");
		butCancel.addClickHandler(new butCancel_Click());
		hp.add(butCancel);
		hp.setSpacing(5);
		Button butSearch=new Button("Search");
		butSearch.addClickHandler(new butSearch_Click());
		labelHello=new Label("Patient Select Loaded...");
		vp.add(labelHello);
		vp.add(butSearch);
		vp.add(hp);
		
		this.add(vp);
	}
	
	private void FillGrid() {
		// TODO Make this function accept a list of patients to fill a grid with
	}
	
	private void Close(){
		this.hide();
	}
	
	private class butSearch_Click implements ClickHandler {
		public void onClick(ClickEvent event) {
			//Make a call to the db to get a list of patients based on the patient entered fields. 
			String url=HttpRequestTest.WEBSERVICE_URL;
			url=URL.encode(url+"/ProcessRequest?dtoString=string");
			RequestBuilder builder = new RequestBuilder(RequestBuilder.GET, url);
			try {//Try catch is required around http request.
				builder.sendRequest(null, new RequestCallback(){
					public void onResponseReceived(Request request,Response response) 
					{	
						if (200 == response.getStatusCode()) {
							FillGrid();
						}	else	{
							MsgBox msg=new MsgBox("Error status text: "+ response.getStatusText()
								+"\r\nError status code:"+Integer.toString(response.getStatusCode())
								+"\r\nError text: "+response.getText());
							msg.show();
						}
					}
					public void onError(Request request, Throwable exception) {		
						MsgBox msg=new MsgBox("Error: "+exception.getMessage());
						msg.show();	
					}
				});
			}
			catch (RequestException e) {
				e.printStackTrace();
			}
		}
	}
	
	private class butOK_Click implements ClickHandler {
		public void onClick(ClickEvent event) {
			Close();
		}
	}
	
	private class butCancel_Click implements ClickHandler {
		public void onClick(ClickEvent event) {
			Close();
		}
	}

}
