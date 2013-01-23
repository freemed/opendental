package com.opendental.odweb.client.usercontrols;

import java.util.ArrayList;

import java_cup.internal_error;

import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.opendental.odweb.client.usercontrols.OutlookButton.ButtonClickHandler;

public class OutlookBar extends SimplePanel {
	
	private ArrayList<Integer> selectedIndices=new ArrayList<Integer>();
	private ArrayList<OutlookButton> Buttons;
	private OutlookBarClickHandler clickHandler;
	private VerticalPanel contentPanel;
	
	/**  */
	public OutlookBar(OutlookBarClickHandler outlookClickHandler) {
		contentPanel=new VerticalPanel();
		//Fill the button list with the main modules.
		Buttons=GetModuleButtons();
		refreshOutlookBar();
		this.add(contentPanel);
		clickHandler=outlookClickHandler;
		// TODO Probably put the code here to retrieve messaging buttons.
	}
	
	/** Fills Buttons with the module buttons. */
	private ArrayList<OutlookButton> GetModuleButtons() {
		ArrayList<OutlookButton> buttonList=new ArrayList<OutlookButton>();
		buttonList.add(new OutlookButton("Appts",0,new outlookButton_Click()));
		buttonList.add(new OutlookButton("Family",1,new outlookButton_Click()));
		buttonList.add(new OutlookButton("Account",2,new outlookButton_Click()));
		buttonList.add(new OutlookButton("Treat' Plan",3,new outlookButton_Click()));
		buttonList.add(new OutlookButton("Chart",4,new outlookButton_Click()));
		buttonList.add(new OutlookButton("Images",5,new outlookButton_Click()));
		buttonList.add(new OutlookButton("Manage",6,new outlookButton_Click()));
		return buttonList;
	}
	
	public void refreshOutlookBar() {
		contentPanel.clear();
		for(int i=0;i<Buttons.size();i++) {
			contentPanel.add(Buttons.get(i));
		}
	}
	
	/** This sets each outlook button's isSelected accordingly so that the mouse methods work correctly in showing the hovering etc. */
	private void setSelectedButton(int buttonIndex) {
		for(int i=0;i<Buttons.size();i++) {
			Buttons.get(i).setSelected(false);
		}
		Buttons.get(buttonIndex).setSelected(true);
	}
	
	private class outlookButton_Click implements ButtonClickHandler {
		public void onClick(int buttonIndex) {
			//This should get enhanced when we add messaging buttons.
			selectedIndices.clear();
			selectedIndices.add(buttonIndex);
			setSelectedButton(buttonIndex);
			clickHandler.onClick(selectedIndices);
		}
	}
	
	/** Returns the selected indices of all the buttons that are clicked. There will typically be one index in the list for the modules.  But this will be enhanced to have the messaging buttons on them. */
	public interface OutlookBarClickHandler {
		void onClick(ArrayList<Integer> selectedIndices);
	}
	
	// TODO Add methods for manipulating messaging buttons... eventually.
	
	
	
}
