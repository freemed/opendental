package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.GWT;
import com.google.gwt.core.client.RunAsyncCallback;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.Widget;
import com.opendental.odweb.client.ui.ModuleWidget;
import com.opendental.odweb.client.ui.ODGrid;

public class ContrFamily extends ModuleWidget {
	//These lines need to be in every class that uses UiBinder.  This is what makes this class point to it's respective ui.xml file. 
	private static ContrFamilyUiBinder uiBinder=GWT.create(ContrFamilyUiBinder.class);
	interface ContrFamilyUiBinder extends UiBinder<Widget, ContrFamily> {
	}
	
	@UiField SimplePanel panelContainer;
	@UiField(provided=true) ODGrid gridFamily;
	@UiField(provided=true) ODGrid gridPat;
	@UiField Button butAdd;
	
	public ContrFamily() {
		//Instantiate the grids.
		gridFamily=new ODGrid("Family Members");
		gridFamily.setWidthAndHeight(300, 100);
		gridPat=new ODGrid("Patient Information");
		gridPat.setWidthAndHeight(200, 500);
		uiBinder.createAndBindUi(this);
		this.add(panelContainer);
	}

	public Widget onInitialize() {
		return null;
	}
	
	protected void asyncOnInitialize(final AsyncCallback<Widget> callback) {
		GWT.runAsync(ContrFamily.class, new RunAsyncCallback() {
	    public void onFailure(Throwable error) {
	      callback.onFailure(error);
	    }
	    public void onSuccess() {
	  	  callback.onSuccess(onInitialize());
	    }
		});
	}

}
