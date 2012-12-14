package com.opendental.odweb.client.usercontrols;

import java.util.ArrayList;

import com.google.gwt.user.cellview.client.CellList;
import com.google.gwt.view.client.SingleSelectionModel;

public class OutlookBar extends CellList<OutlookButton> {
	public ArrayList<Integer> selectedIndicies=new ArrayList<Integer>();
	public ArrayList<OutlookButton> Buttons;
	
	/**  */
	public OutlookBar(SingleSelectionModel<OutlookButton> selectionModel) {
		//Create a CellList of outlook buttons.
		super(new OutlookButton(""));//I don't know why this is required but it is.  Passing an empty OutlookButton should be harmless and quick.
		//Fill the button list with the main modules.
		Buttons=GetModuleButtons();
		// TODO Probably put the code here to retrieve messaging buttons.
		this.setSelectionModel(selectionModel);
		this.setPageSize(30);//This is a larger number because it can have the messaging buttons below the modules. 
    this.setKeyboardPagingPolicy(KeyboardPagingPolicy.INCREASE_RANGE);
    this.setKeyboardSelectionPolicy(KeyboardSelectionPolicy.BOUND_TO_SELECTION);
    this.setRowData(Buttons);
	}
	
	/**  */
	private ArrayList<OutlookButton> GetModuleButtons() {
		ArrayList<OutlookButton> buttonList=new ArrayList<OutlookButton>();
		buttonList.add(new OutlookButton("Appts",0));
		buttonList.add(new OutlookButton("Family",1));
		buttonList.add(new OutlookButton("Account",2));
		buttonList.add(new OutlookButton("Treat' Plan",3));
		buttonList.add(new OutlookButton("Chart",4));
		buttonList.add(new OutlookButton("Images",5));
		buttonList.add(new OutlookButton("Manage",6));
		return buttonList;
	}
	
	// TODO Add methods for manipulating messaging buttons... eventually.
	
	
	
}
