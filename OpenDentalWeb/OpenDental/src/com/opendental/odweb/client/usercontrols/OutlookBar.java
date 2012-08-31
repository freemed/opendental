package com.opendental.odweb.client.usercontrols;

import com.google.gwt.event.dom.client.ClickEvent;
import com.google.gwt.event.dom.client.ClickHandler;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.opendental.odweb.client.ui.MsgBox;

public class OutlookBar extends Composite {
	private OutlookButton[] Buttons; 
	private VerticalPanel panel;
	public int SelectedIndex=-1;
	
	public OutlookBar(){
		panel=new VerticalPanel();
		Buttons=new OutlookButton[7];
		Buttons[0]=new OutlookButton("Appts",0,this);
		Buttons[1]=new OutlookButton("Family",1,this);
		Buttons[2]=new OutlookButton("Account",2,this);
		Buttons[3]=new OutlookButton("Treat' Plan",3,this);
		Buttons[4]=new OutlookButton("Chart",4,this);
		Buttons[5]=new OutlookButton("Images",5,this);
		Buttons[6]=new OutlookButton("Manage",6,this);
		//Add each button to the panel.
		for(int i=0;i<Buttons.length;i++){
			panel.add(Buttons[i]);
			Buttons[i].addClickHandler(new OnButtonClick());
		}
		//We have to call initWidget in the constructor because the class extends Composite. 
		initWidget(panel);
	}
	
	/**
	 * This will make all the module buttons pop "up" and then will the button with the hotIndex will be pushed down.
	 */
	public void ToggleModuleButtons(int imageIndex){
		Buttons[imageIndex].setDown(true);
		if(imageIndex==SelectedIndex){//User clicked on the current module.
			// TODO Refresh the selected module.
			return;
		}
		for(int i=0;i<Buttons.length;i++){
			if(i==imageIndex){
				continue;
			}
			Buttons[i].setDown(false);
		}
		SelectedIndex=imageIndex;
	}
	
	private class OnButtonClick implements ClickHandler{
		@Override
		public void onClick(ClickEvent event) {
			MsgBox msg=new MsgBox("You clicked on: "+Buttons[SelectedIndex].getCaption());
			msg.show();
		}
	}
	
	
}
