package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.RunAsyncCallback;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.uibinder.client.UiHandler;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.opendentbusiness.tabletypes.Userod;
import com.opendental.odweb.client.ui.ModuleWidget;

public class ContrLogOn extends ModuleWidget {
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static ContrLogOnUiBinder uiBinder=GWT.create(ContrLogOnUiBinder.class);
	interface ContrLogOnUiBinder extends UiBinder<Widget, ContrLogOn> {
	}
	
	@UiField VerticalPanel panelContainer;
	@UiField TextBox textUser;
	@UiField TextBox textPassword;
	@UiField Button butOK;
	private LogOnHandler logOnHandler;
	
	public ContrLogOn(LogOnHandler logOnHandler) {
		this.logOnHandler=logOnHandler;
		uiBinder.createAndBindUi(this);
		this.add(panelContainer);
	}

	@UiHandler("butOK")
	void butOK_Click(ClickEvent event) {
		// TODO Verify the credentials somehow. 
		// TODO Pass the correct UserOD object.
		Userod user=new Userod();
		user.UserName="Jason";
		if(logOnHandler!=null) {//Should never be null.
			logOnHandler.onSuccess(user);
		}
	}

	public Widget onInitialize() {
		return null;
	}
	
	protected void asyncOnInitialize(final AsyncCallback<Widget> callback) {
		GWT.runAsync(ContrAppt.class, new RunAsyncCallback() {
			public void onFailure(Throwable error) {
				callback.onFailure(error);
			}
			public void onSuccess() {
				callback.onSuccess(onInitialize());
			}
		});
	}
	
	/**  */
	public interface LogOnHandler {
		void onSuccess(Userod user);
	}

}
