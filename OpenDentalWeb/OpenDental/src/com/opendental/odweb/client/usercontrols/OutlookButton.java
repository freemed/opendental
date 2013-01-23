package com.opendental.odweb.client.usercontrols;

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
import com.google.gwt.user.client.ui.Composite;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.VerticalPanel;

/** Simple cell container that holds the module buttons and will eventually hold the messaging buttons as well.
 *  It uses HtmlConstants to display the information within the AbstractCell. */
public class OutlookButton extends Composite implements HasMouseDownHandlers,HasMouseOutHandlers,HasMouseOverHandlers {
	
	private VerticalPanel contentPanel;
	private boolean isSelected;
	private String caption;
	private int buttonIndex;
	private ButtonClickHandler buttonClickHandler;
	private Label labelText;
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
		caption=text;
		buttonIndex=index;
		labelText=new Label(caption);
		//Make it so that the cursor does not change to the TEXT cursor when hovering over text.
		labelText.getElement().getStyle().setCursor(Cursor.DEFAULT);
		//Set the click handler.
		buttonClickHandler=clickHandler;
		contentPanel=new VerticalPanel();
		contentPanel.add(labelText);
		initWidget(contentPanel);
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

	public void setSelected(boolean isSelected) {
		this.isSelected = isSelected;
	}

	/** Returns the index of the button that was clicked. */
	public interface ButtonClickHandler {
		void onClick(int buttonIndex);
	}
	
	private class mouseDownHandler implements MouseDownHandler {
		public void onMouseDown(MouseDownEvent event) {
			if(!isSelected()){//Already suppressed.
				buttonClickHandler.onClick(buttonIndex);			
			}
		}
	}
	
	private class mouseOverHandler implements MouseOverHandler {
		public void onMouseOver(MouseOverEvent event) {
			// TODO Add code so that the panel gets a little outline so that the user can tell they are hovering over the module button.
			if(!isSelected()) {
				//Change the CSS style so that the panel has an outline.
			}
		}
	}
	
	private class mouseOutHandler implements MouseOutHandler {
		public void onMouseOut(MouseOutEvent event) {
			// TODO Add code to change the button to remove the hover affect.
			if(!isSelected()) {
				//Change the CSS style back to normal.
			}
		}
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
