package com.opendental.odweb.client.usercontrols;

import com.google.gwt.user.client.Command;
import com.google.gwt.user.client.ui.MenuBar;
import com.google.gwt.user.client.ui.MenuItem;
import com.opendental.odweb.client.windows.WindowPatientSelect;

public class MenuBarMainPatient extends MenuBar{
	
	public MenuBarMainPatient(){
		this.setWidth("auto");
		this.setAnimationEnabled(true);
		
		MenuItem menuItemSelectPatient = new MenuItem("Select Patient", false, new SelectPatient_Command());
		this.addItem(menuItemSelectPatient);
		
		MenuItem menuItemCommlog = new MenuItem("Commlog", false, new Commlog_Command());
		this.addItem(menuItemCommlog);
	}
	
	private class SelectPatient_Command implements Command{
		public void execute() {
			WindowPatientSelect FormPS=new WindowPatientSelect();
			FormPS.show();
			FormPS.center();
		}
	}
	
	private class Commlog_Command implements Command{
		public void execute() {
		}
	}

}
