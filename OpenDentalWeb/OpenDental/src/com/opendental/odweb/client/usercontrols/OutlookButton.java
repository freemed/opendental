package com.opendental.odweb.client.usercontrols;

import com.google.gwt.core.client.GWT;
import com.google.gwt.dom.client.Style.Cursor;
import com.google.gwt.event.dom.client.HasMouseDownHandlers;
import com.google.gwt.event.dom.client.HasMouseOutHandlers;
import com.google.gwt.event.dom.client.HasMouseOverHandlers;
import com.google.gwt.event.dom.client.MouseDownEvent;
import com.google.gwt.event.dom.client.MouseDownHandler;
import com.google.gwt.event.dom.client.MouseOutEvent;
import com.google.gwt.event.dom.client.MouseOutHandler;
import com.google.gwt.event.dom.client.MouseOverEvent;
import com.google.gwt.event.dom.client.MouseOverHandler;
import com.google.gwt.event.shared.HandlerRegistration;
import com.google.gwt.resources.client.CssResource;
import com.google.gwt.uibinder.client.UiBinder;
import com.google.gwt.uibinder.client.UiField;
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.DecoratorPanel;
import com.google.gwt.user.client.ui.HasEnabled;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.Widget;

/** Simple cell container that holds the module buttons and will eventually hold the messaging buttons as well.
 *  It uses HtmlConstants to display the information within the AbstractCell. */
public class OutlookButton extends Composite implements HasMouseDownHandlers,HasMouseOutHandlers,HasMouseOverHandlers,HasEnabled {
	private static OutlookButtonUiBinder uiBinder = GWT.create(OutlookButtonUiBinder.class);
	interface OutlookButtonUiBinder extends UiBinder<Widget, OutlookButton> {
	}
	
	/** This is going to allow programmatic access to specified UiBinder styles. */
	interface ButtonStyle extends CssResource {
		String buttonStandard();
		String buttonSelected();
		String buttonHover();
	}
	/** RowStyle is strictly used to refer to the CSS portion of the UiBinder file programmatically. */
	@UiField ButtonStyle style;
	
	@UiField DecoratorPanel contentPanel;
	@UiField Label labelText;
	private boolean isSelected;
	private String caption;
	private int buttonIndex;
	private ButtonClickHandler buttonClickHandler;
	private boolean isEnabled;
	// TODO Enhance OutlookButton to accept images for the module "buttons".
  //private Image image;
	// TODO Enhance to accept colors.
  //private CssColor color;
	
	/** Used to add messaging buttons.  Sets the button index to -1 so that we know it's not a module button. 
	 * @param caption The text that will show on the messaging button. */
	public OutlookButton(String caption,ButtonClickHandler clickHandler) {
		this(caption,-1,clickHandler);
	}
	
	/** Used to add the main module buttons.  This constructor sets the Button Index and sets the caption.  We will be able to enhance this class to accept images. */
	public OutlookButton(String text,int index,ButtonClickHandler clickHandler) {
		initWidget(uiBinder.createAndBindUi(this));
		caption=text;
		buttonIndex=index;
		labelText.setText(caption);
		//Make it so that the cursor does not change to the TEXT cursor when hovering over text.
		labelText.getElement().getStyle().setCursor(Cursor.DEFAULT);
		// TODO: Change the border of the decorator panel by creating my own pictures to make a rounded edge.  But for now just use CSS.
		contentPanel.setStyleName(style.buttonStandard());
		//Set the click handler.
		buttonClickHandler=clickHandler;
		//Add mouse handlers.
		this.addMouseDownHandler(new mouseDownHandler());
		this.addMouseOverHandler(new mouseOverHandler());
		this.addMouseOutHandler(new mouseOutHandler());
	}
	
	public String getCaption() {
		return caption;
	}
	
	public int getButtonIndex() {
		return buttonIndex;
	}
	
	public boolean isSelected() {
		return isSelected;
	}

	public void setSelected(boolean selected) {
		isSelected=selected;
	}

	public boolean isEnabled() {
		return isEnabled;
	}

	public void setEnabled(boolean enabled) {
		isEnabled=enabled;
	}
	
	/** Called when a user clicks a button.  This method can be called to simulate the user physically clicking this button as well.  Example is when a user logs in, we simulate them clicking the Appts module. */
	public void clickButton() {
		removeButtonStyles();
		//Set the CSS for this button to selected.
		contentPanel.setStyleName(style.buttonSelected());
		buttonClickHandler.onClick(buttonIndex);
	}

	/** Returns the index of the button that was clicked. */
	public interface ButtonClickHandler {
		void onClick(int buttonIndex);
	}
	
	private class mouseDownHandler implements MouseDownHandler {
		public void onMouseDown(MouseDownEvent event) {
			if(!isEnabled) {
				return;
			}
			if(!isSelected()){//Already suppressed.
				clickButton();
			}
		}
	}
	
	private class mouseOverHandler implements MouseOverHandler {
		public void onMouseOver(MouseOverEvent event) {
			if(!isEnabled) {
				return;
			}
			if(!isSelected()) {
				removeButtonStyles();
				//Change the CSS style so that the panel has an outline.
				contentPanel.setStyleName(style.buttonHover());
			}
		}
	}
	
	private class mouseOutHandler implements MouseOutHandler {
		public void onMouseOut(MouseOutEvent event) {
			if(!isEnabled) {
				return;
			}
			if(!isSelected()) {
				removeButtonStyles();
				//Change the CSS style back to normal.
				contentPanel.setStyleName(style.buttonStandard());
			}
		}
	}
	
	/** Gets called after a module button gets clicked.  This will make sure the correct module is showing selected. */
	public void refreshButtonStyles() {
		removeButtonStyles();
		if(isSelected()) {
			contentPanel.setStyleName(style.buttonSelected());
		}
		else {
			contentPanel.setStyleName(style.buttonStandard());
		}
	}
	
	private void removeButtonStyles() {
		contentPanel.removeStyleName(style.buttonHover());
		contentPanel.removeStyleName(style.buttonSelected());
		contentPanel.removeStyleName(style.buttonStandard());
	}
	
	public HandlerRegistration addMouseDownHandler(MouseDownHandler handler) {
		return contentPanel.addDomHandler(handler, MouseDownEvent.getType());
	}

	public HandlerRegistration addMouseOverHandler(MouseOverHandler handler) {
		return contentPanel.addDomHandler(handler, MouseOverEvent.getType());
	}

	public HandlerRegistration addMouseOutHandler(MouseOutHandler handler) {
		return contentPanel.addDomHandler(handler, MouseOutEvent.getType());
	}
}
