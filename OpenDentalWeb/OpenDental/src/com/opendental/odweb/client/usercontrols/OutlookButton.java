package com.opendental.odweb.client.usercontrols;

import com.google.gwt.cell.client.AbstractCell;
import com.google.gwt.safehtml.shared.SafeHtmlBuilder;

/** Simple cell container that holds the module buttons and will eventually hold the messaging buttons as well.
 *  It uses HtmlConstants to display the information within the AbstractCell. */
public class OutlookButton extends AbstractCell<OutlookButton> {
	private String caption;
	private int buttonIndex;
	// TODO Enhance OutlookButton to accept images for the module "buttons".
  //private Image image;
	// TODO Enhance to accept colors.
  //private CssColor color;
	
	/** Used to add messaging buttons.  Sets the button index to -1 so that we know it's not a module button. 
	 * @param caption The text that will show on the messaging button. */
	public OutlookButton(String caption) {
		this(caption,-1);
	}
	
	/** Used to add the main module buttons.  This constructor sets the Button Index and sets the caption.  We will be able to enhance this class to accept images. */
	public OutlookButton(String text,int index) {
		caption=text;
		buttonIndex=index;
	}
	
	public String getCaption() {
		return caption;
	}
	
	public int getButtonIndex() {
		return buttonIndex;
	}

	@Override
	public void render(Context context,OutlookButton value, SafeHtmlBuilder sb) {
		if(value==null) {
			return;
		}
		sb.appendHtmlConstant("<table>");
		// TODO Add the capabilities for adding images to the buttons here.
    sb.appendHtmlConstant("<tr><td style='font-size:95%;'>");
		//Simply add the caption of the Module to the Cell.		
		sb.appendEscaped(value.getCaption());
    sb.appendHtmlConstant("</td></tr></table>");
		
	}
}
