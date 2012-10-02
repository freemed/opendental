package com.opendental.odweb.client.windows;

import java.lang.reflect.InvocationTargetException;

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
import com.opendental.odweb.client.datainterface.Accounts;
import com.opendental.odweb.client.remoting.*;
import com.opendental.odweb.client.request.HttpRequestTest;
import com.opendental.odweb.client.tabletypes.Account;
import com.opendental.odweb.client.tabletypes.Account.AccountType;
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
		Button butSerialize=new Button("Serialize");
		butSerialize.addClickHandler(new butSerialize_Click());
		Button butDeserialize=new Button("Deserialize");
		butDeserialize.addClickHandler(new butDeserialize_Click());
		labelHello=new Label("Patient Select Loaded...");
		vp.add(labelHello);
		vp.add(butSerialize);
		vp.add(butDeserialize);
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
	
	private class butSerialize_Click implements ClickHandler {
		public void onClick(ClickEvent event) {
			Accounts.GetListLong();
			Accounts.GetListShort();
			Account account=new Account();
			account.AccountNum=1;
			account.AccountColor=-360334;
			account.AcctType=AccountType.Income;
			account.BankNumber="12345678";
			account.Description="Income < > & ^ ! = [ ] ? ' \" </Description> Account Description";			
			MsgBox.Show(account.SerializeToXml());
		}
	}
	
	private class butDeserialize_Click implements ClickHandler {
		public void onClick(ClickEvent event) {
			String xml="<Account><AccountNum>1</AccountNum><Description>Income Account Description</Description><AcctType>3</AcctType><BankNumber>12345678</BankNumber><Inactive>0</Inactive><AccountColor>-356789</AccountColor></Account>";
			Account account=new Account();
			try {
				account.DeserializeFromXml(xml);
			} catch (Exception e) {
				e.printStackTrace();
			}
			MsgBox msg=new MsgBox("AcctNum:"+Integer.toString(account.AccountNum)+"\r\n"
					+"Desc:"+account.Description+"\r\n"
					+"Type:"+account.AcctType.toString()+"\r\n"
					+"BankNum:"+account.BankNumber+"\r\n"
					+"Inactive:"+account.Inactive+"\r\n"
					+"Color:"+Integer.toString(account.AccountColor)+"\r\n");
			msg.show();
		}
	}
	
	private class butSearch_Click implements ClickHandler {
		public void onClick(ClickEvent event) {
			//Make a call to the db to get a list of patients based on the patient entered fields. 
			String url=HttpRequestTest.WEBSERVICE_URL;
			url=URL.encode(url+"/ProcessRequest?dtoString=string");
			RequestBuilder builder = new RequestBuilder(RequestBuilder.GET, url);
			try {//Try catch is required around http request.
				builder.sendRequest(null, new RequestCallback(){
					public void onResponseReceived(Request request,Response response) {	
						if (200 == response.getStatusCode()) {
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
				e.printStackTrace();
			}
		}
	}
	
	private class butOK_Click implements ClickHandler {
		public void onClick(ClickEvent event) {
			Object obj=new Account();
			java.lang.reflect.Method method = null;
			try {
				method=obj.getClass().getMethod("SerializeToXml");
			}
			catch (SecurityException e){}
			catch (NoSuchMethodException e){}
			try{
				method.invoke(obj);
			}
			catch(IllegalArgumentException | IllegalAccessException | InvocationTargetException e){}
			MsgBox.Show("");
		}
	}
	
	private class butCancel_Click implements ClickHandler {
		public void onClick(ClickEvent event) {
			Close();
		}
	}

}
