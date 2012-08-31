package com.opendental.odweb.client.mainmodules;

import com.google.gwt.core.client.EntryPoint;
import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.event.dom.client.ClickHandler;
import com.google.gwt.user.client.ui.FocusPanel;
import com.google.gwt.user.client.ui.HorizontalPanel;
import com.google.gwt.user.client.ui.RootPanel;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Label;
import com.opendental.odweb.client.usercontrols.MenuBarMain;
import com.opendental.odweb.client.usercontrols.MenuBarMainPatient;
import com.opendental.odweb.client.usercontrols.LabelMainTitle;
import com.opendental.odweb.client.usercontrols.OutlookBar;

public class WindowOpenDental implements EntryPoint {

	public OutlookBar MyOutlookBar;
	private SimplePanel ModuleCur;
	
	public void onModuleLoad() {
		try{
			//A loading label will be showing until the app has loaded.
			RootPanel.get("loading").setVisible(false);
			
			VerticalPanel vertPanelMain=new VerticalPanel();
			vertPanelMain.add(new LabelMainTitle());
			
			RootPanel.get("mainPanel").add(vertPanelMain);
			vertPanelMain.add(new MenuBarMain());
			
			HorizontalPanel hp=new HorizontalPanel();
			FocusPanel outlookBarPanel=new FocusPanel();//Using a focus panel only to catch a click handler.
			MyOutlookBar=new OutlookBar();
			outlookBarPanel.add(MyOutlookBar);
			outlookBarPanel.addClickHandler(new MyOutlookBar_ButtonClicked());
			hp.add(outlookBarPanel);
			
			VerticalPanel vertPanelModules=new VerticalPanel();
			vertPanelModules.add(new MenuBarMainPatient());
			ModuleCur=new SimplePanel(); 
			vertPanelModules.add(ModuleCur);
			hp.add(vertPanelModules);
			
			vertPanelMain.add(hp);			
		}
		catch(Exception e){
			e.printStackTrace();
			VerticalPanel panel=new VerticalPanel();
			panel.add(new Label("Error"));
			panel.add(new Label(e.getMessage()));
			RootPanel.get("mainPanel").add(panel);
		}
	}
	
	private class MyOutlookBar_ButtonClicked implements ClickHandler{		
		@Override
		public void onClick(ClickEvent event) {
			switch(MyOutlookBar.SelectedIndex){
				case 0://Appointment
					break;
				case 1://Family
					break;
				case 2://Account
					break;
				case 3://Treat Plan
					break;
				case 4://Chart
					break;
				case 5://Images
					break;
				case 6://Manage
					break;
			}
		}
	}
}
